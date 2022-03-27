using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Avalonia.Media;
using AvaloniaEdit.TextMate.Grammars;
using CutCode.CrossPlatform.Helpers;
using CutCode.CrossPlatform.Models;
using CutCode.CrossPlatform.Views;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace CutCode.CrossPlatform.ViewModels;

public class AddViewModel : PageBaseViewModel, IRoutableViewModel
{
    private string _selectedLanguage;

    [Reactive] public ObservableCollection<Language> AllLanguages { get; set; }

    [Reactive] public Language SelectedLanguage2 { get; set; }

    public AddViewModel()
    {
        AllLangs = new ObservableCollection<string>
        {
            "All languages", "Python", "C++", "C#", "CSS", "Dart", "Golang", "Html", "Java",
            "Javascript", "Kotlin", "Php", "C", "Ruby", "Rust", "Sql", "Swift"
        };

        Cells = new ObservableCollection<CodeCellViewModel?>();
        IsCellEmpty = true;
        Cells.CollectionChanged += (sender, args) => { IsCellEmpty = Cells.Count == 0; };
    }

    public AddViewModel(IScreen screen)
    {
        HostScreen = screen;
        AllLangs = new ObservableCollection<string>
        {
            "All languages", "Python", "C++", "C#", "CSS", "Dart", "Golang", "Html", "Java",
            "Javascript", "Kotlin", "Php", "C", "Ruby", "Rust", "Sql", "Swift"
        };

        Cells = new ObservableCollection<CodeCellViewModel?>();
        IsCellEmpty = true;
        Cells.CollectionChanged += (sender, args) => { IsCellEmpty = Cells.Count == 0; };

        var reg = new RegistryOptions(ThemeName.Dark);

        AllLanguages = new ObservableCollection<Language>(reg.GetAvailableLanguages());
        SelectedLanguage2 = reg.GetLanguageByExtension(".cs");
        this.WhenAnyValue(x => x.SelectedLanguage2).WhereNotNull().Subscribe(GlobalEvents.LanguageSet);
    }

    public static AddViewModel Current { get; } = new();

    public ObservableCollection<CodeCellViewModel?> Cells { get; }
    public IList<string> AllLangs { get; set; }

    [Reactive] public bool IsCellEmpty { get; set; }

    [Reactive] public string Title { get; set; }

    [Reactive] public Color BackgroundColor { get; set; }

    [Reactive] public Color ComboBoxBackground { get; set; }

    [Reactive] public Color ComboBoxBackgroundOnHover { get; set; }

    [Reactive] public Color BarBackground { get; set; }

    [Reactive] public Color TextAreaBackground { get; set; }

    [Reactive] public Color TextAreaForeground { get; set; }

    [Reactive] public Color TextAreaOverlayBackground { get; set; }

    protected override void OnLightThemeIsSet()
    {
        BackgroundColor = Color.Parse("#FCFCFC");
        BarBackground = Color.Parse("#F6F6F6");

        TextAreaBackground = Color.Parse("#ECECEC");
        TextAreaForeground = Color.Parse("#000000");
        TextAreaOverlayBackground = Color.Parse("#E2E2E2");

        ComboBoxBackground = Color.Parse("#ECECEC");
        ComboBoxBackgroundOnHover = Color.Parse("#E2E2E2");
    }

    protected override void OnDarkThemeIsSet()
    {
        BackgroundColor = Color.Parse("#36393F");
        BarBackground = Color.Parse("#303338");

        TextAreaBackground = Color.Parse("#2A2E33");
        TextAreaForeground = Color.Parse("#FFFFFF");
        TextAreaOverlayBackground = Color.Parse("#24272B");

        ComboBoxBackground = Color.Parse("#2A2E33");
        ComboBoxBackgroundOnHover = Color.Parse("#24272B");
    }

    public async void AddCell()
    {
        Cells.Add(new CodeCellViewModel(this));
        // the problem is after this async block ends.
    }

    public async void Cancel()
    {
        Cells.Clear();
        GlobalEvents.CancelClicked();
    }

    public async void Save()
    {
        if (!string.IsNullOrEmpty(Title) &&
            Cells.Count > 0 &&
            !Cells.Select(x => x.Description).ToList().Any(string.IsNullOrEmpty) &&
            !Cells.Select(x => x.Document.Text).ToList().Any(string.IsNullOrEmpty))
        {
            var cellsList = Cells.Select(x =>
                new Dictionary<string, string>
                {
                    { "Description", x.Description },
                    { "Code", x.Document.Text }
                }).ToList();

            CodeModel codeModel = DataBase.AddCode(Title, cellsList, SelectedLanguage2.Id);
            GlobalEvents.ShowCodeModel(codeModel);
            Title = "";
            Cells.Clear();
            NotificationService.CreateNotification("Notification", "New code is successfully created", 3);
        }
        else
        {
            NotificationService.CreateNotification("Warning", "Please Fill the Empty fields", 2);
        }
    }

    public static void DeleteCell(AddViewModel vm, CodeCellViewModel cell)
    {
        vm.Cells.Remove(cell);
    }

    public async void LanguageChanged(string selectedLanguage)
    {
        _selectedLanguage = selectedLanguage;
    }

    public string? UrlPathSegment => Guid.NewGuid().ToString().Substring(0, 5);
    public IScreen HostScreen { get; }
}