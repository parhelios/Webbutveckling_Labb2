using StoreApp.Shared.Dtos.UserDtos;
using StoreApp.Shared.Interfaces;
using StoreApp.Shared.Dtos.OrderDtos;

namespace StoreApp.Client.Services;

public class UserService : IUserService<UserDto>
{
	private readonly HttpClient _httpClient;

	public UserService(IHttpClientFactory factory)
	{
		_httpClient = factory.CreateClient("storeApp");
	}

	public async Task<IEnumerable<UserDto>> GetAllAsync()
	{
		var response = await _httpClient.GetAsync("/api/users/");

		if (!response.IsSuccessStatusCode)
		{
			return Enumerable.Empty<UserDto>();

		}
		var result = await response.Content.ReadFromJsonAsync<IEnumerable<UserDto>>();
		return result ?? Enumerable.Empty<UserDto>();
	}

	public async Task<UserDto> GetByIdAsync(int id)
	{
		var response = await _httpClient.GetAsync($"/api/users/{id}");

		if (!response.IsSuccessStatusCode)
		{
			return null;
		}

		var result = await response.Content.ReadFromJsonAsync<UserDto>();
		return result ?? null;
	}

	public async Task AddAsync(UserDto entity)
	{
		var response = await _httpClient.PostAsJsonAsync("/api/users/", entity);

		if (!response.IsSuccessStatusCode)
		{
			return;
		}
	}

	public async Task DeleteAsync(int id)
	{
		var response = await _httpClient.DeleteAsync($"/api/users/customers/{id}");

		if (!response.IsSuccessStatusCode)
		{
			return;
		}
	}

	public async Task<UserDto> GetByEmailAsync(string email)
	{
		var response = await _httpClient.GetAsync($"/api/users/email/{email}");

		if (!response.IsSuccessStatusCode)
		{
			return null;
		}

		var result = await response.Content.ReadFromJsonAsync<UserDto>();
		return result ?? null;
	}

	public async Task UpdatePassword(int userId, string newPassword)
	{
		var response = await _httpClient.PatchAsync($"/api/customer/password/{userId}/{newPassword}", null);

		if (!response.IsSuccessStatusCode)
		{
			return;
		}
	}

	public async Task<IEnumerable<UserDto>> GetUsersByRole(string role)
	{
		var response = await _httpClient.GetAsync($"/api/users/role/{role}");

		if (!response.IsSuccessStatusCode)
		{
			return Enumerable.Empty<UserDto>();
		}

		var result = await response.Content.ReadFromJsonAsync<IEnumerable<UserDto>>();
		return result ?? Enumerable.Empty<UserDto>();
	}

	public async Task CreateOrder(int userId, List<ProductsAmountsDto> orders)
	{
        var response = await _httpClient.PostAsJsonAsync($"/api/customer/{userId}", orders);

		if (!response.IsSuccessStatusCode)
		{
			return;
		}
	}

	public async Task UpdateContactInfo(int userId, ContactInfoDto contactInfo)
	{
		var response = await _httpClient.PatchAsJsonAsync($"/api/customer/{userId}", contactInfo);

		if (!response.IsSuccessStatusCode)
		{
			return;
		}
	}

    public async Task<IEnumerable<OrderDto>> GetAllOrders()
    {
        var response = await _httpClient.GetAsync("/api/customer/orders/");

        if (!response.IsSuccessStatusCode)
        {
            return Enumerable.Empty<OrderDto>();

        }
        var result = await response.Content.ReadFromJsonAsync<IEnumerable<OrderDto>>();
        return result ?? Enumerable.Empty<OrderDto>();
    }
}