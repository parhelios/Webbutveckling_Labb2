using DataAccess.Repositories;
using StoreApp.DataAccess;
using StoreApp.DataAccess.Repositories;
using StoreApp.Shared.Interfaces;
using StoreApp.Shared.Models;

namespace DataAccess;

public class UnitOfWork : IUnitOfWork
{
	private readonly StoreDbContext _context;
	public ICategoryService<ProductCategory> CategoryRepository { get; private set; }
	public IContactInfoService<ContactInfo> ContactInfoRepository { get; private set; }
	public IOrderService<Order> OrderRepository { get; private set; }
	public IProductService<Product> ProductRepository { get; private set; }
	public IUserService<User> UserRepository { get; private set; }

	public UnitOfWork(StoreDbContext context)
	{
		_context = context;

		CategoryRepository = new CategoryRepository(context);
		ContactInfoRepository = new ContactInfoServiceRepository(context);
		OrderRepository = new OrderRepository(context);
		ProductRepository = new ProductRepository(context);
		UserRepository = new UserRepository(context);
	}
	
	public async Task CompleteAsync()
	{
		await _context.SaveChangesAsync();
	}

	public async void Dispose()
	{
		await _context.DisposeAsync();
	}
}