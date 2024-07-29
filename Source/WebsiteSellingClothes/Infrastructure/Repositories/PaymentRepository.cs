using Common.DTOs;
using Common.Helpers;
using Common.Helplers;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;
public class PaymentRepository : IPaymentRepository

{
    private readonly AppDbContext appDbContext;

    public PaymentRepository(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async Task<int> DeleteAsync(string id)
    {
        var payment = await appDbContext.Payments.FirstOrDefaultAsync(x => x.Id == id);
        if (payment == null) return 0;
        appDbContext.Payments.Remove(payment);
        var result = await appDbContext.SaveChangesAsync();
        return result;
    }

    public async Task<List<Payment>?> GetAllAsync()
    {
        var payments = await appDbContext.Payments.ToListAsync();
        return payments;
    }

    public async Task<PagedListDto<Payment?>> GetAllForMeAsync(Guid userId, FilterDto filterDto)
    {
        IQueryable<Payment> query;
        if (string.IsNullOrWhiteSpace(filterDto.Keyword))
        {
            query = appDbContext.Payments.Where(x => x.User!.Id == userId);
        }
        else
        {
            filterDto.Keyword = filterDto.Keyword.Trim().ToLower();
            query = appDbContext.Payments.Where(x => x.User!.Id == userId && (x.Id.ToLower().Contains(filterDto.Keyword) ||
            x.PaymentStatus.ToLower().Contains(filterDto.Keyword)));
        }
        if (!string.IsNullOrWhiteSpace(filterDto.SortColumn))
        {
            filterDto.SortColumn = filterDto.SortColumn.Trim();
            StringBuilder orderQueryBuilder = new StringBuilder();
            PropertyInfo[] propertyInfo = typeof(Payment).GetProperties();
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
            return new PagedListDto<Payment?>()
            {
                TotalCount = data!.Count(),
                PageSize = filterDto.PageSize,
                PageIndex = filterDto.PageIndex,
                Data = data!
            };
        }
        var totalCount = await query.CountAsync();
        var totalPage = (int)Math.Ceiling(totalCount / (double)filterDto.PageSize);
        var pagePayments = await query.Skip((filterDto.PageIndex - 1) * filterDto.PageSize).Take(filterDto.PageSize).ToListAsync();
        return new PagedListDto<Payment?>()
        {
            TotalCount = totalCount,
            PageSize = filterDto.PageSize,
            PageIndex = filterDto.PageIndex,
            Data = pagePayments
        };
    }

    public async Task<Payment?> GetByIdAsync(string id)
    {
        var payment = await appDbContext.Payments.FirstOrDefaultAsync(x => x.Id == id);
        return payment;
    }

    public async Task<PagedListDto<Payment?>> GetListAsync(FilterDto filterDto)
    {
        IQueryable<Payment> query;
        if (string.IsNullOrWhiteSpace(filterDto.Keyword))
        {
            query = appDbContext.Payments;
        }
        else
        {
            filterDto.Keyword = filterDto.Keyword.Trim().ToLower();
            query = appDbContext.Payments.Where(x => x.Id.ToLower().Contains(filterDto.Keyword) ||
            x.PaymentStatus.ToLower().Contains(filterDto.Keyword));
        }
        if (!string.IsNullOrWhiteSpace(filterDto.SortColumn))
        {
            filterDto.SortColumn = filterDto.SortColumn.Trim();
            StringBuilder orderQueryBuilder = new StringBuilder();
            PropertyInfo[] propertyInfo = typeof(Payment).GetProperties();
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
            return new PagedListDto<Payment?>()
            {
                TotalCount = data!.Count(),
                PageSize = filterDto.PageSize,
                PageIndex = filterDto.PageIndex,
                Data = data!
            };
        }
        var totalCount = await query.CountAsync();
        var totalPage = (int)Math.Ceiling(totalCount / (double)filterDto.PageSize);
        var pagePayments = await query.Skip((filterDto.PageIndex - 1) * filterDto.PageSize).Take(filterDto.PageSize).ToListAsync();
        return new PagedListDto<Payment?>()
        {
            TotalCount = totalCount,
            PageSize = filterDto.PageSize,
            PageIndex = filterDto.PageIndex,
            Data = pagePayments
        };
    }

    public async Task<Payment?> InsertAsync(Payment payment,Order order, Guid userId)
    {
        try
        {
            payment.Id = GenerateHelper.GeneratePaymentId();
            payment.User = await appDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);     
            payment.Order = order;
            appDbContext.Payments.Add(payment);
            var result = await appDbContext.SaveChangesAsync();
            return result > 0 ? payment : null;
        }
        catch (Exception ex) {
            Debug.WriteLine(ex);
            throw new Exception(ex.Message);
        }


    }

    public async Task<int> SetPaidAsync(string id, decimal paidAmount)
    {
        var result = await appDbContext.Payments.Where(x => x.Id == id).ExecuteUpdateAsync(setter => setter.SetProperty(o => o.PaymentStatus, "Paid").SetProperty(x => x.PaidAmount, paidAmount));
        return result;
    }

    public async Task<Payment?> UpdateAsync(string id, Payment payment)
    {
        var paymentModel = await appDbContext.Payments.FirstOrDefaultAsync(x => x.Id == id);
        if (paymentModel == null) return null;
        paymentModel.PaymentDestination = payment.PaymentDestination;
        paymentModel.ExpireDate = payment.ExpireDate;
        paymentModel.PaymentDate = payment.PaymentDate;
        paymentModel.Merchant = payment.Merchant;
        paymentModel.OrderId = payment.OrderId;
        paymentModel.PaymentLastMessage = payment.PaymentLastMessage;
        paymentModel.PaidAmount = payment.PaidAmount;
        paymentModel.PaymentContent = payment.PaymentContent;
        paymentModel.PaymentCurrency = payment.PaymentCurrency;
        paymentModel.PaymentLanguage = payment.PaymentLanguage;
        paymentModel.PaymentStatus = payment.PaymentStatus;
        paymentModel.RequiredAmount = payment.RequiredAmount;
        appDbContext.Entry(paymentModel).State = EntityState.Modified;
        var result = await appDbContext.SaveChangesAsync();
        return result > 0 ? paymentModel : null;
    }
}
