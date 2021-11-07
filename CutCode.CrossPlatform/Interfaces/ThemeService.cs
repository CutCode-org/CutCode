using System;
using Avalonia.Media;
using CutCode.CrossPlatform.Models;


namespace CutCode.CrossPlatform.Interfaces
{
    public interface IThemeService
    {
        event EventHandler ThemeChanged;
        ColorModel CurrentColorModel { get; set; }
    }

    public class ThemeService : IThemeService
    {
        public ColorModel CurrentColorModel { get; set; }
        public event EventHandler ThemeChanged = null!;
        
        public ThemeService()
        {
            var themeId = GetCurrentThemeId(); // get the Id From Cache
            CurrentColorModel = ThemeConfig(themeId); // extract the Colors from the theme Id
        }

        private int GetCurrentThemeId()
        /*
         Method: Extracting(Getting) Theme id of the user preference from cache folder
         Return: Integer 
         */
        {
            // Do read from cache and return the theme id.
            return 0;
        }

        public void ChangeTheme(int themeId)
        /*
         Method: Change theme Method then Invoke the ThemeChanged event.
         Return: void 
         */
        {
            CurrentColorModel = ThemeConfig(themeId); // extract the Color with the theme Id
            ThemeChanged?.Invoke(CurrentColorModel, EventArgs.Empty); // invoke the theme chanaged event
        }

        private static ColorModel ThemeConfig(int themeId)
        /*
         Method: Extracting the colors based on the theme Id
         Return: ColorModel
         */
        {
            // do the extraction here
            return new ColorModel();
        }
    }
}