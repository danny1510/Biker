using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Biker.Web.Data.Entities
{
    public class BikeTypeEntity
    {
        //tipo de moto

        public int Id { get; set; }

        [Display(Name = "Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }

        public ICollection<MotorbikeEntity> Motorbikes { get; set; }

        public ICollection<TypeMakerEntity> TypeBrands { get; set; }



    }
}
