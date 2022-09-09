
DELETE FROM Encomienda_Rectificatoria

INSERT INTO Encomienda_Rectificatoria (id_encrec,id_encomienda_anterior,id_encomienda_nueva)
SELECT id_relencrec,id_encomienda_anterior,id_encomienda_nueva
FROM rel_encomienda_rectificatoria


DECLARE 
	@id_solicitud int,
	@id_encomienda int
	

DECLARE @tbl_result TABLE(id_solicitud int,
						 id_encomienda int)

DECLARE cur CURSOR FAST_FORWARD FOR
SELECT id_solicitud,id_encomienda
FROM SSIT_solicitudes_bk 
WHERE id_estado <> 20  --and id_solicitud = 235562
ORDER BY id_solicitud

OPEN cur
FETCH NEXT FROM cur INTO @id_solicitud,@id_encomienda

WHILE @@FETCH_STATUS = 0
BEGIN

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
	

	DECLARE cur1 CURSOR FAST_FORWARD FOR
	SELECT id_encomienda_nueva
	FROM Encomienda_Rectificatoria  
	WHERE id_encomienda_anterior = @id_encomienda_buscar
		
	OPEN cur1 
		
	FETCH NEXT FROM cur1 INTO @id_encomienda_nueva 
	WHILE @@FETCH_STATUS = 0
	BEGIN
		INSERT INTO @tbl_result
		VALUES(@id_solicitud,@id_encomienda_nueva)
		PRINT(convert(nvarchar,@id_encomienda_nueva))

		SET @iterar = 1
		SET @id_encomienda_buscar = @id_encomienda_nueva
	
		-- Buscar para el futuro
		WHILE @iterar = 1
		BEGIN
	
			SELECT @id_encomienda_nueva = id_encomienda_nueva
			FROM Encomienda_Rectificatoria  
			WHERE id_encomienda_anterior = @id_encomienda_buscar
		
			
			IF @@ROWCOUNT > 0 
			BEGIN
				PRINT(convert(nvarchar,@id_encomienda_nueva))
				INSERT INTO @tbl_result
				VALUES(@id_solicitud,@id_encomienda_nueva)
			
				SET @id_encomienda_buscar = @id_encomienda_nueva
			END
			ELSE
				SET @iterar = 0
	
		END


		FETCH NEXT FROM cur1 INTO @id_encomienda_nueva 

	END
	CLOSE cur1
	DEALLOCATE cur1
			
	

	
	FETCH NEXT FROM cur INTO @id_solicitud,@id_encomienda
	
END
CLOSE cur
DEALLOCATE cur

DECLARE 
	@id_solicitud2 int
	,@id_encomienda2 int

DECLARE cur CURSOR FAST_FORWARD FOR
SELECT id_solicitud, id_encomienda FROM @tbl_result

OPEN cur
FETCH NEXT FROM cur INTO @id_solicitud2,@id_encomienda2
WHILE @@FETCH_STATUS = 0
BEGIN
	--PRINT( 'Solicitud: ' + convert(nvarchar, @id_solicitud) + ' Encomienda: ' + convert(nvarchar, @id_encomienda))
	UPDATE Encomienda SET id_solicitud = @id_solicitud2 WHERE id_encomienda = @id_encomienda2

	FETCH NEXT FROM cur INTO @id_solicitud2,@id_encomienda2

END
CLOSE cur
DEALLOCATE cur