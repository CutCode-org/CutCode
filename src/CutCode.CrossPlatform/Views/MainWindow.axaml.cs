using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Mixins;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Avalonia.Threading;
using Avalonia.VisualTree;
using CutCode.CrossPlatform.Controllers;
using CutCode.CrossPlatform.Helpers;
using CutCode.CrossPlatform.ViewModels;
using ReactiveUI;

namespace CutCode.CrossPlatform.Views
{
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public StackPanel MenuPanel => this.FindControl<StackPanel>(nameof(MenuPanel));

        public string CurrVm { get; set; }

        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            this.WhenActivated(d =>
            {
                if (ViewModel != null) ViewModel.Navigated += ViewModelOnNavigated;
            });
        }

        private void ViewModelOnNavigated(object? sender, string? e)
        {
            var lt = MenuPanel.GetLogicalChildren();
            foreach (NavigationItem? logical in lt.Cast<NavigationItem>())
            {
                if (logical.Name == e)
                    logical.IsActive = true;
                else
                    logical.IsActive = false;
            }
        }

        public void ChangeWindowPosition(object sender, PointerPressedEventArgs e)
        {
            BeginMoveDrag(e);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}