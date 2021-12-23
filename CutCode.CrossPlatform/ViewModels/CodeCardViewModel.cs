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
        
        public CodeCardViewModel(CodeBoxModel code)
        {
            Title = code.Title;
            Desc = code.Desc;
            Code = code.Code;
            Timestamp = code.Timestamp;
            Language = code.Language;
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

        private bool _isFav;
        public bool IsFav
        {
            get => _isFav;
            set => this.RaiseAndSetIfChanged(ref _isFav, value);
        }

        public string Language { get; set; }

        private string _code;
        public string Code
        {
            get => _code;
            set => this.RaiseAndSetIfChanged(ref _code, value);
        }
        public long Timestamp { get; set; }
    }
}