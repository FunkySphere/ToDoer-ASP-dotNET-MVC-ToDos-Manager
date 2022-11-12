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
        if(ModelState.IsValid && _db.ToDos != null)
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
        if(id == null || id == 0 || _db.ToDos == null)
        {
            return NotFound();
        }
        var obj = _db.ToDos.Find(id);
        if(obj == null)
        {
            return NotFound();
        }
        return View(obj);
    }

    //POST Delete
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePost(int? id)
    {
        var obj = _db.ToDos.Find(id);
        if(obj == null)
        {
            return NotFound();
        }
        _db.ToDos.Remove(obj);
        _db.SaveChanges();
        return RedirectToAction("List");
    }
}