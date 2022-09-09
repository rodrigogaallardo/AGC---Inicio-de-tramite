--USE [DGHP_Solicitudes3]
/*Creacion tabla Menues de InicioTramite*/


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'InicioTramite_Menues')
   begin
   print 'Creacion de tabla InicioTramite_Menues' ;
   
CREATE TABLE [dbo].[InicioTramite_Menues](
	[Id_menu] [int] NOT NULL,
	[Descripcion_menu] [nvarchar](200) NULL,
	[Aclaracion_menu] [nvarchar](300) NULL,
	[Pagina_menu] [nvarchar](200) NOT NULL,
	[IconCssClass_menu] [nvarchar](50) NULL,
	[Id_menu_padre] [int] NULL,
	[NroOrden] [int] NOT NULL,
	[Visible] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [uniqueidentifier] NOT NULL,
	[UpdateDate] [datetime] NULL,
	[UpdateUser] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Dbo_Menues] PRIMARY KEY CLUSTERED 
(
	[id_menu] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]



ALTER TABLE [dbo].[InicioTramite_Menues]  WITH CHECK ADD  CONSTRAINT [FK_dbo_Menues_dbo_Menues] FOREIGN KEY([id_menu_padre])
REFERENCES [dbo].[InicioTramite_Menues] ([id_menu])
ALTER TABLE [dbo].[InicioTramite_Menues] CHECK CONSTRAINT [FK_dbo_Menues_dbo_Menues]
ALTER TABLE [dbo].[InicioTramite_Menues]  WITH CHECK ADD  CONSTRAINT [FK_Init_Menues_aspnet_Users] FOREIGN KEY([CreateUser])
REFERENCES [dbo].[aspnet_Users] ([UserId])


--ALTER TABLE [dbo].[InicioTramite_Menues] CHECK CONSTRAINT [FK_SGI_Menues_aspnet_Users1]
--ALTER TABLE [dbo].[InicioTramite_Menues] ADD  CONSTRAINT [DF_SGI_Menues_visible]  DEFAULT ((1)) FOR [visible]
end


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'InicioTramite_Perfiles')
   begin
   print 'No Existe ' ;
CREATE TABLE dbo.[InicioTramite_Perfiles]
(
	[id_perfil] [int] NOT NULL,
	[nombre_perfil] [nvarchar](50) NOT NULL,
	[descripcion_perfil] [nvarchar](200) NULL,
	[CreateDate] [datetime] NULL,
	[CreateUser] [uniqueidentifier] NULL,
	[LastUpdateDate] [datetime] NULL,
	[LastUpdateUser] [uniqueidentifier] NULL,
 CONSTRAINT [PK_init_Perfiles] PRIMARY KEY CLUSTERED 
(
	[id_perfil] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
ALTER TABLE [dbo].[InicioTramite_Perfiles]  WITH CHECK ADD  CONSTRAINT [FK_init_Perfiles_aspnet_Users] FOREIGN KEY([CreateUser])
REFERENCES [dbo].[aspnet_Users] ([UserId])
ALTER TABLE [dbo].[InicioTramite_Perfiles] CHECK CONSTRAINT [FK_init_Perfiles_aspnet_Users]
ALTER TABLE [dbo].[InicioTramite_Perfiles]  WITH CHECK ADD  CONSTRAINT [FK_init_Perfiles_aspnet_Users1] FOREIGN KEY([LastUpdateUser])
REFERENCES [dbo].[aspnet_Users] ([UserId])
ALTER TABLE [dbo].[InicioTramite_Perfiles] CHECK CONSTRAINT [FK_init_Perfiles_aspnet_Users1]
end


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'InicioTramite_Rel_Perfiles_Menues')
   begin
   print 'No Existe ' 
   
   CREATE TABLE dbo.[InicioTramite_Rel_Perfiles_Menues](
	[Id_perfil] [int] NOT NULL,
	[Id_menu] [int] NOT NULL,
 CONSTRAINT [PK_init_Rel_Perfiles_Menues] PRIMARY KEY CLUSTERED 
(
	[Id_perfil] ASC,
	[Id_menu] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
ALTER TABLE [dbo].[InicioTramite_Rel_Perfiles_Menues]  WITH CHECK ADD  CONSTRAINT [FK_init_Rel_Perfiles_Menues_init_Menues] FOREIGN KEY([id_menu])
REFERENCES [dbo].[InicioTramite_Menues] ([id_menu])
ALTER TABLE [dbo].[InicioTramite_Rel_Perfiles_Menues] CHECK CONSTRAINT [FK_init_Rel_Perfiles_Menues_init_Menues]
ALTER TABLE [dbo].[InicioTramite_Rel_Perfiles_Menues]  WITH CHECK ADD  CONSTRAINT [FK_init_Rel_Perfiles_Menues_init_Perfiles] FOREIGN KEY([Id_perfil])
REFERENCES [dbo].InicioTramite_Perfiles ([id_perfil])
ALTER TABLE [dbo].[InicioTramite_Rel_Perfiles_Menues] CHECK CONSTRAINT [FK_init_Rel_Perfiles_Menues_init_Perfiles]
end


/*PERFILES */


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'InicioTramite_Rel_Perfiles_Usuarios')
   begin
   print 'No Existe ' 
CREATE TABLE [dbo].[InicioTramite_Rel_Perfiles_Usuarios](
	[Userid] [uniqueidentifier] NOT NULL,
	[Id_perfil] [int] NOT NULL,
 CONSTRAINT [PK_Rel_INIT_Usuarios_Perfiles] PRIMARY KEY CLUSTERED 
(
	[Userid] ASC,
	[Id_perfil] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[InicioTramite_Rel_Perfiles_Usuarios]  WITH CHECK ADD  CONSTRAINT [FK_init_Rel_Usuarios_Perfiles_aspnet_Users] FOREIGN KEY([Userid])
REFERENCES [dbo].[aspnet_Users] ([UserId])
ALTER TABLE [dbo].[InicioTramite_Rel_Perfiles_Usuarios] CHECK CONSTRAINT [FK_init_Rel_Usuarios_Perfiles_aspnet_Users]
end


/*CREACion  Tabla para FASE 2*/

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


IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SSIT_Ubicaciones_SubTiposDeUbicacion]') AND parent_object_id = OBJECT_ID(N'[dbo].[SSIT_Ubicaciones]'))
ALTER TABLE [dbo].[SSIT_Ubicaciones] DROP CONSTRAINT [FK_SSIT_Ubicaciones_SubTiposDeUbicacion]
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


/****** Object:  Table [dbo].[SSIT_Solicitudes_Ubicaciones_Puertas]    Script Date: 10/11/2016 14:36:53 ******/
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SSIT_Solicitudes_Ubicaciones_Puertas]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SSIT_Solicitudes_Ubicaciones_Puertas](
	[id_solicitudpuerta] [int] NOT NULL,
	[id_solicitudubicacion] [int] NOT NULL,
	[codigo_calle] [int] NOT NULL,
	[nombre_calle] [nvarchar](100) NOT NULL,
	[NroPuerta] [int] NOT NULL,
 CONSTRAINT [PK_SSIT_Solicitudes_Ubicaciones_Puertas] PRIMARY KEY CLUSTERED 
(
	[id_solicitudpuerta] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

/****** Object:  Table [dbo].[SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal]    Script Date: 10/11/2016 14:36:53 ******/
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal](
	[id_solicitudprophorizontal] [int] NOT NULL,
	[id_solicitudubicacion] [int] NULL,
	[id_propiedadhorizontal] [int] NULL,
 CONSTRAINT [PK_SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal] PRIMARY KEY CLUSTERED 
(
	[id_solicitudprophorizontal] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

/****** Object:  Table [dbo].[SSIT_Solicitudes_Ubicaciones]    Script Date: 10/11/2016 14:36:53 ******/
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SSIT_Solicitudes_Ubicaciones]') AND type in (N'U'))
BEGIN

CREATE TABLE [dbo].[SSIT_Solicitudes_Ubicaciones](
	[id_solicitudubicacion] [int] NOT NULL,
	[id_solicitud] [int] NULL,
	[id_ubicacion] [int] NULL,
	[id_subtipoubicacion] [int] NULL,
	[local_subtipoubicacion] [nvarchar](25) NULL,
	[deptoLocal_ubicacion] [nvarchar](50) NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUser] [uniqueidentifier] NOT NULL,
	[id_zonaplaneamiento] [int] NOT NULL,
 CONSTRAINT [PK_SSIT_Solicitudes_Ubicaciones] PRIMARY KEY CLUSTERED 
(
	[id_solicitudubicacion] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]



ALTER TABLE [dbo].[SSIT_Solicitudes_Ubicaciones_Puertas]  WITH CHECK ADD  CONSTRAINT [FK_SSIT_Solicitudes_Ubicaciones_Puertas_SSIT_Ubicaciones] FOREIGN KEY([id_solicitudubicacion])
REFERENCES [dbo].[SSIT_Solicitudes_Ubicaciones] ([id_solicitudubicacion])


ALTER TABLE [dbo].[SSIT_Solicitudes_Ubicaciones_Puertas] CHECK CONSTRAINT [FK_SSIT_Solicitudes_Ubicaciones_Puertas_SSIT_Ubicaciones]


ALTER TABLE [dbo].[SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal]  WITH CHECK ADD  CONSTRAINT [FK_SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal_SSIT_Ubicaciones] FOREIGN KEY([id_solicitudubicacion])
REFERENCES [dbo].[SSIT_Solicitudes_Ubicaciones] ([id_solicitudubicacion])


ALTER TABLE [dbo].[SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal] CHECK CONSTRAINT [FK_SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal_SSIT_Ubicaciones]


ALTER TABLE [dbo].[SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal]  WITH CHECK ADD  CONSTRAINT [FK_SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal_Ubicaciones_PropiedadHorizontal] FOREIGN KEY([id_propiedadhorizontal])
REFERENCES [dbo].[Ubicaciones_PropiedadHorizontal] ([id_propiedadhorizontal])


ALTER TABLE [dbo].[SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal] CHECK CONSTRAINT [FK_SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal_Ubicaciones_PropiedadHorizontal]


ALTER TABLE [dbo].[SSIT_Solicitudes_Ubicaciones]  WITH CHECK ADD  CONSTRAINT [FK_SSIT_Solicitudes_Ubicaciones_SSIT_Solicitudes] FOREIGN KEY([id_solicitud])
REFERENCES [dbo].[SSIT_Solicitudes] ([id_solicitud])


ALTER TABLE [dbo].[SSIT_Solicitudes_Ubicaciones] CHECK CONSTRAINT [FK_SSIT_Solicitudes_Ubicaciones_SSIT_Solicitudes]


ALTER TABLE [dbo].[SSIT_Solicitudes_Ubicaciones]  WITH CHECK ADD  CONSTRAINT [FK_SSIT_Solicitudes_Ubicaciones_SubTiposDeUbicacion] FOREIGN KEY([id_subtipoubicacion])
REFERENCES [dbo].[SubTiposDeUbicacion] ([id_subtipoubicacion])


ALTER TABLE [dbo].[SSIT_Solicitudes_Ubicaciones] CHECK CONSTRAINT [FK_SSIT_Solicitudes_Ubicaciones_SubTiposDeUbicacion]


ALTER TABLE [dbo].[SSIT_Solicitudes_Ubicaciones]  WITH CHECK ADD  CONSTRAINT [FK_SSIT_Solicitudes_Ubicaciones_Ubicaciones] FOREIGN KEY([id_ubicacion])
REFERENCES [dbo].[Ubicaciones] ([id_ubicacion])


ALTER TABLE [dbo].[SSIT_Solicitudes_Ubicaciones] CHECK CONSTRAINT [FK_SSIT_Solicitudes_Ubicaciones_Ubicaciones]


ALTER TABLE [dbo].[SSIT_Solicitudes_Ubicaciones]  WITH CHECK ADD  CONSTRAINT [FK_SSIT_Solicitudes_Ubicaciones_Zonas_Planeamiento] FOREIGN KEY([id_zonaplaneamiento])
REFERENCES [dbo].[Zonas_Planeamiento] ([id_zonaplaneamiento])


ALTER TABLE [dbo].[SSIT_Solicitudes_Ubicaciones] CHECK CONSTRAINT [FK_SSIT_Solicitudes_Ubicaciones_Zonas_Planeamiento]

END
GO




IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'SSIT_Solicitudes_Firmantes_PersonasFisicas')
                 
       begin

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
end



IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'SSIT_Solicitudes_Firmantes_PersonasJuridicas')
                 begin
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

end


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'SSIT_Solicitudes_Firmantes_PersonasJuridicas')
begin
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

end


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'SSIT_Solicitudes_Titulares_PersonasFisicas')
begin
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
end


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'SSIT_Solicitudes_Titulares_PersonasJuridicas')
begin
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
end


IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Encomienda_Rectificatoria]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[Encomienda_Rectificatoria](
		[id_encrec] [int] NOT NULL,
		[id_encomienda_anterior] [int] NOT NULL,
		[id_encomienda_nueva] [int] NOT NULL,
	 CONSTRAINT [PK_Encomienda_Rectificatoria] PRIMARY KEY CLUSTERED 
	(
		[id_encrec] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	ALTER TABLE [dbo].[Encomienda_Rectificatoria]  WITH CHECK ADD  CONSTRAINT [FK_Encomienda_Rectificatoria_Encomienda] FOREIGN KEY([id_encomienda_anterior])
	REFERENCES [dbo].[Encomienda] ([id_encomienda])

	ALTER TABLE [dbo].[Encomienda_Rectificatoria] CHECK CONSTRAINT [FK_Encomienda_Rectificatoria_Encomienda]

	ALTER TABLE [dbo].[Encomienda_Rectificatoria]  WITH CHECK ADD  CONSTRAINT [FK_Encomienda_Rectificatoria_Encomienda1] FOREIGN KEY([id_encomienda_nueva])
	REFERENCES [dbo].[Encomienda] ([id_encomienda])

	ALTER TABLE [dbo].[Encomienda_Rectificatoria] CHECK CONSTRAINT [FK_Encomienda_Rectificatoria_Encomienda1]
END
GO






IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas')
begin
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
end



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
IF EXISTS(SELECT * FROM sys.indexes WHERE object_id = object_id('dbo.SSIT_solicitudes') AND NAME ='IX_SSIT_Solicitudes')    
	drop index IX_SSIT_Solicitudes on  dbo.SSIT_solicitudes

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SSIT_Solicitudes_Encomienda]') AND parent_object_id = OBJECT_ID(N'[dbo].[SSIT_solicitudes]'))
	  ALTER TABLE SSIT_solicitudes DROP FK_SSIT_Solicitudes_Encomienda

IF EXISTS(SELECT * FROM sys.columns  WHERE Name = N'id_encomienda'
      AND Object_ID = Object_ID(N'SSIT_solicitudes'))
BEGIN
	
	ALTER TABLE SSIT_solicitudes
    DROP COLUMN id_encomienda   
    print 'ID_ENCOMINDA was Dropped From SSIT_solicitudes'
    
end
go


IF not EXISTS(SELECT * FROM sys.columns  WHERE Name      = N'CodigoSeguridad'
      AND Object_ID = Object_ID(N'SSIT_solicitudes'))
BEGIN   
	--select * from SSIT_solicitudes	
    ALTER TABLE SSIT_solicitudes
    add CodigoSeguridad  varchar(6)
    print 'CodigoSeguridad was Added TO SSIT_solicitudes'    
END


IF not EXISTS(SELECT * FROM sys.columns  WHERE Name      = N'id_solicitud'
      AND Object_ID = Object_ID(N'Encomienda'))
BEGIN
	ALTER TABLE dbo.Encomienda ADD id_solicitud int NULL
	
	ALTER TABLE Encomienda ADD CONSTRAINT FK_Encomienda_SSIT_Solicitudes FOREIGN KEY(id_solicitud) 
	REFERENCES dbo.SSIT_Solicitudes(id_solicitud)
END

GO	



/*STORE PROCEDURE*/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Encomienda_Imprimir_Encomienda]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Encomienda_Imprimir_Encomienda]

go

CREATE PROCEDURE [dbo].[Encomienda_Imprimir_Encomienda]
	@id_encomienda INT
AS
BEGIN
	SET NOCOUNT ON;
DECLARE @strPlantasHabilitar VARCHAR(MAX) = ''
DECLARE @id_encomienda_anterior INT
DECLARE @NroExpediente NVARCHAR(40)
DECLARE @id_profesional INT
DECLARE @logo_impresion_grupoconsejo NVARCHAR(100)

SELECT
	@id_profesional = enc.id_profesional
FROM encomienda enc
INNER JOIN encomienda_estados est ON enc.id_estado = est.id_estado
LEFT JOIN Zonas_Planeamiento zonpla ON enc.ZonaDeclarada = zonpla.CodZonaPla
WHERE 
	enc.id_encomienda = @id_encomienda

SELECT distinct
	@logo_impresion_grupoconsejo = gc.logo_impresion_grupoconsejo
FROM Profesional prof
INNER JOIN ConsejoProfesional cp ON prof.idconsejo = cp.id
INNER JOIN grupoconsejos gc ON cp.id_grupoconsejo = gc.id_grupoconsejo
WHERE 
	prof.id = @id_profesional

--------------------------------------------------------------Plantas Habilitar---------------------------------------------------------------------
--Variables Cursor
	DECLARE @id_tiposector INT, @Descripcion VARCHAR(255), @MuestraCampoAdicional BIT, @Detalle NVARCHAR(100), @TamanoCampoAdicional INT
	DECLARE @separador VARCHAR(5)

	DECLARE PlantasHabilitar CURSOR FOR
	SELECT
		tipsec.Id AS id_tiposector
		,tipsec.Descripcion
		,CONVERT(BIT, ISNULL(tipsec.MuestraCampoAdicional, 0)) AS MuestraCampoAdicional
		,encplan.detalle_encomiendatiposector AS Detalle
		,ISNULL(tipsec.TamanoCampoAdicional, 0) AS TamanoCampoAdicional
	FROM TipoSector tipsec
	INNER JOIN Encomienda_Plantas encplan ON tipsec.Id = encplan.id_tiposector
	WHERE 
		encplan.id_encomienda = @id_encomienda

	OPEN PlantasHabilitar
		FETCH NEXT FROM PlantasHabilitar INTO @id_tiposector, @Descripcion, @MuestraCampoAdicional, @Detalle, @TamanoCampoAdicional
		WHILE @@FETCH_STATUS = 0 BEGIN

		IF (LEN(@strPlantasHabilitar) > 0) BEGIN
			SET @separador = ', '
		END ELSE BEGIN
			SET @separador = ''
		END
	
		IF (@MuestraCampoAdicional = 1) BEGIN
			IF (@TamanoCampoAdicional >= 10) BEGIN
				SET @strPlantasHabilitar = @separador + @strPlantasHabilitar + @Detalle
			END ELSE BEGIN
				SET @strPlantasHabilitar = @separador + @strPlantasHabilitar + @Descripcion + ' ' + @Detalle
			END
		END

		FETCH NEXT FROM PlantasHabilitar INTO @id_tiposector, @Descripcion, @MuestraCampoAdicional, @Detalle, @TamanoCampoAdicional
		END
	CLOSE PlantasHabilitar
	DEALLOCATE PlantasHabilitar

-----------------------------------------------------------Encomienda Anterior y Nro Expediente-----------------------------------------------------
	SELECT
		@id_encomienda_anterior = rel.id_encomienda_anterior
		,@NroExpediente = ISNULL(sol.NroExpediente, '')
	FROM Rel_Encomienda_Rectificatoria rel
	LEFT JOIN SSIT_Solicitudes sol ON rel.id_solicitud_anterior = sol.id_solicitud
	WHERE 
		rel.id_encomienda_nueva = @id_encomienda

----------------------------------------------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------Encomienda-------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------------------------------
	SELECT
		enc.id_encomienda
		,enc.FechaEncomienda
		,enc.nroEncomiendaconsejo
		,enc.ZonaDeclarada
		,tt.cod_tipotramite AS TipoDeTramite
		,te.cod_tipoexpediente AS TipoDeExpediente
		,ste.cod_subtipoexpediente AS SubTipoDeExpediente
		,prof.matricula AS MatriculaProfesional
		,prof.apellido AS ApellidoProfesional
		,prof.nombre AS NombresProfesional
		,tdoc.nombre AS TipoDocProfesional
		,prof.nroDocumento AS DocumentoProfesional
		,grucon.id_grupoconsejo
		,grucon.nombre_grupoconsejo AS ConsejoProfesional
		,tipnorm.descripcion AS TipoNormativa
		,entnorm.descripcion AS EntidadNormativa
		,encnorm.nro_normativa AS NroNormativa
		,grucon.logo_impresion_grupoconsejo AS LogoUrl
		,CASE WHEN enc.id_estado <= 1 THEN CONVERT(BIT, 1) ELSE CONVERT(BIT, 0) END AS ImpresionDePrueba
		,@strPlantasHabilitar AS PlantasHabilitar
		,enc.Observaciones_plantas AS ObservacionesPlantasHabilitar
		,enc.Observaciones_rubros AS ObservacionesRubros
		,@id_encomienda_anterior AS id_encomienda_anterior
		,@NroExpediente AS NroExpediente
		,@logo_impresion_grupoconsejo AS NombreLogo
	FROM encomienda enc
	INNER JOIN tipotramite tt ON enc.id_tipotramite = tt.id_tipotramite
	INNER JOIN tipoexpediente te ON enc.id_tipoexpediente = te.id_tipoexpediente
	INNER JOIN subtipoexpediente ste ON enc.id_subtipoexpediente = ste.id_subtipoexpediente
	INNER JOIN profesional prof ON enc.id_profesional = prof.id
	INNER JOIN tipodocumentopersonal tdoc ON prof.idTipoDocumento = tdoc.tipodocumentopersonalId
	INNER JOIN ConsejoProfesional cprof ON enc.id_consejo = cprof.id
	INNER JOIN grupoconsejos grucon ON cprof.id_grupoconsejo = grucon.id_grupoconsejo
	LEFT JOIN Encomienda_Normativas encnorm ON enc.id_encomienda = encnorm.id_encomienda
	LEFT JOIN TipoNormativa tipnorm ON encnorm.id_tiponormativa = tipnorm.id
	LEFT JOIN EntidadNormativa entnorm ON encnorm.id_entidadnormativa = entnorm.id
	WHERE 
		enc.id_encomienda = @id_encomienda
--xxx Falta el logo
----------------------------------------------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------Ubicaciones------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------------------------------
	SELECT
		encubic.id_encomiendaubicacion
		,enc.id_encomienda
		,encubic.id_ubicacion
		,mat.Seccion
		,mat.Manzana
		,mat.parcela
		,mat.NroPartidaMatriz AS NroPartidaMatriz
		,encubic.local_subtipoubicacion
		,zon1.codzonapla AS ZonaParcela
		,dbo.Encomienda_Solicitud_DireccionesPartidasPlancheta(enc.id_encomienda, encubic.id_ubicacion) AS Direcciones
		,encubic.deptoLocal_encomiendaubicacion AS DeptoLocal
	FROM encomienda enc
	INNER JOIN Encomienda_Ubicaciones encubic ON enc.id_encomienda = encubic.id_encomienda
	INNER JOIN Ubicaciones mat ON encubic.id_ubicacion = mat.id_ubicacion
	INNER JOIN Zonas_Planeamiento zon1 ON encubic.id_zonaplaneamiento = zon1.id_zonaplaneamiento
	WHERE 
		enc.id_encomienda = @id_encomienda
	ORDER BY encubic.id_encomiendaubicacion

----------------------------------------------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------PropiedadHorizontal----------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------------------------------
	SELECT
		encubic.id_encomiendaubicacion
		,encubic.id_ubicacion
		,phor.NroPartidaHorizontal AS NroPartidaHorizontal
		,phor.piso
		,phor.depto
	FROM Encomienda_Ubicaciones encubic
	INNER JOIN Encomienda_Ubicaciones_PropiedadHorizontal encphor ON encubic.id_encomiendaubicacion = encphor.id_encomiendaubicacion
	INNER JOIN Ubicaciones_PropiedadHorizontal phor ON encphor.id_propiedadhorizontal = phor.id_propiedadhorizontal
	WHERE 
		encubic.id_encomienda = @id_encomienda
	ORDER BY encubic.id_encomiendaubicacion

----------------------------------------------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------Puertas----------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------------------------------
	SELECT
		encubic.id_encomiendaubicacion
		,encpuer.id_encomiendapuerta
		,encubic.id_encomienda
		,encubic.id_ubicacion
		,encpuer.nombre_calle AS Calle
		,encpuer.NroPuerta
	FROM Encomienda_Ubicaciones encubic
	INNER JOIN Encomienda_Ubicaciones_Puertas encpuer ON encubic.id_encomiendaubicacion = encpuer.id_encomiendaubicacion
	WHERE 
		encubic.id_encomienda = @id_encomienda
	ORDER BY encubic.id_encomiendaubicacion

----------------------------------------------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------ConformacionLocal------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------------------------------
	SELECT
		conf.id_encomiendaconflocal
		,enc.id_encomienda
		,dest.nombre AS Destino
		,conf.largo_conflocal
		,conf.ancho_conflocal
		,conf.alto_conflocal
		,conf.Paredes_conflocal
		,conf.Techos_conflocal
		,conf.Pisos_conflocal
		,conf.Frisos_conflocal
		,conf.Observaciones_conflocal
		,conf.Detalle_conflocal
		,conf.superficie_conflocal
		,conf.id_tiposuperficie
		,ISNULL(conf.id_encomiendatiposector, 0) AS id_encomiendatiposector
		,ISNULL(enc_plan.id_tiposector, 0) AS id_tiposector
		,CASE WHEN tip_sec.id = 11 THEN ISNULL(enc_plan.detalle_encomiendatiposector, '') ELSE tip_sec.Nombre + ' ' + ISNULL(enc_plan.detalle_encomiendatiposector, '') END AS desc_planta
		,ISNULL(conf.id_ventilacion, 0) AS id_ventilacion
		,ISNULL(vent.nom_ventilacion, '') AS desc_ventilacion
		,ISNULL(conf.id_iluminacion, 0) AS id_iluminacion
		,ISNULL(ilu.nom_iluminacion, '') AS desc_iluminacion
		,tipoSup.nombre AS desc_tiposuperficie
	FROM encomienda enc
	INNER JOIN Encomienda_ConformacionLocal conf ON enc.id_encomienda = conf.id_encomienda
	INNER JOIN TipoDestino dest ON conf.id_destino = dest.id
	LEFT JOIN TipoSuperficie tipoSup ON conf.id_tiposuperficie = tipoSup.id
	LEFT JOIN tipo_iluminacion ilu ON conf.id_iluminacion = ilu.id_iluminacion
	LEFT JOIN tipo_ventilacion vent ON conf.id_ventilacion = vent.id_ventilacion
	LEFT JOIN encomienda_plantas enc_plan ON conf.id_encomiendatiposector = enc_plan.id_encomiendatiposector
	LEFT JOIN TipoSector tip_sec ON tip_sec.id = enc_plan.id_tiposector
	WHERE 
		enc.id_encomienda = @id_encomienda

----------------------------------------------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------Firmantes--------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------------------------------
	SELECT
		pj.id_firmante_pj AS id_firmante
		,@id_encomienda AS id_encomienda
		,'PJ' AS TipoPersona
		,UPPER(pj.Apellido) AS Apellido
		,UPPER(pj.Nombres) AS Nombres
		,tdoc.nombre AS TipoDoc
		,pj.Nro_Documento AS NroDoc
		,tcl.nom_tipocaracter AS CaracterLegal
		,pj.Email
		,pj.cargo_firmante_pj
		,UPPER(titpj.Razon_Social) AS FirmanteDe
	FROM Encomienda_Firmantes_PersonasJuridicas pj
	INNER JOIN Encomienda_Titulares_PersonasJuridicas titpj ON pj.id_personajuridica = titpj.id_personajuridica
	INNER JOIN tiposdecaracterlegal tcl ON pj.id_tipocaracter = tcl.id_tipocaracter
	INNER JOIN tipodocumentopersonal tdoc ON pj.id_tipodoc_personal = tdoc.tipodocumentopersonalId
	WHERE 
		pj.id_encomienda = @id_encomienda
		AND titpj.Id_TipoSociedad NOT IN (2, 32)
	UNION ALL
	SELECT
		pj.id_firmante_pj AS id_firmante
		,@id_encomienda AS id_encomienda
		,'PJ' AS TipoPersona
		,UPPER(pj.Apellido) AS Apellido
		,UPPER(pj.Nombres) AS Nombres
		,tdoc.nombre AS TipoDoc
		,pj.Nro_Documento AS NroDoc
		,tcl.nom_tipocaracter AS CaracterLegal
		,pj.Email
		,pj.cargo_firmante_pj
		,UPPER((SELECT titsh.Apellido + ' ' + titsh.Nombres
			FROM Encomienda_Titulares_PersonasJuridicas_PersonasFisicas titsh
			WHERE titsh.id_firmante_pj = pj.id_firmante_pj)) AS FirmanteDe
	FROM Encomienda_Firmantes_PersonasJuridicas pj
	INNER JOIN Encomienda_Titulares_PersonasJuridicas titpj ON pj.id_personajuridica = titpj.id_personajuridica
	INNER JOIN tiposdecaracterlegal tcl ON pj.id_tipocaracter = tcl.id_tipocaracter
	INNER JOIN tipodocumentopersonal tdoc ON pj.id_tipodoc_personal = tdoc.tipodocumentopersonalId
	WHERE
		pj.id_encomienda = @id_encomienda
		AND titpj.Id_TipoSociedad IN (2, 32)
	UNION ALL
	SELECT
		pf.id_firmante_pf AS id_firmante
		,@id_encomienda AS id_encomienda
		,'PF' AS TipoPersona
		,UPPER(pf.Apellido) AS Apellido
		,UPPER(pf.Nombres) AS Nombres
		,tdoc.nombre AS TipoDoc
		,pf.Nro_Documento AS NroDoc
		,tcl.nom_tipocaracter AS CaracterLegal
		,pf.Email
		,'' AS cargo_firmante_pj
		,UPPER(titpf.Apellido + ', ' + titpf.Nombres) AS FirmanteDe
	FROM Encomienda_Firmantes_PersonasFisicas pf
	INNER JOIN Encomienda_Titulares_PersonasFisicas titpf ON pf.id_personafisica = titpf.id_personafisica
	INNER JOIN tiposdecaracterlegal tcl ON pf.id_tipocaracter = tcl.id_tipocaracter
	INNER JOIN tipodocumentopersonal tdoc ON pf.id_tipodoc_personal = tdoc.tipodocumentopersonalId
	WHERE 
		pf.id_encomienda = @id_encomienda

----------------------------------------------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------Titulares--------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------------------------------
	SELECT
		@id_encomienda AS id_persona
		,@id_encomienda AS id_encomienda
		,'PJ' AS TipoPersona
		,UPPER(pj.Razon_Social) AS RazonSocial
		,tsoc.descripcion AS TipoSociedad
		,'' AS Apellido
		,'' AS Nombres
		,'' AS TipoDoc
		,'' AS NroDoc
		,tipoiibb.nom_tipoiibb AS TipoIIBB
		,pj.Nro_IIBB AS NroIIBB
		,pj.cuit
		,1 AS MuestraEnTitulares
		,CASE WHEN pj.id_tiposociedad IN (2, 32) THEN 0 ELSE 1 END AS MuestraEnPlancheta
	FROM Encomienda_Titulares_PersonasJuridicas pj
	INNER JOIN TipoSociedad tsoc ON pj.id_tiposociedad = tsoc.id
	INNER JOIN TiposDeIngresosBrutos tipoiibb ON pj.id_tipoiibb = tipoiibb.id_tipoiibb
	WHERE 
		pj.id_encomienda = @id_encomienda
	UNION
	SELECT
		@id_encomienda AS id_persona
		,@id_encomienda AS id_encomienda
		,'PF' AS TipoPersona
		,'' AS RazonSocial
		,'' AS TipoSociedad
		,UPPER(pj.Apellido) AS Apellido
		,UPPER(pj.Nombres) AS Nombres
		,tdoc.nombre AS TipoDoc
		,pj.Nro_Documento AS NroDoc
		,'' AS TipoIIBB
		,'' AS NroIIBB
		,'' AS cuit
		,0 AS MuestraEnTitulares
		,1 AS MuestraEnPlancheta
	FROM Encomienda_Titulares_PersonasJuridicas_PersonasFisicas pj
	INNER JOIN Encomienda_Titulares_PersonasJuridicas titpj ON pj.id_personajuridica = titpj.id_personajuridica
	INNER JOIN tipodocumentopersonal tdoc ON pj.id_tipodoc_personal = tdoc.tipodocumentopersonalId
	WHERE 
		titpj.id_encomienda = @id_encomienda
		AND titpj.Id_TipoSociedad IN (2, 32)
	UNION
	SELECT
		@id_encomienda AS id_persona
		,@id_encomienda AS id_encomienda
		,'PF' AS TipoPersona
		,'' AS RazonSocial
		,'' AS TipoSociedad
		,UPPER(pf.Apellido) AS Apellido
		,UPPER(pf.Nombres) AS Nombres
		,tdoc.nombre AS TipoDoc
		,pf.Nro_Documento AS NroDoc
		,tipoiibb.nom_tipoiibb AS TipoIIBB
		,pf.Ingresos_Brutos AS NroIIBB
		,pf.cuit
		,1 AS MuestraEnTitulares
		,1 AS MuestraEnPlancheta
	FROM Encomienda_Titulares_PersonasFisicas pf
	INNER JOIN tipodocumentopersonal tdoc ON pf.id_tipodoc_personal = tdoc.tipodocumentopersonalId
	INNER JOIN TiposDeIngresosBrutos tipoiibb ON pf.id_tipoiibb = tipoiibb.id_tipoiibb
	WHERE 
		pf.id_encomienda = @id_encomienda
	
----------------------------------------------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------Rubros-----------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------------------------------
	SELECT
		rub.id_encomiendarubro
		,enc.id_encomienda
		,rub.cod_rubro
		,UPPER(rub.desc_rubro) AS desc_rubro
		,rub.EsAnterior
		,tact.nombre AS TipoActividad
		,docreq.nomenclatura AS DocRequerida
		,rub.SuperficieHabilitar
	FROM encomienda enc
	INNER JOIN Encomienda_rubros rub ON enc.id_encomienda = rub.id_encomienda
	INNER JOIN tipoactividad tact ON rub.id_tipoactividad = tact.id
	INNER JOIN Tipo_Documentacion_Req docreq ON rub.id_tipodocreq = docreq.id
	WHERE 
		enc.id_encomienda = @id_encomienda

----------------------------------------------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------DatosLocal-------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------------------------------
--Tiene el mapa xxx hay que probar
	SELECT
		dl.id_encomiendadatoslocal
		,enc.id_encomienda
		,dl.superficie_cubierta_dl
		,dl.superficie_descubierta_dl
		,dl.dimesion_frente_dl
		,dl.lugar_carga_descarga_dl
		,dl.estacionamiento_dl
		,dl.red_transito_pesado_dl
		,dl.sobre_avenida_dl
		,dl.materiales_pisos_dl
		,dl.materiales_paredes_dl
		,dl.materiales_techos_dl
		,dl.materiales_revestimientos_dl
		,dl.sanitarios_ubicacion_dl
		,dl.sanitarios_distancia_dl
		,dl.cantidad_sanitarios_dl
		,dl.superficie_sanitarios_dl
		,dl.frente_dl
		,dl.fondo_dl
		,dl.lateral_izquierdo_dl
		,dl.lateral_derecho_dl
		,dl.sobrecarga_corresponde_dl
		,dl.sobrecarga_tipo_observacion
		,dl.sobrecarga_requisitos_opcion
		,dl.sobrecarga_art813_inciso
		,dl.sobrecarga_art813_item
		,dl.cantidad_operarios_dl
		,dl.local_venta
		,Mapas.plano_mapa AS mapa_dl
		,Mapas.croquis_mapa AS croquis_dl
	FROM encomienda enc
	INNER JOIN Encomienda_DatosLocal dl ON enc.id_encomienda = dl.id_encomienda
	LEFT JOIN Encomienda_Ubicaciones encubic ON enc.id_encomienda = encubic.id_encomienda
	LEFT JOIN Mapas ON encubic.id_ubicacion = mapas.id_ubicacion
	WHERE 
		enc.id_encomienda = @id_encomienda
	
----------------------------------------------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------CertificadoSobrecarga--------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------------------------------
	SELECT
		sob.id_sobrecarga
		,sob.id_encomienda_datoslocal
		,sob.id_tipo_sobrecarga
		,sob.id_tipo_certificado
		,tc.descripcion AS tipo_certificado
		,ts.descripcion AS tipo_sobrecarga
	FROM Encomienda_DatosLocal dl
	INNER JOIN Encomienda_Certificado_Sobrecarga sob ON dl.id_encomiendadatoslocal = sob.id_encomienda_datoslocal
	INNER JOIN Encomienda_Tipos_Certificados_Sobrecarga tc ON tc.id_tipo_certificado = sob.id_tipo_certificado
	INNER JOIN Encomienda_Tipos_Sobrecargas ts ON ts.id_tipo_sobrecarga = sob.id_tipo_sobrecarga
	WHERE 
		dl.id_encomienda = @id_encomienda

	
----------------------------------------------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------SobrecargaDetalle1-----------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------------------------------
	SELECT
		sd1.id_sobrecarga_detalle1
		,sd1.id_sobrecarga
		,td.descripcion AS tipo_destino
		,tu.descripcion AS tipo_uso
		,sd1.valor
		,CASE WHEN tip_sec.id = 11 THEN ISNULL(ep.detalle_encomiendatiposector, '') ELSE tip_sec.Nombre + ' ' + ISNULL(ep.detalle_encomiendatiposector, '') END AS planta
		,sd1.losa_sobre
		,sd1.detalle
		,CASE WHEN sob.id_tipo_sobrecarga = 1 THEN 'Admite sobrecarga de [kg/m2]' ELSE 'Admite sobrecarga de [kN/m2]' END AS label_valor
	FROM Encomienda_DatosLocal dl
	INNER JOIN Encomienda_Certificado_Sobrecarga sob ON dl.id_encomiendadatoslocal = sob.id_encomienda_datoslocal
	INNER JOIN Encomienda_Sobrecarga_Detalle1 sd1 ON sd1.id_sobrecarga = sob.id_sobrecarga
	INNER JOIN Encomienda_Tipos_Destinos td ON td.id_tipo_destino = sd1.id_tipo_destino
	INNER JOIN Encomienda_Tipos_Usos tu ON tu.id_tipo_uso = sd1.id_tipo_uso
	INNER JOIN Encomienda_Plantas ep ON ep.id_encomiendatiposector = sd1.id_encomiendatiposector
	LEFT JOIN TipoSector tip_sec ON tip_sec.id = ep.id_tiposector
	WHERE 
		dl.id_encomienda = @id_encomienda

	
----------------------------------------------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------SobrecargaDetalle2-----------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------------------------------
	SELECT
		sd2.id_sobrecarga_detalle2
		,sd2.id_sobrecarga_detalle1
		,tu1.descripcion AS tipo_uso_1
		,sd2.valor_1
		,tu2.descripcion AS tipo_uso_2
		,sd2.valor_2
		,CASE WHEN sob.id_tipo_sobrecarga = 1 THEN 'Pasillos de acceso general, escaleras, balcones' ELSE 'Escaleras' END AS label_tipo_uso_1
		,CASE WHEN sob.id_tipo_sobrecarga = 1 THEN 'Barandilla de balcones y escaleras, esfuerzo horizontal dirigido al interior y aplicado sobre el pasamanos' ELSE 'Barandas' END AS label_tipo_uso_2
		,CASE WHEN sob.id_tipo_sobrecarga = 1 THEN 'Admite sobrecarga de [kg/m2]' ELSE 'Admite sobrecarga de [kN/m2]' END AS label_valor
	FROM Encomienda_DatosLocal dl
	INNER JOIN Encomienda_Certificado_Sobrecarga sob ON dl.id_encomiendadatoslocal = sob.id_encomienda_datoslocal
	INNER JOIN Encomienda_Sobrecarga_Detalle1 sd1 ON sd1.id_sobrecarga = sob.id_sobrecarga
	INNER JOIN Encomienda_Sobrecarga_Detalle2 sd2 ON sd2.id_sobrecarga_detalle1 = sd1.id_sobrecarga_detalle1
	LEFT JOIN Encomienda_Tipos_Usos tu1 ON tu1.id_tipo_uso = sd2.id_tipo_uso_1
	LEFT JOIN Encomienda_Tipos_Usos tu2 ON tu2.id_tipo_uso = sd2.id_tipo_uso_2
	WHERE 
		dl.id_encomienda = @id_encomienda
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[wsPagos_CancelarBoletas]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[wsPagos_CancelarBoletas]

go
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'wsPagos_CancelarBoletas')
DROP PROCEDURE wsPagos_CancelarBoletas

go
CREATE PROCEDURE [dbo].[wsPagos_CancelarBoletas] (@idPago AS INT, @Usuario AS VARCHAR(100))
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRANSACTION
		BEGIN TRY
				
			UPDATE bu
			SET 
				bu.EstadoPago_BU = (SELECT id_estadopago FROM wsPagos_BoletaUnica_Estados WHERE codigo_estadopago_BUI = 'Cancelada')
				,bu.FechaCancelado_BU = GETDATE()
				,bu.UpdateUser = @Usuario
			FROM 
				wsPagos_BoletaUnica bu
			WHERE 
				bu.id_pago = @idPago
		END TRY
		BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
		END CATCH
	IF @@TRANCOUNT > 0
		COMMIT TRANSACTION
END


go
IF NOT EXISTS(SELECT 1 FROM TipoEstadoSolicitud WHERE Id=39)
  INSERT INTO TipoEstadoSolicitud VALUES(39,'DATOSCONF','Datos Confirmados')



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SSIT_Solicitud_DireccionesPartidasPlancheta]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[SSIT_Solicitud_DireccionesPartidasPlancheta]
GO


CREATE FUNCTION [dbo].[SSIT_Solicitud_DireccionesPartidasPlancheta]
(
	@id_solicitud	int,
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
		SSIT_Solicitudes_Ubicaciones solubic
		INNER JOIN SSIT_Solicitudes_Ubicaciones_Puertas solpuer ON solubic.id_solicitudubicacion = solpuer.id_solicitudubicacion 
	WHERE
		solubic.id_solicitud = @id_solicitud
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
		@deptoLocal_ubicacion = deptoLocal_ubicacion,
		@id_tipoubicacion = tubic.id_tipoubicacion,
		@DescripcionUbicacionEspecial = tubic.descripcion_tipoubicacion + ' ' +	stubic.descripcion_subtipoubicacion  + IsNull(' Local ' + solubic.local_subtipoubicacion,'')
	FROM 
		SSIT_Solicitudes_Ubicaciones solubic
		INNER JOIN SubTiposDeUbicacion stubic ON solubic.id_subtipoubicacion = stubic.id_subtipoubicacion
		INNER JOIN TiposDeUbicacion tubic ON stubic.id_tipoubicacion = tubic.id_tipoubicacion
	WHERE 
		solubic.id_solicitud = @id_solicitud
		AND solubic.id_ubicacion = @id_ubicacion
	
	
	IF @id_tipoubicacion <> 0  -- Parcela Común
		SET @Result = @Result + IsNull(' ' + @DescripcionUbicacionEspecial ,'')
	
	
	IF LEN(IsNull(@deptoLocal_ubicacion,'')) > 0
		SET @Result = @Result + ' ' + @deptoLocal_ubicacion 
	

	-- Return the result of the function
	RETURN @Result

END

GO
/****** Object:  StoredProcedure [dbo].[SSIT_Imprimir_Solicitud]    Script Date: 10/05/2016 16:34:27 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SSIT_Imprimir_Solicitud]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SSIT_Imprimir_Solicitud]
GO

CREATE PROCEDURE [dbo].[SSIT_Imprimir_Solicitud]
	@id_solicitud INT
AS
BEGIN
	SET NOCOUNT ON;

----------------------------------------------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------Solicitudes-------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------------------------------
--	select * from SSIT_Solicitudes
	
	SELECT
		sol.id_solicitud
		,sol.CreateDate as FechaCreacion
		,sol.CodigoSeguridad
		,tt.cod_tipotramite AS TipoDeTramite
		,te.cod_tipoexpediente AS TipoDeExpediente
		,ste.cod_subtipoexpediente AS SubTipoDeExpediente
	FROM SSIT_Solicitudes sol
	LEFT JOIN tipotramite tt ON sol.id_tipotramite = tt.id_tipotramite
	LEFT JOIN tipoexpediente te ON sol.id_tipoexpediente = te.id_tipoexpediente
	LEFT JOIN subtipoexpediente ste ON sol.id_subtipoexpediente = ste.id_subtipoexpediente
	WHERE 
		sol.id_solicitud = @id_solicitud
----------------------------------------------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------Ubicaciones------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------------------------------
	SELECT
		solubic.id_solicitudubicacion
		,solubic.id_solicitud
		,solubic.id_ubicacion
		,mat.Seccion
		,mat.Manzana
		,mat.parcela
		,mat.NroPartidaMatriz
		,solubic.local_subtipoubicacion
		,zon1.codzonapla AS ZonaParcela
		,dbo.SSIT_Solicitud_DireccionesPartidasPlancheta(solubic.id_solicitud, solubic.id_ubicacion) AS Direcciones
		,solubic.deptoLocal_ubicacion AS DeptoLocal
	FROM SSIT_Solicitudes_Ubicaciones solubic
	INNER JOIN Ubicaciones mat ON solubic.id_ubicacion = mat.id_ubicacion
	INNER JOIN Zonas_Planeamiento zon1 ON solubic.id_zonaplaneamiento = zon1.id_zonaplaneamiento
	WHERE 
		solubic.id_solicitud = @id_solicitud
	ORDER BY solubic.id_solicitudubicacion

----------------------------------------------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------PropiedadHorizontal----------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------------------------------
	SELECT
		solubic.id_solicitudubicacion
		,solubic.id_ubicacion
		,phor.NroPartidaHorizontal AS NroPartidaHorizontal
		,phor.piso
		,phor.depto
	FROM SSIT_Solicitudes_Ubicaciones solubic
	INNER JOIN SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal solphor ON solubic.id_solicitudubicacion = solphor.id_solicitudubicacion
	INNER JOIN Ubicaciones_PropiedadHorizontal phor ON solphor.id_propiedadhorizontal = phor.id_propiedadhorizontal
	WHERE 
		solubic.id_solicitud = @id_solicitud
	ORDER BY solubic.id_solicitudubicacion

----------------------------------------------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------Puertas----------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------------------------------
	SELECT
		solubic.id_solicitudubicacion
		,solpuer.id_solicitudpuerta
		,solubic.id_solicitud
		,solubic.id_ubicacion
		,solpuer.nombre_calle AS Calle
		,solpuer.NroPuerta
	FROM SSIT_Solicitudes_Ubicaciones solubic
	INNER JOIN SSIT_Solicitudes_Ubicaciones_Puertas solpuer ON solubic.id_solicitudubicacion = solpuer.id_solicitudubicacion
	WHERE 
		solubic.id_solicitud = @id_solicitud
	ORDER BY solubic.id_solicitudubicacion

----------------------------------------------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------Firmantes--------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------------------------------
	SELECT
		pj.id_firmante_pj AS id_firmante
		,@id_solicitud AS id_solicitud
		,'PJ' AS TipoPersona
		,UPPER(pj.Apellido) AS Apellido
		,UPPER(pj.Nombres) AS Nombres
		,tdoc.nombre AS TipoDoc
		,pj.Nro_Documento AS NroDoc
		,tcl.nom_tipocaracter AS CaracterLegal
		,pj.Email
		,pj.cargo_firmante_pj
		,UPPER(titpj.Razon_Social) AS FirmanteDe
	FROM SSIT_Solicitudes_Firmantes_PersonasJuridicas pj
	INNER JOIN SSIT_Solicitudes_Titulares_PersonasJuridicas titpj ON pj.id_personajuridica = titpj.id_personajuridica
	INNER JOIN tiposdecaracterlegal tcl ON pj.id_tipocaracter = tcl.id_tipocaracter
	INNER JOIN tipodocumentopersonal tdoc ON pj.id_tipodoc_personal = tdoc.tipodocumentopersonalId
	WHERE 
		pj.id_solicitud = @id_solicitud
		AND titpj.Id_TipoSociedad NOT IN (2, 32)
	UNION ALL
	SELECT
		pj.id_firmante_pj AS id_firmante
		,@id_solicitud AS id_solicitud
		,'PJ' AS TipoPersona
		,UPPER(pj.Apellido) AS Apellido
		,UPPER(pj.Nombres) AS Nombres
		,tdoc.nombre AS TipoDoc
		,pj.Nro_Documento AS NroDoc
		,tcl.nom_tipocaracter AS CaracterLegal
		,pj.Email
		,pj.cargo_firmante_pj
		,UPPER((SELECT titsh.Apellido + ' ' + titsh.Nombres
			FROM Encomienda_Titulares_PersonasJuridicas_PersonasFisicas titsh
			WHERE titsh.id_firmante_pj = pj.id_firmante_pj)) AS FirmanteDe
	FROM SSIT_Solicitudes_Firmantes_PersonasJuridicas pj
	INNER JOIN SSIT_Solicitudes_Titulares_PersonasJuridicas titpj ON pj.id_personajuridica = titpj.id_personajuridica
	INNER JOIN tiposdecaracterlegal tcl ON pj.id_tipocaracter = tcl.id_tipocaracter
	INNER JOIN tipodocumentopersonal tdoc ON pj.id_tipodoc_personal = tdoc.tipodocumentopersonalId
	WHERE
		pj.id_solicitud = @id_solicitud
		AND titpj.Id_TipoSociedad IN (2, 32)
	UNION ALL
	SELECT
		pf.id_firmante_pf AS id_firmante
		,@id_solicitud AS id_solicitud
		,'PF' AS TipoPersona
		,UPPER(pf.Apellido) AS Apellido
		,UPPER(pf.Nombres) AS Nombres
		,tdoc.nombre AS TipoDoc
		,pf.Nro_Documento AS NroDoc
		,tcl.nom_tipocaracter AS CaracterLegal
		,pf.Email
		,'' AS cargo_firmante_pj
		,UPPER(titpf.Apellido + ', ' + titpf.Nombres) AS FirmanteDe
	FROM SSIT_Solicitudes_Firmantes_PersonasFisicas pf
	INNER JOIN SSIT_Solicitudes_Titulares_PersonasFisicas titpf ON pf.id_personafisica = titpf.id_personafisica
	INNER JOIN tiposdecaracterlegal tcl ON pf.id_tipocaracter = tcl.id_tipocaracter
	INNER JOIN tipodocumentopersonal tdoc ON pf.id_tipodoc_personal = tdoc.tipodocumentopersonalId
	WHERE 
		pf.id_solicitud = @id_solicitud

----------------------------------------------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------Titulares--------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------------------------------
	SELECT
		@id_solicitud AS id_persona
		,@id_solicitud AS id_solicitud
		,'PJ' AS TipoPersona
		,UPPER(pj.Razon_Social) AS RazonSocial
		,tsoc.descripcion AS TipoSociedad
		,'' AS Apellido
		,'' AS Nombres
		,'' AS TipoDoc
		,'' AS NroDoc
		,tipoiibb.nom_tipoiibb AS TipoIIBB
		,pj.Nro_IIBB AS NroIIBB
		,pj.cuit
		,1 AS MuestraEnTitulares
		,CASE WHEN pj.id_tiposociedad IN (2, 32) THEN 0 ELSE 1 END AS MuestraEnPlancheta
	FROM SSIT_Solicitudes_Titulares_PersonasJuridicas pj
	INNER JOIN TipoSociedad tsoc ON pj.id_tiposociedad = tsoc.id
	INNER JOIN TiposDeIngresosBrutos tipoiibb ON pj.id_tipoiibb = tipoiibb.id_tipoiibb
	WHERE 
		pj.id_solicitud = @id_solicitud
	UNION
	SELECT
		@id_solicitud AS id_persona
		,@id_solicitud AS id_solicitud
		,'PF' AS TipoPersona
		,'' AS RazonSocial
		,'' AS TipoSociedad
		,UPPER(pj.Apellido) AS Apellido
		,UPPER(pj.Nombres) AS Nombres
		,tdoc.nombre AS TipoDoc
		,pj.Nro_Documento AS NroDoc
		,'' AS TipoIIBB
		,'' AS NroIIBB
		,'' AS cuit
		,0 AS MuestraEnTitulares
		,1 AS MuestraEnPlancheta
	FROM SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas pj
	INNER JOIN SSIT_Solicitudes_Titulares_PersonasJuridicas titpj ON pj.id_personajuridica = titpj.id_personajuridica
	INNER JOIN tipodocumentopersonal tdoc ON pj.id_tipodoc_personal = tdoc.tipodocumentopersonalId
	WHERE 
		titpj.id_solicitud = @id_solicitud
		AND titpj.Id_TipoSociedad IN (2, 32)
	UNION
	SELECT
		@id_solicitud AS id_persona
		,@id_solicitud AS id_solicitud
		,'PF' AS TipoPersona
		,'' AS RazonSocial
		,'' AS TipoSociedad
		,UPPER(pf.Apellido) AS Apellido
		,UPPER(pf.Nombres) AS Nombres
		,tdoc.nombre AS TipoDoc
		,pf.Nro_Documento AS NroDoc
		,tipoiibb.nom_tipoiibb AS TipoIIBB
		,pf.Ingresos_Brutos AS NroIIBB
		,pf.cuit
		,1 AS MuestraEnTitulares
		,1 AS MuestraEnPlancheta
	FROM SSIT_Solicitudes_Titulares_PersonasFisicas pf
	INNER JOIN tipodocumentopersonal tdoc ON pf.id_tipodoc_personal = tdoc.tipodocumentopersonalId
	INNER JOIN TiposDeIngresosBrutos tipoiibb ON pf.id_tipoiibb = tipoiibb.id_tipoiibb
	WHERE 
		pf.id_solicitud = @id_solicitud
	
END
GO



   
IF NOT EXISTS (SELECT * FROM [dbo].[Emails_Origenes] WHERE descripcion = 'SSIT' AND id_origen = 15) 
BEGIN
print 'Creacion [Emails_Origenes]'
	INSERT INTO [dbo].[Emails_Origenes] (
							id_origen
							,descripcion
							,cfg_mail_from
							,id_email_template
							,cfg_smtp
							,cfg_smpt_puerto)
	VALUES (15, 'SSIT', 'noreply@buenosaires.gob.ar', 1, '10.20.1.103', 25)
END
GO

IF NOT EXISTS (SELECT * FROM [dbo].[Emails_Tipos] WHERE descripcion = 'Creación de usuario' AND id_tipo_email = 13) BEGIN
	INSERT INTO [dbo].[Emails_Tipos] (id_tipo_email
							,descripcion)
	VALUES (13, 'Creación de usuario')
END
GO
IF NOT EXISTS (SELECT * FROM [dbo].[Emails_Tipos] WHERE descripcion = 'Recupero de contraseña' AND id_tipo_email = 14) BEGIN
	INSERT INTO [dbo].[Emails_Tipos] (id_tipo_email
							,descripcion)
	VALUES (14, 'Recupero de contraseña')
END
GO

/*Alter encomienda*/
IF NOT EXISTS(SELECT TOP 1 1 FROM sys.COLUMNS where name='tipo_anexo' AND object_id = OBJECT_ID(N'[dbo].[Encomienda]'))
ALTER TABLE dbo.Encomienda ADD tipo_anexo nvarchar(1) NULL
GO

IF NOT EXISTS(SELECT 1 FROM [TiposDeDocumentosSistema] WHERE id_tipdocsis=18)
 INSERT INTO [TiposDeDocumentosSistema] VALUES (18,'DOC_ADJUNTO_SSIT','Documento Adjunto de SSIT', GETDATE())
GO

IF NOT EXISTS(SELECT 1 FROM [TiposDeDocumentosSistema] WHERE id_tipdocsis=19)
 INSERT INTO [TiposDeDocumentosSistema] VALUES (19,'DOC_ADJUNTO_ENCOMIENDA','Documento Adjunto de Encomienda', GETDATE())
GO



IF NOT  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CPadron_Titulares_PersonasFisicas_CPadron_Solicitudes]') AND parent_object_id = OBJECT_ID(N'[dbo].[CPadron_Titulares_PersonasFisicas]'))
BEGIN
print 'Alter taBLE CPadron_Titulares_PersonasFisicas]'
ALTER TABLE [dbo].[CPadron_Titulares_PersonasFisicas]  WITH CHECK ADD  CONSTRAINT [FK_CPadron_Titulares_PersonasFisicas_CPadron_Solicitudes] FOREIGN KEY([id_cpadron])
REFERENCES [dbo].[CPadron_Solicitudes] ([id_cpadron])

ALTER TABLE [dbo].[CPadron_Titulares_PersonasFisicas] CHECK CONSTRAINT [FK_CPadron_Titulares_PersonasFisicas_CPadron_Solicitudes]
END

GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CPadron_Titulares_PersonasJuridicas_CPadron_Solicitudes]') AND parent_object_id = OBJECT_ID(N'[dbo].[CPadron_Titulares_PersonasJuridicas]'))
BEGIN
print 'Alter taBLE CPadron_Titulares_PersonasjURIDICAS]'
ALTER TABLE [dbo].[CPadron_Titulares_PersonasJuridicas]  WITH CHECK ADD  CONSTRAINT [FK_CPadron_Titulares_PersonasJuridicas_CPadron_Solicitudes] FOREIGN KEY([id_cpadron])
REFERENCES [dbo].[CPadron_Solicitudes] ([id_cpadron])

ALTER TABLE [dbo].[CPadron_Titulares_PersonasJuridicas] CHECK CONSTRAINT [FK_CPadron_Titulares_PersonasJuridicas_CPadron_Solicitudes]
END


/*Insert data section*/

Print 'Insert Section'
IF NOT EXISTS (SELECT * FROM TiposDeDocumentosSistema WHERE id_tipdocsis = 20) BEGIN
INSERT INTO TiposDeDocumentosSistema
VALUES (20, 'SOLICITUD_HABILITACION', 'Solicitud Habilitacion', GETDATE())
END

go
IF NOT EXISTS (SELECT * FROM Emails_Tipos WHERE descripcion LIKE 'Anulaci%n de Anexo T%cnico') 
BEGIN
print 'Insercion de datos en Emails_Tipos Value: Anulación de Anexo Técnico '
DECLARE @IDTipoMail INT
SELECT @IDTipoMail = MAX(id_tipo_email) + 1 FROM emails_tipos

INSERT INTO Emails_Tipos
VALUES (@IDTipoMail, 'Anulación de Anexo Técnico')
END 

go
IF NOT EXISTS (SELECT * FROM TiposDeDocumentosSistema WHERE id_tipdocsis = 20) BEGIN
INSERT INTO TiposDeDocumentosSistema
VALUES (20, 'SOLICITUD_HABILITACION', 'Solicitud Habilitacion', GETDATE())
END

go
update TiposDeIngresosBrutos
set formato_tipoiibb = '99999999999' where cod_tipoibb = 'ISIB'

go
update TiposDeCaracterLegal set disponibilidad_tipocaracter = 1 WHERE cod_tipocaracter = 'A'





IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[wsPagos_BoletaUnica_insertar]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[wsPagos_BoletaUnica_insertar]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[wsPagos_BoletaUnica_insertar]
(
	@id_pago int
	,@BUI_ID uniqueidentifier
	,@BUI_Numero nvarchar(50)
	,@Monto_BU money
	,@UpdateUser nvarchar(100)
	,@id_pago_BU int output
)
AS
BEGIN
	
	DECLARE @Numero_BU bigint
	DECLARE @NroDependencia INT
	SET @Numero_BU  = CONVERT(bigint, SUBSTRING(@BUI_Numero,6,10))

	SET @NroDependencia = LEFT(@BUI_Numero, 4)
	
	EXEC @id_pago_BU = Id_Nuevo 'wsPagos_BoletaUnica'
	INSERT INTO wsPagos_BoletaUnica(
		id_pago_BU 
		,id_pago 
		,Numero_BU 
		,Monto_BU 
		,FechaPago_BU 
		,TrazaPago_BU 
		,EstadoPago_BU
		,BUI_ID
		,BUI_Numero
		,UpdateUser
		,NroDependencia_BU
	)
	VALUES
	(
		@id_pago_BU 
		,@id_pago 
		,@Numero_BU 
		,@Monto_BU 
		,NULL 
		,NULL
		,0			-- Sin Pagar
		,@BUI_ID
		,@BUI_Numero
		,@UpdateUser
		,@NroDependencia
	)

	
END


GO


UPDATE wsPagos_BoletaUnica
SET NroDependencia_BU = LEFT(BUI_Numero, 4)
WHERE BUI_Numero IS NOT NULL
--r
GO

update TiposDeIngresosBrutos set formato_tipoiibb = '99999999999' where cod_tipoibb = 'ISIB'

go

IF NOT EXISTS(SELECT 1 FROM [TiposDeDocumentosSistema] where id_tipdocsis=21)
	INSERT INTO [dbo].[TiposDeDocumentosSistema] VALUES(21,'PLANCHETA_HABILITACION','Plancheta de Habilitación',GETDATE())
GO
IF NOT EXISTS(SELECT 1 FROM [TiposDeDocumentosSistema] where id_tipdocsis=22)
	INSERT INTO [dbo].[TiposDeDocumentosSistema] VALUES(22,'CERTIF_CONSEJO_HABILITACION','Certificación del consejo',GETDATE())
GO

if not exists (select * from [dbo].[TiposDedocumentosSistema] where cod_tipodocsis = 'SOLICITUD_CPADRON' and id_tipdocsis = 22)
BEGIN
DECLARE @id_nuevo int
EXEC @id_nuevo = Id_Nuevo 'TiposDedocumentosSistema'
   INSERT INTO [dbo].[TiposDedocumentosSistema] (
   	id_tipdocsis
   	,cod_tipodocsis
   	,nombre_tipodocsis
   	,CreateDate
   	) 
   VALUES ( 
   	@id_nuevo
   	,'SOLICITUD_CPADRON'
   	,'Solicitud de CPadron'
   	,GETDATE()
   	) 
END
go


/****** Script for SelectTopNRows command from SSMS  ******/
 if exists (SELECT *  FROM [dbo].[TiposDeIngresosBrutos] where id_tipoiibb = 3)
 Begin 
  UPDATE [TiposDeIngresosBrutos]
  
   SET formato_tipoiibb = '99999999999'
   WHERE id_tipoiibb = 3
    END
GO
  if exists (SELECT *  FROM [dbo].[TiposDeIngresosBrutos] where id_tipoiibb = 2)
   Begin 
  UPDATE [TiposDeIngresosBrutos]
  
   SET formato_tipoiibb = '99999999999'
   WHERE id_tipoiibb = 2
       END
GO


/*Agrega campo telefono a titulares y elimina los otros 3 y actualiza el mismo campo con los datos */
GO

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'Telefono' AND Object_ID = Object_ID(N'CPadron_Titulares_Solicitud_PersonasFisicas')) BEGIN

		ALTER TABLE dbo.CPadron_Titulares_Solicitud_PersonasFisicas ADD
			Telefono nvarchar(50) NULL

		ALTER TABLE dbo.CPadron_Titulares_Solicitud_PersonasFisicas SET (LOCK_ESCALATION = TABLE)
		DECLARE @Query AS VARCHAR(MAX) = 'UPDATE CPadron_Titulares_Solicitud_PersonasFisicas 
		SET Telefono = LTRIM(RTRIM(TelefonoArea)) + LTRIM(RTRIM(TelefonoPrefijo)) + LTRIM(RTRIM(TelefonoSufijo))'
		EXEC (@Query)
		ALTER TABLE dbo.CPadron_Titulares_Solicitud_PersonasFisicas
			DROP COLUMN TelefonoArea, TelefonoPrefijo, TelefonoSufijo
END
GO
IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'Telefono' AND Object_ID = Object_ID(N'Encomienda_Titulares_PersonasFisicas')) BEGIN

		ALTER TABLE dbo.Encomienda_Titulares_PersonasFisicas ADD
			Telefono nvarchar(50) NULL

		ALTER TABLE dbo.Encomienda_Titulares_PersonasFisicas SET (LOCK_ESCALATION = TABLE)
		DECLARE @Query AS VARCHAR(MAX) = 'UPDATE Encomienda_Titulares_PersonasFisicas 
		SET Telefono = LTRIM(RTRIM(TelefonoArea)) + LTRIM(RTRIM(TelefonoPrefijo)) + LTRIM(RTRIM(TelefonoSufijo))'
		EXEC (@Query)
		ALTER TABLE dbo.Encomienda_Titulares_PersonasFisicas
			DROP COLUMN TelefonoArea, TelefonoPrefijo, TelefonoSufijo
END
GO
IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'Telefono' AND Object_ID = Object_ID(N'SSIT_Solicitudes_Titulares_PersonasFisicas')) BEGIN
		ALTER TABLE dbo.SSIT_Solicitudes_Titulares_PersonasFisicas ADD
			Telefono nvarchar(50) NULL

		ALTER TABLE dbo.SSIT_Solicitudes_Titulares_PersonasFisicas SET (LOCK_ESCALATION = TABLE)
		DECLARE @Query AS VARCHAR(MAX) = 'UPDATE SSIT_Solicitudes_Titulares_PersonasFisicas 
		SET Telefono = LTRIM(RTRIM(TelefonoArea)) + LTRIM(RTRIM(TelefonoPrefijo)) + LTRIM(RTRIM(TelefonoSufijo))'

		EXEC (@Query)
		ALTER TABLE dbo.SSIT_Solicitudes_Titulares_PersonasFisicas
			DROP COLUMN TelefonoArea, TelefonoPrefijo, TelefonoSufijo
END
GO
IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'Telefono' AND Object_ID = Object_ID(N'CPadron_Titulares_PersonasFisicas')) BEGIN
		ALTER TABLE dbo.CPadron_Titulares_PersonasFisicas ADD
			Telefono nvarchar(50) NULL

		ALTER TABLE dbo.CPadron_Titulares_PersonasFisicas SET (LOCK_ESCALATION = TABLE)
		DECLARE @Query AS VARCHAR(MAX) = 'UPDATE CPadron_Titulares_PersonasFisicas 
		SET Telefono = LTRIM(RTRIM(TelefonoArea)) + LTRIM(RTRIM(TelefonoPrefijo)) + LTRIM(RTRIM(TelefonoSufijo))'

		EXEC (@Query)
		ALTER TABLE dbo.CPadron_Titulares_PersonasFisicas
			DROP COLUMN TelefonoArea, TelefonoPrefijo, TelefonoSufijo
END

/*Agregar campo booleano servidumbre de paso*/
GO

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'Servidumbre_paso' AND Object_ID = Object_ID(N'Encomienda')) BEGIN
ALTER TABLE dbo.Encomienda ADD
	Servidumbre_paso bit NOT NULL DEFAULT 0
ALTER TABLE dbo.Encomienda SET (LOCK_ESCALATION = TABLE)
END


IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'Servidumbre_paso' AND Object_ID = Object_ID(N'SSIT_Solicitudes')) BEGIN
ALTER TABLE dbo.SSIT_Solicitudes ADD
	Servidumbre_paso bit NOT NULL DEFAULT 0
ALTER TABLE dbo.SSIT_Solicitudes SET (LOCK_ESCALATION = TABLE)
END

/*Consulta de Escribanos para Reporte*/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SSIT_Consultar_Escribanos]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SSIT_Consultar_Escribanos]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE  [dbo].[SSIT_Consultar_Escribanos]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT [ApyNom],[Email],[Matricula],[Telefono], (select COUNT(*) from [Escribano]) as Id
	FROM [Escribano] order by [ApyNom]			
END

GO


/****** Object:  Trigger [AprobarUsuario]    Script Date: 12/06/2016 11:16:02 ******/
IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[AprobarUsuario]'))
DROP TRIGGER [dbo].[AprobarUsuario]
GO

/*SP SSIT_Consultar_Profesional*/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SSIT_Consultar_Profesional]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SSIT_Consultar_Profesional]
GO

CREATE PROCEDURE [dbo].[SSIT_Consultar_Profesional] as BEGIN

SELECT
	pro.Id	AS [Id]
	,pro.Email as [Email]
	,tipodoc.Nombre	AS [TipoDoc]
	,CASE 
		WHEN pro.Cuit IS NULL THEN ''
		ELSE LTRIM(RTRIM(pro.Cuit))
	END					AS [CUIT]
	,pro.Matricula		AS [Matricula]
	,CASE
		WHEN pro.Apellido IS NULL THEN ''
		ELSE LTRIM(RTRIM(pro.Apellido))
	END + ', ' +
	CASE
		WHEN pro.Nombre IS NULL THEN ''
		ELSE LTRIM(RTRIM(pro.Nombre))
	END		AS [Nombre]
	,CASE
		WHEN pro.Calle IS NULL THEN ''
		ELSE LTRIM(RTRIM(pro.calle))
	END + ' ' +
	CASE
		WHEN pro.NroPuerta IS NULL OR pro.NroPuerta = 0 THEN ''
		ELSE LTRIM(RTRIM(REPLACE(pro.NroPuerta, '"', '')))
	END + ' ' +
	CASE
		WHEN pro.Piso IS NULL OR pro.Piso = '' OR pro.Depto IS NULL OR pro.Depto = '' THEN ''
		ELSE 'Piso ' + LTRIM(RTRIM(pro.Piso)) + ' "' + LTRIM(RTRIM(pro.Depto)) + '"'
	END								AS [Direccion]
	,LTRIM(RTRIM(pro.Telefono))		AS [Telefono]
	,grucon.nombre_grupoconsejo		AS [Grupo]
	,grucon.descripcion_grupoconsejo	AS [DescGrupo]
FROM 
	Profesional pro
	LEFT JOIN TipoDocumentoPersonal tipodoc ON pro.IdTipoDocumento = tipodoc.TipoDocumentoPersonalId
	INNER JOIN aspnet_Users usu ON pro.UserId = usu.UserId
	INNER JOIN ConsejoProfesional con ON pro.IdConsejo = con.Id
	INNER JOIN aspnet_Applications app ON usu.ApplicationId = app.ApplicationId
	INNER JOIN ConsejoProfesional_RolesPermitidos rolper ON con.id_grupoconsejo = rolper.id_grupoconsejo
	INNER JOIN aspnet_Roles rol ON rol.RoleId = rolper.RoleID AND rol.ApplicationId = app.ApplicationId
	INNER JOIN aspnet_UsersInRoles usurol ON usurol.UserId = usu.UserId AND usurol.RoleId = rol.RoleId
	INNER JOIN GrupoConsejos grucon ON con.id_grupoconsejo = grucon.id_grupoconsejo
WHERE 
	con.Nombre NOT IN ('CPIAYE', 'COPITEC', 'CPIN', 'CPIQ')
	AND rol.RoleName = 'EncomiendaHabilitaciones'
	AND pro.BajaLogica = 0
END

GO
