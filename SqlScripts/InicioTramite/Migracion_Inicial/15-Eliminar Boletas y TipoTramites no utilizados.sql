DECLARE 
	@SQL nvarchar(4000)

SET @SQL = N'
use tempdb

dbcc shrinkfile (tempdev, 20)

dbcc shrinkfile (templog, 10)

'
EXECUTE sp_executesql @SQL
GO
DELETE FROM Certificados WHERE TipoTramite = 7
DELETE FROM Rel_TipoTramite_Roles WHERE TipoTramite IN(1,2,5,6,7,8)
DELETE FROM TipoTramiteCertificados WHERE TipoTramite IN(1,2,5,6,7,8)

IF  EXISTS (SELECT * FROM AGC_FILES.sys.objects WHERE name = 'Encomienda_Planos')
BEGIN
	DROP TABLE AGC_Files.dbo.Encomienda_Planos
END
GO