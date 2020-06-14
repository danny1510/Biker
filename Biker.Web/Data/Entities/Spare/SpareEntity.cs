using Biker.Web.Data.Entities.Provider;
using Biker.Web.Data.Entities.Spare;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Biker.Web.Data.Entities
{
    public class SpareEntity
    {

        //Repuestos
        public int Id { get; set; }

        [Display(Name = "CODE")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Code { get; set; }

        [Display(Name = "Name")]
        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }

        public string Name1 { get; set; }

        public string Name2 { get; set; }

        [Display(Name = "PRICE")]
        public float Price { get; set; }

        [Display(Name = "LAST UPDATE")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime LastUpdate { get; set; }

        [Display(Name = "DISCOUNT")]
        public float Discount { get; set; }

        [Display(Name = "IMAGE")]
        public string ImageUrl { get; set; }

        public ICollection<SpareProviderEntity> SpareProviders { get; set; }
        public BrandCategoryEntity BrandCategory { get; set; }

        [Display(Name = "LAST UPDATE")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime LastUpdateLocal => LastUpdate.ToLocalTime();

        public string ImageFullPath => string.IsNullOrEmpty(ImageUrl)
        ? null
        : $"https://TBD.net{ImageUrl.Substring(1)}";


    }
}
