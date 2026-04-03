using System.Net.Http.Headers;
using System.Security.Claims;

namespace Reglas
{
    public static class ClienteApiAutenticado
    {
        public const string ClaimToken = "Token";

        public static void AgregarBearer(HttpClient cliente, ClaimsPrincipal? usuario)
        {
            var token = usuario?.FindFirst(ClaimToken)?.Value;
            if (string.IsNullOrEmpty(token))
                return;
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
