using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.Serialization;
using Avalonia;
using Avalonia.Media;
using ReactiveUI;
using Avalonia.Media.Imaging;
using CutCode.CrossPlatform.Interfaces;
using CutCode.CrossPlatform.Models;
using CutCode.DataBase;

namespace CutCode.CrossPlatform.ViewModels
{
    public class SettingsViewModel : PageBaseViewModel
    {
        protected override void OnLoad()
        {
            developers = new ObservableCollection<DevAboutModel>()
            {
              new (
                  "avares://CutCode.CrossPlatform/Assets/Images/Developers/abdesol.png", 
                  "Abdella Solomon", 
                  "@abdesol", 
                  "https://github.com/Abdesol",
                  "https://twitter.com/AbdellaSolomon"),
              
              new (
                  "avares://CutCode.CrossPlatform/Assets/Images/Developers/piero.png", 
                  "Piero Castillo", 
                  $"@sharped_net", 
                  "https://github.com/PieroCastillo",
                  "https://twitter.com/sharped_net")
            };
        }
        protected override void OnLightThemeIsSet()
        {
            
            mainTextColor = Color.Parse("#0B0B13");
            cardColor = Color.Parse("#F2F3F5");
        }
        
        protected override void OnDarkThemeIsSet()
        {
            mainTextColor = Color.Parse("#94969A");
            cardColor = Color.Parse("#2F3136");
        }
        
        private Color _mainTextColor;
        public Color mainTextColor
        {
            get => _mainTextColor;
            set => this.RaiseAndSetIfChanged(ref _mainTextColor, value);
        }
        
        private Color _cardColor;
        public Color cardColor
        {
            get => _cardColor;
            set => this.RaiseAndSetIfChanged(ref _cardColor, value);
        }

        public ObservableCollection<DevAboutModel> developers { get; set; }
    }
}