using Biker.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Biker.Web.Models.MotorBike
{
    public class AddMotorBikeSpareViewModel : MotorBikeSpareEntity
    {
        public int MotorbikeId { get; set; }

        [Display(Name = "IMAGE")]
        public IFormFile ImageFile { get; set; }
    }
}
