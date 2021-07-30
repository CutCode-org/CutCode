using Stylet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CutCode
{
    public class CodeBoxModel : Screen
    {
        public CodeBoxModel(string __title, string __desc, bool __isFav, string __langType, IThemeService _themeService)
        {
            title = __title;
            desc = __desc;
            isFav = __isFav;
            langType = __langType;
            themeService = _themeService;
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

        private bool _isFav;
        public bool isFav
        {
            get => _isFav;
            set => SetAndNotify(ref _isFav, value);
        }

        private string _langType;
        public string langType
        {
            get => _langType;
            set => SetAndNotify(ref _langType, value);
        }

        public IThemeService themeService { get; set; }
    }
}
