using System.Diagnostics;
using System.Reactive.Disposables;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using AvaloniaEdit;
using AvaloniaEdit.Indentation.CSharp;
using AvaloniaEdit.TextMate;
using AvaloniaEdit.TextMate.Grammars;
using CutCode.CrossPlatform.Helpers;
using CutCode.CrossPlatform.ViewModels;
using ReactiveUI;

namespace CutCode.CrossPlatform.Views
{
    public class CodeCellView : ReactiveUserControl<CodeCellViewModel>
    {
        private TextEditor TextEditor => this.FindControl<TextEditor>(nameof(TextEditor));
        private readonly TextMate.Installation _textMateInstallation;
        private RegistryOptions _registryOptions;

        public CodeCellView()
        {
            InitializeComponent();
            this.WhenActivated(d => { this.Bind(ViewModel, x => x.Code, x => x.TextEditor.Text).DisposeWith(d); });

            TextEditor.TextArea.IndentationStrategy = new CSharpIndentationStrategy(TextEditor.Options);

            _registryOptions = new RegistryOptions(ThemeName.Dark);
            _textMateInstallation = TextEditor.InstallTextMate(_registryOptions);

            Language csharpLanguage = _registryOptions.GetLanguageByExtension(".cs");

            string scopeName = _registryOptions.GetScopeByLanguageId(csharpLanguage.Id);
            _textMateInstallation.SetGrammar(scopeName);

            GlobalEvents.OnLanguageSet += GlobalEventsOnOnLanguageSet;
        }

        private void GlobalEventsOnOnLanguageSet(object? sender, Language e)
        {
            string scopeName = _registryOptions.GetScopeByLanguageId(e.Id);
            _textMateInstallation.SetGrammar(scopeName);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}