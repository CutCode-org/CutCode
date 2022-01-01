using System;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Disposables;
using System.Runtime.InteropServices;
using Aura.UI.Extensions;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.VisualTree;

namespace CutCode.CrossPlatform.Controllers
{
    //maximizePath = Geometry.Parse("F1M50,-150L50,-50 150,-50 150,-150 50,-150z M37.5,-162.5L162.5,-162.5 162.5,-37.5 37.5,-37.5 37.5,-162.5z");
    //restorePath = Geometry.Parse("F1M50,-125L50,-37.5 137.5,-37.5 137.5,-125 50,-125z M75,-150L75,-137.5 150,-137.5 150,-62.5 162.5,-62.5 162.5,-150 75,-150z M62.5,-162.5L175,-162.5 175,-50 150,-50 150,-25 37.5,-25 37.5,-137.5 62.5,-137.5 62.5,-162.5z");

    [PseudoClasses(":minimized", ":normal", ":maximized")]
    public class WindowButtons : TemplatedControl
    {
        private CompositeDisposable? _disposables;

        public WindowButtons()
        {
            this.GetObservable(HostWindowProperty).Subscribe(w =>
            {
                Detach();
                if(w is not null)
                {
                    Attach(w);
                }
            });
        }

        public virtual void Attach(Window hostWindow)
        {
            if (_disposables == null)
            {
                HostWindow = hostWindow;

                _disposables = new CompositeDisposable
                {
                    HostWindow.GetObservable(Window.WindowStateProperty)
                    .Subscribe(x =>
                    {
                        PseudoClasses.Set(":minimized", x == WindowState.Minimized);
                        PseudoClasses.Set(":normal", x == WindowState.Normal);
                        PseudoClasses.Set(":maximized", x == WindowState.Maximized);
                        PseudoClasses.Set(":fullscreen", x == WindowState.FullScreen);
                    })
                };

                Console.WriteLine("attached");
            }
        }


        public virtual void Detach()
        {
            if (_disposables != null)
            {
                _disposables.Dispose();
                _disposables = null;

                HostWindow = null;
            }
        }

        protected virtual void OnClose()
        {
            Console.WriteLine("closed");
            HostWindow.Close();
        }

        protected virtual void OnRestore()
        {
            Console.WriteLine("restored/maximized");
            HostWindow.WindowState = HostWindow.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        protected virtual void OnMinimize()
        {
            Console.WriteLine("minimized");
            HostWindow.WindowState = WindowState.Minimized;
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            var closeButton = e.NameScope.Get<Panel>("PART_CloseButton");
            var restoreButton = e.NameScope.Get<Panel>("PART_RestoreButton");
            var minimiseButton = e.NameScope.Get<Panel>("PART_MinimiseButton");

            closeButton.AddHandler(PointerReleasedEvent, (sender, e) => OnClose());

            restoreButton.AddHandler(PointerReleasedEvent, (sender, e) => OnRestore());

            minimiseButton.AddHandler(PointerReleasedEvent, (sender, e) => OnMinimize());
        }
        public Window HostWindow
        {
            get => GetValue(HostWindowProperty);
            set => SetValue(HostWindowProperty, value);
        }

        public static readonly StyledProperty<Window> HostWindowProperty =
            AvaloniaProperty.Register<WindowButtons, Window>(nameof(HostWindow));

    }
}