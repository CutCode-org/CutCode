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
        private IWindowManager windowManager;
        public ObservableCollection<SideBarBtnModel> sideBarBtns { get; set; }
        public MainViewModel(IWindowManager _windowManager, IThemeService themeService)
        {
            _themeService = themeService;
            windowManager = _windowManager;
            _themeService.ThemeChanged += ThemeChanged;
            _themeService.IsLightTheme = false;

            sideBarBtns = new ObservableCollection<SideBarBtnModel>();

            // there should be some kind of condition here
            sideBarBtns.Add(new SideBarBtnModel("Home", _themeService));
            sideBarBtns.Add(new SideBarBtnModel("Add", _themeService));
            sideBarBtns.Add(new SideBarBtnModel("Favourite", _themeService));
            sideBarBtns.Add(new SideBarBtnModel("Settings", _themeService));
            sideBarBtns[0].background = _themeService.IsLightTheme ? ColorCon.Convert("#FCFCFC") : ColorCon.Convert("#36393F");


            Pages = new List<Object>() { new HomeViewModel(themeService), new AddViewModel(), new FavViewModel(themeService), new SettingViewModel(_themeService) };
            currentPage = Pages[0];
        }
        private void ThemeChanged(object sender, EventArgs e)
        {
            
            backgroundColor = _themeService.IsLightTheme ? ColorCon.Convert("#FCFCFC") : ColorCon.Convert("#36393F");
            titleBarColor = _themeService.IsLightTheme ? ColorCon.Convert("#E3E5E8") : ColorCon.Convert("#202225");
            SideBarColor = _themeService.IsLightTheme ? ColorCon.Convert("#F2F3F5") : ColorCon.Convert("#2A2E33");
            mainTextColor = _themeService.IsLightTheme ? ColorCon.Convert("#0B0B13") : ColorCon.Convert("#94969A");

            exitImage = _themeService.IsLightTheme ? "../Resources/Images/Icons/exit_black.png" : "../Resources/Images/Icons/exit_white.png";
            minImage = _themeService.IsLightTheme ? "../Resources/Images/Icons/min_black.png" : "../Resources/Images/Icons/min_white.png";
            maxImage = _themeService.IsLightTheme ? "../Resources/Images/Icons/max_black.png" : "../Resources/Images/Icons/max_white.png";

            titlebarBtnsHoverColor = _themeService.IsLightTheme ? ColorCon.Convert("#D0D1D2") : ColorCon.Convert("#373737");
        }

        private Object _currentPage;
        public Object currentPage
        {
            get => _currentPage;
            set { SetAndNotify(ref _currentPage, value); }
        }

        public void ChangePageCommand(string selected_item)
        {
            int ind = 0;
            foreach (var btn in sideBarBtns)
            {
                if (btn.toolTipText != selected_item) btn.background = ColorCon.Convert("#00FFFFFF");
                else ind = sideBarBtns.IndexOf(btn);
            }
            sideBarBtns[ind].background = _themeService.IsLightTheme ? ColorCon.Convert("#FCFCFC") : ColorCon.Convert("#36393F");
            if (currentPage != Pages[ind]) currentPage = Pages[ind];
        }

        private SolidColorBrush _backgroundColor;
        public SolidColorBrush backgroundColor
        {
            get => _backgroundColor;
            set
            {
                if (value != _backgroundColor)
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

        private string _exitImage;
        public string exitImage
        {
            get => _exitImage;
            set
            { SetAndNotify(ref _exitImage, value); }
        }

        private string _maxImage;
        public string maxImage
        {
            get => _maxImage;
            set
            { SetAndNotify(ref _maxImage, value); }
        }

        private string _minImage;
        public string minImage
        {
            get => _minImage;
            set
            { SetAndNotify(ref _minImage, value); }
        }

        private SolidColorBrush _titlebarBtnsHoverColor;
        public SolidColorBrush titlebarBtnsHoverColor
        {
            get => _titlebarBtnsHoverColor;
            set
            {
                if (value != _titlebarBtnsHoverColor)
                {
                    SetAndNotify(ref _titlebarBtnsHoverColor, value);
                }
            }
        }
    }
        
}
