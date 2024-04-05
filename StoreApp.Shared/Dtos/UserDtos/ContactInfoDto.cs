using System.ComponentModel.DataAnnotations;

namespace StoreApp.Shared.Dtos.UserDtos;

public class ContactInfoDto
{
    [Required, Phone]
    public string Phone { get; set; }

    [Required]
    public string Address { get; set; }

    [Required]
    public string ZipCode { get; set; }

    [Required]
    public string City { get; set; }

    public string Region { get; set; }

    [Required]
    public string Country { get; set; }
}