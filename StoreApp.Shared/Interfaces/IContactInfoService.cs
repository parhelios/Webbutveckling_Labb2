namespace StoreApp.Shared.Interfaces;

public interface IContactInfoService<T> : IService<T> where T : class
{
    Task<T> UpdateAsync(int id, T entity);
}