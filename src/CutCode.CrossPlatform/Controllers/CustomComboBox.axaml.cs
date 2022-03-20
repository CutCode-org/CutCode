using System;
using System.Collections.Generic;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using CutCode.CrossPlatform.Helpers;

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
            
            comboBox.SelectionChanged += (sender, e) =>
            {
                ItemSelected?.Execute((sender as ComboBox)?.SelectedItem as string);
            };
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        
        public new static readonly StyledProperty<IBrush> BackgroundProperty =
            AvaloniaProperty.Register<CustomComboBox, IBrush>(nameof(Background));

        public new IBrush Background
        {
            get => GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
        }

        private static void BackgroundPropertyChanged(AvaloniaPropertyChangedEventArgs<IBrush> e)
        {
            if (e.Sender is CustomComboBox ctrl)
            {
                ctrl.comboBox.Background = e.NewValue.Value;
            }
        }
        
        public new static readonly StyledProperty<IBrush> ForegroundProperty =
            AvaloniaProperty.Register<CustomComboBox, IBrush>(nameof(Foreground));

        public new IBrush Foreground
        {
            get => GetValue(ForegroundProperty);
            set => SetValue(ForegroundProperty, value);
        }

        private static void ForegroundPropertyChanged(AvaloniaPropertyChangedEventArgs<IBrush> e)
        {
            if (e.Sender is CustomComboBox ctrl)
            {
                ctrl.nameLabel.Foreground = e.NewValue.Value;
                ctrl.comboBox.Foreground = e.NewValue.Value;
            }
        }
        
        public new static readonly StyledProperty<IBrush> OverlayBrushProperty =
            AvaloniaProperty.Register<CustomComboBox, IBrush>(nameof(OverlayBrush));

        public new IBrush OverlayBrush
        {
            get => GetValue(OverlayBrushProperty);
            set => SetValue(OverlayBrushProperty, value);
        }

        private static void OverlayBrushPropertyChanged(AvaloniaPropertyChangedEventArgs<IBrush> e)
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
        
        public static readonly StyledProperty<int> SelectedIndexProperty  =
            AvaloniaProperty.Register<CustomComboBox, int>(nameof(SelectedIndex), defaultValue:-1, defaultBindingMode:BindingMode.TwoWay);
            
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