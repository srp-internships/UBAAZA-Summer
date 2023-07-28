namespace BlazorEcommerce.Server.Services.AuthService
{
	public interface IAuthService
	{
		public Task<ServiceResponse<int>> Register(User user, string password); 
		public Task<bool> UserExists(string email);
	}
}
