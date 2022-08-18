#pragma warning disable CS8618
namespace MyCRM.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[NotMapped]
public class NewBusinessForm
{
    [Required(ErrorMessage = "is required")]
    [MinLength(2, ErrorMessage = "must be at least 2 characters long")]
    [Display(Name = "Business Name")]
    public string BusinessName {get; set;}

    [Required(ErrorMessage = "name is required")]
    [MinLength(2, ErrorMessage = "name must be at least 2 characters long")]
    [Display(Name = "Business Owner")]
    public string BusinessOwner {get; set;}

    [MyDate(ErrorMessage = "must be in the past")]
    [Display(Name = "Date Started or Acquisition")]
    public DateTime? StartDate {get; set;}

    [Required(ErrorMessage = "is required")]
    public string Industry {get; set;}

    [MinLength(6, ErrorMessage = "must be valid")]
    [Display(Name = "Street")]
    public string? Street {get; set;}

    [Required(ErrorMessage = "is required")]
    public string City {get; set;}

    [Required(ErrorMessage = "State is required")]
    public string State {get; set;}

    [Required(ErrorMessage = "is required")]
    [RegularExpression("^[0-9]{5}?$", ErrorMessage = "must be valid")]
    [Display(Name = "Zipcode")]
    public int ZipCode {get; set;}
}