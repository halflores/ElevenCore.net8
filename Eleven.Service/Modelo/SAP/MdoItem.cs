using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleven.Service.Modelo.SAP
{
    public class MdoItem
    {
        #region Caracteristicas
        public string? U_Descripcion { get; set; }
        public string? U_Descripcion1 { get; set; }
        public string? U_Descripcion2 { get; set; }
        public string? U_Descripcion3 { get; set; }
        public string? U_Descripcion4 { get; set; }
        #endregion
        #region Comercial
        public string? User_Text { get; set; }
        #endregion
        #region Compras
        public string? ValidRemarks { get; set; }
        public string? Mainsupplier { get; set; }
        public string? U_Nandina { get; set; }
        #endregion
        #region Dimensiones
        public decimal? PurchaseUnitWeight { get; set; }
        public int? PurchaseWeightUnit { get; set; }
        public string? PurchaseUnit { get; set; }
        public decimal? PurchaseUnitVolume { get; set; }
        public int? PurchaseVolumeUnit { get; set; }
        public string? SalesUnit { get; set; }
        public decimal? PurchaseUnitLength { get; set; }
        public int? PurchaseLengthUnit { get; set; }
        public decimal? PurchaseUnitWidth { get; set; }
        public int? PurchaseWidthUnit { get; set; }
        public decimal? SalesUnitHeight { get; set; }
        public int? SalesHeightUnit { get; set; }
        public decimal? SalesUnitWidth { get; set; }
        public int? SalesWidthUnit { get; set; }
        public decimal? SalesUnitLength { get; set; }
        public int? SalesLengthUnit { get; set; }
        public decimal? SalesUnitVolume { get; set; }
        public int? SalesVolumeUnit { get; set; }
        public decimal? SalesUnitWeight { get; set; }
        public int? SalesWeightUnit { get; set; }
        public decimal? PurchaseUnitHeight { get; set; }
        public int? PurchaseHeightUnit { get; set; }
        #endregion

        #region General
        public int? ItemsGroupCode { get; set; }
        public int? U_Conjunto { get; set; }
        public int? U_SubCategoria { get; set; }
        public string ItemCode { get; set; } = null!;
        public string? ItemName { get; set; }
        public string U_Type { get; set; } = null!;
        public string? BarCode { get; set; }
        public int? Manufacturer { get; set; }
        public string? SWW { get; set; }
        public string? SupplierCatalogNo { get; set; }
        #endregion

        #region Inventario
        public string? GLMethod { get; set; }
        public string? ManageStockByWarehouse { get; set; }
        #endregion
        #region Operaciones
        public string? PurchaseItem { get; set; }
        public string? InventoryItem { get; set; }
        public string? SalesItem { get; set; }
        public string? ManageSerialNumbers { get; set; }
        public string? ManageBatchNumbers { get; set; }
        public string? Valid { get; set; }
        public string? Frozen { get; set; }
        public string? SRIAndBatchManageMethod { get; set; }
        public string? VatLiable { get; set; }
        public string? WTLiable { get; set; }
        #endregion
        #region Planificacion
        public decimal? MinOrderQuantity { get; set; }
        public int? ToleranceDays { get; set; }
        public int? OrderIntervals { get; set; }
        public string? ProcurementMethod { get; set; }
        public string? PlanningSystem { get; set; }
        public decimal? OrderMultiple { get; set; }
        public int? LeadTime { get; set; }
        #endregion
        #region Propiedades
        public string? U_Category { get; set; }
        public int? U_MesGarantiaDismac { get; set; }
        public int? U_MesGarantiaProveedor { get; set; }
        #endregion
        #region GSE
        public int? U_ActvEC { get; set; }
        public int? U_ProSIN { get; set; }
        public string? U_CodNandi { get; set; }
        #endregion
        #region GSE (Mayorista)
        public int? U_ActvECM { get; set; }
        public int? U_ProSINM { get; set; }
        public string? U_CodNandiM { get; set; }
        #endregion


    }
}
