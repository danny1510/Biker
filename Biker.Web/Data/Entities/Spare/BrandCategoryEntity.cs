using Biker.Web.Data.Entities.MotorBike;
using System.Collections.Generic;

namespace Biker.Web.Data.Entities.Spare
{
    public class BrandCategoryEntity
    {

        //Marca repuesto y categoría--> Separa las marcas por categoría
        public int Id { get; set; }

        public SpareBrandEntity SpareBrand { get; set; }

        public SpareCategoryEntity SpareCategory { get; set; }

        public ICollection<SpareEntity> Spares { get; set; }

}

}