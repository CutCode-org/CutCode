using Stylet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CutCode
{
    public class CodeViewModel : Screen
    {
        private readonly IThemeService themeService;
        private readonly IPageService pageService;
        public CodeViewModel(IThemeService _themeService, IPageService _pageService, CodeBoxModel code)
        {
            themeService = _themeService;
            pageService = _pageService;

            SetAppearance();

            title = code.title;
            desc = code.desc;
            this.code = code.code;
            langType = code.langType;
            createdDate = "Will be changed later";

            isEnabled = false;

            SetAppearance();
        }
        private void SetAppearance()
        {
            mainTextColor = themeService.IsLightTheme ? ColorCon.Convert("#0B0B13") : ColorCon.Convert("#94969A");
            textBackground = themeService.IsLightTheme ? ColorCon.Convert("#DADBDC") : ColorCon.Convert("#2A2E33");
            textForeground = themeService.IsLightTheme ? ColorCon.Convert("#1A1A1A") : ColorCon.Convert("#F7F7F7");
            codeBackground = themeService.IsLightTheme ? ColorCon.Convert("#E3E5E8") : ColorCon.Convert("#2C3036");

            copyAddr = themeService.IsLightTheme ? "../Resources/Images/Icons/copy_black.png" : "../Resources/Images/Icons/copy_white.png";
            favAddr = isFav ? "../Resources/Images/Icons/favT.png" : "../Resources/Images/Icons/favF.png";
            backAddr = themeService.IsLightTheme ? "../Resources/Images/Icons/back_black.png" : "../Resources/Images/Icons/back_white.png";
            editAddr = themeService.IsLightTheme ? "../Resources/Images/Icons/edit_black.png" : "../Resources/Images/Icons/edit_white.png";
            delAddr = "../Resources/Images/Icons/delete.png";
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
        #endregion

        #region Color
        private SolidColorBrush _mainTextColor;
        public SolidColorBrush mainTextColor
        {
            get => _mainTextColor;
            set => SetAndNotify(ref _mainTextColor, value);
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

        private string _delAddr;
        public string delAddr
        {
            get => _delAddr;
            set => SetAndNotify(ref _delAddr, value);
        }
        #endregion

        #region Commands

        public void FavCommand()
        {

        }

        public void CopyCommand()
        {

        }

        public void DelCommand()
        {
            // delete the code before going back ...
            BackCommand();
        }

        public void BackCommand() => pageService.Page = new HomeViewModel(themeService, pageService);

        public void EditCommand()
        {
            isEnabled = true;
        }

        public void SaveCommand()
        {
            // do some savings
            isEnabled = false;
        }

        public void CancelCommand()
        {
            isEnabled = false;
        }
        #endregion
    }
}
