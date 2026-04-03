using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{
    public class ProductoBase
    {
        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres.")]
        public string Nombre { get; set; }

        [StringLength(500, ErrorMessage = "La descripción no puede exceder los 500 caracteres.")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio.")]
        [Range(0.01, 1000000, ErrorMessage = "El precio debe ser mayor a 0 y menor a 1 millón.")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "El stock es obligatorio.")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo.")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "El código de barras es obligatorio.")]
        [StringLength(50, ErrorMessage = "El código de barras no puede exceder 50 caracteres.")]
        public string CodigoBarras { get; set; }
    }

    public class ProductoRequest : ProductoBase
    {
        [Required(ErrorMessage = "La subcategoría es obligatoria.")]
        public Guid IdSubCategoria { get; set; }
    }

    public class ProductoResponse : ProductoBase
    {
        public Guid Id { get; set; }
        public string SubCategoria { get; set; }
        public string Categoria { get; set; }
        public decimal PrecioUSD { get; set; }
    }
}