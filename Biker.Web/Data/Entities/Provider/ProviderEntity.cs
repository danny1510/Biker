using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Biker.Web.Data.Entities.Provider
{
    public class ProviderEntity
    {

        //Proveedor
        public int Id { get; set; }

        [Display(Name = "TYPE")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }

        public ICollection<SpareProviderEntity> spareProviders { get; set; }
        public ICollection<ProviderDetailsEntity> ProviderDetails { get; set; }

        //Imagen
        [Display(Name = "IMAGE")]
        public string ImageUrl { get; set; }
        //TODO:definir ruta
        public string ImageFullPath => string.IsNullOrEmpty(ImageUrl)
           ? null
           : $"https://TBD.net{ImageUrl.Substring(1)}";



    }
}
