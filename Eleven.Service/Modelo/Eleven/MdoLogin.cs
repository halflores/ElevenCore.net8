using B1SLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleven.Service.Modelo.Eleven
{
    public class MdoLogin
    {
        public string Login { get; set; } = null!;

        public string Password { get; set; } = null!;

    }
}
