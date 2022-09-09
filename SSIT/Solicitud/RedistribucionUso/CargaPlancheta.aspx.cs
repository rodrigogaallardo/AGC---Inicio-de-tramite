using BusinesLayer.Implementation;
using DataTransferObject;
using ExternalService;
using SSIT.App_Components;
using SSIT.Solicitud.Controls;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSIT.Solicitud.RedistribucionUso
{
    public partial class CargaPlancheta : SecurePage
    {

        TiposDeDocumentosRequeridosBL blTipoDoc = new TiposDeDocumentosRequeridosBL();
        SSITDocumentosAdjuntosBL blDoc = new SSITDocumentosAdjuntosBL();

        private int id_solicitud
        {
            get
            {
                int ret = 0;
                int.TryParse(hid_id_solicitud.Value, out ret);
                return ret;
            }
            set
            {
                hid_id_solicitud.Value = value.ToString();
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager sm = ScriptManager.GetCurrent(this);

            if (sm.IsInAsyncPostBack)
            {
                ScriptManager.RegisterStartupScript(upPnlDocumentos, upPnlDocumentos.GetType(), "init_Js_upPnlDocumentos", "init_Js_upPnlDocumentos();", true);
            }


            if (!IsPostBack)
            {
                hid_return_url.Value = Request.Url.AbsoluteUri;
                ComprobarSolicitud();
                Titulo.CargarDatos(id_solicitud, "Constancia de Habilitación");
            }

        }
        private void ComprobarSolicitud()
        {
            if (Page.RouteData.Values["id_solicitud"] != null)
            {
                this.id_solicitud = Convert.ToInt32(Page.RouteData.Values["id_solicitud"].ToString());
                SSITSolicitudesBL solBL = new SSITSolicitudesBL();
                var sol = solBL.Single(id_solicitud);
                if (sol != null)
                {
                    Guid userid_solicitud = (Guid)Membership.GetUser().ProviderUserKey;

                    if (userid_solicitud != sol.CreateUser)
                        Server.Transfer("~/Errores/Error3002.aspx");
                    else
                    {
                        if (!(sol.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.INCOM ||
                                sol.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.COMP ||
                                sol.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.DATOSCONF ||
                                sol.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.OBSERVADO))
                        {
                            Server.Transfer("~/Errores/Error3003.aspx");
                        }
                    }
                }
                else
                    Server.Transfer("~/Errores/Error3004.aspx");

            }
            else
                Server.Transfer("~/Errores/Error3001.aspx");
        }

        protected void btnCargarDatos_Click(object sender, EventArgs e)
        {
            try
            {
                CargarComboDocumentos();
                CargarDatos(this.id_solicitud);
                this.EjecutarScript(updCargarDatos, "finalizarCarga();");
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = Funciones.GetErrorMessage(ex);
                this.EjecutarScript(updCargarDatos, "finalizarCarga();showfrmError();");
            }

        }

        private void CargarComboDocumentos()
        {

            var lstTiposDocumentos = blTipoDoc.GetVisibleSSIT().Where(x => x.id_tipdocsis == (int)Constantes.TiposDeDocumentosSistema.CERTIFICADO_HABILITACION).ToList();
            ucCargaDocumentos.CargarCombo(lstTiposDocumentos);
        }

        private void CargarDatos(int id_solicitud)
        {
            var lstDocumentos = blDoc.GetByFKIdSolicitudTipoDocSis(id_solicitud, (int)Constantes.TiposDeDocumentosSistema.CERTIFICADO_HABILITACION).ToList();

            foreach (var doc in lstDocumentos)
                doc.url = string.Format("~/" + RouteConfig.DESCARGA_FILE + "{0}", Funciones.ConvertToBase64String(doc.id_file));


            gridAgregados_db.DataSource = lstDocumentos;
            gridAgregados_db.DataBind();

            pnlTituloGrilla.Visible = (gridAgregados_db.Rows.Count > 0);


        }

        protected void OnErrorCargaDocumentoClick(object sender, CargaDocumentoErrorEventArgs e)
        {
            lblError.Text = e.Description;
            this.EjecutarScript(updCargaDocumentos, "showfrmError();");
        }

        protected void CargaDocumentos_SubirDocumentoClick(object sender, ucCargaDocumentosEventsArgs e)
        {
            SubirDocumento(e.Documento, e.nombre_archivo, e.id_tdocreq, e.detalle_tdocreq);
        }

        private void SubirDocumento(byte[] fileContent, string fileName, int id_tdocreq, string detalle_tdocreq)
        {
            Guid userid = (Guid)Membership.GetUser().ProviderUserKey;
            try
            {
                //Grabar el documento en la base
                ExternalServiceFiles service = new ExternalServiceFiles();
                int id_file = service.addFile(fileName, fileContent);


                var doc = new SSITDocumentosAdjuntosDTO();
                doc.CreateDate = DateTime.Now;
                doc.CreateUser = userid;
                doc.id_file = id_file;
                doc.generadoxSistema = false;
                doc.id_solicitud = id_solicitud;
                doc.id_tipodocsis = (int)Constantes.TiposDeDocumentosSistema.CERTIFICADO_HABILITACION;
                doc.id_tdocreq = id_tdocreq;
                doc.nombre_archivo = fileName;
                doc.tdocreq_detalle = detalle_tdocreq;

                blDoc.Insert(doc, false);
                CargarDatos(this.id_solicitud);
            }
            catch (Exception ex)
            {

                lblError.Text = ex.Message;
                lblError.Text = Funciones.GetErrorMessage(ex);
                this.EjecutarScript(updCargaDocumentos, "showfrmError();");
            }

        }

        protected void btnEliminarDocumento_Click(object sender, EventArgs e)
        {
            int id_docadj = 0;
            int.TryParse(hid_id_docadjuntoEliminar.Value, out id_docadj);

            var ssitDocDTO = blDoc.Single(id_docadj);
            blDoc.Delete(ssitDocDTO);

            CargarDatos(id_solicitud);
            this.EjecutarScript(updConfirmarEliminarDocumento, "hidefrmConfirmarEliminarDocumento();");


        }

        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            try
            {

                if (gridAgregados_db.Rows.Count == 0)
                    throw new Exception("Debe ingresar al menos un archivo para poder continuar.");

                Response.Redirect(string.Format("~/{0}{1}", RouteConfig.AGREGAR_TITULAR_SOLICITUD_REDISTRIBUCION_USO, this.id_solicitud));

            }
            catch (Exception ex)
            {
                lblError.Text = Funciones.GetErrorMessage(ex);
                this.EjecutarScript(updContinuar, "showfrmError();");
            }
        }
    }
}