using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Projeto.Enums;
using Projeto.helper;

namespace Projeto.Models
{   
    public class NovoUsuarioPJModel
    {
        public int Id { get; set; }


       

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
        public PerfilEnum Perfil { get; set; }




        [Required(ErrorMessage = "Digite o E-mail do usuario")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é valido!")]
        public string Email { get; set; }



        [Required(ErrorMessage = "ConfirmEmail")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é valido!")]
        public string ConfirmEmail { get; set; }






        public string? Senha { get; set; }


        public string? ConfirmSenha { get; set; }



        public string? AutorizacaoLGPD { get; set; }


                
        public DateTime? DataCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }


        public bool? IsDeleted { get; set; }

        public DateTime? DataExclusao { get; set; }


            




       

    }
}
