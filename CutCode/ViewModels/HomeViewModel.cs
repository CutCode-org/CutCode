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
        private readonly IDataBase database;
        public HomeViewModel(IThemeService _themeService, IPageService _pageService, IDataBase _dataBase)
        {
            themeService = _themeService;
            themeService.ThemeChanged += ThemeChanged;

            pageService = _pageService;

            database = _dataBase;
            database.AllCodesUpdated += AllCodesUpdated;

            SetAppearance();
            IsSearched = false;

            AllCodes = database.AllCodes;

            AllLangs = new ObservableCollection<string>()
            {
            "All languages", "Python", "C++", "C#", "CSS", "Dart", "Golang", "Html", "Java",
            "Javascript", "Kotlin", "Php", "C", "Ruby", "Rust","Sql", "Swift"
            };
            Sorts = new ObservableCollection<string>() { "Date", "Alphabet" };
        }
        private void ThemeChanged(object sender, EventArgs e)
        {
            SetAppearance();
        }
        private void AllCodesUpdated(object sender, EventArgs e) => AllCodes = database.AllCodes;

        private void SetAppearance()
        {
            Theme = themeService.IsLightTheme;
            searchBarBackground = themeService.IsLightTheme ? ColorCon.Convert("#DADBDC") : ColorCon.Convert("#2A2E33");
            searchBarTextColor = themeService.IsLightTheme ? ColorCon.Convert("#000000") : ColorCon.Convert("#FFFFFF");
            searchBarHoverColor = themeService.IsLightTheme ? ColorCon.Convert("#D0D1D2") : ColorCon.Convert("#373737");
            comboboxHoverColor = themeService.IsLightTheme ? ColorCon.Convert("#C5C7C9") : ColorCon.Convert("#202326");
            comboboxBackgroundColor = themeService.IsLightTheme ? ColorCon.Convert("#E3E5E8") : ColorCon.Convert("#202225");
        }

        public ObservableCollection<CodeBoxModel> AllCodes { get; set; }
        public IList<string> AllLangs { get; set; }
        public IList<string> Sorts { get; set; }

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

        private string _CurrentSort1;
        public string CurrentSort1
        {
            get => _CurrentSort1;
            set
            {
                SetAndNotify(ref _CurrentSort1, value);
                ComboBoxItemSelected(value);
            }
        }

        private string _CurrentSort2;
        public string CurrentSort2
        {
            get => _CurrentSort2;
            set 
            {
                SetAndNotify(ref _CurrentSort2, value);
                ComboBoxItemSelected(value);
            } 
        }

        private void ComboBoxItemSelected(string kind) 
        {
            Trace.WriteLine($"Gone order them ... {kind} ...");
            AllCodes = database.OrderCode(kind, AllCodes);
        } 

        public void SearchCommand(string text)
        {
            IsSearched = false;
            AllCodes = database.SearchCode(text, "Home", AllCodes);
            IsSearched = true;
        }

        public void CodeSelectCommand(CodeBoxModel code) 
        {
            pageService.Page = new CodeViewModel(themeService, pageService, database, code);
        }
    }
}
