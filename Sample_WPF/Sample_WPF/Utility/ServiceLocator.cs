using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample_WPF.Utility
{
    public static class ServiceLocator
    {
        public static Func<Type, object> Resolve { get; set; }
    }
}
