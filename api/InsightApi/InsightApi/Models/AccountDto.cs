using InsightApi.Enums;

namespace InsightApi.Models
{
	public class AccountDto
	{
		public string AccountId { get; set; }

		public DateTime? CreationDate { get; set; }

		public string DisplayName { get; set; }

		public string? Nickname { get; set; }

		public OpenStatus? OpenStatus { get; set; }

		public bool? IsOwned { get; set; }

		public AccountOwnership AccountOwnership { get; set; }

		public string MaskedNumber { get; set; }

		public BankingProductCategory ProductCategory { get; set; }

		public string ProductName { get; set; }
	}
}
