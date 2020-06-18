using Biker.Web.Data.Entities.MotorBike;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Biker.Web.Models.Spare
{
    public class AddSpareCategoriesViewModel : SpareCategoryEntity
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }
    }
}
