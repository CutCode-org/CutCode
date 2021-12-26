using Avalonia.Media;
using CutCode.CrossPlatform.Models;
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
            Language = code.Language;
            IsFavourtie = code.IsFavourite;
        }


        protected override void OnLightThemeIsSet()
        {
            mainTextColor = Color.Parse("#0B0B13");
            CardColor = Color.Parse("#F2F3F5");
        }
        
        protected override void OnDarkThemeIsSet()
        {
            mainTextColor = Color.Parse("#94969A");
            CardColor = Color.Parse("#2F3136");
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
        
        public int Id { get; set; }
        private string _title;
        public string Title
        {
            get => _title;
            set => this.RaiseAndSetIfChanged(ref _title, value);
        }

        private string _desc;
        public string Desc
        {
            get => _desc;
            set => this.RaiseAndSetIfChanged(ref _desc, value);
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