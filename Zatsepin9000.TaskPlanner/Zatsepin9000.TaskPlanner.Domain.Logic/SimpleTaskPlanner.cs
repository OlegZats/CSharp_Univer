using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zatsip9000.TaskPlanner.Domain.Models;

namespace Zatsepin9000.TaskPlanner.Domain.Logic
{
    public class SimpleTaskPlanner
    {
        public WorkItem[] CreatePlan(WorkItem[] items)
        {
            List<WorkItem> itemList = items.ToList();

            itemList.Sort((x, y) =>
            {
                int priorityComparison = y.Priority.CompareTo(x.Priority);
                if (priorityComparison != 0)
                    return priorityComparison;

                int dueDateComparison = x.DueDate.CompareTo(y.DueDate);
                if (dueDateComparison != 0)
                    return dueDateComparison;

                return string.Compare(x.Title, y.Title, StringComparison.Ordinal);
            });

            return itemList.ToArray();
        }
    }
}
