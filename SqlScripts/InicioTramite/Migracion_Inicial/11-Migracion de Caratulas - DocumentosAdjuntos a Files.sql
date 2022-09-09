SET NOCOUNT ON
DECLARE 
	@id_tipdocsis int,
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
SELECT @id_tipdocsis =  id_tipdocsis FROM TiposDeDocumentosSistema WHERE cod_tipodocsis = 'CARATULA_HABILITACION'
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
	 id_tdocreq = 29
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
	SET @FileName = 'Caratula-' + CAST(@id_file as nvarchar) + '.' + @formato_archivo

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
	
	
	EXEC @id_docadjunto_sol = Id_Nuevo 'SSIT_DocumentosAdjuntos'
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
		,UpdateDate 
		,UpdateUser 
		,nombre_archivo 
	)
	VALUES
	(
		@id_docadjunto_sol 
		,@id_solicitud 
		,0 
		,@tdocreq_detalle 
		,@id_tipdocsis 
		,@id_file 
		,1		-- generado por sistema 
		,@CreateDate 
		,@CreateUser 
		,NULL 
		,NULL
		,@FileName 
	)
	IF @@ROWCOUNT <> 1
		SET @hubo_error = 1
	

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

