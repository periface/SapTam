namespace SapModule.Application.Todo.Dto
{
    /// <summary>
    /// Inputs should not be automapped to the domain model
    /// </summary>
    public class TodoInput
    {
        public int TodoListId { get; set; }
        public string TodoName { get; set; }
    }
}
