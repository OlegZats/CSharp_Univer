using System;
using System.Collections.Generic;
using System.Linq;
using Zatsepin9000.TaskPlanner.Domain.Logic;
using Zatsip9000.TaskPlanner.Domain.Models;
using Zatsepin9000.TaskPlanner.Domain.Models.Enums;

internal static class Program
{

    public static void Main(string[] args)
    {
        List<WorkItem> workItems = new List<WorkItem>();

        while (true)
        {
            Console.WriteLine("Ввеедіть назву завдання (або натисніть Enter для завершення): ");
            string title = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(title))
            {
                break;
            }

            Console.WriteLine("Введіть опис завдання: ");
            string description = Console.ReadLine();

            Console.WriteLine("Введіть дату завершення (dd.MM.yyyy): ");
            DateTime dueDate;
            while(!DateTime.TryParse(Console.ReadLine(), out dueDate))
            {
                Console.WriteLine("Непраивльний формат дати. Спробуй ще раз (dd.MM.yyyy): ");

            }

            Console.WriteLine("Введіть приіорітет (None, Low, Medium, High, Urgent): ");
            Priority priority;
            while(!Enum.TryParse(Console.ReadLine(), true, out priority))
            {
                Console.WriteLine("Не правильний приіорітет. Спробуй ще раз (Low, Medium, High): ");
            }

            Complexity complexity = default;
            WorkItem workIte = new WorkItem
            {
                Title = title,
                Description = description,
                DueDate = dueDate,
                Priority = priority,
                Complexity = complexity,
                CreationDate = DateTime.Now,
                IsComplete = false,
            };

            workItems.Add(workIte);

        }

        SimpleTaskPlanner planner = new SimpleTaskPlanner();

        WorkItem[] sortedItems = planner.CreatePlan(workItems.ToArray());

        Console.WriteLine("\nВідсортовані завдання: ");
        foreach(var item in sortedItems)
        {
            Console.WriteLine(item.ToString());
        }
    }
}