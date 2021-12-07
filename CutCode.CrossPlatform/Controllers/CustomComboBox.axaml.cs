using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
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
            CurrentIndexProperty.Changed.Subscribe(CurrentIndexPropertyChanged);
        }
        
        public CustomComboBox()
        {
            InitializeComponent();
            this.GetControl("nameLabel", out nameLabel);
            this.GetControl("comboBox", out comboBox);
            
            comboBox.SelectionChanged += (sender, e) =>
            {
                ItemSelected?.Execute((sender as ComboBox)?.SelectedItem as string);
            };
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        
        public new static readonly StyledProperty<Brush> BackgroundProperty =
            AvaloniaProperty.Register<CustomComboBox, Brush>(nameof(Background));

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
            AvaloniaProperty.Register<CustomComboBox, Brush>(nameof(Foreground));

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
            AvaloniaProperty.Register<CustomComboBox, Brush>(nameof(OverlayBrush));

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
        
        public static readonly StyledProperty<string> NameProperty =
            AvaloniaProperty.Register<CustomComboBox, string>(nameof(Name), defaultValue:"Combo Box");

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
            AvaloniaProperty.Register<CustomComboBox, IList<string>>(nameof(Items));

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
        
        public new static readonly StyledProperty<ICommand> ItemSelectedProperty =
            AvaloniaProperty.Register<CustomComboBox, ICommand>(nameof(ItemSelected));

        public ICommand ItemSelected
        {
            get => GetValue(ItemSelectedProperty);
            set => SetValue(ItemSelectedProperty, value);
        }
        
        public static readonly StyledProperty<string> CurrentIndexProperty =
            AvaloniaProperty.Register<CustomComboBox, string>(nameof(CurrentIndex), defaultBindingMode:BindingMode.TwoWay);
            
        public string CurrentIndex
        {
            get => GetValue(CurrentIndexProperty);
            set => SetValue(CurrentIndexProperty, value);
        }
        
        private static void CurrentIndexPropertyChanged(AvaloniaPropertyChangedEventArgs<string> e)
        {
            if (e.Sender is CustomComboBox ctrl)
            {
                ctrl.comboBox.SelectedIndex = int.Parse(e.NewValue.Value);
            }
        }
    }
}