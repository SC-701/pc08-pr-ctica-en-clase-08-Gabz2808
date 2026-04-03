

namespace Abstracciones.Interfaces.Reglas
{
    public interface IConfiguracion
    {
        string ObtenerMetodo(string seccion, string nombre);
        public string ObtenerValor(string llave);
    }
}