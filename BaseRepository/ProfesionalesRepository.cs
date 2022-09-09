using Dal.UnitOfWork;
using DataAcess;
using DataAcess.EntityCustom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRepository
{
    public class ProfesionalesRepository : BaseRepository<Profesional>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProfesionalesRepository(IUnitOfWork unit)
                : base(unit)
        {
            if (unit == null) throw new ArgumentNullException("unitOfWork Exception");
            _unitOfWork = unit;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public Profesional Get(Guid UserId)
        {
            Profesional p = _unitOfWork.Db.Profesional.FirstOrDefault(x => x.UserId == UserId);
            return p;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdProfesional"></param>
        /// <returns></returns>
        public Profesional GetProfesional(int IdProfesional)
        {
            Profesional p = _unitOfWork.Db.Profesional.FirstOrDefault(x => x.Id == IdProfesional);
            return p;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<string> GetLocalidadProfesional()
        {
            var q = (from prof in _unitOfWork.Db.Profesional
                     where prof.Localidad.Length > 3
                     select prof.Localidad
                     ).ToList();

            return q;
        }
        public List<aspnet_Applications> GetAppByUser(Guid BusUser)
        {
            var query = (from usu in _unitOfWork.Db.aspnet_Roles.Where(x => x.aspnet_Users.Any(y => y.UserId == BusUser))
                         join app in _unitOfWork.Db.aspnet_Applications on usu.ApplicationId equals app.ApplicationId
                         select app).ToList();



            return query;
        }
        public List<Encomienda_URLxROL> GetEncURL(Guid BusUser)
        {
            var query = (from usu in _unitOfWork.Db.aspnet_Roles.Where(x => x.aspnet_Users.Any(y => y.UserId == BusUser))
                         join url in _unitOfWork.Db.Encomienda_URLxROL on usu.RoleId equals url.RoleId
                         select url).GroupBy(g => g.url).Select(x => x.FirstOrDefault()).ToList();



            return query;
        }
        public List<aspnet_Roles> GetRolxAppId(Guid AppId, Guid BusUser)
        {
            var query = (from rol in _unitOfWork.Db.aspnet_Roles.Where(x => x.ApplicationId == AppId && x.aspnet_Users.Any(y => y.UserId == BusUser))
                         select rol).OrderBy(x => x.RoleId).ToList();

            return query;
        }

        public List<aspnet_Roles> GetRolxURL(string url)
        {
            var query = (from rurl in _unitOfWork.Db.Encomienda_URLxROL.Where(x => x.url == url)
                         join rol in _unitOfWork.Db.aspnet_Roles on rurl.RoleId equals rol.RoleId
                         select rol).OrderBy(x => x.RoleId).ToList();

            return query;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="BusMatricula"></param>
        /// <param name="BusNomyApe"></param>
        /// <param name="BusGrupoConsejo"></param>
        /// <param name="BusConsejo"></param>
        /// <param name="BusLocalidad"></param>
        /// <returns></returns>
        public IQueryable<Profesional> GetProfesionales(string BusMatricula, string BusNomyApe, int BusGrupoConsejo, int BusConsejo, string BusLocalidad)
        {
            var q = (from profesionales in _unitOfWork.Db.Profesional.Where(y => y.BajaLogica == false)
                     where profesionales.ConsejoProfesional.id_grupoconsejo != 0
                     select profesionales);

            //Filtro NºMatricula
            if (BusMatricula.Length > 0)
                q = q.Where(x => x.Matricula == BusMatricula);

            //Filtro Apellido y Nombre
            if (BusNomyApe.Length > 0)
            {
                string[] valores = BusNomyApe.Trim().Split(' ');
                for (int i = 0; i <= valores.Length - 1; i++)
                {
                    string valor = valores[i];
                    q = q.Where(x => x.Apellido.Contains(valor.Trim()) || x.Nombre.Contains(valor.Trim()));
                }
            }
            //Filtro "Consejo"
            if (BusGrupoConsejo != 99)
                q = q.Where(x => x.ConsejoProfesional.id_grupoconsejo == BusGrupoConsejo);

            if (BusConsejo != 99)
                q = q.Where(x => x.IdConsejo == BusConsejo);

            if (BusLocalidad.Length > 3)
                q = q.Where(x => x.Localidad.Contains(BusLocalidad));

            q = q.Where(x => x.aspnet_Users.aspnet_Roles.Select(y => y.RoleName).Contains("EncomiendaHabilitaciones"));

            return q;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Profesional> Get(int IdGrupoConsejo, string ApellidoNombre, int? Dni, string Matricula, string Cuit, bool? Baja, bool? bloqueado, bool? inhibido, string userName)
        {
            var profesionales = (from prof in _unitOfWork.Db.Profesional
                                 join con in _unitOfWork.Db.ConsejoProfesional on prof.IdConsejo equals con.Id
                                 where con.id_grupoconsejo == IdGrupoConsejo
                                 select prof
                                     );

            if (!string.IsNullOrWhiteSpace(ApellidoNombre))
                profesionales = profesionales.Where(p => p.Apellido.Contains(ApellidoNombre) || p.Nombre.Contains(ApellidoNombre));

            if (!string.IsNullOrWhiteSpace(userName))
                profesionales = profesionales.Where(p => p.aspnet_Users.UserName.ToUpper() == userName.ToUpper());

            if (Dni.HasValue)
                profesionales = profesionales.Where(p => p.NroDocumento == Dni);

            if (!string.IsNullOrWhiteSpace(Matricula))
                profesionales = profesionales.Where(p => p.Matricula.Equals(Matricula));

            if (inhibido.HasValue)
            {
                profesionales = profesionales.Where(p => p.InhibidoBit == inhibido);
            }

            if (bloqueado.HasValue)
            {
                profesionales = profesionales.Where(p => p.aspnet_Users.aspnet_Membership.IsLockedOut == bloqueado);                
            }

            if (Baja.HasValue)
            {
                profesionales = profesionales.Where(p => p.BajaLogica == Baja);
            }

            return profesionales;
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
        public IEnumerable<Profesional> Get(int IdGrupoConsejo, Guid RoleId, string ApellidoNombre, string UserName, string Matricula, bool? Baja)
        {
            var profesionales = (from prof in _unitOfWork.Db.Profesional
                                 join con in _unitOfWork.Db.ConsejoProfesional on new { IdConsejo = prof.IdConsejo.Value } equals new { IdConsejo = con.Id }
                                 join usu in _unitOfWork.Db.aspnet_Users on new { UserId = prof.UserId.Value } equals new { UserId = usu.UserId } into usu_join
                                 from usu in usu_join.DefaultIfEmpty()
                                 where
                                     con.id_grupoconsejo == IdGrupoConsejo
                                     && ((RoleId != Guid.Empty && usu.aspnet_Roles.Any(a => a.RoleId == RoleId)) || RoleId == Guid.Empty)
                                 select prof);

            if (!string.IsNullOrWhiteSpace(ApellidoNombre))
                profesionales = profesionales.Where(p => p.Apellido.Contains(ApellidoNombre) || p.Nombre.Contains(ApellidoNombre));

            if (!string.IsNullOrWhiteSpace(UserName))
                profesionales = profesionales.Where(p => p.aspnet_Users.UserName.Equals(UserName));

            if (!string.IsNullOrWhiteSpace(Matricula))
                profesionales = profesionales.Where(p => p.Matricula.Equals(Matricula));

            if (Baja.HasValue)
            {
                profesionales = profesionales.Where(p => p.BajaLogica == Baja);
            }

            return profesionales;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ApplicationName"></param>
        /// <returns></returns>
        public IQueryable<aspnet_Roles> TraerPerfilesProfesional(string ApplicationName, Guid UserId)
        {

            return (from rol in _unitOfWork.Db.aspnet_Roles
                    where rol.aspnet_Applications.ApplicationName.Equals(ApplicationName)
                && rol.aspnet_Users.Any(p => p.UserId == UserId)
                    select rol);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdProfesional"></param>
        /// <returns></returns>
        public IEnumerable<Profesional_Perfiles_Inhibiciones> TraerProfesional_Perfiles_Inhibiciones(int IdProfesional)
        {
            return (from rol in _unitOfWork.Db.aspnet_Roles
                    join perf_inh in _unitOfWork.Db.Profesional_Perfiles_Inhibiciones on rol.RoleId equals perf_inh.RoleId
                    where perf_inh.id_Prof == IdProfesional
                    select perf_inh);
        }

        public virtual IEnumerable<Profesional> GetByCuit(string cuit)
        {
            return (from con in _unitOfWork.Db.ConsejoProfesional
                    join p in _unitOfWork.Db.Profesional on con.Id equals p.IdConsejo
                    where p.Cuit == cuit
                    select p);
        }

        public bool ExisteMatricula(int consejo, string matricula)
        {
            return (from prof in _unitOfWork.Db.Profesional
                    where (prof.Matricula == matricula) && (prof.IdConsejo == consejo)
                    select prof).Count() == 0;
        }
    }
}
