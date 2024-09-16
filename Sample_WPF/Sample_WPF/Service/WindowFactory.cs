using Microsoft.Extensions.DependencyInjection;
using Sample_WPF.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sample_WPF.Service
{
    public class WindowFactory : IWindowFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public WindowFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public T CreateWindow<T>() where T : Window
        {
            return _serviceProvider.GetRequiredService<T>();
        }
    }
}
