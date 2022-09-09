using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBusinessLayer
{
    public interface ITransferenciasFirmantesSolicitudPersonasFisicasBL<T>
    {
        IEnumerable<T> GetAll();
        bool Insert(T objectDto);
        //void Update(T objectDto);
        //void Delete(T objectDto);
        T Single(int IdPersonaFisica);
        IEnumerable<T> GetByFKIdSolicitud(int IdSolicitud);
        //IEnumerable<T> GetByFKIdTipoDocumentoPersonal(int IdTipoDocumentoPersonal);
        //IEnumerable<T> GetByFKIdTipoiibb(int IdTipoiibb);
        //IEnumerable<T> GetByFKIdLocalidad(int IdLocalidad);
    }
}
