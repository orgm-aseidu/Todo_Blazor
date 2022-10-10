using Microsoft.AspNetCore.Components;
using TodoApp.Web.Data.Dto;
using TodoApp.Web.Models;

namespace TodoApp.Web.Pages.Bases
{
    public class TodosBase : ComponentBase
    {
        public List<TodoItem> Todos { get; set; } = new List<TodoItem>();

        public TodoItemCreateDto CreateDto { get; set; } = new TodoItemCreateDto();
        public void AddItem (TodoItemCreateDto item)
        {
            Todos.Add(new TodoItem() { Task=item.Task});
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
    }
}
