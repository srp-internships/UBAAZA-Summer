﻿using System.Security.Cryptography;

namespace BlazorEcommerce.Server.Services.AuthService
{
	public class AuthService : IAuthService
	{
		private readonly DataContext _context;

		public AuthService(DataContext context)
		{
		     _context = context;
		}

		public async Task<ServiceResponse<int>> Register(User user, string password)
		{
			if (await UserExists(user.Email))
			{
				return new ServiceResponse<int> { 
					Success = false, 
					Message = "User alreadu exists... "
				};
			}
			CreatePasswordHash(password,out byte[] passwordHash , out byte[] passwordsalt);

			user.PasswordHash = passwordHash;
			user.PasswordSalt = passwordsalt;
			_context.Users.Add(user);
			await _context.SaveChangesAsync();
			return new ServiceResponse<int> { Data=user.Id,Message="Registration successful!"};
		
		}

		public async Task<bool> UserExists(string email)
		{
			if (await _context.Users.AnyAsync(user=>user.Email.ToLower()
			.Equals(email.ToLower())))
			{
				return true;
			}
			return false;
		}
		private void CreatePasswordHash(string password ,out byte[] passwordHash, out byte[] passwordSolt)
		{
			using (var hmac =new HMACSHA512())
			{
				passwordSolt = hmac.Key;
				passwordHash = hmac
					.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
			}
		}
	}
}
