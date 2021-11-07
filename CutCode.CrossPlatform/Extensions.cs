using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CutCode.CrossPlatform
{
    public static class Extensions
    {
        public static void GetControl<T>(this Control control, string name, out T element) where T : Control
        {
            element = control.FindControl<T>(name);
        }
    }
}
