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
using DataAcess.EntityCustom;
using System.Web.Security;

namespace BusinesLayer.Implementation
{
    public class ConsejoProfesionalBL : IConsejoProfesionalBL<ConsejoProfesionalDTO>
    {
        private ConsejoProfesionalRepository repo = null;
        private IUnitOfWorkFactory uowF = null;

        IMapper mapperBase;
        IMapper mapperRoles;

        public ConsejoProfesionalBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ConsejoProfesional, ConsejoProfesionalDTO>()
                    .ForMember(dest => dest.GrupoConsejosDTO, opt => opt.MapFrom(source => source.GrupoConsejos));

                cfg.CreateMap<GrupoConsejos, GrupoConsejosDTO>()
                  .ForMember(dest => dest.Descripcion, source => source.MapFrom(p => p.descripcion_grupoconsejo))
                  .ForMember(dest => dest.Id, source => source.MapFrom(p => p.id_grupoconsejo))
                  .ForMember(dest => dest.LogoImpresion, source => source.MapFrom(p => p.logo_impresion_grupoconsejo))
                  .ForMember(dest => dest.LogoPantalla, source => source.MapFrom(p => p.logo_pantalla_grupoconsejo))
                  .ForMember(dest => dest.Nombre, source => source.MapFrom(p => p.nombre_grupoconsejo));

            });
            mapperBase = config.CreateMapper();

            var configRoles = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<aspnet_Roles, RolesDTO>()
                    .ForMember(dest => dest.RolesGruposClasificacion, opt => opt.MapFrom(source => source.GrupoConsejos_Roles_Clasificacion));

                cfg.CreateMap<RolesDTO, aspnet_Roles>()
                    .ForMember(dest => dest.Rel_UsuariosProf_Roles_Clasificacion, opt => opt.MapFrom(source => source.GruposUsuariosClasificacion))
                    .ForMember(dest => dest.GrupoConsejos_Roles_Clasificacion, opt => opt.MapFrom(source => source.RolesGruposClasificacion));

                cfg.CreateMap<GrupoConsejosRolesClasificacionDTO, GrupoConsejos_Roles_Clasificacion>().ReverseMap()
                    .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(source => source.descripcion_clasificacion))
                    .ForMember(dest => dest.IdClasificacion, opt => opt.MapFrom(source => source.id_clasificacion))
                    .ForMember(dest => dest.RoleId, opt => opt.MapFrom(source => source.RoleID));

                cfg.CreateMap<GrupoConsejos_Roles_Clasificacion, GrupoConsejosRolesClasificacionDTO>().ReverseMap()
                    .ForMember(dest => dest.descripcion_clasificacion, opt => opt.MapFrom(source => source.Descripcion))
                    .ForMember(dest => dest.id_clasificacion, opt => opt.MapFrom(source => source.IdClasificacion))
                    .ForMember(dest => dest.RoleID, opt => opt.MapFrom(source => source.RoleId));
            });
            mapperRoles = configRoles.CreateMapper();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_grupoconsejo"></param>
        /// <returns></returns>
        public IEnumerable<ConsejoProfesionalDTO> GetConsejosxGrupo(int id_grupoconsejo)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ConsejoProfesionalRepository(this.uowF.GetUnitOfWork());

                var elements = repo.GetConsejosxGrupo(id_grupoconsejo);
                var elementsDto = mapperBase.Map<IEnumerable<ConsejoProfesional>, IEnumerable<ConsejoProfesionalDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<ConsejoProfesionalDTO> GetGrupoConsejo(Guid userId)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ConsejoProfesionalRepository(this.uowF.GetUnitOfWork());

                var elements = repo.GetGrupoConsejo(userId);
                var elementsDto = mapperBase.Map<IEnumerable<ConsejoProfesional>, IEnumerable<ConsejoProfesionalDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdConsejoGrupo"></param>
        /// <returns></returns>
        public IEnumerable<RolesDTO> TraerPerfilesProfesionalXGrupo(int IdConsejoGrupo)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ConsejoProfesionalRepository(this.uowF.GetUnitOfWork());

                var elements = repo.TraerPerfilesProfesionalXGrupo(IdConsejoGrupo);

                var elementsDto = mapperRoles.Map<IEnumerable<aspnet_Roles>, IEnumerable<RolesDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<RolesDTO> TraerPerfilesProfesionalXGrupo(int IdConsejoGrupo, Guid UserId)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ConsejoProfesionalRepository(this.uowF.GetUnitOfWork());

                var elements = repo.TraerPerfilesProfesionalXGrupo(IdConsejoGrupo, UserId);
                var rolesSeleccionados = mapperRoles.Map<IEnumerable<aspnet_Roles>, IEnumerable<RolesDTO>>(elements).ToList();

                var calificacionesSeleccionadas = repo.TraerCalificacionesSeleccionadas(IdConsejoGrupo, UserId).ToList();

                foreach (var rolSeleccionado in rolesSeleccionados)
                {
                    if (calificacionesSeleccionadas.Any(p => p.RoleID == rolSeleccionado.RoleId))
                    {
                        var calificacionSeleccionda = calificacionesSeleccionadas.Where(p => p.RoleID == rolSeleccionado.RoleId).FirstOrDefault();

                        rolSeleccionado.GruposUsuariosClasificacion = new UsuariosProfesionalesRolesClasificacionDTO();

                        rolSeleccionado.GruposUsuariosClasificacion.Id = calificacionSeleccionda.id_rel_prof_clasificacion;
                        rolSeleccionado.GruposUsuariosClasificacion.IdClasificacion = calificacionSeleccionda.id_clasificacion;
                    }
                }
                return rolesSeleccionados;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

