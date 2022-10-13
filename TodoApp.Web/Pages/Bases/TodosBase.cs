using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
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

        public TodoItemCreateDto CreateDto { get; set; } = new TodoItemCreateDto();

        protected override async Task OnInitializedAsync()
        {
           
        }
        public void AddItem (TodoItemCreateDto item)
        {
            int id = Todos.Count > 0 ? Todos.Max(t => t.Id) + 1 : 1;
            Todos.Add(new TodoItem() {Id=id, Task=item.Task});
            CreateDto.Task = "";

        }

        public void UpdateItem(TodoItem item)
        {
            
        }

        public async void MakeReadOnly(string elementId)
        {
            await JSRuntime.InvokeVoidAsync("makeFieldReadOnly", elementId);
        }
        public void RemoveItem(TodoItem item)
        {
            Todos.Remove(item);
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

        //public void FilterByCompletionStatus(bool status)
        //{
        //    Tod
        //}
    }
}
