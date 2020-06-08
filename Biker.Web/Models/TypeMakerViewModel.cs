using Biker.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Biker.Web.Models
{
    public class TypeMakerViewModel : TypeMakerEntity
    {

        [Display(Name = "IMAGE FOR TYPE")]
        public IFormFile ImageFile { get; set; }

        [Display(Name = "TYPE")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a motorbike type.")]
        public int TypeId { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public int MakerId { get; set; }

        public IEnumerable<SelectListItem> Types { get; set; }

    }
}
