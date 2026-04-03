using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Seguridad;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Reglas;

namespace Web.Pages.Seguridad
{
    public class RegistroModel : PageModel
    {
        [BindProperty]
        public Usuario usuario { get; set; } = default!;

        private readonly IConfiguracion _configuracion;

        public RegistroModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var hash = Autenticacion.GenerarHash(usuario.Password);
            usuario.PasswordHash = Autenticacion.ObtenerHash(hash);

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPointsSeguridad", "Registro");
            using var cliente = new HttpClient();
            var respuesta = await cliente.PostAsJsonAsync(endpoint, (UsuarioBase)usuario);
            respuesta.EnsureSuccessStatusCode();
            return RedirectToPage("/Seguridad/Login");
        }
    }
}
