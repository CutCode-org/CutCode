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
using CutCode.CrossPlatform.DataBase;
using ReactiveUI;

namespace CutCode.CrossPlatform.ViewModels
{
    public class FavoritesViewModel : PageBaseViewModel
    {
        public FavoritesViewModel()
        {
            AllFavouriteCodes = new ObservableCollection<CodeCardViewModel>();
            CodeModeToViewModel(DataBase.FavCodes);

            DataBase.FavCodesUpdated += FavCodesUpdated;

            IsSearchBusy = false;

            Languages = new ObservableCollection<string>()
            {
                "All languages", "Python", "C++", "C#", "CSS", "Dart", "Golang", "Html", "Java",
                "Javascript", "Kotlin", "Php", "C", "Ruby", "Rust", "Sql", "Swift"
            };
            Sorts = new ObservableCollection<string>() { "Date", "Alphabet" };

            Sortby = DataBase.sortBy == "Date" ? 0 : 1;
            _basicSort = DataBase.sortBy;
            _languageSort = "All languages";
            
            VisChange();
        }

        protected override void OnLightThemeIsSet()
        {
            BackgroundColor =  Color.Parse("#FCFCFC");
            SearchBarBackground = Color.Parse("#ECECEC");
            SearchBarOnHoverColor = Color.Parse("#E2E2E2");
            SearchBarTextColor = Color.Parse("#000000");
            ComboboxHoverColor = Color.Parse("#C5C7C9");
            ComboboxBackgroundColor = Color.Parse("#E3E5E8");
        }

        protected override void OnDarkThemeIsSet()
        {
            BackgroundColor =  Color.Parse("#36393F");
            SearchBarBackground = Color.Parse("#2A2E33");
            SearchBarOnHoverColor = Color.Parse("#24272B");
            SearchBarTextColor = Color.Parse("#FFFFFF");
            ComboboxHoverColor = Color.Parse("#202326");
            ComboboxBackgroundColor = Color.Parse("#202225");    
        }
        
        private void FavCodesUpdated(object sender, EventArgs e) 
        {
            CodeModeToViewModel(DataBase.FavCodes);
            SearchCancelled();
        }

        private void CodeModeToViewModel(List<CodeModel> codes)
        {
            AllFavouriteCodes.Clear();
            foreach(var code in codes) AllFavouriteCodes.Add(new CodeCardViewModel(code));
        }

        private void VisChange(string text = "You don't have any notes :(")
        {
            EmptyLabelVisibility = AllFavouriteCodes.Count == 0;
            EmptyLabel = text;
        }

        private ObservableCollection<CodeCardViewModel> _allFavouriteCodes;
        public ObservableCollection<CodeCardViewModel> AllFavouriteCodes
        {
            get => _allFavouriteCodes;
            set => this.RaiseAndSetIfChanged(ref _allFavouriteCodes, value);
        }
        public IList<string> Languages { get; set; }
        public IList<string> Sorts { get; set; }

        #region Color
        private Color _backgroundColor;

        public Color BackgroundColor
        {
            get => _backgroundColor;
            set => this.RaiseAndSetIfChanged(ref _backgroundColor, value);
        }
        
        private Color _searchBarBackground;
        public Color SearchBarBackground
        {
            get => _searchBarBackground;
            set =>this.RaiseAndSetIfChanged(ref _searchBarBackground, value);
            
        }
        
        private Color _searchBarOnHoverColor;
        public Color SearchBarOnHoverColor
        {
            get => _searchBarOnHoverColor;
            set =>this.RaiseAndSetIfChanged(ref _searchBarOnHoverColor, value);
            
        }

        private Color _searchBarTextColor;
        public Color SearchBarTextColor
        {
            get => _searchBarTextColor;
            set =>  this.RaiseAndSetIfChanged(ref _searchBarTextColor, value);
            
        }

        private Color _comboboxHoverColor;
        public Color ComboboxHoverColor
        {
            get => _comboboxHoverColor;
            set => this.RaiseAndSetIfChanged(ref _comboboxHoverColor, value);

        }

        private Color _comboboxBackgroundColor;
        public Color ComboboxBackgroundColor
        {
            get => _comboboxBackgroundColor;
            set => this.RaiseAndSetIfChanged(ref _comboboxBackgroundColor, value);

        }
        #endregion

        private bool _emptyLabelVisibility;
        public bool EmptyLabelVisibility
        {
            get => _emptyLabelVisibility;
            set => this.RaiseAndSetIfChanged(ref _emptyLabelVisibility, value);
        }

        private int Sortby { get; set; }

        private string _emptyLabel;
        public string EmptyLabel
        {
            get => _emptyLabel;
            set => this.RaiseAndSetIfChanged(ref _emptyLabel, value);
        }

        private string _basicSort;
        private string _languageSort;
        public async void ComboBoxCommand(string sort)
        {
            var favcodes = AllFavouriteCodes.Select(c => c.Code).ToList();
            if (sort is "Date" or "Alphabet")
            {
                _basicSort = sort;
                CodeModeToViewModel(await  DataBase.OrderCode(_basicSort, favcodes, "Favourite"));
            }
            else
            {
                _languageSort = sort;
                var codes = await DataBase.OrderCode(_languageSort, favcodes, "Favourite");
                CodeModeToViewModel(await  DataBase.OrderCode(_basicSort, codes, "Favourite"));
            }
            VisChange("Not found :(");
        }

        private bool _isSearchBusy;
        public bool IsSearchBusy
        {
            get => _isSearchBusy;
            set => this.RaiseAndSetIfChanged(ref _isSearchBusy, value);

        }
        public async void SearchCommand(string text)
        {
            if (string.IsNullOrEmpty(text)) return;
            IsSearchBusy = true;
            if(text != "" && AllFavouriteCodes.Count > 0)
            {
                CodeModeToViewModel(await DataBase.SearchCode(text, "Favourite"));
                VisChange("Not found :(");
            }
            
            IsSearchBusy = false;
        }

        public async void SearchCancelled()
        {
            var allCodes = AllFavouriteCodes.Select(c => c.Code).ToList();
            allCodes = await DataBase.OrderCode(_languageSort, allCodes, "Favourite");
            CodeModeToViewModel(allCodes);
            VisChange();
        }
    }
}