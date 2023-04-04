using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Data;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    public class ToDoController : Controller
    {
        private readonly ToDoDatabase _database;

        public ToDoController(ToDoDatabase db)
        {
            _database = db;
        }

        public async Task<IActionResult> Index()
        { 
            var todos = await _database.ToDos.ToListAsync();
            return View(todos);
        }

        public async Task<IActionResult> Add(ToDoItem item) {
            _database.ToDos.Add(item);
            await _database.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Complete(int id)
        {
            var todo = await _database.ToDos.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }
            todo.IsComplete = true;
            await _database.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
