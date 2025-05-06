using InsightApi.Api;
using InsightApi.Enums;
using InsightApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace InsightApi.Controllers
{
	[ApiController]
	[Route("cds-au/v1/banking/accounts")]
	public class AccountController : ControllerBase
	{
		// private readonly InsightAccountsApiDbContext _insightAccountsApiDbContext;

		public AccountController()
		{
		}

		[HttpGet]
		public IActionResult GetAccounts(
			[FromQuery(Name = "product-category")] BankingProductCategory? productCategory,
			[FromQuery(Name = "open-status")] OpenStatus? openStatus,
			[FromQuery(Name = "is-owned")] bool? isOwned,
			[FromQuery(Name = "page")] int page = 1,
			[FromQuery(Name = "page-size")] int pageSize = 25)
		{

			// Add a database for quering.
			// await _insightAccountsApiDbContext.Account.ToListAsync();

			// TODO: Remove test data account.
			var allAccounts = new List<Account> {
				new Account { AccountId = "123", DisplayName = "Main Account", ProductCategory = BankingProductCategory.TRANS_AND_SAVINGS_ACCOUNTS, OpenStatus = OpenStatus.OPEN, IsOwned = true },
				new Account { AccountId = "124", DisplayName = "Second Account", ProductCategory = BankingProductCategory.TRADE_FINANCE, OpenStatus = OpenStatus.OPEN, IsOwned = true },
				new Account { AccountId = "125", DisplayName = "Third Account", ProductCategory = BankingProductCategory.TRANS_AND_SAVINGS_ACCOUNTS, OpenStatus = OpenStatus.OPEN, IsOwned = true },
			};

			var filtered = allAccounts.AsQueryable();

			if (productCategory != null)
				filtered = filtered.Where(a => a.ProductCategory == productCategory);

			if (openStatus != null)
				filtered = filtered.Where(a => a.OpenStatus == openStatus);

			if (isOwned.HasValue)
				filtered = filtered.Where(a => a.IsOwned == isOwned.Value);

			return Ok(allAccounts); // new { data = filtered.Skip((page - 1) * pageSize).Take(pageSize) });
		}
	}
}
