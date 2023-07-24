namespace BlazorEcommerce.Server.Services.ProductService
{
	public interface IProductService
	{
		Task<ServiceResponse<List<Product>>> GetProductsAsync();
		Task<ServiceResponse<Product>> GetProductsAsync(int productId);
		Task<ServiceResponse<List<Product>>>  GetProductsByCategory(string categoruUrl);
		Task<ServiceResponse<List<Product>>>  SearchProducts(string searchText);
		Task<ServiceResponse<List<string>>>  GetProductSearchSuggestions(string searchText);
	}
}
