using System.ComponentModel.DataAnnotations;

namespace Biker.Web.Data.Entities.Provider
{
    public class ProviderDetailsEntity
    {
        public int Id { get; set; }

        [Display(Name = "TYPE")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }

        public ProviderEntity Provider { get; set; }

        //Falta Ciudad
        [Display(Name = "DIRECCIÓN")]
        public string Direccion { get; set; }

        [Display(Name = "LATITUDE")]
        [DisplayFormat(DataFormatString = "{0:N6}")]
        public double Latitude { get; set; }

        [Display(Name = "LONGITUDE")]
        [DisplayFormat(DataFormatString = "{0:N6}")]
        public double Longitude { get; set; }

        [Display(Name = "URL")]
        public string Url { get; set; }

    }
}
