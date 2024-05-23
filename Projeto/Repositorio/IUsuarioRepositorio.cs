using Projeto.Data;
using Projeto.Models;

namespace Projeto.Repositorio
{
    public interface IUsuarioRepositorio
    {


        UsuarioModel BuscarPorLogin(string login);

        UsuarioModel BuscarPorEmailELogin(string email, string login);

        UsuarioModel ListarPorId(int id);

        
        List<UsuarioModel> BuscarRegistro();

        UsuarioModel Adicionar(UsuarioModel usuario);
        

        UsuarioModel Atualizar(UsuarioModel usuario);

        UsuarioModel AlterarSenha(AlterarSenhaUsuarioModel alterarSenhaUsuario);

        UsuarioModel AlterarPerfil(AlterarDadosUsuarioModel alterarDadosusuario); 


                    
        bool Apagar(int id);
        


        List<ClienteDomusModel> GetClienteDomus();


        UsuarioModel AdicionarClienteDomus(ClienteDomusModel clienteDomus);




    }
}


