using Stylet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace CutCode
{
    public class CodeViewModel : Screen
    {
        private readonly IThemeService themeService;
        private readonly IPageService pageService;
        private readonly IDataBase database;
        public CodeBoxModel codeModel;
        public CodeViewModel(IThemeService _themeService, IPageService _pageService, IDataBase _database, CodeBoxModel code)
        {
            themeService = _themeService;
            pageService = _pageService;

            database = _database;

            SetAppearance();

            codeModel = code;

            title = code.title;
            desc = code.desc;
            this.code = code.code;
            isFav = code.isFav;
            langType = code.langType;
            createdDate = "Will be changed later";

            isEnabled = false;
            oppisEnabled = !isEnabled;

            SetAppearance();
        }
        private void SetAppearance()
        {
            mainTextColor = themeService.IsLightTheme ? ColorCon.Convert("#0B0B13") : ColorCon.Convert("#94969A");
            textBackground = themeService.IsLightTheme ? ColorCon.Convert("#DADBDC") : ColorCon.Convert("#2A2E33");
            textForeground = themeService.IsLightTheme ? ColorCon.Convert("#1A1A1A") : ColorCon.Convert("#F7F7F7");
            codeBackground = themeService.IsLightTheme ? ColorCon.Convert("#E3E5E8") : ColorCon.Convert("#2C3036");
            ButtonsBackground = themeService.IsLightTheme ? ColorCon.Convert("#F2F2F2") : ColorCon.Convert("#2A2C30");

            copyAddr = themeService.IsLightTheme ? "../Resources/Images/Icons/copy_black.png" : "../Resources/Images/Icons/copy_white.png";
            favAddr = isFav ? "../Resources/Images/Icons/fav.png" : themeService.IsLightTheme ? "../Resources/Images/Icons/fav_black.png" : "../Resources/Images/Icons/fav_white.png";
            backAddr = themeService.IsLightTheme ? "../Resources/Images/Icons/back_black.png" : "../Resources/Images/Icons/back_white.png";
            editAddr = themeService.IsLightTheme ? "../Resources/Images/Icons/edit_black.png" : "../Resources/Images/Icons/edit_white.png";
            saveAddr = themeService.IsLightTheme ? "../Resources/Images/Icons/save_black.png" : "../Resources/Images/Icons/save_white.png";
            closeAddr = themeService.IsLightTheme ? "../Resources/Images/Icons/exit_black.png" : "../Resources/Images/Icons/exit_white.png";

            opacity1 = 1;
            opacity2 = 0.3;
        }

        #region Code datas
        private string _title;
        public string title
        {
            get => _title;
            set
            {
                SetAndNotify(ref _title, value);
            }
        }

        private string _desc;
        public string desc
        {
            get => _desc;
            set
            {
                SetAndNotify(ref _desc, value);
            }
        }

        private string _code;
        public string code
        {
            get => _code;
            set
            {
                SetAndNotify(ref _code, value);
            }
        }

        private string _langType;
        public string langType
        {
            get => _langType;
            set
            {
                SetAndNotify(ref _langType, value);
            }
        }

        private bool _isFav;
        public bool isFav
        {
            get => _isFav;
            set
            {
                SetAndNotify(ref _isFav, value);
            }
        }

        private string _createdDate;
        public string createdDate
        {
            get => _createdDate;
            set
            {
                SetAndNotify(ref _createdDate, value);
            }
        }

        private bool _isEnabled;
        public bool isEnabled
        {
            get => _isEnabled;
            set => SetAndNotify(ref _isEnabled, value);
        }

        private bool _oppisEnabled;
        public bool oppisEnabled
        {
            get => _oppisEnabled;
            set => SetAndNotify(ref _oppisEnabled, value);
        }

        private string _leftText;
        public string leftText
        {
            get => _leftText;
            set
            {
                SetAndNotify(ref _leftText, value);
            }
        }

        private double _opacity1;
        public double opacity1
        {
            get => _opacity1;
            set => SetAndNotify(ref _opacity1, value);
        }

        private double _opacity2;
        public double opacity2
        {
            get => _opacity2;
            set => SetAndNotify(ref _opacity2, value);
        }
        #endregion

        #region Color
        private SolidColorBrush _mainTextColor;
        public SolidColorBrush mainTextColor
        {
            get => _mainTextColor;
            set => SetAndNotify(ref _mainTextColor, value);
        }

        private SolidColorBrush _ButtonsBackground;
        public SolidColorBrush ButtonsBackground
        {
            get => _ButtonsBackground;
            set => SetAndNotify(ref _ButtonsBackground, value);
        }

        private SolidColorBrush _textForeground;
        public SolidColorBrush textForeground
        {
            get => _textForeground;
            set => SetAndNotify(ref _textForeground, value);
        }

        private SolidColorBrush _textBackground;
        public SolidColorBrush textBackground
        {
            get => _textBackground;
            set => SetAndNotify(ref _textBackground, value);
        }

        private SolidColorBrush _codeBackground;
        public SolidColorBrush codeBackground
        {
            get => _codeBackground;
            set => SetAndNotify(ref _codeBackground, value);
        }
        #endregion

        #region Image Addrress
        private string _copyAddr;
        public string copyAddr 
        {
            get => _copyAddr;
            set => SetAndNotify(ref _copyAddr, value);
        }

        private string _favAddr;
        public string favAddr
        {
            get => _favAddr;
            set => SetAndNotify(ref _favAddr, value);
        }

        private string _backAddr;
        public string backAddr
        {
            get => _backAddr;
            set => SetAndNotify(ref _backAddr, value);
        }

        private string _editAddr;
        public string editAddr
        {
            get => _editAddr;
            set => SetAndNotify(ref _editAddr, value);
        }

        private string _closeAddr;
        public string closeAddr
        {
            get => _closeAddr;
            set => SetAndNotify(ref _closeAddr, value);
        }

        private string _saveAddr;
        public string saveAddr
        {
            get => _saveAddr;
            set => SetAndNotify(ref _saveAddr, value);
        }
        #endregion

        #region Commands

        public void FavCommand()
        {
            isFav = !isFav;
            codeModel.isFav = isFav;
            database.FavModify(codeModel);
            favAddr = isFav ? "../Resources/Images/Icons/fav.png" : themeService.IsLightTheme ? "../Resources/Images/Icons/fav_black.png" : "../Resources/Images/Icons/fav_white.png";
        }

        public void CopyCommand()
        {
            if (!string.IsNullOrEmpty(code)) Clipboard.SetText(code);
        }

        public void DelCommand()
        {
            database.DelCode(codeModel);
            BackCommand();
        }

        public void BackCommand() => pageService.Page = MainViewModel.Pages[0];

        private string BeforeEditTitle;
        private string BeforeEditDesc;
        private string BeforeEditCode;
        public void EditCommand()
        {
            BeforeEditCode = code;
            BeforeEditTitle = title;
            BeforeEditDesc = desc;

            isEnabled = true;
            oppisEnabled = !isEnabled;

            opacity1 = 0.3;
            opacity2 = 1;
        }

        public void SaveCommand()
        {
            codeModel.title = title;
            codeModel.desc = desc;
            codeModel.code = code;

            database.EditCode(codeModel);

            isEnabled = false;
            oppisEnabled = !isEnabled;

            opacity1 = 1;
            opacity2 = 0.3;
        }

        public void CancelCommand()
        {
            code = BeforeEditCode;
            title = BeforeEditTitle;
            desc = BeforeEditDesc;

            isEnabled = false;
            oppisEnabled = !isEnabled;

            opacity1 = 1;
            opacity2 = 0.3;
        }
        #endregion
    }
}
