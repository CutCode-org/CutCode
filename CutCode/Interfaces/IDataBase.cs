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
        ObservableCollection<CodeBoxModel> FavCodes { get; set; }
        event EventHandler AllCodesUpdated;
        event EventHandler FavCodesUpdated;
        CodeBoxModel AddCode(string title, string desc, string code, string langType);
        bool EditCode(CodeBoxModel code);
        bool DelCode(CodeBoxModel code);
        Task<ObservableCollection<CodeBoxModel>> OrderCode(string order);
        bool FavModify(CodeBoxModel code);
        Task<ObservableCollection<CodeBoxModel>> SearchCode(string text, string from);

        bool isLightTheme { get; set; }
        string sortBy { get; set; }

        void ChangeSort(string sort);
        void ChangeTheme(bool IsLightTheme);
    }
}
