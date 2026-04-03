using Abstracciones.Interfaces.Api;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using DA;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/")]
    [Authorize(Roles = "1")]
    public class ProductoController : ControllerBase, IProductoController
    {
        private readonly IProductoFlujo _productoFlujo;
        private readonly ILogger<ProductoController> _logger;
            
        public ProductoController(IProductoFlujo productoFlujo, ILogger<ProductoController> logger)
        {
            _productoFlujo = productoFlujo;
            _logger = logger;
        }
        [HttpPost("AgregarProducto")]
        public async Task<IActionResult> Agregar(ProductoRequest producto)
        {
            var resultado = await _productoFlujo.Agregar(producto);
            return CreatedAtAction(nameof(Obtener), new { Id = resultado }, null);
        }
        [HttpPut("EditarProducto/{Id}")]
        public async Task<IActionResult> Editar(Guid Id, ProductoRequest producto)
        {
            var resultado = await _productoFlujo.Editar(Id, producto);
            return Ok(resultado);

        }
        [HttpDelete("EliminarProducto/{Id}")]
        public async Task<IActionResult> Eliminar([FromRoute] Guid Id)
        {
            var resultado = await _productoFlujo.Eliminar(Id);
            return NoContent();
        }
        [HttpGet("ObtenerProductos")]
        public async Task<IActionResult> Obtener()
        {
            var resultado = await _productoFlujo.Obtener();
            if(!resultado.Any())
                return NoContent();
            return Ok(resultado);
        }
        [HttpGet("ObtenerProducto/{Id}")]
        public async Task<IActionResult> Obtener([FromRoute]Guid Id)
        {
            var resultado = await _productoFlujo.Obtener(Id);
            return Ok(resultado);
        }
    }
}
