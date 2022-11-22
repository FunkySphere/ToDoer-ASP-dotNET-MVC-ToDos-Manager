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

    public IActionResult Info(int? id)
    {
        if (id == null || _db.ArchivedTasks == null)
        {
            return NotFound();
        }
        var obj = _db.ArchivedTasks.Find(id);
        if (obj == null)
        {
            return NotFound();
        }
        return View(obj);
    }

    //GET Delete
    public IActionResult Delete(int? id)
    {
        if (id == null || _db.ArchivedTasks == null)
        {
            return NotFound();
        }
        var obj = _db.ArchivedTasks.Find(id);
        if (obj == null)
        {
            return NotFound();
        }
        return View(obj);
    }

    //POST Delete
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult PostDelete(int? id)
    {
        if (_db.ArchivedTasks != null)
        {
            var obj = _db.ArchivedTasks.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.ArchivedTasks.Remove(obj);
            _db.SaveChanges();
        }
        return RedirectToAction("List");
    }
}