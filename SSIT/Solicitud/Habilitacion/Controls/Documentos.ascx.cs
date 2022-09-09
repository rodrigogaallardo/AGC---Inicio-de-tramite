using BusinesLayer.Implementation;
using DataTransferObject;
using ExternalService;
using SSIT.Common;
using SSIT.Solicitud.Controls;
using StaticClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSIT.Solicitud.Habilitacion.Controls
{
    public partial class Documentos : System.Web.UI.UserControl
    {

        TiposDeDocumentosRequeridosBL blTdocReq = new TiposDeDocumentosRequeridosBL();

        private int id_solicitud
        {
            get
            {
                int ret = 0;
                int.TryParse(hid_doc_id_solicitud.Value, out ret);
                return ret;
            }
            set
            {
                hid_doc_id_solicitud.Value = value.ToString();
            }
        }

        public class ucRecargarPagosEventsArgs : EventArgs
        {
            public int id_solicitud { get; set; }
            public Constantes.PagosTipoTramite tipo_tramite { get; set; }
        }

        public class ucLabelProTeatroEventsArgs : EventArgs
        {
            public bool chequeado { get; set; }
        }

        public delegate void EventHandlerRecargarPagos(object sender, ucRecargarPagosEventsArgs ev);
        public event EventHandlerRecargarPagos EventRecargarPagos;

        //evento error en cargadocumento



        protected void OnErrorCargaDocumentoClick(object sender, CargaDocumentoErrorEventArgs e)
        {
            lblError.Text = e.Description;
            ScriptManager.RegisterClientScriptBlock(pnlDatosDocumento, pnlDatosDocumento.GetType(), "mostrarError", "showfrmErrorDocumentos(); ", true);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void CargarProTeatro(int id_solicitud)
        {
            SSITSolicitudesBL ssitBL = new SSITSolicitudesBL();
            EncomiendaBL encBL = new EncomiendaBL();
            var ssitDTO = ssitBL.Single(id_solicitud);
            var lstencDTO = encBL.GetByFKIdSolicitud(id_solicitud);
            CargarProTeatro(ssitDTO, lstencDTO);
        }
        public void CargarCentroCultural(int id_solicitud)
        {
            SSITSolicitudesBL ssitBL = new SSITSolicitudesBL();
            EncomiendaBL encBL = new EncomiendaBL();
            var ssitDTO = ssitBL.Single(id_solicitud);
            var lstencDTO = encBL.GetByFKIdSolicitud(id_solicitud);
            CargarCentroCultural(ssitDTO, lstencDTO);
        }
        public void CargarProTeatro(SSITSolicitudesDTO ssitDTO, IEnumerable<EncomiendaDTO> lstEncDTO)
        {
            int id_solicitud = ssitDTO.IdSolicitud;
            
            var encDTO = lstEncDTO
                .Where(x => x.IdEstado == (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo)
                .OrderByDescending(o => o.IdEncomienda)
                .FirstOrDefault();

            bool TieneRubroProTeatro = false;

            if (encDTO != null)
            {
                RubrosBL rubBL = new RubrosBL();
                RubrosCNBL rubCNBL = new RubrosCNBL();
                TieneRubroProTeatro = rubBL.GetByListCodigo(encDTO.EncomiendaRubrosDTO.Select(s => s.CodigoRubro).ToList()).Where(x => x.EsProTeatro).Any() ||
                                      rubCNBL.GetByListCodigo(encDTO.EncomiendaRubrosCNDTO.Select(s => s.CodigoRubro).ToList()).Where(x => x.Codigo == Constantes.RubrosCN.Teatro_Independiente).Any(); ;
            }

            if (TieneRubroProTeatro)
            {
                pnlDocumentosProTeatro.Style["display"] = "block";
            }
            else
            {
                SSITDocumentosAdjuntosBL ssitDocBL = new SSITDocumentosAdjuntosBL();
                var listSsitDocDTO = ssitDocBL.GetByFKIdSolicitudTipoDocReq(id_solicitud, (int)Constantes.TipoDocumentoRequerido.CertificadoProTeatro);
                foreach (var docDTO in listSsitDocDTO)
                    ssitDocBL.Delete(docDTO);

                pnlDocumentosProTeatro.Style["display"] = "none";
                return;
            }
            
            SSITDocumentosAdjuntosEntityBL blDoc = new SSITDocumentosAdjuntosEntityBL();
            var docProTeatro = blDoc.GetByFKIdSolicitudGeneradosIdDocReq(id_solicitud, (int)Constantes.TipoDocumentoRequerido.ConstanciaInicioTramiteIGJoINAES);
            foreach (var doc in docProTeatro)
                doc.url = string.Format("~/" + RouteConfig.DESCARGA_FILE + "{0}", Functions.ConvertToBase64String(doc.id_file));

            if (docProTeatro.Count() > 0)
                btnMostrarAgregadoDocumentosPT.Style["display"] = "none";
            else
                btnMostrarAgregadoDocumentosPT.Style["display"] = "block";

            if (ssitDTO.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.DATOSCONF && ssitDTO.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.OBSERVADO)
                pnlDocumentosProTeatro.Enabled = false;
            

            if (this.EventRecargarPagos != null) // if there is no event handler then it will be null and invoking it will throw an error.
            {
                ucRecargarPagosEventsArgs ev = new ucRecargarPagosEventsArgs();
                ev.id_solicitud = id_solicitud;
                ev.tipo_tramite = Constantes.PagosTipoTramite.HAB;
                this.EventRecargarPagos(this, ev);
            }
        }

        public void CargarCentroCultural(SSITSolicitudesDTO ssitDTO, IEnumerable<EncomiendaDTO> lstEncDTO)
        {
            int id_solicitud = ssitDTO.IdSolicitud;

            var encDTO = lstEncDTO
                .Where(x => x.IdEstado == (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo)
                .OrderByDescending(o => o.IdEncomienda)
                .FirstOrDefault();

            bool TieneRubroCentroCultural = false;

            if (encDTO != null)
            {
                RubrosBL rubBL = new RubrosBL();
                TieneRubroCentroCultural = rubBL.GetByListCodigo(encDTO.EncomiendaRubrosDTO.Select(s => s.CodigoRubro).ToList()).Where(x => x.EsCentroCultural).Any();
            }

            if (TieneRubroCentroCultural)
            {
                pnlDocumentosCCultural.Style["display"] = "block";
            }
            else
            {
                //SSITDocumentosAdjuntosBL ssitDocBL = new SSITDocumentosAdjuntosBL();
                //var listSsitDocDTO = ssitDocBL.GetByFKIdSolicitudTipoDocReq(id_solicitud, (int)Constantes.TipoDocumentoRequerido.CertificadoProTeatro);
                //foreach (var docDTO in listSsitDocDTO)
                //    ssitDocBL.Delete(docDTO);

                pnlDocumentosCCultural.Style["display"] = "none";
                return;
            }

            SSITDocumentosAdjuntosEntityBL blDoc = new SSITDocumentosAdjuntosEntityBL();
            var docCentroCultural = blDoc.GetByFKIdSolicitudGeneradosIdDocReq(id_solicitud, (int)Constantes.TipoDocumentoRequerido.CertificadoProTeatro);
            foreach (var doc in docCentroCultural)
                doc.url = string.Format("~/" + RouteConfig.DESCARGA_FILE + "{0}", Functions.ConvertToBase64String(doc.id_file));

            if (docCentroCultural.Count() > 0)
                btnMostrarAgregadoDocumentosCC.Style["display"] = "none";
            else
                btnMostrarAgregadoDocumentosCC.Style["display"] = "block";

            if (ssitDTO.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.DATOSCONF && ssitDTO.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.OBSERVADO)
                pnlDocumentosCCultural.Enabled = false;


            if (this.EventRecargarPagos != null) // if there is no event handler then it will be null and invoking it will throw an error.
            {
                ucRecargarPagosEventsArgs ev = new ucRecargarPagosEventsArgs();
                ev.id_solicitud = id_solicitud;
                ev.tipo_tramite = Constantes.PagosTipoTramite.HAB;
                this.EventRecargarPagos(this, ev);
            }
        }

        public string tieneOblea(int id_solicitud)
        {
            SSITDocumentosAdjuntosBL blDoc = new SSITDocumentosAdjuntosBL();
            var ingresados = blDoc.GetByFKIdSolicitud(id_solicitud);
            foreach (var doc in ingresados)
            {
                if (doc.id_tipodocsis == (int)StaticClass.Constantes.TiposDeDocumentosSistema.OBLEA_SOLICITUD)
                {
                    string url = string.Format("~/" + RouteConfig.DESCARGA_FILE + "{0}", Functions.ConvertToBase64String(doc.id_file));
                    return url;
                }
            }
            return "";
        }

        public void CargarDatos(int id_solicitud)
        {
            this.id_solicitud = id_solicitud;
            SSITSolicitudesBL ssitBL = new SSITSolicitudesBL();
            EncomiendaBL encBL = new EncomiendaBL();
            var ssitDTO = ssitBL.Single(id_solicitud);
            var lstencDTO = encBL.GetByFKIdSolicitud(id_solicitud);
            CargarDatos(ssitDTO, lstencDTO);
        }

        public void CargarDatos(SSITSolicitudesDTO ssitDTO, IEnumerable<EncomiendaDTO> lstEncDTO)
        {
            this.id_solicitud = ssitDTO.IdSolicitud;
            if (ssitDTO.LastUpdateDate != null)
            {
                hid_FechaDeCambioDeEstado.Value = ssitDTO.LastUpdateDate.Value.Date.ToString();
            }
            else
            {
                hid_FechaDeCambioDeEstado.Value = ssitDTO.CreateDate.Date.ToString();
            }
            if (id_solicitud <= Constantes.SOLICITUDES_NUEVAS_MAYORES_A)
            {
                if (!string.IsNullOrEmpty(ssitDTO.NroExpediente))
                {
                    btnMostrarAgregadoDocumentos.Visible = false;
                }
            }

            SSITDocumentosAdjuntosEntityBL blDoc = new SSITDocumentosAdjuntosEntityBL();
            var elements = blDoc.GetByFKListIdEncomienda(lstEncDTO.Select(x => x.IdEncomienda).ToList());

            foreach (var doc in elements)
                doc.url = string.Format("~/" + RouteConfig.DESCARGA_FILE + "{0}", Functions.ConvertToBase64String(doc.id_file));

            gridRelacionados_db.DataSource = elements.ToList();
            gridRelacionados_db.DataBind();

            SSITDocumentosAdjuntosBL ssitDocAdjuntosBL = new SSITDocumentosAdjuntosBL();
            var ingresados = ssitDocAdjuntosBL.GetByFKIdSolicitudGenerados(id_solicitud);
            foreach (var doc in ingresados)
                doc.url = string.Format("~/" + RouteConfig.DESCARGA_FILE + "{0}", Functions.ConvertToBase64String(doc.id_file));

            if (!ingresados.Where(x => x.id_tipodocsis == (int)Constantes.TiposDeDocumentosSistema.DISPOSICION_HABILITACION).Any())
                ingresados = ingresados.Where(x => x.id_tipodocsis != (int)Constantes.TiposDeDocumentosSistema.PLANCHETA_HABILITACION);

            gridIngresados_db.DataSource = ingresados.ToList();
            gridIngresados_db.DataBind();

            var agregados = blDoc.GetByFKIdSolicitud(id_solicitud);

            foreach (var doc in agregados)
                doc.url = string.Format("~/" + RouteConfig.DESCARGA_FILE + "{0}", Functions.ConvertToBase64String(doc.id_file));
            //List<int> lstid_tdocreqSeparados = new List<int>()
            //                                        {
            //                                            (int)Constantes.TipoDocumentoRequerido.CertificadoProTeatro
            //                                            ,(int)Constantes.TipoDocumentoRequerido.CertificadoProTeatro
            //                                        };

            gridAgregados_db.DataSource = agregados;// agregados.Where(x => !lstid_tdocreqSeparados.Contains(x.id_tdocreq)).ToList(); 
            gridAgregados_db.DataBind();

            List<int> lstid_tdocreqProTeatro = new List<int>()
            {
                (int)Constantes.TipoDocumentoRequerido.CertificadoProTeatro
                ,(int)Constantes.TipoDocumentoRequerido.ConstanciaInicioTramiteIGJoINAES
            };
            gridProTeatro_db.DataSource = agregados.Where(x => lstid_tdocreqProTeatro.Contains(x.id_tdocreq)).ToList();
            gridProTeatro_db.DataBind();

            gridCCultural_db.DataSource = agregados.Where(x => x.id_tdocreq == (int)Constantes.TipoDocumentoRequerido.ConstanciaInicioTramiteIGJoINAES).ToList();
            gridCCultural_db.DataBind();

            SGITareaCalificarObsDocsBL sgiTareaCalObsDocBL = new SGITareaCalificarObsDocsBL();

            var agregadosObs = sgiTareaCalObsDocBL.GetByFKIdSolicitud(id_solicitud).Where(x => x.id_file > 0);

            foreach (var doc in agregadosObs)
                doc.Url = string.Format("~/" + RouteConfig.DESCARGA_FILE + "{0}", Functions.ConvertToBase64String(doc.id_file));

            grdRelacionadosObservaciones.DataSource = agregadosObs;
            grdRelacionadosObservaciones.DataBind();

            // Lista de estados permitidos para adjuntar documentos
            List<int> lstEstadosPermitidos = new List<int>();
            lstEstadosPermitidos.Add((int)Constantes.TipoEstadoSolicitudEnum.DATOSCONF);
            lstEstadosPermitidos.Add((int)Constantes.TipoEstadoSolicitudEnum.OBSERVADO);

            if (ssitDTO != null)
            {
                if (!lstEstadosPermitidos.Contains(ssitDTO.IdEstado))
                {
                    pnlAgregarDocumentos.Style["display"] = "none";
                    btnMostrarAgregadoDocumentos.Style["display"] = "none";
                    btnMostrarAgregadoDocumentosPT.Style["display"] = "none";
                }
                else
                {
                    btnMostrarAgregadoDocumentos.Style["display"] = "block;";
                }
            }
            CargarCombos();

            if ((gridAgregados_db.Rows.Count) == 0 && (gridIngresados_db.Rows.Count == 0) && (gridRelacionados_db.Rows.Count) == 0 && (grdRelacionadosObservaciones.Rows.Count == 0))
            {
                textDocProf.Style["display"] = "none";
                textDocAgencia.Style["display"] = "none";
                textDocUd.Style["display"] = "none";
                textDocObs.Style["display"] = "none";
                upPnlDocumentos.Visible = false;
                UpdSinRegistros.Visible = true;
            }
            else
                {
                    upPnlDocumentos.Visible = true;
                    UpdSinRegistros.Visible = false;
            }
            if ((gridAgregados_db.Rows.Count) == 0)
                textDocUd.Style["display"] = "none";
            else
                textDocUd.Style["display"] = "inline-block";
            if ((gridIngresados_db.Rows.Count) == 0)
                textDocAgencia.Style["display"] = "none";
            else
                textDocAgencia.Style["display"] = "inline-block";
            if ((gridRelacionados_db.Rows.Count) == 0)
                textDocProf.Style["display"] = "none";
            else
                textDocProf.Style["display"] = "inline-block";
            if ((grdRelacionadosObservaciones.Rows.Count) == 0)
                textDocObs.Style["display"] = "none";
            else
                textDocObs.Style["display"] = "inline-block";

            UpdSinRegistros.Update();
            upPnlDocumentos.Update();

            CargarProTeatro(ssitDTO, lstEncDTO);
            CargarCentroCultural(ssitDTO, lstEncDTO);

            updAgregarDocumentos.Update();
            updAgregarDocumentosPT.Update();
            updAgregarDocumentosCC.Update();
        }
        #region carga documentos
        private void CargarCombos()
        {
            TiposDeDocumentosRequeridosBL blTipoDoc = new TiposDeDocumentosRequeridosBL();
            var lstTiposDocumentos = blTipoDoc.GetVisibleSSIT();
            foreach (var item in lstTiposDocumentos)
                item.nombre_tdocreq = item.nombre_tdocreq + " (" + item.formato_archivo + ")";

            var lstDocsProTeatro = blTipoDoc.GetByListIdTdoReq(new List<int> {(int)Constantes.TipoDocumentoRequerido.CertificadoProTeatro,
                (int)Constantes.TipoDocumentoRequerido.ConstanciaInicioTramiteIGJoINAES});
            foreach (var item in lstDocsProTeatro)
                item.nombre_tdocreq = item.nombre_tdocreq + " (" + item.formato_archivo + ")";

            var lstDocsCentroCultural = blTipoDoc.GetByListIdTdoReq(new List<int> { (int)Constantes.TipoDocumentoRequerido.ConstanciaInicioTramiteIGJoINAES });
            foreach (var item in lstDocsCentroCultural)
                item.nombre_tdocreq = item.nombre_tdocreq + " (" + item.formato_archivo + ")";

            CargaDocumentos.CargarCombo(lstTiposDocumentos.ToList());
            CargaDocumentosPT.CargarCombo(lstDocsProTeatro.ToList());
            CargaDocumentosCC.CargarCombo(lstDocsCentroCultural.ToList());

            updpnlAgregarDocumentos.Update();
            updpnlAgregarDocumentosPT.Update();
            updpnlAgregarDocumentosCC.Update();
        }


        protected void lnkEliminar_Command(object sender, CommandEventArgs e)
        {
            try
            {
                LinkButton lnkEliminar = (LinkButton)sender;
                int id_docadjunto = Convert.ToInt32(lnkEliminar.CommandArgument);
                SSITDocumentosAdjuntosBL blDoc = new SSITDocumentosAdjuntosBL();
                var doc = blDoc.Single(id_docadjunto);
                blDoc.Delete(doc);
                CargarDatos(id_solicitud);

            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                ScriptManager.RegisterClientScriptBlock(pnlDatosDocumento, pnlDatosDocumento.GetType(), "mostrarError", "showfrmErrorDocumentos(); ", true);
            }
        }
        #endregion

        protected void btnEliminarDocumento_Click(object sender, EventArgs e)
        {
            int id_docadj= 0;
            int.TryParse(hid_id_docadjuntoEliminar.Value, out id_docadj);

            SSITDocumentosAdjuntosBL ssitDocBL = new SSITDocumentosAdjuntosBL();
            var ssitDocDTO = ssitDocBL.Single(id_docadj);
            ssitDocBL.Delete(ssitDocDTO);
            
            CargarDatos(id_solicitud);
            ScriptManager.RegisterStartupScript(upPnlDocumentos, upPnlDocumentos.GetType(), "ScriptOcultarAnularencomienda", "hidefrmConfirmarEliminarDocumento();", true);
  
        }

        protected void CargaDocumentos_SubirDocumentoClick(object sender, ucCargaDocumentosEventsArgs e)
        {
            SubirDocumento(e.Documento, e.nombre_archivo, e.id_tdocreq, e.detalle_tdocreq);
        }

        private void SubirDocumento(byte[] fileContent, string fileName,int id_tdocreq, string detalle_tdocreq)
        {
            Guid userid = (Guid)Membership.GetUser().ProviderUserKey;
            try
            {
                List<int> lstDocs = new List<int>() { id_tdocreq };
                var tdocreq  = blTdocReq.GetByListIdTdoReq(lstDocs).FirstOrDefault();

                //Grabar el documento en la base
                ExternalServiceFiles service = new ExternalServiceFiles();
                int id_file = service.addFile(fileName, fileContent);

                SSITDocumentosAdjuntosBL blDoc = new SSITDocumentosAdjuntosBL();
                var doc = new SSITDocumentosAdjuntosDTO();
                doc.CreateDate = DateTime.Now;
                doc.CreateUser = userid;
                doc.id_file = id_file;
                doc.generadoxSistema = false;
                doc.id_solicitud = id_solicitud;
                doc.id_tipodocsis = (tdocreq.id_tipdocsis.HasValue ? tdocreq.id_tipdocsis.Value : (int) Constantes.TiposDeDocumentosSistema.DOC_ADJUNTO_SSIT);
                doc.id_tdocreq = id_tdocreq;
                doc.nombre_archivo = fileName;
                doc.tdocreq_detalle = detalle_tdocreq;

                blDoc.Insert(doc, false);
                CargarDatos(id_solicitud);
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                ScriptManager.RegisterClientScriptBlock(pnlDatosDocumento, pnlDatosDocumento.GetType(), "mostrarError", "showfrmErrorDocumentos(); ", true);
            }

        }
    }
}