using Biker.Web.Data.Entities.MotorBike;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Biker.Web.Models.MotorBike
{
    public class AddMotorBikeViewModel : MotorBikeEntity
    {
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "MAKER")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a maker.")]
        public int MakerId { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "MOTORBIKE TYPE")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a motorbike type.")]
        public int TypeId { get; set; }

        //[Display(Name = "IMAGE")]
        //public IFormFile ImageFile { get; set; }

        public IEnumerable<SelectListItem> Makers { get; set; }
        public IEnumerable<SelectListItem> Types { get; set; }



    }
}
