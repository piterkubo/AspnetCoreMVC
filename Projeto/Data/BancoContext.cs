
using Projeto.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace Projeto.Data
{
    public class BancoContext: DbContext
    {
        private static readonly string SERVER = "seu ip";
        private static readonly string USER = "user";
        private static readonly string DATABASE = "teste";
        private static readonly string PASSWORD = "sua senha";


        public BancoContext(DbContextOptions<BancoContext>options) :base(options)
        {
        
            
        
        }


        public DbSet<UsuarioModel> Usuarios { get; set; }
       
        public DbSet<UsuarioAdmModel> UsuariosAdm { get; set; }

        public DbSet<UsuarioPJModel> UsuariosPJ { get; set; }

        





        public static SqlConnection GetConnection()
        {
      
            string connetionString = $@"Password={PASSWORD};Persist Security Info=False;User ID={USER};Initial Catalog={DATABASE};Data Source={SERVER}";

           
            SqlConnection connection = new SqlConnection(connetionString);

            try
            {
               
                connection.Open();
            }
            catch (Exception ex)
            {
             
                Assistant.WriteLogFile(ex.Message, true);
            }

            
            return connection;
        }


        public override int SaveChanges()
        {
            var objectStateEntries = ChangeTracker.Entries()
          .Where(e => e.Entity is UsuarioModel && e.State != EntityState.Detached && e.State != EntityState.Unchanged).ToList();

            var objectStateEntries2 = ChangeTracker.Entries()
          .Where(e => e.Entity is UsuarioAdmModel && e.State != EntityState.Detached && e.State != EntityState.Unchanged).ToList();


            var objectStateEntries3 = ChangeTracker.Entries()
           .Where(e => e.Entity is UsuarioPJModel && e.State != EntityState.Detached && e.State != EntityState.Unchanged).ToList();


            


            var currentTime = DateTime.UtcNow;
            foreach (var entry in objectStateEntries)
            {
                var entityBase = entry.Entity as UsuarioModel;


                if (entityBase == null) continue;
                switch (entry.State)
                {

                    case EntityState.Deleted:
                        {
                            entry.State = EntityState.Modified;
                            entityBase.DataExclusao = currentTime;
                            entityBase.IsDeleted = true;
                            entityBase.Status = Enums.StatusEnums.Inativo;                                                       
                            entityBase.Senha = "34415d8d04a3fc792aa57a13d5115b7c7a8e6f9b";
                            break;
                        }

                    case EntityState.Modified:
                        {
                            entityBase.DataAlteracao = currentTime;
                            entityBase.DataExclusao = null;                            
                            entityBase.Senha = entityBase.ConfirmSenha;
                            entityBase.IsDeleted = false;
                            break;
                        }


                    case EntityState.Added:
                        {
                            entityBase.DataCadastro = currentTime;
                            entityBase.Status = Enums.StatusEnums.Inativo;
                            entityBase.Perfil = Enums.PerfilEnum.Usuario;
                            
                           

                            break;
                        }

                    default:
                        break;
                }


                


            }

            var currentTime2 = DateTime.UtcNow;

            foreach (var entry2 in objectStateEntries2)
            {
                var entityBase2 = entry2.Entity as UsuarioAdmModel;


                if (entityBase2 == null) continue;
                switch (entry2.State)
                {

                    case EntityState.Deleted:
                        {
                            entry2.State = EntityState.Modified;
                            entityBase2.DataExclusao = currentTime;
                            entityBase2.IsDeleted = true;
                            entityBase2.Status = Enums.StatusEnums.Inativo;
                            entityBase2.Senha = "34415d8d04a3fc792aa57a13d5115b7c7a8e6f9b";
                            break;
                        }

                    case EntityState.Modified:
                        {
                            entityBase2.DataAlteracao = currentTime;
                            entityBase2.DataExclusao = null;                            
                            entityBase2.Senha = entityBase2.ConfirmSenha;
                            entityBase2.IsDeleted = false;
                            break;
                        }


                    case EntityState.Added:
                        {
                            entityBase2.DataCadastro = currentTime;
                            entityBase2.Status = Enums.StatusEnums.Inativo;
                            entityBase2.Perfil = Enums.PerfilEnum.Admin;
                            break;
                        }

                    default:
                        break;
                
                }

            }



            var currentTime3 = DateTime.UtcNow;

            foreach (var entry3 in objectStateEntries3)
            {
                var entityBase3 = entry3.Entity as UsuarioPJModel;


                if (entityBase3 == null) continue;
                switch (entry3.State)
                {

                    case EntityState.Deleted:
                        {
                            entry3.State = EntityState.Modified;
                            entityBase3.DataExclusao = currentTime;
                            entityBase3.IsDeleted = true;
                            entityBase3.Status = Enums.StatusEnums.Inativo;
                            entityBase3.Senha = "34415d8d04a3fc792aa57a13d5115b7c7a8e6f9b";
                            break;
                        }

                    case EntityState.Modified:
                        {
                            entityBase3.DataAlteracao = currentTime;
                            entityBase3.DataExclusao = null;                           
                            entityBase3.Senha = entityBase3.ConfirmSenha;
                            entityBase3.IsDeleted = false;
                            break;
                        }


                    case EntityState.Added:
                        {
                            entityBase3.DataCadastro = currentTime;
                            entityBase3.Status = Enums.StatusEnums.Inativo;
                            entityBase3.Perfil = Enums.PerfilEnum.Empresa;
                            break;
                        }

                    default:
                        break;

                }

            }
                       


            return base.SaveChanges();
        }


        




    }
}
