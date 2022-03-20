using System;
using System.Reactive.Linq;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using CutCode.CrossPlatform.Helpers;
using CutCode.CrossPlatform.Interfaces;
using CutCode.CrossPlatform.Services;
using ReactiveUI;

namespace CutCode.CrossPlatform.Controllers
{
    public class CustomSearchBar : UserControl
    {
        private TextBox textBox;
        private ProgressBar progressBar;
        private Button CloseButton;
        private Grid panel;
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
            this.GetControl("CloseButton", out CloseButton);
            this.GetControl("panel", out panel);

            textBox.DataContext = this;
            progressBar.IsVisible = false;
             
            CloseButton.Foreground = ThemeService.Current.Theme == ThemeType.Light ? Brushes.Black : Brushes.White;
            ThemeService.Current.ThemeChanged += (sender, args) =>
            {
                var service = (ThemeService)sender!;
                textBox.CaretBrush = ThemeService.Current.Theme == ThemeType.Light ? Brushes.Black : Brushes.White;
                CloseButton.Foreground = ThemeService.Current.Theme == ThemeType.Light ? Brushes.Black : Brushes.White;
            };
            
            this.WhenAnyValue(x => x.textBox.Text)
                .Throttle(TimeSpan.FromMilliseconds(700))
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(SearchActivate!);
            
            CloseButton.Click += CloseButtonOnClick;
        }

        private void CloseButtonOnClick(object? sender, RoutedEventArgs e)
        {
            textBox.Clear();
            Text = string.Empty;
            CloseButton.IsVisible = false;
            SearchCancelled?.Execute(null);
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
                CloseButton.IsVisible = false;
                SearchCancelled?.Execute(s);
                return;
            }
            CloseButton.IsVisible = true;
            _oldText = s;
            SearchCommand?.Execute(s);
        }
        
        public new static readonly StyledProperty<IBrush> BackgroundProperty =
            AvaloniaProperty.Register<Border, IBrush>(nameof(Background));

        public new IBrush Background
        {
            get => GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
        }

        private static void BackgroundPropertyChanged(AvaloniaPropertyChangedEventArgs<IBrush> e)
        {
            if (e.Sender is CustomSearchBar ctrl)
            {
                ctrl.textBox.Background = e.NewValue.Value;
                ctrl.panel.Background = e.NewValue.Value;
            }
        }
        
        public new static readonly StyledProperty<IBrush> ForegroundProperty =
            AvaloniaProperty.Register<Border, IBrush>(nameof(Foreground));

        public new IBrush Foreground
        {
            get => GetValue(ForegroundProperty);
            set => SetValue(ForegroundProperty, value);
        }

        private static void ForegroundPropertyChanged(AvaloniaPropertyChangedEventArgs<IBrush> e)
        {
            if (e.Sender is CustomSearchBar ctrl)
            {
                ctrl.textBox.Foreground = e.NewValue.Value;
            }
        }
        
        public new static readonly StyledProperty<IBrush> OverlayBrushProperty =
            AvaloniaProperty.Register<Border, IBrush>(nameof(OverlayBrush));

        public new IBrush OverlayBrush
        {
            get => GetValue(OverlayBrushProperty);
            set => SetValue(OverlayBrushProperty, value);
        }

        private static void OverlayBrushPropertyChanged(AvaloniaPropertyChangedEventArgs<IBrush> e)
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
        
        public new static readonly StyledProperty<ICommand> SearchCancelledProperty =
            AvaloniaProperty.Register<TextBox, ICommand>(nameof(SearchCancelled));
        public ICommand SearchCancelled
        {
            get => GetValue(SearchCancelledProperty);
            set => SetValue(SearchCancelledProperty, value);
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