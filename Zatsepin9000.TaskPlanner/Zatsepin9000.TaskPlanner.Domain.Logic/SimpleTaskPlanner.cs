using System;
using System.Collections.Generic;
using System.Linq;
using Zatsip9000.TaskPlanner.Domain.Models;
using Zatsepin9000.TaskPlanner.DataAccess.Abstractions;


namespace Zatsepin9000.TaskPlanner.Domain.Logic
{
    public class SimpleTaskPlanner : IWorkItemsRepository
    {
        private readonly IWorkItemsRepository _repository;

        public SimpleTaskPlanner()
        {
        }

        public SimpleTaskPlanner(IWorkItemsRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public WorkItem[] All => throw new NotImplementedException();

        public WorkItem[] CreatePlan(WorkItem[] workItems)
        {

            var items = _repository.All;

            List<WorkItem> activeItems = items.Where(item => !item.IsComplete).ToList();

            activeItems.Sort((x, y) =>
            {
                int priorityComparison = y.Priority.CompareTo(x.Priority);
                if (priorityComparison != 0)
                    return priorityComparison;

                int dueDateComparison = x.DueDate.CompareTo(y.DueDate);
                if (dueDateComparison != 0)
                    return dueDateComparison;

                return string.Compare(x.Title, y.Title, StringComparison.Ordinal);
            });

            return activeItems.ToArray();
        }

        public IEnumerable<object> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
