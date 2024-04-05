namespace StoreApp.Shared.Interfaces;

public interface IAuthenticationService
{
    bool IsCustomerLoggedIn { get; set; }
    bool IsAdminLoggedIn { get; set; }
    string LoggedInUserEmail { get; set; }
}