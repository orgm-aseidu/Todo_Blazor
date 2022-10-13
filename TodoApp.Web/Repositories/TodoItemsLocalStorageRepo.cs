using Blazored.LocalStorage;
using TodoApp.Web.Models;
using TodoApp.Web.Repositories.Contracts;

namespace TodoApp.Web.Repositories
{
    public class TodoItemsLocalStorageRepo : ITodoItemsLocalStorageRepo
    {
        const string StorageKey = "TodoList";
        private ILocalStorageService _localStorageService;

        public TodoItemsLocalStorageRepo(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }
        public async Task<List<TodoItem>> GetTodoItemsCollection()
        {
            return await _localStorageService.GetItemAsync<List<TodoItem>>(StorageKey);
        }

        public Task RemoveTodoItemsCollection()
        {
            throw new NotImplementedException();
        }

        public async Task SaveTodoItemsCollection(List<TodoItem> todoItems)
        {
            await _localStorageService.SetItemAsync(StorageKey, todoItems);
        }
    }
}
