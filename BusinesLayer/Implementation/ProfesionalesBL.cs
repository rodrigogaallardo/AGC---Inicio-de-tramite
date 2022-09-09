using AutoMapper;
using DataTransferObject;
using IBusinessLayer;
using System;
using System.Linq;
using BaseRepository;
using DataAcess;
using UnitOfWork;
using System.Collections.Generic;
using Dal.UnitOfWork;
using System.Web.Security;
using System.Data.SqlClient;
using DataAcess.EntityCustom;
using System.Data;
using ExternalService;

namespace BusinesLayer.Implementation
{
    public class ProfesionalesBL : IProfesionalesBL<ProfesionalDTO>
    {
        MembershipProvider securityProfesionales = null;
        RoleProvider roleProvider = null;

        private UsuariosProfesionalesRolesClasificacionRepository repoUPRC = null;
        private AspnetMembershipRepository repoMemberShip = null;
        private AspnetUsersRepository repoUser = null;
        private ProfesionalesRepository repo = null;
        private IUnitOfWorkFactory uowF = null;

        IMapper mapperBase;
        IMapper mapperRoles;
        IMapper mapperInhibiciones;

        public ProfesionalesBL()
        {
            securityProfesionales = Membership.Providers["SqlMembershipProviderProfesionales"];
            roleProvider = System.Web.Security.Roles.Providers["SqlRoleProviderProfesionales"];

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Profesional, ProfesionalDTO>()
                    .ForMember(dest => dest.ConsejoProfesionalDTO, source => source.MapFrom(p => p.ConsejoProfesional))
                    .ForMember(dest => dest.UserAspNet, source => source.MapFrom(p => p.aspnet_Users));

                cfg.CreateMap<ProfesionalDTO, Profesional>();

                cfg.CreateMap<ConsejoProfesional, ConsejoProfesionalDTO>()
                    .ForMember(dest => dest.GrupoConsejosDTO, source => source.MapFrom(p => p.GrupoConsejos));

                cfg.CreateMap<GrupoConsejos, GrupoConsejosDTO>()
                    .ForMember(dest => dest.Descripcion, source => source.MapFrom(p => p.descripcion_grupoconsejo))
                    .ForMember(dest => dest.Id, source => source.MapFrom(p => p.id_grupoconsejo))
                    .ForMember(dest => dest.LogoImpresion, source => source.MapFrom(p => p.logo_impresion_grupoconsejo))
                    .ForMember(dest => dest.LogoPantalla, source => source.MapFrom(p => p.logo_pantalla_grupoconsejo))
                    .ForMember(dest => dest.Nombre, source => source.MapFrom(p => p.nombre_grupoconsejo));

                cfg.CreateMap<aspnet_Users, AspnetUserDTO>()
                    .ForMember(dest => dest.AspNetRoles, source => source.MapFrom(p => p.aspnet_Roles))
                    .ForMember(dest => dest.Usuario, source => source.MapFrom(p => p.Usuario))
                    .ForMember(dest => dest.IsLockedOut, source => source.MapFrom(p => p.aspnet_Membership.IsLockedOut));

                cfg.CreateMap<Usuario, UsuarioDTO>();


                cfg.CreateMap<UsuariosProfesionalesRolesClasificacionDTO, Rel_UsuariosProf_Roles_Clasificacion>()
                    .ForMember(dest => dest.id_rel_prof_clasificacion, source => source.MapFrom(p => p.Id))
                    .ForMember(dest => dest.id_clasificacion, source => source.MapFrom(p => p.IdClasificacion))
                    .ForMember(dest => dest.GrupoConsejos_Roles_Clasificacion, source => source.Ignore())
                    .ForMember(dest => dest.aspnet_Roles, source => source.Ignore())
                    .ForMember(dest => dest.aspnet_Users, source => source.Ignore())
                    .ForMember(dest => dest.aspnet_Users1, source => source.Ignore());

                cfg.CreateMap<Rel_UsuariosProf_Roles_Clasificacion, UsuariosProfesionalesRolesClasificacionDTO>()
                    .ForMember(dest => dest.IdClasificacion, source => source.MapFrom(p => p.id_clasificacion))
                    .ForMember(dest => dest.Id, source => source.MapFrom(p => p.id_rel_prof_clasificacion));
                ;

                cfg.CreateMap<aspnet_Roles, RolesDTO>()
                    .ForMember(dest => dest.EncomiendaURLDTO, source => source.MapFrom(p => p.Encomienda_URLxROL));

                cfg.CreateMap<RolesDTO, aspnet_Roles>()
                    .ForMember(dest => dest.Encomienda_URLxROL, source => source.MapFrom(p => p.EncomiendaURLDTO));

                cfg.CreateMap<Encomienda_URLxROL, EncomiendaURLxROLDTO>();

                cfg.CreateMap<EncomiendaURLxROLDTO, Encomienda_URLxROL>();

                cfg.CreateMap<aspnet_Applications, ApplicationsDTO>();

                cfg.CreateMap<ApplicationsDTO, aspnet_Applications>();

                cfg.CreateMap<AspnetUserDTO, aspnet_Users>();
                cfg.CreateMap<TipoDocumentoPersonal, TipoDocumentoPersonalDTO>().ReverseMap();

                cfg.CreateMap<Profesional_historialDTO, Profesional_historial>()
                    .ForMember(dest => dest.ConsejoProfesional, source => source.MapFrom(p => p.ConsejoProfesional))
                    .ForMember(dest => dest.TipoDocumentoPersonal, source => source.MapFrom(p => p.TipoDocumentoPersonal)).ReverseMap();
            });
            mapperBase = config.CreateMapper();

            var configmapperRoles = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RolesDTO, aspnet_Roles>().ReverseMap();
            });
            mapperRoles = configmapperRoles.CreateMapper();

            var configInhibiciones = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Profesional_Perfiles_Inhibiciones, ProfesionalesPerfilesInhibicionesDTO>().ReverseMap();
            });
            mapperInhibiciones = configInhibiciones.CreateMapper();
        }

        public IEnumerable<Profesional_historialDTO> GetHistorial(int idProfesional, int startRowIndex, int maximumRows, out int totalRowCount)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                var repo = new ProfesionalHistorialRepository(this.uowF.GetUnitOfWork());

                var lst = repo.Get(idProfesional);
                totalRowCount = lst.Count();

                var q = lst.OrderBy(o => o.Id).Skip(startRowIndex).Take(maximumRows);

                var elementsDto = mapperBase.Map<IEnumerable<Profesional_historial>, IEnumerable<Profesional_historialDTO>>(q);

                return elementsDto;
            }
            catch (Exception es)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public ProfesionalDTO Get(Guid userid)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ProfesionalesRepository(this.uowF.GetUnitOfWork());
                var entityProf = repo.Get(userid);
                var prof = mapperBase.Map<Profesional, ProfesionalDTO>(entityProf);

                return prof;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="BusMatricula"></param>
        /// <param name="BusNomyApe"></param>
        /// <param name="BusGrupoConsejo"></param>
        /// <param name="BusConsejo"></param>
        /// <param name="BusLocalidad"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <param name="sortByExpression"></param>
        /// <param name="totalRowCount"></param>
        /// <returns></returns>
        public List<ProfesionalDTO> GetProfesionales(string BusMatricula, string BusNomyApe, int BusGrupoConsejo, int BusConsejo, string BusLocalidad,
            int startRowIndex, int maximumRows, string sortByExpression, out int totalRowCount)
        {

            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ProfesionalesRepository(this.uowF.GetUnitOfWork());

                var q = repo.GetProfesionales(BusMatricula, BusNomyApe, BusGrupoConsejo, BusConsejo, BusLocalidad);

                totalRowCount = q.Count();

                q = q.OrderBy(o => o.Matricula).Skip(startRowIndex).Take(maximumRows);

                var rq = q.ToList();

                var elementsDto = mapperBase.Map<List<Profesional>, List<ProfesionalDTO>>(rq);

                return elementsDto;
            }
            catch
            {
                throw;
            }
        }

        public void BloquearUsuarioProfesional(Guid userid, bool IsLockedOut)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repoMemberShip = new AspnetMembershipRepository(unitOfWork);
                    repoUser = new AspnetUsersRepository(unitOfWork);

                    var userMembership = repoUser.Single(userid).aspnet_Membership;

                    userMembership.IsLockedOut = IsLockedOut;

                    repoMemberShip.Update(userMembership);

                    unitOfWork.Commit();
                }

            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<LocalidadDTO> GetLocalidadProfesional()
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ProfesionalesRepository(this.uowF.GetUnitOfWork());
                var entityProf = repo.GetLocalidadProfesional().OrderBy(x => x);


                List<LocalidadDTO> lstLocalidades = new List<LocalidadDTO>();

                foreach (var item in entityProf)
                    lstLocalidades.Add(new LocalidadDTO() { Depto = item });

                return lstLocalidades;
            }
            catch
            {
                throw;
            }
        }

        public void darDeBajaProfesional(ProfesionalDTO prof)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new ProfesionalesRepository(unitOfWork);
                    var element = repo.Single(prof.Id);
                    element.BajaLogica = true;
                    repo.Update(element);
                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void reactivarProfesional(ProfesionalDTO prof)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new ProfesionalesRepository(unitOfWork);
                    var element = repo.Single(prof.Id);
                    element.BajaLogica = false;
                    repo.Update(element);
                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdProfesional"></param>
        /// <returns></returns>
        public ProfesionalDTO Single(int IdProfesional)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ProfesionalesRepository(this.uowF.GetUnitOfWork());
                var entityProf = repo.Single(IdProfesional);
                var prof = mapperBase.Map<Profesional, ProfesionalDTO>(entityProf);

                return prof;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ApplicationsDTO> GetAppByUser(Guid BusUser)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ProfesionalesRepository(this.uowF.GetUnitOfWork());
                var entityProf = repo.GetAppByUser(BusUser);

                var prof = mapperBase.Map<List<aspnet_Applications>, List<ApplicationsDTO>>(entityProf);

                return prof;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<EncomiendaURLxROLDTO> GetEncURL(Guid BusUser)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ProfesionalesRepository(this.uowF.GetUnitOfWork());
                var entityProf = repo.GetEncURL(BusUser);

                var prof = mapperBase.Map<List<Encomienda_URLxROL>, List<EncomiendaURLxROLDTO>>(entityProf);

                return prof;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<RolesDTO> GetRolxURL(string url)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ProfesionalesRepository(this.uowF.GetUnitOfWork());
                var entityProf = repo.GetRolxURL(url);
                var prof = mapperBase.Map<List<aspnet_Roles>, List<RolesDTO>>(entityProf);

                return prof;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RolesDTO> GetRolxAppId(Guid AppId, Guid BusUser)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ProfesionalesRepository(this.uowF.GetUnitOfWork());
                var entityProf = repo.GetRolxAppId(AppId, BusUser);
                var prof = mapperBase.Map<List<aspnet_Roles>, List<RolesDTO>>(entityProf);

                return prof;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProfesionalDTO> Get(int IdGrupoConsejo, string ApellidoNombre, int? Dni, string Matricula, string Cuit, bool? Baja, bool? Bloqueado, bool? inhibido, string userName, int startRowIndex, int maximumRows, out int totalRowCount)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ProfesionalesRepository(this.uowF.GetUnitOfWork());
                var entityProf = repo.Get(IdGrupoConsejo, ApellidoNombre, Dni, Matricula, Cuit, Baja, Bloqueado, inhibido, userName);
                totalRowCount = entityProf.Count();

                var q = entityProf.OrderBy(o => o.Matricula).Skip(startRowIndex).Take(maximumRows);

                var elementsDto = mapperBase.Map<IEnumerable<Profesional>, IEnumerable<ProfesionalDTO>>(q);

                return elementsDto;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdGrupoConsejo"></param>
        /// <param name="RoleId"></param>
        /// <param name="ApellidoNombre"></param>
        /// <param name="UserName"></param>
        /// <param name="Matricula"></param>
        /// <param name="Baja"></param>
        /// <returns></returns>
        public IEnumerable<ProfesionalDTO> Get(int IdGrupoConsejo, Guid RoleId, string ApellidoNombre, string UserName, string Matricula, bool? Baja, int maximumRows, int startRowIndex, out int totalRowCount)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ProfesionalesRepository(this.uowF.GetUnitOfWork());
                var entityProf = repo.Get(IdGrupoConsejo, RoleId, ApellidoNombre, UserName, Matricula, Baja);
                totalRowCount = entityProf.Count();

                var q = entityProf.OrderBy(o => o.Matricula).Skip(startRowIndex).Take(maximumRows);
                var elementsDto = mapperBase.Map<IEnumerable<Profesional>, IEnumerable<ProfesionalDTO>>(q);
                return elementsDto;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdProfesional"></param>
        public ProfesionalDTO DeleteUser(Guid IdProfesional)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {

                    repo = new ProfesionalesRepository(unitOfWork);
                    repoUPRC = new UsuariosProfesionalesRolesClasificacionRepository(unitOfWork);
                    repoMemberShip = new AspnetMembershipRepository(unitOfWork);
                    repoUser = new AspnetUsersRepository(unitOfWork);

                    var objectEntity = repo.Get(IdProfesional);

                    if (objectEntity.aspnet_Users != null)
                    {
                        repoUPRC.DeleteRange(objectEntity.aspnet_Users.Rel_UsuariosProf_Roles_Clasificacion);
                        repoMemberShip.Delete(objectEntity.aspnet_Users.aspnet_Membership);
                        objectEntity.aspnet_Users.aspnet_Roles.Clear();
                        repoUser.Delete(objectEntity.aspnet_Users);
                    }
                    var profesionalDTO = mapperBase.Map<Profesional, ProfesionalDTO>(objectEntity);

                    unitOfWork.Commit();
                    return profesionalDTO;
                }
            }
            catch (SqlException)
            {
                throw new Exception("No se puede eliminar el usuario ya que el mismo posee Encomiendas realizadas, si desea que el profesional no pueda utilizar el sistema presione la opción 'Editar' y bloquee el usuario. Esto hará que el profesional no pueda utilizar el sistema.");
            }
            catch
            {
                throw new Exception("no se pudo eliminar el usuario");
            }
        }
        public bool ExisteMatricula(int consejo, string matricula)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new ProfesionalesRepository(this.uowF.GetUnitOfWork());
            return repo.ExisteMatricula(consejo, matricula);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="RolesList"></param>
        /// <param name="cal"></param>
        public void Insert(UsuarioDTO usuario, bool Nuevo, EmailEntity emailEntity)
        {

            if (Nuevo && securityProfesionales.GetUser(usuario.UserName, false) != null)
                throw new Exception("Ya existe un usuario con este mismo nombre, por favor elija otro o agregue algun caracter.");

            uowF = new TransactionScopeUnitOfWorkFactory();

            if (Nuevo)
            {

                repo = new ProfesionalesRepository(this.uowF.GetUnitOfWork());

                var drProfesional = repo.Single(usuario.IdProfesional);

                if (drProfesional != null)
                {
                    if (drProfesional.Cuit == null || drProfesional.Cuit.Trim().Replace("-", "").Length != 11)
                    {
                        throw new Exception("No se puede generar el usuario a un profesional que no posee el Número de CUIT o que el mismo no tiene el formato 20-12345678-0.");
                    }
                    if (usuario.Roles.Any(p => p.RoleName.Contains("Calderas")))
                    {
                        if (!drProfesional.MatriculaMetrogas.HasValue)
                        {
                            throw new Exception("El profesional no posee matrícula de metrogas, no es posible asiganarle el perfil Calderas.");
                        }
                    }
                }
                Guid userid_profesional = Guid.NewGuid();
                try
                {
                    MembershipCreateStatus status;
                    securityProfesionales.CreateUser(usuario.UserName, usuario.UserName, usuario.Email, null, null, true, userid_profesional, out status);
                    if (status == MembershipCreateStatus.Success)
                    {
                        var users = new string[1] { usuario.UserName };

                        if (usuario.Roles.Any())
                            roleProvider.AddUsersToRoles(users, usuario.Roles.Select(p => p.RoleName).ToArray());

                    }
                    else
                        throw new Exception("No se pudo crear el usuario, Error: " + status);
                }
                catch (Exception ex)
                {
                    if (securityProfesionales.GetUser(usuario.UserName, false) != null)
                        securityProfesionales.DeleteUser(usuario.UserName, true);

                    throw ex;
                }

                try
                {
                    uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                    using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                    {
                        repoUPRC = new UsuariosProfesionalesRolesClasificacionRepository(unitOfWork);
                        repo = new ProfesionalesRepository(unitOfWork);
                        var objectEntity = repo.Single(usuario.IdProfesional.Value);

                        foreach (var role in usuario.Roles)
                        {
                            if (role.GruposUsuariosClasificacion != null)
                            {
                                var clasificacion = role.GruposUsuariosClasificacion;

                                clasificacion.IdClasificacion = clasificacion.IdClasificacion;
                                clasificacion.RoleID = role.RoleId;
                                clasificacion.UserID = userid_profesional;
                                clasificacion.CreateUser = usuario.CreateUser;
                                clasificacion.CreateDate = DateTime.Now;
                                clasificacion.Id = repoUPRC.Max() + 1;
                                var entity = mapperBase.Map<UsuariosProfesionalesRolesClasificacionDTO, Rel_UsuariosProf_Roles_Clasificacion>(clasificacion);

                                repoUPRC.Insert(entity);
                            }
                        }

                        if (objectEntity != null)
                        {
                            objectEntity.UserId = userid_profesional;
                            objectEntity.Email = usuario.Email;

                            repo.Update(objectEntity);
                        }



                        //MailMessages.SendActivationClave(user, roles);
                        unitOfWork.Commit();
                    }

                    EmailServiceBL serviceMail = new EmailServiceBL();
                    serviceMail.SendMail(emailEntity);
                }
                catch
                {
                    if (securityProfesionales.GetUser(usuario.UserName, false) != null)
                        securityProfesionales.DeleteUser(usuario.UserName, true);
                    throw;
                }
            }
            else
                ModificarUsuario(usuario);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="roles"></param>
        /// <param name="rolesSeleccionados"></param>
        /// <param name="calificaciones"></param>
        private void ModificarUsuario(UsuarioDTO usuario)
        {
            int id_profesional = 0;
            int.TryParse(usuario.IdProfesional.ToString(), out id_profesional);
            repo = new ProfesionalesRepository(this.uowF.GetUnitOfWork());
            Profesional drProfesional = repo.Single(id_profesional);

            var roles = usuario.Roles.Select(p => p.RoleName).ToArray();

            if (roles.Contains("Calderas"))
            {
                if (!(drProfesional.MatriculaMetrogas.HasValue))
                {
                    throw new Exception("El profesional no posee matrícula de metrogas, no es posible asignarle el perfil Calderas.");
                }
            }

            var users = new string[1] { usuario.UserName };
            string[] rolesdeUsuario = roleProvider.GetRolesForUser(usuario.UserName);
            if (rolesdeUsuario.Any())
                roleProvider.RemoveUsersFromRoles(users, rolesdeUsuario);
            if (roles != null && roles.Length > 0)
                roleProvider.AddUsersToRoles(users, roles);

            try
            {
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new ProfesionalesRepository(unitOfWork);
                    drProfesional = repo.Single(id_profesional);

                    repoUPRC = new UsuariosProfesionalesRolesClasificacionRepository(unitOfWork);                    
                    repoUPRC.RemoveRange(drProfesional.aspnet_Users.Rel_UsuariosProf_Roles_Clasificacion1);


                    foreach (var role in usuario.Roles)
                    {
                        if (role.GruposUsuariosClasificacion != null)
                        {
                            Rel_UsuariosProf_Roles_Clasificacion clasi = new Rel_UsuariosProf_Roles_Clasificacion();
                            clasi.id_rel_prof_clasificacion = repoUPRC.Max() + 1;
                            clasi.UserID = drProfesional.aspnet_Users.UserId;
                            clasi.RoleID = role.RoleId;
                            clasi.id_clasificacion = role.GruposUsuariosClasificacion.IdClasificacion;
                            clasi.CreateUser = usuario.CreateUser;
                            clasi.CreateDate = DateTime.Now;
                            repoUPRC.Insert(clasi);
                        }
                    }

                    drProfesional.Email = usuario.Email;
                    drProfesional.aspnet_Users.aspnet_Membership.Email = usuario.Email;
                    drProfesional.aspnet_Users.aspnet_Membership.LoweredEmail = usuario.Email.ToLower();
                    if (!usuario.Bloqueado)
                    {
                        drProfesional.aspnet_Users.aspnet_Membership.IsLockedOut = false;
                    }
                    else
                    {
                        drProfesional.aspnet_Users.aspnet_Membership.IsLockedOut = true;
                    }

                    repo.Update(drProfesional);


                    unitOfWork.Commit();
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectDto"></param>
        /// <param name="BajaLogica"></param>
        public void Delete(ProfesionalDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {

                    repo = new ProfesionalesRepository(unitOfWork);
                    var objectEntity = repo.Single(objectDto.Id);

                    if (!objectEntity.BajaLogica && !(objectEntity.EncomiendaExt.Any() || objectEntity.Encomienda.Any() || objectEntity.Solicitud.Any()))
                    {
                        repo.Delete(objectEntity);
                    }
                    else
                    {
                        objectEntity.BajaLogica = true;
                        if (objectEntity.aspnet_Users != null)
                            objectEntity.aspnet_Users.aspnet_Membership.IsApproved = false;

                    }
                    unitOfWork.Commit();
                }
            }
            catch (DataException)
            {
                throw new Exception("No es posible eliminar el profesional, tiene datos registrados");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectDTO"></param>
        public void Update(ProfesionalDTO objectDTO)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new ProfesionalesRepository(unitOfWork);
                    var elementDTO = mapperBase.Map<ProfesionalDTO, Profesional>(objectDTO);
                    repo.Update(elementDTO);

                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectDTO"></param>
        public void Insert(ProfesionalDTO objectDTO)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new ProfesionalesRepository(unitOfWork);
                    var elementEntity = mapperBase.Map<ProfesionalDTO, Profesional>(objectDTO);                    
                    repo.Insert(elementEntity);
                    objectDTO.Id = elementEntity.Id;
                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<RolesDTO> TraerPerfilesProfesional(string ApplicationName, Guid UserId)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ProfesionalesRepository(this.uowF.GetUnitOfWork());
                var entityProf = repo.TraerPerfilesProfesional(ApplicationName, UserId);
                var elementsDto = mapperRoles.Map<IEnumerable<aspnet_Roles>, IEnumerable<RolesDTO>>(entityProf);
                return elementsDto;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdProfesional"></param>
        /// <returns></returns>
        public IEnumerable<ProfesionalesPerfilesInhibicionesDTO> TraerProfesional_Perfiles_Inhibiciones(int IdProfesional)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ProfesionalesRepository(this.uowF.GetUnitOfWork());
                var entityProf = repo.TraerProfesional_Perfiles_Inhibiciones(IdProfesional);
                var elementsDto = mapperInhibiciones.Map<IEnumerable<Profesional_Perfiles_Inhibiciones>, IEnumerable<ProfesionalesPerfilesInhibicionesDTO>>(entityProf);
                return elementsDto;
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<ProfesionalDTO> GetByCuit(string cuit)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ProfesionalesRepository(this.uowF.GetUnitOfWork());

                var elements = repo.GetByCuit(cuit);
                var elementsDto = mapperBase.Map<IEnumerable<Profesional>, IEnumerable<ProfesionalDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
