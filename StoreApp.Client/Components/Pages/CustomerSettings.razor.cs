using StoreApp.Shared.Dtos.OrderDtos;
using StoreApp.Shared.Dtos.UserDtos;

namespace StoreApp.Client.Components.Pages;

public partial class CustomerSettings
{
    private UserDto _user;
    private string _newPassword = "";
    public List<OrderDto> Orders { get; set; } = [];
    private OrderDto _selectedItemOrder = null;
    private string _searchStringOrder = "";
    private bool FilterFuncOrder1(OrderDto orderDto) => FilterFuncOrder(orderDto, _searchStringOrder);

    private bool FilterFuncOrder(OrderDto orderDto, string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return true;
        if (orderDto.Id.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        return false;
    }

    protected override async Task OnInitializedAsync()
    {
        var allOrders = await UserService.GetAllOrders();
        var currentUser = await UserService.GetByEmailAsync(AuthenticationService.LoggedInUserEmail);
        Orders = allOrders.Where(o => o.CustomerId == currentUser.Id).ToList();

        if (AuthenticationService.IsCustomerLoggedIn || AuthenticationService.IsAdminLoggedIn)
        {
            _user = await UserService.GetByEmailAsync(AuthenticationService.LoggedInUserEmail);
        }
    }

    private async Task UpdateUserProfile()
    {
        var currentUser = await UserService.GetByEmailAsync(AuthenticationService.LoggedInUserEmail);
        await UserService.UpdateContactInfo(currentUser.Id, _user.ContactInfo);

        _user = await UserService.GetByEmailAsync(AuthenticationService.LoggedInUserEmail);
    }

    private async Task UpdatePassword()
    {
        var currentUser = await UserService.GetByEmailAsync(AuthenticationService.LoggedInUserEmail);

        await UserService.UpdatePassword(currentUser.Id, _newPassword);

        _user = await UserService.GetByEmailAsync(AuthenticationService.LoggedInUserEmail);
    }

    private async Task ShowBtnPress(int orderId)
    {
        var showOrder = Orders.First(o => o.Id == orderId);

        showOrder.ShowDetails = !showOrder.ShowDetails;
    }
}