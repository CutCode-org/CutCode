using Stylet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CutCode
{
    public class CodeViewModel : Screen
    {
        private readonly IThemeService themeService;
        public CodeViewModel(IThemeService _themeService)
        {
            themeService = _themeService;
        }
    }
}
