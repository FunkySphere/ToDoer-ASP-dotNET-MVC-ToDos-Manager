using System.ComponentModel.DataAnnotations;

namespace ToDoer.Models;
public class ArchivedToDo
{
    [Key]
    public int Id { get; set; }

    [Display(Name = "Task Name")]
    [Required]
    public string TodoName { get; set; } = "";

    public bool Complete { get; set; } = false;

    public DateTime? CreationDate {get; set;} 
    public DateTime? Deadline {get; set;}
    public DateTime? ArchiveDate {get; set;} = DateTime.Now;

    public string? Tags {get; set;} = "";
}