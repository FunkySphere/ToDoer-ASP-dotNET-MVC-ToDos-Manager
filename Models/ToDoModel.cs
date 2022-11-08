using System.ComponentModel.DataAnnotations;

namespace ToDoer.Models;
public class ToDo
{
    [Key]
    public int Id { get; set; }

    [Display(Name = "Task Name")]
    [Required]
    public string TodoName { get; set; } = "";

    public bool Complete { get; set; } = false;

    //add creation date and deadline date later
    //also add tags to be able to view these tasks in an organizaed manner
}