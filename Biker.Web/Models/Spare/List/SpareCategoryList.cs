using System.Collections.Generic;

namespace Biker.Web.Models.Spare.List
{
    public class SpareCategoryList
    {
        public int IdCategory{ get; set; }

        public string NameCategory { get; set; }

        public string ImgUrlCategory { get; set; }

        public List<SpareBrandList2> SpareBrands { get; set; }

    }
}
