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

namespace CutCode.DataBase
{
    public class DataBaseManager : IDataBase
    {
        public ObservableCollection<CodeBoxModel> AllCodes { get; set; }
        public ObservableCollection<CodeBoxModel> FavCodes { get; set; }
        private SQLiteConnection _db;
        private readonly IThemeService themeService;

        private string prefpath { get; set; }
        private string dbpath { get; set; }
        #region Set region
        public DataBaseManager()
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

            themeService = AvaloniaLocator.CurrentMutable.GetService<ThemeService>();
            themeService.IsLightTheme = isLightTheme;
            OpenDB();
        }
        private void OpenDB()
        {
            _db = new SQLiteConnection(dbpath);
            _db.CreateTable<CodeTable>();

            AllCodes = new ObservableCollection<CodeBoxModel>();

            var codes = _db.Query<CodeTable>("SELECT * From CodeTable");
            foreach (var c in codes)
            {
                AllCodes.Add(new CodeBoxModel(c.Id, c.title, c.desc, c.isFav, c.lang, c.code, c.timestamp, themeService));
            }

            var lst = new ObservableCollection<CodeBoxModel>();
            foreach (var code in AllCodes)
            {
                if (code.isFav) lst.Add(code);
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

        private int GetIndex(CodeBoxModel code)
        {
            int ind = 0;
            foreach (var c in AllCodes)
            {
                if (c.id == code.id) break;
                ind++;
            }
            return ind;
        }

        public event EventHandler AllCodesUpdated;
        public event EventHandler FavCodesUpdated;
        public void PropertyChanged()
        {
            var lst = new ObservableCollection<CodeBoxModel>();
            foreach (var code in AllCodes)
            {
                if (code.isFav) lst.Add(code);
            }
            FavCodes = lst;

            AllCodesUpdated?.Invoke(this, EventArgs.Empty);
            FavCodesUpdated?.Invoke(this, EventArgs.Empty);
        }

        public CodeBoxModel AddCode(string title, string desc, string code, string langType)
        {
            var time = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();

            var dbCode = new CodeTable
            {
                title = title,
                desc = desc,
                code = code,
                lang = langType,
                isFav = false,
                timestamp = time
            };
            _db.Insert(dbCode);

            int id = (int)SQLite3.LastInsertRowid(_db.Handle);
            var codeModel = new CodeBoxModel(id, title, desc, false, langType, code, time, themeService);
            AllCodes.Add(codeModel);
            PropertyChanged();

            return codeModel;
        }

        public bool EditCode(CodeBoxModel code)
        {
            try
            {
                var dbCode = _db.Query<CodeTable>("select * from CodeTable where Id = ?", code.id).FirstOrDefault();
                if (dbCode is not null)
                {
                    dbCode.title = code.title;
                    dbCode.desc = code.desc;
                    dbCode.code = code.code;
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

        public bool DelCode(CodeBoxModel code)
        {
            try
            {
                _db.Delete<CodeTable>(code.id);
                AllCodes.Remove(code);
                PropertyChanged();
            }
            catch
            {
                return false;
            }
            return true;
        }

        private List<string> AllOrderKind = new List<string>()
        {
            "Alphabet", "Date", "All languages", "Python", "C++", "C#", "CSS", "Dart", "Golang",
            "Html", "Java", "Javascript", "Kotlin", "Php", "C", "Ruby", "Rust","Sql", "Swift"
        };

        public async Task<ObservableCollection<CodeBoxModel>> OrderCode(string order)
        {
            int ind = AllOrderKind.IndexOf(order);
            ObservableCollection<CodeBoxModel> lst;

            var currentCodes = AllCodes;

            if (ind > 2)
            {
                lst = new ObservableCollection<CodeBoxModel>();
                foreach (var code in currentCodes)
                {
                    if (code.langType == order) lst.Add(code);
                }
            }
            else
            {
                if (ind == 0) lst = new ObservableCollection<CodeBoxModel>(currentCodes.OrderBy(x => x.title).ToList());
                else if (ind == 1) lst = new ObservableCollection<CodeBoxModel>(currentCodes.OrderBy(x => x.timestamp).ToList());
                else lst = AllCodes;

                if (ind == 0 || ind == 1) ChangeSort(AllOrderKind[ind]);
            }

            return lst;
        }

        public bool FavModify(CodeBoxModel code)
        {
            try
            {
                var dbCode = _db.Query<CodeTable>("select * from CodeTable where Id = ?", code.id).FirstOrDefault();
                if (dbCode is not null)
                {
                    dbCode.isFav = code.isFav;
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
        public async Task<ObservableCollection<CodeBoxModel>> SearchCode(string text, string from)
        {
            var currentCode = from == "Home" ? AllCodes : FavCodes;
            var newCodesList = currentCode.Where(x => x.title.ToLower().Contains(text.ToLower())).ToList();
            var newCodes = new ObservableCollection<CodeBoxModel>();
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
