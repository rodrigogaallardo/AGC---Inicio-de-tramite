using DataTransferObject;
using System;
using System.Collections.Generic;

namespace IBusinessLayer
{
    public interface IConsultaPadronSolicitudesBL<T>
	{
        bool Insert(T objectDto);        
		void Update(T objectDto); 
		void Delete(T objectDto); 
        T Single(int IdConsultaPadron );
        void ConfirmarTramite(int IdConsultaPadron, Guid userId);
        void AnularTramite(int IdConsultaPadron, Guid userId);
        IEnumerable<ItemDirectionDTO> GetDireccionesCpadron(List<int> lstSolicitudes);
        void ActualizarZonaDeclarada(int IdSolicitud, string Zona);        
        string ActualizarEstadoConsultaPadron(ref T consultaPadron, Guid userId);
    }
}


