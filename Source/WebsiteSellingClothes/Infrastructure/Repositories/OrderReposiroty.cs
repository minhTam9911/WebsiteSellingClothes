using Common.DTOs;
using Common.Helpers;
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
public class OrderReposiroty : IOrderRepository
{
    private readonly AppDbContext appDbContext;

    public OrderReposiroty(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async Task<int?> DeleteAsync(string id, Guid userId)
    {
        var result = await appDbContext.Orders.Where(x => x.Id == id && x.User!.Id == userId).ExecuteDeleteAsync();
        return result;
    }

    public async Task<List<Order>?> GetAllAsync()
    {
        return await appDbContext.Orders.ToListAsync();
    }

    public async Task<PagedListDto<Order>?> GetAllForMeAsync(Guid userId, FilterDto filter)
    {
        IQueryable<Order> query;
        if (string.IsNullOrWhiteSpace(filter.Keyword))
        {
            query = appDbContext.Orders.Where(x => x.User!.Id == userId);
        }
        else
        {
            filter.Keyword = filter.Keyword.Trim().ToLower();
            query = appDbContext.Orders.Where(x => x.User!.Id == userId && (x.Id.ToString().Contains(filter.Keyword) ||
                                            x.Status.ToLower().Contains(filter.Keyword) ||
                                            x.Quantity.ToString().Contains(filter.Keyword) ||
                                            x.CreateDate.ToString().Contains(filter.Keyword) ||
                                            x.Amount.ToString().Contains(filter.Keyword))
                                            );
        }
        if (!string.IsNullOrWhiteSpace(filter.SortColumn))
        {
            filter.SortColumn = filter.SortColumn.Trim();
            StringBuilder orderQueryBuilder = new StringBuilder();
            PropertyInfo[] propertyInfo = typeof(Order).GetProperties();
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
            return new PagedListDto<Order>()
            {
                TotalCount = data.Count(),
                PageSize = filter.PageSize,
                PageIndex = filter.PageIndex,
                Data = data
            };
        }
        var totalCount = await query.CountAsync();
        var totalPage = (int)Math.Ceiling(totalCount / (double)filter.PageSize);
        var pageOrders = await query.Skip((filter.PageIndex - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
        return new PagedListDto<Order>()
        {
            TotalCount = totalCount,
            PageSize = filter.PageSize,
            PageIndex = filter.PageIndex,
            Data = pageOrders
        };
    }

    public async Task<Order?> GetByIdAsync(string id, Guid userId)
    {
        var user = await appDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
        var order = await appDbContext.Orders.FirstOrDefaultAsync(x => x.Id == id);
        if (user!.Role!.Id == 1) return order;
        if (order!.User!.Id == userId) return order;
        throw new UnauthorizedAccessException("Forbidden");
    }

    public async Task<PagedListDto<Order>?> GetListAsync(FilterDto filter)
    {
        IQueryable<Order> query;
        if (string.IsNullOrWhiteSpace(filter.Keyword))
        {
            query = appDbContext.Orders;
        }
        else
        {
            filter.Keyword = filter.Keyword.Trim().ToLower();
            query = appDbContext.Orders.Where(x => x.Id.ToString().Contains(filter.Keyword) ||
                                            x.Status.ToLower().Contains(filter.Keyword) ||
                                            x.Quantity.ToString().Contains(filter.Keyword) ||
                                            x.CreateDate.ToString().Contains(filter.Keyword) ||
                                            x.Amount.ToString().Contains(filter.Keyword)
                                            );
        }
        if (!string.IsNullOrWhiteSpace(filter.SortColumn))
        {
            filter.SortColumn = filter.SortColumn.Trim();
            StringBuilder orderQueryBuilder = new StringBuilder();
            PropertyInfo[] propertyInfo = typeof(Order).GetProperties();
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
            return new PagedListDto<Order>()
            {
                TotalCount = data!.Count(),
                PageSize = filter.PageSize,
                PageIndex = filter.PageIndex,
                Data = data!
            };
        }
        var totalCount = await query.CountAsync();
        var totalPage = (int)Math.Ceiling(totalCount / (double)filter.PageSize);
        var pageOrders = await query.Skip((filter.PageIndex - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
        return new PagedListDto<Order>()
        {
            TotalCount = totalCount,
            PageSize = filter.PageSize,
            PageIndex = filter.PageIndex,
            Data = pageOrders
        };
    }

    public async Task<Order?> InsertAsync(Order order)
    {
        order.Id = GenerateHelper.GenerateOrderId();
        order.CreateDate = DateTime.Now;
        appDbContext.Orders.Add(order);
        var result = await appDbContext.SaveChangesAsync();
        return result > 0 ? order : null;
    }

    public async Task<Order?> SetPaidAsync(string id, Payment payment)
    {
        var order = await appDbContext.Orders.FirstOrDefaultAsync(x => x.Id == id);
        order.Status = "Paid";
        order.Payment = payment;
        appDbContext.Entry(order).State = EntityState.Modified;
        var result = await appDbContext.SaveChangesAsync();

        if (result > 0)
        {
            var data = await appDbContext.Orders.FirstOrDefaultAsync(x => x.Id == id);
            return data;
        }
        return null;
    }

    public async Task<Order?> UpdateAsync(Order order)
    {
        appDbContext.Entry(order).State = EntityState.Modified;
        var result = await appDbContext.SaveChangesAsync();
        return result > 0 ? order : null;
    }
}
