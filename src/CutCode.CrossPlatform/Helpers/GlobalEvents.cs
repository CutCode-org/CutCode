using System;
using CutCode.CrossPlatform.Models;

namespace CutCode.CrossPlatform.Helpers;

public static class GlobalEvents
{
    public static event EventHandler<CodeModel> OnShowCodeModel;

    public static void ShowCodeModel(CodeModel code)
    {
        OnShowCodeModel?.Invoke(null, code);
    }
}