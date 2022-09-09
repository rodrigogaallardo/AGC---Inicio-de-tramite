using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBusinessLayer
{
    public interface ISSITListado_ProfesionalesBL<T>
    {
        List<T> GetProfesionalesSolicitud(int BusCircuito, int startRowIndex, int maximumRows, string sortByExpression, string profesional, out int totalRowCount);

    }
}
