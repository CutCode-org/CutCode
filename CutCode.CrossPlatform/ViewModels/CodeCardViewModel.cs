using System.Collections.Generic;
using System.Linq;
using Avalonia.Media;
using CutCode.CrossPlatform.Helpers;
using CutCode.CrossPlatform.Interfaces;
using CutCode.CrossPlatform.Models;
using CutCode.CrossPlatform.Views;
using CutCode.DataBase;
using Newtonsoft.Json;
using ReactiveUI;


namespace CutCode.CrossPlatform.ViewModels
{
    public class CodeCardViewModel : ViewModelBase
    {
        public CodeModel Code;
        public CodeCardViewModel(CodeModel code)
        {
            Code = code;
            Title = code.Title;
            LastModificationTime = code.LastModificationTime;
            Language = Languages.LanguagesDict[code.Language];
            IsFavouritePath = code.IsFavourite ? IconPaths.StarFull : IconPaths.Star;
            FavouriteText = Code.IsFavourite ? "Remove from favourite" : "Add to favourite";
            
            if(ThemeService.Current.IsLightTheme) OnLightThemeIsSet();
            else OnDarkThemeIsSet();
            
            ThemeService.Current.ThemeChanged += (sender, args) =>
            {
                if(ThemeService.Current.IsLightTheme) OnLightThemeIsSet();
                else OnDarkThemeIsSet();
            };
            
            SetDescription(code.Cells);
            IsPopupOpen = false;
            
            DataBaseManager.Current.AllCodesUpdated += (sender, args) =>
            {
                if (DataBaseManager.Current.AllCodes.Count > 0)
                {
                    var currentCode = DataBaseManager.Current.AllCodes.Find(c => c.Id == Code.Id);
                    if (currentCode is not null)
                    {
                        FavouriteText = currentCode.IsFavourite ? "Remove from favourite" : "Add to favourite";
                        if (ThemeService.Current.IsLightTheme) IsFavouriteColor = currentCode.IsFavourite ? Color.Parse("#F7A000") : Color.Parse("#4D4D4D");
                        else IsFavouriteColor = currentCode.IsFavourite ? Color.Parse("#F7A000") : Color.Parse("#94969A");
                        IsFavouritePath = currentCode.IsFavourite ? IconPaths.StarFull : IconPaths.Star;
                        Code = currentCode;
                    }
                }
            };
        }
        
        private void OnLightThemeIsSet()
        {
            mainTextColor = Color.Parse("#0B0B13");
            LanguageColor = Color.Parse("#4D4D4D");
            CardColor = Color.Parse("#F2F3F5");
            PopupColor = Color.Parse("#CECECE");
            CardColorHover = Color.Parse("#E1E1E1");
            IsFavouriteColor = Code.IsFavourite ? Color.Parse("#F7A000") : Color.Parse("#4D4D4D");
        }
        
        private void OnDarkThemeIsSet()
        {
            mainTextColor = Color.Parse("#E8E8E8");
            LanguageColor = Color.Parse("#94969A");
            CardColor = Color.Parse("#2F3136");
            PopupColor = Color.Parse("#26272B");
            CardColorHover = Color.Parse("#282A2E");
            IsFavouriteColor = Code.IsFavourite ? Color.Parse("#F7A000") : Color.Parse("#94969A");
        }

        private void SetDescription(string _cells)
        {
            var cells = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(_cells);
            var descriptions = cells.Select(x => x["Description"]);
            
            Description = "";
            var i = 0;
            foreach (var description in descriptions)
            {
                if (i == 0) Description += $"● {description}";
                else Description += $"\n● {description}";
                i++;
            }
        }
        
        private Color _mainTextColor;
        public Color mainTextColor
        {
            get => _mainTextColor;
            set => this.RaiseAndSetIfChanged(ref _mainTextColor, value);
        }
        
        private Color _cardColor;
        public Color CardColor
        {
            get => _cardColor;
            set => this.RaiseAndSetIfChanged(ref _cardColor, value);
        }
        
        private Color _popupColor;
        public Color PopupColor
        {
            get => _popupColor;
            set => this.RaiseAndSetIfChanged(ref _popupColor, value);
        }
        
        private Color _cardColorHover;
        public Color CardColorHover
        {
            get => _cardColorHover;
            set => this.RaiseAndSetIfChanged(ref _cardColorHover, value);
        }
        
        private Color _languageColor;
        public Color LanguageColor
        {
            get => _languageColor;
            set => this.RaiseAndSetIfChanged(ref _languageColor, value);
        }
        
        public int Id { get; set; }
        
        private string _title;
        public string Title
        {
            get => _title;
            set => this.RaiseAndSetIfChanged(ref _title, value);
        }
        
        private string _description;
        public string Description
        {
            get => _description;
            set => this.RaiseAndSetIfChanged(ref _description, value);
        }
        
        private string _favouriteText;
        public string FavouriteText
        {
            get => _favouriteText;
            set => this.RaiseAndSetIfChanged(ref _favouriteText, value);
        }

        private string _isFavouritePath;
        public string IsFavouritePath
        {
            get => _isFavouritePath;
            set => this.RaiseAndSetIfChanged(ref _isFavouritePath, value);
        }
        
        private bool _isPopupOpen;
        public bool IsPopupOpen
        {
            get => _isPopupOpen;
            set => this.RaiseAndSetIfChanged(ref _isPopupOpen, value);
        }
        
        private Color _isFavouriteColor;
        public Color IsFavouriteColor
        {
            get => _isFavouriteColor;
            set => this.RaiseAndSetIfChanged(ref _isFavouriteColor, value);
        }

        public string Language { get; set; }
        public long LastModificationTime { get; set; }

        public async void Clicked()
        {
            PageService.Current.ExternalPage = new CodeView()
            {
              DataContext  = new CodeViewModel(Code)
            };
        }


        public async void Favourite()
        {
            IsPopupOpen = false;
            Code.IsFavourite = !Code.IsFavourite;
            DataBaseManager.Current.FavModify(Code);
        }

        public async void Share()
        {
            // code sharing will be implemented later
        }
        
        public async void Delete()
        {
            IsPopupOpen = false;
            var delete = DataBaseManager.Current.DelCode(Code);
            if(!delete)  NotificationManager.Current.CreateNotification("Error", "Error, Unable to delete the code!", 3);
        }
    }
}