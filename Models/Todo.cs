using System;
using System.Collections.Generic;
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

          string ConnString = "Host=tantor.db.elephantsql.com;Username=whiptylt;Password=uLlB5fEK9y_Q82cNj8daLMRtSzys03jf;Database=whiptylt";
          using(NpgsqlConnection Conn = new NpgsqlConnection(ConnString)){
            try{
              Conn.Open();
            } catch (Exception e){
              Console.WriteLine(e.ToString());
            }
            List<string> returnList = new List<string>();
            using (var cmd = new NpgsqlCommand("SELECT * FROM todos", Conn)){
              // cmd.ExecuteNonQuery();
              NpgsqlDataReader dr = cmd.ExecuteReader();
              while(dr.Read()) {
                  returnList.Add(dr.GetString(1));
                  
                }
                // Console.WriteLine(dr[1].GetType());
                // Todo x = new Todo();
                // Console.WriteLine(dr);
                // x.Task = dr[1];
                // GlobalVariables.Todos.Add(x);
              }
              Console.WriteLine("done");
              foreach(string s in returnList){
                Todo x = new Todo();
                x.Task = s;
                GlobalVariables.Todos.Add(x);
                Console.WriteLine(s);
              }
              

            }
          // }
          // GlobalVariables.foreach()
        Console.WriteLine(GlobalVariables.Todos);
          return GlobalVariables.Todos;
        }
        // public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}