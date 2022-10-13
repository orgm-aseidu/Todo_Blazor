using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using TodoApp.Web.Data.Dto;
using TodoApp.Web.Models;

namespace TodoApp.Web.Pages.Bases
{
    public class TodosBase : ComponentBase
    {
        [Inject]
        IJSRuntime JSRuntime { get; set; }  
        public List<TodoItem> Todos { get; set; } = new List<TodoItem>();

        public TodoItemCreateDto CreateDto { get; set; } = new TodoItemCreateDto();
        public void AddItem (TodoItemCreateDto item)
        {
            int id = Todos.Count > 0 ? Todos.Max(t => t.Id) + 1 : 1;
            Todos.Add(new TodoItem() {Id=id, Task=item.Task});
            CreateDto.Task = "";

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
