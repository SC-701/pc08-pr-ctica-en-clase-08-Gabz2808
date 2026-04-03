-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE Actualizar
@Id AS UNIQUEIDENTIFIER
      ,@IdSubCategoria AS UNIQUEIDENTIFIER
      ,@Nombre AS VARCHAR(MAX)
      ,@Descripcion AS VARCHAR(MAX)
      ,@Precio AS DECIMAL
      ,@Stock AS INT
      ,@CodigoBarras AS VARCHAR(MAX)
AS
BEGIN

SET NOCOUNT ON;

BEGIN TRANSACTION;
UPDATE [dbo].[Producto]
   SET 
      [IdSubCategoria] = @IdSubCategoria
      ,[Nombre] = @Nombre
      ,[Descripcion] = @Descripcion
      ,[Precio] = @Precio
      ,[Stock] = @Stock
      ,[CodigoBarras] = @CodigoBarras 
 WHERE [Id]  = @Id

 SELECT @Id

 COMMIT TRANSACTION;

END
