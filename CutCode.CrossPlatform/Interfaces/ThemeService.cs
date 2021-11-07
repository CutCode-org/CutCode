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
        private readonly IDataBase databaseService;
        public ColorModel CurrentColorModel { get; set; }
        public event EventHandler ThemeChanged = null!;
        
        public ThemeService(IDataBase _databaseService)
        {
            databaseService = _databaseService;
            
            var themeId = databaseService.GetCurrentThemeId(); // get the Id From Cache
            CurrentColorModel = databaseService.GetColorModelById(themeId); // extract the Colors from the theme Id
        }

        public void ChangeTheme(int themeId)
        /*
         Method: Change theme Method then Invoke the ThemeChanged event.
         Return: void 
         */
        {
            CurrentColorModel = databaseService.GetColorModelById(themeId); // extract the Color with the theme Id
            ThemeChanged?.Invoke(CurrentColorModel, EventArgs.Empty); // invoke the theme chanaged event
        }
    }
}