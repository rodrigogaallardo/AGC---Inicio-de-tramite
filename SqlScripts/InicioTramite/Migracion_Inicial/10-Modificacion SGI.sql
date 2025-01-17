
UPDATE ENG_Rel_Resultados_Tareas_Transiciones SET [id_transicion]=401 WHERE [id_resultadotareatransicion]=401
UPDATE [ENG_Transiciones] SET [condiciones_transicion]='select @es_valido = dbo.SGI_ENG_Validar_Proxima_Tarea(@id_transicion, @id_tramitetarea, @id_resultado)'
	WHERE [id_transicion] IN(300,301,400,401)
GO  
IF NOT EXISTS(SELECT 1 FROM [ENG_Transiciones] WHERE id_transicion=330)
BEGIN
	INSERT INTO [dbo].[ENG_Transiciones] values(330, 311, 303, NULL, NULL, NULL)
	INSERT INTO [dbo].[ENG_Rel_Resultados_Tareas] values(330, 311, 55)
	INSERT INTO [dbo].[ENG_Rel_Resultados_Tareas_Transiciones] VALUES(330, 330, 330)

	INSERT INTO [dbo].[ENG_Transiciones] values(430, 411, 403, NULL, NULL, NULL)
	INSERT INTO [dbo].[ENG_Rel_Resultados_Tareas] values(430, 411, 55)
	INSERT INTO [dbo].[ENG_Rel_Resultados_Tareas_Transiciones] VALUES(430, 430, 430)
	
	--circuito viejo 
	--SSP
	UPDATE [ENG_Transiciones] set [id_tarea_destino]=9 WHERE [id_transicion]=31
	update [ENG_Rel_Resultados_Tareas] set [id_resultado]=55 where [id_resultadotarea]=10
	INSERT INTO [dbo].[ENG_Rel_Resultados_Tareas_Transiciones] VALUES(6, 10, 31)
	INSERT INTO [dbo].[ENG_Transiciones] values(10, 25, 10, NULL, NULL, NULL)
	INSERT INTO [dbo].[ENG_Rel_Resultados_Tareas] values(9, 25, 55)
	INSERT INTO [dbo].[ENG_Rel_Resultados_Tareas_Transiciones] VALUES(9, 9, 10)
	--SCP
	update [ENG_Transiciones] set [id_tarea_destino]=34 where [id_transicion]=60
	update [ENG_Rel_Resultados_Tareas] set [id_resultado]=55 where [id_resultadotarea]=60
	INSERT INTO [dbo].[ENG_Transiciones] values(49, 49, 35, NULL, NULL, NULL)
	INSERT INTO [dbo].[ENG_Rel_Resultados_Tareas] values(49, 49, 55)
	INSERT INTO [dbo].[ENG_Rel_Resultados_Tareas_Transiciones] VALUES(49, 49, 49)
	--especial
  	INSERT INTO [dbo].[ENG_Transiciones] values(185, 120, 102, 'select @es_valido = dbo.SGI_ENG_Validar_Proxima_Tarea(@id_transicion, @id_tramitetarea, @id_resultado)', NULL, NULL)
	INSERT INTO [dbo].[ENG_Rel_Resultados_Tareas] values(185, 120, 55)
	INSERT INTO [dbo].[ENG_Rel_Resultados_Tareas_Transiciones] VALUES(185, 185, 185)
  	INSERT INTO [dbo].[ENG_Transiciones] values(186, 120, 121, 'select @es_valido = dbo.SGI_ENG_Validar_Proxima_Tarea(@id_transicion, @id_tramitetarea, @id_resultado)', NULL, NULL)
	INSERT INTO [dbo].[ENG_Rel_Resultados_Tareas_Transiciones] VALUES(186, 185, 186)	
	--espercimiento
  	INSERT INTO [dbo].[ENG_Transiciones] values(239, 222, 202, 'select @es_valido = dbo.SGI_ENG_Validar_Proxima_Tarea(@id_transicion, @id_tramitetarea, @id_resultado)', NULL, NULL)
	INSERT INTO [dbo].[ENG_Rel_Resultados_Tareas] values(239, 222, 55)
	INSERT INTO [dbo].[ENG_Rel_Resultados_Tareas_Transiciones] VALUES(239, 239, 239)
  	INSERT INTO [dbo].[ENG_Transiciones] values(240, 222, 204, 'select @es_valido = dbo.SGI_ENG_Validar_Proxima_Tarea(@id_transicion, @id_tramitetarea, @id_resultado)', NULL, NULL)
	INSERT INTO [dbo].[ENG_Rel_Resultados_Tareas_Transiciones] VALUES(240, 239, 240)		
		
END  

GO
/****** Object:  StoredProcedure [dbo].[ENG_TramitesRelacionados]    Script Date: 10/26/2016 16:06:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[ENG_TramitesRelacionados](
	@id_solicitud				int,
	@start_row_index            int,
	@maximum_rows				int,
	@sort_expression			nvarchar(100),
	@cant_reg					int out	
)  
AS  
BEGIN   
	DECLARE @id_ubicacion int
    DECLARE @id_tipoubicacion int
    DECLARE @id_partidaHoriz int
    
	DECLARE @sql nvarchar(4000)
	DECLARE @sql_where nvarchar(4000)
	DECLARE @filtro nvarchar(4000)
	
    DECLARE @paramDefinition nvarchar(1000)	
    DECLARE @paramValue nvarchar(1000)

	DECLARE @id_tipoexpediente			int
	DECLARE @id_subtipoexpediente		int

	DECLARE @tabla_datos TABLE (id_solicitud int, id_tramitetarea int)
	DECLARE @Direcciones TABLE (id_solicitud int, Direccion nvarchar(1000))
	
	DECLARE @tabla_ordenada TABLE(
		id_tramitetarea						int,
		id_solicitud						int,
		id_tarea							int,
		FechaInicio_tramitetarea			datetime,
		FechaAsignacion_tramtietarea		datetime,
		id_tipotramite						int,
		id_tipoexpediente					int,
		id_subtipoexpediente				int,
		direccion							nvarchar(1000),
		nombre_tarea						nvarchar(100),
		asignable_tarea						bit,
		formulario_tarea					nvarchar(100),
		descripcion_tipotramite				nvarchar(100),
		descripcion_tipoexpediente			nvarchar(100),
		descripcion_subtipoexpediente		nvarchar(100),
		descripcion_estado					nvarchar(100),
		descripcion_tramite					nvarchar(100),
		Dias_Transcurridos					int,
		UsuarioAsignado_tramitetarea		uniqueidentifier,
		nombre_UsuarioAsignado_tramitetarea	nvarchar(100),
		nro_orden							int
	)

	SELECT @id_ubicacion= enc_ubi.id_ubicacion, 
		   @id_tipoubicacion= stubic.id_tipoubicacion,
		   @id_partidaHoriz= encPHor.id_propiedadhorizontal
	FROM dbo.SSIT_Solicitudes sol
	join dbo.SSIT_Solicitudes_Ubicaciones enc_ubi on sol.id_solicitud = enc_ubi.id_solicitud
	join SubTiposDeUbicacion stubic ON enc_ubi.id_subtipoubicacion = stubic.id_subtipoubicacion
	left join dbo.SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal encPHor ON encPHor.id_solicitudubicacion=enc_ubi.id_solicitudubicacion
	where sol.id_solicitud = @id_solicitud

    SET @paramDefinition = ''
    SET @paramValue  = ''
	SET @sql_where = '';

	SET @sql_where = @sql_where  + ' and tt.id_tramitetarea > 0 '

	----------------------
	--filtro por solicitud
	----------------------	
	SET @paramDefinition = @paramDefinition + '@ssit_id_solicitud int'
	SET @paramValue = @paramValue + ' '	
	SET @filtro = ' and sol.id_solicitud <> ' + CONVERT(nvarchar, @id_solicitud)
	set @filtro= @filtro + ' and tt.id_tarea not in(32,6)'
	SET @sql_where = @sql_where + @filtro
	
	----------------
	--filtro por ubicacion		
	----------------	
	IF @id_tipoubicacion =  0
		SET @filtro = ' and enc_ubi.id_ubicacion = ' + CONVERT(nvarchar, @id_ubicacion)
	ELSE IF @id_partidaHoriz is null			
		SET @filtro = ' and st_ubi.id_tipoubicacion = ' + ltrim( rtrim(@id_tipoubicacion) ) 
	ELSE			
		SET @filtro = ' and enc_ubi_phor.id_propiedadhorizontal = ' + ltrim( rtrim(@id_partidaHoriz) ) 
	SET @sql_where = @sql_where + @filtro

	SET @sql = N'
		SELECT tt_HAB.id_solicitud, MAX(tt.id_tramitetarea) as id_tramitetarea
        FROM SGI_Tramites_Tareas tt
        INNER JOIN SGI_Tramites_Tareas_HAB tt_HAB ON tt.id_tramitetarea = tt_HAB.id_tramitetarea
        INNER JOIN SSIT_Solicitudes sol ON tt_HAB.id_solicitud = sol.id_solicitud
        INNER JOIN SSIT_Solicitudes_Ubicaciones enc_ubi ON sol.id_solicitud = enc_ubi.id_solicitud
        LEFT JOIN SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal enc_ubi_phor ON enc_ubi.id_solicitudubicacion = enc_ubi_phor.id_solicitudubicacion
        INNER JOIN Ubicaciones ubi ON enc_ubi.id_ubicacion = ubi.id_ubicacion
        INNER JOIN SubTiposDeUbicacion st_ubi ON enc_ubi.id_subtipoubicacion = st_ubi.id_subtipoubicacion
        '
    
	IF LEN(@sql_where) > 0 
	BEGIN
		SET @sql_where = SUBSTRING(@sql_where, 5, LEN(@sql_where) )
		SET @sql = @sql + ' where ' + @sql_where + ' GROUP BY tt_HAB.id_solicitud'
		insert into @tabla_datos exec sp_executesql @sql
		
		---------------------------------------------------------------
		-- Se obtienen la direcciones de los trámites pre-seleccionados
		---------------------------------------------------------------
			
		DECLARE @idSolicitud int
				,@direccion nvarchar(1000)
				,@calle		nvarchar(200)
				,@id_solicitud_ant int
				,@Direccion_armada nvarchar(1000)
				
		
		DECLARE cur CURSOR FAST_FORWARD FOR
			SELECT DISTINCT
				datos.id_solicitud,
				CASE 
					WHEN tubic.id_tipoubicacion = 0 
					THEN
						IsNull(encpuer.nombre_calle,'''') 
					ELSE
						tubic.descripcion_tipoubicacion + ' ' +	stubic.descripcion_subtipoubicacion
				END as Calle,
				CASE 
					WHEN tubic.id_tipoubicacion = 0 
					THEN
						IsNull(convert(nvarchar, encpuer.nropuerta), '') 
					ELSE
						IsNull('Local ' + encubic.local_subtipoubicacion, '')
				END
			FROM
				@tabla_datos datos
				INNER JOIN SSIT_Solicitudes_Ubicaciones encubic ON datos.id_solicitud = encubic.id_solicitud
				INNER JOIN SubTiposDeUbicacion stubic ON encubic.id_subtipoubicacion = stubic.id_subtipoubicacion
				INNER JOIN TiposDeUbicacion tubic ON stubic.id_tipoubicacion = tubic.id_tipoubicacion
				LEFT JOIN SSIT_Solicitudes_Ubicaciones_puertas encpuer ON encubic.id_solicitudubicacion = encpuer.id_solicitudubicacion 
				LEFT JOIN SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal encphor ON encubic.id_solicitudubicacion = encphor.id_solicitudubicacion 
				LEFT JOIN Ubicaciones_PropiedadHorizontal phor ON encphor.id_propiedadhorizontal = phor.id_propiedadhorizontal
			ORDER BY 1,2,3
				
		OPEN cur
		FETCH NEXT FROM cur INTO @idSolicitud, @direccion, @calle
		SET @id_solicitud_ant = @idSolicitud
		
		WHILE @@FETCH_STATUS = 0
		BEGIN
				
			SET @Direccion_armada = ''
			WHILE @@FETCH_STATUS = 0 AND @id_solicitud_ant = @idSolicitud
			BEGIN

				IF IsNull(@Direccion_armada, '') = ''		
					SET @Direccion_armada =  IsNull(@direccion, '') + ' ' + IsNull(@calle, '')
				ELSE
					SET @Direccion_armada = @Direccion_armada + ' / ' + IsNull(@calle, '')
				
				FETCH NEXT FROM cur INTO @idSolicitud, @direccion, @calle
		
			END
			
			INSERT INTO @Direcciones VALUES(@id_solicitud_ant,@Direccion_armada)
			SET @id_solicitud_ant = @idSolicitud
			
		END
		CLOSE cur
		DEALLOCATE cur
		
	END
      
    INSERT INTO @tabla_ordenada  		
	SELECT 
		tt.id_tramitetarea, tt_HAB.id_solicitud, tt.id_tarea,
		tt.FechaInicio_tramitetarea, 
		tt.FechaAsignacion_tramtietarea,		
		sol.id_tipotramite,
		sol.id_tipoexpediente, sol.id_subtipoexpediente,
		dir.Direccion as direccion,     
		tarea.nombre_tarea,
		tarea.asignable_tarea,
		tarea.formulario_tarea,
		tiptra.descripcion_tipotramite,
		tipexp.descripcion_tipoexpediente,
		subtipexp.descripcion_subtipoexpediente,
		est.Descripcion,
		tiptra.descripcion_tipotramite + SPACE(1) + tipexp.descripcion_tipoexpediente + SPACE(1) + subtipexp.descripcion_subtipoexpediente as descripcion_tramite,
		DATEDIFF(dd, tt.FechaInicio_tramitetarea, GETDATE()) as Dias_Transcurridos,
        tt.UsuarioAsignado_tramitetarea,
        (sgi_usr.Apellido + ', ' + sgi_usr.Nombres) as nombre_UsuarioAsignado_tramitetarea,
        ROW_NUMBER() OVER(ORDER BY tt_HAB.id_solicitud) as nro_orden	
	FROM @tabla_datos datos
    INNER JOIN SGI_Tramites_Tareas tt ON datos.id_tramitetarea = tt.id_tramitetarea
    INNER JOIN SGI_Tramites_Tareas_HAB tt_HAB ON datos.id_tramitetarea = tt_HAB.id_tramitetarea
	INNER JOIN @Direcciones dir ON datos.id_solicitud = dir.id_solicitud
	INNER JOIN ENG_Tareas tarea ON tt.id_tarea = tarea.id_tarea
	INNER JOIN SSIT_Solicitudes sol ON tt_HAB.id_solicitud = sol.id_solicitud
	INNER JOIN tipotramite tiptra ON sol.id_tipotramite = tiptra.id_tipotramite
	INNER JOIN tipoexpediente tipexp ON sol.id_tipoexpediente = tipexp.id_tipoexpediente
	INNER JOIN subtipoexpediente subtipexp ON sol.id_subtipoexpediente = subtipexp .id_subtipoexpediente	
	INNER JOIN TipoEstadoSolicitud est ON est.id=sol.id_estado
	LEFT JOIN SGI_Profiles sgi_usr ON tt.UsuarioAsignado_tramitetarea = sgi_usr.userid
	ORDER BY tt_HAB.id_solicitud
	
	SELECT @cant_reg = COUNT(*) 
	FROM @tabla_ordenada
	
	SELECT 
		id_tramitetarea, id_solicitud, id_tarea,
		FechaInicio_tramitetarea, 
		FechaAsignacion_tramtietarea,		
		id_tipotramite,
		id_tipoexpediente, id_subtipoexpediente,
		direccion,     
		nombre_tarea,
		asignable_tarea,
		formulario_tarea,
		descripcion_tipotramite,
		descripcion_tipoexpediente,
		descripcion_subtipoexpediente,
		descripcion_estado,
		descripcion_tramite,
		Dias_Transcurridos,
        UsuarioAsignado_tramitetarea,
        nombre_UsuarioAsignado_tramitetarea,
        @cant_reg as cant_reg
	FROM @tabla_ordenada
	WHERE nro_orden > @start_row_index AND nro_orden <= (@start_row_index + @maximum_rows)
	ORDER BY id_solicitud
END
GO
/****** Object:  UserDefinedFunction [dbo].[SGI_ENG_Validar_Proxima_Tarea]    Script Date: 10/27/2016 12:09:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER FUNCTION [dbo].[SGI_ENG_Validar_Proxima_Tarea]
(
	@id_transicion		int 
	,@id_tramitetarea	int
	,@id_resultado		int 
)
RETURNS bit AS  
BEGIN 
	DECLARE @es_valida	bit
	
	set @es_valida = 1
	
	--tramite tarea actual
	DECLARE @tt_id_solicitud			int   
			,@tt_id_tarea				int   
			,@tt_id_resultado			int    
			,@id_circuito				int
			,@cod_circuito				nvarchar(10)
    		,@cantCorrecActual			int
			,@cantCorrec				int
			,@id_encomienda				int   

	--tramite tarea actual anterior
	DECLARE @tt_ant_id_tramitetarea			int
			,@tt_ant_id_tarea				int   
			,@tt_ant_id_resultado			int   
			,@tt_ant_id_resultadoG			int   
	        
	DECLARE @id_tarea_origen			int   
			,@id_tarea_destino			int   
			,@existe					int
 
	-- buscar tramite tarea actual
	select
        @tt_id_tarea  = id_tarea 
        ,@tt_id_resultado  = id_resultado
	from SGI_Tramites_Tareas
	where id_tramitetarea = @id_tramitetarea


	------------------------------------------------------------------
	-- Busqueda de circuito
	------------------------------------------------------------------
	SELECT
		@cod_circuito = cir.cod_circuito
		,@id_circuito = cir.id_circuito
	FROM
		ENG_Tareas tarea
		INNER JOIN ENG_Circuitos cir ON tarea.id_circuito = cir.id_circuito
	WHERE
		tarea.id_tarea = @tt_id_tarea
	------------------------------------------------------------------
	-- Busqueda de la solicitud
	------------------------------------------------------------------
	EXEC @tt_id_solicitud = Bus_id_solicitud @cod_circuito, @id_tramitetarea
	--Busco la ultima encomienda
	SELECT TOP 1 @id_encomienda = id_encomienda FROM encomienda 
		WHERE id_solicitud = @tt_id_solicitud 
		AND id_estado = dbo.Encomienda_Bus_idEstadoSolicitud('APROC')
		ORDER BY id_encomienda DESC	

	--buscar proxima tarea
	select 
		@id_tarea_origen = id_tarea_origen,
		@id_tarea_destino = id_tarea_destino
	from ENG_Transiciones
	where @id_transicion = id_transicion
	
	-- 11	25
	
	-- buscar tramite tarea anterior
	IF @cod_circuito = 'SCP' OR @cod_circuito ='SSP' OR @cod_circuito ='ESPECIAL'
	BEGIN
		select top 1 
			@tt_ant_id_tramitetarea  = tt.id_tramitetarea 
			,@tt_ant_id_tarea  = tt.id_tarea 
			,@tt_ant_id_resultado  = tt.id_resultado
		from SGI_Tramites_Tareas tt
		join SGI_Tramites_Tareas_HAB tt_HAB on tt_HAB.id_tramitetarea=tt.id_tramitetarea
		where tt_HAB.id_solicitud = @tt_id_solicitud
		and tt.id_tramitetarea < @id_tramitetarea
		order by tt.id_tramitetarea desc
		
	END
	ELSE IF @cod_circuito = 'CP'	
	BEGIN
		select top 1 
			@tt_ant_id_tramitetarea  = tt.id_tramitetarea 
			,@tt_ant_id_tarea  = tt.id_tarea 
			,@tt_ant_id_resultado  = tt.id_resultado
		from SGI_Tramites_Tareas tt
		join SGI_Tramites_Tareas_CPADRON tt_CP on tt_CP.id_tramitetarea=tt.id_tramitetarea
		where tt_CP.id_cpadron = @tt_id_solicitud
		and tt.id_tramitetarea < @id_tramitetarea
		order by tt.id_tramitetarea desc
	END
	ELSE IF @cod_circuito = 'TRANSF'	
	BEGIN
		select top 1 
			@tt_ant_id_tramitetarea  = tt.id_tramitetarea 
			,@tt_ant_id_tarea  = tt.id_tarea 
			,@tt_ant_id_resultado  = tt.id_resultado
		from SGI_Tramites_Tareas tt
		join SGI_Tramites_Tareas_TRANSF tt_TR on tt_TR.id_tramitetarea=tt.id_tramitetarea
		where tt_TR.id_solicitud = @tt_id_solicitud
		and tt.id_tramitetarea < @id_tramitetarea
		order by tt.id_tramitetarea desc
	END
	
	-- 218	10	19

	IF @tt_id_tarea in (dbo.Bus_id_tarea(111), dbo.Bus_id_tarea(211)) and 	    --> Revisión Sub-Gerente
	   @tt_ant_id_tarea in (dbo.Bus_id_tarea(110), dbo.Bus_id_tarea(210))		 --> Calificar Tramite
	begin
		--el subGerente esta de acuerdo con la calificacion.
		--Pero la decision del calificador puede derivar el tramite 
		--a diferentes tareas o por lo tanto en el combo de proximas
		--tareas solo se deben mostrar las correctas para 
		--la decision que tomo el calificador. y no todas las transacciones.

		IF	@id_tarea_destino in (dbo.Bus_id_tarea(112), dbo.Bus_id_tarea(212)) and 		-->Revision Gerente 
			@id_resultado = 23	and	    -->Estoy de Acuerdo con el Calificador
			@tt_ant_id_resultado not in (19)	-- 20 Pedir Rectificación o 21 Pedir Documentación Adicional
			
		BEGIN
		
			--lo revisa el gerente porque estoy de acuerdo con calificador
			--pero cuando el calificador no aprobo ( 19 ) la calificacion 
			--es una incoherencia y por lo tanto no es valido
			
			SET @es_valida = 0
		END
				
		IF	@id_tarea_destino in (dbo.Bus_id_tarea(125), dbo.Bus_id_tarea(225)) and 		-->Corrección de la Solicitud
			@id_resultado = 23	and	    -->Estoy de Acuerdo con el Calificador
			@tt_ant_id_resultado = 19 -- el calificador aprobo en tarea anterior
			
		BEGIN
		
			--lo revisa el gerente porque estoy de acuerdo con calificador
			--pero cuando el calificador no aprobo ( 19 ) la calificacion 
			--es una incoherencia y por lo tanto no es valido
			
			SET @es_valida = 0
		END
						
	END	

	IF @tt_id_tarea in (dbo.Bus_id_tarea(222)) 	    --> Generar Expediente SCP
	BEGIN
		DECLARE @tieneLey105 bit
		SET @tieneLey105 = 0
		--Busco si hay algun rubro con ley105
		IF  EXISTS (SELECT 1 FROM Encomienda_Rubros er
			INNER JOIN Rubros r ON r.cod_rubro=er.cod_rubro
			WHERE er.id_encomienda=@id_encomienda AND r.ley105=1)
			SET @tieneLey105 = 1
		
		IF	@id_tarea_destino in (dbo.Bus_id_tarea(231)) and 		-->Enviar AVH
			@tieneLey105 = 0		    -->Si NO tiene rubro de ley 105
			SET @es_valida = 0
	END

	IF @tt_id_tarea in (dbo.Bus_id_tarea(310),dbo.Bus_id_tarea(610)) 	    -->Especiales: Visar Trámite 1º
	BEGIN
		--Cantidad de Verificación AVH
		SELECT @cantCorrecActual = COUNT(*)
		FROM SGI_Tramites_Tareas tt
		join SGI_Tramites_Tareas_HAB tt_HAB on tt_HAB.id_tramitetarea=tt.id_tramitetarea
		WHERE tt_HAB.id_solicitud = @tt_id_solicitud
		AND tt.id_tarea IN(dbo.Bus_id_tarea(331),dbo.Bus_id_tarea(631))
				
		IF	@id_tarea_destino in (dbo.Bus_id_tarea(311),dbo.Bus_id_tarea(611)) AND 		-->Revisión Subgerente
			@cantCorrecActual = 0		    
			SET @es_valida = 0
	END

	IF @tt_id_tarea in (dbo.Bus_id_tarea(312),dbo.Bus_id_tarea(612)) 	    -->Especiales: Revisión Gerente 1º
	BEGIN
		--Cantidad de Dictamen - Asignar Profesional
		SELECT @cantCorrecActual = COUNT(*)
		FROM SGI_Tramites_Tareas tt
		join SGI_Tramites_Tareas_HAB tt_HAB on tt_HAB.id_tramitetarea=tt.id_tramitetarea
		WHERE tt_HAB.id_solicitud = @tt_id_solicitud
		AND tt.id_tarea IN(dbo.Bus_id_tarea(340),dbo.Bus_id_tarea(640))
				
		IF	@id_tarea_destino in (dbo.Bus_id_tarea(302),dbo.Bus_id_tarea(602)) AND 		-->Revisión Gerente 2º
			@cantCorrecActual = 0		    
			SET @es_valida = 0
	END
			
	IF @tt_id_tarea in (dbo.Bus_id_tarea(512)) 	    -->Transferencia: Revisión Gerente 1º
	BEGIN
		--Cantidad de Dictamen - Asignar Profesional
		SELECT @cantCorrecActual = COUNT(*)
		FROM SGI_Tramites_Tareas tt
		join SGI_Tramites_Tareas_HAB tt_HAB on tt_HAB.id_tramitetarea=tt.id_tramitetarea
		WHERE tt_HAB.id_solicitud = @tt_id_solicitud
		AND tt.id_tarea IN(dbo.Bus_id_tarea(540))
				
		IF	@id_tarea_destino in (dbo.Bus_id_tarea(531)) AND 		-->Revisión Gerente 2º
			@cantCorrecActual = 0		    
			SET @es_valida = 0
	END

	IF @tt_id_tarea in (dbo.Bus_id_tarea(325), dbo.Bus_id_tarea(625)) -->Correccion Solicitudes
	BEGIN
		--Busco si existe la tarea de calificar tramite
		SET @existe=0
		IF EXISTS(SELECT 1
					FROM SGI_Tramites_Tareas tt
					join SGI_Tramites_Tareas_HAB tt_HAB on tt_HAB.id_tramitetarea=tt.id_tramitetarea
					WHERE tt_HAB.id_solicitud = @tt_id_solicitud
					AND tt.id_tarea IN(dbo.Bus_id_tarea(301), dbo.Bus_id_tarea(601)))
			SET @existe = 1

		IF	@id_tarea_destino in (dbo.Bus_id_tarea(310), dbo.Bus_id_tarea(610))  		-->1er Calificar trámite
			AND @existe = 1
			SET @es_valida = 0
		IF	@id_tarea_destino in (dbo.Bus_id_tarea(301), dbo.Bus_id_tarea(601))  		-->Calificar trámite
			AND @existe = 0
			SET @es_valida = 0
	END	

	DECLARE @revisaGerente			bit
			,@revisaSubGerente		bit
			,@Superficie			int
			,@Revisa				nvarchar(50)
			,@RevisaMenor			nvarchar(50)
			,@RevisaMayor			nvarchar(50)
			,@supLocal				decimal(8,2)
			,@sup					decimal(8,2)	

	IF @tt_id_tarea in (dbo.Bus_id_tarea(1106), dbo.Bus_id_tarea(1206))   --> Solicitud de Habilitación
	BEGIN

		SET @revisaGerente = 0
		SET @revisaSubGerente = 0
		
		------------------------------------------
		-- Condiciones rubros
		------------------------------------------
		IF EXISTS(SELECT 1 FROM dbo.Encomienda_Rubros er 
				JOIN Rubros r ON r.cod_rubro=er.cod_rubro
				JOIN Parametros_Bandeja_Rubro pr ON pr.id_rubro=r.id_rubro
				WHERE er.id_encomienda=@id_encomienda AND pr.Revisa='Gerente')
			SET @revisaGerente = 1
		IF EXISTS(SELECT 1 FROM dbo.Encomienda_Rubros er 
				JOIN dbo.Rubros r ON r.cod_rubro=er.cod_rubro
				JOIN dbo.Parametros_Bandeja_Rubro pr ON pr.id_rubro=r.id_rubro
				WHERE er.id_encomienda=@id_encomienda AND pr.Revisa='SubGerente')
			SET @revisaSubGerente = 1
		
		IF @revisaGerente = 0 AND @revisaSubGerente = 0
		BEGIN
			------------------------------------------
			--Condiciones por superficie
			------------------------------------------
			SELECT @Superficie = Superficie
				,@RevisaMenor = RevisaMenor
				,@RevisaMayor = RevisaMayor
			FROM dbo.Parametros_Bandeja_Superficie
			WHERE id_circuito = @id_circuito
			
			SET @sup = CONVERT(decimal(8,2), @Superficie)
			IF @supLocal < @sup
				SET @Revisa = @RevisaMenor
			ELSE
				SET @Revisa = @RevisaMayor
			
			IF @Revisa = 'Gerente'
				SET @revisaGerente = 1
			ELSE
				SET @revisaSubGerente = 1
		END

		IF	@id_tarea_destino in (dbo.Bus_id_tarea(1108), dbo.Bus_id_tarea(1208))  	-->Asignación al Calificador Subgerente
			AND @revisaSubGerente = 0													
		BEGIN
			SET @es_valida = 0
		END
				
		IF	@id_tarea_destino in (dbo.Bus_id_tarea(1109), dbo.Bus_id_tarea(1209)) 	-->Asignación al Calificador Gerente
			AND @revisaGerente = 0													
		BEGIN
			SET @es_valida = 0
		END
						
	END	
	
	IF @tt_id_tarea in (dbo.Bus_id_tarea(1110), dbo.Bus_id_tarea(1210))   --> Calificar Tramite
	BEGIN
		DECLARE @tieneAsigSubGe bit
				,@tieneAsigGe bit
				
		SET @tieneAsigSubGe = 0
		SET @tieneAsigGe = 0

		--Cantidad de corecciones del circuito
		SELECT @cantCorrec = Cantidad FROM [dbo].[Parametros_Observaciones] WHERE id_circuito=@id_circuito
		SET @cantCorrec = ISNULL(@cantCorrec, 0)
		
		--Cantidad de corecciones de solicitud
		SELECT @cantCorrecActual = COUNT(*)
		FROM SGI_Tramites_Tareas tt
		join SGI_Tramites_Tareas_HAB tt_HAB on tt_HAB.id_tramitetarea=tt.id_tramitetarea
		WHERE tt_HAB.id_solicitud = @tt_id_solicitud
		AND tt.id_tarea IN(dbo.Bus_id_tarea(1125), dbo.Bus_id_tarea(1225))
		
		--Busco la si tiene la tarea de Asignación al Calificador Subgerente
		IF  EXISTS (SELECT 1 FROM SGI_Tramites_Tareas tt
					JOIN SGI_Tramites_Tareas_HAB tt_HAB ON tt_HAB.id_tramitetarea=tt.id_tramitetarea
					WHERE tt_HAB.id_solicitud = @tt_id_solicitud
					AND tt.id_tarea IN(dbo.Bus_id_tarea(1108), dbo.Bus_id_tarea(1208)))
			SET @tieneAsigSubGe = 1
		--Busco la si tiene la tarea de Asignación al Calificador Gerente
		IF  EXISTS (SELECT 1 FROM SGI_Tramites_Tareas tt
					JOIN SGI_Tramites_Tareas_HAB tt_HAB ON tt_HAB.id_tramitetarea=tt.id_tramitetarea
					WHERE tt_HAB.id_solicitud = @tt_id_solicitud
					AND tt.id_tarea IN(dbo.Bus_id_tarea(1109), dbo.Bus_id_tarea(1209)))
			SET @tieneAsigGe = 1

		IF	@id_tarea_destino in (dbo.Bus_id_tarea(1111), dbo.Bus_id_tarea(1211))  	-->Revision SubGerente 
			AND @tieneAsigSubGe = 0													--> Asignación al Calificador Subgerente
		BEGIN
			SET @es_valida = 0
		END
				
		IF	@id_tarea_destino in (dbo.Bus_id_tarea(1112), dbo.Bus_id_tarea(1212)) 	-->Revision Gerente 
			AND @tieneAsigGe = 0													--> Asignación al Calificador Gerente
		BEGIN
			SET @es_valida = 0
		END
						
		IF	@id_tarea_destino in (dbo.Bus_id_tarea(1125), dbo.Bus_id_tarea(1225)) 	-->Correccion de Solicitudes
			AND @cantCorrecActual >= @cantCorrec	
		BEGIN
			SET @es_valida = 0
		END
	END	
	
	IF @tt_id_tarea in (dbo.Bus_id_tarea(1111), dbo.Bus_id_tarea(1211))   --> Revision Suberente
	BEGIN

		--Busco el resultado el calificador 
		SELECT TOP 1 
			@tt_ant_id_tramitetarea  = tt.id_tramitetarea 
			,@tt_ant_id_tarea  = tt.id_tarea 
			,@tt_ant_id_resultado  = tt.id_resultado
		FROM SGI_Tramites_Tareas tt
		join SGI_Tramites_Tareas_HAB tt_HAB on tt_HAB.id_tramitetarea=tt.id_tramitetarea
		WHERE tt_HAB.id_solicitud = @tt_id_solicitud
		AND tt.id_tarea IN(dbo.Bus_id_tarea(1110), dbo.Bus_id_tarea(1210))
		ORDER BY tt.id_tramitetarea DESC

		IF	@id_tarea_destino in (dbo.Bus_id_tarea(1114), dbo.Bus_id_tarea(1214)) and 		-->Revisión DGHyP
			@id_resultado = 61	and	    -->Ratifica Calificación
			@tt_ant_id_resultado = 20 -- el calificador Pedir Rectificación

		BEGIN
			SET @es_valida = 0
		END

		IF	@id_tarea_destino in (dbo.Bus_id_tarea(1125), dbo.Bus_id_tarea(1225)) and 		-->Correccion Solicitudes
			@id_resultado = 61	and	    -->Ratifica Calificación
			@tt_ant_id_resultado = 19 -- el calificador aprobo

		BEGIN
			SET @es_valida = 0
		END						
	END	

	IF @tt_id_tarea in (dbo.Bus_id_tarea(1112), dbo.Bus_id_tarea(1212))   --> Revision Gerente
	BEGIN

		--Busco el resultado el calificador 
		SELECT TOP 1 
			@tt_ant_id_tramitetarea  = tt.id_tramitetarea 
			,@tt_ant_id_tarea  = tt.id_tarea 
			,@tt_ant_id_resultado  = tt.id_resultado
		FROM SGI_Tramites_Tareas tt
		join SGI_Tramites_Tareas_HAB tt_HAB on tt_HAB.id_tramitetarea=tt.id_tramitetarea
		WHERE tt_HAB.id_solicitud = @tt_id_solicitud
		AND tt.id_tarea IN(dbo.Bus_id_tarea(1110), dbo.Bus_id_tarea(1210))
		ORDER BY tt.id_tramitetarea DESC

		IF	@id_tarea_destino in (dbo.Bus_id_tarea(1114), dbo.Bus_id_tarea(1214)) and 		-->Revisión DGHyP
			@id_resultado = 61	and	    -->Ratifica Calificación
			@tt_ant_id_resultado = 20 -- el calificador Pedir Rectificación

		BEGIN
			SET @es_valida = 0
		END

		IF	@id_tarea_destino in (dbo.Bus_id_tarea(1125), dbo.Bus_id_tarea(1225)) and 		-->Correccion Solicitudes
			@id_resultado = 61	and	    -->Ratifica Calificación
			@tt_ant_id_resultado = 19 -- el calificador aprobo

		BEGIN
			SET @es_valida = 0
		END						
	END	
	
	IF @tt_id_tarea in (dbo.Bus_id_tarea(1114), dbo.Bus_id_tarea(1214))   --> Revisión DGHyP
	BEGIN

		--Busco la tarea anterior 
		SELECT TOP 1 
			@tt_ant_id_tarea  = tt.id_tarea 
		FROM SGI_Tramites_Tareas tt
		join SGI_Tramites_Tareas_HAB tt_HAB on tt_HAB.id_tramitetarea=tt.id_tramitetarea
		WHERE tt_HAB.id_solicitud = @tt_id_solicitud
		AND tt.id_tramitetarea < @id_tramitetarea
		ORDER BY tt.id_tramitetarea DESC

		IF	@id_tarea_destino in (dbo.Bus_id_tarea(1111), dbo.Bus_id_tarea(1211), dbo.Bus_id_tarea(1112), dbo.Bus_id_tarea(1212))
		AND @id_tarea_destino <> @tt_ant_id_tarea
		BEGIN
			SET @es_valida = 0
		END
	END	

	IF @tt_id_tarea in (dbo.Bus_id_tarea(1127), dbo.Bus_id_tarea(1227))   --> Revision Firma Disposicion
	BEGIN
		--Busco el resultado el calificador 
		SELECT TOP 1 
			@tt_ant_id_resultado  = tt.id_resultado
		FROM SGI_Tramites_Tareas tt
		join SGI_Tramites_Tareas_HAB tt_HAB on tt_HAB.id_tramitetarea=tt.id_tramitetarea
		WHERE tt_HAB.id_solicitud = @tt_id_solicitud
		AND tt.id_tarea IN(dbo.Bus_id_tarea(1110), dbo.Bus_id_tarea(1210))
		ORDER BY tt.id_tramitetarea DESC

		--Busco el resultado el gerente o subgerente 
		SELECT TOP 1 
			@tt_ant_id_resultadoG  = tt.id_resultado
		FROM SGI_Tramites_Tareas tt
		join SGI_Tramites_Tareas_HAB tt_HAB on tt_HAB.id_tramitetarea=tt.id_tramitetarea
		WHERE tt_HAB.id_solicitud = @tt_id_solicitud
		AND tt.id_tarea IN(dbo.Bus_id_tarea(1111), dbo.Bus_id_tarea(1211),dbo.Bus_id_tarea(1112), dbo.Bus_id_tarea(1212))
		ORDER BY tt.id_tramitetarea DESC
		
		IF	@id_tarea_destino in (dbo.Bus_id_tarea(1123), dbo.Bus_id_tarea(1223)) and 		-->Entrega tramite
			(@tt_ant_id_resultado = 20 -- el calificador Pedir Rectificación
			or @tt_ant_id_resultadoG = 60 --el gerente o subgerente Requiere rechazo
			)
		BEGIN
			SET @es_valida = 0
		END

		IF	@id_tarea_destino in (dbo.Bus_id_tarea(1135), dbo.Bus_id_tarea(1235)) and 		-->Enviar_DGFC.aspx
			@tt_ant_id_resultado = 19 and -- el calificador aprobo
			@tt_ant_id_resultadoG = 61 --Ratifica calificación
		BEGIN
			SET @es_valida = 0
		END						
	END	

	--Especiales/Esparcimiento
	IF @tt_id_tarea in (dbo.Bus_id_tarea(1310), dbo.Bus_id_tarea(1410))   -->1er Calificar Tramite
	BEGIN

		SET @revisaGerente = 0
		SET @revisaSubGerente = 0
		
		--Condiciones rubros
		IF EXISTS(SELECT 1 FROM Encomienda_Rubros er 
				JOIN Rubros r ON r.cod_rubro=er.cod_rubro
				JOIN Parametros_Bandeja_Rubro pr ON pr.id_rubro=r.id_rubro
				WHERE er.id_encomienda=@id_encomienda AND pr.Revisa='Gerente')
			SET @revisaGerente = 1
		IF EXISTS(SELECT 1 FROM Encomienda_Rubros er 
				JOIN Rubros r ON r.cod_rubro=er.cod_rubro
				JOIN Parametros_Bandeja_Rubro pr ON pr.id_rubro=r.id_rubro
				WHERE er.id_encomienda=@id_encomienda AND pr.Revisa='SubGerente')
			SET @revisaSubGerente = 1
		
		IF @revisaGerente=0 AND @revisaSubGerente=0
		BEGIN
			--condiciones por superficie
			SELECT @supLocal = ed.superficie_cubierta_dl+ed.superficie_descubierta_dl 
			FROM Encomienda_DatosLocal ed
			WHERE ed.id_encomienda=@id_encomienda
			
			SELECT @Superficie = Superficie
				,@RevisaMenor = RevisaMenor
				,@RevisaMayor = RevisaMayor
			FROM dbo.Parametros_Bandeja_Superficie
			WHERE id_circuito = @id_circuito
			
			SET @sup = CONVERT(decimal(8,2), @Superficie)
			IF @supLocal < @sup
				set @Revisa = @RevisaMenor
			ELSE
				set @Revisa = @RevisaMayor
			
			IF @Revisa = 'Gerente'
				SET @revisaGerente = 1
			ELSE
				SET @revisaSubGerente = 1
		END

		--Cantidad de corecciones del circuito
		SELECT @cantCorrec = Cantidad FROM [Parametros_Observaciones] WHERE id_circuito=@id_circuito
		SET @cantCorrec = ISNULL(@cantCorrec, 0)
		
		--Cantidad de corecciones de solicitud
		SELECT @cantCorrecActual = COUNT(*)
		FROM SGI_Tramites_Tareas tt
		join SGI_Tramites_Tareas_HAB tt_HAB on tt_HAB.id_tramitetarea=tt.id_tramitetarea
		WHERE tt_HAB.id_solicitud = @tt_id_solicitud
		AND tt.id_tarea IN(dbo.Bus_id_tarea(1325), dbo.Bus_id_tarea(1425))
		
		IF	@id_tarea_destino in (dbo.Bus_id_tarea(1311), dbo.Bus_id_tarea(1411))  	-->Revision SubGerente 
			AND @revisaSubGerente = 0													
		BEGIN
			SET @es_valida = 0
		END
				
		IF	@id_tarea_destino in (dbo.Bus_id_tarea(1312), dbo.Bus_id_tarea(1412)) 	-->Revision Gerente 
			AND @revisaGerente = 0													
		BEGIN
			SET @es_valida = 0
		END
						
		IF	@id_tarea_destino in (dbo.Bus_id_tarea(1325), dbo.Bus_id_tarea(1425)) 	-->Correccion de Solicitudes
			AND @cantCorrecActual >= @cantCorrec	
		BEGIN
			SET @es_valida = 0
		END
	END	
	IF @tt_id_tarea in (dbo.Bus_id_tarea(1301), dbo.Bus_id_tarea(1401))   -->Calificar Tramite
	BEGIN

		SET @revisaGerente = 0
		SET @revisaSubGerente = 0
		
		--Condiciones rubros
		IF EXISTS(SELECT 1 FROM Encomienda_Rubros er 
				JOIN Rubros r ON r.cod_rubro=er.cod_rubro
				JOIN Parametros_Bandeja_Rubro pr ON pr.id_rubro=r.id_rubro
				WHERE er.id_encomienda=@id_encomienda AND pr.Revisa='Gerente')
			SET @revisaGerente = 1
		IF EXISTS(SELECT 1 FROM Encomienda_Rubros er 
				JOIN Rubros r ON r.cod_rubro=er.cod_rubro
				JOIN Parametros_Bandeja_Rubro pr ON pr.id_rubro=r.id_rubro
				WHERE er.id_encomienda=@id_encomienda AND pr.Revisa='SubGerente')
			SET @revisaSubGerente = 1
		
		IF @revisaGerente=0 AND @revisaSubGerente=0
		BEGIN
			--condiciones por superficie
			SELECT @supLocal = ed.superficie_cubierta_dl+ed.superficie_descubierta_dl 
			FROM Encomienda_DatosLocal ed 
			WHERE ed.id_encomienda=@id_encomienda
			
			SELECT @Superficie = Superficie
				,@RevisaMenor = RevisaMenor
				,@RevisaMayor = RevisaMayor
			FROM dbo.Parametros_Bandeja_Superficie
			WHERE id_circuito = @id_circuito
			
			SET @sup = CONVERT(decimal(8,2), @Superficie)
			IF @supLocal < @sup
				set @Revisa = @RevisaMenor
			ELSE
				set @Revisa = @RevisaMayor
			
			IF @Revisa = 'Gerente'
				SET @revisaGerente = 1
			ELSE
				SET @revisaSubGerente = 1
		END

		--Cantidad de corecciones del circuito
		SELECT @cantCorrec = Cantidad FROM [Parametros_Observaciones] WHERE id_circuito=@id_circuito
		SET @cantCorrec = ISNULL(@cantCorrec, 0)
		
		--Cantidad de corecciones de solicitud
		SELECT @cantCorrecActual = COUNT(*)
		FROM SGI_Tramites_Tareas tt
		join SGI_Tramites_Tareas_HAB tt_HAB on tt_HAB.id_tramitetarea=tt.id_tramitetarea
		WHERE tt_HAB.id_solicitud = @tt_id_solicitud
		AND tt.id_tarea IN(dbo.Bus_id_tarea(1325), dbo.Bus_id_tarea(1425))
		
		IF	@id_tarea_destino in (dbo.Bus_id_tarea(1303), dbo.Bus_id_tarea(1403))  	-->Revision SubGerente 
			AND @revisaSubGerente = 0													
		BEGIN
			SET @es_valida = 0
		END
				
		IF	@id_tarea_destino in (dbo.Bus_id_tarea(1302), dbo.Bus_id_tarea(1402)) 	-->Revision Gerente 
			AND @revisaGerente = 0													
		BEGIN
			SET @es_valida = 0
		END
						
		IF	@id_tarea_destino in (dbo.Bus_id_tarea(1325), dbo.Bus_id_tarea(1425)) 	-->Correccion de Solicitudes
			AND @cantCorrecActual >= @cantCorrec	
		BEGIN
			SET @es_valida = 0
		END
	END	
	IF @tt_id_tarea in (dbo.Bus_id_tarea(1303), dbo.Bus_id_tarea(1403))   --> Revision Subgerente
	BEGIN

		--Busco el resultado el calificador 
		SELECT TOP 1 
			@tt_ant_id_tramitetarea  = tt.id_tramitetarea 
			,@tt_ant_id_tarea  = tt.id_tarea 
			,@tt_ant_id_resultado  = tt.id_resultado
		FROM SGI_Tramites_Tareas tt
		join SGI_Tramites_Tareas_HAB tt_HAB on tt_HAB.id_tramitetarea=tt.id_tramitetarea
		WHERE tt_HAB.id_solicitud = @tt_id_solicitud
		AND tt.id_tarea IN(dbo.Bus_id_tarea(1301), dbo.Bus_id_tarea(1401))
		ORDER BY tt.id_tramitetarea DESC

		IF	@id_tarea_destino in (dbo.Bus_id_tarea(1340), dbo.Bus_id_tarea(1440)) and 		-->Dictamen - Asignar Profesional
			@id_resultado = 65	and	    -->Ratifica Calificación
			@tt_ant_id_resultado = 20 -- el calificador Pedir Rectificación
		BEGIN
			SET @es_valida = 0
		END

		IF	@id_tarea_destino in (dbo.Bus_id_tarea(1345), dbo.Bus_id_tarea(1445), dbo.Bus_id_tarea(1331), dbo.Bus_id_tarea(1431)) and 		
			-->Generar ticket liza or Verificacion AVH
			@id_resultado = 65	and	    -->Ratifica Calificación
			@tt_ant_id_resultado = 19 -- el calificador Aprobo
		BEGIN
			SET @es_valida = 0
		END
		
		IF	@id_tarea_destino in (dbo.Bus_id_tarea(1325), dbo.Bus_id_tarea(1425)) and 		-->Correccion Solicitudes
			@id_resultado = 65	and	    -->Ratifica Calificación
			@tt_ant_id_resultado = 19 -- el calificador aprobo
		BEGIN
			SET @es_valida = 0
		END						
	END	

	IF @tt_id_tarea in (dbo.Bus_id_tarea(1302), dbo.Bus_id_tarea(1402))   --> Revision Gerente
	BEGIN

		--Busco el resultado el calificador 
		SELECT TOP 1 
			@tt_ant_id_tramitetarea  = tt.id_tramitetarea 
			,@tt_ant_id_tarea  = tt.id_tarea 
			,@tt_ant_id_resultado  = tt.id_resultado
		FROM SGI_Tramites_Tareas tt
		join SGI_Tramites_Tareas_HAB tt_HAB on tt_HAB.id_tramitetarea=tt.id_tramitetarea
		WHERE tt_HAB.id_solicitud = @tt_id_solicitud
		AND tt.id_tarea IN(dbo.Bus_id_tarea(1301), dbo.Bus_id_tarea(1401))
		ORDER BY tt.id_tramitetarea DESC

		IF	@id_tarea_destino in (dbo.Bus_id_tarea(1340), dbo.Bus_id_tarea(1440)) and 		-->Dictamen - Asignar Profesional
			@id_resultado = 65	and	    -->Ratifica 
			@tt_ant_id_resultado = 20 -- el calificador Pedir Rectificación
		BEGIN
			SET @es_valida = 0
		END

		IF	@id_tarea_destino in (dbo.Bus_id_tarea(1345), dbo.Bus_id_tarea(1445), dbo.Bus_id_tarea(1331), dbo.Bus_id_tarea(1431)) and 		
			-->Generar ticket liza or Verificacion AVH
			@id_resultado = 65	and	    -->Ratifica Calificación
			@tt_ant_id_resultado = 19 -- el calificador Aprobo
		BEGIN
			SET @es_valida = 0
		END

		IF	@id_tarea_destino in (dbo.Bus_id_tarea(1325), dbo.Bus_id_tarea(1425)) and 		-->Correccion Solicitudes
			@id_resultado = 65	and	    -->Ratifica 
			@tt_ant_id_resultado = 19 -- el calificador aprobo
		BEGIN
			SET @es_valida = 0
		END						
	END	
	IF @tt_id_tarea in (dbo.Bus_id_tarea(1314), dbo.Bus_id_tarea(1414))   --> Revisión DGHyP
	BEGIN
		SET @revisaGerente = 0
		SET @revisaSubGerente = 0
		
		--Condiciones rubros
		IF EXISTS(SELECT 1 FROM Encomienda_Rubros er 
				JOIN Rubros r ON r.cod_rubro=er.cod_rubro
				JOIN Parametros_Bandeja_Rubro pr ON pr.id_rubro=r.id_rubro
				WHERE er.id_encomienda=@id_encomienda AND pr.Revisa='Gerente')
			SET @revisaGerente = 1
		IF EXISTS(SELECT 1 FROM Encomienda_Rubros er 
				JOIN Rubros r ON r.cod_rubro=er.cod_rubro
				JOIN Parametros_Bandeja_Rubro pr ON pr.id_rubro=r.id_rubro
				WHERE er.id_encomienda=@id_encomienda AND pr.Revisa='SubGerente')
			SET @revisaSubGerente = 1
		
		IF @revisaGerente=0 AND @revisaSubGerente=0
		BEGIN
			--condiciones por superficie
			SELECT @supLocal = ed.superficie_cubierta_dl+ed.superficie_descubierta_dl 
			FROM Encomienda_DatosLocal ed
			WHERE ed.id_encomienda=@id_encomienda
			
			SELECT @Superficie = Superficie
				,@RevisaMenor = RevisaMenor
				,@RevisaMayor = RevisaMayor
			FROM dbo.Parametros_Bandeja_Superficie
			WHERE id_circuito = @id_circuito
			
			SET @sup = CONVERT(decimal(8,2), @Superficie)
			IF @supLocal < @sup
				set @Revisa = @RevisaMenor
			ELSE
				set @Revisa = @RevisaMayor
			
			IF @Revisa = 'Gerente'
				SET @revisaGerente = 1
			ELSE
				SET @revisaSubGerente = 1
		END

		
		IF	@id_tarea_destino in (dbo.Bus_id_tarea(1303), dbo.Bus_id_tarea(1403))  	-->Revision SubGerente 
			AND @revisaSubGerente = 0													
		BEGIN
			SET @es_valida = 0
		END
				
		IF	@id_tarea_destino in (dbo.Bus_id_tarea(1302), dbo.Bus_id_tarea(1402)) 	-->Revision Gerente 
			AND @revisaGerente = 0													
		BEGIN
			SET @es_valida = 0
		END
	END	

	IF @tt_id_tarea in (dbo.Bus_id_tarea(1327), dbo.Bus_id_tarea(1427))   --> Revision Firma Disposicion
	BEGIN
		--Busco el resultado el calificador 
		SELECT TOP 1 
			@tt_ant_id_resultado  = tt.id_resultado
		FROM SGI_Tramites_Tareas tt
		join SGI_Tramites_Tareas_HAB tt_HAB on tt_HAB.id_tramitetarea=tt.id_tramitetarea
		WHERE tt_HAB.id_solicitud = @tt_id_solicitud
		AND tt.id_tarea IN(dbo.Bus_id_tarea(1301), dbo.Bus_id_tarea(1401))
		ORDER BY tt.id_tramitetarea DESC

		--Busco el resultado el gerente o subgerente 
		SELECT TOP 1 
			@tt_ant_id_resultadoG  = tt.id_resultado
		FROM SGI_Tramites_Tareas tt
		join SGI_Tramites_Tareas_HAB tt_HAB on tt_HAB.id_tramitetarea=tt.id_tramitetarea
		WHERE tt_HAB.id_solicitud = @tt_id_solicitud
		AND tt.id_tarea IN(dbo.Bus_id_tarea(1302), dbo.Bus_id_tarea(1402),dbo.Bus_id_tarea(1303), dbo.Bus_id_tarea(1403))
		ORDER BY tt.id_tramitetarea DESC

		IF	@id_tarea_destino in (dbo.Bus_id_tarea(1323), dbo.Bus_id_tarea(1423)) and 		-->Entrega tramite
			(@tt_ant_id_resultado = 20 -- el calificador Pedir Rectificación
			or @tt_ant_id_resultadoG = 60 --el gerente o subgerente Requiere rechazo
			)
		BEGIN
			SET @es_valida = 0
		END

		IF	@id_tarea_destino in (dbo.Bus_id_tarea(1335), dbo.Bus_id_tarea(1435)) and 		-->Enviar_DGFC.aspx
			@tt_ant_id_resultado = 19 and -- el calificador aprobo
			@tt_ant_id_resultadoG = 61 --Ratifica calificación

		BEGIN
			SET @es_valida = 0
		END						
	END	
	
	
	IF @tt_id_tarea in (dbo.Bus_id_tarea(1325), dbo.Bus_id_tarea(1425)) -->Correccion Solicitudes
	BEGIN
		--Busco si existe la tarea de calificar tramite
		SET @existe=0
		IF EXISTS(SELECT 1
					FROM SGI_Tramites_Tareas tt
					join SGI_Tramites_Tareas_HAB tt_HAB on tt_HAB.id_tramitetarea=tt.id_tramitetarea
					WHERE tt_HAB.id_solicitud = @tt_id_solicitud
					AND tt.id_tarea IN(dbo.Bus_id_tarea(1301), dbo.Bus_id_tarea(1401)))
			SET @existe = 1

		IF	@id_tarea_destino in (dbo.Bus_id_tarea(1310), dbo.Bus_id_tarea(1410))  		-->1er Calificar trámite
			AND @existe = 1
			SET @es_valida = 0
		IF	@id_tarea_destino in (dbo.Bus_id_tarea(1301), dbo.Bus_id_tarea(1401))  		-->Calificar trámite
			AND @existe = 0
			SET @es_valida = 0
	END	
	RETURN @es_valida
END
GO
/****** Object:  StoredProcedure [dbo].[SGI_HAB_GenerarProcesos_SADE]    Script Date: 10/28/2016 08:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [dbo].[SGI_HAB_GenerarProcesos_SADE]
(
	@id_tramitetarea int,
	@userid uniqueidentifier
)
AS
BEGIN

	DECLARE 
		@id_tarea_proc int
		,@id_proceso  int				-- id del proceso que se va a generar
		,@descripcion_tramite nvarchar(200)		-- descripción del registro ( referencia en pantalla)
		,@parametros_SADE nvarchar(200)		-- parametros necesarios para la pasarela, como ser el destinatario de un pase.
		,@id_paquete int
		,@id_solicitud int					-- Nro de trámite de consulta al padrón
		,@id_docadjunto	int
		,@nombre_tdocreq nvarchar(100)
		,@id_origen_reg int
		,@id_file int
		,@id_tdocreq int
		,@cod_tipodocsis nvarchar(30)
		,@nombre_tipodocsis nvarchar(100)
		,@UserName_SADE_Cal nvarchar(20)
		,@UserName_SADE_SG nvarchar(20)
		,@UserName_SADE_GE nvarchar(20)
		,@UserName_SADE_DGHP nvarchar(20)
		,@acronimo_SADE nvarchar(20)
		,@hay_tarea_Cal bit
		,@hay_tarea_SG bit
		,@hay_tarea_GE bit
		,@msgError nvarchar(100)
		,@nro_tramite int
		,@id_encomienda int

    DECLARE @enc_hist TABLE
    (
		id_encomienda	int
    )			
	
	SET @id_paquete = 0 
	SET @hay_tarea_Cal = 0
	SET @hay_tarea_SG = 0
	SET @hay_tarea_GE = 0
	-------------------------------------
	-- Recupera el nro de solicitud
	-------------------------------------
	SELECT TOP 1 
		@id_solicitud = tt_hab.id_solicitud
	FROM 
		SGI_Tramites_Tareas_HAB tt_hab 
	INNER JOIN
		SSIT_Solicitudes sol ON sol.id_solicitud = tt_hab.id_solicitud
	WHERE 
		tt_hab.id_tramitetarea = @id_tramitetarea
		
	------------------------------------------------
	-- Iniciov Validaciones
	-- Validar que el calificador tenga usuario SADE
	------------------------------------------------
  	SELECT TOP 1 @UserName_SADE_Cal = pro.UserName_SADE
	FROM 
		SGI_Tramites_Tareas tt
		INNER JOIN SGI_Tramites_Tareas_HAB tt_hab ON tt.id_tramitetarea = tt_hab.id_tramitetarea
		INNER JOIN SGI_Tarea_Calificar tt_cal ON tt.id_tramitetarea = tt_cal.id_tramitetarea
		INNER JOIN SGI_Profiles pro ON tt.UsuarioAsignado_tramitetarea=pro.userid
	WHERE tt_hab.id_solicitud = @id_solicitud
	ORDER BY tt.id_tramitetarea DESC
	
	IF @@ROWCOUNT > 0 
		SET @hay_tarea_Cal = 1
		
	IF LEN(ISNULL(@UserName_SADE_Cal,''))=0
	BEGIN
		SET @msgError = 'El usuario responsable de la tarea de calificación no tiene usuario en SADE.'
		RAISERROR(@msgError,16,1)
		RETURN
	END
	-----------------------------------------------
	-- Validar que el subgerente tenga usuario SADE
	-----------------------------------------------
  	SELECT TOP 1 @UserName_SADE_SG = pro.UserName_SADE
	FROM 
		SGI_Tramites_Tareas tt
		INNER JOIN SGI_Tramites_Tareas_HAB tt_hab ON tt.id_tramitetarea = tt_hab.id_tramitetarea
		INNER JOIN SGI_Tarea_Revision_SubGerente tt_sg ON tt.id_tramitetarea = tt_sg.id_tramitetarea
		INNER JOIN SGI_Profiles pro ON tt.UsuarioAsignado_tramitetarea=pro.userid
	WHERE tt_hab.id_solicitud = @id_solicitud
	ORDER BY tt.id_tramitetarea DESC
	
	-- Puede que no haya tarea de Revision Sub Gerente
	IF @@ROWCOUNT > 0 
		SET @hay_tarea_SG = 1
	
	IF @hay_tarea_SG = 1 AND LEN(ISNULL(@UserName_SADE_SG,''))=0
	BEGIN
		SET @msgError = 'El usuario responsable de la tarea Revisión Subgerente no tiene usuario en SADE.'
		RAISERROR(@msgError,16,1)
		RETURN
	END	
	
	----------------------------------------------
	-- Validar que el gerente tenga usuario SADE
	----------------------------------------------
  	SELECT TOP 1 @UserName_SADE_GE = pro.UserName_SADE
	FROM 
		SGI_Tramites_Tareas tt
		INNER JOIN SGI_Tramites_Tareas_HAB tt_hab ON tt.id_tramitetarea = tt_hab.id_tramitetarea
		INNER JOIN SGI_Tarea_Revision_Gerente tt_ge ON tt.id_tramitetarea = tt_ge.id_tramitetarea
		INNER JOIN SGI_Profiles pro ON tt.UsuarioAsignado_tramitetarea=pro.userid
	WHERE tt_hab.id_solicitud = @id_solicitud
	ORDER BY tt.id_tramitetarea DESC
	
	IF @@ROWCOUNT > 0 
		SET @hay_tarea_GE = 1
	
	IF @hay_tarea_GE = 1 AND LEN(ISNULL(@UserName_SADE_GE,''))=0
	BEGIN
		SET @msgError = 'El usuario responsable de la tarea Revisión Gerente no tiene usuario en SADE.'
		RAISERROR(@msgError,16,1)
		RETURN
	END		
	/*
	---------------------------------------------------------------
	-- Validar que el usuario de Revisión DGHyP tenga usuario SADE
	---------------------------------------------------------------
  	SELECT TOP 1 @UserName_SADE_DGHP = pro.UserName_SADE
	FROM 
		SGI_Tramites_Tareas tt
		INNER JOIN SGI_Tramites_Tareas_HAB tt_hab ON tt.id_tramitetarea = tt_hab.id_tramitetarea
		INNER JOIN SGI_Tarea_Revision_DGHP tt_rv ON tt.id_tramitetarea = tt_rv.id_tramitetarea
		INNER JOIN SGI_Profiles pro ON tt.UsuarioAsignado_tramitetarea=pro.userid
	WHERE tt_hab.id_solicitud = @id_solicitud
	ORDER BY tt.id_tramitetarea DESC
	
	IF LEN(ISNULL(@UserName_SADE_DGHP,''))=0
	BEGIN
		SET @msgError = 'El usuario responsable de la tarea Revisión DGHyP no tiene usuario en SADE.'
		RAISERROR(@msgError,16,1)
		RETURN
	END		
	*/
	-------------------------------------------------------------------------------
	--Fin-Validaciones
	-------------------------------------------------------------------------------

	-- cargar en tabla la encomienda y sus rectificatorias
	INSERT INTO @enc_hist(id_encomienda)
	SELECT id_encomienda
	FROM Encomienda
	WHERE id_solicitud = @id_solicitud
	
	--Busco si alguna de las tareas anteriores genero el paquete
	--si es asi no hay que volver a generar el paquete y hay que relacionar los documentos subido en dicha etapa
	SELECT TOP 1
		@id_paquete = p.id_paquete
	FROM SGI_SADE_Procesos p
	INNER JOIN SGI_Tramites_Tareas tt on p.id_tramitetarea = tt.id_tramitetarea
	INNER JOIN SGI_Tramites_Tareas_HAB tt_HAB ON tt.id_tramitetarea = tt_HAB.id_tramitetarea
	WHERE tt_HAB.id_solicitud=@id_solicitud

	SET @id_paquete = ISNULL(@id_paquete, 0)
	
	IF @id_paquete = 0 BEGIN
		-----------------------------------------------------------------
		-- 1) Generar Paquete
		-----------------------------------------------------------------
		SET @id_proceso = dbo.Bus_id_proceso_EE('GEN_PAQUETE')
		SET @descripcion_tramite = ''
		
		-- Si no existe crea el proceso.
		IF NOT EXISTS(SELECT 'X' FROM SGI_SADE_Procesos 
					  WHERE id_proceso = @id_proceso AND id_tramitetarea = @id_tramitetarea)
		BEGIN
			EXEC @id_tarea_proc = Id_Nuevo 'SGI_SADE_Procesos'

			INSERT INTO SGI_SADE_Procesos(
				id_tarea_proc 
				,id_paquete
				,id_tramitetarea 
				,id_proceso 
				,id_origen_reg 
				,realizado_en_pasarela 
				,realizado_en_SADE
				,descripcion_tramite 
				,CreateUser 
				,CreateDate 
			)
			VALUES
			(
				@id_tarea_proc 
				,@id_paquete
				,@id_tramitetarea 
				,@id_proceso
				,NULL 
				,0 
				,0
				,@descripcion_tramite 
				,@userid
				,GETDATE()
			)
		END
	END
	-----------------------------------------------------------------
	-- 2) Generar Carátula
	-----------------------------------------------------------------
	SET @id_proceso = dbo.Bus_id_proceso_EE('GEN_CARATULA')
	SET @descripcion_tramite = 'Generación de la carátula'
	
	-- Si no existe crea el proceso.
	IF NOT EXISTS(SELECT 'X' FROM SGI_SADE_Procesos 
				  WHERE id_proceso = @id_proceso AND id_tramitetarea = @id_tramitetarea)
	BEGIN
		
		SET @parametros_SADE = '{"Usuario_SADE":"' + @UserName_SADE_Cal + '"}'
		
		EXEC @id_tarea_proc = Id_Nuevo 'SGI_SADE_Procesos'

		INSERT INTO SGI_SADE_Procesos(
			id_tarea_proc 
			,id_paquete
			,id_tramitetarea 
			,id_proceso 
			,id_origen_reg 
			,realizado_en_pasarela 
			,realizado_en_SADE
			,descripcion_tramite 
			,parametros_SADE
			,CreateUser 
			,CreateDate 
		)
		VALUES
		(
			@id_tarea_proc 
			,@id_paquete
			,@id_tramitetarea 
			,@id_proceso
			,NULL 
			,0 
			,0
			,@descripcion_tramite 
			,@parametros_SADE
			,@userid
			,GETDATE()
		)
	END
	
	-----------------------------------------------------------------
	-- 3) Obtener la carátula
	-----------------------------------------------------------------
	
	SET @id_proceso = dbo.Bus_id_proceso_EE('GET_CARATULA')
	SET @descripcion_tramite = 'Obtener la carátula'
	
	-- Si no existe crea el proceso.
	IF NOT EXISTS(SELECT 'X' FROM SGI_SADE_Procesos 
				  WHERE id_proceso = @id_proceso AND id_tramitetarea = @id_tramitetarea)
	BEGIN
		
		SET @parametros_SADE = '{"Usuario_SADE":"' + @UserName_SADE_Cal + '"}'

		EXEC @id_tarea_proc = Id_Nuevo 'SGI_SADE_Procesos'

		INSERT INTO SGI_SADE_Procesos(
			id_tarea_proc 
			,id_paquete
			,id_tramitetarea 
			,id_proceso 
			,id_origen_reg 
			,realizado_en_pasarela 
			,realizado_en_SADE
			,descripcion_tramite 
			,parametros_SADE
			,CreateUser 
			,CreateDate 
		)
		VALUES
		(
			@id_tarea_proc 
			,@id_paquete
			,@id_tramitetarea 
			,@id_proceso
			,NULL 
			,0 
			,0
			,@descripcion_tramite 
			,@parametros_SADE
			,@userid
			,GETDATE()
		)
	END
	
	-----------------------------------------------------------------
	-- 4) Proceso de subida de los documentos.
	--    Se suben todos los documentos asociados al trámite 
	-----------------------------------------------------------------
	
	----------------------------------------------------------
	-- Documentos de la Encomienda y SSIT
	----------------------------------------------------------
	SET @id_proceso = dbo.Bus_id_proceso_EE('SUBIR_DOCUMENTO')
	
	DECLARE curDocs CURSOR LOCAL FAST_FORWARD FOR
	SELECT		-- Documentos Encomienda
		doc.id_docadjunto,
		doc.id_file,
		tdocsis.cod_tipodocsis,
		tdocsis.nombre_tipodocsis,
		tdocreq.nombre_tdocreq,
		tdocreq.id_tdocreq
	FROM 
		Encomienda_DocumentosAdjuntos doc
		INNER JOIN TiposDeDocumentosRequeridos tdocreq ON doc.id_tdocreq = tdocreq.id_tdocreq
		INNER JOIN TiposDeDocumentosSistema tdocsis ON doc.id_tipodocsis = tdocsis.id_tipdocsis
	WHERE 
		tdocsis.cod_tipodocsis <> 'CARATULA_HABILITAION'
		AND doc.id_encomienda in ( SELECT id_encomienda FROM @enc_hist )
	UNION ALL
	SELECT		-- Documentos SSIT
		doc.id_docadjunto,
		doc.id_file,
		tdocsis.cod_tipodocsis,
		tdocsis.nombre_tipodocsis,
		tdocreq.nombre_tdocreq,
		tdocreq.id_tdocreq
	FROM 
		SSIT_DocumentosAdjuntos doc
		INNER JOIN TiposDeDocumentosRequeridos tdocreq ON doc.id_tdocreq = tdocreq.id_tdocreq
		INNER JOIN TiposDeDocumentosSistema tdocsis ON doc.id_tipodocsis = tdocsis.id_tipdocsis
	WHERE 
		doc.id_solicitud = @id_solicitud
		AND tdocsis.cod_tipodocsis <> 'CARATULA_HABILITAION'
	UNION ALL 
	SELECT		-- Certificados CAA
		doc.id_docadjunto,
		doc.id_file,
		tdocsis.cod_tipodocsis,
		tdocsis.nombre_tipodocsis,
		tdocreq.nombre_tdocreq,
		tdocreq.id_tdocreq
	FROM 
		CAA_DocumentosAdjuntos doc
		INNER JOIN CAA_Solicitudes solcaa ON doc.id_caa = solcaa.id_caa
		INNER JOIN TiposDeDocumentosRequeridos tdocreq ON doc.id_tdocreq = tdocreq.id_tdocreq
		INNER JOIN TiposDeDocumentosSistema tdocsis ON doc.id_tipodocsis = tdocsis.id_tipdocsis
	WHERE 
		doc.id_tipodocsis = 4		-- CERTIFICADO CAA
		AND solcaa.id_encomienda in ( SELECT id_encomienda FROM @enc_hist )

	OPEN curDocs
	FETCH NEXT FROM curDocs INTO
								@id_docadjunto 
								,@id_file 
								,@cod_tipodocsis 
								,@nombre_tipodocsis 
								,@nombre_tdocreq 
								,@id_tdocreq

	WHILE @@FETCH_STATUS = 0
	BEGIN
		SET @id_origen_reg = @id_docadjunto
		
		IF @id_tdocreq > 0
			SET @descripcion_tramite = @nombre_tdocreq + ' (id de archivo ' + CONVERT(nvarchar,@id_file) + ')'
		ELSE
			SET @descripcion_tramite = @nombre_tipodocsis + ' (id de archivo ' + CONVERT(nvarchar,@id_file) + ')'

		
		-- Si no existe crea el proceso.
		IF NOT EXISTS(SELECT 'X' FROM SGI_SADE_Procesos 
					  WHERE id_proceso = @id_proceso AND id_tramitetarea = @id_tramitetarea 
					  AND id_origen_reg = @id_origen_reg)
					  
		BEGIN

			SET @parametros_SADE = '{"Usuario_SADE":"' + @UserName_SADE_Cal + '"}'

			EXEC @id_tarea_proc = Id_Nuevo 'SGI_SADE_Procesos'

			INSERT INTO SGI_SADE_Procesos(
				id_tarea_proc 
				,id_paquete
				,id_tramitetarea 
				,id_proceso 
				,id_origen_reg 
				,id_file
				,realizado_en_pasarela 
				,realizado_en_SADE
				,descripcion_tramite 
				,parametros_SADE
				,CreateUser 
				,CreateDate 
			)
			VALUES
			(
				@id_tarea_proc 
				,@id_paquete
				,@id_tramitetarea 
				,@id_proceso
				,@id_origen_reg 
				,@id_file
				,0 
				,0
				,@descripcion_tramite 
				,@parametros_SADE
				,@userid
				,GETDATE()
			)
		END
		

		FETCH NEXT FROM curDocs INTO
									@id_docadjunto 
									,@id_file 
									,@cod_tipodocsis 
									,@nombre_tipodocsis 
									,@nombre_tdocreq 
									,@id_tdocreq
	END
	CLOSE curDocs
	DEALLOCATE curDocs
	
	-----------------------------------------------------------------------------
	-- Documentos provenientes de la tabla vieja de Certificados
	-----------------------------------------------------------------------------
	DECLARE curDocs CURSOR LOCAL FAST_FORWARD FOR
	SELECT 
		doc.id_certificado, 
		0,
		'ACTUACION_NOTARIAL' as cod_tipodocsis,
		'Actuación Notarial' as nombre_tipodocsis,
		'Actuación Notarial' as nombre_tdocreq,
		0 as id_tdocreq
	FROM 
		Certificados doc
		INNER JOIN wsEscribanos_ActaNotarial acta ON doc.TipoTramite = 3 AND NroTramite = acta.id_actanotarial
	WHERE 
		acta.id_encomienda in ( SELECT id_encomienda FROM @enc_hist )
	
	OPEN curDocs
	FETCH NEXT FROM curDocs INTO
								@id_docadjunto 
								,@id_file 
								,@cod_tipodocsis 
								,@nombre_tipodocsis 
								,@nombre_tdocreq 
								,@id_tdocreq

	WHILE @@FETCH_STATUS = 0
	BEGIN
		SET @id_origen_reg = @id_docadjunto
		
		IF @id_tdocreq > 0
			SET @descripcion_tramite = @nombre_tdocreq + ' (id de certificado ' + CONVERT(nvarchar,@id_docadjunto) + ')'
		ELSE
			SET @descripcion_tramite = @nombre_tipodocsis + ' (id de certificado ' + CONVERT(nvarchar,@id_docadjunto) + ')'

		
		-- Si no existe crea el proceso.
		IF NOT EXISTS(SELECT 'X' FROM SGI_SADE_Procesos 
					  WHERE id_proceso = @id_proceso AND id_tramitetarea = @id_tramitetarea 
					  AND id_origen_reg = @id_origen_reg)
					  
		BEGIN

			SELECT @acronimo_SADE  = valorchar_param FROM EE_parametros WHERE cod_param = 'EE.acronimo.minuta.notarial.hab'
			SET @parametros_SADE = '{"Usuario_SADE":"' + @UserName_SADE_Cal + '","Tabla_Origen":"Certificados","Acronimo_SADE":"' + @acronimo_SADE + '"}'

			EXEC @id_tarea_proc = Id_Nuevo 'SGI_SADE_Procesos'

			INSERT INTO SGI_SADE_Procesos(
				id_tarea_proc 
				,id_paquete
				,id_tramitetarea 
				,id_proceso 
				,id_origen_reg 
				,id_file
				,realizado_en_pasarela 
				,realizado_en_SADE
				,descripcion_tramite 
				,parametros_SADE
				,CreateUser 
				,CreateDate 
			)
			VALUES
			(
				@id_tarea_proc 
				,@id_paquete
				,@id_tramitetarea 
				,@id_proceso
				,@id_origen_reg 
				,0
				,0 
				,0
				,@descripcion_tramite 
				,@parametros_SADE
				,@userid
				,GETDATE()
			)
		END
		

		FETCH NEXT FROM curDocs INTO
									@id_docadjunto 
									,@id_file 
									,@cod_tipodocsis 
									,@nombre_tipodocsis 
									,@nombre_tdocreq 
									,@id_tdocreq
	END
	CLOSE curDocs
	DEALLOCATE curDocs

	------------------------------------------
	-- Documentos provenientes de la tabla SGI_Tarea_Documentos_Adjuntos
	------------------------------------------
	DECLARE curSGIDocs CURSOR LOCAL FAST_FORWARD FOR
		SELECT 		
			da.id_doc_adj, 
			da.id_file,
			tipo_doc.nombre_tdocreq
		FROM [dbo].[SGI_Tarea_Documentos_Adjuntos] da
		INNER JOIN SGI_Tramites_Tareas tt on da.id_tramitetarea = tt.id_tramitetarea
		INNER JOIN SGI_Tramites_Tareas_HAB tt_HAB ON tt.id_tramitetarea = tt_HAB.id_tramitetarea
		INNER JOIN TiposDeDocumentosRequeridos tipo_doc ON da.id_tdocreq = tipo_doc.id_tdocreq
		WHERE tt_HAB.id_solicitud=@id_solicitud
		AND NOT EXISTS(SELECT 1 FROM SGI_Tarea_Verificacion_AVH ta where ta.id_tramitetarea=tt.id_tramitetarea)
		order by 1 desc
	
	OPEN curSGIDocs
	FETCH NEXT FROM curSGIDocs INTO
								@id_docadjunto 
								,@id_file 
								,@nombre_tdocreq 

	WHILE @@FETCH_STATUS = 0
	BEGIN
		SET @id_origen_reg = @id_docadjunto
		
		SET @descripcion_tramite = @nombre_tdocreq + ' (id de docadjunto ' + CONVERT(nvarchar,@id_docadjunto) + ')'

		-- Si no existe crea el proceso.
		IF NOT EXISTS(SELECT 'X' FROM SGI_SADE_Procesos 
					  WHERE id_proceso = @id_proceso AND id_tramitetarea = @id_tramitetarea 
					  AND id_file = @id_file)
					  
		BEGIN

			SET @parametros_SADE = '{"Usuario_SADE":"' + @UserName_SADE_Cal + '"}'

			EXEC @id_tarea_proc = Id_Nuevo 'SGI_SADE_Procesos'

			INSERT INTO SGI_SADE_Procesos(
				id_tarea_proc 
				,id_paquete
				,id_tramitetarea 
				,id_proceso 
				,id_origen_reg 
				,id_file
				,realizado_en_pasarela 
				,realizado_en_SADE
				,descripcion_tramite 
				,parametros_SADE
				,CreateUser 
				,CreateDate 
			)
			VALUES
			(
				@id_tarea_proc 
				,@id_paquete
				,@id_tramitetarea 
				,@id_proceso
				,@id_origen_reg
				,@id_file
				,0 
				,0
				,@descripcion_tramite 
				,@parametros_SADE
				,@userid
				,GETDATE()
			)
		END
		

		FETCH NEXT FROM curSGIDocs INTO
								@id_docadjunto 
								,@id_file 
								,@nombre_tdocreq 
	END
	CLOSE curSGIDocs
	DEALLOCATE curSGIDocs
	
	------------------------------------------
	-- Planos 
	------------------------------------------
	SET @id_proceso = dbo.Bus_id_proceso_EE('SUBIR_PLANO')

	DECLARE @id_encomienda_plano int
	DECLARE curPlanos CURSOR FAST_FORWARD FOR
    SELECT 
		id_encomienda_plano,
		'Plano ' + Isnull(tplan.extension,'') + ' - Encomienda ' + convert(nvarchar,encplan.id_encomienda) + ' (Archivo Nº ' + convert(nvarchar,encplan.id_file),
		encplan.id_file
    FROM
		Encomienda_Planos encplan
		INNER JOIN TiposDePlanos tplan ON encplan.id_tipo_plano = tplan.id_tipo_plano
		INNER JOIN Files fil ON fil.id_file = encplan.id_file
	WHERE
		 encplan.id_encomienda in ( SELECT id_encomienda FROM @enc_hist )
    
    OPEN curPlanos
    FETCH NEXT FROM curPlanos INTO @id_encomienda_plano, @descripcion_tramite, @id_file
    
    WHILE @@FETCH_STATUS = 0
    BEGIN
		
		SET @id_origen_reg = @id_encomienda_plano
		SET @parametros_SADE = '{"Usuario_SADE":"' + @UserName_SADE_Cal + '"}'

		EXEC @id_tarea_proc = Id_Nuevo 'SGI_SADE_Procesos'

		INSERT INTO SGI_SADE_Procesos(
			id_tarea_proc 
			,id_paquete
			,id_tramitetarea 
			,id_proceso 
			,id_origen_reg 
			,id_file
			,realizado_en_pasarela 
			,realizado_en_SADE
			,descripcion_tramite 
			,parametros_SADE
			,CreateUser 
			,CreateDate 
		)
		VALUES
		(
			@id_tarea_proc 
			,@id_paquete
			,@id_tramitetarea 
			,@id_proceso
			,@id_origen_reg
			,@id_file 
			,0
			,0
			,@descripcion_tramite 
			,@parametros_SADE
			,@userid
			,GETDATE()
		)
			
			
		FETCH NEXT FROM curPlanos INTO @id_encomienda_plano, @descripcion_tramite, @id_file

	END
	

	
	/*
	------------------------------------------	
	-- 5 Subida Providencia Calificador a SubGerente
	------------------------------------------		
	SET @id_proceso = dbo.Bus_id_proceso_EE('SUBIR_PROVIDENCIA')
	SET @descripcion_tramite = 'Providencia Calificador a SubGerente' 
	
	-- Si no existe crea el proceso.
	IF NOT EXISTS(SELECT 'X' FROM SGI_SADE_Procesos 
				  WHERE id_proceso = @id_proceso AND id_tramitetarea = @id_tramitetarea
				  AND descripcion_tramite = @descripcion_tramite)
	BEGIN

		SET @parametros_SADE = '{"Usuario_SADE":"' + @UserName_SADE_Cal + '"}'

		EXEC @id_tarea_proc = Id_Nuevo 'SGI_SADE_Procesos'

		INSERT INTO SGI_SADE_Procesos(
			id_tarea_proc 
			,id_paquete
			,id_tramitetarea 
			,id_proceso 
			,id_origen_reg 
			,realizado_en_pasarela 
			,realizado_en_SADE
			,descripcion_tramite 
			,parametros_SADE
			,CreateUser 
			,CreateDate 
		)
		VALUES
		(
			@id_tarea_proc 
			,@id_paquete
			,@id_tramitetarea 
			,@id_proceso
			,0	-- Calificador_a_SubGerente 
			,0 
			,0
			,@descripcion_tramite 
			,@parametros_SADE
			,@userid
			,GETDATE()
		)
	END
	*/

	------------------------------------------	
	-- 7	Relacion de Documento
	------------------------------------------		
	SET @id_proceso = dbo.Bus_id_proceso_EE('RELACIONAR_DOCUMENTO')
	--Busco los documentos subidos en las tareas anterirores
	IF @id_paquete <> 0 BEGIN
		DECLARE cur_doc_a_relacionar CURSOR FAST_FORWARD FOR			
  			SELECT DISTINCT p.resultado_ee,p.id_devolucion_ee
			FROM SGI_SADE_Procesos p
			INNER JOIN SGI_Tramites_Tareas tt on p.id_tramitetarea = tt.id_tramitetarea
			INNER JOIN SGI_Tramites_Tareas_HAB tt_HAB ON tt.id_tramitetarea = tt_HAB.id_tramitetarea
			WHERE tt_HAB.id_solicitud=@id_solicitud and tt.id_tramitetarea < @id_tramitetarea
			AND p.id_proceso=3			

		DECLARE @resultado_ee nvarchar(500)
	
		OPEN cur_doc_a_relacionar	
		FETCH NEXT FROM cur_doc_a_relacionar INTO @resultado_ee, @nro_tramite
		WHILE @@FETCH_STATUS = 0
		BEGIN
			SET @descripcion_tramite = 'Relacionar documento ' + CONVERT(nvarchar, @resultado_ee)
			SET @parametros_SADE = '{"Usuario_SADE":"' + @UserName_SADE_Cal + '"}'
			EXEC @id_tarea_proc = Id_Nuevo 'SGI_SADE_Procesos'

			INSERT INTO SGI_SADE_Procesos(
				id_tarea_proc 
				,id_paquete
				,id_tramitetarea 
				,id_proceso 
				,id_origen_reg 
				,realizado_en_pasarela 
				,realizado_en_SADE
				,descripcion_tramite 
				,parametros_SADE
				,CreateUser 
				,CreateDate 
			)
			VALUES
			(
				@id_tarea_proc 
				,@id_paquete
				,@id_tramitetarea 
				,@id_proceso
				,@nro_tramite
				,0
				,0
				,@descripcion_tramite 
				,@parametros_SADE
				,@userid
				,GETDATE()
			)
				
			FETCH NEXT FROM cur_doc_a_relacionar INTO @resultado_ee, @nro_tramite
			
		END
				
		CLOSE cur_doc_a_relacionar	
		DEALLOCATE cur_doc_a_relacionar 
	END	

	-----------------------------------------------------------------
	-- 6) Proceso de pase
	-----------------------------------------------------------------
	-- Si hubo Revision de SubGerente
	IF @hay_tarea_SG = 1
	BEGIN
		
		SET @id_proceso = dbo.Bus_id_proceso_EE('GEN_PASE')
		SET @descripcion_tramite = 'Pase a SubGerente'
		
		-- Si no existe crea el proceso.
		IF NOT EXISTS(SELECT 'X' FROM SGI_SADE_Procesos 
					  WHERE id_proceso = @id_proceso AND id_tramitetarea = @id_tramitetarea
					  AND descripcion_tramite = @descripcion_tramite)
		BEGIN
		
			SET @parametros_SADE = '{"Usuario":"' + @UserName_SADE_Cal + '"}'
		
			EXEC @id_tarea_proc = Id_Nuevo 'SGI_SADE_Procesos'

			INSERT INTO SGI_SADE_Procesos(
				id_tarea_proc 
				,id_paquete
				,id_tramitetarea 
				,id_proceso 
				,id_origen_reg 
				,realizado_en_pasarela 
				,realizado_en_SADE
				,descripcion_tramite 
				,parametros_SADE
				,CreateUser 
				,CreateDate 
			)
			VALUES
			(
				@id_tarea_proc
				,@id_paquete
				,@id_tramitetarea 
				,@id_proceso
				,NULL 
				,0 
				,0
				,@descripcion_tramite 
				,@parametros_SADE
				,@userid
				,GETDATE()
			)
		END
	END
	
	------------------------------------------	
	-- 7 Subida Providencia a Gerente
	------------------------------------------		
	IF @hay_tarea_GE = 1
	BEGIN
		
		SET @id_proceso = dbo.Bus_id_proceso_EE('SUBIR_PROVIDENCIA')
		SET @descripcion_tramite = 'Providencia a Gerente' 
		
		-- Si no existe crea el proceso.
		IF NOT EXISTS(SELECT 'X' FROM SGI_SADE_Procesos 
					  WHERE id_proceso = @id_proceso AND id_tramitetarea = @id_tramitetarea
					  AND descripcion_tramite = @descripcion_tramite)
		BEGIN
			
			-- Si no hay tarea de subGerente quiere decir que viene del Calificador
			IF @hay_tarea_SG = 1
				SET @parametros_SADE = '{"Usuario_SADE":"' + @UserName_SADE_SG + '"}'
			ELSE
				SET @parametros_SADE = '{"Usuario_SADE":"' + @UserName_SADE_SG + '"}'
			
			

			EXEC @id_tarea_proc = Id_Nuevo 'SGI_SADE_Procesos'

			INSERT INTO SGI_SADE_Procesos(
				id_tarea_proc 
				,id_paquete
				,id_tramitetarea 
				,id_proceso 
				,id_origen_reg 
				,realizado_en_pasarela 
				,realizado_en_SADE
				,descripcion_tramite 
				,parametros_SADE
				,CreateUser 
				,CreateDate 
			)
			VALUES
			(
				@id_tarea_proc 
				,@id_paquete
				,@id_tramitetarea 
				,@id_proceso
				,1	-- Providencia de SubGerente 
				,0 
				,0
				,@descripcion_tramite 
				,@parametros_SADE
				,@userid
				,GETDATE()
			)
		END
	END
	-----------------------------------------------------------------
	-- 8) Proceso de pase
	-----------------------------------------------------------------
	
	IF @hay_tarea_GE = 1
	BEGIN
		
		SET @id_proceso = dbo.Bus_id_proceso_EE('GEN_PASE')
		SET @descripcion_tramite = 'Pase a Gerente'
		
		-- Si no existe crea el proceso.
		IF NOT EXISTS(SELECT 'X' FROM SGI_SADE_Procesos 
					  WHERE id_proceso = @id_proceso AND id_tramitetarea = @id_tramitetarea
					  AND descripcion_tramite = @descripcion_tramite)
		BEGIN
		
			-- Si no hay tarea de subGerente quiere decir que viene del Calificador
			IF @hay_tarea_SG = 1
				SET @parametros_SADE = '{"Usuario":"' + @UserName_SADE_SG + '"}'
			ELSE
				SET @parametros_SADE = '{"Usuario":"' + @UserName_SADE_Cal + '"}'
			
			
			EXEC @id_tarea_proc = Id_Nuevo 'SGI_SADE_Procesos'

			INSERT INTO SGI_SADE_Procesos(
				id_tarea_proc 
				,id_paquete
				,id_tramitetarea 
				,id_proceso 
				,id_origen_reg 
				,realizado_en_pasarela 
				,realizado_en_SADE
				,descripcion_tramite 
				,parametros_SADE
				,CreateUser 
				,CreateDate 
			)
			VALUES
			(
				@id_tarea_proc
				,@id_paquete
				,@id_tramitetarea 
				,@id_proceso
				,NULL 
				,0 
				,0
				,@descripcion_tramite 
				,@parametros_SADE
				,@userid
				,GETDATE()
			)
		END
	END 
	------------------------------------------	
	-- 9 Subida Providencia Gerente a Dictamen
	------------------------------------------		
	
	SET @id_proceso = dbo.Bus_id_proceso_EE('SUBIR_PROVIDENCIA')
	SET @descripcion_tramite = 'Providencia Gerente a DGHyP' 
	
	-- Si no existe crea el proceso.
	IF NOT EXISTS(SELECT 'X' FROM SGI_SADE_Procesos 
				  WHERE id_proceso = @id_proceso AND id_tramitetarea = @id_tramitetarea
				  AND descripcion_tramite = @descripcion_tramite)
	BEGIN
	
		-- Si no hay tarea de Gerente quiere decir que viene del SubGerente
		IF @hay_tarea_GE = 1
			SET @parametros_SADE = '{"Usuario_SADE":"' + @UserName_SADE_GE + '"}'
		ELSE
			SET @parametros_SADE = '{"Usuario_SADE":"' + @UserName_SADE_SG + '"}'

		EXEC @id_tarea_proc = Id_Nuevo 'SGI_SADE_Procesos'

		INSERT INTO SGI_SADE_Procesos(
			id_tarea_proc 
			,id_paquete
			,id_tramitetarea 
			,id_proceso 
			,id_origen_reg 
			,realizado_en_pasarela 
			,realizado_en_SADE
			,descripcion_tramite 
			,parametros_SADE
			,CreateUser 
			,CreateDate 
		)
		VALUES
		(
			@id_tarea_proc 
			,@id_paquete
			,@id_tramitetarea 
			,@id_proceso
			,2	-- Providencia de Gerente 
			,0 
			,0
			,@descripcion_tramite 
			,@parametros_SADE
			,@userid
			,GETDATE()
		)
	END

	------------------------------------------------------------------
	-- 10 Proceso de Generación de la tarea de firma de la disposición
	-------------------------------------------------------------------
	SET @id_proceso = dbo.Bus_id_proceso_EE('GEN_TAREA_A_LA_FIRMA')
	SET @descripcion_tramite =  CONVERT(nvarchar,@id_solicitud) + ' (Nro. Expediente)'
	
	-- Si no existe crea el proceso.
	IF NOT EXISTS(SELECT 'X' FROM SGI_SADE_Procesos 
				  WHERE id_proceso = @id_proceso AND id_tramitetarea = @id_tramitetarea)
	BEGIN

		IF @hay_tarea_GE = 1
			SET @parametros_SADE = '{"Usuario_SADE":"' + @UserName_SADE_GE + '"}'
		ELSE
			SET @parametros_SADE = '{"Usuario_SADE":"' + @UserName_SADE_SG + '"}'


		EXEC @id_tarea_proc = Id_Nuevo 'SGI_SADE_Procesos'

		INSERT INTO SGI_SADE_Procesos(
			id_tarea_proc 
			,id_paquete
			,id_tramitetarea 
			,id_proceso 
			,id_origen_reg 
			,realizado_en_pasarela 
			,realizado_en_SADE
			,descripcion_tramite 
			,parametros_SADE
			,CreateUser 
			,CreateDate 
		)
		VALUES
		(
			@id_tarea_proc 
			,@id_paquete
			,@id_tramitetarea 
			,@id_proceso
			,NULL 
			,0 
			,0
			,@descripcion_tramite 
			,@parametros_SADE
			,@userid
			,GETDATE()
		)
	END
	
	-----------------------------------------------------------------
	-- 12 Proceso de pase
	-----------------------------------------------------------------
	SET @id_proceso = dbo.Bus_id_proceso_EE('GEN_PASE')
	SET @descripcion_tramite = 'Pase a DGHyP'
	
	-- Si no existe crea el proceso.
	IF NOT EXISTS(SELECT 'X' FROM SGI_SADE_Procesos 
				  WHERE id_proceso = @id_proceso AND id_tramitetarea = @id_tramitetarea
				  AND descripcion_tramite = @descripcion_tramite)
	BEGIN
	
		SET @parametros_SADE = '{"Usuario":"' + dbo.Parametro_char('SGI.Username.Receptor.Habilitaciones') + '"}'
	
		EXEC @id_tarea_proc = Id_Nuevo 'SGI_SADE_Procesos'

		INSERT INTO SGI_SADE_Procesos(
			id_tarea_proc 
			,id_paquete
			,id_tramitetarea 
			,id_proceso 
			,id_origen_reg 
			,realizado_en_pasarela 
			,realizado_en_SADE
			,descripcion_tramite 
			,parametros_SADE
			,CreateUser 
			,CreateDate 
		)
		VALUES
		(
			@id_tarea_proc
			,@id_paquete
			,@id_tramitetarea 
			,@id_proceso
			,NULL 
			,0 
			,0
			,@descripcion_tramite 
			,@parametros_SADE
			,@userid
			,GETDATE()
		)
	END
END
GO
/****** Object:  StoredProcedure [dbo].[SGI_Cargar_Procesos_Expedientes]    Script Date: 10/28/2016 14:35:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SGI_Cargar_Procesos_Expedientes]  (   
	@id_tramitetarea			int
    ,@userId				uniqueidentifier   
 )  
AS  
BEGIN

	DECLARE @id_solicitud			int
	
	---------	
	DECLARE @id_caa			int
	DECLARE @id_acta		int
	DECLARE @nroTramite		int
	DECLARE @item			int
	
	---------
	DECLARE @id_paquete				int
	DECLARE @id_caratula			int
	DECLARE @id_proceso				int 
	DECLARE @id_devolucion_ee		int 
    DECLARE @nro_tramite			int
    DECLARE @id_file				int
    DECLARE @descripcion_tramite	nvarchar(200) 
    DECLARE @id_tipoexpediente		int
    DECLARE @id_subtipoexpediente	int
		,@UserName_SADE_Cal nvarchar(20)
		,@UserName_SADE_SG nvarchar(20)
		,@UserName_SADE_GE nvarchar(20)
		,@parametros_SADE nvarchar(200)
		,@msgError nvarchar(100)
		,@nombre_tipodocsis nvarchar(100)
		,@id_generar_expediente_proc int 
		,@id_tarea_proc int
		,@id_origen_reg  int
		,@cod_tipodocsis int
		     
    DECLARE @enc_hist TABLE
    (
		id_encomienda	int
    )
    
	DECLARE @cod_circuito	nvarchar(20)
	DECLARE @cod_circuito_scp	nvarchar(20)
	DECLARE @cant_registros		int
	
	SET @cod_circuito_scp = 'SCP'
	SET @cant_registros = 0
	
	
    SET @id_caratula = 0
    
	SELECT TOP 1
		@id_paquete = id_paquete
	FROM SGI_Tarea_Generar_Expediente_Procesos
	WHERE id_tramitetarea = @id_tramitetarea
	ORDER BY id_generar_expediente_proc DESC
	
	IF @@ROWCOUNT > 0 
	BEGIN
		RETURN 0 --salgo, los registros ya fueron generados
	END
	
	
	SELECT TOP 1
		@id_solicitud = tt_HAB.id_solicitud,
		@cod_circuito = cod_circuito,
		@id_tipoexpediente = id_tipoexpediente,
		@id_subtipoexpediente = id_subtipoexpediente
	FROM SGI_Tramites_Tareas tt
	INNER JOIN SGI_Tramites_Tareas_HAB tt_HAB ON tt.id_tramitetarea = tt_HAB.id_tramitetarea
	INNER JOIN SSIT_Solicitudes sol ON sol.id_solicitud = tt_HAB.id_solicitud
	INNER JOIN ENG_Tareas t ON tt.id_tarea = t.id_tarea
	INNER JOIN ENG_Circuitos c ON t.id_circuito = c.id_circuito 
	WHERE tt.id_tramitetarea = @id_tramitetarea

	SET @id_paquete = 0 --con cero se identifica que aun no se proceso paquete
	SET @id_devolucion_ee = -1 --con -1 e identifica que aun no se proceso item
	
	--si es especiales hay que ver si se genero el paquete en la tarea de verificacion AVH
	--si es asi no hay que volver a generar el paquete y hay que relacionar los documentos
	--subido en dicha etapa
	IF @id_tipoexpediente=2 --ESPECIAL
		AND @id_subtipoexpediente=3 -- INSPECCION_PREVIA
	BEGIN
		SELECT TOP 1
			@id_paquete = p.id_paquete
		FROM SGI_SADE_Procesos p
		INNER JOIN SGI_Tramites_Tareas tt on p.id_tramitetarea = tt.id_tramitetarea
		INNER JOIN SGI_Tramites_Tareas_HAB tt_HAB ON tt.id_tramitetarea = tt_HAB.id_tramitetarea
		INNER JOIN SGI_Tarea_Verificacion_AVH tt_AVH on tt.id_tramitetarea = tt_AVH.id_tramitetarea
		WHERE tt_HAB.id_solicitud=@id_solicitud
		
	END
		
	------------------------------------------------
	-- Validar que el calificador tenga usuario SADE
	------------------------------------------------
  	SELECT TOP 1 @UserName_SADE_Cal = pro.UserName_SADE
	FROM 
		SGI_Tramites_Tareas tt
		INNER JOIN SGI_Tramites_Tareas_HAB tt_hab ON tt.id_tramitetarea = tt_hab.id_tramitetarea
		INNER JOIN SGI_Tarea_Calificar tt_cal ON tt.id_tramitetarea = tt_cal.id_tramitetarea
		INNER JOIN SGI_Profiles pro ON tt.UsuarioAsignado_tramitetarea=pro.userid
	WHERE tt_hab.id_solicitud = @id_solicitud
	ORDER BY tt.id_tramitetarea DESC
	
	IF LEN(ISNULL(@UserName_SADE_Cal,''))=0
	BEGIN
		SET @msgError = 'El usuario responsable de la tarea de calificación no tiene usuario en SADE.'
		RAISERROR(@msgError,16,1)
		RETURN
	END
	-----------------------------------------------
	-- Validar que el subgerente tenga usuario SADE
	-----------------------------------------------
  	SELECT TOP 1 @UserName_SADE_SG = pro.UserName_SADE
	FROM 
		SGI_Tramites_Tareas tt
		INNER JOIN SGI_Tramites_Tareas_HAB tt_hab ON tt.id_tramitetarea = tt_hab.id_tramitetarea
		INNER JOIN SGI_Tarea_Revision_SubGerente tt_sg ON tt.id_tramitetarea = tt_sg.id_tramitetarea
		INNER JOIN SGI_Profiles pro ON tt.UsuarioAsignado_tramitetarea=pro.userid
	WHERE tt_hab.id_solicitud = @id_solicitud
	ORDER BY tt.id_tramitetarea DESC
	
	IF LEN(ISNULL(@UserName_SADE_SG,''))=0
	BEGIN
		SET @msgError = 'El usuario responsable de la tarea Revisión Subgerente no tiene usuario en SADE.'
		RAISERROR(@msgError,16,1)
		RETURN
	END	
	
	----------------------------------------------
	-- Validar que el gerente tenga usuario SADE
	----------------------------------------------
  	SELECT TOP 1 @UserName_SADE_GE = pro.UserName_SADE
	FROM 
		SGI_Tramites_Tareas tt
		INNER JOIN SGI_Tramites_Tareas_HAB tt_hab ON tt.id_tramitetarea = tt_hab.id_tramitetarea
		INNER JOIN SGI_Tarea_Revision_Gerente tt_ge ON tt.id_tramitetarea = tt_ge.id_tramitetarea
		INNER JOIN SGI_Profiles pro ON tt.UsuarioAsignado_tramitetarea=pro.userid
	WHERE tt_hab.id_solicitud = @id_solicitud
	ORDER BY tt.id_tramitetarea DESC
	
	IF LEN(ISNULL(@UserName_SADE_GE,''))=0
	BEGIN
		SET @msgError = 'El usuario responsable de la tarea Revisión Gerente no tiene usuario en SADE.'
		RAISERROR(@msgError,16,1)
		RETURN
	END		

	IF @id_paquete = 0 BEGIN
	
	------------------------------------------	
	-- 2 Generacion de la Paquete
	------------------------------------------	

		SET @id_proceso = 1
		SET @nro_tramite = null
		SET @descripcion_tramite = 'Paquete'
			
		EXEC SGI_Tarea_Generar_Expediente_Procesos_insert
			@id_tramitetarea
			,@id_paquete
			,@id_caratula
			,@id_proceso
			,@id_devolucion_ee
			,@descripcion_tramite
			,0
			,@nro_tramite
			,@descripcion_tramite
			,@userId
	END
		
	------------------------------------------	
	-- 2 Generacion de Caratula
	------------------------------------------	
		
	SET @id_proceso = 2
	SET @nro_tramite = null
	SET @descripcion_tramite = ''
	
	SET @parametros_SADE = '{"Usuario_SADE":"' + @UserName_SADE_Cal + '"}'
	
	EXEC @id_generar_expediente_proc = Id_Nuevo 'SGI_Tarea_Generar_Expediente_Procesos'  
	
    INSERT INTO SGI_Tarea_Generar_Expediente_Procesos(   
        id_generar_expediente_proc    
        ,id_paquete    
        ,id_caratula
        ,id_proceso    
        ,id_devolucion_ee    
        ,resultado_ee    
        ,realizado   
        ,nro_tramite        
        ,descripcion_tramite
        ,CreateUser    
        ,CreateDate
        ,id_tramitetarea
        ,parametros_SADE)  
    VALUES(   
        @id_generar_expediente_proc    
        ,@id_paquete  
        ,@id_caratula  
        ,@id_proceso    
        ,@id_devolucion_ee    
        ,''    
        ,0
        ,@nro_tramite        
        ,@descripcion_tramite        
        ,@userId    
        ,GETDATE()
        ,@id_tramitetarea
        ,@parametros_SADE)  
	

	------------------------------------------	
	-- 11 Obtener carátula de SADE
	------------------------------------------	
	SET @id_proceso = 11
	SET @nro_tramite = null
	SET @descripcion_tramite = ''
	
	EXEC @id_generar_expediente_proc = Id_Nuevo 'SGI_Tarea_Generar_Expediente_Procesos'  
	
    INSERT INTO SGI_Tarea_Generar_Expediente_Procesos(   
        id_generar_expediente_proc    
        ,id_paquete    
        ,id_caratula
        ,id_proceso    
        ,id_devolucion_ee    
        ,resultado_ee    
        ,realizado   
        ,nro_tramite        
        ,descripcion_tramite
        ,CreateUser    
        ,CreateDate
        ,id_tramitetarea)  
    VALUES(   
        @id_generar_expediente_proc    
         ,@id_paquete  
        ,@id_caratula  
        ,@id_proceso    
        ,@id_devolucion_ee    
        ,''    
        ,0
        ,@nro_tramite        
        ,@descripcion_tramite        
        ,@userId    
        ,GETDATE()
        ,@id_tramitetarea)  
		
	------------------------------------------	
	-- 7	Relacion de Documento
	------------------------------------------		
	SET @id_proceso = 7
	IF @id_paquete <> 0 BEGIN
		DECLARE cur_doc_a_relacionar CURSOR FAST_FORWARD FOR			
  			SELECT DISTINCT p.resultado_ee,p.id_devolucion_ee
			FROM SGI_SADE_Procesos p
			INNER JOIN SGI_Tramites_Tareas tt on p.id_tramitetarea = tt.id_tramitetarea
			INNER JOIN SGI_Tramites_Tareas_HAB tt_HAB ON tt.id_tramitetarea = tt_HAB.id_tramitetarea
			INNER JOIN SGI_Tarea_Verificacion_AVH tt_AVH on tt.id_tramitetarea = tt_HAB.id_tramitetarea
			WHERE tt_HAB.id_solicitud=@id_solicitud
			AND p.id_proceso=3			

		DECLARE @resultado_ee nvarchar(500)
	
		OPEN cur_doc_a_relacionar	
		FETCH NEXT FROM cur_doc_a_relacionar INTO @resultado_ee, @nro_tramite
		WHILE @@FETCH_STATUS = 0
		BEGIN
		
			SET @descripcion_tramite = 'Relacionar documento ' + CONVERT(nvarchar, @resultado_ee)
							
			EXEC @id_generar_expediente_proc = Id_Nuevo 'SGI_Tarea_Generar_Expediente_Procesos'  
	
			INSERT INTO SGI_Tarea_Generar_Expediente_Procesos(   
				id_generar_expediente_proc    
				,id_paquete    
				,id_caratula
				,id_proceso    
				,id_devolucion_ee    
				,resultado_ee    
				,realizado   
				,nro_tramite        
				,descripcion_tramite
				,CreateUser    
				,CreateDate
				,id_tramitetarea)  
			VALUES(   
				@id_generar_expediente_proc    
				,@id_paquete  
				,@id_caratula  
				,@id_proceso    
				,@id_devolucion_ee    
				,''    
				,0
				,@nro_tramite        
				,@descripcion_tramite        
				,@userId    
				,GETDATE()
				,@id_tramitetarea)  
				
			FETCH NEXT FROM cur_doc_a_relacionar INTO @resultado_ee, @nro_tramite
			
		END
				
		CLOSE cur_doc_a_relacionar	
		DEALLOCATE cur_doc_a_relacionar 
	END	
	------------------------------------------	
	-- 3	Subida de Documento
	------------------------------------------		
	
	-- cargar en tabla la encomienda y sus rectificatorias
	INSERT INTO @enc_hist(id_encomienda)
	SELECT id_encomienda
	FROM SSIT_Solicitudes_Encomienda
	WHERE id_solicitud = @id_solicitud
	
	SET @id_proceso = 3

    ------------------------------------------
	--documento encomienda
	------------------------------------------
			
	DECLARE cur_cargar_cert_enco CURSOR FAST_FORWARD FOR			
		SELECT id_file, id_encomienda
		FROM Encomienda_DocumentosAdjuntos 
		WHERE id_tipodocsis = 2 -- Encomienda Digital
		AND id_encomienda in ( SELECT id_encomienda FROM @enc_hist )
		
	OPEN cur_cargar_cert_enco	
		
	FETCH NEXT FROM cur_cargar_cert_enco INTO @id_file, @nro_tramite

            
	WHILE @@FETCH_STATUS = 0
	BEGIN
	
		SET @descripcion_tramite = 'Encomienda de Habilitaciones Nro. ' + CONVERT(nvarchar, @nro_tramite)
						
		SET @parametros_SADE = '{"Usuario_SADE":"' + @UserName_SADE_Cal + '"}'
		
		EXEC @id_generar_expediente_proc = Id_Nuevo 'SGI_Tarea_Generar_Expediente_Procesos'  
		
		INSERT INTO SGI_Tarea_Generar_Expediente_Procesos(   
			id_generar_expediente_proc    
			,id_paquete    
			,id_caratula
			,id_proceso    
			,id_devolucion_ee    
			,resultado_ee    
			,realizado   
			,nro_tramite        
			,descripcion_tramite
			,CreateUser    
			,CreateDate
			,id_tramitetarea
			,parametros_SADE)  
		VALUES(   
			@id_generar_expediente_proc    
			,@id_paquete  
			,@id_caratula  
			,@id_proceso    
			,@id_devolucion_ee    
			,''    
			,0
			,@id_file        
			,@descripcion_tramite        
			,@userId    
			,GETDATE()
			,@id_tramitetarea
			,@parametros_SADE)  			

		FETCH NEXT FROM cur_cargar_cert_enco INTO @id_file, @nro_tramite
	END
			
	CLOSE cur_cargar_cert_enco	
	DEALLOCATE cur_cargar_cert_enco 
			
			
    ------------------------------------------
	-- documentos solicitud ssit
    ------------------------------------------
    	
	SET @nro_tramite = @id_solicitud
	SET @descripcion_tramite = 'Solicitud de Habilitación Nro. ' + CONVERT(nvarchar, @id_solicitud)

	SET @parametros_SADE = '{"Usuario_SADE":"' + @UserName_SADE_Cal + '"}'
	
	EXEC @id_generar_expediente_proc = Id_Nuevo 'SGI_Tarea_Generar_Expediente_Procesos'  
	
	INSERT INTO SGI_Tarea_Generar_Expediente_Procesos(   
		id_generar_expediente_proc    
		,id_paquete    
		,id_caratula
		,id_proceso    
		,id_devolucion_ee    
		,resultado_ee    
		,realizado   
		,nro_tramite        
		,descripcion_tramite
		,CreateUser    
		,CreateDate
		,id_tramitetarea
		,parametros_SADE)  
	VALUES(   
		@id_generar_expediente_proc    
		,@id_paquete  
		,@id_caratula  
		,@id_proceso    
		,@id_devolucion_ee    
		,''    
		,0
		,@nro_tramite        
		,@descripcion_tramite        
		,@userId    
		,GETDATE()
		,@id_tramitetarea
		,@parametros_SADE)  		
	
	------------------------------------------	
	--Documentos impacto ambiental - apra -caa
	------------------------------------------	

	-- primero buscar si tiene reverso, tiene reverso tiene dos certificados
	DECLARE @cant_certificados	int;

	DECLARE cur_cargar_impacto CURSOR FAST_FORWARD FOR
		SELECT id_caa
		FROM CAA_Solicitudes
		WHERE id_encomienda in ( SELECT id_encomienda FROM @enc_hist )
		AND id_estado = 5
		ORDER BY id_caa 
		
	
	OPEN cur_cargar_impacto	
		
	FETCH NEXT FROM cur_cargar_impacto INTO @id_caa
		
	WHILE @@FETCH_STATUS = 0
	BEGIN
		
		SELECT @cant_certificados = COUNT(*)
		FROM CAA_DocumentosAdjuntos 
		WHERE id_tipodocsis = 4   -- Certificado APRA
		AND id_caa = @id_caa

			
		DECLARE cur_cargar_caa CURSOR FAST_FORWARD FOR
			SELECT id_file, ROW_NUMBER() OVER(ORDER BY id_docadjunto) as item
			FROM CAA_DocumentosAdjuntos 
			WHERE id_tipodocsis = 4   -- Certificado APRA
			AND id_caa = @id_caa

		OPEN cur_cargar_caa	
			
		FETCH NEXT FROM cur_cargar_caa INTO @nro_tramite, @item
	            
		WHILE @@FETCH_STATUS = 0
		BEGIN
			SET @descripcion_tramite = 'Certificado de Aptitud Ambiental Nro. ' + CONVERT(nvarchar, @id_caa)
				
			IF @cant_certificados > 0 
			BEGIN
				IF @item = 1
					SET @descripcion_tramite = @descripcion_tramite + ' (Anverso)'
				ELSE
					SET @descripcion_tramite = @descripcion_tramite + ' (Reverso)'
			END
			
			SET @parametros_SADE = '{"Usuario_SADE":"' + @UserName_SADE_Cal + '"}'
			
			EXEC @id_generar_expediente_proc = Id_Nuevo 'SGI_Tarea_Generar_Expediente_Procesos'  
			
			INSERT INTO SGI_Tarea_Generar_Expediente_Procesos(   
				id_generar_expediente_proc    
				,id_paquete    
				,id_caratula
				,id_proceso    
				,id_devolucion_ee    
				,resultado_ee    
				,realizado   
				,nro_tramite        
				,descripcion_tramite
				,CreateUser    
				,CreateDate
				,id_tramitetarea
				,parametros_SADE)  
			VALUES(   
				@id_generar_expediente_proc    
				,@id_paquete  
				,@id_caratula  
				,@id_proceso    
				,@id_devolucion_ee    
				,''    
				,0
				,@nro_tramite        
				,@descripcion_tramite        
				,@userId    
				,GETDATE()
				,@id_tramitetarea
				,@parametros_SADE)  	        
			FETCH NEXT FROM cur_cargar_caa INTO @nro_tramite, @item
			
		END
				
		CLOSE cur_cargar_caa	
		DEALLOCATE cur_cargar_caa 
				
		FETCH NEXT FROM cur_cargar_impacto INTO @id_caa
		
	END
			
	CLOSE cur_cargar_impacto	
	DEALLOCATE cur_cargar_impacto 
	
	------------------------------------------	
	--Documentos adjuntos
	------------------------------------------	
	DECLARE @id_docadjunto			int
	DECLARE @tdocreq_detalle		nvarchar(200)
	DECLARE @createDate				datetime
	DECLARE @nombre_tdocreq			nvarchar(200)

		
	DECLARE cur_cargar_adj CURSOR FAST_FORWARD FOR
		SELECT 
			doc.id_docadjunto, 
			doc.tdocreq_detalle, 
			doc.createDate, 
			tipo_doc.nombre_tdocreq
		FROM DocumentosAdjuntos doc
		INNER JOIN TiposDeDocumentosRequeridos tipo_doc ON doc.id_tdocreq = tipo_doc.id_tdocreq
		WHERE doc.id_solicitud = @id_solicitud
		
	OPEN cur_cargar_adj	
		
	FETCH NEXT FROM cur_cargar_adj INTO 
		@id_docadjunto, 
		@tdocreq_detalle, 
		@createDate, 
		@nombre_tdocreq
            
	WHILE @@FETCH_STATUS = 0
	BEGIN
		
		SET @nro_tramite = @id_docadjunto
        SET @descripcion_tramite = 'Documentos Adjuntos' + ' id_docadjunto: ' + CONVERT(nvarchar, @nro_tramite)
					
		SET @parametros_SADE = '{"Usuario_SADE":"' + @UserName_SADE_Cal + '"}'
	
		EXEC @id_generar_expediente_proc = Id_Nuevo 'SGI_Tarea_Generar_Expediente_Procesos'  
	
		INSERT INTO SGI_Tarea_Generar_Expediente_Procesos(   
			id_generar_expediente_proc    
			,id_paquete    
			,id_caratula
			,id_proceso    
			,id_devolucion_ee    
			,resultado_ee    
			,realizado   
			,nro_tramite        
			,descripcion_tramite
			,CreateUser    
			,CreateDate
			,id_tramitetarea
			,parametros_SADE)  
		VALUES(   
			@id_generar_expediente_proc    
			,@id_paquete  
			,@id_caratula  
			,@id_proceso    
			,@id_devolucion_ee    
			,''    
			,0
			,@nro_tramite        
			,@descripcion_tramite        
			,@userId    
			,GETDATE()
			,@id_tramitetarea
			,@parametros_SADE)  		
	        
		FETCH NEXT FROM cur_cargar_adj INTO 
			@id_docadjunto, 
			@tdocreq_detalle, 
			@createDate, 
			@nombre_tdocreq
		
	END
			
	CLOSE cur_cargar_adj	
	DEALLOCATE cur_cargar_adj 
			
	
	--Documentos minuta notarial 
	DECLARE cur_cargar_acta CURSOR FAST_FORWARD FOR
		SELECT id_actanotarial			
		FROM wsEscribanos_ActaNotarial
		WHERE id_encomienda in ( SELECT id_encomienda FROM @enc_hist )
		AND anulada = 0
		ORDER BY id_actanotarial
		
	OPEN cur_cargar_acta	
	
	FETCH NEXT FROM cur_cargar_acta INTO @id_acta
	
	WHILE @@FETCH_STATUS = 0
	BEGIN
	
		SET @id_acta = ISNULL(@id_acta, 0)
		
		IF @id_acta > 0 
		BEGIN
		
			SELECT 
				@nro_tramite = id_certificado
			FROM Certificados 
			WHERE TipoTramite = 3   -- acta notarial
			AND nroTramite = @id_acta
				
			IF @@ROWCOUNT > 0 
			BEGIN
				SET @descripcion_tramite = 'Minuta Notarial Nro. ' + CONVERT(nvarchar, @id_acta)
					
			SET @parametros_SADE = '{"Usuario_SADE":"' + @UserName_SADE_Cal + '"}'
			
			EXEC @id_generar_expediente_proc = Id_Nuevo 'SGI_Tarea_Generar_Expediente_Procesos'  
			
			INSERT INTO SGI_Tarea_Generar_Expediente_Procesos(   
				id_generar_expediente_proc    
				,id_paquete    
				,id_caratula
				,id_proceso    
				,id_devolucion_ee    
				,resultado_ee    
				,realizado   
				,nro_tramite        
				,descripcion_tramite
				,CreateUser    
				,CreateDate
				,id_tramitetarea
				,parametros_SADE)  
			VALUES(   
				@id_generar_expediente_proc    
				,@id_paquete  
				,@id_caratula  
				,@id_proceso    
				,@id_devolucion_ee    
				,''    
				,0
				,@nro_tramite        
				,@descripcion_tramite        
				,@userId    
				,GETDATE()
				,@id_tramitetarea
				,@parametros_SADE)  
			END
							
		END

		FETCH NEXT FROM cur_cargar_acta INTO @id_acta
		
	END
			
	CLOSE cur_cargar_acta	
	DEALLOCATE cur_cargar_acta 

	------------------------------------------	
	--Documentos adjuntos Desde el SGI
	------------------------------------------	
	
	DECLARE @sgi_id_doc_adj			int
			,@sgi_id_tdoc_adj		int
			,@sgi_tdoc_adj_detalle	nvarchar(100)
			,@sgi_createDate		datetime
			,@sgi_nombre_tdoc_adj	nvarchar(100)
		
	DECLARE cur_cargar_sgi_adj CURSOR FAST_FORWARD FOR
		SELECT 
			doc.id_doc_adj, 
			doc.id_tdocreq, 
			tdoc_adj_detalle,
			doc.createDate,
			tipo_doc.nombre_tdocreq
		FROM SGI_Tarea_Documentos_Adjuntos doc
		INNER JOIN SGI_Tramites_Tareas tt ON doc.id_tramitetarea = tt.id_tramitetarea
		INNER JOIN SGI_Tramites_Tareas_HAB tt_HAB ON tt.id_tramitetarea = tt_HAB.id_tramitetarea
		INNER JOIN TiposDeDocumentosRequeridos tipo_doc ON doc.id_tdocreq = tipo_doc.id_tdocreq
		WHERE tt_HAB.id_solicitud = @id_solicitud AND tt.id_tarea<>103 --Verificación AVH
		
	OPEN cur_cargar_sgi_adj	
	
	FETCH NEXT FROM cur_cargar_sgi_adj INTO
		@sgi_id_doc_adj
		,@sgi_id_tdoc_adj
		,@sgi_tdoc_adj_detalle
		,@sgi_createDate
		,@sgi_nombre_tdoc_adj
			
	
	WHILE @@FETCH_STATUS = 0
	BEGIN
	
		
		SET @nro_tramite = @sgi_id_doc_adj
        SET @descripcion_tramite = 'SGI Documentos Adjuntos' +
			' id_doc_adj: ' + CONVERT(nvarchar, @nro_tramite)
						
		SET @parametros_SADE = '{"Usuario_SADE":"' + @UserName_SADE_Cal + '"}'
		
		EXEC @id_generar_expediente_proc = Id_Nuevo 'SGI_Tarea_Generar_Expediente_Procesos'  
		
		INSERT INTO SGI_Tarea_Generar_Expediente_Procesos(   
			id_generar_expediente_proc    
			,id_paquete    
			,id_caratula
			,id_proceso    
			,id_devolucion_ee    
			,resultado_ee    
			,realizado   
			,nro_tramite        
			,descripcion_tramite
			,CreateUser    
			,CreateDate
			,id_tramitetarea
			,parametros_SADE)  
		VALUES(   
			@id_generar_expediente_proc    
			,@id_paquete  
			,@id_caratula  
			,@id_proceso    
			,@id_devolucion_ee    
			,''    
			,0
			,@nro_tramite        
			,@descripcion_tramite        
			,@userId    
			,GETDATE()
			,@id_tramitetarea
			,@parametros_SADE)  
		
				
		FETCH NEXT FROM cur_cargar_sgi_adj INTO
			@sgi_id_doc_adj
			,@sgi_id_tdoc_adj
			,@sgi_tdoc_adj_detalle
			,@sgi_createDate
			,@sgi_nombre_tdoc_adj
		
	END
			
	CLOSE cur_cargar_sgi_adj	
	DEALLOCATE cur_cargar_sgi_adj 	
	
	
	------------------------------------------
	-- Planos 
	------------------------------------------
	
	IF @cod_circuito = @cod_circuito_scp
	BEGIN
	
		-- es el circuito de planos y tiene planos cargados

		DECLARE curPlanos CURSOR FAST_FORWARD FOR
	    SELECT 
			id_encomienda_plano,
			'Plano ' + Isnull(tplan.extension,'') + ' - Encomienda ' + convert(nvarchar,encplan.id_encomienda) + ' (Archivo Nº ' + convert(nvarchar,encplan.id_file)
	    FROM
			Encomienda_Planos encplan
			INNER JOIN TiposDePlanos tplan ON encplan.id_tipo_plano = tplan.id_tipo_plano
			INNER JOIN Files fil ON fil.id_file = encplan.id_file
		WHERE
			encplan.id_encomienda IN ( SELECT id_encomienda FROM @enc_hist )
	    
	    OPEN curPlanos
	    FETCH NEXT FROM curPlanos INTO @nro_tramite , @descripcion_tramite
	    
	    WHILE @@FETCH_STATUS = 0
	    BEGIN

			SET @id_proceso = 9
			
			SET @parametros_SADE = '{"Usuario_SADE":"' + @UserName_SADE_Cal + '"}'
			
			EXEC @id_generar_expediente_proc = Id_Nuevo 'SGI_Tarea_Generar_Expediente_Procesos'  
			
			INSERT INTO SGI_Tarea_Generar_Expediente_Procesos(   
				id_generar_expediente_proc    
				,id_paquete    
				,id_caratula
				,id_proceso    
				,id_devolucion_ee    
				,resultado_ee    
				,realizado   
				,nro_tramite        
				,descripcion_tramite
				,CreateUser    
				,CreateDate
				,id_tramitetarea
				,parametros_SADE)  
			VALUES(   
				@id_generar_expediente_proc    
				,@id_paquete  
				,@id_caratula  
				,@id_proceso    
				,@id_devolucion_ee    
				,''    
				,0
				,@nro_tramite        
				,@descripcion_tramite        
				,@userId    
				,GETDATE()
				,@id_tramitetarea
				,@parametros_SADE)  
				
				
			FETCH NEXT FROM curPlanos INTO @nro_tramite , @descripcion_tramite

		END
		
	END

    ------------------------------------------
	-- pasar expediente a SubGerente
	------------------------------------------
	SET @id_proceso = 10
	SET @nro_tramite = -1 
	SET @descripcion_tramite = 'Pase expediente' -- + CONVERT(nvarchar, @id_solicitud)	

	SET @parametros_SADE = '{"Usuario":"' + @UserName_SADE_SG + '"}'
	
	EXEC @id_generar_expediente_proc = Id_Nuevo 'SGI_Tarea_Generar_Expediente_Procesos'  

	INSERT INTO SGI_Tarea_Generar_Expediente_Procesos(   
		id_generar_expediente_proc    
		,id_paquete    
		,id_caratula
		,id_proceso    
		,id_devolucion_ee    
		,resultado_ee    
		,realizado   
		,nro_tramite        
		,descripcion_tramite
		,CreateUser    
		,CreateDate
		,id_tramitetarea
		,parametros_SADE)  
	VALUES(   
		@id_generar_expediente_proc    
		,@id_paquete  
		,@id_caratula  
		,@id_proceso    
		,@id_devolucion_ee    
		,''    
		,0
		,@nro_tramite        
		,@descripcion_tramite        
		,@userId    
		,GETDATE()
		,@id_tramitetarea
		,@parametros_SADE)  

	------------------------------------------	
	-- 13	Subida Providencia SubGerente
	------------------------------------------		
	SET @id_proceso = 13
	SET @nro_tramite = 1
	SET @descripcion_tramite = 'Providencia Sub-Gerente' 
						
	SET @parametros_SADE = '{"Usuario_SADE":"' + @UserName_SADE_SG + '"}'
	
	EXEC @id_generar_expediente_proc = Id_Nuevo 'SGI_Tarea_Generar_Expediente_Procesos'  

	INSERT INTO SGI_Tarea_Generar_Expediente_Procesos(   
		id_generar_expediente_proc    
		,id_paquete    
		,id_caratula
		,id_proceso    
		,id_devolucion_ee    
		,resultado_ee    
		,realizado   
		,nro_tramite        
		,descripcion_tramite
		,CreateUser    
		,CreateDate
		,id_tramitetarea
		,parametros_SADE)  
	VALUES(   
		@id_generar_expediente_proc    
		,@id_paquete  
		,@id_caratula  
		,@id_proceso    
		,@id_devolucion_ee    
		,''    
		,0
		,@nro_tramite        
		,@descripcion_tramite        
		,@userId    
		,GETDATE()
		,@id_tramitetarea
		,@parametros_SADE) 

	-- Se sube siempre y cuando no sea con planos		
	if(@cod_circuito <> @cod_circuito_scp)
	BEGIN
		------------------------------------------
		-- pasar expediente a SubGerente
		------------------------------------------
		SET @id_proceso = 10
		SET @nro_tramite = -1 
		SET @descripcion_tramite = 'Pase expediente' -- + CONVERT(nvarchar, @id_solicitud)	

		SET @parametros_SADE = '{"Usuario":"' + @UserName_SADE_GE + '"}'
		
		EXEC @id_generar_expediente_proc = Id_Nuevo 'SGI_Tarea_Generar_Expediente_Procesos'  

		INSERT INTO SGI_Tarea_Generar_Expediente_Procesos(   
			id_generar_expediente_proc    
			,id_paquete    
			,id_caratula
			,id_proceso    
			,id_devolucion_ee    
			,resultado_ee    
			,realizado   
			,nro_tramite        
			,descripcion_tramite
			,CreateUser    
			,CreateDate
			,id_tramitetarea
			,parametros_SADE)  
		VALUES(   
			@id_generar_expediente_proc    
			,@id_paquete  
			,@id_caratula  
			,@id_proceso    
			,@id_devolucion_ee    
			,''    
			,0
			,@nro_tramite        
			,@descripcion_tramite        
			,@userId    
			,GETDATE()
			,@id_tramitetarea
			,@parametros_SADE)  
			
		------------------------------------------	
		-- 13	Subida Providencia Gerente
		------------------------------------------		
		SET @id_proceso = 13
		SET @nro_tramite = 2
		SET @descripcion_tramite = 'Providencia Gerente'
							
		SET @parametros_SADE = '{"Usuario_SADE":"' + @UserName_SADE_GE + '"}'
		
		EXEC @id_generar_expediente_proc = Id_Nuevo 'SGI_Tarea_Generar_Expediente_Procesos'  

		INSERT INTO SGI_Tarea_Generar_Expediente_Procesos(   
			id_generar_expediente_proc    
			,id_paquete    
			,id_caratula
			,id_proceso    
			,id_devolucion_ee    
			,resultado_ee    
			,realizado   
			,nro_tramite        
			,descripcion_tramite
			,CreateUser    
			,CreateDate
			,id_tramitetarea
			,parametros_SADE)  
		VALUES(   
			@id_generar_expediente_proc    
			,@id_paquete  
			,@id_caratula  
			,@id_proceso    
			,@id_devolucion_ee    
			,''    
			,0
			,@nro_tramite        
			,@descripcion_tramite        
			,@userId    
			,GETDATE()
			,@id_tramitetarea
			,@parametros_SADE) 
			
	END

	------------------------------------------	
	-- 6 Firma Plancheta
	------------------------------------------		
	SET @id_proceso = 6
	SET @nro_tramite = -1 -- solo para probar envio el pdf de solicitud
	SET @descripcion_tramite = 'Disposición a la firma'
	--6 Firma Plancheta
	
	SET @parametros_SADE = '{"Usuario_SADE":"' + @UserName_SADE_GE + '"}'
	
	EXEC @id_generar_expediente_proc = Id_Nuevo 'SGI_Tarea_Generar_Expediente_Procesos'  

	INSERT INTO SGI_Tarea_Generar_Expediente_Procesos(   
		id_generar_expediente_proc    
		,id_paquete    
		,id_caratula
		,id_proceso    
		,id_devolucion_ee    
		,resultado_ee    
		,realizado   
		,nro_tramite        
		,descripcion_tramite
		,CreateUser    
		,CreateDate
		,id_tramitetarea
		,parametros_SADE)  
	VALUES(   
		@id_generar_expediente_proc    
		,@id_paquete  
		,@id_caratula  
		,@id_proceso    
		,@id_devolucion_ee    
		,''    
		,0
		,@nro_tramite        
		,@descripcion_tramite        
		,@userId    
		,GETDATE()
		,@id_tramitetarea
		,@parametros_SADE) 


    ------------------------------------------
	-- pasar expediente a otro usuario
	------------------------------------------
	-- Para los SIMPLES CON PLANOS no hace el pase
	IF @cod_circuito <> @cod_circuito_scp
	BEGIN 
		SET @id_proceso = 10
		SET @nro_tramite = -1 
		SET @descripcion_tramite = 'Pase expediente' -- + CONVERT(nvarchar, @id_solicitud)	
		
		EXEC SGI_Tarea_Generar_Expediente_Procesos_insert
			@id_tramitetarea
			,@id_paquete
			,@id_caratula
			,@id_proceso
			,@id_devolucion_ee
			,''
			,0
			,@nro_tramite
			,@descripcion_tramite
			,@userId
	END
END
