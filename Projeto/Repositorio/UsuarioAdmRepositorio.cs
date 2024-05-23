using Projeto.Data;
using Projeto.Models;
using Projeto.Repositorio;

namespace Projeto.Repositorio
{
    public class UsuarioAdmRepositorio : IUsuarioAdmRepositorio
    {
        private readonly BancoContext _bancoContext;


        public UsuarioAdmRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }





        public UsuarioAdmModel Adicionar(UsuarioAdmModel usuarioAdm)
        {
    

            usuarioAdm.DataCadastro = DateTime.Now;
            usuarioAdm.Senha = "34415d8d04a3fc792aa57a13d5115b7c7a8e6f9b";
            usuarioAdm.ConfirmSenha = "34415d8d04a3fc792aa57a13d5115b7c7a8e6f9b";
            usuarioAdm.SetSenhaHash();           
            _bancoContext.UsuariosAdm.Add(usuarioAdm);
            _bancoContext.SaveChanges();
            return usuarioAdm;

        }





        public UsuarioAdmModel AlterarSenha(AlterarSenhaUsuarioAdmModel alterarSenhaUsuarioAdm)
        {
            UsuarioAdmModel usuarioAdmDB = ListarPorId(alterarSenhaUsuarioAdm.Id);

            if (usuarioAdmDB == null) throw new Exception("Houve um erro na atualização da senha, usuário não encontrado!");

            if (!usuarioAdmDB.SenhaValida(alterarSenhaUsuarioAdm.SenhaAtual)) throw new Exception("Senha atual não confere!");

            if (usuarioAdmDB.SenhaValida(alterarSenhaUsuarioAdm.NovaSenha)) throw new Exception("Nova senha deve ser diferente da senha atual!");

            usuarioAdmDB.SetNovaSenha(alterarSenhaUsuarioAdm.NovaSenha);
            usuarioAdmDB.SetNovaSenha(alterarSenhaUsuarioAdm.ConfirmarNovaSenha);
            usuarioAdmDB.DataAlteracao = DateTime.Now;

            _bancoContext.UsuariosAdm.Update(usuarioAdmDB);
            _bancoContext.SaveChanges();

            return usuarioAdmDB;
        }






        public bool Apagar(int id)
        {
            UsuarioAdmModel usuarioDB = ListarPorId(id);

            if (usuarioDB == null) throw new System.Exception("Houve um erro na exclusão do usuario");

            _bancoContext.UsuariosAdm.Remove(usuarioDB);
            _bancoContext.SaveChanges();

            return true;
        }






        public UsuarioAdmModel Atualizar(UsuarioAdmModel usuarioAdm)
        {
            UsuarioAdmModel usuarioAdmDB = ListarPorId(usuarioAdm.Id);

            if (usuarioAdmDB == null) throw new System.Exception("Houve um erro na atualização do contato");

            usuarioAdmDB.Nome = usuarioAdm.Nome;
            usuarioAdmDB.SobreNome = usuarioAdm.SobreNome;
            usuarioAdmDB.Login = usuarioAdm.Login;
            usuarioAdmDB.Email = usuarioAdm.Email;
            usuarioAdmDB.ConfirmEmail = usuarioAdm.ConfirmEmail;
            usuarioAdmDB.Perfil = usuarioAdm.Perfil;
            usuarioAdmDB.Status = usuarioAdm.Status;
            usuarioAdmDB.DataAlteracao = DateTime.Now;


            _bancoContext.UsuariosAdm.Update(usuarioAdmDB);

            _bancoContext.SaveChanges();

            return usuarioAdmDB;
        }






        public UsuarioAdmModel BuscarPorEmailELogin(string email, string login)
        {
            return _bancoContext.UsuariosAdm.FirstOrDefault(x => x.Email == email && x.Login == login);
        }



        public UsuarioAdmModel BuscarPorLogin(string login)
        {

            return _bancoContext.UsuariosAdm.FirstOrDefault(x => x.Login == login);

        }




        public List<UsuarioAdmModel> BuscarRegistro()
        {
            return _bancoContext.UsuariosAdm.ToList();
        }




        public UsuarioAdmModel ListarPorId(int id)
        {
            return _bancoContext.UsuariosAdm.FirstOrDefault(x => x.Id == id);
        }





    }


}































































