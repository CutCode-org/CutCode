using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Media;
using CutCode.CrossPlatform.Models;
using CutCode.CrossPlatform.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace CutCode.CrossPlatform.ViewModels;

public class FavoritesViewModel : PageBaseViewModel, IRoutableViewModel
{
    private string _basicSort;

    private bool _isSearchCancelled = true;
    private string _languageSort;

    private string _searchText;

    public FavoritesViewModel()
    {
        Inititalise();
    }

    public FavoritesViewModel(IScreen screen)
    {
        HostScreen = screen;
        Inititalise();
    }

    public void Inititalise()
    {
        AllFavouriteCodes = new ObservableCollection<CodeCardViewModel>();
        CodeModeToViewModel(DataBase.FavCodes);

        DatabaseService.Current.FavCodesUpdated += FavCodesUpdated;

        IsSearchBusy = false;

        Languages = new ObservableCollection<string>
        {
            "All languages", "Python", "C++", "C#", "CSS", "Dart", "Golang", "Html", "Java",
            "Javascript", "Kotlin", "Php", "C", "Ruby", "Rust", "Sql", "Swift"
        };
        Sorts = new ObservableCollection<string> { "Date", "Alphabet" };

        Sortby = DataBase.sortBy == "Date" ? 0 : 1;
        _basicSort = DataBase.sortBy;
        _languageSort = "All languages";

        VisChange();
    }

    [Reactive] public ObservableCollection<CodeCardViewModel> AllFavouriteCodes { get; set; }
    public IList<string> Languages { get; set; }
    public IList<string> Sorts { get; set; }

    [Reactive] public bool EmptyLabelVisibility { get; set; }

    private int Sortby { get; set; }

    [Reactive] public string EmptyLabel { get; set; }

    [Reactive] public bool IsSearchBusy { get; set; }

    protected override void OnLightThemeIsSet()
    {
        BackgroundColor = Color.Parse("#FCFCFC");
        SearchBarBackground = Color.Parse("#ECECEC");
        SearchBarOnHoverColor = Color.Parse("#E2E2E2");
        SearchBarTextColor = Color.Parse("#000000");
        ComboboxHoverColor = Color.Parse("#C5C7C9");
        ComboboxBackgroundColor = Color.Parse("#E3E5E8");
    }

    protected override void OnDarkThemeIsSet()
    {
        BackgroundColor = Color.Parse("#36393F");
        SearchBarBackground = Color.Parse("#2A2E33");
        SearchBarOnHoverColor = Color.Parse("#24272B");
        SearchBarTextColor = Color.Parse("#FFFFFF");
        ComboboxHoverColor = Color.Parse("#202326");
        ComboboxBackgroundColor = Color.Parse("#202225");
    }

    private void FavCodesUpdated(object sender, EventArgs e)
    {
        CodeModeToViewModel(DataBase.FavCodes);
        SearchCancelled();
    }

    private void CodeModeToViewModel(List<CodeModel> codes)
    {
        AllFavouriteCodes.Clear();
        foreach (CodeModel code in codes) AllFavouriteCodes.Add(new CodeCardViewModel(code));
    }

    private void VisChange(string text = "You don't have any notes :(")
    {
        EmptyLabelVisibility = AllFavouriteCodes.Count == 0;
        EmptyLabel = text;
    }

    public async void ComboBoxCommand(string sort)
    {
        var favcodes = AllFavouriteCodes.Select(c => c.Code).ToList();
        if (sort is "Date" or "Alphabet")
        {
            _basicSort = sort;
            CodeModeToViewModel(await DataBase.OrderCode(_basicSort, favcodes, "Favourite"));
        }
        else
        {
            _languageSort = sort;
            if (_isSearchCancelled)
            {
                var codes = await DataBase.OrderCode(_languageSort, DataBase.AllCodes);
                CodeModeToViewModel(await DataBase.OrderCode(_basicSort, codes, "Favourite"));
            }
            else
            {
                var codes = await DataBase.SearchCode(_searchText, "Favourite");
                codes = await DataBase.OrderCode(_languageSort, codes);
                CodeModeToViewModel(await DataBase.OrderCode(_basicSort, codes, "Favourite"));
            }
        }

        VisChange("Not found :(");
    }

    public async void SearchCommand(string text)
    {
        _searchText = text;
        _isSearchCancelled = false;
        if (string.IsNullOrEmpty(text)) return;
        IsSearchBusy = true;
        if (text != "" && AllFavouriteCodes.Count > 0)
        {
            CodeModeToViewModel(await DataBase.SearchCode(text, "Favourite"));
            VisChange("Not found :(");
        }

        IsSearchBusy = false;
    }

    public async void SearchCancelled()
    {
        var allCodes = AllFavouriteCodes.Select(c => c.Code).ToList();
        allCodes = await DataBase.OrderCode(_languageSort, allCodes, "Favourite");
        CodeModeToViewModel(allCodes);
        VisChange();
        _isSearchCancelled = true;
    }

    #region Color

    [Reactive] public Color BackgroundColor { get; set; }

    [Reactive] public Color SearchBarBackground { get; set; }

    [Reactive] public Color SearchBarOnHoverColor { get; set; }

    [Reactive] public Color SearchBarTextColor { get; set; }

    [Reactive] public Color ComboboxHoverColor { get; set; }

    [Reactive] public Color ComboboxBackgroundColor { get; set; }

    #endregion

    public string? UrlPathSegment => Guid.NewGuid().ToString().Substring(0, 5);
    public IScreen HostScreen { get; }
}