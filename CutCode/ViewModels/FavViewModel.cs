using Stylet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
            AllCodes = database.FavCodes;
            database.FavCodesUpdated += FavCodesUpdated;

            VisChange();
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

        private void FavCodesUpdated(object sender, EventArgs e)
        {
            AllCodes = database.FavCodes;
            VisChange();
        }

        private void VisChange(string text = "You don't have any favourite notes :(")
        {
            if (AllCodes.Count == 0)
            {
                labelVis = Visibility.Visible;
                codesVis = Visibility.Hidden;
                emptyLabel = text;
            }
            else
            {
                labelVis = Visibility.Hidden;
                codesVis = Visibility.Visible;
            }
        }


        private Visibility _labelVis;
        public Visibility labelVis
        {
            get => _labelVis;
            set => SetAndNotify(ref _labelVis, value);
        }
        private Visibility _codesVis;
        public Visibility codesVis
        {
            get => _codesVis;
            set => SetAndNotify(ref _codesVis, value);
        }

        private string _emptyLabel;
        public string emptyLabel
        {
            get => _emptyLabel;
            set => SetAndNotify(ref _emptyLabel, value);
        }

        private ObservableCollection<CodeBoxModel> _AllCodes;
        public ObservableCollection<CodeBoxModel> AllCodes
        {
            get => _AllCodes;
            set => SetAndNotify(ref _AllCodes, value);
        }

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
            Trace.WriteLine($"this is {text}");
            IsSearched = false;
            if (text == "")
            {
                AllCodes = database.FavCodes;
                VisChange();
            }
            else
            {
                if(AllCodes.Count > 0)
                {
                    AllCodes = await database.SearchCode(text, "Home");
                    if (AllCodes.Count == 0) VisChange("Not found :(");
                }
            }
            IsSearched = true;
        }

        public void CodeSelectCommand(CodeBoxModel code)
        {
            pageService.remoteChange = "Home";
            pageService.Page = new CodeViewModel(themeService, pageService, database, code);
        }
    }
}
