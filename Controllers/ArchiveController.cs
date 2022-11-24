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
    public IActionResult List(string sortOrder, string searchString)
    {
        if (_db.ArchivedTasks != null)
        {
            ViewBag.currentSort = sortOrder;

            ViewBag.IdSortParm = String.IsNullOrEmpty(sortOrder) ? "id" : "";
            ViewBag.NameSortParam = sortOrder == "name" ? "name_desc" : "name";
            ViewBag.CreationDateSortParam = sortOrder == "creation_date" ? "creation_date_desc" : "creation_date";
            ViewBag.ArchiveDateSortParam = sortOrder == "archive_date" ? "archive_date_desc" : "archive_date";

            IEnumerable<ArchivedToDo> objList = from s in _db.ArchivedTasks select s;

            if(!String.IsNullOrEmpty(searchString))
            {
                objList = objList.Where(s => s.TodoName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name":
                    objList = objList.OrderBy(s => s.TodoName);
                    break;
                case "name_desc":
                    objList = objList.OrderByDescending(s => s.TodoName);
                    break;
                case "creation_date":
                    objList = objList.OrderBy(s => s.CreationDate);
                    break;
                case "creation_date_desc":
                    objList = objList.OrderByDescending(s => s.CreationDate);
                    break;
                case "archive_date":
                    objList = objList.OrderBy(s => s.ArchiveDate);
                    break;
                case "archive_date_desc":
                    objList = objList.OrderByDescending(s => s.ArchiveDate);
                    break;
                default:
                    objList = objList.OrderBy(s => s.Id);
                    break;
            }

            return View(objList.ToList());
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
        if (_db.ArchivedTasks != null && _db.ToDos != null)
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