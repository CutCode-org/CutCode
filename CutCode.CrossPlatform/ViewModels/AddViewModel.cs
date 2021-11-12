using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Media;
using CutCode.CrossPlatform.Interfaces;
using CutCode.CrossPlatform.ViewModels;
using CutCode.DataBase;
using ReactiveUI;

namespace CutCode.CrossPlatform.ViewModels
{
    public class AddViewModel : PageBaseViewModel
    {
        private IDataBase database => DataBase;
        public AddViewModel()
        {
            AllLangs = new ObservableCollection<string>()
            {
                "Any language", "Python", "C++", "C#", "CSS", "Dart", "Golang", "Html", "Java",
                "Javascript", "Kotlin", "Php", "C", "Ruby", "Rust","Sql", "Swift"
            };
            leftText = "";
        }

        protected override void OnLightThemeIsSet()
        {
            textBoxBackground = Color.Parse("#DADBDC");
            textBoxForeground = Color.Parse("#1A1A1A");
            richtextBoxBackground = Color.Parse("#E3E5E8");
            btnHoverColor = Color.Parse("#D0D1D2");
        }

        protected override void OnDarkThemeIsSet()
        {
            textBoxBackground = Color.Parse("#2A2E33");
            textBoxForeground = Color.Parse("#F7F7F7");
            richtextBoxBackground = Color.Parse("#2C3036");
            btnHoverColor = Color.Parse("#373737");
        }

        public IList<string> AllLangs { get; set; }

        private Color _textBoxBackground;
        public Color textBoxBackground
        {
            get => _textBoxBackground;
            set => this.RaiseAndSetIfChanged(ref _textBoxBackground, value);
        }

        private Color _textBoxForeground;
        public Color textBoxForeground
        {
            get => _textBoxForeground;
            set => this.RaiseAndSetIfChanged(ref _textBoxForeground, value);
        }

        private Color _richtextBoxBackground;
        public Color richtextBoxBackground
        {
            get => _richtextBoxBackground;
            set => this.RaiseAndSetIfChanged(ref _richtextBoxBackground, value);
        }

        private Color _btnHoverColor;
        public Color btnHoverColor
        {
            get => _btnHoverColor;
            set => this.RaiseAndSetIfChanged(ref _btnHoverColor, value);
        }

        private string _title;
        public string title
        {
            get => _title;
            set => this.RaiseAndSetIfChanged(ref _title, value);
        }

        private string _desc;
        public string desc
        {
            get => _desc;
            set => this.RaiseAndSetIfChanged(ref _desc, value);
        }

        private string _code;
        public string code
        {
            get => _code;
            set => this.RaiseAndSetIfChanged(ref _code, value);
        }

        private string _leftText;
        public string leftText
        {
            get => _leftText;
            set => this.RaiseAndSetIfChanged(ref _leftText, value);
        }

        private int ind;

        private string _CurrentLang;
        public string CurrentLang
        {
            get => _CurrentLang;
            set
            {
                ind = AllLangs.IndexOf(value);
                this.RaiseAndSetIfChanged(ref _CurrentLang, SyntaxLanguages[ind]);
            }
        }

        private List<string> SyntaxLanguages = new List<string>()
        {
        "text", "Python", "C++", "C#", "CSS", "C#", "Java", "CSS", "Java",
        "JavaScript", "Java", "C++", "C++", "C#", "C++","C++", "Java"
        };

        public void SaveCommand()
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
                leftText = "";
                var codeModel = database.AddCode(title, desc, code, AllLangs[ind]);

                /*
                pageService.remoteChange = "Home";
                pageService.Page = new CodeViewModel(themeService, database, codeModel);
                */
            }
        }
    }
}
