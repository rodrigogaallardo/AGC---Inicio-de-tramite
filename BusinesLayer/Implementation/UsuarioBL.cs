using AutoMapper;
using BaseRepository;
using IBusinessLayer;
using Dal.UnitOfWork;
using DataAcess;
using DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using UnitOfWork;
using StaticClass;
using System.Web.Security;
using ExternalService;

namespace BusinesLayer.Implementation
{
    public class UsuarioBL : IUsuarioBL<UsuarioDTO>
    {
        private UsuarioRepository repo = null;
        private AspnetUsersRepository repoUser = null;
        private AspnetMembershipRepository repoMember = null;
        private UsuarioConsejoRepository repoUsuarioConsejo = null;

        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;

        public UsuarioBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UsuarioDTO, Usuario>()
                    .ForMember(dest => dest.CodPostal, source => source.MapFrom(p => p.CodigoPostal))
                    .ForMember(dest => dest.SignU, source => source.MapFrom(p => p.sign));

                cfg.CreateMap<Usuario, UsuarioDTO>()
                    .ForMember(dest => dest.CodigoPostal, source => source.MapFrom(p => p.CodPostal))
                    .ForMember(dest => dest.Roles, source => source.MapFrom(p => p.aspnet_Users.aspnet_Roles))
                    .ForMember(dest => dest.sign, source => source.MapFrom(p => p.SignU));


                cfg.CreateMap<UsuarioConsejoDTO, Rel_Usuarios_GrupoConsejo>().ReverseMap()
                    .ForMember(dest => dest.Id, source => source.MapFrom(p => p.id_rel_usugrucon))
                    .ForMember(dest => dest.IdConsejo, source => source.MapFrom(p => p.id_grupoconsejo))
                    .ForMember(dest => dest.UserId, source => source.MapFrom(p => p.userid));

                cfg.CreateMap<Rel_Usuarios_GrupoConsejo, UsuarioConsejoDTO>().ReverseMap()
                    .ForMember(dest => dest.id_rel_usugrucon, source => source.MapFrom(p => p.Id))
                    .ForMember(dest => dest.id_grupoconsejo, source => source.MapFrom(p => p.IdConsejo))
                    .ForMember(dest => dest.userid, source => source.MapFrom(p => p.UserId));

                cfg.CreateMap<aspnet_Users, UsuarioDTO>()
                   .ForMember(dest => dest.Apellido, source => source.MapFrom(p => p.Usuario.Apellido))
                   .ForMember(dest => dest.Nombre, source => source.MapFrom(p => p.Usuario.Nombre))
                   .ForMember(dest => dest.Email, source => source.MapFrom(p => p.Usuario.Email))
                   .ForMember(dest => dest.Consejos, source => source.MapFrom(p => p.Rel_Usuarios_GrupoConsejo));

                cfg.CreateMap<UsuarioDTO, aspnet_Users>()
                    .ForMember(dest => dest.Rel_Usuarios_GrupoConsejo, source => source.MapFrom(p => p.Consejos))
                    .ForMember(dest => dest.UserName, source => source.MapFrom(p => p.UserName))
                    .ForMember(dest => dest.UserId, source => source.MapFrom(p => p.UserId));

                cfg.CreateMap<aspnet_Roles, RolesDTO>();
                cfg.CreateMap<RolesDTO, aspnet_Roles>();

            });
            mapperBase = config.CreateMapper();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public UsuarioDTO Single(Guid UserId)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new UsuarioRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(UserId);
                var entityDto = mapperBase.Map<Usuario, UsuarioDTO>(entity);

                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region Métodos de actualizacion e insert
        /// <summary>
        /// Inserta la entidad para por parametro
        /// </summary>
        /// <param name="objectDto"></param>
        public bool Insert(UsuarioDTO objectDto, EmailEntity emailEntity)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new UsuarioRepository(unitOfWork);
                    var elementDto = mapperBase.Map<UsuarioDTO, Usuario>(objectDto);
                    var insertOk = repo.Insert(elementDto);
                    if (emailEntity != null)
                    {
                        EmailServiceBL serviceMail = new EmailServiceBL();
                        serviceMail.SendMail(emailEntity);
                    }
                    unitOfWork.Commit();

                    return true;
                }
            }
            catch (Exception ex)
            {
                Membership.DeleteUser(objectDto.UserName);
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuarioDTO"></param>
        /// <returns></returns>
        public UsuarioDTO InsertMembership(UsuarioDTO usuarioDTO)
        {
            Guid userid = Guid.NewGuid();
            MembershipCreateStatus status;
            MembershipUser user = Membership.CreateUser(usuarioDTO.UserName, usuarioDTO.UserName, usuarioDTO.Email, null, null, true, userid, out status);
            if (status == MembershipCreateStatus.Success)
            {
                string[] strRoles = Roles.GetRolesForUser(user.UserName);
                if (strRoles.Length > 0)
                    Roles.RemoveUserFromRoles(user.UserName, strRoles);

                foreach (var item in usuarioDTO.Roles)
                    Roles.AddUserToRole(user.UserName, item.RoleName);

                usuarioDTO.UserId = userid;
                try
                {
                    uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                    using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                    {
                        repoUser = new AspnetUsersRepository(unitOfWork);
                        repo = new UsuarioRepository(unitOfWork);

                        var usuarioCreator = repo.Single(usuarioDTO.CreateUser);

                        if (!usuarioCreator.aspnet_Users.Rel_Usuarios_GrupoConsejo.Any())
                            throw new Exception("No se puede ingresar el usuario debido a que no se encuentra configurado el Consejo para el usuario que quiere generar el ingreso.Por favor comuniquese con el Administrador del Sistema.");

                        //El usuario que crea el usuario es de tal consejo
                        int IdConsejo = usuarioCreator.aspnet_Users.Rel_Usuarios_GrupoConsejo.FirstOrDefault().id_grupoconsejo;



                        usuarioDTO.Consejos = new List<UsuarioConsejoDTO>();

                        usuarioDTO.Consejos.Add(new UsuarioConsejoDTO()
                        {
                            IdConsejo = IdConsejo,
                            UserId = userid
                        });

                        var usuarioEntity = mapperBase.Map<UsuarioDTO, Usuario>(usuarioDTO);
                        repo.Insert(usuarioEntity);

                        var usuarioAspNetEntity = repoUser.Single(usuarioDTO.UserId);
                        var consejo = mapperBase.Map<ICollection<UsuarioConsejoDTO>, ICollection<Rel_Usuarios_GrupoConsejo>>(usuarioDTO.Consejos);

                        usuarioAspNetEntity.Rel_Usuarios_GrupoConsejo = consejo;

                        repoUser.Update(usuarioAspNetEntity);

                        usuarioDTO.UserId = userid;

                        unitOfWork.Commit();

                        return usuarioDTO;

                    }
                }
                catch (Exception ex)
                {
                    if (Membership.GetUser(usuarioDTO.UserName, false) != null)
                        Membership.DeleteUser(usuarioDTO.UserName, true);
                    throw ex;
                }
            }
            else
                throw new Exception(Enum.GetName(typeof(MembershipCreateStatus), status));
        }

        #endregion
        #region Métodos de actualizacion e insert
        public void UpdateMembership(UsuarioDTO usuarioDTO)
        {
            string[] strRoles = Roles.GetRolesForUser(usuarioDTO.UserName);
            if (strRoles.Length > 0)
                Roles.RemoveUserFromRoles(usuarioDTO.UserName, strRoles);

            foreach (var item in usuarioDTO.Roles)
                Roles.AddUserToRole(usuarioDTO.UserName, item.RoleName);


            usuarioDTO.UserId = (Guid)Membership.GetUser(usuarioDTO.UserName, false).ProviderUserKey;

            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repoUser = new AspnetUsersRepository(unitOfWork);
                    repo = new UsuarioRepository(unitOfWork);

                    var user = repoUser.Single(usuarioDTO.UserId);

                    user.aspnet_Membership.IsApproved = !usuarioDTO.Bloqueado;

                    if (usuarioDTO.Bloqueado && user.aspnet_Membership.IsLockedOut)
                        user.aspnet_Membership.IsApproved = true;

                    repoUser.Update(user);

                    var usuarioEntity = mapperBase.Map<UsuarioDTO, Usuario>(usuarioDTO);
                    repo.Update(usuarioEntity);

                    unitOfWork.Commit();
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Modifica la entidad para por parametro
        /// </summary>
        /// <param name="objectDto"></param>
        public void Update(UsuarioDTO objectDTO)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new UsuarioRepository(unitOfWork);
                    var elementDTO = mapperBase.Map<UsuarioDTO, Usuario>(objectDTO);
                    repo.Update(elementDTO);
                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion
        #region Métodos de actualizacion e insert
        /// <summary>
        /// elimina la entidad para por parametro
        /// </summary>
        /// <param name="objectDto"></param>      
        public void Delete(UsuarioDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                repoUser = new AspnetUsersRepository(this.uowF.GetUnitOfWork());

                var userEntity = repoUser.Single(objectDto.UserId);
                if (userEntity != null)
                {
                    string[] strRoles = Roles.GetRolesForUser(userEntity.UserName);
                    if (strRoles.Length > 0)
                        Roles.RemoveUserFromRoles(userEntity.UserName, strRoles);

                    using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                    {
                        repo = new UsuarioRepository(unitOfWork);
                        repoUser = new AspnetUsersRepository(unitOfWork);
                        repoMember = new AspnetMembershipRepository(unitOfWork);
                        repoUsuarioConsejo = new UsuarioConsejoRepository(unitOfWork);

                        userEntity = repoUser.Single(objectDto.UserId);

                        repoMember.Delete(userEntity.aspnet_Membership);
                        repoUsuarioConsejo.RemoveRange(userEntity.Rel_Usuarios_GrupoConsejo);
                        repo.Delete(userEntity.Usuario);
                        repoUser.Delete(userEntity);

                        unitOfWork.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        #endregion

        public IEnumerable<UsuarioDTO> Get(string strUsername, string strApellido, string strNombres, int id_grupoconsejo, string ApplicationName)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repoUser = new AspnetUsersRepository(this.uowF.GetUnitOfWork());
                var entity = repoUser.Get(strUsername, strApellido, strNombres, id_grupoconsejo, ApplicationName);
                var entityDto = mapperBase.Map<IEnumerable<aspnet_Users>, IEnumerable<UsuarioDTO>>(entity);

                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int TransferirUsuario(Guid userid_nuevo, Guid userid_anterior)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new UsuarioRepository(this.uowF.GetUnitOfWork());
                return repo.TransferirUsuario(userid_nuevo, userid_anterior);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

