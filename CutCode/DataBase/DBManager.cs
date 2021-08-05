using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CutCode
{
    public class DBManager
    {
        public List<CodeBoxModel> AllCodes { get; set; }
        private readonly IThemeService themeService;
        public DBManager(IThemeService _themeService)
        {
            themeService = _themeService;
        }

        public CodeBoxModel AddCode(string _title, string _desc, string _lang, string _code)
        {
            ///var _id = 0; // will be set later ...
            return new CodeBoxModel(2, "C++ binary search", "blah blah binary blah ... ye and ok \n so what??", false, "cpp", "print('Hello world')", 1628136352, themeService);
        }

        public bool DelCode(CodeBoxModel code)
        {
            try
            {
                // delete the code from db here
            }
            catch
            {
                return false;
            }
            return true;
        }

        public List<CodeBoxModel> SearchCode(string searchText)
        {
            return new List<CodeBoxModel>();
        }

        public bool EditCode(CodeBoxModel newCode)
        {
            try
            {
                // edit it here
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
