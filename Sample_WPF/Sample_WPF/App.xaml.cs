using Microsoft.Extensions.DependencyInjection;
using Sample_WPF.Interface;
using Sample_WPF.Repository;
using Sample_WPF.Service;
using Sample_WPF.ServiceInterface;
using Sample_WPF.Utility;
using Sample_WPF.ViewModel;
using Sample_WPF.Views;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Sample_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider;

        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = new MainWindow
            {
                DataContext = ServiceProvider.GetService<TaskViewModel>()
            };

            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ITaskService, TaskService>();
            services.AddSingleton<ITaskRepository, TaskRepository>();

            // Register view models
            services.AddTransient<TaskViewModel>();
            services.AddTransient<TaskAddUpdateViewModel>();

            // Register views
            //services.AddTransient<MainWindow>();
            services.AddTransient<AddUpdateTaskWindow>();
            services.AddTransient<AddUpdateView>();
            services.AddSingleton<IWindowFactory, WindowFactory>();
        }
    }
}

