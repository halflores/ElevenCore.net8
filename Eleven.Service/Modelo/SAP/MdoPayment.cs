﻿using SAPBo.Data.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleven.Service.Modelo.SAP
{
    public class MdoPayment
    {
        public MdoPayment()
        {
            PaymentInvoices = new List<MdoPaymentInvoice>();
            PaymentCreditCards = new List<MdoPaymentCreditCard>();
            PaymentChecks = new List<MdoPaymentCheck>();
            PaymentAccounts = new List<MdoPaymentAccount>();
        }
        public int? Series { get; set; }
        public int? DocEntry { get; set; }
        public int? DocNum { get; set; }
        public string DocType { get; set; } = "rCustomer"; //rSupplier = Proveedor - rAccount = Cuenta
        public string CardCode { get; set; }
        public DateTime? DocDate { get; set; }
        public DateTime? TaxDate { get; set; }
        public string ControlAccount { get; set; }

        public string CashAccount { get; set; }
        public string? CheckAccount { get; set; }
        public string? TransferAccount { get; set; }

        public DateTime? TransferDate { get; set; }

        public double? CashSum { get; set; }
        public double? TransferSum { get; set; }

        public string? TransferReference { get; set; }

        public List<MdoPaymentInvoice> PaymentInvoices { get; set; }
        public List<MdoPaymentCreditCard> PaymentCreditCards { get; set; }
        public List<MdoPaymentCheck> PaymentChecks { get; set; }
        public List<MdoPaymentAccount> PaymentAccounts { get; set; }

        public int? U_TipoPago { get; set; }
        public string U_CajaControl { get; set; }
        public long? U_PagoClever { get; set; }
        public int? U_OrigenAplicacion { get; set; }
        public int? U_LB_TipoDocumento { get; set; }
        public string? U_Ref { get; set; }
        public long? U_NumeroPedido { get; set; }
        public string? CounterReference { get; set; }

        public string? Remarks { get; set; }
        public string? JournalRemarks { get; set; }
        public string DocCurrency { get; set; } = "BS";
        public string? Address { get; set; }
    }
}
