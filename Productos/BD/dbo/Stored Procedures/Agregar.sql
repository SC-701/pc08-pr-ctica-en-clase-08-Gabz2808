
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE Agregar

@Id AS UNIQUEIDENTIFIER
      ,@IdSubCategoria AS UNIQUEIDENTIFIER
      ,@Nombre AS VARCHAR(MAX)
      ,@Descripcion AS VARCHAR(MAX)
      ,@Precio AS DECIMAL
      ,@Stock AS INT
      ,@CodigoBarras AS VARCHAR(MAX)
      as
  
BEGIN

set nocount on;
begin transaction;
insert into [dbo].[Producto]
       ([Id]
      ,[IdSubCategoria]
      ,[Nombre]
      ,[Descripcion]
      ,[Precio]
      ,[Stock]
      ,[CodigoBarras])
    VALUES
    (@Id,@IdSubCategoria,@Nombre,@Descripcion,@Precio,@Stock,@CodigoBarras)
    select @id
    commit transaction;

END
