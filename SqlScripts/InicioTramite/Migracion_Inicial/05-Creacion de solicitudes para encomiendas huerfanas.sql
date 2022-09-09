DECLARE 
	@userid_mesa uniqueidentifier,
	@id_estado int,
	@id_encomienda int

DECLARE @tbl_result TABLE(id_solicitud int,
						 id_encomienda int)


SELECT @userid_mesa = userid FROM aspnet_users WHERE username = 'AGC-mesa' AND applicationid = 'A2EAEF96-F109-4B62-BC31-53E219C76362'
SET @id_estado = 0	-- Incompleto


DECLARE cur CURSOR FAST_FORWARD FOR
SELECT id_encomienda FROM Encomienda WHERE id_solicitud IS NULL AND id_tipotramite = 1

OPEN cur
FETCH NEXT FROM cur INTO @id_encomienda

WHILE @@FETCH_STATUS = 0
BEGIN

	--------------------------------------------
	-- Crear solicitud	
	--------------------------------------------
	DECLARE 
		@id_solicitud int,
		@id_tipotramite int,
		@id_tipoexpediente int,
		@id_subtipoexpediente int,
		@MatriculaEscribano int

	EXEC @id_solicitud = id_nuevo 'SSIT_Solicitudes'
	
	SELECT 
		 @id_tipotramite = enc.id_tipotramite,
		 @id_tipoexpediente = enc.id_tipoexpediente,
		 @id_subtipoexpediente = enc.id_subtipoexpediente,
		 @MatriculaEscribano = esc.nro_matricula_escribano_acta
	FROM 
		Encomienda enc
		LEFT JOIN wsEscribanos_Actanotarial esc ON enc.id_encomienda = esc.id_encomienda
	WHERE 
		enc.id_encomienda = @id_encomienda

	INSERT INTO SSIT_Solicitudes
	(
		id_solicitud,
		id_tipotramite,
		id_tipoexpediente,
		id_subtipoexpediente,
		MatriculaEscribano,
		NroExpediente,
		id_estado,
		CreateDate,
		CreateUser,
		LastUpdateDate,
		LastUpdateUser,
		NroExpedienteSade,
		telefono,
		FechaLibrado,
		CodigoSeguridad
	)
	VALUES
	(
		@id_solicitud,
		@id_tipotramite,
		@id_tipoexpediente,
		@id_subtipoexpediente,
		@MatriculaEscribano,
		NULL,
		@id_estado,
		GETDATE(),
		@userid_mesa,
		GETDATE(),
		@userid_mesa,
		NULL,
		NULL,
		NULL,
		NULL
	)
	
	UPDATE Encomienda SET id_solicitud = @id_solicitud WHERE id_encomienda = @id_encomienda

	----------------------------------------------------------------------------
	-- Buscar las rectificatorias asociadas a la encomienda
	----------------------------------------------------------------------------
	INSERT INTO @tbl_result
	VALUES(@id_solicitud,@id_encomienda)
	
	DECLARE 
		@iterar bit,
		@id_encomienda_anterior int,
		@id_encomienda_buscar int,
		@id_encomienda_nueva  int
	
	SET @iterar  = 1
	SET @id_encomienda_buscar = @id_encomienda
	
	-- Buscar para el pasado
	WHILE @iterar = 1
	BEGIN
	
		SELECT @id_encomienda_anterior = id_encomienda_anterior 
		FROM Encomienda_Rectificatoria  
		WHERE id_encomienda_nueva = @id_encomienda_buscar
		
		IF @@ROWCOUNT > 0 
		BEGIN
		
			INSERT INTO @tbl_result
			VALUES(@id_solicitud,@id_encomienda_anterior)
			
			SET @id_encomienda_buscar = @id_encomienda_anterior
		END
		ELSE
			SET @iterar = 0
	
	END
	
	SET @iterar = 1
	SET @id_encomienda_buscar = @id_encomienda
	
	-- Buscar para el futuro
	WHILE @iterar = 1
	BEGIN
	
		SELECT @id_encomienda_nueva = id_encomienda_nueva
		FROM Encomienda_Rectificatoria  
		WHERE id_encomienda_anterior = @id_encomienda_buscar
		
		IF @@ROWCOUNT > 0 
		BEGIN
		
			INSERT INTO @tbl_result
			VALUES(@id_solicitud,@id_encomienda_nueva)
			
			SET @id_encomienda_buscar = @id_encomienda_nueva
		END
		ELSE
			SET @iterar = 0
	
	END



	FETCH NEXT FROM cur INTO @id_encomienda

END

CLOSE cur
DEALLOCATE cur

--------------------------------------------------------
-- Asociar todas las rectificatorias a las solicitudes
--------------------------------------------------------
DECLARE cur CURSOR FAST_FORWARD FOR
SELECT id_solicitud, id_encomienda FROM @tbl_result

OPEN cur
FETCH NEXT FROM cur INTO @id_solicitud,@id_encomienda
WHILE @@FETCH_STATUS = 0
BEGIN
	--PRINT( 'Solicitud: ' + convert(nvarchar, @id_solicitud) + ' Encomienda: ' + convert(nvarchar, @id_encomienda))
	UPDATE Encomienda SET id_solicitud = @id_solicitud WHERE id_encomienda = @id_encomienda

	FETCH NEXT FROM cur INTO @id_solicitud,@id_encomienda

END
CLOSE cur
DEALLOCATE cur
GO
