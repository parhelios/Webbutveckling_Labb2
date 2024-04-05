using Microsoft.EntityFrameworkCore;
using StoreApp.DataAccess;
using StoreApp.Shared.Interfaces;
using StoreApp.Shared.Models;

namespace DataAccess.Repositories;

public class OrderRepository(StoreDbContext context) : IOrderService<Order>
{
    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        var allorders = await context.Orders
            .Include(o => o.Products)
            .ThenInclude(po => po.Product)
            .ToListAsync();
        return allorders;
    }

	public async Task<Order> GetByIdAsync(int id)
	{
		return await context.Orders.FindAsync(id);
	}

	public async Task AddAsync(Order entity)
	{
		await context.Orders.AddAsync(entity);
	}

	public async Task DeleteAsync(int id)
	{
		var delete = await context.Orders.FindAsync(id);
		context.Orders.Remove(delete);
	}

    public async Task AddToProductOrders(ProductsOrders prodOrder)
    {
        await context.ProductOrders.AddAsync(prodOrder);
    }
}
