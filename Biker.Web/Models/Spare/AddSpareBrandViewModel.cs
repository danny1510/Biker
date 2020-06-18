using Biker.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Biker.Web.Models.Spare
{
    public class AddSpareBrandViewModel : SpareBrandEntity
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }

    }
}
