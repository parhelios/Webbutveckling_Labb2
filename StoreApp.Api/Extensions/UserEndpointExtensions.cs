using StoreApp.Shared.Interfaces;
using StoreApp.Shared.Models;

namespace StoreApp.Api.Extensions;

public static class UserEndpointExtensions
{
	public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
	{
		var group = app.MapGroup("api/users");

		group.MapGet("/", GetAllUsers);
		group.MapGet("/{userId}", GetUserById);
		group.MapGet("/email/{email}", GetUserByEmail);
		group.MapGet("/role/{role}", GetUsersByRole);
		group.MapPost("/", AddNewUser);
		group.MapDelete("/{userId}", DeleteUser);
		return app;
	}

	private static async Task<IResult> DeleteUser(IUnitOfWork uow, int userId)
	{
		var userToRemove = await uow.UserRepository.GetByIdAsync(userId);
		if (userToRemove is null)
		{
			return Results.BadRequest("User not found");
		}

		await uow.UserRepository.DeleteAsync(userId);
		await uow.CompleteAsync();
		return Results.Ok("User found and Removed.");
	}


	private static async Task<IResult> AddNewUser(IUnitOfWork uow, User user)
	{
		var allCustomers = await uow.UserRepository.GetAllAsync();

		var customerList = allCustomers.ToList();

		if (customerList.Any(p => p.Email == user.Email))
		{
			return Results.BadRequest($"The user with this Email: {user.Email} already exists");
		}

		await uow.UserRepository.AddAsync(user);
		await uow.CompleteAsync();
		return Results.Ok();
	}

	private static async Task<User> GetUserByEmail(IUnitOfWork uow, string email)
	{
		var customer = await uow.UserRepository.GetByEmailAsync(email);

        if (customer is null)
        {
			Results.NotFound("User not found");
            return null;
        }

		Results.Ok();
		return customer;
	}

	private static async Task<User> GetUserById(IUnitOfWork uow, int userId)
	{
		var userById = await uow.UserRepository.GetByIdAsync(userId);

		if (userById is null)
		{
			Results.BadRequest("User not found");
			return null;
		}

		Results.Ok();
		return userById;
	}


	private static async Task<List<User>> GetAllUsers(IUnitOfWork uow)
	{
		var allCustomers = await uow.UserRepository.GetAllAsync();

		if (allCustomers is null)
		{
			return null;
		}

		Results.Ok();
		return allCustomers.ToList();
	}

	private static async Task<List<User>> GetUsersByRole(IUnitOfWork uow, string role)
	{
		var users = await uow.UserRepository.GetUsersByRole(role);

		if (!users.Any())
		{
			Results.NotFound("No users found");
			return Enumerable.Empty<User>().ToList();
		}

		Results.Ok();
		return users.ToList();
	}
}