using BusinesLayer.Implementation;
using DataTransferObject;
using ExternalService;
using ExternalService.Class.Express;
using ExternalService.ws_interface_AGC;
using iTextSharp.text.pdf;
using SSIT.App_Components;
using SSIT.Common;
using StaticClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SSIT.Solicitud.Habilitacion.Controls
{
    public partial class Presentacion : System.Web.UI.UserControl
    {
        private int id_solicitud { get; set; }
        IEnumerable<EncomiendaDTO> _encomiendas = null; 

        public IEnumerable<EncomiendaDTO> Encomiendas { get {

            if (_encomiendas == null)
            { 
                EncomiendaBL encomiendaBL =  new EncomiendaBL();
                _encomiendas = encomiendaBL.GetByFKIdSolicitud(id_solicitud); 
            }

            return _encomiendas;
        } } 

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(updPnlObservaciones, updPnlObservaciones.GetType(), "init_Js_updFilesObservaciones", "init_Js_updFilesObservaciones();", true);
        }
        private List<ListItem> _lstItems = null;
  
        private List<ListItem> Items
        {
            get {
                if (_lstItems == null)
                {
                    var lst = this.Encomiendas.SelectMany(p => p.EncomiendaDocumentosAdjuntosDTO.Where(a => a.id_tipodocsis == (int)Constantes.TiposDeDocumentosSistema.ENCOMIENDA_DIGITAL));

                    List<ListItem> lstItems = new List<ListItem>();

                    try
                    {

                        List<GetCAAsByEncomiendasResponse> l = null;
                        Task.Run(async () =>
                        {
                            var listCaaW = await GetCAAsByEncomiendas(this.Encomiendas.Select(p => p.IdEncomienda).ToList());
                            l = listCaaW.ListCaa;
                        }).Wait();
                        lstItems.Add(new ListItem());
                    foreach (var item in l)
                    {
                        string doc = "";
                        if (item.certificado != null)
                            doc = item.certificado.idFile.ToString();

                        lstItems.Add(new ListItem(item.nombre_tipocertificado + item.formulario.id_caa.ToString(), doc));
                    }

                    lstItems.Add(new ListItem());
                    foreach (var item in lst)
                    {
                        lstItems.Add(new ListItem("Encomienda Nº " + item.id_encomienda.ToString(), item.id_file.ToString()));
                    }
                    }
                    catch (Exception exc){
                        LogError.Write(exc); 
                    }

                    _lstItems = lstItems;
                }

                return _lstItems;

            }
        }
        private List<SGITareaCalificarObsDocsGrillaDTO> _tareasObservaciones = null;
        
        /// <summary>
        /// /
        /// </summary>
        /// <param name="ssitDTO"></param>
        public void CargarDatos(SSITSolicitudesDTO ssitDTO, IEnumerable<EncomiendaDTO> lstEncomiendas )
        {
            _encomiendas = lstEncomiendas;

            this.id_solicitud = ssitDTO.IdSolicitud;
            SSITSolicitudesHistorialEstadosBL blHistls = new SSITSolicitudesHistorialEstadosBL();
            var elements = blHistls.GetByFKIdSolicitudGrilla(ssitDTO);

            gridHistorial_db.DataSource = elements.OrderBy(p => p.id_solhistest);
            gridHistorial_db.DataBind();
            CargarDatosObservaciones(ssitDTO);
            updHistorial.Update();
        }

        public void CargarDatos(SSITSolicitudesDTO ssitDTO)
        {
            
            this.id_solicitud = ssitDTO.IdSolicitud;
            SSITSolicitudesHistorialEstadosBL blHistls = new SSITSolicitudesHistorialEstadosBL();
            var elements = blHistls.GetByFKIdSolicitudGrilla(ssitDTO);

            gridHistorial_db.DataSource = elements.OrderBy(p => p.id_solhistest);
            gridHistorial_db.DataBind();
            CargarDatosObservaciones(ssitDTO);
            updHistorial.Update();
        }

        #region observaciones
        private class Observacion : IEqualityComparer<Observacion>
        {
            public DateTime CreateDate { get; set; }
            public string userApeNom { get; set; }
            public int id_ObsGrupo { get; set; }
            public DateTime DateObs { get; set; }
            public bool Equals(Observacion source, Observacion dest)
            { 
                return (source.id_ObsGrupo == dest.id_ObsGrupo);
            }
            public int GetHashCode(Observacion obj)
            { 
                return obj.id_ObsGrupo.GetHashCode(); 
            }       
        }
        private void CargarDatosObservaciones(SSITSolicitudesDTO ssitDTO)
        {
            //Observaciones nuevas
            SGITareaCalificarObsGrupoBL blGrupos = new SGITareaCalificarObsGrupoBL();
            var tareas = blGrupos.GetByFKIdSolicitud(id_solicitud).ToList();
            _tareasObservaciones = tareas.Select(p => p.SGITareaCalificarObsGrupo).ToList();
            var listDistinct = (from lst in tareas
                                select new Observacion
                                {
                                    CreateDate = lst.CreateDate,
                                    DateObs = lst.SGITareaCalificarObsGrupo.ObsDate,  //tiene que agarrar la fecha de la tarea correccion de la solicitud correspondiente
                                    id_ObsGrupo = lst.id_ObsGrupo,
                                    userApeNom = lst.userApeNom
                                }).Distinct(new Observacion());



            datlstObservaciones.DataSource = listDistinct;
            datlstObservaciones.DataBind();
            if(listDistinct.Count() == 0)
                datlstObservaciones.Visible = false;

            //Observaciones viejas
            gridObservaciones.Visible = false;
            string nroExpediente = ssitDTO.NroExpediente;
            SSITSolicitudesObservacionesBL blObs = new SSITSolicitudesObservacionesBL();
            var list = blObs.GetByFKIdSolicitud(id_solicitud);
            if (list.Count() != 0)
            {
                gridObservaciones.Visible = true;
                gridObservaciones.DataSource = list;
                gridObservaciones.DataBind();
            }
            else if (!string.IsNullOrWhiteSpace(nroExpediente) && id_solicitud <= Constantes.SOLICITUDES_NUEVAS_MAYORES_A)
            {
                lblSolictarTurno.Visible = true;
                btnSolicitarTurno.Visible = true;
            }
            else
            {
                if (list.Count() == 0 && listDistinct.Count() == 0)
                {
                    box_observacion.Style["display"] = "none";
                }
            }
        }
        #region observaciones nuevas
        protected void datlstObservaciones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView grdDetalleObs = (GridView)e.Row.FindControl("grdDetalleObs");

                int id_ObsGrupo = Convert.ToInt32(datlstObservaciones.DataKeys[e.Row.RowIndex].Values["id_ObsGrupo"].ToString());
                var dt = _tareasObservaciones.Where(p => p.id_ObsGrupo == id_ObsGrupo).ToList(); 

                grdDetalleObs.DataSource = dt;
                grdDetalleObs.DataBind();

            }
        }

        protected void btn_observacion_btnUpDown_Click(object sender, EventArgs e)
        {
            LinkButton btn_observacion_btnUpDow = (LinkButton)sender;
            DataControlFieldCell row = (DataControlFieldCell)btn_observacion_btnUpDow.Parent;
            Panel pnl_datlstObservaciones_body = (Panel)row.FindControl("pnl_datlstObservaciones_body");
            Panel pnl_icono_up_down = (Panel)btn_observacion_btnUpDow.FindControl("pnl_icono_up_down");
            pnl_datlstObservaciones_body.Visible = !pnl_datlstObservaciones_body.Visible;

            if (pnl_datlstObservaciones_body.Visible)
            {
                btn_observacion_btnUpDow.Attributes["data-toggle"] = "open";
                pnl_icono_up_down.CssClass = "imoon imoon-chevron-up";
            }
            else
            {
                btn_observacion_btnUpDow.Attributes["data-toggle"] = "close";
                pnl_icono_up_down.CssClass = "imoon imoon-chevron-down";
            }

        }
        protected void chk_Decido_no_subir_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk_Decido_no_subir = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk_Decido_no_subir.Parent.Parent;

            HiddenField hid_id_ObsDocs = (HiddenField)row.FindControl("id_ObsDocs");
            HiddenField hid_id_file = (HiddenField)row.FindControl("hid_id_file");
            HiddenField hid_cod_tipodocsis = (HiddenField)row.FindControl("hid_cod_tipodocsis");
            Panel pnlFilesObservaciones = (Panel)row.FindControl("pnlFilesObservaciones");
            Panel pnlFilesObservaciones_SinArchivo = (Panel)row.FindControl("pnlFilesObservaciones_SinArchivo");
            Panel pnlFilesObservaciones_ConArchivo = (Panel)row.FindControl("pnlFilesObservaciones_ConArchivo");
            HyperLink lnkPDFObservaciones = (HyperLink)pnlFilesObservaciones_ConArchivo.FindControl("lnkPDFObservaciones");
            DropDownList ddlArchivo = (DropDownList)row.FindControl("ddlArchivo");

            int id_obsdocs = Convert.ToInt32(hid_id_ObsDocs.Value);

            SGITareaCalificarObsDocsBL blObs = new SGITareaCalificarObsDocsBL();
            var obs = blObs.Single(id_obsdocs);
            int idGrupoObs = obs.id_ObsGrupo;
            obs.Decido_no_subir = chk_Decido_no_subir.Checked;
            obs.LastUpdateDate = DateTime.Now;
            obs.LastUpdateUser = (Guid)Membership.GetUser().ProviderUserKey;
            if (!obs.Decido_no_subir)
                obs.id_file = null;
            blObs.Update(obs);

            pnlFilesObservaciones_SinArchivo.Visible = false;
            pnlFilesObservaciones_ConArchivo.Visible = false;
            ddlArchivo.Visible = false;
            pnlFilesObservaciones.Visible = false;

            if (chk_Decido_no_subir.Checked)
            {
                pnlFilesObservaciones_SinArchivo.Visible = true;
                hid_id_file.Value = "";
            }
            else
            {
                if (hid_cod_tipodocsis.Value.Length > 0)
                    ddlArchivo.Visible = true;
                else
                    pnlFilesObservaciones.Visible = true;
            }
        
            ScriptManager.RegisterClientScriptBlock(updPnlObservaciones, updPnlObservaciones.GetType(), "abrirAcordionAlDecidir", "abrirAcordionAlDecidir('" + idGrupoObs + "');" , true);
        }

        protected void btnSubirDocumentoObservaciones_Click(object sender, EventArgs e)
        {
            Button btnSubirDocumentoObservaciones = (Button)sender;
            GridViewRow row = (GridViewRow)btnSubirDocumentoObservaciones.Parent.Parent.Parent.Parent.Parent;
            Panel pnlFilesObservaciones = (Panel)btnSubirDocumentoObservaciones.Parent;
            HiddenField hid_filename_observaciones_numerico = (HiddenField)pnlFilesObservaciones.FindControl("hid_filename_observaciones_numerico");
            HiddenField hid_filename_observaciones_original = (HiddenField)pnlFilesObservaciones.FindControl("hid_filename_observaciones_original");
            Panel pnlFilesObservaciones_ConArchivo = (Panel)row.FindControl("pnlFilesObservaciones_ConArchivo");
            HyperLink lnkPDFObservaciones = (HyperLink)pnlFilesObservaciones_ConArchivo.FindControl("lnkPDFObservaciones");
            HiddenField hid_id_ObsDocs = (HiddenField)row.FindControl("id_ObsDocs");
            HiddenField hid_id_file = (HiddenField)row.FindControl("hid_id_file");

            Guid userid = (Guid)Membership.GetUser().ProviderUserKey;
            string filename = hid_filename_observaciones_original.Value;
            int id_ObsDocs = Convert.ToInt32(hid_id_ObsDocs.Value);

            pnlErrorFoto.Style["display"] = "none";
            string savedFileName = Constantes.PathTemporal + hid_filename_observaciones_numerico.Value;
            
            //Elimina las fotos de firmas con mas de 1 día para mantener el directorio limpio.
            string[] lstArchs = Directory.GetFiles(Constantes.PathTemporal);
            foreach (string arch in lstArchs)
            {
                DateTime fechaCreacion = File.GetCreationTime(arch);
                if (fechaCreacion < DateTime.Now.AddDays(-3))
                    File.Delete(arch);
            }

            lblError.Text = "";
            try
            {
                SGITareaCalificarObsDocsBL blObs = new SGITareaCalificarObsDocsBL();
                var obs = blObs.Single(id_ObsDocs);
                TiposDeDocumentosRequeridosBL tdoc = new TiposDeDocumentosRequeridosBL();
                var tdocDTO = tdoc.Single(obs.id_tdocreq);

                int limite = 2097152;

                if (tdocDTO.tamanio_maximo_mb != null)
                    limite = (tdocDTO.tamanio_maximo_mb.Value * 1024) * 1024;

                if (hid_filename_observaciones_numerico.Value.Length > 0)
                {
                    if (Path.GetExtension(savedFileName).ToLower() == ".pdf")
                    {
                        using (var pdf = new PdfReader(savedFileName))
                        {
                            if (!pdf.IsOpenedWithFullPermissions)
                                throw new Exception("El documento que intenta subir tiene un nivel de seguridad no aceptado. Por favor genere un pdf con el nivel de seguridad estandar, sin contraseñas ni permisos especiales.");

                            if (pdf.FileLength > limite)
                                throw new Exception("El tamaño máximo permitido para los documentos es de " + (tdocDTO.tamanio_maximo_mb != null ? tdocDTO.tamanio_maximo_mb.Value.ToString() : "2") + " MB");
                        }
                    }

                   

                    if (tdocDTO.formato_archivo != Path.GetExtension(savedFileName).ToLower().Substring(1))
                    {
                        throw new Exception("Solo se permiten archivos de tipo (*." + tdocDTO.formato_archivo + ")");
                    }

                    byte[] documento = File.ReadAllBytes(savedFileName);

                    //Grabar el documento en la base
                    ExternalServiceFiles service = new ExternalServiceFiles();
                    int id_file = service.addFile(filename, documento);                   

                    obs.LastUpdateDate = DateTime.Now;
                    obs.LastUpdateUser = userid;
                    obs.id_file = id_file;
                    blObs.Update(obs);
                    hid_id_file.Value = id_file.ToString();
                    lnkPDFObservaciones.NavigateUrl = string.Format("~/" + RouteConfig.DESCARGA_FILE + "{0}", Functions.ConvertToBase64String(id_file));
                    lnkPDFObservaciones.Text = filename;
      

                    File.Delete(savedFileName);
                    grdDetalleObs_RowDataBound(row, new GridViewRowEventArgs(row));

                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                ScriptManager.RegisterStartupScript(updPnlObservaciones, updPnlObservaciones.GetType(), "showfrmErrorPresentacion", "showfrmErrorPresentacion();", true);
            }
        }

        protected void btn_eliminar_files_observaciones_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn_eliminar_files_observaciones = (LinkButton)sender;
                GridViewRow row = (GridViewRow)btn_eliminar_files_observaciones.Parent.Parent.Parent.Parent.Parent;
                HiddenField hid_id_obsdocs = (HiddenField)row.FindControl("id_ObsDocs");
                HiddenField hid_id_file = (HiddenField)row.FindControl("hid_id_file");


                int id_obsdocs = Convert.ToInt32(hid_id_obsdocs.Value);
                Panel pnlFilesObservaciones_ConArchivo = (Panel)row.FindControl("pnlFilesObservaciones_ConArchivo");
                HyperLink lnkPDFObservaciones = (HyperLink)pnlFilesObservaciones_ConArchivo.FindControl("lnkPDFObservaciones");
                hid_id_file.Value = "";

                SGITareaCalificarObsDocsBL blObs = new SGITareaCalificarObsDocsBL();
                var obs = blObs.Single(id_obsdocs);
                obs.id_file = null;
                blObs.Update(obs);

                grdDetalleObs_RowDataBound(row, new GridViewRowEventArgs(row));
            }
            catch (Exception ex)
            {
                lblmpeInfo.Text = ex.Message;
            }
        }

        protected void grdDetalleObs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                CheckBox chk_Decido_no_subir = (CheckBox)e.Row.FindControl("chk_Decido_no_subir");
                UpdatePanel updFilesObservaciones = (UpdatePanel)e.Row.FindControl("updFilesObservaciones");
                Panel pnlFilesObservaciones = (Panel)e.Row.FindControl("pnlFilesObservaciones");
                Panel pnlFilesObservaciones_SinArchivo = (Panel)e.Row.FindControl("pnlFilesObservaciones_SinArchivo");
                Panel pnlFilesObservaciones_ConArchivo = (Panel)e.Row.FindControl("pnlFilesObservaciones_ConArchivo");
                HyperLink lnkPDFObservaciones = (HyperLink)pnlFilesObservaciones_ConArchivo.FindControl("lnkPDFObservaciones");
                HiddenField hid_cod_tipodocsis = (HiddenField)e.Row.FindControl("hid_cod_tipodocsis");
                HiddenField hid_id_file = (HiddenField)e.Row.FindControl("hid_id_file");
                HiddenField hid_id_certificado = (HiddenField)e.Row.FindControl("hid_id_certificado");
                HiddenField hid_actual = (HiddenField)e.Row.FindControl("hid_actual");
                DropDownList ddlArchivo = (DropDownList)e.Row.FindControl("ddlArchivo");
                LinkButton btnSeleccionarFilesObservaciones = (LinkButton)e.Row.FindControl("btnSeleccionarFilesObservaciones");
                LinkButton btn_eliminar_files_observaciones = (LinkButton)e.Row.FindControl("btn_eliminar_files_observaciones");


                string cod_tipodocsis = hid_cod_tipodocsis.Value;
                pnlFilesObservaciones.Visible = false;
                pnlFilesObservaciones_SinArchivo.Visible = false;
                pnlFilesObservaciones_ConArchivo.Visible = false;
                ddlArchivo.Visible = false;

                int id_file = 0;
                bool actual = false;
                int id_certificado = 0;
                int.TryParse(hid_id_file.Value, out id_file);
                bool.TryParse(hid_actual.Value, out actual);
                int.TryParse(hid_id_certificado.Value, out id_certificado);

                lnkPDFObservaciones.NavigateUrl = string.Format("~/" + RouteConfig.DESCARGA_FILE + "{0}", Functions.ConvertToBase64String(id_file));


                if (cod_tipodocsis == Constantes.TiposDeDocumentosSistema.ENCOMIENDA_DIGITAL.ToString())
                {
                    
                    var lst = this.Encomiendas.SelectMany(p => p.EncomiendaDocumentosAdjuntosDTO).Where( a => a.id_tipodocsis == (int)Constantes.TiposDeDocumentosSistema.ENCOMIENDA_DIGITAL);
                    List<ListItem> lstItems = new List<ListItem>();

                    lstItems.Add(new ListItem());
                    foreach (var item in lst)
                    {
                        lstItems.Add(new ListItem("Encomienda Nº " + item.id_encomienda.ToString(), item.id_file.ToString()));
                    }

                    ddlArchivo.DataSource = lstItems;
                    ddlArchivo.DataValueField = "Value";
                    ddlArchivo.DataTextField = "Text";
                    ddlArchivo.DataBind();
                    ddlArchivo.Visible = true;

                    if (id_file > 0)
                        ddlArchivo.SelectedValue = id_file.ToString();

                }
                else if (cod_tipodocsis == Constantes.TiposDeDocumentosSistema.CERTIFICADO_CAA.ToString() && this.Encomiendas.Any())
                {
                  
                    ddlArchivo.DataSource = Items;
                    ddlArchivo.DataValueField = "Value";
                    ddlArchivo.DataTextField = "Text";
                    ddlArchivo.DataBind();
                    ddlArchivo.Visible = true;

                    if (id_file > 0)
                        ddlArchivo.SelectedValue = id_file.ToString();

                }

                else if (cod_tipodocsis == Constantes.TiposDeDocumentosSistema.ACTUACION_NOTARIAL.ToString())
                {
                    List<ListItem> lstItems = new List<ListItem>();
                    
                    var lstActas = this.Encomiendas.SelectMany(p => p.ActasNotariales).ToList();

                    lstItems.Add(new ListItem());
                    foreach (var item in lstActas)
                        lstItems.Add(new ListItem("Acta notarial Nº " + item.id_actanotarial.ToString(), item.id_file.ToString()));

                    ddlArchivo.DataSource = lstItems;
                    ddlArchivo.DataValueField = "Value";
                    ddlArchivo.DataTextField = "Text";
                    ddlArchivo.DataBind();
                    ddlArchivo.Visible = true;

                    if (id_file > 0)
                        ddlArchivo.SelectedValue = id_file.ToString();

                }
                else
                {
                    // No es un tipo de documento de sistema, es un archivo a adjuntar

                    if (chk_Decido_no_subir.Checked)
                    {
                        pnlFilesObservaciones_SinArchivo.Visible = true;
                    }
                    else
                    {
                        if (id_file > 0)
                            pnlFilesObservaciones_ConArchivo.Visible = true;
                        else
                            pnlFilesObservaciones.Visible = true;
                    }

                }

                if (!actual)
                {
                    ControlHelper.DisableControls(e.Row);
                    btnSeleccionarFilesObservaciones.Visible = false;
                    btn_eliminar_files_observaciones.Visible = false;
                }

                ScriptManager.RegisterStartupScript(updFilesObservaciones, updFilesObservaciones.GetType(), "init_Js_updFilesObservaciones", "init_Js_updFilesObservaciones();", true);
            }
        }

        protected void ddlArchivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddlArchivo = (DropDownList)sender;
                GridViewRow row = (GridViewRow)ddlArchivo.Parent.Parent.Parent.Parent;
                HiddenField hid_id_ObsDocs = (HiddenField)row.FindControl("id_ObsDocs");
                HiddenField hid_cod_tipodocsis = (HiddenField)row.FindControl("hid_cod_tipodocsis");
                string cod_tipodocsis = hid_cod_tipodocsis.Value;

                int id_obsdocs = Convert.ToInt32(hid_id_ObsDocs.Value);
                int id_file_certificado = 0;

                SGITareaCalificarObsDocsBL blObs = new SGITareaCalificarObsDocsBL();
                var obs = blObs.Single(id_obsdocs);
                obs.LastUpdateUser = (Guid)Membership.GetUser().ProviderUserKey;
                obs.LastUpdateDate = DateTime.Now;
                obs.id_certificado = null;
                if (int.TryParse(ddlArchivo.SelectedValue, out id_file_certificado))
                    obs.id_file = id_file_certificado;
                else
                    obs.id_file = null;
                blObs.Update(obs);

            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region observaciones viejas
        protected void btnConfirmarObservacion_Command(object sender, CommandEventArgs e)
        {
            try
            {

                int id_solobs = Convert.ToInt32(e.CommandArgument);

                SSITSolicitudesObservacionesBL blObs = new SSITSolicitudesObservacionesBL();
                var obs = blObs.Single(id_solobs);
                obs.leido = true;
                blObs.Update(obs);
            }
            catch (Exception ex)
            {
                lblmpeInfo.Text = ex.Message;
                ScriptManager.RegisterClientScriptBlock(updPnlObservaciones, updPnlObservaciones.GetType(), "pnlErrorPresentacion", "mostrarPopup('pnlError'); ", true);
            }

        }
        protected void gridObservaciones_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                SSITSolicitudesObservacionesDTO row = (SSITSolicitudesObservacionesDTO)e.Row.DataItem;

                LinkButton lnkModal = (LinkButton)e.Row.FindControl("lnkModal");
                Panel pnlObservacionModal = (Panel)e.Row.FindControl("pnlObservacionModal");
                lnkModal.Attributes["data-target"] = "#" + pnlObservacionModal.ClientID;

                if (!row.leido)
                {
                    //this.mensajeAlertaObservacion = "Falta leer observaciones";
                }
            }

        }
        #endregion
        #endregion
        private async Task<GetCAAsByEncomiendasWrapResponse> GetCAAsByEncomiendas(List<int> lst_id_Encomiendas)
        {
            ExternalService.ApraSrvRest apraSrvRest = new ExternalService.ApraSrvRest();
            GetCAAsByEncomiendasWrapResponse lstCaa = await apraSrvRest.GetCAAsByEncomiendas(lst_id_Encomiendas.ToList());
            return lstCaa;
        }
    }
}