
USE DatosObviam;
GO
IF OBJECT_ID ( N'dbo.GuardarPersonasDetalle', N'P' ) IS NOT NULL 
    DROP PROCEDURE dbo.GuardarPersonasDetalle;
GO
CREATE PROCEDURE dbo.GuardarPersonasDetalle 
	@IdOrigen    int, 
	@DetalleJson nvarchar(max)
	WITH ENCRYPTION
	
AS
	DECLARE @Existe      tinyint = 0;
	DECLARE @Id          int;
	DECLARE @Tipo        smallint;
	DECLARE @Posicion    smallint;
	declare @Codigo      nvarchar(10);
	DECLARE @Descripcion nvarchar(250);
	DECLARE @IdPersona   int;
	DECLARE @IdCatalogo  int;
	DECLARE @IdDato      int;
	DECLARE @Valor       float;
	DECLARE @Notas       nvarchar(250);
	DECLARE @Bandera     tinyint;
	
	DELETE FROM CatPersonasDetalle WHERE IdOrigen=@IdOrigen;

	DECLARE TABLA CURSOR FOR SELECT * FROM OPENJSON(@DetalleJson)
		WITH ( 
		Tipo        smallint      '$.Tipo',
		Posicion    smallint      '$.Posicion',
		Codigo      nvarchar(10)  '$.Codigo',
		Descripcion nvarchar(250) '$.Descripcion',
		IdPersona   int           '$.IdPersona',
		IdCatalogo  int           '$.IdCatalogo',
		IdDato      int           '$.IdDato',
		Valor       float         '$.Valor',
		Notas       nvarchar(250) '$.Notas',
		Bandera     tinyint       '$.Bandera'
		)
	OPEN TABLA;
	FETCH NEXT FROM TABLA INTO @Tipo,@Posicion,@Codigo,@Descripcion,@IdPersona,@IdCatalogo,@IdDato,@Valor,@Notas,@Bandera;
	WHILE @@FETCH_STATUS = 0
		BEGIN
			
			SELECT @Id=Id FROM [CatPersonasDetalle] WHERE IdOrigen=@IdOrigen and Tipo=@Tipo and Posicion=@Posicion;
			IF (@Id>0)
				UPDATE CatPersonasDetalle SET Codigo=@Codigo, 
					   Descripcion=@Descripcion,
					   IdPersona=@IdPersona,
					   IdCatalogo=@IdCatalogo,
					   IdDato=@IdDato,
					   Valor=@Valor,
					   Notas=@Notas,
					   Bandera=@Bandera 
					   WHERE Id=@Id;
			ELSE
				INSERT INTO CatPersonasDetalle (IdOrigen,Tipo,Codigo,Descripcion,IdPersona,IdCatalogo,IdDato,Valor,Notas,Bandera) 
				                        VALUES (@IdOrigen,@Tipo,@Codigo,@Descripcion,@IdPersona,@IdCatalogo,@IdDato,@Valor,@Notas,@Bandera);

			FETCH NEXT FROM TABLA INTO @Tipo,@Posicion,@Codigo,@Descripcion,@IdPersona,@IdCatalogo,@IdDato,@Valor,@Notas,@Bandera;
		END;
	CLOSE TABLA;
	DEALLOCATE TABLA;
	SET NOCOUNT OFF;
GO

