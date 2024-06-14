using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPBo.Data.Entidad
{
    public class DocumentLine
    {
        public DocumentLine()
        {
            DocumentLineAdditionalExpenses = new List<DocumentLineAdditionalExpense>();
            SerialNumbers = new List<SerialNumber>();
            LineTaxJurisdictions = new List<JuridiccionFiscal>();
        }

        public int LineNum { get; set; }
        public string ItemCode { get; set; } = null!;
        public double? Price { get; set; }
        public double? UnitPrice { get; set; }
        public double? Quantity { get; set; }
        public double? DiscountPercent { get; set; }
        public double Discount { get; set; } = 0;
        public string TaxCode { get; set; } = "IVA";
        public double? PriceAfterVAT { get; set; }
        public string? WhsCode { get; set; }
        public int? SalesPersonCode { get; set; }
        public string? FreeText { get; set; }
        public int? UoMEntry { get; set; }
        //PARAMETROS PARA SERVICIO REFINANCIADO
        public string ItemDescription { get; set; }
        public string AccountCode { get; set; }

        //PARAMETROS PARA REFERENCIA
        public int? BaseType { get; set; }
        public string? BaseEntry { get; set; }
        public int? BaseLine { get; set; }

        //CONFIGURACION DE IMPUESTO IT
        public List<DocumentLineAdditionalExpense> DocumentLineAdditionalExpenses { get; set; }
        //CENTRO DE COSTOS
        public string CostingCode { get; set; }
        public string CostingCode2 { get; set; }
        public string CostingCode3 { get; set; }
        public string COGSCostingCode { get; set; }
        public string COGSCostingCode2 { get; set; }
        public string COGSCostingCode3 { get; set; }

        //PARAMETROS PARA VENTA
        public double? U_PrecioBase { get; set; }
        public double? U_PrecioDescontado { get; set; }
        public string U_ClaCom { get; set; }
        public double? U_DescuentoPrimeraCompra { get; set; }
        public double? U_DescuentoCampana { get; set; }
        public double? U_DescuentoRating { get; set; }
        public double? U_DescuentoAfinidad { get; set; }
        public double? U_DescuentoPOS { get; set; }
        public double? U_DescuentoSAP { get; set; }
        public string ItemDetails { get; set; }
        public double? U_ValeDismaclub { get; set; }
        public int? PickListIdNumber { get; set; }
        public double? U_DescuentoGiftCard { get; set; }
        public double? U_DescuentoEcommerce { get; set; }

        //ENTREGA
        public string BackOrder { get; set; } = "tYES";
        public string SerialNum { get; set; }
        public List<SerialNumber> SerialNumbers { get; set; }
        public bool EntregaAutomatica { get; set; }

        //FACTURA PRECIOS
        public double? GrossPrice { get; set; }
        public double? GrossTotal { get; set; }

        //ORDEN VENTA
        public int TypeSale { get; set; } //CondicionVenta.CONTADO, CondicionVenta.MINICUOTA
        public int TypeProduct { get; set; } //TipoVenta.ARTICULO, TipoVenta.SERVICIO

        //PARAMETRO NOTA CREDITO
        public List<JuridiccionFiscal> LineTaxJurisdictions { get; set; }

        //PARAMETROS PARA FACTURACION SIN
        public string U_SNumSerie { get; set; }
        public string U_SNumImei { get; set; }
        public string DistributeExpense { get; set; }
        public string LineVendor { get; set; }
        public DateTime? RequiredDate { get; set; }
        public int? U_OrderNum { get; set; }

    }

}
