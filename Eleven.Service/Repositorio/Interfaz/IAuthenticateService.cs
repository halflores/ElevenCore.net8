﻿using Eleven.Data.Entidad;
using Eleven.Service.Modelo.Eleven;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleven.Service.Repositorio.Interfaz
{
    public interface IAuthenticateService //: IServiceAsync<Usuario, MdoUsuario>
    {
        bool CheckPassword(MdoUsuario usuario, string password);

    }
}