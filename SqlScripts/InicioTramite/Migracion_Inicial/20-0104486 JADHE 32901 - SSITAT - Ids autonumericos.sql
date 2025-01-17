/****** Object:  StoredProcedure [dbo].[Encomienda_DocumentosAdjuntos_Add]    Script Date: 11/23/2016 17:08:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[Encomienda_DocumentosAdjuntos_Add]			
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
	INSERT INTO Encomienda_DocumentosAdjuntos
           (id_encomienda
           ,id_tdocreq
           ,tdocreq_detalle
           ,id_tipodocsis
           ,id_file
           ,generadoxSistema
           ,CreateDate
           ,CreateUser
           ,nombre_archivo)
		
	VALUES(@id_encomienda  
		,@id_tdocreq
		,@tdocreq_detalle
		,@id_tipodocsis 
		,@id_file  
		,@generadoxSistema 
		,GETDATE()
		,@CreateUser
		,@nombre_archivo
		)
	SET @id_docadjunto = IDENT_CURRENT('Encomienda_DocumentosAdjuntos')
END
GO
/****** Object:  StoredProcedure [dbo].[CPadron_AgregarUbicacion]    Script Date: 11/24/2016 08:48:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[CPadron_AgregarUbicacion]
(
	@id_cpadron int
	,@id_ubicacion int
	,@id_subtipoubicacion int
	,@local_subtipoubicacion nvarchar(25)
	,@deptoLocal_CPadronubicacion nvarchar(50)
	,@CreateUser uniqueidentifier
	,@id_cpadronubicacion int OUT
)
AS
BEGIN

	IF dbo.CPadron_VerificarEstadoActualizacion(@id_cpadron) = 0
	BEGIN
		RAISERROR('El estado de la Consulta al Padron no admite cambios en los datos.',16,1)
		RETURN	
	END 
	
	DECLARE 
		@id_zonaplaneamiento int
		
	SELECT 
		@id_zonaplaneamiento = id_zonaplaneamiento
	FROM 
		Ubicaciones 
	WHERE 
		id_ubicacion = @id_ubicacion

	IF @id_zonaplaneamiento = 0
	BEGIN
		RAISERROR('La ubicación no posee zonificación, no es posible ingresarla.',16,1)
		RETURN
	END
	
	IF EXISTS(	
		SELECT 1
		FROM Ubicaciones_Inhibiciones
		WHERE id_ubicacion = @id_ubicacion
		AND ( ( fecha_vencimiento is null ) OR
			  ( GETDATE() BETWEEN fecha_inhibicion AND fecha_vencimiento ) 
			)
		)
	BEGIN
		RAISERROR('La ubicación que desea agregar se encuentra inhibida.',16,1)
		RETURN	
	END	
	
	INSERT INTO CPadron_Ubicaciones(
		id_cpadron 
		,id_ubicacion 
		,id_subtipoubicacion 
		,local_subtipoubicacion 
		,deptoLocal_CPadronubicacion 
		,id_zonaplaneamiento
		,CreateDate 
		,CreateUser 
	)
	VALUES
	(
		@id_cpadron 
		,@id_ubicacion 
		,@id_subtipoubicacion 
		,@local_subtipoubicacion 
		,@deptoLocal_CPadronubicacion 
		,@id_zonaplaneamiento
		,GETDATE() 
		,@CreateUser 
	)
	
	SET @id_cpadronubicacion = IDENT_CURRENT('CPadron_Ubicaciones')

	IF EXISTS(SELECT 'X' FROM CPadron_Solicitudes 
			  WHERE id_cpadron = @id_cpadron 
				AND LEN(IsNull(ZonaDeclarada,''))= 0)
	BEGIN
		DECLARE @ZonaDeclarada nvarchar(15)
		
		SELECT 
			@ZonaDeclarada = zonpla.CodZonaPla
		FROM
			Ubicaciones ubic
			INNER JOIN Zonas_Planeamiento zonpla ON ubic.id_zonaplaneamiento = zonpla.id_zonaplaneamiento
		WHERE
			ubic.id_ubicacion = @id_ubicacion
		
		IF LEN(@ZonaDeclarada) > 0
		BEGIN
			UPDATE CPadron_Solicitudes SET ZonaDeclarada = @ZonaDeclarada WHERE id_cpadron = @id_cpadron
		END
	END
END
GO
/****** Object:  StoredProcedure [dbo].[CPadron_ActualizarDatosLocal]    Script Date: 11/24/2016 09:07:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[CPadron_ActualizarDatosLocal]
(
		   @id_cpadron int
           ,@superficie_cubierta_dl decimal(8,2)
           ,@superficie_descubierta_dl decimal(8,2)
           ,@dimesion_frente_dl decimal(8,2)
           ,@lugar_carga_descarga_dl bit
           ,@estacionamiento_dl bit
           ,@red_transito_pesado_dl bit
           ,@sobre_avenida_dl bit
           ,@materiales_pisos_dl nvarchar(500)
           ,@materiales_paredes_dl nvarchar(500)
           ,@materiales_techos_dl nvarchar(500)
           ,@materiales_revestimientos_dl nvarchar(500)
           ,@sanitarios_ubicacion_dl int
           ,@sanitarios_distancia_dl decimal(8,2)
           ,@croquis_ubicacion_dl nvarchar(300)
		   ,@cantidad_sanitarios_dl int
		   ,@superficie_sanitarios_dl decimal(8,2)
		   ,@frente_dl decimal(8,2)
		   ,@fondo_dl decimal(8,2)
		   ,@lateral_izquierdo_dl decimal(8,2)
		   ,@lateral_derecho_dl decimal(8,2)
           ,@cantidad_operarios_dl int
           ,@userid uniqueidentifier
)
AS
BEGIN

	IF dbo.CPadron_VerificarEstadoActualizacion(@id_cpadron) = 0
	BEGIN
		RAISERROR('El estado de la Consula al Padron no admite cambios en los datos.',16,1)
		RETURN	
	END 
	
	DECLARE @id_cpadrondatoslocal int
    
    SELECT @id_cpadrondatoslocal = id_cpadrondatoslocal 
    FROM CPadron_DatosLocal 
    WHERE id_cpadron = @id_cpadron
    
    SET @id_cpadrondatoslocal = IsNull(@id_cpadrondatoslocal,0)

    IF @id_cpadrondatoslocal = 0
    BEGIN
		INSERT INTO CPadron_DatosLocal
           ([id_cpadron]
           ,[superficie_cubierta_dl]
           ,[superficie_descubierta_dl]
           ,[dimesion_frente_dl]
           ,[lugar_carga_descarga_dl]
           ,[estacionamiento_dl]
           ,[red_transito_pesado_dl]
           ,[sobre_avenida_dl]
           ,[materiales_pisos_dl]
           ,[materiales_paredes_dl]
           ,[materiales_techos_dl]
           ,[materiales_revestimientos_dl]
           ,[sanitarios_ubicacion_dl]
           ,[sanitarios_distancia_dl]
           ,[croquis_ubicacion_dl]
           ,[cantidad_sanitarios_dl]
           ,[superficie_sanitarios_dl]
           ,[frente_dl]
           ,[fondo_dl]
           ,[lateral_izquierdo_dl]
           ,[lateral_derecho_dl]
           ,[cantidad_operarios_dl]
           ,[CreateDate]
           ,[CreateUser])
     VALUES(@id_cpadron 
			,@superficie_cubierta_dl 
			,@superficie_descubierta_dl 
			,@dimesion_frente_dl 
			,@lugar_carga_descarga_dl 
			,@estacionamiento_dl 
			,@red_transito_pesado_dl 
			,@sobre_avenida_dl 
			,@materiales_pisos_dl 
			,@materiales_paredes_dl 
			,@materiales_techos_dl 
			,@materiales_revestimientos_dl 
			,@sanitarios_ubicacion_dl 
			,@sanitarios_distancia_dl 
			,@croquis_ubicacion_dl 
			,@cantidad_sanitarios_dl
		    ,@superficie_sanitarios_dl
			,@frente_dl 
			,@fondo_dl 
			,@lateral_izquierdo_dl 
			,@lateral_derecho_dl 			   
			,@cantidad_operarios_dl
			,GETDATE()
			,@userid 
		)
		SELECT @id_cpadrondatoslocal=MAX(id_cpadrondatoslocal) FROM CPadron_DatosLocal
	END
	ELSE
	BEGIN
	
		UPDATE CPadron_DatosLocal
		SET
			superficie_cubierta_dl = @superficie_cubierta_dl 
			,superficie_descubierta_dl = @superficie_descubierta_dl 
			,dimesion_frente_dl = @dimesion_frente_dl 
			,lugar_carga_descarga_dl = @lugar_carga_descarga_dl 
			,estacionamiento_dl = @estacionamiento_dl 
			,red_transito_pesado_dl = @red_transito_pesado_dl 
			,sobre_avenida_dl = @sobre_avenida_dl 
			,materiales_pisos_dl = @materiales_pisos_dl 
			,materiales_paredes_dl = @materiales_paredes_dl 
			,materiales_techos_dl = @materiales_techos_dl 
			,materiales_revestimientos_dl = @materiales_revestimientos_dl 
			,sanitarios_ubicacion_dl = @sanitarios_ubicacion_dl 
			,sanitarios_distancia_dl = @sanitarios_distancia_dl 
			,croquis_ubicacion_dl = @croquis_ubicacion_dl 
			,cantidad_sanitarios_dl = @cantidad_sanitarios_dl
		    ,superficie_sanitarios_dl = @superficie_sanitarios_dl
			,frente_dl = @frente_dl 
			,fondo_dl = @fondo_dl 
			,lateral_izquierdo_dl = @lateral_izquierdo_dl
			,lateral_derecho_dl = @lateral_derecho_dl
			,cantidad_operarios_dl = @cantidad_operarios_dl
			,LastUpdateDate = GETDATE()
			,LastUpdateUser = @userid
		WHERE
			id_cpadrondatoslocal = @id_cpadrondatoslocal
	END
	--actualizar campo id_tipoexpediente y id_subtipoexpediente	
	EXEC CPadron_Actualizar_TipoSubtipo_Expediente @id_cpadron

	RETURN @id_cpadrondatoslocal
END
GO
/****** Object:  StoredProcedure [dbo].[CPadron_ActualizarNormativa]    Script Date: 11/24/2016 09:09:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[CPadron_ActualizarNormativa]
(
		@id_cpadron			int,
		@id_tiponormativa		int,
		@id_entidadnormativa	int,
		@nro_normativa			nvarchar(15),
		@userid					uniqueidentifier
)
AS
BEGIN

	IF dbo.CPadron_VerificarEstadoActualizacion(@id_cpadron) = 0
	BEGIN
		RAISERROR('El estado de la encomienda no admite cambios en los datos.',16,1)
		RETURN	
	END 
	
	DECLARE @id_cpadrontiponormativa	int

	SELECT @id_cpadrontiponormativa = id_cpadrontiponormativa
	FROM CPadron_Normativas
	WHERE id_cpadron = @id_cpadron

	SET @id_cpadrontiponormativa = IsNull(@id_cpadrontiponormativa,0)

	IF @id_cpadrontiponormativa = 0
	BEGIN

		EXEC @id_cpadrontiponormativa = id_nuevo 'CPadron_Normativas'

		INSERT INTO CPadron_Normativas
		(
			id_cpadron,
			id_tiponormativa,
			id_entidadnormativa,
			nro_normativa,
			CreateUser,
			CreateDate,
			LastUpdateUser,
			LastUpdateDate
		)
		VALUES
		(
			@id_cpadron			,
			@id_tiponormativa		,
			@id_entidadnormativa	,
			@nro_normativa			,
			@userid					,
			GETDATE()				,
			NULL					,
			NULL					
		)
		SELECT @id_cpadrontiponormativa=MAX(id_cpadrontiponormativa) FROM CPadron_Normativas
	END
	ELSE
	BEGIN
		UPDATE CPadron_Normativas
		SET
			id_cpadron  = @id_cpadron,
			id_tiponormativa = @id_tiponormativa,
			id_entidadnormativa = @id_entidadnormativa,
			nro_normativa = @nro_normativa,
			LastUpdateUser = @userid,
			LastUpdateDate	= GETDATE()		
		WHERE 
			id_cpadrontiponormativa = @id_cpadrontiponormativa
	END
	RETURN @id_cpadrontiponormativa
END
GO
/****** Object:  StoredProcedure [dbo].[CPadron_ActualizarPlanos]    Script Date: 11/24/2016 09:11:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[CPadron_ActualizarPlanos]
(
	@id_cpadron_plano		int,
	@id_cpadron  int,
	@id_tipo_plano int,
	@detalle        nvarchar(50),
	@documento   	varbinary(max),
	@nombre_archivo nvarchar(100),
	@usuario		nvarchar(50)
)
AS
BEGIN
	DECLARE @id_file int
	
	IF @documento IS NOT NULL
	BEGIN
		IF @id_cpadron_plano > 0		-- Actualiza, ya existe el registro
		BEGIN	
			select  @id_file= id_file from dbo.CPadron_Planos
			where id_cpadron_plano=@id_cpadron_plano
			
			EXEC AGC_Files.dbo.Files_Actualizar @id_file, @documento, @usuario
				
		END
		ELSE
		BEGIN
			EXEC AGC_Files.dbo.Files_Agregar @documento, @usuario, @id_file OUTPUT
			
            INSERT INTO CPadron_Planos
			(
				id_cpadron,
				id_file,
				id_tipo_plano,
				detalle,
				nombre_archivo,
				CreateDate,
				CreateUser
			)
			VALUES
			(
				@id_cpadron,
				@id_file,
				@id_tipo_plano,
				@detalle,
				@nombre_archivo,
				GETDATE(),
				@usuario
			)
			SELECT @id_cpadron_plano=MAX(id_cpadron_plano) FROM CPadron_Planos			
		END
	END
	SELECT @id_cpadron_plano
	RETURN @id_cpadron_plano
END
GO
/****** Object:  StoredProcedure [dbo].[CPadron_AgregarConformacionLocal]    Script Date: 11/24/2016 09:13:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[CPadron_AgregarConformacionLocal]
(
	@id_cpadron				int,
	@id_destino					int,
	@largo_conflocal			decimal(10,2),
	@ancho_conflocal			decimal(10,2),
	@alto_conflocal				decimal(10,2),
	@superficie_conflocal		decimal(10,2),
	@Paredes_conflocal			nvarchar(50),
	@Techos_conflocal			nvarchar(50),
	@Pisos_conflocal			nvarchar(50),
	@Frisos_conflocal			nvarchar(50),
	@Observaciones_conflocal	nvarchar(4000),
	@userid						uniqueidentifier,
	@Detalle_conflocal			nvarchar(200),
	@id_cpadrontiposector	int,
	@id_ventilacion				int,
	@id_iluminacion				int,
	@id_tiposuperficie          int
)
AS
BEGIN

	IF dbo.CPadron_VerificarEstadoActualizacion(@id_cpadron) = 0
	BEGIN
		RAISERROR('El estado de la Consulta al Padron no admite cambios en los datos.',16,1)
		RETURN	
	END 
	
	DECLARE @id_cpadronconflocal int

	INSERT INTO CPadron_ConformacionLocal
	(
		id_cpadron,
		id_destino,
		largo_conflocal,
		ancho_conflocal,
		alto_conflocal,
		superficie_conflocal,
		Paredes_conflocal,
		Techos_conflocal,
		Pisos_conflocal,
		Frisos_conflocal,
		Observaciones_conflocal,
		CreateDate,
		CreateUser,
		UpdateDate,
		Updateuser,
		Detalle_conflocal,
		id_cpadrontiposector,
		id_ventilacion,
		id_iluminacion,
		id_tiposuperficie
	)
	VALUES
	(
		@id_cpadron,
		@id_destino,
		@largo_conflocal,
		@ancho_conflocal,
		@alto_conflocal,
		@superficie_conflocal,
		@Paredes_conflocal,
		@Techos_conflocal,
		@Pisos_conflocal,
		@Frisos_conflocal,
		@Observaciones_conflocal,
		GETDATE(),
		@userid,
		NULL,
		NULL,
		@Detalle_conflocal,
		@id_cpadrontiposector,
		@id_ventilacion,
		@id_iluminacion	,
		@id_tiposuperficie			
	)
	SELECT @id_cpadronconflocal=MAX(id_cpadronconflocal) FROM CPadron_ConformacionLocal
	
	RETURN @id_cpadronconflocal

END
GO
/****** Object:  StoredProcedure [dbo].[CPadron_AgregarFirmantesPersonasFisicas]    Script Date: 11/24/2016 09:28:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[CPadron_AgregarFirmantesPersonasFisicas]
(
	@id_cpadron int
	,@id_personafisica int
	,@Apellido varchar(50)
	,@Nombres nvarchar(50)
	,@id_tipodoc_personal int
	,@Nro_Documento nvarchar(15)
	,@id_tipocaracter int
	,@Email nvarchar(70)
	,@id_firmante_pf int OUT
)
AS
BEGIN

	IF dbo.CPadron_VerificarEstadoActualizacion(@id_cpadron) = 0
	BEGIN
		RAISERROR('El estado de la Consulta al Padron no admite cambios en los datos.',16,1)
		RETURN	
	END 
	
	INSERT INTO CPadron_Firmantes_PersonasFisicas(
		id_cpadron 
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
		@id_cpadron 
		,@id_personafisica 
		,@Apellido 
		,@Nombres 
		,@id_tipodoc_personal 
		,@Nro_Documento 
		,@id_tipocaracter
		,@Email 
	)
	SELECT @id_firmante_pf=MAX(id_firmante_pf) FROM CPadron_Firmantes_PersonasFisicas
END
GO
/****** Object:  StoredProcedure [dbo].[CPadron_AgregarFirmantesPersonasFisicasSol]    Script Date: 11/24/2016 09:31:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[CPadron_AgregarFirmantesPersonasFisicasSol]
(
	@id_cpadron int
	,@id_personafisica int
	,@Apellido varchar(50)
	,@Nombres nvarchar(50)
	,@id_tipodoc_personal int
	,@Nro_Documento nvarchar(15)
	,@id_tipocaracter int
	,@Email nvarchar(70)
	,@id_firmante_pf int OUT
)
AS
BEGIN

	IF dbo.CPadron_VerificarEstadoActualizacion(@id_cpadron) = 0
	BEGIN
		RAISERROR('El estado de la Consulta al Padron no admite cambios en los datos.',16,1)
		RETURN	
	END 

	INSERT INTO CPadron_Firmantes_Solicitud_PersonasFisicas(
		id_cpadron 
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
		@id_cpadron 
		,@id_personafisica 
		,@Apellido 
		,@Nombres 
		,@id_tipodoc_personal 
		,@Nro_Documento 
		,@id_tipocaracter
		,@Email 
	)
	SELECT @id_firmante_pf=MAX(id_firmante_pf) FROM CPadron_Firmantes_Solicitud_PersonasFisicas
END
GO
/****** Object:  StoredProcedure [dbo].[CPadron_AgregarFirmantesPersonasJuridicas]    Script Date: 11/24/2016 09:32:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[CPadron_AgregarFirmantesPersonasJuridicas]
(
	@id_cpadron int
	,@id_personajuridica int
	,@Apellido varchar(50)
	,@Nombres nvarchar(50)
	,@id_tipodoc_personal int
	,@Nro_Documento nvarchar(15)
	,@id_tipocaracter int
	,@cargo_firmante_pj nvarchar(50)
	,@email nvarchar(70)
	,@id_firmante_pj int OUT
)
AS
BEGIN

	IF dbo.CPadron_VerificarEstadoActualizacion(@id_cpadron) = 0
	BEGIN
		RAISERROR('El estado de la Consulta al Padron no admite cambios en los datos.',16,1)
		RETURN	
	END 
	
	INSERT INTO CPadron_Firmantes_PersonasJuridicas(
		id_cpadron 
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
		@id_cpadron 
		,@id_personajuridica 
		,@Apellido 
		,@Nombres 
		,@id_tipodoc_personal 
		,@Nro_Documento 
		,@id_tipocaracter 
		,@cargo_firmante_pj
		,@email
	)
	SELECT @id_firmante_pj=MAX(id_firmante_pj) FROM CPadron_Firmantes_PersonasJuridicas
END
GO
/****** Object:  StoredProcedure [dbo].[CPadron_AgregarFirmantesPersonasJuridicasSol]    Script Date: 11/24/2016 09:34:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[CPadron_AgregarFirmantesPersonasJuridicasSol]
(
	@id_cpadron int
	,@id_personajuridica int
	,@Apellido varchar(50)
	,@Nombres nvarchar(50)
	,@id_tipodoc_personal int
	,@Nro_Documento nvarchar(15)
	,@id_tipocaracter int
	,@cargo_firmante_pj nvarchar(50)
	,@email nvarchar(70)
	,@id_firmante_pj int OUT
)
AS
BEGIN

	IF dbo.CPadron_VerificarEstadoActualizacion(@id_cpadron) = 0
	BEGIN
		RAISERROR('El estado de la Consulta al Padron no admite cambios en los datos.',16,1)
		RETURN	
	END 
	
	INSERT INTO CPadron_Firmantes_Solicitud_PersonasJuridicas(
		id_cpadron 
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
		@id_cpadron 
		,@id_personajuridica 
		,@Apellido 
		,@Nombres 
		,@id_tipodoc_personal 
		,@Nro_Documento 
		,@id_tipocaracter 
		,@cargo_firmante_pj
		,@email
	)
	SELECT @id_firmante_pj=MAX(id_firmante_pj) FROM CPadron_Firmantes_Solicitud_PersonasJuridicas
END
GO
/****** Object:  StoredProcedure [dbo].[CPadron_AgregarPlanta]    Script Date: 11/24/2016 09:35:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[CPadron_AgregarPlanta]
(
	@id_cpadron int
	,@id_tiposector int
	,@detalle_cpadrontiposector nvarchar(50)
	,@id_cpadrontiposector int out
)
AS
BEGIN

	IF dbo.CPadron_VerificarEstadoActualizacion(@id_cpadron) = 0
	BEGIN
		RAISERROR('El estado de la Consulta al Padron no admite cambios en los datos.',16,1)
		RETURN	
	END 
	
	INSERT INTO CPadron_plantas(
		id_cpadron 
		,id_tiposector 
		,detalle_cpadrontiposector
	)
	VALUES
	(
		@id_cpadron 
		,@id_tiposector 
		,@detalle_cpadrontiposector
	)
	SELECT @id_cpadrontiposector=MAX(id_cpadrontiposector) FROM CPadron_plantas
END
GO
/****** Object:  StoredProcedure [dbo].[CPadron_AgregarPropiedadHorizontal]    Script Date: 11/24/2016 09:36:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[CPadron_AgregarPropiedadHorizontal]
(
	@id_cpadronubicacion int
	,@id_propiedadhorizontal int
	,@id_cpadron	 int = 0
)
AS
BEGIN

	IF dbo.CPadron_VerificarEstadoActualizacion(@id_cpadron) = 0
	BEGIN
		RAISERROR('El estado de la Consulta al Padron no admite cambios en los datos.',16,1)
		RETURN	
	END 
	
	IF EXISTS(
		SELECT 1
		FROM Ubicaciones_PropiedadHorizontal_Inhibiciones
		WHERE id_propiedadhorizontal = @id_propiedadhorizontal
		AND ( ( fecha_vencimiento is null ) OR
			  ( GETDATE() BETWEEN fecha_inhibicion AND fecha_vencimiento )  
			)
		)
	BEGIN
		RAISERROR('La propiedad horizontal que desea agregar se encuentra inhibida.',16,1)
		RETURN
	END
		
	INSERT INTO CPadron_Ubicaciones_PropiedadHorizontal(
		id_cpadronubicacion 
		,id_propiedadhorizontal 
	)
	VALUES
	(
		@id_cpadronubicacion 
		,@id_propiedadhorizontal 
	)
END
GO
/****** Object:  StoredProcedure [dbo].[CPadron_AgregarPuerta]    Script Date: 11/24/2016 09:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[CPadron_AgregarPuerta]
(
	@id_cpadronubicacion int
	,@codigo_calle int
	,@NroPuerta int
	,@id_cpadron int = 0
)
AS
BEGIN
	
	IF dbo.CPadron_VerificarEstadoActualizacion(@id_cpadron) = 0
	BEGIN
		RAISERROR('El estado de la ConsulTa al Padron no admite cambios en los datos.',16,1)
		RETURN	
	END 
		
	INSERT INTO CPadron_Ubicaciones_Puertas(
		id_cpadronubicacion 
		,codigo_calle 
		,nombre_calle 
		,NroPuerta 
	)
	VALUES
	(
		@id_cpadronubicacion 
		,@codigo_calle 
		,dbo.Bus_NombreCalle( @codigo_calle,@NroPuerta )
		,@NroPuerta 
	)
END
GO
/****** Object:  StoredProcedure [dbo].[CPadron_AgregarRubro]    Script Date: 11/24/2016 09:40:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[CPadron_AgregarRubro]
(
		@id_cpadron	int,
		@cod_rubro		nvarchar(50),
		@Superficie		decimal(10,2)
)
AS
BEGIN
	
	IF dbo.CPadron_VerificarEstadoActualizacion(@id_cpadron) = 0
	BEGIN
		RAISERROR('El estado de la Consulta al Padron no admite cambios en los datos.',16,1)
		RETURN	
	END 
		
	DECLARE @Descripcion varchar(200)
	DECLARE @id_tipoactividad int
	DECLARE @id_tipodocreq int
	DECLARE @EsAnterior bit
	DECLARE @id_ImpactoAmbiental int
	DECLARE @id_rubro int
	DECLARE @id_cpadronrubro int
	
	DECLARE @superficie_cubierta_dl		decimal(8,2)
	DECLARE @superficie_descubierta_dl	decimal(8,2)
		
	--Verificar que la superficie del rubro no puede ser mayor 
	--a la superficie a habilitar. mantis 70912
	
	SELECT 
		@superficie_cubierta_dl = IsNull(superficie_cubierta_dl, 0),
		@superficie_descubierta_dl = IsNull(superficie_descubierta_dl, 0)
	FROM 
		CPadron_DatosLocal
	WHERE 
		id_cpadron = @id_cpadron
	
	IF @@ROWCOUNT > 0 
		AND @Superficie > @superficie_cubierta_dl + @superficie_descubierta_dl  
	BEGIN
		RAISERROR('La superficie del rubro no puede ser mayor a la <br/>superficie a habilitar',16,1)
		RETURN -1
	END	
	
	SELECT @id_cpadronrubro = id_cpadronrubro  
	FROM CPadron_rubros
	WHERE 
		id_cpadron = @id_cpadron
		AND cod_rubro = @cod_rubro
		
	SET @id_cpadronrubro = IsNull(@id_cpadronrubro,0)



	IF @id_cpadronrubro = 0
	BEGIN

		SELECT TOP 1 
			@Descripcion  = nom_rubro ,
			@id_tipoactividad = id_tipoactividad,
			@id_tipodocreq = id_tipodocreq,
			@EsAnterior = EsAnterior_Rubro,
			@id_rubro = id_rubro
		FROM 
			rubros 
		WHERE 
			cod_rubro= @cod_rubro
			AND (VigenciaHasta_rubro IS NULL or VigenciaHasta_rubro > GETDATE())


		IF @@ROWCOUNT < 1
		BEGIN
			RAISERROR('El Rubro no ha sido encontrado en la base de datos<br />No es posible agregarlo a la encomienda',16,1)
			RETURN -1
		END
		
		-- Si el Rubro es Declaración Jurada Sin Planos y la superficie es mayor a 500 m2
		-- se pone com Declaración Jurada con planos.
		IF (@id_tipodocreq = 1 AND @Superficie > 500)
			SET @id_tipodocreq = 2
			
		

		SET @id_ImpactoAmbiental = dbo.Devolver_ImpactoAmbiental(@id_rubro,@Superficie)

		IF (@id_ImpactoAmbiental = 0)
		BEGIN
			RAISERROR('El Rubro no ha sido Categorizado según el Impacto Ambiental para la superficie indicada. <br />No es posible agregarlo a la solicitud',16,1)
			RETURN -1
		END
		
		INSERT INTO CPadron_rubros
		(
			id_cpadron,
			cod_rubro,
			desc_rubro,
			EsAnterior,
			id_tipoactividad,
			id_tipodocreq,
			SuperficieHabilitar,
			id_ImpactoAmbiental,
			CreateDate
		)
		VALUES
		(
			@id_cpadron,
			@cod_rubro,
			@Descripcion,
			@EsAnterior,
			@id_tipoactividad,
			@id_tipodocreq,
			@Superficie,
			@id_ImpactoAmbiental,
			GETDATE()
		)
		EXEC CPadron_Actualizar_TipoSubtipo_Expediente @id_cpadron
	END
	ELSE
	BEGIN
		RAISERROR('El Rubro ya se encuentra en la encomienda<br />Si necesita modificar la superficie, eliminelo e ingreselo nuevamente con la nueva superficie.',16,1)
	END
END
GO
/****** Object:  StoredProcedure [dbo].[CPadron_AgregarRubroUsoNoContemplado]    Script Date: 11/24/2016 09:41:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[CPadron_AgregarRubroUsoNoContemplado]
(
	@id_cpadron int
	,@desc_rubro varchar(200)
	,@id_tipoactividad int
	,@id_tipodocreq int
	,@SuperficieHabilitar decimal(10,2)
)
AS
BEGIN

	IF dbo.CPadron_VerificarEstadoActualizacion(@id_cpadron) = 0
	BEGIN
		RAISERROR('El estado de la encomienda no admite cambios en los datos.',16,1)
		RETURN	
	END 
	
	DECLARE @cod_rubro  varchar(50)
	DECLARE @id_cpadronrubro int 
	
	SET @cod_rubro = '888888'
	-- Si el Rubro es Declaración Jurada Sin Planos y la superficie es mayor a 500 m2
	-- se pone com Declaración Jurada con planos.
	IF (@id_tipodocreq = 1 AND @SuperficieHabilitar > 500)
		SET @id_tipodocreq = 2
		
	INSERT INTO CPadron_rubros(
		id_cpadron 
		,cod_rubro 
		,desc_rubro 
		,EsAnterior 
		,id_tipoactividad 
		,id_tipodocreq 
		,SuperficieHabilitar 
		,id_ImpactoAmbiental 
		,CreateDate 
	)
	VALUES
	(
		@id_cpadron 
		,@cod_rubro 
		,@desc_rubro 
		,0 
		,@id_tipoactividad 
		,@id_tipodocreq 
		,@SuperficieHabilitar 
		,3			-- Sujeto a Categorización
		,GETDATE() 
	)
	EXEC CPadron_Actualizar_TipoSubtipo_Expediente @id_cpadron
	
END
GO
/****** Object:  StoredProcedure [dbo].[CPadron_AgregarTitularesPersonasFisicas]    Script Date: 11/24/2016 09:42:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[CPadron_AgregarTitularesPersonasFisicas]
(
	@id_cpadron int
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
	,@Telefono nvarchar(50)
	,@TelefonoMovil nvarchar(20)
	,@Sms nvarchar(50)
	,@Email nvarchar(70)
	,@MismoFirmante bit
	,@CreateUser uniqueidentifier
	,@id_personafisica int OUT
)
AS
BEGIN

	IF dbo.CPadron_VerificarEstadoActualizacion(@id_cpadron) = 0
	BEGIN
		RAISERROR('El estado de la Consulta al Padron no admite cambios en los datos.',16,1)
		RETURN	
	END 
	
	INSERT INTO CPadron_Titulares_PersonasFisicas(
		id_cpadron 
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
		@id_cpadron 
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
		,LOWER(@Email)
		,@MismoFirmante
		,@CreateUser 
		,GETDATE()
		,NULL
		,NULL
	)
	SELECT @id_personafisica=MAX(id_personafisica) FROM CPadron_Titulares_PersonasFisicas
END
GO
/****** Object:  StoredProcedure [dbo].[CPadron_AgregarTitularesPersonasFisicasSol]    Script Date: 11/24/2016 09:43:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[CPadron_AgregarTitularesPersonasFisicasSol]
(
	@id_cpadron int
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
	,@Telefono nvarchar(50)
	,@TelefonoMovil nvarchar(20)
	,@Sms nvarchar(50)
	,@Email nvarchar(70)
	,@MismoFirmante bit
	,@CreateUser uniqueidentifier
	,@id_personafisica int OUT
)
AS
BEGIN

	IF dbo.CPadron_VerificarEstadoActualizacion(@id_cpadron) = 0
	BEGIN
		RAISERROR('El estado de la Consulta al Padron no admite cambios en los datos.',16,1)
		RETURN	
	END 
	
	INSERT INTO CPadron_Titulares_Solicitud_PersonasFisicas(
		id_cpadron 
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
		@id_cpadron 
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
		,@CreateUser 
		,GETDATE()
		,NULL
		,NULL
	)
	SELECT @id_personafisica=MAX(id_personafisica) FROM CPadron_Titulares_Solicitud_PersonasFisicas
END
GO
/****** Object:  StoredProcedure [dbo].[CPadron_AgregarTitularesPersonasJuridicas]    Script Date: 11/24/2016 09:45:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[CPadron_AgregarTitularesPersonasJuridicas]
(
	@id_cpadron int
	,@Id_TipoSociedad int
	,@Razon_Social nvarchar(200)
	,@CUIT nvarchar(13)
	,@id_tipoiibb int
	,@Nro_IIBB nvarchar(20)
	,@Calle nvarchar(70)
	,@NroPuerta int
	,@Piso nvarchar(5)
	,@Depto nvarchar(5)
	,@id_localidad int
	,@Codigo_Postal nvarchar(10)
	,@Telefono nvarchar(50)
	,@Email nvarchar(70)
	,@CreateUser uniqueidentifier
	,@id_personajuridica int OUT
)
AS
BEGIN

	IF dbo.CPadron_VerificarEstadoActualizacion(@id_cpadron) = 0
	BEGIN
		RAISERROR('El estado de la Consulta al Padron no admite cambios en los datos.',16,1)
		RETURN	
	END 
		
	INSERT INTO CPadron_Titulares_PersonasJuridicas
	(
		id_cpadron 
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
		@id_cpadron 
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
		,LOWER(@Email)
		,@CreateUser 
		,GETDATE()
		,NULL
		,NULL
	)
	SELECT @id_personajuridica=MAX(id_personajuridica) FROM CPadron_Titulares_PersonasJuridicas
END
GO
/****** Object:  StoredProcedure [dbo].[CPadron_AgregarTitularesPersonasJuridicasSol]    Script Date: 11/24/2016 09:46:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[CPadron_AgregarTitularesPersonasJuridicasSol]
(
	@id_cpadron int
	,@Id_TipoSociedad int
	,@Razon_Social nvarchar(200)
	,@CUIT nvarchar(13)
	,@id_tipoiibb int
	,@Nro_IIBB nvarchar(20)
	,@Calle nvarchar(70)
	,@NroPuerta int
	,@Piso nvarchar(5)
	,@Depto nvarchar(5)
	,@id_localidad int
	,@Codigo_Postal nvarchar(10)
	,@Telefono nvarchar(50)
	,@Email nvarchar(70)
	,@CreateUser uniqueidentifier
	,@id_personajuridica int OUT
)
AS
BEGIN

	IF dbo.CPadron_VerificarEstadoActualizacion(@id_cpadron) = 0
	BEGIN
		RAISERROR('El estado de la Consulta al Padron no admite cambios en los datos.',16,1)
		RETURN	
	END 
		
	INSERT INTO CPadron_Titulares_Solicitud_PersonasJuridicas
	(
		id_cpadron 
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
		@id_cpadron 
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
		,@CreateUser 
		,GETDATE()
		,NULL
		,NULL
	)
	SELECT @id_personajuridica=MAX(id_personajuridica) FROM CPadron_Titulares_Solicitud_PersonasJuridicas
END
GO
/****** Object:  StoredProcedure [dbo].[CPadron_AgregarTitularesSHPersonasFisicas]    Script Date: 11/24/2016 09:47:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[CPadron_AgregarTitularesSHPersonasFisicas]
(
	@id_cpadron int
	,@id_personajuridica int
	,@Apellido varchar(50)
	,@Nombres nvarchar(50)
	,@id_tipodoc_personal int
	,@Nro_Documento nvarchar(15)
	,@email nvarchar(70)
	,@id_titular_pj int OUTPUT
)
AS
BEGIN
	INSERT INTO CPadron_Titulares_PersonasJuridicas_PersonasFisicas
    (
        id_cpadron
        ,id_personajuridica
        ,Apellido
        ,Nombres
        ,id_tipodoc_personal
        ,Nro_Documento
        ,Email
        ,firmante_misma_persona
	)
	VALUES
    (
		@id_cpadron
		,@id_personajuridica 
		,@Apellido 
		,@Nombres 
		,@id_tipodoc_personal 
		,@Nro_Documento 
		,@email 
		,1
	)
	SELECT @id_titular_pj=MAX(id_titular_pj) FROM CPadron_Titulares_PersonasJuridicas_PersonasFisicas
END
GO
/****** Object:  StoredProcedure [dbo].[CPadron_AgregarTitularesSHPersonasFisicasSol]    Script Date: 11/24/2016 09:48:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[CPadron_AgregarTitularesSHPersonasFisicasSol]
(
	@id_cpadron int
	,@id_personajuridica int
	,@Apellido varchar(50)
	,@Nombres nvarchar(50)
	,@id_tipodoc_personal int
	,@Nro_Documento nvarchar(15)
	,@email nvarchar(70)
	,@id_titular_pj int OUTPUT
)
AS
BEGIN
	INSERT INTO CPadron_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas
    (
        id_cpadron
        ,id_personajuridica
        ,Apellido
        ,Nombres
        ,id_tipodoc_personal
        ,Nro_Documento
        ,Email
        ,firmante_misma_persona
	)
	VALUES
    (
		@id_cpadron
		,@id_personajuridica 
		,@Apellido 
		,@Nombres 
		,@id_tipodoc_personal 
		,@Nro_Documento 
		,@email 
		,1
	)
	SELECT @id_titular_pj=MAX(id_titular_pj) FROM CPadron_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas
END
GO
/****** Object:  StoredProcedure [dbo].[CPadron_DocumentosAdjuntos_Agregar]    Script Date: 11/24/2016 09:50:18 ******/
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
	INSERT INTO CPadron_DocumentosAdjuntos(   	
		id_cpadron 
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
		@id_cpadron  
		,@id_tdocreq
		,@tdocreq_detalle
		,@id_tipodocsis 
		,@id_file  
		,@generadoxSistema 
		,GETDATE()
		,@CreateUser
		,@nombre_archivo
		)
	SELECT @id_docadjunto=MAX(id_docadjunto) FROM CPadron_DocumentosAdjuntos
END
GO