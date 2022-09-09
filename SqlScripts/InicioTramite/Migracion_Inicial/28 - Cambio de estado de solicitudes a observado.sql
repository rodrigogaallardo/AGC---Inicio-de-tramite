-- Cambia a Observado las solicitudes que tengan al menos una tarea de correccion de solicitud y que se encuentren en estado completo
UPDATE SSIT_Solicitudes SET id_estado = 27 
WHERE id_solicitud IN(
SELECT sol.id_solicitud
FROM 
	SSIT_Solicitudes sol
	INNER JOIN SGI_Tramites_Tareas_HAB tt_hab ON sol.id_solicitud = tt_hab.id_solicitud
	INNER JOIN SGI_Tramites_Tareas tt ON tt_hab.id_tramitetarea = tt.id_tramitetarea
WHERE
	sol.id_estado = 1
	AND	 tt.id_tarea IN(select id_tarea from ENG_tareas WHERE nombre_tarea like '%correcci%')
)
	
