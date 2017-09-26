using System;

namespace Todo_Mvc.Models
{
    public class Todo
    {
        public string Task { get; set; }
        public bool Completed { get; set; } = false;

        public void Create(string Task){
          var task = new Todo();
          task.Task = Task;
          GlobalVariables.Todos.Add(task);
        }

        public static System.Collections.Generic.List<Todo> GetAll(){
          return GlobalVariables.Todos;
        }
        // public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}