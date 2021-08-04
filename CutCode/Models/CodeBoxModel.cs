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
        public CodeBoxModel(string __title, string __desc, bool __isFav, string __langType)
        {
            title = __title;
            desc = __desc;
            isFav = __isFav;
            langType = __langType;
            // there are something to be changed in the initializer ...
        }
        public int id { get; set; }
        public string title { get; set; }
        public string desc { get; set; }
        public bool isFav { get; set; }
        public  string langType { get; set; }
        public string code { get; set; }
        public long created { get; set; }
    }
}
