using GalaSoft.MvvmLight.Messaging;
using Sample_WPF.Model;
using Sample_WPF.ServiceInterface;
using Sample_WPF.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Sample_WPF.ViewModel
{
    public class TaskAddUpdateViewModel :ViewModelBase
    {
        private readonly ITaskService _taskService;
        private TaskItem _task;
        private bool _isEditMode;

        public TaskItem Task
        {
            get => _task;
            set
            {
                _task = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> StatusOptions { get; }

        public ICommand SaveTaskCommand { get; }
        public ICommand CancelCommand { get; }

        public TaskAddUpdateViewModel(ITaskService taskService)
        {
            _taskService = taskService;
            StatusOptions = new ObservableCollection<string> { "To Do", "In Progress", "Done" };

            SaveTaskCommand = new AsyncCommand(SaveTaskAsync);
            CancelCommand = new RelayCommand(Cancel);
        }

        public void Initialize(TaskItem task)
        {
            Task = task;
            _isEditMode = true;
        }

        private async Task SaveTaskAsync()
        {
            if (_isEditMode)
            {
                await _taskService.UpdateTaskAsync(Task);
            }
            else
            {
                await _taskService.AddTaskAsync(Task);
            }

            OnTaskSaved?.Invoke(this, EventArgs.Empty); // Notify that the task was saved
        }

        private void Cancel()
        {
            Messenger.Default.Send(new CloseUserControlMessage());
        }

        public event EventHandler OnTaskSaved; // Event to notify when the task is saved
        public event EventHandler OnTaskCanceled; // Event to notify when the task is canceled
    }
}

