#pragma warning disable CS8618
namespace MyCRM.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Business
{

    [Key]
    public int BusinessId {get; set;}

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

    public DateTime CreatedAt {get; set;} = DateTime.Now;
    public DateTime UpdatedAt {get; set;} = DateTime.Now;

    public List<Activity> UsersWorkedWith {get; set;} = new List<Activity>();
    public List<Address> AddressList {get; set;} = new List<Address>();
    public List<Note> SpecialNotes {get; set;} = new List<Note>(); 
    public List<Purchase> PurchaseList {get; set;} = new List<Purchase>();
}

[NotMapped]
public class MyDateAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        DateTime d = Convert.ToDateTime(value);
        return d <= DateTime.Now;
    }
}