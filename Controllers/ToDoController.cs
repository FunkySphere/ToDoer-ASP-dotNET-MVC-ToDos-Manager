using Microsoft.AspNetCore.Mvc;
using ToDoer.Models;
using ToDoer.Data;
namespace ToDoer.Controllers;

public class ToDoController : Controller
{
    private readonly ApplicationDbContext _db;

    public ToDoController(ApplicationDbContext db)
    {
        _db = db;
    }
    public IActionResult List()
    {
        List<ToDo> objList = _db.ToDos.ToList();
        return View(objList);
    }
}