#pragma warning disable CS8618
namespace MyCRM.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Staff
{
    [Key]
    public int StaffId {get; set;}

    [Required(ErrorMessage = "is required")]
    [Display(Name = "Position")]
    public string StaffType {get; set;}

    [Required(ErrorMessage = "is required")]
    [MinLength(2, ErrorMessage = "must be at least 2 characters long")]
    [Display(Name = "First Name")]
    public string FirstName {get; set;}

    [Required(ErrorMessage = "is required")]
    [MinLength(2, ErrorMessage = "must be at least 2 characters long")]
    [Display(Name = "Last Name")]
    public string LastName {get; set;}

    [Required(ErrorMessage = "is required")]
    [Display(Name = "Phone Number")]
    public long PhoneNumber {get; set;}

    [Required(ErrorMessage = "is required")]
    [EmailAddress]
    [Display(Name = "Email Address")]
    public string Email {get; set;}

    [Required]
    public int BusinessId {get; set;}
    public DateTime CreatedAt {get; set;} = DateTime.Now;
    public DateTime UpdatedAt {get; set;} = DateTime.Now;

    public List<UpcomingTask> TaskList {get; set;} = new List<UpcomingTask>();
}