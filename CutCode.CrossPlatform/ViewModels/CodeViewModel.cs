using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Avalonia;
using Avalonia.Input.Platform;
using Avalonia.Media;
using CutCode.CrossPlatform.Interfaces;
using CutCode.CrossPlatform.Models;
using CutCode.DataBase;
using ReactiveUI;

namespace CutCode.CrossPlatform.ViewModels
{
    public class CodeViewModel : ViewModelBase
    {
        private readonly IThemeService themeService;
        private readonly IDataBase database;
        public CodeBoxModel codeModel;
        public CodeViewModel(CodeBoxModel code)
        {
            themeService = AvaloniaLocator.CurrentMutable.GetService<ThemeService>();

            database = AvaloniaLocator.CurrentMutable.GetService<DataBaseManager>();

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
            mainTextColor = themeService.IsLightTheme ? Color.Parse("#0B0B13") : Color.Parse("#94969A");
            textBackground = themeService.IsLightTheme ? Color.Parse("#DADBDC") : Color.Parse("#2A2E33");
            textForeground = themeService.IsLightTheme ? Color.Parse("#1A1A1A") : Color.Parse("#F7F7F7");
            codeBackground = themeService.IsLightTheme ? Color.Parse("#E3E5E8") : Color.Parse("#2C3036");
            ButtonsBackground = themeService.IsLightTheme ? Color.Parse("#F2F2F2") : Color.Parse("#2A2C30");

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
                this.RaiseAndSetIfChanged(ref _title, value);
            }
        }

        private string _desc;
        public string desc
        {
            get => _desc;
            set
            {
                this.RaiseAndSetIfChanged(ref _desc, value);
            }
        }

        private string _code;
        public string code
        {
            get => _code;
            set
            {
                this.RaiseAndSetIfChanged(ref _code, value);
            }
        }

        private string _langType;
        public string langType
        {
            get => _langType;
            set
            {
                this.RaiseAndSetIfChanged(ref _langType, value);
            }
        }

        private bool _isFav;
        public bool isFav
        {
            get => _isFav;
            set
            {
                this.RaiseAndSetIfChanged(ref _isFav, value);
            }
        }

        private string _createdDate;
        public string createdDate
        {
            get => _createdDate;
            set
            {
                this.RaiseAndSetIfChanged(ref _createdDate, value);
            }
        }

        private bool _isEnabled;
        public bool isEnabled
        {
            get => _isEnabled;
            set => this.RaiseAndSetIfChanged(ref _isEnabled, value);
        }

        private bool _oppisEnabled;
        public bool oppisEnabled
        {
            get => _oppisEnabled;
            set => this.RaiseAndSetIfChanged(ref _oppisEnabled, value);
        }

        private string _leftText;
        public string leftText
        {
            get => _leftText;
            set
            {
                this.RaiseAndSetIfChanged(ref _leftText, value);
            }
        }

        private double _opacity1;
        public double opacity1
        {
            get => _opacity1;
            set => this.RaiseAndSetIfChanged(ref _opacity1, value);
        }

        private double _opacity2;
        public double opacity2
        {
            get => _opacity2;
            set => this.RaiseAndSetIfChanged(ref _opacity2, value);
        }
        #endregion

        #region Color
        private Color _mainTextColor;
        public Color mainTextColor
        {
            get => _mainTextColor;
            set => this.RaiseAndSetIfChanged(ref _mainTextColor, value);
        }

        private Color _ButtonsBackground;
        public Color ButtonsBackground
        {
            get => _ButtonsBackground;
            set => this.RaiseAndSetIfChanged(ref _ButtonsBackground, value);
        }

        private Color _textForeground;
        public Color textForeground
        {
            get => _textForeground;
            set => this.RaiseAndSetIfChanged(ref _textForeground, value);
        }

        private Color _textBackground;
        public Color textBackground
        {
            get => _textBackground;
            set => this.RaiseAndSetIfChanged(ref _textBackground, value);
        }

        private Color _codeBackground;
        public Color codeBackground
        {
            get => _codeBackground;
            set => this.RaiseAndSetIfChanged(ref _codeBackground, value);
        }
        #endregion

        #region Image Addrress
        private string _copyAddr;
        public string copyAddr 
        {
            get => _copyAddr;
            set => this.RaiseAndSetIfChanged(ref _copyAddr, value);
        }

        private string _favAddr;
        public string favAddr
        {
            get => _favAddr;
            set => this.RaiseAndSetIfChanged(ref _favAddr, value);
        }

        private string _backAddr;
        public string backAddr
        {
            get => _backAddr;
            set => this.RaiseAndSetIfChanged(ref _backAddr, value);
        }

        private string _editAddr;
        public string editAddr
        {
            get => _editAddr;
            set => this.RaiseAndSetIfChanged(ref _editAddr, value);
        }

        private string _closeAddr;
        public string closeAddr
        {
            get => _closeAddr;
            set => this.RaiseAndSetIfChanged(ref _closeAddr, value);
        }

        private string _saveAddr;
        public string saveAddr
        {
            get => _saveAddr;
            set => this.RaiseAndSetIfChanged(ref _saveAddr, value);
        }
        #endregion

        #region Commands

        public void FavCommand()
        {
            isFav = !isFav;
            codeModel.isFav = isFav;
            var isdone = database.FavModify(codeModel);
            if(isdone) favAddr = isFav ? "../Resources/Images/Icons/fav.png" : themeService.IsLightTheme ? "../Resources/Images/Icons/fav_black.png" : "../Resources/Images/Icons/fav_white.png";
        }

        public void CopyCommand()
        {
            if (!string.IsNullOrEmpty(code)) Application.Current.Clipboard.SetTextAsync(code);
        }

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

            var isdone = database.EditCode(codeModel);

            if (isdone)
            {
                isEnabled = false;
                oppisEnabled = !isEnabled;

                opacity1 = 1;
                opacity2 = 0.3;
            }
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
