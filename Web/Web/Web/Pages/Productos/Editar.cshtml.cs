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
    public class EditarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;
        public IList<ProductoResponse> productos { get; set; } = default!;
        public EditarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        [BindProperty]
        public ProductoResponse productoResponse { get; set; }
        [BindProperty]
        public List<SelectListItem> categorias { get; set; }
        [BindProperty]
        public List<SelectListItem> subcategorias { get; set; }
        [BindProperty]
        public Guid categoriaSeleccionada { get; set; }
        [BindProperty]
        public Guid subcategoriaSeleccionada { get; set; }

        public async Task<ActionResult> OnGet(Guid? id)
        {
            if (id == null || id == Guid.Empty)
                return NotFound();
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerProducto");
            using var cliente = new HttpClient();
            ClienteApiAutenticado.AgregarBearer(cliente, User);
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, id));
            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                await ObtenerCategorias();
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                productoResponse = JsonSerializer.Deserialize<ProductoResponse>(resultado, opciones);

                if (productoResponse != null && categorias != null)
                {
                    var catMatch = categorias.FirstOrDefault(m => m.Text == productoResponse.Categoria);
                    if (catMatch != null)
                    {
                        categoriaSeleccionada = Guid.Parse(catMatch.Value);
                        subcategorias = (await ObtenerSubCategoria(categoriaSeleccionada)).Select(m =>
                        new SelectListItem
                        {
                            Value = m.Id.ToString(),
                            Text = m.Nombre,
                            Selected = m.Nombre == productoResponse.SubCategoria
                        }).ToList();

                        var subMatch = subcategorias.FirstOrDefault(m => m.Text == productoResponse.SubCategoria);
                        if (subMatch != null)
                        {
                            subcategoriaSeleccionada = Guid.Parse(subMatch.Value);
                        }
                    }
                }
            }

            return Page();
        }

        public async Task<ActionResult> OnPost()
        {
            // Remover errores de validación de propiedades que no son relevantes para la actualización
            ModelState.Remove("productoResponse.Categoria");
            ModelState.Remove("productoResponse.SubCategoria");

            if (subcategoriaSeleccionada == Guid.Empty)
            {
                ModelState.AddModelError("subcategoriaSeleccionada", "La subcategoría es obligatoria.");
            }

            if (!ModelState.IsValid)
            {
                await ObtenerCategorias();
                if (categoriaSeleccionada != Guid.Empty)
                {
                    subcategorias = (await ObtenerSubCategoria(categoriaSeleccionada)).Select(m =>
                    new SelectListItem
                    {
                        Value = m.Id.ToString(),
                        Text = m.Nombre,
                        Selected = m.Id.ToString() == subcategoriaSeleccionada.ToString()
                    }).ToList();
                }
                return Page();
            }

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "EditarProducto");
            using var cliente = new HttpClient();
            ClienteApiAutenticado.AgregarBearer(cliente, User);
            var req = new ProductoRequest
            {
                Nombre = productoResponse.Nombre,
                Descripcion = productoResponse.Descripcion,
                Precio = productoResponse.Precio,
                Stock = productoResponse.Stock,
                CodigoBarras = productoResponse.CodigoBarras,
                IdSubCategoria = subcategoriaSeleccionada
            };

            var respuesta = await cliente.PutAsJsonAsync(string.Format(endpoint, productoResponse.Id), req);

            if (!respuesta.IsSuccessStatusCode)
            {
                var error = await respuesta.Content.ReadAsStringAsync();
                throw new Exception($"Excepción de API ({respuesta.StatusCode}): {error}");
            }

            return RedirectToPage("./Index");
        }

        private async Task ObtenerCategorias()
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerCategorias");
            using var cliente = new HttpClient();
            ClienteApiAutenticado.AgregarBearer(cliente, User);
            var solicitud = new HttpRequestMessage(HttpMethod.Get, endpoint);
            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
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
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerSubCategorias");
            using var cliente = new HttpClient();
            ClienteApiAutenticado.AgregarBearer(cliente, User);
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, categoriaID));
            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<List<SubCategoria>>(resultado, opciones);
            }

            return new List<SubCategoria>();
        }

        public async Task<JsonResult> OnGetObtenerSubCategoria(Guid categoriaID)
        {
            var modelos = await ObtenerSubCategoria(categoriaID);
            return new JsonResult(modelos);
        }
    }
}