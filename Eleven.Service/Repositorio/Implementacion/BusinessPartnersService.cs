using AutoMapper;
using Eleven.Data.Entidad;
using Eleven.Data.Repositorio.Implementacion;
using Eleven.Data.Repositorio.Interfaz;
using Eleven.Service.Modelo.Eleven;
using Eleven.Service.Modelo.SAP;
using Eleven.Service.Repositorio.Interfaz;
using Microsoft.Extensions.Logging;
using SAPBo.Data.Repositorio.Interfaz;
using SAPBo.Data.Entidad;

namespace Eleven.Service.Repositorio.Implementacion
{
    public class BusinessPartnersService:IBusinessPartnersService
    {
        private readonly IBoBusinessPartnersRepository _bussinessPartnersRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<BusinessPartnersService> _logger;
        public BusinessPartnersService(IBoBusinessPartnersRepository bussinessPartnersRepository, IMapper mapper, ILogger<BusinessPartnersService> logger) 
        {
            _bussinessPartnersRepository = bussinessPartnersRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<MdoBusinessPartners> GetBusinessPartnerByCardCodeAsync(string cardCode, MdoSLConexion boLogin)
        {
            try
            {
                Login login = new Login()
                {
                    SLConexion = boLogin.SLConexion,
                    UserName = boLogin.UserName
                };

                BusinessPartners entidad = await _bussinessPartnersRepository.GetBusinessPartnerByCardCodeAsync(cardCode, login);
                return _mapper.Map<MdoBusinessPartners>(entidad);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetBusinessPartnerByCardCodeAsync");
                throw new Exception("—Bien, Houston, hemos tenido un problema aquí.");
            }
        }
        public async Task<MdoBusinessPartners> GetBusinessPartnerByNroDocumentoAsync(string nroDocumento, string tipoDocumento, MdoSLConexion boLogin)
        {
            try
            {
                Login login = new Login()
                {
                    SLConexion = boLogin.SLConexion,
                    UserName = boLogin.UserName
                };

                BusinessPartners entidad = await _bussinessPartnersRepository.GetBusinessPartnerByNroDocumentoAsync(nroDocumento, tipoDocumento, login);
                return _mapper.Map<MdoBusinessPartners>(entidad);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetBusinessPartnerByNroDocumentoAsync");
                throw new Exception("—Bien, Houston, hemos tenido un problema aquí.");
            }
        }
        private BusinessPartners MapBusinessPartners(MdoBusinessPartners mdoBP, MdoEmpleado empleado)
        {
            try
            {
                BusinessPartners businessPartners = new BusinessPartners()
                {
                    //CardType = "cCustomer",
                    CardName = mdoBP.NombreCompleto!,
                    GroupCode = mdoBP.GrupoCliente,
                    Series = mdoBP.SerieCliente,
                    //Currency = "BS",
                    FederalTaxID = mdoBP.NroDocumento, // NroDocumento
                    U_Tipo_Documento = mdoBP.TipoDocumentoID,

                    //GENERAL
                    Cellular = mdoBP.MovilNro,
                    EmailAddress = mdoBP.Correo,
                    SalesPersonCode = empleado!.VendedorId,

                    //RETENCIONES
                    //SubjectToWithholdingTax = "boYES",
                    //WTCode = "IT",
                    U_COMPLEFE = mdoBP.Complemento,
                    U_TIPDOC = mdoBP.TipoDocumento.DescripcionSIN,
                };

                foreach (MdoContactEmployee item in mdoBP.ContactEmployees!)
                {
                    ContactEmployee contactEmployee = new ContactEmployee()
                    {
                        // El primer campo Name, debe ser "NATURAL". Una extension de los datos del cliente
                        Name = item.Name,
                        FirstName = string.IsNullOrEmpty(item.PrimerNombre) ? "" : item.PrimerNombre.ToUpper(),
                        MiddleName = string.IsNullOrEmpty(item.SegundoNombre) ? "" : item.SegundoNombre.ToUpper(),
                        LastName = string.IsNullOrEmpty(item.ApellidoPaterno) ? "" : item.ApellidoPaterno.ToUpper(),
                        U_Apellido_Materno = string.IsNullOrEmpty(item.ApellidoMaterno) ? "" : item.ApellidoMaterno.ToUpper(),
                        U_Apellido_de_casada = string.IsNullOrEmpty(item.ApellidoCasada) ? "" : item.ApellidoCasada.ToUpper(),
                        MobilePhone = item.NumeroMovil,
                        E_Mail = item.Correo!.Trim(),
                        DateOfBirth = item.FechaNacimiento,
                        Gender = item.Genero == null ? "gt_Undefined" : item.Genero == "M" ? "gt_Male" : "gt_Female",
                        U_Tipo_Documento = item.TipoDocumentoID.ToString(),
                        U_Estado_Civil = item.EstadoCivilID > 0 ? item.EstadoCivilID.ToString() : "",
                        U_Expedido = item.Expedido
                    };
                    businessPartners.ContactEmployees!.Add(contactEmployee);
                }

                foreach (MdoBPAddress item in mdoBP.BPAddresses!)
                {
                    BPAddress bPAddress = new BPAddress()
                    {
                        // AddressType = bo_BillTo (Factura), bo_ShipTo (Entrega)
                        AddressType = item.AddressType,
                        AddressName = item.Nombre, // Nombre de la la direccion. Ejm. "DIRCLIENTE"
                        AddressName2 = item.Avenida,
                        AddressName3 = item.Telefono,
                        Street = item.Calle,
                        StreetNo = item.CalleNro,
                        Block = item.Barrio,
                        Country = "BO",
                        State = item.DepartamentoId == 0 ? "1" : item.DepartamentoId.ToString(),
                        TaxCode = "IVA",
                        BuildingFloorRoom = string.Format("{0}, {1}", item.Edificio, item.EdificioDepartamento),
                        U_Coordenadas = item.Coordenadas,
                        U_Nro_Medidor_Luz = item.NroMedidor,
                        U_Manzana = item.Mz,
                        U_Referencia = string.IsNullOrEmpty(item.PersonaReferencia) ? "" : item.PersonaReferencia.ToUpper(),
                        U_Unidad_Vecinal = item.UV
                    };
                    businessPartners.BPAddresses!.Add(bPAddress);
                }
                
                return businessPartners;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "MapBusinessPartners");
                throw new Exception("—Bien, Houston, hemos tenido un problema aquí.");
            }
        }

        public async Task<MdoBusinessPartners> CreateBusinessPartnerAsync(MdoBusinessPartners mdoBP, MdoSLConexion boLogin)
        {
            try
            {
                Login login = new Login()
                {
                    SLConexion = boLogin.SLConexion,
                    UserName = boLogin.UserName
                };

                BusinessPartners businessPartners = MapBusinessPartners(mdoBP, mdoBP.Empleado!);

                BusinessPartners entidad = await _bussinessPartnersRepository.CreateBusinessPartnerAsync(businessPartners, login);
                return _mapper.Map<MdoBusinessPartners>(entidad);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CreateBusinessPartnerAsync");
                throw new Exception("—Bien, Houston, hemos tenido un problema aquí.");
            }
        }

        public async Task<List<MdoBatchResponse>> CreateBatchBusinessPartnerAsync(List<MdoBusinessPartners> mdoBPs, MdoEmpleado mdoEmpleado, MdoSLConexion boLogin)
        {
            try
            {
                int indice = 0;
                Login login = new Login()
                {
                    SLConexion = boLogin.SLConexion,
                    UserName = boLogin.UserName
                };

                List<BusinessPartners> BPs = new List<BusinessPartners>();
                List<BatchResponse> results = new List<BatchResponse>();
                List<MdoBatchResponse> mdoResults = new List<MdoBatchResponse>();
                
                foreach (MdoBusinessPartners mdoBP in mdoBPs)
                {
                    BusinessPartners businessPartners = MapBusinessPartners(mdoBP, mdoEmpleado);
                    BPs.Add(businessPartners);
                    if (indice == 9)
                    {
                        results = await _bussinessPartnersRepository.CreateBatchBusinessPartnerAsync(BPs, login);
                        BPs.Clear();
                        indice = 0;
                    }
                    indice++;
                }
                if (indice > 0)
                {
                    List<BatchResponse> res = await _bussinessPartnersRepository.CreateBatchBusinessPartnerAsync(BPs, login);
                    results.AddRange(res);
                }
                return _mapper.Map<List<MdoBatchResponse>>(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CreateBatchBusinessPartnerAsync");
                throw new Exception("—Bien, Houston, hemos tenido un problema aquí.");
            }
        }
    }
}
