using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Biker.Web.Helpers
{
    public interface ICombosHelper
    {
        IEnumerable<SelectListItem> GetComboMakers();
        IEnumerable<SelectListItem> GetComboTypes();
    }
}