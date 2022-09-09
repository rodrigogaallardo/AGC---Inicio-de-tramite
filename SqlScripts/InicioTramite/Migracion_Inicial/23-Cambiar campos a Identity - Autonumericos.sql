DECLARE 
	@SQL nvarchar(4000)

SET @SQL = N'
use tempdb

dbcc shrinkfile (tempdev, 20)

dbcc shrinkfile (templog, 10)

'
EXECUTE sp_executesql @SQL
GO

BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Transf_DocumentosAdjuntos
	DROP CONSTRAINT FK_Transf_DocumentosAdjuntos_Niveles_Agrupamiento
GO
ALTER TABLE dbo.Niveles_Agrupamiento SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.CPadron_Solicitudes
	DROP CONSTRAINT FK_CPadron_Solicitudes_CPadron_Estados
GO
ALTER TABLE dbo.CPadron_Estados SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.CPadron_Normativas
	DROP CONSTRAINT FK_CPadron_Normativas_EntidadNormativa
GO
ALTER TABLE dbo.Encomienda_Normativas
	DROP CONSTRAINT FK_Encomienda_Normativas_EntidadNormativa
GO
ALTER TABLE dbo.EntidadNormativa SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Encomienda_Normativas
	DROP CONSTRAINT FK_Encomienda_Normativas_TipoNormativa
GO
ALTER TABLE dbo.CPadron_Normativas
	DROP CONSTRAINT FK_CPadron_Normativas_TipoNormativa
GO
ALTER TABLE dbo.TipoNormativa SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.CPadron_Rubros
	DROP CONSTRAINT FK_CPadron_Rubros_TipoActividad
GO
ALTER TABLE dbo.Encomienda_Rubros
	DROP CONSTRAINT FK_Encomienda_Rubros_TipoActividad
GO
ALTER TABLE dbo.TipoActividad SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.CPadron_Rubros
	DROP CONSTRAINT FK_CPadron_Rubros_Tipo_Documentacion_Req
GO
ALTER TABLE dbo.Encomienda_Rubros
	DROP CONSTRAINT FK_Encomienda_Rubros_Tipo_Documentacion_Req
GO
ALTER TABLE dbo.Tipo_Documentacion_Req SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.CPadron_Rubros
	DROP CONSTRAINT FK_CPadron_Rubros_ImpactoAmbiental
GO
ALTER TABLE dbo.Encomienda_Rubros
	DROP CONSTRAINT FK_Encomienda_Rubros_ImpactoAmbiental
GO
ALTER TABLE dbo.ImpactoAmbiental SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Encomienda_Certificado_Sobrecarga
	DROP CONSTRAINT FK_Encomienda_Certificado_Sobrecarga_Encomienda_Tipos_Sobrecargas
GO
ALTER TABLE dbo.Encomienda_Tipos_Sobrecargas SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Encomienda_Certificado_Sobrecarga
	DROP CONSTRAINT FK_Encomienda_Certificado_Sobrecarga_Encomienda_Tipos_Certificados_Sobrecarga
GO
ALTER TABLE dbo.Encomienda_Tipos_Certificados_Sobrecarga SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Encomienda
	DROP CONSTRAINT FK_Encomienda_Profesional
GO
ALTER TABLE dbo.Profesional SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Encomienda
	DROP CONSTRAINT FK_Encomienda_Encomienda_Estados
GO
ALTER TABLE dbo.Encomienda_Estados SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Encomienda_Planos
	DROP CONSTRAINT FK_Encomienda_Planos_Tipo_Plano
GO
ALTER TABLE dbo.TiposDePlanos SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Encomienda_ConformacionLocal
	DROP CONSTRAINT FK_Encomienda_ConformacionLocal_TipoDestino
GO
ALTER TABLE dbo.CPadron_ConformacionLocal
	DROP CONSTRAINT FK_CPadron_ConformacionLocal_TipoDestino
GO
ALTER TABLE dbo.TipoDestino SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Encomienda_ConformacionLocal
	DROP CONSTRAINT FK_Encomienda_ConformacionLocal_Tipo_Superficie
GO
ALTER TABLE dbo.CPadron_ConformacionLocal
	DROP CONSTRAINT FK_CPadron_ConformacionLocal_Tipo_Superficie
GO
ALTER TABLE dbo.TipoSuperficie SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Encomienda_ConformacionLocal
	DROP CONSTRAINT FK_Encomienda_ConformacionLocal_tipo_ventilacion
GO
ALTER TABLE dbo.CPadron_ConformacionLocal
	DROP CONSTRAINT FK_CPadron_ConformacionLocal_tipo_ventilacion
GO
ALTER TABLE dbo.tipo_ventilacion SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Encomienda_ConformacionLocal
	DROP CONSTRAINT FK_Encomienda_ConformacionLocal_tipo_iluminacion
GO
ALTER TABLE dbo.CPadron_ConformacionLocal
	DROP CONSTRAINT FK_CPadron_ConformacionLocal_tipo_iluminacion
GO
ALTER TABLE dbo.tipo_iluminacion SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.CPadron_Plantas
	DROP CONSTRAINT FK_CPadron_Plantas_TipoSector
GO
ALTER TABLE dbo.Encomienda_Plantas
	DROP CONSTRAINT FK_Encomienda_Plantas_TipoSector
GO
ALTER TABLE dbo.TipoSector SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Encomienda_Sobrecarga_Detalle1
	DROP CONSTRAINT FK_Encomienda_Sobrecarga_Detalle1_Encomienda_Tipos_Destinos
GO
ALTER TABLE dbo.Encomienda_Tipos_Destinos SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Encomienda_Sobrecarga_Detalle2
	DROP CONSTRAINT FK_Encomienda_Sobrecarga_Detalle2_Encomienda_Tipos_Usos
GO
ALTER TABLE dbo.Encomienda_Sobrecarga_Detalle2
	DROP CONSTRAINT FK_Encomienda_Sobrecarga_Detalle2_Encomienda_Tipos_Usos1
GO
ALTER TABLE dbo.Encomienda_Sobrecarga_Detalle1
	DROP CONSTRAINT FK_Encomienda_Sobrecarga_Detalle1_Encomienda_Tipos_Usos
GO
ALTER TABLE dbo.Encomienda_Tipos_Usos SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.CPadron_Titulares_Solicitud_PersonasJuridicas
	DROP CONSTRAINT FK_CPadron_Titulares_Solicitud_PersonasJuridicas_TipoSociedad
GO
ALTER TABLE dbo.Encomienda_Titulares_PersonasJuridicas
	DROP CONSTRAINT FK_Encomienda_Titulares_PersonasJuridicas_TipoSociedad
GO
ALTER TABLE dbo.SSIT_Solicitudes_Titulares_PersonasJuridicas
	DROP CONSTRAINT FK_SSIT_Solicitudes_Titulares_PersonasJuridicas_TipoSociedad
GO
ALTER TABLE dbo.TipoSociedad SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.CPadron_Ubicaciones
	DROP CONSTRAINT FK_CPadron_Ubicaciones_Zonas_Planeamiento
GO
ALTER TABLE dbo.SSIT_Solicitudes_Ubicaciones
	DROP CONSTRAINT FK_SSIT_Solicitudes_Ubicaciones_Zonas_Planeamiento
GO
ALTER TABLE dbo.Encomienda_Ubicaciones
	DROP CONSTRAINT FK_Encomienda_Ubicaciones_Zonas_Planeamiento
GO
ALTER TABLE dbo.Zonas_Planeamiento SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.CPadron_Ubicaciones
	DROP CONSTRAINT FK_CPadron_Ubicaciones_Ubicaciones
GO
ALTER TABLE dbo.Encomienda_Ubicaciones
	DROP CONSTRAINT FK_Encomienda_Ubicaciones_Ubicaciones
GO
ALTER TABLE dbo.SSIT_Solicitudes_Ubicaciones
	DROP CONSTRAINT FK_SSIT_Solicitudes_Ubicaciones_Ubicaciones
GO
ALTER TABLE dbo.Ubicaciones SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.CPadron_Ubicaciones
	DROP CONSTRAINT FK_CPadron_Ubicaciones_SubTiposDeUbicacion
GO
ALTER TABLE dbo.SSIT_Solicitudes_Ubicaciones
	DROP CONSTRAINT FK_SSIT_Solicitudes_Ubicaciones_SubTiposDeUbicacion
GO
ALTER TABLE dbo.Encomienda_Ubicaciones
	DROP CONSTRAINT FK_Encomienda_Ubicaciones_SubTiposDeUbicacion
GO
ALTER TABLE dbo.SubTiposDeUbicacion SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal
	DROP CONSTRAINT FK_SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal_Ubicaciones_PropiedadHorizontal
GO
ALTER TABLE dbo.Encomienda_Ubicaciones_PropiedadHorizontal
	DROP CONSTRAINT FK_Encomienda_Ubicaciones_PropiedadHorizontal_Ubicaciones_PropiedadHorizontal
GO
ALTER TABLE dbo.Ubicaciones_PropiedadHorizontal SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.CPadron_Solicitudes
	DROP CONSTRAINT FK_CPadron_Solicitudes_TipoTramite
GO
ALTER TABLE dbo.SSIT_Solicitudes
	DROP CONSTRAINT FK_SSIT_Solicitudes_TipoTramite
GO
ALTER TABLE dbo.Encomienda
	DROP CONSTRAINT FK_Encomienda_TipoTramite
GO
ALTER TABLE dbo.Transf_Solicitudes
	DROP CONSTRAINT FK_Transf_Solicitudes_TipoTramite
GO
ALTER TABLE dbo.TipoTramite SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.CPadron_Solicitudes
	DROP CONSTRAINT FK_CPadron_Solicitudes_TipoExpediente
GO
ALTER TABLE dbo.SSIT_Solicitudes
	DROP CONSTRAINT FK_SSIT_Solicitudes_TipoExpediente
GO
ALTER TABLE dbo.Encomienda
	DROP CONSTRAINT FK_Encomienda_TipoExpediente
GO
ALTER TABLE dbo.Transf_Solicitudes
	DROP CONSTRAINT FK_Transf_Solicitudes_TipoExpediente
GO
ALTER TABLE dbo.TipoExpediente SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.SSIT_Solicitudes
	DROP CONSTRAINT FK_SSIT_Solicitudes_TipoEstadoSolicitud
GO
ALTER TABLE dbo.Transf_Solicitudes
	DROP CONSTRAINT FK_Transf_Solicitudes_TipoEstadoSolicitud
GO
ALTER TABLE dbo.TipoEstadoSolicitud SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.CPadron_Solicitudes
	DROP CONSTRAINT FK_CPadron_Solicitudes_SubtipoExpediente
GO
ALTER TABLE dbo.SSIT_Solicitudes
	DROP CONSTRAINT FK_SSIT_Solicitudes_SubtipoExpediente
GO
ALTER TABLE dbo.Encomienda
	DROP CONSTRAINT FK_Encomienda_SubtipoExpediente
GO
ALTER TABLE dbo.Transf_Solicitudes
	DROP CONSTRAINT FK_Transf_Solicitudes_SubtipoExpediente
GO
ALTER TABLE dbo.SubtipoExpediente SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.CPadron_DocumentosAdjuntos
	DROP CONSTRAINT FK_CPadron_DocumentosAdjuntos_TiposDeDocumentosSistema
GO
ALTER TABLE dbo.SSIT_DocumentosAdjuntos
	DROP CONSTRAINT FK_SSIT_DocumentosAdjuntos_TiposDeDocumentosSistema
GO
ALTER TABLE dbo.Encomienda_DocumentosAdjuntos
	DROP CONSTRAINT FK_Encomienda_DocumentosAdjuntos_TiposDeDocumentosSistema
GO
ALTER TABLE dbo.Transf_DocumentosAdjuntos
	DROP CONSTRAINT FK_Transf_DocumentosAdjuntos_TiposDeDocumentosSistema
GO
ALTER TABLE dbo.TiposDeDocumentosSistema SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.CPadron_DocumentosAdjuntos
	DROP CONSTRAINT FK_CPadron_DocumentosAdjuntos_TiposDeDocumentosRequeridos
GO
ALTER TABLE dbo.SSIT_DocumentosAdjuntos
	DROP CONSTRAINT FK_SSIT_DocumentosAdjuntos_TiposDeDocumentosRequeridos
GO
ALTER TABLE dbo.Encomienda_DocumentosAdjuntos
	DROP CONSTRAINT FK_Encomienda_DocumentosAdjuntos_TiposDeDocumentosRequeridos
GO
ALTER TABLE dbo.Transf_DocumentosAdjuntos
	DROP CONSTRAINT FK_Transf_DocumentosAdjuntos_TiposDeDocumentosRequeridos
GO
ALTER TABLE dbo.TiposDeDocumentosRequeridos SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.CPadron_DatosLocal
	DROP CONSTRAINT FK_CPadron_DatosLocal_aspnet_Users_CreateUser
GO
ALTER TABLE dbo.CPadron_DatosLocal
	DROP CONSTRAINT FK_CPadron_DatosLocal_aspnet_Users_LastUpdateUser
GO
ALTER TABLE dbo.Transf_Titulares_PersonasJuridicas
	DROP CONSTRAINT FK_Transf_Titulares_PersonasJuridicas_aspnet_Users_CreateUser
GO
ALTER TABLE dbo.Transf_Titulares_PersonasJuridicas
	DROP CONSTRAINT FK_Transf_Titulares_PersonasJuridicas_aspnet_Users_UpdateUser
GO
ALTER TABLE dbo.CPadron_Normativas
	DROP CONSTRAINT FK_CPadron_Normativas_aspnet_Users_CreateUser
GO
ALTER TABLE dbo.SSIT_Solicitudes_Encomienda
	DROP CONSTRAINT FK_SSIT_Solicitudes_Encomienda_aspnet_Users
GO
ALTER TABLE dbo.SSIT_DocumentosAdjuntos
	DROP CONSTRAINT FK_SSIT_DocumentosAdjuntos_aspnet_Users
GO
ALTER TABLE dbo.CPadron_Normativas
	DROP CONSTRAINT FK_CPadron_Normativas_aspnet_Users_LastUpdateUser
GO
ALTER TABLE dbo.SSIT_DocumentosAdjuntos
	DROP CONSTRAINT FK_SSIT_DocumentosAdjuntos_SSIT_DocumentosAdjuntos
GO
ALTER TABLE dbo.CPadron_Solicitudes
	DROP CONSTRAINT FK_CPadron_Solicitudes_aspnet_Users_CreateUser
GO
ALTER TABLE dbo.CPadron_Solicitudes
	DROP CONSTRAINT FK_CPadron_Solicitudes_aspnet_Users_LastUpdateUser
GO
ALTER TABLE dbo.CPadron_Ubicaciones
	DROP CONSTRAINT FK_CPadron_Ubicaciones_aspnet_Users_CreateUser
GO
ALTER TABLE dbo.SSIT_Solicitudes_Pagos
	DROP CONSTRAINT FK_SSIT_Solicitudes_Pagos_aspnet_Users
GO
ALTER TABLE dbo.Encomienda_DocumentosAdjuntos
	DROP CONSTRAINT FK_Encomienda_DocumentosAdjuntos_Encomienda_DocumentosAdjuntos
GO
ALTER TABLE dbo.Encomienda_DocumentosAdjuntos
	DROP CONSTRAINT FK_Encomienda_DocumentosAdjuntos_aspnet_Users
GO
ALTER TABLE dbo.Encomienda
	DROP CONSTRAINT FK_Encomienda_aspnet_Users
GO
ALTER TABLE dbo.Transf_Titulares_PersonasFisicas
	DROP CONSTRAINT FK_Transf_Titulares_PersonasFisicas_aspnet_Users_CreateUser
GO
ALTER TABLE dbo.Transf_Titulares_PersonasFisicas
	DROP CONSTRAINT FK_Transf_Titulares_PersonasFisicas_aspnet_Users_UpdateUser
GO
ALTER TABLE dbo.aspnet_Users SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_CPadron_Solicitudes
	(
	id_cpadron int NOT NULL IDENTITY (1, 1),
	CodigoSeguridad nvarchar(10) NOT NULL,
	id_tipotramite int NOT NULL,
	id_tipoexpediente int NOT NULL,
	id_subtipoexpediente int NOT NULL,
	id_estado int NOT NULL,
	CreateDate datetime NOT NULL,
	CreateUser uniqueidentifier NOT NULL,
	LastUpdateDate datetime NULL,
	LastUpdateUser uniqueidentifier NULL,
	observaciones_internas varchar(2000) NULL,
	observaciones_contribuyente varchar(2000) NULL,
	ZonaDeclarada nvarchar(15) NULL,
	nro_expediente_anterior nvarchar(50) NULL,
	observaciones varchar(2000) NULL,
	nombre_apellido_escribano nvarchar(1000) NULL,
	nro_matricula_escribano int NULL,
	NroExpedienteSade nvarchar(50) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_CPadron_Solicitudes SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_CPadron_Solicitudes ON
GO
IF EXISTS(SELECT * FROM dbo.CPadron_Solicitudes)
	 EXEC('INSERT INTO dbo.Tmp_CPadron_Solicitudes (id_cpadron, CodigoSeguridad, id_tipotramite, id_tipoexpediente, id_subtipoexpediente, id_estado, CreateDate, CreateUser, LastUpdateDate, LastUpdateUser, observaciones_internas, observaciones_contribuyente, ZonaDeclarada, nro_expediente_anterior, observaciones, nombre_apellido_escribano, nro_matricula_escribano, NroExpedienteSade)
		SELECT id_cpadron, CodigoSeguridad, id_tipotramite, id_tipoexpediente, id_subtipoexpediente, id_estado, CreateDate, CreateUser, LastUpdateDate, LastUpdateUser, observaciones_internas, observaciones_contribuyente, ZonaDeclarada, nro_expediente_anterior, observaciones, nombre_apellido_escribano, nro_matricula_escribano, NroExpedienteSade FROM dbo.CPadron_Solicitudes WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_CPadron_Solicitudes OFF
GO
ALTER TABLE dbo.CPadron_DatosLocal
	DROP CONSTRAINT FK_CPadron_DatosLocal_CPadron_Solicitudes
GO
ALTER TABLE dbo.CPadron_HistorialEstados
	DROP CONSTRAINT FK_CPadron_HistorialEstados_CPadron_Solicitudes
GO
ALTER TABLE dbo.CPadron_Normativas
	DROP CONSTRAINT FK_CPadron_Normativas_CPadron_Solicitudes
GO
ALTER TABLE dbo.CPadron_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas
	DROP CONSTRAINT FK_CPadron_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas_CPadron_Solicitudes
GO
ALTER TABLE dbo.CPadron_Plantas
	DROP CONSTRAINT FK_CPadron_Plantas_CPadron_Solicitudes
GO
ALTER TABLE dbo.CPadron_Rubros
	DROP CONSTRAINT FK_CPadron_Rubros_CPadron_Solicitudes
GO
ALTER TABLE dbo.CPadron_Ubicaciones
	DROP CONSTRAINT FK_CPadron_Ubicaciones_CPadron_Solicitudes
GO
ALTER TABLE dbo.CPadron_DocumentosAdjuntos
	DROP CONSTRAINT FK_CPadron_DocumentosAdjuntos_CPadron_DocumentosAdjuntos
GO
ALTER TABLE dbo.CPadron_Solicitudes_Observaciones
	DROP CONSTRAINT FK_CPadron_Solicitudes_Observaciones_CPadron_Solicitudes
GO
ALTER TABLE dbo.SGI_Tramites_Tareas_CPADRON
	DROP CONSTRAINT FK_SGI_Tramites_Tareas_CPADRON_CPadron_Solicitudes
GO
ALTER TABLE dbo.CPadron_Titulares_PersonasFisicas
	DROP CONSTRAINT FK_CPadron_Titulares_PersonasFisicas_CPadron_Solicitudes
GO
ALTER TABLE dbo.CPadron_Titulares_PersonasJuridicas
	DROP CONSTRAINT FK_CPadron_Titulares_PersonasJuridicas_CPadron_Solicitudes
GO
ALTER TABLE dbo.Transf_Solicitudes
	DROP CONSTRAINT FK_Transf_Solicitudes_CPadron_Solicitudes
GO
ALTER TABLE dbo.CPadron_Titulares_Solicitud_PersonasFisicas
	DROP CONSTRAINT FK_CPadron_Titulares_Solicitud_PersonasFisicas_CPadron_Solicitudes
GO
ALTER TABLE dbo.CPadron_ConformacionLocal
	DROP CONSTRAINT FK_CPadron_ConformacionLocal_CPadron_Solicitudes
GO
ALTER TABLE dbo.CPadron_Titulares_Solicitud_PersonasJuridicas
	DROP CONSTRAINT FK_CPadron_Titulares_Solicitud_PersonasJuridicas_CPadron_Solicitudes
GO
DROP TABLE dbo.CPadron_Solicitudes
GO
EXECUTE sp_rename N'dbo.Tmp_CPadron_Solicitudes', N'CPadron_Solicitudes', 'OBJECT' 
GO
ALTER TABLE dbo.CPadron_Solicitudes ADD CONSTRAINT
	PK_CPadron_Solicitudes PRIMARY KEY CLUSTERED 
	(
	id_cpadron
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.CPadron_Solicitudes ADD CONSTRAINT
	FK_CPadron_Solicitudes_aspnet_Users_CreateUser FOREIGN KEY
	(
	CreateUser
	) REFERENCES dbo.aspnet_Users
	(
	UserId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CPadron_Solicitudes ADD CONSTRAINT
	FK_CPadron_Solicitudes_aspnet_Users_LastUpdateUser FOREIGN KEY
	(
	LastUpdateUser
	) REFERENCES dbo.aspnet_Users
	(
	UserId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CPadron_Solicitudes ADD CONSTRAINT
	FK_CPadron_Solicitudes_CPadron_Estados FOREIGN KEY
	(
	id_estado
	) REFERENCES dbo.CPadron_Estados
	(
	id_estado
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CPadron_Solicitudes ADD CONSTRAINT
	FK_CPadron_Solicitudes_SubtipoExpediente FOREIGN KEY
	(
	id_subtipoexpediente
	) REFERENCES dbo.SubtipoExpediente
	(
	id_subtipoexpediente
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CPadron_Solicitudes ADD CONSTRAINT
	FK_CPadron_Solicitudes_TipoExpediente FOREIGN KEY
	(
	id_tipoexpediente
	) REFERENCES dbo.TipoExpediente
	(
	id_tipoexpediente
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CPadron_Solicitudes ADD CONSTRAINT
	FK_CPadron_Solicitudes_TipoTramite FOREIGN KEY
	(
	id_tipotramite
	) REFERENCES dbo.TipoTramite
	(
	id_tipotramite
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
------------------------------------------------------------------
-- Agrega trigger a la tabla de CPadron_Solicitudes
------------------------------------------------------------------
CREATE TRIGGER [dbo].[tr_CPadron_Solicitudes_historial_estados]
ON dbo.CPadron_Solicitudes
AFTER UPDATE
AS
BEGIN
	SET NOCOUNT ON;


	DECLARE @cod_estado_ant nvarchar(20)
	DECLARE @cod_estado_nuevo nvarchar(20)
	DECLARE @usuario uniqueidentifier
	DECLARE @id_cpadron int
	--DECLARE @id_cpadron_his int

	IF UPDATE(id_estado)
	BEGIN

		DECLARE cur CURSOR FAST_FORWARD FOR
		SELECT
			i.id_cpadron,
			i.LastUpdateUser,
			tipest_i.cod_estado,
			tipest_d.cod_estado
		FROM
			inserted i
			INNER JOIN deleted d ON i.id_cpadron = d.id_cpadron
			INNER JOIN CPadron_Estados tipest_i ON tipest_i.id_estado = i.id_estado
			INNER JOIN CPadron_Estados tipest_d ON tipest_d.id_estado = d.id_estado

		OPEN cur
		FETCH NEXT FROM cur INTO @id_cpadron, @usuario, @cod_estado_nuevo, @cod_estado_ant

		WHILE @@FETCH_STATUS = 0
		BEGIN
			IF @cod_estado_ant <> @cod_estado_nuevo
			BEGIN
				--EXEC @id_cpadron_his = Id_Nuevo 'CPadron_HistorialEstados'
				INSERT INTO CPadron_HistorialEstados
				(
					--id_cpadron_his,
					id_cpadron,
					cod_estado_ant,
					cod_estado_nuevo,
					fecha_modificacion,
					usuario_modificacion
				)
				VALUES
				(
					--@id_cpadron_his,
					@id_cpadron,
					@cod_estado_ant,
					@cod_estado_nuevo,
					GETDATE(),
					@usuario
				)
			END

			FETCH NEXT FROM cur INTO @id_cpadron, @usuario, @cod_estado_nuevo, @cod_estado_ant

		END
		CLOSE cur
		DEALLOCATE cur

	END

END
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Transf_Solicitudes
	(
	id_solicitud int NOT NULL IDENTITY (1, 1),
	id_cpadron int NOT NULL,
	id_tipotramite int NOT NULL,
	id_tipoexpediente int NOT NULL,
	id_subtipoexpediente int NOT NULL,
	id_estado int NOT NULL,
	NroExpedienteSade nvarchar(50) NULL,
	CreateDate datetime NOT NULL,
	CreateUser uniqueidentifier NOT NULL,
	LastUpdateDate datetime NULL,
	LastUpdateUser uniqueidentifier NULL,
	CodigoSeguridad nvarchar(10) NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Transf_Solicitudes SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_Transf_Solicitudes ON
GO
IF EXISTS(SELECT * FROM dbo.Transf_Solicitudes)
	 EXEC('INSERT INTO dbo.Tmp_Transf_Solicitudes (id_solicitud, id_cpadron, id_tipotramite, id_tipoexpediente, id_subtipoexpediente, id_estado, NroExpedienteSade, CreateDate, CreateUser, LastUpdateDate, LastUpdateUser, CodigoSeguridad)
		SELECT id_solicitud, id_cpadron, id_tipotramite, id_tipoexpediente, id_subtipoexpediente, id_estado, NroExpedienteSade, CreateDate, CreateUser, LastUpdateDate, LastUpdateUser, CodigoSeguridad FROM dbo.Transf_Solicitudes WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Transf_Solicitudes OFF
GO
ALTER TABLE dbo.Transf_Titulares_PersonasJuridicas
	DROP CONSTRAINT FK_Transf_Titulares_PersonasJuridicas_Transf_Solicitudes
GO
ALTER TABLE dbo.Transf_Firmantes_PersonasFisicas
	DROP CONSTRAINT FK_Transf_Firmantes_PersonasFisicas_Transf_Solicitudes
GO
ALTER TABLE dbo.Transf_Firmantes_PersonasJuridicas
	DROP CONSTRAINT FK_Transf_Firmantes_PersonasJuridicas_Transf_Solicitudes
GO
ALTER TABLE dbo.Transf_Titulares_PersonasJuridicas_PersonasFisicas
	DROP CONSTRAINT FK_Transf_Titulares_PersonasJuridicas_PersonasFisicas_CAA
GO
ALTER TABLE dbo.SGI_Tramites_Tareas_TRANSF
	DROP CONSTRAINT FK_SGI_Tramites_Tareas_TRANSF_Transf_Solicitudes
GO
ALTER TABLE dbo.Transf_Solicitudes_HistorialEstados
	DROP CONSTRAINT FK_Transf_Solicitudes_HistorialEstados_Transf_Solicitudes
GO
ALTER TABLE dbo.Transf_Solicitudes_Observaciones
	DROP CONSTRAINT FK_Transf_Solicitudes_Observaciones_Transf_Solicitudes
GO
ALTER TABLE dbo.Transf_DocumentosAdjuntos
	DROP CONSTRAINT FK_Transf_DocumentosAdjuntos_Transf_Solicitudes
GO
ALTER TABLE dbo.Transf_Titulares_PersonasFisicas
	DROP CONSTRAINT FK_Transf_Titulares_PersonasFisicas_Transf_Solicitudes
GO
DROP TABLE dbo.Transf_Solicitudes
GO
EXECUTE sp_rename N'dbo.Tmp_Transf_Solicitudes', N'Transf_Solicitudes', 'OBJECT' 
GO
ALTER TABLE dbo.Transf_Solicitudes ADD CONSTRAINT
	PK_Transf_Solicitudes PRIMARY KEY CLUSTERED 
	(
	id_solicitud
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Transf_Solicitudes ADD CONSTRAINT
	FK_Transf_Solicitudes_CPadron_Solicitudes FOREIGN KEY
	(
	id_cpadron
	) REFERENCES dbo.CPadron_Solicitudes
	(
	id_cpadron
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Transf_Solicitudes ADD CONSTRAINT
	FK_Transf_Solicitudes_SubtipoExpediente FOREIGN KEY
	(
	id_subtipoexpediente
	) REFERENCES dbo.SubtipoExpediente
	(
	id_subtipoexpediente
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Transf_Solicitudes ADD CONSTRAINT
	FK_Transf_Solicitudes_TipoExpediente FOREIGN KEY
	(
	id_tipoexpediente
	) REFERENCES dbo.TipoExpediente
	(
	id_tipoexpediente
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Transf_Solicitudes ADD CONSTRAINT
	FK_Transf_Solicitudes_TipoTramite FOREIGN KEY
	(
	id_tipotramite
	) REFERENCES dbo.TipoTramite
	(
	id_tipotramite
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Transf_Solicitudes ADD CONSTRAINT
	FK_Transf_Solicitudes_TipoEstadoSolicitud FOREIGN KEY
	(
	id_estado
	) REFERENCES dbo.TipoEstadoSolicitud
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
CREATE TRIGGER [dbo].[tr_Transf_Solicitudes_historial_estados]
   ON  dbo.Transf_Solicitudes
   AFTER UPDATE
AS 
BEGIN
	SET NOCOUNT ON;


	DECLARE @cod_estado_ant	nvarchar(20)
	DECLARE @cod_estado_nuevo nvarchar(20)
	DECLARE @usuario uniqueidentifier
	DECLARE @id_solicitud int
	DECLARE @username nvarchar(50)
	DECLARE @userid uniqueidentifier
	
	IF UPDATE(id_estado)
	BEGIN
		
		DECLARE cur CURSOR FAST_FORWARD FOR
		SELECT 
			i.id_solicitud,
			tipest_i.nombre,
			tipest_d.nombre,
			IsNull(i.LastUpdateUser,i.CreateUser)
		FROM 
			inserted i 
			INNER JOIN deleted d ON i.id_solicitud = d.id_solicitud
			INNER JOIN TipoEstadoSolicitud tipest_i ON tipest_i.id = i.id_estado
			INNER JOIN TipoEstadoSolicitud tipest_d ON tipest_d.id = d.id_estado
			
		
		OPEN cur
		FETCH NEXT FROM cur INTO @id_solicitud,  @cod_estado_nuevo, @cod_estado_ant,@userid
		
		WHILE @@FETCH_STATUS = 0
		BEGIN

			IF @cod_estado_ant <> @cod_estado_nuevo 
			BEGIN
			
				SELECT @username = usu.loweredusername FROM aspnet_users usu WHERE usu.userid = @userid
				
				INSERT INTO Transf_Solicitudes_HistorialEstados
				(
					id_solicitud,
					cod_estado_ant,
					cod_estado_nuevo,
					username,
					fecha_modificacion,
					usuario_modificacion
				)
				VALUES
				(
					@id_solicitud,
					@cod_estado_ant,
					@cod_estado_nuevo,
					@username,
					GETDATE(),
					@userid
				)
			END
		
			FETCH NEXT FROM cur INTO @id_solicitud, @cod_estado_nuevo, @cod_estado_ant,@userid
		
		END
		CLOSE cur
		DEALLOCATE cur

	END  
END
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Transf_DocumentosAdjuntos
	(
	id_docadjunto int NOT NULL IDENTITY (1, 1),
	id_solicitud int NOT NULL,
	id_tdocreq int NOT NULL,
	tdocreq_detalle nvarchar(50) NULL,
	id_tipodocsis int NOT NULL,
	id_file int NOT NULL,
	generadoxSistema bit NOT NULL,
	CreateDate datetime NOT NULL,
	CreateUser uniqueidentifier NOT NULL,
	UpdateDate datetime NULL,
	UpdateUser uniqueidentifier NULL,
	nombre_archivo nvarchar(50) NULL,
	id_agrupamiento int NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Transf_DocumentosAdjuntos SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_Transf_DocumentosAdjuntos ON
GO
IF EXISTS(SELECT * FROM dbo.Transf_DocumentosAdjuntos)
	 EXEC('INSERT INTO dbo.Tmp_Transf_DocumentosAdjuntos (id_docadjunto, id_solicitud, id_tdocreq, tdocreq_detalle, id_tipodocsis, id_file, generadoxSistema, CreateDate, CreateUser, UpdateDate, UpdateUser, nombre_archivo, id_agrupamiento)
		SELECT id_docadjunto, id_solicitud, id_tdocreq, tdocreq_detalle, id_tipodocsis, id_file, generadoxSistema, CreateDate, CreateUser, UpdateDate, UpdateUser, nombre_archivo, id_agrupamiento FROM dbo.Transf_DocumentosAdjuntos WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Transf_DocumentosAdjuntos OFF
GO
ALTER TABLE dbo.wsEscribanos_Files_TRANSF
	DROP CONSTRAINT FK_wsEscribanos_Files_TRANSF_Transf_DocumentosAdjuntos
GO
DROP TABLE dbo.Transf_DocumentosAdjuntos
GO
EXECUTE sp_rename N'dbo.Tmp_Transf_DocumentosAdjuntos', N'Transf_DocumentosAdjuntos', 'OBJECT' 
GO
ALTER TABLE dbo.Transf_DocumentosAdjuntos ADD CONSTRAINT
	PK_Transf_DocumentosAdjuntos PRIMARY KEY CLUSTERED 
	(
	id_docadjunto
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Transf_DocumentosAdjuntos ADD CONSTRAINT
	FK_Transf_DocumentosAdjuntos_Niveles_Agrupamiento FOREIGN KEY
	(
	id_agrupamiento
	) REFERENCES dbo.Niveles_Agrupamiento
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Transf_DocumentosAdjuntos ADD CONSTRAINT
	FK_Transf_DocumentosAdjuntos_Transf_Solicitudes FOREIGN KEY
	(
	id_solicitud
	) REFERENCES dbo.Transf_Solicitudes
	(
	id_solicitud
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Transf_DocumentosAdjuntos ADD CONSTRAINT
	FK_Transf_DocumentosAdjuntos_TiposDeDocumentosRequeridos FOREIGN KEY
	(
	id_tdocreq
	) REFERENCES dbo.TiposDeDocumentosRequeridos
	(
	id_tdocreq
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Transf_DocumentosAdjuntos ADD CONSTRAINT
	FK_Transf_DocumentosAdjuntos_TiposDeDocumentosSistema FOREIGN KEY
	(
	id_tipodocsis
	) REFERENCES dbo.TiposDeDocumentosSistema
	(
	id_tipdocsis
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.wsEscribanos_Files_TRANSF ADD CONSTRAINT
	FK_wsEscribanos_Files_TRANSF_Transf_DocumentosAdjuntos FOREIGN KEY
	(
	id_docadjunto
	) REFERENCES dbo.Transf_DocumentosAdjuntos
	(
	id_docadjunto
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.wsEscribanos_Files_TRANSF SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Transf_Solicitudes_Observaciones ADD CONSTRAINT
	FK_Transf_Solicitudes_Observaciones_Transf_Solicitudes FOREIGN KEY
	(
	id_solicitud
	) REFERENCES dbo.Transf_Solicitudes
	(
	id_solicitud
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Transf_Solicitudes_Observaciones SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Transf_Solicitudes_HistorialEstados ADD CONSTRAINT
	FK_Transf_Solicitudes_HistorialEstados_Transf_Solicitudes FOREIGN KEY
	(
	id_solicitud
	) REFERENCES dbo.Transf_Solicitudes
	(
	id_solicitud
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Transf_Solicitudes_HistorialEstados SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.SGI_Tramites_Tareas_TRANSF ADD CONSTRAINT
	FK_SGI_Tramites_Tareas_TRANSF_Transf_Solicitudes FOREIGN KEY
	(
	id_solicitud
	) REFERENCES dbo.Transf_Solicitudes
	(
	id_solicitud
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SGI_Tramites_Tareas_TRANSF SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_CPadron_Titulares_PersonasJuridicas
	(
	id_personajuridica int NOT NULL IDENTITY (1, 1),
	id_cpadron int NOT NULL,
	Id_TipoSociedad int NOT NULL,
	Razon_Social nvarchar(200) NULL,
	CUIT nvarchar(13) NULL,
	id_tipoiibb int NOT NULL,
	Nro_IIBB nvarchar(20) NULL,
	Calle nvarchar(70) NULL,
	NroPuerta int NULL,
	Piso nvarchar(5) NULL,
	Depto nvarchar(5) NULL,
	id_localidad int NOT NULL,
	Codigo_Postal nvarchar(10) NULL,
	Telefono nvarchar(50) NULL,
	Email nvarchar(70) NULL,
	CreateUser uniqueidentifier NOT NULL,
	CreateDate datetime NOT NULL,
	LastUpdateUser uniqueidentifier NULL,
	LastUpdateDate datetime NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_CPadron_Titulares_PersonasJuridicas SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_CPadron_Titulares_PersonasJuridicas ON
GO
IF EXISTS(SELECT * FROM dbo.CPadron_Titulares_PersonasJuridicas)
	 EXEC('INSERT INTO dbo.Tmp_CPadron_Titulares_PersonasJuridicas (id_personajuridica, id_cpadron, Id_TipoSociedad, Razon_Social, CUIT, id_tipoiibb, Nro_IIBB, Calle, NroPuerta, Piso, Depto, id_localidad, Codigo_Postal, Telefono, Email, CreateUser, CreateDate, LastUpdateUser, LastUpdateDate)
		SELECT id_personajuridica, id_cpadron, Id_TipoSociedad, Razon_Social, CUIT, id_tipoiibb, Nro_IIBB, Calle, NroPuerta, Piso, Depto, id_localidad, Codigo_Postal, Telefono, Email, CreateUser, CreateDate, LastUpdateUser, LastUpdateDate FROM dbo.CPadron_Titulares_PersonasJuridicas WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_CPadron_Titulares_PersonasJuridicas OFF
GO
DROP TABLE dbo.CPadron_Titulares_PersonasJuridicas
GO
EXECUTE sp_rename N'dbo.Tmp_CPadron_Titulares_PersonasJuridicas', N'CPadron_Titulares_PersonasJuridicas', 'OBJECT' 
GO
ALTER TABLE dbo.CPadron_Titulares_PersonasJuridicas ADD CONSTRAINT
	FK_CPadron_Titulares_PersonasJuridicas_CPadron_Solicitudes FOREIGN KEY
	(
	id_cpadron
	) REFERENCES dbo.CPadron_Solicitudes
	(
	id_cpadron
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_CPadron_Titulares_PersonasFisicas
	(
	id_personafisica int NOT NULL IDENTITY (1, 1),
	id_cpadron int NOT NULL,
	Apellido varchar(50) NOT NULL,
	Nombres nvarchar(50) NOT NULL,
	id_tipodoc_personal int NOT NULL,
	Cuit nvarchar(13) NULL,
	id_tipoiibb int NOT NULL,
	Ingresos_Brutos nvarchar(25) NULL,
	Calle nvarchar(70) NOT NULL,
	Nro_Puerta int NOT NULL,
	Piso varchar(2) NULL,
	Depto varchar(10) NULL,
	Id_Localidad int NOT NULL,
	Codigo_Postal nvarchar(10) NULL,
	TelefonoMovil nvarchar(20) NULL,
	Sms nvarchar(50) NULL,
	Email nvarchar(70) NULL,
	MismoFirmante bit NOT NULL,
	CreateUser uniqueidentifier NOT NULL,
	CreateDate datetime NOT NULL,
	LastUpdateUser uniqueidentifier NULL,
	LastupdateDate datetime NULL,
	Nro_Documento nvarchar(15) NULL,
	Telefono nchar(50) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_CPadron_Titulares_PersonasFisicas SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_CPadron_Titulares_PersonasFisicas ON
GO
IF EXISTS(SELECT * FROM dbo.CPadron_Titulares_PersonasFisicas)
	 EXEC('INSERT INTO dbo.Tmp_CPadron_Titulares_PersonasFisicas (id_personafisica, id_cpadron, Apellido, Nombres, id_tipodoc_personal, Cuit, id_tipoiibb, Ingresos_Brutos, Calle, Nro_Puerta, Piso, Depto, Id_Localidad, Codigo_Postal, TelefonoMovil, Sms, Email, MismoFirmante, CreateUser, CreateDate, LastUpdateUser, LastupdateDate, Nro_Documento, Telefono)
		SELECT id_personafisica, id_cpadron, Apellido, Nombres, id_tipodoc_personal, Cuit, id_tipoiibb, Ingresos_Brutos, Calle, Nro_Puerta, Piso, Depto, Id_Localidad, Codigo_Postal, TelefonoMovil, Sms, Email, MismoFirmante, CreateUser, CreateDate, LastUpdateUser, LastupdateDate, Nro_Documento, Telefono FROM dbo.CPadron_Titulares_PersonasFisicas WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_CPadron_Titulares_PersonasFisicas OFF
GO
DROP TABLE dbo.CPadron_Titulares_PersonasFisicas
GO
EXECUTE sp_rename N'dbo.Tmp_CPadron_Titulares_PersonasFisicas', N'CPadron_Titulares_PersonasFisicas', 'OBJECT' 
GO
ALTER TABLE dbo.CPadron_Titulares_PersonasFisicas ADD CONSTRAINT
	FK_CPadron_Titulares_PersonasFisicas_CPadron_Solicitudes FOREIGN KEY
	(
	id_cpadron
	) REFERENCES dbo.CPadron_Solicitudes
	(
	id_cpadron
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.SGI_Tramites_Tareas_CPADRON ADD CONSTRAINT
	FK_SGI_Tramites_Tareas_CPADRON_CPadron_Solicitudes FOREIGN KEY
	(
	id_cpadron
	) REFERENCES dbo.CPadron_Solicitudes
	(
	id_cpadron
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SGI_Tramites_Tareas_CPADRON SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.CPadron_Solicitudes_Observaciones ADD CONSTRAINT
	FK_CPadron_Solicitudes_Observaciones_CPadron_Solicitudes FOREIGN KEY
	(
	id_cpadron
	) REFERENCES dbo.CPadron_Solicitudes
	(
	id_cpadron
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CPadron_Solicitudes_Observaciones SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_CPadron_DocumentosAdjuntos
	(
	id_docadjunto int NOT NULL IDENTITY (1, 1),
	id_cpadron int NOT NULL,
	id_tdocreq int NOT NULL,
	tdocreq_detalle nvarchar(50) NULL,
	id_tipodocsis int NOT NULL,
	id_file int NOT NULL,
	generadoxSistema bit NOT NULL,
	CreateDate datetime NOT NULL,
	CreateUser uniqueidentifier NOT NULL,
	UpdateDate datetime NULL,
	UpdateUser uniqueidentifier NULL,
	nombre_archivo nvarchar(50) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_CPadron_DocumentosAdjuntos SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_CPadron_DocumentosAdjuntos ON
GO
IF EXISTS(SELECT * FROM dbo.CPadron_DocumentosAdjuntos)
	 EXEC('INSERT INTO dbo.Tmp_CPadron_DocumentosAdjuntos (id_docadjunto, id_cpadron, id_tdocreq, tdocreq_detalle, id_tipodocsis, id_file, generadoxSistema, CreateDate, CreateUser, UpdateDate, UpdateUser, nombre_archivo)
		SELECT id_docadjunto, id_cpadron, id_tdocreq, tdocreq_detalle, id_tipodocsis, id_file, generadoxSistema, CreateDate, CreateUser, UpdateDate, UpdateUser, nombre_archivo FROM dbo.CPadron_DocumentosAdjuntos WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_CPadron_DocumentosAdjuntos OFF
GO
DROP TABLE dbo.CPadron_DocumentosAdjuntos
GO
EXECUTE sp_rename N'dbo.Tmp_CPadron_DocumentosAdjuntos', N'CPadron_DocumentosAdjuntos', 'OBJECT' 
GO
ALTER TABLE dbo.CPadron_DocumentosAdjuntos ADD CONSTRAINT
	PK_CPadron_DocumentosAdjuntos PRIMARY KEY CLUSTERED 
	(
	id_docadjunto
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.CPadron_DocumentosAdjuntos ADD CONSTRAINT
	FK_CPadron_DocumentosAdjuntos_TiposDeDocumentosRequeridos FOREIGN KEY
	(
	id_tdocreq
	) REFERENCES dbo.TiposDeDocumentosRequeridos
	(
	id_tdocreq
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CPadron_DocumentosAdjuntos ADD CONSTRAINT
	FK_CPadron_DocumentosAdjuntos_TiposDeDocumentosSistema FOREIGN KEY
	(
	id_tipodocsis
	) REFERENCES dbo.TiposDeDocumentosSistema
	(
	id_tipdocsis
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CPadron_DocumentosAdjuntos ADD CONSTRAINT
	FK_CPadron_DocumentosAdjuntos_CPadron_DocumentosAdjuntos FOREIGN KEY
	(
	id_cpadron
	) REFERENCES dbo.CPadron_Solicitudes
	(
	id_cpadron
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_CPadron_Ubicaciones
	(
	id_cpadronubicacion int NOT NULL IDENTITY (1, 1),
	id_cpadron int NULL,
	id_ubicacion int NULL,
	id_subtipoubicacion int NULL,
	local_subtipoubicacion nvarchar(25) NULL,
	deptoLocal_cpadronubicacion nvarchar(50) NULL,
	id_zonaplaneamiento int NOT NULL,
	CreateDate datetime NOT NULL,
	CreateUser uniqueidentifier NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_CPadron_Ubicaciones SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_CPadron_Ubicaciones ON
GO
IF EXISTS(SELECT * FROM dbo.CPadron_Ubicaciones)
	 EXEC('INSERT INTO dbo.Tmp_CPadron_Ubicaciones (id_cpadronubicacion, id_cpadron, id_ubicacion, id_subtipoubicacion, local_subtipoubicacion, deptoLocal_cpadronubicacion, id_zonaplaneamiento, CreateDate, CreateUser)
		SELECT id_cpadronubicacion, id_cpadron, id_ubicacion, id_subtipoubicacion, local_subtipoubicacion, deptoLocal_cpadronubicacion, id_zonaplaneamiento, CreateDate, CreateUser FROM dbo.CPadron_Ubicaciones WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_CPadron_Ubicaciones OFF
GO
ALTER TABLE dbo.CPadron_Ubicaciones_PropiedadHorizontal
	DROP CONSTRAINT FK_CPadron_Ubicaciones_PropiedadHorizontal_CPadron_Ubicaciones
GO
ALTER TABLE dbo.CPadron_Ubicaciones_Puertas
	DROP CONSTRAINT FK_CPadron_Ubicaciones_Puertas_CPadron_Ubicaciones
GO
DROP TABLE dbo.CPadron_Ubicaciones
GO
EXECUTE sp_rename N'dbo.Tmp_CPadron_Ubicaciones', N'CPadron_Ubicaciones', 'OBJECT' 
GO
ALTER TABLE dbo.CPadron_Ubicaciones ADD CONSTRAINT
	PK_CPadron_Ubicaciones PRIMARY KEY CLUSTERED 
	(
	id_cpadronubicacion
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.CPadron_Ubicaciones ADD CONSTRAINT
	FK_CPadron_Ubicaciones_aspnet_Users_CreateUser FOREIGN KEY
	(
	CreateUser
	) REFERENCES dbo.aspnet_Users
	(
	UserId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CPadron_Ubicaciones ADD CONSTRAINT
	FK_CPadron_Ubicaciones_CPadron_Solicitudes FOREIGN KEY
	(
	id_cpadron
	) REFERENCES dbo.CPadron_Solicitudes
	(
	id_cpadron
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CPadron_Ubicaciones ADD CONSTRAINT
	FK_CPadron_Ubicaciones_SubTiposDeUbicacion FOREIGN KEY
	(
	id_subtipoubicacion
	) REFERENCES dbo.SubTiposDeUbicacion
	(
	id_subtipoubicacion
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CPadron_Ubicaciones ADD CONSTRAINT
	FK_CPadron_Ubicaciones_Ubicaciones FOREIGN KEY
	(
	id_ubicacion
	) REFERENCES dbo.Ubicaciones
	(
	id_ubicacion
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CPadron_Ubicaciones ADD CONSTRAINT
	FK_CPadron_Ubicaciones_Zonas_Planeamiento FOREIGN KEY
	(
	id_zonaplaneamiento
	) REFERENCES dbo.Zonas_Planeamiento
	(
	id_zonaplaneamiento
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_CPadron_Ubicaciones_Puertas
	(
	id_cpadronpuerta int NOT NULL IDENTITY (1, 1),
	id_cpadronubicacion int NOT NULL,
	codigo_calle int NOT NULL,
	nombre_calle nvarchar(100) NOT NULL,
	NroPuerta int NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_CPadron_Ubicaciones_Puertas SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_CPadron_Ubicaciones_Puertas ON
GO
IF EXISTS(SELECT * FROM dbo.CPadron_Ubicaciones_Puertas)
	 EXEC('INSERT INTO dbo.Tmp_CPadron_Ubicaciones_Puertas (id_cpadronpuerta, id_cpadronubicacion, codigo_calle, nombre_calle, NroPuerta)
		SELECT id_cpadronpuerta, id_cpadronubicacion, codigo_calle, nombre_calle, NroPuerta FROM dbo.CPadron_Ubicaciones_Puertas WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_CPadron_Ubicaciones_Puertas OFF
GO
DROP TABLE dbo.CPadron_Ubicaciones_Puertas
GO
EXECUTE sp_rename N'dbo.Tmp_CPadron_Ubicaciones_Puertas', N'CPadron_Ubicaciones_Puertas', 'OBJECT' 
GO
ALTER TABLE dbo.CPadron_Ubicaciones_Puertas ADD CONSTRAINT
	PK_CPadron_Ubicaciones_Puertas PRIMARY KEY CLUSTERED 
	(
	id_cpadronpuerta
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.CPadron_Ubicaciones_Puertas ADD CONSTRAINT
	FK_CPadron_Ubicaciones_Puertas_CPadron_Ubicaciones FOREIGN KEY
	(
	id_cpadronubicacion
	) REFERENCES dbo.CPadron_Ubicaciones
	(
	id_cpadronubicacion
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_CPadron_Ubicaciones_PropiedadHorizontal
	(
	id_cpadronprophorizontal int NOT NULL IDENTITY (1, 1),
	id_cpadronubicacion int NULL,
	id_propiedadhorizontal int NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_CPadron_Ubicaciones_PropiedadHorizontal SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_CPadron_Ubicaciones_PropiedadHorizontal ON
GO
IF EXISTS(SELECT * FROM dbo.CPadron_Ubicaciones_PropiedadHorizontal)
	 EXEC('INSERT INTO dbo.Tmp_CPadron_Ubicaciones_PropiedadHorizontal (id_cpadronprophorizontal, id_cpadronubicacion, id_propiedadhorizontal)
		SELECT id_cpadronprophorizontal, id_cpadronubicacion, id_propiedadhorizontal FROM dbo.CPadron_Ubicaciones_PropiedadHorizontal WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_CPadron_Ubicaciones_PropiedadHorizontal OFF
GO
DROP TABLE dbo.CPadron_Ubicaciones_PropiedadHorizontal
GO
EXECUTE sp_rename N'dbo.Tmp_CPadron_Ubicaciones_PropiedadHorizontal', N'CPadron_Ubicaciones_PropiedadHorizontal', 'OBJECT' 
GO
ALTER TABLE dbo.CPadron_Ubicaciones_PropiedadHorizontal ADD CONSTRAINT
	PK_CPadron_Ubicaciones_PropiedadHorizontal PRIMARY KEY CLUSTERED 
	(
	id_cpadronprophorizontal
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.CPadron_Ubicaciones_PropiedadHorizontal ADD CONSTRAINT
	FK_CPadron_Ubicaciones_PropiedadHorizontal_CPadron_Ubicaciones FOREIGN KEY
	(
	id_cpadronubicacion
	) REFERENCES dbo.CPadron_Ubicaciones
	(
	id_cpadronubicacion
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.CPadron_Rubros
	DROP CONSTRAINT DF_CPadron_Rubros_CreateDate
GO
CREATE TABLE dbo.Tmp_CPadron_Rubros
	(
	id_cpadronrubro int NOT NULL IDENTITY (1, 1),
	id_cpadron int NOT NULL,
	cod_rubro varchar(10) NOT NULL,
	desc_rubro varchar(200) NULL,
	EsAnterior bit NOT NULL,
	id_tipoactividad int NOT NULL,
	id_tipodocreq int NOT NULL,
	SuperficieHabilitar decimal(10, 2) NOT NULL,
	id_ImpactoAmbiental int NULL,
	CreateDate datetime NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_CPadron_Rubros SET (LOCK_ESCALATION = TABLE)
GO
ALTER TABLE dbo.Tmp_CPadron_Rubros ADD CONSTRAINT
	DF_CPadron_Rubros_CreateDate DEFAULT (getdate()) FOR CreateDate
GO
SET IDENTITY_INSERT dbo.Tmp_CPadron_Rubros ON
GO
IF EXISTS(SELECT * FROM dbo.CPadron_Rubros)
	 EXEC('INSERT INTO dbo.Tmp_CPadron_Rubros (id_cpadronrubro, id_cpadron, cod_rubro, desc_rubro, EsAnterior, id_tipoactividad, id_tipodocreq, SuperficieHabilitar, id_ImpactoAmbiental, CreateDate)
		SELECT id_cpadronrubro, id_cpadron, cod_rubro, desc_rubro, EsAnterior, id_tipoactividad, id_tipodocreq, SuperficieHabilitar, id_ImpactoAmbiental, CreateDate FROM dbo.CPadron_Rubros WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_CPadron_Rubros OFF
GO
DROP TABLE dbo.CPadron_Rubros
GO
EXECUTE sp_rename N'dbo.Tmp_CPadron_Rubros', N'CPadron_Rubros', 'OBJECT' 
GO
ALTER TABLE dbo.CPadron_Rubros ADD CONSTRAINT
	PK_CPadron_Rubros PRIMARY KEY CLUSTERED 
	(
	id_cpadronrubro
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.CPadron_Rubros ADD CONSTRAINT
	FK_CPadron_Rubros_CPadron_Solicitudes FOREIGN KEY
	(
	id_cpadron
	) REFERENCES dbo.CPadron_Solicitudes
	(
	id_cpadron
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CPadron_Rubros ADD CONSTRAINT
	FK_CPadron_Rubros_ImpactoAmbiental FOREIGN KEY
	(
	id_ImpactoAmbiental
	) REFERENCES dbo.ImpactoAmbiental
	(
	id_ImpactoAmbiental
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CPadron_Rubros ADD CONSTRAINT
	FK_CPadron_Rubros_Tipo_Documentacion_Req FOREIGN KEY
	(
	id_tipodocreq
	) REFERENCES dbo.Tipo_Documentacion_Req
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CPadron_Rubros ADD CONSTRAINT
	FK_CPadron_Rubros_TipoActividad FOREIGN KEY
	(
	id_tipoactividad
	) REFERENCES dbo.TipoActividad
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_CPadron_Plantas
	(
	id_cpadrontiposector int NOT NULL IDENTITY (1, 1),
	id_cpadron int NOT NULL,
	id_tiposector int NOT NULL,
	detalle_cpadrontiposector nvarchar(50) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_CPadron_Plantas SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_CPadron_Plantas ON
GO
IF EXISTS(SELECT * FROM dbo.CPadron_Plantas)
	 EXEC('INSERT INTO dbo.Tmp_CPadron_Plantas (id_cpadrontiposector, id_cpadron, id_tiposector, detalle_cpadrontiposector)
		SELECT id_cpadrontiposector, id_cpadron, id_tiposector, detalle_cpadrontiposector FROM dbo.CPadron_Plantas WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_CPadron_Plantas OFF
GO
ALTER TABLE dbo.CPadron_ConformacionLocal
	DROP CONSTRAINT FK_CPadron_ConformacionLocal_CPadron_Plantas
GO
DROP TABLE dbo.CPadron_Plantas
GO
EXECUTE sp_rename N'dbo.Tmp_CPadron_Plantas', N'CPadron_Plantas', 'OBJECT' 
GO
ALTER TABLE dbo.CPadron_Plantas ADD CONSTRAINT
	PK_CPadron_Plantas PRIMARY KEY CLUSTERED 
	(
	id_cpadrontiposector
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.CPadron_Plantas ADD CONSTRAINT
	FK_CPadron_Plantas_CPadron_Solicitudes FOREIGN KEY
	(
	id_cpadron
	) REFERENCES dbo.CPadron_Solicitudes
	(
	id_cpadron
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CPadron_Plantas ADD CONSTRAINT
	FK_CPadron_Plantas_TipoSector FOREIGN KEY
	(
	id_tiposector
	) REFERENCES dbo.TipoSector
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.CPadron_ConformacionLocal
	DROP CONSTRAINT DF_CPadron_ConformacionLocal_CreateDate
GO
CREATE TABLE dbo.Tmp_CPadron_ConformacionLocal
	(
	id_cpadronconflocal int NOT NULL IDENTITY (1, 1),
	id_cpadron int NOT NULL,
	id_destino int NOT NULL,
	largo_conflocal decimal(10, 2) NULL,
	ancho_conflocal decimal(10, 2) NULL,
	alto_conflocal decimal(10, 2) NULL,
	Paredes_conflocal nvarchar(50) NULL,
	Techos_conflocal nvarchar(50) NULL,
	Pisos_conflocal nvarchar(50) NULL,
	Frisos_conflocal nvarchar(50) NULL,
	Observaciones_conflocal nvarchar(4000) NULL,
	CreateDate datetime NOT NULL,
	CreateUser uniqueidentifier NOT NULL,
	UpdateDate datetime NULL,
	Updateuser uniqueidentifier NULL,
	Detalle_conflocal nvarchar(200) NULL,
	id_cpadrontiposector int NULL,
	id_ventilacion int NULL,
	id_iluminacion int NULL,
	superficie_conflocal decimal(10, 2) NULL,
	id_tiposuperficie int NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_CPadron_ConformacionLocal SET (LOCK_ESCALATION = TABLE)
GO
ALTER TABLE dbo.Tmp_CPadron_ConformacionLocal ADD CONSTRAINT
	DF_CPadron_ConformacionLocal_CreateDate DEFAULT (getdate()) FOR CreateDate
GO
SET IDENTITY_INSERT dbo.Tmp_CPadron_ConformacionLocal ON
GO
IF EXISTS(SELECT * FROM dbo.CPadron_ConformacionLocal)
	 EXEC('INSERT INTO dbo.Tmp_CPadron_ConformacionLocal (id_cpadronconflocal, id_cpadron, id_destino, largo_conflocal, ancho_conflocal, alto_conflocal, Paredes_conflocal, Techos_conflocal, Pisos_conflocal, Frisos_conflocal, Observaciones_conflocal, CreateDate, CreateUser, UpdateDate, Updateuser, Detalle_conflocal, id_cpadrontiposector, id_ventilacion, id_iluminacion, superficie_conflocal, id_tiposuperficie)
		SELECT id_cpadronconflocal, id_cpadron, id_destino, largo_conflocal, ancho_conflocal, alto_conflocal, Paredes_conflocal, Techos_conflocal, Pisos_conflocal, Frisos_conflocal, Observaciones_conflocal, CreateDate, CreateUser, UpdateDate, Updateuser, Detalle_conflocal, id_cpadrontiposector, id_ventilacion, id_iluminacion, superficie_conflocal, id_tiposuperficie FROM dbo.CPadron_ConformacionLocal WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_CPadron_ConformacionLocal OFF
GO
DROP TABLE dbo.CPadron_ConformacionLocal
GO
EXECUTE sp_rename N'dbo.Tmp_CPadron_ConformacionLocal', N'CPadron_ConformacionLocal', 'OBJECT' 
GO
ALTER TABLE dbo.CPadron_ConformacionLocal ADD CONSTRAINT
	PK_CPadron_ConformacionLocal PRIMARY KEY CLUSTERED 
	(
	id_cpadronconflocal
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.CPadron_ConformacionLocal ADD CONSTRAINT
	FK_CPadron_ConformacionLocal_CPadron_Plantas FOREIGN KEY
	(
	id_cpadrontiposector
	) REFERENCES dbo.CPadron_Plantas
	(
	id_cpadrontiposector
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CPadron_ConformacionLocal ADD CONSTRAINT
	FK_CPadron_ConformacionLocal_CPadron_Solicitudes FOREIGN KEY
	(
	id_cpadron
	) REFERENCES dbo.CPadron_Solicitudes
	(
	id_cpadron
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CPadron_ConformacionLocal ADD CONSTRAINT
	FK_CPadron_ConformacionLocal_tipo_iluminacion FOREIGN KEY
	(
	id_iluminacion
	) REFERENCES dbo.tipo_iluminacion
	(
	id_iluminacion
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CPadron_ConformacionLocal ADD CONSTRAINT
	FK_CPadron_ConformacionLocal_Tipo_Superficie FOREIGN KEY
	(
	id_tiposuperficie
	) REFERENCES dbo.TipoSuperficie
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CPadron_ConformacionLocal ADD CONSTRAINT
	FK_CPadron_ConformacionLocal_tipo_ventilacion FOREIGN KEY
	(
	id_ventilacion
	) REFERENCES dbo.tipo_ventilacion
	(
	id_ventilacion
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CPadron_ConformacionLocal ADD CONSTRAINT
	FK_CPadron_ConformacionLocal_TipoDestino FOREIGN KEY
	(
	id_destino
	) REFERENCES dbo.TipoDestino
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.CPadron_HistorialEstados ADD CONSTRAINT
	FK_CPadron_HistorialEstados_CPadron_Solicitudes FOREIGN KEY
	(
	id_cpadron
	) REFERENCES dbo.CPadron_Solicitudes
	(
	id_cpadron
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CPadron_HistorialEstados SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.CPadron_Normativas
	DROP CONSTRAINT DF_CPadron_Normativas_CreateDate
GO
CREATE TABLE dbo.Tmp_CPadron_Normativas
	(
	id_cpadrontiponormativa int NOT NULL IDENTITY (1, 1),
	id_cpadron int NOT NULL,
	id_tiponormativa int NOT NULL,
	id_entidadnormativa int NOT NULL,
	nro_normativa nvarchar(15) NOT NULL,
	CreateUser uniqueidentifier NOT NULL,
	CreateDate datetime NOT NULL,
	LastUpdateUser uniqueidentifier NULL,
	LastUpdateDate datetime NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_CPadron_Normativas SET (LOCK_ESCALATION = TABLE)
GO
ALTER TABLE dbo.Tmp_CPadron_Normativas ADD CONSTRAINT
	DF_CPadron_Normativas_CreateDate DEFAULT (getdate()) FOR CreateDate
GO
SET IDENTITY_INSERT dbo.Tmp_CPadron_Normativas ON
GO
IF EXISTS(SELECT * FROM dbo.CPadron_Normativas)
	 EXEC('INSERT INTO dbo.Tmp_CPadron_Normativas (id_cpadrontiponormativa, id_cpadron, id_tiponormativa, id_entidadnormativa, nro_normativa, CreateUser, CreateDate, LastUpdateUser, LastUpdateDate)
		SELECT id_cpadrontiponormativa, id_cpadron, id_tiponormativa, id_entidadnormativa, nro_normativa, CreateUser, CreateDate, LastUpdateUser, LastUpdateDate FROM dbo.CPadron_Normativas WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_CPadron_Normativas OFF
GO
DROP TABLE dbo.CPadron_Normativas
GO
EXECUTE sp_rename N'dbo.Tmp_CPadron_Normativas', N'CPadron_Normativas', 'OBJECT' 
GO
ALTER TABLE dbo.CPadron_Normativas ADD CONSTRAINT
	PK_CPadron_Normativas PRIMARY KEY CLUSTERED 
	(
	id_cpadrontiponormativa
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.CPadron_Normativas ADD CONSTRAINT
	FK_CPadron_Normativas_aspnet_Users_CreateUser FOREIGN KEY
	(
	CreateUser
	) REFERENCES dbo.aspnet_Users
	(
	UserId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CPadron_Normativas ADD CONSTRAINT
	FK_CPadron_Normativas_aspnet_Users_LastUpdateUser FOREIGN KEY
	(
	LastUpdateUser
	) REFERENCES dbo.aspnet_Users
	(
	UserId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CPadron_Normativas ADD CONSTRAINT
	FK_CPadron_Normativas_CPadron_Solicitudes FOREIGN KEY
	(
	id_cpadron
	) REFERENCES dbo.CPadron_Solicitudes
	(
	id_cpadron
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CPadron_Normativas ADD CONSTRAINT
	FK_CPadron_Normativas_EntidadNormativa FOREIGN KEY
	(
	id_entidadnormativa
	) REFERENCES dbo.EntidadNormativa
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CPadron_Normativas ADD CONSTRAINT
	FK_CPadron_Normativas_TipoNormativa FOREIGN KEY
	(
	id_tiponormativa
	) REFERENCES dbo.TipoNormativa
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.CPadron_DatosLocal
	DROP CONSTRAINT DF_CPadron_DatosLocal_CreateDate
GO
CREATE TABLE dbo.Tmp_CPadron_DatosLocal
	(
	id_cpadrondatoslocal int NOT NULL IDENTITY (1, 1),
	id_cpadron int NOT NULL,
	superficie_cubierta_dl decimal(8, 2) NULL,
	superficie_descubierta_dl decimal(8, 2) NULL,
	dimesion_frente_dl decimal(8, 2) NULL,
	lugar_carga_descarga_dl bit NOT NULL,
	estacionamiento_dl bit NOT NULL,
	red_transito_pesado_dl bit NOT NULL,
	sobre_avenida_dl bit NOT NULL,
	materiales_pisos_dl nvarchar(500) NULL,
	materiales_paredes_dl nvarchar(500) NULL,
	materiales_techos_dl nvarchar(500) NULL,
	materiales_revestimientos_dl nvarchar(500) NULL,
	sanitarios_ubicacion_dl int NOT NULL,
	sanitarios_distancia_dl decimal(8, 2) NULL,
	croquis_ubicacion_dl nvarchar(300) NULL,
	cantidad_sanitarios_dl int NULL,
	superficie_sanitarios_dl decimal(8, 2) NULL,
	frente_dl decimal(8, 2) NULL,
	fondo_dl decimal(8, 2) NULL,
	lateral_izquierdo_dl decimal(8, 2) NULL,
	lateral_derecho_dl decimal(8, 2) NULL,
	cantidad_operarios_dl int NULL,
	CreateDate datetime NOT NULL,
	CreateUser uniqueidentifier NOT NULL,
	LastUpdateDate datetime NULL,
	LastUpdateUser uniqueidentifier NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_CPadron_DatosLocal SET (LOCK_ESCALATION = TABLE)
GO
ALTER TABLE dbo.Tmp_CPadron_DatosLocal ADD CONSTRAINT
	DF_CPadron_DatosLocal_CreateDate DEFAULT (getdate()) FOR CreateDate
GO
SET IDENTITY_INSERT dbo.Tmp_CPadron_DatosLocal ON
GO
IF EXISTS(SELECT * FROM dbo.CPadron_DatosLocal)
	 EXEC('INSERT INTO dbo.Tmp_CPadron_DatosLocal (id_cpadrondatoslocal, id_cpadron, superficie_cubierta_dl, superficie_descubierta_dl, dimesion_frente_dl, lugar_carga_descarga_dl, estacionamiento_dl, red_transito_pesado_dl, sobre_avenida_dl, materiales_pisos_dl, materiales_paredes_dl, materiales_techos_dl, materiales_revestimientos_dl, sanitarios_ubicacion_dl, sanitarios_distancia_dl, croquis_ubicacion_dl, cantidad_sanitarios_dl, superficie_sanitarios_dl, frente_dl, fondo_dl, lateral_izquierdo_dl, lateral_derecho_dl, cantidad_operarios_dl, CreateDate, CreateUser, LastUpdateDate, LastUpdateUser)
		SELECT id_cpadrondatoslocal, id_cpadron, superficie_cubierta_dl, superficie_descubierta_dl, dimesion_frente_dl, lugar_carga_descarga_dl, estacionamiento_dl, red_transito_pesado_dl, sobre_avenida_dl, materiales_pisos_dl, materiales_paredes_dl, materiales_techos_dl, materiales_revestimientos_dl, sanitarios_ubicacion_dl, sanitarios_distancia_dl, croquis_ubicacion_dl, cantidad_sanitarios_dl, superficie_sanitarios_dl, frente_dl, fondo_dl, lateral_izquierdo_dl, lateral_derecho_dl, cantidad_operarios_dl, CreateDate, CreateUser, LastUpdateDate, LastUpdateUser FROM dbo.CPadron_DatosLocal WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_CPadron_DatosLocal OFF
GO
DROP TABLE dbo.CPadron_DatosLocal
GO
EXECUTE sp_rename N'dbo.Tmp_CPadron_DatosLocal', N'CPadron_DatosLocal', 'OBJECT' 
GO
ALTER TABLE dbo.CPadron_DatosLocal ADD CONSTRAINT
	PK_CPadron_DatosLocal PRIMARY KEY CLUSTERED 
	(
	id_cpadrondatoslocal
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.CPadron_DatosLocal ADD CONSTRAINT
	FK_CPadron_DatosLocal_aspnet_Users_CreateUser FOREIGN KEY
	(
	CreateUser
	) REFERENCES dbo.aspnet_Users
	(
	UserId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CPadron_DatosLocal ADD CONSTRAINT
	FK_CPadron_DatosLocal_aspnet_Users_LastUpdateUser FOREIGN KEY
	(
	LastUpdateUser
	) REFERENCES dbo.aspnet_Users
	(
	UserId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CPadron_DatosLocal ADD CONSTRAINT
	FK_CPadron_DatosLocal_CPadron_Solicitudes FOREIGN KEY
	(
	id_cpadron
	) REFERENCES dbo.CPadron_Solicitudes
	(
	id_cpadron
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Transf_Titulares_PersonasJuridicas
	DROP CONSTRAINT FK_Transf_Titulares_PersonasJuridicas_TiposDeIngresosBrutos
GO
ALTER TABLE dbo.Encomienda_Titulares_PersonasJuridicas
	DROP CONSTRAINT FK_Encomienda_Titulares_PersonasJuridicas_TiposDeIngresosBrutos
GO
ALTER TABLE dbo.SSIT_Solicitudes_Titulares_PersonasFisicas
	DROP CONSTRAINT FK_SSIT_Solicitudes_Titulares_PersonasFisicas_TiposDeIngresosBrutos
GO
ALTER TABLE dbo.SSIT_Solicitudes_Titulares_PersonasJuridicas
	DROP CONSTRAINT FK_SSIT_Solicitudes_Titulares_PersonasJuridicas_TiposDeIngresosBrutos
GO
ALTER TABLE dbo.Encomienda_Titulares_PersonasFisicas
	DROP CONSTRAINT FK_Encomienda_Titulares_PersonasFisicas_TiposDeIngresosBrutos
GO
ALTER TABLE dbo.CPadron_Titulares_Solicitud_PersonasFisicas
	DROP CONSTRAINT FK_CPadron_Titulares_Solicitud_PersonasFisicas_TiposDeIngresosBrutos
GO
ALTER TABLE dbo.Transf_Titulares_PersonasFisicas
	DROP CONSTRAINT FK_Transf_Titulares_PersonasFisicas_TiposDeIngresosBrutos
GO
ALTER TABLE dbo.CPadron_Titulares_Solicitud_PersonasJuridicas
	DROP CONSTRAINT FK_CPadron_Titulares_Solicitud_PersonasJuridicas_TiposDeIngresosBrutos
GO
ALTER TABLE dbo.TiposDeIngresosBrutos SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Transf_Titulares_PersonasJuridicas
	DROP CONSTRAINT FK_Transf_Titulares_PersonasJuridicas_Localidad
GO
ALTER TABLE dbo.Encomienda_Titulares_PersonasJuridicas
	DROP CONSTRAINT FK_Encomienda_Titulares_PersonasJuridicas_Localidad
GO
ALTER TABLE dbo.SSIT_Solicitudes_Titulares_PersonasFisicas
	DROP CONSTRAINT FK_SSIT_Solicitudes_Titulares_PersonasFisicas_Localidad
GO
ALTER TABLE dbo.SSIT_Solicitudes_Titulares_PersonasJuridicas
	DROP CONSTRAINT FK_SSIT_Solicitudes_Titulares_PersonasJuridicas_Localidad
GO
ALTER TABLE dbo.Encomienda_Titulares_PersonasFisicas
	DROP CONSTRAINT FK_Encomienda_Titulares_PersonasFisicas_Localidad
GO
ALTER TABLE dbo.CPadron_Titulares_Solicitud_PersonasFisicas
	DROP CONSTRAINT FK_CPadron_Titulares_Solicitud_PersonasFisicas_Localidad
GO
ALTER TABLE dbo.Transf_Titulares_PersonasFisicas
	DROP CONSTRAINT FK_Transf_Titulares_PersonasFisicas_Localidad
GO
ALTER TABLE dbo.CPadron_Titulares_Solicitud_PersonasJuridicas
	DROP CONSTRAINT FK_CPadron_Titulares_Solicitud_PersonasJuridicas_Localidad
GO
ALTER TABLE dbo.Localidad SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_CPadron_Titulares_Solicitud_PersonasJuridicas
	(
	id_personajuridica int NOT NULL IDENTITY (1, 1),
	id_cpadron int NOT NULL,
	Id_TipoSociedad int NOT NULL,
	Razon_Social nvarchar(200) NULL,
	CUIT nvarchar(13) NULL,
	id_tipoiibb int NOT NULL,
	Nro_IIBB nvarchar(20) NULL,
	Calle nvarchar(70) NULL,
	NroPuerta int NULL,
	Piso nvarchar(5) NULL,
	Depto nvarchar(5) NULL,
	id_localidad int NOT NULL,
	Codigo_Postal nvarchar(10) NULL,
	Telefono nvarchar(50) NULL,
	Email nvarchar(70) NULL,
	CreateUser uniqueidentifier NOT NULL,
	CreateDate datetime NOT NULL,
	LastUpdateUser uniqueidentifier NULL,
	LastUpdateDate datetime NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_CPadron_Titulares_Solicitud_PersonasJuridicas SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_CPadron_Titulares_Solicitud_PersonasJuridicas ON
GO
IF EXISTS(SELECT * FROM dbo.CPadron_Titulares_Solicitud_PersonasJuridicas)
	 EXEC('INSERT INTO dbo.Tmp_CPadron_Titulares_Solicitud_PersonasJuridicas (id_personajuridica, id_cpadron, Id_TipoSociedad, Razon_Social, CUIT, id_tipoiibb, Nro_IIBB, Calle, NroPuerta, Piso, Depto, id_localidad, Codigo_Postal, Telefono, Email, CreateUser, CreateDate, LastUpdateUser, LastUpdateDate)
		SELECT id_personajuridica, id_cpadron, Id_TipoSociedad, Razon_Social, CUIT, id_tipoiibb, Nro_IIBB, Calle, NroPuerta, Piso, Depto, id_localidad, Codigo_Postal, Telefono, Email, CreateUser, CreateDate, LastUpdateUser, LastUpdateDate FROM dbo.CPadron_Titulares_Solicitud_PersonasJuridicas WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_CPadron_Titulares_Solicitud_PersonasJuridicas OFF
GO
ALTER TABLE dbo.CPadron_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas
	DROP CONSTRAINT FK_CPadron_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas_CPadron_Titulares_Solicitud_PersonasJuridicas
GO
DROP TABLE dbo.CPadron_Titulares_Solicitud_PersonasJuridicas
GO
EXECUTE sp_rename N'dbo.Tmp_CPadron_Titulares_Solicitud_PersonasJuridicas', N'CPadron_Titulares_Solicitud_PersonasJuridicas', 'OBJECT' 
GO
ALTER TABLE dbo.CPadron_Titulares_Solicitud_PersonasJuridicas ADD CONSTRAINT
	PK_CPadron_Titulares_Solicitud_PersonasJuridicas PRIMARY KEY CLUSTERED 
	(
	id_personajuridica
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.CPadron_Titulares_Solicitud_PersonasJuridicas ADD CONSTRAINT
	FK_CPadron_Titulares_Solicitud_PersonasJuridicas_TipoSociedad FOREIGN KEY
	(
	Id_TipoSociedad
	) REFERENCES dbo.TipoSociedad
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CPadron_Titulares_Solicitud_PersonasJuridicas ADD CONSTRAINT
	FK_CPadron_Titulares_Solicitud_PersonasJuridicas_CPadron_Solicitudes FOREIGN KEY
	(
	id_cpadron
	) REFERENCES dbo.CPadron_Solicitudes
	(
	id_cpadron
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CPadron_Titulares_Solicitud_PersonasJuridicas ADD CONSTRAINT
	FK_CPadron_Titulares_Solicitud_PersonasJuridicas_Localidad FOREIGN KEY
	(
	id_localidad
	) REFERENCES dbo.Localidad
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CPadron_Titulares_Solicitud_PersonasJuridicas ADD CONSTRAINT
	FK_CPadron_Titulares_Solicitud_PersonasJuridicas_TiposDeIngresosBrutos FOREIGN KEY
	(
	id_tipoiibb
	) REFERENCES dbo.TiposDeIngresosBrutos
	(
	id_tipoiibb
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Transf_Titulares_PersonasJuridicas
	(
	id_personajuridica int NOT NULL IDENTITY (1, 1),
	id_solicitud int NOT NULL,
	Id_TipoSociedad int NOT NULL,
	Razon_Social nvarchar(200) NULL,
	CUIT nvarchar(11) NULL,
	id_tipoiibb int NOT NULL,
	Nro_IIBB nvarchar(20) NULL,
	Calle nvarchar(70) NULL,
	NroPuerta int NULL,
	Piso nvarchar(5) NULL,
	Depto nvarchar(5) NULL,
	id_localidad int NOT NULL,
	Codigo_Postal nvarchar(10) NULL,
	Telefono nvarchar(50) NULL,
	Email nvarchar(70) NULL,
	CreateUser uniqueidentifier NOT NULL,
	CreateDate datetime NOT NULL,
	LastUpdateUser uniqueidentifier NULL,
	LastUpdateDate datetime NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Transf_Titulares_PersonasJuridicas SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_Transf_Titulares_PersonasJuridicas ON
GO
IF EXISTS(SELECT * FROM dbo.Transf_Titulares_PersonasJuridicas)
	 EXEC('INSERT INTO dbo.Tmp_Transf_Titulares_PersonasJuridicas (id_personajuridica, id_solicitud, Id_TipoSociedad, Razon_Social, CUIT, id_tipoiibb, Nro_IIBB, Calle, NroPuerta, Piso, Depto, id_localidad, Codigo_Postal, Telefono, Email, CreateUser, CreateDate, LastUpdateUser, LastUpdateDate)
		SELECT id_personajuridica, id_solicitud, Id_TipoSociedad, Razon_Social, CUIT, id_tipoiibb, Nro_IIBB, Calle, NroPuerta, Piso, Depto, id_localidad, Codigo_Postal, Telefono, Email, CreateUser, CreateDate, LastUpdateUser, LastUpdateDate FROM dbo.Transf_Titulares_PersonasJuridicas WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Transf_Titulares_PersonasJuridicas OFF
GO
ALTER TABLE dbo.Transf_Firmantes_PersonasJuridicas
	DROP CONSTRAINT FK_Transf_Firmantes_PersonasJuridicas_Transf_Titulares_PersonasJuridicas
GO
ALTER TABLE dbo.Transf_Titulares_PersonasJuridicas_PersonasFisicas
	DROP CONSTRAINT FK_Transf_Titulares_PersonasJuridicas_PersonasFisicas_Transf_Titulares_PersonasJuridicas
GO
DROP TABLE dbo.Transf_Titulares_PersonasJuridicas
GO
EXECUTE sp_rename N'dbo.Tmp_Transf_Titulares_PersonasJuridicas', N'Transf_Titulares_PersonasJuridicas', 'OBJECT' 
GO
ALTER TABLE dbo.Transf_Titulares_PersonasJuridicas ADD CONSTRAINT
	PK_Transf_Titulares_PersonasJuridicas PRIMARY KEY CLUSTERED 
	(
	id_personajuridica
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Transf_Titulares_PersonasJuridicas ADD CONSTRAINT
	FK_Transf_Titulares_PersonasJuridicas_aspnet_Users_CreateUser FOREIGN KEY
	(
	CreateUser
	) REFERENCES dbo.aspnet_Users
	(
	UserId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Transf_Titulares_PersonasJuridicas ADD CONSTRAINT
	FK_Transf_Titulares_PersonasJuridicas_aspnet_Users_UpdateUser FOREIGN KEY
	(
	LastUpdateUser
	) REFERENCES dbo.aspnet_Users
	(
	UserId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Transf_Titulares_PersonasJuridicas ADD CONSTRAINT
	FK_Transf_Titulares_PersonasJuridicas_Localidad FOREIGN KEY
	(
	id_localidad
	) REFERENCES dbo.Localidad
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Transf_Titulares_PersonasJuridicas ADD CONSTRAINT
	FK_Transf_Titulares_PersonasJuridicas_TiposDeIngresosBrutos FOREIGN KEY
	(
	id_tipoiibb
	) REFERENCES dbo.TiposDeIngresosBrutos
	(
	id_tipoiibb
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Transf_Titulares_PersonasJuridicas ADD CONSTRAINT
	FK_Transf_Titulares_PersonasJuridicas_Transf_Solicitudes FOREIGN KEY
	(
	id_solicitud
	) REFERENCES dbo.Transf_Solicitudes
	(
	id_solicitud
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Transf_Firmantes_PersonasFisicas
	DROP CONSTRAINT FK_Transf_Firmantes_PersonasFisicas_TiposDeCaracterLegal
GO
ALTER TABLE dbo.Transf_Firmantes_PersonasJuridicas
	DROP CONSTRAINT FK_Transf_Firmantes_PersonasJuridicas_TiposDeCaracterLegal
GO
ALTER TABLE dbo.Encomienda_Firmantes_PersonasFisicas
	DROP CONSTRAINT FK_Encomienda_Firmantes_PersonasFisicas_TiposDeCaracterLegal
GO
ALTER TABLE dbo.Encomienda_Firmantes_PersonasJuridicas
	DROP CONSTRAINT FK_Encomienda_Firmantes_PersonasJuridicas_TiposDeCaracterLegal
GO
ALTER TABLE dbo.SSIT_Solicitudes_Firmantes_PersonasFisicas
	DROP CONSTRAINT FK_SSIT_Solicitudes_Firmantes_PersonasFisicas_TiposDeCaracterLegal
GO
ALTER TABLE dbo.SSIT_Solicitudes_Firmantes_PersonasJuridicas
	DROP CONSTRAINT FK_SSIT_Solicitudes_Firmantes_PersonasJuridicas_TiposDeCaracterLegal
GO
ALTER TABLE dbo.TiposDeCaracterLegal SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Transf_Firmantes_PersonasFisicas
	DROP CONSTRAINT FK_Transf_Firmantes_PersonasFisicas_TipoDocumentoPersonal
GO
ALTER TABLE dbo.Transf_Firmantes_PersonasJuridicas
	DROP CONSTRAINT FK_Transf_Firmantes_PersonasJuridicas_TipoDocumentoPersonal
GO
ALTER TABLE dbo.CPadron_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas
	DROP CONSTRAINT FK_CPadron_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas_TipoDocumentoPersonal
GO
ALTER TABLE dbo.Transf_Titulares_PersonasJuridicas_PersonasFisicas
	DROP CONSTRAINT FK_Transf_Titulares_PersonasJuridicas_PersonasFisicas_TipoDocumentoPersonal
GO
ALTER TABLE dbo.Encomienda_Firmantes_PersonasFisicas
	DROP CONSTRAINT FK_Encomienda_Firmantes_PersonasFisicas_TipoDocumentoPersonal
GO
ALTER TABLE dbo.Encomienda_Firmantes_PersonasJuridicas
	DROP CONSTRAINT FK_Encomienda_Firmantes_PersonasJuridicas_TipoDocumentoPersonal
GO
ALTER TABLE dbo.SSIT_Solicitudes_Firmantes_PersonasFisicas
	DROP CONSTRAINT FK_SSIT_Solicitudes_Firmantes_PersonasFisicas_TipoDocumentoPersonal
GO
ALTER TABLE dbo.SSIT_Solicitudes_Firmantes_PersonasJuridicas
	DROP CONSTRAINT FK_SSIT_Solicitudes_Firmantes_PersonasJuridicas_TipoDocumentoPersonal
GO
ALTER TABLE dbo.SSIT_Solicitudes_Titulares_PersonasFisicas
	DROP CONSTRAINT FK_SSIT_Solicitudes_Titulares_PersonasFisicas_TipoDocumentoPersonal
GO
ALTER TABLE dbo.SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas
	DROP CONSTRAINT FK_SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas_TipoDocumentoPersonal
GO
ALTER TABLE dbo.Encomienda_Titulares_PersonasFisicas
	DROP CONSTRAINT FK_Encomienda_Titulares_PersonasFisicas_TipoDocumentoPersonal
GO
ALTER TABLE dbo.Encomienda_Titulares_PersonasJuridicas_PersonasFisicas
	DROP CONSTRAINT FK_Encomienda_Titulares_PersonasJuridicas_PersonasFisicas_TipoDocumentoPersonal
GO
ALTER TABLE dbo.CPadron_Titulares_Solicitud_PersonasFisicas
	DROP CONSTRAINT FK_CPadron_Titulares_Solicitud_PersonasFisicas_TipoDocumentoPersonal
GO
ALTER TABLE dbo.Transf_Titulares_PersonasFisicas
	DROP CONSTRAINT FK_Transf_Titulares_PersonasFisicas_TipoDocumentoPersonal
GO
ALTER TABLE dbo.TipoDocumentoPersonal SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Transf_Titulares_PersonasFisicas
	(
	id_personafisica int NOT NULL IDENTITY (1, 1),
	id_solicitud int NOT NULL,
	Apellido varchar(50) NOT NULL,
	Nombres nvarchar(50) NOT NULL,
	id_tipodoc_personal int NOT NULL,
	Nro_Documento nvarchar(15) NOT NULL,
	Cuit nvarchar(11) NULL,
	id_tipoiibb int NOT NULL,
	Ingresos_Brutos nvarchar(25) NULL,
	Calle nvarchar(70) NOT NULL,
	Nro_Puerta int NOT NULL,
	Piso varchar(2) NULL,
	Depto varchar(10) NULL,
	id_Localidad int NOT NULL,
	Codigo_Postal nvarchar(10) NULL,
	Telefono nvarchar(50) NULL,
	Celular nvarchar(50) NULL,
	Email nvarchar(70) NULL,
	MismoFirmante bit NOT NULL,
	CreateUser uniqueidentifier NOT NULL,
	CreateDate datetime NOT NULL,
	LastUpdateUser uniqueidentifier NULL,
	LastupdateDate datetime NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Transf_Titulares_PersonasFisicas SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_Transf_Titulares_PersonasFisicas ON
GO
IF EXISTS(SELECT * FROM dbo.Transf_Titulares_PersonasFisicas)
	 EXEC('INSERT INTO dbo.Tmp_Transf_Titulares_PersonasFisicas (id_personafisica, id_solicitud, Apellido, Nombres, id_tipodoc_personal, Nro_Documento, Cuit, id_tipoiibb, Ingresos_Brutos, Calle, Nro_Puerta, Piso, Depto, id_Localidad, Codigo_Postal, Telefono, Celular, Email, MismoFirmante, CreateUser, CreateDate, LastUpdateUser, LastupdateDate)
		SELECT id_personafisica, id_solicitud, Apellido, Nombres, id_tipodoc_personal, Nro_Documento, Cuit, id_tipoiibb, Ingresos_Brutos, Calle, Nro_Puerta, Piso, Depto, id_Localidad, Codigo_Postal, Telefono, Celular, Email, MismoFirmante, CreateUser, CreateDate, LastUpdateUser, LastupdateDate FROM dbo.Transf_Titulares_PersonasFisicas WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Transf_Titulares_PersonasFisicas OFF
GO
ALTER TABLE dbo.Transf_Firmantes_PersonasFisicas
	DROP CONSTRAINT FK_Transf_Firmantes_PersonasFisicas_Transf_Titulares_PersonasFisicas
GO
DROP TABLE dbo.Transf_Titulares_PersonasFisicas
GO
EXECUTE sp_rename N'dbo.Tmp_Transf_Titulares_PersonasFisicas', N'Transf_Titulares_PersonasFisicas', 'OBJECT' 
GO
ALTER TABLE dbo.Transf_Titulares_PersonasFisicas ADD CONSTRAINT
	PK_Transf_Titulares_PersonasFisicas PRIMARY KEY CLUSTERED 
	(
	id_personafisica
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Transf_Titulares_PersonasFisicas ADD CONSTRAINT
	FK_Transf_Titulares_PersonasFisicas_aspnet_Users_CreateUser FOREIGN KEY
	(
	CreateUser
	) REFERENCES dbo.aspnet_Users
	(
	UserId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Transf_Titulares_PersonasFisicas ADD CONSTRAINT
	FK_Transf_Titulares_PersonasFisicas_aspnet_Users_UpdateUser FOREIGN KEY
	(
	LastUpdateUser
	) REFERENCES dbo.aspnet_Users
	(
	UserId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Transf_Titulares_PersonasFisicas ADD CONSTRAINT
	FK_Transf_Titulares_PersonasFisicas_Localidad FOREIGN KEY
	(
	id_Localidad
	) REFERENCES dbo.Localidad
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Transf_Titulares_PersonasFisicas ADD CONSTRAINT
	FK_Transf_Titulares_PersonasFisicas_TipoDocumentoPersonal FOREIGN KEY
	(
	id_tipodoc_personal
	) REFERENCES dbo.TipoDocumentoPersonal
	(
	TipoDocumentoPersonalId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Transf_Titulares_PersonasFisicas ADD CONSTRAINT
	FK_Transf_Titulares_PersonasFisicas_TiposDeIngresosBrutos FOREIGN KEY
	(
	id_tipoiibb
	) REFERENCES dbo.TiposDeIngresosBrutos
	(
	id_tipoiibb
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Transf_Titulares_PersonasFisicas ADD CONSTRAINT
	FK_Transf_Titulares_PersonasFisicas_Transf_Solicitudes FOREIGN KEY
	(
	id_solicitud
	) REFERENCES dbo.Transf_Solicitudes
	(
	id_solicitud
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_CPadron_Titulares_Solicitud_PersonasFisicas
	(
	id_personafisica int NOT NULL IDENTITY (1, 1),
	id_cpadron int NOT NULL,
	Apellido varchar(50) NOT NULL,
	Nombres nvarchar(50) NOT NULL,
	id_tipodoc_personal int NOT NULL,
	Cuit nvarchar(13) NULL,
	id_tipoiibb int NOT NULL,
	Ingresos_Brutos nvarchar(25) NULL,
	Calle nvarchar(70) NOT NULL,
	Nro_Puerta int NOT NULL,
	Piso varchar(2) NULL,
	Depto varchar(10) NULL,
	Id_Localidad int NOT NULL,
	Codigo_Postal nvarchar(10) NULL,
	TelefonoMovil nvarchar(20) NULL,
	Sms nvarchar(50) NULL,
	Email nvarchar(70) NULL,
	MismoFirmante bit NOT NULL,
	CreateUser uniqueidentifier NOT NULL,
	CreateDate datetime NOT NULL,
	LastUpdateUser uniqueidentifier NULL,
	LastupdateDate datetime NULL,
	Nro_Documento nvarchar(15) NULL,
	Telefono nchar(50) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_CPadron_Titulares_Solicitud_PersonasFisicas SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_CPadron_Titulares_Solicitud_PersonasFisicas ON
GO
IF EXISTS(SELECT * FROM dbo.CPadron_Titulares_Solicitud_PersonasFisicas)
	 EXEC('INSERT INTO dbo.Tmp_CPadron_Titulares_Solicitud_PersonasFisicas (id_personafisica, id_cpadron, Apellido, Nombres, id_tipodoc_personal, Cuit, id_tipoiibb, Ingresos_Brutos, Calle, Nro_Puerta, Piso, Depto, Id_Localidad, Codigo_Postal, TelefonoMovil, Sms, Email, MismoFirmante, CreateUser, CreateDate, LastUpdateUser, LastupdateDate, Nro_Documento, Telefono)
		SELECT id_personafisica, id_cpadron, Apellido, Nombres, id_tipodoc_personal, Cuit, id_tipoiibb, Ingresos_Brutos, Calle, Nro_Puerta, Piso, Depto, Id_Localidad, Codigo_Postal, TelefonoMovil, Sms, Email, MismoFirmante, CreateUser, CreateDate, LastUpdateUser, LastupdateDate, Nro_Documento, Telefono FROM dbo.CPadron_Titulares_Solicitud_PersonasFisicas WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_CPadron_Titulares_Solicitud_PersonasFisicas OFF
GO
DROP TABLE dbo.CPadron_Titulares_Solicitud_PersonasFisicas
GO
EXECUTE sp_rename N'dbo.Tmp_CPadron_Titulares_Solicitud_PersonasFisicas', N'CPadron_Titulares_Solicitud_PersonasFisicas', 'OBJECT' 
GO
ALTER TABLE dbo.CPadron_Titulares_Solicitud_PersonasFisicas ADD CONSTRAINT
	PK_CPadron_Titulares_Solicitud_PersonasFisicas PRIMARY KEY CLUSTERED 
	(
	id_personafisica
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.CPadron_Titulares_Solicitud_PersonasFisicas ADD CONSTRAINT
	FK_CPadron_Titulares_Solicitud_PersonasFisicas_CPadron_Solicitudes FOREIGN KEY
	(
	id_cpadron
	) REFERENCES dbo.CPadron_Solicitudes
	(
	id_cpadron
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CPadron_Titulares_Solicitud_PersonasFisicas ADD CONSTRAINT
	FK_CPadron_Titulares_Solicitud_PersonasFisicas_Localidad FOREIGN KEY
	(
	Id_Localidad
	) REFERENCES dbo.Localidad
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CPadron_Titulares_Solicitud_PersonasFisicas ADD CONSTRAINT
	FK_CPadron_Titulares_Solicitud_PersonasFisicas_TipoDocumentoPersonal FOREIGN KEY
	(
	id_tipodoc_personal
	) REFERENCES dbo.TipoDocumentoPersonal
	(
	TipoDocumentoPersonalId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CPadron_Titulares_Solicitud_PersonasFisicas ADD CONSTRAINT
	FK_CPadron_Titulares_Solicitud_PersonasFisicas_TiposDeIngresosBrutos FOREIGN KEY
	(
	id_tipoiibb
	) REFERENCES dbo.TiposDeIngresosBrutos
	(
	id_tipoiibb
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.CPadron_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas
	DROP CONSTRAINT DF_CPadron_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas_firmante_misma_persona
GO
CREATE TABLE dbo.Tmp_CPadron_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas
	(
	id_titular_pj int NOT NULL IDENTITY (1, 1),
	id_cpadron int NOT NULL,
	id_personajuridica int NOT NULL,
	Apellido varchar(50) NOT NULL,
	Nombres nvarchar(50) NOT NULL,
	id_tipodoc_personal int NOT NULL,
	Email nvarchar(70) NULL,
	firmante_misma_persona bit NOT NULL,
	Nro_Documento nvarchar(15) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_CPadron_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas SET (LOCK_ESCALATION = TABLE)
GO
ALTER TABLE dbo.Tmp_CPadron_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas ADD CONSTRAINT
	DF_CPadron_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas_firmante_misma_persona DEFAULT ((0)) FOR firmante_misma_persona
GO
SET IDENTITY_INSERT dbo.Tmp_CPadron_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas ON
GO
IF EXISTS(SELECT * FROM dbo.CPadron_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas)
	 EXEC('INSERT INTO dbo.Tmp_CPadron_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas (id_titular_pj, id_cpadron, id_personajuridica, Apellido, Nombres, id_tipodoc_personal, Email, firmante_misma_persona, Nro_Documento)
		SELECT id_titular_pj, id_cpadron, id_personajuridica, Apellido, Nombres, id_tipodoc_personal, Email, firmante_misma_persona, Nro_Documento FROM dbo.CPadron_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_CPadron_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas OFF
GO
DROP TABLE dbo.CPadron_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas
GO
EXECUTE sp_rename N'dbo.Tmp_CPadron_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas', N'CPadron_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas', 'OBJECT' 
GO
ALTER TABLE dbo.CPadron_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas ADD CONSTRAINT
	PK_CPadron_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas PRIMARY KEY CLUSTERED 
	(
	id_titular_pj
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.CPadron_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas ADD CONSTRAINT
	FK_CPadron_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas_CPadron_Solicitudes FOREIGN KEY
	(
	id_cpadron
	) REFERENCES dbo.CPadron_Solicitudes
	(
	id_cpadron
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CPadron_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas ADD CONSTRAINT
	FK_CPadron_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas_CPadron_Titulares_Solicitud_PersonasJuridicas FOREIGN KEY
	(
	id_personajuridica
	) REFERENCES dbo.CPadron_Titulares_Solicitud_PersonasJuridicas
	(
	id_personajuridica
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CPadron_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas ADD CONSTRAINT
	FK_CPadron_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas_TipoDocumentoPersonal FOREIGN KEY
	(
	id_tipodoc_personal
	) REFERENCES dbo.TipoDocumentoPersonal
	(
	TipoDocumentoPersonalId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Transf_Firmantes_PersonasJuridicas
	(
	id_firmante_pj int NOT NULL IDENTITY (1, 1),
	id_solicitud int NOT NULL,
	id_personajuridica int NOT NULL,
	Apellido varchar(50) NOT NULL,
	Nombres nvarchar(50) NOT NULL,
	id_tipodoc_personal int NOT NULL,
	Nro_Documento nvarchar(15) NOT NULL,
	Email nvarchar(70) NULL,
	id_tipocaracter int NOT NULL,
	cargo_firmante_pj nvarchar(50) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Transf_Firmantes_PersonasJuridicas SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_Transf_Firmantes_PersonasJuridicas ON
GO
IF EXISTS(SELECT * FROM dbo.Transf_Firmantes_PersonasJuridicas)
	 EXEC('INSERT INTO dbo.Tmp_Transf_Firmantes_PersonasJuridicas (id_firmante_pj, id_solicitud, id_personajuridica, Apellido, Nombres, id_tipodoc_personal, Nro_Documento, Email, id_tipocaracter, cargo_firmante_pj)
		SELECT id_firmante_pj, id_solicitud, id_personajuridica, Apellido, Nombres, id_tipodoc_personal, Nro_Documento, Email, id_tipocaracter, cargo_firmante_pj FROM dbo.Transf_Firmantes_PersonasJuridicas WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Transf_Firmantes_PersonasJuridicas OFF
GO
ALTER TABLE dbo.Transf_Titulares_PersonasJuridicas_PersonasFisicas
	DROP CONSTRAINT FK_Transf_Titulares_PersonasJuridicas_PersonasFisicas_Transf_Firmantes_PersonasJuridicas
GO
DROP TABLE dbo.Transf_Firmantes_PersonasJuridicas
GO
EXECUTE sp_rename N'dbo.Tmp_Transf_Firmantes_PersonasJuridicas', N'Transf_Firmantes_PersonasJuridicas', 'OBJECT' 
GO
ALTER TABLE dbo.Transf_Firmantes_PersonasJuridicas ADD CONSTRAINT
	PK_Transf_Firmantes_PersonasJuridicas PRIMARY KEY CLUSTERED 
	(
	id_firmante_pj
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Transf_Firmantes_PersonasJuridicas ADD CONSTRAINT
	FK_Transf_Firmantes_PersonasJuridicas_TipoDocumentoPersonal FOREIGN KEY
	(
	id_tipodoc_personal
	) REFERENCES dbo.TipoDocumentoPersonal
	(
	TipoDocumentoPersonalId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Transf_Firmantes_PersonasJuridicas ADD CONSTRAINT
	FK_Transf_Firmantes_PersonasJuridicas_TiposDeCaracterLegal FOREIGN KEY
	(
	id_tipocaracter
	) REFERENCES dbo.TiposDeCaracterLegal
	(
	id_tipocaracter
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Transf_Firmantes_PersonasJuridicas ADD CONSTRAINT
	FK_Transf_Firmantes_PersonasJuridicas_Transf_Solicitudes FOREIGN KEY
	(
	id_solicitud
	) REFERENCES dbo.Transf_Solicitudes
	(
	id_solicitud
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Transf_Firmantes_PersonasJuridicas ADD CONSTRAINT
	FK_Transf_Firmantes_PersonasJuridicas_Transf_Titulares_PersonasJuridicas FOREIGN KEY
	(
	id_personajuridica
	) REFERENCES dbo.Transf_Titulares_PersonasJuridicas
	(
	id_personajuridica
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Transf_Titulares_PersonasJuridicas_PersonasFisicas
	DROP CONSTRAINT DF_Transf_Titulares_PersonasJuridicas_PersonasFisicas_firmante_misma_persona
GO
CREATE TABLE dbo.Tmp_Transf_Titulares_PersonasJuridicas_PersonasFisicas
	(
	id_titular_pj int NOT NULL IDENTITY (1, 1),
	id_solicitud int NOT NULL,
	id_personajuridica int NOT NULL,
	Apellido varchar(50) NOT NULL,
	Nombres nvarchar(50) NOT NULL,
	id_tipodoc_personal int NOT NULL,
	Nro_Documento nvarchar(15) NOT NULL,
	Email nvarchar(70) NULL,
	id_firmante_pj int NOT NULL,
	firmante_misma_persona bit NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Transf_Titulares_PersonasJuridicas_PersonasFisicas SET (LOCK_ESCALATION = TABLE)
GO
ALTER TABLE dbo.Tmp_Transf_Titulares_PersonasJuridicas_PersonasFisicas ADD CONSTRAINT
	DF_Transf_Titulares_PersonasJuridicas_PersonasFisicas_firmante_misma_persona DEFAULT ((0)) FOR firmante_misma_persona
GO
SET IDENTITY_INSERT dbo.Tmp_Transf_Titulares_PersonasJuridicas_PersonasFisicas ON
GO
IF EXISTS(SELECT * FROM dbo.Transf_Titulares_PersonasJuridicas_PersonasFisicas)
	 EXEC('INSERT INTO dbo.Tmp_Transf_Titulares_PersonasJuridicas_PersonasFisicas (id_titular_pj, id_solicitud, id_personajuridica, Apellido, Nombres, id_tipodoc_personal, Nro_Documento, Email, id_firmante_pj, firmante_misma_persona)
		SELECT id_titular_pj, id_solicitud, id_personajuridica, Apellido, Nombres, id_tipodoc_personal, Nro_Documento, Email, id_firmante_pj, firmante_misma_persona FROM dbo.Transf_Titulares_PersonasJuridicas_PersonasFisicas WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Transf_Titulares_PersonasJuridicas_PersonasFisicas OFF
GO
DROP TABLE dbo.Transf_Titulares_PersonasJuridicas_PersonasFisicas
GO
EXECUTE sp_rename N'dbo.Tmp_Transf_Titulares_PersonasJuridicas_PersonasFisicas', N'Transf_Titulares_PersonasJuridicas_PersonasFisicas', 'OBJECT' 
GO
ALTER TABLE dbo.Transf_Titulares_PersonasJuridicas_PersonasFisicas ADD CONSTRAINT
	PK_Transf_Titulares_PersonasJuridicas_PersonasFisicas PRIMARY KEY CLUSTERED 
	(
	id_titular_pj
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Transf_Titulares_PersonasJuridicas_PersonasFisicas ADD CONSTRAINT
	FK_Transf_Titulares_PersonasJuridicas_PersonasFisicas_CAA FOREIGN KEY
	(
	id_solicitud
	) REFERENCES dbo.Transf_Solicitudes
	(
	id_solicitud
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Transf_Titulares_PersonasJuridicas_PersonasFisicas ADD CONSTRAINT
	FK_Transf_Titulares_PersonasJuridicas_PersonasFisicas_TipoDocumentoPersonal FOREIGN KEY
	(
	id_tipodoc_personal
	) REFERENCES dbo.TipoDocumentoPersonal
	(
	TipoDocumentoPersonalId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Transf_Titulares_PersonasJuridicas_PersonasFisicas ADD CONSTRAINT
	FK_Transf_Titulares_PersonasJuridicas_PersonasFisicas_Transf_Firmantes_PersonasJuridicas FOREIGN KEY
	(
	id_firmante_pj
	) REFERENCES dbo.Transf_Firmantes_PersonasJuridicas
	(
	id_firmante_pj
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Transf_Titulares_PersonasJuridicas_PersonasFisicas ADD CONSTRAINT
	FK_Transf_Titulares_PersonasJuridicas_PersonasFisicas_Transf_Titulares_PersonasJuridicas FOREIGN KEY
	(
	id_personajuridica
	) REFERENCES dbo.Transf_Titulares_PersonasJuridicas
	(
	id_personajuridica
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Transf_Firmantes_PersonasFisicas
	(
	id_firmante_pf int NOT NULL IDENTITY (1, 1),
	id_solicitud int NOT NULL,
	id_personafisica int NOT NULL,
	Apellido varchar(50) NOT NULL,
	Nombres nvarchar(50) NOT NULL,
	id_tipodoc_personal int NOT NULL,
	Nro_Documento nvarchar(15) NOT NULL,
	id_tipocaracter int NOT NULL,
	Email nvarchar(70) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Transf_Firmantes_PersonasFisicas SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_Transf_Firmantes_PersonasFisicas ON
GO
IF EXISTS(SELECT * FROM dbo.Transf_Firmantes_PersonasFisicas)
	 EXEC('INSERT INTO dbo.Tmp_Transf_Firmantes_PersonasFisicas (id_firmante_pf, id_solicitud, id_personafisica, Apellido, Nombres, id_tipodoc_personal, Nro_Documento, id_tipocaracter, Email)
		SELECT id_firmante_pf, id_solicitud, id_personafisica, Apellido, Nombres, id_tipodoc_personal, Nro_Documento, id_tipocaracter, Email FROM dbo.Transf_Firmantes_PersonasFisicas WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Transf_Firmantes_PersonasFisicas OFF
GO
DROP TABLE dbo.Transf_Firmantes_PersonasFisicas
GO
EXECUTE sp_rename N'dbo.Tmp_Transf_Firmantes_PersonasFisicas', N'Transf_Firmantes_PersonasFisicas', 'OBJECT' 
GO
ALTER TABLE dbo.Transf_Firmantes_PersonasFisicas ADD CONSTRAINT
	PK_Transf_Firmantes_PersonasFisicas PRIMARY KEY CLUSTERED 
	(
	id_firmante_pf
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Transf_Firmantes_PersonasFisicas ADD CONSTRAINT
	FK_Transf_Firmantes_PersonasFisicas_TipoDocumentoPersonal FOREIGN KEY
	(
	id_tipodoc_personal
	) REFERENCES dbo.TipoDocumentoPersonal
	(
	TipoDocumentoPersonalId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Transf_Firmantes_PersonasFisicas ADD CONSTRAINT
	FK_Transf_Firmantes_PersonasFisicas_TiposDeCaracterLegal FOREIGN KEY
	(
	id_tipocaracter
	) REFERENCES dbo.TiposDeCaracterLegal
	(
	id_tipocaracter
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Transf_Firmantes_PersonasFisicas ADD CONSTRAINT
	FK_Transf_Firmantes_PersonasFisicas_Transf_Solicitudes FOREIGN KEY
	(
	id_solicitud
	) REFERENCES dbo.Transf_Solicitudes
	(
	id_solicitud
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Transf_Firmantes_PersonasFisicas ADD CONSTRAINT
	FK_Transf_Firmantes_PersonasFisicas_Transf_Titulares_PersonasFisicas FOREIGN KEY
	(
	id_personafisica
	) REFERENCES dbo.Transf_Titulares_PersonasFisicas
	(
	id_personafisica
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.SSIT_Solicitudes
	DROP CONSTRAINT DF_ScCSSIT_Solicitudes_telefono
GO
ALTER TABLE dbo.SSIT_Solicitudes
	DROP CONSTRAINT DF_ScCSSIT_Solicitudes_Servidumbre_paso
GO
CREATE TABLE dbo.Tmp_SSIT_Solicitudes
	(
	id_solicitud int NOT NULL IDENTITY (1, 1),
	id_tipotramite int NOT NULL,
	id_tipoexpediente int NOT NULL,
	id_subtipoexpediente int NOT NULL,
	MatriculaEscribano int NULL,
	NroExpediente nvarchar(20) NULL,
	id_estado int NOT NULL,
	CreateDate datetime NOT NULL,
	CreateUser uniqueidentifier NOT NULL,
	LastUpdateDate datetime NULL,
	LastUpdateUser uniqueidentifier NULL,
	NroExpedienteSade nvarchar(50) NULL,
	telefono varchar(25) NULL,
	FechaLibrado datetime NULL,
	CodigoSeguridad varchar(6) NULL,
	Servidumbre_paso bit NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_SSIT_Solicitudes SET (LOCK_ESCALATION = TABLE)
GO
ALTER TABLE dbo.Tmp_SSIT_Solicitudes ADD CONSTRAINT
	DF_ScCSSIT_Solicitudes_telefono DEFAULT (NULL) FOR telefono
GO
ALTER TABLE dbo.Tmp_SSIT_Solicitudes ADD CONSTRAINT
	DF_ScCSSIT_Solicitudes_Servidumbre_paso DEFAULT ((0)) FOR Servidumbre_paso
GO
SET IDENTITY_INSERT dbo.Tmp_SSIT_Solicitudes ON
GO
IF EXISTS(SELECT * FROM dbo.SSIT_Solicitudes)
	 EXEC('INSERT INTO dbo.Tmp_SSIT_Solicitudes (id_solicitud, id_tipotramite, id_tipoexpediente, id_subtipoexpediente, MatriculaEscribano, NroExpediente, id_estado, CreateDate, CreateUser, LastUpdateDate, LastUpdateUser, NroExpedienteSade, telefono, FechaLibrado, CodigoSeguridad, Servidumbre_paso)
		SELECT id_solicitud, id_tipotramite, id_tipoexpediente, id_subtipoexpediente, MatriculaEscribano, NroExpediente, id_estado, CreateDate, CreateUser, LastUpdateDate, LastUpdateUser, NroExpedienteSade, telefono, FechaLibrado, CodigoSeguridad, Servidumbre_paso FROM dbo.SSIT_Solicitudes WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_SSIT_Solicitudes OFF
GO
ALTER TABLE dbo.SSIT_Solicitudes_Encomienda
	DROP CONSTRAINT FK_SSIT_Solicitudes_Encomienda_SSIT_Solicitudes
GO
ALTER TABLE dbo.SSIT_DocumentosAdjuntos
	DROP CONSTRAINT FK_SSIT_DocumentosAdjuntos_SSIT_Solicitudes
GO
ALTER TABLE dbo.SSIT_Solicitudes_Observaciones
	DROP CONSTRAINT FK_SSIT_Solicitudes_Observaciones_SSIT_Solicitudes
GO
ALTER TABLE dbo.SSIT_Solicitudes_AvisoCaducidad
	DROP CONSTRAINT FK_SSIT_Solicitudes_AvisoCaducidad_SSIT_Solicitudes
GO
ALTER TABLE dbo.SSIT_Solicitudes_HistorialEstados
	DROP CONSTRAINT FK_SSIT_Solicitudes_HistorialEstados_SSIT_Solicitudes
GO
ALTER TABLE dbo.SSIT_Solicitudes_Ubicaciones
	DROP CONSTRAINT FK_SSIT_Solicitudes_Ubicaciones_SSIT_Solicitudes
GO
ALTER TABLE dbo.SSIT_Solicitudes_Pagos
	DROP CONSTRAINT FK_SSIT_Solicitudes_Pagos_SSIT_Solicitudes
GO
ALTER TABLE dbo.SSIT_Solicitudes_Firmantes_PersonasFisicas
	DROP CONSTRAINT FK_SSIT_Solicitudes_Firmantes_PersonasFisicas_SSIT_Solicitudes
GO
ALTER TABLE dbo.SSIT_Solicitudes_Firmantes_PersonasJuridicas
	DROP CONSTRAINT FK_SSIT_Solicitudes_Firmantes_PersonasJuridicas_SSIT_Solicitudes
GO
ALTER TABLE dbo.SGI_Tramites_Tareas_HAB
	DROP CONSTRAINT FK_SGI_Tramites_Tareas_HAB_SSIT_Solicitudes
GO
ALTER TABLE dbo.SSIT_Solicitudes_Titulares_PersonasFisicas
	DROP CONSTRAINT FK_SSIT_Solicitudes_Titulares_PersonasFisicas_SSIT_Solicitudes
GO
ALTER TABLE dbo.SSIT_Solicitudes_Titulares_PersonasJuridicas
	DROP CONSTRAINT FK_SSIT_Solicitudes_Titulares_PersonasJuridicas_SSIT_Solicitudes
GO
ALTER TABLE dbo.SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas
	DROP CONSTRAINT FK_SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas_SSIT_Solicitudes
GO
ALTER TABLE dbo.Encomienda
	DROP CONSTRAINT FK_Encomienda_SSIT_Solicitudes
GO
DROP TABLE dbo.SSIT_Solicitudes
GO
EXECUTE sp_rename N'dbo.Tmp_SSIT_Solicitudes', N'SSIT_Solicitudes', 'OBJECT' 
GO
ALTER TABLE dbo.SSIT_Solicitudes ADD CONSTRAINT
	PK_SSIT_Solicitudes PRIMARY KEY CLUSTERED 
	(
	id_solicitud
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
CREATE NONCLUSTERED INDEX IX_SSIT_Solicitudes_1 ON dbo.SSIT_Solicitudes
	(
	id_estado
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE dbo.SSIT_Solicitudes ADD CONSTRAINT
	FK_SSIT_Solicitudes_SubtipoExpediente FOREIGN KEY
	(
	id_subtipoexpediente
	) REFERENCES dbo.SubtipoExpediente
	(
	id_subtipoexpediente
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SSIT_Solicitudes ADD CONSTRAINT
	FK_SSIT_Solicitudes_TipoEstadoSolicitud FOREIGN KEY
	(
	id_estado
	) REFERENCES dbo.TipoEstadoSolicitud
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SSIT_Solicitudes ADD CONSTRAINT
	FK_SSIT_Solicitudes_TipoExpediente FOREIGN KEY
	(
	id_tipoexpediente
	) REFERENCES dbo.TipoExpediente
	(
	id_tipoexpediente
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SSIT_Solicitudes ADD CONSTRAINT
	FK_SSIT_Solicitudes_TipoTramite FOREIGN KEY
	(
	id_tipotramite
	) REFERENCES dbo.TipoTramite
	(
	id_tipotramite
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
CREATE TRIGGER [dbo].[tr_SSIT_Solicitudes_historial_estados]
   ON  dbo.SSIT_Solicitudes
   AFTER UPDATE
AS 
BEGIN
	SET NOCOUNT ON;


	DECLARE @cod_estado_ant	nvarchar(20)
	DECLARE @cod_estado_nuevo nvarchar(20)
	DECLARE @usuario uniqueidentifier
	DECLARE @id_solicitud int
	DECLARE @username nvarchar(50)
	DECLARE @userid uniqueidentifier
	
	IF UPDATE(id_estado)
	BEGIN
		
		DECLARE cur CURSOR FAST_FORWARD FOR
		SELECT 
			i.id_solicitud,
			tipest_i.nombre,
			tipest_d.nombre,
			IsNull(i.LastUpdateUser,i.CreateUser)
		FROM 
			inserted i 
			INNER JOIN deleted d ON i.id_solicitud = d.id_solicitud
			INNER JOIN TipoEstadoSolicitud tipest_i ON tipest_i.id = i.id_estado
			INNER JOIN TipoEstadoSolicitud tipest_d ON tipest_d.id = d.id_estado
			
		
		OPEN cur
		FETCH NEXT FROM cur INTO @id_solicitud,  @cod_estado_nuevo, @cod_estado_ant,@userid
		
		WHILE @@FETCH_STATUS = 0
		BEGIN

			IF @cod_estado_ant <> @cod_estado_nuevo 
			BEGIN
			
				SELECT @username = usu.loweredusername FROM aspnet_users usu WHERE usu.userid = @userid
				
				INSERT INTO SSIT_Solicitudes_HistorialEstados
				(
					id_solicitud,
					cod_estado_ant,
					cod_estado_nuevo,
					username,
					fecha_modificacion,
					usuario_modificacion
				)
				VALUES
				(
					@id_solicitud,
					@cod_estado_ant,
					@cod_estado_nuevo,
					@username,
					GETDATE(),
					@userid
				)
			END
		
			FETCH NEXT FROM cur INTO @id_solicitud, @cod_estado_nuevo, @cod_estado_ant,@userid
		
		END
		CLOSE cur
		DEALLOCATE cur
		
	END  

END
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Encomienda
	DROP CONSTRAINT DF_ScCEncomienda_Pro_teatro
GO
ALTER TABLE dbo.Encomienda
	DROP CONSTRAINT DF_ScCEncomienda_Servidumbre_paso
GO
CREATE TABLE dbo.Tmp_Encomienda
	(
	id_encomienda int NOT NULL IDENTITY (1, 1),
	FechaEncomienda datetime NOT NULL,
	nroEncomiendaconsejo int NOT NULL,
	id_consejo int NOT NULL,
	id_profesional int NOT NULL,
	ZonaDeclarada nvarchar(15) NULL,
	id_tipotramite int NOT NULL,
	id_tipoexpediente int NOT NULL,
	id_subtipoexpediente int NOT NULL,
	id_estado int NOT NULL,
	CodigoSeguridad nvarchar(10) NOT NULL,
	Observaciones_plantas nvarchar(200) NULL,
	Observaciones_rubros nvarchar(300) NULL,
	CreateDate datetime NOT NULL,
	CreateUser uniqueidentifier NOT NULL,
	LastUpdateDate datetime NULL,
	LastUpdateUser uniqueidentifier NULL,
	Pro_teatro bit NOT NULL,
	id_solicitud int NULL,
	tipo_anexo nvarchar(1) NULL,
	Servidumbre_paso bit NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Encomienda SET (LOCK_ESCALATION = TABLE)
GO
ALTER TABLE dbo.Tmp_Encomienda ADD CONSTRAINT
	DF_ScCEncomienda_Pro_teatro DEFAULT ((0)) FOR Pro_teatro
GO
ALTER TABLE dbo.Tmp_Encomienda ADD CONSTRAINT
	DF_ScCEncomienda_Servidumbre_paso DEFAULT ((0)) FOR Servidumbre_paso
GO
SET IDENTITY_INSERT dbo.Tmp_Encomienda ON
GO
IF EXISTS(SELECT * FROM dbo.Encomienda)
	 EXEC('INSERT INTO dbo.Tmp_Encomienda (id_encomienda, FechaEncomienda, nroEncomiendaconsejo, id_consejo, id_profesional, ZonaDeclarada, id_tipotramite, id_tipoexpediente, id_subtipoexpediente, id_estado, CodigoSeguridad, Observaciones_plantas, Observaciones_rubros, CreateDate, CreateUser, LastUpdateDate, LastUpdateUser, Pro_teatro, id_solicitud, tipo_anexo, Servidumbre_paso)
		SELECT id_encomienda, FechaEncomienda, nroEncomiendaconsejo, id_consejo, id_profesional, ZonaDeclarada, id_tipotramite, id_tipoexpediente, id_subtipoexpediente, id_estado, CodigoSeguridad, Observaciones_plantas, Observaciones_rubros, CreateDate, CreateUser, LastUpdateDate, LastUpdateUser, Pro_teatro, id_solicitud, tipo_anexo, Servidumbre_paso FROM dbo.Encomienda WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Encomienda OFF
GO
ALTER TABLE dbo.SSIT_Solicitudes_Encomienda
	DROP CONSTRAINT FK_SSIT_Solicitudes_Encomienda_Encomienda
GO
ALTER TABLE dbo.Encomienda_Titulares_PersonasJuridicas
	DROP CONSTRAINT FK_Encomienda_Titulares_PersonasJuridicas_Encomienda
GO
ALTER TABLE dbo.Encomienda_Firmantes_PersonasFisicas
	DROP CONSTRAINT FK_Encomienda_Firmantes_PersonasFisicas_Encomienda
GO
ALTER TABLE dbo.Encomienda_Firmantes_PersonasJuridicas
	DROP CONSTRAINT FK_Encomienda_Firmantes_PersonasJuridicas_Encomienda
GO
ALTER TABLE dbo.Encomienda_Plantas
	DROP CONSTRAINT FK_Encomienda_Plantas_Encomienda
GO
ALTER TABLE dbo.Encomienda_Planos
	DROP CONSTRAINT FK_Encomienda_Planos_Encomienda
GO
ALTER TABLE dbo.Rel_Encomienda_Rectificatoria
	DROP CONSTRAINT FK_Rel_Encomienda_Rectificatoria_Encomienda
GO
ALTER TABLE dbo.Rel_Encomienda_Rectificatoria
	DROP CONSTRAINT FK_Rel_Encomienda_Rectificatoria_Encomienda1
GO
ALTER TABLE dbo.wsEscribanos_ActaNotarial
	DROP CONSTRAINT FK_wsEscribanos_ActaNotarial_Encomienda
GO
ALTER TABLE dbo.Encomienda_DocumentosAdjuntos
	DROP CONSTRAINT FK_Encomienda_DocumentosAdjuntos_Encomienda
GO
ALTER TABLE dbo.Encomienda_Rectificatoria
	DROP CONSTRAINT FK_Encomienda_Rectificatoria_Encomienda
GO
ALTER TABLE dbo.Encomienda_Rectificatoria
	DROP CONSTRAINT FK_Encomienda_Rectificatoria_Encomienda1
GO
ALTER TABLE dbo.Encomienda_Ubicaciones
	DROP CONSTRAINT FK_Encomienda_Ubicaciones_Encomienda
GO
ALTER TABLE dbo.Encomienda_ZonasActualizadas
	DROP CONSTRAINT FK_Encomienda_ZonasActualizadas_Encomienda
GO
ALTER TABLE dbo.Encomienda_DatosLocal
	DROP CONSTRAINT FK_Encomienda_DatosLocal_Encomienda
GO
ALTER TABLE dbo.Encomienda_ConformacionLocal
	DROP CONSTRAINT FK_Encomienda_ConformacionLocal_Encomienda
GO
ALTER TABLE dbo.Encomienda_Rubros
	DROP CONSTRAINT FK_Encomienda_Rubros_Encomienda
GO
ALTER TABLE dbo.Encomienda_Titulares_PersonasFisicas
	DROP CONSTRAINT FK_Encomienda_Titulares_PersonasFisicas_Encomienda
GO
ALTER TABLE dbo.Encomienda_Titulares_PersonasJuridicas_PersonasFisicas
	DROP CONSTRAINT FK_Encomienda_Titulares_PersonasJuridicas_PersonasFisicas_Encomienda
GO
ALTER TABLE dbo.Encomienda_Normativas
	DROP CONSTRAINT FK_Encomienda_Normativas_Encomienda
GO
ALTER TABLE dbo.CAA_Rel_CAA_DocAdjuntos
	DROP CONSTRAINT FK_CAA_Rel_CAA_DocAdjuntos_Encomienda
GO
ALTER TABLE dbo.Encomienda_Sobrecargas
	DROP CONSTRAINT FK_Encomienda_Sobrecargas_Encomienda
GO
DROP TABLE dbo.Encomienda
GO
EXECUTE sp_rename N'dbo.Tmp_Encomienda', N'Encomienda', 'OBJECT' 
GO
ALTER TABLE dbo.Encomienda ADD CONSTRAINT
	PK_Encomienda PRIMARY KEY CLUSTERED 
	(
	id_encomienda
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Encomienda ADD CONSTRAINT
	FK_Encomienda_aspnet_Users FOREIGN KEY
	(
	CreateUser
	) REFERENCES dbo.aspnet_Users
	(
	UserId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Encomienda ADD CONSTRAINT
	FK_Encomienda_Encomienda_Estados FOREIGN KEY
	(
	id_estado
	) REFERENCES dbo.Encomienda_Estados
	(
	id_estado
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Encomienda ADD CONSTRAINT
	FK_Encomienda_Profesional FOREIGN KEY
	(
	id_profesional
	) REFERENCES dbo.Profesional
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Encomienda ADD CONSTRAINT
	FK_Encomienda_SubtipoExpediente FOREIGN KEY
	(
	id_subtipoexpediente
	) REFERENCES dbo.SubtipoExpediente
	(
	id_subtipoexpediente
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Encomienda ADD CONSTRAINT
	FK_Encomienda_TipoExpediente FOREIGN KEY
	(
	id_tipoexpediente
	) REFERENCES dbo.TipoExpediente
	(
	id_tipoexpediente
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Encomienda ADD CONSTRAINT
	FK_Encomienda_TipoTramite FOREIGN KEY
	(
	id_tipotramite
	) REFERENCES dbo.TipoTramite
	(
	id_tipotramite
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Encomienda ADD CONSTRAINT
	FK_Encomienda_SSIT_Solicitudes FOREIGN KEY
	(
	id_solicitud
	) REFERENCES dbo.SSIT_Solicitudes
	(
	id_solicitud
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
------------------------------------------------------------------
-- Agrega trigger a la tabla de Encomienda
------------------------------------------------------------------
CREATE TRIGGER [dbo].[tr_Encomienda_historial_estados]
   ON  dbo.Encomienda
   AFTER UPDATE
AS 
BEGIN
	SET NOCOUNT ON;


	DECLARE @cod_estado_ant	nvarchar(20)
	DECLARE @cod_estado_nuevo nvarchar(20)
	DECLARE @usuario uniqueidentifier
	DECLARE @id_encomienda int
		
	
	IF UPDATE(id_estado)
	BEGIN
	
		
		DECLARE cur CURSOR FAST_FORWARD FOR
		SELECT 
			i.id_encomienda,
			i.LastUpdateUser,
			tipest_i.cod_estado,
			tipest_d.cod_estado
		FROM 
			inserted i 
			INNER JOIN deleted d ON i.id_encomienda = d.id_encomienda
			INNER JOIN Encomienda_Estados tipest_i ON tipest_i.id_estado = i.id_estado
			INNER JOIN Encomienda_Estados tipest_d ON tipest_d.id_estado = d.id_estado
		
		
		OPEN cur
		FETCH NEXT FROM cur INTO @id_encomienda, @usuario, @cod_estado_nuevo, @cod_estado_ant
		
		WHILE @@FETCH_STATUS = 0
		BEGIN

			IF @cod_estado_ant <> @cod_estado_nuevo 
			BEGIN
				INSERT INTO Encomienda_HistorialEstados
				(
					id_encomienda,
					cod_estado_ant,
					cod_estado_nuevo,
					fecha_modificacion,
					usuario_modificacion
				)
				VALUES
				(
					@id_encomienda,
					@cod_estado_ant,
					@cod_estado_nuevo,
					GETDATE(),
					@usuario
				)
			END
		
			FETCH NEXT FROM cur INTO @id_encomienda, @usuario, @cod_estado_nuevo, @cod_estado_ant
		
		END
		CLOSE cur
		DEALLOCATE cur
		
	END  

END
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Encomienda_Sobrecargas
	DROP CONSTRAINT DF_Encomienda_Sobrecargas_CreateDate
GO
CREATE TABLE dbo.Tmp_Encomienda_Sobrecargas
	(
	id_sobrecarga int NOT NULL IDENTITY (1, 1),
	id_encomienda int NOT NULL,
	estructura_sobrecarga nvarchar(50) NOT NULL,
	peso_sobrecarga int NOT NULL,
	CreateDate datetime NOT NULL,
	CreateUser uniqueidentifier NOT NULL,
	LastUpdateDate datetime NULL,
	LastUpdateUser uniqueidentifier NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Encomienda_Sobrecargas SET (LOCK_ESCALATION = TABLE)
GO
ALTER TABLE dbo.Tmp_Encomienda_Sobrecargas ADD CONSTRAINT
	DF_Encomienda_Sobrecargas_CreateDate DEFAULT (getdate()) FOR CreateDate
GO
SET IDENTITY_INSERT dbo.Tmp_Encomienda_Sobrecargas ON
GO
IF EXISTS(SELECT * FROM dbo.Encomienda_Sobrecargas)
	 EXEC('INSERT INTO dbo.Tmp_Encomienda_Sobrecargas (id_sobrecarga, id_encomienda, estructura_sobrecarga, peso_sobrecarga, CreateDate, CreateUser, LastUpdateDate, LastUpdateUser)
		SELECT id_sobrecarga, id_encomienda, estructura_sobrecarga, peso_sobrecarga, CreateDate, CreateUser, LastUpdateDate, LastUpdateUser FROM dbo.Encomienda_Sobrecargas WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Encomienda_Sobrecargas OFF
GO
DROP TABLE dbo.Encomienda_Sobrecargas
GO
EXECUTE sp_rename N'dbo.Tmp_Encomienda_Sobrecargas', N'Encomienda_Sobrecargas', 'OBJECT' 
GO
ALTER TABLE dbo.Encomienda_Sobrecargas ADD CONSTRAINT
	PK_Encomienda_Sobrecargas PRIMARY KEY CLUSTERED 
	(
	id_sobrecarga
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Encomienda_Sobrecargas ADD CONSTRAINT
	FK_Encomienda_Sobrecargas_Encomienda FOREIGN KEY
	(
	id_encomienda
	) REFERENCES dbo.Encomienda
	(
	id_encomienda
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.CAA_Rel_CAA_DocAdjuntos ADD CONSTRAINT
	FK_CAA_Rel_CAA_DocAdjuntos_Encomienda FOREIGN KEY
	(
	id_encomienda
	) REFERENCES dbo.Encomienda
	(
	id_encomienda
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CAA_Rel_CAA_DocAdjuntos SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Encomienda_Normativas
	DROP CONSTRAINT DF_Encomienda_Normativas_CreateDate
GO
CREATE TABLE dbo.Tmp_Encomienda_Normativas
	(
	id_encomiendatiponormativa int NOT NULL IDENTITY (1, 1),
	id_encomienda int NOT NULL,
	id_tiponormativa int NOT NULL,
	id_entidadnormativa int NOT NULL,
	nro_normativa nvarchar(15) NOT NULL,
	CreateUser uniqueidentifier NOT NULL,
	CreateDate datetime NOT NULL,
	LastUpdateUser uniqueidentifier NULL,
	LastUpdateDate datetime NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Encomienda_Normativas SET (LOCK_ESCALATION = TABLE)
GO
ALTER TABLE dbo.Tmp_Encomienda_Normativas ADD CONSTRAINT
	DF_Encomienda_Normativas_CreateDate DEFAULT (getdate()) FOR CreateDate
GO
SET IDENTITY_INSERT dbo.Tmp_Encomienda_Normativas ON
GO
IF EXISTS(SELECT * FROM dbo.Encomienda_Normativas)
	 EXEC('INSERT INTO dbo.Tmp_Encomienda_Normativas (id_encomiendatiponormativa, id_encomienda, id_tiponormativa, id_entidadnormativa, nro_normativa, CreateUser, CreateDate, LastUpdateUser, LastUpdateDate)
		SELECT id_encomiendatiponormativa, id_encomienda, id_tiponormativa, id_entidadnormativa, nro_normativa, CreateUser, CreateDate, LastUpdateUser, LastUpdateDate FROM dbo.Encomienda_Normativas WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Encomienda_Normativas OFF
GO
DROP TABLE dbo.Encomienda_Normativas
GO
EXECUTE sp_rename N'dbo.Tmp_Encomienda_Normativas', N'Encomienda_Normativas', 'OBJECT' 
GO
ALTER TABLE dbo.Encomienda_Normativas ADD CONSTRAINT
	PK_Encomienda_Normativas PRIMARY KEY CLUSTERED 
	(
	id_encomiendatiponormativa
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Encomienda_Normativas ADD CONSTRAINT
	FK_Encomienda_Normativas_TipoNormativa FOREIGN KEY
	(
	id_tiponormativa
	) REFERENCES dbo.TipoNormativa
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Encomienda_Normativas ADD CONSTRAINT
	FK_Encomienda_Normativas_EntidadNormativa FOREIGN KEY
	(
	id_entidadnormativa
	) REFERENCES dbo.EntidadNormativa
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Encomienda_Normativas ADD CONSTRAINT
	FK_Encomienda_Normativas_Encomienda FOREIGN KEY
	(
	id_encomienda
	) REFERENCES dbo.Encomienda
	(
	id_encomienda
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Encomienda_Titulares_PersonasFisicas
	(
	id_personafisica int NOT NULL IDENTITY (1, 1),
	id_encomienda int NOT NULL,
	Apellido varchar(50) NOT NULL,
	Nombres nvarchar(50) NOT NULL,
	id_tipodoc_personal int NOT NULL,
	Nro_Documento nvarchar(15) NULL,
	Cuit nvarchar(13) NULL,
	id_tipoiibb int NOT NULL,
	Ingresos_Brutos nvarchar(25) NULL,
	Calle nvarchar(70) NOT NULL,
	Nro_Puerta int NOT NULL,
	Piso varchar(2) NULL,
	Depto varchar(10) NULL,
	Id_Localidad int NOT NULL,
	Codigo_Postal nvarchar(10) NULL,
	TelefonoMovil nvarchar(20) NULL,
	Sms nvarchar(50) NULL,
	Email nvarchar(70) NULL,
	MismoFirmante bit NOT NULL,
	CreateUser uniqueidentifier NOT NULL,
	CreateDate datetime NOT NULL,
	LastUpdateUser uniqueidentifier NULL,
	LastupdateDate datetime NULL,
	Telefono nchar(50) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Encomienda_Titulares_PersonasFisicas SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_Encomienda_Titulares_PersonasFisicas ON
GO
IF EXISTS(SELECT * FROM dbo.Encomienda_Titulares_PersonasFisicas)
	 EXEC('INSERT INTO dbo.Tmp_Encomienda_Titulares_PersonasFisicas (id_personafisica, id_encomienda, Apellido, Nombres, id_tipodoc_personal, Nro_Documento, Cuit, id_tipoiibb, Ingresos_Brutos, Calle, Nro_Puerta, Piso, Depto, Id_Localidad, Codigo_Postal, TelefonoMovil, Sms, Email, MismoFirmante, CreateUser, CreateDate, LastUpdateUser, LastupdateDate, Telefono)
		SELECT id_personafisica, id_encomienda, Apellido, Nombres, id_tipodoc_personal, Nro_Documento, Cuit, id_tipoiibb, Ingresos_Brutos, Calle, Nro_Puerta, Piso, Depto, Id_Localidad, Codigo_Postal, TelefonoMovil, Sms, Email, MismoFirmante, CreateUser, CreateDate, LastUpdateUser, LastupdateDate, Telefono FROM dbo.Encomienda_Titulares_PersonasFisicas WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Encomienda_Titulares_PersonasFisicas OFF
GO
ALTER TABLE dbo.Encomienda_Firmantes_PersonasFisicas
	DROP CONSTRAINT FK_Encomienda_Firmantes_PersonasFisicas_Encomienda_Titulares_PersonasFisicas
GO
DROP TABLE dbo.Encomienda_Titulares_PersonasFisicas
GO
EXECUTE sp_rename N'dbo.Tmp_Encomienda_Titulares_PersonasFisicas', N'Encomienda_Titulares_PersonasFisicas', 'OBJECT' 
GO
ALTER TABLE dbo.Encomienda_Titulares_PersonasFisicas ADD CONSTRAINT
	PK_Encomienda_Titulares_PersonasFisicas PRIMARY KEY CLUSTERED 
	(
	id_personafisica
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
CREATE NONCLUSTERED INDEX IX_Encomienda_Titulares_PersonasFisicas_id_enc ON dbo.Encomienda_Titulares_PersonasFisicas
	(
	id_encomienda
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE dbo.Encomienda_Titulares_PersonasFisicas ADD CONSTRAINT
	FK_Encomienda_Titulares_PersonasFisicas_Encomienda FOREIGN KEY
	(
	id_encomienda
	) REFERENCES dbo.Encomienda
	(
	id_encomienda
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Encomienda_Titulares_PersonasFisicas ADD CONSTRAINT
	FK_Encomienda_Titulares_PersonasFisicas_Localidad FOREIGN KEY
	(
	Id_Localidad
	) REFERENCES dbo.Localidad
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Encomienda_Titulares_PersonasFisicas ADD CONSTRAINT
	FK_Encomienda_Titulares_PersonasFisicas_TipoDocumentoPersonal FOREIGN KEY
	(
	id_tipodoc_personal
	) REFERENCES dbo.TipoDocumentoPersonal
	(
	TipoDocumentoPersonalId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Encomienda_Titulares_PersonasFisicas ADD CONSTRAINT
	FK_Encomienda_Titulares_PersonasFisicas_TiposDeIngresosBrutos FOREIGN KEY
	(
	id_tipoiibb
	) REFERENCES dbo.TiposDeIngresosBrutos
	(
	id_tipoiibb
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Encomienda_Rubros
	DROP CONSTRAINT DF_Encomienda_Rubros_CreateDate
GO
CREATE TABLE dbo.Tmp_Encomienda_Rubros
	(
	id_encomiendarubro int NOT NULL IDENTITY (1, 1),
	id_encomienda int NOT NULL,
	cod_rubro varchar(50) NOT NULL,
	desc_rubro varchar(200) NULL,
	EsAnterior bit NOT NULL,
	id_tipoactividad int NOT NULL,
	id_tipodocreq int NOT NULL,
	SuperficieHabilitar decimal(10, 2) NOT NULL,
	id_ImpactoAmbiental int NULL,
	CreateDate datetime NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Encomienda_Rubros SET (LOCK_ESCALATION = TABLE)
GO
ALTER TABLE dbo.Tmp_Encomienda_Rubros ADD CONSTRAINT
	DF_Encomienda_Rubros_CreateDate DEFAULT (getdate()) FOR CreateDate
GO
SET IDENTITY_INSERT dbo.Tmp_Encomienda_Rubros ON
GO
IF EXISTS(SELECT * FROM dbo.Encomienda_Rubros)
	 EXEC('INSERT INTO dbo.Tmp_Encomienda_Rubros (id_encomiendarubro, id_encomienda, cod_rubro, desc_rubro, EsAnterior, id_tipoactividad, id_tipodocreq, SuperficieHabilitar, id_ImpactoAmbiental, CreateDate)
		SELECT id_encomiendarubro, id_encomienda, cod_rubro, desc_rubro, EsAnterior, id_tipoactividad, id_tipodocreq, SuperficieHabilitar, id_ImpactoAmbiental, CreateDate FROM dbo.Encomienda_Rubros WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Encomienda_Rubros OFF
GO
DROP TABLE dbo.Encomienda_Rubros
GO
EXECUTE sp_rename N'dbo.Tmp_Encomienda_Rubros', N'Encomienda_Rubros', 'OBJECT' 
GO
ALTER TABLE dbo.Encomienda_Rubros ADD CONSTRAINT
	PK_Encomienda_Rubros PRIMARY KEY CLUSTERED 
	(
	id_encomiendarubro
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
CREATE NONCLUSTERED INDEX IX_Encomienda_Rubros_id_enc ON dbo.Encomienda_Rubros
	(
	id_encomienda
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE dbo.Encomienda_Rubros ADD CONSTRAINT
	FK_Encomienda_Rubros_Encomienda FOREIGN KEY
	(
	id_encomienda
	) REFERENCES dbo.Encomienda
	(
	id_encomienda
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Encomienda_Rubros ADD CONSTRAINT
	FK_Encomienda_Rubros_ImpactoAmbiental FOREIGN KEY
	(
	id_ImpactoAmbiental
	) REFERENCES dbo.ImpactoAmbiental
	(
	id_ImpactoAmbiental
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Encomienda_Rubros ADD CONSTRAINT
	FK_Encomienda_Rubros_Tipo_Documentacion_Req FOREIGN KEY
	(
	id_tipodocreq
	) REFERENCES dbo.Tipo_Documentacion_Req
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Encomienda_Rubros ADD CONSTRAINT
	FK_Encomienda_Rubros_TipoActividad FOREIGN KEY
	(
	id_tipoactividad
	) REFERENCES dbo.TipoActividad
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Encomienda_DatosLocal
	DROP CONSTRAINT DF_Encomienda_DatosLocal_CreateDate
GO
CREATE TABLE dbo.Tmp_Encomienda_DatosLocal
	(
	id_encomiendadatoslocal int NOT NULL IDENTITY (1, 1),
	id_encomienda int NOT NULL,
	superficie_cubierta_dl decimal(8, 2) NULL,
	superficie_descubierta_dl decimal(8, 2) NULL,
	dimesion_frente_dl decimal(8, 2) NULL,
	lugar_carga_descarga_dl bit NOT NULL,
	estacionamiento_dl bit NOT NULL,
	red_transito_pesado_dl bit NOT NULL,
	sobre_avenida_dl bit NOT NULL,
	materiales_pisos_dl nvarchar(500) NULL,
	materiales_paredes_dl nvarchar(500) NULL,
	materiales_techos_dl nvarchar(500) NULL,
	materiales_revestimientos_dl nvarchar(500) NULL,
	sanitarios_ubicacion_dl int NOT NULL,
	sanitarios_distancia_dl decimal(8, 2) NULL,
	croquis_ubicacion_dl nvarchar(300) NULL,
	cantidad_sanitarios_dl int NULL,
	superficie_sanitarios_dl decimal(8, 2) NULL,
	frente_dl decimal(8, 2) NULL,
	fondo_dl decimal(8, 2) NULL,
	lateral_izquierdo_dl decimal(8, 2) NULL,
	lateral_derecho_dl decimal(8, 2) NULL,
	sobrecarga_corresponde_dl bit NOT NULL,
	sobrecarga_tipo_observacion int NULL,
	sobrecarga_requisitos_opcion int NULL,
	sobrecarga_art813_inciso nvarchar(10) NULL,
	sobrecarga_art813_item nvarchar(10) NULL,
	cantidad_operarios_dl int NULL,
	CreateDate datetime NOT NULL,
	CreateUser uniqueidentifier NOT NULL,
	LastUpdateDate datetime NULL,
	LastUpdateUser uniqueidentifier NULL,
	local_venta float(53) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Encomienda_DatosLocal SET (LOCK_ESCALATION = TABLE)
GO
ALTER TABLE dbo.Tmp_Encomienda_DatosLocal ADD CONSTRAINT
	DF_Encomienda_DatosLocal_CreateDate DEFAULT (getdate()) FOR CreateDate
GO
SET IDENTITY_INSERT dbo.Tmp_Encomienda_DatosLocal ON
GO
IF EXISTS(SELECT * FROM dbo.Encomienda_DatosLocal)
	 EXEC('INSERT INTO dbo.Tmp_Encomienda_DatosLocal (id_encomiendadatoslocal, id_encomienda, superficie_cubierta_dl, superficie_descubierta_dl, dimesion_frente_dl, lugar_carga_descarga_dl, estacionamiento_dl, red_transito_pesado_dl, sobre_avenida_dl, materiales_pisos_dl, materiales_paredes_dl, materiales_techos_dl, materiales_revestimientos_dl, sanitarios_ubicacion_dl, sanitarios_distancia_dl, croquis_ubicacion_dl, cantidad_sanitarios_dl, superficie_sanitarios_dl, frente_dl, fondo_dl, lateral_izquierdo_dl, lateral_derecho_dl, sobrecarga_corresponde_dl, sobrecarga_tipo_observacion, sobrecarga_requisitos_opcion, sobrecarga_art813_inciso, sobrecarga_art813_item, cantidad_operarios_dl, CreateDate, CreateUser, LastUpdateDate, LastUpdateUser, local_venta)
		SELECT id_encomiendadatoslocal, id_encomienda, superficie_cubierta_dl, superficie_descubierta_dl, dimesion_frente_dl, lugar_carga_descarga_dl, estacionamiento_dl, red_transito_pesado_dl, sobre_avenida_dl, materiales_pisos_dl, materiales_paredes_dl, materiales_techos_dl, materiales_revestimientos_dl, sanitarios_ubicacion_dl, sanitarios_distancia_dl, croquis_ubicacion_dl, cantidad_sanitarios_dl, superficie_sanitarios_dl, frente_dl, fondo_dl, lateral_izquierdo_dl, lateral_derecho_dl, sobrecarga_corresponde_dl, sobrecarga_tipo_observacion, sobrecarga_requisitos_opcion, sobrecarga_art813_inciso, sobrecarga_art813_item, cantidad_operarios_dl, CreateDate, CreateUser, LastUpdateDate, LastUpdateUser, local_venta FROM dbo.Encomienda_DatosLocal WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Encomienda_DatosLocal OFF
GO
ALTER TABLE dbo.Encomienda_Certificado_Sobrecarga
	DROP CONSTRAINT FK_Encomienda_Certificado_Sobrecarga_Encomienda_Certificado_Sobrecarga
GO
DROP TABLE dbo.Encomienda_DatosLocal
GO
EXECUTE sp_rename N'dbo.Tmp_Encomienda_DatosLocal', N'Encomienda_DatosLocal', 'OBJECT' 
GO
ALTER TABLE dbo.Encomienda_DatosLocal ADD CONSTRAINT
	PK_Encomienda_DatosLocal PRIMARY KEY CLUSTERED 
	(
	id_encomiendadatoslocal
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
CREATE UNIQUE NONCLUSTERED INDEX IX_Encomienda_DatosLocal ON dbo.Encomienda_DatosLocal
	(
	id_encomienda
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE dbo.Encomienda_DatosLocal ADD CONSTRAINT
	FK_Encomienda_DatosLocal_Encomienda FOREIGN KEY
	(
	id_encomienda
	) REFERENCES dbo.Encomienda
	(
	id_encomienda
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Encomienda_Certificado_Sobrecarga
	DROP CONSTRAINT DF_Encomienda_Certificado_Sobrecarga_CreateDate
GO
CREATE TABLE dbo.Tmp_Encomienda_Certificado_Sobrecarga
	(
	id_sobrecarga int NOT NULL IDENTITY (1, 1),
	id_encomienda_datoslocal int NOT NULL,
	id_tipo_certificado int NOT NULL,
	id_tipo_sobrecarga int NOT NULL,
	CreateDate datetime NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Encomienda_Certificado_Sobrecarga SET (LOCK_ESCALATION = TABLE)
GO
ALTER TABLE dbo.Tmp_Encomienda_Certificado_Sobrecarga ADD CONSTRAINT
	DF_Encomienda_Certificado_Sobrecarga_CreateDate DEFAULT (getdate()) FOR CreateDate
GO
SET IDENTITY_INSERT dbo.Tmp_Encomienda_Certificado_Sobrecarga ON
GO
IF EXISTS(SELECT * FROM dbo.Encomienda_Certificado_Sobrecarga)
	 EXEC('INSERT INTO dbo.Tmp_Encomienda_Certificado_Sobrecarga (id_sobrecarga, id_encomienda_datoslocal, id_tipo_certificado, id_tipo_sobrecarga, CreateDate)
		SELECT id_sobrecarga, id_encomienda_datoslocal, id_tipo_certificado, id_tipo_sobrecarga, CreateDate FROM dbo.Encomienda_Certificado_Sobrecarga WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Encomienda_Certificado_Sobrecarga OFF
GO
ALTER TABLE dbo.Encomienda_Sobrecarga_Detalle1
	DROP CONSTRAINT FK_Encomienda_Sobrecarga_Detalle1_Encomienda_Certificado_Sobrecarga
GO
DROP TABLE dbo.Encomienda_Certificado_Sobrecarga
GO
EXECUTE sp_rename N'dbo.Tmp_Encomienda_Certificado_Sobrecarga', N'Encomienda_Certificado_Sobrecarga', 'OBJECT' 
GO
ALTER TABLE dbo.Encomienda_Certificado_Sobrecarga ADD CONSTRAINT
	PK_Encomienda_Certificado_Sobrecarga PRIMARY KEY CLUSTERED 
	(
	id_sobrecarga
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Encomienda_Certificado_Sobrecarga ADD CONSTRAINT
	FK_Encomienda_Certificado_Sobrecarga_Encomienda_Certificado_Sobrecarga FOREIGN KEY
	(
	id_encomienda_datoslocal
	) REFERENCES dbo.Encomienda_DatosLocal
	(
	id_encomiendadatoslocal
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Encomienda_Certificado_Sobrecarga ADD CONSTRAINT
	FK_Encomienda_Certificado_Sobrecarga_Encomienda_Tipos_Certificados_Sobrecarga FOREIGN KEY
	(
	id_tipo_certificado
	) REFERENCES dbo.Encomienda_Tipos_Certificados_Sobrecarga
	(
	id_tipo_certificado
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Encomienda_Certificado_Sobrecarga ADD CONSTRAINT
	FK_Encomienda_Certificado_Sobrecarga_Encomienda_Tipos_Sobrecargas FOREIGN KEY
	(
	id_tipo_sobrecarga
	) REFERENCES dbo.Encomienda_Tipos_Sobrecargas
	(
	id_tipo_sobrecarga
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Encomienda_ZonasActualizadas ADD CONSTRAINT
	FK_Encomienda_ZonasActualizadas_Encomienda FOREIGN KEY
	(
	id_encomienda
	) REFERENCES dbo.Encomienda
	(
	id_encomienda
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Encomienda_ZonasActualizadas SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Encomienda_Ubicaciones
	(
	id_encomiendaubicacion int NOT NULL IDENTITY (1, 1),
	id_encomienda int NULL,
	id_ubicacion int NULL,
	id_subtipoubicacion int NULL,
	local_subtipoubicacion nvarchar(25) NULL,
	deptoLocal_encomiendaubicacion nvarchar(50) NULL,
	CreateDate datetime NOT NULL,
	CreateUser uniqueidentifier NOT NULL,
	id_zonaplaneamiento int NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Encomienda_Ubicaciones SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_Encomienda_Ubicaciones ON
GO
IF EXISTS(SELECT * FROM dbo.Encomienda_Ubicaciones)
	 EXEC('INSERT INTO dbo.Tmp_Encomienda_Ubicaciones (id_encomiendaubicacion, id_encomienda, id_ubicacion, id_subtipoubicacion, local_subtipoubicacion, deptoLocal_encomiendaubicacion, CreateDate, CreateUser, id_zonaplaneamiento)
		SELECT id_encomiendaubicacion, id_encomienda, id_ubicacion, id_subtipoubicacion, local_subtipoubicacion, deptoLocal_encomiendaubicacion, CreateDate, CreateUser, id_zonaplaneamiento FROM dbo.Encomienda_Ubicaciones WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Encomienda_Ubicaciones OFF
GO
ALTER TABLE dbo.Encomienda_Ubicaciones_PropiedadHorizontal
	DROP CONSTRAINT FK_Encomienda_Ubicaciones_PropiedadHorizontal_Encomienda_Ubicaciones
GO
ALTER TABLE dbo.Encomienda_Ubicaciones_Puertas
	DROP CONSTRAINT FK_Encomienda_Ubicaciones_Puertas_Encomienda_Ubicaciones
GO
DROP TABLE dbo.Encomienda_Ubicaciones
GO
EXECUTE sp_rename N'dbo.Tmp_Encomienda_Ubicaciones', N'Encomienda_Ubicaciones', 'OBJECT' 
GO
ALTER TABLE dbo.Encomienda_Ubicaciones ADD CONSTRAINT
	PK_Encomienda_Ubicaciones PRIMARY KEY CLUSTERED 
	(
	id_encomiendaubicacion
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
CREATE NONCLUSTERED INDEX IX_Encomienda_Ubicaciones_id_enc ON dbo.Encomienda_Ubicaciones
	(
	id_encomienda
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE dbo.Encomienda_Ubicaciones ADD CONSTRAINT
	FK_Encomienda_Ubicaciones_Ubicaciones FOREIGN KEY
	(
	id_ubicacion
	) REFERENCES dbo.Ubicaciones
	(
	id_ubicacion
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Encomienda_Ubicaciones ADD CONSTRAINT
	FK_Encomienda_Ubicaciones_Encomienda FOREIGN KEY
	(
	id_encomienda
	) REFERENCES dbo.Encomienda
	(
	id_encomienda
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Encomienda_Ubicaciones ADD CONSTRAINT
	FK_Encomienda_Ubicaciones_SubTiposDeUbicacion FOREIGN KEY
	(
	id_subtipoubicacion
	) REFERENCES dbo.SubTiposDeUbicacion
	(
	id_subtipoubicacion
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Encomienda_Ubicaciones ADD CONSTRAINT
	FK_Encomienda_Ubicaciones_Zonas_Planeamiento FOREIGN KEY
	(
	id_zonaplaneamiento
	) REFERENCES dbo.Zonas_Planeamiento
	(
	id_zonaplaneamiento
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Encomienda_Ubicaciones_Puertas
	(
	id_encomiendapuerta int NOT NULL IDENTITY (1, 1),
	id_encomiendaubicacion int NOT NULL,
	codigo_calle int NOT NULL,
	nombre_calle nvarchar(100) NOT NULL,
	NroPuerta int NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Encomienda_Ubicaciones_Puertas SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_Encomienda_Ubicaciones_Puertas ON
GO
IF EXISTS(SELECT * FROM dbo.Encomienda_Ubicaciones_Puertas)
	 EXEC('INSERT INTO dbo.Tmp_Encomienda_Ubicaciones_Puertas (id_encomiendapuerta, id_encomiendaubicacion, codigo_calle, nombre_calle, NroPuerta)
		SELECT id_encomiendapuerta, id_encomiendaubicacion, codigo_calle, nombre_calle, NroPuerta FROM dbo.Encomienda_Ubicaciones_Puertas WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Encomienda_Ubicaciones_Puertas OFF
GO
DROP TABLE dbo.Encomienda_Ubicaciones_Puertas
GO
EXECUTE sp_rename N'dbo.Tmp_Encomienda_Ubicaciones_Puertas', N'Encomienda_Ubicaciones_Puertas', 'OBJECT' 
GO
ALTER TABLE dbo.Encomienda_Ubicaciones_Puertas ADD CONSTRAINT
	PK_Encomienda_Ubicaciones_Puertas PRIMARY KEY CLUSTERED 
	(
	id_encomiendapuerta
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
CREATE NONCLUSTERED INDEX IX_Encomienda_Ubicaciones_puertas_id_enc_ubi ON dbo.Encomienda_Ubicaciones_Puertas
	(
	id_encomiendaubicacion
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE dbo.Encomienda_Ubicaciones_Puertas ADD CONSTRAINT
	FK_Encomienda_Ubicaciones_Puertas_Encomienda_Ubicaciones FOREIGN KEY
	(
	id_encomiendaubicacion
	) REFERENCES dbo.Encomienda_Ubicaciones
	(
	id_encomiendaubicacion
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Encomienda_Ubicaciones_PropiedadHorizontal
	(
	id_encomiendaprophorizontal int NOT NULL IDENTITY (1, 1),
	id_encomiendaubicacion int NULL,
	id_propiedadhorizontal int NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Encomienda_Ubicaciones_PropiedadHorizontal SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_Encomienda_Ubicaciones_PropiedadHorizontal ON
GO
IF EXISTS(SELECT * FROM dbo.Encomienda_Ubicaciones_PropiedadHorizontal)
	 EXEC('INSERT INTO dbo.Tmp_Encomienda_Ubicaciones_PropiedadHorizontal (id_encomiendaprophorizontal, id_encomiendaubicacion, id_propiedadhorizontal)
		SELECT id_encomiendaprophorizontal, id_encomiendaubicacion, id_propiedadhorizontal FROM dbo.Encomienda_Ubicaciones_PropiedadHorizontal WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Encomienda_Ubicaciones_PropiedadHorizontal OFF
GO
DROP TABLE dbo.Encomienda_Ubicaciones_PropiedadHorizontal
GO
EXECUTE sp_rename N'dbo.Tmp_Encomienda_Ubicaciones_PropiedadHorizontal', N'Encomienda_Ubicaciones_PropiedadHorizontal', 'OBJECT' 
GO
ALTER TABLE dbo.Encomienda_Ubicaciones_PropiedadHorizontal ADD CONSTRAINT
	PK_Encomienda_Ubicaciones_PropiedadHorizontal PRIMARY KEY CLUSTERED 
	(
	id_encomiendaprophorizontal
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
CREATE NONCLUSTERED INDEX IX_Encomienda_Ubicaciones_ph_id_enc_ubi ON dbo.Encomienda_Ubicaciones_PropiedadHorizontal
	(
	id_encomiendaubicacion
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE dbo.Encomienda_Ubicaciones_PropiedadHorizontal ADD CONSTRAINT
	FK_Encomienda_Ubicaciones_PropiedadHorizontal_Encomienda_Ubicaciones FOREIGN KEY
	(
	id_encomiendaubicacion
	) REFERENCES dbo.Encomienda_Ubicaciones
	(
	id_encomiendaubicacion
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Encomienda_Ubicaciones_PropiedadHorizontal ADD CONSTRAINT
	FK_Encomienda_Ubicaciones_PropiedadHorizontal_Ubicaciones_PropiedadHorizontal FOREIGN KEY
	(
	id_propiedadhorizontal
	) REFERENCES dbo.Ubicaciones_PropiedadHorizontal
	(
	id_propiedadhorizontal
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Encomienda_Rectificatoria
	(
	id_encrec int NOT NULL IDENTITY (1, 1),
	id_encomienda_anterior int NOT NULL,
	id_encomienda_nueva int NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Encomienda_Rectificatoria SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_Encomienda_Rectificatoria ON
GO
IF EXISTS(SELECT * FROM dbo.Encomienda_Rectificatoria)
	 EXEC('INSERT INTO dbo.Tmp_Encomienda_Rectificatoria (id_encrec, id_encomienda_anterior, id_encomienda_nueva)
		SELECT id_encrec, id_encomienda_anterior, id_encomienda_nueva FROM dbo.Encomienda_Rectificatoria WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Encomienda_Rectificatoria OFF
GO
DROP TABLE dbo.Encomienda_Rectificatoria
GO
EXECUTE sp_rename N'dbo.Tmp_Encomienda_Rectificatoria', N'Encomienda_Rectificatoria', 'OBJECT' 
GO
ALTER TABLE dbo.Encomienda_Rectificatoria ADD CONSTRAINT
	PK_Encomienda_Rectificatoria PRIMARY KEY CLUSTERED 
	(
	id_encrec
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Encomienda_Rectificatoria ADD CONSTRAINT
	FK_Encomienda_Rectificatoria_Encomienda FOREIGN KEY
	(
	id_encomienda_anterior
	) REFERENCES dbo.Encomienda
	(
	id_encomienda
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Encomienda_Rectificatoria ADD CONSTRAINT
	FK_Encomienda_Rectificatoria_Encomienda1 FOREIGN KEY
	(
	id_encomienda_nueva
	) REFERENCES dbo.Encomienda
	(
	id_encomienda
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Encomienda_DocumentosAdjuntos
	(
	id_docadjunto int NOT NULL IDENTITY (1, 1),
	id_encomienda int NOT NULL,
	id_tdocreq int NOT NULL,
	tdocreq_detalle nvarchar(50) NULL,
	id_tipodocsis int NOT NULL,
	id_file int NOT NULL,
	generadoxSistema bit NOT NULL,
	CreateDate datetime NOT NULL,
	CreateUser uniqueidentifier NOT NULL,
	UpdateDate datetime NULL,
	UpdateUser uniqueidentifier NULL,
	nombre_archivo nvarchar(50) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Encomienda_DocumentosAdjuntos SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_Encomienda_DocumentosAdjuntos ON
GO
IF EXISTS(SELECT * FROM dbo.Encomienda_DocumentosAdjuntos)
	 EXEC('INSERT INTO dbo.Tmp_Encomienda_DocumentosAdjuntos (id_docadjunto, id_encomienda, id_tdocreq, tdocreq_detalle, id_tipodocsis, id_file, generadoxSistema, CreateDate, CreateUser, UpdateDate, UpdateUser, nombre_archivo)
		SELECT id_docadjunto, id_encomienda, id_tdocreq, tdocreq_detalle, id_tipodocsis, id_file, generadoxSistema, CreateDate, CreateUser, UpdateDate, UpdateUser, nombre_archivo FROM dbo.Encomienda_DocumentosAdjuntos WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Encomienda_DocumentosAdjuntos OFF
GO
DROP TABLE dbo.Encomienda_DocumentosAdjuntos
GO
EXECUTE sp_rename N'dbo.Tmp_Encomienda_DocumentosAdjuntos', N'Encomienda_DocumentosAdjuntos', 'OBJECT' 
GO
ALTER TABLE dbo.Encomienda_DocumentosAdjuntos ADD CONSTRAINT
	PK_Encomienda_DocumentosAdjuntos PRIMARY KEY CLUSTERED 
	(
	id_docadjunto
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Encomienda_DocumentosAdjuntos ADD CONSTRAINT
	FK_Encomienda_DocumentosAdjuntos_Encomienda FOREIGN KEY
	(
	id_encomienda
	) REFERENCES dbo.Encomienda
	(
	id_encomienda
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Encomienda_DocumentosAdjuntos ADD CONSTRAINT
	FK_Encomienda_DocumentosAdjuntos_TiposDeDocumentosRequeridos FOREIGN KEY
	(
	id_tdocreq
	) REFERENCES dbo.TiposDeDocumentosRequeridos
	(
	id_tdocreq
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Encomienda_DocumentosAdjuntos ADD CONSTRAINT
	FK_Encomienda_DocumentosAdjuntos_TiposDeDocumentosSistema FOREIGN KEY
	(
	id_tipodocsis
	) REFERENCES dbo.TiposDeDocumentosSistema
	(
	id_tipdocsis
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Encomienda_DocumentosAdjuntos ADD CONSTRAINT
	FK_Encomienda_DocumentosAdjuntos_Encomienda_DocumentosAdjuntos FOREIGN KEY
	(
	CreateUser
	) REFERENCES dbo.aspnet_Users
	(
	UserId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Encomienda_DocumentosAdjuntos ADD CONSTRAINT
	FK_Encomienda_DocumentosAdjuntos_aspnet_Users FOREIGN KEY
	(
	UpdateUser
	) REFERENCES dbo.aspnet_Users
	(
	UserId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.wsEscribanos_ActaNotarial ADD CONSTRAINT
	FK_wsEscribanos_ActaNotarial_Encomienda FOREIGN KEY
	(
	id_encomienda
	) REFERENCES dbo.Encomienda
	(
	id_encomienda
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.wsEscribanos_ActaNotarial SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Rel_Encomienda_Rectificatoria ADD CONSTRAINT
	FK_Rel_Encomienda_Rectificatoria_Encomienda FOREIGN KEY
	(
	id_encomienda_anterior
	) REFERENCES dbo.Encomienda
	(
	id_encomienda
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Rel_Encomienda_Rectificatoria ADD CONSTRAINT
	FK_Rel_Encomienda_Rectificatoria_Encomienda1 FOREIGN KEY
	(
	id_encomienda_nueva
	) REFERENCES dbo.Encomienda
	(
	id_encomienda
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Rel_Encomienda_Rectificatoria SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Encomienda_Planos
	(
	id_encomienda_plano int NOT NULL IDENTITY (1, 1),
	id_encomienda int NOT NULL,
	id_file int NOT NULL,
	id_tipo_plano int NOT NULL,
	detalle nvarchar(50) NULL,
	nombre_archivo nvarchar(100) NULL,
	CreateDate datetime NOT NULL,
	CreateUser nvarchar(50) NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Encomienda_Planos SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_Encomienda_Planos ON
GO
IF EXISTS(SELECT * FROM dbo.Encomienda_Planos)
	 EXEC('INSERT INTO dbo.Tmp_Encomienda_Planos (id_encomienda_plano, id_encomienda, id_file, id_tipo_plano, detalle, nombre_archivo, CreateDate, CreateUser)
		SELECT id_encomienda_plano, id_encomienda, id_file, id_tipo_plano, detalle, nombre_archivo, CreateDate, CreateUser FROM dbo.Encomienda_Planos WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Encomienda_Planos OFF
GO
DROP TABLE dbo.Encomienda_Planos
GO
EXECUTE sp_rename N'dbo.Tmp_Encomienda_Planos', N'Encomienda_Planos', 'OBJECT' 
GO
ALTER TABLE dbo.Encomienda_Planos ADD CONSTRAINT
	PK_Encomienda_Planos PRIMARY KEY NONCLUSTERED 
	(
	id_encomienda_plano
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Encomienda_Planos ADD CONSTRAINT
	FK_Encomienda_Planos_Encomienda FOREIGN KEY
	(
	id_encomienda
	) REFERENCES dbo.Encomienda
	(
	id_encomienda
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Encomienda_Planos ADD CONSTRAINT
	FK_Encomienda_Planos_Tipo_Plano FOREIGN KEY
	(
	id_tipo_plano
	) REFERENCES dbo.TiposDePlanos
	(
	id_tipo_plano
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Encomienda_Plantas
	(
	id_encomiendatiposector int NOT NULL IDENTITY (1, 1),
	id_encomienda int NOT NULL,
	id_tiposector int NOT NULL,
	detalle_encomiendatiposector nvarchar(50) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Encomienda_Plantas SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_Encomienda_Plantas ON
GO
IF EXISTS(SELECT * FROM dbo.Encomienda_Plantas)
	 EXEC('INSERT INTO dbo.Tmp_Encomienda_Plantas (id_encomiendatiposector, id_encomienda, id_tiposector, detalle_encomiendatiposector)
		SELECT id_encomiendatiposector, id_encomienda, id_tiposector, detalle_encomiendatiposector FROM dbo.Encomienda_Plantas WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Encomienda_Plantas OFF
GO
ALTER TABLE dbo.Encomienda_Sobrecarga_Detalle1
	DROP CONSTRAINT FK_Encomienda_Sobrecarga_Detalle1_Encomienda_Plantas
GO
ALTER TABLE dbo.Encomienda_ConformacionLocal
	DROP CONSTRAINT FK_Encomienda_ConformacionLocal_Encomienda_Plantas
GO
DROP TABLE dbo.Encomienda_Plantas
GO
EXECUTE sp_rename N'dbo.Tmp_Encomienda_Plantas', N'Encomienda_Plantas', 'OBJECT' 
GO
ALTER TABLE dbo.Encomienda_Plantas ADD CONSTRAINT
	PK_Encomienda_TiposDeSector PRIMARY KEY CLUSTERED 
	(
	id_encomiendatiposector
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
CREATE NONCLUSTERED INDEX IX_Encomienda_Plantas_id_enc ON dbo.Encomienda_Plantas
	(
	id_encomienda
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE dbo.Encomienda_Plantas ADD CONSTRAINT
	FK_Encomienda_Plantas_Encomienda FOREIGN KEY
	(
	id_encomienda
	) REFERENCES dbo.Encomienda
	(
	id_encomienda
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Encomienda_Plantas ADD CONSTRAINT
	FK_Encomienda_Plantas_TipoSector FOREIGN KEY
	(
	id_tiposector
	) REFERENCES dbo.TipoSector
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Encomienda_ConformacionLocal
	DROP CONSTRAINT DF_Encomienda_ConformacionLocal_CreateDate
GO
CREATE TABLE dbo.Tmp_Encomienda_ConformacionLocal
	(
	id_encomiendaconflocal int NOT NULL IDENTITY (1, 1),
	id_encomienda int NOT NULL,
	id_destino int NOT NULL,
	largo_conflocal decimal(10, 2) NULL,
	ancho_conflocal decimal(10, 2) NULL,
	alto_conflocal decimal(10, 2) NULL,
	Paredes_conflocal nvarchar(50) NULL,
	Techos_conflocal nvarchar(50) NULL,
	Pisos_conflocal nvarchar(50) NULL,
	Frisos_conflocal nvarchar(50) NULL,
	Observaciones_conflocal nvarchar(4000) NULL,
	CreateDate datetime NOT NULL,
	CreateUser uniqueidentifier NOT NULL,
	UpdateDate datetime NULL,
	Updateuser uniqueidentifier NULL,
	Detalle_conflocal nvarchar(200) NULL,
	id_encomiendatiposector int NULL,
	id_ventilacion int NULL,
	id_iluminacion int NULL,
	superficie_conflocal decimal(10, 2) NULL,
	id_tiposuperficie int NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Encomienda_ConformacionLocal SET (LOCK_ESCALATION = TABLE)
GO
ALTER TABLE dbo.Tmp_Encomienda_ConformacionLocal ADD CONSTRAINT
	DF_Encomienda_ConformacionLocal_CreateDate DEFAULT (getdate()) FOR CreateDate
GO
SET IDENTITY_INSERT dbo.Tmp_Encomienda_ConformacionLocal ON
GO
IF EXISTS(SELECT * FROM dbo.Encomienda_ConformacionLocal)
	 EXEC('INSERT INTO dbo.Tmp_Encomienda_ConformacionLocal (id_encomiendaconflocal, id_encomienda, id_destino, largo_conflocal, ancho_conflocal, alto_conflocal, Paredes_conflocal, Techos_conflocal, Pisos_conflocal, Frisos_conflocal, Observaciones_conflocal, CreateDate, CreateUser, UpdateDate, Updateuser, Detalle_conflocal, id_encomiendatiposector, id_ventilacion, id_iluminacion, superficie_conflocal, id_tiposuperficie)
		SELECT id_encomiendaconflocal, id_encomienda, id_destino, largo_conflocal, ancho_conflocal, alto_conflocal, Paredes_conflocal, Techos_conflocal, Pisos_conflocal, Frisos_conflocal, Observaciones_conflocal, CreateDate, CreateUser, UpdateDate, Updateuser, Detalle_conflocal, id_encomiendatiposector, id_ventilacion, id_iluminacion, superficie_conflocal, id_tiposuperficie FROM dbo.Encomienda_ConformacionLocal WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Encomienda_ConformacionLocal OFF
GO
DROP TABLE dbo.Encomienda_ConformacionLocal
GO
EXECUTE sp_rename N'dbo.Tmp_Encomienda_ConformacionLocal', N'Encomienda_ConformacionLocal', 'OBJECT' 
GO
ALTER TABLE dbo.Encomienda_ConformacionLocal ADD CONSTRAINT
	PK_Encomienda_ConformacionLocal PRIMARY KEY CLUSTERED 
	(
	id_encomiendaconflocal
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Encomienda_ConformacionLocal ADD CONSTRAINT
	FK_Encomienda_ConformacionLocal_Encomienda_Plantas FOREIGN KEY
	(
	id_encomiendatiposector
	) REFERENCES dbo.Encomienda_Plantas
	(
	id_encomiendatiposector
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Encomienda_ConformacionLocal ADD CONSTRAINT
	FK_Encomienda_ConformacionLocal_tipo_iluminacion FOREIGN KEY
	(
	id_iluminacion
	) REFERENCES dbo.tipo_iluminacion
	(
	id_iluminacion
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Encomienda_ConformacionLocal ADD CONSTRAINT
	FK_Encomienda_ConformacionLocal_tipo_ventilacion FOREIGN KEY
	(
	id_ventilacion
	) REFERENCES dbo.tipo_ventilacion
	(
	id_ventilacion
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Encomienda_ConformacionLocal ADD CONSTRAINT
	FK_Encomienda_ConformacionLocal_Tipo_Superficie FOREIGN KEY
	(
	id_tiposuperficie
	) REFERENCES dbo.TipoSuperficie
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Encomienda_ConformacionLocal ADD CONSTRAINT
	FK_Encomienda_ConformacionLocal_Encomienda FOREIGN KEY
	(
	id_encomienda
	) REFERENCES dbo.Encomienda
	(
	id_encomienda
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Encomienda_ConformacionLocal ADD CONSTRAINT
	FK_Encomienda_ConformacionLocal_TipoDestino FOREIGN KEY
	(
	id_destino
	) REFERENCES dbo.TipoDestino
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Encomienda_Sobrecarga_Detalle1
	(
	id_sobrecarga_detalle1 int NOT NULL IDENTITY (1, 1),
	id_sobrecarga int NOT NULL,
	id_tipo_destino int NOT NULL,
	id_tipo_uso int NOT NULL,
	valor decimal(8, 2) NOT NULL,
	detalle nvarchar(100) NULL,
	id_encomiendatiposector int NOT NULL,
	losa_sobre nvarchar(20) NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Encomienda_Sobrecarga_Detalle1 SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_Encomienda_Sobrecarga_Detalle1 ON
GO
IF EXISTS(SELECT * FROM dbo.Encomienda_Sobrecarga_Detalle1)
	 EXEC('INSERT INTO dbo.Tmp_Encomienda_Sobrecarga_Detalle1 (id_sobrecarga_detalle1, id_sobrecarga, id_tipo_destino, id_tipo_uso, valor, detalle, id_encomiendatiposector, losa_sobre)
		SELECT id_sobrecarga_detalle1, id_sobrecarga, id_tipo_destino, id_tipo_uso, valor, detalle, id_encomiendatiposector, losa_sobre FROM dbo.Encomienda_Sobrecarga_Detalle1 WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Encomienda_Sobrecarga_Detalle1 OFF
GO
ALTER TABLE dbo.Encomienda_Sobrecarga_Detalle2
	DROP CONSTRAINT FK_Encomienda_Sobrecarga_Detalle2_Encomienda_Sobrecarga_Detalle1
GO
DROP TABLE dbo.Encomienda_Sobrecarga_Detalle1
GO
EXECUTE sp_rename N'dbo.Tmp_Encomienda_Sobrecarga_Detalle1', N'Encomienda_Sobrecarga_Detalle1', 'OBJECT' 
GO
ALTER TABLE dbo.Encomienda_Sobrecarga_Detalle1 ADD CONSTRAINT
	PK_Encomienda_Sobrecarga_Detalle1 PRIMARY KEY CLUSTERED 
	(
	id_sobrecarga_detalle1
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Encomienda_Sobrecarga_Detalle1 ADD CONSTRAINT
	FK_Encomienda_Sobrecarga_Detalle1_Encomienda_Certificado_Sobrecarga FOREIGN KEY
	(
	id_sobrecarga
	) REFERENCES dbo.Encomienda_Certificado_Sobrecarga
	(
	id_sobrecarga
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Encomienda_Sobrecarga_Detalle1 ADD CONSTRAINT
	FK_Encomienda_Sobrecarga_Detalle1_Encomienda_Plantas FOREIGN KEY
	(
	id_encomiendatiposector
	) REFERENCES dbo.Encomienda_Plantas
	(
	id_encomiendatiposector
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Encomienda_Sobrecarga_Detalle1 ADD CONSTRAINT
	FK_Encomienda_Sobrecarga_Detalle1_Encomienda_Tipos_Destinos FOREIGN KEY
	(
	id_tipo_destino
	) REFERENCES dbo.Encomienda_Tipos_Destinos
	(
	id_tipo_destino
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Encomienda_Sobrecarga_Detalle1 ADD CONSTRAINT
	FK_Encomienda_Sobrecarga_Detalle1_Encomienda_Tipos_Usos FOREIGN KEY
	(
	id_tipo_uso
	) REFERENCES dbo.Encomienda_Tipos_Usos
	(
	id_tipo_uso
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Encomienda_Sobrecarga_Detalle2
	(
	id_sobrecarga_detalle2 int NOT NULL IDENTITY (1, 1),
	id_sobrecarga_detalle1 int NOT NULL,
	id_tipo_uso_1 int NOT NULL,
	valor_1 decimal(8, 2) NOT NULL,
	id_tipo_uso_2 int NOT NULL,
	valor_2 decimal(8, 2) NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Encomienda_Sobrecarga_Detalle2 SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_Encomienda_Sobrecarga_Detalle2 ON
GO
IF EXISTS(SELECT * FROM dbo.Encomienda_Sobrecarga_Detalle2)
	 EXEC('INSERT INTO dbo.Tmp_Encomienda_Sobrecarga_Detalle2 (id_sobrecarga_detalle2, id_sobrecarga_detalle1, id_tipo_uso_1, valor_1, id_tipo_uso_2, valor_2)
		SELECT id_sobrecarga_detalle2, id_sobrecarga_detalle1, id_tipo_uso_1, valor_1, id_tipo_uso_2, valor_2 FROM dbo.Encomienda_Sobrecarga_Detalle2 WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Encomienda_Sobrecarga_Detalle2 OFF
GO
DROP TABLE dbo.Encomienda_Sobrecarga_Detalle2
GO
EXECUTE sp_rename N'dbo.Tmp_Encomienda_Sobrecarga_Detalle2', N'Encomienda_Sobrecarga_Detalle2', 'OBJECT' 
GO
ALTER TABLE dbo.Encomienda_Sobrecarga_Detalle2 ADD CONSTRAINT
	PK_Encomienda_Sobrecarga_Detalle2 PRIMARY KEY CLUSTERED 
	(
	id_sobrecarga_detalle2
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Encomienda_Sobrecarga_Detalle2 ADD CONSTRAINT
	FK_Encomienda_Sobrecarga_Detalle2_Encomienda_Sobrecarga_Detalle1 FOREIGN KEY
	(
	id_sobrecarga_detalle1
	) REFERENCES dbo.Encomienda_Sobrecarga_Detalle1
	(
	id_sobrecarga_detalle1
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Encomienda_Sobrecarga_Detalle2 ADD CONSTRAINT
	FK_Encomienda_Sobrecarga_Detalle2_Encomienda_Tipos_Usos FOREIGN KEY
	(
	id_tipo_uso_1
	) REFERENCES dbo.Encomienda_Tipos_Usos
	(
	id_tipo_uso
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Encomienda_Sobrecarga_Detalle2 ADD CONSTRAINT
	FK_Encomienda_Sobrecarga_Detalle2_Encomienda_Tipos_Usos1 FOREIGN KEY
	(
	id_tipo_uso_2
	) REFERENCES dbo.Encomienda_Tipos_Usos
	(
	id_tipo_uso
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Encomienda_Firmantes_PersonasFisicas
	(
	id_firmante_pf int NOT NULL IDENTITY (1, 1),
	id_encomienda int NOT NULL,
	id_personafisica int NOT NULL,
	Apellido varchar(50) NOT NULL,
	Nombres nvarchar(50) NOT NULL,
	id_tipodoc_personal int NOT NULL,
	Nro_Documento nvarchar(15) NULL,
	id_tipocaracter int NOT NULL,
	Email nvarchar(70) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Encomienda_Firmantes_PersonasFisicas SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_Encomienda_Firmantes_PersonasFisicas ON
GO
IF EXISTS(SELECT * FROM dbo.Encomienda_Firmantes_PersonasFisicas)
	 EXEC('INSERT INTO dbo.Tmp_Encomienda_Firmantes_PersonasFisicas (id_firmante_pf, id_encomienda, id_personafisica, Apellido, Nombres, id_tipodoc_personal, Nro_Documento, id_tipocaracter, Email)
		SELECT id_firmante_pf, id_encomienda, id_personafisica, Apellido, Nombres, id_tipodoc_personal, Nro_Documento, id_tipocaracter, Email FROM dbo.Encomienda_Firmantes_PersonasFisicas WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Encomienda_Firmantes_PersonasFisicas OFF
GO
DROP TABLE dbo.Encomienda_Firmantes_PersonasFisicas
GO
EXECUTE sp_rename N'dbo.Tmp_Encomienda_Firmantes_PersonasFisicas', N'Encomienda_Firmantes_PersonasFisicas', 'OBJECT' 
GO
ALTER TABLE dbo.Encomienda_Firmantes_PersonasFisicas ADD CONSTRAINT
	PK_Encomienda_Firmantes_PersonasFisicas PRIMARY KEY CLUSTERED 
	(
	id_firmante_pf
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Encomienda_Firmantes_PersonasFisicas ADD CONSTRAINT
	FK_Encomienda_Firmantes_PersonasFisicas_Encomienda FOREIGN KEY
	(
	id_encomienda
	) REFERENCES dbo.Encomienda
	(
	id_encomienda
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Encomienda_Firmantes_PersonasFisicas ADD CONSTRAINT
	FK_Encomienda_Firmantes_PersonasFisicas_TipoDocumentoPersonal FOREIGN KEY
	(
	id_tipodoc_personal
	) REFERENCES dbo.TipoDocumentoPersonal
	(
	TipoDocumentoPersonalId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Encomienda_Firmantes_PersonasFisicas ADD CONSTRAINT
	FK_Encomienda_Firmantes_PersonasFisicas_TiposDeCaracterLegal FOREIGN KEY
	(
	id_tipocaracter
	) REFERENCES dbo.TiposDeCaracterLegal
	(
	id_tipocaracter
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Encomienda_Firmantes_PersonasFisicas ADD CONSTRAINT
	FK_Encomienda_Firmantes_PersonasFisicas_Encomienda_Titulares_PersonasFisicas FOREIGN KEY
	(
	id_personafisica
	) REFERENCES dbo.Encomienda_Titulares_PersonasFisicas
	(
	id_personafisica
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Encomienda_Titulares_PersonasJuridicas
	(
	id_personajuridica int NOT NULL IDENTITY (1, 1),
	id_encomienda int NOT NULL,
	Id_TipoSociedad int NOT NULL,
	Razon_Social nvarchar(200) NULL,
	CUIT nvarchar(13) NULL,
	id_tipoiibb int NOT NULL,
	Nro_IIBB nvarchar(20) NULL,
	Calle nvarchar(70) NULL,
	NroPuerta int NULL,
	Piso nvarchar(5) NULL,
	Depto nvarchar(5) NULL,
	id_localidad int NOT NULL,
	Codigo_Postal nvarchar(10) NULL,
	Telefono nvarchar(50) NULL,
	Email nvarchar(70) NULL,
	CreateUser uniqueidentifier NOT NULL,
	CreateDate datetime NOT NULL,
	LastUpdateUser uniqueidentifier NULL,
	LastUpdateDate datetime NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Encomienda_Titulares_PersonasJuridicas SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_Encomienda_Titulares_PersonasJuridicas ON
GO
IF EXISTS(SELECT * FROM dbo.Encomienda_Titulares_PersonasJuridicas)
	 EXEC('INSERT INTO dbo.Tmp_Encomienda_Titulares_PersonasJuridicas (id_personajuridica, id_encomienda, Id_TipoSociedad, Razon_Social, CUIT, id_tipoiibb, Nro_IIBB, Calle, NroPuerta, Piso, Depto, id_localidad, Codigo_Postal, Telefono, Email, CreateUser, CreateDate, LastUpdateUser, LastUpdateDate)
		SELECT id_personajuridica, id_encomienda, Id_TipoSociedad, Razon_Social, CUIT, id_tipoiibb, Nro_IIBB, Calle, NroPuerta, Piso, Depto, id_localidad, Codigo_Postal, Telefono, Email, CreateUser, CreateDate, LastUpdateUser, LastUpdateDate FROM dbo.Encomienda_Titulares_PersonasJuridicas WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Encomienda_Titulares_PersonasJuridicas OFF
GO
ALTER TABLE dbo.Encomienda_Firmantes_PersonasJuridicas
	DROP CONSTRAINT FK_Encomienda_Firmantes_PersonasJuridicas_Encomienda_Titulares_PersonasJuridicas
GO
ALTER TABLE dbo.Encomienda_Titulares_PersonasJuridicas_PersonasFisicas
	DROP CONSTRAINT FK_Encomienda_Titulares_PersonasJuridicas_PersonasFisicas_Encomienda_Titulares_PersonasJuridicas
GO
DROP TABLE dbo.Encomienda_Titulares_PersonasJuridicas
GO
EXECUTE sp_rename N'dbo.Tmp_Encomienda_Titulares_PersonasJuridicas', N'Encomienda_Titulares_PersonasJuridicas', 'OBJECT' 
GO
ALTER TABLE dbo.Encomienda_Titulares_PersonasJuridicas ADD CONSTRAINT
	PK_Encomienda_Titulares_PersonasJuridicas PRIMARY KEY CLUSTERED 
	(
	id_personajuridica
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
CREATE NONCLUSTERED INDEX IX_Encomienda_Titulares_PersonasJuridicas_id_enc ON dbo.Encomienda_Titulares_PersonasJuridicas
	(
	id_encomienda
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE dbo.Encomienda_Titulares_PersonasJuridicas ADD CONSTRAINT
	FK_Encomienda_Titulares_PersonasJuridicas_Encomienda FOREIGN KEY
	(
	id_encomienda
	) REFERENCES dbo.Encomienda
	(
	id_encomienda
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Encomienda_Titulares_PersonasJuridicas ADD CONSTRAINT
	FK_Encomienda_Titulares_PersonasJuridicas_Localidad FOREIGN KEY
	(
	id_localidad
	) REFERENCES dbo.Localidad
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Encomienda_Titulares_PersonasJuridicas ADD CONSTRAINT
	FK_Encomienda_Titulares_PersonasJuridicas_TiposDeIngresosBrutos FOREIGN KEY
	(
	id_tipoiibb
	) REFERENCES dbo.TiposDeIngresosBrutos
	(
	id_tipoiibb
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Encomienda_Titulares_PersonasJuridicas ADD CONSTRAINT
	FK_Encomienda_Titulares_PersonasJuridicas_TipoSociedad FOREIGN KEY
	(
	Id_TipoSociedad
	) REFERENCES dbo.TipoSociedad
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Encomienda_Firmantes_PersonasJuridicas
	(
	id_firmante_pj int NOT NULL IDENTITY (1, 1),
	id_encomienda int NOT NULL,
	id_personajuridica int NOT NULL,
	Apellido varchar(50) NOT NULL,
	Nombres nvarchar(50) NOT NULL,
	id_tipodoc_personal int NOT NULL,
	Nro_Documento nvarchar(15) NULL,
	id_tipocaracter int NOT NULL,
	cargo_firmante_pj nvarchar(50) NULL,
	Email nvarchar(70) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Encomienda_Firmantes_PersonasJuridicas SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_Encomienda_Firmantes_PersonasJuridicas ON
GO
IF EXISTS(SELECT * FROM dbo.Encomienda_Firmantes_PersonasJuridicas)
	 EXEC('INSERT INTO dbo.Tmp_Encomienda_Firmantes_PersonasJuridicas (id_firmante_pj, id_encomienda, id_personajuridica, Apellido, Nombres, id_tipodoc_personal, Nro_Documento, id_tipocaracter, cargo_firmante_pj, Email)
		SELECT id_firmante_pj, id_encomienda, id_personajuridica, Apellido, Nombres, id_tipodoc_personal, Nro_Documento, id_tipocaracter, cargo_firmante_pj, Email FROM dbo.Encomienda_Firmantes_PersonasJuridicas WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Encomienda_Firmantes_PersonasJuridicas OFF
GO
ALTER TABLE dbo.Encomienda_Titulares_PersonasJuridicas_PersonasFisicas
	DROP CONSTRAINT FK_Encomienda_Titulares_PersonasJuridicas_PersonasFisicas_Encomienda_Firmantes_PersonasJuridicas
GO
DROP TABLE dbo.Encomienda_Firmantes_PersonasJuridicas
GO
EXECUTE sp_rename N'dbo.Tmp_Encomienda_Firmantes_PersonasJuridicas', N'Encomienda_Firmantes_PersonasJuridicas', 'OBJECT' 
GO
ALTER TABLE dbo.Encomienda_Firmantes_PersonasJuridicas ADD CONSTRAINT
	PK_Encomienda_Firmantes_PersonasJuridicas PRIMARY KEY CLUSTERED 
	(
	id_firmante_pj
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Encomienda_Firmantes_PersonasJuridicas ADD CONSTRAINT
	FK_Encomienda_Firmantes_PersonasJuridicas_Encomienda FOREIGN KEY
	(
	id_encomienda
	) REFERENCES dbo.Encomienda
	(
	id_encomienda
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Encomienda_Firmantes_PersonasJuridicas ADD CONSTRAINT
	FK_Encomienda_Firmantes_PersonasJuridicas_Encomienda_Titulares_PersonasJuridicas FOREIGN KEY
	(
	id_personajuridica
	) REFERENCES dbo.Encomienda_Titulares_PersonasJuridicas
	(
	id_personajuridica
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Encomienda_Firmantes_PersonasJuridicas ADD CONSTRAINT
	FK_Encomienda_Firmantes_PersonasJuridicas_TipoDocumentoPersonal FOREIGN KEY
	(
	id_tipodoc_personal
	) REFERENCES dbo.TipoDocumentoPersonal
	(
	TipoDocumentoPersonalId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Encomienda_Firmantes_PersonasJuridicas ADD CONSTRAINT
	FK_Encomienda_Firmantes_PersonasJuridicas_TiposDeCaracterLegal FOREIGN KEY
	(
	id_tipocaracter
	) REFERENCES dbo.TiposDeCaracterLegal
	(
	id_tipocaracter
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Encomienda_Titulares_PersonasJuridicas_PersonasFisicas
	DROP CONSTRAINT DF_Encomienda_Titulares_PersonasJuridicas_PersonasFisicas_firmante_misma_persona
GO
CREATE TABLE dbo.Tmp_Encomienda_Titulares_PersonasJuridicas_PersonasFisicas
	(
	id_titular_pj int NOT NULL IDENTITY (1, 1),
	id_encomienda int NOT NULL,
	id_personajuridica int NOT NULL,
	Apellido varchar(50) NOT NULL,
	Nombres nvarchar(50) NOT NULL,
	id_tipodoc_personal int NOT NULL,
	Nro_Documento nvarchar(15) NULL,
	Email nvarchar(70) NULL,
	id_firmante_pj int NOT NULL,
	firmante_misma_persona bit NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Encomienda_Titulares_PersonasJuridicas_PersonasFisicas SET (LOCK_ESCALATION = TABLE)
GO
ALTER TABLE dbo.Tmp_Encomienda_Titulares_PersonasJuridicas_PersonasFisicas ADD CONSTRAINT
	DF_Encomienda_Titulares_PersonasJuridicas_PersonasFisicas_firmante_misma_persona DEFAULT ((0)) FOR firmante_misma_persona
GO
SET IDENTITY_INSERT dbo.Tmp_Encomienda_Titulares_PersonasJuridicas_PersonasFisicas ON
GO
IF EXISTS(SELECT * FROM dbo.Encomienda_Titulares_PersonasJuridicas_PersonasFisicas)
	 EXEC('INSERT INTO dbo.Tmp_Encomienda_Titulares_PersonasJuridicas_PersonasFisicas (id_titular_pj, id_encomienda, id_personajuridica, Apellido, Nombres, id_tipodoc_personal, Nro_Documento, Email, id_firmante_pj, firmante_misma_persona)
		SELECT id_titular_pj, id_encomienda, id_personajuridica, Apellido, Nombres, id_tipodoc_personal, Nro_Documento, Email, id_firmante_pj, firmante_misma_persona FROM dbo.Encomienda_Titulares_PersonasJuridicas_PersonasFisicas WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Encomienda_Titulares_PersonasJuridicas_PersonasFisicas OFF
GO
DROP TABLE dbo.Encomienda_Titulares_PersonasJuridicas_PersonasFisicas
GO
EXECUTE sp_rename N'dbo.Tmp_Encomienda_Titulares_PersonasJuridicas_PersonasFisicas', N'Encomienda_Titulares_PersonasJuridicas_PersonasFisicas', 'OBJECT' 
GO
ALTER TABLE dbo.Encomienda_Titulares_PersonasJuridicas_PersonasFisicas ADD CONSTRAINT
	PK_Encomienda_Titulares_PersonasJuridicas_PersonasFisicas PRIMARY KEY CLUSTERED 
	(
	id_titular_pj
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Encomienda_Titulares_PersonasJuridicas_PersonasFisicas ADD CONSTRAINT
	FK_Encomienda_Titulares_PersonasJuridicas_PersonasFisicas_Encomienda FOREIGN KEY
	(
	id_encomienda
	) REFERENCES dbo.Encomienda
	(
	id_encomienda
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Encomienda_Titulares_PersonasJuridicas_PersonasFisicas ADD CONSTRAINT
	FK_Encomienda_Titulares_PersonasJuridicas_PersonasFisicas_Encomienda_Firmantes_PersonasJuridicas FOREIGN KEY
	(
	id_firmante_pj
	) REFERENCES dbo.Encomienda_Firmantes_PersonasJuridicas
	(
	id_firmante_pj
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Encomienda_Titulares_PersonasJuridicas_PersonasFisicas ADD CONSTRAINT
	FK_Encomienda_Titulares_PersonasJuridicas_PersonasFisicas_Encomienda_Titulares_PersonasJuridicas FOREIGN KEY
	(
	id_personajuridica
	) REFERENCES dbo.Encomienda_Titulares_PersonasJuridicas
	(
	id_personajuridica
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Encomienda_Titulares_PersonasJuridicas_PersonasFisicas ADD CONSTRAINT
	FK_Encomienda_Titulares_PersonasJuridicas_PersonasFisicas_TipoDocumentoPersonal FOREIGN KEY
	(
	id_tipodoc_personal
	) REFERENCES dbo.TipoDocumentoPersonal
	(
	TipoDocumentoPersonalId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_SSIT_Solicitudes_Titulares_PersonasJuridicas
	(
	id_personajuridica int NOT NULL IDENTITY (1, 1),
	id_solicitud int NOT NULL,
	Id_TipoSociedad int NOT NULL,
	Razon_Social nvarchar(200) NULL,
	CUIT nvarchar(13) NULL,
	id_tipoiibb int NOT NULL,
	Nro_IIBB nvarchar(20) NULL,
	Calle nvarchar(70) NULL,
	NroPuerta int NULL,
	Piso nvarchar(5) NULL,
	Depto nvarchar(5) NULL,
	id_localidad int NOT NULL,
	Codigo_Postal nvarchar(10) NULL,
	Telefono nvarchar(50) NULL,
	Email nvarchar(70) NULL,
	CreateUser uniqueidentifier NOT NULL,
	CreateDate datetime NOT NULL,
	LastUpdateUser uniqueidentifier NULL,
	LastUpdateDate datetime NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_SSIT_Solicitudes_Titulares_PersonasJuridicas SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_SSIT_Solicitudes_Titulares_PersonasJuridicas ON
GO
IF EXISTS(SELECT * FROM dbo.SSIT_Solicitudes_Titulares_PersonasJuridicas)
	 EXEC('INSERT INTO dbo.Tmp_SSIT_Solicitudes_Titulares_PersonasJuridicas (id_personajuridica, id_solicitud, Id_TipoSociedad, Razon_Social, CUIT, id_tipoiibb, Nro_IIBB, Calle, NroPuerta, Piso, Depto, id_localidad, Codigo_Postal, Telefono, Email, CreateUser, CreateDate, LastUpdateUser, LastUpdateDate)
		SELECT id_personajuridica, id_solicitud, Id_TipoSociedad, Razon_Social, CUIT, id_tipoiibb, Nro_IIBB, Calle, NroPuerta, Piso, Depto, id_localidad, Codigo_Postal, Telefono, Email, CreateUser, CreateDate, LastUpdateUser, LastUpdateDate FROM dbo.SSIT_Solicitudes_Titulares_PersonasJuridicas WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_SSIT_Solicitudes_Titulares_PersonasJuridicas OFF
GO
ALTER TABLE dbo.SSIT_Solicitudes_Firmantes_PersonasJuridicas
	DROP CONSTRAINT FK_SSIT_Solicitudes_Firmantes_PersonasJuridicas_SSIT_Solicitudes_Titulares_PersonasJuridicas
GO
ALTER TABLE dbo.SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas
	DROP CONSTRAINT FK_SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas_SSIT_Solicitudes_Titulares_PersonasJuridicas
GO
DROP TABLE dbo.SSIT_Solicitudes_Titulares_PersonasJuridicas
GO
EXECUTE sp_rename N'dbo.Tmp_SSIT_Solicitudes_Titulares_PersonasJuridicas', N'SSIT_Solicitudes_Titulares_PersonasJuridicas', 'OBJECT' 
GO
ALTER TABLE dbo.SSIT_Solicitudes_Titulares_PersonasJuridicas ADD CONSTRAINT
	PK_SSIT_Solicitudes_Titulares_PersonasJuridicas PRIMARY KEY CLUSTERED 
	(
	id_personajuridica
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.SSIT_Solicitudes_Titulares_PersonasJuridicas ADD CONSTRAINT
	FK_SSIT_Solicitudes_Titulares_PersonasJuridicas_Localidad FOREIGN KEY
	(
	id_localidad
	) REFERENCES dbo.Localidad
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SSIT_Solicitudes_Titulares_PersonasJuridicas ADD CONSTRAINT
	FK_SSIT_Solicitudes_Titulares_PersonasJuridicas_SSIT_Solicitudes FOREIGN KEY
	(
	id_solicitud
	) REFERENCES dbo.SSIT_Solicitudes
	(
	id_solicitud
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SSIT_Solicitudes_Titulares_PersonasJuridicas ADD CONSTRAINT
	FK_SSIT_Solicitudes_Titulares_PersonasJuridicas_TiposDeIngresosBrutos FOREIGN KEY
	(
	id_tipoiibb
	) REFERENCES dbo.TiposDeIngresosBrutos
	(
	id_tipoiibb
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SSIT_Solicitudes_Titulares_PersonasJuridicas ADD CONSTRAINT
	FK_SSIT_Solicitudes_Titulares_PersonasJuridicas_TipoSociedad FOREIGN KEY
	(
	Id_TipoSociedad
	) REFERENCES dbo.TipoSociedad
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.SGI_Tramites_Tareas_HAB ADD CONSTRAINT
	FK_SGI_Tramites_Tareas_HAB_SSIT_Solicitudes FOREIGN KEY
	(
	id_solicitud
	) REFERENCES dbo.SSIT_Solicitudes
	(
	id_solicitud
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SGI_Tramites_Tareas_HAB SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_SSIT_Solicitudes_Firmantes_PersonasJuridicas
	(
	id_firmante_pj int NOT NULL IDENTITY (1, 1),
	id_solicitud int NOT NULL,
	id_personajuridica int NOT NULL,
	Apellido varchar(50) NOT NULL,
	Nombres nvarchar(50) NOT NULL,
	id_tipodoc_personal int NOT NULL,
	Nro_Documento nvarchar(15) NULL,
	id_tipocaracter int NOT NULL,
	cargo_firmante_pj nvarchar(50) NULL,
	Email nvarchar(70) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_SSIT_Solicitudes_Firmantes_PersonasJuridicas SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_SSIT_Solicitudes_Firmantes_PersonasJuridicas ON
GO
IF EXISTS(SELECT * FROM dbo.SSIT_Solicitudes_Firmantes_PersonasJuridicas)
	 EXEC('INSERT INTO dbo.Tmp_SSIT_Solicitudes_Firmantes_PersonasJuridicas (id_firmante_pj, id_solicitud, id_personajuridica, Apellido, Nombres, id_tipodoc_personal, Nro_Documento, id_tipocaracter, cargo_firmante_pj, Email)
		SELECT id_firmante_pj, id_solicitud, id_personajuridica, Apellido, Nombres, id_tipodoc_personal, Nro_Documento, id_tipocaracter, cargo_firmante_pj, Email FROM dbo.SSIT_Solicitudes_Firmantes_PersonasJuridicas WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_SSIT_Solicitudes_Firmantes_PersonasJuridicas OFF
GO
ALTER TABLE dbo.SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas
	DROP CONSTRAINT FK_SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas_SSIT_Solicitudes_Firmantes_PersonasJuridicas
GO
DROP TABLE dbo.SSIT_Solicitudes_Firmantes_PersonasJuridicas
GO
EXECUTE sp_rename N'dbo.Tmp_SSIT_Solicitudes_Firmantes_PersonasJuridicas', N'SSIT_Solicitudes_Firmantes_PersonasJuridicas', 'OBJECT' 
GO
ALTER TABLE dbo.SSIT_Solicitudes_Firmantes_PersonasJuridicas ADD CONSTRAINT
	PK_SSIT_Solicitudes_Firmantes_PersonasJuridicas PRIMARY KEY CLUSTERED 
	(
	id_firmante_pj
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.SSIT_Solicitudes_Firmantes_PersonasJuridicas ADD CONSTRAINT
	FK_SSIT_Solicitudes_Firmantes_PersonasJuridicas_SSIT_Solicitudes FOREIGN KEY
	(
	id_solicitud
	) REFERENCES dbo.SSIT_Solicitudes
	(
	id_solicitud
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SSIT_Solicitudes_Firmantes_PersonasJuridicas ADD CONSTRAINT
	FK_SSIT_Solicitudes_Firmantes_PersonasJuridicas_SSIT_Solicitudes_Titulares_PersonasJuridicas FOREIGN KEY
	(
	id_personajuridica
	) REFERENCES dbo.SSIT_Solicitudes_Titulares_PersonasJuridicas
	(
	id_personajuridica
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SSIT_Solicitudes_Firmantes_PersonasJuridicas ADD CONSTRAINT
	FK_SSIT_Solicitudes_Firmantes_PersonasJuridicas_TipoDocumentoPersonal FOREIGN KEY
	(
	id_tipodoc_personal
	) REFERENCES dbo.TipoDocumentoPersonal
	(
	TipoDocumentoPersonalId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SSIT_Solicitudes_Firmantes_PersonasJuridicas ADD CONSTRAINT
	FK_SSIT_Solicitudes_Firmantes_PersonasJuridicas_TiposDeCaracterLegal FOREIGN KEY
	(
	id_tipocaracter
	) REFERENCES dbo.TiposDeCaracterLegal
	(
	id_tipocaracter
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas
	DROP CONSTRAINT DF_SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas_firmante_misma_persona
GO
CREATE TABLE dbo.Tmp_SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas
	(
	id_titular_pj int NOT NULL IDENTITY (1, 1),
	id_solicitud int NOT NULL,
	id_personajuridica int NOT NULL,
	Apellido varchar(50) NOT NULL,
	Nombres nvarchar(50) NOT NULL,
	id_tipodoc_personal int NOT NULL,
	Nro_Documento nvarchar(15) NULL,
	Email nvarchar(70) NULL,
	id_firmante_pj int NOT NULL,
	firmante_misma_persona bit NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas SET (LOCK_ESCALATION = TABLE)
GO
ALTER TABLE dbo.Tmp_SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas ADD CONSTRAINT
	DF_SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas_firmante_misma_persona DEFAULT ((0)) FOR firmante_misma_persona
GO
SET IDENTITY_INSERT dbo.Tmp_SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas ON
GO
IF EXISTS(SELECT * FROM dbo.SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas)
	 EXEC('INSERT INTO dbo.Tmp_SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas (id_titular_pj, id_solicitud, id_personajuridica, Apellido, Nombres, id_tipodoc_personal, Nro_Documento, Email, id_firmante_pj, firmante_misma_persona)
		SELECT id_titular_pj, id_solicitud, id_personajuridica, Apellido, Nombres, id_tipodoc_personal, Nro_Documento, Email, id_firmante_pj, firmante_misma_persona FROM dbo.SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas OFF
GO
DROP TABLE dbo.SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas
GO
EXECUTE sp_rename N'dbo.Tmp_SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas', N'SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas', 'OBJECT' 
GO
ALTER TABLE dbo.SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas ADD CONSTRAINT
	PK_SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas PRIMARY KEY CLUSTERED 
	(
	id_titular_pj
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas ADD CONSTRAINT
	FK_SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas_SSIT_Solicitudes FOREIGN KEY
	(
	id_solicitud
	) REFERENCES dbo.SSIT_Solicitudes
	(
	id_solicitud
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas ADD CONSTRAINT
	FK_SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas_SSIT_Solicitudes_Firmantes_PersonasJuridicas FOREIGN KEY
	(
	id_firmante_pj
	) REFERENCES dbo.SSIT_Solicitudes_Firmantes_PersonasJuridicas
	(
	id_firmante_pj
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas ADD CONSTRAINT
	FK_SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas_SSIT_Solicitudes_Titulares_PersonasJuridicas FOREIGN KEY
	(
	id_personajuridica
	) REFERENCES dbo.SSIT_Solicitudes_Titulares_PersonasJuridicas
	(
	id_personajuridica
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas ADD CONSTRAINT
	FK_SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas_TipoDocumentoPersonal FOREIGN KEY
	(
	id_tipodoc_personal
	) REFERENCES dbo.TipoDocumentoPersonal
	(
	TipoDocumentoPersonalId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_SSIT_Solicitudes_Pagos
	(
	id_sol_pago int NOT NULL IDENTITY (1, 1),
	id_solicitud int NOT NULL,
	id_pago int NOT NULL,
	monto_pago money NOT NULL,
	CreateUser uniqueidentifier NOT NULL,
	CreateDate datetime NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_SSIT_Solicitudes_Pagos SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_SSIT_Solicitudes_Pagos ON
GO
IF EXISTS(SELECT * FROM dbo.SSIT_Solicitudes_Pagos)
	 EXEC('INSERT INTO dbo.Tmp_SSIT_Solicitudes_Pagos (id_sol_pago, id_solicitud, id_pago, monto_pago, CreateUser, CreateDate)
		SELECT id_sol_pago, id_solicitud, id_pago, monto_pago, CreateUser, CreateDate FROM dbo.SSIT_Solicitudes_Pagos WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_SSIT_Solicitudes_Pagos OFF
GO
DROP TABLE dbo.SSIT_Solicitudes_Pagos
GO
EXECUTE sp_rename N'dbo.Tmp_SSIT_Solicitudes_Pagos', N'SSIT_Solicitudes_Pagos', 'OBJECT' 
GO
ALTER TABLE dbo.SSIT_Solicitudes_Pagos ADD CONSTRAINT
	PK_SSIT_Solicitudes_Pagos PRIMARY KEY CLUSTERED 
	(
	id_sol_pago
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.SSIT_Solicitudes_Pagos ADD CONSTRAINT
	FK_SSIT_Solicitudes_Pagos_aspnet_Users FOREIGN KEY
	(
	CreateUser
	) REFERENCES dbo.aspnet_Users
	(
	UserId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SSIT_Solicitudes_Pagos ADD CONSTRAINT
	FK_SSIT_Solicitudes_Pagos_SSIT_Solicitudes FOREIGN KEY
	(
	id_solicitud
	) REFERENCES dbo.SSIT_Solicitudes
	(
	id_solicitud
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_SSIT_Solicitudes_Ubicaciones
	(
	id_solicitudubicacion int NOT NULL IDENTITY (1, 1),
	id_solicitud int NULL,
	id_ubicacion int NULL,
	id_subtipoubicacion int NULL,
	local_subtipoubicacion nvarchar(25) NULL,
	deptoLocal_ubicacion nvarchar(50) NULL,
	CreateDate datetime NOT NULL,
	CreateUser uniqueidentifier NOT NULL,
	id_zonaplaneamiento int NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_SSIT_Solicitudes_Ubicaciones SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_SSIT_Solicitudes_Ubicaciones ON
GO
IF EXISTS(SELECT * FROM dbo.SSIT_Solicitudes_Ubicaciones)
	 EXEC('INSERT INTO dbo.Tmp_SSIT_Solicitudes_Ubicaciones (id_solicitudubicacion, id_solicitud, id_ubicacion, id_subtipoubicacion, local_subtipoubicacion, deptoLocal_ubicacion, CreateDate, CreateUser, id_zonaplaneamiento)
		SELECT id_solicitudubicacion, id_solicitud, id_ubicacion, id_subtipoubicacion, local_subtipoubicacion, deptoLocal_ubicacion, CreateDate, CreateUser, id_zonaplaneamiento FROM dbo.SSIT_Solicitudes_Ubicaciones WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_SSIT_Solicitudes_Ubicaciones OFF
GO
ALTER TABLE dbo.SSIT_Solicitudes_Ubicaciones_Puertas
	DROP CONSTRAINT FK_SSIT_Solicitudes_Ubicaciones_Puertas_SSIT_Ubicaciones
GO
ALTER TABLE dbo.SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal
	DROP CONSTRAINT FK_SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal_SSIT_Ubicaciones
GO
DROP TABLE dbo.SSIT_Solicitudes_Ubicaciones
GO
EXECUTE sp_rename N'dbo.Tmp_SSIT_Solicitudes_Ubicaciones', N'SSIT_Solicitudes_Ubicaciones', 'OBJECT' 
GO
ALTER TABLE dbo.SSIT_Solicitudes_Ubicaciones ADD CONSTRAINT
	PK_SSIT_Solicitudes_Ubicaciones PRIMARY KEY CLUSTERED 
	(
	id_solicitudubicacion
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.SSIT_Solicitudes_Ubicaciones ADD CONSTRAINT
	FK_SSIT_Solicitudes_Ubicaciones_SSIT_Solicitudes FOREIGN KEY
	(
	id_solicitud
	) REFERENCES dbo.SSIT_Solicitudes
	(
	id_solicitud
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SSIT_Solicitudes_Ubicaciones ADD CONSTRAINT
	FK_SSIT_Solicitudes_Ubicaciones_SubTiposDeUbicacion FOREIGN KEY
	(
	id_subtipoubicacion
	) REFERENCES dbo.SubTiposDeUbicacion
	(
	id_subtipoubicacion
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SSIT_Solicitudes_Ubicaciones ADD CONSTRAINT
	FK_SSIT_Solicitudes_Ubicaciones_Ubicaciones FOREIGN KEY
	(
	id_ubicacion
	) REFERENCES dbo.Ubicaciones
	(
	id_ubicacion
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SSIT_Solicitudes_Ubicaciones ADD CONSTRAINT
	FK_SSIT_Solicitudes_Ubicaciones_Zonas_Planeamiento FOREIGN KEY
	(
	id_zonaplaneamiento
	) REFERENCES dbo.Zonas_Planeamiento
	(
	id_zonaplaneamiento
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal
	(
	id_solicitudprophorizontal int NOT NULL IDENTITY (1, 1),
	id_solicitudubicacion int NULL,
	id_propiedadhorizontal int NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal ON
GO
IF EXISTS(SELECT * FROM dbo.SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal)
	 EXEC('INSERT INTO dbo.Tmp_SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal (id_solicitudprophorizontal, id_solicitudubicacion, id_propiedadhorizontal)
		SELECT id_solicitudprophorizontal, id_solicitudubicacion, id_propiedadhorizontal FROM dbo.SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal OFF
GO
DROP TABLE dbo.SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal
GO
EXECUTE sp_rename N'dbo.Tmp_SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal', N'SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal', 'OBJECT' 
GO
ALTER TABLE dbo.SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal ADD CONSTRAINT
	PK_SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal PRIMARY KEY CLUSTERED 
	(
	id_solicitudprophorizontal
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal ADD CONSTRAINT
	FK_SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal_SSIT_Ubicaciones FOREIGN KEY
	(
	id_solicitudubicacion
	) REFERENCES dbo.SSIT_Solicitudes_Ubicaciones
	(
	id_solicitudubicacion
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal ADD CONSTRAINT
	FK_SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal_Ubicaciones_PropiedadHorizontal FOREIGN KEY
	(
	id_propiedadhorizontal
	) REFERENCES dbo.Ubicaciones_PropiedadHorizontal
	(
	id_propiedadhorizontal
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_SSIT_Solicitudes_Ubicaciones_Puertas
	(
	id_solicitudpuerta int NOT NULL IDENTITY (1, 1),
	id_solicitudubicacion int NOT NULL,
	codigo_calle int NOT NULL,
	nombre_calle nvarchar(100) NOT NULL,
	NroPuerta int NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_SSIT_Solicitudes_Ubicaciones_Puertas SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_SSIT_Solicitudes_Ubicaciones_Puertas ON
GO
IF EXISTS(SELECT * FROM dbo.SSIT_Solicitudes_Ubicaciones_Puertas)
	 EXEC('INSERT INTO dbo.Tmp_SSIT_Solicitudes_Ubicaciones_Puertas (id_solicitudpuerta, id_solicitudubicacion, codigo_calle, nombre_calle, NroPuerta)
		SELECT id_solicitudpuerta, id_solicitudubicacion, codigo_calle, nombre_calle, NroPuerta FROM dbo.SSIT_Solicitudes_Ubicaciones_Puertas WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_SSIT_Solicitudes_Ubicaciones_Puertas OFF
GO
DROP TABLE dbo.SSIT_Solicitudes_Ubicaciones_Puertas
GO
EXECUTE sp_rename N'dbo.Tmp_SSIT_Solicitudes_Ubicaciones_Puertas', N'SSIT_Solicitudes_Ubicaciones_Puertas', 'OBJECT' 
GO
ALTER TABLE dbo.SSIT_Solicitudes_Ubicaciones_Puertas ADD CONSTRAINT
	PK_SSIT_Solicitudes_Ubicaciones_Puertas PRIMARY KEY CLUSTERED 
	(
	id_solicitudpuerta
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.SSIT_Solicitudes_Ubicaciones_Puertas ADD CONSTRAINT
	FK_SSIT_Solicitudes_Ubicaciones_Puertas_SSIT_Ubicaciones FOREIGN KEY
	(
	id_solicitudubicacion
	) REFERENCES dbo.SSIT_Solicitudes_Ubicaciones
	(
	id_solicitudubicacion
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.SSIT_Solicitudes_HistorialEstados ADD CONSTRAINT
	FK_SSIT_Solicitudes_HistorialEstados_SSIT_Solicitudes FOREIGN KEY
	(
	id_solicitud
	) REFERENCES dbo.SSIT_Solicitudes
	(
	id_solicitud
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SSIT_Solicitudes_HistorialEstados SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.SSIT_Solicitudes_AvisoCaducidad ADD CONSTRAINT
	FK_SSIT_Solicitudes_AvisoCaducidad_SSIT_Solicitudes FOREIGN KEY
	(
	id_solicitud
	) REFERENCES dbo.SSIT_Solicitudes
	(
	id_solicitud
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SSIT_Solicitudes_AvisoCaducidad SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.SSIT_Solicitudes_Observaciones ADD CONSTRAINT
	FK_SSIT_Solicitudes_Observaciones_SSIT_Solicitudes FOREIGN KEY
	(
	id_solicitud
	) REFERENCES dbo.SSIT_Solicitudes
	(
	id_solicitud
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SSIT_Solicitudes_Observaciones SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_SSIT_DocumentosAdjuntos
	(
	id_docadjunto int NOT NULL IDENTITY (1, 1),
	id_solicitud int NOT NULL,
	id_tdocreq int NOT NULL,
	tdocreq_detalle nvarchar(50) NULL,
	id_tipodocsis int NOT NULL,
	id_file int NOT NULL,
	generadoxSistema bit NOT NULL,
	CreateDate datetime NOT NULL,
	CreateUser uniqueidentifier NOT NULL,
	UpdateDate datetime NULL,
	UpdateUser uniqueidentifier NULL,
	nombre_archivo nvarchar(50) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_SSIT_DocumentosAdjuntos SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_SSIT_DocumentosAdjuntos ON
GO
IF EXISTS(SELECT * FROM dbo.SSIT_DocumentosAdjuntos)
	 EXEC('INSERT INTO dbo.Tmp_SSIT_DocumentosAdjuntos (id_docadjunto, id_solicitud, id_tdocreq, tdocreq_detalle, id_tipodocsis, id_file, generadoxSistema, CreateDate, CreateUser, UpdateDate, UpdateUser, nombre_archivo)
		SELECT id_docadjunto, id_solicitud, id_tdocreq, tdocreq_detalle, id_tipodocsis, id_file, generadoxSistema, CreateDate, CreateUser, UpdateDate, UpdateUser, nombre_archivo FROM dbo.SSIT_DocumentosAdjuntos WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_SSIT_DocumentosAdjuntos OFF
GO
DROP TABLE dbo.SSIT_DocumentosAdjuntos
GO
EXECUTE sp_rename N'dbo.Tmp_SSIT_DocumentosAdjuntos', N'SSIT_DocumentosAdjuntos', 'OBJECT' 
GO
ALTER TABLE dbo.SSIT_DocumentosAdjuntos ADD CONSTRAINT
	PK_SSIT_DocumentosAdjuntos PRIMARY KEY CLUSTERED 
	(
	id_docadjunto
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.SSIT_DocumentosAdjuntos ADD CONSTRAINT
	FK_SSIT_DocumentosAdjuntos_aspnet_Users FOREIGN KEY
	(
	UpdateUser
	) REFERENCES dbo.aspnet_Users
	(
	UserId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SSIT_DocumentosAdjuntos ADD CONSTRAINT
	FK_SSIT_DocumentosAdjuntos_SSIT_DocumentosAdjuntos FOREIGN KEY
	(
	CreateUser
	) REFERENCES dbo.aspnet_Users
	(
	UserId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SSIT_DocumentosAdjuntos ADD CONSTRAINT
	FK_SSIT_DocumentosAdjuntos_SSIT_Solicitudes FOREIGN KEY
	(
	id_solicitud
	) REFERENCES dbo.SSIT_Solicitudes
	(
	id_solicitud
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SSIT_DocumentosAdjuntos ADD CONSTRAINT
	FK_SSIT_DocumentosAdjuntos_TiposDeDocumentosRequeridos FOREIGN KEY
	(
	id_tdocreq
	) REFERENCES dbo.TiposDeDocumentosRequeridos
	(
	id_tdocreq
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SSIT_DocumentosAdjuntos ADD CONSTRAINT
	FK_SSIT_DocumentosAdjuntos_TiposDeDocumentosSistema FOREIGN KEY
	(
	id_tipodocsis
	) REFERENCES dbo.TiposDeDocumentosSistema
	(
	id_tipdocsis
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_SSIT_Solicitudes_Encomienda
	(
	id_sol_enc int NOT NULL IDENTITY (1, 1),
	id_solicitud int NOT NULL,
	id_encomienda int NOT NULL,
	CreateDate datetime NOT NULL,
	CreateUser uniqueidentifier NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_SSIT_Solicitudes_Encomienda SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_SSIT_Solicitudes_Encomienda ON
GO
IF EXISTS(SELECT * FROM dbo.SSIT_Solicitudes_Encomienda)
	 EXEC('INSERT INTO dbo.Tmp_SSIT_Solicitudes_Encomienda (id_sol_enc, id_solicitud, id_encomienda, CreateDate, CreateUser)
		SELECT id_sol_enc, id_solicitud, id_encomienda, CreateDate, CreateUser FROM dbo.SSIT_Solicitudes_Encomienda WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_SSIT_Solicitudes_Encomienda OFF
GO
DROP TABLE dbo.SSIT_Solicitudes_Encomienda
GO
EXECUTE sp_rename N'dbo.Tmp_SSIT_Solicitudes_Encomienda', N'SSIT_Solicitudes_Encomienda', 'OBJECT' 
GO
CREATE UNIQUE CLUSTERED INDEX INX_SSIT_Solicitudes_Encomienda_sol_enc ON dbo.SSIT_Solicitudes_Encomienda
	(
	id_solicitud,
	id_encomienda
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE dbo.SSIT_Solicitudes_Encomienda ADD CONSTRAINT
	FK_SSIT_Solicitudes_Encomienda_aspnet_Users FOREIGN KEY
	(
	CreateUser
	) REFERENCES dbo.aspnet_Users
	(
	UserId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SSIT_Solicitudes_Encomienda ADD CONSTRAINT
	FK_SSIT_Solicitudes_Encomienda_Encomienda FOREIGN KEY
	(
	id_encomienda
	) REFERENCES dbo.Encomienda
	(
	id_encomienda
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SSIT_Solicitudes_Encomienda ADD CONSTRAINT
	FK_SSIT_Solicitudes_Encomienda_SSIT_Solicitudes FOREIGN KEY
	(
	id_solicitud
	) REFERENCES dbo.SSIT_Solicitudes
	(
	id_solicitud
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_SSIT_Solicitudes_Titulares_PersonasFisicas
	(
	id_personafisica int NOT NULL IDENTITY (1, 1),
	id_solicitud int NOT NULL,
	Apellido varchar(50) NOT NULL,
	Nombres nvarchar(50) NOT NULL,
	id_tipodoc_personal int NOT NULL,
	Nro_Documento nvarchar(15) NULL,
	Cuit nvarchar(13) NULL,
	id_tipoiibb int NOT NULL,
	Ingresos_Brutos nvarchar(25) NULL,
	Calle nvarchar(70) NOT NULL,
	Nro_Puerta int NOT NULL,
	Piso varchar(2) NULL,
	Depto varchar(10) NULL,
	Id_Localidad int NOT NULL,
	Codigo_Postal nvarchar(10) NULL,
	TelefonoMovil nvarchar(20) NULL,
	Sms nvarchar(50) NULL,
	Email nvarchar(70) NULL,
	MismoFirmante bit NOT NULL,
	CreateUser uniqueidentifier NOT NULL,
	CreateDate datetime NOT NULL,
	LastUpdateUser uniqueidentifier NULL,
	LastupdateDate datetime NULL,
	Telefono nchar(50) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_SSIT_Solicitudes_Titulares_PersonasFisicas SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_SSIT_Solicitudes_Titulares_PersonasFisicas ON
GO
IF EXISTS(SELECT * FROM dbo.SSIT_Solicitudes_Titulares_PersonasFisicas)
	 EXEC('INSERT INTO dbo.Tmp_SSIT_Solicitudes_Titulares_PersonasFisicas (id_personafisica, id_solicitud, Apellido, Nombres, id_tipodoc_personal, Nro_Documento, Cuit, id_tipoiibb, Ingresos_Brutos, Calle, Nro_Puerta, Piso, Depto, Id_Localidad, Codigo_Postal, TelefonoMovil, Sms, Email, MismoFirmante, CreateUser, CreateDate, LastUpdateUser, LastupdateDate, Telefono)
		SELECT id_personafisica, id_solicitud, Apellido, Nombres, id_tipodoc_personal, Nro_Documento, Cuit, id_tipoiibb, Ingresos_Brutos, Calle, Nro_Puerta, Piso, Depto, Id_Localidad, Codigo_Postal, TelefonoMovil, Sms, Email, MismoFirmante, CreateUser, CreateDate, LastUpdateUser, LastupdateDate, Telefono FROM dbo.SSIT_Solicitudes_Titulares_PersonasFisicas WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_SSIT_Solicitudes_Titulares_PersonasFisicas OFF
GO
ALTER TABLE dbo.SSIT_Solicitudes_Firmantes_PersonasFisicas
	DROP CONSTRAINT FK_SSIT_Solicitudes_Firmantes_PersonasFisicas_SSIT_Solicitudes_Titulares_PersonasFisicas
GO
DROP TABLE dbo.SSIT_Solicitudes_Titulares_PersonasFisicas
GO
EXECUTE sp_rename N'dbo.Tmp_SSIT_Solicitudes_Titulares_PersonasFisicas', N'SSIT_Solicitudes_Titulares_PersonasFisicas', 'OBJECT' 
GO
ALTER TABLE dbo.SSIT_Solicitudes_Titulares_PersonasFisicas ADD CONSTRAINT
	PK_SSIT_Solicitudes_Titulares_PersonasFisicas PRIMARY KEY CLUSTERED 
	(
	id_personafisica
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.SSIT_Solicitudes_Titulares_PersonasFisicas ADD CONSTRAINT
	FK_SSIT_Solicitudes_Titulares_PersonasFisicas_Localidad FOREIGN KEY
	(
	Id_Localidad
	) REFERENCES dbo.Localidad
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SSIT_Solicitudes_Titulares_PersonasFisicas ADD CONSTRAINT
	FK_SSIT_Solicitudes_Titulares_PersonasFisicas_SSIT_Solicitudes FOREIGN KEY
	(
	id_solicitud
	) REFERENCES dbo.SSIT_Solicitudes
	(
	id_solicitud
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SSIT_Solicitudes_Titulares_PersonasFisicas ADD CONSTRAINT
	FK_SSIT_Solicitudes_Titulares_PersonasFisicas_TipoDocumentoPersonal FOREIGN KEY
	(
	id_tipodoc_personal
	) REFERENCES dbo.TipoDocumentoPersonal
	(
	TipoDocumentoPersonalId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SSIT_Solicitudes_Titulares_PersonasFisicas ADD CONSTRAINT
	FK_SSIT_Solicitudes_Titulares_PersonasFisicas_TiposDeIngresosBrutos FOREIGN KEY
	(
	id_tipoiibb
	) REFERENCES dbo.TiposDeIngresosBrutos
	(
	id_tipoiibb
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_SSIT_Solicitudes_Firmantes_PersonasFisicas
	(
	id_firmante_pf int NOT NULL IDENTITY (1, 1),
	id_solicitud int NOT NULL,
	id_personafisica int NOT NULL,
	Apellido varchar(50) NOT NULL,
	Nombres nvarchar(50) NOT NULL,
	id_tipodoc_personal int NOT NULL,
	Nro_Documento nvarchar(15) NULL,
	id_tipocaracter int NOT NULL,
	Email nvarchar(70) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_SSIT_Solicitudes_Firmantes_PersonasFisicas SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_SSIT_Solicitudes_Firmantes_PersonasFisicas ON
GO
IF EXISTS(SELECT * FROM dbo.SSIT_Solicitudes_Firmantes_PersonasFisicas)
	 EXEC('INSERT INTO dbo.Tmp_SSIT_Solicitudes_Firmantes_PersonasFisicas (id_firmante_pf, id_solicitud, id_personafisica, Apellido, Nombres, id_tipodoc_personal, Nro_Documento, id_tipocaracter, Email)
		SELECT id_firmante_pf, id_solicitud, id_personafisica, Apellido, Nombres, id_tipodoc_personal, Nro_Documento, id_tipocaracter, Email FROM dbo.SSIT_Solicitudes_Firmantes_PersonasFisicas WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_SSIT_Solicitudes_Firmantes_PersonasFisicas OFF
GO
DROP TABLE dbo.SSIT_Solicitudes_Firmantes_PersonasFisicas
GO
EXECUTE sp_rename N'dbo.Tmp_SSIT_Solicitudes_Firmantes_PersonasFisicas', N'SSIT_Solicitudes_Firmantes_PersonasFisicas', 'OBJECT' 
GO
ALTER TABLE dbo.SSIT_Solicitudes_Firmantes_PersonasFisicas ADD CONSTRAINT
	PK_SSIT_Solicitudes_Firmantes_PersonasFisicas PRIMARY KEY CLUSTERED 
	(
	id_firmante_pf
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.SSIT_Solicitudes_Firmantes_PersonasFisicas ADD CONSTRAINT
	FK_SSIT_Solicitudes_Firmantes_PersonasFisicas_SSIT_Solicitudes FOREIGN KEY
	(
	id_solicitud
	) REFERENCES dbo.SSIT_Solicitudes
	(
	id_solicitud
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SSIT_Solicitudes_Firmantes_PersonasFisicas ADD CONSTRAINT
	FK_SSIT_Solicitudes_Firmantes_PersonasFisicas_SSIT_Solicitudes_Titulares_PersonasFisicas FOREIGN KEY
	(
	id_personafisica
	) REFERENCES dbo.SSIT_Solicitudes_Titulares_PersonasFisicas
	(
	id_personafisica
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SSIT_Solicitudes_Firmantes_PersonasFisicas ADD CONSTRAINT
	FK_SSIT_Solicitudes_Firmantes_PersonasFisicas_TipoDocumentoPersonal FOREIGN KEY
	(
	id_tipodoc_personal
	) REFERENCES dbo.TipoDocumentoPersonal
	(
	TipoDocumentoPersonalId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SSIT_Solicitudes_Firmantes_PersonasFisicas ADD CONSTRAINT
	FK_SSIT_Solicitudes_Firmantes_PersonasFisicas_TiposDeCaracterLegal FOREIGN KEY
	(
	id_tipocaracter
	) REFERENCES dbo.TiposDeCaracterLegal
	(
	id_tipocaracter
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT

DECLARE 
	@SQL nvarchar(4000)

SET @SQL = N'
use tempdb

dbcc shrinkfile (tempdev, 20)

dbcc shrinkfile (templog, 10)

'
EXECUTE sp_executesql @SQL
GO