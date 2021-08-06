using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CutCode
{
    public interface IDataBase
    {
        ObservableCollection<CodeBoxModel> AllCodes { get; set; }
        event EventHandler PropertyChanged;
        CodeBoxModel AddCode(string title, string desc, string code, string langType);
        bool EditCode(CodeBoxModel code);
        bool DelCode(CodeBoxModel code);
        void OrderCode(string order);
        bool FavModify(CodeBoxModel code);
        void SearchCode(string text);
    }
}
