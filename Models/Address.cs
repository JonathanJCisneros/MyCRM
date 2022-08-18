#pragma warning disable CS8618
namespace MyCRM.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Address
{
    [Key]
    public int AddressId {get; set;}

    [MinLength(6, ErrorMessage = "must be valid")]
    [Display(Name = "Street")]
    public string? Street {get; set;}

    [Required(ErrorMessage = "is required")]
    public string City {get; set;}

    [Required(ErrorMessage = "State is required")]
    public string State {get; set;}

    [Required(ErrorMessage = "is required")]
    [DataType(DataType.PostalCode, ErrorMessage = "must be valid")]
    [Display(Name = "Zipcode")]
    public string ZipCode {get; set;}

    [Required]
    public int BusinessId {get; set;}
    public DateTime CreatedAt {get; set;} = DateTime.Now;
    public DateTime UpdatedAt {get; set;} = DateTime.Now;

    public Business? Business {get; set;}
}