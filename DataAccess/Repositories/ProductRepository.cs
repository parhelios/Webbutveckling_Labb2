using Microsoft.EntityFrameworkCore;
using StoreApp.DataAccess;
using StoreApp.Shared.Interfaces;
using StoreApp.Shared.Models;

namespace DataAccess.Repositories;

public class ProductRepository(StoreDbContext context) : IProductService<Product>
{
    public async Task<IEnumerable<Product>> GetAllAsync()
	{
		return context.Products;
	}

	public async Task<Product?> GetByIdAsync(int id)
	{
        return await context.Products.FirstOrDefaultAsync(p => p.Id == id);
    }

	public async Task AddAsync(Product entity)
	{
		await context.Products.AddAsync(entity);
	}

	public async Task DeleteAsync(int id)
    {
        var delete = await GetByIdAsync(id);
		context.Products.Remove(delete);
	}

	public async Task<Product> UpdateAsync(int id, Product entity)
	{
        var oldProduct = await GetByIdAsync(id);

        if (oldProduct is null)
		{
			return null;
		}

		oldProduct.Name = entity.Name;
		oldProduct.Description = entity.Description;
		oldProduct.Price = entity.Price;
		oldProduct.Category = entity.Category;
		oldProduct.ImageUrl = entity.ImageUrl;
		oldProduct.Stock = entity.Stock;

		return entity;
	}

	public async Task<Product>? GetByNameAsync(string name)
	{
		return await context.Products
			.FirstOrDefaultAsync(p => p.Name
			.ToLower()
			.Equals(name
				.ToLower()));
	}

    public async Task ToggleProductStatusAsync(int id)
    {
        var product = await GetByIdAsync(id);
		product.Status = !product.Status;
    }
}