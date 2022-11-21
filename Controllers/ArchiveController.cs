using Microsoft.AspNetCore.Mvc;
using ToDoer.Models;
using ToDoer.Data;

namespace ToDoer.Controllers;

public class ArchiveController : Controller
{
    private readonly ApplicationDbContext _db;
    public ArchiveController(ApplicationDbContext db)
    {
        _db = db;
    }
    public IActionResult List()
    {
        if (_db.ArchivedTasks != null)
        {
            List<ArchivedToDo> objList = _db.ArchivedTasks.ToList();
            return View(objList);
        }
        return NotFound();
    }
}