using StoreApp.Shared.Models;
using System.ComponentModel.DataAnnotations;

namespace StoreApp.Shared.Dtos.OrderDtos;

public class ProductsOrdersDto
{
    [Key]
    public int Id { get; set; }

    public int ProductId { get; set; }

    public Product Product { get; set; }

    public int OrderId { get; set; }

    public Order Order { get; set; }

    public int Amount { get; set; }
}