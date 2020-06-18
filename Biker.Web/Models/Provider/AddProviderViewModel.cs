using Biker.Web.Data.Entities.Provider;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Biker.Web.Models.Provider
{
    public class AddProviderViewModel : ProviderEntity
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }
    }
}
