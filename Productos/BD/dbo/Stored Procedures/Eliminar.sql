-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE Eliminar

@Id AS UNIQUEIDENTIFIER

      as
  
BEGIN

BEGIN TRANSACTION;

DELETE FROM [dbo].[Producto]
      WHERE [Id] = @Id

SELECT @Id

COMMIT TRANSACTION;

END

