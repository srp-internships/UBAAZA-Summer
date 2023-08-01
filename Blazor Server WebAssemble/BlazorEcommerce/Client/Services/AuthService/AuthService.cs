namespace BlazorEcommerce.Client.Services.AuthService
{
	public class AuthService : IAuthService
	{
		private readonly HttpClient _httpClient;

		public AuthService(HttpClient httpClient)
        {
		    _httpClient = httpClient;
		}

		public async Task<ServiceResponse<string>> Login(UserLogin request)
		{
			var result = await _httpClient.PostAsJsonAsync("api/Auth/login", request);
			return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
		}

		public async Task<ServiceResponse<int>> Register(UserRegister request)
		{
			var result = await _httpClient.PostAsJsonAsync("api/Auth/register", request);
			return await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
		}
	}
}
