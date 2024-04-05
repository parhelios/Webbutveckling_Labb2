using StoreApp.Shared.Models;

namespace StoreApp.Shared.Interfaces;

public interface IOrderService<T> : IService<T> where T : class
{
    Task AddToProductOrders(ProductsOrders entity);
}