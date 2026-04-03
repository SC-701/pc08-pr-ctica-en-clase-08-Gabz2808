using Abstracciones.Interfaces.DA;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace DA.Repositorios
{
    public class RepositorioDapper : IRepositorioDapper
    {
        private readonly  IConfiguration _configuracion;
        private readonly SqlConnection _conexionBaseDatos;

        public RepositorioDapper(IConfiguration configuracion)
        {
            _configuracion = configuracion;
            _conexionBaseDatos = new SqlConnection(_configuracion.GetConnectionString("DB"));
        }
        public SqlConnection ObtenerRepositorio()
        {
            return _conexionBaseDatos;
        }
    }
}