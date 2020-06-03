using System.ComponentModel.DataAnnotations;

namespace Biker.Web.Data.Entities.MotorBike
{
    //categoria del repuesto
    public class SpareCategoryEntity
    {

        public int Id { get; set; }

        [Display(Name = "Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        public string ImageFullPath => string.IsNullOrEmpty(ImageUrl)
           ? null
           : $"https://TBD.net{ImageUrl.Substring(1)}";




    }
}
