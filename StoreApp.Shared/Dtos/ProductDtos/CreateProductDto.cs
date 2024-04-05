using System.ComponentModel.DataAnnotations;

namespace StoreApp.Shared.Dtos.ProductDtos;

public class CreateProductDto
{
    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    [Required]
    public double Price { get; set; }

    public int Category { get; set; }

    [Url]
    public string ImageUrl { get; set; }

}