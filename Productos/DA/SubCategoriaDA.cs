using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Reflection;

namespace DA
{
    public class SubCategoriaDA : ISubCategoriaDA
    {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;

        public SubCategoriaDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }


        public async Task<IEnumerable<SubCategoria>> Obtener(Guid IdCategoria)
        {
            string query = @"ObtenerSubCategoria";
            var resultadoConsulta = await _sqlConnection.QueryAsync<SubCategoria>(query,
                new { IdCategoria = IdCategoria });
            return resultadoConsulta;
        }

    }

}