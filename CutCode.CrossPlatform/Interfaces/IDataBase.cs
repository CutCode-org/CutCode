using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CutCode.CrossPlatform.Models;

namespace CutCode.CrossPlatform.Interfaces
{
    public interface IDataBase
    {
        ObservableCollection<CodeModel> AllCodes { get; set; }
        ObservableCollection<CodeModel> FavCodes { get; set; }
        event EventHandler AllCodesUpdated;
        event EventHandler FavCodesUpdated;
        CodeModel AddCode(string title, List<Dictionary<string, string>> cells, string language);
        bool EditCode(CodeModel code);
        bool DelCode(CodeModel code);
        Task<ObservableCollection<CodeModel>> OrderCode(string order);
        bool FavModify(CodeModel code);
        Task<ObservableCollection<CodeModel>> SearchCode(string text, string from);

        bool isLightTheme { get; set; }
        string sortBy { get; set; }

        void ChangeSort(string sort);
        void ChangeTheme(bool IsLightTheme);

        string ExportData(string path);
        string ImportData(string path);
    }
}
