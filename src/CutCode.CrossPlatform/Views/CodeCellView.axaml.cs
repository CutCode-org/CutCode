using System.Reactive.Disposables;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using AvaloniaEdit;
using CutCode.CrossPlatform.ViewModels;
using ReactiveUI;

namespace CutCode.CrossPlatform.Views
{
    public class CodeCellView : ReactiveUserControl<CodeCellViewModel>
    {
        private TextEditor TextEditor => this.FindControl<TextEditor>(nameof(TextEditor));

        public CodeCellView()
        {
            InitializeComponent();
            this.WhenActivated(d => { this.Bind(ViewModel, x => x.Code, x => x.TextEditor.Text).DisposeWith(d); });
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}