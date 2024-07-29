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
public class PaymentNotificationRepository : IPaymentNotificationRepository
{
    private readonly AppDbContext appDbContext;

    public PaymentNotificationRepository(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async Task<int> DeleteAsync(string id)
    {
        var paymentNotification = await appDbContext.PaymentNotifications.FirstOrDefaultAsync(x => x.Id == id);
        if (paymentNotification == null) return 0;
        appDbContext.PaymentNotifications.Remove(paymentNotification);
        var result = await appDbContext.SaveChangesAsync();
        return result;
    }

    public async Task<List<PaymentNotification>?> GetAllAsync()
    {
        var paymentNotifications = await appDbContext.PaymentNotifications.ToListAsync();
        return paymentNotifications;
    }
    public async Task<PagedListDto<PaymentNotification?>> GetAllForMeAsync(Guid userId, FilterDto filterDto)
    {
        IQueryable<PaymentNotification> query;
        if (string.IsNullOrWhiteSpace(filterDto.Keyword))
        {
            query = appDbContext.PaymentNotifications.Where(x => x.Payment!.User!.Id == userId);
        }
        else
        {
            filterDto.Keyword = filterDto.Keyword.Trim().ToLower();
            query = appDbContext.PaymentNotifications.Where(x => x.Payment!.User!.Id == userId &&
            (x.Id.ToLower().Contains(filterDto.Keyword) ||
            x.NotificationStatus.ToLower().Contains(filterDto.Keyword) ||
            x.NotificationMessage.ToLower().Contains(filterDto.Keyword) ||
            x.NotificationContent.ToLower().Contains(filterDto.Keyword)));
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
            return new PagedListDto<PaymentNotification?>()
            {
                TotalCount = data!.Count(),
                PageSize = filterDto.PageSize,
                PageIndex = filterDto.PageIndex,
                Data = data!
            };
        }
        var totalCount = await query.CountAsync();
        var totalPage = (int)Math.Ceiling(totalCount / (double)filterDto.PageSize);
        var pagePaymentNotifications = await query.Skip((filterDto.PageIndex - 1) * filterDto.PageSize).Take(filterDto.PageSize).ToListAsync();
        return new PagedListDto<PaymentNotification?>()
        {
            TotalCount = totalCount,
            PageSize = filterDto.PageSize,
            PageIndex = filterDto.PageIndex,
            Data = pagePaymentNotifications
        };
    }


    public async Task<PaymentNotification?> GetByIdAsync(string id)
    {
        var paymentNotification = await appDbContext.PaymentNotifications.FirstOrDefaultAsync(x => x.Id == id);
        return paymentNotification;
    }

    public async Task<PagedListDto<PaymentNotification?>> GetListAsync(FilterDto filterDto)
    {
        IQueryable<PaymentNotification> query;
        if (string.IsNullOrWhiteSpace(filterDto.Keyword))
        {
            query = appDbContext.PaymentNotifications;
        }
        else
        {
            filterDto.Keyword = filterDto.Keyword.Trim().ToLower();
            query = appDbContext.PaymentNotifications.Where(x => x.Id.ToLower().Contains(filterDto.Keyword) ||
            x.NotificationStatus.ToLower().Contains(filterDto.Keyword) || x.Payment!.Id.ToLower().Contains(filterDto.Keyword));
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
            return new PagedListDto<PaymentNotification?>()
            {
                TotalCount = data!.Count(),
                PageSize = filterDto.PageSize,
                PageIndex = filterDto.PageIndex,
                Data = data!
            };
        }
        var totalCount = await query.CountAsync();
        var totalPage = (int)Math.Ceiling(totalCount / (double)filterDto.PageSize);
        var pagePaymentNotifications = await query.Skip((filterDto.PageIndex - 1) * filterDto.PageSize).Take(filterDto.PageSize).ToListAsync();
        return new PagedListDto<PaymentNotification?>()
        {
            TotalCount = totalCount,
            PageSize = filterDto.PageSize,
            PageIndex = filterDto.PageIndex,
            Data = pagePaymentNotifications
        };
    }

    public async Task<PaymentNotification?> InsertAsync(PaymentNotification paymentNotification)
    {
        paymentNotification.Id = GenerateHelper.GenerateKeyNumber("NOTI");
        appDbContext.PaymentNotifications.Add(paymentNotification);
        var result = await appDbContext.SaveChangesAsync();
        return result > 0 ? paymentNotification : null;
    }

    public async Task<PaymentNotification?> UpdateAsync(string id, PaymentNotification paymentNotification)
    {
        var paymentNotificationModel = await appDbContext.PaymentNotifications.FirstOrDefaultAsync(x => x.Id == id);
        if (paymentNotificationModel == null) return null;
        paymentNotificationModel.NotificationStatus = paymentNotification.NotificationStatus;
        paymentNotificationModel.NotificationMessage = paymentNotification.NotificationMessage;
        paymentNotificationModel.NotificationResDate = paymentNotification.NotificationResDate;
        paymentNotificationModel.NotificationContent = paymentNotification.NotificationContent;
        paymentNotificationModel.NotificationSignature = paymentNotification.NotificationSignature;
        paymentNotificationModel.NotificaitonDate = paymentNotification.NotificaitonDate;
        paymentNotificationModel.Merchant = paymentNotification.Merchant;
        paymentNotificationModel.NotificationAmount = paymentNotification.NotificationAmount;
        paymentNotificationModel.Payment = paymentNotification.Payment;
        paymentNotificationModel.Payment = paymentNotification.Payment;
        appDbContext.Entry(paymentNotificationModel).State = EntityState.Modified;
        var result = await appDbContext.SaveChangesAsync();
        return result > 0 ? paymentNotificationModel : null;
    }
}
