#pragma warning disable CS8618
namespace MyCRM.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class UpcomingTask
{
    [Key]
    public int TaskId {get; set;}

    [Required]
    public int UserId {get; set;}

    [Required]
    public int BusinessId {get; set;}

    [Required]
    [Display(Name = "With")]
    public int StaffId {get; set;}

    [Required]
    [Display(Name = "Type")]
    public string TaskType {get; set;}

    [Display(Name = "Details")]
    public string? Details {get; set;}

    [Required]
    [MyDate2(ErrorMessage = "must be in the future")]
    [Display(Name = "Due Date")]
    public DateTime DueDate {get; set;}


    public bool Status {get; set;} = false;
    public DateTime CreatedAt {get; set;} = DateTime.Now;
    public DateTime UpdatedAt {get; set;} = DateTime.Now;

    public Business? business {get; set;}

    public User? user {get; set;}

    public Staff? staff {get; set;}
}

[NotMapped]
public class MyDate2Attribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        DateTime d = Convert.ToDateTime(value);
        return d >= DateTime.Now;
    }
}