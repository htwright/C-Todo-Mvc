using System;
using Npgsql;
using System.Data.SqlClient;
namespace Todo_Mvc.Models
{
    public class Todo
    {

        public string Task { get; set; }
        public bool Completed { get; set; } = false;

        public static void Create(string TaskText){
          var task = new Todo();
          string ConnString = "Host=tantor.db.elephantsql.com;Username=whiptylt;Password=uLlB5fEK9y_Q82cNj8daLMRtSzys03jf;Database=whiptylt";
          using(NpgsqlConnection Conn = new NpgsqlConnection(ConnString)){
            try{
              Conn.Open();
            } catch (Exception e){
              Console.WriteLine(e.ToString());
            }
            using (var cmd = new NpgsqlCommand(string.Format("INSERT INTO todos (task) values ('{0}')", TaskText), Conn)){
              cmd.ExecuteNonQuery();
              Console.WriteLine("done");
              

            }
          }
          task.Task = TaskText;
          GlobalVariables.Todos.Add(task);
        }

        public static System.Collections.Generic.List<Todo> GetAll(){
          // GlobalVariables.foreach()
        Console.WriteLine(GlobalVariables.Todos);
          return GlobalVariables.Todos;
        }
        // public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}