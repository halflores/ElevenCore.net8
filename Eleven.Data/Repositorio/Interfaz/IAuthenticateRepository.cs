using Eleven.Data.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleven.Data.Repositorio.Interfaz
{
    public interface IAuthenticateRepository : IRepository<Usuario>
    {
        bool CheckPassword(string login, string password);

    }
}
