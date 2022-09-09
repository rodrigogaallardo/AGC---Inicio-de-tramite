SET NOCOUNT ON
DECLARE 
	@id_tipdocsis int
	,@id_certificado int
	,@NroTramite int
	,@CreateDate datetime
	,@CreateUser uniqueidentifier
	,@id_solicitud int
	,@id_agrupamiento int
	,@formato_archivo nvarchar(20)
	,@cantReg int
	,@cantReg_borrar int
	,@start_time datetime
	,@stop_time datetime
	,@dif int
	,@documento varbinary(max)
	,@tdocreq_detalle nvarchar(50)

DECLARE @tbl_borrar TABLE(id_certificado int)
SELECT @id_tipdocsis =  id_tipdocsis FROM TiposDeDocumentosSistema WHERE cod_tipodocsis = 'PLANCHETA_HABILITACION'
SET @cantReg = 0
SET @start_time  = GETDATE()
SET @tdocreq_detalle = NULL

DECLARE cur CURSOR FAST_FORWARD FOR
SELECT 
	id_certificado,
	NroTramite,
	CreateDate,
	CreateUser
FROM
	Certificados
WHERE
	 TipoTramite = 5		-- Plancheta de Habilitación
ORDER BY 
	NroTramite


OPEN cur
FETCH NEXT FROM cur INTO  @id_certificado 
							,@NroTramite 
							,@CreateDate 
							,@CreateUser 

WHILE @@FETCH_STATUS = 0
BEGIN
	
	DECLARE 
		@id_file int,
		@FileName nvarchar(100),
		@id_docadjunto_sol int ,
		@hubo_error bit,
		@Md5 binary

	SELECT @documento = certificado FROM Certificados WHERE id_certificado = @id_certificado
	SET @id_solicitud = @NroTramite

	SET @hubo_error = 0
	SET @formato_archivo = 'pdf'

	DECLARE @rowid uniqueidentifier 
	SET @rowid = NEWID()
	SELECT @id_file = MAX(id_file) FROM Files 
	SET @id_file = @id_file +1
	SET @FileName = 'Plancheta-' + CAST(@id_file as nvarchar) + '.' + @formato_archivo

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
	
	END

	IF @hubo_error = 0
	BEGIN
		INSERT INTO @tbl_borrar VALUES(@id_certificado)
	END

	IF @cantReg % 100 = 0
	BEGIN
		SET @stop_time = GETDATE()
		
		SET @dif = ABS(DATEDIFF(mi,@stop_time ,@start_time))
		PRINT(convert(nvarchar,@cantReg) + ' documentos migrados. Tiempo: ' + convert(nvarchar,@dif) + ' minuto/s.')
	END
	

	FETCH NEXT FROM cur INTO  @id_certificado 
								,@NroTramite 
								,@CreateDate 
								,@CreateUser 


END
CLOSE cur
DEALLOCATE cur


DELETE FROM Certificados WHERE id_certificado IN(SELECT id_certificado FROM @tbl_borrar)

