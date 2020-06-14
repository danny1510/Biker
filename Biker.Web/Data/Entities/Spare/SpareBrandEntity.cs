using Biker.Web.Data.Entities.Spare;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Biker.Web.Data.Entities
{
    public class SpareBrandEntity
    {
        //marca de repuesto
        public int Id { get; set; }

        [Display(Name = "Name")]
        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }

        public ICollection<BrandCategoryEntity> BrandCategories { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }
        public string ImageFullPath => string.IsNullOrEmpty(ImageUrl)
        ? null
        : $"https://TBD.net{ImageUrl.Substring(1)}";




    }
}
