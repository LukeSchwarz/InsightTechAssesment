using InsightApi.Controllers;
using InsightApi.Enums;
using InsightApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsightApi.Tests;

public class AccountControllerTest
{
	/// <summary>
	/// Sample test using in memory database to test the AccountController.
	/// </summary>
	/// <returns></returns>
	[Fact]
	public async Task GetAccounts_FiltersByProductCategory()
	{
		// Arrange: setup in-memory EF context
		var options = new DbContextOptionsBuilder<InsightApiDbContext>()
			.UseInMemoryDatabase(databaseName: "TestDb")
			.Options;

		await using var context = new InsightApiDbContext(options);
		context.Account.AddRange(new List<Account>
		{
			new Account
			{
				AccountId = "1",
				DisplayName = "Business Loan Account",
				MaskedNumber = "****1234",
				ProductCategory = BankingProductCategory.REGULATED_TRUST_ACCOUNTS,
				AccountOwnership = AccountOwnership.ONE_PARTY,
				ProductName = "BizLoan",
				IsOwned = true
			},
			new Account
			{
				AccountId = "2",
				DisplayName = "Personal Loan Account",
				MaskedNumber = "****5678",
				ProductCategory = BankingProductCategory.REGULATED_TRUST_ACCOUNTS,
				AccountOwnership = AccountOwnership.ONE_PARTY,
				ProductName = "PersonalLoan",
				IsOwned = false
			}
		});
		await context.SaveChangesAsync();

		var controller = new AccountController(context);

		var result = await controller.GetAccounts(BankingProductCategory.REGULATED_TRUST_ACCOUNTS, null, null, 1,~25);

		var okResult = Assert.IsType<OkObjectResult>(result);
		var returned = Assert.IsAssignableFrom<IQueryable<Account>>(okResult.Value);

		Assert.Equal("1", returned.First().AccountId);
	}
}