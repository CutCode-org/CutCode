using System.Collections.Generic;
using System.Linq;
using CutCode.CrossPlatform.Services;
using TextMateSharp.Grammars;

namespace CutCode.CrossPlatform.Helpers
{
    public static class Languages
    {
        private static RegistryOptions _registryOptions => new(ThemeService.Current.Theme == ThemeType.Light
            ? ThemeName.LightPlus
            : ThemeName.DarkPlus);
        public static string GetLanguagePath(Language language)
        {
            try
            {
                return LanguagesDict[language.Aliases[0]];
            }
            catch
            {
                return LanguagesDict["File"];
            }
        }
        private static Dictionary<string, string> LanguagesDict = new Dictionary<string, string>()
        {
            {"File", IconPaths.File},
            {"Python", IconPaths.Python},
            {"C++", IconPaths.Cpp},
            {"C#", IconPaths.CSharp},
            {"CSS", IconPaths.Css},
            {"Dart", IconPaths.Dart},
            {"Golang", IconPaths.Go},
            {"Html", IconPaths.Html},
            {"Java", IconPaths.Java},
            {"Javascript", IconPaths.Js},
            {"Kotlin", IconPaths.Kotlin},
            {"Php", IconPaths.Php},
            {"C", IconPaths.C},
            {"Ruby", IconPaths.Ruby},
            {"Rust", IconPaths.Rust},
            {"Sql", IconPaths.Sql},
            {"Swift", IconPaths.Swift}
        };
    }
}