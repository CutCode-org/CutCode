using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media;
using ReactiveUI;

namespace CutCode.CrossPlatform.ViewModels
{
    public class SettingsViewModel : PageBaseViewModel
    {
        protected override void OnLoad()
        {
            developers = new ObservableCollection<DeveloperCardViewModel>()
            {
                new(
                    "avares://CutCode.CrossPlatform/Assets/Images/Developers/abdesol.png",
                    "Abdella Solomon",
                    "C# and Python Programmer. Currently into AI/ML",
                    "@abdesol",
                    "https://github.com/Abdesol",
                    "https://twitter.com/AbdellaSolomon"),

                new(
                    "avares://CutCode.CrossPlatform/Assets/Images/Developers/piero.png",
                    "Piero Castillo",
                    "Student and C# programmer who lives in Lima, Peru",
                    $"@sharped_net",
                    "https://github.com/PieroCastillo",
                    "https://twitter.com/sharped_net")
            };
        }

        protected override void OnLightThemeIsSet()
        {
            backgroundColor = Color.Parse("#FCFCFC");
            mainTextColor = Color.Parse("#0B0B13");
            cardColor = Color.Parse("#F2F3F5");
            btnColor = Color.Parse("#E5E6E8");
        }

        protected override void OnDarkThemeIsSet()
        {
            backgroundColor = Color.Parse("#36393F");
            mainTextColor = Color.Parse("#94969A");
            cardColor = Color.Parse("#2F3136");
            btnColor = Color.Parse("#27282C");
        }

        private Color _backgroundColor;

        public Color backgroundColor
        {
            get => _backgroundColor;
            set => this.RaiseAndSetIfChanged(ref _backgroundColor, value);
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

        private Color _btnColor;

        public Color btnColor
        {
            get => _btnColor;
            set => this.RaiseAndSetIfChanged(ref _btnColor, value);
        }

        public ObservableCollection<DeveloperCardViewModel> developers { get; set; }

        public async void SyncCommand(string sync)
        {
            var message = "";
            if (sync == "Import")
            {
                var fileDialog = new OpenFileDialog();
                fileDialog.AllowMultiple = false;
                fileDialog.Directory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                var fileExt = new List<FileDialogFilter>()
                {
                    new FileDialogFilter()
                };
                fileExt[0].Extensions = new List<string>() { "whl" };

                fileDialog.Filters = fileExt;
                if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopLifetime)
                {
                    var dialogResult = await fileDialog.ShowAsync(desktopLifetime.MainWindow);
                    
                    if (!string.IsNullOrEmpty(dialogResult?[0]))
                    {
                        var processing = NotificationService.CreateNotification("Notification", "Processing...", 100);
                        message = await DataBase.ImportData(dialogResult[0]);
                        NotificationService.CreateNotification("Notification", message, 4);
                        NotificationService.CloseNotification(processing);
                        return;
                    }
                }
            }
            else
            {
                var fileDialog = new SaveFileDialog();
                fileDialog.Directory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                var fileExt = new List<FileDialogFilter>()
                {
                    new FileDialogFilter()
                };
                fileExt[0].Extensions = new List<string>() { "whl" };
                fileDialog.InitialFileName = $"Export_CutCode_{DateTime.Now.ToString("yyyyMMddhhmmss")}.whl";
                fileDialog.Filters = fileExt;
                if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopLifetime)
                {
                    var dialogResult = await fileDialog.ShowAsync(desktopLifetime.MainWindow);
                    if (!string.IsNullOrEmpty(dialogResult))
                    {
                        message = DataBase.ExportData(dialogResult);
                    }
                }
            }
            if(!string.IsNullOrEmpty(message)) NotificationService.CreateNotification("Notification", message, 4);
        }
    }
}