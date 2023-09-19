using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IBusinessLayer
{
    public interface ISSITSolicitudesBL<T>
	{
        int Insert(T objectDto);        
		void Update(T objectDto); 
		void Delete(T objectDto); 
        T Single(int IdSolicitud );  
		T GetByFKIdEncomienda(int IdEncomienda);
        T GetAnteriorByFKIdEncomienda(int id_encomienda);
        bool anularSolicitud(int id_solicitud, Guid userid);
        bool confirmarSolicitud(int id_solicitud, Guid userid);
        bool presentarSolicitud(int id_solicitud, Guid userid, byte[] oblea, String emailUsuario);
        Task<bool> ValidacionSolicitudes(int id_solicitud);
        bool ExisteAnexosEnCurso(int id_solicitud);
        bool ExisteAnexosTipoAAprobada(int id_solicitud);
        bool ExisteAnexosNotarialAprobada(int id_solicitud);
        bool CompareWithEncomienda(int id_solicitud);
        bool isProTeatro(int id_solicitud);
    }
}


