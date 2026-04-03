using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Microsoft.Data.SqlClient;
using Dapper;

namespace DA
{
    public class ProductoDA : IProductoDA
    {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;

        #region Constructor
        public ProductoDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }
        #endregion

        #region Operaciones

        public async Task<Guid> Agregar(ProductoRequest producto)
        {
            string query = @"Agregar";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id = Guid.NewGuid(),

                IdSubCategoria = producto.IdSubCategoria,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Stock = producto.Stock,
                CodigoBarras = producto.CodigoBarras,
            });

            return resultadoConsulta;
        }

        public async Task<Guid> Editar(Guid Id, ProductoRequest producto)
        {
            string query = @"Actualizar";
            await verificarProductoExiste(Id);
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id = Id,

                IdSubCategoria = producto.IdSubCategoria,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Stock = producto.Stock,
                CodigoBarras = producto.CodigoBarras,
            });

            return resultadoConsulta;
        }



        public async Task<Guid> Eliminar(Guid Id)
        {
            await verificarProductoExiste(Id);

            string query = @"Eliminar";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id = Id
            });

            return resultadoConsulta;
        }

        public async Task<IEnumerable<ProductoResponse>> Obtener()
        {
            string query = @"ObtenerTodos";
            var resultadoConsulta = await _sqlConnection.QueryAsync<ProductoResponse>(query);
            return resultadoConsulta;
        }

        public async Task<ProductoResponse> Obtener(Guid Id)
        {

            string query = @"Obtener";

            var resultadoConsulta = await _sqlConnection.QueryAsync<ProductoResponse>(query, new { id = Id });

            return resultadoConsulta.FirstOrDefault();
        }
        #endregion

        #region Helpers
        private async Task verificarProductoExiste(Guid Id)
        {
            ProductoResponse? reseultadoConsultaExistente = await Obtener(Id);
            if (reseultadoConsultaExistente == null)
            {
                throw new Exception("El producto no existe");
            }
        }
        #endregion


    }

}