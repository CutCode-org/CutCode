using System.Collections.Generic;
using System.Linq;
using Avalonia.Media;
using CutCode.CrossPlatform.Helpers;
using CutCode.CrossPlatform.Models;
using Newtonsoft.Json;
using ReactiveUI;


namespace CutCode.CrossPlatform.ViewModels
{
    public class CodeCardViewModel : PageBaseViewModel
    {
        public CodeCardViewModel()
        {
            
        }
        
        public CodeCardViewModel(CodeModel code)
        {
            Title = code.Title;
            LastModificationTime = code.LastModificationTime;
            Language = Languages.LanguagesDict[code.Language];
            IsFavourtie = code.IsFavourite;
            SetDescription(code.Cells);
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


        protected override void OnLightThemeIsSet()
        {
            mainTextColor = Color.Parse("#0B0B13");
            LanguageColor = Color.Parse("#4D4D4D");
            CardColor = Color.Parse("#F2F3F5");
            CardColorHover = Color.Parse("#E1E1E1");
        }
        
        protected override void OnDarkThemeIsSet()
        {
            mainTextColor = Color.Parse("#E8E8E8");
            LanguageColor = Color.Parse("#94969A");
            CardColor = Color.Parse("#2F3136");
            CardColorHover = Color.Parse("#282A2E");
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

        private bool _isFavourtie;
        public bool IsFavourtie
        {
            get => _isFavourtie;
            set => this.RaiseAndSetIfChanged(ref _isFavourtie, value);
        }

        public string Language { get; set; }
        public long LastModificationTime { get; set; }
    }
}