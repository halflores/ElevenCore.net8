using Eleven.Service.Modelo.SAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleven.Service.Repositorio.Interfaz
{
    public interface IEmployeeService
    {
        Task<MdoEmpleado> GetEmployeeAsync(int empleadoId);
    }
}
