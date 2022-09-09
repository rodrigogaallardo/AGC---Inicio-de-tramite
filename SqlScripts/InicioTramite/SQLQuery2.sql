  
CREATE PROCEDURE [dbo].[CAA_Imprimir_Solicitud]  1234
(  
 @id_caa int  
)  
AS  
BEGIN  
 DECLARE   
  @strPlantasHabilitar nvarchar(500),  
  @separador nvarchar(300),  
  @id_tiposector int,  
  @descripcion_tiposector nvarchar(255),  
  @detalle_planta nvarchar(50),  
  @MuestraCampoAdicional bit,  
  @TamanoCampoAdicional int  
    
   
 -------------------------------------------------  
 -- Plantas a habilitar  
 -------------------------------------------------  
 SET @strPlantasHabilitar = ''  
   
  DECLARE curPlantas CURSOR FAST_FORWARD FOR  
  SELECT   
   tipsec.id,  
   tipsec.descripcion,  
   caaplan.detalle_caatiposector,  
   convert(bit,IsNull(tipsec.MuestraCampoAdicional,0)),  
   IsNull(tipsec.TamanoCampoAdicional,0)   
  FROM   
   TipoSector tipsec   
   INNER JOIN CAA_Plantas caaplan ON tipsec.id = caaplan.id_tiposector AND caaplan.id_caa = @id_caa  
  GROUP BY  
   tipsec.id,  
   tipsec.descripcion,  
   caaplan.detalle_caatiposector,  
   convert(bit,IsNull(tipsec.MuestraCampoAdicional,0)),  
   IsNull(tipsec.TamanoCampoAdicional,0)   
  ORDER BY  
   tipsec.id  
  
 OPEN curPlantas  
 FETCH NEXT FROM curPlantas INTO @id_tiposector,@descripcion_tiposector,@detalle_planta,@MuestraCampoAdicional,@TamanoCampoAdicional  
   
 WHILE @@FETCH_STATUS = 0  
 BEGIN  
   
  IF LEN(@strPlantasHabilitar) = 0  
            SET @separador = ''  
        ELSE  
            SET @separador = ', '  
  
  IF @MuestraCampoAdicional = 1  
  BEGIN  
              
            IF @TamanoCampoAdicional >= 10  
                SET @strPlantasHabilitar = @strPlantasHabilitar + @separador + @detalle_planta  
            ELSE  
                SET @strPlantasHabilitar = @strPlantasHabilitar + @separador + @descripcion_tiposector + ' ' + @detalle_planta  
              
        END  
        ELSE  
            SET @strPlantasHabilitar = @strPlantasHabilitar + @separador + @descripcion_tiposector  
   
  FETCH NEXT FROM curPlantas INTO @id_tiposector,@descripcion_tiposector,@detalle_planta,@MuestraCampoAdicional,@TamanoCampoAdicional  
 END  
 CLOSE curPlantas  
 DEALLOCATE curPlantas  
  
  
 -----------------------------------------------  
 -- Datos de la tabla Solicitud  
 -----------------------------------------------  
  
        SELECT  
         sol.id_caa,  
         sol.id_encomienda,  
         sol.FechaIngreso,  
         sol.id_tipocertificado,  
         tcer.codigo_tipocertificado,  
         tcer.nombre_tipocertificado,  
         sol.ZonaDeclarada,  
         sol.id_estado,  
         sol.NroCertificado,  
         sol.FechaVencCertificado,  
         sol.Observaciones_rubros,  
         sol.CreateDate,  
         tnorm.Descripcion as TipoNormativa,  
         enorm.Descripcion as EntidadNormativa,  
         norm.nro_normativa as NroNormativa,   
         @strPlantasHabilitar as PlantasHabilitar,   
         sol.Observaciones_plantas as ObservacionesPlantasHabilitar,  
         sol.CodigoSeguridad  
        FROM  
         CAA_Solicitudes sol  
         INNER JOIN CAA_TiposDeCertificados tcer ON sol.id_tipocertificado = tcer.id_tipocertificado  
         LEFT JOIN CAA_Normativas norm ON sol.id_caa = norm.id_caa  
         LEFT JOIN TipoNormativa tnorm ON norm.id_tiponormativa = tnorm.Id  
         LEFT JOIN EntidadNormativa enorm ON norm.id_entidadnormativa = enorm.Id  
        WHERE  
         sol.id_caa = @id_caa  
  
   
 -----------------------------------------------  
 -- Ubicaciones  
 -----------------------------------------------  
   
  SELECT   
          caaubic.id_caaubicacion,  
          caaubic.id_caa,  
          caaubic.id_ubicacion,  
          mat.Seccion,  
          mat.Manzana,  
          mat.parcela,  
          mat.NroPartidaMatriz as NroPartidaMatriz,  
          caaubic.local_subtipoubicacion,  
          zon1.codzonapla as ZonaParcela,  
          dbo.CAA_Solicitud_DireccionesPartidasPlancheta(caaubic.id_caa,caaubic.id_ubicacion) as Direcciones,  
          caaubic.deptoLocal_caaubicacion as DeptoLocal  
        FROM  
          CAA_Ubicaciones caaubic   
          INNER JOIN Ubicaciones mat ON caaubic.id_ubicacion = mat.id_ubicacion  
          INNER JOIN Zonas_Planeamiento zon1 ON  mat.id_zonaplaneamiento = zon1.id_zonaplaneamiento  
        WHERE  
          caaubic.id_caa = @id_caa  
            
 -----------------------------------------------  
 -- Ubicaciones Porpiedad Horizontal  
 -----------------------------------------------  
             
  SELECT   
          caaubic.id_ubicacion,  
          phor.NroPartidaHorizontal as NroPartidaHorizontal,  
          phor.piso,  
          phor.depto,  
          phor.UnidadFuncional  
        FROM  
          CAA_Ubicaciones caaubic   
          INNER JOIN CAA_Ubicaciones_PropiedadHorizontal caaphor ON caaubic.id_caaubicacion = caaphor.id_caaubicacion   
          INNER JOIN Ubicaciones_PropiedadHorizontal phor ON caaphor.id_propiedadhorizontal = phor.id_propiedadhorizontal  
        WHERE  
          caaubic.id_caa = @id_caa  
            
 -----------------------------------------------  
 -- Ubicaciones Puertas  
 -----------------------------------------------  
   
        SELECT  
          caapuer.id_caaubicacion,  
          caapuer.nombre_calle as Calle,  
          caapuer.NroPuerta  
        FROM  
          CAA_Ubicaciones caaubic   
          INNER JOIN CAA_Ubicaciones_Puertas caapuer ON caaubic.id_caaubicacion = caapuer.id_caaubicacion  
        WHERE  
          caaubic.id_caa = @id_caa  
          
  
 -----------------------------------------------  
 -- Firmantes  
 -----------------------------------------------  
    
  IF EXISTS (SELECT pj.id_caa   
     FROM CAA_Firmantes_PersonasJuridicas pj   
     INNER JOIN CAA_Titulares_PersonasJuridicas titpj ON titpj.id_personajuridica = pj.id_personajuridica  
     WHERE  pj.id_caa = @id_caa AND titpj.Id_TipoSociedad = 2) BEGIN  
       
   SELECT  
    pj.id_firmante_pj as id_firmante,   
    @id_caa as id_caa,  
    'PJ' as TipoPersona,   
    UPPER(pj.Apellido) as Apellido,   
    UPPER(pj.Nombres) as Nombres,   
    tdoc.nombre as TipoDoc,   
    pj.Nro_Documento as NroDoc,   
    tcl.nom_tipocaracter as CaracterLegal,   
    UPPER(pjpf.Apellido + ' ' + pjpf.Nombres) as FirmanteDe  
   FROM CAA_Firmantes_PersonasJuridicas pj  
   INNER JOIN CAA_Titulares_PersonasJuridicas titpj ON titpj.id_personajuridica = pj.id_personajuridica  
   INNER JOIN tipodocumentopersonal tdoc ON pj.id_tipodoc_personal = tdoc.tipodocumentopersonalId   
   INNER JOIN CAA_Titulares_PersonasJuridicas_PersonasFisicas pjpf ON pjpf.id_firmante_pj = pj.id_firmante_pj  
   INNER JOIN tiposdecaracterlegal tcl ON pj.id_tipocaracter = tcl.id_tipocaracter   
   WHERE   
    pj.id_caa = @id_caa  
    AND titpj.Id_TipoSociedad = 2  
      
  END ELSE BEGIN        
     
   SELECT   
    pj.id_firmante_pj as id_firmante,   
    @id_caa as id_caa,  
    'PJ' as TipoPersona,   
    UPPER(pj.Apellido) as Apellido,   
    UPPER(pj.Nombres) as Nombres,   
    tdoc.nombre as TipoDoc,   
    pj.Nro_Documento as NroDoc,   
    tcl.nom_tipocaracter as CaracterLegal,   
    UPPER(titpj.Razon_Social) as FirmanteDe  
   FROM   
    CAA_Firmantes_PersonasJuridicas pj    
    INNER JOIN CAA_Titulares_PersonasJuridicas titpj ON pj.id_personajuridica = titpj.id_personajuridica  
    INNER JOIN tiposdecaracterlegal tcl ON pj.id_tipocaracter = tcl.id_tipocaracter   
    INNER JOIN tipodocumentopersonal tdoc ON pj.id_tipodoc_personal = tdoc.tipodocumentopersonalId   
   WHERE   
       pj.id_caa = @id_caa  
   UNION ALL   
   SELECT   
       pf.id_firmante_pf as id_firmante,   
     @id_caa as id_caa,  
       'PF' as TipoPersona,   
       UPPER(pf.Apellido) as Apellido,   
       UPPER(pf.Nombres) as Nombres,   
       tdoc.nombre as TipoDoc,   
       pf.Nro_Documento as NroDoc,   
       tcl.nom_tipocaracter as CaracterLegal,   
       UPPER(titpf.Apellido + ', ' + titpf.Nombres) as FirmanteDe  
   FROM   
    CAA_Firmantes_PersonasFisicas pf    
    INNER JOIN CAA_Titulares_PersonasFisicas titpf ON pf.id_personafisica = titpf.id_personafisica  
    INNER JOIN tiposdecaracterlegal tcl ON pf.id_tipocaracter = tcl.id_tipocaracter   
    INNER JOIN tipodocumentopersonal tdoc ON pf.id_tipodoc_personal = tdoc.tipodocumentopersonalId   
   WHERE   
       pf.id_caa = @id_caa  
  END  
  
 -----------------------------------------------  
 -- Titulares  
 -----------------------------------------------          
   
  SELECT   
         pj.id_personajuridica as id_persona,   
   @id_caa as id_caa,  
         'PJ' as TipoPersona,   
         UPPER(pj.Razon_Social) as RazonSocial,   
         tsoc.descripcion as TipoSociedad,   
         '' as Apellido,   
         ''as Nombres,   
         '' as TipoDoc,   
         '' as NroDoc,   
   tipoiibb.nom_tipoiibb as TipoIIBB,   
         pj.Nro_IIBB as NroIIBB,   
         pj.cuit,   
         1 as MuestraEnTitulares,   
         CASE WHEN pj.id_tiposociedad = 2 THEN 0 ELSE 1 END as MuestraEnPlancheta,  
         pj.Email  
        FROM   
         CAA_Titulares_PersonasJuridicas pj   
   INNER JOIN TipoSociedad tsoc ON pj.id_tiposociedad = tsoc.id   
   INNER JOIN TiposDeIngresosBrutos tipoiibb ON pj.id_tipoiibb = tipoiibb.id_tipoiibb   
        WHERE   
         pj.id_caa = @id_caa  
  
        UNION ALL   
       /* SELECT   
   pj.id_firmante_pj as id_persona,   
   @id_caa as id_caa,  
   'PF' as TipoPersona,   
   '' as RazonSocial,   
   '' as TipoSociedad,   
   UPPER(pj.Apellido) as Apellido,   
   UPPER(pj.Nombres) as Nombres,   
   tdoc.nombre as TipoDoc,   
   pj.Nro_Documento as NroDoc,   
   '' as TipoIIBB,   
   '' as NroIIBB,   
   '' as cuit,  
   0 as MuestraEnTitulares,   
   1 as MuestraEnPlancheta,  
   titpj.Email  
        FROM   
   CAA_Firmantes_PersonasJuridicas pj    
   INNER JOIN CAA_Titulares_PersonasJuridicas titpj ON pj.id_personajuridica = titpj.id_personajuridica  
   INNER JOIN tiposdecaracterlegal tcl ON pj.id_tipocaracter = tcl.id_tipocaracter   
   INNER JOIN tipodocumentopersonal tdoc ON pj.id_tipodoc_personal = tdoc.tipodocumentopersonalId   
        WHERE  
   titpj.id_caa =  @id_caa  
   AND titpj.Id_TipoSociedad = 2    -- Sociedad de Hecho  
  
        UNION ALL */  
        SELECT   
   pf.id_personafisica as id_persona,   
   @id_caa as id_caa,   
   'PF' as TipoPersona,   
   '' as RazonSocial,   
   '' as TipoSociedad,   
   UPPER(pf.Apellido) as Apellido,   
   UPPER(pf.Nombres) as Nombres,   
   tdoc.nombre as TipoDoc,   
   pf.Nro_Documento as NroDoc,   
   tipoiibb.nom_tipoiibb as TipoIIBB,   
   pf.Ingresos_Brutos as NroIIBB,   
   pf.cuit,  
   1 as MuestraEnTitulares,   
   1 as MuestraEnPlancheta,  
   pf.Email  
        FROM   
   CAA_Titulares_PersonasFisicas pf    
   INNER JOIN tipodocumentopersonal tdoc ON pf.id_tipodoc_personal = tdoc.tipodocumentopersonalId   
   INNER JOIN TiposDeIngresosBrutos tipoiibb ON pf.id_tipoiibb = tipoiibb.id_tipoiibb   
        WHERE   
         pf.id_caa = @id_caa  
          
        UNION ALL  
        SELECT   
   tpjpf.id_firmante_pj as id_persona,   
   @id_caa as id_caa,  
   'PF' as TipoPersona,   
   '' as RazonSocial,   
   '' as TipoSociedad,   
   UPPER(tpjpf.Apellido) as Apellido,   
   UPPER(tpjpf.Nombres) as Nombres,   
   tdoc.nombre as TipoDoc,   
   tpjpf.Nro_Documento as NroDoc,   
   '' as TipoIIBB,   
   '' as NroIIBB,   
   '' as cuit,  
   1 as MuestraEnTitulares,   
   1 as MuestraEnPlancheta,  
   tpjpf.Email  
        FROM   
   CAA_Titulares_PersonasJuridicas_PersonasFisicas tpjpf    
   INNER JOIN tipodocumentopersonal tdoc ON tdoc.tipodocumentopersonalId = tpjpf.id_tipodoc_personal  
        WHERE   
         tpjpf.id_caa = @id_caa  
 -----------------------------------------------  
 -- Rubros  
 -----------------------------------------------          
          
  SELECT  
   rub.id_caarubro,  
   rub.id_caa,  
   rub.cod_rubro,  
   rub.desc_rubro,  
   rub.EsAnterior,  
   tact.nombre as TipoActividad,  
   ia.cod_ImpactoAmbiental as cod_ImpactoAmbiental,  
   ia.nom_ImpactoAmbiental as desc_ImpactoAmbiental,  
   rub.SuperficieHabilitar  
        FROM  
   CAA_rubros rub   
   INNER JOIN tipoactividad tact ON rub.id_tipoactividad = tact.id  
   INNER JOIN ImpactoAmbiental ia ON rub.id_ImpactoAmbiental = ia.id_ImpactoAmbiental   
   WHERE  
   rub.id_caa = @id_caa          
          
 -----------------------------------------------  
 -- Datos del Local  
 -----------------------------------------------          
  
        SELECT  
         dl.id_caadatoslocal,  
         dl.id_caa,  
         dl.superficie_cubierta_dl,  
         dl.superficie_descubierta_dl,  
         dl.dimesion_frente_dl,  
         dl.lugar_carga_descarga_dl,  
         dl.estacionamiento_dl,  
         dl.red_transito_pesado_dl,  
         dl.sobre_avenida_dl,  
         dl.materiales_pisos_dl,  
         dl.materiales_paredes_dl,  
         dl.materiales_techos_dl,  
         dl.materiales_revestimientos_dl,  
         dl.sanitarios_ubicacion_dl,  
         dl.sanitarios_distancia_dl,  
         dl.cantidad_sanitarios_dl,  
         dl.superficie_sanitarios_dl,  
         dl.frente_dl,  
         dl.fondo_dl,  
         dl.lateral_izquierdo_dl,  
         dl.lateral_derecho_dl,  
         dl.sobrecarga_corresponde_dl,  
         dl.sobrecarga_tipo_observacion,  
         dl.sobrecarga_requisitos_opcion,  
         dl.sobrecarga_art813_inciso,  
         dl.sobrecarga_art813_item,  
         dl.cantidad_operarios_dl  
        FROM  
   CAA_DatosLocal dl   
        WHERE  
   dl.id_caa = @id_caa  
  
 -----------------------------------------------  
 -- Resultados de los SC  
 -----------------------------------------------             
    
  SELECT  
   id_caa,  
   id_tipocertificado,  
   indice_form_A,  
   indice_form_B,  
   CASE   
    WHEN id_tipocertificado = 1 THEN 'Sin Relevante Efecto (SRE) - Documentación Anexo III c'  
    WHEN id_tipocertificado = 2 THEN 'Sin Relevante Efecto (SRE) - Documentación Anexo III d'  
    WHEN id_tipocertificado = 4 THEN 'Con Relevante Efecto (CRE) - Documentación Anexo III d'  
   END as descripcion_tramite  
  FROM  
   CAA_Resultados_SC  
  WHERE  
   id_caa = @id_caa  
  
  
 ------------------------------------------------------------------------  
 -- Profesional interviniente en los tipos de certificado que lo requiera  
 ------------------------------------------------------------------------  
     
 SELECT  
  prof.id_profesional,  
  prof.Apellido,  
  prof.Nombre,  
  prof.MTNumeroMat,  
  prof.CUIT,  
  prof.NroRegistro,  
  prof.Email  
 FROM  
  CAA_Profesionales caa_prof  
  INNER JOIN Profesionales prof ON caa_prof.id_profesional = prof.id_profesional  
 WHERE  
  caa_prof.id_caa = @id_caa  
    
    
END  itud