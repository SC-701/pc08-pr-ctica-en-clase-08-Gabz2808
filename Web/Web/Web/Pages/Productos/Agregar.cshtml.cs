using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Abstracciones.Modelos.Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Reglas;
using System.Net;
using System.Text.Json;

namespace Web.Pages.Productos
{
    public class AgregarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;
        public IList<ProductoResponse> productos { get; set; } = default!;
        public AgregarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }
        [BindProperty]
        public ProductoRequest producto { get; set; }
        [BindProperty]
        public List<SelectListItem> categorias { get; set; }
        [BindProperty]
        public List<SelectListItem> subcategorias { get; set; }
        [BindProperty]
        public Guid categoriaSeleccionada { get; set; }
        public async Task<ActionResult> OnGet()
        {
            await ObtenerCategorias();
            return Page();
        }

        public async Task<ActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints",
              "AgregarProducto");
            using var cliente = new HttpClient();
            ClienteApiAutenticado.AgregarBearer(cliente, User);
            var respuesta = await cliente.PostAsJsonAsync(endpoint, producto);
            respuesta.EnsureSuccessStatusCode();
            return RedirectToPage("./Index");
        }

        private async Task ObtenerCategorias()
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints",
               "ObtenerCategorias");
            using var cliente = new HttpClient();
            ClienteApiAutenticado.AgregarBearer(cliente, User);
            var solicitud = new HttpRequestMessage(HttpMethod.Get, endpoint);
            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions
            { PropertyNameCaseInsensitive = true };
            var resultaddeserializado = JsonSerializer.Deserialize<List<Categoria>>(resultado, opciones);
            categorias = resultaddeserializado.Select(m =>
            new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.Nombre
            }
            ).ToList();

        }
        private async Task<List<SubCategoria>> ObtenerSubCategoria(Guid categoriaID)
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints",
               "ObtenerSubCategorias");
            using var cliente = new HttpClient();
            ClienteApiAutenticado.AgregarBearer(cliente, User);
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, categoriaID));
            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions
                { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<List<SubCategoria>>(resultado, opciones);
            }

            return new List<SubCategoria>();
        }




        public async Task<JsonResult> OnGetObtenerSubCategoria(Guid categoriaID)
        {
            var subcategorias = await ObtenerSubCategoria(categoriaID);
            return new JsonResult(subcategorias);
        }
    }
}