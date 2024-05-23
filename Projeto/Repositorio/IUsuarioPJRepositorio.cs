using Projeto.Models;

namespace Projeto.Repositorio
{
    public interface IUsuarioPJRepositorio
    {


        UsuarioPJModel BuscarPorLogin(string login);

        UsuarioPJModel BuscarPorEmailELogin(string email, string login);

        UsuarioPJModel ListarPorId(int id);
               
        List<UsuarioPJModel> BuscarRegistro();

        UsuarioPJModel Adicionar(UsuarioPJModel usuarioPJ);

        UsuarioPJModel Atualizar(UsuarioPJModel usuarioPJ);

        UsuarioPJModel AlterarSenha(AlterarSenhaUsuarioPJModel alterarSenhaUsuario);

        UsuarioPJModel AlterarPerfil(AlterarDadosUsuarioPJModel alterarDadosUsuarioPJ);

                    
        bool Apagar(int id);


        List<ClienteDomusPJModel> GetClienteDomus();


        UsuarioPJModel AdicionarClienteDomus(ClienteDomusPJModel clientePJDomus);




    }
}
