using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;

namespace CutCode.CrossPlatform.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        public void ChangeWindowPosition(object sender, PointerPressedEventArgs e)
        {
            if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
            {
                BeginMoveDrag(e);
            }
        }

        public void MaxChangeWindowPosition(object sender, PointerEventArgs e)
        {
            var cp = e.GetCurrentPoint(this);
            if (cp.Properties.IsLeftButtonPressed)
            {
                WindowState = WindowState.Normal;
                WindowStartupLocation = WindowStartupLocation.Manual;

                BeginMoveDrag(new PointerPressedEventArgs(this, e.Pointer, this.GetVisualRoot(), cp.Position, e.Timestamp, cp.Properties, e.KeyModifiers));
            }
        }

        private void MinimizeClicked(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

        private void MaximizeClicked(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        private void ExitClicked(object sender, RoutedEventArgs e) => Close();

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}