using Stylet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CutCode
{
    public class ShareViewModel : Screen
    {
        private readonly IThemeService themeService;
        private readonly IPageService pageService;
        private readonly IDataBase database;
        private readonly IApiManager apiManager;
        private readonly INotificationManager notifyManager;

        public ShareViewModel(IThemeService _themeService, 
                              IPageService _pageService, 
                              IDataBase _dataBase, 
                              IApiManager _apiManager,
                              INotificationManager _notifyManager)
        {
            themeService = _themeService;
            pageService = _pageService;
            database = _dataBase;
            apiManager = _apiManager;
            notifyManager = _notifyManager;
        }
    }
}
