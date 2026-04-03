using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Api
{
    public interface ICategoriaController
    {
        Task<IActionResult> Obtener();

    }
}
