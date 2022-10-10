using Microsoft.AspNetCore.Components;
using TodoApp.Web.Data.Dto;
using TodoApp.Web.Models;

namespace TodoApp.Web.Pages.Bases
{
    public class TodosBase : ComponentBase
    {
        public List<TodoItem> Todos { get; set; }

        public void AddItem (TodoItemCreateDto item)
        {
             Todos.Add(new TodoItem() { Task=item.Task});
        }

        public void RemoveItem(TodoItem item)
        {
            Todos.Remove(item);
        }
    }
}
