namespace TodoApp.Web.Data.Dto
{
    public class TodoItemReadDto
    {
        public int Id { get; set; }
        public string Task { get; set; }
        public bool IsCompleted { get; set; } = false;
    }
}
