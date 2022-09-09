using BusinesLayer.Implementation;
using DataAcess;
using DataTransferObject;
using ExternalService;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork;
using System.Data.Linq;

namespace RegenerarPDFs
{
    public class RegenerarManifiestoTransmision
    {
        public void Procesar()
        {
            Guid userid = Guid.Parse("4cab5874-b6cc-4735-8c6b-7f3bb79390a8");   // usuario Procesos
            Logger.WriteLine(string.Format("{0} - Inicio del proceso", DateTime.Now));
            Logger.WriteLine("Armando la lista a procesar...");

            var lstTramites = GetSolicitudesaProcesar();
            int i = 0;
            int CantSolicitudes = lstTramites.Count;
            foreach (int id_solicitud in lstTramites)
            {
                i++;
                Logger.WriteLine(string.Format("Procesando Solicitud {0} - {1} de {2}", id_solicitud, i, CantSolicitudes));
                var inicio = DateTime.Now;
                RegenerarSolicitud(id_solicitud, userid);
                Logger.WriteLine(string.Format("Tiempo de proceso solicitud {0}: {1} s.", id_solicitud, (DateTime.Now - inicio).TotalSeconds));
            }
            Logger.WriteLine(string.Format("{0} - fin del proceso", DateTime.Now));
            Logger.SaveLog(true);
            
        }
        public List<int> GetSolicitudesaProcesar()
        {
            List<int> lstResult = new List<int>();

            var uowF = new TransactionScopeUnitOfWorkFactory();
            var uof = uowF.GetUnitOfWork();
            var fDesde = new DateTime(2021, 10, 13);
            var fHasta = DateTime.Now;
            /*
            lstResult = uof.Db.Transf_Solicitudes_HistorialEstados
                        .Where(x => x.cod_estado_nuevo == "ETRA" && x.fecha_modificacion >= fDesde && x.fecha_modificacion <= fHasta)
                        .Select(x => x.id_solicitud)
                        .ToList();
            */
            lstResult.Add(4520);
            lstResult.Add(4650);
            lstResult.Add(4392);
            return lstResult;
        }
        public void RegenerarSolicitud(int id_solicitud, Guid userid)
        {
            ExternalService.Class.ReportingEntity ReportingEntity = new ExternalService.Class.ReportingEntity();
            ExternalServiceFiles esf = new ExternalServiceFiles();
            TransferenciasDocumentosAdjuntosBL trDocAdjBL = new TransferenciasDocumentosAdjuntosBL();
            
            byte[] pdfSolicitud = new byte[0];
            string arch = "ManifiestoTransmision-" + id_solicitud.ToString() + ".pdf";

            int id_tipodocsis = 0;
            id_tipodocsis = (int)Constantes.TiposDeDocumentosSistema.MANIFIESTO_TRANSMISION;

            var DocAdjDTO = trDocAdjBL.GetByFKIdSolicitud(id_solicitud).Where(x => x.IdTipoDocsis == (int)Constantes.TiposDeDocumentosSistema.MANIFIESTO_TRANSMISION).FirstOrDefault();

            ExternalServiceReporting reportingService = new ExternalServiceReporting();

            ReportingEntity = reportingService.GetPDFTransmision(id_solicitud, true);

            pdfSolicitud = ReportingEntity.Reporte;
            int id_file = ReportingEntity.Id_file;

            if (DocAdjDTO != null)
            {
                if (id_file != DocAdjDTO.IdFile)
                    esf.deleteFile(DocAdjDTO.IdFile);
                DocAdjDTO.IdFile = id_file;
                DocAdjDTO.NombreArchivo = arch;
                DocAdjDTO.UpdateDate = DateTime.Now;
                DocAdjDTO.UpdateUser = userid;
                trDocAdjBL.Update(DocAdjDTO);
            }
            else
            {
                DocAdjDTO = new TransferenciasDocumentosAdjuntosDTO();
                DocAdjDTO.IdSolicitud = id_solicitud;
                DocAdjDTO.IdTipoDocumentoRequerido = 0;
                DocAdjDTO.TipoDocumentoRequeridoDetalle = "";
                DocAdjDTO.GeneradoxSistema = true;
                DocAdjDTO.CreateDate = DateTime.Now;
                DocAdjDTO.CreateUser = userid;
                DocAdjDTO.NombreArchivo = arch;
                DocAdjDTO.IdFile = id_file;
                DocAdjDTO.IdTipoDocsis = id_tipodocsis;

                trDocAdjBL.Insert(DocAdjDTO, true);
            }
        }
    }
}
