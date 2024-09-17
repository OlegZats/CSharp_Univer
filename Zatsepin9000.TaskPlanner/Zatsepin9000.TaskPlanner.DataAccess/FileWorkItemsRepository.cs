using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Zatsip9000.TaskPlanner.Domain.Models;
using Zatsepin9000.TaskPlanner.DataAccess.Abstractions;


namespace Zatsepin9000.TaskPlanner.DataAccess
{
    public class FileWorkItemsRepository : IWorkItemsRepository
    {
        private const string FileName = "work-items.json";
        private readonly Dictionary<Guid, WorkItem> _workItems;

        public FileWorkItemsRepository()
        {
            if (File.Exists(FileName) && new FileInfo(FileName).Length > 0)
            {
                var jsonData = File.ReadAllText(FileName);
                var workItemArray = JsonConvert.DeserializeObject<WorkItem[]>(jsonData);
                _workItems = new Dictionary<Guid, WorkItem>();

                foreach (var workItem in workItemArray)
                {
                    _workItems[workItem.Id] = workItem;
                }
            }
            else
            {
                _workItems = new Dictionary<Guid, WorkItem>();
            }
        }

        public Guid Add(WorkItem workItem)
        {
            var workItemCopy = workItem.Clone();
            _workItems[workItemCopy.Id] = workItemCopy;
            return workItemCopy.Id;
        }

        public WorkItem Get(Guid id)
        {
            return _workItems.ContainsKey(id) ? _workItems[id] : null;
        }

        public WorkItem[] GetAll()
        {
            return new List<WorkItem>(_workItems.Values).ToArray();
        }

        public bool Update(WorkItem workItem)
        {
            if (!_workItems.ContainsKey(workItem.Id))
            {
                return false;
            }

            _workItems[workItem.Id] = workItem;
            return true;
        }

        public bool Remove(Guid id)
        {
            return _workItems.Remove(id);
        }

        public void SaveChanges()
        {
            var workItemArray = new List<WorkItem>(_workItems.Values).ToArray();
            var jsonData = JsonConvert.SerializeObject(workItemArray, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(FileName, jsonData);
        }
    }
}
