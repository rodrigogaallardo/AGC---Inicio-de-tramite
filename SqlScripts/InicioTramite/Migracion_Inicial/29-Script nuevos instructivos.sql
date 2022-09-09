DECLARE @DataBase VARCHAR(MAX) = 'DGHP_Solicitudes'
DECLARE @DataBaseFile VARCHAR(MAX) = 'AGC_Files'
DECLARE @Path_HAB VARCHAR(MAX), @Path_AT VARCHAR(MAX), @Path_CPARON VARCHAR(MAX), @Path_TRANSF VARCHAR(MAX), @Query VARCHAR(MAX)

SET @Path_HAB = 'C:\pdfs\Instructivo_SSIT_HAB.pdf'
SET @Path_AT = 'C:\pdfs\Instructivo_AT.pdf'
SET @Path_CPARON='C:\pdfs\Instructivo_SSIT_CPADRON.pdf'
SET @Path_TRANSF = 'C:\pdfs\Instructivo_SSIT_TRANSF.pdf'

SET @Query =
'DECLARE @archivo varbinary(max), @id_file INT, @IDNuevo INT

SELECT @archivo = BulkColumn FROM OPENROWSET(BULK N''' + @Path_HAB + ''' , SINGLE_BLOB) AS Document
EXEC [' + @DataBaseFile + '].[dbo].[Files_Agregar] @archivo, ''digsis'', @id_file OUT
UPDATE [' + @DataBaseFile + '].[dbo].[Files] SET FileName = ''Instructivo_SSIT_HAB.pdf'' WHERE id_file = @id_file

IF NOT EXISTS(SELECT * FROM ' + @DataBase + '.dbo.Instructivos WHERE cod_instructivo = ''DGHyP_Habilitaciones'') BEGIN
	EXEC @IDNuevo = ' + @DataBase + '.dbo.Id_Nuevo ''Instructivos''
	INSERT INTO ' + @DataBase + '.dbo.Instructivos 
	VALUES (@IDNuevo, ''DGHyP_Habilitaciones'', @id_file, GETDATE(), ''A153211F-CCF4-4E86-BB90-C8D030974DD9'', NULL, NULL)
END ELSE BEGIN 
	UPDATE [' + @DataBase + '].[dbo].[Instructivos] SET id_file = @id_file, LastUpdateDate = GETDATE(), LastUpdateUser = ''A153211F-CCF4-4E86-BB90-C8D030974DD9'' WHERE cod_instructivo = ''DGHyP_Habilitaciones''
END

SELECT @archivo = BulkColumn FROM OPENROWSET(BULK N''' + @Path_AT + ''' , SINGLE_BLOB) AS Document
EXEC [' + @DataBaseFile + '].[dbo].[Files_Agregar] @archivo, ''digsis'', @id_file OUT
UPDATE [' + @DataBaseFile + '].[dbo].[Files] SET FileName = ''Instructivo_AT.pdf'' WHERE id_file = @id_file

IF NOT EXISTS(SELECT * FROM ' + @DataBase + '.dbo.Instructivos WHERE cod_instructivo = ''DGHyP_Anexo_Tecnico'') BEGIN
	EXEC @IDNuevo = Id_Nuevo ''Instructivos''
	INSERT INTO ' + @DataBase + '.dbo.Instructivos 
	VALUES (@IDNuevo, ''DGHyP_Anexo_Tecnico'', @id_file, GETDATE(), ''A153211F-CCF4-4E86-BB90-C8D030974DD9'', NULL, NULL)
END ELSE BEGIN 
	UPDATE [' + @DataBase + '].[dbo].[Instructivos] SET id_file = @id_file, LastUpdateDate = GETDATE(), LastUpdateUser = ''A153211F-CCF4-4E86-BB90-C8D030974DD9'' WHERE cod_instructivo = ''DGHyP_Anexo_Tecnico''
END

SELECT @archivo = BulkColumn FROM OPENROWSET(BULK N''' + @Path_CPARON + ''' , SINGLE_BLOB) AS Document
EXEC [' + @DataBaseFile + '].[dbo].[Files_Agregar] @archivo, ''digsis'', @id_file OUT
UPDATE [' + @DataBaseFile + '].[dbo].[Files] SET FileName = ''Instructivo_SSIT_CPADRON.pdf'' WHERE id_file = @id_file

IF NOT EXISTS(SELECT * FROM ' + @DataBase + '.dbo.Instructivos WHERE cod_instructivo = ''DGHyP_Consulta_Padron'') BEGIN
	EXEC @IDNuevo = Id_Nuevo ''Instructivos''
	INSERT INTO ' + @DataBase + '.dbo.Instructivos 
	VALUES (@IDNuevo, ''DGHyP_Consulta_Padron'', @id_file, GETDATE(), ''A153211F-CCF4-4E86-BB90-C8D030974DD9'', NULL, NULL)
END ELSE BEGIN 
	UPDATE [' + @DataBase + '].[dbo].[Instructivos] SET id_file = @id_file, LastUpdateDate = GETDATE(), LastUpdateUser = ''A153211F-CCF4-4E86-BB90-C8D030974DD9'' WHERE cod_instructivo = ''DGHyP_Consulta_Padron''
END

SELECT @archivo = BulkColumn FROM OPENROWSET(BULK N''' + @Path_TRANSF + ''' , SINGLE_BLOB) AS Document
EXEC [' + @DataBaseFile + '].[dbo].[Files_Agregar] @archivo, ''digsis'', @id_file OUT
UPDATE [' + @DataBaseFile + '].[dbo].[Files] SET FileName = ''Instructivo_SSIT_TRANSF.pdf'' WHERE id_file = @id_file

IF NOT EXISTS(SELECT * FROM ' + @DataBase + '.dbo.Instructivos WHERE cod_instructivo = ''DGHyP_Transferencias'') BEGIN
	EXEC @IDNuevo = Id_Nuevo ''Instructivos''
	INSERT INTO ' + @DataBase + '.dbo.Instructivos 
	VALUES (@IDNuevo, ''DGHyP_Transferencias'', @id_file, GETDATE(), ''A153211F-CCF4-4E86-BB90-C8D030974DD9'', NULL, NULL)
END ELSE BEGIN 
	UPDATE [' + @DataBase + '].[dbo].[Instructivos] SET id_file = @id_file, LastUpdateDate = GETDATE(), LastUpdateUser = ''A153211F-CCF4-4E86-BB90-C8D030974DD9'' WHERE cod_instructivo = ''DGHyP_Transferencias''
END
'

EXEC (@Query)