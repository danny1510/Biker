using Biker.Web.Data;
using Biker.Web.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Biker.Web.Helpers
{
    public class CheckHelper : ICheckHelper
    {
        private readonly DataContext _dataContext;

        public CheckHelper(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public List<SpareBrandEntity> GetListBrands()
        {
            var list = _dataContext.SpareBrands.ToList();
        
            return list;

        }




    }

    
}
