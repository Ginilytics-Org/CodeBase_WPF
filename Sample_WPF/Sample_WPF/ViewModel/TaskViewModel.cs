using Microsoft.Extensions.DependencyInjection;
using Sample_WPF.Model;
using Sample_WPF.Service;
using Sample_WPF.ServiceInterface;
using Sample_WPF.Utility;
using Sample_WPF.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Sample_WPF.ViewModel
{
    public class TaskViewModel : ViewModelBase
    {
        // Observable collections for tasks
        public ObservableCollection<TaskItem> ToDoTasks { get; set; }
        public ObservableCollection<TaskItem> InProgressTasks { get; set; }
        public ObservableCollection<TaskItem> DoneTasks { get; set; }

        // Selected task
        private TaskItem _selectedTask;
        public TaskItem SelectedTask
        {
            get => _selectedTask;
            set
            {
                _selectedTask = value;
                OnPropertyChanged(nameof(SelectedTask));
                UpdateTaskButtonsVisibility();
            }
        }

        // Add/Update Task properties
        private TaskItem _newTask = new TaskItem();
        public TaskItem NewTask
        {
            get => _newTask;
            set
            {
                _newTask = value;
                OnPropertyChanged(nameof(NewTask));
            }
        }

        private bool _isAddTaskVisible;
        public bool IsAddTaskVisible
        {
            get => _isAddTaskVisible;
            set
            {
                _isAddTaskVisible = value;
                OnPropertyChanged(nameof(IsAddTaskVisible));
            }
        }

        private bool _isTaskButtonsVisible;
        public bool IsTaskButtonsVisible
        {
            get => _isTaskButtonsVisible;
            set
            {
                _isTaskButtonsVisible = value;
                OnPropertyChanged(nameof(IsTaskButtonsVisible));
            }
        }
        public ObservableCollection<string> StatusOptions { get; }


        // Commands
        public ICommand AddTaskCommand { get; }
        public ICommand UpdateTaskCommand { get; }
        public ICommand DeleteTaskCommand { get; }
        public ICommand SaveTaskCommand { get; }

        public TaskViewModel()
        {
            StatusOptions = new ObservableCollection<string> { "To Do", "In Progress", "Done" };

            // Initialize commands
            AddTaskCommand = new RelayCommand(ShowAddTaskForm);
            UpdateTaskCommand = new RelayCommand(UpdateTask);
            DeleteTaskCommand = new RelayCommand(DeleteTask);
            SaveTaskCommand = new RelayCommand(SaveTask);

            // Initialize task collections
            ToDoTasks = new ObservableCollection<TaskItem>();
            InProgressTasks = new ObservableCollection<TaskItem>();
            DoneTasks = new ObservableCollection<TaskItem>();
        }

        private void ShowAddTaskForm()
        {
            NewTask = new TaskItem(); // Clear the form for adding a new task
            IsAddTaskVisible = true;
            IsTaskButtonsVisible = false;
        }

        private void UpdateTask()
        {
            if (SelectedTask != null)
            {
                NewTask = new TaskItem
                {
                    Id = SelectedTask.Id,
                    Title = SelectedTask.Title,
                    Description = SelectedTask.Description,
                    Status = SelectedTask.Status
                };
                IsAddTaskVisible = true;
                IsTaskButtonsVisible = false;
            }
        }

        private void DeleteTask()
        {
            if (SelectedTask != null)
            {
                if (ToDoTasks.Contains(SelectedTask)) ToDoTasks.Remove(SelectedTask);
                if (InProgressTasks.Contains(SelectedTask)) InProgressTasks.Remove(SelectedTask);
                if (DoneTasks.Contains(SelectedTask)) DoneTasks.Remove(SelectedTask);
                SelectedTask = null;
                IsTaskButtonsVisible = false;
            }
        }

        private void SaveTask()
        {
            if (NewTask == null) return;

            if (NewTask.Id == 0) // New task
            {
                // Add to the appropriate collection based on status
                if (NewTask.Status == "To Do") ToDoTasks.Add(NewTask);
                if (NewTask.Status == "In Progress") InProgressTasks.Add(NewTask);
                if (NewTask.Status == "Done") DoneTasks.Add(NewTask);
            }
            else // Update existing task
            {
                var taskToUpdate = ToDoTasks.FirstOrDefault(t => t.Id == NewTask.Id)
                                ?? InProgressTasks.FirstOrDefault(t => t.Id == NewTask.Id)
                                ?? DoneTasks.FirstOrDefault(t => t.Id == NewTask.Id);

                if (taskToUpdate != null)
                {
                    taskToUpdate.Title = NewTask.Title;
                    taskToUpdate.Description = NewTask.Description;
                    taskToUpdate.Status = NewTask.Status;
                }
            }

            NewTask = new TaskItem(); // Clear the form
            IsAddTaskVisible = false;
            IsTaskButtonsVisible = false;
        }

        private void UpdateTaskButtonsVisibility()
        {
            IsTaskButtonsVisible = SelectedTask != null;
        }

    }
}

