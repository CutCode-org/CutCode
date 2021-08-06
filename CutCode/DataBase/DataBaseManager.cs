using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CutCode
{
    public class DataBaseManager : IDataBase
    {
        public ObservableCollection<CodeBoxModel> AllCodes { get; set; }
        private SQLiteConnection _db;
        private readonly IThemeService themeService;
        public DataBaseManager(IThemeService _themeService)
        {
            var path = "DataBase.db";
            _db = new SQLiteConnection(path);
            _db.CreateTable<CodeTable>();

            themeService = _themeService;

            AllCodes = new ObservableCollection<CodeBoxModel>();

            var codes = _db.Query<CodeTable>("SELECT * From CodeTable");
            foreach (var c in codes)
            {
                AllCodes.Add(new CodeBoxModel(c.Id, c.title, c.desc, c.isFav, c.lang, c.code, c.timestamp, themeService));
            }
        }

        private int GetIndex(CodeBoxModel code)
        {
            int ind = 0;
            foreach(var c in AllCodes)
            {
                if (c.id == code.id) break;
                ind++;
            }
            return ind;
        }

        public event EventHandler PropertyChanged;
        public void AllCodesPropertyChanged() => PropertyChanged?.Invoke(this, EventArgs.Empty);
        

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
            AllCodesPropertyChanged();

            return codeModel;
        }

        public bool EditCode(CodeBoxModel code)
        {
            try
            {
                var dbCode = _db.Query<CodeTable>("select * from CodeTable where Id = ?", code.id).FirstOrDefault();
                if(dbCode is not null)
                {
                    dbCode.title = code.title;
                    dbCode.desc = code.desc;
                    dbCode.code = code.code;
                    _db.RunInTransaction(() =>
                    {
                        _db.Update(dbCode);
                    });
                    AllCodes[GetIndex(code)] = code;
                    AllCodesPropertyChanged();
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
                AllCodesPropertyChanged();
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
        public void OrderCode(string order)
        {
            int ind = AllOrderKind.IndexOf(order);
            ObservableCollection<CodeBoxModel> lst;

            if (ind > 2) 
            {
                lst = new ObservableCollection<CodeBoxModel>();
                foreach (var code in AllCodes) if (code.langType == order) lst.Add(code);
            }
            else
            {
                if (ind == 0) lst = new ObservableCollection<CodeBoxModel>(AllCodes.OrderBy(x => x.title).ToList());
                else if(ind == 1) lst = new ObservableCollection<CodeBoxModel>(AllCodes.OrderBy(x => x.timestamp).ToList());
                else
                {
                    var codes = _db.Query<CodeTable>("SELECT * From CodeTable");
                    lst = new ObservableCollection<CodeBoxModel>();
                    foreach(var c in codes)
                    {
                        lst.Add(new CodeBoxModel(c.Id, c.title, c.desc, c.isFav, c.lang, c.code, c.timestamp, themeService));
                    }
                }
            }
            AllCodes = lst;
            AllCodesPropertyChanged();
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
                    AllCodesPropertyChanged();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public void SearchCode(string text)
        {

        }
    }
}
