// ---------------------------------------------
//      --- CutCode.CrossPlatform by Scarementus ---
//      ---        Licence MIT       ---
// ---------------------------------------------

using System.Windows.Input;
using Aura.UI.Extensions;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace CutCode.CrossPlatform.Controllers;

public class NavigationItem : UserControl
{
    private Path IconPath => this.FindControl<Path>(nameof(IconPath));

    private Border ActiveBorder => this.FindControl<Border>(nameof(ActiveBorder));

    private Button MainButton => this.FindControl<Button>(nameof(MainButton));

    static NavigationItem()
    {
        IconProperty.Changed.AddClassHandler<NavigationItem>((x, e) => x.IconPropertyChanged(e));
        IconColourProperty.Changed.AddClassHandler<NavigationItem>((x, e) => x.IconColourPropertyChanged(e));
    }


    public NavigationItem()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public static readonly StyledProperty<IBrush> IconColourProperty =
        AvaloniaProperty.Register<NavigationItem, IBrush>(nameof(IconColour));

    public IBrush IconColour
    {
        get => GetValue(IconColourProperty);
        set => SetValue(IconColourProperty, value);
    }

    public static readonly StyledProperty<ICommand> CommandProperty =
        AvaloniaProperty.Register<NavigationItem, ICommand>(nameof(Command));

    public ICommand Command
    {
        get => GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }


    public static readonly StyledProperty<string> IconProperty =
        AvaloniaProperty.Register<NavigationItem, string>(nameof(Icon));

    public string Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public static readonly StyledProperty<bool> IsActiveProperty =
        AvaloniaProperty.Register<NavigationItem, bool>(nameof(IsActive));

    public bool IsActive
    {
        get => GetValue(IsActiveProperty);
        set => SetValue(IsActiveProperty, value);
    }

    private void IconPropertyChanged(AvaloniaPropertyChangedEventArgs e)
    {
        if (e.Sender is NavigationItem ctrl)
        {
            if (e.NewValue is string rawString)
                ctrl.IconPath.Data = Geometry.Parse(rawString);
        }
    }

    private void IconColourPropertyChanged(AvaloniaPropertyChangedEventArgs e)
    {
        if (e.Sender is NavigationItem ctrl)
            ctrl.IconPath.Fill = (IBrush?)e.NewValue;
    }

    private void IsActivePropertyChanged(AvaloniaPropertyChangedEventArgs e)
    {
        if (e.Sender is NavigationItem ctrl)
            ctrl.ActiveBorder.IsVisible = (bool)e.NewValue;
    }
    
    private void CommandPropertyChanged(AvaloniaPropertyChangedEventArgs e)
    {
        if (e.Sender is NavigationItem ctrl)
            ctrl.MainButton.Command = (ICommand)e.NewValue;
    }
}