SET NOCOUNT ON
DECLARE 
	@id_docadjunto int
	,@id_tdocreq int
	,@id_encomienda int
	,@tdocreq_detalle nvarchar(50)
	,@documento varbinary(max)
	,@CreateDate datetime
	,@CreateUser nvarchar(50)
	,@puede_eliminar bit
	,@origen nvarchar(15)
	,@id_solicitud int
	,@id_agrupamiento int
	,@formato_archivo nvarchar(20)
	,@cantReg int
	,@cantReg_borrar int
	,@start_time datetime
	,@stop_time datetime
	,@dif int

DECLARE @tbl_borrar TABLE(id_docadjunto int)

SET @cantReg = 0
SET @start_time  = GETDATE()

DECLARE cur CURSOR FAST_FORWARD FOR
SELECT 
	id_docadjunto,
	id_tdocreq,
	id_encomienda,
	tdocreq_detalle,
	CreateDate,
	CreateUser,
	puede_eliminar,
	origen,
	id_solicitud,
	id_agrupamiento
FROM
	DocumentosAdjuntos
WHERE
	origen ='SGI'
ORDER BY 
	id_solicitud


OPEN cur
FETCH NEXT FROM cur INTO @id_docadjunto 
	,@id_tdocreq 
	,@id_encomienda 
	,@tdocreq_detalle 
	,@CreateDate 
	,@CreateUser 
	,@puede_eliminar 
	,@origen 
	,@id_solicitud 
	,@id_agrupamiento 

WHILE @@FETCH_STATUS = 0
BEGIN
	
	DECLARE 
		@id_file int,
		@FileName nvarchar(100),
		@id_docadjunto_sol int ,
		@hubo_error bit,
		@Md5 binary

	SELECT @documento = documento FROM DocumentosAdjuntos WHERE id_docadjunto = @id_docadjunto
	SET @hubo_error = 0
	SET @formato_archivo = NULL
	SELECT @formato_archivo = formato_archivo  FROM tiposDeDocumentosRequeridos WHERE id_tdocreq = @id_tdocreq
	SET @formato_archivo = ISNULL(@formato_archivo,'pdf')

	DECLARE @rowid uniqueidentifier 
	SET @rowid = NEWID()
	SELECT @id_file = MAX(id_file) FROM Files 
	SET @id_file = @id_file +1
	SET @FileName = 'Documento-' + CAST(@id_file as nvarchar) + '.' + @formato_archivo

	SET @Md5 = master.sys.fn_repl_hash_binary(@documento)

	SET @cantReg = @cantReg + 1
	
	INSERT INTO Files(
		rowid 
		,id_file 
		,content_file 
		,datos_documento_oficial 
		,CreateDate 
		,CreateUser 
		,UpdateDate 
		,UpdateUser 
		,FileName 
		,Md5 
	)
	VALUES
	(
		@rowid
		,@id_file 
		,@documento 
		,NULL 
		,@CreateDate 
		,@CreateUser 
		,NULL 
		,NULL
		,@FileName 
		,@Md5
	)
	

	IF @@ROWCOUNT <> 1
		SET @hubo_error = 1
	

	IF @hubo_error = 0
	BEGIN
		DECLARE @id_tramitetarea int
		SET @id_tramitetarea = 0

		SELECT TOP 1 @id_tramitetarea = tt.id_tramitetarea, 
					 @CreateUser = tt.UsuarioAsignado_tramitetarea
		FROM 
			SGI_Tramites_tareas tt 
			INNER JOIN sgi_tramites_tareas_HAB tt_hab ON tt.id_tramitetarea = tt_hab.id_tramitetarea 
			INNER JOIN ENG_Tareas tar ON tt.id_tarea = tar.id_tarea
		WHERE 
			tt_hab.id_solicitud = @id_solicitud
			-- Tareas de calificacion y visado de trámites
			AND tar.cod_tarea IN(110,210,510,1110,1210,1213,1310,1301,1410,1401,310,610)
		ORDER BY
			tt.id_Tramitetarea 

		SET @id_tramitetarea = ISNULL(@id_tramitetarea,0)
		If @id_tramitetarea = 0
			SET @hubo_error = 1
	
		IF @hubo_error = 0
		BEGIN
			EXEC @id_docadjunto_sol = Id_Nuevo 'SGI_Tarea_Documentos_Adjuntos'
			INSERT INTO SGI_Tarea_Documentos_Adjuntos(
				id_doc_adj,
				id_tramitetarea,
				tdoc_adj_detalle,
				id_file,
				CreateDate,
				CreateUser,
				LastUpdateDate,
				LastUpdateUser,
				nombre_archivo,
				id_tdocreq
			)
			VALUES
			(
				@id_docadjunto_sol 
				,@id_tramitetarea 
				,@tdocreq_detalle 
				,@id_file 
				,@CreateDate 
				,@CreateUser 
				,NULL 
				,NULL
				,@FileName 
				,@id_tdocreq 
			)
			IF @@ROWCOUNT <> 1
				SET @hubo_error = 1
		END
	END

	IF @hubo_error = 0
	BEGIN
		INSERT INTO @tbl_borrar VALUES(@id_docadjunto)
	END

	IF @cantReg % 100 = 0
	BEGIN
		SET @stop_time = GETDATE()
		
		SET @dif = DATEDIFF(mi,@stop_time ,@start_time)
		PRINT(convert(nvarchar,@cantReg) + ' documentos migrados. Tiempo: ' + convert(nvarchar,@dif) + ' minuto/s.')
	END
	
	FETCH NEXT FROM cur INTO @id_docadjunto 
		,@id_tdocreq 
		,@id_encomienda 
		,@tdocreq_detalle 
		,@CreateDate 
		,@CreateUser 
		,@puede_eliminar 
		,@origen 
		,@id_solicitud 
		,@id_agrupamiento 




END
CLOSE cur
DEALLOCATE cur

DELETE FROM DocumentosAdjuntos WHERE id_docadjunto IN(SELECT id_docadjunto FROM @tbl_borrar)


IF (SELECT COUNT(*) FROM DocumentosAdjuntos) = 0
BEGIN
	DROP TABLE AGC_FILES.dbo.DocumentosAdjuntos 
	
	IF  EXISTS (SELECT * FROM sys.synonyms WHERE name = N'DocumentosAdjuntos')
		DROP SYNONYM [dbo].[DocumentosAdjuntos]
END
GO

UPDATE files 
SET md5 = master.sys.fn_repl_hash_binary(content_file)
WHERE md5 IS NULL
GO