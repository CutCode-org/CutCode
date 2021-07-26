using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CutCode
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Button> leftBarBtns;
        public MainWindow()
        {
            InitializeComponent();

            leftBarBtns = new List<Button>() { homeBtn, addBtn, favBtn, setBtn };
        }
        public void ChangeWindowPosition(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        public void MaxChangeWindowPosition(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                WindowState = WindowState.Normal;
                DragMove();
            }
        }

        private void MinimizeClicked(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

        private void MaximizeClicked(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        private void LeftBarButtonsClicked(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            btn.Width = btn.Width - 10;
            btn.Height = btn.Height - 10;
            btn.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#36393F");
            btn.Width = btn.Width + 10;
            btn.Height = btn.Height + 10;

            foreach(var b in leftBarBtns)
            {
                if(b != btn) b.Background = Brushes.Transparent;
            }
        }



    }
}
