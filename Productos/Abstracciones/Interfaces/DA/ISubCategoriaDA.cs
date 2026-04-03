using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.DA
{
    public interface ISubCategoriaDA
    {
        Task<IEnumerable<SubCategoria>> Obtener(Guid id);

    }
}
