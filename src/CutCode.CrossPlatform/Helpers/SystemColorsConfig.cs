using Avalonia;
using Avalonia.Media;

namespace CutCode.CrossPlatform.Helpers
{
    public static class SystemColorsConfig
    {
        public static void LightThemeColors()
        {
            // Scroll bar colors
            Application.Current.Resources["ScrollBarTrackFill"] = new SolidColorBrush(Color.Parse("#BCBCBC"), 0.8);
            Application.Current.Resources["ScrollBarTrackFillPointerOver"] = new SolidColorBrush(Color.Parse("#BCBCBC"), 0.8);
                        
            Application.Current.Resources["ScrollBarButtonArrowForeground"] = new SolidColorBrush(Colors.Black, 1);
            Application.Current.Resources["ScrollBarButtonArrowForegroundPointerOver"] = new SolidColorBrush(Colors.Black, 1);
            Application.Current.Resources["ScrollBarButtonArrowForegroundPressed"] = new SolidColorBrush(Colors.Black, 1);
                        
                        
            Application.Current.Resources["ScrollBarThumbFillPointerOver"] = new SolidColorBrush(Color.Parse("#B3B3B3"), 0.9);
            Application.Current.Resources["ScrollBarThumbFillPressed"] = new SolidColorBrush(Color.Parse("#ACACAC"), 0.9);
            
            // Combo Box colors
            Application.Current.Resources["ComboBoxItemForeground"] = new SolidColorBrush(Colors.Black, 1);
            Application.Current.Resources["ComboBoxItemForegroundPressed"] = new SolidColorBrush(Colors.Black, 1);
            Application.Current.Resources["ComboBoxItemForegroundPointerOver"] = new SolidColorBrush(Colors.Black, 1);
            Application.Current.Resources["ComboBoxItemForegroundSelected"] = new SolidColorBrush(Colors.Black, 1);
            Application.Current.Resources["ComboBoxItemForegroundSelectedPressed"] = new SolidColorBrush(Colors.Black, 1);
            Application.Current.Resources["ComboBoxItemForegroundSelectedPointerOver"] = new SolidColorBrush(Colors.Black, 1);

            Application.Current.Resources["TextColor1"] = new SolidColorBrush(Color.Parse(("#0B0B13")), 1);
            Application.Current.Resources["BackgroundColor1"] = new SolidColorBrush(Color.Parse(("#F2F3F5")), 1);
        }
        
        public static void DarkThemeColors()
        {
            Application.Current.Resources["ScrollBarTrackFill"] = new SolidColorBrush(Color.Parse("#2A2E33"), 0.8);
            Application.Current.Resources["ScrollBarTrackFillPointerOver"] = new SolidColorBrush(Color.Parse("#24272B"), 0.8);
                        
            Application.Current.Resources["ScrollBarButtonArrowForeground"] = new SolidColorBrush(Colors.White, 1);
            Application.Current.Resources["ScrollBarButtonArrowForegroundPointerOver"] = new SolidColorBrush(Colors.White, 1);
            Application.Current.Resources["ScrollBarButtonArrowForegroundPressed"] = new SolidColorBrush(Colors.White, 1);
                        
            Application.Current.Resources["ScrollBarThumbFillPointerOver"] = new SolidColorBrush(Color.Parse("#393E44"), 0.9);
            Application.Current.Resources["ScrollBarThumbFillPressed"] = new SolidColorBrush(Color.Parse("#3E4249"), 0.9);
            
            // Combo Box colors
            Application.Current.Resources["ComboBoxItemForeground"] = new SolidColorBrush(Colors.White, 1);
            Application.Current.Resources["ComboBoxItemForegroundPressed"] = new SolidColorBrush(Colors.White, 1);
            Application.Current.Resources["ComboBoxItemForegroundPointerOver"] = new SolidColorBrush(Colors.White, 1);
            Application.Current.Resources["ComboBoxItemForegroundSelected"] = new SolidColorBrush(Colors.White, 1);
            Application.Current.Resources["ComboBoxItemForegroundSelectedPressed"] = new SolidColorBrush(Colors.White, 1);
            Application.Current.Resources["ComboBoxItemForegroundSelectedPointerOver"] = new SolidColorBrush(Colors.White, 1);

            Application.Current.Resources["TextColor1"] = new SolidColorBrush(Color.Parse(("#94969A")), 1);
            Application.Current.Resources["BackgroundColor1"] = new SolidColorBrush(Color.Parse(("#32363C")), 1);
        }
    }
}