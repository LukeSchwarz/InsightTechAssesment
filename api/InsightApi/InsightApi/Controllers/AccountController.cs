using AutoMapper;
using InsightApi.Enums;
using InsightApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsightApi.Controllers
{
	[ApiController]
	[Route("cds-au/v1/banking/accounts")]
	public class AccountController : ControllerBase
	{
		private readonly InsightApiDbContext _insightAccountsApiDbContext;

		public AccountController(InsightApiDbContext context)
		{
			_insightAccountsApiDbContext = context;
		}

		/// <summary>
		/// Use auto mapper to map the Account to AccountDto.
		/// </summary>
		private class AccountControllerProfile : Profile
		{
			public AccountControllerProfile()
			{
				CreateMap<Account, AccountDto>();
			}
		}

		[HttpGet]
		public async Task<IActionResult> GetAccounts(
			[FromQuery(Name = "product-category")] BankingProductCategory? productCategory,
			[FromQuery(Name = "open-status")] OpenStatus? openStatus,
			[FromQuery(Name = "is-owned")] bool? isOwned,
			[FromQuery(Name = "page")] int page = 1,
			[FromQuery(Name = "page-size")] int pageSize = 25)
		{

			// Add a database for quering.
			var allAccounts = await _insightAccountsApiDbContext.Account.ToListAsync();

			var filtered = allAccounts.AsQueryable();

			if (productCategory != null)
				filtered = filtered.Where(a => a.ProductCategory == productCategory);

			if (openStatus != null)
				filtered = filtered.Where(a => a.OpenStatus == openStatus);

			if (isOwned.HasValue)
				filtered = filtered.Where(a => a.IsOwned == isOwned.Value);

			return Ok(filtered); // new { data = filtered.Skip((page - 1) * pageSize).Take(pageSize) });
		}
	}
}
