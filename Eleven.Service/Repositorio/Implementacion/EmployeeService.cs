using AutoMapper;
using Eleven.Service.Modelo.SAP;
using Eleven.Service.Repositorio.Interfaz;
using SAPBo.Data.Entidad;
using SAPBo.Data.Repositorio.Implementacion;
using SAPBo.Data.Repositorio.Interfaz;
using Microsoft.Extensions.Logging;

namespace Eleven.Service.Repositorio.Implementacion
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IBoEmployeeRepository _employeeRepositoryBo;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(IBoEmployeeRepository employeeRepositoryBo, IMapper mapper, ILogger<EmployeeService> logger)
        {
            _employeeRepositoryBo = employeeRepositoryBo;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<MdoEmpleado> GetEmployeeAsync(int empleadoId)
        {
            try
            {
                MdoEmpleado boEmpleado = new MdoEmpleado();
                _logger.LogInformation("GetEmployeeAsync iniciando ...");
                Empleado empleado = await _employeeRepositoryBo.GetEmpleadoAsync(empleadoId);
                _logger.LogInformation("GetEmployeeAsync finalizado ...");

                boEmpleado = _mapper.Map<MdoEmpleado>(empleado);

                return boEmpleado;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetEmployeeAsync");
                throw new Exception("—Bien, Houston, hemos tenido un problema aquí.");
            }
        }
    }
}
