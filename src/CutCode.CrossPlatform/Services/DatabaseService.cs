using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CutCode.CrossPlatform.Models;
using Newtonsoft.Json;
using SQLite;

namespace CutCode.CrossPlatform.Services
{
    /// <summary>
    /// A service class for database service.
    /// </summary>
    public class DatabaseService
    {
        /// <summary>
        /// Defines a static class for using the same DatabaseService object everywhere.
        /// </summary>
        public static DatabaseService Current { get; } = new();

        /// <summary>
        /// All Codes list.
        /// </summary>
        public List<CodeModel> AllCodes { get; set; }
        
        /// <summary>
        /// All favourite codes list
        /// </summary>
        public List<CodeModel> FavCodes { get; set; }
        private SQLiteConnection _db;
        private readonly ThemeService ThemeService = ThemeService.Current;

        private string prefpath { get; set; }
        private string dbpath { get; set; }
        #region Set region
        private DatabaseService()
        {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var path = Path.Combine(appDataPath, "CutCode");
            Directory.CreateDirectory(path);
            dbpath = Path.Combine(path, "DataBase.db");
            prefpath = Path.Combine(path, "pref.json");

            if (File.Exists(prefpath))
            {
                string pref = File.ReadAllText(prefpath);
                prefModel = JsonConvert.DeserializeObject<PrefModel>(pref)!;
                Theme = prefModel.Theme;
                sortBy = prefModel.SortBy;
            }
            else
            {
                Theme = ThemeType.Light;
                sortBy = "Date";
                prefModel = new PrefModel() { Theme = Theme, SortBy = sortBy };
                UpdatePref();
            }

            ThemeService.Theme = Theme;
            OpenDB();
        }
        private void OpenDB()
        {
            _db = new SQLiteConnection(dbpath , SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.FullMutex);
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
        public ThemeType Theme;
        public string sortBy { get; set; }
        
        /// <summary>
        /// For changing the sort type.
        /// </summary>
        /// <param name="sort">
        /// The sort type.
        /// </param>
        public void ChangeSort(string sort)
        {
            prefModel.SortBy = sort;
            UpdatePref();
        }
        
        /// <summary>
        /// For changing theme.
        /// </summary>
        /// <param name="Theme">
        /// Theme theme type.
        /// </param>
        public void ChangeTheme(ThemeType Theme)
        {
            prefModel.Theme = Theme;
            ThemeService.Theme = Theme;
            UpdatePref();
        }
        private void UpdatePref()
        {
            var json = JsonConvert.SerializeObject(prefModel);
            File.WriteAllText(prefpath, json);
        }

        #endregion

        private int GetIndex(CodeModel code) => AllCodes.TakeWhile(c => c.Id != code.Id).Count();

        /// <summary>
        /// An event that fires when all codes are updated
        /// </summary>
        public event EventHandler AllCodesUpdated;
        
        /// <summary>
        /// An event that fires when all favorite codes are updated
        /// </summary>
        public event EventHandler FavCodesUpdated;
        
        
        public void PropertyChanged()
        {
            FavCodes.Clear();
            foreach (var code in AllCodes)
            {
                if (code.IsFavourite) FavCodes.Add(code);
            }

            AllCodesUpdated?.Invoke(this, EventArgs.Empty);
            FavCodesUpdated?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// For Adding a code
        /// </summary>
        /// <param name="title">
        /// The code title
        /// </param>
        /// <param name="cells">
        /// The cells in the code
        /// </param>
        /// <param name="language">
        /// The language type of the code
        /// </param>
        /// <returns>
        /// Type of CodeModel
        /// </returns>
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

        /// <summary>
        /// For editing a code.
        /// </summary>
        /// <param name="code">
        /// the code that is to be edited
        /// </param>
        /// <returns>
        /// bool type of data if the request was successful or not.
        /// </returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code">
        /// The code that's going to be deleted
        /// </param>
        /// <returns>
        /// Returns a boolean data type if the request was successful or not.
        /// </returns>
        public bool DelCode(CodeModel code)
        {
            try
            {
                _db.Delete<CodesTable>(code.Id);
                AllCodes.Remove(code);
                PropertyChanged();
            }
            catch
            {
                return false;
            }

            return true;
        }

        private List<string> AllKindsOfOrder = new List<string>()
        {
            "Alphabet", "Date", "All languages", "Python", "C++", "C#", "CSS", "Dart", "Golang",
            "Html", "Java", "Javascript", "Kotlin", "Php", "C", "Ruby", "Rust","Sql", "Swift"
        };

        /// <summary>
        /// For changing the order of codes.
        /// </summary>
        /// <param name="order">
        /// The order type
        /// </param>
        /// <param name="codes">
        /// The list of codes
        /// </param>
        /// <param name="page">
        /// The page. If it is home or fav page.
        /// </param>
        /// <returns>
        /// The re-ordered list.
        /// </returns>
        /// <exception cref="NullReferenceException">
        /// If there was null exception
        /// </exception>
        public async Task<List<CodeModel>> OrderCode(string order, List<CodeModel> codes, string page="Home")
        {
            int ind = AllKindsOfOrder.IndexOf(order);
            List<CodeModel> lst;

            if (codes == null)
            {
                throw new NullReferenceException();
            }

            if (ind > 2)
            {
                lst = new List<CodeModel>();
                foreach (var code in codes)
                {
                    if (code.Language == order) lst.Add(code);
                }
            }
            else
            {
                if (ind == 0) lst = new List<CodeModel>(codes.OrderBy(x => x.Title).ToList());
                else if (ind == 1) lst = new List<CodeModel>(codes.OrderBy(x => x.LastModificationTime).ToList());
                else lst = page=="Home" ? AllCodes : FavCodes;

                if (ind == 0 || ind == 1) ChangeSort(AllKindsOfOrder[ind]);
            }

            return lst;
        }

        /// <summary>
        /// For adding a code to favorites list.
        /// </summary>
        /// <param name="code">
        /// The code that's going to be added to the favorites list.
        /// </param>
        /// <returns>
        /// Returns boolean data that tells if the request was successfully processed or not.
        /// </returns>
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
        
        /// <summary>
        /// For searching a code
        /// </summary>
        /// <param name="text">
        /// The search text
        /// </param>
        /// <param name="from">
        /// The page
        /// </param>
        /// <returns>
        /// Returns the most relevant codes that match the search text
        /// </returns>
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
        
        /// <summary>
        /// For exporting the user data.
        /// </summary>
        /// <param name="path">
        /// The path where the data is going to be exported.
        /// </param>
        /// <returns>
        /// Returns the message if it was successful or not.
        /// </returns>
        public string ExportData(string path)
        {
            if (Path.GetExtension(path) != ".whl") return "This type of file is not supported!";
            try
            {
                File.Copy(dbpath, path);
            }
            catch
            {
                return "Unexpected error occurred!";
            }

            return "Successfully exported your codes!";
        }

        /// <summary>
        /// For importing the data.
        /// </summary>
        /// <param name="path">
        /// The path from where it is going to be imported.
        /// </param>
        /// <returns>
        /// Returns the message if it was successful or not.
        /// </returns>
        public async Task<string> ImportData(string path)
        {
            try
            {
                var currentDb = new SQLiteConnection(path);
                currentDb.CreateTable<CodesTable>();
                var codes = currentDb.Query<CodesTable>("SELECT * From CodesTable");
                if (codes.Count > 0)
                {
                    var currentCodes = _db.Query<CodesTable>("SELECT * From CodesTable");
                    foreach (var codeDb in currentCodes) _db.Delete<CodesTable>(codeDb.Id);
                    foreach (var newcode in codes) _db.Insert(newcode);
                    AllCodes = codes.Select(c => new CodeModel(c.Title,
                                                            JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(c.Cells)!, 
                                                            c.Language,
                                                            c.LastModificationTime, 
                                                            c.IsFavourite)).ToList();
                    PropertyChanged();
                    currentDb.Close();
                    return "Successfully imported your codes!";
                }
                else
                {
                    return "Your syncing file is corrupted! We are unable to sync your codes!";
                }
            }
            catch
            {
                return "Your syncing file is corrupted! We are unable to sync your codes!";
            }
        }
    }
}