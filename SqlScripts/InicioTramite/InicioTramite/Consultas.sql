SELECT * FROM INFORMATION_SCHEMA.TABLES 
SELECT * FROM INFORMATION_SCHEMA.COLUMNS where COLUMN_NAME like '%perfil%'

select * from bafyco.BAFYCO_Menues

select * from dbo.InicioTramite_Perfiles
select * from dbo.InicioTramite_Rel_Perfiles_Menues
go
select * from dbo.InicioTramite_Rel_Perfiles_Usuarios
select * from aspnet_Users where UserName = '13800595'
select * from Profesional where [ID] = 13800595
select * from InicioTramite_Menues
/*Menues*/
--insert into InicioTramite_Perfiles (id_perfil,nombre_perfil,descripcion_perfil) values (1,'Habilitaciones','Unico perfildehabilitaciones')
--insert into dbo.InicioTramite_Rel_Perfiles_Usuarios(userid,id_perfil)values('5674BEEF-AFEA-4ADE-AEC3-6774E251BAF2',1)
--insert into dbo.InicioTramite_Menues select * FROM bafyco.BAFYCO_Menues
--INSERT INTO dbo.InicioTramite_Rel_Perfiles_Menues SELECT 1,id_menu FROM InicioTramite_Menues