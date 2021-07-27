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
using System.Windows;
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
            _themeService.ThemeChanged += ThemeChanged;
            _themeService.IsLightTheme = false;

            sideBarBtns = new ObservableCollection<SideBarBtnModel>();

            // there should be some kind of condition here
            sideBarBtns.Add(new SideBarBtnModel("Home", _themeService));
            sideBarBtns.Add(new SideBarBtnModel("Add", _themeService));
            sideBarBtns.Add(new SideBarBtnModel("Favourite", _themeService));
            sideBarBtns.Add(new SideBarBtnModel("Settings", _themeService));

            Pages = new List<Object>() { new HomePage(), new AddPage(), new FavPage(), new SettingPage()};
            currentPage = Pages[0];
        }
        private void ThemeChanged(object sender, EventArgs e)
        {
            Trace.WriteLine("Theme changed ...");

            backgroundColor = _themeService.IsLightTheme ? (Color)ColorConverter.ConvertFromString("#787C84") : (Color)ColorConverter.ConvertFromString("#36393F");
            titleBarColor = _themeService.IsLightTheme ? (Color)ColorConverter.ConvertFromString("#23406C") : (Color)ColorConverter.ConvertFromString("#202225");
            SideBarColor = _themeService.IsLightTheme ? (Color)ColorConverter.ConvertFromString("#000099") : (Color)ColorConverter.ConvertFromString("#2A2E33");
            mainTextColor = _themeService.IsLightTheme ? (Color)ColorConverter.ConvertFromString("#A0A0A0") : (Color)ColorConverter.ConvertFromString("#94969A");
            this.Resources["tooltip_background"] = _themeService.IsLightTheme ? (SolidColorBrush)new BrushConverter().ConvertFrom("#94969A") : (SolidColorBrush)new BrushConverter().ConvertFrom("#1E1E1E");
            Application.Current.Resources["tooltip_foreground"] = !_themeService.IsLightTheme ? (SolidColorBrush)new BrushConverter().ConvertFrom("#94969A") : (SolidColorBrush)new BrushConverter().ConvertFrom("#1E1E1E");
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

        //public SideBarBtnModel selected_item { get; set; }
        public void ChangePageCommand(string selected_item)
        {
            int ind = 0;
            foreach (var btn in sideBarBtns)
            {
                if (btn.toolTipText != selected_item) btn.background = (Color)ColorConverter.ConvertFromString("#00FFFFFF");
                else ind = sideBarBtns.IndexOf(btn);
            }
            sideBarBtns[ind].background = _themeService.IsLightTheme ? (Color)ColorConverter.ConvertFromString("#9933FF") : (Color)ColorConverter.ConvertFromString("#36393F");
            if (currentPage != Pages[ind]) currentPage = Pages[ind];
        }

        private Color _backgroundColor;
        public Color backgroundColor
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

        private Color _titleBarColor;
        public Color titleBarColor
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

        private Color _sideBarColor;
        public Color SideBarColor
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

        private Color _mainTextColor;
        public Color mainTextColor
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

        private Color _toolTipColor;
        public Color toolTipColor
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

        private Color _toolTipTextColor;
        public Color toolTipTextColor
        {
            get => _toolTipTextColor;
            set
            {
                if(value != _toolTipTextColor)
                {
                    SetAndNotify(ref _toolTipTextColor, value);
                }
            }
        }
    }
}
