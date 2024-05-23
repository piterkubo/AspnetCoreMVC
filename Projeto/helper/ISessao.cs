using Projeto.Models;

namespace Projeto.helper
{

    public interface ISessao
    {
        void CriarSessaoDoUsuario(UsuarioModel usuario);
        void AtualizarSessaoDoUsuario(UsuarioModel usuario);
        void RemoverSessaoDoUsuario();
        



        void CriarSessaoDoUsuarioAdm(UsuarioAdmModel usuarioAdm);
        void RemoverSessaoDoUsuarioAdm();


        void CriarSessaoDoUsuarioPJ(UsuarioPJModel usuarioPJ);
        void AtualizarSessaoDoUsuarioPJ(UsuarioPJModel usuarioPJ);
        void RemoverSessaoDoUsuarioPJ();



       


        UsuarioModel BuscarSessaoDoUsuario();
       
        UsuarioAdmModel BuscarSessaoDoUsuarioAdm();
        UsuarioPJModel BuscarSessaoDoUsuarioPJ();


        
    }
}
