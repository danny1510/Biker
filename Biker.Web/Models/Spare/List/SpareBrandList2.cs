
using System.ComponentModel.DataAnnotations;

namespace Biker.Web.Models.Spare.List
{
    public class SpareBrandList2
    {

        public int Id_Brand { get; set; }

        [Display(Name = "NAME")]
        public string NameBrand { get; set; }

        [Display(Name = "IMAGE")]
        public string ImageUrlBrand { get; set; }

        public bool Is_checkedBrand { get; set; }

        

    }
}
