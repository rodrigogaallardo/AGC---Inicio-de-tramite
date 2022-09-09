

/*Campo torre en titulares*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'Torre' AND Object_ID = Object_ID(N'SSIT_Solicitudes_Titulares_PersonasJuridicas')) BEGIN 
ALTER TABLE dbo.SSIT_Solicitudes_Titulares_PersonasJuridicas ADD
	Torre nvarchar(10) NULL
ALTER TABLE dbo.SSIT_Solicitudes_Titulares_PersonasJuridicas SET (LOCK_ESCALATION = TABLE)
END

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'Torre' AND Object_ID = Object_ID(N'SSIT_Solicitudes_Titulares_PersonasFisicas')) BEGIN 
ALTER TABLE dbo.SSIT_Solicitudes_Titulares_PersonasFisicas ADD
	Torre nvarchar(10) NULL
ALTER TABLE dbo.SSIT_Solicitudes_Titulares_PersonasFisicas SET (LOCK_ESCALATION = TABLE)
END

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'Torre' AND Object_ID = Object_ID(N'Encomienda_Titulares_PersonasJuridicas')) BEGIN 
ALTER TABLE dbo.Encomienda_Titulares_PersonasJuridicas ADD
	Torre nvarchar(10) NULL
ALTER TABLE dbo.Encomienda_Titulares_PersonasJuridicas SET (LOCK_ESCALATION = TABLE)
END

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'Torre' AND Object_ID = Object_ID(N'Encomienda_Titulares_PersonasFisicas')) BEGIN 
ALTER TABLE dbo.Encomienda_Titulares_PersonasFisicas ADD
	Torre nvarchar(10) NULL
ALTER TABLE dbo.Encomienda_Titulares_PersonasFisicas SET (LOCK_ESCALATION = TABLE)
END

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'Torre' AND Object_ID = Object_ID(N'CPadron_Titulares_PersonasJuridicas')) BEGIN 
ALTER TABLE dbo.CPadron_Titulares_PersonasJuridicas ADD
	Torre nvarchar(10) NULL
ALTER TABLE dbo.CPadron_Titulares_PersonasJuridicas SET (LOCK_ESCALATION = TABLE)
END

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'Torre' AND Object_ID = Object_ID(N'CPadron_Titulares_PersonasFisicas')) BEGIN 
ALTER TABLE dbo.CPadron_Titulares_PersonasFisicas ADD
	Torre nvarchar(10) NULL
ALTER TABLE dbo.CPadron_Titulares_PersonasFisicas SET (LOCK_ESCALATION = TABLE)
END

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'Torre' AND Object_ID = Object_ID(N'CPadron_Titulares_Solicitud_PersonasFisicas')) BEGIN 
ALTER TABLE dbo.CPadron_Titulares_Solicitud_PersonasFisicas ADD
	Torre nvarchar(10) NULL
ALTER TABLE dbo.CPadron_Titulares_Solicitud_PersonasFisicas SET (LOCK_ESCALATION = TABLE)
END

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'Torre' AND Object_ID = Object_ID(N'CPadron_Titulares_Solicitud_PersonasJuridicas')) BEGIN 
ALTER TABLE dbo.CPadron_Titulares_Solicitud_PersonasJuridicas ADD
	Torre nvarchar(10) NULL
ALTER TABLE dbo.CPadron_Titulares_Solicitud_PersonasJuridicas SET (LOCK_ESCALATION = TABLE)
END

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'Torre' AND Object_ID = Object_ID(N'EncomiendaExt_Titulares_PersonasFisicas')) BEGIN 
ALTER TABLE dbo.EncomiendaExt_Titulares_PersonasFisicas ADD
	Torre nvarchar(10) NULL
ALTER TABLE dbo.EncomiendaExt_Titulares_PersonasFisicas SET (LOCK_ESCALATION = TABLE)
END

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'Torre' AND Object_ID = Object_ID(N'EncomiendaExt_Titulares_PersonasJuridicas')) BEGIN 
ALTER TABLE dbo.EncomiendaExt_Titulares_PersonasJuridicas ADD
	Torre nvarchar(10) NULL
ALTER TABLE dbo.EncomiendaExt_Titulares_PersonasJuridicas SET (LOCK_ESCALATION = TABLE)
END

/*Torre ubicaciones*/
IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'Depto' AND Object_ID = Object_ID(N'Encomienda_Ubicaciones')) BEGIN 
ALTER TABLE dbo.Encomienda_Ubicaciones ADD
	Depto nvarchar(10) NULL
ALTER TABLE dbo.Encomienda_Ubicaciones SET (LOCK_ESCALATION = TABLE)
END
IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'Local' AND Object_ID = Object_ID(N'Encomienda_Ubicaciones')) BEGIN 
ALTER TABLE dbo.Encomienda_Ubicaciones ADD
	Local nvarchar(10) NULL
ALTER TABLE dbo.Encomienda_Ubicaciones SET (LOCK_ESCALATION = TABLE)
END
IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'Torre' AND Object_ID = Object_ID(N'Encomienda_Ubicaciones')) BEGIN 
ALTER TABLE dbo.Encomienda_Ubicaciones ADD
	Torre nvarchar(10) NULL
ALTER TABLE dbo.Encomienda_Ubicaciones SET (LOCK_ESCALATION = TABLE)
END


IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'Torre' AND Object_ID = Object_ID(N'CPadron_Ubicaciones')) BEGIN 
ALTER TABLE dbo.CPadron_Ubicaciones ADD
	Torre nvarchar(10) NULL
ALTER TABLE dbo.CPadron_Ubicaciones SET (LOCK_ESCALATION = TABLE)
END
IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'Local' AND Object_ID = Object_ID(N'CPadron_Ubicaciones')) BEGIN 
ALTER TABLE dbo.CPadron_Ubicaciones ADD
	Local nvarchar(10) NULL
ALTER TABLE dbo.CPadron_Ubicaciones SET (LOCK_ESCALATION = TABLE)
END
IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'Depto' AND Object_ID = Object_ID(N'CPadron_Ubicaciones')) BEGIN 
ALTER TABLE dbo.CPadron_Ubicaciones ADD
	Depto nvarchar(10) NULL
ALTER TABLE dbo.CPadron_Ubicaciones SET (LOCK_ESCALATION = TABLE)
END


IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'Torre' AND Object_ID = Object_ID(N'SSIT_Solicitudes_Ubicaciones')) BEGIN 
ALTER TABLE dbo.SSIT_Solicitudes_Ubicaciones ADD
	Torre nvarchar(10) NULL
ALTER TABLE dbo.SSIT_Solicitudes_Ubicaciones SET (LOCK_ESCALATION = TABLE)
END
IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'Local' AND Object_ID = Object_ID(N'SSIT_Solicitudes_Ubicaciones')) BEGIN 
ALTER TABLE dbo.SSIT_Solicitudes_Ubicaciones ADD
	Local nvarchar(10) NULL
ALTER TABLE dbo.SSIT_Solicitudes_Ubicaciones SET (LOCK_ESCALATION = TABLE)
END
IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'Depto' AND Object_ID = Object_ID(N'SSIT_Solicitudes_Ubicaciones')) BEGIN 
ALTER TABLE dbo.SSIT_Solicitudes_Ubicaciones ADD
	Depto nvarchar(10) NULL
ALTER TABLE dbo.SSIT_Solicitudes_Ubicaciones SET (LOCK_ESCALATION = TABLE)
END

/*STORED PROCEDURE PARA REPORTE CPADRON*/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Cpadron_Solicitud_DireccionesPartidasPlancheta]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[Cpadron_Solicitud_DireccionesPartidasPlancheta]
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




IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Cpadron_Imprimir_Solicitud]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Cpadron_Imprimir_Solicitud]
GO

CREATE PROCEDURE [dbo].[Cpadron_Imprimir_Solicitud]
	@IdCPadron INT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @PlantasHabilitar VARCHAR(MAX) = ''
			,@Descripcion VARCHAR(255)
			,@MuestraCampoAdicional BIT
			,@detalle_cpadrontiposector VARCHAR(100)

	DECLARE PlantasHabilitar CURSOR FOR
		SELECT 
			ts.Descripcion
			,ts.MuestraCampoAdicional
			,cpp.detalle_cpadrontiposector 
		FROM 
			CPadron_Plantas cpp
		INNER JOIN TipoSector ts on cpp.id_tiposector = ts.Id
		WHERE 
			cpp.id_cpadron = @IdCPadron

	OPEN PlantasHabilitar
		FETCH NEXT FROM PlantasHabilitar INTO @Descripcion, @MuestraCampoAdicional, @detalle_cpadrontiposector 
		WHILE @@FETCH_STATUS = 0 BEGIN

		IF len(@PlantasHabilitar) > 0 
			SET @PlantasHabilitar = @PlantasHabilitar + ', '
		
		SET @PlantasHabilitar = @PlantasHabilitar + @Descripcion + CASE WHEN @MuestraCampoAdicional = 1 THEN @detalle_cpadrontiposector ELSE '' END 

		FETCH NEXT FROM PlantasHabilitar INTO @Descripcion, @MuestraCampoAdicional, @detalle_cpadrontiposector 
		END
		CLOSE PlantasHabilitar
		DEALLOCATE PlantasHabilitar

----------------------------------------------------------------------------------------------------------------------------------------------------	
/*Solicitud Cpadron*/
	SELECT
		cp.id_cpadron																		AS [IdCpadron]
		,cp.CodigoSeguridad																	AS [CodigoSeguridad]
		,cp.ZonaDeclarada																	AS [ZonaDeclarada]
		,cp.CreateDate																		AS [CreateDate]
		,tt.descripcion_tipotramite															AS [TipoTramite]
		,cp.nro_matricula_escribano															AS [NroMatriculaEscribano]
		,cp.nombre_apellido_escribano														AS [NombreEscribano]
		,tn.Descripcion																		AS [TipoNormativa]
		,en.Descripcion																		AS [TipoEntidad]
		,cpn.nro_normativa																	AS [NroNormativa]
		,cp.observaciones_internas															AS [ObservacionesInternas]
		,@PlantasHabilitar																	AS [PlantasHabilitar]
	FROM CPadron_Solicitudes cp
		LEFT JOIN CPadron_Normativas cpn ON cp.id_cpadron = cpn.id_cpadron
		LEFT JOIN TipoTramite tt ON cp.id_tipotramite = tt.id_tipotramite
		LEFT JOIN TipoNormativa tn ON cpn.id_tiponormativa = tn.Id
		LEFT JOIN EntidadNormativa en ON en.Id = cpn.id_entidadnormativa
	WHERE
		cp.id_cpadron = @IdCPadron

----------------------------------------------------------------------------------------------------------------------------------------------------
/*Ubicacion*/
	SELECT 
	cpu.id_cpadronubicacion																	AS [IdCpadronUbicacion]
	,cpu.id_cpadron																			AS [IdCpadron]
	,cpu.id_ubicacion																		AS [IdUbicacion]
	,ubi.Seccion																			AS [Seccion]
	,ubi.Manzana																			AS [Manzana]
	,ubi.Parcela																			AS [Parcela]
	,ubi.NroPartidaMatriz																	AS [PartidaMatriz]
	,zp.CodZonaPla																			AS [ZonaPlaneamiento]
	,dbo.Cpadron_Solicitud_DireccionesPartidasPlancheta(cpu.id_cpadron, cpu.id_ubicacion)	AS [Direcciones]
	,cpu.deptoLocal_cpadronubicacion														AS [Otros]
	,cpu.Torre																				AS [Torre]
	,cpu.Depto																				AS [Depto]
	,cpu.Local																				AS [Local]
	FROM CPadron_Ubicaciones cpu
		INNER JOIN Ubicaciones ubi ON cpu.id_ubicacion = ubi.id_ubicacion
		INNER JOIN Zonas_Planeamiento zp ON zp.id_zonaplaneamiento = ubi.id_zonaplaneamiento
	WHERE 
		cpu.id_cpadron = @IdCPadron

----------------------------------------------------------------------------------------------------------------------------------------------------
/*Propiedad Horizontal*/
	SELECT 
		cpuph.id_cpadronprophorizontal														AS [IdCpadronPropHorizontal]
		,cpuph.id_cpadronubicacion															AS [IdCpadronUbicacion]
		,ubicph.NroPartidaHorizontal														AS [PartidaHorizontal]
		,ubicph.Piso																		AS [Piso]
		,ubicph.Depto																		AS [Depto]
	FROM CPadron_Ubicaciones cpu
		INNER JOIN CPadron_Ubicaciones_PropiedadHorizontal cpuph ON cpu.id_cpadronubicacion = cpuph.id_cpadronubicacion
		INNER JOIN Ubicaciones_PropiedadHorizontal ubicph ON cpuph.id_propiedadhorizontal = ubicph.id_propiedadhorizontal
	WHERE 
		cpu.id_cpadron = @IdCPadron

----------------------------------------------------------------------------------------------------------------------------------------------------
/*Conformacion Local
	SELECT 
		cpcl.id_cpadronconflocal															AS [IdCpadronConfLocal]
		,cpcl.id_cpadron																	AS [IdCpadron]
		,td.Descripcion																		AS [Destino]
		,cpcl.largo_conflocal																AS [Largo]
		,cpcl.ancho_conflocal																AS [Ancho]
		,cpcl.alto_conflocal																AS [Alto]
		,cpcl.Paredes_conflocal																AS [Paredes]
		,cpcl.Techos_conflocal																AS [Techos]
		,cpcl.Pisos_conflocal																AS [Pisos]
		,cpcl.Frisos_conflocal																AS [Frisos]
		,cpcl.Observaciones_conflocal														AS [Observaciones]
	FROM CPadron_ConformacionLocal cpcl
		INNER JOIN TipoDestino td ON cpcl.id_destino = td.Id
	WHERE 
		cpcl.id_cpadron = @IdCPadron
*/
----------------------------------------------------------------------------------------------------------------------------------------------------
/*Titulares*/
	SELECT
		cptpj.id_personajuridica															AS [IdPersona]
		,cptpj.id_cpadron																	AS [IdCpadron]
		,'PJ'				 																AS [TipoPersona]
		,UPPER(cptpj.Razon_Social)															AS [RazonSocial]
		,ts.Descripcion																		AS [TipoSociedad]
		,''																					AS [Apellido]
		,''																					AS [Nombres]
		,''																					AS [TipoDoc]
		,''																					AS [NroDoc]
		,tib.nom_tipoiibb																	AS [TipoIIBB]
		,cptpj.Nro_IIBB																		AS [NroIIBB]
		,cptpj.CUIT																			AS [CUIT]
		,1																					AS [MuestraEnTitulares]
	FROM CPadron_Titulares_PersonasJuridicas cptpj
		INNER JOIN TipoSociedad ts ON cptpj.Id_TipoSociedad = ts.Id
		INNER JOIN TiposDeIngresosBrutos tib ON cptpj.id_tipoiibb = tib.id_tipoiibb
	WHERE
		cptpj.id_cpadron = @IdCPadron
	UNION
	SELECT 
		cptpj.id_personajuridica															AS [IdPersona]
		,cptpj.id_cpadron																	AS [IdCpadron]
		,'PF'																				AS [TipoPersona]
		,''																					AS [RazonSocial]
		,''																					AS [TipoSociedad]
		,UPPER(cptpjpf.Apellido)															AS [Apellido]
		,UPPER(cptpjpf.Nombres)																AS [Nombres]
		,tdp.Nombre																			AS [TipoDoc]
		,cptpjpf.Nro_Documento																AS [NroDoc]
		,''																					AS [TipoIIBB]
		,''																					AS [NroIIBB]
		,''																					AS [CUIT]
		,0																					AS [MuestraEnTitulares]
	FROM CPadron_Titulares_PersonasJuridicas_PersonasFisicas cptpjpf
		INNER JOIN CPadron_Titulares_PersonasJuridicas cptpj ON cptpj.id_personajuridica = cptpjpf.id_personajuridica
		INNER JOIN TipoDocumentoPersonal tdp ON cptpjpf.id_tipodoc_personal = tdp.TipoDocumentoPersonalId
	WHERE 
		cptpj.Id_TipoSociedad IN (2, 32)
		AND cptpj.id_cpadron = @IdCPadron
	UNION
	SELECT 
		cptpf.id_personafisica																AS [IdPersona]
		,cptpf.id_cpadron																	AS [IdCpadron]
		,'PF'																				AS [TipoPersona]
		,''																					AS [RazonSocial]
		,''																					AS [TipoSociedad]
		,UPPER(cptpf.Apellido)																AS [Apellido]
		,UPPER(cptpf.Nombres)																AS [Nombres]
		,tdp.Nombre																			AS [TipoDoc]
		,cptpf.Nro_Documento																AS [NroDoc]
		,''																					AS [TipoIIBB]
		,''																					AS [NroIIBB]
		,''																					AS [CUIT]
		,1																					AS [MuestraEnTitulares]
	FROM CPadron_Titulares_PersonasFisicas cptpf
		INNER JOIN TipoDocumentoPersonal tdp ON cptpf.id_tipodoc_personal = tdp.TipoDocumentoPersonalId
		INNER JOIN TiposDeIngresosBrutos tib ON cptpf.id_tipoiibb = tib.id_tipoiibb
	WHERE 
		cptpf.id_cpadron = @IdCPadron

----------------------------------------------------------------------------------------------------------------------------------------------------	
/*Titulares Solicitud*/
	SELECT
		cptpj.id_personajuridica															AS [IdPersona]
		,cptpj.id_cpadron																	AS [IdCpadron]
		,'PJ'				 																AS [TipoPersona]
		,UPPER(cptpj.Razon_Social)															AS [RazonSocial]
		,ts.Descripcion																		AS [TipoSociedad]
		,''																					AS [Apellido]
		,''																					AS [Nombres]
		,''																					AS [TipoDoc]
		,''																					AS [NroDoc]
		,tib.nom_tipoiibb																	AS [TipoIIBB]
		,cptpj.Nro_IIBB																		AS [NroIIBB]
		,cptpj.CUIT																			AS [CUIT]
		,1																					AS [MuestraEnTitulares]
	FROM CPadron_Titulares_Solicitud_PersonasJuridicas cptpj
		INNER JOIN TipoSociedad ts ON cptpj.Id_TipoSociedad = ts.Id
		INNER JOIN TiposDeIngresosBrutos tib ON cptpj.id_tipoiibb = tib.id_tipoiibb
	WHERE
		cptpj.id_cpadron = @IdCPadron
	UNION
	SELECT 
		cptpj.id_personajuridica															AS [IdPersona]
		,cptpj.id_cpadron																	AS [IdCpadron]
		,'PF'																				AS [TipoPersona]
		,''																					AS [RazonSocial]
		,''																					AS [TipoSociedad]
		,UPPER(cptpjpf.Apellido)															AS [Apellido]
		,UPPER(cptpjpf.Nombres)																AS [Nombres]
		,tdp.Nombre																			AS [TipoDoc]
		,cptpjpf.Nro_Documento																AS [NroDoc]
		,''																					AS [TipoIIBB]
		,''																					AS [NroIIBB]
		,''																					AS [CUIT]
		,0																					AS [MuestraEnTitulares]
	FROM CPadron_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas cptpjpf
		INNER JOIN CPadron_Titulares_PersonasJuridicas cptpj ON cptpj.id_personajuridica = cptpjpf.id_personajuridica
		INNER JOIN TipoDocumentoPersonal tdp ON cptpjpf.id_tipodoc_personal = tdp.TipoDocumentoPersonalId
	WHERE 
		cptpj.Id_TipoSociedad IN (2, 32)
		AND cptpj.id_cpadron = @IdCPadron
	UNION
	SELECT 
		cptpf.id_personafisica																AS [IdPersona]
		,cptpf.id_cpadron																	AS [IdCpadron]
		,'PF'																				AS [TipoPersona]
		,''																					AS [RazonSocial]
		,''																					AS [TipoSociedad]
		,UPPER(cptpf.Apellido)																AS [Apellido]
		,UPPER(cptpf.Nombres)																AS [Nombres]
		,tdp.Nombre																			AS [TipoDoc]
		,cptpf.Nro_Documento																AS [NroDoc]
		,''																					AS [TipoIIBB]
		,''																					AS [NroIIBB]
		,''																					AS [CUIT]
		,1																					AS [MuestraEnTitulares]
	FROM CPadron_Titulares_Solicitud_PersonasFisicas cptpf
		INNER JOIN TipoDocumentoPersonal tdp ON cptpf.id_tipodoc_personal = tdp.TipoDocumentoPersonalId
		INNER JOIN TiposDeIngresosBrutos tib ON cptpf.id_tipoiibb = tib.id_tipoiibb
	WHERE 
		cptpf.id_cpadron = @IdCPadron

----------------------------------------------------------------------------------------------------------------------------------------------------
/*Rubros*/
	SELECT
		cpr.id_cpadronrubro																	AS [IdCpadronRubro]
		,cpr.id_cpadron																		AS [IdCpadron]
		,cpr.cod_rubro																		AS [CodRubro]
		,cpr.desc_rubro																		AS [Rubro]
		,ta.Descripcion																		AS [Actividad]
		,tdr.Descripcion																	AS [DocRequerida]
		,cpr.SuperficieHabilitar															AS [SuperficieHabilitar]
	FROM CPadron_Rubros cpr
		INNER JOIN TipoActividad ta ON cpr.id_tipoactividad = ta.Id
		INNER JOIN Tipo_Documentacion_Req tdr ON cpr.id_tipodocreq = tdr.Id
	WHERE
		cpr.id_cpadron = @IdCPadron

----------------------------------------------------------------------------------------------------------------------------------------------------
/*Datos Local*/
	SELECT 
		cpdl.id_cpadrondatoslocal															AS [IdCpadronDatosLocal]
		,cpdl.id_cpadron																	AS [IdCpadron]
		,cpdl.superficie_cubierta_dl														AS [SuperficieCubierta]
		,cpdl.superficie_descubierta_dl														AS [SuperficieDescubierta]
		,cpdl.dimesion_frente_dl															AS [DimensionFrente]
		,cpdl.frente_dl																		AS [Frente]
		,cpdl.fondo_dl																		AS [Fondo]
	FROM CPadron_DatosLocal cpdl
	WHERE
		cpdl.id_cpadron = @IdCPadron

END
GO


/*SP SGI*/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENG_Crear_Tarea]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ENG_Crear_Tarea]
GO

CREATE PROCEDURE [dbo].[ENG_Crear_Tarea]
(
	@id_tramite		int,
	@id_tarea		int,
	@CreateUser		uniqueidentifier,
	@id_tramitetarea int output
)
AS
BEGIN
	
	
	DECLARE		
		@cod_circuito nvarchar(10),
		@id_paquete int,
		@id_rel_tt int
	
	SELECT
		@cod_circuito = cir.cod_circuito
	FROM
		ENG_Tareas tarea
		INNER JOIN ENG_Circuitos cir ON tarea.id_circuito = cir.id_circuito
	WHERE
		tarea.id_tarea = @id_tarea
	
	
	IF @cod_circuito = 'SCP' OR @cod_circuito ='SSP' OR @cod_circuito ='ESPECIAL' OR @cod_circuito ='ESPAR'
		OR @cod_circuito = 'SCP2' OR @cod_circuito ='SSP2' OR @cod_circuito ='ESPECIAL2' OR @cod_circuito ='ESPAR2'
	BEGIN
		SELECT TOP 1 
			@id_tramitetarea = tt.id_tramitetarea
		FROM 
			SGI_Tramites_Tareas_HAB tt_HAB
			INNER JOIN SGI_Tramites_Tareas tt ON tt.id_tramitetarea=tt_HAB.id_tramitetarea
		WHERE 
			tt_HAB.id_solicitud = @id_tramite
			AND tt.id_tarea = @id_tarea
			AND tt.FechaCierre_tramitetarea IS NULL
		ORDER BY 
			tt.id_tramitetarea desc
	END
	ELSE IF @cod_circuito = 'CP'	
	BEGIN
		SELECT TOP 1 
			@id_tramitetarea = tt.id_tramitetarea
		FROM 
			SGI_Tramites_Tareas_CPADRON tt_CP
			INNER JOIN SGI_Tramites_Tareas tt ON tt.id_tramitetarea=tt_CP.id_tramitetarea
		WHERE 
			tt_CP.id_cpadron = @id_tramite
			AND tt.id_tarea = @id_tarea
			AND tt.FechaCierre_tramitetarea IS NULL
		ORDER BY tt.id_tramitetarea desc
	END
	ELSE IF @cod_circuito = 'TRANSF'	
	BEGIN
		SELECT TOP 1 
			@id_tramitetarea = tt.id_tramitetarea
		FROM 
			SGI_Tramites_Tareas_TRANSF tt_TR
			INNER JOIN SGI_Tramites_Tareas tt ON tt.id_tramitetarea=tt_TR.id_tramitetarea
		WHERE 
			tt_TR.id_solicitud = @id_tramite
			and tt.id_tarea = @id_tarea
			and tt.FechaCierre_tramitetarea IS NULL
		ORDER BY
			tt.id_tramitetarea desc
	END	
	
	SET @id_tramitetarea = ISNULL(@id_tramitetarea,0)
	
	IF @id_tramitetarea = 0
	BEGIN
	
		EXEC @id_tramitetarea  = Id_Nuevo 'SGI_Tramites_Tareas'
			
		INSERT INTO SGI_Tramites_Tareas( id_tramitetarea,id_tarea, FechaInicio_tramitetarea,id_resultado, CreateUser)
		VALUES(@id_tramitetarea,@id_tarea,GETDATE(), 0, @CreateUser)

		IF @cod_circuito = 'SCP' OR @cod_circuito ='SSP' OR @cod_circuito ='ESPECIAL' OR @cod_circuito ='ESPAR'
			OR @cod_circuito = 'SCP2' OR @cod_circuito ='SSP2' OR @cod_circuito ='ESPECIAL2' OR @cod_circuito ='ESPAR2'
		BEGIN
			
			EXEC @id_rel_tt  = Id_Nuevo 'SGI_Tramites_Tareas_HAB'
			
			INSERT INTO SGI_Tramites_Tareas_HAB(id_rel_tt_HAB,id_tramitetarea,id_solicitud)
			VALUES(@id_rel_tt ,@id_tramitetarea,@id_tramite)
			
			IF @id_tarea = dbo.Bus_id_tarea(125) OR  
				@id_tarea = dbo.Bus_id_tarea(225) OR 
				@id_tarea = dbo.Bus_id_tarea(325) OR 
				@id_tarea = dbo.Bus_id_tarea(625)
				--Corrección de la Solicitud
			BEGIN
				
				UPDATE SSIT_Solicitudes
				SET 
					id_estado = dbo.Bus_idEstadoSolicitud('COMP'),
					LastUpdateDate = GETDATE(),
					LastUpdateUser = @CreateUser
				WHERE 
					id_solicitud = @id_tramite
			
			END
			ELSE IF @id_tarea = dbo.Bus_id_tarea(1125) OR
				@id_tarea = dbo.Bus_id_tarea(1225) OR
				@id_tarea = dbo.Bus_id_tarea(1325) OR
				@id_tarea = dbo.Bus_id_tarea(1425) 
				--Corrección de la Solicitud
			BEGIN
				
				UPDATE SSIT_Solicitudes
				SET 
					id_estado = dbo.Bus_idEstadoSolicitud('OBSERVADO'),
					LastUpdateDate = GETDATE(),
					LastUpdateUser = @CreateUser
				WHERE 
					id_solicitud = @id_tramite
			
			END
			
		END
		ELSE IF @cod_circuito = 'CP' 
		BEGIN
			
			EXEC @id_rel_tt  = Id_Nuevo 'SGI_Tramites_Tareas_CPADRON'
			
			INSERT INTO SGI_Tramites_Tareas_CPADRON(id_rel_tt_CPADRON,id_tramitetarea,id_cpadron)
			VALUES(@id_rel_tt ,@id_tramitetarea,@id_tramite)
			
			IF @id_tarea = dbo.Bus_id_tarea(425)  --Corrección de la Solicitud
			BEGIN
				UPDATE cpadron_solicitudes
				SET id_estado = dbo.Bus_idEstadoCPadron('COMP'),
					LastUpdateDate = GETDATE(),
					LastUpdateUser = @CreateUser
				WHERE id_cpadron = @id_tramite
			END
			
		END
		ELSE IF @cod_circuito = 'TRANSF' 
		BEGIN
			
			EXEC @id_rel_tt  = Id_Nuevo 'SGI_Tramites_Tareas_TRANSF'
			
			INSERT INTO SGI_Tramites_Tareas_TRANSF(id_rel_tt_TRANSF,id_tramitetarea,id_solicitud)
			VALUES(@id_rel_tt ,@id_tramitetarea,@id_tramite)
			
			IF @id_tarea = dbo.Bus_id_tarea(525)  --Corrección de la Solicitud
			BEGIN
				UPDATE Transf_Solicitudes
				SET id_estado = dbo.Bus_idEstadoSolicitud('COMP'),
					LastUpdateDate = GETDATE(),
					LastUpdateUser = @CreateUser
				WHERE id_solicitud = @id_tramite
			END
			
		END
	END

END
/****** Object:  UserDefinedFunction [dbo].[Bus_id_solicitud]    Script Date: 05/02/2016 17:36:19 ******/
SET ANSI_NULLS ON



GO


GO
IF NOT EXISTS(SELECT * FROM sys.columns  WHERE Name = N'CodArea' AND Object_ID = Object_ID(N'SSIT_Solicitudes')) BEGIN
	ALTER TABLE dbo.SSIT_Solicitudes ADD
		CodArea nvarchar(5) NULL
END

IF NOT EXISTS(SELECT * FROM sys.columns  WHERE Name = N'Prefijo' AND Object_ID = Object_ID(N'SSIT_Solicitudes')) BEGIN
	ALTER TABLE dbo.SSIT_Solicitudes ADD
		Prefijo nvarchar(5) NULL
END

IF NOT EXISTS(SELECT * FROM sys.columns  WHERE Name = N'Sufijo' AND Object_ID = Object_ID(N'SSIT_Solicitudes')) BEGIN
	ALTER TABLE dbo.SSIT_Solicitudes ADD
		Sufijo nvarchar(5) NULL
END

GO

ALTER TABLE dbo.SSIT_Solicitudes SET (LOCK_ESCALATION = TABLE)

GO



/****** Object:  Index [PK_CPadron_Titulares_PersonasFisicas]    Script Date: 01/06/2017 11:32:40 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[CPadron_Titulares_PersonasFisicas]') AND name = N'PK_CPadron_Titulares_PersonasFisicas')


/****** Object:  Index [PK_CPadron_Titulares_PersonasFisicas]    Script Date: 01/06/2017 11:32:40 ******/
ALTER TABLE [dbo].[CPadron_Titulares_PersonasFisicas] ADD  CONSTRAINT [PK_CPadron_Titulares_PersonasFisicas] PRIMARY KEY CLUSTERED 
(
	[id_personafisica] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]

GO



/****** Object:  Index [PK_CPadron_Titulares_PersonasJuridicas]    Script Date: 01/06/2017 11:33:34 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[CPadron_Titulares_PersonasJuridicas]') AND name = N'PK_CPadron_Titulares_PersonasJuridicas')

/****** Object:  Index [PK_CPadron_Titulares_PersonasJuridicas]    Script Date: 01/06/2017 11:33:34 ******/
ALTER TABLE [dbo].[CPadron_Titulares_PersonasJuridicas] ADD  CONSTRAINT [PK_CPadron_Titulares_PersonasJuridicas] PRIMARY KEY CLUSTERED 
(
	[id_personajuridica] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]


GO



UPDATE Rel_TiposDeDocumentosRequeridos_TipoNormativa set id_tdocreq = 21 where id_tdoc_tnor  = 4

GO


IF NOT EXISTS (SELECT * FROM TiposDeDocumentosSistema WHERE cod_tipodocsis = 'CERTIFICADO_CAA') BEGIN
	INSERT INTO TiposDeDocumentosSistema(id_tipdocsis, cod_tipodocsis, nombre_tipodocsis, CreateDate)
	VALUES (4, 'CERTIFICADO_CAA', 'Certificado de Aptitud Ambiental', GETDATE())
END

UPDATE TiposDeDocumentosRequeridos SET id_tipdocsis = 4 where nombre_tdocreq like '%aptitud%ambiental%'
UPDATE TiposDeDocumentosRequeridos SET id_tipdocsis = 2 where id_tdocreq = 52
UPDATE TiposDeDocumentosRequeridos SET id_tipdocsis = 10 where nombre_tdocreq like '%actua%not%'


GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENG_Crear_Tarea]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ENG_Crear_Tarea]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ENG_Crear_Tarea]
(
	@id_tramite		int,
	@id_tarea		int,
	@CreateUser		uniqueidentifier,
	@id_tramitetarea int output
)
AS
BEGIN
	
	
	DECLARE		
		@cod_circuito nvarchar(10),
		@id_paquete int,
		@id_rel_tt int
	
	SELECT
		@cod_circuito = cir.cod_circuito
	FROM
		ENG_Tareas tarea
		INNER JOIN ENG_Circuitos cir ON tarea.id_circuito = cir.id_circuito
	WHERE
		tarea.id_tarea = @id_tarea
	
	
	IF @cod_circuito = 'SCP' OR @cod_circuito ='SSP' OR @cod_circuito ='ESPECIAL' OR @cod_circuito ='ESPAR'
		OR @cod_circuito = 'SCP2' OR @cod_circuito ='SSP2' OR @cod_circuito ='ESPECIAL2' OR @cod_circuito ='ESPAR2'
	BEGIN
		SELECT TOP 1 
			@id_tramitetarea = tt.id_tramitetarea
		FROM 
			SGI_Tramites_Tareas_HAB tt_HAB
			INNER JOIN SGI_Tramites_Tareas tt ON tt.id_tramitetarea=tt_HAB.id_tramitetarea
		WHERE 
			tt_HAB.id_solicitud = @id_tramite
			AND tt.id_tarea = @id_tarea
			AND tt.FechaCierre_tramitetarea IS NULL
		ORDER BY 
			tt.id_tramitetarea desc
	END
	ELSE IF @cod_circuito = 'CP'	
	BEGIN
		SELECT TOP 1 
			@id_tramitetarea = tt.id_tramitetarea
		FROM 
			SGI_Tramites_Tareas_CPADRON tt_CP
			INNER JOIN SGI_Tramites_Tareas tt ON tt.id_tramitetarea=tt_CP.id_tramitetarea
		WHERE 
			tt_CP.id_cpadron = @id_tramite
			AND tt.id_tarea = @id_tarea
			AND tt.FechaCierre_tramitetarea IS NULL
		ORDER BY tt.id_tramitetarea desc
	END
	ELSE IF @cod_circuito = 'TRANSF'	
	BEGIN
		SELECT TOP 1 
			@id_tramitetarea = tt.id_tramitetarea
		FROM 
			SGI_Tramites_Tareas_TRANSF tt_TR
			INNER JOIN SGI_Tramites_Tareas tt ON tt.id_tramitetarea=tt_TR.id_tramitetarea
		WHERE 
			tt_TR.id_solicitud = @id_tramite
			and tt.id_tarea = @id_tarea
			and tt.FechaCierre_tramitetarea IS NULL
		ORDER BY
			tt.id_tramitetarea desc
	END	
	
	SET @id_tramitetarea = ISNULL(@id_tramitetarea,0)
	
	IF @id_tramitetarea = 0
	BEGIN
	
		EXEC @id_tramitetarea  = Id_Nuevo 'SGI_Tramites_Tareas'
			
		INSERT INTO SGI_Tramites_Tareas( id_tramitetarea,id_tarea, FechaInicio_tramitetarea,id_resultado, CreateUser)
		VALUES(@id_tramitetarea,@id_tarea,GETDATE(), 0, @CreateUser)

		IF @cod_circuito = 'SCP' OR @cod_circuito ='SSP' OR @cod_circuito ='ESPECIAL' OR @cod_circuito ='ESPAR'
			OR @cod_circuito = 'SCP2' OR @cod_circuito ='SSP2' OR @cod_circuito ='ESPECIAL2' OR @cod_circuito ='ESPAR2'
		BEGIN
			
			EXEC @id_rel_tt  = Id_Nuevo 'SGI_Tramites_Tareas_HAB'
			
			INSERT INTO SGI_Tramites_Tareas_HAB(id_rel_tt_HAB,id_tramitetarea,id_solicitud)
			VALUES(@id_rel_tt ,@id_tramitetarea,@id_tramite)
			
			IF @id_tarea = dbo.Bus_id_tarea(125) OR  
				@id_tarea = dbo.Bus_id_tarea(225) OR 
				@id_tarea = dbo.Bus_id_tarea(325) OR 
				@id_tarea = dbo.Bus_id_tarea(625)
				--Corrección de la Solicitud
			BEGIN
				
				UPDATE SSIT_Solicitudes
				SET 
					id_estado = dbo.Bus_idEstadoSolicitud('OBSERVADO'),
					LastUpdateDate = GETDATE(),
					LastUpdateUser = @CreateUser
				WHERE 
					id_solicitud = @id_tramite
			
			END
			ELSE IF @id_tarea = dbo.Bus_id_tarea(1125) OR
				@id_tarea = dbo.Bus_id_tarea(1225) OR
				@id_tarea = dbo.Bus_id_tarea(1325) OR
				@id_tarea = dbo.Bus_id_tarea(1425) 
				--Corrección de la Solicitud
			BEGIN
				
				UPDATE SSIT_Solicitudes
				SET 
					id_estado = dbo.Bus_idEstadoSolicitud('OBSERVADO'),
					LastUpdateDate = GETDATE(),
					LastUpdateUser = @CreateUser
				WHERE 
					id_solicitud = @id_tramite
			
			END
			
		END
		ELSE IF @cod_circuito = 'CP' 
		BEGIN
			
			EXEC @id_rel_tt  = Id_Nuevo 'SGI_Tramites_Tareas_CPADRON'
			
			INSERT INTO SGI_Tramites_Tareas_CPADRON(id_rel_tt_CPADRON,id_tramitetarea,id_cpadron)
			VALUES(@id_rel_tt ,@id_tramitetarea,@id_tramite)
			
			IF @id_tarea = dbo.Bus_id_tarea(425)  --Corrección de la Solicitud
			BEGIN
				UPDATE cpadron_solicitudes
				SET id_estado = dbo.Bus_idEstadoCPadron('COMP'),
					LastUpdateDate = GETDATE(),
					LastUpdateUser = @CreateUser
				WHERE id_cpadron = @id_tramite
			END
			
		END
		ELSE IF @cod_circuito = 'TRANSF' 
		BEGIN
			
			EXEC @id_rel_tt  = Id_Nuevo 'SGI_Tramites_Tareas_TRANSF'
			
			INSERT INTO SGI_Tramites_Tareas_TRANSF(id_rel_tt_TRANSF,id_tramitetarea,id_solicitud)
			VALUES(@id_rel_tt ,@id_tramitetarea,@id_tramite)
			
			IF @id_tarea = dbo.Bus_id_tarea(525)  --Corrección de la Solicitud
			BEGIN
				UPDATE Transf_Solicitudes
				SET id_estado = dbo.Bus_idEstadoSolicitud('COMP'),
					LastUpdateDate = GETDATE(),
					LastUpdateUser = @CreateUser
				WHERE id_solicitud = @id_tramite
			END
			
		END
	END

END
/****** Object:  UserDefinedFunction [dbo].[Bus_id_solicitud]    Script Date: 05/02/2016 17:36:19 ******/
SET ANSI_NULLS ON
GO

/*agregar el campo id_file a la tabla de historial*/
if not exists (select * from syscolumns where id=object_id('SSIT_Solicitudes_HistorialUsuarios') and name='id_file')
    alter table [dbo].[SSIT_Solicitudes_HistorialUsuarios] add id_file int NULL
go


/*Agregar Insert para los titulares de las empresas */
ALTER PROCEDURE [dbo].[SSIT_Solicitudes_Actualizar_Usuario] 
(
	@id_solicitud int,
	@Usuario_anterior uniqueidentifier, 
	@Usuario_nuevo uniqueidentifier,
	@Usuario_Editor uniqueidentifier,
	@id_file int,
	@id_solicitud_historial int
)
AS
BEGIN
BEGIN TRANSACTION [Tran1]

BEGIN TRY

/*Modifico la tabla*/
	UPDATE 
		SSIT_Solicitudes
	SET
		CreateUser = @Usuario_nuevo
		
	WHERE
		id_solicitud = @id_solicitud

/*Inserto en la tabla de historial*/

	EXEC @id_solicitud_historial = Id_Nuevo 'SSIT_Solicitudes_HistorialUsuarios'
		

			INSERT INTO SSIT_Solicitudes_HistorialUsuarios(
				id_solicituduser
				,id_solicitud
				,Usuario_Origen
				,Usuario_Destino
				,Usuario_Editor
				,FechaHora_Modificación
				,id_file 
			) 
			Values (
				@id_solicitud_historial
				,@id_solicitud
				,@Usuario_anterior
				,@Usuario_nuevo
				,@Usuario_Editor
				,GETDATE()
				,@id_file
			)
				

COMMIT TRANSACTION [Tran1]

END TRY
BEGIN CATCH
  ROLLBACK TRANSACTION [Tran1]
END CATCH  
END