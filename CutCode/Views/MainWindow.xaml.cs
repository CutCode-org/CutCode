using GalaSoft.MvvmLight.Ioc;
using StyletIoC;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
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
        private readonly IThemeService _themeService;
        public MainWindow()
        {
            InitializeComponent();
            var leftBarBtns = new List<Button>() { homeBtn, addBtn, favBtn, setBtn };


            _themeService = StyletIoC.Get<IThemeService>();
            _themeService.ThemeChanged += ThemeChanged;

            DataContext = new MainViewModel(leftBarBtns);
        }

        public void ThemeChanged(object sender, EventArgs e)
        {
            Trace.WriteLine("Theme Changed");
            App.Current.Resources["background"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFF"); ;
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
                WindowStartupLocation = WindowStartupLocation.Manual;
                DragMove();
            }
        }

        private void MinimizeClicked(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

        private void MaximizeClicked(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }
    }
}
