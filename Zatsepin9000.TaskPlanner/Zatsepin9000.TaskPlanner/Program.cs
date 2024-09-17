using System;
using System.Collections.Generic;
using System.Linq;
using Zatsepin9000.TaskPlanner.Domain.Logic;
using Zatsip9000.TaskPlanner.Domain.Models;
using Zatsepin9000.TaskPlanner.Domain.Models.Enums;
using Zatsepin9000.TaskPlanner.DataAccess;
using Zatsepin9000.TaskPlanner.DataAccess.Abstractions;
using System.Text;

internal static class Program
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        List<WorkItem> workItems = new List<WorkItem>();
        SimpleTaskPlanner planner = GetPlanner();

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("Виберіть варіант:");
            Console.WriteLine("[A]dd work item");
            Console.WriteLine("[B]uild a plan");
            Console.WriteLine("[M]ark work item as completed");
            Console.WriteLine("[R]emove a work item");
            Console.WriteLine("[Q]uit the app");
            Console.Write("Варіант: ");
            string input = Console.ReadLine().ToUpper();

            switch (input)
            {
                case "A":
                    AddWorkItem(workItems);
                    break;
                case "B":
                    BuildPlan(workItems, planner);
                    break;
                case "M":
                    MarkAsCompleted(workItems);
                    break;
                case "R":
                    RemoveWorkItem(workItems);
                    break;
                case "Q":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }

    private static SimpleTaskPlanner GetPlanner()
    {
        return new SimpleTaskPlanner();
    }

    private static void AddWorkItem(List<WorkItem> workItems)
    {
        Console.WriteLine("Введіть назву завдання (або натисніть Enter для завершення): ");
        string title = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(title)) return;

        Console.WriteLine("Введіть опис завдання: ");
        string description = Console.ReadLine();

        Console.WriteLine("Введіть дату завершення (dd.MM.yyyy): ");
        DateTime dueDate;
        while (!DateTime.TryParse(Console.ReadLine(), out dueDate))
        {
            Console.WriteLine("Неправильний формат дати. Спробуйте ще раз (dd.MM.yyyy): ");
        }

        Console.WriteLine("Введіть пріоритет (None, Low, Medium, High, Urgent): ");
        Priority priority;
        while (!Enum.TryParse(Console.ReadLine(), true, out priority))
        {
            Console.WriteLine("Неправильний пріоритет. Спробуйте ще раз (Low, Medium, High): ");
        }

        Complexity complexity = default;

        WorkItem workItem = new WorkItem
        {
            Title = title,
            Description = description,
            DueDate = dueDate,
            Priority = priority,
            Complexity = complexity,
            CreationDate = DateTime.Now,
            IsComplete = false
        };

        workItems.Add(workItem);
        Console.WriteLine("Завдання додано!");
    }

    private static void BuildPlan(List<WorkItem> workItems, SimpleTaskPlanner planner)
    {
        WorkItem[] sortedItems = planner.CreatePlan(workItems.ToArray());
        //WorkItem[] sortedItems = planner.CreatePlan();


        Console.WriteLine("\nВідсортовані завдання:");
        foreach (var item in sortedItems)
        {
            Console.WriteLine(item.ToString());
        }
    }

    private static void MarkAsCompleted(List<WorkItem> workItems)
    {
        Console.WriteLine("Введіть номер завдання для позначення як виконаного: ");
        int index;
        if (int.TryParse(Console.ReadLine(), out index) && index > 0 && index <= workItems.Count)
        {
            workItems[index - 1].IsComplete = true;
            Console.WriteLine("Завдання позначено як виконане!");
        }
        else
        {
            Console.WriteLine("Неправильний номер завдання.");
        }
    }

    private static void RemoveWorkItem(List<WorkItem> workItems)
    {
        Console.WriteLine("Введіть номер завдання для видалення: ");
        int index;
        if (int.TryParse(Console.ReadLine(), out index) && index > 0 && index <= workItems.Count)
        {
            workItems.RemoveAt(index - 1);
            Console.WriteLine("Завдання видалено!");
        }
        else
        {
            Console.WriteLine("Неправильний номер завдання.");
        }
    }
}
