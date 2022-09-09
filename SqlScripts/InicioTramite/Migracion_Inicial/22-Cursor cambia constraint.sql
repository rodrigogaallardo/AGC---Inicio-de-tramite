
DECLARE 
	@SQL nvarchar(4000)

SET @SQL = N'
use tempdb

dbcc shrinkfile (tempdev, 20)

dbcc shrinkfile (templog, 10)

'
EXECUTE sp_executesql @SQL
GO
/**********************************************************************************************************************/
			     /*Create By Demian Change Name of constraint Names for status  > 5000*/
/**********************************************************************************************************************/
DECLARE @constraint_name VARCHAR(200) -- path for backup files  
DECLARE @tableName VARCHAR(256) -- filename for backup  
DECLARE @columnName VARCHAR(20) -- used for file name 
DECLARE @constraintNEWname VARCHAR(200) -- path for backup files  

DECLARE db_cursor CURSOR FOR 
select oc.name as constraint_name, o.name as Table_name,sc.name as Column_name
from sys.sysconstraints c INNER JOIN sysobjects o ON c.id = o.id
INNER JOIN sysobjects oc ON c.constid = oc.id
INNER JOIN sys.columns sc ON  o.id = sc.object_id AND c.colid = sc.column_id
WHERE 
c.status > 5000 

OPEN db_cursor   
FETCH NEXT FROM db_cursor INTO @constraint_name,@tableName,@columnName

WHILE @@FETCH_STATUS = 0   
BEGIN              
       set @constraintNEWname = 'DF_ScC' + @tableName + '_' + @columnName
       print 'Anterior: '+ @constraint_name +' Nuevo: ' + @constraintNEWname
       
       exec sp_rename @constraint_name , @constraintNEWname
       
       FETCH NEXT FROM db_cursor INTO @constraint_name,@tableName,@columnName       
END   

CLOSE db_cursor   
DEALLOCATE db_cursor

/**********************************************************************************************************************/




