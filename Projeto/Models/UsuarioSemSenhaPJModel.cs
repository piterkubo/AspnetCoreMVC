using System.ComponentModel.DataAnnotations;
using Projeto.Enums;

namespace Projeto.Models
{
    public class UsuarioSemSenhaPJModel
    {
        public int Id { get; set; }



        [Required(ErrorMessage = "Selecione o Status do usuario")]
        public StatusEnums Status { get; set; }




        [Required(ErrorMessage = "Digite o Nome do usuario")]
        public string Nome { get; set; }




        [Required(ErrorMessage = "Digite o SobreNome do usuario")]
        public string SobreNome { get; set; }




        [Required(ErrorMessage = "Digite a Razão Social")]
        public string RazaoSocial { get; set; }




        [Required(ErrorMessage = "Digite o Login")]
        public string Login { get; set; }




        [Required(ErrorMessage = "Digite a Data de Abertura")]
        public string DataAbertura { get; set; }




        [Required(ErrorMessage = "Selecione o Perfil do usuario")]
        public PerfilEnum? Perfil { get; set; }




        [Required(ErrorMessage = "Digite o E-mail do usuario")]
        
        public string Email { get; set; }



        [Required(ErrorMessage = "ConfirmEmail")]
        
        public string ConfirmEmail { get; set; }





    }
}
