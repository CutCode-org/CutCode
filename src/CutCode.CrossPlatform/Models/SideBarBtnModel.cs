using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CutCode.CrossPlatform.Interfaces;
using CutCode.CrossPlatform.Services;
using CutCode.CrossPlatform.ViewModels;
using ReactiveUI;

namespace CutCode.CrossPlatform.Models
{
    public static class SideBarBtns
    {
        public static List<string> AllSideBarBtns = new List<string>() { "Home", "Add", "Favourite", "Share","Settings"};
        public static List<string> SideBarBtnsDarkTheme = new List<string>() { "home_white.png", "add_white.png", "fav_white.png", "share_white.png", "settings_white.png" };
        public static List<string> SideBarBtnsLightTheme = new List<string>() { "home_black.png", "add_black.png", "fav_black.png", "share_black.png", "settings_black.png" };

    }
    public class SideBarBtnModel : ViewModelBase
    {
        private readonly ThemeService themeService;
        private readonly IAssetLoader assetLoader;
        private List<string> btnBothimages = new List<string>();
        public SideBarBtnModel(string _toolTipText, ThemeService _themeService)
        {
            assetLoader = AvaloniaLocator.CurrentMutable.GetService<IAssetLoader>();
            
            themeService = _themeService;
            themeService.ThemeChanged += ThemeChanged;

            toolTipText = _toolTipText;
            var index = SideBarBtns.AllSideBarBtns.IndexOf(_toolTipText);
            btnBothimages.Add(SideBarBtns.SideBarBtnsLightTheme[index]);
            btnBothimages.Add(SideBarBtns.SideBarBtnsDarkTheme[index]);
            background = Color.Parse("#00FFFFFF");

            SetAppearance();
        }
        private void ThemeChanged(object sender, EventArgs e)
        {
            SetAppearance();
        }

        private void SetAppearance()
        {
            imageSource = ThemeService.Current.Theme == ThemeType.Light ? ImageFromUri($"avares://CutCode.CrossPlatform/Assets/Images/Icons/{btnBothimages[0]}") : ImageFromUri($"avares://CutCode.CrossPlatform/Assets/Images/Icons/{btnBothimages[1]}");

            toolTipBackground = ThemeService.Current.Theme == ThemeType.Light ? Color.Parse("#CBD0D5") : Color.Parse("#1E1E1E");
            toolTipForeground = ThemeService.Current.Theme == ThemeType.Light ? Color.Parse("#060607") : Color.Parse("#94969A");

            
            if (background != Color.Parse("#00FFFFFF"))
            {
                background = ThemeService.Current.Theme == ThemeType.Light ? Color.Parse("#FCFCFC") : Color.Parse("#36393F");
            }
        }
        
        private IImage ImageFromUri(string path)
        {
            var uri = new Uri(path);
            return assetLoader.Exists(uri) ? new Bitmap(assetLoader.Open(uri)) : throw new Exception();
        } 
        
        public string toolTipText { get; set; }

        private Color _background;
        public Color background
        {
            get => _background;
            set
            {
                if(value != _background)
                {
                    this.RaiseAndSetIfChanged(ref _background, value);
                }
            }
        }

        private IImage _imageSource;
        public IImage imageSource
        {
            get => _imageSource;
            set
            {
                if (value != this._imageSource)
                {
                    this.RaiseAndSetIfChanged(ref _imageSource, value);
                }
            }
        }

        private Color _toolTipBackground;
        public Color toolTipBackground
        {
            get => _toolTipBackground;
            set { this.RaiseAndSetIfChanged(ref _toolTipBackground, value); }
        }

        private Color _toolTipForeground;
        public Color toolTipForeground
        {
            get => _toolTipForeground;
            set { this.RaiseAndSetIfChanged(ref _toolTipForeground, value); }
        }
    }
}
