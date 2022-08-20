#pragma warning disable CS8618
namespace MyCRM.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Activity
{
    [Key]
    public int ActivityId {get; set;}


    [Required]
    public int UserId {get; set;}


    [Required]
    public int BusinessId {get; set;}


    [Required(ErrorMessage = "Please choose one of the following")]
    [Display(Name = "Action Type")]
    public string ActivityType {get; set;}


    [MinLength(3, ErrorMessage = "Notes must be at least 3 characters long")]
    public string? Notes {get; set;}

    [Required]
    [Display(Name = "Spoke with")]
    public int StaffId {get; set;}


    public DateTime CreatedAt {get; set;} = DateTime.Now;
    public DateTime UpdatedAt {get; set;} = DateTime.Now;

    public User? User {get; set;}

    public Business? Business {get; set;}

    public Staff? Staff {get; set;}
}