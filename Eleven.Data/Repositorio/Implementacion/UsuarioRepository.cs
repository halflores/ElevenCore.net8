using Eleven.Data.Entidad;
using Eleven.Data.Repositorio.Interfaz;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleven.Data.Repositorio.Implementacion
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        protected readonly RepositoryPatternContext _repositoryPatternContext;
        private readonly ILogger<UsuarioRepository> _logger;
        public UsuarioRepository(RepositoryPatternContext repositoryPatternContext, ILogger<UsuarioRepository> logger) : base(repositoryPatternContext, logger)
        {
            _repositoryPatternContext = repositoryPatternContext;
            _logger = logger;
        }

        public async Task<Usuario> Create(Usuario usuario)
        {
            try
            {
                using (var db = _repositoryPatternContext)
                {
                    var vLogin = new SqlParameter("@login", usuario.Login);
                    var vPassword = new SqlParameter("@password", usuario.RegPassword);
                    var vNombres = new SqlParameter("@nombres", usuario.Nombres);
                    var vApellidos = new SqlParameter("@apellidos", usuario.Apellidos);
                    var vEmpleadoSAP = new SqlParameter("@empleadoSAP", usuario.EmpleadoSAP);
                    var vPerfilId = new SqlParameter("@perfilId", usuario.PerfilId);

                    var vNew_Identity = new SqlParameter("@new_identity", System.Data.SqlDbType.Int);
                    vNew_Identity.Direction = System.Data.ParameterDirection.Output;

                    await db.Database.ExecuteSqlRawAsync("exec spCrearUsuario @login, @password, @nombres, @apellidos, @perfilId, @empleadoSAP, @new_identity OUTPUT", 
                                                            vLogin, vPassword, vNombres, vApellidos, vPerfilId, vEmpleadoSAP, vNew_Identity);
                    
                    if (vNew_Identity != null)
                    {
                        usuario.UsuarioId =  Convert.ToInt32(vNew_Identity.Value);
                    }
                    return usuario;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Create");
                throw;
            }
        }
    }
}
