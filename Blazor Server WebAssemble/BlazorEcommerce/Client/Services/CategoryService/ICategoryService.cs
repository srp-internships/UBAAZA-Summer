namespace BlazorEcommerce.Client.Services.CategoryService
{
	public interface ICategoryService
	{
		List<Category> Categories{ get; set; }
		Task GetCategories();

		//Task<ServiceResponse<Product>> GetProduct(int productId);
	}
}
