using Bibliotech.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Bibliotech.Repository
{
    //posteriormente, fazer interfaces para tudo isso
    public class UsuarioRepository
    {
        private IConfiguration _configuration;

        public UsuarioRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
             
        public Usuario GetUsuario(Usuario usuario)
        {
            //tirar o dapper e usar apenas o entity framework!
            using (SqlConnection conexao = new SqlConnection(
                _configuration.GetConnectionString("ConnString")))
            {
                return conexao.QueryFirstOrDefault<Usuario>(
                    "SELECT Id, Login, Descricao " +
                    "FROM dbo.tb_usuario " +
                    "WHERE Login = @Login", new { usuario.Login });
            }
        }
    }
}
