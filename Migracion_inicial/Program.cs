using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinesLayer.Implementation;
using StaticClass;

namespace Migracion_inicial
{
    class Program
    {
        static void Main(string[] args)
        {

            int cantReg = 0;
            EncomiendaBL encomiendaBL = new EncomiendaBL();
            
            
            DGHP_SolicitudesEntities db = new DGHP_SolicitudesEntities();

            var lstSolicitudes = (from sol in db.SSIT_Solicitudes
                                join enc in db.Encomienda on sol.id_solicitud equals enc.id_solicitud
                                where string.IsNullOrEmpty(enc.tipo_anexo) 
                                orderby sol.id_solicitud
                                select sol.id_solicitud).Distinct().ToList();

            foreach (int id_solicitud in lstSolicitudes)
            {
                TimeSpan stop;
                TimeSpan start = new TimeSpan(DateTime.Now.Ticks);

                try
                {
                    cantReg++;
                    var lstEncomiendas = db.Encomienda.Where(x => x.id_solicitud == id_solicitud && x.id_estado == (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo).OrderBy(o => o.id_encomienda).ToList();


                    if (lstEncomiendas.Count >= 2)
                    {
                        
                        for( int nroReg = 0; nroReg < lstEncomiendas.Count -1 ; nroReg++)
                        if (nroReg < lstEncomiendas.Count)
                        {
                            var encomienda1 = lstEncomiendas[nroReg];
                            var encomienda2 = lstEncomiendas[nroReg + 1];

                            Console.WriteLine(string.Format("Encomienda 1 {0}, Encomienda 2: {1}", encomienda1.id_encomienda,encomienda2.id_encomienda ));

                            if (encomienda1 != null && encomienda2 != null)
                            {
                                bool iguales = encomiendaBL.CompareBetween(encomienda1.id_encomienda, encomienda2.id_encomienda);
                                string tipo_anexo = (iguales ? "B" : "A");

                                // Actualiza el tipo de anexo con lo obtenido
                                encomienda2.tipo_anexo = tipo_anexo;
                                db.SaveChanges();

                            }

                        }

                            
                    }
                }
                catch (Exception ex) 
                {
                    Console.WriteLine(string.Format("Error en la solicitud {0} -----------------------------------------------------------------------------------------", id_solicitud));
                }
                
                stop = new TimeSpan(DateTime.Now.Ticks);
                Console.WriteLine(string.Format("Solicitud {0}, procesadas: {1},  restan {2}, tiempo {3} seg.", id_solicitud, cantReg, lstSolicitudes.Count() - cantReg, stop.Subtract(start).TotalMilliseconds / 1000.0000));

            }

            db.Dispose();

        }
    }
}
