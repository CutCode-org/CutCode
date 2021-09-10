using Stylet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace CutCode
{
    public class SettingViewModel : Stylet.Screen
    {
        public ObservableCollection<ThemeButtonModel> themeBtns { get; set; }
        public ObservableCollection<SyncBtnModel> SyncBtns { get; set; }
        private readonly IThemeService themeService;
        private readonly IDataBase database;
        private readonly INotificationManager notificationManager;
        public SettingViewModel(IThemeService _themeService, IDataBase _database, INotificationManager _notificationManager)
        {
            database = _database;
            notificationManager = _notificationManager;

            themeService = _themeService;
            themeService.ThemeChanged += ThemeChanged;

            themeBtns = new ObservableCollection<ThemeButtonModel>();
            themeBtns.Add(new ThemeButtonModel("Light Mode", themeService));
            themeBtns.Add(new ThemeButtonModel("Dark Mode", themeService));

            SyncBtns = new ObservableCollection<SyncBtnModel>() 
            {
                new SyncBtnModel("Import", themeService),
                new SyncBtnModel("Export", themeService)
            };

            ThemeChanged(null, null);
        }

        private void ThemeChanged(object sender, EventArgs e)
        {
            mainTextColor = themeService.IsLightTheme ? ColorCon.Convert("#0B0B13") : ColorCon.Convert("#94969A");
            cardBackgroundColor = themeService.IsLightTheme ? ColorCon.Convert("#F2F3F5") : ColorCon.Convert("#2F3136");
            syncButtonBackground = themeService.IsLightTheme ? ColorCon.Convert("#E5E6E8") : ColorCon.Convert("#27282C");
        }

        private SolidColorBrush _mainTextColor;
        public SolidColorBrush mainTextColor
        {
            get => _mainTextColor;
            set
            {
                if (value != _mainTextColor)
                {
                    SetAndNotify(ref _mainTextColor, value);
                }
            }
        }

        private SolidColorBrush _cardBackgroundColor;
        public SolidColorBrush cardBackgroundColor
        {
            get => _cardBackgroundColor;
            set
            {
                if (value != _cardBackgroundColor)
                {
                    SetAndNotify(ref _cardBackgroundColor, value);
                }
            }
        }

        private SolidColorBrush _syncButtonBackground;
        public SolidColorBrush syncButtonBackground
        {
            get => _syncButtonBackground;
            set
            {
                if (value != _syncButtonBackground)
                {
                    SetAndNotify(ref _syncButtonBackground, value);
                }
            }
        }
        public void ThemeChangeCommand(string selectedTheme) 
        {
            themeService.IsLightTheme = selectedTheme == "Light Mode" ? true : false;
            database.ChangeTheme(themeService.IsLightTheme);
        } 

        public void SyncCommand(string syncType)
        {
            string message = "";

            if(syncType == "Import")
            {
                var fileDialog = new OpenFileDialog();
                fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                fileDialog.DefaultExt = "whl";
                fileDialog.Filter = "whl files (*.whl)|*.whl";
                fileDialog.ShowDialog();

                if (!string.IsNullOrEmpty(fileDialog.FileName))
                {
                    message = database.ImportData(fileDialog.FileName);
                }
            }
            else
            {
                var fileDialog = new SaveFileDialog();
                fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                fileDialog.DefaultExt = "whl";
                fileDialog.FileName = $"Export_CutCode_{DateTime.Now.ToString("yyyyMMddhhmmss")}.whl";
                fileDialog.Filter = "whl files (*.whl)|*.whl";
                fileDialog.ShowDialog();
                if (!string.IsNullOrEmpty(fileDialog.FileName))
                {
                    message = database.ExportData(fileDialog.FileName);
                }
            }
            if(!string.IsNullOrEmpty(message)) notificationManager.CreateNotification(message, 4);
        }
    }
}
