using Abstracciones.Interfaces.TipoCambio;
using Abstracciones.Modelos;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace Servicios
{
    public class TipoCambioServicio : ITipoCambioServicio
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public TipoCambioServicio(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<decimal> ObtenerTipoCambioVentaAsync()
        {
            var urlBase = _configuration["BancoCentralCR:UrlBase"];
            var token = _configuration["BancoCentralCR:BearerToken"];

            var fecha = DateTime.Now.ToString("yyyy/MM/dd");

            var url = $"{urlBase}?fechaInicio={fecha}&fechaFin={fecha}&idioma=ES";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var resultado = JsonSerializer.Deserialize<BccrResponse>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            var tipoCambio = resultado
                .Datos.First()
                .Indicadores.First()
                .Series.First()
                .ValorDatoPorPeriodo;

            return tipoCambio;
        }
    }

}
