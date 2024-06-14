using Eleven.Service.Modelo.Eleven;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleven.Service.Modelo.SAP
{
    public class MdoIncome
    {
        public MdoLogin boLogin { get; set; } = null!;
        public MdoDocument boDocument { get; set; } = null!;

    }
}
