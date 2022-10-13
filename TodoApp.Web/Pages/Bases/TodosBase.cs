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
            Todos = await TodoItemsLocalStorageRepo.GetTodoItemsCollection();
        }
        public async void AddItem (TodoItemCreateDto item)
        {
            int id = Todos.Count > 0 ? Todos.Max(t => t.Id) + 1 : 1;
            Todos.Add(new TodoItem() {Id=id, Task=item.Task});
            CreateDto.Task = "";
            await TodoItemsLocalStorageRepo.SaveTodoItemsCollection(Todos);
            

        }

        public async void UpdateCompletionStatus(TodoItem item)
        {
            try
            {
                var index = Todos.IndexOf(item);
                if(index == -1)
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
            catch (Exception)
            {

                throw;
            }
           
            
        }

        public async void MakeReadOnly(string elementId)
        {
            await JSRuntime.InvokeVoidAsync("makeFieldReadOnly", elementId);
        }
        public async void RemoveItem(TodoItem item)
        {
            Todos.Remove(item);
            await TodoItemsLocalStorageRepo.SaveTodoItemsCollection(Todos);
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
