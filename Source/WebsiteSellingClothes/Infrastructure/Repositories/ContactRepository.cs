using Common.DTOs;
using Common.Helplers;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;
public class ContactRepository : IContactRepository
{
	public readonly AppDbContext appDbContext;
	public readonly IConfiguration configuration;

	public ContactRepository(AppDbContext appDbContext, IConfiguration configuration)
	{
		this.appDbContext = appDbContext;
		this.configuration = configuration;
	}

	public async Task<int> DeleteAsync(int id)
	{
		var contact = await appDbContext.Contacts.FirstOrDefaultAsync(x => x.Id == id);
		if (contact == null) throw new BadHttpRequestException("Role does not exist");
		appDbContext.Contacts.Remove(contact);
		var result = await appDbContext.SaveChangesAsync();
		return result;
	}

	public async Task<List<Contact>?> GetAllAsync()
	{
		return await appDbContext.Contacts.ToListAsync();
	}

	public async Task<Contact?> GetByIdAsync(int id)
	{
		return await appDbContext.Contacts.FirstOrDefaultAsync(x => x.Id == id);
	}

	public async Task<PagedListDto<Contact>?> GetListAsync(FilterDto filter)
	{
		IQueryable<Contact> query;
		if (string.IsNullOrWhiteSpace(filter.Keyword))
		{
			query = appDbContext.Contacts;
		}
		else
		{
			filter.Keyword = filter.Keyword.Trim().ToLower();
			query = appDbContext.Contacts.Where(
				x => x.Name.ToLower().Contains(filter.Keyword) ||
				x.Title.ToLower().Contains(filter.Keyword) ||
				x.Content.ToLower().Contains(filter.Keyword) ||
				x.Email.ToLower().Contains(filter.Keyword) ||
				x.Phone.ToLower().Contains(filter.Keyword) ||
				x.IsSent.ToString().Contains(filter.Keyword)
				);
		}
		if (!string.IsNullOrWhiteSpace(filter.SortColumn))
		{
			filter.SortColumn = filter.SortColumn.Trim();
			StringBuilder orderQueryBuilder = new StringBuilder();
			PropertyInfo[] propertyInfo = typeof(Contact).GetProperties();
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
			return new PagedListDto<Contact>()
			{
				TotalCount = data!.Count(),
				PageSize = filter.PageSize,
				PageIndex = filter.PageIndex,
				Data = data
			};
		}
		var totalCount = await query.CountAsync();
		var totalPage = (int)Math.Ceiling(totalCount / (double)filter.PageSize);
		var pageContacts = await query.Skip((filter.PageIndex - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
		return new PagedListDto<Contact>()
		{
			TotalCount = totalCount,
			PageSize = filter.PageSize,
			PageIndex = filter.PageIndex,
			Data = pageContacts
		};
	}

	public async Task<Contact?> InsertAsync(Contact contact)
	{
		contact.CreatedDate = DateTime.Now;
		contact.UpdatedDate = DateTime.Now;
		var mailHelper = new MailHelper(configuration);
		var content = MailHelper.HtmlContact(contact.Name, contact.Email, contact.Phone, contact.Content);
		var check = mailHelper.Send(configuration["Contact:EmailNoReply"]!, configuration["Gmail:Username"]!, contact.Title, content);
		if (!check)
		{
			throw new Exception("Send email failure");
		}
		contact.IsSent = true;
		await appDbContext.Contacts.AddAsync(contact);
		var result = await appDbContext.SaveChangesAsync() > 0;
		return result ? contact : null;
	}

	/*public async Task<Contact?> UpdateAsync(int id, Contact contact)
	{
		var contactModel = await appDbContext.Contacts.FirstOrDefaultAsync(x => x.Id == id);
		if (contactModel == null) throw new BadHttpRequestException("Role doesn't exist");
		contactModel.UpdatedDate = DateTime.Now;
		appDbContext.Entry(contactModel).State = EntityState.Modified;
		var result = await appDbContext.SaveChangesAsync();
		return result > 0 ? contactModel : null;
	}*/
}
