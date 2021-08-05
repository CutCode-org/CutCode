using Stylet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CutCode
{
    public class CodeBoxModel
    {
        public CodeBoxModel(int _id, string _title, string _desc, bool _isFav, string _langType, string _code, long _created, IThemeService _themeService)
        {
            id = _id;
            title = _title;
            desc = _desc;
            isFav = _isFav;
            langType = _langType;
            code = _code;
            // do something with the time long here ...
            dateCreated = "12:01 AM";

            themeService = _themeService;
            // there are something to be changed in the initializer ...
        }
        public int id { get; set; }
        public string title { get; set; }
        public string desc { get; set; }
        public bool isFav { get; set; }
        public  string langType { get; set; }
        public string code { get; set; }
        public long created { get; set; }
        public string dateCreated { get; set; }
        public IThemeService themeService { get; set; }
    }
}
