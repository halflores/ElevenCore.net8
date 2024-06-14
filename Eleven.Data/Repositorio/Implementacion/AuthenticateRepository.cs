using Eleven.Data.Entidad;
using Eleven.Data.Repositorio.Interfaz;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Eleven.Data.Repositorio.Implementacion
{
    public class AuthenticateRepository : Repository<Usuario>, IAuthenticateRepository
    {
        protected readonly RepositoryPatternContext _repositoryPatternContext;
        private readonly ILogger<AuthenticateRepository> _logger;
        public AuthenticateRepository(RepositoryPatternContext repositoryPatternContext, ILogger<AuthenticateRepository> logger) : base(repositoryPatternContext, logger)
        {
            _repositoryPatternContext = repositoryPatternContext;
            _logger = logger;
        }

        public bool CheckPassword(string login, string password)
        {
            try
            {
                int res = 0;
                using (var db = _repositoryPatternContext)
                {
                    var vLogin = new SqlParameter("@login", login);
                    var vPassword = new SqlParameter("@password", password);
                    var vResult = new SqlParameter("@result",System.Data.SqlDbType.Int);
                    vResult.Direction = System.Data.ParameterDirection.Output;

                    db.Database.ExecuteSqlRaw("exec spVerificarUsuario @login, @password, @result OUTPUT", vLogin, vPassword, vResult);
                    res = vResult == null? 0 : Convert.ToInt32(vResult.Value);
                    return res > 0;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CheckPassword");
                throw;
            }
        }

    }
}
