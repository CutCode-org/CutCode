using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Security.AccessControl;
using System.IO.Compression;
using Avalonia;
using CutCode.CrossPlatform.Interfaces;
using CutCode.CrossPlatform.Models;
using ReactiveUI;

namespace CutCode.DataBase
{
    public class DataBaseManager : IDataBase
    {
        private static DataBaseManager _dataBase = new DataBaseManager();
        public static DataBaseManager Current => _dataBase;
        
        public List<CodeModel> AllCodes { get; set; }
        public List<CodeModel> FavCodes { get; set; }
        private SQLiteConnection _db;
        private readonly IThemeService themeService = ThemeService.Current;

        private string prefpath { get; set; }
        private string dbpath { get; set; }
        #region Set region
        private DataBaseManager()
        {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var path = Path.Combine(appDataPath, "CutCode");
            Directory.CreateDirectory(path);
            dbpath = Path.Combine(path, "DataBase.db");
            prefpath = Path.Combine(path, "pref.json");

            if (File.Exists(prefpath))
            {
                string pref = File.ReadAllText(prefpath);
                prefModel = JsonConvert.DeserializeObject<PrefModel>(pref);
                isLightTheme = prefModel.IsLightTheme;
                sortBy = prefModel.SortBy;
            }
            else
            {
                isLightTheme = true;
                sortBy = "Date";
                prefModel = new PrefModel() { IsLightTheme = isLightTheme, SortBy = sortBy };
                UpdatePref();
            }

            themeService.IsLightTheme = isLightTheme;
            OpenDB();
        }
        private void OpenDB()
        {
            _db = new SQLiteConnection(dbpath);
            _db.CreateTable<CodesTable>();

            AllCodes = new List<CodeModel>();

            var codes = _db.Query<CodesTable>("SELECT * From CodesTable");
            foreach (var c in codes)
            {
                if (c.Cells == null) continue;
                var cells = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(c.Cells);
                var code = new CodeModel(c.Title, cells, c.Language, c.LastModificationTime, c.IsFavourite);
                code.SetId(c.Id);
                AllCodes.Add(code);
            }

            var lst = new List<CodeModel>();
            foreach (var code in AllCodes)
            {
                if (code.IsFavourite) lst.Add(code);
            }
            FavCodes = lst;

            PropertyChanged();
        }
        #endregion

        #region Preference region

        private PrefModel prefModel = new PrefModel();
        public bool isLightTheme { get; set; }
        public string sortBy { get; set; }

        public void ChangeSort(string sort)
        {
            prefModel.SortBy = sort;
            UpdatePref();
        }
        public void ChangeTheme(bool IsLightTheme)
        {
            prefModel.IsLightTheme = IsLightTheme;
            themeService.IsLightTheme = IsLightTheme;
            UpdatePref();
        }
        private void UpdatePref()
        {
            var json = JsonConvert.SerializeObject(prefModel);
            File.WriteAllText(prefpath, json);
        }

        #endregion

        private int GetIndex(CodeModel code) => AllCodes.TakeWhile(c => c.Id != code.Id).Count();

        public event EventHandler AllCodesUpdated;
        public event EventHandler FavCodesUpdated;
        public void PropertyChanged()
        {
            var lst = new List<CodeModel>();
            foreach (var code in AllCodes)
            {
                if (code.IsFavourite) lst.Add(code);
            }
            FavCodes = lst;

            AllCodesUpdated?.Invoke(this, EventArgs.Empty);
            FavCodesUpdated?.Invoke(this, EventArgs.Empty);
        }

        public CodeModel AddCode(string title, List<Dictionary<string, string>> cells, string language)
        {
            var lastModificationTime = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            var code = new CodeModel(title, cells, language, lastModificationTime, false);
            var codeTable = new CodesTable
            {
                Title = code.Title,
                Cells = code.Cells,
                Language = code.Language,
                IsFavourite = code.IsFavourite,
                LastModificationTime = code.LastModificationTime
            };
            _db.Insert(codeTable);

            var id = (int)SQLite3.LastInsertRowid(_db.Handle);
            code.SetId(id);
            
            AllCodes.Add(code);
            PropertyChanged();

            return code;
        }

        public bool EditCode(CodeModel code)
        {
            try
            {
                var dbCode = _db.Query<CodesTable>("select * from CodesTable where Id = ?", code.Id).FirstOrDefault();
                if (dbCode is not null)
                {
                    dbCode.Cells = code.Cells;
                    _db.RunInTransaction(() =>
                    {
                        _db.Update(dbCode);
                    });
                    AllCodes[AllCodes.FindIndex(c => c.Id == dbCode.Id)] = code;
                    PropertyChanged();
                    return true;
                }
                else return false;
            }
            catch
            {
                return false;
            }
        }

        public bool DelCode(CodeModel code)
        {
            _db.Delete<CodesTable>(code.Id);
            AllCodes.Remove(code);
            PropertyChanged();
            /*
            try
            {
            }
            catch
            {
                return false;
            */
            return true;
        }

        private List<string> AllKindsOfOrder = new List<string>()
        {
            "Alphabet", "Date", "All languages", "Python", "C++", "C#", "CSS", "Dart", "Golang",
            "Html", "Java", "Javascript", "Kotlin", "Php", "C", "Ruby", "Rust","Sql", "Swift"
        };

        public async Task<List<CodeModel>> OrderCode(string order, List<CodeModel> codes = null)
        {
            int ind = AllKindsOfOrder.IndexOf(order);
            List<CodeModel> lst;

            var currentCodes = codes == null ? AllCodes : codes;

            if (ind > 2)
            {
                lst = new List<CodeModel>();
                foreach (var code in currentCodes)
                {
                    if (code.Language == order) lst.Add(code);
                }
            }
            else
            {
                if (ind == 0) lst = new List<CodeModel>(currentCodes.OrderBy(x => x.Title).ToList());
                else if (ind == 1) lst = new List<CodeModel>(currentCodes.OrderBy(x => x.LastModificationTime).ToList());
                else lst = AllCodes;

                if (ind == 0 || ind == 1) ChangeSort(AllKindsOfOrder[ind]);
            }

            return lst;
        }

        public bool FavModify(CodeModel code)
        {
            try
            {
                var dbCode = _db.Query<CodesTable>("select * from CodesTable where Id = ?", code.Id).FirstOrDefault();
                if (dbCode is not null)
                {
                    dbCode.IsFavourite = code.IsFavourite;
                    _db.RunInTransaction(() =>
                    {
                        _db.Update(dbCode);
                    });
                    AllCodes[GetIndex(code)] = code;
                    PropertyChanged();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
        public async Task<List<CodeModel>> SearchCode(string text, string from)
        {
            var currentCode = from == "Home" ? AllCodes : FavCodes;
            var newCodesList = currentCode.Where(x => x.Title.ToLower().Contains(text.ToLower())).ToList();
            var newCodes = new List<CodeModel>();
            foreach (var code in newCodesList)
            {
                newCodes.Add(code);
            }
            return newCodes;
        }

        public string ExportData(string path)
        {
            if (Path.GetExtension(path) != ".whl") return "This type of file are not supported!";
            _db.Close();
            var bytes = File.ReadAllBytes(dbpath);
            File.WriteAllBytes(path, bytes);
            OpenDB();

            return "Successfully exported your codes!";
        }

        public string ImportData(string path)
        {
            _db.Close();
            var currentData = File.ReadAllBytes(dbpath);

            var importingData = File.ReadAllBytes(path);
            File.WriteAllBytes(dbpath, importingData);
            try
            {
                OpenDB();
            }
            catch
            {
                _db.Close();
                File.WriteAllBytes(dbpath, currentData);
                OpenDB();
                return "Your syncing file is corrupted! We are unable to sync your codes!";
            }
            return "Successfully imported your codes!";
        }
    }
}
