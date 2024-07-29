using Common.DTOs;
using Common.Helpers;
using Common.Helplers;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;

namespace Infrastructure.Repositories;
public class UserRepository : IUserRepository
{
    private readonly AppDbContext appDbContext;
    private readonly IWebHostEnvironment webHostEnvironment;
    private readonly IConfiguration configuration;

    public UserRepository(AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
    {
        this.appDbContext = appDbContext;
        this.webHostEnvironment = webHostEnvironment;
        this.configuration = configuration;
    }

    public async Task<int> DeleteAsync(Guid id)
    {
        var user = await appDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (user == null) throw new BadHttpRequestException("User does not exist");
        string pathDelete = "";
        if (!string.IsNullOrWhiteSpace(user.Image))
        {
            pathDelete = Path.Combine(webHostEnvironment.WebRootPath, "user", user.Image!);

        }
        appDbContext.Users.Remove(user);
        var result = await appDbContext.SaveChangesAsync();
        if (result > 0) File.Delete(pathDelete);
        return result;
    }

    public async Task<int> DeleteImageAsync(Guid id)
    {
        var user = await appDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (user == null) throw new BadHttpRequestException("User does not exist");
        if (!string.IsNullOrWhiteSpace(user.Image))
        {
            var pathDelete = Path.Combine(webHostEnvironment.WebRootPath, "users", user.Image);
            File.Delete(pathDelete);
            user.Image = string.Empty;
            appDbContext.Entry(user).State = EntityState.Modified;
            return await appDbContext.SaveChangesAsync();
        }
        return 0;
    }

    public async Task<List<User>?> GetAllAsync()
    {
        return await appDbContext.Users.Where(x => x.Role!.Id != 1).ToListAsync();
    }

    public async Task<List<User>?> GetByRoleAsync(string name)
    {
        return await appDbContext.Users.Where(x => x.Role!.Name.ToLower().Contains(name.ToLower().Trim())).ToListAsync();

    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await appDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<PagedListDto<User>?> GetListAsync(FilterDto filter)
    {
        IQueryable<User> query;
        if (string.IsNullOrWhiteSpace(filter.Keyword))
        {
            query = appDbContext.Users.Where(x => x.Role!.Id != 1);
        }
        else
        {
            filter.Keyword = filter.Keyword.Trim().ToLower();
            query = appDbContext.Users.Where(x => x.Role!.Id != 1 && (x.FullName.ToLower().Contains(filter.Keyword) ||
                                                  x.PhoneNumber.Contains(filter.Keyword) ||
                                                  x.Username.ToLower().Contains(filter.Keyword) ||
                                                  x.Email.ToLower().Contains(filter.Keyword) ||
                                                  x.Address.ToLower().Contains(filter.Keyword) ||
                                                  x.DateOfBirth.ToString().Contains(filter.Keyword) ||
                                                  x.CreatedDate.ToString().Contains(filter.Keyword)
                                                  ));
        }
        if (!string.IsNullOrWhiteSpace(filter.SortColumn))
        {
            filter.SortColumn = filter.SortColumn.Trim();
            StringBuilder orderQueryBuilder = new StringBuilder();
            PropertyInfo[] propertyInfo = typeof(User).GetProperties();
            var property = propertyInfo.FirstOrDefault(x => x.Name.Equals(filter.SortColumn, StringComparison.OrdinalIgnoreCase));
            var orderBy = filter.IsDescending ? "descending" : "ascending";
            if (property != null)
            {
                orderQueryBuilder.Append($"{property.Name.ToString()} {orderBy}");
            }
            string orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
            if (!string.IsNullOrWhiteSpace(orderQuery))
            {
                query = query.OrderBy(orderQuery);
            }
            else
            {
                query = query.OrderBy(a => a.Id);
            }
        }
        else
        {
            if (filter.IsDescending) query = query.OrderByDescending(a => a.Id);
            else
            {
                query = query.OrderBy(a => a.Id);
            }
        }
        if (filter.PageSize == -1)
        {
            var data = await query.ToListAsync();
            return new PagedListDto<User>()
            {
                TotalCount = data!.Count(),
                PageSize = filter.PageSize,
                PageIndex = filter.PageIndex,
                Data = data
            };
        }
        var totalCount = await query.CountAsync();
        var totalPage = (int)Math.Ceiling(totalCount / (double)filter.PageSize);
        var pageUsers = await query.Skip((filter.PageIndex - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
        return new PagedListDto<User>()
        {
            TotalCount = totalCount,
            PageSize = filter.PageSize,
            PageIndex = filter.PageIndex,
            Data = pageUsers
        };
    }

    public async Task<User?> InsertAsync(User user)
    {
        if (await appDbContext.Users.FirstOrDefaultAsync(x => x.Email == user.Email.ToLower()) != null)
            throw new BadHttpRequestException("Email already");
        if (await appDbContext.Users.FirstOrDefaultAsync(x => x.Username == user.Username.ToLower()) != null)
            throw new BadHttpRequestException("Username already");
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        user.Email = user.Email.ToLower().Trim();
        user.CreatedDate = DateTime.Now;
        user.UpdatedDate = DateTime.Now;
        user.IsActive = false;
        user.Username = user.Username.ToLower().Trim();
        user.Role = await appDbContext.Roles.FindAsync(2);
        user.SecurityCode = RandomHelper.RandomInt(6);
        appDbContext.Users.Add(user);
        var result = await appDbContext.SaveChangesAsync();

        if (result > 0)
        {
            var mailHelper = new MailHelper(configuration);
            var linkActive = "https://localhost:7260/api/v1/Auths/active-account?email="+user.Email+"&code="+user.SecurityCode;
            var linkSupport = "";
            var check = mailHelper.Send(configuration["Gmail:Username"]!, user.Email, "Check your email address", MailHelper.HtmlActiveAccount(linkActive, linkSupport));
        }

        return result > 0 ? user : null;

    }

    public async Task<User?> UpdateAsync(Guid id, User user)
    {
        var userModel = await appDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (userModel == null) throw new BadHttpRequestException("User does not exist");
        userModel.FullName = user.FullName;
        userModel.Address = user.Address;
        userModel.PhoneNumber = user.PhoneNumber;
        userModel.DateOfBirth = user.DateOfBirth;
        userModel.Gender = user.Gender;
        userModel.UpdatedDate = DateTime.Now;
        appDbContext.Entry(userModel).State = EntityState.Modified;
        var result = await appDbContext.SaveChangesAsync();
        return result > 0 ? userModel : null;
    }



    public async Task<int> UploadImageAsync(Guid id, IFormFile image)
    {
        var user = await appDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (user == null) throw new BadHttpRequestException("User does not exist");
        if (!string.IsNullOrWhiteSpace(user.Image))
        {
            var pathDelete = Path.Combine(webHostEnvironment.WebRootPath, "users", user.Image);
            File.Delete(pathDelete);
            var fileName = FileHelper.GenerateFileName(image.FileName);
            var path = Path.Combine(webHostEnvironment.WebRootPath, "users", fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                image.CopyTo(fileStream);
            }
            user.Image = fileName;
        }
        else
        {
            var fileName = FileHelper.GenerateFileName(image.FileName);
            var path = Path.Combine(webHostEnvironment.WebRootPath, "users", fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                image.CopyTo(fileStream);
            }
            user.Image = fileName;
        }
        user.UpdatedDate = DateTime.Now;
        appDbContext.Entry(user).State = EntityState.Modified;
        return await appDbContext.SaveChangesAsync();

    }
}
