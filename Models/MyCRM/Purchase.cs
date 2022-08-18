#pragma warning disable CS8618
namespace MyCRM.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Purchase
{
    [Key]
    public int PurchaseId {get; set;}

    [Required]
    public int BusinessId {get; set;}

    [Required]
    public int ProductId {get; set;}

    [Required]
    public int AddressId {get; set;}
    public DateTime CreatedAt {get; set;} = DateTime.Now;
    public DateTime UpdatedAt {get; set;} = DateTime.Now;

    public Business? Business {get; set;}

    public Product? Product {get; set;}

    public Address? Address {get; set;}
}