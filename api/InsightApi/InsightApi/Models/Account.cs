using InsightApi.Enums;
using System.ComponentModel.DataAnnotations;

namespace InsightApi.Models;

public class Account
{
	[Key]
	public string AccountId { get; set; }

	public DateTime? CreationDate { get; set; }

	[Required]
	public string DisplayName { get; set; }

	public string? Nickname { get; set; }

	public OpenStatus? OpenStatus { get; set; }

	public bool? IsOwned { get; set; }

	[Required]
	public AccountOwnership AccountOwnership { get; set; }

	[Required]
	public string MaskedNumber { get; set; }

	[Required]
	public BankingProductCategory ProductCategory { get; set; }

	[Required]
	public string ProductName { get; set; }
}