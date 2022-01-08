using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Avalonia;
using Avalonia.Input.Platform;
using Avalonia.Media;
using CutCode.CrossPlatform.Helpers;
using CutCode.CrossPlatform.Interfaces;
using CutCode.CrossPlatform.Models;
using CutCode.CrossPlatform.Views;
using CutCode.CrossPlatform.DataBase;
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
            for(int i = 0; i < Cells.Count; i++)
            {
                Cells[i].IsEditable = false;
            }
            
            IsCellEmpty = Cells.Count == 0;
            Cells.CollectionChanged += (sender, args) =>
            {
                IsCellEmpty = Cells.Count == 0;
                if (IsCellEmpty && !IsEditEnabled)
                {
                    IsEditEnabled = IsCellEmpty;
                    for(int i = 0; i < Cells.Count; i++)
                    {
                        Cells[i].IsEditable = IsEditEnabled;
                    }
                }
                    
            };
            
            if(ThemeService.IsLightTheme) OnLightThemeIsSet();
            else OnDarkThemeIsSet();
            
            ThemeService.Current.ThemeChanged += (sender, args) =>
            {
                if(ThemeService.IsLightTheme) OnLightThemeIsSet();
                else OnDarkThemeIsSet();
            };
            
            IsFavouritePath = code.IsFavourite ? IconPaths.StarFull : IconPaths.Star;
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

        private void OnLightThemeIsSet()
        {
            BackgroundColor =  Color.Parse("#FCFCFC");
            BarBackground =  Color.Parse("#F6F6F6");
            
            TextAreaBackground = Color.Parse("#ECECEC");
            TextAreaForeground = Color.Parse("#000000");
            TextAreaOverlayBackground = Color.Parse("#E2E2E2");
            
            ComboBoxBackground = Color.Parse("#ECECEC");
            ComboBoxBackgroundOnHover = Color.Parse("#E2E2E2");
            
            BtnColor = Color.Parse("#090909");
            IsFavouriteColor = Code.IsFavourite ? Color.Parse("#F7A000") : Color.Parse("#4D4D4D");
        }

        private void OnDarkThemeIsSet()
        {
            BackgroundColor =  Color.Parse("#36393F");
            BarBackground =  Color.Parse("#303338");
            
            TextAreaBackground = Color.Parse("#2A2E33");
            TextAreaForeground = Color.Parse("#FFFFFF");
            TextAreaOverlayBackground = Color.Parse("#24272B");
            
            ComboBoxBackground = Color.Parse("#2A2E33");
            ComboBoxBackgroundOnHover = Color.Parse("#24272B");
            
            BtnColor = Color.Parse("#F2F2F2");
            IsFavouriteColor = Code.IsFavourite ? Color.Parse("#F7A000") : Color.Parse("#94969A");
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
        
        private string _isFavouritePath;
        public string IsFavouritePath
        {
            get => _isFavouritePath;
            set => this.RaiseAndSetIfChanged(ref _isFavouritePath, value);
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
            PageService.Current.ExternalPage = new HomeView();
        }

        public async void Save()
        {
            if (Cells.Count > 0 &&
                !Cells.Select(x => x.Description).ToList().Any(string.IsNullOrEmpty) &&
                !Cells.Select(x => x.Code).ToList().Any(string.IsNullOrEmpty))
            {
                var cellsList = Cells.Select(x => 
                    new Dictionary<string, string>()
                    {
                        {"Description", x.Description},
                        {"Code", x.Code}
                    }).ToList();
            
                var editedCode = new CodeModel(Title, cellsList, Language,
                    new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds(), Code.IsFavourite);
                editedCode.SetId(Code.Id);

                if (Database.EditCode(editedCode))
                {
                    IsEditEnabled = false;
                    for(int i = 0; i < Cells.Count; i++)
                    {
                        Cells[i].IsEditable = false;
                    }
                }
                else
                {
                    NotificationManager.CreateNotification("Error", "Error, Unable to save the changes", 5);
                }
            }
            else
            {
                NotificationManager.CreateNotification("Warning", "Please Fill the Empty fields", 2);
            }
        }

        public async void EditCommand()
        {
            IsEditEnabled = true;
            for(int i = 0; i < Cells.Count; i++)
            {
                Cells[i].IsEditable = true;
            }
        }

        public async void FavouriteCommand()
        {
            var favUpdate = DataBaseManager.Current.FavModify(Code);
            if (favUpdate)
            {
                Code.IsFavourite = !Code.IsFavourite;
                IsFavouritePath = Code.IsFavourite ? IconPaths.StarFull : IconPaths.Star;
            
                if(ThemeService.IsLightTheme) IsFavouriteColor = Code.IsFavourite ? Color.Parse("#F7A000") : Color.Parse("#4D4D4D");
                else IsFavouriteColor = Code.IsFavourite ? Color.Parse("#F7A000") : Color.Parse("#94969A");
            }
            else
            {
                NotificationManager.Current.CreateNotification("Error", "Error, Unable to save the changes!", 3);
            }
            
        }

        public async void DeleteCode()
        {
            var delete = DataBaseManager.Current.DelCode(Code);
            if (delete)
            {
                PageService.Current.ExternalPage = new HomeView();
            }
            else
            {
                // do notification
            }
            // if it wasn't deleted, we will show notificaiton
        }

        public async void Share()
        {
            // will be implemented later
        }

        public static void DeleteCell(CodeViewModel vm, CodeCellViewModel cell)
        {
            if(vm.IsEditEnabled) vm.Cells.Remove(cell);
        }
    }
}
