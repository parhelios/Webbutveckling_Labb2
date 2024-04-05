using StoreApp.Shared.Interfaces;
using StoreApp.Shared.Models;

namespace StoreApp.Api.Extensions;

public static class CategoryEndpointExtensions
{
    public static IEndpointRouteBuilder MapCategoryEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/categories");

        group.MapGet("/", GetAllCategories);
        group.MapGet("/{id}", GetCategoryById);
        group.MapPost("/", AddCategory);
        group.MapDelete("/{id}", DeleteCategory);

        return app;
    }

    private static async Task<ProductCategory> GetCategoryById(IUnitOfWork uow, int id)
    {
        var category = await uow.CategoryRepository.GetByIdAsync(id);
        if (category is null)
        {
            Results.NotFound($"The category with id {id} could not be found");
            return null;
        }

        Results.Ok();
        return category;
    }

    private static async Task<IResult> DeleteCategory(IUnitOfWork uow, int id)
    {
        var categoryToDelete = await uow.CategoryRepository.GetByIdAsync(id);

        if (categoryToDelete == null)
        {
            uow.Dispose();
            return Results.NotFound();
        }

        await uow.CategoryRepository.DeleteAsync(id);
        await uow.CompleteAsync();

        return Results.Ok();
    }

    private static async Task<IResult> AddCategory(IUnitOfWork uow, ProductCategory category)
    {
        var allCategories = await uow.CategoryRepository.GetAllAsync();

        if (allCategories.Any(c => c.Name
	            .ToLower() == category.Name
	            .ToLower()))
        {
            return Results.BadRequest($"The category with name {category.Name} already exists");
        }

        await uow.CategoryRepository.AddAsync(category);
        await uow.CompleteAsync();

        return Results.Ok();
    }

    private static async Task<List<ProductCategory>> GetAllCategories(IUnitOfWork uow)
    {
	    var allCategories = await uow.CategoryRepository.GetAllAsync();

	    var categoriesList = allCategories.ToList();

		if (categoriesList.Count == 0)
        {
            uow.Dispose();
            Results.NotFound();
            return null;
        }

        return categoriesList;
    }
}