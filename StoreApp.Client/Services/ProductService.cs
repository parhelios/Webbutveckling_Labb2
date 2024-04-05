using StoreApp.Shared.Dtos.ProductDtos;
using StoreApp.Shared.Interfaces;

namespace StoreApp.Client.Services;

public class ProductService : IProductService<ProductDto>
{
    private readonly HttpClient _httpClient;

    public ProductService(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("storeApp");
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        var response = await _httpClient.GetAsync("/api/products");

        if (!response.IsSuccessStatusCode)
        {
            return Enumerable.Empty<ProductDto>();

        }
        var result = await response.Content.ReadFromJsonAsync<IEnumerable<ProductDto>>();
        return result ?? Enumerable.Empty<ProductDto>();
    }

    public async Task<ProductDto?> GetByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"/api/products/{id}");

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var result = await response.Content.ReadFromJsonAsync<ProductDto>();
        return result ?? null;
    }

    public async Task AddAsync(ProductDto entity)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/products", entity);

        if (!response.IsSuccessStatusCode)
        {
            return;
        }
    }

    public async Task AddNewProductAsync(CreateProductDto entity)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/products", entity);

        if (!response.IsSuccessStatusCode)
        {
            return;
        }
    }

    public async Task DeleteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"/api/products/{id}");

        if (!response.IsSuccessStatusCode)
        {
            return;
        }
    }

    public async Task<ProductDto> UpdateAsync(int id, ProductDto entity)
    {
        var response = await _httpClient.PutAsJsonAsync($"/api/products/{id}", entity);

        if (!response.IsSuccessStatusCode)
        {
            return new ProductDto();
        }

        var result = await response.Content.ReadFromJsonAsync<ProductDto>();

        return result ?? new ProductDto();
    }

    public async Task<ProductDto?> GetByNameAsync(string name)
    {
        var response = await _httpClient.GetAsync($"/api/products/name/{name}");

        if (!response.IsSuccessStatusCode)
        {
            return new ProductDto();
        }

        var result = await response.Content.ReadFromJsonAsync<ProductDto>();

        return result ?? new ProductDto();
    }

    public async Task ToggleProductStatusAsync(int id)
    {
        var response = await _httpClient.PatchAsync($"/api/products/status/{id}", null);

        if (!response.IsSuccessStatusCode)
        {
            return;
        }
    }

    public async Task<IEnumerable<ProductDto?>> GetByCategoryAsync(string category)
    {
        var response = await _httpClient.GetAsync($"/api/products/category/{category}");

        if (!response.IsSuccessStatusCode)
        {
            return Enumerable.Empty<ProductDto>();
        }

        var result = await response.Content.ReadFromJsonAsync<IEnumerable<ProductDto>>();

        return result ?? Enumerable.Empty<ProductDto>();
    }
}
