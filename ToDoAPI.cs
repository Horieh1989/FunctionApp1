using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.AspNetCore.Http.Internal;
using System.Collections.Generic;
using Microsoft.AspNetCore.Routing;
using System.Linq;

namespace FunctionApp1
{
    public static class ToDoAPI
    {
        // for using the rest API
        // show the list of items

        static List<Todo> items = new List<Todo>();
        [FunctionName("CreatToDo")]
        public static async Task<IActionResult> CreatToDo
            ([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "Todo")] HttpRequest reg, TraceWriter log)
        {
            log.Info("Creat a new to do list item");
            string requestbody = await new StreamReader(reg.Body).ReadToEndAsync();
            var input = JsonConvert.DeserializeObject<ToDoCreatModel>(requestbody);
            var todo = new Todo() { TaskDescription = input.TaskDescription };
            items.Add(todo);
            return new OkObjectResult(input);


        }
        [FunctionName("GetTools")]// get items
        public static IActionResult GetToDos(

            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Todo")] HttpRequest reg, TraceWriter log)
        { log.Info("Getting  to do list item");
            return new ObjectResult(items);
        }



        [FunctionName("GetTodoById")]
        public static IActionResult GetToDoById
            ([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Todo/{id}")] HttpRequest reg, TraceWriter log,string Id )
        {
            var todo=items.FirstOrDefault(x => x.Id == Id); // take Id Items 
            if (todo == null)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(todo);    
        }


        [FunctionName("UpdateToDo")]
        public static async Task< IActionResult> UpdateToDo(
            [
            HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "Todo/{id}")] HttpRequest reg, TraceWriter log, string id 
            )
        {
            var todo = items.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return new NotFoundResult();
            }
            string requestbody = await new StreamReader(reg.Body).ReadToEndAsync();
            var update = JsonConvert.DeserializeObject<ToDoUpdateModel>(requestbody);
            
            todo.IsCompleted= update.IsCompleted;
            if(!string.IsNullOrEmpty(update.TaskDescription)) { 
            todo.TaskDescription = update.TaskDescription;
            }
            return new OkObjectResult(todo);


        }
    }      
    
}
