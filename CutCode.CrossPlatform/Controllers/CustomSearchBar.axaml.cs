using System;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Platform;
using Avalonia.Data;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Media;
using Avalonia.Rendering;
using Avalonia.Styling;
using CutCode.CrossPlatform.Interfaces;
using CutCode.CrossPlatform.ViewModels;
using MathNet.Numerics.Optimization.LineSearch;
using ReactiveUI;

namespace CutCode.CrossPlatform.Controllers
{
    public class CustomSearchBar : UserControl
    {
        private TextBox textBox;
        private ProgressBar progressBar;
        static CustomSearchBar()
        {
            BackgroundProperty.Changed.Subscribe(BackgroundPropertyChanged);
            ForegroundProperty.Changed.Subscribe(ForegroundPropertyChanged);
            OverlayBrushProperty.Changed.Subscribe(OverlayBrushPropertyChanged);
            TextProperty.Changed.Subscribe(TextPropertyChanged);
            PlaceHolderTextProperty.Changed.Subscribe(PlaceHolderTextPropertyChanged);
            IsSearchBusyProperty.Changed.Subscribe(IsSearchBusyPropertyChanged);
        }
        public CustomSearchBar()
        {
            InitializeComponent();
            this.GetControl("textBox", out textBox);
            this.GetControl("progressBar", out progressBar);

            textBox.DataContext = this;
            progressBar.IsVisible = false;
             
            ThemeService.Current.ThemeChanged += (sender, args) =>
            {
                var service = (ThemeService)sender!;
                textBox.CaretBrush = service.IsLightTheme == true ? Brushes.Black : Brushes.White;
            };
            
            this.WhenAnyValue(x => x.textBox.Text)
                .Throttle(TimeSpan.FromMilliseconds(400))
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(SearchActivate!);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        
        private string _oldText = "";
        private async void SearchActivate(string s)
        {
            if (string.IsNullOrWhiteSpace(s) || s == _oldText)
            {
                IsSearchBusy = false;
                return;
            }
            SearchCommand?.Execute(s);
            _oldText = s;
        }
        
        public new static readonly StyledProperty<Brush> BackgroundProperty =
            AvaloniaProperty.Register<Border, Brush>(nameof(Background));

        public new Brush Background
        {
            get => GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
        }

        private static void BackgroundPropertyChanged(AvaloniaPropertyChangedEventArgs<Brush> e)
        {
            if (e.Sender is CustomSearchBar ctrl)
            {
                ctrl.textBox.Background = e.NewValue.Value;
            }
        }
        
        public new static readonly StyledProperty<Brush> ForegroundProperty =
            AvaloniaProperty.Register<Border, Brush>(nameof(Foreground));

        public new Brush Foreground
        {
            get => GetValue(ForegroundProperty);
            set => SetValue(ForegroundProperty, value);
        }

        private static void ForegroundPropertyChanged(AvaloniaPropertyChangedEventArgs<Brush> e)
        {
            if (e.Sender is CustomSearchBar ctrl)
            {
                ctrl.textBox.Foreground = e.NewValue.Value;
            }
        }
        
        public new static readonly StyledProperty<Brush> OverlayBrushProperty =
            AvaloniaProperty.Register<Border, Brush>(nameof(OverlayBrush));

        public new Brush OverlayBrush
        {
            get => GetValue(OverlayBrushProperty);
            set => SetValue(OverlayBrushProperty, value);
        }

        private static void OverlayBrushPropertyChanged(AvaloniaPropertyChangedEventArgs<Brush> e)
        {
            if (e.Sender is CustomSearchBar ctrl)
            {
                ctrl.textBox.BorderBrush = e.NewValue.Value;
            }
        }
        
        public new static readonly StyledProperty<string> TextProperty =
            AvaloniaProperty.Register<TextBox, string>(nameof(TextProperty), defaultBindingMode: BindingMode.TwoWay);

        public new string Text
        {
            get => GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
        
        private static void TextPropertyChanged(AvaloniaPropertyChangedEventArgs<string> e)
        {
            if (e.Sender is CustomSearchBar ctrl)
            {
                ctrl.textBox.Text = e.NewValue.Value;
            }
        }
        
        public new static readonly StyledProperty<string> PlaceHolderTextProperty =
            AvaloniaProperty.Register<TextBox, string>(nameof(PlaceHolderText));

        public new string PlaceHolderText
        {
            get => GetValue(PlaceHolderTextProperty);
            set => SetValue(PlaceHolderTextProperty, value);
        }
        
        private static void PlaceHolderTextPropertyChanged(AvaloniaPropertyChangedEventArgs<string> e)
        {
            if (e.Sender is CustomSearchBar ctrl)
            {
                ctrl.textBox.Watermark = e.NewValue.Value;
            }
        }
        
        public new static readonly StyledProperty<ICommand> SearchCommandProperty =
            AvaloniaProperty.Register<TextBox, ICommand>(nameof(SearchCommand));
        public ICommand SearchCommand
        {
            get => GetValue(SearchCommandProperty);
            set => SetValue(SearchCommandProperty, value);
        }
        
        public new static readonly StyledProperty<bool> IsSearchBusyProperty =
            AvaloniaProperty.Register<ProgressBar, bool>(nameof(IsSearchBusy));

        public bool IsSearchBusy
        {
            get => GetValue(IsSearchBusyProperty);
            set => SetValue(IsSearchBusyProperty, value);
        }
        
        private static void IsSearchBusyPropertyChanged(AvaloniaPropertyChangedEventArgs<bool> e)
        {
            if (e.Sender is CustomSearchBar ctrl)
            {
                ctrl.progressBar.IsVisible = e.NewValue.Value;
            }
        }
    }
}