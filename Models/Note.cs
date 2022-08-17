#pragma warning disable CS8618
namespace MyCRM.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Note
{
    [Key]
    public int NoteId {get; set;}

    [Required(ErrorMessage = "Notes are required")]
    [MinLength(5, ErrorMessage = "Notes must be at least 5 characters long")]
    public string SpecialNote {get; set;}

    [Required]
    public int BusinessId {get; set;}

    [Required]
    public int UserId {get; set;}

    public DateTime CreatedAt {get; set;} = DateTime.Now;
    public DateTime UpdatedAt {get; set;} = DateTime.Now;

    public Business? Business {get; set;}

    public User? User {get; set;}
}