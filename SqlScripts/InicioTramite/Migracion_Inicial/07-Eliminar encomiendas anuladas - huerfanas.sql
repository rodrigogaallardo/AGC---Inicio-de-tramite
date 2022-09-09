
/*
	se borran todas las encomiendas que no poseen una solicitud y que están anuladas.
	esto hace que al final de toda la migración, no quede ninguna encomienda sin solicitud (las encomiendas anuladas huerfanas se borran)
	
	Query de comprobación: 
	SELECT id_encomienda FROM encomienda WHERE id_solicitud is null 
	debe dar vacío
*/


DECLARE @tbl TABLE (id_encomienda int)
INSERT INTO @tbl 
SELECT id_encomienda FROM encomienda WHERE id_solicitud is null AND id_estado = 20


DELETE FROM Encomienda_Firmantes_PersonasFisicas WHERE id_encomienda IN(SELECT id_encomienda FROM @tbl )
DELETE FROM Encomienda_Titulares_PersonasFisicas WHERE id_encomienda IN(SELECT id_encomienda FROM @tbl )
DELETE FROM Encomienda_Titulares_PersonasJuridicas_PersonasFisicas WHERE id_encomienda IN(SELECT id_encomienda FROM @tbl )
DELETE FROM Encomienda_Firmantes_PersonasJuridicas WHERE id_encomienda IN(SELECT id_encomienda FROM @tbl )
DELETE FROM Encomienda_Titulares_PersonasJuridicas WHERE id_encomienda IN(SELECT id_encomienda FROM @tbl )

DELETE FROM Encomienda_ConformacionLocal WHERE id_encomienda IN(SELECT id_encomienda FROM @tbl )
DELETE FROM Encomienda_Sobrecarga_Detalle2 WHERE id_sobrecarga_detalle1 IN(
SELECT id_sobrecarga_detalle1 FROM Encomienda_Sobrecarga_Detalle1 WHERE id_sobrecarga IN(
SELECT id_sobrecarga FROM Encomienda_Certificado_Sobrecarga WHERE id_encomienda_datoslocal IN(select id_encomiendadatoslocal FROM  Encomienda_datoslocal WHERE Id_encomienda IN(SELECT id_encomienda FROM @tbl )))
)
DELETE FROM Encomienda_Sobrecarga_Detalle1 WHERE id_sobrecarga IN(
SELECT id_sobrecarga FROM Encomienda_Certificado_Sobrecarga WHERE id_encomienda_datoslocal IN(select id_encomiendadatoslocal FROM  Encomienda_datoslocal WHERE Id_encomienda IN(SELECT id_encomienda FROM @tbl ))
)

DELETE FROM Encomienda_Certificado_Sobrecarga WHERE id_encomienda_datoslocal IN(select id_encomiendadatoslocal FROM  Encomienda_datoslocal WHERE Id_encomienda IN(SELECT id_encomienda FROM @tbl ))


DELETE FROM Encomienda_DatosLocal WHERE id_encomienda IN(SELECT id_encomienda FROM @tbl )
DELETE FROM Encomienda_Plantas WHERE id_encomienda IN(SELECT id_encomienda FROM @tbl )
DELETE FROM Rel_Encomienda_Rectificatoria WHERE id_encomienda_nueva IN(SELECT id_encomienda FROM @tbl )
DELETE FROM Encomienda_Rectificatoria WHERE id_encomienda_nueva IN(SELECT id_encomienda FROM @tbl )

DELETE FROM Encomienda_Ubicaciones_Puertas WHERE id_encomiendaubicacion  IN(
SELECT id_encomiendaubicacion FROM Encomienda_Ubicaciones WHERE id_encomienda IN(SELECT id_encomienda FROM @tbl )
)
DELETE FROM Encomienda_Ubicaciones_PropiedadHorizontal WHERE id_encomiendaubicacion  IN(
SELECT id_encomiendaubicacion FROM Encomienda_Ubicaciones WHERE id_encomienda IN(SELECT id_encomienda FROM @tbl )
)
DELETE FROM Encomienda_Ubicaciones WHERE id_encomienda IN(SELECT id_encomienda FROM @tbl )

DELETE FROM Encomienda_Rubros WHERE id_encomienda IN(SELECT id_encomienda FROM @tbl )
DELETE FROM Encomienda_Normativas WHERE id_encomienda IN(SELECT id_encomienda FROM @tbl )
DELETE FROM Encomienda_Planos WHERE id_encomienda IN(SELECT id_encomienda FROM @tbl )
DELETE FROM Encomienda_DocumentosAdjuntos WHERE id_encomienda IN(SELECT id_encomienda FROM @tbl )
DELETE FROM Encomienda_Sobrecargas WHERE id_encomienda IN(SELECT id_encomienda FROM @tbl )
DELETE FROM Encomienda WHERE id_encomienda IN(SELECT id_encomienda FROM @tbl )

