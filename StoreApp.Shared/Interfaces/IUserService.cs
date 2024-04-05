namespace StoreApp.Shared.Interfaces;

public interface IUserService<T> : IService<T> where T : class
{
    Task<T> GetByEmailAsync(string email);

    Task UpdatePassword(int userId, string newPassword);

    Task<IEnumerable<T>> GetUsersByRole (string role);
}