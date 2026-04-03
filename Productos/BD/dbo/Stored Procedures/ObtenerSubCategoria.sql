CREATE PROCEDURE ObtenerSubCategoria




	@IdCategoria uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Id, IdCategoria, Nombre
	FROM SubCategorias
	WHERE (IdCategoria = @IdCategoria)


END