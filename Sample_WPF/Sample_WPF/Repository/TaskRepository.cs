using Sample_WPF.Interface;
using Sample_WPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample_WPF.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly List<TaskItem> _tasks = new List<TaskItem>
        {
            new TaskItem { Id = 1, Title = "Task 1", Description = "Description 1", Status = "To Do" },
            new TaskItem { Id = 2, Title = "Task 2", Description = "Description 2", Status = "In Progress" }
        };

        public async Task<IEnumerable<TaskItem>> GetTasksAsync()
        {
            await Task.Delay(100); // Simulate async operation
            return _tasks;
        }

        public async Task<TaskItem> GetTaskByIdAsync(int id)
        {
            await Task.Delay(100); // Simulate async operation
            return _tasks.FirstOrDefault(t => t.Id == id);
        }

        public async Task AddTaskAsync(TaskItem task)
        {
            await Task.Delay(100); // Simulate async operation
            _tasks.Add(task);
        }

        public async Task UpdateTaskAsync(TaskItem task)
        {
            await Task.Delay(100); // Simulate async operation
            var existingTask = _tasks.FirstOrDefault(t => t.Id == task.Id);
            if (existingTask != null)
            {
                existingTask.Title = task.Title;
                existingTask.Description = task.Description;
                existingTask.Status = task.Status;
            }
        }

        public async Task DeleteTaskAsync(int id)
        {
            await Task.Delay(100); // Simulate async operation
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                _tasks.Remove(task);
            }
        }
    }
}
