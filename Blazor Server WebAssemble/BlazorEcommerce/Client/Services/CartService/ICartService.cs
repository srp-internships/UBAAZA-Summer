namespace BlazorEcommerce.Client.Services.CartService
{
	public interface ICartService
	{
		event Action OnChange;
		Task AddToCard(CartItem cartItem);
		Task<List<CartProductResponse>> GetCartProducts();
		Task RemoveProductFromCart(int productId, int productTypeId);
		Task UpdateQuantity(CartProductResponse product);
		Task StoreCartItem(bool emptyLocalCart);

		Task GetCartItemCount();
	}
}
