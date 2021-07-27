using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using Stylet;
using StyletIoC;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CutCode
{
    public class MainViewModel : Screen
    {
        //private List<Button> leftBarBtns;
        private List<Object> Pages;
        private readonly IThemeService _themeService;
        public ObservableCollection<SideBarBtnModel> sideBarBtns { get; set; }
        public MainViewModel(IThemeService themeService)
        {
            _themeService = themeService;
            _themeService.IsLightTheme = false;
            _themeService.ThemeChanged += ThemeChanged;

            sideBarBtns = new ObservableCollection<SideBarBtnModel>();

            // there should be some kind of condition here
            sideBarBtns.Add(new SideBarBtnModel("Home", _themeService));
            sideBarBtns.Add(new SideBarBtnModel("Add", _themeService));
            sideBarBtns.Add(new SideBarBtnModel("Favourite", _themeService));
            sideBarBtns.Add(new SideBarBtnModel("Settings", _themeService));

            Pages = new List<Object>() { new HomePage(), new AddPage(), new FavPage(), new SettingPage()};
            currentPage = Pages[0];

            backgroundColor = _themeService.IsLightTheme ? Brushes.Blue : (SolidColorBrush)new BrushConverter().ConvertFrom("#36393F");
            titleBarColor = _themeService.IsLightTheme ? Brushes.Gray : (SolidColorBrush)new BrushConverter().ConvertFrom("#202225");
            SideBarColor = _themeService.IsLightTheme ? Brushes.Violet : (SolidColorBrush)new BrushConverter().ConvertFrom("#2A2E33");
            mainTextColor = _themeService.IsLightTheme ? Brushes.Green : (SolidColorBrush)new BrushConverter().ConvertFrom("#94969A");
            toolTipColor = _themeService.IsLightTheme ? Brushes.AntiqueWhite : (SolidColorBrush)new BrushConverter().ConvertFrom("#1E1E1E");

        }
        private void ThemeChanged(object sender, EventArgs e)
        {
            Trace.WriteLine("Theme changed ...");
            backgroundColor = _themeService.IsLightTheme ? Brushes.Yellow : (SolidColorBrush)new BrushConverter().ConvertFrom("#36393F");
            titleBarColor = _themeService.IsLightTheme ? Brushes.Green : (SolidColorBrush)new BrushConverter().ConvertFrom("#202225");
            SideBarColor = _themeService.IsLightTheme ? Brushes.Wheat : (SolidColorBrush)new BrushConverter().ConvertFrom("#2A2E33");
            mainTextColor = _themeService.IsLightTheme ? Brushes.White : (SolidColorBrush)new BrushConverter().ConvertFrom("#94969A");
            toolTipColor = _themeService.IsLightTheme ? Brushes.WhiteSmoke : (SolidColorBrush)new BrushConverter().ConvertFrom("#1E1E1E");
        }

        private Object _currentPage;
        public Object currentPage
        {
            get => _currentPage;
            set
            {
                if(value != _currentPage)
                {
                    SetAndNotify(ref _currentPage, value);
                }
            }
        }

        public SideBarBtnModel selected_item { get; set; }
        public void ChangePageCommand()
        {
            selected_item.background = _themeService.IsLightTheme ? Brushes.Red : (SolidColorBrush)new BrushConverter().ConvertFrom("#36393F");
            int ind = 0;
            foreach (var btn in sideBarBtns)
            {
                if (btn != selected_item) btn.background = Brushes.Transparent;
                else ind = sideBarBtns.IndexOf(btn);
            }
            if (currentPage != Pages[ind]) currentPage = Pages[ind];
            
        }

        private SolidColorBrush _backgroundColor;
        public SolidColorBrush backgroundColor
        {
            get => _backgroundColor;
            set
            {
                if(value != _backgroundColor)
                {
                    SetAndNotify(ref _backgroundColor, value);
                }
            }
        }

        private SolidColorBrush _titleBarColor;
        public SolidColorBrush titleBarColor
        {
            get => _titleBarColor;
            set
            {
                if (value != _titleBarColor)
                {
                    SetAndNotify(ref _titleBarColor, value);
                }
            }
        }

        private SolidColorBrush _sideBarColor;
        public SolidColorBrush SideBarColor
        {
            get => _sideBarColor;
            set
            {
                if (value != _sideBarColor)
                {
                    SetAndNotify(ref _sideBarColor, value);
                }
            }
        }

        private SolidColorBrush _mainTextColor;
        public SolidColorBrush mainTextColor
        {
            get => _mainTextColor;
            set
            {
                if (value != _mainTextColor)
                {
                    SetAndNotify(ref _mainTextColor, value);
                }
            }
        }

        private SolidColorBrush _toolTipColor;
        public SolidColorBrush toolTipColor
        {
            get => _toolTipColor;
            set
            {
                if (value != _toolTipColor)
                {
                    SetAndNotify(ref _toolTipColor, value);
                }
            }
        }
    }
}
