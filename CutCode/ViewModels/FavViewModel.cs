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
        private readonly IPageService pageService;
        private readonly IDataBase database;
        public FavViewModel(IThemeService _themeService, IPageService _pageService, IDataBase _database)
        {
            themeService = _themeService;
            themeService.ThemeChanged += ThemeChanged;
            SetAppearance();
            IsSearched = false;

            pageService = _pageService;

            database = _database;
            database.PropertyChanged += DataBaseUpdated;

            AllCodes = new ObservableCollection<CodeBoxModel>();
            foreach(var code in DataBaseStatic.AllCodes)
            {
                if (code.isFav) AllCodes.Add(code);
            }
        }

        private void DataBaseUpdated(object sender, EventArgs e)
        {
            AllCodes = new ObservableCollection<CodeBoxModel>();
            foreach (var code in DataBaseStatic.AllCodes)
            {
                if (code.isFav) AllCodes.Add(code);
            }
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

        public void SearchCommand(string text)
        {
            IsSearched = false;
            database.SearchCode(text);
            IsSearched = true;
        }

        public void CodeSelectCommand(CodeBoxModel code)
        {
            pageService.Page = new CodeViewModel(themeService, pageService, database, code);
        }
    }
}
