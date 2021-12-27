using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Avalonia.VisualTree;
using CutCode.CrossPlatform.Controllers;
using CutCode.CrossPlatform.Helpers;
using CutCode.CrossPlatform.Interfaces;
using CutCode.CrossPlatform.ViewModels;

namespace CutCode.CrossPlatform.Views
{
    public partial class MainWindow : Window
    {
        private TabControl _tabControl;
        private TabItem _homeTab;
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            
            this.GetControl("tabControl", out _tabControl);
            this.GetControl("homeTab", out _homeTab);

            _homeTab.Content = new HomeView();
            
            PageService.Current.TabChanged += (sender, args) =>
            {
                _tabControl.SelectedIndex = PageService.Current.CurrentTabIndex;
            };

            PageService.Current.ExternalPageChange += (sender, args) =>
            {
                
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