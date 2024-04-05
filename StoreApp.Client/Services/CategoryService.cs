using StoreApp.Shared.Dtos.ProductDtos;
using StoreApp.Shared.Interfaces;

namespace StoreApp.Client.Services;

public class CategoryService : ICategoryService<ProductCategoryDto>
{
    private readonly HttpClient _httpClient;

    public CategoryService(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("storeApp");
    }

    public async Task<IEnumerable<ProductCategoryDto>> GetAllAsync()
    {
        var response = await _httpClient.GetAsync("/api/categories");

        if (!response.IsSuccessStatusCode)
        {
            return Enumerable.Empty<ProductCategoryDto>();

        }
        var result = await response.Content.ReadFromJsonAsync<IEnumerable<ProductCategoryDto>>();
        return result ?? Enumerable.Empty<ProductCategoryDto>();
    }

    #region RegionOfShame

    public async Task<ProductCategoryDto> GetByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"/api/categories/{id}");

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var result = await response.Content.ReadFromJsonAsync<ProductCategoryDto>();
        return result;
    }

    #endregion

    public async Task AddAsync(ProductCategoryDto entity)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/categories", entity);

        if (!response.IsSuccessStatusCode)
        {
            return;
        }
    }

    public async Task DeleteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"/api/categories/{id}");

        if (!response.IsSuccessStatusCode)
        {
            return;
        }
    }
}