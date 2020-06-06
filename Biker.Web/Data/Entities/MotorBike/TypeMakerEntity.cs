using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Biker.Web.Data.Entities
{
    public class TypeMakerEntity
    {

        //Marcatipo --> maneja las imagenes de los tipos para cada marca
        public int Id { get; set; }

        [Display(Name = "IMAGE")]
        public string ImageUrl { get; set; }

        //TODO:definir ruta
        public string ImageFullPath => string.IsNullOrEmpty(ImageUrl)
           ? null
           : $"https://TBD.net{ImageUrl.Substring(1)}";

        public BikeMakerEntity Maker { get; set; }
        public BikeTypeEntity MotorbikeType { get; set; }


    }
}
