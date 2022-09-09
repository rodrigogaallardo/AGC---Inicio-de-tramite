 DECLARE @tbl TABLE(id int)

 INSERT INTO @tbl VALUES(3)
 INSERT INTO @tbl VALUES(4)
 INSERT INTO @tbl VALUES(8)
  INSERT INTO @tbl VALUES(107)
 

 select * from  @tbl


		SELECT * FROM (  
            SELECT DISTINCT  
            	sol.id_cpadron as id_solicitud,  
            	CASE   
            		WHEN tubic.id_tipoubicacion = 0   
            		THEN  
            			IsNull(encpuer.nombre_calle,'')   
            		ELSE  
            			tubic.descripcion_tipoubicacion + ' ' +	stubic.descripcion_subtipoubicacion  
            	END as calle,  
            	CASE   
            		WHEN tubic.id_tipoubicacion = 0   
            		THEN  
            			IsNull(convert(nvarchar, encpuer.nropuerta),'')   
            		ELSE  
            			IsNull('Local ' + encubic.local_subtipoubicacion,'')  
            	END as puerta  
            FROM  
            	CPadron_Solicitudes sol  
            	INNER JOIN CPadron_Ubicaciones encubic ON sol.id_cpadron = encubic.id_cpadron  
            	INNER JOIN SubTiposDeUbicacion stubic ON encubic.id_subtipoubicacion = stubic.id_subtipoubicacion  
            	INNER JOIN TiposDeUbicacion tubic ON stubic.id_tipoubicacion = tubic.id_tipoubicacion  
            	LEFT JOIN CPadron_Ubicaciones_Puertas encpuer ON encubic.id_cpadronubicacion = encpuer.id_cpadronubicacion   
            	LEFT JOIN CPadron_Ubicaciones_PropiedadHorizontal encphor ON encubic.id_cpadronubicacion = encphor.id_cpadronubicacion   
            	LEFT JOIN Ubicaciones_PropiedadHorizontal phor ON encphor.id_propiedadhorizontal = phor.id_propiedadhorizontal  
            	--INNER JOIN @tbl tmp ON sol.id_cpadron = tmp.id 
            	where sol.id_cpadron in(3,4,8,107) 
            ) as con  
            ORDER BY  
            	id_solicitud  
            	,calle  
            	
            	
            	
            	
            	
            	--select * from CPadron_Solicitudes where ZonaDeclarada is not null