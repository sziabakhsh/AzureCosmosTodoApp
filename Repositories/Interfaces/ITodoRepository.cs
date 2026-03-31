using AzureCosmosTodoApp.Models;

namespace AzureCosmosTodoApp.Repositories.Interfaces
{
    public interface ITodoRepository
    {
        Task<List<TodoItem>> GetAllAsync();
        Task<TodoItem?> GetByIdAsync(string id);
        Task AddAsync(TodoItem item);
        Task UpdateAsync(TodoItem item);
        Task DeleteAsync(string id);
    }
}