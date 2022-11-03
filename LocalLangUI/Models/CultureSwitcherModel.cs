#nullable disable

using System.Collections.Generic;
using System.Globalization;

namespace LocalLangUI.Models
{
    public class CultureSwitcherModel
    {
        public CultureInfo CurrentUICulture { get; set; }
        public List<CultureInfo> SupportedCultures { get; set; }
    }
}

#nullable restore
