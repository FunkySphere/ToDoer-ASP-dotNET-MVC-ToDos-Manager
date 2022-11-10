using Microsoft.AspNetCore.Mvc;
using ToDoer.Models;
using ToDoer.Data;

namespace ToDoer.Controllers;

public class BacklogController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}