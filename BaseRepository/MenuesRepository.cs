using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class MenuesRepository : BaseRepository<InicioTramite_Menues>
    {
        private readonly IUnitOfWork _unitOfWork;
        public MenuesRepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null) throw new ArgumentNullException("unitOfWork Exception");
            _unitOfWork = unit;
        }

        public IEnumerable<InicioTramite_Menues> GetMenuUsuario(Guid userId)
        {
            var perfiles_usuario = _unitOfWork.Db.aspnet_Users.FirstOrDefault(x => x.UserId == userId).InicioTramite_Perfiles.Select(x=> x.id_perfil).SingleOrDefault();
            var userpp = _unitOfWork.Db.aspnet_Users.Where(x => x.UserId == userId).FirstOrDefault();


            var q = (from menu in _unitOfWork.Db.InicioTramite_Menues
                     where menu.InicioTramite_Perfiles.Any(s => s.id_perfil == perfiles_usuario)
                     select new
                     {
                         menu.Id_menu,
                         menu.Descripcion_menu,
                         menu.Pagina_menu,
                         menu.IconCssClass_menu,
                         menu.NroOrden,
                         menu.Id_menu_padre,
                     }
               ).Distinct().OrderBy(x => x.NroOrden);


            List<InicioTramite_Menues> menu_usuario = new List<InicioTramite_Menues>();
            InicioTramite_Menues menu_p;
            foreach (var item in q.ToList())
            {
                menu_p = new InicioTramite_Menues();
                menu_p.Id_menu = (int)item.Id_menu;
                menu_p.Descripcion_menu = (string)item.Descripcion_menu;
                menu_p.Pagina_menu = (string)item.Pagina_menu + "?id_menu=" + menu_p.Id_menu;
                menu_p.IconCssClass_menu = (string)item.IconCssClass_menu;
                menu_p.NroOrden = (int)item.NroOrden;
                menu_p.Id_menu_padre = item.Id_menu_padre;

                menu_usuario.Add(menu_p);
            }

            return menu_usuario;
        }

        public IEnumerable<InicioTramite_Menues> CargarMenu(Guid userid, string userName, int id_menu_padre)
        {


            int[] arrPerfilesUsuario = _unitOfWork.Db.aspnet_Users.FirstOrDefault(x => x.UserId == userid).InicioTramite_Perfiles.ToList().Select(s => s.id_perfil).ToArray();

            var menu_padre = _unitOfWork.Db.InicioTramite_Menues.Where(x => x.Id_menu == id_menu_padre).FirstOrDefault();

            var lstMenues = (from sm in _unitOfWork.Db.InicioTramite_Menues
                             where sm.Id_menu_padre == id_menu_padre
                             //&& (sm.InicioTramite_Perfiles.Any(s => arrPerfilesUsuario.Contains(s.id_perfil)) || userName == Constants.UserGod)
                             && (sm.InicioTramite_Perfiles.Any(s => arrPerfilesUsuario.Contains(s.id_perfil)))
                             orderby sm.NroOrden
                             select sm);

            return lstMenues;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Acceso"></param>
        /// <returns></returns>
        //public bool TienePermiso(Guid userId, string Acceso)
        //{
        //    BAFICOEntyties db = new BAFICOEntyties();

        //    var perfiles_usuario = db.aspnet_Users.FirstOrDefault(x => x.UserId == userId).BAFYCO_Perfiles2.Select(x => x.nombre_perfil).ToList();

        //    foreach (var perfil in perfiles_usuario)
        //    {
        //        var menu_usuario = db.BAFYCO_Perfiles.FirstOrDefault(x => x.nombre_perfil == perfil).BAFYCO_Menues.Select(x => x.pagina_menu).ToList();

        //        if (menu_usuario.Contains(Acceso))
        //            return true;

        //    }
        //    return false;
        //}
    }
}
