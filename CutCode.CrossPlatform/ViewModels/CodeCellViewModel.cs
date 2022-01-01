using System.Diagnostics;
using System.Dynamic;
using Avalonia.Media;
using ReactiveUI;

namespace CutCode.CrossPlatform.ViewModels
{
    public class CodeCellViewModel : PageBaseViewModel
    {
        private readonly AddViewModel AddViewModelInstance;
        private readonly CodeViewModel CodeViewModelInstance;
        
        public CodeCellViewModel(AddViewModel viewModelInstance)
        {  // Creating new one
            AddViewModelInstance = viewModelInstance;
            IsEditable = true;
            IsMoreClickable = false;
            Code = "";
        }
        
        public CodeCellViewModel(CodeViewModel viewModelInstance, string description, string code)
        { // if we are fetching from database
            CodeViewModelInstance = viewModelInstance;
            Description = description;
            Code = code;
            
            IsEditable = false;
            IsMoreClickable = true;
        }
        
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
        
        private bool _isMoreClickable;
        public bool IsMoreClickable
        {
            get => _isMoreClickable;
            set => this.RaiseAndSetIfChanged(ref _isMoreClickable, value);
        }
        
        protected override void OnLightThemeIsSet()
        {
            Background = Color.Parse("#E3E5E8");
            TextColor = Color.Parse("#000000");
            BtnColor = Color.Parse("#090909");
        }

        protected override void OnDarkThemeIsSet()
        {
            Background = Color.Parse("#202225");
            TextColor = Color.Parse("#FFFFFF");
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
            get => _btnColor;
            set =>  this.RaiseAndSetIfChanged(ref _btnColor, value);
        }
        
        public async void DeleteCell(CodeCellViewModel cell)
        {
            if(AddViewModelInstance != null) AddViewModel.DeleteCell(AddViewModelInstance, cell);
            else CodeViewModel.DeleteCell(CodeViewModelInstance, cell);
        }
    }
}