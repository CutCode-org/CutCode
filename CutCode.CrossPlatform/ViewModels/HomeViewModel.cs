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
using CutCode.DataBase;
using ReactiveUI;

namespace CutCode.CrossPlatform.ViewModels
{
    public class HomeViewModel : PageBaseViewModel
    {
        public HomeViewModel()
        {
            AllCodes = new ObservableCollection<CodeCardViewModel>();
            CodeModeToViewModel(DataBase.AllCodes);
            
            DataBase.AllCodesUpdated += AllCodesUpdated;

            IsSearchBusy = false;

            Languages = new ObservableCollection<string>()
            {
            "All languages", "Python", "C++", "C#", "CSS", "Dart", "Golang", "Html", "Java",
            "Javascript", "Kotlin", "Php", "C", "Ruby", "Rust","Sql", "Swift"
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
        
        private void AllCodesUpdated(object sender, EventArgs e) 
        {
            SearchCancelled();
        }

        private void CodeModeToViewModel(List<CodeModel> codes)
        {
            var tempCodes = new ObservableCollection<CodeCardViewModel>();
            foreach(var code in codes) tempCodes.Add(new CodeCardViewModel(code));
            AllCodes = tempCodes;
        }

        private void VisChange(string text = "You don't have any notes :(")
        {
            EmptyLabelVisibility = AllCodes.Count == 0;
            EmptyLabel = text;
        }

        private ObservableCollection<CodeCardViewModel> _AllCodes;
        public ObservableCollection<CodeCardViewModel> AllCodes
        {
            get => _AllCodes;
            set => this.RaiseAndSetIfChanged(ref _AllCodes, value);
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
            var allcodes = AllCodes.Select(c => c.Code).ToList();
            if (sort is "Date" or "Alphabet")
            {
                _basicSort = sort;
                CodeModeToViewModel(await  DataBase.OrderCode(_basicSort, allcodes));
            }
            else
            {
                _languageSort = sort;
                var codes = await DataBase.OrderCode(_languageSort, allcodes);
                CodeModeToViewModel(await  DataBase.OrderCode(_basicSort, codes));
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
            if(text != "" && AllCodes.Count > 0)
            {
                CodeModeToViewModel(await DataBase.SearchCode(text, "Home"));
                VisChange("Not found :(");
            }
            
            IsSearchBusy = false;
        }

        public async void SearchCancelled()
        {
            var codes = await DataBase.OrderCode(_languageSort, AllCodes.Select(c => c.Code).ToList());
            CodeModeToViewModel(await  DataBase.OrderCode(_basicSort, codes));
            VisChange();
        }
    }
}
