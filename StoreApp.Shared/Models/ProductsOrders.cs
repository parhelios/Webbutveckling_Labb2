using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StoreApp.Shared.Models;

public class ProductsOrders
{
	[Key]
	public int Id { get; set; }

	public int ProductId { get; set; }
    
    public Product Product { get; set; }

    public int OrderId { get; set; }
    [JsonIgnore]
    public Order Order { get; set; }

    public int Amount { get; set; }
}