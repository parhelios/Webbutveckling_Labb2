using StoreApp.Shared.Dtos.ProductDtos;
using StoreApp.Shared.Interfaces;
using StoreApp.Shared.Models;
using Results = Microsoft.AspNetCore.Http.Results;

namespace StoreApp.Api.Extensions;

public static class ProductEndpointExtensions
{
    public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/products");

        group.MapGet("/", GetAllProducts);
        group.MapGet("/{id}", GetProductById);
        group.MapGet("/name/{name}", GetProductByName);
        group.MapGet("/category/{category}", GetAllProductsByCategory);
        group.MapPost("/", AddProduct);
        group.MapPut("/{id}", UpdateProduct);
        group.MapPatch("/status/{id}", ToggleProductStatus);
        group.MapDelete("/{id}", DeleteProduct);

        return app;
    }

    private async static Task<IResult> ToggleProductStatus(IUnitOfWork uow, int id)
    {
        var product = await uow.ProductRepository.GetByIdAsync(id);
        if (product is null)
        {
            return Results.NotFound($"The product with id {id} could not be found");
        }

        await uow.ProductRepository.ToggleProductStatusAsync(id);
        await uow.CompleteAsync();
        return Results.Ok();
    }

    private static async Task<IResult> DeleteProduct(IUnitOfWork uow, int id)
    {
        var product = await uow.ProductRepository.GetByIdAsync(id);
        if (product is null)
        {
            return Results.NotFound($"The product with id {id} could not be found");
        }

		await uow.ProductRepository.DeleteAsync(id);
        await uow.CompleteAsync();
		return Results.Ok("Product deleted successfully");
    }

    private static async Task<IResult> UpdateProduct(IUnitOfWork uow, Product product, int id)
    {
        var prod = await uow.ProductRepository.GetByIdAsync(id);
        if (prod is null)
        {
            return Results.BadRequest($"The product with id {id} could not be found");
        }

        await uow.ProductRepository.UpdateAsync(id, product);
        await uow.CompleteAsync();
        return Results.Ok(new ProductDto());
    }

    private static async Task<IResult> AddProduct(IUnitOfWork uow, Product product)
    {
        var allProducts = await uow.ProductRepository.GetAllAsync();
        var existingProduct = allProducts.Any(p => p.Name.ToLower() == product.Name.ToLower());

        if (existingProduct)
        {
            return Results.BadRequest($"The product with name {product.Name} already exists");
        }

        await uow.ProductRepository.AddAsync(product);
        await uow.CompleteAsync();
        return Results.Ok();
	}

	private static async Task<List<Product>> GetAllProductsByCategory(IUnitOfWork uow, string category)
    {
        var allProducts = await uow.ProductRepository.GetAllAsync();
        var allCategories = await uow.CategoryRepository.GetAllAsync();
        var selectedCategory = allCategories.FirstOrDefault(c => c.Name == category);

        var products = allProducts
	        .ToList()
	        .Where(p => p.Category == selectedCategory.Id)
	        .ToList();

		if (!allCategories
			    .Any(c => c.Name
			    .ToLower()
			    .Equals(category
				    .ToLower())))
        {
	        Results.NotFound($"The category {category} could not be found");
	        return null;
        }

        if (products.Count == 0)
        {
            Results.NotFound($"No products in category {category} could be found");
            return null;
        }

        Results.Ok();
        return products;
    }

    private static async Task<Product>? GetProductByName(IUnitOfWork uow, string name)
    {
        var product = await uow.ProductRepository.GetByNameAsync(name.ToLower());

        if (product is null)
        {
            Results.NotFound($"The product with name {name} could not be found");
            return null;
        }

        Results.Ok();
        return product;
    }

    private static async Task<Product>? GetProductById(IUnitOfWork uow, int id)
    {
	    var product = await uow.ProductRepository.GetByIdAsync(id);
        if (product is null)
        {
            Results.NotFound($"The product with id {id} could not be found");
            return null;
        }

        Results.Ok();
        return product;
    }

    private static async Task<List<Product>> GetAllProducts(IUnitOfWork uow)
    {
	    var products = await uow.ProductRepository.GetAllAsync();

	    var productsList = products.ToList();
	    if (productsList.Count == 0)
        {
            Results.NotFound("No products could be found");
            return null;
        }

        Results.Ok();
        return productsList;
    }
}