/*
	Se eliminan las solicitudes anuladas que no tengan ninguna encomienda relacionada.
	Se verifica que no tengan circuito SGI antes de realizar la eliminación
*/

SET NOCOUNT ON

DECLARE @id_solicitud_borrar int

DECLARE cur CURSOR FAST_FORWARD FOR
SELECT sol.id_solicitud
FROM 
	SSIT_solicitudes sol
	LEFT JOIN Encomienda enc  ON sol.id_solicitud = enc.id_solicitud
WHERE
	enc.id_encomienda IS NULL
	AND sol.id_estado  = 20
	AND  NOT EXISTS(
	SELECT tt_hab.id_tramitetarea
	FROM
		SGI_Tramites_Tareas_HAB tt_hab
		INNER JOIN SGI_Tramites_Tareas tt ON tt_hab.id_tramitetarea = tt.id_tramitetarea
		INNER JOIN ENG_Tareas tar ON tt.id_tarea = tar.id_tarea
	WHERE
		tar.cod_tarea  IN(109,209,309,110,210,310,1108,1209,1308,1409,609,1309)
		AND id_solicitud = sol.id_solicitud
	) 
	AND LEN(ISNULL(sol.NroExpediente,'')) = 0

OPEN cur
FETCH NEXT FROM cur INTO @id_solicitud_borrar

WHILE @@FETCH_STATUS = 0
BEGIN

	PRINT(convert(nvarchar,@id_solicitud_borrar))
	DECLARE @tbl1 TABLE(id_tramitetarea int)
	
	INSERT INTO @tbl1
	SELECT tt_hab.id_tramitetarea
	FROM
		SGI_Tramites_Tareas_HAB tt_hab
		INNER JOIN SGI_Tramites_Tareas tt ON tt_hab.id_tramitetarea = tt.id_tramitetarea
		INNER JOIN ENG_Tareas tar ON tt.id_tarea = tar.id_tarea
	WHERE
		tar.cod_tarea IN(106,206,306,606,129,229,329,629,1106,1206,1306,1406,1129,1229,1329,1429)
		AND id_solicitud =@id_solicitud_borrar
	
	DELETE FROM SGI_Tramites_Tareas_HAB WHERE id_Tramitetarea IN(SELECT id_tramitetarea FROM @tbl1)
	DELETE FROM SGI_Tramites_Tareas WHERE id_Tramitetarea IN(SELECT id_tramitetarea FROM @tbl1)

	DELETE FROM SSIT_Solicitudes_Observaciones WHERE id_solicitud = @id_solicitud_borrar
	DELETE FROM ssit_solicitudes_encomienda WHERE id_solicitud = @id_solicitud_borrar
	DELETE FROM SSIT_Solicitudes_HistorialEstados WHERE id_solicitud = @id_solicitud_borrar
	
	DELETE FROM SSIT_Solicitudes_Ubicaciones_Puertas WHERE id_solicitudubicacion  IN(
	SELECT id_solicitudubicacion  FROM SSIT_Solicitudes_Ubicaciones WHERE id_solicitud = @id_solicitud_borrar
	)
	DELETE FROM SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal WHERE id_solicitudubicacion  IN(
	SELECT id_solicitudubicacion FROM SSIT_Solicitudes_Ubicaciones WHERE id_solicitud = @id_solicitud_borrar
	)
	DELETE FROM SSIT_Solicitudes_Ubicaciones WHERE id_solicitud = @id_solicitud_borrar


	DELETE FROM SSIT_Solicitudes_Firmantes_PersonasFisicas WHERE id_solicitud = @id_solicitud_borrar
	DELETE FROM SSIT_Solicitudes_Titulares_PersonasFisicas WHERE id_solicitud = @id_solicitud_borrar
	DELETE FROM SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas WHERE id_solicitud = @id_solicitud_borrar
	DELETE FROM SSIT_Solicitudes_Firmantes_PersonasJuridicas WHERE id_solicitud = @id_solicitud_borrar
	DELETE FROM SSIT_Solicitudes_Titulares_PersonasJuridicas WHERE id_solicitud = @id_solicitud_borrar
	DELETE FROM SSIT_Solicitudes_AvisoCaducidad WHERE id_solicitud = @id_solicitud_borrar
	DELETE FROM SSIT_Solicitudes_Pagos WHERE id_solicitud = @id_solicitud_borrar
	DELETE FROM SSIT_DocumentosAdjuntos WHERE id_solicitud = @id_solicitud_borrar
	DELETE FROM DocumentosAdjuntos WHERE id_solicitud = @id_solicitud_borrar
	
	
	DELETE FROM SSIT_Solicitudes WHERE id_solicitud = @id_solicitud_borrar


	FETCH NEXT FROM cur INTO @id_solicitud_borrar

END
CLOSE cur
DEALLOCATE cur



/*
SELECT tar.*,tt.*
	FROM
		SGI_Tramites_Tareas_HAB tt_hab
		INNER JOIN SGI_Tramites_Tareas tt ON tt_hab.id_tramitetarea = tt.id_tramitetarea
		INNER JOIN ENG_Tareas tar ON tt.id_tarea = tar.id_tarea
	WHERE
		id_solicitud =301469
*/
