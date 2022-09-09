--use DGHP_Solicitudes2
--select top 10 * from SGI_Tramites_Tareas tr
--inner join SGI_Tramites_Tareas_HAB h ON tr.id_tramitetarea=h.id_tramitetarea
--order by h.id_tramitetarea desc

select top 100 * from  ENG_Rel_Perfiles_Tareas perfiles_tarea 
inner join  SGI_Perfiles perfiles on  perfiles_tarea.id_perfil = perfiles.id_perfil
inner join SGI_Tramites_Tareas tareas on perfiles_tarea.id_tarea = tareas.id_tarea
--where tareas.id_tramitetarea = 206643
order by tareas.id_tarea  desc

select * from SGI_Perfiles where id_perfil = 50

select * from dbo.SGI_Rel_Usuarios_Perfiles p inner join aspnet_Users u on p.userid = u.UserId
where p.id_perfil = 50