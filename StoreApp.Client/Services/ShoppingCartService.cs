using StoreApp.Shared.Dtos.ProductDtos;
using StoreApp.Shared.Interfaces;

namespace StoreApp.Client.Services;

public class ShoppingCartService : IShoppingCartService<ProductDto>
{
    public Dictionary<ProductDto, int> UserCart { get; set; } = [];

    public double TotalPrice
    {
        get
        {
            double totalPrice = 0.0;
            foreach (var product in UserCart)
            {
                totalPrice += (product.Key.Price * product.Value);
            }
            return totalPrice;
        }
        set { }
    }
}