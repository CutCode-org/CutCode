using Stylet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CutCode
{
    public class CodeBoxModel : PropertyChangedBase
    {
        public int id { get; set; }
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

        public string langType { get; set; }

        private string _code;
        public string code
        {
            get => _code;
            set => SetAndNotify(ref _code, value);
        }
        public long timestamp { get; set; }
        public IThemeService themeService { get; set; }


        public CodeBoxModel(int _id, string _title, string _desc, bool _isFav, string _langType, string _code, long _timestamp, IThemeService _themeService)
        {
            id = _id;
            title = _title;
            desc = _desc;
            isFav = _isFav;
            langType = _langType;
            code = _code;
            timestamp = _timestamp;
            themeService = _themeService;
        }
    }
}
