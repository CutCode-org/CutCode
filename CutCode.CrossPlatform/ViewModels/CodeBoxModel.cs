using CutCode.CrossPlatform.Interfaces;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CutCode.CrossPlatform.ViewModels
{
    public class CodeBoxModel : ViewModelBase
    {
        public int id { get; set; }
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

        private bool _isFav;
        public bool isFav
        {
            get => _isFav;
            set => this.RaiseAndSetIfChanged(ref _isFav, value);
        }

        public string langType { get; set; }

        private string _code;
        public string code
        {
            get => _code;
            set => this.RaiseAndSetIfChanged(ref _code, value);
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
