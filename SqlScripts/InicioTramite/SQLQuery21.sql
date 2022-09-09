  
  
CREATE PROCEDURE [dbo].[ENG_Bandeja_Asignar](  
  @id_tramitetarea int,  
  @userid_a_asignar uniqueidentifier,  
  @userid_asignador uniqueidentifier  
)  
AS  
BEGIN  
   
 DECLARE @id_solicitud int  
 DECLARE @id_tarea int  
 DECLARE @id_resultado int  
 DECLARE @id_tarea_siguiente int  
 DECLARE @id_tramitetarea_siguiente int  
 DECLARE @cod_circuito nvarchar(10)  
   
 SELECT   
  @id_tarea = id_tarea   
 FROM   
  SGI_Tramites_Tareas   
 WHERE   
  id_tramitetarea = @id_tramitetarea  
  
 ------------------------------------------------------------------  
 -- Busqueda de circuito  
 ------------------------------------------------------------------  
 SELECT  
  @cod_circuito = cir.cod_circuito  
 FROM  
  ENG_Tareas tarea  
  INNER JOIN ENG_Circuitos cir ON tarea.id_circuito = cir.id_circuito  
 WHERE  
  tarea.id_tarea = @id_tarea  
  
 ------------------------------------------------------------------  
 -- Busqueda de la solicitud  
 ------------------------------------------------------------------  
 IF @cod_circuito = 'SCP' OR @cod_circuito ='SSP' OR @cod_circuito ='ESPECIAL' OR @cod_circuito ='ESPAR'  
 OR @cod_circuito = 'SCP2' OR @cod_circuito ='SSP2' OR @cod_circuito ='ESPECIAL2' OR @cod_circuito ='ESPAR2'  
 BEGIN  
  SELECT  
   @id_solicitud = id_solicitud   
  FROM  
   SGI_Tramites_Tareas_HAB   
  WHERE  
   id_tramitetarea = @id_tramitetarea  
 END  
 ELSE IF @cod_circuito = 'CP'   
 BEGIN  
  SELECT  
   @id_solicitud = id_cpadron   
  FROM  
   SGI_Tramites_Tareas_CPADRON   
  WHERE  
   id_tramitetarea = @id_tramitetarea  
 END  
 ELSE IF @cod_circuito = 'TRANSF'   
 BEGIN  
  SELECT  
   @id_solicitud = id_solicitud   
  FROM  
   SGI_Tramites_Tareas_TRANSF   
  WHERE  
   id_tramitetarea = @id_tramitetarea  
 END  
   
 ------------------------------------------------------------------  
 -- Asigna el usuario de la tarea de asignacion y la cierra  
 ------------------------------------------------------------------  
 UPDATE SGI_Tramites_Tareas   
 SET  
  UsuarioAsignado_tramitetarea = @userid_asignador  
  , FechaAsignacion_tramtietarea = GETDATE()  
  , FechaCierre_tramitetarea = GETDATE()  
 WHERE  
  id_tramitetarea = @id_tramitetarea  
    
 ------------------------------------------------------------------  
 -- Obtiene el resultado de la tarea de asignacion  
 ------------------------------------------------------------------  
 SELECT TOP 1 @id_resultado = id_resultado FROM ENG_Rel_Resultados_Tareas WHERE id_tarea = @id_tarea  
   
   
 ----------------------------------------------------------------------------------------  
 -- Obtiene las transiciones (siguientes tareas) del resultado de la tarea de asignacion  
 ----------------------------------------------------------------------------------------  
 DECLARE @tmp_transiciones TABLE(id_tarea int, nombre_tarea nvarchar(200))  
 INSERT INTO @tmp_transiciones  
 EXEC ENG_GetTransicionesxResultado @id_tarea, @id_resultado  
   
 ----------------------------------------------------------------------------------------  
 -- Crea y asigna las tareas obtenidas  
 ---------------------------------------------------------------------------------------  
   
 DECLARE @tbl TABLE (id_tareasiguiente int)  
 DECLARE curTrans CURSOR FAST_FORWARD FOR  
 SELECT id_tarea FROM @tmp_transiciones  
   
   
 OPEN curTrans  
 FETCH NEXT FROM curTrans INTO @id_tarea_siguiente  
   
 WHILE @@FETCH_STATUS = 0  
 BEGIN  
   
    
  EXEC ENG_Crear_Tarea @id_solicitud, @id_tarea_siguiente, @userid_asignador, @id_tramitetarea_siguiente out  
    
  EXEC ENG_Asignar_Tarea @id_tramitetarea_siguiente, @userid_a_asignar  
   
  FETCH NEXT FROM curTrans INTO @id_tarea_siguiente  
 END  
   
 CLOSE curTrans  
 DEALLOCATE curTrans  
   
END  