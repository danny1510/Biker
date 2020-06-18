using Biker.Web.Data.Entities.Biker;
using Biker.Web.Data.Entities.MotorBike;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Biker.Web.Data.Entities
{
    public class MotorBikeSpareEntity
    {

        public int Id { get; set; }

        ////Numero llanta delantera
        //[Display(Name = "#Front Tire")]
        //[MaxLength(5, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        //public int FrontTire { get; set; }

        ////Numero llanta Trasera
        //[Display(Name = "#Rear Tire")]
        //[MaxLength(5, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        //public int RearTire { get; set; }

        ////milimetros o pulgadas
        //[Required(ErrorMessage = "The field {0} is mandatory.")]
        //public bool millimeters { get; set; }

        ////Ancho llanta delantera
        //[Display(Name = "Original Width front Tire")]
        //[MaxLength(5, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        //public int OriginalWidthTireF { get; set; }

        ////[Display(Name = "Maximum  Width front Tire")]
        ////[MaxLength(5, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        ////public int MaxWidthTireF { get; set; }

        ////[Display(Name = "Minimum  Width front Tire")]
        ////[MaxLength(5, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        ////public int MinWidthTireF { get; set; }

        ////Ancho llanta Trasera
        //[Display(Name = "Original Width Rear Tire")]
        //[MaxLength(5, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        //[Required(ErrorMessage = "The field {0} is mandatory.")]
        //public int OriginalWidthTireR { get; set; }

        ////[Display(Name = "Maximun Width Rear Tire")]
        ////[MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        ////public int MaxWidthTireR { get; set; }

        ////[Display(Name = "Minimum Width Rear Tire")]
        ////[MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        ////public int MinWidthTireR { get; set; }

        ////Perfil Llanta--> alto

        //[Display(Name = "Original Heigh Front Tire ")]
        //[MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        //public int OriginalHeightTireF { get; set; }

        //[Display(Name = "Original Heigh Rear Tire")]
        //[MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        //public int OriginalHeightTireR { get; set; }

        ////[Display(Name = "Maximum Heigh Tire")]
        ////[MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        ////public int MaxHeightTire { get; set; }

        ////[Display(Name = "Minimum Heigh Tire")]
        ////[MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        ////public int MinHeightTire { get; set; }

        //Modelo desde
        [Display(Name = "YEAR SINCE")]
        [Required]
        public int YearSince { get; set; }

        //Modelo hasta
        [Display(Name = "YEAR UNTIL")]
        [Required]
        public int YearUntil { get; set; }

        //Imagen
        [Display(Name = "IMAGE")]
        public string ImageUrl { get; set; }


        public MotorBikeEntity MotorBike { get; set; }

        public ICollection<BikerMotorEntity> BikerMotors { get; set; }

        //TODO:definir ruta
        public string ImageFullPath => string.IsNullOrEmpty(ImageUrl)
           ? null
           : $"https://TBD.net{ImageUrl.Substring(1)}";

        public string ReadUntil => YearUntil == 0
            ? "Current"
            : YearUntil.ToString();

        [Display(Name = "MODEL")]
        public string ReadModel => $"({YearSince}-{ReadUntil})";


    }
}
