using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CutCode
{
    public class SettingViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly IThemeService _themeService;
        public SettingViewModel()
        {/*
            _themeService = SimpleIoc.Default.GetInstance<IThemeService>();
            _themeService.ThemeChanged += (object sender, EventArgs e) =>
            {
                if (_themeService.IsLightTheme)
                {

                }
                else
                {

                }
            };*/

            themeBtnBackground = (SolidColorBrush)new BrushConverter().ConvertFrom("#17191B");
        }

        private SolidColorBrush _themeBtnBackground { get; set; }
        public SolidColorBrush themeBtnBackground
        {
            get => _themeBtnBackground;
            set
            {
                if(value != _themeBtnBackground)
                {
                    _themeBtnBackground = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
