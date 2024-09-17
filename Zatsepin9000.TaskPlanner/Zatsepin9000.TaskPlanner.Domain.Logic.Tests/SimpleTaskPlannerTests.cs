using Moq;
using Xunit;
using System;
using System.Collections.Generic;
using Zatsepin9000.TaskPlanner.Domain.Logic;
using Zatsip9000.TaskPlanner.Domain.Models;

namespace Zatsepin9000.TaskPlanner.Domain.Logic.Tests
{
    public class SimpleTaskPlannerTests
{
    [Fact]

    public void CreatePlan_ShouldSortTasksCorrectly()
    {
        var mockRepository = new Mock<IWorkItemsRepository>();
        var tasks = new List<WorkItem>
            {
                new WorkItem { Id = Guid.NewGuid(), Title = "Task 3", Priority = (Models.Enums.Priority)2, DueDate = DateTime.Now.AddDays(3), IsCompleted = false },
                new WorkItem { Id = Guid.NewGuid(), Title = "Task 1", Priority = (Models.Enums.Priority) 1, DueDate = DateTime.Now.AddDays(1), IsCompleted = false },
                new WorkItem { Id = Guid.NewGuid(), Title = "Task 2", Priority = (Models.Enums.Priority) 1, DueDate = DateTime.Now.AddDays(2), IsCompleted = false }
            };

        mockRepository.Setup(repo => repo.All).Returns(tasks);

        var planner = new SimpleTaskPlanner(mockRepository.Object);

        var plan = planner.CreatePlan();

        Assert.Equal("Task 1", plan[0].Title);
        Assert.Equal("Task 2", plan[1].Title);
        Assert.Equal("Task 3", plan[2].Title);
    }

    [Fact]
    public void CreatePlan_ShouldIncludeOnlyRelevantTasks()
    {
        var mockRepository = new Mock<IWorkItemsRepository>();
        var tasks = new List<WorkItem>
            {
                new WorkItem { Id = Guid.NewGuid(), Title = "Task 1", Priority = (Models.Enums.Priority) 1, DueDate = DateTime.Now, IsCompleted = false },
                new WorkItem { Id = Guid.NewGuid(), Title = "Task 2", Priority = (Models.Enums.Priority) 1, DueDate = DateTime.Now, IsCompleted = true }
            };

        mockRepository.Setup(repo => repo.All).Returns(tasks);

        var planner = new SimpleTaskPlanner(mockRepository.Object);

        var plan = planner.CreatePlan();

        Assert.Single(plan);
        Assert.Equal("Task 1", plan[0].Title);
    }
}
}
