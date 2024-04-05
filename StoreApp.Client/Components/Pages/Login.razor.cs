using System.ComponentModel.DataAnnotations;
using StoreApp.Shared.Dtos.UserDtos;
using static MudBlazor.CategoryTypes;

namespace StoreApp.Client.Components.Pages;

public partial class Login
{
	[EmailAddress] private string LoginEmail { get; set; }
	private string LoginPassword { get; set; }
	private bool IsAdmin { get; set; }
	private int _activeTab = 0;
	private string NewCustomerEmail { get; set; }
	private string NewCustomerPassword { get; set; }
	private string NewCustomerFirstName { get; set; }
	private string NewCustomerLastName { get; set; }
	private string NewCustomerPhone { get; set; }
	private string NewCustomerAddress { get; set; }
	private string NewCustomerZipcode { get; set; }
	private string NewCustomerCity { get; set; }
	private string NewCustomerRegion { get; set; }
	private string NewCustomerCountry { get; set; }

	private async Task LoginUser()
	{
		var loginUser = await UserService.GetByEmailAsync(LoginEmail);

		if (loginUser is null)
		{
			return;
		}

		if (loginUser.Password == LoginPassword)
		{
			if (loginUser.Role.ToLower() == "Customer".ToLower())
			{
				// Login customer
				AuthenticationService.IsCustomerLoggedIn = true;
				AuthenticationService.LoggedInUserEmail = loginUser.Email;
				NavigationManager.NavigateTo("/Store");
			}

			if (loginUser.Role.ToLower() == "Admin".ToLower())
			{
				// Login admin
				AuthenticationService.IsAdminLoggedIn = true;
				AuthenticationService.LoggedInUserEmail = loginUser.Email;
				NavigationManager.NavigateTo("/Store");
			}
		}
	}

	private async Task RegisterUser()
	{
		var existingCustomer = await UserService.GetByEmailAsync(NewCustomerEmail);
		if (existingCustomer is not null)
		{
			return;
		}

		if (IsAdmin)
		{
			await RegisterAdmin();
		}
		else
		{
			await RegisterCustomer();
		}
		
	}

	private async Task RegisterCustomer()
	{
		await UserService.AddAsync(new UserDto
		{
			Email = NewCustomerEmail,
			Password = NewCustomerPassword,
			FirstName = NewCustomerFirstName,
			LastName = NewCustomerLastName,
			Role = "Customer",
			ContactInfo = new ContactInfoDto
			{
				Phone = NewCustomerPhone,
				Address = NewCustomerAddress,
				ZipCode = NewCustomerZipcode,
				City = NewCustomerCity,
				Region = NewCustomerRegion,
				Country = NewCustomerCountry
			},
		});

		NavigationManager.NavigateTo("/");
		_activeTab = 0;
	}

	private async Task RegisterAdmin()
	{
		await UserService.AddAsync(new UserDto
		{
			Email = NewCustomerEmail,
			Password = NewCustomerPassword,
			FirstName = NewCustomerFirstName,
			LastName = NewCustomerLastName,
			Role = "Admin",
			ContactInfo = new ContactInfoDto
			{
				Phone = NewCustomerPhone,
				Address = NewCustomerAddress,
				ZipCode = NewCustomerZipcode,
				City = NewCustomerCity,
				Region = NewCustomerRegion,
				Country = NewCustomerCountry
			},
		});
		NavigationManager.NavigateTo("/");
		_activeTab = 0;
	}

}