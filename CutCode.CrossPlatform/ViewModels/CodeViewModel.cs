using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia;
using Avalonia.Input.Platform;
using Avalonia.Media;
using CutCode.CrossPlatform.Interfaces;
using CutCode.CrossPlatform.Models;
using CutCode.DataBase;
using Newtonsoft.Json;
using ReactiveUI;

namespace CutCode.CrossPlatform.ViewModels
{
    public class CodeViewModel : PageBaseViewModel
    {
        private IDataBase Database => DataBase;
        public CodeModel Code;
        
        public ObservableCollection<CodeCellViewModel?> Cells { get; }
        
        public CodeViewModel(CodeModel code)
        {
            Code = code;
            Title = Code.Title;
            Language = code.Language;

            IsEditEnabled = false;
            
            var cellsDict = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(code.Cells);
            Cells = new ObservableCollection<CodeCellViewModel?>();
            CellsToViewModel(cellsDict);
            
            IsCellEmpty = Cells.Count == 0;
            Cells.CollectionChanged += (sender, args) =>
            {
                IsCellEmpty = Cells.Count == 0;
                IsEditEnabled = IsCellEmpty;
            };
        }

        private void CellsToViewModel(List<Dictionary<string, string>>? cells)
        {
            Cells.Clear();
            if (cells == null) return;
            foreach (var cell in cells)
            {
                Cells.Add(new CodeCellViewModel(this,cell["Description"], cell["Code"]));
            }
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
            
            BtnColor = Color.Parse("#090909");
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
            
            BtnColor = Color.Parse("#F2F2F2");
        }
        
        private string _title;
        public string Title
        {
            get => _title;
            set => this.RaiseAndSetIfChanged(ref _title, value);
        }
        
        private bool _isCellEmpty;

        public bool IsCellEmpty
        {
            get => _isCellEmpty;
            set => this.RaiseAndSetIfChanged(ref _isCellEmpty, value);
        }

        private bool _isEditEnabled;

        public bool IsEditEnabled
        {
            get => _isEditEnabled;
            set => this.RaiseAndSetIfChanged(ref _isEditEnabled, value);
        }
        
        private Color _backgroundColor;

        public Color BackgroundColor
        {
            get => _backgroundColor;
            set => this.RaiseAndSetIfChanged(ref _backgroundColor, value);
        }
        
        private Color _btnColor;
        public Color BtnColor
        {
            get => _btnColor;
            set =>  this.RaiseAndSetIfChanged(ref _btnColor, value);
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
        
        private Color _isFavouriteColor;
        public Color IsFavouriteColor
        {
            get => _isFavouriteColor;
            set => this.RaiseAndSetIfChanged(ref _isFavouriteColor, value);
        }
        
        private string _language;
        public string Language
        {
            get => _language;
            set => this.RaiseAndSetIfChanged(ref _language, value);
        }

        public async void AddCell()
        {
            Cells.Add(new CodeCellViewModel(AddViewModel.Current));
        }

        public async void Cancel()
        {
            
        }

        public async void Save()
        {
            
        }

        public async void EditCommand()
        {
            
        }

        public async void FavouriteCommand()
        {
            
        }

        public async void DeleteCode()
        {
            
        }

        public async void Share()
        {
            
        }

        public static void DeleteCell(CodeViewModel vm, CodeCellViewModel cell)
        {
            vm.Cells.Remove(cell);
        }
    }
}
