using System.ComponentModel.DataAnnotations;

namespace Projeto.Models
{
    public class AlterarDadosUsuarioModel
    {
        public int Id { get; set; }




        [Required(ErrorMessage = "Digite o Nome do usuario")]
        public string Nome { get; set; }




        [Required(ErrorMessage = "Digite o SobreNome do usuario")]
        public string SobreNome { get; set; }




        [Required(ErrorMessage = "Digite o E-mail do usuario")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é valido!")]
        public string Email { get; set; }



        [Required(ErrorMessage = "ConfirmEmail")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é valido!")]
        public string? ConfirmEmail { get; set; }
    }
}
