using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBusinessLayer
{
    public interface IUbicacionesBL<T>
    {
        IEnumerable<T> Get(int NroPartida, DateTime FechaActual);
        IEnumerable<T> GetXPartidaHorizontal(int NroPartida, DateTime FechaActual);
        IEnumerable<T> GetXPartidaHorizontal(int NroPartida, DateTime FechaActual, int CodigoCalle);
        IEnumerable<T> Get(int minvaluePuerta, int maxvaluePuerta, DateTime FechaActual, int CodigoCalle, int parimpar);
        IEnumerable<T> Get(int Seccion, string Manzana, string Parcela, DateTime FechaActual);
        IEnumerable<T> GetXTipo(int IdSubTipoUbicacion, DateTime FechaActual);
        IEnumerable<T> GetAll();
        bool Insert(T objectDto);        
		void Update(T objectDto); 
		void Delete(T objectDto); 
        T Single(int IdUbicacion);     
    }
}
