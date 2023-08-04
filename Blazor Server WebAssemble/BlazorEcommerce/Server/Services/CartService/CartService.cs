﻿using BlazorEcommerce.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Security.Claims;

namespace BlazorEcommerce.Server.Services.CartService
{
	public class CartService : ICartService
	{
		private readonly DataContext _context;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public CartService(DataContext context, IHttpContextAccessor httpContextAccessor)
		{
			_context = context;
			_httpContextAccessor = httpContextAccessor;
		}
		private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

		public async Task<ServiceResponse<List<CartProductResponse>>> GetCartProducts(List<CartItem> cartItems)
		{
			var result = new ServiceResponse<List<CartProductResponse>>
			{
				Data = new List<CartProductResponse>()
			};
			foreach (var item in cartItems)
			{
				var product = await _context.Products
					.Where(p => p.Id == item.ProductId)
					.FirstOrDefaultAsync();
				if (product == null)
				{
					continue;
				}
				var productVariant = await _context.ProductVariant
					.Where(v => v.ProductId == item.ProductId
					&& v.ProductTypeId == item.ProductTypeId)
					.Include(v => v.ProductType)
					.FirstOrDefaultAsync();
				if (productVariant == null)
				{
					continue;
				}
				var cartProduct = new CartProductResponse
				{
					ProductId = product.Id,
					Title = product.Title,
					ImageUrl = product.ImageUrl,
					Price = productVariant.Price,
					ProductType = productVariant.ProductType.Name,
					ProductTypeId = productVariant.ProductTypeId,
					Quantity = item.Quantity
				};
				result.Data.Add(cartProduct);
			}
			return result;
		}

		public async Task<ServiceResponse<List<CartProductResponse>>> StoreCartItems(List<CartItem> cartItems)
		{

			cartItems.ForEach(cartItem => cartItem.UserId = GetUserId());
			_context.CartItems.AddRange(cartItems);
			try
			{
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				var exp = ex.Message.ToString();
			}



			return await GetDbCartProducts();
		}

		public async Task<ServiceResponse<int>> GetCartItemsCount()
		{
			var count = (await _context.CartItems.Where(ci => ci.UserId == GetUserId()).ToListAsync()).Count;
			return new ServiceResponse<int>
			{
				Data = count
			};
		}

		public async Task<ServiceResponse<List<CartProductResponse>>> GetDbCartProducts()
		{
			return await GetCartProducts(await _context.CartItems
				.Where(ci => ci.UserId == GetUserId()).ToListAsync()); 
		}

		public async Task<ServiceResponse<bool>> AddToCart(CartItem cartItem)
		{
			cartItem.UserId = GetUserId();
			var SameItem = await _context.CartItems
				.FirstOrDefaultAsync(ci => ci.ProductId == cartItem.ProductId && 
				ci.ProductTypeId == cartItem.ProductTypeId && ci.UserId == cartItem.UserId);
			if (SameItem == null)
			{
				_context.CartItems.Add(cartItem);
			}
			else
			{
				SameItem.Quantity+= cartItem.Quantity;
			}
			await _context.SaveChangesAsync();
			return new ServiceResponse<bool> {  Data = true };

		}

		public async Task<ServiceResponse<bool>> UpdateQuantity(CartItem cartItem)
		{
			var dbCartItem = await _context.CartItems
				.FirstOrDefaultAsync(ci => ci.ProductId == cartItem.ProductId &&
				ci.ProductTypeId == cartItem.ProductTypeId && ci.UserId ==cartItem.UserId);
			if (dbCartItem == null)
			{
				return new ServiceResponse<bool>
				{
					Data = false,
					Success = false,
					Message = " Cart item does not exist."

				};
			}
			dbCartItem.Quantity = cartItem.Quantity;
			await _context.SaveChangesAsync();
			return new ServiceResponse<bool> { Data = true };
		}

		public async Task<ServiceResponse<bool>> RemoveItemFromCart(int productId, int productTypeId)
		{
			var dbCartItem = await _context.CartItems
				.FirstOrDefaultAsync(ci => ci.ProductId ==productId &&
				ci.ProductTypeId == productTypeId && ci.UserId == GetUserId());
			if (dbCartItem == null)
			{
				return new ServiceResponse<bool>
				{
					Data = false,
					Success = false,
					Message = " Cart item does not exist."

				};
			}

			_context.CartItems.Remove(dbCartItem);
			await _context.SaveChangesAsync();
			return new ServiceResponse<bool> { Data= true };
		}
	}
}
