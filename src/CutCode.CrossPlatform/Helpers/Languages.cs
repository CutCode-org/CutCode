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
            {"Go", IconPaths.Go},
            {"HTML", IconPaths.Html},
            {"Java", IconPaths.Java},
            {"JavaScript", IconPaths.Js},
            {"Kotlin", IconPaths.Kotlin},
            {"PHP", IconPaths.Php},
            {"C", IconPaths.C},
            {"Ruby", IconPaths.Ruby},
            {"Rust", IconPaths.Rust},
            {"SQL", IconPaths.Sql},
            {"Swift", IconPaths.Swift}
        };
    }
}