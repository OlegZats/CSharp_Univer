using Zatsip9000.TaskPlanner.Domain.Models;

namespace Zatsepin9000.TaskPlanner.Domain.Logic
{
    public interface IWorkItemsRepository
    {
        WorkItem[] All { get; }

        IEnumerable<object> GetAll();
    }
}