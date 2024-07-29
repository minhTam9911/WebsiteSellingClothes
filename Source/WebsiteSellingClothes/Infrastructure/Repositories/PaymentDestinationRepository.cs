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
public class PaymentDestinationRepository : IPaymentDestinationRepository
{
    private readonly AppDbContext appDbContext;

    public PaymentDestinationRepository(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async Task<int> DeleteAsync(string id)
    {
        var paymentDestination = await appDbContext.PaymentDestinations.FirstOrDefaultAsync(x => x.Id == id);
        if (paymentDestination == null) return 0;
        appDbContext.PaymentDestinations.Remove(paymentDestination);
        var result = await appDbContext.SaveChangesAsync();
        return result;
    }

    public async Task<List<PaymentDestination>?> GetAllAsync()
    {
        var paymentDestinations = await appDbContext.PaymentDestinations.ToListAsync();
        return paymentDestinations;
    }

    public async Task<PaymentDestination?> GetByIdAsync(string id)
    {
        var paymentDestination = await appDbContext.PaymentDestinations.FirstOrDefaultAsync(x => x.Id == id);
        return paymentDestination;
    }

    public async Task<PagedListDto<PaymentDestination?>> GetListAsync(FilterDto filterDto)
    {
        IQueryable<PaymentDestination> query;
        if (string.IsNullOrWhiteSpace(filterDto.Keyword))
        {
            query = appDbContext.PaymentDestinations;
        }
        else
        {
            filterDto.Keyword = filterDto.Keyword.Trim().ToLower();
            query = appDbContext.PaymentDestinations.Where(x => x.Id.ToLower().Contains(filterDto.Keyword) ||
            x.DestinationName.ToLower().Contains(filterDto.Keyword));
        }
        if (!string.IsNullOrWhiteSpace(filterDto.SortColumn))
        {
            filterDto.SortColumn = filterDto.SortColumn.Trim();
            StringBuilder orderQueryBuilder = new StringBuilder();
            PropertyInfo[] propertyInfo = typeof(PaymentDestination).GetProperties();
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
            return new PagedListDto<PaymentDestination?>()
            {
                TotalCount = data!.Count(),
                PageSize = filterDto.PageSize,
                PageIndex = filterDto.PageIndex,
                Data = data!
            };
        }
        var totalCount = await query.CountAsync();
        var totalPage = (int)Math.Ceiling(totalCount / (double)filterDto.PageSize);
        var pagePaymentDestinations = await query.Skip((filterDto.PageIndex - 1) * filterDto.PageSize).Take(filterDto.PageSize).ToListAsync();
        return new PagedListDto<PaymentDestination?>()
        {
            TotalCount = totalCount,
            PageSize = filterDto.PageSize,
            PageIndex = filterDto.PageIndex,
            Data = pagePaymentDestinations
        };
    }

    public async Task<PaymentDestination?> InsertAsync(PaymentDestination paymentDestination)
    {
        paymentDestination.Id = GenerateHelper.GenerateKeyNumber("DEST");
        appDbContext.PaymentDestinations.Add(paymentDestination);
        var result = await appDbContext.SaveChangesAsync();
        return result > 0 ? paymentDestination : null;
    }

    public async Task<PaymentDestination?> UpdateAsync(string id, PaymentDestination paymentDestination)
    {
        var paymentDestinationModel = await appDbContext.PaymentDestinations.FirstOrDefaultAsync(x => x.Id == id);
        if (paymentDestinationModel == null) return null;
        paymentDestinationModel.PaymentDestinationParent = paymentDestination.PaymentDestinationParent;
        paymentDestinationModel.DestinationName = paymentDestination.DestinationName;
        paymentDestinationModel.DestinationLogo = paymentDestination.DestinationLogo;
        paymentDestinationModel.IsActive = paymentDestination.IsActive;
        paymentDestinationModel.DestinationShortName = paymentDestination.DestinationShortName;
        
        appDbContext.Entry(paymentDestinationModel).State = EntityState.Modified;
        var result = await appDbContext.SaveChangesAsync();
        return result > 0 ? paymentDestinationModel : null;
    }
}
