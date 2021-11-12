using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using Avalonia;
using Avalonia.Media;
using ReactiveUI;
using Avalonia.Media.Imaging;
using CutCode.CrossPlatform.Interfaces;
using CutCode.DataBase;

namespace CutCode.CrossPlatform.ViewModels
{
    public class MainWindowViewModel : PageBaseViewModel
    {
        protected override void OnLoad()
        {

        }

        protected override void OnLightThemeIsSet()
        {
            windowsBtnColor = Color.Parse("#090909");
            backgroundColor = Color.Parse("#FCFCFC");
            titleBarColor = Color.Parse("#E3E5E8");
            SideBarColor = Color.Parse("#F2F3F5");
            mainTextColor = Color.Parse("#0B0B13");

            titlebarBtnsHoverColor = Color.Parse("#D0D1D2");
        }

        protected override void OnDarkThemeIsSet()
        {
            windowsBtnColor = Color.Parse("#F2F2F2");
            backgroundColor = Color.Parse("#36393F");
            titleBarColor = Color.Parse("#202225");
            SideBarColor = Color.Parse("#2A2E33");
            mainTextColor = Color.Parse("#94969A");

            titlebarBtnsHoverColor = Color.Parse("#373737");
        }

        #region Color
        private Color _windowsBtnColor;
        public Color windowsBtnColor
        {
            get => _windowsBtnColor;
            set => this.RaiseAndSetIfChanged(ref _windowsBtnColor, value);
        }
        
        private Color _backgroundColor;
        public Color backgroundColor
        {
            get => _backgroundColor;
            set => this.RaiseAndSetIfChanged(ref _backgroundColor, value);
        }

        private Color _titleBarColor;
        public Color titleBarColor
        {
            get => _titleBarColor;
            set => this.RaiseAndSetIfChanged(ref _titleBarColor, value);
        }

        private Color _sideBarColor;
        public Color SideBarColor
        {
            get => _sideBarColor;
            set => this.RaiseAndSetIfChanged(ref _sideBarColor, value);
        }

        private Color _mainTextColor;
        public Color mainTextColor
        {
            get => _mainTextColor;
            set => this.RaiseAndSetIfChanged(ref _mainTextColor, value);
        }

        private Color _titlebarBtnsHoverColor;
        public Color titlebarBtnsHoverColor
        {
            get => _titlebarBtnsHoverColor;
            set => this.RaiseAndSetIfChanged(ref _titlebarBtnsHoverColor, value);
        }
        #endregion

    }
}
