using System.ComponentModel.DataAnnotations;

namespace StoreApp.Shared.Dtos.OrderDtos;

public class OrderDto
{
    public int Id { get; set; } 

    [Required]
    public int CustomerId { get; set; }

    public DateTime OrderDate { get; set; }

    public double TotalPrice { get; set; }

    public bool Status { get; set; }

    public bool ShowDetails { get; set; }

    public virtual ICollection<ProductsOrdersDto> Products { get; set; }
}