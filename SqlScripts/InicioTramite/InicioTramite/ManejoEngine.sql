use DGHP_Solicitudes2
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


/*Asignacion de la Tarea*/
select * from SGI_Tramites_Tareas where id_tramitetarea = 348943
select * from SGI_Tramites_Tareas
select * from Encomienda 

--update Encomienda set id_solicitud = 200929 where id_encomienda = 2

select * from SGI_Tramites_Tareas where id_tramitetarea = 339384

select * from dbo.SGI_Tramites_Tareas_HAB TH inner join SGI_Tramites_Tareas TT on th.id_tramitetarea = tt.id_tramitetarea 
where tt.id_tramitetarea = 330991


select * from dbo.SGI_Tramites_Tareas_HAB TH inner join SGI_Tramites_Tareas TT on th.id_tramitetarea = tt.id_tramitetarea 
where tt.id_tramitetarea = 339383


select * from SSIT_Solicitudes where id_solicitud = 339385
select * from aspnet_Users where UserName = 'lvila'

select id_solicitud from ssit_solicitudes 
WHERE id_solicitud NOT IN(select id_solicitud FROM sgi_Tramites_tareas_HAB)


