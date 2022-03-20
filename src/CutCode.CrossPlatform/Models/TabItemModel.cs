using Avalonia.Media;
using CutCode.CrossPlatform.ViewModels;
using ReactiveUI;

namespace CutCode.CrossPlatform.Models
{
    public class TabItemModel : PageBaseViewModel
    {
        public string ToolTip { get; set; }
        public string Path { get; set; }
        private object _content;
        public object Content 
        { 
            get => _content; 
            set=> this.RaiseAndSetIfChanged(ref _content, value); 
        }

        protected override void OnLightThemeIsSet()
        {
            PathColor = Color.Parse("#0B0B13");
        }

        protected override void OnDarkThemeIsSet()
        {
            PathColor = Color.Parse("#94969A");
        }
        
        private Color _pathColor;
        public Color PathColor
        {
            get => _pathColor;
            set => this.RaiseAndSetIfChanged(ref _pathColor, value);
        }
    }
}