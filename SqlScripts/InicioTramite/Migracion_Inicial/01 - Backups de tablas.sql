
IF NOT EXISTS(SELECT * FROM sys.tables WHERE name = 'Encomienda_bk')
BEGIN
	SELECT * INTO Encomienda_bk FROM Encomienda
END
IF NOT EXISTS(SELECT * FROM sys.tables WHERE name = 'SSIT_Solicitudes_bk')
BEGIN
	SELECT * INTO SSIT_Solicitudes_bk FROM SSIT_Solicitudes
END

