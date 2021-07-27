using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using Stylet;
using StyletIoC;
using System;
using System.Collections.Generic;
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
        public MainViewModel(IThemeService themeService)
        {
            //this.leftBarBtns = leftBarBtns;
            Pages = new List<Object>() { new HomePage(), new AddPage(), new FavPage(), new SettingPage()};
            currentPage = Pages[0];

            _themeService = themeService;
            _themeService.ThemeChanged += ThemeChanged;
        }
        private void ThemeChanged(object sender, EventArgs e)
        {
            Trace.WriteLine("Theme changed ...");
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

        private SolidColorBrush _homeBtn;
        public SolidColorBrush homeBtn
        {
            get => _homeBtn;
            set
            {
                if (value != _homeBtn)
                {
                    SetAndNotify(ref _homeBtn, value);
                }
            }
        }
        public void HomeBtnCommand()
        {

        }

        private SolidColorBrush _addBtn;
        public SolidColorBrush addBtn
        {
            get => _addBtn;
            set
            {
                if (value != _addBtn)
                {
                    SetAndNotify(ref _addBtn, value);
                }
            }
        }
        public void AddBtnCommand()
        {

        }

        private SolidColorBrush _favBtn;
        public SolidColorBrush favBtn
        {
            get => _favBtn;
            set
            {
                if (value != _favBtn)
                {
                    SetAndNotify(ref _favBtn, value);
                }
            }
        }
        public void FavBtnCommand()
        {

        }

        private SolidColorBrush _settingBtn;
        public SolidColorBrush settingBtn
        {
            get => _settingBtn;
            set
            {
                if (value != _settingBtn)
                {
                    SetAndNotify(ref _settingBtn, value);
                }
            }
        }
        public void SettingBtnCommand()
        {

        }



        public void ChangePageCommand(Button btn)
        {
            //_themeService.IsLightTheme = true;
            //Trace.WriteLine(_themeService.IsLightTheme);
            btn.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#36393F");

            int ind = 0;
            foreach (var b in leftBarBtns)
            {
                if (b != btn) b.Background = Brushes.Transparent;
                else ind = leftBarBtns.IndexOf(b);
            }
            if(currentPage != Pages[ind]) currentPage = Pages[ind];
        }
    }
}
