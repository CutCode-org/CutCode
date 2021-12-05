using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Threading;

namespace CutCode
{
    /// <summary>
    /// Interaction logic for SearchBarControl.xaml
    /// </summary>
    public partial class SearchBarControl : UserControl
    {
        private readonly DispatcherTimer activityTimer;
        public SearchBarControl()
        {
            InitializeComponent();

            searchBox.SetBinding(TextProperty, new Binding("Text"));
            exitBtn.Visibility = Visibility.Hidden;
            circularBar.Visibility = Visibility.Hidden;
            exitBtn.Click += ClearClicked;
            searchBox.TextChanged += TextChanged;

            activityTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(600),
                IsEnabled = true
            };
            activityTimer.Tick += InActivity;
        }

        #region Text property
        public static readonly StyledProperty<string> TextProperty = 
            AvaloniaProperty.Register<TextBox, string>(nameof(Text));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set 
            {
                SetValue(TextProperty, value);
            } 
        }
        private static void TextPropertyChanged(DependencyObject d, StyledPropertyChangedEventArgs e)
        {
            if (d is not SearchBarControl ctrl || e.NewValue is not string) return;
            ctrl.searchBox.Text = (string)e.NewValue;
        }
        #endregion

        #region Placeholder Text property
        public static readonly StyledProperty PlaceholderProperty =
            StyledProperty.Register("PlaceholderText", typeof(string), typeof(SearchBarControl),
                new PropertyMetadata("", PlaceholderPropertyChanged));

        public string PlaceholderText
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }
        private static void PlaceholderPropertyChanged(DependencyObject d, StyledPropertyChangedEventArgs e)
        {
            if (d is not SearchBarControl ctrl || e.NewValue is not string) return;
            ctrl.placeholder.Text = (string)e.NewValue;
        }
        #endregion

        #region PlaceholderText color property
        public static readonly StyledProperty PlaceholderTextColorProperty =
            StyledProperty.Register("PlaceholderTextColor", typeof(SolidColorBrush), typeof(SearchBarControl),
                new PropertyMetadata(ColorCon.Convert("#FFFFFF"), PlaceholderTextColorPropertyChanged));

        public SolidColorBrush PlaceholderTextColor
        {
            get => (SolidColorBrush)GetValue(PlaceholderTextColorProperty);
            set => SetValue(PlaceholderTextColorProperty, value);
        }
        private static void PlaceholderTextColorPropertyChanged(DependencyObject d, StyledPropertyChangedEventArgs e)
        {
            if (d is not SearchBarControl ctrl || e.NewValue is not SolidColorBrush) return;
            ctrl.placeholder.Foreground = (SolidColorBrush)e.NewValue;
        }
        #endregion

        #region Background color property
        public static readonly StyledProperty BackgroundColorProperty =
            StyledProperty.Register("BackgroundColor", typeof(SolidColorBrush), typeof(SearchBarControl),
                new PropertyMetadata(ColorCon.Convert("#000000"), BackgroundColorPropertyChanged));

        public SolidColorBrush BackgroundColor
        {
            get => (SolidColorBrush)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }
        private static void BackgroundColorPropertyChanged(DependencyObject d, StyledPropertyChangedEventArgs e)
        {
            if (d is not SearchBarControl ctrl || e.NewValue is not SolidColorBrush) return;
            ctrl.searchBarFrame.Background = (SolidColorBrush)e.NewValue;
        }
        #endregion

        #region Text color property
        public static readonly StyledProperty TextColorProperty =
            StyledProperty.Register("TextColor", typeof(SolidColorBrush), typeof(SearchBarControl),
                new PropertyMetadata(ColorCon.Convert("#FFFFFF"), TextColorPropertyChanged));

        public SolidColorBrush TextColor
        {
            get => (SolidColorBrush)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }
        private static void TextColorPropertyChanged(DependencyObject d, StyledPropertyChangedEventArgs e)
        {
            if (d is not SearchBarControl ctrl || e.NewValue is not SolidColorBrush) return;
            ctrl.searchBox.Foreground = (SolidColorBrush)e.NewValue;
        }
        #endregion

        #region ButtonHover color property
        public static readonly StyledProperty ButtonHoverColorProperty =
            StyledProperty.Register("ButtonHoverColor", typeof(SolidColorBrush), typeof(SearchBarControl),
                new PropertyMetadata(ColorCon.Convert("#000000"), ButtonHoverColorPropertyChanged));

        public SolidColorBrush ButtonHoverColor
        {
            get => (SolidColorBrush)GetValue(ButtonHoverColorProperty);
            set => SetValue(ButtonHoverColorProperty, value);
        }
        private static void ButtonHoverColorPropertyChanged(DependencyObject d, StyledPropertyChangedEventArgs e)
        {
            if (d is not SearchBarControl ctrl || e.NewValue is not SolidColorBrush) return;
            ctrl.exitBtn.BorderBrush = (SolidColorBrush)e.NewValue;
        }
        #endregion

        #region Theme property
        public static readonly StyledProperty ThemeProperty =
            StyledProperty.Register("Theme", typeof(bool), typeof(SearchBarControl),
                new PropertyMetadata(true, ThemePropertyChanged));

        public bool Theme
        {
            get => (bool)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
        private static void ThemePropertyChanged(DependencyObject d, StyledPropertyChangedEventArgs e)
        {
            if (d is not SearchBarControl ctrl || e.NewValue is not bool) return;
            ctrl.exitBtnImage.Source = new BitmapImage(new Uri((bool)e.NewValue ? @"../Resources/Images/Icons/exit_black.png" : @"../Resources/Images/Icons/exit_white.png", UriKind.Relative));
        }
        #endregion

        #region IsSearched property
        public static readonly StyledProperty IsSearchedProperty =
            StyledProperty.Register("IsSearched", typeof(bool), typeof(SearchBarControl),
                new PropertyMetadata(true, IsSearchedPropertyChanged));

        public bool IsSearched
        {
            get => (bool)GetValue(IsSearchedProperty);
            set => SetValue(IsSearchedProperty, value);
        }
        private static void IsSearchedPropertyChanged(DependencyObject d, StyledPropertyChangedEventArgs e)
        {
            if (d is not SearchBarControl ctrl || e.NewValue is not bool) return;
            if (!string.IsNullOrEmpty(ctrl.Text))
            {
                ctrl.circularBar.Visibility = (bool)e.NewValue ? Visibility.Hidden : Visibility.Visible;
                ctrl.exitBtn.Visibility = (bool)e.NewValue ? Visibility.Visible : Visibility.Hidden;
                ctrl.activityTimer.IsEnabled = (bool)e.NewValue;
            }
        }
        #endregion

        #region Search Command property

        public static readonly StyledProperty<ICommand> SearchCommandProperty =
            StyledProperty.Register("SearchCommand", typeof(ICommand), typeof(SearchBarControl),
                new PropertyMetadata(null));

        public ICommand SearchCommand
        {
            get => (ICommand)GetValue(SearchCommandProperty);
            set => SetValue(SearchCommandProperty, value);
        }
        #endregion

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            if (searchBox.Text.Count() == 0) ClearClicked(null, null);
            else
            {
                Text = searchBox.Text;
                exitBtn.Visibility = Visibility.Hidden;
                circularBar.Visibility = string.IsNullOrEmpty(searchBox.Text) ? Visibility.Hidden : Visibility.Visible;
                activityTimer.IsEnabled = true;
                letsType = true;
            }
        }

        private void ClearClicked(object sender, EventArgs e)
        {
            searchBox.Text = "";
            SearchCommand?.Execute("");
            letsType = false;
        }

        private bool letsType = true;
        private string OldText = "";
        private string SearchedText = "";
        private void InActivity(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Text))
            {
                if (OldText == Text)
                {
                    if (letsType)
                    {
                        OldText = "";
                        SearchedText = Text;
                        SearchCommand?.Execute(Text);
                        activityTimer.IsEnabled = false;
                    }
                }
                else
                {
                    if(SearchedText != Text) OldText = Text;
                    else
                    {
                        exitBtn.Visibility = Visibility.Visible;
                        circularBar.Visibility = Visibility.Hidden;
                    }
                }
            }
        }
    }
}
