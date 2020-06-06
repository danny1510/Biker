using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Biker.Web.Data.Entities.MotorBike
{
    public class MotorBikeEntity
    {
        public int Id { get; set; }

        [Display(Name = "NAME")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }

        //Cilindraje
        [Display(Name = "CYLINDER")]
        [Range(1, 3000, ErrorMessage = "You must select a valid cylinder.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public int Cylinder { get; set; }

        //milimetros o pulgadas
        [Display(Name = "MILLIMETERS")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public bool Millimeters { get; set; }

        //Numero llanta delantera
        [Display(Name = "#FRONT TIRE")]
        [Range(1,21, ErrorMessage = "You must select a valid front tire.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public int FrontTire { get; set; }

        //Numero llanta Trasera
        [Display(Name = "#REAR TIRE")]
        [Range(1, 21, ErrorMessage = "You must select a valid rear tire.")]
        public int RearTire { get; set; }

        //Ancho llanta delantera
        [Display(Name = "WIDTH FRONT TIRE")]
        [Range(1, 400, ErrorMessage = "You must select a valid width front tire.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public int WidthTireF { get; set; }

        //Ancho llanta Trasera
        [Display(Name = "WIDTH REAR TIRE")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a valid width rear tire.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public int WidthTireR { get; set; }

        //Perfil Llanta--> alto

        [Display(Name = "HEIGH  FRONT TIRE ")]
        [Range(1, 100, ErrorMessage = "You must select a valid heigh front tire.")]
        public int HeightTireF { get; set; }

        [Display(Name = "HEIGH REAR TIRE")]
        [Range(1, 100, ErrorMessage = "You must select a heigh rear tire.")]
        public int HeightTireR { get; set; }

        public ICollection<MotorBikeSpareEntity> MotorBikeSpares { get; set; }

        public BikeMakerEntity BikeMaker { get; set; }

        public BikeTypeEntity BikeType { get; set; }

        [Display(Name = "FRONT TIRE")]
        public string ReadNumberFrontMil => $"{WidthTireF}/{HeightTireF}-{FrontTire}";

        [Display(Name = "REAR TIRE")]
        public string ReadNumberRearMil => $"{WidthTireR}/{HeightTireR}-{RearTire}";

        [Display(Name = "FRONT TIRE")]
        public string ReadNumberFrontPul => $"{WidthTireF}-{FrontTire}";

        [Display(Name = "REAR TIRE")]
        public string ReadNumberRearPul => $"{WidthTireR}-{RearTire}";

        [Display(Name = "MOTORBIKE")]
        public string ReadnNameandCillinder => $"{Name}/{Cylinder}";

    }
}
