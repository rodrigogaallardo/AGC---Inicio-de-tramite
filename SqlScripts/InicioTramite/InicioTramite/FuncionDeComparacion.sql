declare @idSolicitud int
set @idSolicitud = 235562

select * from Encomienda


--select top 1 * from Encomienda 
--where id_solicitud = @idSolicitud
--	and id_estado = 4
--order by id_encomienda desc

--select * from SSIT_Solicitudes_Titulares_PersonasFisicas where id_solicitud = @idSolicitud
--select * from Encomienda_Titulares_PersonasFisicas where id_encomienda = 69054
--SELECT * FROM SSIT_Solicitudes_Firmantes_PersonasFisicas where id_solicitud = @idSolicitud
--SELECT * FROM Encomienda_Firmantes_PersonasFisicas where id_encomienda = 69054
--update SSIT_Solicitudes_Firmantes_PersonasFisicas set apellido = 'LOPEZ' where id_firmante_pf = 40832
--update SSIT_Solicitudes_Titulares_PersonasFisicas set apellido = 'LOPEZ' where id_personafisica = 40833
/*
SELECT * FROM SSIT_Solicitudes_Firmantes_PersonasFisicas where id_solicitud = @idSolicitud
SELECT * FROM Encomienda_Firmantes_PersonasFisicas where id_encomienda = 69054

select * from SSIT_Solicitudes_Titulares_PersonasFisicas where id_solicitud = @idSolicitud
select * from Encomienda_Titulares_PersonasFisicas where id_encomienda = 69054
select * from Rel_Encomienda_Rectificatoria where  id_solicitud_anterior = 235562
*/
/*
235562
225661
242887
231428
238714
231787
247657
update Encomienda set id_solicitud = 235562 where id_encomienda in(57646,
61310,
62166,
62569,
63432,
63975,
67362,
67972,
68208,
69054
)

************LLENAR LA TABLA DE SSIT SSIT_Solicitudes_Titulares_PersonasFisicas**********

INSERT INTO SSIT_Solicitudes_Titulares_PersonasFisicas  
SELECT id_personafisica, 235562, Apellido, Nombres, id_tipodoc_personal, Nro_Documento, Cuit, id_tipoiibb, Ingresos_Brutos, Calle, Nro_Puerta, Piso, Depto, Id_Localidad, Codigo_Postal, TelefonoArea, TelefonoPrefijo, TelefonoSufijo, TelefonoMovil, Sms, Email, MismoFirmante, CreateUser, CreateDate, LastUpdateUser, LastupdateDate
FROM  Encomienda_Titulares_PersonasFisicas where id_encomienda = 69054


INSERT INTO SSIT_Solicitudes_Firmantes_PersonasFisicas  
select id_firmante_pf,235562, id_personafisica, Apellido, Nombres, id_tipodoc_personal, Nro_Documento, id_tipocaracter, Email
FROM  Encomienda_Firmantes_PersonasFisicas where id_encomienda = 69054
--select * from SSIT_Solicitudes_Firmantes_PersonasFisicas


--235562
INSERT INTO SSIT_Solicitudes_Firmantes_PersonasJuridicas  
select id_firmante_pj, id_encomienda, id_personajuridica, Apellido, Nombres, id_tipodoc_personal, Nro_Documento, id_tipocaracter, cargo_firmante_pj, Email
FROM  Encomienda_Firmantes_PersonasJuridicas where id_encomienda = 69054
--select * from SSIT_Solicitudes_Firmantes_PersonasFisicas


*/

--INSERT INTO ENCOMIENDA 
--select 900000 as id_encomienda ,FechaEncomienda ,        nroEncomiendaconsejo ,id_consejo , id_profesional ,ZonaDeclarada ,  id_tipotramite ,id_tipoexpediente, id_subtipoexpediente ,id_estado  , CodigoSeguridad, Observaciones_plantas , Observaciones_rubros ,    CreateDate    ,          CreateUser  ,  LastUpdateDate       ,   LastUpdateUser, Pro_teatro ,id_solicitud ,tipo_anexo from encomienda WHERE id_encomienda = 94474
select * from Encomienda order by id_encomienda desc
