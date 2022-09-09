using SSIT.App_Components;
using BusinesLayer.Implementation;
using DataTransferObject;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExternalService;

namespace SSIT.Solicitud
{
    public partial class EstadoDeActuacionNotarial : SecurePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            ScriptManager sm = ScriptManager.GetCurrent(this.Page);

            if (sm.IsInAsyncPostBack)
            {
                ScriptManager.RegisterStartupScript(UpdPnlConsultar, UpdPnlConsultar.GetType(), "init_Js_UpdPnlConsultar", "init_Js_UpdPnlConsultar();", true);

            }
            if (!IsPostBack)
            {

            }
        }

        protected void gridObservaciones_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                LinkButton lnkModal = (LinkButton)e.Row.FindControl("lnkModal");
                Panel pnlObservacionModal = (Panel)e.Row.FindControl("pnlObservacionModal");
                lnkModal.Attributes["data-target"] = "#" + pnlObservacionModal.ClientID;

            }
        }
        protected void gridObservacionesNew_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                LinkButton lnkModal = (LinkButton)e.Row.FindControl("lnkModal");
                Panel pnlObservacionModal = (Panel)e.Row.FindControl("pnlObservacionModal");
                lnkModal.Attributes["data-target"] = "#" + pnlObservacionModal.ClientID;

            }
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            int nro_encomienda = 0;
            int.TryParse(txtNumDeEncomienda.Text.Trim(), out nro_encomienda);

            int nro_acta = 0;
            int.TryParse(txtCodConfirmacionEscribanos.Text.Trim(), out nro_acta);

            try
            {

                SSITSolicitudesObservacionesBL businessOBSOLD = new SSITSolicitudesObservacionesBL();
                SGITareaCalificarObsDocsBL businessOBSNEW = new SGITareaCalificarObsDocsBL();

                EncomiendaBL businessENC = new EncomiendaBL();

                var Encomienda = businessENC.Single(nro_encomienda);

                List<SSITSolicitudesObservacionesDTO> obsold = new List<SSITSolicitudesObservacionesDTO>();
                List<SGITareaCalificarObsDocsGrillaDTO> obsnew = new List<SGITareaCalificarObsDocsGrillaDTO>();

                if (Encomienda != null)
                {
                    //Metodo viejo
                    if (Encomienda.IdSolicitud <= Constantes.SOLICITUDES_NUEVAS_MAYORES_A) //EncomiendaSSITSolicitudesDTO.Select(x => x.id_solicitud).FirstOrDefault()
                    {
                        obsold = businessOBSOLD.GetByFKIdSolicitud(Encomienda.IdSolicitud).ToList(); //EncomiendaSSITSolicitudesDTO.Select(x => x.id_solicitud).FirstOrDefault()

                        gridObservaciones.DataSource = obsold;
                        gridObservaciones.DataBind();
                    }
                    //Metodo nuevo
                    if (Encomienda.IdSolicitud > Constantes.SOLICITUDES_NUEVAS_MAYORES_A) //EncomiendaSSITSolicitudesDTO.Select(x => x.id_solicitud).FirstOrDefault()
                    {
                        obsnew = businessOBSNEW.GetByFKIdSolicitud(Encomienda.IdSolicitud).ToList(); //EncomiendaSSITSolicitudesDTO.Select(x => x.id_solicitud).FirstOrDefault()

                        gridObservacionesNew.DataSource = obsnew;
                        gridObservacionesNew.DataBind();

                    }
                }
                else
                {
                    lblError.Text = "No existe el número de encomienda ingresado.";
                    this.EjecutarScript(UpdPnlConsultar, "showfrmError();");
                    return;
                }

                CargarCertificados(nro_encomienda,nro_acta);
                //if (repeater_certificados.Items.Count == 0)
                //    pnlNoRecordsCertificados.Style["display"] = "block";

                UpdPnlConsultar.Update();
                updPnlListaDocumentos.Update();
                updPnlObservaciones.Update();
                this.EjecutarScript(UpdPnlConsultar, "showResults();");

            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = Funciones.GetErrorMessage(ex);
                this.EjecutarScript(UpdPnlConsultar, "showfrmError();");
                return;
            }
        }

        private void CargarCertificados(int nro_encomienda, int nro_acta)
        {

            WsEscribanosActaNotarialBL businessACTA = new WsEscribanosActaNotarialBL();
            CertificadosBL businessCTF = new CertificadosBL();

            List<CertificadosDTO> Certificado = new List<CertificadosDTO>();
            var acta = businessACTA.GetByFKIdEncomienda(nro_encomienda);

            if (acta != null)
            {
                if (acta.id_actanotarial != nro_acta)
                {
                    lblError.Text = "No coincide la encomienda con el acta notarial ingresada.";
                    this.EjecutarScript(UpdPnlConsultar, "showfrmError();");
                    return;
                }
                else
                {
                    var cer = businessCTF.GetByFKNroTipo(nro_acta, (int)Constantes.TipoTramiteCertificados.Certificado_Acta_Notarial_Encomienda);

                    GrdvCertificados.DataSource = cer;
                    GrdvCertificados.DataBind();
                }

            }
        }

        protected void lnkCertificado_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkCertificado = (LinkButton)sender;
                int id_certificado = 0;
                int.TryParse(lnkCertificado.CommandArgument, out id_certificado);

                Server.Transfer(string.Format("~/Reportes/GetActa.aspx?id_certificado={0}", id_certificado));
                //ExternalServiceFiles ser = new ExternalServiceFiles();
                //byte[] documento = ser.downloadFileByidCertificado(id_certificado);
                //if (documento == null)
                //    throw new Exception("No se encontro el Certificado en la base de archivos.");

                //System.IO.MemoryStream msPdf = new System.IO.MemoryStream();
                //msPdf = new System.IO.MemoryStream(documento);

                ////mostrar archivo
                //HttpContext.Current.Response.Clear();
                //HttpContext.Current.Response.Buffer = true;//false;
                //HttpContext.Current.Response.ContentType = "application/pdf";
                //HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + "CertificadoActaNotarial_" + id_certificado);
                //HttpContext.Current.Response.AddHeader("Content-Length", msPdf.Length.ToString());
                //HttpContext.Current.Response.BinaryWrite(msPdf.ToArray());
                //HttpContext.Current.Response.Flush();
                //HttpContext.Current.Response.SuppressContent = true;

                //HttpContext.Current.ApplicationInstance.CompleteRequest();; 

            }
            catch (Exception ex)
            {
                throw new Exception("Se produjo un error al recuperar el pdf: " + Environment.NewLine + ex.Message);
            }
        }
    }
}