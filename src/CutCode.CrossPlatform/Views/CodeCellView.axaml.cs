using System.Diagnostics;
using System.Reactive.Disposables;
using Avalonia;
using Avalonia.Controls;
using Avalonia.LogicalTree;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using AvaloniaEdit;
using AvaloniaEdit.Document;
using AvaloniaEdit.Indentation.CSharp;
using AvaloniaEdit.TextMate;
using AvaloniaEdit.TextMate.Grammars;
using CutCode.CrossPlatform.Helpers;
using CutCode.CrossPlatform.Services;
using CutCode.CrossPlatform.ViewModels;
using ReactiveUI;
using TextMateSharp.Internal.Themes;

namespace CutCode.CrossPlatform.Views
{
    public class CodeCellView : ReactiveUserControl<CodeCellViewModel>
    {
        private TextEditor TextEditor => this.FindControl<TextEditor>(nameof(TextEditor));
        private TextMate.Installation _textMateInstallation;
        private RegistryOptions _registryOptions;

        public CodeCellView()
        {
            InitializeComponent();
            this.WhenActivated(d =>
            {
                // this.Bind(ViewModel, x => x.Code, x => x.TextEditor.Text).DisposeWith(d);
                this.Bind(ViewModel, x => x.Document, x => x.TextEditor.Document).DisposeWith(d);
            });

            TextEditor.TextArea.IndentationStrategy = new CSharpIndentationStrategy(TextEditor.Options);

            _registryOptions = new RegistryOptions(ThemeService.Current.Theme == ThemeType.Light ? ThemeName.LightPlus : ThemeName.DarkPlus);
            ThemeService.Current.ThemeChanged += (sender, args) =>
            {
                _registryOptions = new RegistryOptions(ThemeService.Current.Theme == ThemeType.Light ? ThemeName.LightPlus : ThemeName.DarkPlus);
                _textMateInstallation = TextEditor.InstallTextMate(_registryOptions);

                var scopeName = _registryOptions.GetScopeByLanguageId(_currentLanguage!.Id);
                _textMateInstallation.SetGrammar(scopeName);
            };

            _textMateInstallation = TextEditor.InstallTextMate(_registryOptions);

            _currentLanguage = _registryOptions.GetLanguageByExtension(".cs");

            var scopeName = _registryOptions.GetScopeByLanguageId(_currentLanguage.Id);
            _textMateInstallation.SetGrammar(scopeName);
            GlobalEvents.OnLanguageSet += GlobalEventsOnOnLanguageSet;
            GlobalEvents.ViewRegistered(this);
        }

        private Language _currentLanguage;
        private void GlobalEventsOnOnLanguageSet(object? sender, Language e)
        {
            _currentLanguage = e;
            string scopeName = _registryOptions.GetScopeByLanguageId(e.Id);
            _textMateInstallation.SetGrammar(scopeName);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}