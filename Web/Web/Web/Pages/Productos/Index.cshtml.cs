using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Abstracciones.Modelos.Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Reglas;
using System.Text.Json;

namespace Web.Pages.Productos
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguracion _configuracion;
        public IList<ProductoResponse> Productos { get; set; } = default!;
        public IndexModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }
        public async Task OnGet()
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints",
                "ObtenerProductos");
            using var cliente = new HttpClient();
            ClienteApiAutenticado.AgregarBearer(cliente, User);
            var solicitud = new HttpRequestMessage(HttpMethod.Get, endpoint);
            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions
            { PropertyNameCaseInsensitive = true };
            Productos = JsonSerializer.Deserialize<List<ProductoResponse>>(resultado, opciones);


        }
    }
}