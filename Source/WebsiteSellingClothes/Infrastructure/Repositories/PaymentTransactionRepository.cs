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
public class PaymentTransactionRepository : IPaymentTransactionRepository
{
    private readonly AppDbContext appDbContext;

    public PaymentTransactionRepository(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async Task<int> DeleteAsync(string id)
    {
        var paymentTransaction = await appDbContext.PaymentTransactions.FirstOrDefaultAsync(x => x.Id == id);
        if (paymentTransaction == null) return 0;
        appDbContext.PaymentTransactions.Remove(paymentTransaction);
        var result = await appDbContext.SaveChangesAsync();
        return result;
    }

    public async Task<List<PaymentTransaction>?> GetAllAsync()
    {
        var paymentTransactions = await appDbContext.PaymentTransactions.ToListAsync();
        return paymentTransactions;
    }
    public async Task<PagedListDto<PaymentTransaction?>> GetAllForMeAsync(Guid userId, FilterDto filterDto)
    {
        IQueryable<PaymentTransaction> query;
        if (string.IsNullOrWhiteSpace(filterDto.Keyword))
        {
            query = appDbContext.PaymentTransactions.Where(x => x.Payment!.User!.Id == userId);
        }
        else
        {
            filterDto.Keyword = filterDto.Keyword.Trim().ToLower();
            query = appDbContext.PaymentTransactions.Where(x => x.Payment!.User!.Id == userId && 
            (x.Id.ToLower().Contains(filterDto.Keyword) ||
            x.TransactionMessage.ToLower().Contains(filterDto.Keyword) ||
            x.TransactionStatus.ToLower().Contains(filterDto.Keyword)));
        }
        if (!string.IsNullOrWhiteSpace(filterDto.SortColumn))
        {
            filterDto.SortColumn = filterDto.SortColumn.Trim();
            StringBuilder orderQueryBuilder = new StringBuilder();
            PropertyInfo[] propertyInfo = typeof(PaymentTransaction).GetProperties();
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
            return new PagedListDto<PaymentTransaction?>()
            {
                TotalCount = data!.Count(),
                PageSize = filterDto.PageSize,
                PageIndex = filterDto.PageIndex,
                Data = data!
            };
        }
        var totalCount = await query.CountAsync();
        var totalPage = (int)Math.Ceiling(totalCount / (double)filterDto.PageSize);
        var pagePaymentTransactions = await query.Skip((filterDto.PageIndex - 1) * filterDto.PageSize).Take(filterDto.PageSize).ToListAsync();
        return new PagedListDto<PaymentTransaction?>()
        {
            TotalCount = totalCount,
            PageSize = filterDto.PageSize,
            PageIndex = filterDto.PageIndex,
            Data = pagePaymentTransactions
        };
    }


    public async Task<PaymentTransaction?> GetByIdAsync(string id)
    {
        var paymentTransaction = await appDbContext.PaymentTransactions.FirstOrDefaultAsync(x => x.Id == id);
        return paymentTransaction;
    }

    public async Task<PagedListDto<PaymentTransaction?>> GetListAsync(FilterDto filterDto)
    {
        IQueryable<PaymentTransaction> query;
        if (string.IsNullOrWhiteSpace(filterDto.Keyword))
        {
            query = appDbContext.PaymentTransactions;
        }
        else
        {
            filterDto.Keyword = filterDto.Keyword.Trim().ToLower();
            query = appDbContext.PaymentTransactions.Where(x => x.Id.ToLower().Contains(filterDto.Keyword) ||
            x.TransactionMessage.ToLower().Contains(filterDto.Keyword) || x.TransactionPayload.ToLower().Contains(filterDto.Keyword) || x.TransactionDate.ToString().Contains(filterDto.Keyword));
        }
        if (!string.IsNullOrWhiteSpace(filterDto.SortColumn))
        {
            filterDto.SortColumn = filterDto.SortColumn.Trim();
            StringBuilder orderQueryBuilder = new StringBuilder();
            PropertyInfo[] propertyInfo = typeof(PaymentTransaction).GetProperties();
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
            return new PagedListDto<PaymentTransaction?>()
            {
                TotalCount = data!.Count(),
                PageSize = filterDto.PageSize,
                PageIndex = filterDto.PageIndex,
                Data = data!
            };
        }
        var totalCount = await query.CountAsync();
        var totalPage = (int)Math.Ceiling(totalCount / (double)filterDto.PageSize);
        var pagePaymentTransactions = await query.Skip((filterDto.PageIndex - 1) * filterDto.PageSize).Take(filterDto.PageSize).ToListAsync();
        return new PagedListDto<PaymentTransaction?>()
        {
            TotalCount = totalCount,
            PageSize = filterDto.PageSize,
            PageIndex = filterDto.PageIndex,
            Data = pagePaymentTransactions
        };
    }

    public async Task<PaymentTransaction?> InsertAsync(PaymentTransaction paymentTransaction)
    {
        paymentTransaction.Id = GenerateHelper.GenerateKeyNumber("TRAN");
        appDbContext.PaymentTransactions.Add(paymentTransaction);
        var result = await appDbContext.SaveChangesAsync();
        return result > 0 ? paymentTransaction : null;
    }

    public async Task<PaymentTransaction?> UpdateAsync(string id, PaymentTransaction paymentTransaction)
    {
        var paymentTransactionModel = await appDbContext.PaymentTransactions.FirstOrDefaultAsync(x => x.Id == id);
        if (paymentTransactionModel == null) return null;
        paymentTransactionModel.TransactionStatus = paymentTransaction.TransactionStatus;
        paymentTransactionModel.TransactionDate = paymentTransaction.TransactionDate;
        paymentTransactionModel.TransactionMessage = paymentTransaction.TransactionMessage;
        paymentTransactionModel.TransactionPayload = paymentTransaction.TransactionPayload;
        paymentTransactionModel.TransactionAmount = paymentTransaction.TransactionAmount;
        paymentTransactionModel.Payment = paymentTransaction.Payment;
        appDbContext.Entry(paymentTransactionModel).State = EntityState.Modified;
        var result = await appDbContext.SaveChangesAsync();
        return result > 0 ? paymentTransactionModel : null;
    }
}
