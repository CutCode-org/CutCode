using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Avalonia;
using Avalonia.Media;
using Avalonia.Threading;
using CutCode.CrossPlatform.Interfaces;
using CutCode.CrossPlatform.Models;
using CutCode.CrossPlatform.ViewModels;
using ReactiveUI;

namespace CutCode
{
    public class HomeViewModel : ViewModelBase
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
            AllCodes = database.AllCodes;
            database.AllCodesUpdated += AllCodesUpdated;

            sortby = database.sortBy == "Date" ? 0 : 1;

            SetAppearance();
            IsSearched = false;

            AllLangs = new ObservableCollection<string>()
            {
            "All languages", "Python", "C++", "C#", "CSS", "Dart", "Golang", "Html", "Java",
            "Javascript", "Kotlin", "Php", "C", "Ruby", "Rust","Sql", "Swift"
            };
            Sorts = new ObservableCollection<string>() { "Date", "Alphabet" };

            VisChange();
        }
        private void ThemeChanged(object sender, EventArgs e)
        {
            SetAppearance();
        }
        private void SetAppearance()
        {
            Theme = themeService.IsLightTheme;
            searchBarBackground = themeService.IsLightTheme ? Color.Parse("#DADBDC") : Color.Parse("#2A2E33");
            searchBarTextColor = themeService.IsLightTheme ? Color.Parse("#000000") : Color.Parse("#FFFFFF");
            searchBarHoverColor = themeService.IsLightTheme ? Color.Parse("#D0D1D2") : Color.Parse("#373737");
            comboboxHoverColor = themeService.IsLightTheme ? Color.Parse("#C5C7C9") : Color.Parse("#202326");
            comboboxBackgroundColor = themeService.IsLightTheme ? Color.Parse("#E3E5E8") : Color.Parse("#202225");
        }
        private void AllCodesUpdated(object sender, EventArgs e) 
        {
            AllCodes = database.AllCodes;
            VisChange();
        } 

        private void VisChange(string text = "You don't have any notes :(")
        {
            if(AllCodes.Count == 0)
            {
                //labelVis = Visibility.Visible;
                labelVis = true;
                //codesVis = Visibility.Hidden;
                codesVis = false;
                emptyLabel = text;
            }
            else
            {
                
                //labelVis = Visibility.Hidden;
                labelVis = false;
                //codesVis = Visibility.Visible;
                codesVis = true;
            }
        }

        private ObservableCollection<CodeBoxModel> _AllCodes;
        public ObservableCollection<CodeBoxModel> AllCodes
        {
            get => _AllCodes;
            set => this.RaiseAndSetIfChanged(ref _AllCodes, value);
        }
        public IList<string> AllLangs { get; set; }
        public IList<string> Sorts { get; set; }

        #region Color
        private Color _searchBarBackground;
        public Color searchBarBackground
        {
            get => _searchBarBackground;
            set =>this.RaiseAndSetIfChanged(ref _searchBarBackground, value);
            
        }

        private Color _searchBarHoverColor;
        public Color searchBarHoverColor
        {
            get => _searchBarHoverColor;
            set => this.RaiseAndSetIfChanged(ref _searchBarHoverColor, value);

        }

        private Color _searchBarTextColor;
        public Color searchBarTextColor
        {
            get => _searchBarTextColor;
            set =>  this.RaiseAndSetIfChanged(ref _searchBarTextColor, value);
            
        }

        private Color _comboboxHoverColor;
        public Color comboboxHoverColor
        {
            get => _comboboxHoverColor;
            set => this.RaiseAndSetIfChanged(ref _comboboxHoverColor, value);

        }

        private Color _comboboxBackgroundColor;
        public Color comboboxBackgroundColor
        {
            get => _comboboxBackgroundColor;
            set => this.RaiseAndSetIfChanged(ref _comboboxBackgroundColor, value);

        }
        #endregion

        private bool _labelVis;
        public bool labelVis
        {
            get => _labelVis;
            set => this.RaiseAndSetIfChanged(ref _labelVis, value);
        }
        private bool _codesVis;
        public bool codesVis
        {
            get => _codesVis;
            set => this.RaiseAndSetIfChanged(ref _codesVis, value);
        }
        public int sortby { get; set; }

        private string _emptyLabel;
        public string emptyLabel
        {
            get => _emptyLabel;
            set => this.RaiseAndSetIfChanged(ref _emptyLabel, value);
        }

        private bool _Theme;
        public bool Theme
        {
            get => _Theme;
            set => this.RaiseAndSetIfChanged(ref _Theme, value);
        }

        private bool _IsSearched;
        public bool IsSearched
        {
            get => _IsSearched;
            set => this.RaiseAndSetIfChanged(ref _IsSearched, value);

        }

        private string _CurrentSort1;
        public string CurrentSort1
        {
            get => _CurrentSort1;
            set
            {
                this.RaiseAndSetIfChanged(ref _CurrentSort1, value);
                Avalonia.Threading.Dispatcher.UIThread.Post(() =>
                { 
                    ComboBoxItemSelected(value);
                    database.sortBy = value;
                });
                /*
                 Application.Current.Dispatcher.Invoke(new Action(() => 
                { 
                    ComboBoxItemSelected(value);
                    database.sortBy = value;
                }));
                */
            }
        }

        private string _CurrentSort2;
        public string CurrentSort2
        {
            get => _CurrentSort2;
            set 
            {
                this.RaiseAndSetIfChanged(ref _CurrentSort2, value);
                Avalonia.Threading.Dispatcher.UIThread.Post(() => { ComboBoxItemSelected(value); });
                // Application.Current.Dispatcher.Invoke(new Action(() => { ComboBoxItemSelected(value); }));
            } 
        }

        private async void ComboBoxItemSelected(string kind) 
        {
            AllCodes = await database.OrderCode(kind);
            VisChange("Not found :(");
        } 

        public async void SearchCommand(string text)
        {
            IsSearched = false;
            if(text == "")
            {

                AllCodes = database.AllCodes;
                VisChange();
            }
            else
            {
                if (AllCodes.Count > 0)
                {
                    AllCodes = await database.SearchCode(text, "Home");
                    VisChange("Not found :(");
                }
            }
            IsSearched = true;
        }

        public void CodeSelectCommand(CodeBoxModel code) 
        {
            pageService.Page = new CodeViewModel(themeService, pageService, database, code);
        }
    }
}
