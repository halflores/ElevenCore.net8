using B1SLayer;
using Microsoft.Extensions.Logging;
using SAPBo.Data.Entidad;
using SAPBo.Data.Repositorio.Interfaz;
using System.Data;
using Utils.Helper;

namespace SAPBo.Data.Repositorio.Implementacion
{
    public class BoBusinessPartnersRepository : IBoBusinessPartnersRepository
    {
        private readonly Dictionary<string, SLConnection> _slConnections;
        private readonly ILogger<BoBusinessPartnersRepository> _logger;
        public BoBusinessPartnersRepository(Dictionary<string, SLConnection> slConnections, ILogger<BoBusinessPartnersRepository> logger)
        {
            _slConnections = slConnections;
            _logger = logger;
        }
        public async Task<BusinessPartners> GetBusinessPartnerByNroDocumentoAsync(string nroDocumento, string tipoDocumento, Login login)
        {
            try
            {
                List<BusinessPartners>? item = await _slConnections[login.SLConexion] 
                   .Request("BusinessPartners") 
                   .Select("CardCode, " +
                            "CardName, " +
                            "Cellular, " +
                            "FederalTaxID, " +
                            "U_Tipo_Documento")
                    .Filter(string.Format("FederalTaxID eq '{0}' and U_Tipo_Documento eq {1} and CardType eq 'cCustomer'", nroDocumento, tipoDocumento))
                   .GetAsync<List<BusinessPartners>>();

                return item.FirstOrDefault()!;
            }
            catch (Exception ex)
            {
                //LogException.Write(ex);
                _logger.LogError(ex, "GetBusinessPartnerByNroDocumentoAsync");
                throw new Exception("—Bien, Houston, hemos tenido un problema aquí.");
            }
        }

        public async Task<BusinessPartners> GetBusinessPartnerByCardCodeAsync(string cardCode, Login login)
        {
            try
            {
                BusinessPartners? item = await _slConnections[login.SLConexion]
                   .Request("BusinessPartners", cardCode)
                   .GetAsync<BusinessPartners>();

                return item;
            }
            catch (Exception ex)
            {
                //LogException.Write(ex);
                _logger.LogError(ex, "GetBusinessPartnerByCardCodeAsync");
                throw new Exception("—Bien, Houston, hemos tenido un problema aquí.");
            }
        }

        public async Task<BusinessPartners> CreateBusinessPartnerAsync(BusinessPartners bpEntidad, Login login)
        {
            try
            {
                var businessPartners = new
                {
                    CardCode = "",
                    CardType = "cCustomer",
                    CardName = bpEntidad.CardName,
                    GroupCode = bpEntidad.GroupCode,
                    Series = bpEntidad.Series,
                    Currency = "BS",
                    FederalTaxID = bpEntidad.FederalTaxID,

                    //GENERAL                    
                    Cellular = bpEntidad.Cellular,
                    EmailAddress = bpEntidad.EmailAddress,
                    SalesPersonCode = bpEntidad.SalesPersonCode,

                    //RETENCIONES
                    SubjectToWithholdingTax = "boYES",
                    WTCode = "IT",
                    U_Tipo_Documento = bpEntidad.U_Tipo_Documento,
                    U_COMPLEFE = bpEntidad.U_COMPLEFE,
                    U_TIPDOC = bpEntidad.U_TIPDOC,

                    ContactEmployees = bpEntidad.ContactEmployees?
                                        .Select(x => new { 
                                            Name = x.Name,
                                            FirstName = x.FirstName,
                                            MiddleName = x.MiddleName,
                                            LastName = x.LastName,
                                            U_Apellido_Materno = x.U_Apellido_Materno,
                                            U_Apellido_de_casada = x.U_Apellido_de_casada,
                                            MobilePhone = x.MobilePhone,
                                            E_Mail = x.E_Mail,
                                            DateOfBirth = x.DateOfBirth,
                                            Gender = x.Gender,
                                            U_Tipo_Documento = x.U_Tipo_Documento,
                                            U_Expedido = x.U_Expedido,
                                            U_Estado_Civil = x.U_Estado_Civil
                                        }).ToList(),
                    BPAddresses = bpEntidad.BPAddresses?.Select(y => new {
                                            AddressType = y.AddressType,
                                            AddressName = y.AddressName,
                                            AddressName2 = y.AddressName2,
                                            AddressName3 = y.AddressName3,
                                            Street = y.Street,
                                            StreetNo = y.StreetNo,
                                            Block = y.Block,
                                            Country = "BO",
                                            State = y.State,
                                            TaxCode = "IVA",
                                            BuildingFloorRoom = y.BuildingFloorRoom,
                                            U_Coordenadas = y.U_Coordenadas,
                                            U_Nro_Medidor_Luz = y.U_Nro_Medidor_Luz,
                                            U_Manzana = y.U_Manzana,
                                            U_Referencia = y.U_Referencia,
                                            U_Unidad_Vecinal = y.U_Unidad_Vecinal,
                                            U_Partido = "",
                                            U_Prefijo = ""
                                        }).ToList(),
                };

                BusinessPartners createdBP = await _slConnections[login.SLConexion]
                                            .Request("BusinessPartners")
                                            .PostAsync<BusinessPartners>(businessPartners);
                return createdBP;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CreateBusinessPartnerAsync");
                throw;
            }
        
        }

        public async Task<List<BatchResponse>> CreateBatchBusinessPartnerAsync(List<BusinessPartners> bpEntidades, Login login)
        {
            try
            {
                List<BatchResponse> batchResponses = new List<BatchResponse>();
                List<SLBatchRequest> slBatchRequests = new List<SLBatchRequest>();
                foreach (BusinessPartners bp in bpEntidades)
                {
                    SLBatchRequest slBatchRequest = new SLBatchRequest(HttpMethod.Post, "BusinessPartners", bp.ToJson(_logger), 1);
                    slBatchRequests.Add(slBatchRequest);
                }

                Tuple<HttpResponseMessage[], string> results = await _slConnections[login.SLConexion].PostBatchAsync(slBatchRequests.ToArray<SLBatchRequest>());

                foreach (HttpResponseMessage resultItem in results.Item1)
                {
                    if (Convert.ToInt32(resultItem.StatusCode) == 201)
                    {
                        string urlLocation = resultItem.Headers.GetValues("Location").SingleOrDefault()!;
                        string docEntry = Functions.GetDocEntry(urlLocation!)!;

                        batchResponses.Add(new BatchResponse
                        {
                            StatusCode = Convert.ToInt32(resultItem.StatusCode),
                            //ContentID = Convert.ToInt32(resultItem.Headers.Select(x => x.Key == "").SingleOrDefault()),
                            Location = urlLocation,
                            DocEntry = Convert.ToInt32(docEntry),
                            Description = "ÉXITO"
                        });
                    }
                    else
                    {
                        batchResponses.Add(new BatchResponse
                        {
                            StatusCode = Convert.ToInt32(resultItem.StatusCode),
                            //ContentID = Convert.ToInt32(resultItem.Headers.GetValues("Content-ID").SingleOrDefault()),
                            Location = @"—Bien, Houston, hemos tenido un problema aquí.",
                            DocEntry = 0,
                            Description = results.Item2
                        });
                    }

                }
                return batchResponses;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CreateBusinessPartnerAsync");
                throw;
            }

        }


    }
}
