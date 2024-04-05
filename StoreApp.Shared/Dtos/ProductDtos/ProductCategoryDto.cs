using System.ComponentModel.DataAnnotations;

namespace StoreApp.Shared.Dtos.ProductDtos;

public class ProductCategoryDto
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
}