GO
IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'id_file' AND Object_ID = Object_ID(N'wsEscribanos_ActaNotarial')) BEGIN 

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

	ALTER TABLE dbo.wsEscribanos_ActaNotarial ADD
		id_file int NULL

	ALTER TABLE dbo.wsEscribanos_ActaNotarial SET (LOCK_ESCALATION = TABLE)

	COMMIT

END

GO
/****** Object:  StoredProcedure [dbo].[ws_Escribanos_ActaNotarial_insertar]    Script Date: 11/18/2016 11:36:34 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ws_Escribanos_ActaNotarial_insertar]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ws_Escribanos_ActaNotarial_insertar]
GO

/****** Object:  StoredProcedure [dbo].[ws_Escribanos_DerechoOcupacion_insertar]    Script Date: 11/18/2016 11:36:34 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ws_Escribanos_DerechoOcupacion_insertar]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ws_Escribanos_DerechoOcupacion_insertar]
GO

/****** Object:  StoredProcedure [dbo].[ws_Escribanos_InstrumentoJudicial_insertar]    Script Date: 11/18/2016 11:36:34 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ws_Escribanos_InstrumentoJudicial_insertar]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ws_Escribanos_InstrumentoJudicial_insertar]
GO

/****** Object:  StoredProcedure [dbo].[ws_Escribanos_InstrumentoPrivado_insertar]    Script Date: 11/18/2016 11:36:34 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ws_Escribanos_InstrumentoPrivado_insertar]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ws_Escribanos_InstrumentoPrivado_insertar]
GO

/****** Object:  StoredProcedure [dbo].[ws_Escribanos_InstrumentoPublico_insertar]    Script Date: 11/18/2016 11:36:34 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ws_Escribanos_InstrumentoPublico_insertar]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ws_Escribanos_InstrumentoPublico_insertar]
GO

/****** Object:  StoredProcedure [dbo].[ws_Escribanos_PersonasFisicas_insertar]    Script Date: 11/18/2016 11:36:34 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ws_Escribanos_PersonasFisicas_insertar]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ws_Escribanos_PersonasFisicas_insertar]
GO

/****** Object:  StoredProcedure [dbo].[ws_Escribanos_PersonasFisicas_Representantes_insertar]    Script Date: 11/18/2016 11:36:34 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ws_Escribanos_PersonasFisicas_Representantes_insertar]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ws_Escribanos_PersonasFisicas_Representantes_insertar]
GO

/****** Object:  StoredProcedure [dbo].[ws_Escribanos_PersonasJuridicas_insertar]    Script Date: 11/18/2016 11:36:34 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ws_Escribanos_PersonasJuridicas_insertar]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ws_Escribanos_PersonasJuridicas_insertar]
GO

/****** Object:  StoredProcedure [dbo].[ws_Escribanos_PersonasJuridicas_Representantes_insertar]    Script Date: 11/18/2016 11:36:34 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ws_Escribanos_PersonasJuridicas_Representantes_insertar]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ws_Escribanos_PersonasJuridicas_Representantes_insertar]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ws_Escribanos_ActaNotarial_insertar]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ws_Escribanos_ActaNotarial_insertar]
GO

CREATE PROCEDURE [dbo].[ws_Escribanos_ActaNotarial_insertar]
(
	@id_encomienda int
	,@id_tipo_escritura int = 0
	,@reemplaza bit = 0
	,@nro_matricula_escribano_acta int
	,@id_actuacion_notarial_acta int
	,@nro_escritura_acta int
	,@fecha_escritura_acta smalldatetime
	,@registro_acta nvarchar(50)
	,@local_afectado_ley13512 bit
	,@reglamento_admite_actividad_ley13512 bit
	,@fecha_asamblea_ley13512 smalldatetime
	,@fecha_reglamento_ley13512 smalldatetime
	,@nro_escritura_ley13512 int
	,@nro_matricula_escribano_ley13512 int
	,@registro_ley13512 nvarchar(50)
	,@jurisdiccion_ley13512 nvarchar(50)
	,@fecha_inscrip_reglamento_ley13512 smalldatetime
	,@nro_matricula_regprop_ley13512 int
	,@CreateUser nvarchar(50)
	,@id_file int
	,@id_actanotarial int output
)
AS
BEGIN

	DECLARE 
		@id_tipo_escritura_ant int,
		@msgError nvarchar(200)
	
	SELECT TOP 1 @id_actanotarial = id_actanotarial
	FROM wsEscribanos_ActaNotarial 
	WHERE id_encomienda = @id_encomienda AND anulada = 0
	
	SET @id_actanotarial = ISNULL(@id_actanotarial,0)
	
	IF @id_actanotarial = 0 
	BEGIN
		
		EXEC @id_actanotarial = Id_Nuevo 'wsEscribanos_ActaNotarial'
		
		INSERT INTO wsEscribanos_ActaNotarial(
			id_actanotarial 
			,id_encomienda 
			,id_tipo_escritura
			,nro_matricula_escribano_acta 
			,id_actuacion_notarial_acta 
			,nro_escritura_acta 
			,fecha_escritura_acta 
			,registro_acta 
			,local_afectado_ley13512 
			,reglamento_admite_actividad_ley13512 
			,fecha_asamblea_ley13512 
			,fecha_reglamento_ley13512 
			,nro_escritura_ley13512 
			,nro_matricula_escribano_ley13512 
			,registro_ley13512 
			,jurisdiccion_ley13512 
			,fecha_inscrip_reglamento_ley13512 
			,nro_matricula_regprop_ley13512 
			,CreateDate 
			,CreateUser
			,anulada 
			,id_file
		)
		VALUES
		(
			@id_actanotarial 
			,@id_encomienda 
			,@id_tipo_escritura
			,@nro_matricula_escribano_acta 
			,@id_actuacion_notarial_acta 
			,@nro_escritura_acta 
			,@fecha_escritura_acta 
			,@registro_acta 
			,@local_afectado_ley13512 
			,@reglamento_admite_actividad_ley13512 
			,@fecha_asamblea_ley13512 
			,@fecha_reglamento_ley13512 
			,@nro_escritura_ley13512 
			,@nro_matricula_escribano_ley13512 
			,@registro_ley13512 
			,@jurisdiccion_ley13512 
			,@fecha_inscrip_reglamento_ley13512 
			,@nro_matricula_regprop_ley13512 
			,GETDATE() 
			,@CreateUser
			,0 
			,@id_file
		)
		
	END
	
END
GO

GO

/****** Object:  StoredProcedure [dbo].[ws_Escribanos_DerechoOcupacion_insertar]    Script Date: 11/18/2016 11:36:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ws_Escribanos_DerechoOcupacion_insertar]
(
	@id_actanotarial int
	,@id_tipo_derecho_ocupacion int
	,@tipo_derecho_ocupacion nvarchar(10)
	,@porcentaje_titularidad decimal(10,2)
	,@acredita_istrumento_publico bit
	,@acredita_instrumento_privado bit
	,@acredita_instrumento_judicial bit
	,@id_derecho_ocupacion int output
)
AS
BEGIN

	EXEC @id_derecho_ocupacion = Id_Nuevo 'wsEscribanos_DerechoOcupacion'
	INSERT INTO wsEscribanos_DerechoOcupacion(
		id_derecho_ocupacion 
		,id_actanotarial 
		,id_tipo_derecho_ocupacion 
		,tipo_derecho_ocupacion 
		,porcentaje_titularidad 
		,acredita_istrumento_publico 
		,acredita_instrumento_privado 
		,acredita_instrumento_judicial 
	)
	VALUES
	(
		@id_derecho_ocupacion 
		,@id_actanotarial 
		,@id_tipo_derecho_ocupacion 
		,@tipo_derecho_ocupacion 
		,@porcentaje_titularidad 
		,@acredita_istrumento_publico 
		,@acredita_instrumento_privado 
		,@acredita_instrumento_judicial 
	)

	RETURN @id_derecho_ocupacion
	
END

GO

/****** Object:  StoredProcedure [dbo].[ws_Escribanos_InstrumentoJudicial_insertar]    Script Date: 11/18/2016 11:36:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ws_Escribanos_InstrumentoJudicial_insertar]
(
	@id_derecho_ocupacion int
	,@fecha_testimonio_insjud smalldatetime
	,@juzgado_insjud nvarchar(100)
	,@secretaria_insjud nvarchar(100)
	,@jurisdiccion_insjud nvarchar(100)
	,@autos_insjud nvarchar(100)
	,@fecha_pago_impsellos_insjud smalldatetime
	,@Banco_pago_insjud nvarchar(100)
	,@monto_pago_insjud decimal(10,2)
	,@id_insjud int output
)
AS
BEGIN

	IF year(@fecha_testimonio_insjud) <= 1901
		set @fecha_testimonio_insjud = null
		
	IF year(@fecha_pago_impsellos_insjud) <= 1901
		set @fecha_pago_impsellos_insjud = null		
		
	EXEC @id_insjud = Id_Nuevo 'wsEscribanos_InstrumentoJudicial'
	INSERT INTO wsEscribanos_InstrumentoJudicial(
		id_insjud 
		,id_derecho_ocupacion 
		,fecha_testimonio_insjud 
		,juzgado_insjud 
		,secretaria_insjud 
		,jurisdiccion_insjud 
		,autos_insjud 
		,fecha_pago_impsellos_insjud 
		,Banco_pago_insjud 
		,monto_pago_insjud 
	)
	VALUES
	(
		@id_insjud 
		,@id_derecho_ocupacion 
		,@fecha_testimonio_insjud 
		,@juzgado_insjud 
		,@secretaria_insjud 
		,@jurisdiccion_insjud 
		,@autos_insjud 
		,@fecha_pago_impsellos_insjud 
		,@Banco_pago_insjud 
		,@monto_pago_insjud 
	)
	
END

GO

/****** Object:  StoredProcedure [dbo].[ws_Escribanos_InstrumentoPrivado_insertar]    Script Date: 11/18/2016 11:36:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ws_Escribanos_InstrumentoPrivado_insertar]
(
	@id_derecho_ocupacion int
	,@fecha_certificacion_inspriv smalldatetime
	,@nro_acta_inspriv int
	,@nro_matricula_escribano_inspriv int
	,@registro_inspriv nvarchar(50)
	,@jurisdiccion_inspriv nvarchar(100)
	,@fecha_pago_impsellos_inspriv smalldatetime
	,@banco_pago_inspriv nvarchar(100)
	,@monto_pago_inspriv decimal(10,2)
	,@id_inspriv int output
)
AS
BEGIN

	IF year(@fecha_certificacion_inspriv) <= 1901
		set @fecha_certificacion_inspriv = null
		
	IF year(@fecha_pago_impsellos_inspriv) <= 1901
		set @fecha_pago_impsellos_inspriv = null
				
			
	EXEC @id_inspriv = Id_Nuevo 'wsEscribanos_InstrumentoPrivado'
	INSERT INTO wsEscribanos_InstrumentoPrivado(
		id_inspriv 
		,id_derecho_ocupacion 
		,fecha_certificacion_inspriv 
		,nro_acta_inspriv 
		,nro_matricula_escribano_inspriv 
		,registro_inspriv 
		,jurisdiccion_inspriv 
		,fecha_pago_impsellos_inspriv 
		,banco_pago_inspriv 
		,monto_pago_inspriv 
	)
	VALUES
	(
		@id_inspriv 
		,@id_derecho_ocupacion 
		,@fecha_certificacion_inspriv 
		,@nro_acta_inspriv 
		,@nro_matricula_escribano_inspriv 
		,@registro_inspriv 
		,@jurisdiccion_inspriv 
		,@fecha_pago_impsellos_inspriv 
		,@banco_pago_inspriv 
		,@monto_pago_inspriv 
	)
	
END

GO

/****** Object:  StoredProcedure [dbo].[ws_Escribanos_InstrumentoPublico_insertar]    Script Date: 11/18/2016 11:36:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ws_Escribanos_InstrumentoPublico_insertar]
(
	@id_derecho_ocupacion int
	,@fecha_escritura_inspub smalldatetime
	,@nro_escritura_inspub int
	,@nro_matricula_escribano_inspub int
	,@registro_inspub nvarchar(50)
	,@jurisdiccion_inspub nvarchar(100)
	,@matricula_registro_prop_inspub nvarchar(50)
	,@id_inspub int output
)
AS
BEGIN

	IF year(@fecha_escritura_inspub) <= 1901
		set @fecha_escritura_inspub = null
					
	EXEC @id_inspub = Id_Nuevo 'wsEscribanos_InstrumentoPublico'
	INSERT INTO wsEscribanos_InstrumentoPublico(
		id_inspub 
		,id_derecho_ocupacion 
		,fecha_escritura_inspub 
		,nro_escritura_inspub 
		,nro_matricula_escribano_inspub 
		,registro_inspub 
		,jurisdiccion_inspub 
		,matricula_registro_prop_inspub 
	)
	VALUES
	(
		@id_inspub 
		,@id_derecho_ocupacion 
		,@fecha_escritura_inspub 
		,@nro_escritura_inspub 
		,@nro_matricula_escribano_inspub 
		,@registro_inspub 
		,@jurisdiccion_inspub 
		,@matricula_registro_prop_inspub 
	)
	
END

GO

/****** Object:  StoredProcedure [dbo].[ws_Escribanos_PersonasFisicas_insertar]    Script Date: 11/18/2016 11:36:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ws_Escribanos_PersonasFisicas_insertar]
(
	@id_actanotarial int
	,@id_personafisica int
	,@fecha_ultimo_pago_IIBB smalldatetime
	,@porcentaje_titularidad decimal(10,2)
	,@id_wsPersonasFisicas int output
)
AS
BEGIN

	DECLARE @msg				nvarchar(500)
	
	IF year(@fecha_ultimo_pago_IIBB) <= 1901
		set @fecha_ultimo_pago_IIBB = null
			
		
	EXEC @id_wsPersonasFisicas = Id_Nuevo 'wsEscribanos_PersonasFisicas'
	
	INSERT INTO wsEscribanos_PersonasFisicas(
		id_wsPersonasFisicas 
		,id_actanotarial 
		,id_personafisica 
		,fecha_ultimo_pago_IIBB 
		,porcentaje_titularidad 
	)
	VALUES
	(
		@id_wsPersonasFisicas 
		,@id_actanotarial 
		,@id_personafisica 
		,@fecha_ultimo_pago_IIBB 
		,@porcentaje_titularidad 
	)

	
END

GO

/****** Object:  StoredProcedure [dbo].[ws_Escribanos_PersonasFisicas_Representantes_insertar]    Script Date: 11/18/2016 11:36:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ws_Escribanos_PersonasFisicas_Representantes_insertar]
(
	@id_wsPersonaFisica int
	,@fecha_poder smalldatetime
	,@nro_escritura_poder int
	,@nro_matricula_escribano_poder int
	,@registro_poder nvarchar(50)
	,@jurisdiccion_poder nvarchar(100)
	,@id_firmante_pf int = null
	,@id_wsRepresentantePF int output
)
AS
BEGIN
	
	
	
	IF year(@fecha_poder) <= 1901
		set @fecha_poder = null
			
	EXEC @id_wsRepresentantePF = Id_Nuevo 'wsEscribanos_PersonasFisicas_Representantes'
	
	INSERT INTO wsEscribanos_PersonasFisicas_Representantes(
		id_wsRepresentantePF 
		,id_wsPersonaFisica 
		,fecha_poder 
		,nro_escritura_poder 
		,nro_matricula_escribano_poder 
		,registro_poder 
		,jurisdiccion_poder 
		,id_firmante_pf
	)
	VALUES
	(
		@id_wsRepresentantePF 
		,@id_wsPersonaFisica 
		,@fecha_poder 
		,@nro_escritura_poder 
		,@nro_matricula_escribano_poder 
		,@registro_poder 
		,@jurisdiccion_poder 
		,@id_firmante_pf
	)

END

GO

/****** Object:  StoredProcedure [dbo].[ws_Escribanos_PersonasJuridicas_insertar]    Script Date: 11/18/2016 11:36:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ws_Escribanos_PersonasJuridicas_insertar]
(
	@id_actanotarial int
	,@id_personajuridica int
	,@porcentaje_titularidad decimal(10,2)
	,@fecha_ultimo_pago_IIBB smalldatetime
	,@direccion_sede_social nvarchar(200)
	,@fecha_contrato_social smalldatetime
	,@instrumento_constitucion int
	,@nro_escritura_constitucion int
	,@nro_matricula_escribano_constitucion int
	,@registro_constitucion nvarchar(50)
	,@jurisdiccion_constitucion nvarchar(100)
	,@nom_organismo_inscripcion nvarchar(100)
	,@fecha_incripcion smalldatetime
	,@datos_incripcion nvarchar(200)
	,@nom_tipo_IVA nvarchar(70)
	,@id_wsPersonaJuridica int output
)
AS
BEGIN
	
	IF year(@fecha_ultimo_pago_IIBB) <= 1901
		set @fecha_ultimo_pago_IIBB = null
		
	IF year(@fecha_contrato_social) <= 1901
		set @fecha_contrato_social = null
		
	IF year(@fecha_incripcion) <= 1901
		set @fecha_incripcion = null				
			
	EXEC @id_wsPersonaJuridica = Id_Nuevo 'wsEscribanos_PersonasJuridicas'
	INSERT INTO wsEscribanos_PersonasJuridicas(
		id_wsPersonaJuridica 
		,id_actanotarial 
		,id_personajuridica 
		,porcentaje_titularidad 
		,fecha_ultimo_pago_IIBB 
		,direccion_sede_social 
		,fecha_contrato_social 
		,instrumento_constitucion 
		,nro_escritura_constitucion 
		,nro_matricula_escribano_constitucion 
		,registro_constitucion 
		,jurisdiccion_constitucion 
		,nom_organismo_inscripcion 
		,fecha_incripcion 
		,datos_incripcion 
		,nom_tipo_IVA
	)
	VALUES
	(
		@id_wsPersonaJuridica 
		,@id_actanotarial 
		,@id_personajuridica 
		,@porcentaje_titularidad 
		,@fecha_ultimo_pago_IIBB 
		,@direccion_sede_social 
		,@fecha_contrato_social 
		,@instrumento_constitucion 
		,@nro_escritura_constitucion 
		,@nro_matricula_escribano_constitucion 
		,@registro_constitucion 
		,@jurisdiccion_constitucion 
		,@nom_organismo_inscripcion 
		,@fecha_incripcion 
		,@datos_incripcion 
		,@nom_tipo_IVA
	)
	
END

GO

/****** Object:  StoredProcedure [dbo].[ws_Escribanos_PersonasJuridicas_Representantes_insertar]    Script Date: 11/18/2016 11:36:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ws_Escribanos_PersonasJuridicas_Representantes_insertar]
(
	@id_wsPersonaJuridica int
	,@fecha_designacion smalldatetime
	,@fecha_escritura_designacion smalldatetime
	,@nro_escritura_designacion int
	,@nro_matricula_escribano_designacion int
	,@registro_designacion nvarchar(50)
	,@jurisdiccion_designacion nvarchar(100)
	,@id_firmante_pj int = null
	,@id_wsRepresentantePJ int output
)
AS
BEGIN

	
	
	IF year(@fecha_designacion) <= 1901
		set @fecha_designacion = null
	
	IF year(@fecha_escritura_designacion) <= 1901
		set @fecha_escritura_designacion = null		
			
	EXEC @id_wsRepresentantePJ = Id_Nuevo 'wsEscribanos_PersonasJuridicas_Representantes'
	INSERT INTO wsEscribanos_PersonasJuridicas_Representantes(
		id_wsRepresentantePJ 
		,id_wsPersonaJuridica 
		,fecha_designacion 
		,fecha_escritura_designacion 
		,nro_escritura_designacion 
		,nro_matricula_escribano_designacion 
		,registro_designacion 
		,jurisdiccion_designacion 
		,id_firmante_pj
	)
	VALUES
	(
		@id_wsRepresentantePJ 
		,@id_wsPersonaJuridica 
		,@fecha_designacion 
		,@fecha_escritura_designacion 
		,@nro_escritura_designacion 
		,@nro_matricula_escribano_designacion 
		,@registro_designacion 
		,@jurisdiccion_designacion 
		,@id_firmante_pj
	)

	
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ws_Escribanos_AnularActaNotarial]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ws_Escribanos_AnularActaNotarial]
GO

CREATE PROCEDURE [dbo].[ws_Escribanos_AnularActaNotarial]
(
	
	@id_actanotarial	int,
	@UserId				nvarchar(50)
)
AS
BEGIN

	
	UPDATE wsEscribanos_ActaNotarial
	SET anulada = 1
		,LastUpdateUser = @UserId
		,LastUpdateDate = GETDATE()
	WHERE id_actanotarial = @id_actanotarial


END

GO

