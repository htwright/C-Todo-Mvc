using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Todo_Mvc.Models;

namespace Todo_Mvc.Controllers
{
    public class TodoController : Controller
    {
        public IActionResult Index()
        {
          // var x = Todo.GetAll();
          // View.RemoveAll();
          return View(Todo.GetAll());
        }

        public IActionResult Create(string task)
        {
            Todo.Create(task);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int Id){
          Console.WriteLine("inside delete");
          Todo.Delete(Id);
          return RedirectToAction("Index");
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}