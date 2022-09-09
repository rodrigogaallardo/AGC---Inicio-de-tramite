
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CAA_CrearTramite_Encomienda]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CAA_CrearTramite_Encomienda]
GO

CREATE PROCEDURE [dbo].[CAA_CrearTramite_Encomienda]
(
	@id_encomienda int,
	@CodigoSeguridad nvarchar(10), 
	@userid uniqueidentifier,
	@id_caa int output
)
AS
BEGIN

	-- Declaración de variables 
	DECLARE 
		@msgError nvarchar(1000)
		,@id_estado int
		,@CodigoSeguridadCAA nvarchar(10)
		,@id_tipocertificado int
		,@id_caa_duplicado int
		,@id_tramitetarea int
		,@id_tarea int
		,@id_resultado int
		,@id_tramitetarea_nuevo int 
		,@iniciado_x_AGC bit
		,@id_solicitud_AGC int
	
	SET @iniciado_x_AGC = 0
	
	IF NOT EXISTS(SELECT 'X' FROM Encomienda WHERE id_encomienda = @id_encomienda)
	BEGIN
		SET @msgError = 'La Encomienda Nº ' + CONVERT(nvarchar,@id_encomienda) + ' no existe.'
		RAISERROR(@msgError,16,1)
		RETURN -1
	END
	
	IF NOT EXISTS(SELECT 'X' FROM Encomienda WHERE id_encomienda = @id_encomienda AND CodigoSeguridad = @CodigoSeguridad)
	BEGIN
		SET @msgError = 'El Código de Seguridad de la encomienda no es válido.' 
		RAISERROR(@msgError,16,1)
		RETURN -1
	END

	IF NOT EXISTS(SELECT 'X' FROM Encomienda WHERE id_encomienda = @id_encomienda AND id_estado = dbo.Encomienda_Bus_idEstadoSolicitud('APROC'))
	BEGIN
		SET @msgError = 'Para poder inicar un trámite la Encomienda se debe encontrar Aprobada por el consejo al que pertence el Profesional que la confeccionó.' 
		RAISERROR(@msgError,16,1)
		RETURN -1
	END
	
	SELECT @id_caa_duplicado = id_caa FROM CAA_Solicitudes WHERE id_encomienda = @id_encomienda AND id_estado <> 20 
	IF ISNULL(@id_caa_duplicado,0) > 0
	BEGIN
		SET @msgError = 'Ya existe una solicitud basada en esta encomienda, la misma es la Nº ' + CONVERT(nvarchar,@id_caa_duplicado) +'.'
		RAISERROR(@msgError,16,1)
		RETURN -1
	END
	
	-- Verifica si el usuario tiene el rol del servicio de interface de AGC y establece la variable @iniciado_x_AGC
	IF EXISTS(
		SELECT 'X' 
		FROM 
			aspnet_UsersInRoles usurol
			INNER JOIN aspnet_Roles rol ON usurol.roleid = rol.roleid
		WHERE
			usurol.userid = @userid
			AND rol.loweredrolename = 'servicio_interface_agc'
		)
	BEGIN
		SET @iniciado_x_AGC = 1
	END
	
	-- Se obtiene el tipo de trámite (Tipo de Certificado de APRA)
	SELECT @id_tipocertificado = MAX(dbo.Devolver_ImpactoAmbiental(rub.id_rubro,encrub.SuperficieHabilitar))
	FROM 
		Encomienda_Rubros encrub
		LEFT JOIN Rubros rub ON encrub.cod_rubro = rub.cod_rubro
	WHERE id_encomienda = @id_encomienda
	
	IF IsNull(@id_tipocertificado,0) = 5	-- No Permitido en la Ciudad
		SET @id_tipocertificado = 3		-- Sujeto a Categorización

		
	IF IsNull(@id_tipocertificado,0) = 0
		SET @id_tipocertificado = 1		-- Sin Relevante Efecto
	

	SELECT 
		@id_solicitud_AGC = sol.id_solicitud
	FROM 
		Encomienda sol
	WHERE
		sol.id_encomienda = id_encomienda

	SET @id_solicitud_AGC  = ISNULL(@id_solicitud_AGC,999999)

	IF @id_tipocertificado IN(1,2) AND @iniciado_x_AGC <> 1 AND @id_solicitud_AGC > 300000	-- Si es un SRE o SRE c/c no iniciado por Servicio (AGC)
	BEGIN
		SET @msgError = 'Los CAA de tipo SRE y SRE c/c cuyo destino es una habilitación, deben ser iniciados por el sistema SSIT de la AGC. '
		RAISERROR(@msgError,16,1)
		RETURN -1
	END
	
	
	SET @id_estado = 0  -- Incompleto
	SET @CodigoSeguridadCAA  = convert(nvarchar,ceiling(RAND() * 9)) + CHAR(ceiling(RAND() * 25 + 65)) + convert(nvarchar,ceiling(RAND() * 9)) + CHAR(ceiling(RAND() * 25 + 65)) 

	EXEC @id_caa = Id_Nuevo 'CAA_Solicitudes'
	
	INSERT INTO CAA_Solicitudes
	(
		id_caa,
		id_encomienda,
		id_tipotramite,
		id_paquete,
		CodigoSeguridad,
		FechaIngreso,
		id_tipocertificado,
		ZonaDeclarada,
		id_estado,
		NroCertificado,
		FechaVencCertificado,
		Observaciones_rubros,
		iniciado_x_AGC,
		CreateDate,
		CreateUser,
		LastUpdateDate,
		LastUpdateUser
	)
	SELECT
		@id_caa,
		@id_encomienda,
		1,
		@id_caa,
		@CodigoSeguridadCAA,
		GETDATE(),
		@id_tipocertificado,
		ZonaDeclarada,
		@id_estado,
		0,
		NULL,
		Observaciones_rubros,
		@iniciado_x_AGC,
		GETDATE(),
		@userid,
		GETDATE(),			-- para que no falle el trigger del historial de estados
		@userid
	FROM
		Encomienda
	WHERE
		id_encomienda = @id_encomienda
	
	
	------------------------------------------------
	-- Ubicaciones
	------------------------------------------------
	DECLARE 
		@id_caaubicacion int 
		,@id_ubicacion int
		,@id_subtipoubicacion int
		,@local_subtipoubicacion nvarchar(25)
		,@deptoLocal_caaubicacion nvarchar(50)
		,@id_caaprophorizontal int
		,@id_propiedadhorizontal int
		,@id_caapuerta int 
		,@codigo_calle int
		,@Nombre_calle nvarchar(100)
		,@NroPuerta int
		,@id_zonaplaneamiento int

	DECLARE curUbicaciones CURSOR FAST_FORWARD FOR
	SELECT 
		encubic.id_ubicacion,
		encubic.id_subtipoubicacion,
		encubic.local_subtipoubicacion,
		encubic.deptoLocal_encomiendaubicacion,
		encubic.id_zonaplaneamiento
	FROM
		Encomienda_Ubicaciones encubic
	WHERE
		encubic.id_encomienda = @id_encomienda
	
	OPEN curUbicaciones
	FETCH NEXT FROM curUbicaciones INTO @id_ubicacion, 
										@id_subtipoubicacion, 
										@local_subtipoubicacion, 
										@deptoLocal_caaubicacion,
										@id_zonaplaneamiento

	WHILE @@FETCH_STATUS = 0
	BEGIN
	
		EXEC @id_caaubicacion = Id_Nuevo 'CAA_Ubicaciones'
		
		INSERT INTO CAA_Ubicaciones(
			id_caaubicacion 
			,id_caa 
			,id_ubicacion 
			,id_subtipoubicacion 
			,local_subtipoubicacion 
			,deptoLocal_caaubicacion 
			,id_zonaplaneamiento
			,CreateDate 
			,CreateUser 
		)
		VALUES
		(
			@id_caaubicacion 
			,@id_caa 
			,@id_ubicacion 
			,@id_subtipoubicacion 
			,@local_subtipoubicacion 
			,@deptoLocal_caaubicacion 
			,@id_zonaplaneamiento
			,GETDATE() 
			,@userid
		)
		
		
		-----------------------------------------------------
		-- Propiedades Horizontales
		----------------------------------------------------
		DECLARE curUbicacionesPhor CURSOR FAST_FORWARD FOR
		SELECT 
			encphor.id_propiedadhorizontal
		FROM
			Encomienda_Ubicaciones encubic
			INNER JOIN Encomienda_Ubicaciones_PropiedadHorizontal encphor ON encubic.id_encomiendaubicacion = encphor.id_encomiendaubicacion
		WHERE
			encubic.id_encomienda = @id_encomienda
			AND encubic.id_ubicacion = @id_ubicacion
		
		OPEN curUbicacionesPhor
		FETCH NEXT FROM curUbicacionesPhor INTO @id_propiedadhorizontal
		
		WHILE @@FETCH_STATUS = 0
		BEGIN
			
			EXEC @id_caaprophorizontal = Id_Nuevo 'CAA_Ubicaciones_PropiedadHorizontal'
			
			INSERT INTO CAA_Ubicaciones_PropiedadHorizontal
			(
				id_caaprophorizontal 
				,id_caaubicacion 
				,id_propiedadhorizontal 
			)
			VALUES
			(
				@id_caaprophorizontal 
				,@id_caaubicacion 
				,@id_propiedadhorizontal 
			)
			
			FETCH NEXT FROM curUbicacionesPhor INTO @id_propiedadhorizontal
			
		END
		CLOSE curUbicacionesPhor
		DEALLOCATE curUbicacionesPhor
		
		
		
		-----------------------------------------------------
		-- Puertas
		----------------------------------------------------
		DECLARE curUbicacionesPuertas CURSOR FAST_FORWARD FOR
		SELECT
			codigo_calle,
			nombre_calle,
			NroPuerta
		FROM
			Encomienda_Ubicaciones encubic
			INNER JOIN Encomienda_Ubicaciones_Puertas encpuer ON encubic.id_encomiendaubicacion = encpuer.id_encomiendaubicacion
		WHERE 
			encubic.id_encomienda = @id_encomienda
		
		OPEN curUbicacionesPuertas
		FETCH NEXT FROM curUbicacionesPuertas INTO @codigo_calle, @Nombre_calle, @NroPuerta
		
		WHILE @@FETCH_STATUS = 0
		BEGIN
			

			EXEC @id_caapuerta = Id_Nuevo 'CAA_Ubicaciones_Puertas'
			
			INSERT INTO CAA_Ubicaciones_Puertas(
				id_caapuerta 
				,id_caaubicacion 
				,codigo_calle 
				,Nombre_calle 
				,NroPuerta 
			)
			VALUES
			(
				@id_caapuerta 
				,@id_caaubicacion 
				,@codigo_calle 
				,@Nombre_calle 
				,@NroPuerta 
			)

			FETCH NEXT FROM curUbicacionesPuertas INTO @codigo_calle, @Nombre_calle, @NroPuerta
			
		END
		CLOSE curUbicacionesPuertas
		DEALLOCATE curUbicacionesPuertas
		
		
		

		FETCH NEXT FROM curUbicaciones INTO @id_ubicacion, 
											@id_subtipoubicacion, 
											@local_subtipoubicacion, 
											@deptoLocal_caaubicacion,
											@id_zonaplaneamiento
		
	END		
	CLOSE curUbicaciones
	DEALLOCATE curUbicaciones
	


	-----------------------------------------------------
	-- Plantas
	----------------------------------------------------
	DECLARE
		@id_caatiposector int
		,@id_tiposector int
		,@detalle_caatiposector nvarchar(50)


	DECLARE curPlantas CURSOR FAST_FORWARD FOR
	SELECT
		id_tiposector 
		,detalle_encomiendatiposector 
	FROM
		Encomienda_Plantas
	WHERE 
		id_encomienda = @id_encomienda

	OPEN curPlantas
	FETCH NEXT FROM curPlantas INTO @id_tiposector,@detalle_caatiposector
	
	WHILE @@FETCH_STATUS = 0
	BEGIN
	
		EXEC @id_caatiposector = Id_Nuevo 'CAA_Plantas'
		INSERT INTO CAA_Plantas(
			id_caatiposector 
			,id_caa 
			,id_tiposector 
			,detalle_caatiposector 
		)
		VALUES
		(
			@id_caatiposector 
			,@id_caa 
			,@id_tiposector 
			,@detalle_caatiposector 
		)
		
		FETCH NEXT FROM curPlantas INTO @id_tiposector,@detalle_caatiposector
		
	END
	CLOSE curPlantas
	DEALLOCATE curPlantas
	
	
	
	--------------------------------------------------------------------
	-- Datos Del Local
	--------------------------------------------------------------------
	DECLARE @id_caadatoslocal int 

		EXEC @id_caadatoslocal = Id_Nuevo 'CAA_DatosLocal'
		INSERT INTO CAA_DatosLocal(
			id_caadatoslocal 
			,id_caa 
			,superficie_cubierta_dl 
			,superficie_descubierta_dl 
			,dimesion_frente_dl 
			,lugar_carga_descarga_dl 
			,estacionamiento_dl 
			,red_transito_pesado_dl 
			,sobre_avenida_dl 
			,materiales_pisos_dl 
			,materiales_paredes_dl 
			,materiales_techos_dl 
			,materiales_revestimientos_dl 
			,sanitarios_ubicacion_dl 
			,sanitarios_distancia_dl 
			,croquis_ubicacion_dl 
			,cantidad_sanitarios_dl 
			,superficie_sanitarios_dl 
			,frente_dl 
			,fondo_dl 
			,lateral_izquierdo_dl 
			,lateral_derecho_dl 
			,sobrecarga_corresponde_dl 
			,sobrecarga_tipo_observacion 
			,sobrecarga_requisitos_opcion 
			,sobrecarga_art813_inciso 
			,sobrecarga_art813_item 
			,cantidad_operarios_dl 
			,CreateDate 
			,CreateUser 
		)
		SELECT
			@id_caadatoslocal
			,@id_caa
			,superficie_cubierta_dl 
			,superficie_descubierta_dl 
			,dimesion_frente_dl 
			,lugar_carga_descarga_dl 
			,estacionamiento_dl 
			,red_transito_pesado_dl 
			,sobre_avenida_dl 
			,materiales_pisos_dl 
			,materiales_paredes_dl 
			,materiales_techos_dl 
			,materiales_revestimientos_dl 
			,sanitarios_ubicacion_dl 
			,sanitarios_distancia_dl 
			,croquis_ubicacion_dl 
			,cantidad_sanitarios_dl 
			,superficie_sanitarios_dl 
			,frente_dl 
			,fondo_dl 
			,lateral_izquierdo_dl 
			,lateral_derecho_dl 
			,sobrecarga_corresponde_dl 
			,sobrecarga_tipo_observacion 
			,sobrecarga_requisitos_opcion 
			,sobrecarga_art813_inciso 
			,sobrecarga_art813_item 
			,cantidad_operarios_dl 
			,GETDATE()
			,@userid 
		FROM
			Encomienda_DatosLocal
		WHERE 
			id_encomienda = @id_encomienda
			
	
	
	--------------------------------------------------------------------
	-- Sobrecargas
	--------------------------------------------------------------------
	DECLARE
		@id_sobrecarga int
		,@estructura_sobrecarga nvarchar(50)
		,@peso_sobrecarga int


	
	DECLARE curSobrecargas CURSOR FAST_FORWARD FOR
	SELECT
		estructura_sobrecarga 
		,peso_sobrecarga 
	FROM
		Encomienda_Sobrecargas
	WHERE
		id_encomienda = @id_encomienda
	
	OPEN curSobrecargas
	FETCH NEXT FROM curSobrecargas INTO @estructura_sobrecarga, @peso_sobrecarga
	
	WHILE @@FETCH_STATUS = 0
	BEGIN
	
		EXEC @id_sobrecarga = Id_Nuevo 'CAA_Sobrecargas'
		INSERT INTO CAA_Sobrecargas(
			id_sobrecarga 
			,id_caa 
			,estructura_sobrecarga 
			,peso_sobrecarga 
			,CreateDate 
			,CreateUser 
			,LastUpdateDate 
			,LastUpdateUser 
		)

		VALUES(
			@id_sobrecarga 
			,@id_caa
			,@estructura_sobrecarga 
			,@peso_sobrecarga 
			,GETDATE()
			,@userid
			,NULL
			,NULL
			)			
	
		FETCH NEXT FROM curSobrecargas INTO @estructura_sobrecarga, @peso_sobrecarga
	
	END
	CLOSE curSobrecargas
	DEALLOCATE curSobrecargas
	
	
	--------------------------------------------------------------------
	-- Rubros
	--------------------------------------------------------------------
	
	DECLARE
		@id_caarubro int
		,@cod_rubro varchar(50)
		,@desc_rubro varchar(200)
		,@EsAnterior bit
		,@id_tipoactividad int
		,@id_tipodocreq int
		,@SuperficieHabilitar decimal(10,2)
		,@id_ImpactoAmbiental int
		,@LetraAnexo_rubro nvarchar(2)


	DECLARE curRubros CURSOR FAST_FORWARD FOR
	SELECT
		encrub.cod_rubro,
		encrub.desc_rubro,
		encrub.EsAnterior,
		encrub.id_tipoactividad,
		encrub.id_tipodocreq,
		encrub.SuperficieHabilitar,
		dbo.Devolver_ImpactoAmbiental(rub.id_rubro,encrub.SuperficieHabilitar),
		dbo.Devolver_LetraAnexo_ImpactoAmbiental(rub.id_rubro,encrub.SuperficieHabilitar)
	FROM
		Encomienda_Rubros encrub
		LEFT JOIN rubros rub ON encrub.cod_rubro = rub.cod_rubro
	WHERE
		id_encomienda = @id_encomienda
	
	OPEN curRubros
	FETCH NEXT FROM curRubros INTO @cod_rubro, @desc_rubro, @EsAnterior,@id_tipoactividad,
									@id_tipodocreq,@SuperficieHabilitar,@id_ImpactoAmbiental,@LetraAnexo_rubro


	WHILE @@FETCH_STATUS = 0
	BEGIN

		EXEC @id_caarubro = Id_Nuevo 'CAA_Rubros'
		INSERT INTO CAA_Rubros(
			id_caarubro 
			,id_caa 
			,cod_rubro 
			,desc_rubro 
			,EsAnterior 
			,id_tipoactividad 
			,id_tipodocreq 
			,SuperficieHabilitar 
			,id_ImpactoAmbiental 
			,AntenaEmisora 
			,id_barriovcol 
			,LetraAnexo_rubro 
			,CreateDate 
		)
		VALUES
		(
			@id_caarubro 
			,@id_caa
			,@cod_rubro 
			,@desc_rubro 
			,@EsAnterior 
			,@id_tipoactividad 
			,@id_tipodocreq 
			,@SuperficieHabilitar 
			,@id_ImpactoAmbiental 
			,0
			,0
			,@LetraAnexo_rubro
			,GETDATE() 
		)

			FETCH NEXT FROM curRubros INTO @cod_rubro, @desc_rubro, @EsAnterior,@id_tipoactividad,
									@id_tipodocreq,@SuperficieHabilitar,@id_ImpactoAmbiental,@LetraAnexo_rubro

	
	END
	CLOSE curRubros
	DEALLOCATE curRubros


	--------------------------------------------------------------------
	-- Normativas
	--------------------------------------------------------------------
	DECLARE
		@id_CAAtiponormativa int
		,@id_tiponormativa int
		,@id_entidadnormativa int
		,@nro_normativa nvarchar(15)



	DECLARE curNormativas CURSOR FAST_FORWARD FOR
	SELECT
		id_tiponormativa 
		,id_entidadnormativa 
		,nro_normativa 
	FROM
		Encomienda_Normativas
	WHERE
		id_encomienda = @id_encomienda
		
	OPEN curNormativas
	FETCH NEXT FROM curNormativas INTO @id_tiponormativa,@id_entidadnormativa,@nro_normativa
	
	WHILE @@FETCH_STATUS = 0
	BEGIN

		EXEC @id_CAAtiponormativa = Id_Nuevo 'CAA_Normativas'
		INSERT INTO CAA_Normativas(
			id_CAAtiponormativa 
			,id_caa 
			,id_tiponormativa 
			,id_entidadnormativa 
			,nro_normativa 
			,CreateUser 
			,CreateDate 
			,LastUpdateUser 
			,LastUpdateDate 
		)
		VALUES
		(
			@id_CAAtiponormativa 
			,@id_caa
			,@id_tiponormativa 
			,@id_entidadnormativa 
			,@nro_normativa 
			,@userid 
			,GETDATE()
			,NULL
			,NULL
		)
	
		FETCH NEXT FROM curNormativas INTO @id_tiponormativa,@id_entidadnormativa,@nro_normativa
	END
	CLOSE curNormativas
	DEALLOCATE curNormativas


	
	--------------------------------------------------------------------
	-- Titulares (Personas físicas)
	--------------------------------------------------------------------
	DECLARE
		@id_personafisica int
		,@Apellido varchar(50)
		,@Nombres nvarchar(50)
		,@id_tipodoc_personal int
		,@Nro_Documento nvarchar(15)
		,@Cuit nvarchar(13)
		,@id_tipoiibb int
		,@Ingresos_Brutos nvarchar(25)
		,@Calle nvarchar(70)
		,@Nro_Puerta int
		,@Piso varchar(2)
		,@Depto varchar(10)
		,@Id_Localidad int
		,@Codigo_Postal nvarchar(10)
		,@TelefonoMovil nvarchar(20)
		,@Sms nvarchar(50)
		,@Email nvarchar(70)
		,@MismoFirmante bit
		,@id_personafisica_anterior int
		
		
	DECLARE curPerFisicas CURSOR FAST_FORWARD FOR
	SELECT
		Apellido 
		,Nombres 
		,id_tipodoc_personal 
		,Nro_Documento 
		,Cuit 
		,id_tipoiibb 
		,Ingresos_Brutos 
		,Calle 
		,Nro_Puerta 
		,Piso 
		,Depto 
		,Id_Localidad 
		,Codigo_Postal 
		,TelefonoMovil 
		,Sms 
		,Email 
		,MismoFirmante 
		,id_personafisica 
	FROM
		Encomienda_Titulares_PersonasFisicas
	WHERE
		id_encomienda = @id_encomienda
		
	OPEN curPerFisicas
	FETCH NEXT FROM curPerFisicas INTO 
									@Apellido 
									,@Nombres 
									,@id_tipodoc_personal 
									,@Nro_Documento 
									,@Cuit 
									,@id_tipoiibb 
									,@Ingresos_Brutos 
									,@Calle 
									,@Nro_Puerta 
									,@Piso 
									,@Depto
									,@Id_Localidad 
									,@Codigo_Postal 
									,@TelefonoMovil 
									,@Sms 
									,@Email 
									,@MismoFirmante 
									,@id_personafisica_anterior

	WHILE @@FETCH_STATUS = 0
	BEGIN

		EXEC @id_personafisica = Id_Nuevo 'CAA_Titulares_PersonasFisicas'
		INSERT INTO CAA_Titulares_PersonasFisicas(
			id_personafisica 
			,id_caa 
			,Apellido 
			,Nombres 
			,id_tipodoc_personal 
			,Nro_Documento 
			,Cuit 
			,id_tipoiibb 
			,Ingresos_Brutos 
			,Calle 
			,Nro_Puerta 
			,Piso 
			,Depto 
			,Id_Localidad 
			,Codigo_Postal 
			,TelefonoMovil 
			,Sms 
			,Email 
			,MismoFirmante 
			,CreateUser 
			,CreateDate 
			,LastUpdateUser 
			,LastupdateDate 
		)
		VALUES
		(
			@id_personafisica 
			,@id_caa
			,@Apellido 
			,@Nombres 
			,@id_tipodoc_personal 
			,@Nro_Documento 
			,@Cuit 
			,@id_tipoiibb 
			,@Ingresos_Brutos 
			,@Calle 
			,@Nro_Puerta 
			,@Piso 
			,@Depto 
			,@Id_Localidad 
			,@Codigo_Postal 
			,@TelefonoMovil 
			,@Sms 
			,@Email 
			,@MismoFirmante 
			,@userid 
			,GETDATE()
			,NULL
			,NULL
		)



			--------------------------------------------------------------------
			-- Firmantes (Personas físicas)
			--------------------------------------------------------------------
			DECLARE
				@id_firmante_pf int
				,@id_tipocaracter int
					
			DECLARE curFirmantesPF CURSOR FAST_FORWARD FOR
			SELECT
				Apellido 
				,Nombres 
				,id_tipodoc_personal 
				,Nro_Documento 
				,id_tipocaracter 
				,Email
			FROM
				Encomienda_Firmantes_PersonasFisicas
			WHERE 
				id_encomienda = @id_encomienda
				AND id_personafisica = @id_personafisica_anterior
				
			OPEN curFirmantesPF
			FETCH NEXT FROM curFirmantesPF INTO 
												@Apellido 
												,@Nombres 
												,@id_tipodoc_personal 
												,@Nro_Documento 
												,@id_tipocaracter 
												,@Email

			WHILE @@FETCH_STATUS = 0
			BEGIN

				EXEC @id_firmante_pf = Id_Nuevo 'CAA_Firmantes_PersonasFisicas'
				INSERT INTO CAA_Firmantes_PersonasFisicas(
					id_firmante_pf 
					,id_caa 
					,id_personafisica 
					,Apellido 
					,Nombres 
					,id_tipodoc_personal 
					,Nro_Documento 
					,id_tipocaracter 
					,Email
				)
				VALUES
				(
					@id_firmante_pf 
					,@id_caa
					,@id_personafisica			-- id de la tabla Encomienda_Titulares_PersonasFisicas
					,@Apellido 
					,@Nombres 
					,@id_tipodoc_personal 
					,@Nro_Documento 
					,@id_tipocaracter 
					,@Email
				)

				
				FETCH NEXT FROM curFirmantesPF INTO 
													@Apellido 
													,@Nombres 
													,@id_tipodoc_personal 
													,@Nro_Documento 
													,@id_tipocaracter 
													,@Email
			
			END
			CLOSE curFirmantesPF
			DEALLOCATE curFirmantesPF

		-- Fin del Alta de Firmantes correspondientes al Titular Persona Física

		FETCH NEXT FROM curPerFisicas INTO 
										@Apellido 
										,@Nombres 
										,@id_tipodoc_personal 
										,@Nro_Documento 
										,@Cuit 
										,@id_tipoiibb 
										,@Ingresos_Brutos 
										,@Calle 
										,@Nro_Puerta 
										,@Piso 
										,@Depto
										,@Id_Localidad 
										,@Codigo_Postal 
										,@TelefonoMovil 
										,@Sms 
										,@Email 
										,@MismoFirmante 
										,@id_personafisica_anterior

	
	END
	CLOSE curPerFisicas
	DEALLOCATE curPerFisicas
	
	

	--------------------------------------------------------------------
	-- Titulares (Personas Jurídicas)
	--------------------------------------------------------------------
	DECLARE
		@id_personajuridica int
		,@Id_TipoSociedad int
		,@Razon_Social nvarchar(100)
		,@Nro_IIBB nvarchar(20)
		,@Telefono nvarchar(50)
		,@id_personajuridica_anterior int
		,@cargo_firmante_pj nvarchar(50)

	DECLARE curTitularesPJ CURSOR FAST_FORWARD FOR
	SELECT
		Id_TipoSociedad 
		,Razon_Social 
		,CUIT 
		,id_tipoiibb 
		,Nro_IIBB 
		,Calle 
		,NroPuerta 
		,Piso 
		,Depto 
		,id_localidad 
		,Codigo_Postal 
		,Telefono 
		,Email 
		,id_personajuridica
	FROM
		Encomienda_Titulares_PersonasJuridicas
	WHERE
		id_encomienda = @id_encomienda
	
	OPEN curTitularesPJ
	FETCH NEXT FROM curTitularesPJ INTO 
										@Id_TipoSociedad 
										,@Razon_Social 
										,@CUIT 
										,@id_tipoiibb 
										,@Nro_IIBB 
										,@Calle 
										,@NroPuerta 
										,@Piso 
										,@Depto 
										,@id_localidad 
										,@Codigo_Postal 
										,@Telefono 
										,@Email 
										,@id_personajuridica_anterior
										


	WHILE @@FETCH_STATUS = 0
	BEGIN

		EXEC @id_personajuridica = Id_Nuevo 'CAA_Titulares_PersonasJuridicas'
		INSERT INTO CAA_Titulares_PersonasJuridicas(
			id_personajuridica 
			,id_caa 
			,Id_TipoSociedad 
			,Razon_Social 
			,CUIT 
			,id_tipoiibb 
			,Nro_IIBB 
			,Calle 
			,NroPuerta 
			,Piso 
			,Depto 
			,id_localidad 
			,Codigo_Postal 
			,Telefono 
			,Email 
			,CreateUser 
			,CreateDate 
			,LastUpdateUser 
			,LastUpdateDate 
		)
		VALUES
		(
			@id_personajuridica 
			,@id_caa
			,@Id_TipoSociedad 
			,@Razon_Social 
			,@CUIT 
			,@id_tipoiibb 
			,@Nro_IIBB 
			,@Calle 
			,@NroPuerta 
			,@Piso 
			,@Depto 
			,@id_localidad 
			,@Codigo_Postal 
			,@Telefono 
			,@Email 
			,@userid 
			,GETDATE()
			,NULL
			,NULL
		)


			--------------------------------------------------------------------
			-- Firmantes (Personas Jurídicas)
			--------------------------------------------------------------------

			DECLARE
				@id_firmante_pj int,
				@id_titular_pj int,
				@id_firmante_enc int

			DECLARE curFirmantesPJ CURSOR FAST_FORWARD FOR
			SELECT
				Apellido 
				,Nombres 
				,id_tipodoc_personal 
				,Nro_Documento 
				,id_tipocaracter 
				,cargo_firmante_pj		
				,Email
				,id_firmante_pj
			FROM
				Encomienda_Firmantes_PersonasJuridicas
			WHERE
				id_encomienda = @id_encomienda
				AND id_personajuridica = @id_personajuridica_anterior
			
			OPEN curFirmantesPJ
			FETCH NEXT FROM curFirmantesPJ INTO 
											@Apellido 
											,@Nombres 
											,@id_tipodoc_personal 
											,@Nro_Documento 
											,@id_tipocaracter 
											,@cargo_firmante_pj
											,@Email
											,@id_firmante_enc
											

			WHILE @@FETCH_STATUS = 0
			BEGIN

				EXEC @id_firmante_pj = Id_Nuevo 'CAA_Firmantes_PersonasJuridicas'

				INSERT INTO CAA_Firmantes_PersonasJuridicas(
					id_firmante_pj 
					,id_caa 
					,id_personajuridica 
					,Apellido 
					,Nombres 
					,id_tipodoc_personal 
					,Nro_Documento 
					,id_tipocaracter 
					,cargo_firmante_pj
					,Email
				)
				VALUES
				(
					@id_firmante_pj 
					,@id_caa
					,@id_personajuridica		-- Id de la tabla Encomienda_Titulares_PersonasJuridicas
					,@Apellido 
					,@Nombres 
					,@id_tipodoc_personal 
					,@Nro_Documento 
					,@id_tipocaracter 
					,@cargo_firmante_pj
					,@Email
				)
				
				
				-- Se da de alta el titular de la Sociedad de Hecho
				EXEC @id_titular_pj = Id_Nuevo 'CAA_Titulares_PersonasJuridicas_PersonasFisicas'
				
				
				INSERT INTO CAA_Titulares_PersonasJuridicas_PersonasFisicas
				(
					id_titular_pj
					,id_caa
					,id_personajuridica
					,Apellido
					,Nombres
					,id_tipodoc_personal
					,Nro_Documento
					,Email
					,id_firmante_pj
					,firmante_misma_persona
				)
				SELECT 
					@id_titular_pj
					,@id_caa
					,@id_personajuridica
					,Apellido
					,Nombres
					,id_tipodoc_personal
					,Nro_Documento
					,Email
					,@id_firmante_pj
					,firmante_misma_persona
				FROM
					Encomienda_Titulares_PersonasJuridicas_PersonasFisicas
				WHERE
					id_encomienda = @id_encomienda
					AND id_personajuridica = @id_personajuridica_anterior
					AND id_firmante_pj = @id_firmante_enc
						
					
				FETCH NEXT FROM curFirmantesPJ INTO 
												@Apellido 
												,@Nombres 
												,@id_tipodoc_personal 
												,@Nro_Documento 
												,@id_tipocaracter 
												,@cargo_firmante_pj
												,@Email
												,@id_firmante_enc
			
			END
			CLOSE curFirmantesPJ
			DEALLOCATE curFirmantesPJ
					

		FETCH NEXT FROM curTitularesPJ INTO 
											@Id_TipoSociedad 
											,@Razon_Social 
											,@CUIT 
											,@id_tipoiibb 
											,@Nro_IIBB 
											,@Calle 
											,@NroPuerta 
											,@Piso 
											,@Depto 
											,@id_localidad 
											,@Codigo_Postal 
											,@Telefono 
											,@Email 
											,@id_personajuridica_anterior

	
	END
	CLOSE curTitularesPJ
	DEALLOCATE curTitularesPJ
	
	
	-- Se crea la tarea Solicitud de CAA
	SET @id_tarea = dbo.ENG_Bus_id_tarea(101)
	EXEC ENG_Crear_Tarea @id_caa,@id_tarea,@userid,@id_tramitetarea output
	EXEC ENG_Asignar_Tarea @id_tramitetarea, @userid
	
	IF @id_tipocertificado IN(1,2) 
	BEGIN
		UPDATE CAA_Solicitudes
		SET id_estado = dbo.CAA_Bus_idEstadoSolicitud('APRO')	-- Aprobado
		WHERE id_caa = @id_caa
		
		SET @id_resultado = dbo.ENG_Bus_id_resultado('TRAM_APROB')
		--Resultado: Solicitud Confirmada
		EXEC ENG_Finalizar_Tarea @id_tramitetarea, @id_resultado, 0, NULL, @id_tramitetarea_nuevo output
		
	END
	
END


GO


