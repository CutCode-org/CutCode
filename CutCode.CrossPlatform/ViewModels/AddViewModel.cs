using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Media;
using CutCode.CrossPlatform.Interfaces;
using CutCode.CrossPlatform.ViewModels;
using CutCode.DataBase;
using ReactiveUI;

namespace CutCode.CrossPlatform.ViewModels
{
    public class AddViewModel : PageBaseViewModel
    {
        private IDataBase Database => DataBase;
        public AddViewModel()
        {
            
        }

        protected override void OnLightThemeIsSet()
        {
            NameFieldBackground = Color.Parse("#DADBDC");
            NameFieldForeground = Color.Parse("#1A1A1A");
        }

        protected override void OnDarkThemeIsSet()
        {
            NameFieldBackground = Color.Parse("#2A2E33");
            NameFieldForeground = Color.Parse("#F7F7F7");
        }

        private Color _nameFieldBackground;
        public Color NameFieldBackground
        {
            get => _nameFieldBackground;
            set => this.RaiseAndSetIfChanged(ref _nameFieldBackground, value);
        }

        private Color _nameFieldForeground;
        public Color NameFieldForeground
        {
            get => _nameFieldForeground;
            set => this.RaiseAndSetIfChanged(ref _nameFieldForeground, value);
        }        
    }
}
