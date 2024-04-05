namespace StoreApp.Shared.Interfaces;

public interface IShoppingCartService<T> where T : class
{
    Dictionary<T, int> UserCart { get; set; }

    double TotalPrice { get; set; }
}