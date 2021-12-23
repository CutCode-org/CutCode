using System.Dynamic;
using Avalonia.Media;
using ReactiveUI;

namespace CutCode.CrossPlatform.ViewModels
{
    public class CodeCellViewModel : PageBaseViewModel
    {
        
        private string _description;
        public string Description
        {
            get => _description;
            set => this.RaiseAndSetIfChanged(ref _description, value);
        } 
        
        private string _code;
        public string Code
        {
            get => _code;
            set => this.RaiseAndSetIfChanged(ref _code, value);
        }

        private bool _isEditable;

        public bool IsEditable
        {
            get => _isEditable;
            set => this.RaiseAndSetIfChanged(ref _isEditable, value);
        }

        public CodeCellViewModel()
        { // Creating new one
            IsEditable = true;
        }
        
        public CodeCellViewModel(string description, string code)
        { // if we are fetching from database
            IsEditable = false;
            Description = description;
            Code = code;
        }
        
        protected override void OnLightThemeIsSet()
        {
            Background = Color.Parse("#E3E5E8");
            TextColor = Color.Parse("#000000");
        }

        protected override void OnDarkThemeIsSet()
        {
            Background = Color.Parse("#202225");
            TextColor = Color.Parse("#FFFFFF");
        }
        
        private Color _background;
        public Color Background
        {
            get => _background;
            set => this.RaiseAndSetIfChanged(ref _background, value);
        }
        
        private Color _textColor;
        public Color TextColor
        {
            get => _textColor;
            set =>  this.RaiseAndSetIfChanged(ref _textColor, value);
            
        }
    }
}