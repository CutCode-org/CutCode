using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Media;
using CutCode.CrossPlatform.Interfaces;
using CutCode.CrossPlatform.Models;
using CutCode.CrossPlatform.ViewModels;
using CutCode.CrossPlatform.Views;
using CutCode.DataBase;
using FluentAvalonia.UI.Controls;
using Newtonsoft.Json;
using ReactiveUI;

namespace CutCode.CrossPlatform.ViewModels
{
    public class AddViewModel : PageBaseViewModel
    {
        private static readonly AddViewModel _addViewModel = new();
        public static AddViewModel Current => _addViewModel;
        private IDataBase Database => DataBase;
        public ObservableCollection<CodeCellViewModel?> Cells { get; }
        public IList<string> AllLangs { get; set; }
        private bool _isCellEmpty;

        public bool IsCellEmpty
        {
            get => _isCellEmpty;
            set => this.RaiseAndSetIfChanged(ref _isCellEmpty, value);
        }
        
        public AddViewModel()
        {
            AllLangs = new ObservableCollection<string>()
            {
                "All languages", "Python", "C++", "C#", "CSS", "Dart", "Golang", "Html", "Java",
                "Javascript", "Kotlin", "Php", "C", "Ruby", "Rust","Sql", "Swift"
            };
            
            Cells = new ObservableCollection<CodeCellViewModel?>();
            IsCellEmpty = true;
            Cells.CollectionChanged += (sender, args) =>
            {
                IsCellEmpty = Cells.Count == 0;
                
            };
        }

        protected override void OnLightThemeIsSet()
        {
            BackgroundColor =  Color.Parse("#FCFCFC");
            BarBackground =  Color.Parse("#F6F6F6");
            
            TextAreaBackground = Color.Parse("#ECECEC");
            TextAreaForeground = Color.Parse("#000000");
            TextAreaOverlayBackground = Color.Parse("#E2E2E2");
            
            ComboBoxBackground = Color.Parse("#ECECEC");
            ComboBoxBackgroundOnHover = Color.Parse("#E2E2E2");
        }

        protected override void OnDarkThemeIsSet()
        {
            BackgroundColor =  Color.Parse("#36393F");
            BarBackground =  Color.Parse("#303338");
            
            TextAreaBackground = Color.Parse("#2A2E33");
            TextAreaForeground = Color.Parse("#FFFFFF");
            TextAreaOverlayBackground = Color.Parse("#24272B");
            
            ComboBoxBackground = Color.Parse("#2A2E33");
            ComboBoxBackgroundOnHover = Color.Parse("#24272B");
        }
        
        private string _title;
        public string Title 
        {
            get => _title;
            set => this.RaiseAndSetIfChanged(ref _title, value);
        }
        
        private Color _backgroundColor;

        public Color BackgroundColor
        {
            get => _backgroundColor;
            set => this.RaiseAndSetIfChanged(ref _backgroundColor, value);
        }
        
        private Color _comboBoxBackground;
        public Color ComboBoxBackground
        {
            get => _comboBoxBackground;
            set =>this.RaiseAndSetIfChanged(ref _comboBoxBackground, value);
            
        }
        
        private Color _comboBoxBackgroundOnHover;
        public Color ComboBoxBackgroundOnHover
        {
            get => _comboBoxBackgroundOnHover;
            set =>this.RaiseAndSetIfChanged(ref _comboBoxBackgroundOnHover, value);
            
        }
        
        private Color _barBackground;

        public Color BarBackground
        {
            get => _barBackground;
            set => this.RaiseAndSetIfChanged(ref _barBackground, value);
        }

        private Color _textAreaBackground;
        public Color TextAreaBackground
        {
            get => _textAreaBackground;
            set => this.RaiseAndSetIfChanged(ref _textAreaBackground, value);
        }

        private Color _textAreaForeground;
        public Color TextAreaForeground
        {
            get => _textAreaForeground;
            set => this.RaiseAndSetIfChanged(ref _textAreaForeground, value);
        }
        
        private Color _textAreaOverlayBackground;
        public Color TextAreaOverlayBackground
        {
            get => _textAreaOverlayBackground;
            set => this.RaiseAndSetIfChanged(ref _textAreaOverlayBackground, value);
        }

        public async void AddCell()
        {
            Cells.Add(new CodeCellViewModel(this));
            // the problem is after this async block ends.
        }

        public async void Cancel()
        {
            PageService.CurrentTabIndex = 0;
            Title = "";
            Cells.Clear();
        }

        public async void Save()
        {
            if (!string.IsNullOrEmpty(Title) &&
                Cells.Count > 0  &&
                !Cells.Select(x => x.Description).ToList().Any(string.IsNullOrEmpty) &&
                !Cells.Select(x => x.Code).ToList().Any(string.IsNullOrEmpty))
            {
                var cellsList = Cells.Select(x => 
                    new Dictionary<string, string>()
                    {
                        {"Description", x.Description},
                        {"Code", x.Code}
                    }).ToList();
            
                var codeModel = DataBase.AddCode(Title, cellsList, _selectedLanguage);
                var codeViewPage = new CodeView
                {
                    DataContext = new CodeViewModel(codeModel)
                };
                PageService.Current.ExternalPage = codeViewPage;
                Title = "";
                Cells.Clear();
            }
            else
            {
                // do notification thing to fill empty fields
            }
            
        }

        public static void DeleteCell(AddViewModel vm, CodeCellViewModel cell)
        {
            vm.Cells.Remove(cell);
        }

        private string _selectedLanguage;
        public async void LanguageChanged(string selectedLanguage) => _selectedLanguage = selectedLanguage;
    }
}
