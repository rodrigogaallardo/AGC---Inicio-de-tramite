 DECLARE @tbl TABLE(id int)

 INSERT INTO @tbl VALUES(3)
 INSERT INTO @tbl VALUES(4)
 INSERT INTO @tbl VALUES(8)
  INSERT INTO @tbl VALUES(107)
 

 select * from  @tbl


		SELECT * FROM (  
            SELECT DISTINCT  
            	sol.id_encomienda as id_solicitud,  
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
            	--CPadron_Solicitudes sol  
            	Encomienda sol 
            	INNER JOIN Encomienda_Ubicaciones encubic ON sol.id_encomienda = encubic.id_encomienda
            	INNER JOIN SubTiposDeUbicacion stubic ON encubic.id_subtipoubicacion = stubic.id_subtipoubicacion  
            	INNER JOIN TiposDeUbicacion tubic ON stubic.id_tipoubicacion = tubic.id_tipoubicacion  
            	LEFT JOIN Encomienda_Ubicaciones_Puertas encpuer ON encubic.id_encomiendaubicacion = encpuer.id_encomiendaubicacion
            	LEFT JOIN Encomienda_Ubicaciones_PropiedadHorizontal encphor ON encubic.id_encomiendaubicacion = encphor.id_encomiendaubicacion   
            	LEFT JOIN Ubicaciones_PropiedadHorizontal phor ON encphor.id_propiedadhorizontal = phor.id_propiedadhorizontal  
            	--INNER JOIN @tbl tmp ON sol.id_cpadron = tmp.id 
            	where sol.id_encomienda in(3,4,8,107) 
            ) as con  
            ORDER BY  
            	id_solicitud  
            	,calle  
            	
            	
            	
            	
            	
            	--select * from CPadron_Solicitudes where ZonaDeclarada is not null