using System.ComponentModel.DataAnnotations;

namespace Biker.Web.Data.Entities
{
    public class SpareEntity
    {

        //Repuestos
        public int Id { get; set; }

        [Display(Name = "Name")]
        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }

        public string Name2 { get; set; }

        public string Name3 { get; set; }

        [Display(Name = "Price")]
        public float Price { get; set; }

        [Display(Name = "discount")]
        public float Discount { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        public string ImageFullPath => string.IsNullOrEmpty(ImageUrl)
        ? null
        : $"https://TBD.net{ImageUrl.Substring(1)}";





    }
}
