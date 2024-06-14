using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Helper
{
    public static class DocumentType
    {
        [Serializable]
        public enum TipoDocumento
        {
            QUOTATIONS = 23,
            ORDER = 17,
            INVOICE = 13,
            PAYMENT = 24,
            PICKINLIST = 156
        }

        [Serializable]
        public enum TypeProduct
        {
            ARTICULO = 1,
            SERVICIO = 2
        }
    }
}
