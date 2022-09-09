using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcess;
using IBaseRepository;
using Dal.UnitOfWork;


namespace BaseRepository
{
    public class AspnetUsersRepository : BaseRepository<aspnet_Users>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AspnetUsersRepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null) throw new ArgumentNullException("unitOfWork Exception");
            _unitOfWork = unit;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strUsername"></param>
        /// <param name="strApellido"></param>
        /// <param name="strNombres"></param>
        /// <param name="id_grupoconsejo"></param>
        /// <param name="ApplicationName"></param>
        /// <returns></returns>
        public IEnumerable<aspnet_Users> Get(string strUsername, string strApellido, string strNombres, int id_grupoconsejo, string ApplicationName)
        {
            var query =
                 (from usu in _unitOfWork.Db.aspnet_Users
                  join rel in _unitOfWork.Db.Rel_Usuarios_GrupoConsejo on usu.UserId equals rel.userid
                  where
                    usu.aspnet_Membership.aspnet_Applications.LoweredApplicationName.Equals(ApplicationName)
                    && rel.id_grupoconsejo == id_grupoconsejo
                  select usu
                      );

            if (!string.IsNullOrEmpty(strUsername))
                query = query.Where(p => p.UserName.Contains(strUsername));

            if (!string.IsNullOrEmpty(strNombres))
                query = query.Where(p => p.Usuario.Nombre.Contains(strNombres));

            if (!string.IsNullOrEmpty(strApellido))
                query = query.Where(p => p.Usuario.Apellido.Contains(strApellido));

            return query;
        }


        public aspnet_Users Get(string strUsername)
        {
            var query =
                 (from usu in _unitOfWork.Db.aspnet_Users
                  where
                    usu.UserName == strUsername
                  select usu
                      );
            return query.FirstOrDefault();
        }
    }


}

