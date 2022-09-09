using System.Collections.Generic;
using System;
using DataTransferObject;

namespace IBusinessLayer
{
    public interface IEncomiendaBL<T>
	{
        int Insert(T objectDto);        
		void Update(T objectDto); 
		void Delete(T objectDto); 
        T Single(int IdEncomienda );          
        IEnumerable<T> GetByFKListIdEncomienda(List<int> lstEncomiendas);
        DateTime GetFechaCertificacion(int id_encomienda);
        int CrearEncomienda(int id_solicitud, string CodigoSeguridad, Guid userid);
        EncomiendaExternaDTO GetEncomiendaExterna(int IdEncomienda);
        void copyTitularesFromEncomienda(int id_solicitud, int id_encomienda, Guid userid);
        IEnumerable<T> GetRangoIdEncomienda(int id_encomiendaDesde, int id_encomiendaHasta);
        IEnumerable<T> GetListaIdEncomienda(List<int> listIDEncomienda);
        void Anular(int IdEncomienda, Guid userid);
    }
}


