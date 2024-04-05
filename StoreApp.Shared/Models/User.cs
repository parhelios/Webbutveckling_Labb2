using System.ComponentModel.DataAnnotations;

namespace StoreApp.Shared.Models;

public class User
{
	[Key]
	public int Id { get; set; }

	[Required, EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public ContactInfo ContactInfo { get; set; }

    public virtual List<int> OrdersIds { get; set; } = [];

    [Required]
    public string Role { get; set; }

}