using InsightApi.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Headers;

namespace InsightApi.Api
{
	public class InsightAccountsApi
	{
		private readonly HttpClient _client;
		private readonly InsightApiOptions _options;

		// TODO: Add caching for the accounts? (IMemory Cache / Redis).

		public InsightAccountsApi(IOptions<InsightApiOptions> options, HttpClient client)
		{
			_client = client;
			_options = options.Value;

			_client.BaseAddress = new Uri(_options.BaseUrl);
		}

		public async Task<List<Account>> GetAccounts()
		{
			try
			{
				// Run the GET request with the resilience pipeline.
				var request = new HttpRequestMessage(HttpMethod.Get, "https://mtls.dh.example.com/cds-au/v1/banking/accounts");

				request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				request.Headers.Add("x-v", "3");
				request.Headers.Add("x-min-v", "1");
				request.Headers.Add("x-fapi-interaction-id", Guid.NewGuid().ToString());
				request.Headers.Add("x-fapi-auth-date", DateTime.UtcNow.ToString("R")); // RFC1123 format
				request.Headers.Add("x-fapi-customer-ip-address", "203.0.113.42");

				var response = await _client.SendAsync(request);
				response.EnsureSuccessStatusCode();

				if (!response.IsSuccessStatusCode)
					throw new Exception($"API Error: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");

				var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

				var accountsResponse = JsonConvert.DeserializeObject<AccountResponse>(json);

				// If the deserialization is successful, return the Accounts list, otherwise return an empty list.
				return accountsResponse.Accounts ?? [];
			}
			catch (Exception e)
			{
				// TODO: Log error properly (replace with ILogger).
				Console.WriteLine($"Error fetching accounts: {e.Message}");

				// Return empty list to prevent crashes.
				return [];
			}
		}
	}

	public class InsightApiOptions
	{
		[Required]
		public string BaseUrl { get; set; } = null!;

		[Required]
		public string Apikey { get; set; } = null!;
	}
}
