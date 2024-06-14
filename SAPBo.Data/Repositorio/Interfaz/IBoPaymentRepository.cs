using B1SLayer;
using SAPBo.Data.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPBo.Data.Repositorio.Interfaz
{
    public interface IBoPaymentRepository
    {
        Task<Payment> CreatePaymentAsync(Payment ePago, Login login);
    }
}
