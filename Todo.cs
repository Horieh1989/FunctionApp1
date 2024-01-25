using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionApp1
{
    // for using the rest API
    public class Todo
    {
        public string Id { get; set; }=Guid.NewGuid().ToString("n");// what is the reason
        public DateTime CreatedTim { get; set; } = DateTime.UtcNow;
        public string TaskDescription { get; set; }
        public bool IsCompleted { get; set; }=true;
    }
    public class ToDoCreatModel
    {
        public string TaskDescription { get; set; }
    }

    public class ToDoUpdateModel
    {
        public string TaskDescription { get; set; }
        public bool IsCompleted { get; set; }
    }
}
