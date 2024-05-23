using Projeto.Enums;
using Projeto.helper;
using System.ComponentModel.DataAnnotations;

namespace Projeto.Models
{
    public class ClienteDomusModel
    {
        [Required(ErrorMessage = "Selecione o Status do usuario")]
        public StatusEnums? Status { get; set; }


        [Required(ErrorMessage = "Digite o Nome do usuario")]
        public string? Nome { get; set; }





        [Required(ErrorMessage = "Digite o SobreNome do usuario")]
        public string? SobreNome { get; set; }






        [Required(ErrorMessage = "Digite o Login do usuario")]
        public string? Login { get; set; }



        [Required(ErrorMessage = "Selecione o Perfil do usuario")]
        public PerfilEnum? Perfil { get; set; }




        [Required(ErrorMessage = "Digite o E-mail do usuario")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é valido!")]
        public string? Email { get; set; }



        [Required(ErrorMessage = "ConfirmEmail")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é valido!")]
        public string? ConfirmEmail { get; set; }





        [Required(ErrorMessage = "Digite a Senha")]
        public string? Senha { get; set; }



        [Required(ErrorMessage = "ConfirmSenha")]
        public string? ConfirmSenha { get; set; }



        public string? AutorizacaoLGPD { get; set; }


        public ClienteStallosEnum? ClienteStallos { get; set; }

        public DateTime? DataCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }


        public bool? IsDeleted { get; set; }

        public DateTime? DataExclusao { get; set; }







    }
}
