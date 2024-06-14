namespace Eleven.Service.Modelo.SAP
{
    public class MdoDocument
    {
        public MdoDocument()
        {
            DocumentLines = new List<MdoDocumentLine>();
            //DocumentInstallments = new List<DocumentInstallment>();
            WithholdingTaxDataCollection = new List<MdoWithholdingTaxData>();
        }
        public int? DocEntry { get; set; }
        public int? DocNum { get; set; }
        public int? Series { get; set; }
        public string? DocumentStatus { get; set; }
        public string? CardCode { get; set; }
        public string? CardName { get; set; }
        public DateTime? DocDate { get; set; }
        public DateTime? DocDueDate { get; set; }
        public DateTime? TaxDate { get; set; }
        public DateTime? RequriedDate { get; set; }
        public DateTime? CreationDate { get; set; }
        public string? ReserveInvoice { get; set; }
        public string? DocType { get; set; }
        public List<MdoDocumentLine> DocumentLines { get; set; }
        public int? SalesPersonCode { get; set; }
        public int? DocumentsOwner { get; set; }
        public string? Comments { get; set; }
        public int? PaymentGroupCode { get; set; }
        public string? FederalTaxID { get; set; }
        public double? DocTotal { get; set; }
        //BANCARISACION
        public int? Indicator { get; set; }
        public int? U_LB_TipoTransaccion { get; set; }
        public int? U_LB_ModalidadTransa { get; set; }
        public string? U_LB_Bancarizacion { get; set; }
        //CONFIGURACION CUOTAS
        public int? NumberOfInstallments { get; set; }
        //public List<DocumentInstallment> DocumentInstallments { get; set; }
        //CONFIGURACION IMPUESTOS
        public List<MdoWithholdingTaxData> WithholdingTaxDataCollection { get; set; }
        //PARAMETROS PARA VENTA
        public double? U_AnticipoSugeridoMonto { get; set; }
        public double? U_AnticipoSugeridoPorcentaje { get; set; }
        public string? U_Rating { get; set; }
        public string? U_GrupoAfinidad { get; set; }
        public string? U_CajaControl { get; set; }
        //PARAMETROS PARA FACTURA
        public string? U_LB_NumeroFactura { get; set; }
        public string? U_LB_NumeroAutorizac { get; set; }
        public string? U_LB_CodigoControl { get; set; }
        public DateTime? U_LB_FechaLimiteEmis { get; set; }
        public int? TransNum { get; set; }

        public string? U_EXXISOrigen { get; set; }
        public string? U_CodigoClever { get; set; }
        public string? U_LB_RazonSocial { get; set; }
        public long? U_OrderNum { get; set; }
        public double? U_EXX_FE_MontoGifCard { get; set; }
        //PARAMETRO PARA REFINANCIACION
        public string? ControlAccount { get; set; }

        public string? Leyenda { get; set; }
        public string? Actividad { get; set; }

        //public DeliveryJournal EntregaAsiento { get; set; }
        /// <summary>
        /// CONDICION VENTA (CONTADO, MINICUOTA)
        /// </summary>
        public int TypeSale { get; set; } //CondicionVenta.CONTADO, CondicionVenta.MINICUOTA
        /// <summary>
        /// TIPO PRODUCTO (ARTICULO, SERVICIO)
        /// </summary>
        public int TypeProduct { get; set; } //TipoVenta.ARTICULO, TipoVenta.SERVICIO
        public string? Cancelled { get; set; }
        public bool LongTailPreVenta { get; set; }

        //PARAMETROS PARA NOTA CREDITO
        public double? U_LB_TotalNCND { get; set; }
        public string? U_LB_NFacturaNCND { get; set; }
        public DateTime? U_LB_FechaNCND { get; set; }
        public string? U_LB_NAutorizacNCND { get; set; }
        public int? U_LB_DocEntry { get; set; }

        //PARAMETROS PARA FACTURACION SIN
        public string? U_TIPDOC { get; set; }
        public string? U_CORREO { get; set; }
        public string? U_CELULAR { get; set; }
        public string? U_TIPDOC_SIN { get; set; }
        public string? U_MPAGO_SIN { get; set; }

        public string? U_CLAVE_ACCESO { get; set; }

        public string? U_CLAVE_CUIS { get; set; }
        public string? U_CLAVE_CUFD { get; set; }
        public string? U_CLAVE_CONTRL { get; set; }

        public string? U_DOCAFECT { get; set; }
        public string? U_CDOCAFECT { get; set; }
        public DateTime? U_FECHA_DOAFECT { get; set; }
        public string? U_TarjetaFE { get; set; }
        public string? U_OBSERVACION_FACT { get; set; }
        //PARAMETRO ORIGEN APLICACION
        public string? U_Aplicacion { get; set; }
    }
}
