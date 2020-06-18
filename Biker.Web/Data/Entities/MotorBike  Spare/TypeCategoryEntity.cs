using Biker.Web.Data.Entities.MotorBike;
using System.ComponentModel.DataAnnotations;

namespace Biker.Web.Data.Entities.MotorBike__Spare
{
    public class TypeCategoryEntity
    {
        public int Id { get; set; }

        [Display(Name = "IMAGE")]
        public string ImageUrl { get; set; }

        //TODO:definir ruta
        public string ImageFullPath => string.IsNullOrEmpty(ImageUrl)
           ? null
           : $"https://TBD.net{ImageUrl.Substring(1)}";

        public BikeTypeEntity BikeType { get; set; }

        public SpareCategoryEntity SpareCategory { get; set; }

    }
}
