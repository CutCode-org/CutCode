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
    public class FavViewModel : Screen
    {

        private readonly IThemeService themeService;
        public FavViewModel(IThemeService _themeService)
        {
            themeService = _themeService;
            themeService.ThemeChanged += ThemeChanged;
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
        }

        public ObservableCollection<CodeBoxModel> AllCodes { get; set; }

        private SolidColorBrush _searchBarBackground;
        public SolidColorBrush searchBarBackground
        {
            get => _searchBarBackground;
            set => SetAndNotify(ref _searchBarBackground, value);

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
            set => SetAndNotify(ref _searchBarTextColor, value);

        }

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
            Trace.WriteLine("Item selected");
            Trace.WriteLine(code.title);
        }
    }
}
