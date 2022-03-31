using Avalonia.Media;
using AvaloniaEdit.Document;
using AvaloniaEdit.TextMate.Grammars;
using CutCode.CrossPlatform.Helpers;
using ReactiveUI.Fody.Helpers;

namespace CutCode.CrossPlatform.ViewModels;

public class CodeCellViewModel : PageBaseViewModel
{
    private readonly AddViewModel AddViewModelInstance;
    private readonly CodeViewModel CodeViewModelInstance;

    [Reactive] public TextDocument Document { get; set; } = new TextDocument();

    public CodeCellViewModel(AddViewModel viewModelInstance)
    {
        // Creating new one
        AddViewModelInstance = viewModelInstance;
        IsEditable = true;
        IsMoreClickable = false;
        Code = "";
    }

    public CodeCellViewModel(CodeViewModel viewModelInstance, string description, string code)
    {
        // if we are fetching from database
        CodeViewModelInstance = viewModelInstance;
        Description = description;
        Code = code;
        Document.Text = code;

        IsEditable = false;
        IsMoreClickable = true;

        GlobalEvents.OnViewRegistered += (sender, o) =>
        {
            GlobalEvents.LanguageSet(viewModelInstance._language);
        };
    }

    [Reactive] public string Description { get; set; }

    [Reactive] public string Code { get; set; }

    [Reactive] public bool IsEditable { get; set; }

    [Reactive] public bool IsMoreClickable { get; set; }

    [Reactive] public Color Background { get; set; }

    [Reactive] public Color TextColor { get; set; }

    [Reactive] public Color BtnColor { get; set; }

    protected override void OnLightThemeIsSet()
    {
        Background = Color.Parse("#E3E5E8");
        TextColor = Color.Parse("#000000");
        BtnColor = Color.Parse("#090909");
    }

    protected override void OnDarkThemeIsSet()
    {
        Background = Color.Parse("#202225");
        TextColor = Color.Parse("#FFFFFF");
        BtnColor = Color.Parse("#F2F2F2");
    }

    public async void DeleteCell(CodeCellViewModel cell)
    {
        if (AddViewModelInstance != null) AddViewModel.DeleteCell(AddViewModelInstance, cell);
        else CodeViewModel.DeleteCell(CodeViewModelInstance, cell);
    }
}