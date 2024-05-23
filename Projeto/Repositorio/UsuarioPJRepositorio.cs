using Projeto.Data;
using Projeto.Models;
using Projeto.Repositorio;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Projeto.Repositorio
{
    public class UsuarioPJRepositorio : IUsuarioPJRepositorio
    {

        private readonly BancoContext _bancoContext;


        public UsuarioPJRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }




        public UsuarioPJModel Adicionar(UsuarioPJModel UsuarioPJ)
        {
    

            UsuarioPJ.DataCadastro = DateTime.Now;
            UsuarioPJ.Senha = "34415d8d04a3fc792aa57a13d5115b7c7a8e6f9b";
            UsuarioPJ.ConfirmSenha = "34415d8d04a3fc792aa57a13d5115b7c7a8e6f9b";
            UsuarioPJ.Perfil = Enums.PerfilEnum.Empresa;
            UsuarioPJ.Status = Enums.StatusEnums.Inativo;
            UsuarioPJ.SetSenhaHash();
            _bancoContext.UsuariosPJ.Add(UsuarioPJ);
            _bancoContext.SaveChanges();
            return UsuarioPJ;

        }




        public UsuarioPJModel AdicionarClienteDomus(ClienteDomusPJModel usuarioPJDomus)
        {
            
            UsuarioPJModel usuarioPJ = new UsuarioPJModel();

            usuarioPJ.Nome = usuarioPJDomus.Nome;
            usuarioPJ.SobreNome = usuarioPJDomus.SobreNome;
            usuarioPJ.RazaoSocial = usuarioPJDomus.RazaoSocial;
            usuarioPJ.Login = usuarioPJDomus.Login;
            usuarioPJ.DataAbertura = usuarioPJDomus.DataAbertura;
            usuarioPJ.Perfil = Enums.PerfilEnum.Empresa;
            usuarioPJ.Email = usuarioPJDomus.Email;
            usuarioPJ.ConfirmEmail = usuarioPJ.Email;
            usuarioPJ.Senha = "34415d8d04a3fc792aa57a13d5115b7c7a8e6f9b";
            usuarioPJ.ConfirmSenha = "34415d8d04a3fc792aa57a13d5115b7c7a8e6f9b";
            usuarioPJ.ClienteStallos = Enums.ClienteStallosEnum.ClienteStallos;
            usuarioPJ.AutorizacaoLGPD = "true";
            _bancoContext.UsuariosPJ.Add(usuarioPJ);
            _bancoContext.SaveChanges();
            return usuarioPJ;


        }



        public UsuarioPJModel AlterarPerfil(AlterarDadosUsuarioPJModel alterarDadosUsuarioPJ)
        {
            UsuarioPJModel usuarioDB = ListarPorId(alterarDadosUsuarioPJ.Id);

            if (usuarioDB == null) throw new System.Exception("Houve um erro na atualização do contato");

            usuarioDB.Nome = alterarDadosUsuarioPJ.Nome;
            usuarioDB.SobreNome = alterarDadosUsuarioPJ.SobreNome;
            usuarioDB.RazaoSocial = alterarDadosUsuarioPJ.RazaoSocial;
            usuarioDB.Email = alterarDadosUsuarioPJ.Email;
            usuarioDB.ConfirmEmail = alterarDadosUsuarioPJ.ConfirmEmail;
            usuarioDB.DataAlteracao = DateTime.Now;

            _bancoContext.UsuariosPJ.Update(usuarioDB);

            _bancoContext.SaveChanges();

            return usuarioDB;

        }






        public UsuarioPJModel AlterarSenha(AlterarSenhaUsuarioPJModel alterarSenhaUsuarioPJ)
        {
            UsuarioPJModel usuarioPJDB = ListarPorId(alterarSenhaUsuarioPJ.Id);

            if (usuarioPJDB == null) throw new Exception("Houve um erro na atualização da senha, usuário não encontrado!");

            if (!usuarioPJDB.SenhaValida(alterarSenhaUsuarioPJ.SenhaAtual)) throw new Exception("Senha atual não confere!");

            if (usuarioPJDB.SenhaValida(alterarSenhaUsuarioPJ.NovaSenha)) throw new Exception("Nova senha deve ser diferente da senha atual!");

            usuarioPJDB.SetNovaSenha(alterarSenhaUsuarioPJ.NovaSenha);
            usuarioPJDB.SetNovaSenha(alterarSenhaUsuarioPJ.ConfirmarNovaSenha);
            usuarioPJDB.DataAlteracao = DateTime.Now;

            _bancoContext.UsuariosPJ.Update(usuarioPJDB);
            _bancoContext.SaveChanges();

            return usuarioPJDB;
        }



        public bool Apagar(int id)
        {
            UsuarioPJModel usuarioPJDB = ListarPorId(id);

            if (usuarioPJDB == null) throw new System.Exception("Houve um erro na exclusão do usuario");

            _bancoContext.UsuariosPJ.Remove(usuarioPJDB);
            _bancoContext.SaveChanges();

            return true;
        }




        public UsuarioPJModel Atualizar(UsuarioPJModel usuarioPJ)
        {
            UsuarioPJModel usuarioPJDB = ListarPorId(usuarioPJ.Id);

            if (usuarioPJDB == null) throw new System.Exception("Houve um erro na atualização do contato");

            usuarioPJDB.Nome = usuarioPJ.Nome;
            usuarioPJDB.SobreNome = usuarioPJ.SobreNome;
            usuarioPJDB.RazaoSocial = usuarioPJ.RazaoSocial;
            usuarioPJDB.Login = usuarioPJ.Login;
            usuarioPJDB.DataAbertura = usuarioPJ.DataAbertura;
            usuarioPJDB.Email = usuarioPJ.Email;
            usuarioPJDB.ConfirmEmail = usuarioPJ.ConfirmEmail;
            usuarioPJDB.Perfil = usuarioPJ.Perfil;
            usuarioPJDB.Status = usuarioPJ.Status;
            usuarioPJDB.DataAlteracao = DateTime.Now;


            _bancoContext.UsuariosPJ.Update(usuarioPJDB);

            _bancoContext.SaveChanges();

            return usuarioPJDB;
        }




        public UsuarioPJModel BuscarPorEmailELogin(string email, string login)
        {

            var usuariosPJ = _bancoContext.UsuariosPJ.Where(x => x.Email.Contains(email)).ToList();

          
            foreach (var usuarioPJ in usuariosPJ)
            {
            
                if (usuarioPJ.Login == login)
                {
                   
                    return usuarioPJ;
                }
            }

            return _bancoContext.UsuariosPJ.FirstOrDefault(x => x.Email == email && x.Login == login);
        }



        public UsuarioPJModel BuscarPorLogin(string login)
        {
            return _bancoContext.UsuariosPJ.FirstOrDefault(x => x.Login == login);
        }




        public List<UsuarioPJModel> BuscarRegistro()
        {
            return _bancoContext.UsuariosPJ.ToList();
        }







        public List<ClienteDomusPJModel> GetClienteDomus()
        {
            Assistant.WriteLogFile("Função - GetCliente.", false);

            SqlConnection connection = BancoContext.GetConnection();
            List<ClienteDomusPJModel> clientesDomus = new List<ClienteDomusPJModel>();


            try
            {
                string query = "SELECT CASE WHEN CHARINDEX(' ', LTRIM(RTRIM(NOMCLI))) > 0 THEN LEFT(NOMCLI, CHARINDEX(' ', LTRIM(RTRIM(NOMCLI))) - 1)  ELSE NOMCLI END AS 'NOME', " +
                    "CASE  WHEN CHARINDEX(' ', LTRIM(RTRIM(NOMCLI))) > 0 THEN LTRIM(RTRIM(SUBSTRING(NOMCLI, CHARINDEX(' ', LTRIM(RTRIM(NOMCLI))), (LEN(LTRIM(RTRIM(NOMCLI))) + 1 - CHARINDEX(' ', LTRIM(RTRIM(NOMCLI))))))) " +
                    "ELSE '' END AS 'SOBRENOME', C.NOMCLI AS RAZAOSOCIAL, J.CNPJ AS LOGIN, J.DTCONSTITUICAO as DTABERTURA, E.EMAIL FROM TB_CLIENTE AS C JOIN TB_CLIENTE_JCOMPL AS J ON C.CODCLI = J.CODCLI JOIN TB_CLIENTE_EMAIL " +
                    "AS E ON C.CODCLI = E.CODCLI WHERE ATIVO = 'S' AND TIPOPESSOA ='J' AND E.TIPO = 'GERAL' ";




                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    ClienteDomusPJModel clientes = new ClienteDomusPJModel()
                    {

                        Nome = dataReader[0].ToString(),
                        SobreNome = dataReader[1].ToString(),
                        RazaoSocial = dataReader[2].ToString(),
                        Login = dataReader[3].ToString(),
                        DataAbertura = dataReader[4].ToString(),
                        Email = dataReader[5].ToString(),




                    };

                    clientesDomus.Add(clientes);
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







        public UsuarioPJModel ListarPorId(int id)
        {
            return _bancoContext.UsuariosPJ.FirstOrDefault(x => x.Id == id);
        }



    }
}
