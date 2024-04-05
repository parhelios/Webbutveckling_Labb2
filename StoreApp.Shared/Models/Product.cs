using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StoreApp.Shared.Models;

public class Product
{
	[Key]
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
    [JsonIgnore]
    public virtual ICollection<ProductsOrders> Orders { get; set; }

}