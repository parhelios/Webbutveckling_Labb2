namespace StoreApp.Shared.Interfaces;

public interface IProductService<T> : IService<T> where T : class
{
	Task<T> UpdateAsync(int id, T entity);
	Task<T> GetByNameAsync(string name);
    Task ToggleProductStatusAsync(int id);
}