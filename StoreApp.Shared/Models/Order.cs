using System.ComponentModel.DataAnnotations;

namespace StoreApp.Shared.Models;

public class Order
{
	[Key]
	public int Id { get; set; }

	[Required]
    public int CustomerId { get; set; }

    public DateTime OrderDate { get; set; }

    public double TotalPrice { get; set; }

    public bool Status { get; set; }

    public virtual ICollection<ProductsOrders> Products { get; set; }

}