using System;
using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using AuraUtilities;

namespace CutCode.CrossPlatform.Helpers
{
    public class ButtonExts : AvaloniaObject
    {
        public static readonly AttachedProperty<string> UrlProperty =
            AvaloniaProperty.RegisterAttached<ButtonExts, Button, string>("Url");

        public static string GetUrl(Button button) => button.GetValue(UrlProperty);
        public static void SetUrl(Button button, string url) => button.SetValue(UrlProperty, url);

        static ButtonExts()
        {
            UrlProperty.Changed.Subscribe(onNext: e =>
            {
                if (e.Sender is Button btn && !string.IsNullOrEmpty(GetUrl(btn)))
                {
                    Process.Start(new ProcessStartInfo(GetUrl(btn)) { UseShellExecute = true });
                }
            });
        }
    }
}

