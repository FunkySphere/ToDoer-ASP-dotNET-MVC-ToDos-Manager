using Microsoft.AspNetCore.Mvc;
using ToDoer.Models;

namespace ToDoer.Controllers;

public class ToDoController : Controller
{
    public IActionResult List()
    {
        return View();
    }

    public IActionResult Backlog()
    {
        return View();
    }
}