using System.ComponentModel.DataAnnotations;

namespace Biker.Web.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [EmailAddress(ErrorMessage = "El campo {0} debe ser un email valido.")]
        public string Username { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MinLength(6, ErrorMessage = "El campo {0}  debe ser minimo de {1} caracteres.")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }


    }
}
