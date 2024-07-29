using Common.DTOs;
using Common.Helpers;
using Common.Helplers;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;
public class MerchantRepository : IMerchantRepository
{
    private readonly AppDbContext appDbContext;

    public MerchantRepository(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async Task<int> DeleteAsync(string id)
    {
        var merchant = await appDbContext.Merchants.FirstOrDefaultAsync(x => x.Id == id);
        if (merchant == null) return 0;
        appDbContext.Merchants.Remove(merchant);
        var result = await appDbContext.SaveChangesAsync();
        return result;
    }

    public async Task<List<Merchant>?> GetAllAsync()
    {
        var merchants = await appDbContext.Merchants.ToListAsync();
        return merchants;
    }
    public async Task<PagedListDto<Merchant?>> GetAllForMeAsync(Guid userId, FilterDto filterDto)
    {
        IQueryable<Merchant> query;
        if (string.IsNullOrWhiteSpace(filterDto.Keyword))
        {
            query = appDbContext.Merchants.Where(x => x.User!.Id == userId);
        }
        else
        {
            filterDto.Keyword = filterDto.Keyword.Trim().ToLower();
            query = appDbContext.Merchants.Where(x => x.User!.Id == userId && (x.Id.ToLower().Contains(filterDto.Keyword) ||
            x.MerchantName.ToLower().Contains(filterDto.Keyword)));
        }
        if (!string.IsNullOrWhiteSpace(filterDto.SortColumn))
        {
            filterDto.SortColumn = filterDto.SortColumn.Trim();
            StringBuilder orderQueryBuilder = new StringBuilder();
            PropertyInfo[] propertyInfo = typeof(Merchant).GetProperties();
            var property = propertyInfo.FirstOrDefault(x => x.Name.Equals(filterDto.SortColumn, StringComparison.OrdinalIgnoreCase));
            var orderBy = filterDto.IsDescending ? "descending" : "ascending";
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
            if (filterDto.IsDescending) query = query.OrderByDescending(a => a.Id);
            else
            {
                query = query.OrderBy(a => a.Id);
            }
        }
        if (filterDto.PageSize == -1)
        {
            var data = await query.ToListAsync();
            return new PagedListDto<Merchant?>()
            {
                TotalCount = data!.Count(),
                PageSize = filterDto.PageSize,
                PageIndex = filterDto.PageIndex,
                Data = data!
            };
        }
        var totalCount = await query.CountAsync();
        var totalPage = (int)Math.Ceiling(totalCount / (double)filterDto.PageSize);
        var pageMerchants = await query.Skip((filterDto.PageIndex - 1) * filterDto.PageSize).Take(filterDto.PageSize).ToListAsync();
        return new PagedListDto<Merchant?>()
        {
            TotalCount = totalCount,
            PageSize = filterDto.PageSize,
            PageIndex = filterDto.PageIndex,
            Data = pageMerchants
        };
    }

    public async Task<Merchant?> GetByIdAsync(string id)
    {
        var merchant = await appDbContext.Merchants.FirstOrDefaultAsync(x => x.Id == id);
        return merchant;
    }

    public async Task<PagedListDto<Merchant?>> GetListAsync(FilterDto filterDto)
    {
        IQueryable<Merchant> query;
        if (string.IsNullOrWhiteSpace(filterDto.Keyword))
        {
            query = appDbContext.Merchants;
        }
        else
        {
            filterDto.Keyword = filterDto.Keyword.Trim().ToLower();
            query = appDbContext.Merchants.Where(x => x.MerchantName.ToLower().Contains(filterDto.Keyword) ||
            x.Id.ToLower().Contains(filterDto.Keyword));
        }
        if (!string.IsNullOrWhiteSpace(filterDto.SortColumn))
        {
            filterDto.SortColumn = filterDto.SortColumn.Trim();
            StringBuilder orderQueryBuilder = new StringBuilder();
            PropertyInfo[] propertyInfo = typeof(Merchant).GetProperties();
            var property = propertyInfo.FirstOrDefault(x => x.Name.Equals(filterDto.SortColumn, StringComparison.OrdinalIgnoreCase));
            var orderBy = filterDto.IsDescending ? "descending" : "ascending";
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
            if (filterDto.IsDescending) query = query.OrderByDescending(a => a.Id);
            else
            {
                query = query.OrderBy(a => a.Id);
            }
        }
        if (filterDto.PageSize == -1)
        {
            var data = await query.ToListAsync();
            return new PagedListDto<Merchant?>()
            {
                TotalCount = data!.Count(),
                PageSize = filterDto.PageSize,
                PageIndex = filterDto.PageIndex,
                Data = data!
            };
        }
        var totalCount = await query.CountAsync();
        var totalPage = (int)Math.Ceiling(totalCount / (double)filterDto.PageSize);
        var pageMerchants = await query.Skip((filterDto.PageIndex - 1) * filterDto.PageSize).Take(filterDto.PageSize).ToListAsync();
        return new PagedListDto<Merchant?>()
        {
            TotalCount = totalCount,
            PageSize = filterDto.PageSize,
            PageIndex = filterDto.PageIndex,
            Data = pageMerchants
        };
    }

    public async Task<Merchant?> InsertAsync(Merchant merchant,Guid userId)
    {
        merchant.User = await appDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
        merchant.Id = GenerateHelper.GenerateMerchantId();
        appDbContext.Merchants.Add(merchant);
        var result = await appDbContext.SaveChangesAsync();
        return result > 0 ? merchant : null;
    }

    public async Task<Merchant?> SetActiveAsync(string id)
    {
        var merchant = await appDbContext.Merchants.FirstOrDefaultAsync(x => x.Id == id);
        if(merchant == null) return null;
        merchant.IsActive = true;
        merchant.UpdateDate = DateTime.Now;
        appDbContext.Entry(merchant).State = EntityState.Modified;
        var result = await appDbContext.SaveChangesAsync();
        return result > 0 ? merchant : null;
    }

    public async Task<Merchant?> UpdateAsync(string id, Merchant merchant)
    {
        var merchantModel = await appDbContext.Merchants.FirstOrDefaultAsync(x => x.Id == id);
        if (merchantModel == null) return null;
        merchantModel.MerchantReturnUrl = merchant.MerchantReturnUrl;
        merchantModel.MerchantIpnUrl = merchant.MerchantIpnUrl;
        merchantModel.MerchantName = merchant.MerchantName;
        merchantModel.MerchantWebLink = merchant.MerchantWebLink;
        merchantModel.IsActive = merchant.IsActive;
        merchantModel.SercetKey = merchant.SercetKey;
        merchantModel.UpdateDate = DateTime.Now;
        appDbContext.Entry(merchantModel).State = EntityState.Modified;
        var result = await appDbContext.SaveChangesAsync();
        return result > 0 ? merchantModel : null;
    }
}
