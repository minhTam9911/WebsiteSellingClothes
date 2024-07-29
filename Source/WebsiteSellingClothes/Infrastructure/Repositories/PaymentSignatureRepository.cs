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
public class PaymentSignatureRepository : IPaymentSignatureRepository
{
    private readonly AppDbContext appDbContext;

    public PaymentSignatureRepository(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async Task<int> DeleteAsync(string id)
    {
        var paymentSignature = await appDbContext.PaymentSignatures.FirstOrDefaultAsync(x => x.Id == id);
        if (paymentSignature == null) return 0;
        appDbContext.PaymentSignatures.Remove(paymentSignature);
        var result = await appDbContext.SaveChangesAsync();
        return result;
    }

    public async Task<List<PaymentSignature>?> GetAllAsync()
    {
        var paymentSignatures = await appDbContext.PaymentSignatures.ToListAsync();
        return paymentSignatures;
    }
    public async Task<PagedListDto<PaymentSignature?>> GetAllForMeAsync(Guid userId, FilterDto filterDto)
    {
        IQueryable<PaymentSignature> query;
        if (string.IsNullOrWhiteSpace(filterDto.Keyword))
        {
            query = appDbContext.PaymentSignatures.Where(x => x.Payment!.User!.Id == userId);
        }
        else
        {
            filterDto.Keyword = filterDto.Keyword.Trim().ToLower();
            query = appDbContext.PaymentSignatures.Where(x => x.Payment!.User!.Id == userId && 
            (x.Id.ToLower().Contains(filterDto.Keyword) ||
            x.SignatureAlgo.ToLower().Contains(filterDto.Keyword)||
            x.SignatureOwn.ToLower().Contains(filterDto.Keyword)));
        }
        if (!string.IsNullOrWhiteSpace(filterDto.SortColumn))
        {
            filterDto.SortColumn = filterDto.SortColumn.Trim();
            StringBuilder orderQueryBuilder = new StringBuilder();
            PropertyInfo[] propertyInfo = typeof(PaymentNotification).GetProperties();
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
            return new PagedListDto<PaymentSignature?>()
            {
                TotalCount = data!.Count(),
                PageSize = filterDto.PageSize,
                PageIndex = filterDto.PageIndex,
                Data = data!
            };
        }
        var totalCount = await query.CountAsync();
        var totalPage = (int)Math.Ceiling(totalCount / (double)filterDto.PageSize);
        var pagePaymentSignatures = await query.Skip((filterDto.PageIndex - 1) * filterDto.PageSize).Take(filterDto.PageSize).ToListAsync();
        return new PagedListDto<PaymentSignature?>()
        {
            TotalCount = totalCount,
            PageSize = filterDto.PageSize,
            PageIndex = filterDto.PageIndex,
            Data = pagePaymentSignatures
        };
    }


    public async Task<PaymentSignature?> GetByIdAsync(string id)
    {
        var paymentSignatures = await appDbContext.PaymentSignatures.FirstOrDefaultAsync(x => x.Id == id);
        return paymentSignatures;
    }

    public async Task<PagedListDto<PaymentSignature?>> GetListAsync(FilterDto filterDto)
    {
        IQueryable<PaymentSignature> query;
        if (string.IsNullOrWhiteSpace(filterDto.Keyword))
        {
            query = appDbContext.PaymentSignatures;
        }
        else
        {
            filterDto.Keyword = filterDto.Keyword.Trim().ToLower();
            query = appDbContext.PaymentSignatures.Where(x => x.Id.ToLower().Contains(filterDto.Keyword) ||
            x.SignatureOwn.ToLower().Contains(filterDto.Keyword) || x.SignatureValue.ToLower().Contains(filterDto.Keyword) || x.SignatureAlgo.ToLower().Contains(filterDto.Keyword) || x.SignatureDate.ToString().Contains(filterDto.Keyword));
        }
        if (!string.IsNullOrWhiteSpace(filterDto.SortColumn))
        {
            filterDto.SortColumn = filterDto.SortColumn.Trim();
            StringBuilder orderQueryBuilder = new StringBuilder();
            PropertyInfo[] propertyInfo = typeof(PaymentSignature).GetProperties();
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
            return new PagedListDto<PaymentSignature?>()
            {
                TotalCount = data!.Count(),
                PageSize = filterDto.PageSize,
                PageIndex = filterDto.PageIndex,
                Data = data!
            };
        }
        var totalCount = await query.CountAsync();
        var totalPage = (int)Math.Ceiling(totalCount / (double)filterDto.PageSize);
        var pagePaymentSignatures = await query.Skip((filterDto.PageIndex - 1) * filterDto.PageSize).Take(filterDto.PageSize).ToListAsync();
        return new PagedListDto<PaymentSignature?>()
        {
            TotalCount = totalCount,
            PageSize = filterDto.PageSize,
            PageIndex = filterDto.PageIndex,
            Data = pagePaymentSignatures
        };
    }

    public async Task<PaymentSignature?> InsertAsync(PaymentSignature paymentSignature)
    {
        paymentSignature.Id = GenerateHelper.GenerateKeyNumber("SIGN");
        appDbContext.PaymentSignatures.Add(paymentSignature);
        var result = await appDbContext.SaveChangesAsync();
        return result > 0 ? paymentSignature : null;
    }

    public async Task<PaymentSignature?> UpdateAsync(string id, PaymentSignature paymentSignature)
    {
        var paymentSignatureModel = await appDbContext.PaymentSignatures.FirstOrDefaultAsync(x => x.Id == id);
        if (paymentSignatureModel == null) return null;
        paymentSignatureModel.SignatureOwn = paymentSignature.SignatureOwn;
        paymentSignatureModel.SignatureValue = paymentSignature.SignatureValue;
        paymentSignatureModel.SignatureDate = paymentSignature.SignatureDate;
        paymentSignatureModel.SignatureAlgo = paymentSignature.SignatureAlgo;
        paymentSignatureModel.Payment = paymentSignature.Payment;
        appDbContext.Entry(paymentSignatureModel).State = EntityState.Modified;
        var result = await appDbContext.SaveChangesAsync();
        return result > 0 ? paymentSignatureModel : null;
    }
}
