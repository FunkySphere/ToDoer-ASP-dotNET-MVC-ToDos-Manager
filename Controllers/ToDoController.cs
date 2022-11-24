using Microsoft.AspNetCore.Mvc;
using ToDoer.Models;
using ToDoer.Data;

namespace ToDoer.Controllers;

public class ToDoController : Controller
{
    private readonly ApplicationDbContext _db;
    private List<int> _filteredListOfIds = new();
    public ToDoController(ApplicationDbContext db)
    {
        _db = db;
    }
    public IActionResult List(string sortOrder, string searchString)
    {
        if (_db.ToDos != null)
        {
            ViewBag.currentSort = sortOrder;

            ViewBag.IdSortParm = String.IsNullOrEmpty(sortOrder) ? "id" : "";
            ViewBag.NameSortParam = sortOrder == "name" ? "name_desc" : "name";
            ViewBag.DeadlineSortParam = sortOrder == "deadline" ? "deadline" : "deadline_desc";
            ViewBag.CompletedSortParam = sortOrder == "complete" ? "complete_desc" : "complete";

            IEnumerable<ToDo> objList = from s in _db.ToDos select s;

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
                case "deadline":
                    objList = objList.OrderBy(s => s.Deadline);
                    break;
                case "deadline_desc":
                    objList = objList.OrderByDescending(s => s.Deadline);
                    break;
                case "complete":
                    objList = objList.OrderBy(s => s.Complete);
                    break;
                case "complete_desc":
                    objList = objList.OrderByDescending(s => s.Complete);
                    break;
                default:
                    objList = objList.OrderBy(s => s.Id);
                    break;
            }

            return View(objList.ToList());
        }
        return NotFound();
    }

    //GET Create
    public IActionResult Create()
    {
        return View();
    }

    //POST Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(ToDo obj)
    {
        //validation
        if (ModelState.IsValid && _db.ToDos != null)
        {
            _db.ToDos.Add(obj);
            _db.SaveChanges();

            return RedirectToAction("List");
        }
        return View(obj);
    }

    //GET Delete
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0 || _db.ToDos == null)
        {
            return NotFound();
        }
        var obj = _db.ToDos.Find(id);
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
        if (_db.ToDos != null)
        {
            var obj = _db.ToDos.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.ToDos.Remove(obj);
            _db.SaveChanges();
        }
        return RedirectToAction("List");
    }

    //GET Update
    public IActionResult Update(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        if (_db.ToDos != null)
        {
            var obj = _db.ToDos.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        return NotFound();
    }

    //POST Update
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(ToDo task)
    {
        if (ModelState.IsValid && _db.ToDos != null)
        {
            _db.ToDos.Update(task);
            _db.SaveChanges();

            return RedirectToAction("List");
        }
        return View(task);
    }

    //GET Delete
    public IActionResult Finish(int? id)
    {
        if (id == null || id == 0 || _db.ToDos == null)
        {
            return NotFound();
        }
        var obj = _db.ToDos.Find(id);
        if (obj == null)
        {
            return NotFound();
        }
        return View(obj);
    }

    public IActionResult Info(int? id)
    {
        if (id == null || _db.ToDos == null)
        {
            return NotFound();
        }
        var obj = _db.ToDos.Find(id);
        if (obj == null)
        {
            return NotFound();
        }
        return View(obj);
    }

    //POST Archive
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Archive(ToDo obj)
    {
        if (_db.ToDos != null && _db.ArchivedTasks != null)
        {
            var objToArchive = _db.ToDos.Find(obj.Id);
            if (objToArchive == null)
            {
                return NotFound();
            }

            ArchivedToDo toDoToArchive = new ArchivedToDo()
            {
                TodoName = objToArchive.TodoName,
                Complete = objToArchive.Complete,
                CreationDate = objToArchive.CreationDate,
                Deadline = objToArchive.Deadline,
                ArchiveDate = DateTime.Now,
                Tags = objToArchive.Tags
            };

            _db.ArchivedTasks.Add(toDoToArchive);

            _db.ToDos.Remove(objToArchive);

            _db.SaveChanges();
        }
        return RedirectToAction("List");
    }
}