using System.Runtime.InteropServices;
using Aura.UI.Extensions;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Media;

namespace CutCode.CrossPlatform.Controllers
{
    public class WindowButtons : TemplatedControl
    {
        private Button minButton;
        private Button mediumButton;
        private Button closeButton;
        public WindowButtons()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                IsVisible = false;
            }
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            
            this.GetControl(e, "PART_MinimizeButton", out minButton);
            this.GetControl(e, "PART_MediumButton", out mediumButton);
            this.GetControl(e, "PART_CloseButton", out closeButton);

            minButton.Click += (s, e) =>
            {
                WindowState = WindowState.Minimized;
            };
            
            mediumButton.Click += (s,e) =>
            {
                WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
            };

            closeButton.Click += (s, e) =>
            {
                Window.Close();
            };
        }

        public Window Window
        {
            get => GetValue(WindowProperty);
            set => SetValue(WindowProperty, value);
        }

        public static readonly StyledProperty<Window> WindowProperty =
            AvaloniaProperty.Register<WindowButtons, Window>(nameof(Window), defaultBindingMode: BindingMode.OneWay);

        public WindowState WindowState
        {
            get => GetValue(WindowStateProperty);
            set => SetValue(WindowStateProperty, value);
        }
        public static readonly StyledProperty<WindowState> WindowStateProperty =
            AvaloniaProperty.Register<WindowButtons, WindowState>(nameof(WindowState), WindowState.Normal, defaultBindingMode: BindingMode.TwoWay);
    }
}