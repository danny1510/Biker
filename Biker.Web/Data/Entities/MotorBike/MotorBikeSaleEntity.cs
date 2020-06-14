using System.ComponentModel.DataAnnotations;

namespace Biker.Web.Data.Entities.MotorBike
{
    public class MotorBikeSaleEntity
    {
        public int Id { get; set; }

        [Display(Name = "PRICE")]
        [Required]
        public decimal Price { get; set; }

        [Display(Name = "Is Available?")]
        [Required]
        public bool IsAvailable { get; set; }

        [Display(Name = "REMARKS")]
        public string Remasrks { get; set; }

        //Imagen
        [Display(Name = "IMAGE")]
        public string ImageUrl { get; set; }
        //TODO:definir ruta
        public string ImageFullPath => string.IsNullOrEmpty(ImageUrl)
           ? null
           : $"https://TBD.net{ImageUrl.Substring(1)}";


        public MotorBikeEntity MotorBike { get; set; }









    }
}
