using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Abstracciones.Modelos.Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Reglas;
using System.Text.Json;

namespace Web.Pages.Productos
{
    public class EliminarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public EliminarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public ProductoResponse producto { get; set; } = default!;
        public async Task<ActionResult> OnGet(Guid? id)
        {
            if (id == Guid.Empty)
                return NotFound();
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints",
              "ObtenerProducto");
            using var cliente = new HttpClient();
            ClienteApiAutenticado.AgregarBearer(cliente, User);
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, id));
            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions
            { PropertyNameCaseInsensitive = true };
            producto = JsonSerializer.Deserialize<ProductoResponse>(resultado, opciones);
            return Page();

        }
        public async Task<ActionResult> OnPost(Guid? id)
        {
            if (id == null || id == Guid.Empty)
                return NotFound();

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints",
              "EliminarProducto");
            using var cliente = new HttpClient();
            ClienteApiAutenticado.AgregarBearer(cliente, User);
            var solicitud = new HttpRequestMessage(HttpMethod.Delete, string.Format(endpoint, id));
            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            return RedirectToPage("./Index");
        }
    }
}