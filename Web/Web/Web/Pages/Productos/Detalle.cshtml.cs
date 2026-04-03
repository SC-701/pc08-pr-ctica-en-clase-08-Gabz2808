using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Abstracciones.Modelos.Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Reglas;
using System.Text.Json;

namespace Web.Pages.Productos
{
    public class DetalleModel : PageModel
    {
        private readonly IConfiguracion _configuracion;
        public ProductoResponse producto{ get; set; } = default!;
        public DetalleModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }
        public async Task OnGet(Guid? id)
        {
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

        }
    }
}
