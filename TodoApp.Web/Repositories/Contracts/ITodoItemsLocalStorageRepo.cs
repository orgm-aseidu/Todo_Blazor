using TodoApp.Web.Models;

namespace TodoApp.Web.Repositories.Contracts
{
    public interface ITodoItemsLocalStorageRepo
    {
        Task<List<TodoItem>> GetTodoItemsCollection();
        Task SaveTodoItemsCollection(List<TodoItem> todoItems);

        Task RemoveTodoItemsCollection();
    }
}
