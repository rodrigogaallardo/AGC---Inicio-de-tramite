using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegenerarPDFs
{
    class Program
    {
        static void Main(string[] args)
        {
            
            RegenerarManifiestoTransmision proc = new RegenerarManifiestoTransmision();
            proc.Procesar();
            Logger.WriteLine("Presiones ENTER para cerrar...");

        }

        
    }
}
