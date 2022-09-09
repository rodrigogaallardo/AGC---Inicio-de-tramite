
DECLARE 
	@userid_mesa uniqueidentifier,
	@id_solicitud int

SELECT @userid_mesa = userid FROM aspnet_users WHERE username = 'AGC-mesa' AND applicationid = 'A2EAEF96-F109-4B62-BC31-53E219C76362'

DECLARE @tbl_borrar TABLE(id_solicitud int)
---------------------------------------------------------------------------
-- Se reccorren todas las solicitudes par apoblar ubicaciones y titulares
---------------------------------------------------------------------------

DECLARE curSol CURSOR FAST_FORWARD FOR
SELECT sol.id_solicitud 
FROM 
	SSIT_Solicitudes sol
	LEFT JOIN SSIT_Solicitudes_Ubicaciones solubic ON sol.id_solicitud = solubic.id_solicitud
--WHERE
	--solubic.id_solicitud IS NULL
	--sol.id_solicitud IN(200434,200039,200314,200365,200182,200619,203675,200067,202849,204089)
ORDER BY 
	sol.id_solicitud

OPEN curSol 
FETCH NEXT FROM curSol INTO @id_solicitud

WHILE @@FETCH_STATUS = 0
BEGIN
	
	DECLARE @id_encomienda int
	-----------------------------------------
	-- se busca la ultima encomienda aprobada
	-----------------------------------------
	SELECT TOP 1 @id_encomienda = id_encomienda 
	FROM Encomienda 
	WHERE id_solicitud = @id_solicitud 
		AND id_estado = 4	-- Aprobada
	ORDER BY
		id_encomienda DESC

	IF @@ROWCOUNT = 0

		-------------------------------------------------------
		-- si no hay una aprobada se busca la ultima encomienda
		-------------------------------------------------------
		BEGIN
		SELECT TOP 1 @id_encomienda = id_encomienda 
		FROM Encomienda 
		WHERE id_solicitud = @id_solicitud 
		ORDER BY
			id_encomienda DESC
	END

	IF @id_encomienda IS NULL
	BEGIN
		-- Si es Null se borrar porque quiere decir que es una solicitud que se quedo sin anexos.
		-- Esto pasa porque anteriormente se permitia hacer solicitudes distintas, ejemplo se hacia la 200.002 por la encomienda 85
		-- Luego se permitia hacer la rectificatoria por la encomienda 85, la cual dio la rectificatoria 35311 y se cargaba una nueva solicitud, en este caso la 221922
		-- Cuando el script de migracion corre vincula ambas encomiendas a la ultima solicitud, la 221922 dejando sin encomienda a la 200.002, por eso es para borrar.
		
		INSERT INTO @tbl_borrar VALUES(@id_solicitud)
		-- por ahora no se borran, despues se corre un proceso que elimina las solicitudes anuladas sin anexo relacionado y sin circuito en SGI.
	END
	ELSE
	BEGIN
		
		BEGIN -- Población de Ubicaciones 
		-----------------------------------------------------
		-- Creacion de Ubicaciones (con sub tablas hijas)
		-----------------------------------------------------
		DECLARE
			@id_solicitudubicacion int 
			,@id_encomiendaubicacion int
			,@id_ubicacion int
			,@id_subtipoubicacion int
			,@local_subtipoubicacion nvarchar(25)
			,@deptoLocal_encomiendaubicacion nvarchar(50)
			,@id_zonaplaneamiento int

		DECLARE curUbicaciones CURSOR FAST_FORWARD FOR
		SELECT 
			id_encomiendaubicacion,
			id_ubicacion,
			id_subtipoubicacion,
			local_subtipoubicacion,
			deptoLocal_encomiendaubicacion,
			id_zonaplaneamiento 
		FROM
			Encomienda_Ubicaciones
		WHERE
			id_encomienda = @id_encomienda

		OPEN curUbicaciones
		FETCH NEXT FROM curUbicaciones INTO @id_encomiendaubicacion ,@id_ubicacion ,@id_subtipoubicacion ,@local_subtipoubicacion ,@deptoLocal_encomiendaubicacion ,@id_zonaplaneamiento 

		WHILE @@FETCH_STATUS = 0
		BEGIN

			EXEC @id_solicitudubicacion = Id_Nuevo 'SSIT_Solicitudes_Ubicaciones'
			INSERT INTO SSIT_Solicitudes_Ubicaciones(
				id_solicitudubicacion 
				,id_solicitud 
				,id_ubicacion 
				,id_subtipoubicacion 
				,local_subtipoubicacion 
				,deptoLocal_ubicacion 
				,CreateDate 
				,CreateUser 
				,id_zonaplaneamiento 
			)
			VALUES
			(
				@id_solicitudubicacion 
				,@id_solicitud 
				,@id_ubicacion 
				,@id_subtipoubicacion 
				,@local_subtipoubicacion 
				,@deptoLocal_encomiendaubicacion 
				,GETDATE() 
				,@userid_mesa
				,@id_zonaplaneamiento 
			)


			--------------------------------------------------------------------------
			-- Creación de las puertas del registro de ubicación que se está iterando
			--------------------------------------------------------------------------
			DECLARE
				@id_solicitudpuerta int
				,@id_encomiendapuerta int
				,@codigo_calle int
				,@nombre_calle nvarchar(100)
				,@NroPuerta int

			DECLARE curPuertas CURSOR FAST_FORWARD FOR
			SELECT
				codigo_calle,
				nombre_calle,
				NroPuerta
			FROM
				Encomienda_Ubicaciones_Puertas
			WHERE
				id_encomiendaubicacion = @id_encomiendaubicacion

			OPEN curPuertas
			FETCH NEXT FROM curPuertas INTO @codigo_calle,@nombre_calle ,@NroPuerta 

			WHILE @@FETCH_STATUS = 0
			BEGIN
				EXEC @id_solicitudpuerta = Id_Nuevo 'SSIT_Solicitudes_Ubicaciones_Puertas'
				INSERT INTO SSIT_Solicitudes_Ubicaciones_Puertas(
					id_solicitudpuerta 
					,id_solicitudubicacion 
					,codigo_calle 
					,nombre_calle 
					,NroPuerta 
				)
				VALUES
				(
					@id_solicitudpuerta 
					,@id_solicitudubicacion 
					,@codigo_calle 
					,@nombre_calle 
					,@NroPuerta 
				)
		
				FETCH NEXT FROM curPuertas INTO @codigo_calle,@nombre_calle ,@NroPuerta 
			END
			CLOSE curPuertas
			DEALLOCATE curPuertas
			-----------------------------------------------------------------------------------------
			-- Creación de las partidas horizontales  del registro de ubicación que se está iterando
			-----------------------------------------------------------------------------------------

			DECLARE
				@id_propiedadhorizontal int

			DECLARE curPropiedadHorizontal CURSOR FAST_FORWARD FOR
			SELECT
				id_propiedadhorizontal
			FROM
				Encomienda_Ubicaciones_PropiedadHorizontal
			WHERE
				id_encomiendaubicacion = @id_encomiendaubicacion

			OPEN curPropiedadHorizontal
			FETCH NEXT FROM curPropiedadHorizontal INTO @id_propiedadhorizontal

			WHILE @@FETCH_STATUS = 0
			BEGIN
			
				DECLARE @id_solicitudprophorizontal int 

				EXEC @id_solicitudprophorizontal = Id_Nuevo 'SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal'
				INSERT INTO SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal(
					id_solicitudprophorizontal 
					,id_solicitudubicacion 
					,id_propiedadhorizontal 
				)
				VALUES
				(
					@id_solicitudprophorizontal 
					,@id_solicitudubicacion 
					,@id_propiedadhorizontal 
				)

				FETCH NEXT FROM curPropiedadHorizontal INTO @id_propiedadhorizontal
			END
			CLOSE curPropiedadHorizontal
			DEALLOCATE curPropiedadHorizontal

			FETCH NEXT FROM curUbicaciones INTO @id_encomiendaubicacion ,@id_ubicacion ,@id_subtipoubicacion ,@local_subtipoubicacion ,@deptoLocal_encomiendaubicacion ,@id_zonaplaneamiento 
		END
		CLOSE curUbicaciones
		DEALLOCATE curUbicaciones
		--------------
		END

		BEGIN -- Población de Titulares
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
				,@Piso nvarchar(2)
				,@Depto nvarchar(10)
				,@Id_Localidad int
				,@Codigo_Postal nvarchar(10)
				,@Telefono nvarchar(50)
				,@TelefonoMovil nvarchar(20)
				,@Sms nvarchar(50)
				,@Email nvarchar(70)
				,@MismoFirmante bit

				DECLARE curPF CURSOR FAST_FORWARD FOR
				SELECT
					id_personafisica,
					Apellido,
					Nombres,
					id_tipodoc_personal,
					Nro_Documento,
					Cuit,
					id_tipoiibb,
					Ingresos_Brutos,
					Calle,
					Nro_Puerta,
					Piso,
					Depto,
					Id_Localidad,
					Codigo_Postal,
					Telefono,
					TelefonoMovil,
					Sms,
					Email,
					MismoFirmante
				FROM
					Encomienda_Titulares_PersonasFisicas
				WHERE
					id_encomienda = @id_encomienda

				OPEN curPF
				FETCH NEXT FROM curPF INTO @id_personafisica 
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
											,@Telefono
											,@TelefonoMovil 
											,@Sms 
											,@Email 
											,@MismoFirmante 


				WHILE @@FETCH_STATUS = 0
				BEGIN

					DECLARE @id_personafisica_sol int 
					EXEC @id_personafisica_sol = Id_Nuevo 'SSIT_Solicitudes_Titulares_PersonasFisicas'

					INSERT INTO SSIT_Solicitudes_Titulares_PersonasFisicas(
						id_personafisica 
						,id_solicitud 
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
						,Telefono
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
						@id_personafisica_sol 
						,@id_solicitud 
						,@Apellido 
						,@Nombres 
						,@id_tipodoc_personal 
						,@Nro_Documento 
						,REPLACE(@Cuit,'-','')
						,@id_tipoiibb 
						,@Ingresos_Brutos 
						,@Calle 
						,@Nro_Puerta 
						,@Piso 
						,@Depto 
						,@Id_Localidad 
						,@Codigo_Postal 
						,@Telefono
						,@TelefonoMovil 
						,@Sms 
						,@Email 
						,@MismoFirmante 
						,@userid_mesa 
						,GETDATE() 
						,NULL
						,NULL 
					)
					
					-----------------------------------------
					-- Firmantes de la persona física
					-----------------------------------------
					DECLARE
						@id_firmante_pf int
						,@Apellido_firpf varchar(50)
						,@Nombres_firpf nvarchar(50)
						,@id_tipodoc_personal_firpf int
						,@Nro_Documento_firpf nvarchar(15)
						,@id_tipocaracter_firpf int
						,@Email_firpf nvarchar(70)


					DECLARE curFirPF CURSOR FAST_FORWARD FOR
					SELECT
						id_firmante_pf,
						Apellido,
						Nombres,
						id_tipodoc_personal,
						Nro_Documento,
						id_tipocaracter,
						Email
					FROM
						Encomienda_Firmantes_PersonasFisicas
					WHERE
						id_encomienda = @id_encomienda

					OPEN curFirPF
					FETCH NEXT FROM curFirPF INTO 
											@id_firmante_pf 
											,@Apellido_firpf
											,@Nombres_firpf
											,@id_tipodoc_personal_firpf 
											,@Nro_Documento_firpf 
											,@id_tipocaracter_firpf 
											,@Email_firpf 


					WHILE @@FETCH_STATUS = 0
					BEGIN

						DECLARE @id_firmante_pf_sol int 
						EXEC @id_firmante_pf_sol = Id_Nuevo 'SSIT_Solicitudes_Firmantes_PersonasFisicas'
						INSERT INTO SSIT_Solicitudes_Firmantes_PersonasFisicas(
							id_firmante_pf 
							,id_solicitud 
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
							@id_firmante_pf_sol 
							,@id_solicitud 
							,@id_personafisica_sol
							,@Apellido_firpf
							,@Nombres_firpf
							,@id_tipodoc_personal_firpf 
							,@Nro_Documento_firpf 
							,@id_tipocaracter_firpf 
							,@Email_firpf 
						)

						FETCH NEXT FROM curFirPF INTO 
											@id_firmante_pf 
											,@Apellido_firpf
											,@Nombres_firpf
											,@id_tipodoc_personal_firpf 
											,@Nro_Documento_firpf 
											,@id_tipocaracter_firpf 
											,@Email_firpf 

					END
					CLOSE curFirPF
					DEALLOCATE curFirPF
					

					FETCH NEXT FROM curPF INTO @id_personafisica 
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
											,@Telefono
											,@TelefonoMovil 
											,@Sms 
											,@Email 
											,@MismoFirmante 
				
				END
				CLOSE curPF
				DEALLOCATE curPF



				------------------------------------------
				-- Titulares de Personas Jurídicas
				------------------------------------------
				DECLARE
					@id_personajuridica_pjenc int
					,@Id_TipoSociedad_pjenc int
					,@Razon_Social_pjenc nvarchar(200)
					,@CUIT_pjenc nvarchar(13)
					,@id_tipoiibb_pjenc int
					,@Nro_IIBB_pjenc nvarchar(20)
					,@Calle_pjenc nvarchar(70)
					,@NroPuerta_pjenc int
					,@Piso_pjenc nvarchar(5)
					,@Depto_pjenc nvarchar(5)
					,@id_localidad_pjenc int
					,@Codigo_Postal_pjenc nvarchar(10)
					,@Telefono_pjenc nvarchar(50)
					,@Email_pjenc nvarchar(70)


				DECLARE curTitPJ CURSOR FAST_FORWARD FOR
				SELECT 
					id_personajuridica,
					Id_TipoSociedad,
					Razon_Social,
					CUIT,
					id_tipoiibb,
					Nro_IIBB,
					Calle,
					NroPuerta,
					Piso,
					Depto,
					id_localidad,
					Codigo_Postal,
					Telefono,
					Email
				FROM
					Encomienda_Titulares_PersonasJuridicas
				WHERE
					id_encomienda = @id_encomienda


				OPEN curTitPJ
				FETCH NEXT FROM curTitPJ INTO @id_personajuridica_pjenc 
											,@Id_TipoSociedad_pjenc 
											,@Razon_Social_pjenc 
											,@CUIT_pjenc 
											,@id_tipoiibb_pjenc 
											,@Nro_IIBB_pjenc 
											,@Calle_pjenc 
											,@NroPuerta_pjenc 
											,@Piso_pjenc 
											,@Depto_pjenc 
											,@id_localidad_pjenc 
											,@Codigo_Postal_pjenc 
											,@Telefono_pjenc 
											,@Email_pjenc 

				WHILE @@FETCH_STATUS = 0
				BEGIN

					DECLARE @id_personajuridica_sol int 
					EXEC @id_personajuridica_sol = Id_Nuevo 'SSIT_Solicitudes_Titulares_PersonasJuridicas'
					INSERT INTO SSIT_Solicitudes_Titulares_PersonasJuridicas(
						id_personajuridica 
						,id_solicitud 
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
						@id_personajuridica_sol 
						,@id_solicitud
						,@Id_TipoSociedad_pjenc 
						,@Razon_Social_pjenc 
						,@CUIT_pjenc 
						,@id_tipoiibb_pjenc  
						,@Nro_IIBB_pjenc 
						,@Calle_pjenc 
						,@NroPuerta_pjenc 
						,@Piso_pjenc 
						,@Depto_pjenc 
						,@id_localidad_pjenc  
						,@Codigo_Postal_pjenc 
						,@Telefono_pjenc 
						,@Email_pjenc 
						,@userid_mesa
						,GETDATE()
						,NULL
						,NULL
					)

					---------------------------------------------
					-- Crear Firmates de Personas Jurídicas
					---------------------------------------------
					DECLARE
						@id_firmante_pj int
						,@Apellido_firpj varchar(50)
						,@Nombres_firpj nvarchar(50)
						,@id_tipodoc_personal_firpj int
						,@Nro_Documento_firpj nvarchar(15)
						,@id_tipocaracter_firpj int
						,@cargo_firmante_firpj nvarchar(50)
						,@Email_firpj nvarchar(70)

					DECLARE curFirPJ CURSOR FAST_FORWARD FOR
					SELECT
						id_firmante_pj,
						Apellido,
						Nombres,
						id_tipodoc_personal,
						Nro_Documento,
						id_tipocaracter,
						cargo_firmante_pj,
						Email
					FROM 
						Encomienda_Firmantes_PersonasJuridicas
					WHERE
						id_personajuridica = @id_personajuridica_pjenc


					OPEN curFirPJ
					FETCH NEXT FROM curFirPJ INTO 
													@id_firmante_pj 
													,@Apellido_firpj 
													,@Nombres_firpj
													,@id_tipodoc_personal_firpj 
													,@Nro_Documento_firpj 
													,@id_tipocaracter_firpj 
													,@cargo_firmante_firpj 
													,@Email_firpj 
					WHILE @@FETCH_STATUS = 0
					BEGIN
						DECLARE @id_firmante_pj_sol int 
						EXEC @id_firmante_pj_sol = Id_Nuevo 'SSIT_Solicitudes_Firmantes_PersonasJuridicas'
						INSERT INTO SSIT_Solicitudes_Firmantes_PersonasJuridicas(
							id_firmante_pj 
							,id_solicitud 
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
							@id_firmante_pj_sol 
							,@id_solicitud 
							,@id_personajuridica_sol 
							,@Apellido_firpj 
							,@Nombres_firpj 
							,@id_tipodoc_personal_firpj 
							,@Nro_Documento_firpj 
							,@id_tipocaracter_firpj 
							,@cargo_firmante_firpj 
							,@Email_firpj 
						)

						---------------------------------------------------------------------------------
						-- Alta del titular de persona fisica juridica (titulares en sociedades de hecho)
						---------------------------------------------------------------------------------
						DECLARE
							@id_titular_pfpj int
							,@Apellido_pfpj nvarchar(50)
							,@Nombres_pfpj nvarchar(50)
							,@id_tipodoc_personal_pfpj int
							,@Nro_Documento_pfpj nvarchar(15)
							,@Email_pfpj nvarchar(70)
							,@firmante_misma_persona_pfpj bit


						DECLARE curFirPFPJ CURSOR FAST_FORWARD FOR
						SELECT
							id_titular_pj,
							Apellido,
							Nombres,
							id_tipodoc_personal,
							Nro_Documento,
							Email,
							firmante_misma_persona
						FROM
							Encomienda_Titulares_PersonasJuridicas_PersonasFisicas
						WHERE
							id_encomienda = @id_encomienda
							AND id_firmante_pj = @id_firmante_pj

						OPEN curFirPFPJ
						FETCH NEXT FROM curFirPFPJ INTO @id_titular_pfpj 
													,@Apellido_pfpj 
													,@Nombres_pfpj
													,@id_tipodoc_personal_pfpj 
													,@Nro_Documento_pfpj
													,@Email_pfpj 
													,@firmante_misma_persona_pfpj 

						WHILE @@FETCH_STATUS = 0
						BEGIN

							DECLARE @id_titular_pfpj_sol int 
							EXEC @id_titular_pfpj_sol = Id_Nuevo 'SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas'
							INSERT INTO SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas(
								id_titular_pj 
								,id_solicitud 
								,id_personajuridica 
								,Apellido 
								,Nombres 
								,id_tipodoc_personal 
								,Nro_Documento 
								,Email 
								,id_firmante_pj 
								,firmante_misma_persona 
							)
							VALUES
							(
								@id_titular_pfpj_sol 
								,@id_solicitud 
								,@id_personajuridica_sol 
								,@Apellido_pfpj  
								,@Nombres_pfpj  
								,@id_tipodoc_personal_pfpj  
								,@Nro_Documento_pfpj  
								,@Email_pfpj  
								,@id_firmante_pj_sol 
								,@firmante_misma_persona_pfpj  
							)

							FETCH NEXT FROM curFirPFPJ INTO @id_titular_pfpj 
													,@Apellido_pfpj 
													,@Nombres_pfpj
													,@id_tipodoc_personal_pfpj 
													,@Nro_Documento_pfpj
													,@Email_pfpj 
													,@firmante_misma_persona_pfpj 
						END
						CLOSE curFirPFPJ
						DEALLOCATE curFirPFPJ


						FETCH NEXT FROM curFirPJ INTO 
													@id_firmante_pj 
													,@Apellido_firpj 
													,@Nombres_firpj
													,@id_tipodoc_personal_firpj 
													,@Nro_Documento_firpj 
													,@id_tipocaracter_firpj 
													,@cargo_firmante_firpj 
													,@Email_firpj 
					END
					CLOSE curFirPJ
					DEALLOCATE curFirPJ

					-- Fin de alta de titulares de sociedades de hecho

					FETCH NEXT FROM curTitPJ INTO @id_personajuridica_pjenc 
											,@Id_TipoSociedad_pjenc 
											,@Razon_Social_pjenc 
											,@CUIT_pjenc 
											,@id_tipoiibb_pjenc 
											,@Nro_IIBB_pjenc 
											,@Calle_pjenc 
											,@NroPuerta_pjenc 
											,@Piso_pjenc 
											,@Depto_pjenc 
											,@id_localidad_pjenc 
											,@Codigo_Postal_pjenc 
											,@Telefono_pjenc 
											,@Email_pjenc 
				END
				CLOSE curTitPJ
				DEALLOCATE curTitPJ


				DECLARE 
					@matricula_escribano int,
					@CodigoSeguridad  nvarchar(6)

				SELECT TOP 1 @matricula_escribano = nro_matricula_escribano_acta FROM wsEscribanos_ActaNotarial WHERE id_encomienda = @id_encomienda AND anulada = 0
				SET @CodigoSeguridad  = convert(nvarchar,ceiling(RAND() * 9)) + CHAR(ceiling(RAND() * 25 + 65)) + convert(nvarchar,ceiling(RAND() * 9)) + CHAR(ceiling(RAND() * 25 + 65)) 
				
				UPDATE SSIT_Solicitudes 
				SET 
					MatriculaEscribano = @matricula_escribano,
					CodigoSeguridad = @CodigoSeguridad
				WHERE 
					id_solicitud =  @id_solicitud
					
				UPDATE SSIT_Solicitudes 
				SET 
					id_estado = 39
				WHERE 
					id_solicitud =  @id_solicitud
					AND id_estado = 1


		END

	END
		
		
	--SELECT @id_solicitud,@id_encomienda
	FETCH NEXT FROM curSol INTO @id_solicitud

END
CLOSE curSol
DEALLOCATE curSol

-- Actualiza a tipo_anexo A en todas las encomiendas Originales
UPDATE Encomienda  SET tipo_anexo = 'A' WHERE id_tipotramite = 1 and tipo_anexo is null