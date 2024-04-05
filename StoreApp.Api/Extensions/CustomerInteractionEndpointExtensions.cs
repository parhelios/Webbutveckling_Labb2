using Microsoft.AspNetCore.Mvc;
using StoreApp.Shared.Dtos.OrderDtos;
using StoreApp.Shared.Interfaces;
using StoreApp.Shared.Models;

namespace StoreApp.Api.Extensions;

public static class CustomerInteractionEndpointExtensions
{
    public static IEndpointRouteBuilder MapCustomerInteractionEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/customer");

        group.MapGet("/orders", GetAllCustomerOrders);
        group.MapPost("/{userId}", CreateCustomerOrder);
        group.MapPatch("/{userId}", UpdateCustomerInfo);
        group.MapPatch("/password/{userId}/{newPassword}", UpdateUserPassword);

        return app;
    }

    private static async Task<List<Order>> GetAllCustomerOrders(IUnitOfWork uow)
    {
        var allOrders = await uow.OrderRepository.GetAllAsync();

        var ordersList = allOrders.ToList();

        if (ordersList.Count == 0)
        {
            uow.Dispose();
            Results.NotFound();
            return null;
        }

        return ordersList;
    }

    private static async Task<Order> CreateCustomerOrder(IUnitOfWork uow, List<ProductsAmountsDto> orders, int userId)
    {
        var customer = await uow.UserRepository.GetByIdAsync(userId);

        var cartDictionary = new Dictionary<Product, int>();

        if (customer is null)
        {
            Results.BadRequest($"The customer with id {userId} could not be found");
            return null;
        }

        if (orders.Count == 0)
        {
            Results.BadRequest("The cart is empty");
            return null;
        }

        foreach (var product in orders)
        {
            var productInDb = await uow.ProductRepository.GetByIdAsync(product.ProductId);
            if (productInDb is null)
            {
                Results.BadRequest($"The product with id {product.ProductId} could not be found");
                return null;
            }

            if (product.ProductAmount > productInDb.Stock)
            {
                Results.BadRequest($"The amount of product with id {product.ProductId} exceeds the stock");
                return null;
            }

            cartDictionary.Add(productInDb, product.ProductAmount);
        }

        var totalPrice = cartDictionary.Sum(p => p.Key.Price * p.Value);

        var orderTest = new Order
        {
            CustomerId = customer.Id,
            OrderDate = DateTime.Now,
            TotalPrice = totalPrice
        };

        await uow.OrderRepository.AddAsync(orderTest);
        await uow.CompleteAsync();

        var allOrdersTest = await uow.OrderRepository.GetAllAsync();

        var lastOrderTest = allOrdersTest.ToList().LastOrDefault();

        var lastOrderIdTest = lastOrderTest.Id;

        foreach (var product in cartDictionary)
        {
            var productAmount = product.Value;
            var productInCart = product.Key;
            var productId = product.Key.Id;

            productInCart.Stock -= productAmount;

            await uow.OrderRepository.AddToProductOrders(new ProductsOrders
            {
                ProductId = productId,
                OrderId = lastOrderIdTest,
                Amount = productAmount
            });

        }

        customer.OrdersIds.Add(lastOrderTest.Id);

        cartDictionary.Clear();

        await uow.CompleteAsync();
        Results.Ok();
        return orderTest;
    }

    private static async Task<IResult> UpdateCustomerInfo(IUnitOfWork uow, int userId, [FromBody] ContactInfo contactInfo)
    {
        var customer = await uow.UserRepository.GetByIdAsync(userId);
        if (customer is null)
        {
            return Results.NotFound($"The customer with id {userId} could not be found");
        }

        var contactInfoId = customer.ContactInfo.Id;

        await uow.ContactInfoRepository.UpdateAsync(contactInfoId, contactInfo);
        await uow.CompleteAsync();
        return Results.Ok();
    }

    private static async Task<IResult> UpdateUserPassword(IUnitOfWork uow, int userId, string newPassword)
    {
        var customer = await uow.UserRepository.GetByIdAsync(userId);
        if (customer is null)
        {
            return Results.NotFound($"The customer with id {userId} could not be found");
        }

        await uow.UserRepository.UpdatePassword(userId, newPassword);
        await uow.CompleteAsync();
        return Results.Ok();
    }
}