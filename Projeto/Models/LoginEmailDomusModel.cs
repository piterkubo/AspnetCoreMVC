using System.ComponentModel.DataAnnotations;

namespace Projeto.Models
{
    public class LoginEmailDomusModel
    {

        [Required(ErrorMessage = "Digite o Login do usuario")]
        public string? Login { get; set; }

        [Required(ErrorMessage = "Digite o E-mail do usuario")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é valido!")]
        public string? Email { get; set; }

    }
}
