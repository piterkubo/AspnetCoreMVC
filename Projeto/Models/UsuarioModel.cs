
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Projeto.Enums;
using Projeto.helper;

namespace Projeto.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }


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




        public string? Senha { get; set; }




        public string? ConfirmSenha { get; set; }



        public string? AutorizacaoLGPD { get; set; }


        public ClienteStallosEnum? ClienteStallos { get; set; }

        public DateTime? DataCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }


        public bool? IsDeleted { get; set; }

        public DateTime? DataExclusao { get; set; }




        // metodo para verificar a autenticação da senha
        public bool SenhaValida(string senha)
        {
            return Senha == senha.GerarHash();
        }



        //metodo para setar a senha do usuario para virar criptografico

        public void SetSenhaHash() 
        {

            Senha = Senha.GerarHash();
            ConfirmSenha = ConfirmSenha.GerarHash();

        }


        //Metodo Para Setar Nova Senha


        public void SetNovaSenha(string novaSenha)
        {
            Senha = novaSenha.GerarHash();
            ConfirmSenha = novaSenha.GerarHash();

        }





        //metodo para gerar uma nova senha do usuario resetado
        public string GerarNovaSenha()
        {
            string novaSenha = Guid.NewGuid().ToString().Substring(0, 8);

            Senha = novaSenha.GerarHash();
            ConfirmSenha = novaSenha.GerarHash();
            return novaSenha;

        }

    }
}
