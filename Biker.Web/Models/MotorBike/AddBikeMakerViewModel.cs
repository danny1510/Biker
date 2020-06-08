using Biker.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Biker.Web.Models.MotorBike
{
    public class AddBikeMakerViewModel : BikeMakerEntity
    {

        [Display(Name = "IMAGE")]
        public IFormFile ImageFile { get; set; }
    }
}
