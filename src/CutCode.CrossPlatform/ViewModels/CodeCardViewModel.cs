using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using Avalonia.Media;
using CutCode.CrossPlatform.Helpers;
using CutCode.CrossPlatform.Models;
using CutCode.CrossPlatform.Services;
using CutCode.CrossPlatform.Views;
using Newtonsoft.Json;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace CutCode.CrossPlatform.ViewModels;

public class CodeCardViewModel : ViewModelBase
{
    public CodeModel Code;

    public CodeCardViewModel(CodeModel code)
    {
        Initialise(code);
    }

    public void Initialise(CodeModel code)
    {
        Navigate = ReactiveCommand.Create(Clicked);
        Code = code;
        Title = code.Title;
        LastModificationTime = code.LastModificationTime;
        Language = code.Language;
        IsFavouritePath = code.IsFavourite ? IconPaths.StarFull : IconPaths.Star;
        FavouriteText = Code.IsFavourite ? "Remove from favourite" : "Add to favourite";

        if (ThemeService.Current.Theme == ThemeType.Light) OnLightThemeIsSet();
        else OnDarkThemeIsSet();

        ThemeService.Current.ThemeChanged += (sender, args) =>
        {
            if (ThemeService.Current.Theme == ThemeType.Light) OnLightThemeIsSet();
            else OnDarkThemeIsSet();
        };

        SetDescription(code.Cells);
        IsPopupOpen = false;

        DatabaseService.Current.AllCodesUpdated += (sender, args) =>
        {
            if (DatabaseService.Current.AllCodes.Count > 0)
            {
                CodeModel? currentCode = DatabaseService.Current.AllCodes.Find(c => c.Id == Code.Id);
                if (currentCode is not null)
                {
                    FavouriteText = currentCode.IsFavourite ? "Remove from favourite" : "Add to favourite";
                    if (ThemeService.Current.Theme == ThemeType.Light)
                        IsFavouriteColor =
                            currentCode.IsFavourite ? Color.Parse("#F7A000") : Color.Parse("#4D4D4D");
                    else
                        IsFavouriteColor =
                            currentCode.IsFavourite ? Color.Parse("#F7A000") : Color.Parse("#94969A");
                    IsFavouritePath = currentCode.IsFavourite ? IconPaths.StarFull : IconPaths.Star;
                    Code = currentCode;
                }
            }
        };
    }

    [Reactive] public Color MainTextColor { get; set; }

    [Reactive] public Color CardColor { get; set; }

    [Reactive] public Color PopupColor { get; set; }

    [Reactive] public Color CardColorHover { get; set; }

    [Reactive] public Color LanguageColor { get; set; }

    public int Id { get; set; }

    [Reactive] public string Title { get; set; }

    [Reactive] public string Description { get; set; }

    [Reactive] public string FavouriteText { get; set; }

    [Reactive] public string IsFavouritePath { get; set; }

    [Reactive] public bool IsPopupOpen { get; set; }

    [Reactive] public Color IsFavouriteColor { get; set; }

    public string Language { get; set; }
    public long LastModificationTime { get; set; }

    private void OnLightThemeIsSet()
    {
        MainTextColor = Color.Parse("#0B0B13");
        LanguageColor = Color.Parse("#4D4D4D");
        CardColor = Color.Parse("#F2F3F5");
        PopupColor = Color.Parse("#CECECE");
        CardColorHover = Color.Parse("#E1E1E1");
        IsFavouriteColor = Code.IsFavourite ? Color.Parse("#F7A000") : Color.Parse("#4D4D4D");
    }

    private void OnDarkThemeIsSet()
    {
        MainTextColor = Color.Parse("#E8E8E8");
        LanguageColor = Color.Parse("#94969A");
        CardColor = Color.Parse("#2F3136");
        PopupColor = Color.Parse("#26272B");
        CardColorHover = Color.Parse("#282A2E");
        IsFavouriteColor = Code.IsFavourite ? Color.Parse("#F7A000") : Color.Parse("#94969A");
    }

    private void SetDescription(string _cells)
    {
        var cells = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(_cells);
        var descriptions = cells.Select(x => x["Description"]);

        Description = "";
        int i = 0;
        foreach (string description in descriptions)
        {
            if (i == 0) Description += $"● {description}";
            else Description += $"\n● {description}";
            i++;
        }
    }

    public ReactiveCommand<Unit, Unit> Navigate { get; set; }

    public void Clicked()
    {
        GlobalEvents.ShowCodeModel(Code);
        // _router.Navigate.Execute(new CodeViewModel(Code, HostScreen));
        // PageService.Current.ExternalPage = new CodeView
        // {
        //     DataContext = new CodeViewModel(Code)
        // };
    }


    public async void Favourite()
    {
        IsPopupOpen = false;
        Code.IsFavourite = !Code.IsFavourite;
        DatabaseService.Current.FavModify(Code);
    }

    public async void Share()
    {
        // code sharing will be implemented later
    }

    public async void Delete()
    {
        IsPopupOpen = false;
        bool delete = DatabaseService.Current.DelCode(Code);
        if (!delete)
            NotificationService.Current.CreateNotification("Error", "Error, Unable to delete the code!", 3);
    }
}