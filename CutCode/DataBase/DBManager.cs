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
        public DBManager()
        {

        }

        public static CodeBoxModel AddCode(string _title, string _desc, string _lang, string _code)
        {
            var _id = 0; // will be set later ...
            return new CodeBoxModel(_title, _desc, false, _lang);
        }

        public static bool DelCode(CodeBoxModel code)
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

        public static List<CodeBoxModel> SearchCode(string searchText)
        {
            return new List<CodeBoxModel>();
        }

        public static bool EditCode(CodeBoxModel newCode)
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
