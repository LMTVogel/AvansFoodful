using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Portal.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "E-mail is niet ingevuld")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Wachtwoord is niet ingevuld")]
        [PasswordPropertyText]
        public string Password { get; set; } = null!;
    }
}
