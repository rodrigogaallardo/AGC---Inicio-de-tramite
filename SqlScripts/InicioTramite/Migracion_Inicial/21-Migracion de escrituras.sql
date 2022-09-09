

SET NOCOUNT ON
DECLARE 
	@id_tipdocsis int
	,@id_certificado int
	,@NroTramite int
	,@CreateDate datetime
	,@id_actanotarial int
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
SELECT @id_tipdocsis =  id_tipdocsis FROM TiposDeDocumentosSistema WHERE cod_tipodocsis = 'ACTUACION_NOTARIAL'
SET @cantReg = 0
SET @start_time  = GETDATE()
SET @tdocreq_detalle = NULL

DECLARE cur CURSOR FAST_FORWARD FOR
SELECT 
	id_certificado,
	NroTramite,
	CreateDate
FROM
	Certificados
WHERE
	 TipoTramite = 3		-- Acta notarial
ORDER BY 
	NroTramite


OPEN cur
FETCH NEXT FROM cur INTO  @id_certificado 
							,@NroTramite 
							,@CreateDate 


WHILE @@FETCH_STATUS = 0
BEGIN
	
	DECLARE 
		@id_file int,
		@FileName nvarchar(100),
		@id_docadjunto_sol int ,
		@hubo_error bit,
		@Md5 binary

	SELECT @documento = certificado FROM Certificados WHERE id_certificado = @id_certificado
	SET @id_actanotarial = @NroTramite

	SET @hubo_error = 0
	SET @formato_archivo = 'pdf'

	DECLARE @rowid uniqueidentifier 
	SET @rowid = NEWID()
	SELECT @id_file = MAX(id_file) FROM Files 
	SET @id_file = @id_file +1
	SET @FileName = 'Acta_Notarial-' + CAST(@id_file as nvarchar) + '.' + @formato_archivo

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
		,'WS-CECABA' 
		,NULL 
		,NULL
		,@FileName 
		,@Md5
	)
	

	IF @@ROWCOUNT <> 1
		SET @hubo_error = 1
	
	IF @hubo_error = 0
	BEGIN
		
		UPDATE wsEscribanos_actanotarial SET id_file = @id_file WHERE id_actanotarial = @id_actanotarial
		
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
		PRINT(convert(nvarchar,@cantReg) + ' actas migradas. Tiempo: ' + convert(nvarchar,@dif) + ' minuto/s.')
	END
	

	FETCH NEXT FROM cur INTO  @id_certificado 
								,@NroTramite 
								,@CreateDate 


END
CLOSE cur
DEALLOCATE cur

DELETE FROM Certificados WHERE id_certificado IN(SELECT id_certificado FROM @tbl_borrar)
GO
IF (SELECT COUNT(*) FROM Certificados WHERE TipoTramite = 3) < 10
BEGIN
	DELETE FROM Certificados WHERE TipoTramite = 3
	DELETE FROM Rel_TipoTramite_Roles WHERE TipoTramite = 3
	DELETE FROM TipoTramiteCertificados WHERE tipotramite = 3
END

