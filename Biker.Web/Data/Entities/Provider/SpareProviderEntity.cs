using System.ComponentModel.DataAnnotations;

namespace Biker.Web.Data.Entities.Provider
{
    public class SpareProviderEntity
    {

        //Repuesto y proveeedor

        public int Id { get; set; }

        [Required]
        public ProviderEntity Provider { get; set; }

        [Required]
        public SpareEntity Spare { get; set; }

    }
}
