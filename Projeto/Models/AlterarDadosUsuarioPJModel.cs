using System.ComponentModel.DataAnnotations;

namespace Projeto.Models
{
    public class AlterarDadosUsuarioPJModel
    {
        public int Id { get; set; }




        [Required(ErrorMessage = "Digite o Nome do usuario")]
        public string Nome { get; set; }



        [Required(ErrorMessage = "Digite o SobreNome do usuario")]
        public string SobreNome { get; set; }



        [Required(ErrorMessage = "Digite a Razão Social")]
        public string RazaoSocial { get; set; }



        [Required(ErrorMessage = "Digite o E-mail do usuario")]
        
        public string Email { get; set; }



        [Required(ErrorMessage = "ConfirmEmail")]
        
        public string? ConfirmEmail { get; set; }




    }
}
