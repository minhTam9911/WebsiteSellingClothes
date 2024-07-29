using Common.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Repositories;
public class OrderDetailRepository : IOrderDetailRepository
{
	private readonly AppDbContext appDbContext;

	public OrderDetailRepository(AppDbContext appDbContext)
	{
		this.appDbContext = appDbContext;
	}

    public async Task<int?> DeleteAsync(int id, Guid userId)
    {
        var result = await appDbContext.OrderDetails.Where(x => x.Id == id && x.User!.Id == userId).ExecuteDeleteAsync();
        return result;
    }

    public async Task<int?> DeleteByOrderIdAsync(string orderId, Guid userId)
    {
        var result = await appDbContext.OrderDetails.Where(x => x.Order!.Id == orderId && x.User!.Id == userId).ExecuteDeleteAsync();
        return result;
    }

    public async Task<List<OrderDetail>?> GetAllAsync()
	{
		return await appDbContext.OrderDetails.ToListAsync();
	}

    public async Task<PagedListDto<OrderDetail>?> GetAllForMeAsync(FilterDto filter, Guid userId)
    {
        IQueryable<OrderDetail> query;
        if (string.IsNullOrWhiteSpace(filter.Keyword))
        {
            query = appDbContext.OrderDetails;
        }
        else
        {
            filter.Keyword = filter.Keyword.Trim().ToLower();
            query = appDbContext.OrderDetails.Where(x => x.User!.Id == userId && (
                                            x.Order!.Id.ToString().Contains(filter.Keyword) ||
                                            x.Product!.Name.ToLower().Contains(filter.Keyword) ||
                                            x.Quantity.ToString().Contains(filter.Keyword))
                                            );
        }
        if (!string.IsNullOrWhiteSpace(filter.SortColumn))
        {
            filter.SortColumn = filter.SortColumn.Trim();
            StringBuilder orderQueryBuilder = new StringBuilder();
            PropertyInfo[] propertyInfo = typeof(OrderDetail).GetProperties();
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
            return new PagedListDto<OrderDetail>()
            {
                TotalCount = data!.Count(),
                PageSize = filter.PageSize,
                PageIndex = filter.PageIndex,
                Data = data!
            };
        }
        var totalCount = await query.CountAsync();
        var totalPage = (int)Math.Ceiling(totalCount / (double)filter.PageSize);
        var pageOrderDetails = await query.Skip((filter.PageIndex - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
        return new PagedListDto<OrderDetail>()
        {
            TotalCount = totalCount,
            PageSize = filter.PageSize,
            PageIndex = filter.PageIndex,
            Data = pageOrderDetails
        };
    }

    public async Task<OrderDetail?> GetByIdAsync(int id,Guid userId)
	{
        var user = await appDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (user!.Role!.Id == 1) return await appDbContext.OrderDetails.FirstOrDefaultAsync(x => x.Id == id);
        var orderDetail = await appDbContext.OrderDetails.FirstOrDefaultAsync(x => x.Id == id);
        if (orderDetail!.User!.Id == userId) return orderDetail;
        throw new UnauthorizedAccessException("Forbidden");
	}

    public async Task<PagedListDto<OrderDetail>?> GetListAsync(FilterDto filter)
	{
        IQueryable<OrderDetail> query;
        if (string.IsNullOrWhiteSpace(filter.Keyword))
        {
            query = appDbContext.OrderDetails;
        }
        else
        {
            filter.Keyword = filter.Keyword.Trim().ToLower();
            query = appDbContext.OrderDetails.Where(x =>
                                            x.Order!.Id.ToString().Contains(filter.Keyword) ||
                                            x.Product!.Name.ToLower().Contains(filter.Keyword) ||
                                            x.Quantity.ToString().Contains(filter.Keyword) || 
                                            x.User!.Id.ToString().Contains(filter.Keyword)
                                            );
        }
        if (!string.IsNullOrWhiteSpace(filter.SortColumn))
        {
            filter.SortColumn = filter.SortColumn.Trim();
            StringBuilder orderQueryBuilder = new StringBuilder();
            PropertyInfo[] propertyInfo = typeof(OrderDetail).GetProperties();
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
            return new PagedListDto<OrderDetail>()
            {
                TotalCount = data!.Count(),
                PageSize = filter.PageSize,
                PageIndex = filter.PageIndex,
                Data = data!
            };
        }
        var totalCount = await query.CountAsync();
        var totalPage = (int)Math.Ceiling(totalCount / (double)filter.PageSize);
        var pageOrderDetails = await query.Skip((filter.PageIndex - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
        return new PagedListDto<OrderDetail>()
        {
            TotalCount = totalCount,
            PageSize = filter.PageSize,
            PageIndex = filter.PageIndex,
            Data = pageOrderDetails
        };
    }

	public async Task<OrderDetail?> InsertAsync(OrderDetail orderDetail)
	{
        appDbContext.OrderDetails.Add(orderDetail);
        var result = await appDbContext.SaveChangesAsync();
        return result > 0 ? orderDetail : null; 
    }

	public async Task<OrderDetail?> UpdateAsync(int id, OrderDetail orderDetail)
	{
        var orderDetailModel = await appDbContext.OrderDetails.FirstOrDefaultAsync(x => x.Id == id);
        if (orderDetailModel == null) throw new BadHttpRequestException("Order detail not exist");
        orderDetailModel.Order = orderDetail.Order;
        orderDetailModel.Price = orderDetail.Price;
        orderDetailModel.Quantity = orderDetail.Quantity;
        orderDetailModel.Product = orderDetail.Product;
        orderDetailModel.TotalAmount = orderDetail.TotalAmount;
        orderDetailModel.User = orderDetail.User;
        appDbContext.Entry(orderDetailModel).State = EntityState.Modified;
        var result = await appDbContext.SaveChangesAsync();
        return result > 0 ? orderDetail : null;
    }

    public async Task<OrderDetail?> UpdateAsync(OrderDetail orderDetail)
    {
        appDbContext.Entry(orderDetail).State = EntityState.Modified;
        var result = await appDbContext.SaveChangesAsync();
        return result > 0 ? orderDetail : null;
    }
}
