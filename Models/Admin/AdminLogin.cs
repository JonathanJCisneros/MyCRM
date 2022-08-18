#pragma warning disable CS8618
namespace MyCRM.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[NotMapped]
public class AdminLogin
{
    [Required(ErrorMessage = "is required")]
    [EmailAddress]
    [Display(Name = "Email")]
    public string LoginEmail {get; set;}

    [Required(ErrorMessage = "is required")]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string LoginPassword {get; set;}
}