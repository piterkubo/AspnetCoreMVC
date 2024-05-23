using System.ComponentModel.DataAnnotations;
using Projeto.Enums;

namespace Projeto.Models
{
    public class UsuarioSemSenhaModel
    {
        public int Id { get; set; }



        [Required(ErrorMessage = "Selecione o Status do usuario")]
        public StatusEnums Status { get; set; }



        [Required(ErrorMessage = "Digite o Nome do usuario")]
        public string Nome { get; set; }



        [Required(ErrorMessage = "Digite o SobreNome do usuario")]
        public string SobreNome { get; set; }

                

        [Required(ErrorMessage = "Digite o Login do usuario")]
        public string Login { get; set; }




        [Required(ErrorMessage = "Selecione o Perfil do usuario")]
        public PerfilEnum? Perfil { get; set; }





        [Required(ErrorMessage = "Digite o E-mail do usuario")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é valido!")]
        public string Email { get; set; }



        [Required(ErrorMessage = "ConfirmEmail")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é valido!")]
        public string ConfirmEmail { get; set; }



        public string? AutorizacaoLGPD { get; set; }



    }
}
