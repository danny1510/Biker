using Biker.Web.Data.Entities.MotorBike;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Biker.Web.Data.Entities
{
    public class BikeMakerEntity
    {
        //Marca de la moto
        public int Id { get; set; }

        [Display(Name = "MAKER")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }

        [Display(Name = "IMAGE")]
        public string ImageUrl { get; set; }

        //TODO:definir ruta
        public string ImageFullPath => string.IsNullOrEmpty(ImageUrl)
           ? null
           : $"https://TBD.net{ImageUrl.Substring(1)}";

        public ICollection<MotorBikeEntity>Motorbikes { get; set; }
        public ICollection<TypeMakerEntity> TypeMaker { get; set; }






    }
}
