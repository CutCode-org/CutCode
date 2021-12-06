using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace CutCode.CrossPlatform.Controllers
{
    public class CustomComboBox : UserControl
    {
        private Label nameLabel;
        private ComboBox comboBox;
        
        static CustomComboBox()
        {
            BackgroundProperty.Changed.Subscribe(BackgroundPropertyChanged);
            ForegroundProperty.Changed.Subscribe(ForegroundPropertyChanged);
            OverlayBrushProperty.Changed.Subscribe(OverlayBrushPropertyChanged);
            
            NameProperty.Changed.Subscribe(NamePropertyChanged);
            ItemsProperty.Changed.Subscribe(ItemsPropertyChanged);
            SelectedIndexProperty.Changed.Subscribe(SelectedIndexPropertyChanged);
        }
        
        public CustomComboBox()
        {
            InitializeComponent();
            this.GetControl("nameLabel", out nameLabel);
            this.GetControl("comboBox", out comboBox);
            
            comboBox.SelectionChanged += (sender, args) =>
            {
                SelectedIndex = comboBox.SelectedIndex;
                SelectedItem = comboBox.SelectedItem;
            };
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
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
            if (e.Sender is CustomComboBox ctrl)
            {
                ctrl.comboBox.Background = e.NewValue.Value;
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
            if (e.Sender is CustomComboBox ctrl)
            {
                ctrl.nameLabel.Foreground = e.NewValue.Value;
                ctrl.comboBox.Foreground = e.NewValue.Value;
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
            if (e.Sender is CustomComboBox ctrl)
            {
                ctrl.comboBox.BorderBrush = e.NewValue.Value;
            }
        }
        
        public new static readonly StyledProperty<string> NameProperty =
            AvaloniaProperty.Register<ComboBox, string>(nameof(Name));

        public string Name
        {
            get => GetValue(NameProperty);
            set => SetValue(NameProperty, value);
        }

        private static void NamePropertyChanged(AvaloniaPropertyChangedEventArgs<string> e)
        {
            if (e.Sender is CustomComboBox ctrl)
            {
                ctrl.nameLabel.Content = e.NewValue.Value;
            }
        }
        
        public new static readonly StyledProperty<IList<string>> ItemsProperty =
            AvaloniaProperty.Register<ComboBox, IList<string>>(nameof(Items));

        public IList<string> Items
        {
            get => GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }

        private static void ItemsPropertyChanged(AvaloniaPropertyChangedEventArgs<IList<string>> e)
        {
            if (e.Sender is CustomComboBox ctrl)
            {
                ctrl.comboBox.Items = e.NewValue.Value;
            }
        }
        
        public new static readonly StyledProperty<object> SelectedItemProperty =
            AvaloniaProperty.Register<ComboBox, object>(nameof(SelectedItem));

        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }
        
        public new static readonly StyledProperty<int> SelectedIndexProperty =
            AvaloniaProperty.Register<ComboBox, int>(nameof(SelectedIndex));

        public int SelectedIndex
        {
            get => GetValue(SelectedIndexProperty);
            set => SetValue(SelectedIndexProperty, value);
        }
        
        private static void SelectedIndexPropertyChanged(AvaloniaPropertyChangedEventArgs<int> e)
        {
            if (e.Sender is CustomComboBox ctrl)
            {
                ctrl.comboBox.SelectedIndex = e.NewValue.Value;
            }
        }
    }
}