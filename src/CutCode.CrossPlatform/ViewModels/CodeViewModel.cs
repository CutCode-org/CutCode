using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Avalonia.Media;
using AvaloniaEdit.TextMate.Grammars;
using CutCode.CrossPlatform.Helpers;
using CutCode.CrossPlatform.Models;
using CutCode.CrossPlatform.Services;
using CutCode.CrossPlatform.Views;
using Newtonsoft.Json;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace CutCode.CrossPlatform.ViewModels;

public class CodeViewModel : PageBaseViewModel, IRoutableViewModel
{
    public Language? _language;
    public CodeModel Code;

    [Reactive] public ObservableCollection<Language> AllLanguages { get; set; }

    [Reactive] public Language SelectedLanguage { get; set; }

    public CodeViewModel(CodeModel code)
    {
        Initialise(code);
    }

    public CodeViewModel(CodeModel code, IScreen screen)
    {
        HostScreen = screen;
        Initialise(code);
    }

    public ObservableCollection<CodeCellViewModel?> Cells { get; set; }

    [Reactive] public string Title { get; set; }

    [Reactive] public bool IsCellEmpty { get; set; }

    [Reactive] public bool IsEditEnabled { get; set; }

    [Reactive] public Color BackgroundColor { get; set; }

    [Reactive] public Color BtnColor { get; set; }

    [Reactive] public Color ComboBoxBackground { get; set; }

    [Reactive] public Color ComboBoxBackgroundOnHover { get; set; }

    [Reactive] public Color BarBackground { get; set; }

    [Reactive] public Color TextAreaBackground { get; set; }

    [Reactive] public Color TextAreaForeground { get; set; }

    [Reactive] public Color TextAreaOverlayBackground { get; set; }

    [Reactive] public Color IsFavouriteColor { get; set; }

    [Reactive] public string IsFavouritePath { get; set; }

    [Reactive] public string Language { get; set; }

    public string? UrlPathSegment => Guid.NewGuid().ToString().Substring(0, 5);
    public IScreen HostScreen { get; }

    public void Initialise(CodeModel code)
    {
        Code = code;
        Title = Code.Title;

        RegistryOptions reg = new RegistryOptions(ThemeName.Dark);
        AllLanguages = new ObservableCollection<Language>(reg.GetAvailableLanguages());
        _language = reg.GetLanguageByExtension(code.Language);
        if (_language is null)
            UpdateToNewLanguages(code.Language, reg);
        SelectedLanguage = _language;
        Language = _language.ToString();
        IsEditEnabled = false;

        var cellsDict = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(code.Cells);
        Cells = new ObservableCollection<CodeCellViewModel?>();
        CellsToViewModel(cellsDict);
        for (int i = 0; i < Cells.Count; i++) Cells[i].IsEditable = false;

        IsCellEmpty = Cells.Count == 0;
        Cells.CollectionChanged += (sender, args) =>
        {
            IsCellEmpty = Cells.Count == 0;
            if (IsCellEmpty && !IsEditEnabled)
            {
                IsEditEnabled = IsCellEmpty;
                for (int i = 0; i < Cells.Count; i++) Cells[i].IsEditable = IsEditEnabled;
            }
        };

        if (ThemeService.Theme == ThemeType.Light) OnLightThemeIsSet();
        else OnDarkThemeIsSet();

        ThemeService.ThemeChanged += (sender, args) =>
        {
            if (ThemeService.Theme == ThemeType.Light) OnLightThemeIsSet();
            else OnDarkThemeIsSet();
        };

        IsFavouritePath = code.IsFavourite ? IconPaths.StarFull : IconPaths.Star;

        this.WhenAnyValue(x => x.SelectedLanguage).Subscribe(x =>
        {
            Language = x.ToString();
            _language = x;
        });
    }

    private void UpdateToNewLanguages(string language, RegistryOptions reg)
    {
        var all = reg.GetAvailableLanguages();
        _language = language switch
        {
            "Python" => reg.GetLanguageByExtension(".py"),
            "c++" => reg.GetLanguageByExtension(".cpp"),
            "C#" => reg.GetLanguageByExtension(".cs"),
            "CSS" => reg.GetLanguageByExtension(".css"),
            "Dart" => reg.GetLanguageByExtension(".dart"),
            "Golang" => reg.GetLanguageByExtension(".go"),
            "HTML" => reg.GetLanguageByExtension(".html"),
            "Java" => reg.GetLanguageByExtension(".java"),
            "Javascript" => reg.GetLanguageByExtension(".js"),
            "Kotlin" => reg.GetLanguageByExtension(".java"),
            "Php" => reg.GetLanguageByExtension(".php"),
            "C" => reg.GetLanguageByExtension(".c"),
            "Ruby" => reg.GetLanguageByExtension(".rb"),
            "Rust" => reg.GetLanguageByExtension(".rs"),
            "Sql" => reg.GetLanguageByExtension(".sql"),
            "Swift" => reg.GetLanguageByExtension(".swift"),
            _ => _language
        };
    }

    private void CellsToViewModel(List<Dictionary<string, string>>? cells)
    {
        Cells.Clear();
        if (cells == null) return;
        foreach (var cell in cells) Cells.Add(new CodeCellViewModel(this, cell["Description"], cell["Code"]));
    }

    private void OnLightThemeIsSet()
    {
        BackgroundColor = Color.Parse("#FCFCFC");
        BarBackground = Color.Parse("#F6F6F6");

        TextAreaBackground = Color.Parse("#ECECEC");
        TextAreaForeground = Color.Parse("#000000");
        TextAreaOverlayBackground = Color.Parse("#E2E2E2");

        ComboBoxBackground = Color.Parse("#ECECEC");
        ComboBoxBackgroundOnHover = Color.Parse("#E2E2E2");

        BtnColor = Color.Parse("#090909");
        IsFavouriteColor = Code.IsFavourite ? Color.Parse("#F7A000") : Color.Parse("#4D4D4D");
    }

    private void OnDarkThemeIsSet()
    {
        BackgroundColor = Color.Parse("#36393F");
        BarBackground = Color.Parse("#303338");

        TextAreaBackground = Color.Parse("#2A2E33");
        TextAreaForeground = Color.Parse("#FFFFFF");
        TextAreaOverlayBackground = Color.Parse("#24272B");

        ComboBoxBackground = Color.Parse("#2A2E33");
        ComboBoxBackgroundOnHover = Color.Parse("#24272B");

        BtnColor = Color.Parse("#F2F2F2");
        IsFavouriteColor = Code.IsFavourite ? Color.Parse("#F7A000") : Color.Parse("#94969A");
    }

    public async void AddCell()
    {
        Cells.Add(new CodeCellViewModel(this));
    }

    public async void Cancel()
    {
        GlobalEvents.CancelClicked();
    }

    public async void Save()
    {
        StringBuilder? error = new();
        if (string.IsNullOrEmpty(Title))
            error.AppendLine("Title cannot be empty");
        if (Cells.Count > 0)
        {
            if (Cells.Select(x => x.Description).ToList().Any(string.IsNullOrEmpty))
                error.AppendLine("The Description for the cells cannot be empty");
            if (Cells.Select(x => x.Document.Text).ToList().Any(string.IsNullOrEmpty))
                error.AppendLine("The Code text cannot be empty");
        }
        
        if (error.Length == 0)
        {
            var cellsList = Cells.Select(x =>
                new Dictionary<string, string>
                {
                    { "Description", x.Description },
                    { "Code", x.Document.Text }
                }).ToList();

            CodeModel editedCode = new(Title, cellsList, _language.Extensions.First(),
                new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds(), Code.IsFavourite);
            editedCode.SetId(Code.Id);

            if (DataBase.EditCode(editedCode))
            {
                IsEditEnabled = false;
                for (int i = 0; i < Cells.Count; i++) Cells[i].IsEditable = false;
            }
            else
            {
                NotificationService.CreateNotification("Error", error.ToString(), 5);
            }
        }
        else
        {
            NotificationService.CreateNotification("Warning", "Please Fill the Empty fields", 2);
        }
    }

    public async void EditCommand()
    {
        IsEditEnabled = true;
        for (int i = 0; i < Cells.Count; i++) Cells[i].IsEditable = true;
    }

    public async void FavouriteCommand()
    {
        bool favUpdate = DatabaseService.Current.FavModify(Code);
        if (favUpdate)
        {
            Code.IsFavourite = !Code.IsFavourite;
            IsFavouritePath = Code.IsFavourite ? IconPaths.StarFull : IconPaths.Star;

            if (ThemeService.Current.Theme == ThemeType.Light)
                IsFavouriteColor = Code.IsFavourite ? Color.Parse("#F7A000") : Color.Parse("#4D4D4D");
            else IsFavouriteColor = Code.IsFavourite ? Color.Parse("#F7A000") : Color.Parse("#94969A");
        }
        else
        {
            NotificationService.CreateNotification("Error", "Error, Unable to save the changes!", 3);
        }
    }

    public async void DeleteCode()
    {
        bool delete = DatabaseService.Current.DelCode(Code);
        if (delete) GlobalEvents.CancelClicked();
        // if it wasn't deleted, we will show notificaiton
    }
}