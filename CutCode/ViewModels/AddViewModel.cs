using Stylet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CutCode
{
    public class AddViewModel : Screen
    {
        private readonly IThemeService themeService;
        public AddViewModel(IThemeService _themeService)
        {
            themeService = _themeService;
            themeService.ThemeChanged += ThemeChanged;

            AllLangs = new ObservableCollection<string>()
            {
            "Any languages", "Python", "C++", "C#", "CSS", "Dart", "Golang", "Html", "Java",
            "Javascript", "Kotlin", "Php", "C", "Ruby", "Rust","Sql", "Swift"
            };
            leftText = "";
            CurrentLang = AllLangs[0];
            SetAppearance();
        }
        private void ThemeChanged(Object sender, EventArgs e)
        {
            SetAppearance();
        }

        private void SetAppearance()
        {
            //  40444B
            textBoxBackground = themeService.IsLightTheme ? ColorCon.Convert("#DADBDC") : ColorCon.Convert("#2A2E33");
            textBoxForeground = themeService.IsLightTheme ? ColorCon.Convert("#1A1A1A") : ColorCon.Convert("#F7F7F7");
            richtextBoxBackground = themeService.IsLightTheme ? ColorCon.Convert("#E3E5E8") : ColorCon.Convert("#2C3036");
            btnHoverColor = themeService.IsLightTheme ? ColorCon.Convert("#D0D1D2") : ColorCon.Convert("#373737");
        }

        public IList<string> AllLangs { get; set; }

        private SolidColorBrush _textBoxBackground;
        public SolidColorBrush textBoxBackground
        {
            get => _textBoxBackground;
            set => SetAndNotify(ref _textBoxBackground, value);
        }

        private SolidColorBrush _textBoxForeground;
        public SolidColorBrush textBoxForeground
        {
            get => _textBoxForeground;
            set => SetAndNotify(ref _textBoxForeground, value);
        }

        private SolidColorBrush _richtextBoxBackground;
        public SolidColorBrush richtextBoxBackground
        {
            get => _richtextBoxBackground;
            set => SetAndNotify(ref _richtextBoxBackground, value);
        }

        private SolidColorBrush _btnHoverColor;
        public SolidColorBrush btnHoverColor
        {
            get => _btnHoverColor;
            set => SetAndNotify(ref _btnHoverColor, value);
        }

        private string _title;
        public string title
        {
            get => _title;
            set => SetAndNotify(ref _title, value);
        }

        private string _desc;
        public string desc
        {
            get => _desc;
            set => SetAndNotify(ref _desc, value);
        }

        private string _code;
        public string code
        {
            get => _code;
            set => SetAndNotify(ref _code, value);
        }

        private string _leftText;
        public string leftText
        {
            get => _leftText;
            set => SetAndNotify(ref _leftText, value);
        }

        private string _CurrentLang;
        public string CurrentLang
        {
            get => _CurrentLang;
            set
            {
                SetAndNotify(ref _CurrentLang, SyntaxLanguages[AllLangs.IndexOf(value)]);
            }
        }

        private List<string> SyntaxLanguages = new List<string>()
        {
        "text", "Python", "C++", "C#", "CSS", "C#", "Java", "CSS", "Java",
        "Java", "Java", "C++", "C++", "C#", "C++","C++", "Java"
        };

        public void DoneClicked()
        {
            if (string.IsNullOrEmpty(title))
            {
                leftText = "Title should not be emtpy";
            }
            else if (string.IsNullOrEmpty(code))
            {
                leftText = "Please add a code";
            }
            else
            {
                // done lets add the code ...
            }
        }
    }
}
