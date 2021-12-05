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
            AllCodes = DataBase.AllCodes;
            DataBase.AllCodesUpdated += AllCodesUpdated;

            sortby = DataBase.sortBy == "Date" ? 0 : 1;

            IsSearchBusy = false;

            AllLangs = new ObservableCollection<string>()
            {
            "All languages", "Python", "C++", "C#", "CSS", "Dart", "Golang", "Html", "Java",
            "Javascript", "Kotlin", "Php", "C", "Ruby", "Rust","Sql", "Swift"
            };
            Sorts = new ObservableCollection<string>() { "Date", "Alphabet" };

            VisChange();
        }

        protected override void OnLightThemeIsSet()
        {
            backgroundColor =  Color.Parse("#FCFCFC");
            searchBarBackground = Color.Parse("#ECECEC");
            SearchBarOnHoverColor = Color.Parse("#E2E2E2");
            searchBarTextColor = Color.Parse("#000000");
            comboboxHoverColor = Color.Parse("#C5C7C9");
            comboboxBackgroundColor = Color.Parse("#E3E5E8");
        }

        protected override void OnDarkThemeIsSet()
        {
            backgroundColor =  Color.Parse("#36393F");
            searchBarBackground = Color.Parse("#2A2E33");
            SearchBarOnHoverColor = Color.Parse("#24272B");
            searchBarTextColor = Color.Parse("#FFFFFF");
            comboboxHoverColor = Color.Parse("#202326");
            comboboxBackgroundColor = Color.Parse("#202225");       
        }
        
        private void AllCodesUpdated(object sender, EventArgs e) 
        {
            AllCodes = DataBase.AllCodes;
            VisChange();
        } 

        private void VisChange(string text = "You don't have any notes :(")
        {
            if(AllCodes.Count == 0)
            {
                labelVis = true;
                codesVis = false;
                emptyLabel = text;
            }
            else
            {
                
                labelVis = false;
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
        
        private Color _searchBarOnHoverColor;
        public Color SearchBarOnHoverColor
        {
            get => _searchBarOnHoverColor;
            set =>this.RaiseAndSetIfChanged(ref _searchBarOnHoverColor, value);
            
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

        private string _CurrentSort1;
        public string CurrentSort1
        {
            get => _CurrentSort1;
            set
            {
                this.RaiseAndSetIfChanged(ref _CurrentSort1, value);
                Dispatcher.UIThread.Post(() =>
                { 
                    ComboBoxItemSelected(value);
                    DataBase.sortBy = value;
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
                Dispatcher.UIThread.Post(() => { ComboBoxItemSelected(value); });
                // Application.Current.Dispatcher.Invoke(new Action(() => { ComboBoxItemSelected(value); }));
            } 
        }

        private async void ComboBoxItemSelected(string kind) 
        {
            AllCodes = await DataBase.OrderCode(kind);
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
            IsSearchBusy = true;
            await Task.Delay(TimeSpan.FromSeconds(3));
            IsSearchBusy = false;
            /*
            IsSearched = false;
            if(text == "")
            {

                AllCodes = DataBase.AllCodes;
                VisChange();
            }
            else
            {
                if (AllCodes.Count > 0)
                {
                    AllCodes = await DataBase.SearchCode(text, "Home");
                    VisChange("Not found :(");
                }
            }
            IsSearched = true;
            */
        }
        
        private Color _backgroundColor;

        public Color backgroundColor
        {
            get => _backgroundColor;
            set => this.RaiseAndSetIfChanged(ref _backgroundColor, value);
        }
    }
}
