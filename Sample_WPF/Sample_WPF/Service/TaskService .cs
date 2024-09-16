using Sample_WPF.Interface;
using Sample_WPF.Model;
using Sample_WPF.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample_WPF.Service
{
   public class TaskService :ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }


        public async Task<IEnumerable<TaskItem>> GetTasksAsync()
        {
            return await _taskRepository.GetTasksAsync();
        }

        public async Task<TaskItem> GetTaskByIdAsync(int id)
        {
            return await _taskRepository.GetTaskByIdAsync(id);
        }

        public async Task AddTaskAsync(TaskItem task)
        {
            await _taskRepository.AddTaskAsync(task);
        }

        public async Task UpdateTaskAsync(TaskItem task)
        {
            await _taskRepository.UpdateTaskAsync(task);
        }

        public async Task DeleteTaskAsync(int id)
        {
            await _taskRepository.DeleteTaskAsync(id);
        }
    }
}
