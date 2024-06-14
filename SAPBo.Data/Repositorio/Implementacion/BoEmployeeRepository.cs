using B1SLayer;
using Microsoft.Extensions.Logging;
using Sap.Data.Hana;
using SAPBo.B1HanaQuery;
using SAPBo.Data.Entidad;
using SAPBo.Data.Repositorio.Interfaz;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SAPBo.Data.Repositorio.Implementacion
{
    public class BoEmployeeRepository :IBoEmployeeRepository
    {
        private readonly Dictionary<string, SLConnection> _slConnections;
        private readonly ILogger<BoEmployeeRepository> _logger;
        public BoEmployeeRepository(Dictionary<string, SLConnection> slConnections, ILogger<BoEmployeeRepository> logger)
        {
            _slConnections = slConnections;
            _logger = logger;
        }

        HanaRequest.RowMapper<Empleado> empleadoMapper = (delegate (HanaDataReader reader)
        {
            Empleado empleado = new Empleado();
            
            //oitb.ItmsGrpNam = Convert.ToString(reader[1])!;
            //empleado.Id = Convert.ToInt32(reader["ID"]);
            empleado.Id = Convert.ToInt32(reader["ID"]);
            empleado.Nombre = reader["NOMBRE"].ToString()!;
            empleado.Apellido = reader["APELLIDO"].ToString()!;
            empleado.VendedorId = Convert.ToInt32(reader["VENDEDORID"]);
            empleado.Correo = reader["CORREO"].ToString()!;
            empleado.VendedorNombre = reader["VENDEDORNOMBRE"].ToString()!;
            empleado.Estado = reader["ESTADO"].ToString()!;
            empleado.PositionId = Convert.ToInt32(reader["POSICIONID"]);
            empleado.BranchId = Convert.ToInt32(reader["BRANCHID"]);
            empleado.PlantillaId = reader["PLANTILLAID"].ToString()!;
            empleado.Plantilla = new Plantilla
            {
                Code = reader["PLANTILLAID"].ToString()!,
                Name = reader["PLANTILLANOMBRE"].ToString()!,
                CuentaCajaId = reader["PLANTILLACAJAID"].ToString()!,
                CuentaCajaNombre = reader["PLANTILLACAJANOMBRE"].ToString()!,
                SucursalId = reader["PLANTILLASUCURSALID"].ToString()!,
                TC = Convert.ToDouble(reader["TIPOCAMBIO"]),

                Sucursal = new Sucursal
                {
                    Code = (string)(reader["PLANTILLASUCURSALID"] ?? string.Empty),
                    Name = (string)(reader["SUCURSALNOMBRE"] ?? string.Empty),
                    Direccion = (string)(reader["SUCURSALDIRECCION"] ?? string.Empty),
                    Telefono = Convert.ToInt32(reader["SUCURSALTELEFONO"] == DBNull.Value ? 0 : reader["SUCURSALTELEFONO"]),
                    Correo = (string)(reader["SUCURSALCORREO"] == DBNull.Value ? string.Empty: reader["SUCURSALCORREO"]),
                    Coordenada = (string)(reader["SUCURSALCOORDENADA"] == DBNull.Value ? string.Empty : reader["SUCURSALCOORDENADA"]),
                    EstadoId = Convert.ToInt32(reader["SUCURSALESTADO"] == DBNull.Value ? 0 : reader["SUCURSALESTADO"]),
                    AlmacenSucursalId = (string)(reader["SUCURSALALMACENID"] == DBNull.Value ? 0 : reader["SUCURSALALMACENID"]),
                    AlmacenSucursalNombre = (string)(reader["SUCURSALALMACENNOMBRE"] == DBNull.Value ? 0 : reader["SUCURSALALMACENNOMBRE"]),
                    AlmacenDistribucionId = (string)(reader["SUCURSALALMACENDESPACHOID"] == DBNull.Value ? 0 : reader["SUCURSALALMACENDESPACHOID"]),
                    AlmacenDistribucionNombre = (string)(reader["SUCURSALALMACENDESPACHONOMBRE"] == DBNull.Value ? 0 : reader["SUCURSALALMACENDESPACHONOMBRE"]),
                    AlmacenAlternoId = (string)(reader["SUCURSALALMACENALTERNOID"] == DBNull.Value ? string.Empty : reader["SUCURSALALMACENALTERNOID"]),
                    AlmacenAlternoNombre = (string)(reader["SUCURSALALMACENALTERNONOMBRE"] == DBNull.Value ? string.Empty : reader["SUCURSALALMACENALTERNONOMBRE"]),
                    Almacen05Id = (string)(reader["SUCURSALALMACEN05ID"] == DBNull.Value ? string.Empty : reader["SUCURSALALMACEN05ID"]),
                    Almacen05Nombre = (string)(reader["SUCURSALALMACEN05NOMBRE"] == DBNull.Value ? string.Empty : reader["SUCURSALALMACEN05NOMBRE"]),
                    ListaPrecioId = Convert.ToInt32(reader["SUCURSALLISTAPRECIOID"] == DBNull.Value ? 0 : reader["SUCURSALLISTAPRECIOID"]),
                    ListaPrecioNombre = (string)(reader["SUCURSALLISTAPRECIONOMBRE"] == DBNull.Value ? string.Empty : reader["SUCURSALLISTAPRECIONOMBRE"]),
                    PorcentajeDescuentoTienda = Convert.ToDouble(reader["SUCURSALDESCUENTOTIENDA"] == DBNull.Value ? 0.0 : reader["SUCURSALDESCUENTOTIENDA"]),
                    ClienteGenericoCodigo = (string)(reader["SUCURSALCLIENTEGENERICO"] == DBNull.Value ? string.Empty : reader["SUCURSALCLIENTEGENERICO"]),
                    ClienteGenericoNombre = (string)(reader["SUCURSALCLIENTEGENERICONOMBRE"] == DBNull.Value ? string.Empty : reader["SUCURSALCLIENTEGENERICONOMBRE"]),
                    DocificacionAutomaticaId = Convert.ToInt32(reader["SUCURSALDOSIFICACIONAUTOMATICAID"] == DBNull.Value ? 0 : reader["SUCURSALDOSIFICACIONAUTOMATICAID"]),
                    DocificacionManualId = Convert.ToInt32(reader["SUCURSALDOSIFICACIONMANUALID"] == DBNull.Value ? 0 : reader["SUCURSALDOSIFICACIONMANUALID"]),
                    DocificacionServicioAutomaticaId = Convert.ToInt32(reader["SUCURSALDOSIFICACIONSERVICIOAUTOMATICAID"] == DBNull.Value ? 0 : reader["SUCURSALDOSIFICACIONSERVICIOAUTOMATICAID"]),
                    DocificacionServicioManualId = Convert.ToInt32(reader["SUCURSALDOSIFICACIONSERVICIOMANUALID"] == DBNull.Value ? 0 : reader["SUCURSALDOSIFICACIONSERVICIOMANUALID"]),
                    SucursalUserName = (string)(reader["SUCURSALUSERNAME"] == DBNull.Value ? string.Empty : reader["SUCURSALUSERNAME"]),
                    SucursalPassword = (string)(reader["SUCURSALPASSWORD"] == DBNull.Value ? string.Empty : reader["SUCURSALPASSWORD"]),
                    UnidadNegocio = (string)(reader["SUCURSALUNIDADNEGOCIO"] == DBNull.Value ? string.Empty : reader["SUCURSALUNIDADNEGOCIO"]),
                    UnidadRegional = (string)(reader["SUCURSALUNIDADREGIONAL"] == DBNull.Value ? string.Empty : reader["SUCURSALUNIDADREGIONAL"]),
                    UnidadDivision = (string)(reader["SUCURSALUNIDADDIVISION"] == DBNull.Value ? string.Empty : reader["SUCURSALUNIDADDIVISION"]),
                    CiudadId = Convert.ToInt32(reader["SUCURSALREGIONALID"] == DBNull.Value ? 0 : reader["SUCURSALREGIONALID"]),
                    CiudadNombre = (string)(reader["SUCURSALREGIONALNOMBRE"] == DBNull.Value ? string.Empty : reader["SUCURSALREGIONALNOMBRE"]),
                    MunicipioId = Convert.ToInt32(reader["SUCURSALMUNICIPIOID"] == DBNull.Value ? 0 : reader["SUCURSALMUNICIPIOID"]),
                    MunicipioNombre = (string)(reader["SUCURSALMUNICIPIONOMBRE"] == DBNull.Value ? string.Empty : reader["SUCURSALMUNICIPIONOMBRE"]),
                    EntregaAutomatica = Convert.ToInt32(reader["ENTREGAAUTOMATICA"]) > 0 ? true : false,
                    ImprimeDirecto = Convert.ToInt32(reader["IMPRIMEDIRECTO"]) > 0 ? true : false,
                    BranchId = Convert.ToInt32(reader["BRANCHID"] == DBNull.Value ? 0 : reader["BRANCHID"]),
                    SucursalId = Convert.ToInt32(reader["SUCURSALID"] == DBNull.Value ? 0 : reader["SUCURSALID"]),
                    Celular = Convert.ToInt32(reader["CELULAR"] == DBNull.Value ? 0 : reader["CELULAR"]),
                    DespachoRegional = Convert.ToInt32(reader["DESPACHOREGIONAL"]) > 0 ? true : false,
                    CajaControlCodigoBs = (string)(reader["CAJACONTROLCODIGOBS"] == DBNull.Value ? string.Empty : reader["CAJACONTROLCODIGOBS"]),
                    CajaControlDescripcionBs = (string)(reader["CAJACONTROLDESCRIPCIONBS"] == DBNull.Value ? string.Empty : reader["CAJACONTROLDESCRIPCIONBS"]),
                    CajaControlCodigoSu = (string)(reader["CAJACONTROLCODIGOSU"] == DBNull.Value ? string.Empty : reader["CAJACONTROLCODIGOSU"]),
                    CajaControlDescripcionSu = (string)(reader["CAJACONTROLDESCRIPCIONSU"] == DBNull.Value ? string.Empty : reader["CAJACONTROLDESCRIPCIONSU"]),
                    LongTail = Convert.ToInt32(reader["LONGTAIL"] == DBNull.Value ? 0 : reader["LONGTAIL"]),
                    //DevolucionGeneral = Convert.ToInt32(reader["DEVOLUCION"] == DBNull.Value ? 0 : reader["DEVOLUCION"])
                }
            };

            return empleado;
        });

        public async Task<Empleado> GetEmpleadoAsync(int empleadoId)
        {
            string catalog = AppDomain.CurrentDomain.GetData("SBOCatalog")!.ToString()!;

            try
            {
                List<HanaParameter> parametros = new List<HanaParameter>();
                HanaParameter EmployeeID = new HanaParameter("@EmployeeID", empleadoId);
                EmployeeID.HanaDbType = HanaDbType.Integer;
                EmployeeID.Direction = ParameterDirection.Input;
                parametros.Add(EmployeeID);

                string sql = string.Format("{0}.BL_GETEMPLOYEEDATAV2", catalog);
                //_logger.LogInformation("antes de ejecutar ...");
                Empleado empleado = await HanaRequest.ExecuteToEntityAsync<Empleado>(sql, empleadoMapper, parametros, _logger);
                //_logger.LogInformation("fin de ejecutar ...");
                return empleado;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetEmpleadoAsync");
                throw new Exception("—Bien, Houston, hemos tenido un problema aquí.");
            }

        }
    }
}
