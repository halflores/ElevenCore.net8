using Eleven.Service.Modelo.SAP;
using SAPBo.Data.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleven.Service.Repositorio.Interfaz
{
    public interface IPaymentService
    {
        Task<MdoPayment> CreatePaymentAsync(MdoPayment modelo, MdoSLConexion boLogin);
    }
}
