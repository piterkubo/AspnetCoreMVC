using Projeto.Data;
using Projeto.Enums;
using Projeto.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MySqlConnector;
using System.Drawing.Text;

namespace Projeto.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {

        private readonly BancoContext _bancoContext;


        public UsuarioRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;

        }



    
        public UsuarioModel BuscarPorLogin(string login)
        {

            return _bancoContext.Usuarios.FirstOrDefault(x => x.Login == login);

        }




        public UsuarioModel BuscarPorEmailELogin(string email, string login)
        {
            return _bancoContext.Usuarios.FirstOrDefault(x => x.Email == email && x.Login == login);
        }


        public UsuarioModel ListarPorId(int id)
        {
            return _bancoContext.Usuarios.FirstOrDefault(x => x.Id == id);
        }
               


        public List<UsuarioModel> BuscarRegistro()
        {
            return _bancoContext.Usuarios.ToList();
        }


        public UsuarioModel Adicionar(UsuarioModel Usuario)
        {
           
                      

            Usuario.DataCadastro = DateTime.Now;     
            Usuario.Senha = "34415d8d04a3fc792aa57a13d5115b7c7a8e6f9b";
            Usuario.ConfirmSenha = "34415d8d04a3fc792aa57a13d5115b7c7a8e6f9b";                             
            Usuario.SetSenhaHash();
            _bancoContext.Usuarios.Add(Usuario);
            _bancoContext.SaveChanges();
            return Usuario;

        }



        public UsuarioModel Atualizar(UsuarioModel usuario)
        {
            UsuarioModel usuarioDB = ListarPorId(usuario.Id);

            if (usuarioDB == null) throw new System.Exception("Houve um erro na atualização do contato");

            usuarioDB.Nome = usuario.Nome;
            usuarioDB.SobreNome = usuario.SobreNome;
            usuarioDB.Login = usuario.Login;
            usuarioDB.Email = usuario.Email;
            usuarioDB.ConfirmEmail = usuario.ConfirmEmail;
            usuarioDB.Perfil = usuario.Perfil;
            usuarioDB.Status = usuario.Status;
            usuarioDB.AutorizacaoLGPD = usuario.AutorizacaoLGPD;
            usuarioDB.DataAlteracao = DateTime.Now;


            _bancoContext.Usuarios.Update(usuarioDB);

            _bancoContext.SaveChanges();

            return usuarioDB;


        }



        public UsuarioModel AlterarSenha(AlterarSenhaUsuarioModel alterarSenhaUsuario)
        {
            UsuarioModel usuarioDB = ListarPorId(alterarSenhaUsuario.Id);

            if (usuarioDB == null) throw new Exception("Houve um erro na atualização da senha, usuário não encontrado!");

            if (!usuarioDB.SenhaValida(alterarSenhaUsuario.SenhaAtual)) throw new Exception("Senha atual não confere!");

            if (usuarioDB.SenhaValida(alterarSenhaUsuario.NovaSenha)) throw new Exception("Nova senha deve ser diferente da senha atual!");

            usuarioDB.SetNovaSenha(alterarSenhaUsuario.NovaSenha);
            usuarioDB.SetNovaSenha(alterarSenhaUsuario.ConfirmarNovaSenha);
            usuarioDB.DataAlteracao = DateTime.Now;

            _bancoContext.Usuarios.Update(usuarioDB);
            _bancoContext.SaveChanges();

            return usuarioDB;
        }



        public UsuarioModel AlterarPerfil(AlterarDadosUsuarioModel alterarDadosUsuario)
        {
            UsuarioModel usuarioDB = ListarPorId(alterarDadosUsuario.Id);

            if (usuarioDB == null) throw new System.Exception("Houve um erro na atualização do contato");

            usuarioDB.Nome = alterarDadosUsuario.Nome;
            usuarioDB.SobreNome = alterarDadosUsuario.SobreNome;
            usuarioDB.Email = alterarDadosUsuario.Email;
            usuarioDB.ConfirmEmail = alterarDadosUsuario.ConfirmEmail;            
            usuarioDB.DataAlteracao = DateTime.Now;

            _bancoContext.Usuarios.Update(usuarioDB);

            _bancoContext.SaveChanges();

            return usuarioDB;

        }







        public bool Apagar(int id)
        {
            UsuarioModel usuarioDB = ListarPorId(id);

            if (usuarioDB == null) throw new System.Exception("Houve um erro na exclusão do usuario");

            _bancoContext.Usuarios.Remove(usuarioDB);
            _bancoContext.SaveChanges();

            return true;
        }








        public List<ClienteDomusModel> GetClienteDomus()
        {
            Assistant.WriteLogFile("Função - GetCliente.", false);

            SqlConnection connection = BancoContext.GetConnection();
            List<ClienteDomusModel> clientesDomus = new List<ClienteDomusModel>();


            try
            {
                string query = "SELECT \r\n    \r\n    CASE\r\n        WHEN CHARINDEX(' ', LTRIM(RTRIM(NOMCLI))) > 0 THEN LEFT(NOMCLI, CHARINDEX(' ', LTRIM(RTRIM(NOMCLI))) - 1) \r\n" +
                    "        ELSE NOMCLI\r\n    END AS 'NOME',\r\n    CASE   \r\n        WHEN CHARINDEX(' ', LTRIM(RTRIM(NOMCLI))) > 0 THEN LTRIM(RTRIM(SUBSTRING(NOMCLI, CHARINDEX(' ', LTRIM(RTRIM(NOMCLI)))," +
                    " (LEN(LTRIM(RTRIM(NOMCLI))) + 1 - CHARINDEX(' ', LTRIM(RTRIM(NOMCLI)))))))\r\n        ELSE ''\r\n    END AS 'SOBRENOME',\r\n    F.CPF AS LOGIN, \r\n    " +
                    "SUBSTRING(E.EMAIL, 1, CHARINDEX(';', E.EMAIL + ';') - 1) AS 'EMAIL'\r\nFROM \r\n    TB_CLIENTE AS C\r\nJOIN \r\n    TB_CLIENTE_FCOMPL AS F ON C.CODCLI = F.CODCLI\r\nJOIN \r\n" +
                    "    TB_CLIENTE_EMAIL AS E ON C.CODCLI = E.CODCLI\r\nWHERE \r\n    ATIVO = 'S' \r\n    AND TIPOPESSOA ='F' \r\n    AND E.TIPO = 'GERAL'";




                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    ClienteDomusModel cliente = new ClienteDomusModel()
                    {
                        
                        Nome = dataReader[0].ToString(),
                        SobreNome = dataReader[1].ToString(),
                        Login = dataReader[2].ToString(),
                        Email = dataReader[3].ToString(),
                        



                    };

                    clientesDomus.Add(cliente);
                }

                Assistant.WriteLogFile("Consulta executada com sucesso.", false);
            }
            catch (Exception ex)
            {
                Assistant.WriteLogFile(ex.Message, true);
            }
            finally
            {
                connection.Close();
            }

            return clientesDomus;


        }



        public UsuarioModel AdicionarClienteDomus(ClienteDomusModel usuarioDomus)
        {
         

            UsuarioModel usuario = new UsuarioModel();

            usuario.Nome = usuarioDomus.Nome;
            usuario.SobreNome = usuarioDomus.SobreNome;
            usuario.Login = usuarioDomus.Login;
            usuario.Email = usuarioDomus.Email;
            usuario.ConfirmEmail = usuario.Email;
            usuario.Senha = "34415d8d04a3fc792aa57a13d5115b7c7a8e6f9b";
            usuario.ConfirmSenha = "34415d8d04a3fc792aa57a13d5115b7c7a8e6f9b";
            usuario.Perfil = Enums.PerfilEnum.Usuario;
            usuario.ClienteStallos = Enums.ClienteStallosEnum.ClienteStallos;
            _bancoContext.Usuarios.Add(usuario);
            _bancoContext.SaveChanges();
            return usuario;

        }

       
    }       
}



