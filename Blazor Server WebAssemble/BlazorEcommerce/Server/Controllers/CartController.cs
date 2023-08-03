﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlazorEcommerce.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CartController : ControllerBase
	{
		private readonly ICartService _cartService;

		public CartController(ICartService cartService)
		{
			_cartService = cartService;
		}

		[HttpPost("products")]
		public async Task<ActionResult<ServiceResponse<List<CartProductResponse>>>> GetCardProducts(List<CartItem> cartItems)
		{
			var result = await _cartService.GetCartProducts(cartItems);
			return Ok(result);
		}

		[HttpPost]
		public async Task<ActionResult<ServiceResponse<List<CartProductResponse>>>> StoreCartItems(List<CartItem> cartItems)
		{
			var userI = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
			var result = await _cartService.StoreCartItems(cartItems);
			return Ok(result);
		}

		[HttpGet("count")]
		public async Task<IActionResult> GetCartItemsCount()
		{
			return Ok(await _cartService.GetCartItemsCount());
		}

	}
}
