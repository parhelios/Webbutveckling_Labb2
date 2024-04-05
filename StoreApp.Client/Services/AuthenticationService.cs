using IAuthenticationService = StoreApp.Shared.Interfaces.IAuthenticationService;

namespace StoreApp.Client.Services;

public class AuthenticationService : IAuthenticationService
{
    public bool IsCustomerLoggedIn { get; set; }
    public bool IsAdminLoggedIn { get; set; }
    public string LoggedInUserEmail { get; set; }
}