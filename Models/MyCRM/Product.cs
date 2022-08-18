#pragma warning disable CS8618
namespace MyCRM.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Product
{
    [Key]
    public int ProductId {get; set;}

    [Required(ErrorMessage = "is required")]
    [MinLength(2, ErrorMessage = "must be at least 2 characters long")]
    [Display(Name = "Name")]
    public string Name {get; set;}

    [Required(ErrorMessage = "is required")]
    [Range(0.0, double.MaxValue, ErrorMessage = "must be greater than $0.00")]
    public double Price {get; set;}

    [Required(ErrorMessage = "of product is required")]
    [MinLength(8, ErrorMessage = "of product must be at least 8 characters long")]
    public string Description {get; set;}

    public DateTime CreatedAt {get; set;} = DateTime.Now;

    public DateTime UpdatedAt {get; set;} = DateTime.Now;

    public List<Purchase> ClientList {get; set;} = new List<Purchase>();
}