using Stylet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
