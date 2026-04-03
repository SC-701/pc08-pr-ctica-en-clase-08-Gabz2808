using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.TipoCambio
{
    public interface ITipoCambioServicio
    {
        Task<decimal> ObtenerTipoCambioVentaAsync();
    }

}
