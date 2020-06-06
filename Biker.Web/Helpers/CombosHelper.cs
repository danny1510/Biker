using Biker.Web.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace Biker.Web.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _dataContext;

        public CombosHelper(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IEnumerable<SelectListItem> GetComboTypes()
        {
            var list = _dataContext.BikeTypes.Select(t => new SelectListItem
            {
                Text = t.Name,
                Value = $"{t.Id}"

            }).OrderBy(t => t.Text)
              .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Select  a Motorbike Type...]",
                Value = "0"
            });

            return (list);

        }

        public IEnumerable<SelectListItem> GetComboMakers()
        {
            var list = _dataContext.BikeMakers.Select(m => new SelectListItem
            {
                Text = m.Name,
                Value = $"{m.Id}"

            }).OrderBy(t => t.Text)
              .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Select  a Motorbike Marker...]",
                Value = "0"
            });
            return (list);
        }


    }
}
