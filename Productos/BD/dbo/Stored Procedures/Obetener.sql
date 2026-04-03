
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE Obtener

@Id AS UNIQUEIDENTIFIER


AS
BEGIN


SELECT 
      p.Id,
      p.Nombre,
      p.Descripcion,
      p.Precio,
      p.Stock,
      p.CodigoBarras,
      sc.Nombre AS SubCategoria,
      c.Nombre AS Categoria
FROM Producto p
INNER JOIN dbo.SubCategorias sc 
    ON p.IdSubCategoria = sc.Id
INNER JOIN dbo.Categorias c 
    ON sc.IdCategoria = c.Id

  where p.Id = @Id
  select @Id
END