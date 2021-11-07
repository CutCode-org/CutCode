using System;
using System.ComponentModel;
using System.IO;
using CutCode.CrossPlatform.Models;
using Newtonsoft.Json;

namespace CutCode.CrossPlatform.Interfaces
{
    public interface IDataBase
    {
        int GetCurrentThemeId();
        ColorModel? GetColorModelById(int themeId);
        bool AddTheme(ColorModel themeModel);
        bool DelTheme(int id);
    }
    
    public class DataBase : IDataBase
    {
        private int themeId;
        private string dbPath;
        private string prefPath;
        private string themesPath;
        public DataBase()
        {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var path = Path.Combine(appDataPath, "CutCode");
            
            Directory.CreateDirectory(path);
            dbPath = Path.Combine(path, "DataBase.db");
            prefPath = Path.Combine(path, "pref.json");
            
            themesPath = Path.Combine(path, "Themes");
            Directory.CreateDirectory(themesPath);
            
            if (File.Exists(prefPath))
            {
                string pref = File.ReadAllText(prefPath);
                var prefModel = JsonConvert.DeserializeObject<PrefModel>(pref);
                themeId = prefModel.themeId;
            }
            else
            {
                themeId = 0;
                var prefModel = new PrefModel() { themeId = themeId};
                UpdatePref(prefModel);
            }
        }
        
        private void UpdatePref(PrefModel prefModel)
        {
            var json = JsonConvert.SerializeObject(prefModel);
            File.WriteAllText(prefPath, json);
        }
        
        public int GetCurrentThemeId() => themeId;

        public ColorModel? GetColorModelById(int _themeId)
        {
            var currentThemePath = Path.Combine(themesPath, _themeId.ToString());
            try
            {
                return JsonConvert.DeserializeObject<ColorModel>(File.ReadAllText($"{currentThemePath}.json"));
            }
            catch(FileNotFoundException)
            {
                // load the light mode
                return JsonConvert.DeserializeObject<ColorModel>(File.ReadAllText($"0.json"));
            }
        }

        public bool AddTheme(ColorModel themeModel)
        {
            var files = new DirectoryInfo(themesPath);
            int last_ind = 0;
            foreach (var file in files.GetFiles())
            {
                try
                {
                    var file_name = int.Parse(file.Name);
                    if (file_name > last_ind) last_ind = file_name;
                }
                catch{}
            }

            int id = last_ind + 1;
            try
            {
                var json = JsonConvert.SerializeObject(themeModel);
                File.WriteAllText($"{Path.Combine(themesPath, id.ToString())}.json", json);
                
            }catch
            {
                return false;
            }
            return true;
        }

        public bool DelTheme(int id)
        {
            try
            {
                File.Delete($"{Path.Combine(themesPath, id.ToString())}.json");
            }catch
            {
                return false;
            }
            
            return true;
        }
    }
}