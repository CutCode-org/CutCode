using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CutCode
{
    /// <summary>
    /// Interaction logic for CodeBoxControl.xaml
    /// </summary>
    public partial class CodeBoxControl : UserControl
    {
        public CodeBoxControl()
        {
            InitializeComponent();
        }

        #region ThemeService property
        public static readonly DependencyProperty ThemeServiceProperty =
            DependencyProperty.Register("ThemeService", typeof(IThemeService), typeof(CodeBoxControl),
                new FrameworkPropertyMetadata(null, ThemeServicePropertyChanged));

        public IThemeService ThemeService
        {
            get => (IThemeService)GetValue(ThemeServiceProperty);
            set
            {
                SetValue(ThemeServiceProperty, value);
            }
        }
        private static void ThemeServicePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not CodeBoxControl ctrl || e.NewValue is not IThemeService) return;
            ctrl.ThemeService = (IThemeService)e.NewValue;
            ctrl.ThemeService.ThemeChanged += ctrl.ThemeChanged;
        }

        private void ThemeChanged(object sender, EventArgs e)
        {
            titleLabel.Background = ThemeService.IsLightTheme ? ColorCon.Convert("#000000") : ColorCon.Convert("#FFFFFF");
            descLabel.Background = ThemeService.IsLightTheme ? ColorCon.Convert("#000000") : ColorCon.Convert("#FFFFFF");
            card.Background = ThemeService.IsLightTheme ? ColorCon.Convert("#DADBDC") : ColorCon.Convert("#2A2E33");
        }
        #endregion

        #region Title property
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(CodeBoxControl),
                new FrameworkPropertyMetadata("", TitlePropertyChanged));

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set
            {
                SetValue(TitleProperty, value);
            }
        }
        private static void TitlePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not CodeBoxControl ctrl || e.NewValue is not string) return;
            ctrl.titleLabel.Content = (string)e.NewValue;
        }
        #endregion

        #region Description property
        public static readonly DependencyProperty DescProperty =
            DependencyProperty.Register("Desc", typeof(string), typeof(CodeBoxControl),
                new FrameworkPropertyMetadata("", DescPropertyChanged));

        public string Desc
        {
            get => (string)GetValue(DescProperty);
            set
            {
                SetValue(DescProperty, value);
            }
        }
        private static void DescPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not CodeBoxControl ctrl || e.NewValue is not string) return;
            ctrl.descLabel.Text = (string)e.NewValue;
        }
        #endregion

        #region LangType property
        public static readonly DependencyProperty LangTypeProperty =
            DependencyProperty.Register("LangType", typeof(string), typeof(CodeBoxControl),
                new FrameworkPropertyMetadata("", LangTypePropertyChanged));

        public string LangType
        {
            get => (string)GetValue(LangTypeProperty);
            set
            {
                SetValue(LangTypeProperty, value);
            }
        }
        private static void LangTypePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not CodeBoxControl ctrl || e.NewValue is not string) return;
            // show the image related thing
        }
        #endregion

        #region IsFavourite property
        public static readonly DependencyProperty IsFavouriteProperty =
            DependencyProperty.Register("IsFavourite", typeof(bool), typeof(CodeBoxControl),
                new FrameworkPropertyMetadata(false, IsFavouritePropertyChanged));

        public bool IsFavourite
        {
            get => (bool)GetValue(IsFavouriteProperty);
            set
            {
                SetValue(IsFavouriteProperty, value);
            }
        }
        private static void IsFavouritePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not CodeBoxControl ctrl || e.NewValue is not bool) return;
            ctrl.favImg.Visibility = (bool)e.NewValue ? Visibility.Visible : Visibility.Hidden;
        }
        #endregion

        #region Command property

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(CodeBoxControl),
                new PropertyMetadata(null));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }
        #endregion
    }
}
