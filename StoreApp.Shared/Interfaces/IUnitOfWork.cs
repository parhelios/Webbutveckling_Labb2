using StoreApp.Shared.Models;

namespace StoreApp.Shared.Interfaces;

public interface IUnitOfWork : IDisposable
{
	ICategoryService<ProductCategory> CategoryRepository { get; }
	IContactInfoService<ContactInfo> ContactInfoRepository { get; }
	IOrderService<Order> OrderRepository { get; }
	IProductService<Product> ProductRepository { get; }
	IUserService<User> UserRepository { get; }

	Task CompleteAsync();
}