using Aura.UI.Controls;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Avalonia.Data;
using System;
using System.Reactive;
using System.Windows.Input;
using System.Linq;

namespace CutCode.CrossPlatform.Controllers
{
    public partial class SearchBarControl : UserControl
    {
        private readonly DispatcherTimer activityTimer;
        private Button exitBtn;
        private TextBox searchBox;
        private ProgressRing circularBar;

        static SearchBarControl()
        {
            TextProperty.Changed.Subscribe(TextPropertyChanged);
            IsSearchedProperty.Changed.Subscribe(IsSearchedPropertyChanged);
        }

        public SearchBarControl()
        {
            InitializeComponent();

            searchBox.Bind(TextProperty, new Binding("Text"));
            exitBtn.IsVisible = false;
            circularBar.IsVisible = false;

            exitBtn.Click += ClearClicked;
            searchBox.GetObservable(TextBox.TextProperty).Subscribe(TextChanged);

            activityTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(600),
                IsEnabled = true
            };
            activityTimer.Tick += InActivity;
        }

        private static void TextPropertyChanged(AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Sender is SearchBarControl ctrl && e.NewValue is string str)
            {
                ctrl.searchBox.Text = str;
            }
        }
        private static void IsSearchedPropertyChanged(AvaloniaPropertyChangedEventArgs<bool> e)
        {
            if (e.Sender is SearchBarControl ctrl && !string.IsNullOrEmpty(ctrl.Text) && e.NewValue.HasValue)
            {
                ctrl.circularBar.IsVisible = !e.NewValue.Value;
                ctrl.exitBtn.IsVisible = e.NewValue.Value;
                ctrl.activityTimer.IsEnabled = e.NewValue.Value;
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            this.GetControl("exitBtn", out exitBtn);
            this.GetControl("searchBox", out searchBox);
            this.GetControl("circularBar", out circularBar);
        }

        private void TextChanged(string e)
        {
            if (searchBox.Text.Count() == 0)
            {
                ClearClicked(null, null);
            }
            else
            {
                Text = searchBox.Text;
                exitBtn.IsVisible = false;
                circularBar.IsVisible = !string.IsNullOrEmpty(searchBox.Text);
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
                    if (SearchedText != Text) OldText = Text;
                    else
                    {
                        exitBtn.IsVisible = true;
                        circularBar.IsVisible = false;//Todo: maybe can be better "IsEnabled = false, Opacity = 0" .Visibility = Visibility.Hidden;
                    }
                }
            }
        }

        public string Text
        {
            get => GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly StyledProperty<string> TextProperty =
            AvaloniaProperty.Register<SearchBarControl, string>(nameof(Text), defaultBindingMode: BindingMode.TwoWay);

        public bool IsSearched
        {
            get => GetValue(IsSearchedProperty);
            set => SetValue(IsSearchedProperty, value);
        }

        public static readonly StyledProperty<bool> IsSearchedProperty =
            AvaloniaProperty.Register<SearchBarControl, bool>(nameof(IsSearched));

        public ICommand SearchCommand
        {
            get => GetValue(SearchCommandProperty);
            set => SetValue(SearchCommandProperty, value);
        }

        public static readonly StyledProperty<ICommand> SearchCommandProperty =
            AvaloniaProperty.Register<SearchBarControl, ICommand>(nameof(SearchCommand));
    }
}
