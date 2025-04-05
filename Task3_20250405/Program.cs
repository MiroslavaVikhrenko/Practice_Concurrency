using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Task3_20250405
{
    /*
     Разработайте систему управления задачами, которая использует базу данных, построенную на EF Core. 
    Система должна позволять пользователям создавать задачи, назначать ответственных и отслеживать их выполнение. 
    Пользователи должны иметь возможность просматривать список всех задач, задачи, назначенные на них, и задачи, 
    которые они назначили другим пользователям.

Хранить историю изменений записей. Использовать пару скомпилированных запросов и параллелизм для одной таблицы на выбор.

Ключевые функции: 

Создание задачи: Пользователи должны иметь возможность создавать новые задачи, устанавливать их приоритет, 
    описание и срок выполнения. 
Назначение ответственных: Пользователи должны иметь возможность назначать ответственных за задачи. 
Отслеживание выполнения: Пользователи должны иметь возможность отмечать задачи как выполненные и отслеживать прогресс выполнения задач. 

Просмотр списка задач: Пользователи должны иметь возможность просматривать список всех задач и фильтровать его по различным критериям. 
Просмотр задач, назначенных на пользователя: Пользователи должны иметь возможность просматривать список задач, 
    которые были назначены на них. 
Просмотр задач, назначенных пользователем: Пользователи должны иметь возможность просматривать список задач, 
    которые они назначили другим пользователям.
     */
    internal class Program
    {
        static void Main(string[] args)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                SeedDatabase(db);

                // Create new task
                CreateNewTask("Fix login issue", "Resolve the login issue that appears when users enter incorrect credentials", DateTime.Now.AddDays(2), 3, db);

                // Assign task to a user
                AssignTaskToUser(1, 1, db);

                // Mark task as completed
                MarkTaskAsCompleted(1, db);

                // Display all tasks
                DisplayAllTasks(db);

                // Display tasks assigned to a specific user
                DisplayTasksAssignedToUser(1, db);

                // Display tasks assigned by a specific user
                DisplayTasksAssignedByUser(1, db);
            }
        }
        // Create a new task
        public static void CreateNewTask(string title, string description, DateTime dueDate, int priority, ApplicationContext db)
        {
            var task = new Task
            {
                Title = title,
                Description = description,
                DueDate = dueDate,
                Priority = priority
            };

            db.Tasks.Add(task);
            db.SaveChanges();
        }

        // Assign a task to a user
        public static void AssignTaskToUser(int taskId, int userId, ApplicationContext db)
        {
            // Compiled query to get the task by its ID
            var query = EF.CompileQuery((ApplicationContext context, int taskId) =>
                context.Tasks.FirstOrDefault(t => t.Id == taskId));

            // Execute the query with the database context and taskId
            var task = query(db, taskId);

            if (task != null)
            {
                // Assign the user to the task
                task.AssignedUserId = userId;
                db.SaveChanges();
                Console.WriteLine($"Task '{task.Title}' assigned to user with ID {userId}");
            }
            else
            {
                Console.WriteLine("Task not found.");
            }
        }

        // Mark a task as completed
        public static void MarkTaskAsCompleted(int taskId, ApplicationContext db)
        {
            var query = EF.CompileQuery((ApplicationContext context, int taskId) =>
                context.Tasks.FirstOrDefault(t => t.Id == taskId));

            var task = query(db, taskId);

            if (task != null)
            {
                task.IsCompleted = true;
                db.SaveChanges();
                Console.WriteLine($"Task '{task.Title}' marked as completed.");
            }
            else
            {
                Console.WriteLine("Task not found.");
            }
        }

        // Display all tasks
        public static void DisplayAllTasks(ApplicationContext db)
        {
            var query = EF.CompileQuery((ApplicationContext context) =>
                context.Tasks.ToList());

            var tasks = query(db);

            foreach (var task in tasks)
            {
                string status = task.IsCompleted ? "Completed" : "Pending";
                Console.WriteLine($"Task ID: {task.Id}, Title: {task.Title}, Due Date: {task.DueDate}, Status: {status}");
            }
        }

        // Display tasks assigned to a specific user
        public static void DisplayTasksAssignedToUser(int userId, ApplicationContext db)
        {
            var query = EF.CompileQuery((ApplicationContext context, int userId) =>
        context.Tasks.ToList()); // Fetch all tasks first

            var tasks = query(db, userId)
                        .Where(t => t.AssignedUserId == userId)  // Now filter on the client-side
                        .ToList();  // Force in-memory filtering

            foreach (var task in tasks)
            {
                string status = task.IsCompleted ? "Completed" : "Pending";
                Console.WriteLine($"Task ID: {task.Id}, Title: {task.Title}, Due Date: {task.DueDate}, Status: {status}");
            }
        }

        // Display tasks assigned by a specific user
        public static void DisplayTasksAssignedByUser(int userId, ApplicationContext db)
        {
            var query = EF.CompileQuery((ApplicationContext context, int userId) =>
        context.Tasks.ToList()); // Fetch all tasks first

            var tasks = query(db, userId)
                        .Where(t => t.AssignedUserId == userId)  // Now filter on the client-side
                        .ToList();  // Force in-memory filtering

            foreach (var task in tasks)
            {
                string status = task.IsCompleted ? "Completed" : "Pending";
                Console.WriteLine($"Task ID: {task.Id}, Title: {task.Title}, Due Date: {task.DueDate}, Status: {status}");
            }
        }

        // Seed the database with some initial data
        public static void SeedDatabase(ApplicationContext db)
        {
            db.Users.AddRange(
                new User { Name = "Tanaka", Email = "tanaka@example.com" },
                new User { Name = "Yamada", Email = "yamada@example.com" }
            );
            db.SaveChanges();

            db.Tasks.AddRange(
                new Task { Title = "Complete report", Description = "Finish the monthly report", DueDate = DateTime.Now.AddDays(5), Priority = 1 },
                new Task { Title = "Fix bugs", Description = "Resolve the issues in the application", DueDate = DateTime.Now.AddDays(3), Priority = 2 }
            );
            db.SaveChanges();
        }
    }
}
