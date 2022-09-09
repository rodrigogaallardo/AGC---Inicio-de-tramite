IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SSIT_Solicitudes_Firmantes_PersonasFisicas_SSIT_Solicitudes]') AND parent_object_id = OBJECT_ID(N'[dbo].[SSIT_Solicitudes_Firmantes_PersonasFisicas]'))
ALTER TABLE [dbo].[SSIT_Solicitudes_Firmantes_PersonasFisicas] DROP CONSTRAINT [FK_SSIT_Solicitudes_Firmantes_PersonasFisicas_SSIT_Solicitudes]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SSIT_Solicitudes_Firmantes_PersonasFisicas_SSIT_Solicitudes_Titulares_PersonasFisicas]') AND parent_object_id = OBJECT_ID(N'[dbo].[SSIT_Solicitudes_Firmantes_PersonasFisicas]'))
ALTER TABLE [dbo].[SSIT_Solicitudes_Firmantes_PersonasFisicas] DROP CONSTRAINT [FK_SSIT_Solicitudes_Firmantes_PersonasFisicas_SSIT_Solicitudes_Titulares_PersonasFisicas]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SSIT_Solicitudes_Firmantes_PersonasFisicas_TipoDocumentoPersonal]') AND parent_object_id = OBJECT_ID(N'[dbo].[SSIT_Solicitudes_Firmantes_PersonasFisicas]'))
ALTER TABLE [dbo].[SSIT_Solicitudes_Firmantes_PersonasFisicas] DROP CONSTRAINT [FK_SSIT_Solicitudes_Firmantes_PersonasFisicas_TipoDocumentoPersonal]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SSIT_Solicitudes_Firmantes_PersonasFisicas_TiposDeCaracterLegal]') AND parent_object_id = OBJECT_ID(N'[dbo].[SSIT_Solicitudes_Firmantes_PersonasFisicas]'))
ALTER TABLE [dbo].[SSIT_Solicitudes_Firmantes_PersonasFisicas] DROP CONSTRAINT [FK_SSIT_Solicitudes_Firmantes_PersonasFisicas_TiposDeCaracterLegal]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SSIT_Solicitudes_Firmantes_PersonasJuridicas_SSIT_Solicitudes]') AND parent_object_id = OBJECT_ID(N'[dbo].[SSIT_Solicitudes_Firmantes_PersonasJuridicas]'))
ALTER TABLE [dbo].[SSIT_Solicitudes_Firmantes_PersonasJuridicas] DROP CONSTRAINT [FK_SSIT_Solicitudes_Firmantes_PersonasJuridicas_SSIT_Solicitudes]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SSIT_Solicitudes_Firmantes_PersonasJuridicas_SSIT_Solicitudes_Titulares_PersonasJuridicas]') AND parent_object_id = OBJECT_ID(N'[dbo].[SSIT_Solicitudes_Firmantes_PersonasJuridicas]'))
ALTER TABLE [dbo].[SSIT_Solicitudes_Firmantes_PersonasJuridicas] DROP CONSTRAINT [FK_SSIT_Solicitudes_Firmantes_PersonasJuridicas_SSIT_Solicitudes_Titulares_PersonasJuridicas]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SSIT_Solicitudes_Firmantes_PersonasJuridicas_TipoDocumentoPersonal]') AND parent_object_id = OBJECT_ID(N'[dbo].[SSIT_Solicitudes_Firmantes_PersonasJuridicas]'))
ALTER TABLE [dbo].[SSIT_Solicitudes_Firmantes_PersonasJuridicas] DROP CONSTRAINT [FK_SSIT_Solicitudes_Firmantes_PersonasJuridicas_TipoDocumentoPersonal]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SSIT_Solicitudes_Firmantes_PersonasJuridicas_TiposDeCaracterLegal]') AND parent_object_id = OBJECT_ID(N'[dbo].[SSIT_Solicitudes_Firmantes_PersonasJuridicas]'))
ALTER TABLE [dbo].[SSIT_Solicitudes_Firmantes_PersonasJuridicas] DROP CONSTRAINT [FK_SSIT_Solicitudes_Firmantes_PersonasJuridicas_TiposDeCaracterLegal]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SSIT_Solicitudes_Titulares_PersonasFisicas_Localidad]') AND parent_object_id = OBJECT_ID(N'[dbo].[SSIT_Solicitudes_Titulares_PersonasFisicas]'))
ALTER TABLE [dbo].[SSIT_Solicitudes_Titulares_PersonasFisicas] DROP CONSTRAINT [FK_SSIT_Solicitudes_Titulares_PersonasFisicas_Localidad]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SSIT_Solicitudes_Titulares_PersonasFisicas_SSIT_Solicitudes]') AND parent_object_id = OBJECT_ID(N'[dbo].[SSIT_Solicitudes_Titulares_PersonasFisicas]'))
ALTER TABLE [dbo].[SSIT_Solicitudes_Titulares_PersonasFisicas] DROP CONSTRAINT [FK_SSIT_Solicitudes_Titulares_PersonasFisicas_SSIT_Solicitudes]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SSIT_Solicitudes_Titulares_PersonasFisicas_TipoDocumentoPersonal]') AND parent_object_id = OBJECT_ID(N'[dbo].[SSIT_Solicitudes_Titulares_PersonasFisicas]'))
ALTER TABLE [dbo].[SSIT_Solicitudes_Titulares_PersonasFisicas] DROP CONSTRAINT [FK_SSIT_Solicitudes_Titulares_PersonasFisicas_TipoDocumentoPersonal]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SSIT_Solicitudes_Titulares_PersonasFisicas_TiposDeIngresosBrutos]') AND parent_object_id = OBJECT_ID(N'[dbo].[SSIT_Solicitudes_Titulares_PersonasFisicas]'))
ALTER TABLE [dbo].[SSIT_Solicitudes_Titulares_PersonasFisicas] DROP CONSTRAINT [FK_SSIT_Solicitudes_Titulares_PersonasFisicas_TiposDeIngresosBrutos]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SSIT_Solicitudes_Titulares_PersonasJuridicas_Localidad]') AND parent_object_id = OBJECT_ID(N'[dbo].[SSIT_Solicitudes_Titulares_PersonasJuridicas]'))
ALTER TABLE [dbo].[SSIT_Solicitudes_Titulares_PersonasJuridicas] DROP CONSTRAINT [FK_SSIT_Solicitudes_Titulares_PersonasJuridicas_Localidad]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SSIT_Solicitudes_Titulares_PersonasJuridicas_SSIT_Solicitudes]') AND parent_object_id = OBJECT_ID(N'[dbo].[SSIT_Solicitudes_Titulares_PersonasJuridicas]'))
ALTER TABLE [dbo].[SSIT_Solicitudes_Titulares_PersonasJuridicas] DROP CONSTRAINT [FK_SSIT_Solicitudes_Titulares_PersonasJuridicas_SSIT_Solicitudes]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SSIT_Solicitudes_Titulares_PersonasJuridicas_TiposDeIngresosBrutos]') AND parent_object_id = OBJECT_ID(N'[dbo].[SSIT_Solicitudes_Titulares_PersonasJuridicas]'))
ALTER TABLE [dbo].[SSIT_Solicitudes_Titulares_PersonasJuridicas] DROP CONSTRAINT [FK_SSIT_Solicitudes_Titulares_PersonasJuridicas_TiposDeIngresosBrutos]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SSIT_Solicitudes_Titulares_PersonasJuridicas_TipoSociedad]') AND parent_object_id = OBJECT_ID(N'[dbo].[SSIT_Solicitudes_Titulares_PersonasJuridicas]'))
ALTER TABLE [dbo].[SSIT_Solicitudes_Titulares_PersonasJuridicas] DROP CONSTRAINT [FK_SSIT_Solicitudes_Titulares_PersonasJuridicas_TipoSociedad]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas_SSIT_Solicitudes]') AND parent_object_id = OBJECT_ID(N'[dbo].[SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas]'))
ALTER TABLE [dbo].[SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas] DROP CONSTRAINT [FK_SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas_SSIT_Solicitudes]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas_SSIT_Solicitudes_Firmantes_PersonasJuridicas]') AND parent_object_id = OBJECT_ID(N'[dbo].[SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas]'))
ALTER TABLE [dbo].[SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas] DROP CONSTRAINT [FK_SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas_SSIT_Solicitudes_Firmantes_PersonasJuridicas]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas_SSIT_Solicitudes_Titulares_PersonasJuridicas]') AND parent_object_id = OBJECT_ID(N'[dbo].[SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas]'))
ALTER TABLE [dbo].[SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas] DROP CONSTRAINT [FK_SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas_SSIT_Solicitudes_Titulares_PersonasJuridicas]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas_TipoDocumentoPersonal]') AND parent_object_id = OBJECT_ID(N'[dbo].[SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas]'))
ALTER TABLE [dbo].[SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas] DROP CONSTRAINT [FK_SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas_TipoDocumentoPersonal]
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas_firmante_misma_persona]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas] DROP CONSTRAINT [DF_SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas_firmante_misma_persona]
END

GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SSIT_Solicitudes_Plantas_SSIT_Solicitudes]') AND parent_object_id = OBJECT_ID(N'[dbo].[SSIT_Solicitudes_Plantas]'))
ALTER TABLE [dbo].[SSIT_Solicitudes_Plantas] DROP CONSTRAINT [FK_SSIT_Solicitudes_Plantas_SSIT_Solicitudes]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SSIT_Solicitudes_Plantas_TipoSector]') AND parent_object_id = OBJECT_ID(N'[dbo].[SSIT_Solicitudes_Plantas]'))
ALTER TABLE [dbo].[SSIT_Solicitudes_Plantas] DROP CONSTRAINT [FK_SSIT_Solicitudes_Plantas_TipoSector]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SSIT_Ubicaciones_Encomienda]') AND parent_object_id = OBJECT_ID(N'[dbo].[SSIT_Ubicaciones]'))
ALTER TABLE [dbo].[SSIT_Ubicaciones] DROP CONSTRAINT [FK_SSIT_Ubicaciones_Encomienda]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SSIT_Ubicaciones_SubTiposDeUbicacion]') AND parent_object_id = OBJECT_ID(N'[dbo].[SSIT_Ubicaciones]'))
ALTER TABLE [dbo].[SSIT_Ubicaciones] DROP CONSTRAINT [FK_SSIT_Ubicaciones_SubTiposDeUbicacion]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SSIT_Ubicaciones_Ubicaciones]') AND parent_object_id = OBJECT_ID(N'[dbo].[SSIT_Ubicaciones]'))
ALTER TABLE [dbo].[SSIT_Ubicaciones] DROP CONSTRAINT [FK_SSIT_Ubicaciones_Ubicaciones]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SSIT_Ubicaciones_Zonas_Planeamiento]') AND parent_object_id = OBJECT_ID(N'[dbo].[SSIT_Ubicaciones]'))
ALTER TABLE [dbo].[SSIT_Ubicaciones] DROP CONSTRAINT [FK_SSIT_Ubicaciones_Zonas_Planeamiento]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SSIT_Ubicaciones_PropiedadHorizontal_SSIT_Ubicaciones]') AND parent_object_id = OBJECT_ID(N'[dbo].[SSIT_Ubicaciones_PropiedadHorizontal]'))
ALTER TABLE [dbo].[SSIT_Ubicaciones_PropiedadHorizontal] DROP CONSTRAINT [FK_SSIT_Ubicaciones_PropiedadHorizontal_SSIT_Ubicaciones]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SSIT_Ubicaciones_PropiedadHorizontal_Ubicaciones_PropiedadHorizontal]') AND parent_object_id = OBJECT_ID(N'[dbo].[SSIT_Ubicaciones_PropiedadHorizontal]'))
ALTER TABLE [dbo].[SSIT_Ubicaciones_PropiedadHorizontal] DROP CONSTRAINT [FK_SSIT_Ubicaciones_PropiedadHorizontal_Ubicaciones_PropiedadHorizontal]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SSIT_Ubicaciones_Puertas_SSIT_Ubicaciones]') AND parent_object_id = OBJECT_ID(N'[dbo].[SSIT_Ubicaciones_Puertas]'))
ALTER TABLE [dbo].[SSIT_Ubicaciones_Puertas] DROP CONSTRAINT [FK_SSIT_Ubicaciones_Puertas_SSIT_Ubicaciones]
GO

/****** Object:  Table [dbo].[SSIT_Solicitudes_Firmantes_PersonasFisicas]    Script Date: 09/20/2016 11:47:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SSIT_Solicitudes_Firmantes_PersonasFisicas]') AND type in (N'U'))
DROP TABLE [dbo].[SSIT_Solicitudes_Firmantes_PersonasFisicas]
GO

/****** Object:  Table [dbo].[SSIT_Solicitudes_Firmantes_PersonasJuridicas]    Script Date: 09/20/2016 11:47:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SSIT_Solicitudes_Firmantes_PersonasJuridicas]') AND type in (N'U'))
DROP TABLE [dbo].[SSIT_Solicitudes_Firmantes_PersonasJuridicas]
GO

/****** Object:  Table [dbo].[SSIT_Solicitudes_Titulares_PersonasFisicas]    Script Date: 09/20/2016 11:47:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SSIT_Solicitudes_Titulares_PersonasFisicas]') AND type in (N'U'))
DROP TABLE [dbo].[SSIT_Solicitudes_Titulares_PersonasFisicas]
GO

/****** Object:  Table [dbo].[SSIT_Solicitudes_Titulares_PersonasJuridicas]    Script Date: 09/20/2016 11:47:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SSIT_Solicitudes_Titulares_PersonasJuridicas]') AND type in (N'U'))
DROP TABLE [dbo].[SSIT_Solicitudes_Titulares_PersonasJuridicas]
GO

/****** Object:  Table [dbo].[SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas]    Script Date: 09/20/2016 11:47:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas]') AND type in (N'U'))
DROP TABLE [dbo].[SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas]
GO

/****** Object:  Table [dbo].[SSIT_Solicitudes_Plantas]    Script Date: 09/20/2016 11:47:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SSIT_Solicitudes_Plantas]') AND type in (N'U'))
DROP TABLE [dbo].[SSIT_Solicitudes_Plantas]
GO

/****** Object:  Table [dbo].[SSIT_Ubicaciones]    Script Date: 09/20/2016 11:47:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SSIT_Ubicaciones]') AND type in (N'U'))
DROP TABLE [dbo].[SSIT_Ubicaciones]
GO

/****** Object:  Table [dbo].[SSIT_Ubicaciones_PropiedadHorizontal]    Script Date: 09/20/2016 11:47:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SSIT_Ubicaciones_PropiedadHorizontal]') AND type in (N'U'))
DROP TABLE [dbo].[SSIT_Ubicaciones_PropiedadHorizontal]
GO

/****** Object:  Table [dbo].[SSIT_Ubicaciones_Puertas]    Script Date: 09/20/2016 11:47:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SSIT_Ubicaciones_Puertas]') AND type in (N'U'))
DROP TABLE [dbo].[SSIT_Ubicaciones_Puertas]
GO

/****** Object:  Table [dbo].[SSIT_Solicitudes_Firmantes_PersonasFisicas]    Script Date: 09/20/2016 11:47:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SSIT_Solicitudes_Firmantes_PersonasFisicas](
	[id_firmante_pf] [int] NOT NULL,
	[id_solicitud] [int] NOT NULL,
	[id_personafisica] [int] NOT NULL,
	[Apellido] [varchar](50) NOT NULL,
	[Nombres] [nvarchar](50) NOT NULL,
	[id_tipodoc_personal] [int] NOT NULL,
	[Nro_Documento] [nvarchar](15) NULL,
	[id_tipocaracter] [int] NOT NULL,
	[Email] [nvarchar](70) NULL,
 CONSTRAINT [PK_SSIT_Solicitudes_Firmantes_PersonasFisicas] PRIMARY KEY CLUSTERED 
(
	[id_firmante_pf] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[SSIT_Solicitudes_Firmantes_PersonasJuridicas]    Script Date: 09/20/2016 11:47:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SSIT_Solicitudes_Firmantes_PersonasJuridicas](
	[id_firmante_pj] [int] NOT NULL,
	[id_solicitud] [int] NOT NULL,
	[id_personajuridica] [int] NOT NULL,
	[Apellido] [varchar](50) NOT NULL,
	[Nombres] [nvarchar](50) NOT NULL,
	[id_tipodoc_personal] [int] NOT NULL,
	[Nro_Documento] [nvarchar](15) NULL,
	[id_tipocaracter] [int] NOT NULL,
	[cargo_firmante_pj] [nvarchar](50) NULL,
	[Email] [nvarchar](70) NULL,
 CONSTRAINT [PK_SSIT_Solicitudes_Firmantes_PersonasJuridicas] PRIMARY KEY CLUSTERED 
(
	[id_firmante_pj] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[SSIT_Solicitudes_Titulares_PersonasFisicas]    Script Date: 09/20/2016 11:47:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SSIT_Solicitudes_Titulares_PersonasFisicas](
	[id_personafisica] [int] NOT NULL,
	[id_solicitud] [int] NOT NULL,
	[Apellido] [varchar](50) NOT NULL,
	[Nombres] [nvarchar](50) NOT NULL,
	[id_tipodoc_personal] [int] NOT NULL,
	[Nro_Documento] [nvarchar](15) NULL,
	[Cuit] [nvarchar](13) NULL,
	[id_tipoiibb] [int] NOT NULL,
	[Ingresos_Brutos] [nvarchar](25) NULL,
	[Calle] [nvarchar](70) NOT NULL,
	[Nro_Puerta] [int] NOT NULL,
	[Piso] [varchar](2) NULL,
	[Depto] [varchar](10) NULL,
	[Id_Localidad] [int] NOT NULL,
	[Codigo_Postal] [nvarchar](10) NULL,
	[TelefonoArea] [nvarchar](10) NULL,
	[TelefonoPrefijo] [nvarchar](10) NULL,
	[TelefonoSufijo] [nvarchar](10) NULL,
	[TelefonoMovil] [nvarchar](20) NULL,
	[Sms] [nvarchar](50) NULL,
	[Email] [nvarchar](70) NULL,
	[MismoFirmante] [bit] NOT NULL,
	[CreateUser] [uniqueidentifier] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastUpdateUser] [uniqueidentifier] NULL,
	[LastupdateDate] [datetime] NULL,
 CONSTRAINT [PK_SSIT_Solicitudes_Titulares_PersonasFisicas] PRIMARY KEY CLUSTERED 
(
	[id_personafisica] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[SSIT_Solicitudes_Titulares_PersonasJuridicas]    Script Date: 09/20/2016 11:47:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SSIT_Solicitudes_Titulares_PersonasJuridicas](
	[id_personajuridica] [int] NOT NULL,
	[id_solicitud] [int] NOT NULL,
	[Id_TipoSociedad] [int] NOT NULL,
	[Razon_Social] [nvarchar](200) NULL,
	[CUIT] [nvarchar](13) NULL,
	[id_tipoiibb] [int] NOT NULL,
	[Nro_IIBB] [nvarchar](20) NULL,
	[Calle] [nvarchar](70) NULL,
	[NroPuerta] [int] NULL,
	[Piso] [nvarchar](5) NULL,
	[Depto] [nvarchar](5) NULL,
	[id_localidad] [int] NOT NULL,
	[Codigo_Postal] [nvarchar](10) NULL,
	[Telefono] [nvarchar](50) NULL,
	[Email] [nvarchar](70) NULL,
	[CreateUser] [uniqueidentifier] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastUpdateUser] [uniqueidentifier] NULL,
	[LastUpdateDate] [datetime] NULL,
 CONSTRAINT [PK_SSIT_Solicitudes_Titulares_PersonasJuridicas] PRIMARY KEY CLUSTERED 
(
	[id_personajuridica] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas]    Script Date: 09/20/2016 11:47:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas](
	[id_titular_pj] [int] NOT NULL,
	[id_solicitud] [int] NOT NULL,
	[id_personajuridica] [int] NOT NULL,
	[Apellido] [varchar](50) NOT NULL,
	[Nombres] [nvarchar](50) NOT NULL,
	[id_tipodoc_personal] [int] NOT NULL,
	[Nro_Documento] [nvarchar](15) NULL,
	[Email] [nvarchar](70) NULL,
	[id_firmante_pj] [int] NOT NULL,
	[firmante_misma_persona] [bit] NOT NULL,
 CONSTRAINT [PK_SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas] PRIMARY KEY CLUSTERED 
(
	[id_titular_pj] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[SSIT_Solicitudes_Plantas]    Script Date: 09/20/2016 11:47:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SSIT_Solicitudes_Plantas](
	[id_solicitudtiposector] [int] NOT NULL,
	[id_solicitud] [int] NOT NULL,
	[id_tiposector] [int] NOT NULL,
	[detalle_encomiendatiposector] [nvarchar](50) NULL,
 CONSTRAINT [PK_SSIT_Solicitudes_TiposDeSector] PRIMARY KEY CLUSTERED 
(
	[id_solicitudtiposector] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[SSIT_Ubicaciones]    Script Date: 09/20/2016 11:47:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SSIT_Ubicaciones](
	[id_solicitudubicacion] [int] NOT NULL,
	[id_solicitud] [int] NULL,
	[id_ubicacion] [int] NULL,
	[id_subtipoubicacion] [int] NULL,
	[local_subtipoubicacion] [nvarchar](25) NULL,
	[deptoLocal_ubicacion] [nvarchar](50) NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [uniqueidentifier] NOT NULL,
	[id_zonaplaneamiento] [int] NOT NULL,
 CONSTRAINT [PK_SSIT_Ubicaciones] PRIMARY KEY CLUSTERED 
(
	[id_solicitudubicacion] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[SSIT_Ubicaciones_PropiedadHorizontal]    Script Date: 09/20/2016 11:47:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SSIT_Ubicaciones_PropiedadHorizontal](
	[id_solicitudprophorizontal] [int] NOT NULL,
	[id_solicitudubicacion] [int] NULL,
	[id_propiedadhorizontal] [int] NULL,
 CONSTRAINT [PK_SSIT_Ubicaciones_PropiedadHorizontal] PRIMARY KEY CLUSTERED 
(
	[id_solicitudprophorizontal] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[SSIT_Ubicaciones_Puertas]    Script Date: 09/20/2016 11:47:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SSIT_Ubicaciones_Puertas](
	[id_solicitudpuerta] [int] NOT NULL,
	[id_solicitudubicacion] [int] NOT NULL,
	[codigo_calle] [int] NOT NULL,
	[nombre_calle] [nvarchar](100) NOT NULL,
	[NroPuerta] [int] NOT NULL,
 CONSTRAINT [PK_SSIT_Ubicaciones_Puertas] PRIMARY KEY CLUSTERED 
(
	[id_solicitudpuerta] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SSIT_Solicitudes_Firmantes_PersonasFisicas]  WITH CHECK ADD  CONSTRAINT [FK_SSIT_Solicitudes_Firmantes_PersonasFisicas_SSIT_Solicitudes] FOREIGN KEY([id_solicitud])
REFERENCES [dbo].[SSIT_Solicitudes] ([id_solicitud])
GO

ALTER TABLE [dbo].[SSIT_Solicitudes_Firmantes_PersonasFisicas] CHECK CONSTRAINT [FK_SSIT_Solicitudes_Firmantes_PersonasFisicas_SSIT_Solicitudes]
GO

ALTER TABLE [dbo].[SSIT_Solicitudes_Firmantes_PersonasFisicas]  WITH CHECK ADD  CONSTRAINT [FK_SSIT_Solicitudes_Firmantes_PersonasFisicas_SSIT_Solicitudes_Titulares_PersonasFisicas] FOREIGN KEY([id_personafisica])
REFERENCES [dbo].[SSIT_Solicitudes_Titulares_PersonasFisicas] ([id_personafisica])
GO

ALTER TABLE [dbo].[SSIT_Solicitudes_Firmantes_PersonasFisicas] CHECK CONSTRAINT [FK_SSIT_Solicitudes_Firmantes_PersonasFisicas_SSIT_Solicitudes_Titulares_PersonasFisicas]
GO

ALTER TABLE [dbo].[SSIT_Solicitudes_Firmantes_PersonasFisicas]  WITH CHECK ADD  CONSTRAINT [FK_SSIT_Solicitudes_Firmantes_PersonasFisicas_TipoDocumentoPersonal] FOREIGN KEY([id_tipodoc_personal])
REFERENCES [dbo].[TipoDocumentoPersonal] ([TipoDocumentoPersonalId])
GO

ALTER TABLE [dbo].[SSIT_Solicitudes_Firmantes_PersonasFisicas] CHECK CONSTRAINT [FK_SSIT_Solicitudes_Firmantes_PersonasFisicas_TipoDocumentoPersonal]
GO

ALTER TABLE [dbo].[SSIT_Solicitudes_Firmantes_PersonasFisicas]  WITH CHECK ADD  CONSTRAINT [FK_SSIT_Solicitudes_Firmantes_PersonasFisicas_TiposDeCaracterLegal] FOREIGN KEY([id_tipocaracter])
REFERENCES [dbo].[TiposDeCaracterLegal] ([id_tipocaracter])
GO

ALTER TABLE [dbo].[SSIT_Solicitudes_Firmantes_PersonasFisicas] CHECK CONSTRAINT [FK_SSIT_Solicitudes_Firmantes_PersonasFisicas_TiposDeCaracterLegal]
GO

ALTER TABLE [dbo].[SSIT_Solicitudes_Firmantes_PersonasJuridicas]  WITH CHECK ADD  CONSTRAINT [FK_SSIT_Solicitudes_Firmantes_PersonasJuridicas_SSIT_Solicitudes] FOREIGN KEY([id_solicitud])
REFERENCES [dbo].[SSIT_Solicitudes] ([id_solicitud])
GO

ALTER TABLE [dbo].[SSIT_Solicitudes_Firmantes_PersonasJuridicas] CHECK CONSTRAINT [FK_SSIT_Solicitudes_Firmantes_PersonasJuridicas_SSIT_Solicitudes]
GO

ALTER TABLE [dbo].[SSIT_Solicitudes_Firmantes_PersonasJuridicas]  WITH CHECK ADD  CONSTRAINT [FK_SSIT_Solicitudes_Firmantes_PersonasJuridicas_SSIT_Solicitudes_Titulares_PersonasJuridicas] FOREIGN KEY([id_personajuridica])
REFERENCES [dbo].[SSIT_Solicitudes_Titulares_PersonasJuridicas] ([id_personajuridica])
GO

ALTER TABLE [dbo].[SSIT_Solicitudes_Firmantes_PersonasJuridicas] CHECK CONSTRAINT [FK_SSIT_Solicitudes_Firmantes_PersonasJuridicas_SSIT_Solicitudes_Titulares_PersonasJuridicas]
GO

ALTER TABLE [dbo].[SSIT_Solicitudes_Firmantes_PersonasJuridicas]  WITH CHECK ADD  CONSTRAINT [FK_SSIT_Solicitudes_Firmantes_PersonasJuridicas_TipoDocumentoPersonal] FOREIGN KEY([id_tipodoc_personal])
REFERENCES [dbo].[TipoDocumentoPersonal] ([TipoDocumentoPersonalId])
GO

ALTER TABLE [dbo].[SSIT_Solicitudes_Firmantes_PersonasJuridicas] CHECK CONSTRAINT [FK_SSIT_Solicitudes_Firmantes_PersonasJuridicas_TipoDocumentoPersonal]
GO

ALTER TABLE [dbo].[SSIT_Solicitudes_Firmantes_PersonasJuridicas]  WITH CHECK ADD  CONSTRAINT [FK_SSIT_Solicitudes_Firmantes_PersonasJuridicas_TiposDeCaracterLegal] FOREIGN KEY([id_tipocaracter])
REFERENCES [dbo].[TiposDeCaracterLegal] ([id_tipocaracter])
GO

ALTER TABLE [dbo].[SSIT_Solicitudes_Firmantes_PersonasJuridicas] CHECK CONSTRAINT [FK_SSIT_Solicitudes_Firmantes_PersonasJuridicas_TiposDeCaracterLegal]
GO

ALTER TABLE [dbo].[SSIT_Solicitudes_Titulares_PersonasFisicas]  WITH CHECK ADD  CONSTRAINT [FK_SSIT_Solicitudes_Titulares_PersonasFisicas_Localidad] FOREIGN KEY([Id_Localidad])
REFERENCES [dbo].[Localidad] ([Id])
GO

ALTER TABLE [dbo].[SSIT_Solicitudes_Titulares_PersonasFisicas] CHECK CONSTRAINT [FK_SSIT_Solicitudes_Titulares_PersonasFisicas_Localidad]
GO

ALTER TABLE [dbo].[SSIT_Solicitudes_Titulares_PersonasFisicas]  WITH CHECK ADD  CONSTRAINT [FK_SSIT_Solicitudes_Titulares_PersonasFisicas_SSIT_Solicitudes] FOREIGN KEY([id_solicitud])
REFERENCES [dbo].[SSIT_Solicitudes] ([id_solicitud])
GO

ALTER TABLE [dbo].[SSIT_Solicitudes_Titulares_PersonasFisicas] CHECK CONSTRAINT [FK_SSIT_Solicitudes_Titulares_PersonasFisicas_SSIT_Solicitudes]
GO

ALTER TABLE [dbo].[SSIT_Solicitudes_Titulares_PersonasFisicas]  WITH CHECK ADD  CONSTRAINT [FK_SSIT_Solicitudes_Titulares_PersonasFisicas_TipoDocumentoPersonal] FOREIGN KEY([id_tipodoc_personal])
REFERENCES [dbo].[TipoDocumentoPersonal] ([TipoDocumentoPersonalId])
GO

ALTER TABLE [dbo].[SSIT_Solicitudes_Titulares_PersonasFisicas] CHECK CONSTRAINT [FK_SSIT_Solicitudes_Titulares_PersonasFisicas_TipoDocumentoPersonal]
GO

ALTER TABLE [dbo].[SSIT_Solicitudes_Titulares_PersonasFisicas]  WITH CHECK ADD  CONSTRAINT [FK_SSIT_Solicitudes_Titulares_PersonasFisicas_TiposDeIngresosBrutos] FOREIGN KEY([id_tipoiibb])
REFERENCES [dbo].[TiposDeIngresosBrutos] ([id_tipoiibb])
GO

ALTER TABLE [dbo].[SSIT_Solicitudes_Titulares_PersonasFisicas] CHECK CONSTRAINT [FK_SSIT_Solicitudes_Titulares_PersonasFisicas_TiposDeIngresosBrutos]
GO

ALTER TABLE [dbo].[SSIT_Solicitudes_Titulares_PersonasJuridicas]  WITH CHECK ADD  CONSTRAINT [FK_SSIT_Solicitudes_Titulares_PersonasJuridicas_Localidad] FOREIGN KEY([id_localidad])
REFERENCES [dbo].[Localidad] ([Id])
GO

ALTER TABLE [dbo].[SSIT_Solicitudes_Titulares_PersonasJuridicas] CHECK CONSTRAINT [FK_SSIT_Solicitudes_Titulares_PersonasJuridicas_Localidad]
GO

ALTER TABLE [dbo].[SSIT_Solicitudes_Titulares_PersonasJuridicas]  WITH CHECK ADD  CONSTRAINT [FK_SSIT_Solicitudes_Titulares_PersonasJuridicas_SSIT_Solicitudes] FOREIGN KEY([id_solicitud])
REFERENCES [dbo].[SSIT_Solicitudes] ([id_solicitud])
GO

ALTER TABLE [dbo].[SSIT_Solicitudes_Titulares_PersonasJuridicas] CHECK CONSTRAINT [FK_SSIT_Solicitudes_Titulares_PersonasJuridicas_SSIT_Solicitudes]
GO

ALTER TABLE [dbo].[SSIT_Solicitudes_Titulares_PersonasJuridicas]  WITH CHECK ADD  CONSTRAINT [FK_SSIT_Solicitudes_Titulares_PersonasJuridicas_TiposDeIngresosBrutos] FOREIGN KEY([id_tipoiibb])
REFERENCES [dbo].[TiposDeIngresosBrutos] ([id_tipoiibb])
GO

ALTER TABLE [dbo].[SSIT_Solicitudes_Titulares_PersonasJuridicas] CHECK CONSTRAINT [FK_SSIT_Solicitudes_Titulares_PersonasJuridicas_TiposDeIngresosBrutos]
GO

ALTER TABLE [dbo].[SSIT_Solicitudes_Titulares_PersonasJuridicas]  WITH CHECK ADD  CONSTRAINT [FK_SSIT_Solicitudes_Titulares_PersonasJuridicas_TipoSociedad] FOREIGN KEY([Id_TipoSociedad])
REFERENCES [dbo].[TipoSociedad] ([Id])
GO

ALTER TABLE [dbo].[SSIT_Solicitudes_Titulares_PersonasJuridicas] CHECK CONSTRAINT [FK_SSIT_Solicitudes_Titulares_PersonasJuridicas_TipoSociedad]
GO

ALTER TABLE [dbo].[SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas]  WITH CHECK ADD  CONSTRAINT [FK_SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas_SSIT_Solicitudes] FOREIGN KEY([id_solicitud])
REFERENCES [dbo].[SSIT_Solicitudes] ([id_solicitud])
GO

ALTER TABLE [dbo].[SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas] CHECK CONSTRAINT [FK_SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas_SSIT_Solicitudes]
GO

ALTER TABLE [dbo].[SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas]  WITH CHECK ADD  CONSTRAINT [FK_SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas_SSIT_Solicitudes_Firmantes_PersonasJuridicas] FOREIGN KEY([id_firmante_pj])
REFERENCES [dbo].[SSIT_Solicitudes_Firmantes_PersonasJuridicas] ([id_firmante_pj])
GO

ALTER TABLE [dbo].[SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas] CHECK CONSTRAINT [FK_SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas_SSIT_Solicitudes_Firmantes_PersonasJuridicas]
GO

ALTER TABLE [dbo].[SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas]  WITH CHECK ADD  CONSTRAINT [FK_SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas_SSIT_Solicitudes_Titulares_PersonasJuridicas] FOREIGN KEY([id_personajuridica])
REFERENCES [dbo].[SSIT_Solicitudes_Titulares_PersonasJuridicas] ([id_personajuridica])
GO

ALTER TABLE [dbo].[SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas] CHECK CONSTRAINT [FK_SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas_SSIT_Solicitudes_Titulares_PersonasJuridicas]
GO

ALTER TABLE [dbo].[SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas]  WITH CHECK ADD  CONSTRAINT [FK_SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas_TipoDocumentoPersonal] FOREIGN KEY([id_tipodoc_personal])
REFERENCES [dbo].[TipoDocumentoPersonal] ([TipoDocumentoPersonalId])
GO

ALTER TABLE [dbo].[SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas] CHECK CONSTRAINT [FK_SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas_TipoDocumentoPersonal]
GO

ALTER TABLE [dbo].[SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas] ADD  CONSTRAINT [DF_SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas_firmante_misma_persona]  DEFAULT ((0)) FOR [firmante_misma_persona]
GO

ALTER TABLE [dbo].[SSIT_Solicitudes_Plantas]  WITH CHECK ADD  CONSTRAINT [FK_SSIT_Solicitudes_Plantas_SSIT_Solicitudes] FOREIGN KEY([id_solicitud])
REFERENCES [dbo].[SSIT_Solicitudes] ([id_solicitud])
GO

ALTER TABLE [dbo].[SSIT_Solicitudes_Plantas] CHECK CONSTRAINT [FK_SSIT_Solicitudes_Plantas_SSIT_Solicitudes]
GO

ALTER TABLE [dbo].[SSIT_Solicitudes_Plantas]  WITH CHECK ADD  CONSTRAINT [FK_SSIT_Solicitudes_Plantas_TipoSector] FOREIGN KEY([id_tiposector])
REFERENCES [dbo].[TipoSector] ([Id])
GO

ALTER TABLE [dbo].[SSIT_Solicitudes_Plantas] CHECK CONSTRAINT [FK_SSIT_Solicitudes_Plantas_TipoSector]
GO

ALTER TABLE [dbo].[SSIT_Ubicaciones]  WITH CHECK ADD  CONSTRAINT [FK_SSIT_Ubicaciones_Encomienda] FOREIGN KEY([id_solicitud])
REFERENCES [dbo].[SSIT_Solicitudes] ([id_solicitud])
GO

ALTER TABLE [dbo].[SSIT_Ubicaciones] CHECK CONSTRAINT [FK_SSIT_Ubicaciones_Encomienda]
GO

ALTER TABLE [dbo].[SSIT_Ubicaciones]  WITH CHECK ADD  CONSTRAINT [FK_SSIT_Ubicaciones_SubTiposDeUbicacion] FOREIGN KEY([id_subtipoubicacion])
REFERENCES [dbo].[SubTiposDeUbicacion] ([id_subtipoubicacion])
GO

ALTER TABLE [dbo].[SSIT_Ubicaciones] CHECK CONSTRAINT [FK_SSIT_Ubicaciones_SubTiposDeUbicacion]
GO

ALTER TABLE [dbo].[SSIT_Ubicaciones]  WITH CHECK ADD  CONSTRAINT [FK_SSIT_Ubicaciones_Ubicaciones] FOREIGN KEY([id_ubicacion])
REFERENCES [dbo].[Ubicaciones] ([id_ubicacion])
GO

ALTER TABLE [dbo].[SSIT_Ubicaciones] CHECK CONSTRAINT [FK_SSIT_Ubicaciones_Ubicaciones]
GO

ALTER TABLE [dbo].[SSIT_Ubicaciones]  WITH CHECK ADD  CONSTRAINT [FK_SSIT_Ubicaciones_Zonas_Planeamiento] FOREIGN KEY([id_zonaplaneamiento])
REFERENCES [dbo].[Zonas_Planeamiento] ([id_zonaplaneamiento])
GO

ALTER TABLE [dbo].[SSIT_Ubicaciones] CHECK CONSTRAINT [FK_SSIT_Ubicaciones_Zonas_Planeamiento]
GO

ALTER TABLE [dbo].[SSIT_Ubicaciones_PropiedadHorizontal]  WITH CHECK ADD  CONSTRAINT [FK_SSIT_Ubicaciones_PropiedadHorizontal_SSIT_Ubicaciones] FOREIGN KEY([id_solicitudubicacion])
REFERENCES [dbo].[SSIT_Ubicaciones] ([id_solicitudubicacion])
GO

ALTER TABLE [dbo].[SSIT_Ubicaciones_PropiedadHorizontal] CHECK CONSTRAINT [FK_SSIT_Ubicaciones_PropiedadHorizontal_SSIT_Ubicaciones]
GO

ALTER TABLE [dbo].[SSIT_Ubicaciones_PropiedadHorizontal]  WITH CHECK ADD  CONSTRAINT [FK_SSIT_Ubicaciones_PropiedadHorizontal_Ubicaciones_PropiedadHorizontal] FOREIGN KEY([id_propiedadhorizontal])
REFERENCES [dbo].[Ubicaciones_PropiedadHorizontal] ([id_propiedadhorizontal])
GO

ALTER TABLE [dbo].[SSIT_Ubicaciones_PropiedadHorizontal] CHECK CONSTRAINT [FK_SSIT_Ubicaciones_PropiedadHorizontal_Ubicaciones_PropiedadHorizontal]
GO

ALTER TABLE [dbo].[SSIT_Ubicaciones_Puertas]  WITH CHECK ADD  CONSTRAINT [FK_SSIT_Ubicaciones_Puertas_SSIT_Ubicaciones] FOREIGN KEY([id_solicitudubicacion])
REFERENCES [dbo].[SSIT_Ubicaciones] ([id_solicitudubicacion])
GO

ALTER TABLE [dbo].[SSIT_Ubicaciones_Puertas] CHECK CONSTRAINT [FK_SSIT_Ubicaciones_Puertas_SSIT_Ubicaciones]
GO


