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
            themeService.ThemeChanged += ThemeChanged;
            pageService = _pageService;

            SetAppearance();

            title = code.title;
            desc = code.desc;
            this.code = code.code;
            langType = code.langType;
            createdDate = "Will be changed later";

            isEnabled = false;
        }
        private void ThemeChanged(object sender, EventArgs e)
        {
            SetAppearance();
        }
        private void SetAppearance()
        {

        }

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

        public void FavCommand()
        {

        }

        public void CopyCommand()
        {

        }

        public void DelCommand()
        {

        }

        public void BackCommand()
        {

        }

        public void EditCommand()
        {
            isEnabled = true;
        }

        public void SaveCommand()
        {
            isEnabled = false;
        }

        public void CancelCommand()
        {

        }
    }
}
