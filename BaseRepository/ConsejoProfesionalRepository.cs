using Dal.UnitOfWork;
using DataAcess;
using DataAcess.EntityCustom;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BaseRepository
{
    /// <summary>
    /// Representa a la entidad rubros del Schema DTO
    /// </summary>
    public class ConsejoProfesionalRepository : BaseRepository<ConsejoProfesional>
    {

        private readonly IUnitOfWork _unitOfWork;

        public ConsejoProfesionalRepository()
        {
        }

        public ConsejoProfesionalRepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null) throw new ArgumentNullException("unitOfWork Exception");
            _unitOfWork = unit;
        }
     
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_grupoconsejo"></param>
        /// <returns></returns>
        public virtual IEnumerable<ConsejoProfesional> GetConsejosxGrupo(int id_grupoconsejo)
        {
            return (from con in _unitOfWork.Db.ConsejoProfesional.Where(x => x.id_grupoconsejo == id_grupoconsejo)
                     select con);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public virtual IEnumerable<ConsejoProfesional> GetGrupoConsejo(Guid userId)
        {
            var q = (from rel in _unitOfWork.Db.Rel_Usuarios_GrupoConsejo
                     join gru in _unitOfWork.Db.GrupoConsejos on rel.id_grupoconsejo equals gru.id_grupoconsejo
                     join con in _unitOfWork.Db.ConsejoProfesional on rel.id_grupoconsejo equals con.id_grupoconsejo
                     where rel.userid == userId
                     select con);

            return q;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_grupoconsejo"></param>
        /// <returns></returns>
        public virtual IEnumerable<aspnet_Roles> TraerPerfilesProfesionalXGrupo(int id_grupoconsejo)
        {
            return (from c in _unitOfWork.Db.ConsejoProfesional_RolesPermitidos                    
                    where c.id_grupoconsejo == id_grupoconsejo
                    select c.aspnet_Roles
              );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_grupoconsejo"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public virtual IEnumerable<aspnet_Roles> TraerPerfilesProfesionalXGrupo(int id_grupoconsejo, Guid userId)
        {
            return (from c in _unitOfWork.Db.ConsejoProfesional_RolesPermitidos
                    join rel in _unitOfWork.Db.Rel_UsuariosProf_Roles_Clasificacion on c.RoleID equals rel.RoleID 
                    where c.id_grupoconsejo == id_grupoconsejo
                    && rel.UserID == userId
                    select c.aspnet_Roles
              );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdRole"></param>
        /// <returns></returns>
        public virtual IEnumerable<Rel_UsuariosProf_Roles_Clasificacion> TraerCalificacionesSeleccionadas(int id_grupoconsejo, Guid userId)
        {
            return (from c in _unitOfWork.Db.ConsejoProfesional_RolesPermitidos
                    join rel in _unitOfWork.Db.Rel_UsuariosProf_Roles_Clasificacion on c.RoleID equals rel.RoleID
                    where c.id_grupoconsejo == id_grupoconsejo
                    && rel.UserID == userId
                    select rel
              );
        }
    }
}
