using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media;
using CutCode.CrossPlatform.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace CutCode.CrossPlatform.ViewModels;

public class SettingsViewModel : PageBaseViewModel, IRoutableViewModel
{
    public SettingsViewModel(IScreen screen)
    {
        HostScreen = screen;
    }

    public SettingsViewModel()
    {
    }

    [Reactive] public Color BackgroundColor { get; set; }

    [Reactive] public Color MainTextColor { get; set; }

    [Reactive] public Color CardColor { get; set; }

    [Reactive] public Color BtnColor { get; set; }

    public ObservableCollection<DeveloperCardViewModel> developers { get; set; }

    public string? UrlPathSegment => Guid.NewGuid().ToString().Substring(0, 5);
    public IScreen HostScreen { get; }

    protected override void OnLoad()
    {
        developers = new ObservableCollection<DeveloperCardViewModel>
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
                "@sharped_net",
                "https://github.com/PieroCastillo",
                "https://twitter.com/sharped_net")
        };
    }

    protected override void OnLightThemeIsSet()
    {
        BackgroundColor = Color.Parse("#FCFCFC");
        MainTextColor = Color.Parse("#0B0B13");
        CardColor = Color.Parse("#F2F3F5");
        BtnColor = Color.Parse("#E5E6E8");
    }

    protected override void OnDarkThemeIsSet()
    {
        BackgroundColor = Color.Parse("#36393F");
        MainTextColor = Color.Parse("#94969A");
        CardColor = Color.Parse("#2F3136");
        BtnColor = Color.Parse("#27282C");
    }

    public async void SyncCommand(string sync)
    {
        string message = "";
        if (sync == "Import")
        {
            OpenFileDialog fileDialog = new();
            fileDialog.AllowMultiple = false;
            fileDialog.Directory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var fileExt = new List<FileDialogFilter>
            {
                new()
            };
            fileExt[0].Extensions = new List<string> { "whl" };

            fileDialog.Filters = fileExt;
            if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopLifetime)
            {
                string[]? dialogResult = await fileDialog.ShowAsync(desktopLifetime.MainWindow);

                if (!string.IsNullOrEmpty(dialogResult?[0]))
                {
                    Notification processing =
                        NotificationService.CreateNotification("Notification", "Processing...", 100);
                    message = await DataBase.ImportData(dialogResult[0]);
                    NotificationService.CreateNotification("Notification", message, 4);
                    NotificationService.CloseNotification(processing);
                    return;
                }
            }
        }
        else
        {
            SaveFileDialog fileDialog = new();
            fileDialog.Directory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var fileExt = new List<FileDialogFilter>
            {
                new()
            };
            fileExt[0].Extensions = new List<string> { "whl" };
            fileDialog.InitialFileName = $"Export_CutCode_{DateTime.Now.ToString("yyyyMMddhhmmss")}.whl";
            fileDialog.Filters = fileExt;
            if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopLifetime)
            {
                string? dialogResult = await fileDialog.ShowAsync(desktopLifetime.MainWindow);
                if (!string.IsNullOrEmpty(dialogResult)) message = DataBase.ExportData(dialogResult);
            }
        }

        if (!string.IsNullOrEmpty(message)) NotificationService.CreateNotification("Notification", message, 4);
    }
}