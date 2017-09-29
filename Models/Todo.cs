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
        
        public static void Delete(Todo Task){
          string ConnString = "Host=tantor.db.elephantsql.com;Username=whiptylt;Password=uLlB5fEK9y_Q82cNj8daLMRtSzys03jf;Database=whiptylt";
          using(NpgsqlConnection Conn = new NpgsqlConnection(ConnString)){
            try{
              Conn.Open();
            } catch (Exception e){
              Console.WriteLine(e.ToString());
            }
            using (var cmd = new NpgsqlCommand(string.Format("Delete From todos where task = '{0}'", Task.Task), Conn)){
              cmd.ExecuteNonQuery();
              Console.WriteLine("done");
            }
          }
          GlobalVariables.Todos.Remove(Task);

        }
        }

        public static System.Collections.Generic.List<Todo> GetAll(){

          string ConnString = "Host=tantor.db.elephantsql.com;Username=whiptylt;Password=uLlB5fEK9y_Q82cNj8daLMRtSzys03jf;Database=whiptylt";
          using(NpgsqlConnection Conn = new NpgsqlConnection(ConnString)){
            try{
              Conn.Open();
            } catch (Exception e){
              Console.WriteLine(e.ToString());
            }
            List<string> returnStrings = new List<string>();
            List<bool> returnBools = new List<bool>();
            using (var cmd = new NpgsqlCommand("SELECT * FROM todos", Conn)){
              // cmd.ExecuteNonQuery();
              NpgsqlDataReader dr = cmd.ExecuteReader();
              while(dr.Read()) {
                  returnStrings.Add(dr.GetString(1));
                  // Console.WriteLine(dr.GetBoolean(2));
                  returnBools.Add(dr.GetBoolean(2));

                }
              }
              Console.WriteLine("done");
              int i = 0;
              foreach(string s in returnStrings){
                Todo x = new Todo();
                x.Task = s;
                x.Completed = returnBools[i];
                GlobalVariables.Todos.Add(x);
                Console.WriteLine(s);
                i++;
              }
              // for(var i = 0; i < returnStrings.Count; i++){
                
                // GlobalVariables.Todos[i].Completed = returnBools[i];
                // GlobalVariables.Todos.Add(x);
                // Console.WriteLine(returnBools[i]);
              // }

            }
          // }
          // GlobalVariables.foreach()
        Console.WriteLine(GlobalVariables.Todos);
          return GlobalVariables.Todos;
        }
        // public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}