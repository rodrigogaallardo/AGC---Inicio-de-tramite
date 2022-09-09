GO

/****** Object:  UserDefinedFunction [dbo].[CPadron_Solicitud_DireccionesPartidas]    Script Date: 01/02/2017 11:33:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CPadron_Solicitud_DireccionesPartidas]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[CPadron_Solicitud_DireccionesPartidas]
GO

/****** Object:  UserDefinedFunction [dbo].[Cpadron_Solicitud_DireccionesPartidasPlancheta]    Script Date: 01/02/2017 11:33:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Cpadron_Solicitud_DireccionesPartidasPlancheta]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[Cpadron_Solicitud_DireccionesPartidasPlancheta]
GO

/****** Object:  UserDefinedFunction [dbo].[Encomienda_Solicitud_DireccionesPartidas]    Script Date: 01/02/2017 11:33:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Encomienda_Solicitud_DireccionesPartidas]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[Encomienda_Solicitud_DireccionesPartidas]
GO

/****** Object:  UserDefinedFunction [dbo].[Encomienda_Solicitud_DireccionesPartidasPlancheta]    Script Date: 01/02/2017 11:33:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Encomienda_Solicitud_DireccionesPartidasPlancheta]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[Encomienda_Solicitud_DireccionesPartidasPlancheta]
GO

/****** Object:  UserDefinedFunction [dbo].[SGI_DireccionesPartidasPlancheta]    Script Date: 01/02/2017 11:33:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SGI_DireccionesPartidasPlancheta]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[SGI_DireccionesPartidasPlancheta]
GO

/****** Object:  UserDefinedFunction [dbo].[SSIT_Solicitud_DireccionesPartidasPlancheta]    Script Date: 01/02/2017 11:33:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SSIT_Solicitud_DireccionesPartidasPlancheta]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[SSIT_Solicitud_DireccionesPartidasPlancheta]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE FUNCTION [dbo].[CPadron_Solicitud_DireccionesPartidas]
(
	@id_cpadron	int
)
RETURNS nvarchar(1000)
AS
BEGIN

	-- Declare the return variable here
	DECLARE @Result nvarchar(1000)
	DECLARE @Direccion	nvarchar(200)
	DECLARE @id_cpadronubicacion	int
	DECLARE @nombre_calle	nvarchar(100)
	DECLARE @Plantas nvarchar(255)
	DECLARE @PlantasHabilitar nvarchar(1000)
	
	SET @Result = ''
	SET @PlantasHabilitar = ''

	DECLARE cur CURSOR FOR	
	SELECT DISTINCT
		CASE 
			WHEN tubic.id_tipoubicacion = 0 
			THEN
				IsNull(cppuer.nombre_calle,'') +  ' ' +  IsNull(convert(nvarchar, cppuer.nropuerta),'') + 
				IsNull(' Local ' + cpubic.Local,'') +
				IsNull(' Depto ' + cpubic.Depto,'') +
				IsNull(' Torre ' + cpubic.Torre,'') +
				IsNull(' ' + cpubic.deptoLocal_cpadronubicacion,'') 
			ELSE
				tubic.descripcion_tipoubicacion + ' ' +	stubic.descripcion_subtipoubicacion  + IsNull(' Local ' + cpubic.local_subtipoubicacion,'')
		END,
		cpubic.id_cpadronubicacion, cppuer.nombre_calle
	FROM
		CPadron_Ubicaciones	cpubic
		INNER JOIN SubTiposDeUbicacion stubic ON cpubic.id_subtipoubicacion = stubic.id_subtipoubicacion
		INNER JOIN TiposDeUbicacion tubic ON stubic.id_tipoubicacion = tubic.id_tipoubicacion
		LEFT JOIN CPadron_Ubicaciones_Puertas cppuer ON cpubic.id_cpadronubicacion = cppuer.id_cpadronubicacion 
	WHERE
		cpubic.id_cpadron= @id_cpadron
	ORDER BY cpubic.id_cpadronubicacion, cppuer.nombre_calle

	OPEN cur
	FETCH NEXT FROM cur INTO @Direccion, @id_cpadronubicacion, @nombre_calle

	WHILE @@FETCH_STATUS = 0
	BEGIN

		IF @Result = ''		
			SET @Result = @Result + IsNull(@Direccion,'') 
		ELSE
			SET @Result = @Result + ' / ' + IsNull(@Direccion,'') 

		FETCH NEXT FROM cur INTO @Direccion, @id_cpadronubicacion, @nombre_calle
	END 
	CLOSE cur
	DEALLOCATE cur

	
	DECLARE curPlanta CURSOR FOR	
	SELECT 
		CASE WHEN b.Id = 11 THEN a.detalle_cpadrontiposector
		ELSE b.Descripcion
		END Planta
	FROM CPadron_Plantas a 
	INNER JOIN TipoSector b ON a.id_tiposector = b.Id
	WHERE a.id_cpadron = @id_cpadron

	OPEN curPlanta
	FETCH NEXT FROM curPlanta INTO @Plantas

	WHILE @@FETCH_STATUS = 0
	BEGIN

		IF @PlantasHabilitar = '' 
			SET @PlantasHabilitar = @PlantasHabilitar + @Plantas
		ELSE
			SET @PlantasHabilitar = @PlantasHabilitar + ' / ' + @Plantas
			

		FETCH NEXT FROM curPlanta INTO @Plantas
	END 
	CLOSE curPlanta
	DEALLOCATE curPlanta

	IF @PlantasHabilitar != ''
		SET @Result = @Result + ' / ' + @PlantasHabilitar

	-- Return the result of the function
	RETURN @Result
END


GO

/****** Object:  UserDefinedFunction [dbo].[Cpadron_Solicitud_DireccionesPartidasPlancheta]    Script Date: 01/02/2017 11:33:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE FUNCTION [dbo].[Cpadron_Solicitud_DireccionesPartidasPlancheta]
(
	@id_cpadron		int,
	@id_ubicacion	int
)
RETURNS nvarchar(1000)
AS
BEGIN

	-- Declare the return variable here
	DECLARE @Result nvarchar(1000)
	DECLARE @Calle	nvarchar(200)
	DECLARE @Calle_ant	nvarchar(200)
	DECLARE @NroPuerta	nvarchar(20)
	DECLARE @deptoLocal_ubicacion nvarchar(50)
	DECLARE @DescripcionUbicacionEspecial nvarchar(500)
	DECLARE @id_tipoubicacion int
	
	SET @Result = ''

	DECLARE cur CURSOR FOR	
	SELECT 
		IsNull(solpuer.nombre_calle,''), 
		IsNull(convert(nvarchar, solpuer.nropuerta),'') 
	FROM
		CPadron_Ubicaciones solubic
		INNER JOIN CPadron_Ubicaciones_Puertas solpuer ON solubic.id_cpadronubicacion = solpuer.id_cpadronubicacion 
	WHERE
		solubic.id_cpadron = @id_cpadron
		AND solubic.id_ubicacion = @id_ubicacion
	GROUP BY
		IsNull(solpuer.nombre_calle,'') , 
		IsNull(convert(nvarchar,solpuer.nropuerta),'')  
	ORDER BY 
		IsNull(solpuer.nombre_calle,'')
	
	OPEN cur
	FETCH NEXT FROM cur INTO @Calle, @NroPuerta

	WHILE @@FETCH_STATUS = 0
	BEGIN
		
		
		IF @Result = ''	
		BEGIN	
			SET @Result =  @Calle  + ' ' + @NroPuerta
			SET @Calle_ant = @Calle		
		END
		ELSE
		BEGIN
			IF @Calle_ant = @Calle	
				SET @Result = @Result + ' / ' + @NroPuerta
			ELSE
			BEGIN
				SET @Result = @Result + ' - ' + @Calle  + ' ' + @NroPuerta
				SET @Calle_ant = @Calle		
			END
		END
		

		FETCH NEXT FROM cur INTO @Calle, @NroPuerta
	END 
	CLOSE cur
	DEALLOCATE cur
	
	
	SET @Result = IsNull(@Result,'')
	
	
	SELECT 
		@deptoLocal_ubicacion = IsNull(solubic.Local, '') 
								+ IsNull( ' ' + solubic.Depto, '') 
								+ IsNull(' ' + solubic.Torre,'') 
								+ IsNull(' ' + solubic.deptoLocal_cpadronubicacion, ''),
		@deptoLocal_ubicacion = solubic.deptoLocal_cpadronubicacion,
		@id_tipoubicacion = tubic.id_tipoubicacion,
		@DescripcionUbicacionEspecial = tubic.descripcion_tipoubicacion + ' ' +	stubic.descripcion_subtipoubicacion  + IsNull(' Local ' + solubic.local_subtipoubicacion,'')
	FROM 
		CPadron_Ubicaciones solubic
		INNER JOIN SubTiposDeUbicacion stubic ON solubic.id_subtipoubicacion = stubic.id_subtipoubicacion
		INNER JOIN TiposDeUbicacion tubic ON stubic.id_tipoubicacion = tubic.id_tipoubicacion
	WHERE 
		solubic.id_cpadron = @id_cpadron
		AND solubic.id_ubicacion = @id_ubicacion
	
	
	IF @id_tipoubicacion <> 0  -- Parcela Común
		SET @Result = @Result + IsNull(' ' + @DescripcionUbicacionEspecial ,'')
	
	
	IF LEN(IsNull(@deptoLocal_ubicacion,'')) > 0
		SET @Result = @Result + ' ' + @deptoLocal_ubicacion 
	

	-- Return the result of the function
	RETURN @Result

END




GO

/****** Object:  UserDefinedFunction [dbo].[Encomienda_Solicitud_DireccionesPartidas]    Script Date: 01/02/2017 11:33:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE FUNCTION [dbo].[Encomienda_Solicitud_DireccionesPartidas]
(
	@id_encomienda	int
)
RETURNS nvarchar(1000)
AS
BEGIN

	
	-- Declare the return variable here
	DECLARE @Result nvarchar(1000)
	DECLARE @Direccion	nvarchar(200)
	DECLARE @id_encomiendaubicacion	int
	DECLARE @nombre_calle	nvarchar(100)
	DECLARE @Plantas nvarchar(255)
	DECLARE @PlantasHabilitar nvarchar(1000)
	
	SET @Result = ''
	SET @PlantasHabilitar = ''

	DECLARE cur CURSOR FOR	
	SELECT DISTINCT
		CASE 
			WHEN tubic.id_tipoubicacion = 0 
			THEN
				IsNull(encpuer.nombre_calle,'') +  ' ' +  IsNull(convert(nvarchar, encpuer.nropuerta),'') + 
				IsNull(' ' + phor.Piso,'')  +
				IsNull(' ' + phor.Depto,'') +
				IsNull(' Local ' + encubic.Local,'') +
				IsNull(' Depto ' + encubic.Depto,'') +
				IsNull(' Torre ' + encubic.Torre,'') +
				IsNull(' ' + encubic.deptoLocal_encomiendaubicacion,'') 
			ELSE
				tubic.descripcion_tipoubicacion + ' ' +	stubic.descripcion_subtipoubicacion  + IsNull(' Local ' + encubic.local_subtipoubicacion,'')
		END,
		encubic.id_encomiendaubicacion, encpuer.nombre_calle
	FROM
		Encomienda_Ubicaciones	encubic
		INNER JOIN SubTiposDeUbicacion stubic ON encubic.id_subtipoubicacion = stubic.id_subtipoubicacion
		INNER JOIN TiposDeUbicacion tubic ON stubic.id_tipoubicacion = tubic.id_tipoubicacion
		LEFT JOIN Encomienda_Ubicaciones_puertas encpuer ON encubic.id_encomiendaubicacion = encpuer.id_encomiendaubicacion 
		LEFT JOIN Encomienda_Ubicaciones_PropiedadHorizontal encphor ON encubic.id_encomiendaubicacion = encphor.id_encomiendaubicacion 
		LEFT JOIN Ubicaciones_PropiedadHorizontal phor ON encphor.id_propiedadhorizontal = phor.id_propiedadhorizontal
	WHERE
		encubic.id_encomienda= @id_encomienda
	ORDER BY encubic.id_encomiendaubicacion, encpuer.nombre_calle

	OPEN cur
	FETCH NEXT FROM cur INTO @Direccion, @id_encomiendaubicacion, @nombre_calle

	WHILE @@FETCH_STATUS = 0
	BEGIN

		IF @Result = ''		
			SET @Result = @Result + IsNull(@Direccion,'') 
		ELSE
			SET @Result = @Result + ' / ' + IsNull(@Direccion,'') 

		FETCH NEXT FROM cur INTO @Direccion, @id_encomiendaubicacion, @nombre_calle
	END 
	CLOSE cur
	DEALLOCATE cur
	
	DECLARE curPlanta CURSOR FOR	
	SELECT 
		CASE WHEN b.Id = 11 THEN a.detalle_encomiendatiposector
		ELSE b.Descripcion
		END Planta
	FROM Encomienda_Plantas a 
	INNER JOIN TipoSector b ON a.id_tiposector = b.Id
	WHERE a.id_encomienda = @id_encomienda

	OPEN curPlanta
	FETCH NEXT FROM curPlanta INTO @Plantas

	WHILE @@FETCH_STATUS = 0
	BEGIN

		IF @PlantasHabilitar = '' 
			SET @PlantasHabilitar = @PlantasHabilitar + @Plantas
		ELSE
			SET @PlantasHabilitar = @PlantasHabilitar + ' / ' + @Plantas
			

		FETCH NEXT FROM curPlanta INTO @Plantas
	END 
	CLOSE curPlanta
	DEALLOCATE curPlanta

	IF @PlantasHabilitar != ''
		SET @Result = @Result + ' / ' + @PlantasHabilitar
	
	-- Return the result of the function
	RETURN @Result

END



GO

/****** Object:  UserDefinedFunction [dbo].[Encomienda_Solicitud_DireccionesPartidasPlancheta]    Script Date: 01/02/2017 11:33:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE FUNCTION [dbo].[Encomienda_Solicitud_DireccionesPartidasPlancheta]
(
	@id_encomienda	int,
	@id_ubicacion	int
)
RETURNS nvarchar(1000)
AS
BEGIN

	-- Declare the return variable here
	DECLARE @Result nvarchar(1000)
	DECLARE @Calle	nvarchar(200)
	DECLARE @Calle_ant	nvarchar(200)
	DECLARE @NroPuerta	nvarchar(20)
	DECLARE @deptoLocal_encomiendaubicacion nvarchar(50)
	DECLARE @DescripcionUbicacionEspecial nvarchar(500)
	DECLARE @id_tipoubicacion int
	
	SET @Result = ''

	DECLARE cur CURSOR FOR	
	SELECT 
		IsNull(encpuer.nombre_calle,'') , 
		IsNull(convert(nvarchar, encpuer.nropuerta),'') 
	FROM
		Encomienda_Ubicaciones	encubic
		INNER JOIN Encomienda_Ubicaciones_puertas encpuer ON encubic.id_encomiendaubicacion = encpuer.id_encomiendaubicacion 
	WHERE
		encubic.id_encomienda= @id_encomienda
		AND encubic.id_ubicacion = @id_ubicacion
	GROUP BY
		IsNull(encpuer.nombre_calle,'') , 
		IsNull(convert(nvarchar, encpuer.nropuerta),'')  
	ORDER BY 
		IsNull(encpuer.nombre_calle,'')
	
	OPEN cur
	FETCH NEXT FROM cur INTO @Calle, @NroPuerta

	WHILE @@FETCH_STATUS = 0
	BEGIN
		
		
		IF @Result = ''	
		BEGIN	
			SET @Result =  @Calle  + ' ' + @NroPuerta
			SET @Calle_ant = @Calle		
		END
		ELSE
		BEGIN
			IF @Calle_ant = @Calle	
				SET @Result = @Result + ' / ' + @NroPuerta
			ELSE
			BEGIN
				SET @Result = @Result + ' - ' + @Calle  + ' ' + @NroPuerta
				SET @Calle_ant = @Calle		
			END
		END
		

		FETCH NEXT FROM cur INTO @Calle, @NroPuerta
	END 
	CLOSE cur
	DEALLOCATE cur
	
	
	SET @Result = IsNull(@Result,'')
	
	
	SELECT 
		@deptoLocal_encomiendaubicacion = IsNull(encubic.Local, '') 
								+ IsNull( ' ' + encubic.Depto, '') 
								+ IsNull(' ' + encubic.Torre,'') 
								+ IsNull(' ' + encubic.deptoLocal_encomiendaubicacion, ''),
		@id_tipoubicacion = tubic.id_tipoubicacion,
		@DescripcionUbicacionEspecial = tubic.descripcion_tipoubicacion + ' ' +	stubic.descripcion_subtipoubicacion  + IsNull(' Local ' + encubic.local_subtipoubicacion,'')
	FROM 
		Encomienda_Ubicaciones encubic
		INNER JOIN SubTiposDeUbicacion stubic ON encubic.id_subtipoubicacion = stubic.id_subtipoubicacion
		INNER JOIN TiposDeUbicacion tubic ON stubic.id_tipoubicacion = tubic.id_tipoubicacion
	WHERE 
		encubic.id_encomienda= @id_encomienda
		AND encubic.id_ubicacion = @id_ubicacion
	
	
	IF @id_tipoubicacion <> 0  -- Parcela Común
		SET @Result = @Result + IsNull(' ' + @DescripcionUbicacionEspecial ,'')
	
	
	IF LEN(IsNull(@deptoLocal_encomiendaubicacion,'')) > 0
		SET @Result = @Result + ' ' + @deptoLocal_encomiendaubicacion 
	

	-- Return the result of the function
	RETURN @Result

END


GO

/****** Object:  UserDefinedFunction [dbo].[SGI_DireccionesPartidasPlancheta]    Script Date: 01/02/2017 11:33:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[SGI_DireccionesPartidasPlancheta]
(
	@id_solicitud	int
)
RETURNS nvarchar(4000)
AS
BEGIN

	-- Declare the return variable here
	DECLARE @Result nvarchar(4000)
	DECLARE @Calle	nvarchar(200)
	DECLARE @Calle_ant	nvarchar(200)
	DECLARE @NroPuerta	nvarchar(20)
	DECLARE @deptoLocal_ubicacion nvarchar(50)
	DECLARE @DescripcionUbicacionEspecial nvarchar(500)
	DECLARE @id_tipoubicacion int

	DECLARE @id_ubicacion_ant	int,
			@id_ubicacion	int
	
	SET @id_ubicacion_ant = -1
	SET @Result = ''
	SET @Calle_ant = ''

	DECLARE cur CURSOR FOR	
		SELECT 
			IsNull(solpuer.nombre_calle,'') , 
			IsNull(convert(nvarchar, solpuer.nropuerta),''),
			solubic.id_ubicacion
			-- encubic.id_encomiendaubicacion
		FROM SSIT_Solicitudes_Ubicaciones solubic 
		LEFT JOIN SSIT_Solicitudes_Ubicaciones_Puertas solpuer ON solubic.id_solicitudubicacion = solpuer.id_solicitudubicacion 
		WHERE solubic.id_solicitud= @id_solicitud
		GROUP BY 
			IsNull(solpuer.nombre_calle,''),
			IsNull(convert(nvarchar, solpuer.nropuerta),''),
			solubic.id_ubicacion
     	ORDER BY  1,2
	
	OPEN cur
	FETCH NEXT FROM cur INTO @Calle, @NroPuerta, @id_ubicacion

	WHILE @@FETCH_STATUS = 0
	BEGIN
		
		IF @id_ubicacion_ant = -1
			SET @id_ubicacion_ant = @id_ubicacion 
			
		IF @Calle_ant = ''
			SET @Calle_ant = @Calle 

		
				
		-- IF @id_encomiendaubicacion_ant = -1
		--		SET @id_encomiendaubicacion_ant = @id_encomiendaubicacion 		
			
		
		IF @Result = ''	
		BEGIN	
			SET @Result =  @Calle  + ' ' + @NroPuerta
			SET @Calle_ant = @Calle		
		END
		ELSE
		BEGIN
			IF @Calle_ant = @Calle	
				SET @Result = @Result + ' / ' + @NroPuerta
			ELSE
			BEGIN
				SET @Result = @Result + ' - ' + @Calle  + ' ' + @NroPuerta
				SET @Calle_ant = @Calle		
			END
		END
		
		
		-- SET @Result = @Result + ' @id_ubicacion = ' + CONVERT(nvarchar, @id_ubicacion)
		
		IF @id_ubicacion <> @id_ubicacion_ant AND @Calle_ant <> @Calle
		BEGIN
		
			SELECT
				@deptoLocal_ubicacion = deptoLocal_ubicacion,
				@id_tipoubicacion = tubic.id_tipoubicacion,
				@DescripcionUbicacionEspecial = tubic.descripcion_tipoubicacion + ' ' +	stubic.descripcion_subtipoubicacion  + IsNull(' Local ' + solubic.local_subtipoubicacion,'')
			FROM 
				SSIT_Solicitudes_Ubicaciones solubic
				INNER JOIN SubTiposDeUbicacion stubic ON solubic.id_subtipoubicacion = stubic.id_subtipoubicacion
				INNER JOIN TiposDeUbicacion tubic ON stubic.id_tipoubicacion = tubic.id_tipoubicacion
			WHERE 
				solubic.id_solicitud= @id_solicitud
				AND solubic.id_ubicacion = @id_ubicacion
			
			
			IF @id_tipoubicacion <> 0  -- Parcela Común
				SET @Result = @Result + IsNull(' ' + @DescripcionUbicacionEspecial ,'')
			
			
			IF LEN(IsNull(@deptoLocal_ubicacion,'')) > 0
				SET @Result = @Result + ' ' + @deptoLocal_ubicacion
	
			
			SET @id_ubicacion_ant = @id_ubicacion 
			-- SET @id_encomiendaubicacion_ant = @id_encomiendaubicacion 
		END		
		


		FETCH NEXT FROM cur INTO @Calle, @NroPuerta, @id_ubicacion
	END 
	CLOSE cur
	DEALLOCATE cur
	
	SET @Result = IsNull(@Result,'')
	
	IF LEN(@Result) = 0
	BEGIN
	

		SELECT 
			@deptoLocal_ubicacion = IsNull(solubic.Local, '') 
								+ IsNull( ' ' + solubic.Depto, '') 
								+ IsNull(' ' + solubic.Torre,'') 
								+ IsNull(' ' + solubic.deptoLocal_ubicacion, ''),
			@id_tipoubicacion = tubic.id_tipoubicacion,
			@DescripcionUbicacionEspecial = tubic.descripcion_tipoubicacion + ' ' +	stubic.descripcion_subtipoubicacion  + IsNull(' Local ' + solubic.local_subtipoubicacion,'')
		FROM 
			SSIT_Solicitudes_Ubicaciones solubic
			INNER JOIN SubTiposDeUbicacion stubic ON solubic.id_subtipoubicacion = stubic.id_subtipoubicacion
			INNER JOIN TiposDeUbicacion tubic ON stubic.id_tipoubicacion = tubic.id_tipoubicacion
		WHERE 
			solubic.id_solicitud= @id_solicitud
			AND solubic.id_ubicacion = @id_ubicacion
		
		
		IF @id_tipoubicacion <> 0  -- Parcela Común
			SET @Result = @Result + IsNull(' ' + @DescripcionUbicacionEspecial ,'')
		
		
		IF LEN(IsNull(@deptoLocal_ubicacion,'')) > 0
			SET @Result = @Result + ' ' + @deptoLocal_ubicacion

	END


	-- Return the result of the function
	RETURN @Result

END


GO

/****** Object:  UserDefinedFunction [dbo].[SSIT_Solicitud_DireccionesPartidasPlancheta]    Script Date: 01/02/2017 11:33:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE FUNCTION [dbo].[SSIT_Solicitud_DireccionesPartidasPlancheta]
(
	@id_solicitud	int,
	@id_ubicacion	int
)
RETURNS nvarchar(1000)
AS
BEGIN

	-- Declare the return variable here
	DECLARE @Result nvarchar(1000)
	DECLARE @Calle	nvarchar(200)
	DECLARE @Calle_ant	nvarchar(200)
	DECLARE @NroPuerta	nvarchar(20)
	DECLARE @deptoLocal_ubicacion nvarchar(50)
	DECLARE @DescripcionUbicacionEspecial nvarchar(500)
	DECLARE @id_tipoubicacion int
	
	SET @Result = ''

	DECLARE cur CURSOR FOR	
	SELECT 
		IsNull(solpuer.nombre_calle,''), 
		IsNull(convert(nvarchar, solpuer.nropuerta),'') 
	FROM
		SSIT_Solicitudes_Ubicaciones solubic
		INNER JOIN SSIT_Solicitudes_Ubicaciones_Puertas solpuer ON solubic.id_solicitudubicacion = solpuer.id_solicitudubicacion 
	WHERE
		solubic.id_solicitud = @id_solicitud
		AND solubic.id_ubicacion = @id_ubicacion
	GROUP BY
		IsNull(solpuer.nombre_calle,'') , 
		IsNull(convert(nvarchar,solpuer.nropuerta),'')  
	ORDER BY 
		IsNull(solpuer.nombre_calle,'')
	
	OPEN cur
	FETCH NEXT FROM cur INTO @Calle, @NroPuerta

	WHILE @@FETCH_STATUS = 0
	BEGIN
		
		
		IF @Result = ''	
		BEGIN	
			SET @Result =  @Calle  + ' ' + @NroPuerta
			SET @Calle_ant = @Calle		
		END
		ELSE
		BEGIN
			IF @Calle_ant = @Calle	
				SET @Result = @Result + ' / ' + @NroPuerta
			ELSE
			BEGIN
				SET @Result = @Result + ' - ' + @Calle  + ' ' + @NroPuerta
				SET @Calle_ant = @Calle		
			END
		END
		

		FETCH NEXT FROM cur INTO @Calle, @NroPuerta
	END 
	CLOSE cur
	DEALLOCATE cur
	
	
	SET @Result = IsNull(@Result,'')
	
	
	SELECT 
		@deptoLocal_ubicacion = IsNull(solubic.Local, '') 
							+ IsNull( ' ' + solubic.Depto, '') 
							+ IsNull(' ' + solubic.Torre,'') 
							+ IsNull(' ' + solubic.deptoLocal_ubicacion, ''),
		@id_tipoubicacion = tubic.id_tipoubicacion,
		@DescripcionUbicacionEspecial = tubic.descripcion_tipoubicacion + ' ' +	stubic.descripcion_subtipoubicacion  + IsNull(' Local ' + solubic.local_subtipoubicacion,'')
	FROM 
		SSIT_Solicitudes_Ubicaciones solubic
		INNER JOIN SubTiposDeUbicacion stubic ON solubic.id_subtipoubicacion = stubic.id_subtipoubicacion
		INNER JOIN TiposDeUbicacion tubic ON stubic.id_tipoubicacion = tubic.id_tipoubicacion
	WHERE 
		solubic.id_solicitud = @id_solicitud
		AND solubic.id_ubicacion = @id_ubicacion
	
	
	IF @id_tipoubicacion <> 0  -- Parcela Común
		SET @Result = @Result + IsNull(' ' + @DescripcionUbicacionEspecial ,'')
	
	
	IF LEN(IsNull(@deptoLocal_ubicacion,'')) > 0
		SET @Result = @Result + ' ' + @deptoLocal_ubicacion 
	

	-- Return the result of the function
	RETURN @Result

END



GO


