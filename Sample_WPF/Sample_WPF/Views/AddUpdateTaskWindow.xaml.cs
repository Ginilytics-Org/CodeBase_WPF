using GalaSoft.MvvmLight.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Sample_WPF.Utility;
using Sample_WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Sample_WPF.Views
{
    /// <summary>
    /// Interaction logic for AddUpdateTaskWindow.xaml
    /// </summary>
    public partial class AddUpdateTaskWindow : Window
    {
        public AddUpdateTaskWindow()
        {
            InitializeComponent();
            AddUpdateViewInstance.DataContext = (Application.Current as App)?.ServiceProvider.GetService<TaskAddUpdateViewModel>() ;

            Messenger.Default.Register<CloseUserControlMessage>(this, OnCloseUserControlMessageReceived);

        }

        private void OnCloseUserControlMessageReceived(CloseUserControlMessage message)
        {
            if (myContainer.Children.Contains(AddUpdateViewInstance))
            {
                myContainer.Children.Remove(AddUpdateViewInstance);
            }
        }
    }
}
