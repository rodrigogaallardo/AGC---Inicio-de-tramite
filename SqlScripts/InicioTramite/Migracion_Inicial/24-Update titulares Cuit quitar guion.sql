UPDATE SSIT_Solicitudes_Titulares_PersonasJuridicas SET	cuit = REPLACE(cuit, '-', ''), Nro_IIBB = REPLACE(Nro_IIBB, '-', '')
UPDATE SSIT_Solicitudes_Titulares_PersonasFisicas SET cuit = REPLACE(cuit, '-', ''), Ingresos_Brutos = REPLACE(Ingresos_Brutos, '-', '')
UPDATE Encomienda_Titulares_PersonasJuridicas SET cuit = REPLACE(cuit, '-', ''), Nro_IIBB = REPLACE(Nro_IIBB, '-', '')
UPDATE Encomienda_Titulares_PersonasFisicas SET cuit = REPLACE(cuit, '-', ''), Ingresos_Brutos = REPLACE(Ingresos_Brutos, '-', '')
UPDATE CPadron_Titulares_Solicitud_PersonasJuridicas SET cuit = REPLACE(cuit, '-', ''), Nro_IIBB = REPLACE(Nro_IIBB, '-', '')
UPDATE CPadron_Titulares_Solicitud_PersonasFisicas SET cuit = REPLACE(cuit, '-', ''), Ingresos_Brutos = REPLACE(Ingresos_Brutos, '-', '')
UPDATE CPadron_Titulares_PersonasJuridicas SET cuit = REPLACE(cuit, '-', ''), Nro_IIBB = REPLACE(Nro_IIBB, '-', '')
UPDATE CPadron_Titulares_PersonasFisicas SET cuit = REPLACE(cuit, '-', ''), Ingresos_Brutos = REPLACE(Ingresos_Brutos, '-', '')