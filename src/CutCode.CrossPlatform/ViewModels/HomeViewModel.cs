using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Media;
using CutCode.CrossPlatform.Models;
using CutCode.CrossPlatform.Services;
using ReactiveUI.Fody.Helpers;

namespace CutCode.CrossPlatform.ViewModels;

public class HomeViewModel : PageBaseViewModel
{
    private string _basicSort;

    private bool _isSearchCancelled = true;
    private string _languageSort;

    private string _searchText;

    public HomeViewModel()
    {
        AllCodes = new ObservableCollection<CodeCardViewModel>();
        CodeModeToViewModel(DataBase.AllCodes);

        DatabaseService.Current.AllCodesUpdated += AllCodesUpdated;

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

    [Reactive] public ObservableCollection<CodeCardViewModel> AllCodes { get; set; }
    public IList<string> Languages { get; set; }
    public IList<string> Sorts { get; set; }

    [Reactive] public bool EmptyLabelVisibility { get; set; }

    private int Sortby { get; }

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

    private void AllCodesUpdated(object sender, EventArgs e)
    {
        SearchCancelled();
    }

    private void CodeModeToViewModel(List<CodeModel> codes)
    {
        var tempCodes = new ObservableCollection<CodeCardViewModel>();
        foreach (CodeModel code in codes) tempCodes.Add(new CodeCardViewModel(code));
        AllCodes = tempCodes;
    }

    private void VisChange(string text = "You don't have any notes :(")
    {
        EmptyLabelVisibility = AllCodes.Count == 0;
        EmptyLabel = text;
    }

    public async void ComboBoxCommand(string sort)
    {
        var allcodes = AllCodes.Select(c => c.Code).ToList();
        if (sort is "Date" or "Alphabet")
        {
            _basicSort = sort;
            CodeModeToViewModel(await DataBase.OrderCode(_basicSort, allcodes));
        }
        else
        {
            _languageSort = sort;
            if (_isSearchCancelled)
            {
                var codes = await DataBase.OrderCode(_languageSort, DataBase.AllCodes);
                CodeModeToViewModel(await DataBase.OrderCode(_basicSort, codes));
            }
            else
            {
                var codes = await DataBase.SearchCode(_searchText, "Home");
                codes = await DataBase.OrderCode(_languageSort, codes);
                CodeModeToViewModel(await DataBase.OrderCode(_basicSort, codes));
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
        if (text != "" && AllCodes.Count > 0)
        {
            CodeModeToViewModel(await DataBase.SearchCode(text, "Home"));
            VisChange("Not found :(");
        }

        IsSearchBusy = false;
    }

    public async void SearchCancelled()
    {
        var codes = await DataBase.OrderCode(_languageSort, AllCodes.Select(c => c.Code).ToList());
        CodeModeToViewModel(await DataBase.OrderCode(_basicSort, codes));
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
}