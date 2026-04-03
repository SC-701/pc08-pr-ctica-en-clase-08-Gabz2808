using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flujos
{
    public class SubCategoriaFlujo : ISubCategoriaFlujo
    {
        private ISubCategoriaDA _subcategoriaDA;
        public SubCategoriaFlujo(ISubCategoriaDA subcategoriaDA)
        {
            _subcategoriaDA = subcategoriaDA;
        }
        public async Task<IEnumerable<SubCategoria>> Obtener(Guid IdCategoria)
        {
            return await _subcategoriaDA.Obtener(IdCategoria);
        }
    }
}
