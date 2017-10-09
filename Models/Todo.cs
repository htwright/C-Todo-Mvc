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

        public int Id {get; set; }

        public static void Create(string TaskText){
          var task = new Todo();
          Console.WriteLine("creating");
          string ConnString = "Host=tantor.db.elephantsql.com;Username=whiptylt;Password=uLlB5fEK9y_Q82cNj8daLMRtSzys03jf;Database=whiptylt";
          using(NpgsqlConnection Conn = new NpgsqlConnection(ConnString)){
            try{
              Conn.Open();
            } catch (Exception e){
              Console.WriteLine(e.ToString());
            }
            using (var cmd = new NpgsqlCommand(string.Format("INSERT INTO todos (task) values ('{0}') returning id", TaskText), Conn)){
              cmd.ExecuteNonQuery();
              NpgsqlDataReader reader = cmd.ExecuteReader();
              while(reader.Read()){
                task.Id = reader.GetInt32(0);
              }
              

            }
          }
          // task.Task = TaskText;
          // GlobalVariables.Todos.Add(task);
        }
        
        public static void Delete(int Id){
          string ConnString = "Host=tantor.db.elephantsql.com;Username=whiptylt;Password=uLlB5fEK9y_Q82cNj8daLMRtSzys03jf;Database=whiptylt";
          using(NpgsqlConnection Conn = new NpgsqlConnection(ConnString)){
            try{
              Conn.Open();
            } catch (Exception e){
              Console.WriteLine(e.ToString());
            }
            using (var cmd = new NpgsqlCommand(string.Format("Delete From todos where id = '{0}'", Id), Conn)){
              cmd.ExecuteNonQuery();
            }
          }
          GlobalVariables.Todos.RemoveAll(x => x.Id != Id);
          
        }
        

        public static List<Todo> GetAll(){

          string ConnString = "Host=tantor.db.elephantsql.com;Username=whiptylt;Password=uLlB5fEK9y_Q82cNj8daLMRtSzys03jf;Database=whiptylt";
          using(NpgsqlConnection Conn = new NpgsqlConnection(ConnString)){
            try{
              Conn.Open();
            } catch (Exception e){
              Console.WriteLine(e.ToString());
            }
            List<string> returnStrings = new List<string>();
            List<bool> returnBools = new List<bool>();
            List<int> returnInts = new List<int>();
            using (var cmd = new NpgsqlCommand("SELECT * FROM todos", Conn)){
              NpgsqlDataReader dr = cmd.ExecuteReader();
              while(dr.Read()) {
                  returnInts.Add(dr.GetInt32(0));
                  returnStrings.Add(dr.GetString(1));
                  returnBools.Add(dr.GetBoolean(2));

                }
              }
              GlobalVariables.Todos = new List<Todo>();
              int i = 0;
              foreach(string s in returnStrings){
                Todo x = new Todo();
                x.Task = s;
                x.Id = returnInts[i];
                x.Completed = returnBools[i];
                GlobalVariables.Todos.Add(x);
                i++;
              }

            }
          return GlobalVariables.Todos;
        }
    }
}