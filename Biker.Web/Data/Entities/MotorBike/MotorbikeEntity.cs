using Microsoft.EntityFrameworkCore.Scaffolding;
using System;
using System.ComponentModel.DataAnnotations;

namespace Biker.Web.Data.Entities
{
    public class MotorbikeEntity
    {

        public int Id { get; set; }

        [Display(Name = "Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }

        //Numero llanta delantera
        [Display(Name = "#Front Tire")]
        [MaxLength(5, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public int FrontTire { get; set; }

        //Numero llanta Trasera
        [Display(Name = "#Rear Tire")]
        [MaxLength(5, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public int RearTire { get; set; }

        //Ancho maximo llanta delantera
        [Display(Name = "Maximum  Width front Tire")]
        [MaxLength(5, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public int MaxWidthTireF { get; set; }

        //Ancho minimo llanta delantera
        [Display(Name = "Minimum  Width front Tire")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public int MinWidthTireF { get; set; }

        //Ancho maximo llanta Trasera
        [Display(Name = "Maximun Width Rear Tire")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        //[Required(ErrorMessage = "The field {0} is mandatory.")]
        public int MaxWidthTireR { get; set; }

        //Ancho minimo llanta Trasera
        [Display(Name = "Minimum Width Rear Tire")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        //[Required(ErrorMessage = "The field {0} is mandatory.")]
        public int MinWidthTireR { get; set; }

        //Modelo desde
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime YearSince { get; set; }

        //Modelo hasta
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime YearUntil { get; set; }

        //Imagen
        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        //TODO:definir ruta
        public string ImageFullPath => string.IsNullOrEmpty(ImageUrl)
           ? null
           : $"https://TBD.net{ImageUrl.Substring(1)}";

        public string NameandModel => $"{Name} ({YearSince} - {YearUntil})";

        public BikeMakerEntity Maker { get; set; }

        public BikeTypeEntity MotorbikeType { get; set; }

       
    }
}
