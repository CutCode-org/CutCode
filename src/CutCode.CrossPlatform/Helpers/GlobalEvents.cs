using System;
using AvaloniaEdit.TextMate.Grammars;
using CutCode.CrossPlatform.Models;

namespace CutCode.CrossPlatform.Helpers;

public static class GlobalEvents
{
    public static event EventHandler<CodeModel> OnShowCodeModel;

    public static void ShowCodeModel(CodeModel code)
    {
        OnShowCodeModel?.Invoke(null, code);
    }

    public static event EventHandler OnCancelClicked;

    public static void CancelClicked()
    {
        OnCancelClicked?.Invoke(null, null!);
    }

    public static event EventHandler<Language> OnLanguageSet;

    public static void LanguageSet(Language language)
    {
        OnLanguageSet?.Invoke(null, language);
    }
}