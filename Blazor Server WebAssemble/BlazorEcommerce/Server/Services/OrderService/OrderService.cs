using System.Security.Claims;

namespace BlazorEcommerce.Server.Services.OrderService
{
	public class OrderService : IOrderService
	{
		private readonly DataContext _context;
		private readonly ICartService _cartService;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public OrderService(DataContext context,
			ICartService cartService,
			IHttpContextAccessor httpContextAccessor)
		{
			_context = context;
			_cartService = cartService;
			_httpContextAccessor = httpContextAccessor;
		}
		private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
		public async Task<ServiceResponse<bool>> PlanceOpder()
		{
			var products = (await _cartService.GetDbCartProducts()).Data;
			decimal totalPrice = 0;
			products.ForEach(product => totalPrice += product.Price + product.Quantity);

			var orderItems = new List<OrderItem>();
			products.ForEach(product => orderItems.Add(new OrderItem
			{
				ProductId = product.ProductId,
				ProductTypeId=product.ProductTypeId,
				Quantity = product.Quantity,
				TotalPrice= product.Price+product.Quantity
			}));
			var order = new Order
			{
				UserID = GetUserId(),
				OrderData = DateTime.Now,
				TotalPrice = totalPrice,
				OrderItems = orderItems
			};
			_context.Orders.Add(order);
			_context.CartItems.RemoveRange(_context.CartItems
				.Where(ci => ci.UserId == GetUserId()));
			await _context.SaveChangesAsync();
			return new ServiceResponse<bool> { Data=true };

		}
	}
}
