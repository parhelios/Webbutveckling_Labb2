using Microsoft.EntityFrameworkCore;
using StoreApp.Shared.Interfaces;
using StoreApp.Shared.Models;

namespace StoreApp.DataAccess.Repositories;

public class UserRepository(StoreDbContext context) : IUserService<User>
{
	public async Task<IEnumerable<User>> GetAllAsync()
	{
		var users =  await context.Users
			.Include(c => c.ContactInfo)
			.ToListAsync();

		if (users.Count == 0)
		{
			return Enumerable.Empty<User>();
		}

		return users;

	}

	public async Task<User> GetByIdAsync(int id)
	{
		return await context.Users
			.Include(c => c.ContactInfo)
			.FirstOrDefaultAsync(c => c.Id == id);
	}

	public async Task AddAsync(User entity)
	{
		await context.Users.AddAsync(entity);
	}

	public async Task DeleteAsync(int id)
	{
		var user = await context.Users.FindAsync(id);

		if (user is null)
		{
			return;
		}

		context.Users.Remove(user);
	}

	public async Task<User> GetByEmailAsync(string email)
	{
		var user = await context.Users
			.Include(c => c.ContactInfo)
			.FirstOrDefaultAsync(c => c.Email == email);

		if (user is null)
		{
			return null;
		}

		return user;
	}

	public async Task UpdatePassword(int userId, string newPassword)
	{
		var user = await context.Users.FindAsync(userId);

		if (user is null)
		{
			return;
		}

		user.Password = newPassword;
	}

	public async Task<IEnumerable<User>> GetUsersByRole(string role)
	{
		var users = await context.Users
			.Include(c => c.ContactInfo)
			.Where(c => c.Role == role).ToListAsync();

		if (users.Count == 0)
		{
			return Enumerable.Empty<User>();
		}

		return users;
	}
}