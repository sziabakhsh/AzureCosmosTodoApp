using Microsoft.AspNetCore.Mvc;
using AzureCosmosTodoApp.Models;
using AzureCosmosTodoApp.Repositories.Interfaces;

namespace AzureCosmosTodoApp.Controllers
{
    public class TodoController : Controller
    {
        private readonly ITodoRepository _repository;

        public TodoController(ITodoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var todos = await _repository.GetAllAsync();
            return View(todos);
        }

        [HttpPost]
        public async Task<IActionResult> Create(string title)
        {
            var todo = new TodoItem
            {
                Title = title,
                IsCompleted = false
            };

            await _repository.AddAsync(todo);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(string id)
        {
            await _repository.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        // GET: /Todo/Edit/{id}
        public async Task<IActionResult> Edit(string id)
        {
            var todo = await _repository.GetByIdAsync(id);

            if (todo == null)
                return NotFound();

            return View(todo);
        }

        // POST: /Todo/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(TodoItem model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _repository.UpdateAsync(model);

            return RedirectToAction("Index");
        }
    }
}