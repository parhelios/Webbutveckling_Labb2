using System.ComponentModel.DataAnnotations;

namespace StoreApp.Shared.Dtos.ProductDtos;

public class ProductDto
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    [Required]
    public double Price { get; set; }

    [Required]
   public int Category { get; set; }
    public bool Status { get; set; }

    [Url]
    public string ImageUrl { get; set; }

    public int Stock { get; set; }
}
