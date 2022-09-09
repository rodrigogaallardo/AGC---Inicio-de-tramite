--sp_helptext ENG_Asignar_Tarea
  
CREATE PROCEDURE [dbo].[ENG_Asignar_Tarea]  
(  
 @id_tramitetarea int,  
 @userid uniqueidentifier  
)  
AS  
BEGIN  
  
 DECLARE   
  @UsuarioAsignado uniqueidentifier,  
  @NomApe nvarchar(100),  
  @mensaje nvarchar(500)  
   
   select * from SGI_Profiles
   
   
 SELECT   
  --@UsuarioAsignado = UsuarioAsignado_tramitetarea,  
  --@NomApe = prof.Nombres + ' ' + prof.Apellido  
  UsuarioAsignado_tramitetarea,  
  prof.Nombres + ' ' + prof.Apellido  
  
 FROM   
  SGI_Tramites_Tareas tt   
  LEFT JOIN SGI_Profiles prof ON tt.UsuarioAsignado_tramitetarea = prof.userid  
 WHERE   
  id_tramitetarea = @id_tramitetarea  
   
 IF @UsuarioAsignado IS NULL  
 BEGIN  
  UPDATE SGI_Tramites_Tareas   
  SET   
   UsuarioAsignado_tramitetarea = @userid,  
   FechaAsignacion_tramtietarea = GETDATE()  
  WHERE  
   id_tramitetarea = @id_tramitetarea  
 END  
 ELSE  
 BEGIN  
  SET @mensaje = 'No es posible tomar la tarea, la misma ha sido tomada por el usuario ' + @NomApe  
  RAISERROR(@mensaje,16,1)  
  RETURN  
 END  
  
END  