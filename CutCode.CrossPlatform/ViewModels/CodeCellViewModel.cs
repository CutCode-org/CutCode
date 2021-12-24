using System.Dynamic;
using Avalonia.Media;
using ReactiveUI;

namespace CutCode.CrossPlatform.ViewModels
{
    public class CodeCellViewModel : PageBaseViewModel
    {
        private readonly AddViewModel AddViewModelInstance;
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
        
        public CodeCellViewModel(AddViewModel addViewModelInstance)
        {
            // Creating new one
            AddViewModelInstance = addViewModelInstance;
            IsEditable = true;
        }
        
        public CodeCellViewModel(AddViewModel addViewModelInstance, string description, string code)
        { // if we are fetching from database
            AddViewModelInstance = addViewModelInstance;
            IsEditable = false;
            Description = description;
            Code = code;
            AddViewModelInstance = addViewModelInstance;
        }
        
        protected override void OnLightThemeIsSet()
        {
            Background = Color.Parse("#E3E5E8");
            TextColor = Color.Parse("#000000");
            ButtonHoverBackground = Color.Parse("#D0D1D2");
            BtnColor = Color.Parse("#090909");
        }

        protected override void OnDarkThemeIsSet()
        {
            Background = Color.Parse("#202225");
            TextColor = Color.Parse("#FFFFFF");
            ButtonHoverBackground = Color.Parse("#373737");
            BtnColor = Color.Parse("#F2F2F2");
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
        
        private Color _btnColor;

        public Color BtnColor
        {
            get => _textColor;
            set =>  this.RaiseAndSetIfChanged(ref _btnColor, value);
        }
        
        private Color _buttonHoverBackground;
        public Color ButtonHoverBackground
        {
            get => _buttonHoverBackground;
            set => this.RaiseAndSetIfChanged(ref _buttonHoverBackground, value);
        }
        
        public async void DeleteCell(CodeCellViewModel cell)
        {
            AddViewModel.DeleteCell(AddViewModelInstance, cell);
        }
    }
}