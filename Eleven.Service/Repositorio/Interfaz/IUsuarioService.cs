using Eleven.Data.Entidad;
using Eleven.Service.Modelo.Eleven;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleven.Service.Repositorio.Interfaz
{
    public interface IUsuarioService : IServiceAsync<Usuario, MdoUsuario>
    {
        Task<MdoUsuario?> FindByNameAsync(string login);
        Task<MdoUsuario> Create(MdoUsuario MdoUsuario);
    }
}
