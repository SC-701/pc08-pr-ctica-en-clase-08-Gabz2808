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
    public class SubCategoriaController : ControllerBase, ISubCategoriaController

    {
        private readonly ISubCategoriaFlujo _subcategoriaFlujo;

        public SubCategoriaController(ISubCategoriaFlujo subcategoriaFlujo)
        {
            _subcategoriaFlujo = subcategoriaFlujo;
        }

        [HttpGet("ObtenerSubCategorias")]
        public async Task<IActionResult> Obtener(Guid IdCategoria)
        {
            var resultado = await _subcategoriaFlujo.Obtener(IdCategoria);
            if (!resultado.Any())
                return NoContent();
            return Ok(resultado);
        }
    }
}
