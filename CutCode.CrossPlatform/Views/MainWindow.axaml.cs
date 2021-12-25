using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;
using CutCode.CrossPlatform.Controllers;
using CutCode.CrossPlatform.Helpers;
using CutCode.CrossPlatform.Interfaces;

namespace CutCode.CrossPlatform.Views
{
    public partial class MainWindow : Window
    {
        private TabControl _tabControl;
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            
            this.GetControl("tabControl", out _tabControl);
            PageService.Current.TabChanged += (sender, args) =>
            {
                _tabControl.SelectedIndex = PageService.Current.CurrentTabIndex;
            };
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