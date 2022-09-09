using System;
using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface ITransferenciasSolicitudesBL<T>
	{
        bool Insert(T objectDto);        
		void Update(T objectDto); 
		void Delete(T objectDto); 
        T Single(int IdSolicitud);  
		IEnumerable<T> GetByFKIdConsultaPadron(int IdConsultaPadron);
        void ActualizarEstadoCompleto(T transferencia, Guid userId);
        void ValidarSolicitud(T transferencia);
        void Confirmar(int IdSolicitud, Guid userId);
        int ConfirmarInterno(int IdSolicitud, Guid userId);
        void Anular(int IdSolicitud, Guid userId);
        int CrearTransferencia(int IdConsultaPadron, string CodigoSeguridad, Guid userId);
    }
}


