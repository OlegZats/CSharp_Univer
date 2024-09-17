using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zatsepin9000.TaskPlanner.Domain.Models.Enums;

namespace Zatsip9000.TaskPlanner.Domain.Models
{
    public class WorkItem
    {
        public Guid Id { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime DueDate { get; set; }
        public Priority Priority { get; set; }
        public Complexity Complexity { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsComplete { get; set; }
        public bool IsCompleted { get; set; }

        public WorkItem Clone() 
        {
            return new WorkItem
            {
                Id = Guid.NewGuid()
            };
        }

        public override string ToString()
        {
            string formattedDate = DueDate.ToString("dd.MM.yyyy");
            string priorityString = Priority.ToString().ToLower();

            return $"{Title}: due {formattedDate}, {priorityString} pr";
        }
    }
}
