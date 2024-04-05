using StoreApp.DataAccess;
using StoreApp.Shared.Interfaces;
using StoreApp.Shared.Models;

namespace DataAccess.Repositories;

public class CategoryRepository(StoreDbContext context) : ICategoryService<ProductCategory>
{
    public async Task<IEnumerable<ProductCategory>> GetAllAsync()
	{
		return context.ProductCategories;
	}

	public async Task<ProductCategory> GetByIdAsync(int id)
	{
		return await context.ProductCategories.FindAsync(id);
	}

	public async Task AddAsync(ProductCategory entity)
	{
		await context.ProductCategories.AddAsync(entity);
	}

	public async Task DeleteAsync(int id)
    {
        var deleteProductCategory = await GetByIdAsync(id);
		context.ProductCategories.Remove(deleteProductCategory);
	}
}