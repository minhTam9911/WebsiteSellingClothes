using Common.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;
public class CartRepository : ICartRepository
{

    private readonly AppDbContext appDbContext;

    public CartRepository(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async Task<int> DeleteAsync(int id, Guid userId)
    {
        var cart = await appDbContext.Carts.FirstOrDefaultAsync(x => x.Id == id);
        if (cart == null) throw new BadHttpRequestException("Cart does not exist");
        if (cart.User!.Id != userId) throw new UnauthorizedAccessException("Forbidden");
        appDbContext.Carts.Remove(cart);
        var result = await appDbContext.SaveChangesAsync();
        return result;
    }

    public async Task<List<Cart>?> GetAllAsync()
    {
        return await appDbContext.Carts.ToListAsync();
    }

    public async Task<PagedListDto<Cart>?> GetAllForMeAsync(FilterDto filter, Guid userId)
    {
        IQueryable<Cart> query;
        if (string.IsNullOrWhiteSpace(filter.Keyword))
        {
            query = appDbContext.Carts.Where(x => x.User!.Id == userId && x.IsPaid == false);
        }
        else
        {
            filter.Keyword = filter.Keyword.Trim().ToLower();
            query = appDbContext.Carts.Where(x =>
                    (x.Product!.Name.ToLower().Contains(filter.Keyword) || x.Id.ToString().Contains(filter.Keyword)) && x.User!.Id == userId && x.IsPaid == false);
        }
        if (!string.IsNullOrWhiteSpace(filter.SortColumn))
        {
            filter.SortColumn = filter.SortColumn.Trim();
            StringBuilder orderQueryBuilder = new StringBuilder();
            PropertyInfo[] propertyInfo = typeof(Cart).GetProperties();
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
            return new PagedListDto<Cart>()
            {
                TotalCount = await query.CountAsync(),
                PageSize = filter.PageSize,
                PageIndex = filter.PageIndex,
                Data = await query.ToListAsync()
            };
        }
        var totalCount = await query.CountAsync();
        var totalPage = (int)Math.Ceiling(totalCount / (double)filter.PageSize);
        var pageCarts = await query.Skip((filter.PageIndex - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
        return new PagedListDto<Cart>()
        {
            TotalCount = totalCount,
            PageSize = filter.PageSize,
            PageIndex = filter.PageIndex,
            Data = pageCarts
        };
    }

    public async Task<Cart?> GetByIdAsync(int id, Guid userId)
    {
        var user = await appDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (user!.Role!.Id == 1) return await appDbContext.Carts.FirstOrDefaultAsync(x => x.Id == id);
        var cart = await appDbContext.Carts.FirstOrDefaultAsync(x => x.Id == id);
        if (cart!.User!.Id == userId) return cart;
        throw new UnauthorizedAccessException("Forbidden");
    }

    public async Task<PagedListDto<Cart>?> GetListAsync(FilterDto filter)
    {
        IQueryable<Cart> query;
        if (string.IsNullOrWhiteSpace(filter.Keyword))
        {
            query = appDbContext.Carts;
        }
        else
        {
            filter.Keyword = filter.Keyword.Trim().ToLower();
            query = appDbContext.Carts.Where(x => x.Product!.Name.ToLower().Contains(filter.Keyword));
        }
        if (!string.IsNullOrWhiteSpace(filter.SortColumn))
        {
            filter.SortColumn = filter.SortColumn.Trim();
            StringBuilder orderQueryBuilder = new StringBuilder();
            PropertyInfo[] propertyInfo = typeof(Cart).GetProperties();
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
            return new PagedListDto<Cart>()
            {
                TotalCount = data.Count(),
                PageSize = filter.PageSize,
                PageIndex = filter.PageIndex,
                Data = data
            };
        }
        var totalCount = await query.CountAsync();
        var totalPage = (int)Math.Ceiling(totalCount / (double)filter.PageSize);
        var pageCarts = await query.Skip((filter.PageIndex - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
        return new PagedListDto<Cart>()
        {
            TotalCount = totalCount,
            PageSize = filter.PageSize,
            PageIndex = filter.PageIndex,
            Data = pageCarts
        };
    }

    public async Task<Cart?> InsertAsync(int productId, int quantity, Guid userId)
    {

        var cart = await appDbContext.Carts
                        .FirstOrDefaultAsync(x => x.Product!.Id == productId && x.User!.Id == userId);
        if (cart != null)
        {
            cart.Quantity += quantity;
            cart.UpdatedDate = DateTime.Now;
            appDbContext.Entry(cart).State = EntityState.Modified;
            var result = await appDbContext.SaveChangesAsync();
            return result > 0 ? cart : null;
        }
        var product = await appDbContext.Products.FirstOrDefaultAsync(x => x.Id == productId);
        if (product == null) throw new BadHttpRequestException("Product does not exist");
        else
        {
            cart = new Cart();
            cart!.Product = product;
            cart.User = await appDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            cart.Quantity = quantity > 0 ? quantity : 1;
            cart.CreatedDate = DateTime.Now;
            cart.UpdatedDate = DateTime.Now;
            appDbContext.Carts.Add(cart);
            var result = await appDbContext.SaveChangesAsync();
            return result > 0 ? cart : null;
        }
    }

    public async Task<Cart?> SetIsPaid(int id, Guid userId)
    {
        var cart = await appDbContext.Carts.FirstOrDefaultAsync(x => x.Id == id && x.User!.Id == userId);
        cart.IsPaid = true;
        cart.UpdatedDate = DateTime.Now;
        appDbContext.Entry(cart).State = EntityState.Modified;
        var result = await appDbContext.SaveChangesAsync();
        return result > 0 ? cart : null;
    }

    public async Task<int> SetPaidAsync(int id)
    {
        var result = await appDbContext.Carts.Where(x => x.Id == id).ExecuteUpdateAsync(setter => setter.SetProperty(o => o.IsPaid,true));
        return result;
    }

    public async Task<Cart?> UpdateAsync(int id, int productId, int quantity, Guid userId)
    {
        var cart = await appDbContext.Carts
                        .FirstOrDefaultAsync(x => x.Id == id);
        if (cart == null) throw new BadHttpRequestException("Cart does not exist");
        if (cart.User!.Id != userId) throw new UnauthorizedAccessException("Forbidden");
        cart.Quantity = quantity;
        cart.UpdatedDate = DateTime.Now;
        appDbContext.Entry(cart).State = EntityState.Modified;
        var result = await appDbContext.SaveChangesAsync();
        return result > 0 ? cart : null;
    }
}
