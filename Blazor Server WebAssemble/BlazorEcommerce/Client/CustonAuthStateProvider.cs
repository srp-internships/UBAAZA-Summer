using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Data;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Claims;
using System.Text.Json;

namespace BlazorEcommerce.Client
{
	public class CustonAuthStateProvider : AuthenticationStateProvider
	{
		private readonly ILocalStorageService _localStorageService;
		private readonly HttpClient _httpClient;

		public CustonAuthStateProvider(ILocalStorageService localStorageService, HttpClient httpClient)
		{
			_localStorageService = localStorageService;
			_httpClient = httpClient;
		}
		public override async Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			string authToken = await _localStorageService.GetItemAsStringAsync(LocalStorageKeys.authToken);

			var identity = new ClaimsIdentity();
			_httpClient.DefaultRequestHeaders.Authorization = null;
			if (!string.IsNullOrEmpty(authToken))
			{
				try
				{
					identity = new ClaimsIdentity(ParseClimsFromJwt(authToken), "jwt");
					_httpClient.DefaultRequestHeaders.Authorization =
						new AuthenticationHeaderValue("Bearer", authToken.Replace("\"", ""));

				}
				catch (Exception ex)
				{
					await _localStorageService.RemoveItemAsync(LocalStorageKeys.authToken);
					identity = new ClaimsIdentity();
					Console.WriteLine(ex.Message.ToString());
				}
			}
			var user = new ClaimsPrincipal(identity);
			var state = new AuthenticationState(user);
			NotifyAuthenticationStateChanged(Task.FromResult(state));
			return state;

		}
		private byte[] ParseBase64WithoutPadding(string bose64)
		{
			switch (bose64.Length % 4)
			{
				case 2: bose64 += "=="; break;
				case 3: bose64 += "="; break;
			}
			return Convert.FromBase64String(bose64);
		}
		private IEnumerable<Claim> ParseClimsFromJwt(string jwt)
		{
			var paylond = jwt.Split('.')[1];
			var jsonBytes = ParseBase64WithoutPadding(paylond);
			var keyValuePairs = JsonSerializer
				.Deserialize<Dictionary<string, object>>(jsonBytes);
			var claims = keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
			return claims;
		}
	}
}
