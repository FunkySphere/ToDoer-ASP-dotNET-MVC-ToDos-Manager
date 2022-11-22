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

    public IActionResult Restore(int? id)
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

    //POST RestoreFromArchive
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult RestoreFromArchive(ArchivedToDo obj)
    {
        if (_db.ToDos != null && _db.ArchivedTasks != null)
        {
            var objToRestore = _db.ArchivedTasks.Find(obj.Id);
            if (objToRestore == null)
            {
                return NotFound();
            }

            ToDo toDoToRestore = new ToDo(){
                TodoName = objToRestore.TodoName,
                Complete = objToRestore.Complete,
                CreationDate = objToRestore.CreationDate,
                Deadline = objToRestore.Deadline,
                Tags = objToRestore.Tags
            };

            _db.ToDos.Add(toDoToRestore);

            _db.ArchivedTasks.Remove(objToRestore);

            _db.SaveChanges();
        }
        return RedirectToAction("List");
    }
}