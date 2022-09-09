IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Rel_TiposDeDocumentosRequeridos_TipoNormativa]') AND type in (N'U'))
BEGIN
	/****** Object:  Table [dbo].[Rel_TiposDeDocumentosRequeridos_TipoNormativa]    Script Date: 11/16/2016 10:00:39 ******/
	SET ANSI_NULLS ON

	SET QUOTED_IDENTIFIER ON

	CREATE TABLE [dbo].[Rel_TiposDeDocumentosRequeridos_TipoNormativa](
		[id_tdoc_tnor] [int] NOT NULL,
		[id_tdocreq] [int] NOT NULL,
		[id_tnor] [int] NOT NULL,
	 CONSTRAINT [PK_Rel_TiposDeDocumentosRequeridos_TipoNormativa] PRIMARY KEY CLUSTERED 
	(
		[id_tdoc_tnor] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]


	ALTER TABLE [dbo].[Rel_TiposDeDocumentosRequeridos_TipoNormativa]  WITH CHECK ADD  CONSTRAINT [FK_Rel_TiposDeDocumentosRequeridos_TipoNormativa_TipoNormativa] FOREIGN KEY([id_tnor])
	REFERENCES [dbo].[TipoNormativa] ([Id])

	ALTER TABLE [dbo].[Rel_TiposDeDocumentosRequeridos_TipoNormativa] CHECK CONSTRAINT [FK_Rel_TiposDeDocumentosRequeridos_TipoNormativa_TipoNormativa]

	ALTER TABLE [dbo].[Rel_TiposDeDocumentosRequeridos_TipoNormativa]  WITH CHECK ADD  CONSTRAINT [FK_Rel_TiposDeDocumentosRequeridos_TipoNormativa_TiposDeDocumentosRequeridos] FOREIGN KEY([id_tdocreq])
	REFERENCES [dbo].[TiposDeDocumentosRequeridos] ([id_tdocreq])

	ALTER TABLE [dbo].[Rel_TiposDeDocumentosRequeridos_TipoNormativa] CHECK CONSTRAINT [FK_Rel_TiposDeDocumentosRequeridos_TipoNormativa_TiposDeDocumentosRequeridos]
END
GO

IF  EXISTS (SELECT TOP 1 1 FROM sys.COLUMNS where name='id_tdocreq' AND object_id = OBJECT_ID(N'[dbo].[TipoNormativa]'))
BEGIN
	ALTER TABLE [dbo].[TipoNormativa] DROP CONSTRAINT FK_TipoNormativa_TiposDeDocumentosRequeridos
	ALTER TABLE [dbo].[TipoNormativa] DROP COLUMN [id_tdocreq]
END

IF NOT EXISTS(SELECT 1 FROM Rel_TiposDeDocumentosRequeridos_TipoNormativa)
BEGIN
	INSERT INTO Rel_TiposDeDocumentosRequeridos_TipoNormativa VALUES 
	(1,35,1),
	(2,76,2),
	(3,43,3),
	(4,36,3),
	(5,77,4),
	(6,78,5),
	(7,79,6)
END
