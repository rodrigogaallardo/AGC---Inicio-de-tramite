
/****** Object:  StoredProcedure [dbo].[SSIT_DocumentosAdjuntos_Add]    Script Date: 11/03/2016 09:59:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SSIT_DocumentosAdjuntos_Add]
(			
    @id_solicitud int  
    ,@id_tdocreq int
    ,@tdocreq_detalle nvarchar(50)
	,@id_tipodocsis int
	,@generadoxSistema bit
    ,@id_file int 	
    ,@nombre_archivo nvarchar(50)
    ,@CreateUser uniqueidentifier
    ,@id_docadjunto int output
)		
AS			
BEGIN			
			
	EXEC @id_docadjunto = Id_Nuevo 'SSIT_DocumentosAdjuntos'  	
    		
	INSERT INTO SSIT_DocumentosAdjuntos(   	
		id_docadjunto    
		,id_solicitud 
		,id_tdocreq
		,tdocreq_detalle
		,id_tipodocsis 
		,id_file  
		,generadoxSistema 
		,CreateDate    
		,CreateUser 
		,nombre_archivo
		)  
	VALUES(   	
		@id_docadjunto    
		,@id_solicitud  
		,@id_tdocreq
		,@tdocreq_detalle
		,@id_tipodocsis 
		,@id_file  
		,@generadoxSistema 
		,GETDATE()
		,@CreateUser
		,@nombre_archivo
		)
	
END
GO
/****** Object:  StoredProcedure [dbo].[CPadron_DocumentosAdjuntos_Agregar]    Script Date: 11/03/2016 11:30:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[CPadron_DocumentosAdjuntos_Agregar]			
(			
    @id_cpadron int  
    ,@id_tdocreq int
    ,@tdocreq_detalle nvarchar(50)
	,@id_tipodocsis int
	,@generadoxSistema bit
    ,@id_file int 	
    ,@nombre_archivo nvarchar(50)
    ,@CreateUser uniqueidentifier
    ,@id_docadjunto int output
)		
AS			
BEGIN			
			
	EXEC @id_docadjunto = Id_Nuevo 'CPadron_DocumentosAdjuntos'  	
    		
	INSERT INTO CPadron_DocumentosAdjuntos(   	
		id_docadjunto    
		,id_cpadron 
		,id_tdocreq
		,tdocreq_detalle
		,id_tipodocsis 
		,id_file  
		,generadoxSistema 
		,CreateDate    
		,CreateUser 
		,nombre_archivo
		)  
	VALUES(   	
		@id_docadjunto    
		,@id_cpadron  
		,@id_tdocreq
		,@tdocreq_detalle
		,@id_tipodocsis 
		,@id_file  
		,@generadoxSistema 
		,GETDATE()
		,@CreateUser
		,@nombre_archivo
		)
	
END
GO
/****** Object:  StoredProcedure [dbo].[Transf_DocumentosAdjuntos_Agregar]    Script Date: 11/03/2016 11:33:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Transf_DocumentosAdjuntos_Agregar]			
(			
    @id_solicitud int  
    ,@id_tdocreq int
    ,@tdocreq_detalle nvarchar(50)
	,@id_tipodocsis int
	,@generadoxSistema bit
    ,@id_file int 	
    ,@nombre_archivo nvarchar(50)
    ,@CreateUser uniqueidentifier
    ,@id_agrupamiento	int = 0
    ,@id_docadjunto int output
)		
AS			
BEGIN			
			
	EXEC @id_docadjunto = Id_Nuevo 'Transf_DocumentosAdjuntos'  	
    		
	INSERT INTO Transf_DocumentosAdjuntos(   	
		id_docadjunto    
		,id_solicitud 
		,id_tdocreq
		,tdocreq_detalle
		,id_tipodocsis 
		,id_file  
		,generadoxSistema 
		,CreateDate    
		,CreateUser 
		,nombre_archivo
		,id_agrupamiento
		)  
	VALUES(   	
		@id_docadjunto    
		,@id_solicitud  
		,@id_tdocreq
		,@tdocreq_detalle
		,@id_tipodocsis 
		,@id_file  
		,@generadoxSistema 
		,GETDATE()
		,@CreateUser
		,@nombre_archivo
		,@id_agrupamiento
		)
	
END
GO

/****** Object:  StoredProcedure [dbo].[Encomienda_DocumentosAdjuntos_Add]    Script Date: 11/03/2016 14:38:18 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Encomienda_DocumentosAdjuntos_Add]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Encomienda_DocumentosAdjuntos_Add]
GO

/****** Object:  StoredProcedure [dbo].[Encomienda_DocumentosAdjuntos_Add]    Script Date: 11/03/2016 14:38:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Encomienda_DocumentosAdjuntos_Add]			
(			
    @id_encomienda int  
    ,@id_tdocreq int
    ,@tdocreq_detalle nvarchar(50)
	,@id_tipodocsis int
	,@generadoxSistema bit
    ,@id_file int 	
    ,@nombre_archivo nvarchar(50)
    ,@CreateUser uniqueidentifier
    ,@id_docadjunto int output
)		
AS			
BEGIN			
			
	EXEC @id_docadjunto = Id_Nuevo 'Encomienda_DocumentosAdjuntos'  	
    		
	INSERT INTO Encomienda_DocumentosAdjuntos
           (id_docadjunto
           ,id_encomienda
           ,id_tdocreq
           ,tdocreq_detalle
           ,id_tipodocsis
           ,id_file
           ,generadoxSistema
           ,CreateDate
           ,CreateUser
           ,nombre_archivo)
		
	VALUES(   	
		@id_docadjunto    
		,@id_encomienda  
		,@id_tdocreq
		,@tdocreq_detalle
		,@id_tipodocsis 
		,@id_file  
		,@generadoxSistema 
		,GETDATE()
		,@CreateUser
		,@nombre_archivo
		)
	
END
GO
--------------------------------------------------------
-- relacion de tipo doc con TAREA
--------------------------------------------------------
IF NOT EXISTS(SELECT 1 FROM Rel_TiposDeDocumentosRequeridos_ENG_Tareas WHERE id_tarea=10)
BEGIN
	DECLARE cur CURSOR FAST_FORWARD FOR
		SELECT DISTINCT [id_tdocreq]
		FROM [dbo].[Rel_TipoTramite_TiposDeDocumentosRequeridos]
		WHERE [id_tipotramite]=1
	DECLARE @id_tdocreq int
			,@id int
	EXEC @id = Id_Nuevo 'Rel_TiposDeDocumentosRequeridos_ENG_Tareas'
	OPEN cur
	FETCH NEXT FROM cur INTO @id_tdocreq
	WHILE @@FETCH_STATUS = 0
	BEGIN
		--calificar habilitacion
		INSERT INTO Rel_TiposDeDocumentosRequeridos_ENG_Tareas VALUES(@id,@id_tdocreq,10)
		INSERT INTO Rel_TiposDeDocumentosRequeridos_ENG_Tareas VALUES(@id+1,@id_tdocreq,35)
		INSERT INTO Rel_TiposDeDocumentosRequeridos_ENG_Tareas VALUES(@id+2,@id_tdocreq,403)
		INSERT INTO Rel_TiposDeDocumentosRequeridos_ENG_Tareas VALUES(@id+3,@id_tdocreq,303)
		INSERT INTO Rel_TiposDeDocumentosRequeridos_ENG_Tareas VALUES(@id+4,@id_tdocreq,607)
		INSERT INTO Rel_TiposDeDocumentosRequeridos_ENG_Tareas VALUES(@id+5,@id_tdocreq,507)
		INSERT INTO Rel_TiposDeDocumentosRequeridos_ENG_Tareas VALUES(@id+6,@id_tdocreq,102)
		INSERT INTO Rel_TiposDeDocumentosRequeridos_ENG_Tareas VALUES(@id+7,@id_tdocreq,202)
		INSERT INTO Rel_TiposDeDocumentosRequeridos_ENG_Tareas VALUES(@id+8,@id_tdocreq,121)
		INSERT INTO Rel_TiposDeDocumentosRequeridos_ENG_Tareas VALUES(@id+9,@id_tdocreq,204)
		INSERT INTO Rel_TiposDeDocumentosRequeridos_ENG_Tareas VALUES(@id+10,@id_tdocreq,502)
		INSERT INTO Rel_TiposDeDocumentosRequeridos_ENG_Tareas VALUES(@id+11,@id_tdocreq,602)

		SET @id= @id+12
		--Revisi�n Gerente
		INSERT INTO Rel_TiposDeDocumentosRequeridos_ENG_Tareas VALUES(@id,@id_tdocreq,12)
		INSERT INTO Rel_TiposDeDocumentosRequeridos_ENG_Tareas VALUES(@id+1,@id_tdocreq,37)
		INSERT INTO Rel_TiposDeDocumentosRequeridos_ENG_Tareas VALUES(@id+2,@id_tdocreq,305)
		INSERT INTO Rel_TiposDeDocumentosRequeridos_ENG_Tareas VALUES(@id+3,@id_tdocreq,505)
		INSERT INTO Rel_TiposDeDocumentosRequeridos_ENG_Tareas VALUES(@id+4,@id_tdocreq,509)
		INSERT INTO Rel_TiposDeDocumentosRequeridos_ENG_Tareas VALUES(@id+5,@id_tdocreq,609)
		INSERT INTO Rel_TiposDeDocumentosRequeridos_ENG_Tareas VALUES(@id+6,@id_tdocreq,105)
		INSERT INTO Rel_TiposDeDocumentosRequeridos_ENG_Tareas VALUES(@id+7,@id_tdocreq,206)
		INSERT INTO Rel_TiposDeDocumentosRequeridos_ENG_Tareas VALUES(@id+8,@id_tdocreq,212)
		INSERT INTO Rel_TiposDeDocumentosRequeridos_ENG_Tareas VALUES(@id+9,@id_tdocreq,122)

		SET @id= @id+10
		--Revisi�n Subgerente
		INSERT INTO Rel_TiposDeDocumentosRequeridos_ENG_Tareas VALUES(@id,@id_tdocreq,11)
		INSERT INTO Rel_TiposDeDocumentosRequeridos_ENG_Tareas VALUES(@id+1,@id_tdocreq,36)
		INSERT INTO Rel_TiposDeDocumentosRequeridos_ENG_Tareas VALUES(@id+2,@id_tdocreq,74)
		INSERT INTO Rel_TiposDeDocumentosRequeridos_ENG_Tareas VALUES(@id+3,@id_tdocreq,104)
		INSERT INTO Rel_TiposDeDocumentosRequeridos_ENG_Tareas VALUES(@id+4,@id_tdocreq,205)
		INSERT INTO Rel_TiposDeDocumentosRequeridos_ENG_Tareas VALUES(@id+5,@id_tdocreq,304)
		INSERT INTO Rel_TiposDeDocumentosRequeridos_ENG_Tareas VALUES(@id+6,@id_tdocreq,404)
		INSERT INTO Rel_TiposDeDocumentosRequeridos_ENG_Tareas VALUES(@id+7,@id_tdocreq,508)
		INSERT INTO Rel_TiposDeDocumentosRequeridos_ENG_Tareas VALUES(@id+8,@id_tdocreq,608)
		SET @id= @id+9

		FETCH NEXT FROM cur INTO @id_tdocreq

	END
	CLOSE cur
	DEALLOCATE cur
END
GO
/****** Object:  StoredProcedure [dbo].[SSIT_Solicitudes_ActualizarEstado]    Script Date: 11/07/2016 12:56:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SSIT_Solicitudes_ActualizarEstado]
(
	@id_solicitud		int,
	@id_estado			int,
	@userid				uniqueidentifier,
	@NroExpediente		varchar(20) = NULL,
	@telefono			varchar(25) = NULL
)	
AS
BEGIN

	DECLARE @matriculaEscribano			int
		,@ssit_createDate		datetime 
		,@id_encomienda					int
		,@nro_matricula_escribano_acta	int
		,@msgError						nvarchar(1000)
		,@fecha_certificacion_enc		datetime
		,@id_tipotramite_rectificatoria	int
		,@id_tipotramite				int
		,@id_tipoexpediente				int
		,@id_subtipoexpediente			int
		,@id_estado_confirmado			int
		,@id_estado_anulado				int
		,@cant_tareas					int
		,@id_tramitetarea				int	
		,@id_tramitetarea_nuevo			int	
		,@usuario_calif_asig			uniqueidentifier
		,@id_tarea_nueva				int	
		,@id_circuito					int
		,@id_actanotarial				int
		,@fecha_implementacion_esp		datetime
		
	DECLARE @p_id_estado		int
	DECLARE @id_resultado		int
	
	SET @p_id_estado = @id_estado
	SET @fecha_implementacion_esp = convert(datetime,'14/12/2015',103)
	
	-- Rectificatoria de HabilitaciÃ³n
	SELECT @id_tipotramite_rectificatoria = id_tipotramite
	FROM TipoTramite
	WHERE cod_tipotramite = 'RECTIF_HABILITACION'

	-----------------------------------
	-- Obtiene  datos de la Solicitud
	-----------------------------------
	SELECT 
		@matriculaEscribano = matriculaEscribano,
		@ssit_createDate = createDate,
		@id_tipotramite = id_tipotramite,
		@id_tipoexpediente = id_tipoexpediente,
		@id_subtipoexpediente  = id_subtipoexpediente
	FROM 
		SSIT_Solicitudes
	WHERE 
		id_solicitud = @id_solicitud

	--Busco la ultima encomienda
	SELECT TOP 1 @id_encomienda = id_encomienda FROM encomienda 
		WHERE id_solicitud = @id_solicitud 
		AND id_estado = dbo.Encomienda_Bus_idEstadoSolicitud('APROC')
		ORDER BY id_encomienda DESC
		
	SET @fecha_certificacion_enc = dbo.Bus_FechaCertificacionEncomienda(@id_encomienda)
    SET @id_estado_confirmado = dbo.Bus_idEstadoSolicitud('PING') 
    SET @id_estado_anulado = dbo.Bus_idEstadoSolicitud('ANU') 
    

	-----------------------------
	-- Se exige el acta notarial 
	-----------------------------
	IF @id_estado = @id_estado_confirmado 
	BEGIN
		---------------------------------------------------------
		-- Valida que la solicitud tenga un acta notarial digital
		---------------------------------------------------------
		SELECT 
			@nro_matricula_escribano_acta = nro_matricula_escribano_acta,
			@id_actanotarial = id_actanotarial
		FROM
			wsEscribanos_ActaNotarial
		WHERE
			id_encomienda = @id_encomienda
		AND anulada = 0	
				
		IF @@ROWCOUNT = 0
		BEGIN
			IF @id_tipotramite = @id_tipotramite_rectificatoria
			BEGIN
				----------------------------------------------------------------
				-- Verifico cuando es rectificatoria si algunas de las anteriores  
				-- rectificatorias o solicitudes tiene asociada un acta notarial
				----------------------------------------------------------------
				SELECT TOP 1
					@nro_matricula_escribano_acta = nro_matricula_escribano_acta,
					@id_actanotarial = id_actanotarial
				FROM
					wsEscribanos_ActaNotarial
				WHERE
					id_encomienda in(SELECT id_encomienda FROM SSIT_Solicitudes_Encomienda where id_solicitud=@id_solicitud)
					AND anulada = 0	
				ORDER BY 
					id_actanotarial DESC
				
				IF @@ROWCOUNT = 0
				BEGIN
					SET @msgError = 'Todas las encomiendas o rectificatorias de habilitaciones deberÃ¡n estar acompaÃ±adas del Certificado de Acta notarial y el Certificado de Aptitud Ambiental (CAA), para poder confirmar la solicitud.'
					RAISERROR(@msgError,16,1)
					RETURN					
				END
				
			END
			ELSE
			BEGIN
				SET @msgError = 'Todas las encomiendas o rectificatorias de habilitaciones deberÃ¡n estar acompaÃ±adas del Certificado de Acta notarial y el Certificado de Aptitud Ambiental (CAA), para poder confirmar la solicitud.'
				RAISERROR(@msgError,16,1)
				RETURN	
			END
			
			
		END 
		
		------------------------------------------------
		-- Valida que existe el archivo de Acta notarial
		------------------------------------------------
		IF NOT EXISTS(SELECT 'X' FROM Certificados WHERE TipoTramite = 3 AND NroTramite = @id_actanotarial)
		BEGIN
			SET @msgError = 'Se han enviado los datos del acta notarial pero no se ha enviado el archivo pdf correspondiente a la misma.'
			RAISERROR(@msgError,16,1)
			RETURN	
		END
		
		------------------------------------------------------------------------
		-- Se respeta la matricula informada por escribanos para guardar en tabla
		------------------------------------------------------------------------
		SET @matriculaEscribano = @nro_matricula_escribano_acta
		
		------------------------------------
		-- Verificar que no exista clausura para las ubicaciones
		------------------------------------
		IF EXISTS(SELECT 'X' FROM dbo.Encomienda_Ubicaciones eu
					JOIN dbo.Ubicaciones_Clausuras uc ON uc.id_ubicacion=eu.id_ubicacion
					WHERE eu.id_encomienda=@id_encomienda
						AND uc.fecha_alta_clausura < GETDATE() AND ( uc.fecha_baja_clausura > GETDATE() OR uc.fecha_baja_clausura IS NULL))
		OR EXISTS(SELECT 'X' FROM dbo.Encomienda_Ubicaciones eu
					JOIN dbo.Encomienda_Ubicaciones_PropiedadHorizontal eph ON eph.id_encomiendaubicacion=eu.id_encomiendaubicacion
					JOIN dbo.Ubicaciones_PropiedadHorizontal_Clausuras uphc ON uphc.id_propiedadhorizontal=eph.id_propiedadhorizontal
					WHERE eu.id_encomienda=@id_encomienda
					AND uphc.fecha_alta_clausura < GETDATE() AND ( uphc.fecha_baja_clausura > GETDATE() OR uphc.fecha_baja_clausura IS NULL))
		BEGIN
			SET @msgError = 'Se pone en conocimiento que el domicilio declarado por usted presenta irregularidades. Por favor acerquese a nuestras oficinas ubicadas en TTE. GRAL. JUAN DOMINGO PERON 2941.'
			RAISERROR(@msgError,16,1)
			RETURN		
		END

		IF EXISTS((SELECT 'X' FROM dbo.Encomienda_Ubicaciones eu
					JOIN dbo.Ubicaciones_Inhibiciones ui ON ui.id_ubicacion=eu.id_ubicacion
					WHERE eu.id_encomienda=@id_encomienda AND ui.fecha_vencimiento > GETDATE()))
		OR EXISTS(SELECT 'X' FROM dbo.Encomienda_Ubicaciones eu
					JOIN dbo.Encomienda_Ubicaciones_PropiedadHorizontal eph ON eph.id_encomiendaubicacion=eu.id_encomiendaubicacion
					JOIN dbo.Ubicaciones_PropiedadHorizontal_Inhibiciones uphi ON uphi.id_propiedadhorizontal=eph.id_propiedadhorizontal
					WHERE eu.id_encomienda=@id_encomienda AND uphi.fecha_vencimiento > GETDATE())
		BEGIN
			SET @msgError = 'Se pone en conocimiento que el domicilio declarado por usted esta inhibido. Por favor acerquese a nuestras oficinas ubicadas en TTE. GRAL. JUAN DOMINGO PERON 2941.'
			RAISERROR(@msgError,16,1)
			RETURN		
		END
		
	END


	IF  @id_estado <> @id_estado_anulado
	BEGIN
		
		IF @matriculaEscribano is null
		BEGIN
			SET @msgError = 'No se a cargado la matricula del escribanos.'
			RAISERROR(@msgError,16,1)
			RETURN		
		END
		
		------------------------------------
		-- Verificar que la matricula exista
		------------------------------------
		IF NOT EXISTS(SELECT 'X' FROM escribano
				WHERE matricula = @matriculaEscribano)
		BEGIN
			SET @msgError = 'No existe la matricula ' + convert (varchar(20), @matriculaEscribano) + ' para escribanos.'
			RAISERROR(@msgError,16,1)
			RETURN
		END
	END

	--------------------------------------------------------------------------
	-- Se obtiene el circuito y los cÃ³digos de tarea dependiendo del circuito.
	--------------------------------------------------------------------------
	DECLARE 
		@cod_tarea_correccion_solicitud nvarchar(10),
		@cod_tarea_asignar_calificador nvarchar(10),
		@cod_tarea_calificar_tramite nvarchar(10),
		@cod_tarea_fin_tramite nvarchar(10)
	
		
	SELECT @id_circuito = rel.id_circuito 
	FROM ENG_Rel_Circuitos_TiposDeTramite rel
		INNER JOIN ENG_Circuitos cir ON rel.id_circuito = cir.id_circuito
	WHERE id_tipotramite = @id_tipotramite 
		AND id_tipoexpediente = @id_tipoexpediente 
		AND id_subtipoexpediente = @id_subtipoexpediente
		AND cir.version_circuito = 1
			
	SET @cod_tarea_correccion_solicitud = CONVERT(nvarchar,@id_circuito) + '25'
	SET @cod_tarea_asignar_calificador = CONVERT(nvarchar,@id_circuito) + '09'
	SET @cod_tarea_calificar_tramite = CONVERT(nvarchar,@id_circuito) + '10'
	SET @cod_tarea_fin_tramite = CONVERT(nvarchar,@id_circuito) + '29'
	
	----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	-- Si se esta confirmando (o anulando) una solicitud se debe poner en trÃ¡mite ya que la misma va al sistema SGI.
	-- o si el estado de la solicitud es anulado se cierra la tarea.
	-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	IF (@id_tipotramite IN(1,5) 
		-- si es simple or si es especial y la fecha de aprobaciÃ³n de la encomienda es mayor o igual al 15/12/2015.
		AND ( @id_subtipoexpediente IN(1,2) OR (@id_subtipoexpediente IN(3,4) AND @fecha_certificacion_enc >= @fecha_implementacion_esp)) 
		AND @p_id_estado IN(@id_estado_confirmado, @id_estado_anulado )  
		AND NOT EXISTS(SELECT 'X' FROM SSIT_Solicitudes WHERE id_solicitud = @id_solicitud AND LEN(NroExpediente) > 4)	-- No deben entrar al SGI los que tienen expediente
		) 
	BEGIN
		IF @p_id_estado = @id_estado_anulado
		BEGIN
		
			SET @id_tramitetarea  = 0 
			SET @id_resultado = 11
			
			--------------------------------------------------
			-- Busca la tarea 125 (CorrecciÃ³n de la solicitud)
			--------------------------------------------------
			SELECT TOP 1 @id_tramitetarea = tt.id_tramitetarea 
			FROM SGI_Tramites_Tareas tt
			INNER JOIN SGI_Tramites_Tareas_HAB tt_HAB ON tt.id_tramitetarea = tt_HAB.id_tramitetarea
			WHERE tt_HAB.id_solicitud = @id_solicitud 
			AND tt.id_tarea = dbo.Bus_id_tarea(@cod_tarea_correccion_solicitud)
			AND tt.FechaCierre_tramitetarea IS NULL
			ORDER BY tt.id_tramitetarea DESC
			
			--------------------------------------------------
			-- Cierra la tarea (CorrecciÃ³n de la solicitud)
			--------------------------------------------------
			IF @id_tramitetarea > 0 
				EXEC ENG_Finalizar_Tarea @id_tramitetarea, @id_resultado, 0, NULL, @id_tramitetarea_nuevo out
							
			-- Genera una tarea (fin trÃ¡mite) 
			SET @id_tarea_nueva = dbo.Bus_id_tarea(@cod_tarea_fin_tramite)
			SET @id_tramitetarea  = 0
			EXEC ENG_Crear_Tarea  @id_solicitud, @id_tarea_nueva, @userid, @id_tramitetarea out
			IF @id_tramitetarea > 0 
				EXEC ENG_Finalizar_Tarea @id_tramitetarea, @id_resultado, 0, NULL, @id_tramitetarea_nuevo out
			
		END   
				
		ELSE IF @p_id_estado = @id_estado_confirmado
		BEGIN
			
			SET @id_estado = dbo.Bus_idEstadoSolicitud('ETRA') 
			
			--------------------------------------------------
			-- Se ha confirmado la solicitud
			--------------------------------------------------
			SELECT @cant_tareas = COUNT(*)
			FROM SGI_Tramites_Tareas tt
			INNER JOIN SGI_Tramites_Tareas_HAB tt_HAB ON tt.id_tramitetarea = tt_HAB.id_tramitetarea
			WHERE tt_HAB.id_solicitud = @id_solicitud
			
			
			IF @cant_tareas <= 1
			BEGIN
				
				SET @id_tramitetarea  = 0 
				
				-------------------------------------------------------------
				-- Obtiene la Ãºnica tarea que hay (Solicitud de habilitaciÃ³n)
				-------------------------------------------------------------
				
				SELECT TOP 1 @id_tramitetarea = tt.id_tramitetarea 
				FROM SGI_Tramites_Tareas tt
				INNER JOIN SGI_Tramites_Tareas_HAB tt_HAB ON tt.id_tramitetarea = tt_HAB.id_tramitetarea
				WHERE tt_HAB.id_solicitud = @id_solicitud 
					AND tt.FechaCierre_tramitetarea IS NULL

				----------------------------------------------
				-- Cierra la tarea (Solicitud de HabilitaciÃ³n)
				----------------------------------------------
				IF @id_tramitetarea > 0 
					EXEC ENG_Finalizar_Tarea  @id_tramitetarea, 0, 0, NULL, @id_tramitetarea_nuevo out
					
				-----------------------------------------------------------
				-- Si solo hay una tarea serÃ­a la Solicitud de HabilitaciÃ³n
				-- entonces se crea la AsignaciÃ³n del calificador 
				-----------------------------------------------------------
				SET @id_tarea_nueva = dbo.Bus_id_tarea(@cod_tarea_asignar_calificador)
				
				SET @id_tramitetarea = 0
				EXEC ENG_Crear_Tarea @id_solicitud, @id_tarea_nueva, @userid, @id_tramitetarea out
				
				
			END
			ELSE
			BEGIN
				
				SET @id_tramitetarea  = 0
				----------------------------------------------
				-- Busca la tarea (CorrecciÃ³n de la solicitud)
				----------------------------------------------
				SELECT TOP 1 @id_tramitetarea = tt.id_tramitetarea 
				FROM SGI_Tramites_Tareas tt
				INNER JOIN SGI_Tramites_Tareas_HAB tt_HAB ON tt.id_tramitetarea = tt_HAB.id_tramitetarea
				WHERE tt_HAB.id_solicitud = @id_solicitud 
					AND tt.id_tarea in( dbo.Bus_id_tarea('125'),dbo.Bus_id_tarea('225'),dbo.Bus_id_tarea('325'),dbo.Bus_id_tarea('625'))
					AND tt.FechaCierre_tramitetarea IS NULL

				----------------------------------------------
				-- Cierra la tarea (CorrecciÃ³n de la solicitud)
				----------------------------------------------
				IF @id_tramitetarea > 0 
				BEGIN
					EXEC ENG_Finalizar_Tarea  @id_tramitetarea, 0, 0, NULL, @id_tramitetarea_nuevo out
				
					SET @id_tramitetarea  = 0
					
					-- Busca la tarea (Calificar trÃ¡mite) para saber el usuario calificador que tenÃ­a
					SELECT TOP 1
							@usuario_calif_asig = tt.UsuarioAsignado_tramitetarea
					FROM SGI_Tramites_Tareas tt
					INNER JOIN SGI_Tramites_Tareas_HAB tt_HAB ON tt.id_tramitetarea = tt_HAB.id_tramitetarea
					WHERE tt_HAB.id_solicitud = @id_solicitud 
						AND tt.id_tarea = dbo.Bus_id_tarea(@cod_tarea_calificar_tramite)  
						AND tt.FechaCierre_tramitetarea IS NOT NULL
					ORDER BY tt.id_tramitetarea DESC

					IF @usuario_calif_asig IS NOT NULL 
					BEGIN
						DECLARE @id_subtipoexpediente_encomienda int,
							@nuevo_circuito bit
						SET @nuevo_circuito= 0
						--si es rectificatoria y son de sistinto tipo que la solicitud se crea la etapa de asignacion de calificador 
						-- y se cambia el tipo de la solicitud
						IF @id_tipotramite = @id_tipotramite_rectificatoria
						BEGIN
							SELECT
								@id_subtipoexpediente_encomienda = id_subtipoexpediente
							FROM 
								Encomienda
							WHERE 
								id_encomienda = @id_encomienda	
							IF @id_subtipoexpediente <> @id_subtipoexpediente_encomienda
								SET @nuevo_circuito = 1
						END
						IF @nuevo_circuito = 0
						BEGIN
							-- Genera una tarea (Calificar trÃ¡mite) con el usuario calificador que tenÃ­a la anterior
							SET @id_tarea_nueva = dbo.Bus_id_tarea(@cod_tarea_calificar_tramite)
							SET @id_tramitetarea  = 0
							EXEC ENG_Crear_Tarea  @id_solicitud, @id_tarea_nueva, @userid, @id_tramitetarea out
							IF @id_tramitetarea > 0
								EXEC ENG_Asignar_Tarea @id_tramitetarea, @usuario_calif_asig
						END
						ELSE
						BEGIN
							SELECT @id_circuito = id_circuito 
							FROM ENG_Rel_Circuitos_TiposDeTramite
							WHERE id_tipotramite = @id_tipotramite 
								AND id_tipoexpediente = @id_tipoexpediente 
								AND id_subtipoexpediente = @id_subtipoexpediente_encomienda
									
							SET @cod_tarea_asignar_calificador = CONVERT(nvarchar,@id_circuito) + '09'
							--actualizo el suptipo de la solicitud
							UPDATE SSIT_Solicitudes
							SET id_subtipoexpediente = @id_subtipoexpediente_encomienda
							WHERE id_solicitud = @id_solicitud							
							
							-- entonces se crea la AsignaciÃ³n del calificador 
							SET @id_tarea_nueva = dbo.Bus_id_tarea(@cod_tarea_asignar_calificador)
							SET @id_tramitetarea = 0
							EXEC ENG_Crear_Tarea @id_solicitud, @id_tarea_nueva, @userid, @id_tramitetarea out
						END
					END
					ELSE
					BEGIN
						-- Se crea la AsignaciÃ³n del calificador 
						SET @id_tarea_nueva = dbo.Bus_id_tarea(@cod_tarea_asignar_calificador)
						SET @id_tramitetarea = 0
						EXEC ENG_Crear_Tarea @id_solicitud, @id_tarea_nueva, @userid, @id_tramitetarea out
					END
				END
				
			END
		
			
		END  
	
		
	END
	ELSE IF (@id_estado = dbo.Bus_idEstadoSolicitud('ING'))
		SET @id_estado = dbo.Bus_idEstadoSolicitud('ETRA') 
		
	
	IF  @id_estado = dbo.Bus_idEstadoSolicitud('ETRA')
	BEGIN
		UPDATE DocumentosAdjuntos
		SET puede_eliminar = 0
		WHERE id_solicitud = @id_solicitud
		AND puede_eliminar = 1
		AND origen = 'SSIT'
	END 
	
	UPDATE 
		SSIT_Solicitudes
	SET 
		id_estado = @id_estado,
		NroExpediente = IsNull(@NroExpediente,NroExpediente),
		matriculaEscribano = @matriculaEscribano, 
		LastUpdateDate = GETDATE(),
		LastUpdateUser = @userid,
		telefono = IsNull(@telefono,telefono)
	WHERE
		id_solicitud = @id_solicitud
END
GO
/****** Object:  StoredProcedure [dbo].[SSIT_Solicitudes_ActualizarEstado2]    Script Date: 11/07/2016 12:56:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SSIT_Solicitudes_ActualizarEstado2]
(
	@id_solicitud		int,
	@id_estado			int,
	@userid				uniqueidentifier,
	@NroExpediente		varchar(20) = NULL
)	
AS
BEGIN

	DECLARE @matriculaEscribano			int
		,@ssit_createDate		datetime 
		,@id_encomienda					int
		,@nro_matricula_escribano_acta	int
		,@msgError						nvarchar(1000)
		,@fecha_certificacion_enc		datetime
		,@id_tipotramite_rectificatoria	int
		,@id_tipotramite				int
		,@id_tipoexpediente				int
		,@id_subtipoexpediente			int
		,@id_estado_confirmado			int
		,@id_estado_anulado				int
		,@cant_tareas					int
		,@id_tramitetarea				int	
		,@id_tramitetarea_nuevo			int	
		,@usuario_calif_asig			uniqueidentifier
		,@id_tarea_nueva				int	
		,@id_circuito					int
		,@cod_circuito					nvarchar(20)
		,@id_actanotarial				int
		,@fecha_implementacion_esp		datetime
		,@p_id_estado					int
		,@id_resultado					int
		,@id_estado_sol					int
		,@FechaLibradoAlUso				datetime
	
	SET @p_id_estado = @id_estado
	SET @fecha_implementacion_esp = convert(datetime,'14/12/2015',103)
	
	-- Rectificatoria de Habilitación
	SELECT @id_tipotramite_rectificatoria = id_tipotramite
	FROM TipoTramite
	WHERE cod_tipotramite = 'RECTIF_HABILITACION'

	-----------------------------------
	-- Obtiene  datos de la Solicitud
	-----------------------------------
	SELECT 
		@matriculaEscribano = matriculaEscribano,
		@ssit_createDate = createDate,
		@id_tipotramite = id_tipotramite,
		@id_tipoexpediente = id_tipoexpediente,
		@id_subtipoexpediente  = id_subtipoexpediente,
		@id_estado_sol = id_estado
	FROM 
		SSIT_Solicitudes
	WHERE 
		id_solicitud = @id_solicitud

	--Busco la ultima encomienda
	SELECT TOP 1 @id_encomienda = id_encomienda FROM encomienda 
		WHERE id_solicitud = @id_solicitud 
		AND id_estado = dbo.Encomienda_Bus_idEstadoSolicitud('APROC')
		ORDER BY id_encomienda DESC	
    SET @id_estado_confirmado = dbo.Bus_idEstadoSolicitud('PING') 
    SET @id_estado_anulado = dbo.Bus_idEstadoSolicitud('ANU') 
    

	---------------------------------------------------------
	-- Se realizan todas las validaciones para confirmar
	---------------------------------------------------------
	IF @id_estado = @id_estado_confirmado 
	BEGIN
		---------------------------------------------------------
		-- Valida que la solicitud tenga un acta notarial digital
		---------------------------------------------------------
		SELECT 
			@nro_matricula_escribano_acta = nro_matricula_escribano_acta,
			@id_actanotarial = id_actanotarial
		FROM
			wsEscribanos_ActaNotarial
		WHERE
			id_encomienda = @id_encomienda
			AND anulada = 0	
				
		IF @@ROWCOUNT = 0
		BEGIN
			IF @id_tipotramite = @id_tipotramite_rectificatoria
			BEGIN
				----------------------------------------------------------------
				-- Verifico cuando es rectificatoria si algunas de las anteriores  
				-- rectificatorias o solicitudes tiene asociada un acta notarial
				----------------------------------------------------------------
				SELECT TOP 1
					@nro_matricula_escribano_acta = nro_matricula_escribano_acta,
					@id_actanotarial = id_actanotarial
				FROM
					wsEscribanos_ActaNotarial acta
					INNER JOIN SSIT_Solicitudes_Encomienda solenc ON acta.id_encomienda = solenc.id_encomienda
				WHERE
					solenc.id_solicitud = @id_solicitud
					AND acta.anulada = 0	
				ORDER BY 
					id_actanotarial DESC
				
				IF @@ROWCOUNT = 0
				BEGIN
					SET @msgError = 'Todas las encomiendas o rectificatorias de habilitaciones deberán estar acompañadas del Certificado de Acta notarial y el Certificado de Aptitud Ambiental (CAA), para poder confirmar la solicitud.'
					RAISERROR(@msgError,16,1)
					RETURN					
				END
				
			END
			ELSE
			BEGIN
				SET @msgError = 'Todas las encomiendas o rectificatorias de habilitaciones deberán estar acompañadas del Certificado de Acta notarial y el Certificado de Aptitud Ambiental (CAA), para poder confirmar la solicitud.'
				RAISERROR(@msgError,16,1)
				RETURN	
			END
			
			
		END 
		
		------------------------------------------------
		-- Valida que existe el archivo de Acta notarial
		------------------------------------------------
		IF NOT EXISTS(SELECT 'X' FROM Certificados WHERE TipoTramite = 3 AND NroTramite = @id_actanotarial)
		BEGIN
			SET @msgError = 'Se han enviado los datos del acta notarial pero no se ha enviado el archivo pdf correspondiente a la misma.'
			RAISERROR(@msgError,16,1)
			RETURN	
		END
		
		------------------------------------------------------------------------
		-- Se respeta la matricula informada por escribanos para guardar en tabla
		------------------------------------------------------------------------
		SET @matriculaEscribano = @nro_matricula_escribano_acta
	
		IF @matriculaEscribano is null
		BEGIN
			SET @msgError = 'No se a cargado la matricula del escribanos.'
			RAISERROR(@msgError,16,1)
			RETURN		
		END
		
		------------------------------------
		-- Verificar que la matricula exista
		------------------------------------
		IF NOT EXISTS(SELECT 'X' FROM escribano
				WHERE matricula = @matriculaEscribano)
		BEGIN
			SET @msgError = 'No existe la matricula ' + convert (varchar(20), @matriculaEscribano) + ' para escribanos.'
			RAISERROR(@msgError,16,1)
			RETURN		
		END

		------------------------------------
		-- Verificar que no exista clausura para las ubicaciones
		------------------------------------
		IF EXISTS(SELECT 'X' FROM dbo.Encomienda_Ubicaciones eu
					JOIN dbo.Ubicaciones_Clausuras uc ON uc.id_ubicacion=eu.id_ubicacion
					WHERE eu.id_encomienda=@id_encomienda
						AND uc.fecha_alta_clausura < GETDATE() AND ( uc.fecha_baja_clausura > GETDATE() OR uc.fecha_baja_clausura IS NULL))
		OR EXISTS(SELECT 'X' FROM dbo.Encomienda_Ubicaciones eu
					JOIN dbo.Encomienda_Ubicaciones_PropiedadHorizontal eph ON eph.id_encomiendaubicacion=eu.id_encomiendaubicacion
					JOIN dbo.Ubicaciones_PropiedadHorizontal_Clausuras uphc ON uphc.id_propiedadhorizontal=eph.id_propiedadhorizontal
					WHERE eu.id_encomienda=@id_encomienda
					AND uphc.fecha_alta_clausura < GETDATE() AND ( uphc.fecha_baja_clausura > GETDATE() OR uphc.fecha_baja_clausura IS NULL))
		BEGIN
			SET @msgError = 'Se pone en conocimiento que el domicilio declarado por usted presenta irregularidades. Por favor acerquese a nuestras oficinas ubicadas en TTE. GRAL. JUAN DOMINGO PERON 2941.'
			RAISERROR(@msgError,16,1)
			RETURN		
		END
		------------------------------------
		-- Verificar que no exista inhibiciones para las ubicaciones
		------------------------------------
		IF EXISTS((SELECT 'X' FROM dbo.Encomienda_Ubicaciones eu
					JOIN dbo.Ubicaciones_Inhibiciones ui ON ui.id_ubicacion=eu.id_ubicacion
					WHERE eu.id_encomienda=@id_encomienda AND ui.fecha_vencimiento > GETDATE()))
		OR EXISTS(SELECT 'X' FROM dbo.Encomienda_Ubicaciones eu
					JOIN dbo.Encomienda_Ubicaciones_PropiedadHorizontal eph ON eph.id_encomiendaubicacion=eu.id_encomiendaubicacion
					JOIN dbo.Ubicaciones_PropiedadHorizontal_Inhibiciones uphi ON uphi.id_propiedadhorizontal=eph.id_propiedadhorizontal
					WHERE eu.id_encomienda=@id_encomienda AND uphi.fecha_vencimiento > GETDATE())
		BEGIN
			SET @msgError = 'Se pone en conocimiento que el domicilio declarado por usted esta inhibido. Por favor acerquese a nuestras oficinas ubicadas en TTE. GRAL. JUAN DOMINGO PERON 2941.'
			RAISERROR(@msgError,16,1)
			RETURN		
		END

		IF EXISTS(SELECT 
					*
				FROM 
					Encomienda_Normativas encnor
					INNER JOIN TipoNormativa tnor ON encnor.id_tiponormativa = tnor.id
					INNER JOIN SSIT_Solicitudes sol ON encnor.id_encomienda = @id_encomienda
					LEFT JOIN DocumentosAdjuntos docadj ON sol.id_solicitud = docadj.id_solicitud AND tnor.id_tdocreq = docadj.id_tdocreq
				WHERE 
					sol.id_solicitud = @id_solicitud
					AND docadj.id_docadjunto IS NULL
		)
		BEGIN
			SET @msgError = 'Adjunte copia de la normativa declarada en la sección "Aplicar Normativa" de la encomienda  que autoriza el ejercicio de la actividad para esa zona y rubro/s ingresado/s'
			RAISERROR(@msgError,16,1)
			RETURN		
		END
	END
				
	-----------------------------------
	-- FIN DE LAS VALIDACIONES
	-- COMIENZO DEL CIRCUITO
	-----------------------------------

	--------------------------------------------------------------------------
	-- Se obtiene el circuito y los códigos de tarea dependiendo del circuito.
	--------------------------------------------------------------------------
	DECLARE 
		@cod_tarea_correccion_solicitud nvarchar(10),
		@cod_tarea_asignar_calificador_1 nvarchar(10),
		@cod_tarea_asignar_calificador_2 nvarchar(10),
		@cod_tarea_calificar_tramite nvarchar(10),
		@cod_tarea_calificar_tramite2  nvarchar(10),
		@cod_tarea_fin_tramite nvarchar(10)
	
	

	SELECT @id_circuito = rel.id_circuito ,@cod_circuito = cir.cod_circuito
	FROM ENG_Rel_Circuitos_TiposDeTramite rel
		INNER JOIN ENG_Circuitos cir ON rel.id_circuito = cir.id_circuito
	WHERE id_tipotramite = @id_tipotramite 
		AND id_tipoexpediente = @id_tipoexpediente 
		AND id_subtipoexpediente = @id_subtipoexpediente
		AND cir.version_circuito = 2
		
	SET @cod_tarea_correccion_solicitud = CONVERT(nvarchar,@id_circuito) + '25'
	SET @cod_tarea_asignar_calificador_1 = CONVERT(nvarchar,@id_circuito) + '08'
	SET @cod_tarea_asignar_calificador_2 = CONVERT(nvarchar,@id_circuito) + '09'
	SET @cod_tarea_calificar_tramite = CONVERT(nvarchar,@id_circuito) + '10'
	SET @cod_tarea_calificar_tramite2 = CONVERT(nvarchar,@id_circuito) + '01'
	SET @cod_tarea_fin_tramite = CONVERT(nvarchar,@id_circuito) + '29'

	--Para evitar de que apreten seguido el boton 	
	IF @id_estado_sol <> @p_id_estado
	BEGIN
		----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
		-- Si se esta confirmando (o anulando) una solicitud se debe poner en trámite ya que la misma va al sistema SGI.
		-- o si el estado de la solicitud es anulado se cierra la tarea.
		-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
		IF @p_id_estado = @id_estado_anulado
		BEGIN
			
			SET @id_tramitetarea  = 0 
			SET @id_resultado = 11	-- Solicitud Anulada
			
			--------------------------------------------------
			-- Busca la tarea 125 (Corrección de la solicitud)
			--------------------------------------------------
			SELECT TOP 1 @id_tramitetarea = tt.id_tramitetarea 
			FROM SGI_Tramites_Tareas tt
			INNER JOIN SGI_Tramites_Tareas_HAB tt_HAB ON tt.id_tramitetarea = tt_HAB.id_tramitetarea
			WHERE tt_HAB.id_solicitud = @id_solicitud 
			AND tt.id_tarea = dbo.Bus_id_tarea(@cod_tarea_correccion_solicitud)
			AND tt.FechaCierre_tramitetarea IS NULL
			ORDER BY tt.id_tramitetarea DESC

			--------------------------------------------------
			-- Cierra la tarea (Corrección de la solicitud)
			--------------------------------------------------
			IF @id_tramitetarea > 0 
				EXEC ENG_Finalizar_Tarea @id_tramitetarea, @id_resultado, 0, NULL, @id_tramitetarea_nuevo out
							
			-- Genera una tarea (fin trámite) 
			SET @id_tarea_nueva = dbo.Bus_id_tarea(@cod_tarea_fin_tramite)
			SET @id_tramitetarea  = 0
			EXEC ENG_Crear_Tarea  @id_solicitud, @id_tarea_nueva, @userid, @id_tramitetarea out
			IF @id_tramitetarea > 0 
				EXEC ENG_Finalizar_Tarea @id_tramitetarea, @id_resultado, 0, NULL, @id_tramitetarea_nuevo out
			
		END   
		ELSE IF @p_id_estado = @id_estado_confirmado
		BEGIN
			
			SET @id_estado = dbo.Bus_idEstadoSolicitud('ETRA') 
			
			--------------------------------------------------
			-- Se ha confirmado la solicitud
			--------------------------------------------------
			SELECT @cant_tareas = COUNT(*) 
			FROM SGI_Tramites_Tareas tt
			INNER JOIN SGI_Tramites_Tareas_HAB tt_HAB ON tt.id_tramitetarea = tt_HAB.id_tramitetarea
			WHERE tt_HAB.id_solicitud = @id_solicitud
			
			
			IF @cant_tareas <= 1
			BEGIN
			
				--------------------------------------
				-- Se confirma la primera vez
				--------------------------------------
				
				IF (@cod_circuito IN('SSP2','SCP2')) OR (@cod_circuito = 'ESPECIAL2' AND @id_subtipoexpediente = 3)
				BEGIN
					-----------------------------------
					-- Circuitos de simples
					-----------------------------------
					DECLARE 
						@html_email				nvarchar(max)
						,@asunto_email			nvarchar(300)
						,@direccion_email		nvarchar(100)
						,@ubicacion_email		nvarchar(300)
						,@revisaGerente			bit
						,@revisaSubGerente		bit
						,@Superficie			int
						,@Revisa				nvarchar(50)
						,@RevisaMenor			nvarchar(50)
						,@RevisaMayor			nvarchar(50)
						,@supLocal				decimal(8,2)
						,@sup					decimal(8,2)
						
					------------------------------------------------
					-- Preparación del email
					------------------------------------------------
					SET @html_email = N'<!DOCTYPE html>
						<html xmlns="http://www.w3.org/1999/xhtml">
						<head>
							<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
							<title>AGC - Habilitaciones</title></head>
						<body style="margin: 0px; padding: 0px; font-family: ''Segoe UI'', Verdana, Helvetica, Sans-Serif;">
							
							<div style="width: 1000px">
								<header>
									<img src="http://www.dghpsh.agcontrol.gob.ar/SSIT/Mailer/img/header.png" style="width: 1000px; height: 148px; max-height: 148px" />
								</header>
								<div style="padding: 20px 20px 20px 50px; min-height: 500px;">
									<h3><span id="lblTitulo" class="color:#333;">Sr. Contribuyente:</span></h3>
									<table cellspacing="0" id="ContentPlaceHolder1_FormView1">
										<tr>
											<td colspan="2">
												<div style="padding: 0px 20px 20px 20px">
													Su solicitud se encuentra ingresada y pendiente de revisi&oacute;n. 
													Se le env&iacute;a adjunto un documento con un c&oacute;digo QR para comenzar con la actividad comercial. 
													Se recuerda que el mismo no implica habilitaci&oacute;n otorgada, y que ser&aacute; notificado del estado de su solicitud. 
												</div>
											</td>
										</tr>
									</table>
								</div>
								<footer>
									<img src="http://www.dghpsh.agcontrol.gob.ar/SSIT/Mailer/img/footer.png" style="width: 1000px; height: 95px; max-height: 95px" />
								</footer>
							</div>
							
						</body>
						</html>
						'
					
					SELECT TOP 1 @ubicacion_email = dbo.Encomienda_Solicitud_DireccionesPartidasPlancheta(eu.id_encomienda, eu.id_ubicacion)
					FROM Encomienda_Ubicaciones eu
					WHERE eu.id_encomienda = @id_encomienda
					
					SET @asunto_email = 'Solicitud de habilitación N°: ' + convert(nvarchar,@id_solicitud) + ' - ' + @ubicacion_email
					SELECT @direccion_email	 = mem.email
					FROM 
						aspnet_Membership mem
						INNER JOIN SSIT_solicitudes sol ON mem.userid = sol.CreateUser
					WHERE
						sol.id_solicitud = @id_solicitud
					
					-- Fin de preparación del email
					
					-----------------------------------------------------------------------
					-- evaluacion de condiciones para saber a que tarea de asignación va
					-- si a la del SubGerente o a la del Gerente
					-----------------------------------------------------------------------
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
					
					
					-------------------------------------------------------------
					-- Obtiene la única tarea que hay (Solicitud de habilitación)
					-------------------------------------------------------------
					
					SELECT TOP 1 @id_tramitetarea = tt.id_tramitetarea 
					FROM SGI_Tramites_Tareas tt
					INNER JOIN SGI_Tramites_Tareas_HAB tt_HAB ON tt.id_tramitetarea = tt_HAB.id_tramitetarea
					WHERE tt_HAB.id_solicitud = @id_solicitud 
						AND tt.FechaCierre_tramitetarea IS NULL

					----------------------------------------------
					-- Cierra la tarea (Solicitud de Habilitación)
					----------------------------------------------
					IF @id_tramitetarea > 0 
						EXEC ENG_Finalizar_Tarea  @id_tramitetarea, 0, 0, @userid, @id_tramitetarea_nuevo out
						
					-----------------------------------------------------------
					-- Si solo hay una tarea sería la Solicitud de Habilitación
					-- entonces se crea la Asignación del calificador 
					-----------------------------------------------------------
					IF @revisaGerente = 1
						SET @id_tarea_nueva = dbo.Bus_id_tarea(@cod_tarea_asignar_calificador_1)
					ELSE
						SET @id_tarea_nueva = dbo.Bus_id_tarea(@cod_tarea_asignar_calificador_2)
					
					SET @id_tramitetarea = 0
					EXEC ENG_Crear_Tarea @id_solicitud, @id_tarea_nueva, @userid, @id_tramitetarea out
					
					------------------------------------------------------------------------
					-- Se establece la fecha de librado al uso si es que no tiene normativa
					------------------------------------------------------------------------
					IF NOT EXISTS(SELECT 'X' FROM Encomienda_Normativas WHERE id_encomienda = @id_encomienda)
					BEGIN
						SET @FechaLibradoAlUso = GETDATE()
						UPDATE SSIT_Solicitudes SET FechaLibrado = @FechaLibradoAlUso WHERE id_solicitud = @id_solicitud
					END
					
					-------------------------------------
					-- Se inserta el mail con el aviso
					-------------------------------------
					-- El mail no se envía cuando tiene normativa ya que el Qr no se disponibiliza
					IF NOT EXISTS(SELECT 'X' FROM Encomienda_Normativas WHERE id_encomienda = @id_encomienda)
					BEGIN
						EXEC Envio_Mail_insert 5,@id_solicitud,3,10,@direccion_email,@asunto_email,@html_email
					END
				END
				ELSE 
				BEGIN
					-------------------------------------------------------------
					-- Circuitos Espaciales y Esparcimiento (ESPECIAL2 ESPAR2)
					-------------------------------------------------------------
					
					-- Obtiene la única tarea que hay (Solicitud de habilitación)
					-------------------------------------------------------------
					SELECT TOP 1 @id_tramitetarea = tt.id_tramitetarea 
					FROM SGI_Tramites_Tareas tt
					INNER JOIN SGI_Tramites_Tareas_HAB tt_HAB ON tt.id_tramitetarea = tt_HAB.id_tramitetarea
					WHERE tt_HAB.id_solicitud = @id_solicitud 
						AND tt.FechaCierre_tramitetarea IS NULL

					----------------------------------------------
					-- Cierra la tarea (Solicitud de Habilitación)
					----------------------------------------------
					IF @id_tramitetarea > 0 
						EXEC ENG_Finalizar_Tarea   @id_tramitetarea, 0, 0, @userid, @id_tramitetarea_nuevo out
						
					-----------------------------------------------------------
					-- Si solo hay una tarea sería la Solicitud de Habilitación
					-- entonces se crea la Asignación del calificador 
					-----------------------------------------------------------
					SET @id_tarea_nueva = dbo.Bus_id_tarea(@cod_tarea_asignar_calificador_1)
					
					SET @id_tramitetarea = 0
					EXEC ENG_Crear_Tarea @id_solicitud, @id_tarea_nueva, @userid, @id_tramitetarea out
					
				END
					
			END
			ELSE
			BEGIN
				
				SET @id_tramitetarea  = 0
				----------------------------------------------
				-- Busca la tarea (Corrección de la solicitud)
				----------------------------------------------
				SELECT TOP 1 @id_tramitetarea = tt.id_tramitetarea 
				FROM SGI_Tramites_Tareas tt
				INNER JOIN SGI_Tramites_Tareas_HAB tt_HAB ON tt.id_tramitetarea = tt_HAB.id_tramitetarea
				INNER JOIN ENG_Tareas tar ON tt.id_tarea = tar.id_tarea
				WHERE tt_HAB.id_solicitud = @id_solicitud 
					AND tar.cod_tarea = @cod_tarea_correccion_solicitud
					AND tt.FechaCierre_tramitetarea IS NULL

				----------------------------------------------
				-- Cierra la tarea (Corrección de la solicitud)
				----------------------------------------------
				IF @id_tramitetarea > 0 
				BEGIN
					EXEC ENG_Finalizar_Tarea  @id_tramitetarea, 0, 0, NULL, @id_tramitetarea_nuevo out
				
					SET @id_tramitetarea  = 0
					
					-- Busca la tarea (Calificar trámite) para saber el usuario calificador que tenía
					SELECT TOP 1
							@usuario_calif_asig = tt.UsuarioAsignado_tramitetarea
							,@id_tarea_nueva = tt.id_tarea
					FROM SGI_Tramites_Tareas tt
					INNER JOIN SGI_Tramites_Tareas_HAB tt_HAB ON tt.id_tramitetarea = tt_HAB.id_tramitetarea
					WHERE tt_HAB.id_solicitud = @id_solicitud 
						AND tt.id_tarea in (dbo.Bus_id_tarea(@cod_tarea_calificar_tramite), dbo.Bus_id_tarea(@cod_tarea_calificar_tramite2))
						AND tt.FechaCierre_tramitetarea IS NOT NULL
					ORDER BY tt.id_tramitetarea DESC

					IF @usuario_calif_asig IS NOT NULL 
					BEGIN
						DECLARE @id_subtipoexpediente_encomienda int,
							@nuevo_circuito bit
						SET @nuevo_circuito= 0
						--si es rectificatoria y son de distinto tipo que la solicitud se crea la etapa de asignacion de calificador 
						-- y se cambia el tipo de la solicitud
						IF @id_tipotramite = @id_tipotramite_rectificatoria
						BEGIN
							SELECT
								@id_subtipoexpediente_encomienda = id_subtipoexpediente
							FROM 
								Encomienda
							WHERE 
								id_encomienda = @id_encomienda	
							IF @id_subtipoexpediente <> @id_subtipoexpediente_encomienda
								SET @nuevo_circuito = 1
						END
						IF @nuevo_circuito = 0
						BEGIN
							-- Genera una tarea (Calificar trámite) con el usuario calificador que tenía la anterior
							--tomo la ultima que se encontro ya que hay calificar y calificar2
							--SET @id_tarea_nueva = dbo.Bus_id_tarea(@cod_tarea_calificar_tramite)
							SET @id_tramitetarea  = 0
							EXEC ENG_Crear_Tarea  @id_solicitud, @id_tarea_nueva, @userid, @id_tramitetarea out
							IF @id_tramitetarea > 0
								EXEC ENG_Asignar_Tarea @id_tramitetarea, @usuario_calif_asig
						END
						ELSE
						BEGIN
							SELECT @id_circuito = id_circuito
							FROM ENG_Rel_Circuitos_TiposDeTramite
							WHERE id_tipotramite = @id_tipotramite 
								AND id_tipoexpediente = @id_tipoexpediente 
								AND id_subtipoexpediente = @id_subtipoexpediente_encomienda
									
							SET @cod_tarea_asignar_calificador_1 = CONVERT(nvarchar,@id_circuito) + '09'
							--actualizo el suptipo de la solicitud
							UPDATE SSIT_Solicitudes
							SET id_subtipoexpediente = @id_subtipoexpediente_encomienda
							WHERE id_solicitud = @id_solicitud							
							
							-- entonces se crea la Asignación del calificador 
							SET @id_tarea_nueva = dbo.Bus_id_tarea(@cod_tarea_asignar_calificador_1)
							SET @id_tramitetarea = 0
							EXEC ENG_Crear_Tarea @id_solicitud, @id_tarea_nueva, @userid, @id_tramitetarea out
						END
					END
					ELSE
					BEGIN
						-- Se crea la Asignación del calificador 
						SET @id_tarea_nueva = dbo.Bus_id_tarea(@cod_tarea_asignar_calificador_1)
						SET @id_tramitetarea = 0
						EXEC ENG_Crear_Tarea @id_solicitud, @id_tarea_nueva, @userid, @id_tramitetarea out
					END
				END
				
			END
		
			
		END  

		
		IF  @id_estado = dbo.Bus_idEstadoSolicitud('ETRA') 
		BEGIN
		
			UPDATE DocumentosAdjuntos
			SET puede_eliminar = 0
			WHERE id_solicitud = @id_solicitud
			AND puede_eliminar = 1
			AND origen = 'SSIT'
			
			----------------------------------------------
			-- Pone todas las observaciones en historicas
			----------------------------------------------
			UPDATE
				SGI_Tarea_Calificar_ObsDocs
			SET 
				Actual = 0
			WHERE 
				Actual = 1 
				AND id_ObsGrupo IN(
								SELECT 
									id_ObsGrupo 
								FROM 
									SGI_Tarea_Calificar_ObsGrupo obsgru
									INNER JOIN SGI_Tramites_Tareas_HAB tt_hab ON obsgru.id_tramitetarea = tt_hab.id_tramitetarea
								WHERE
									tt_hab.id_solicitud = @id_solicitud
								)
		END 
		
		UPDATE 
			SSIT_Solicitudes
		SET 
			id_estado = @id_estado,
			NroExpediente = IsNull(@NroExpediente,NroExpediente),
			matriculaEscribano = @matriculaEscribano, 
			LastUpdateDate = GETDATE(),
			LastUpdateUser = @userid
		WHERE 
			id_solicitud = @id_solicitud
	END
END
GO
/****** Object:  UserDefinedFunction [dbo].[SGI_DireccionesPartidasPlancheta]    Script Date: 11/07/2016 15:42:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER FUNCTION [dbo].[SGI_DireccionesPartidasPlancheta]
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
			
			
			IF @id_tipoubicacion <> 0  -- Parcela Com�n
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
		
		
		IF @id_tipoubicacion <> 0  -- Parcela Com�n
			SET @Result = @Result + IsNull(' ' + @DescripcionUbicacionEspecial ,'')
		
		
		IF LEN(IsNull(@deptoLocal_ubicacion,'')) > 0
			SET @Result = @Result + ' ' + @deptoLocal_ubicacion

	END


	-- Return the result of the function
	RETURN @Result

END
GO