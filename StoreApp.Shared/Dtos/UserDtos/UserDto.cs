using System.ComponentModel.DataAnnotations;

namespace StoreApp.Shared.Dtos.UserDtos;

public class UserDto
{
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
    public ContactInfoDto ContactInfo { get; set; }

    public List<int> OrdersIds { get; set; } = [];

    [Required]
    public string Role { get; set; }

}