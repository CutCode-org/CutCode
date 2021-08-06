using Stylet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CutCode
{
    public class HomeViewModel : Screen
    {
        private readonly IThemeService themeService;
        private readonly IPageService pageService;
        public HomeViewModel(IThemeService _themeService, IPageService _pageService)
        {
            themeService = _themeService;
            themeService.ThemeChanged += ThemeChanged;

            pageService = _pageService;

            SetAppearance();
            IsSearched = false;

            AllCodes = new ObservableCollection<CodeBoxModel>();
            var code1 = new CodeBoxModel(1, "Pyqt5 scroll bar", "pyqt5 custom scroll bar made in python ok??", true, "Python", "print('Hello world')", 1628136352, themeService);
            var code2 = new CodeBoxModel(2, "C++ binary search", "blah blah binary blah ... ye and ok \n so what??", false, "C++", "print('Hello world')", 1628136352, themeService);
            var code3 = new CodeBoxModel(3, "wpf sample combo box", "combo box style that is responseive to the ui and also \n and ok ?? blah ... ", true, "C#", "print('Hello world')", 1628136352, themeService);
            AllCodes.Add(code1);
            AllCodes.Add(code2);
            AllCodes.Add(code3);
            AllCodes.Add(code1);
            AllCodes.Add(code2);

            AllLangs = new ObservableCollection<string>()
            {
            "All languages", "Python", "C++", "C#", "CSS", "Dart", "Golang", "Html", "Java",
            "Javascript", "Kotlin", "Php", "C", "Ruby", "Rust","Sql", "Swift"
            };

        }
        private void ThemeChanged(object sender, EventArgs e)
        {
            SetAppearance();
        }

        private void SetAppearance()
        {
            Theme = themeService.IsLightTheme;
            searchBarBackground = themeService.IsLightTheme ? ColorCon.Convert("#DADBDC") : ColorCon.Convert("#2A2E33");
            searchBarTextColor = themeService.IsLightTheme ? ColorCon.Convert("#000000") : ColorCon.Convert("#FFFFFF");
            searchBarHoverColor = themeService.IsLightTheme ? ColorCon.Convert("#D0D1D2") : ColorCon.Convert("#373737");
            comboboxHoverColor = themeService.IsLightTheme ? ColorCon.Convert("#C5C7C9") : ColorCon.Convert("#202326");
            comboboxBackgroundColor = themeService.IsLightTheme ? ColorCon.Convert("#E3E5E8") : ColorCon.Convert("#202225");
        }

        public IList<CodeBoxModel> AllCodes { get; set; }
        public IList<string> AllLangs { get; set; }

        #region Color
        private SolidColorBrush _searchBarBackground;
        public SolidColorBrush searchBarBackground
        {
            get => _searchBarBackground;
            set =>SetAndNotify(ref _searchBarBackground, value);
            
        }

        private SolidColorBrush _searchBarHoverColor;
        public SolidColorBrush searchBarHoverColor
        {
            get => _searchBarHoverColor;
            set => SetAndNotify(ref _searchBarHoverColor, value);

        }

        private SolidColorBrush _searchBarTextColor;
        public SolidColorBrush searchBarTextColor
        {
            get => _searchBarTextColor;
            set =>  SetAndNotify(ref _searchBarTextColor, value);
            
        }

        private SolidColorBrush _comboboxHoverColor;
        public SolidColorBrush comboboxHoverColor
        {
            get => _comboboxHoverColor;
            set => SetAndNotify(ref _comboboxHoverColor, value);

        }

        private SolidColorBrush _comboboxBackgroundColor;
        public SolidColorBrush comboboxBackgroundColor
        {
            get => _comboboxBackgroundColor;
            set => SetAndNotify(ref _comboboxBackgroundColor, value);

        }
        #endregion

        private bool _Theme;
        public bool Theme
        {
            get => _Theme;
            set => SetAndNotify(ref _Theme, value);
        }

        private bool _IsSearched;
        public bool IsSearched
        {
            get => _IsSearched;
            set => SetAndNotify(ref _IsSearched, value);

        }
        public async void SearchCommand(string text)
        {
            Trace.WriteLine("Searching ...");
            IsSearched = false;
            await Task.Delay(TimeSpan.FromSeconds(1)); // this will be changed to the search process
            IsSearched = true;
        }

        public void CodeSelectCommand(CodeBoxModel code) 
        {
            pageService.Page = new CodeViewModel(themeService, pageService, code);
        }
    }
}
