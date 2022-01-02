using Avalonia.Controls;

namespace CutCode.CrossPlatform.Helpers
{
    public static class Extensions
    {
        public static void GetControl<T>(this Control control, string name, out T element) where T : Control
        {
            element = control.FindControl<T>(name);
        }
    }
}
