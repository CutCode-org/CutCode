using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CutCode.CrossPlatform.Models;

namespace CutCode.CrossPlatform.Interfaces
{
    public interface IDataBase
    {
        List<CodeModel> AllCodes { get; set; }
        List<CodeModel> FavCodes { get; set; }
        event EventHandler AllCodesUpdated;
        event EventHandler FavCodesUpdated;
        CodeModel AddCode(string title, List<Dictionary<string, string>> cells, string language);
        bool EditCode(CodeModel code);
        bool DelCode(CodeModel code);
        Task<List<CodeModel>> OrderCode(string order, List<CodeModel> codes, string page);
        bool FavModify(CodeModel code);
        Task<List<CodeModel>> SearchCode(string text, string from);

        bool isLightTheme { get; set; }
        string sortBy { get; set; }

        void ChangeSort(string sort);
        void ChangeTheme(bool IsLightTheme);

        string ExportData(string path);
        Task<string> ImportData(string path);
    }
}
