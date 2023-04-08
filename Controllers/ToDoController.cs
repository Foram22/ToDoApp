using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Data;
using ToDoApp.Migrations;
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Add(string newItem) 
        {
            if(newItem != null) {
                var todo = new ToDoItem
                {
                    Title = newItem,
                    IsComplete = false
                };
                _database.ToDos.Add(todo);
                await _database.SaveChangesAsync();
            }
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Complete(IEnumerable<int> isComplete)
        {
            if(isComplete != null)
            {
                foreach (var item in isComplete)
                {
                    var todo = _database.ToDos.FirstOrDefault(t => t.Id == item);
                    if(todo != null) {
                        todo.IsComplete = true;
                    }
                }
                _database.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult DeleteAll()
        {
            _database.ToDos.RemoveRange(_database.ToDos);
            _database.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
