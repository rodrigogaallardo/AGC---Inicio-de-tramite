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

namespace SSIT.Solicitud
{
    public partial class BandejaDeNotificaciones : SecurePage
    {
        private bool formulario_cargado
        {
            get
            {
                bool ret = false;
                bool.TryParse(hid_formulario_cargado.Value, out ret);
                return ret;
            }
            set
            {
                hid_formulario_cargado.Value = value.ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager sm = ScriptManager.GetCurrent(this.Page);
            if (sm.IsInAsyncPostBack)
            {
                ScriptManager.RegisterStartupScript(updBuscar, updBuscar.GetType(), "init_JS_updBuscar", "init_JS_updBuscar();", true);
            }
            if (!IsPostBack)
            {
                CargarComboMotivos();
            }
        }

        private void CargarComboMotivos()
        {
            SSITSolicitudesNotificacionesMotivosBL nmBL = new SSITSolicitudesNotificacionesMotivosBL();

            var lstMotivosAvisos = nmBL.getAllMotivos();

            ddlMotivos.DataSource = lstMotivosAvisos;
            ddlMotivos.DataTextField = "NotificacionMotivo";
            ddlMotivos.DataValueField = "IdNotificacionMotivo";
            ddlMotivos.DataBind();
            ddlMotivos.Items.Insert(0, new ListItem("(Todos)", "-1"));

        }

        protected void btnCargarDatos_Click(object sender, EventArgs e)
        {
            try
            {
                Buscar();
                this.EjecutarScript(updgrdBandeja, "finalizarCarga();");
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = Funciones.GetErrorMessage(ex);
                this.EjecutarScript(updgrdBandeja, "finalizarCarga();showfrmError();");
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }


        private void Buscar()
        {
            this.formulario_cargado = true;
            lblCantidadRegistros.Text = "0";
            grdBandeja.PageIndex = 0;
            hid_id_motivo.Value = ddlMotivos.SelectedValue;
            hid_id_solicitud.Value = txtNroSolicitud.Text.Trim();
            hid_ubicacion.Value = txtUbicacion.Text.ToUpper().Trim();
            grdBandeja.DataBind();
        }

        // El tipo devuelto puede ser modificado a IEnumerable, sin embargo, para ser compatible con paginación y ordenación 
        // , se deben agregar los siguientes parametros:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public List<AvisoNotificacionDTO> GetAvisoNotificaciones(int startRowIndex, int maximumRows, out int totalRowCount, string sortByExpression)
        {
            List<AvisoNotificacionDTO> lstAvisos = new List<AvisoNotificacionDTO>();
            totalRowCount = 0;

            try
            {
                if (this.formulario_cargado)
                {
                    lstAvisos = FiltrarAvisos(startRowIndex, maximumRows, sortByExpression, out totalRowCount);
                    int? TotalRegistros = totalRowCount;
                    totalRowCount = (TotalRegistros.HasValue ? (int)TotalRegistros : 0);
                    lblCantidadRegistros.Visible = true;
                    lblCantidadRegistros.Text = "0";
                }
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = Funciones.GetErrorMessage(ex);
                this.EjecutarScript(updgrdBandeja, "showfrmError();");
            }

            // Cantidad de tramites en la bandeja
            if (totalRowCount > 1)
                lblCantidadRegistros.Text = string.Format("{0} notificaciones", totalRowCount);
            else if (totalRowCount == 1)
                lblCantidadRegistros.Text = string.Format("1 notifición");
            return lstAvisos;
        }

        private List<AvisoNotificacionDTO> FiltrarAvisos(int startRowIndex, int maximumRows, string sortByExpression, out int totalRowCount)
        {

            SSITSolicitudesNotificacionesBL notifBL = new SSITSolicitudesNotificacionesBL();

            Guid userid = (Guid)Membership.GetUser().ProviderUserKey;

            List<AvisoNotificacionDTO> resultados = new List<AvisoNotificacionDTO>();

            totalRowCount = 0;

            int id_solicitud = (hid_id_solicitud.Value.Length > 0 ? int.Parse(hid_id_solicitud.Value) : 0);
            int id_motivo = -1;

            if (!int.TryParse(hid_id_motivo.Value, out id_motivo))
            {
                id_motivo = -1;
            }
            var q = notifBL.GetAvisoNotificaciones(userid, id_solicitud, id_motivo, startRowIndex, maximumRows, sortByExpression, out totalRowCount);

            if (!String.IsNullOrWhiteSpace(hid_ubicacion.Value))
            {
                var qfiltrado = q.Where(x => x.Domicilio.Contains(hid_ubicacion.Value)).ToList();
                totalRowCount = qfiltrado.Count();
                return qfiltrado;

            }
            resultados = q.ToList();

            //if (resultados.Count > 0)
            //{
            //    var path = string.Format("~/" + RouteConfig.VISOR_SOLICITUD);
            //    resultados = resultados.Select(x => { x.Url = path + x.IdTramite.ToString(); return x; }).ToList();

            //    //collection.Select(c => {c.PropertyToSet = value; return c;}).ToList();
            //}
            return resultados;
        }

        #region "Paging gridview Bandeja"

        protected void cmdPage(object sender, EventArgs e)
        {
            LinkButton cmdPage = (LinkButton)sender;
            grdBandeja.PageIndex = int.Parse(cmdPage.Text) - 1;


        }
        protected void cmdAnterior_Click(object sender, EventArgs e)
        {
            grdBandeja.PageIndex = grdBandeja.PageIndex - 1;

        }
        protected void cmdSiguiente_Click(object sender, EventArgs e)
        {
            grdBandeja.PageIndex = grdBandeja.PageIndex + 1;
        }


        protected void grdBandeja_DataBound(object sender, EventArgs e)
        {
            GridView grid = (GridView)grdBandeja;
            GridViewRow fila = (GridViewRow)grid.BottomPagerRow;

            if (fila != null)
            {
                LinkButton btnAnterior = (LinkButton)fila.Cells[0].FindControl("cmdAnterior");
                LinkButton btnSiguiente = (LinkButton)fila.Cells[0].FindControl("cmdSiguiente");

                if (grid.PageIndex == 0)
                    btnAnterior.Visible = false;
                else
                    btnAnterior.Visible = true;

                if (grid.PageIndex == grid.PageCount - 1)
                    btnSiguiente.Visible = false;
                else
                    btnSiguiente.Visible = true;


                // Ocultar todos los botones con Números de Página
                for (int i = 1; i <= 19; i++)
                {
                    LinkButton btn = (LinkButton)fila.Cells[0].FindControl("cmdPage" + i.ToString());
                    btn.Visible = false;
                }


                if (grid.PageIndex == 0 || grid.PageCount <= 10)
                {
                    // Mostrar 10 botones o el máximo de páginas

                    for (int i = 1; i <= 10; i++)
                    {
                        if (i <= grid.PageCount)
                        {
                            LinkButton btn = (LinkButton)fila.Cells[0].FindControl("cmdPage" + i.ToString());
                            btn.Text = i.ToString();
                            btn.Visible = true;
                        }
                    }
                }
                else
                {
                    // Mostrar 9 botones hacia la izquierda y 9 hacia la derecha
                    // o bien los que sea posible en caso de no llegar a 9

                    int CantBucles = 0;

                    LinkButton btnPage10 = (LinkButton)fila.Cells[0].FindControl("cmdPage10");
                    btnPage10.Visible = true;
                    btnPage10.Text = Convert.ToString(grid.PageIndex + 1);

                    // Ubica los 9 botones hacia la izquierda
                    for (int i = grid.PageIndex - 1; i >= grid.PageIndex - 9; i--)
                    {
                        CantBucles++;
                        if (i >= 0)
                        {
                            LinkButton btn = (LinkButton)fila.Cells[0].FindControl("cmdPage" + Convert.ToString(10 - CantBucles));
                            btn.Visible = true;
                            btn.Text = Convert.ToString(i + 1);
                        }

                    }

                    CantBucles = 0;
                    // Ubica los 9 botones hacia la derecha
                    for (int i = grid.PageIndex + 1; i <= grid.PageIndex + 9; i++)
                    {
                        CantBucles++;
                        if (i <= grid.PageCount - 1)
                        {
                            LinkButton btn = (LinkButton)fila.Cells[0].FindControl("cmdPage" + Convert.ToString(10 + CantBucles));
                            btn.Visible = true;
                            btn.Text = Convert.ToString(i + 1);
                        }
                    }



                }
                LinkButton cmdPage;
                string btnPage = "";
                for (int i = 1; i <= 19; i++)
                {
                    btnPage = "cmdPage" + i.ToString();
                    cmdPage = (LinkButton)fila.Cells[0].FindControl(btnPage);
                    if (cmdPage != null)
                        cmdPage.CssClass = "btn  btn-sm btn-default";

                }


                // busca el boton por el texto para marcarlo como seleccionado
                string btnText = Convert.ToString(grid.PageIndex + 1);
                foreach (Control ctl in fila.Cells[0].FindControl("pnlpager").Controls)
                {
                    if (ctl is LinkButton)
                    {
                        LinkButton btn = (LinkButton)ctl;
                        if (btn.Text.Equals(btnText))
                        {
                            btn.CssClass = "btn btn-sm btn-info";
                        }
                    }
                }

            }
        }
        #endregion
        protected void grdBandeja_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = e.Row;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                row.Cells[4].Text = row.Cells[4].Text.Replace("\\n", "<br />");
                if (row.Cells[5].Text == "&nbsp;")
                {
                    e.Row.Font.Bold = true;
                }
                else
                {
                    var check = (CheckBox)e.Row.FindControl("chkSelect");
                    check.Enabled = false;
                }
            }
        }

        protected void lnkDetalles_Click(object sender, EventArgs e)
        {
            LinkButton linnk = (LinkButton)(sender);
            string[] commandArgs = linnk.CommandArgument.ToString().Split(new char[] { ',' });
            string idMail = commandArgs[0];
            string idNotificacion = commandArgs[1];
            string idTramite = commandArgs[2];

            hfMailID.Value = idMail;
            int id_mail;
            int.TryParse(hfMailID.Value, out id_mail);
            MailsBL mailBL = new MailsBL();
            var mailDTO = mailBL.Single(id_mail);

            TableCell IDCorreo = (TableCell)FindControlRecursive(this.Master, "IDCorreo");
            TableCell Email = (TableCell)FindControlRecursive(this.Master, "Email");
            TableCell Asunto = (TableCell)FindControlRecursive(this.Master, "Asunto");
            TableCell FecAlta = (TableCell)FindControlRecursive(this.Master, "FecAlta");
            TableCell FecEnvio = (TableCell)FindControlRecursive(this.Master, "FecEnvio");
            TableCell CantInt = (TableCell)FindControlRecursive(this.Master, "CantInt");
            TableCell Prioridad = (TableCell)FindControlRecursive(this.Master, "Prioridad");
            TableCell CuerpoHTML = (TableCell)FindControlRecursive(this.Master, "CuerpoHTML");

            IDCorreo.Text = mailDTO.Mail_ID.ToString();
            Email.Text = mailDTO.Mail_Email.ToString();
            Asunto.Text = mailDTO.Mail_Asunto.ToString();
            FecAlta.Text = mailDTO.Mail_FechaAlta.ToString();
            CantInt.Text = mailDTO.Mail_Intentos.ToString();
            Prioridad.Text = mailDTO.Mail_Prioridad.ToString();
            FecEnvio.Text = mailDTO.Mail_FechaEnvio.ToString();

            Message.Attributes["src"] = "~/Handlers/Mail_Handler.ashx?HtmlID=" + id_mail;

            int IdNotificacion;
            int.TryParse(idNotificacion, out IdNotificacion);

            int IdTramite;
            int.TryParse(idTramite, out IdTramite);
            if (IdTramite < 200000)
            {
                TransferenciasNotificacionesBL notiTRBL = new TransferenciasNotificacionesBL();
                notiTRBL.UpdateNotificacionByIdNotificacion(IdNotificacion);
            }
            else
            {
                SSITSolicitudesNotificacionesBL notifBL = new SSITSolicitudesNotificacionesBL();
                notifBL.UpdateNotificacionByIdNotificacion(IdNotificacion);
            }

            if (Session["totalNot"] != null)
            {
                int totalnot = Convert.ToInt16(Session["totalNot"]);
                totalnot--;
                Session["totalNot"] = totalnot.ToString();
                Label btnBandejaNoti = (Label)FindControlRecursive(this, "lbtBandejaNotificaciones");
                btnBandejaNoti.Text = totalnot > 0 ? $" ({totalnot})" : "";
            }

            this.EjecutarScript(updBuscar, "finalizarCarga();");
            ScriptManager.RegisterStartupScript(updgrdBandeja, updgrdBandeja.GetType(), "showfrmAvisoNotificacion", "showfrmAvisoNotificacion('');", true);
        }
        public static Control FindControlRecursive(Control Root, string Id)
        {
            if (Root.ID == Id)
                return Root;

            foreach (Control Ctl in Root.Controls)
            {
                Control FoundCtl = FindControlRecursive(Ctl, Id);
                if (FoundCtl != null)
                    return FoundCtl;
            }
            return null;
        }
        protected void chkParent_CheckedChanged(object sender, EventArgs e)
        {
            var chkall = (sender as CheckBox);
            foreach (GridViewRow Row in grdBandeja.Rows)
            {
                if (Row.RowType == DataControlRowType.DataRow)
                {
                    var check = (CheckBox)Row.FindControl("chkSelect");
                    if (Row.Cells[5].Text == "&nbsp;")
                    {
                        check.Checked = chkall.Checked;
                    }
                }
            }
        }
        protected void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            int chktodas = 0;
            foreach (GridViewRow Row in grdBandeja.Rows)
            {
                if (Row.RowType == DataControlRowType.DataRow)
                {
                    var check = (CheckBox)Row.FindControl("chkSelect");
                    if (Row.Cells[5].Text == "&nbsp;" && check.Checked)
                    {
                        chktodas++;
                    }
                }
            }
            if (Session["totalNot"] != null)
            {
                int totalnot = Convert.ToInt16(Session["totalNot"]);
                CheckBox chktodosctrl = (CheckBox)FindControlRecursive(this.Master, "chkParent");
                chktodosctrl.Checked = (totalnot == chktodas);
            }
        }
        protected void btnMarcarComoLeido_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow Row in grdBandeja.Rows)
            {
                if (Row.RowType == DataControlRowType.DataRow)
                {
                    var check = (CheckBox)Row.FindControl("chkSelect");
                    if (Row.Cells[5].Text == "&nbsp;" && check.Checked)
                    {
                        int IdNotificacion;
                        int.TryParse(check.Text, out IdNotificacion);

                        //se necesita el numero de tramite para buscar en la tabla Transf_Solicitudes_Notificaciones o SSIT_Solicitudes_Notificaciones
                        var Tramite = ((HyperLink)grdBandeja.Rows[Row.RowIndex].Cells[0].Controls[0]).Text;
                        int IdTramite;
                        int.TryParse(Tramite, out IdTramite);
                        if (IdTramite < 200000)
                        {
                            TransferenciasNotificacionesBL notiTRBL = new TransferenciasNotificacionesBL();
                            notiTRBL.UpdateNotificacionByIdNotificacion(IdNotificacion);
                        }
                        else
                        {
                            SSITSolicitudesNotificacionesBL notifBL = new SSITSolicitudesNotificacionesBL();
                            notifBL.UpdateNotificacionByIdNotificacion(IdNotificacion);
                        }

                        if (Session["totalNot"] != null)
                        {
                            int totalnot = Convert.ToInt16(Session["totalNot"]);
                            totalnot--;
                            Session["totalNot"] = totalnot.ToString();
                        }
                    }
                }
            }
            Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);
        }
        protected void btnSeleccionarTodos_Click(object sender, EventArgs e)
        {
            grdBandeja.PageIndex = 0;
            foreach (GridViewRow Row in grdBandeja.Rows)
            {
                if (Row.RowType == DataControlRowType.DataRow)
                {
                    var check = (CheckBox)Row.FindControl("chkSelect");
                    if (Row.Cells[5].Text == "&nbsp;")
                    {
                        check.Checked = true;
                    }
                }
            }
        }
        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            ddlMotivos.ClearSelection();
            txtNroSolicitud.Text = "";
            txtUbicacion.Text = "";
            Buscar();
        }
    }
}