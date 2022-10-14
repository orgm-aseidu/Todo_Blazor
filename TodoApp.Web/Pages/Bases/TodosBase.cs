using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text.Json;
using TodoApp.Web.Data.Dto;
using TodoApp.Web.Models;
using TodoApp.Web.Repositories.Contracts;

namespace TodoApp.Web.Pages.Bases
{
    public class TodosBase : ComponentBase
    {
        [Inject]
        IJSRuntime JSRuntime { get; set; }

        [Inject]
        ITodoItemsLocalStorageRepo TodoItemsLocalStorageRepo { get; set; }
        public List<TodoItem> Todos { get; set; } = new List<TodoItem>();

        public List<TodoItem> FilteredTodos { get; set; } = new List<TodoItem>();

        public TodoItemCreateDto CreateDto { get; set; } = new TodoItemCreateDto();

        protected override async Task OnInitializedAsync()
        {
            Todos = await TodoItemsLocalStorageRepo.GetTodoItemsCollection();
            FilteredTodos = await TodoItemsLocalStorageRepo.GetTodoItemsCollection();
        }
        public async void AddItem (TodoItemCreateDto item)
        {
            int id = Todos.Count > 0 ? Todos.Max(t => t.Id) + 1 : 1;
            Todos.Add(new TodoItem() {Id=id, Task=item.Task});
            CreateDto.Task = "";
            await TodoItemsLocalStorageRepo.SaveTodoItemsCollection(Todos);
            

        }

        public async Task GetAllItemsAsync()
        {
            FilteredTodos = await TodoItemsLocalStorageRepo.GetTodoItemsCollection(); 
        }

        public async void UpdateCompletionStatus(TodoItem item)
        {
                var index = Todos.IndexOf(item);
            if (index == -1)
            {
                throw new Exception("item does not exixt in database");
            }
            else
            {
                Todos[index].IsCompleted = !item.IsCompleted;
                //Todos[index]=item;
                await TodoItemsLocalStorageRepo.SaveTodoItemsCollection(Todos);
                StateHasChanged();
            }
            
        }
        public async void UpdateTask(TodoItem item)
        {
            var index = Todos.IndexOf(item);
            if (index == -1)
            {
                throw new Exception("item does not exixt in database");
            }
            else
            {
                Todos[index].IsCompleted = !item.IsCompleted;
                await TodoItemsLocalStorageRepo.SaveTodoItemsCollection(Todos);
                StateHasChanged();
            }

        }
        public async void MakeReadOnly(string elementId)
        {
            await JSRuntime.InvokeVoidAsync("makeFieldReadOnly", elementId);
        }
        public async Task RemoveItem(TodoItem item)
        {
            Todos.Remove(item);
            Console.WriteLine(JsonSerializer.Serialize(Todos));
            //FilteredTodos.Remove(item);
            await TodoItemsLocalStorageRepo.SaveTodoItemsCollection(Todos);
            StateHasChanged();
        }

        public void ChangeCompletionStatus(int itemId)
        {
            var item = Todos.FirstOrDefault(i => i.Id == itemId);
            item.IsCompleted=!item.IsCompleted;

        }

        public async Task MakeEditable(string elementId)
        {
           await JSRuntime.InvokeVoidAsync("makeFieldEditable", elementId);
        }

        public async Task FilterByCompletionStatusAsync(bool status)
        {
            List<TodoItem> allItems = await TodoItemsLocalStorageRepo.GetTodoItemsCollection();
           FilteredTodos = allItems.FindAll(item => item.IsCompleted == status);
        }
    }
}
