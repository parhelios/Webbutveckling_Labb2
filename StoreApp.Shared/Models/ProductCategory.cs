using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StoreApp.Shared.Models;

public class ProductCategory
{
	[Key]
	public int Id { get; set; }

	[Required]
    public string Name { get; set; }

    [JsonIgnore]
    public List<Product> ProductsInCategory { get; set; } = [];
}