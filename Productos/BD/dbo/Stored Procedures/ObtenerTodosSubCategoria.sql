CREATE PROCEDURE ObtenerTodosSubCategoria

AS
BEGIN


SELECT [Id]
       ,[IdCategoria]
      ,[Nombre]
  FROM [dbo].SubCategorias


END