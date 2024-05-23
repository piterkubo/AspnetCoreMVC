using Projeto.Models;

namespace Projeto.Repositorio
{
    public interface IUsuarioAdmRepositorio
    {


        UsuarioAdmModel BuscarPorLogin(string login);

        UsuarioAdmModel BuscarPorEmailELogin(string email, string login);

        UsuarioAdmModel ListarPorId(int id);
               
        List<UsuarioAdmModel> BuscarRegistro();
        UsuarioAdmModel Adicionar(UsuarioAdmModel usuarioAdm);

        UsuarioAdmModel Atualizar(UsuarioAdmModel usuarioAdm);

        UsuarioAdmModel AlterarSenha(AlterarSenhaUsuarioAdmModel alterarSenhaUsuario);
                    
        bool Apagar(int id);

       





    }
}
