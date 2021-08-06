using Stylet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CutCode
{
    public class CodeBoxModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int id { get; set; }
        private string _title { get; set; }
        public string title
        {
            get => _title;
            set
            {
                if(_title != value)
                {
                    _title = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _desc { get; set; }
        public string desc
        {
            get => _desc;
            set
            {
                if (_desc != value)
                {
                    _desc = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private bool _isFav { get; set; }
        public bool isFav
        {
            get => _isFav;
            set
            {
                if (_isFav != value)
                {
                    _isFav = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string langType { get; set; }

        private string _code { get; set; }
        public string code
        {
            get => _code;
            set
            {
                if (_code != value)
                {
                    _code = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public long timestamp { get; set; }
        public string date { get; set; }
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
            date = DateTimeOffset.FromUnixTimeSeconds(timestamp).LocalDateTime.ToString("d MMM yyyy");
        }
    }
}
