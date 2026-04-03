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
    public class CategoriaFlujo : ICategoriaFlujo
    {
        private ICategoriaDA _categoriaDA;
        public CategoriaFlujo(ICategoriaDA categoriaDA)
        {
            _categoriaDA = categoriaDA;
        }
        public Task<IEnumerable<Categoria>> Obtener()
        {
            return _categoriaDA.Obtener();
        }
    }
}
