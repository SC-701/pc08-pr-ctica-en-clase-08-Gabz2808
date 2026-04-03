using Abstracciones.Interfaces.Api;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using DA;
using Flujos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/")]
    [Authorize(Roles = "1")]
    public class CategoriaController : ControllerBase, ICategoriaController

    {
        private readonly ICategoriaFlujo _categoriaFlujo;

        public CategoriaController(ICategoriaFlujo categoriaFlujo)
        {
            _categoriaFlujo = categoriaFlujo;
        }

        [HttpGet("ObtenerCategorias")]
        public async Task<IActionResult> Obtener()
        {
            var resultado = await _categoriaFlujo.Obtener();
            if (!resultado.Any())
                return NoContent();
            return Ok(resultado);
        }
    }
}
