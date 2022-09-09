using BusinesLayer.Implementation;
using DataTransferObject;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;

namespace ConsejosProfesionales.Encomiendas
{
    public partial class SearchEncomiendas : BasePage
    {

        EncomiendaBL encBL = new EncomiendaBL();

        protected void Page_Load(object sender, EventArgs e)
        {

            ScriptManager sm = ScriptManager.GetCurrent(this.Page);
            if (sm.IsInAsyncPostBack)
            {
                ScriptManager.RegisterStartupScript(updgrdBandeja, updgrdBandeja.GetType(), "init_JS_updBuscarTramites", "init_JS_updBuscarTramites();", true);

            }
            if (!IsPostBack)
            {
               if (Membership.GetUser() == null)
                Response.Redirect("~/Default.aspx");

                CargarEstados();
            }
        }
        private void CargarEstados()
        {
            EncomiendaEstadosBL estadosBL = new EncomiendaEstadosBL();
            
            chkEstados.DataSource = estadosBL.GetAll();
            chkEstados.DataTextField = "NomEstado";
            chkEstados.DataValueField = "IdEstado";
            chkEstados.DataBind();

            ListItem itm = new ListItem();
            itm.Text = "Trámites sin confirmar";
            itm.Value = "-1";

            chkEstados.Items.Insert(0, itm);

            int id_estado_pendiente = (int)Constantes.Encomienda_Estados.Confirmada;
            chkEstados.Items.FindByValue(id_estado_pendiente.ToString()).Selected = true;

            //CAN, 06-01-2012, mantis 0067512
            //se agrega boton para limiar campos del filtro 
            string desc = chkEstados.Items.FindByValue(id_estado_pendiente.ToString()).Text;
            //btnBusquedaLimpiar.Attributes.Add("onclick", " return LimpiarCamposFiltro('" + desc + "');");

        }

        private void CargarBandeja()
        {
            try
            {
                grdTramites.DataBind();
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = Funciones.GetErrorMessage(ex);
                this.EjecutarScript(updgrdBandeja, "showfrmError();");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IList<EncomiendaDTO> TraerEncomiendasParaBandeja(int startRowIndex, int maximumRows, out int totalRowCount)
        {

            int id_grupoconsejo = Traer_id_grupoconsejo();
            Guid userid = (Guid)Membership.GetUser().ProviderUserKey;

            string estados = "";

            if (optBusquedaSeleccion.Checked)
            {
                foreach (ListItem chk in chkEstados.Items)
                    if (chk.Selected)
                        if (chk.Value != "-1")
                            
                            estados += "," + chk.Value;
            }
            if (estados.Length > 0)
                estados = estados.Substring(1);

            string nroMatricula = txtNroMatricula.Text.Trim();
            string Apenom = txtApeNom.Text.Trim();

            //CAN, 06-01-2012, mantis 0067512
            //se agrega filtro por nro de tramite y nro de encomienda del consejo
            int nroTramite = 0;
            int nroEncomiendaConsejo = 0;

            int.TryParse(txtNroTramite.Text.Trim(), out nroTramite);
            int.TryParse(txtNroEncomiendaConsejo.Text.Trim(), out nroEncomiendaConsejo);

            
            return encBL.TraerEncomiendasConsejos(id_grupoconsejo, nroMatricula, Apenom, txtCuit.Text.Trim(), estados, nroTramite, nroEncomiendaConsejo, startRowIndex, maximumRows, out totalRowCount);
            
        }

        public IList<EncomiendaDTO> GetTramites(int startRowIndex, int maximumRows, out int totalRowCount, string sortByExpression)
        {
            IList<EncomiendaDTO> lstTramites = new List<EncomiendaDTO>();
            totalRowCount = 0;

            if (!IsPostBack)
                return null;

            totalRowCount = 0;
            lstTramites = TraerEncomiendasParaBandeja(startRowIndex, maximumRows, out totalRowCount);
            int? TotalRegistros = totalRowCount;
            totalRowCount = (TotalRegistros.HasValue ? (int)TotalRegistros : 0);
            lblCantidadRegistros.Text = totalRowCount.ToString();

            return lstTramites;
        }

        protected void lnkNroEncomienda_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.Parent.Parent;
            int id_estado = Convert.ToInt32(grdTramites.DataKeys[row.RowIndex].Values[1]);

            if (id_estado == (int)Constantes.Encomienda_Estados.Incompleta||
                id_estado == (int)Constantes.Encomienda_Estados.Completa ||
                id_estado == (int)Constantes.Encomienda_Estados.Anulada)
            {
                //los pdf se guardan en base cuando el profesional confirma el mismo, en cualquier otro caso no se permite visualizar el pdf
                //lblmpeInfo.Text = "Sólo podrá visualizar las Encomiendas que hayan sido Confirmadas por el Profesional.";
                //ScriptManager.RegisterClientScriptBlock(pnlGrillaTramites, pnlGrillaTramites.GetType(), "mostrarError", "mostrarPopup('pnlInformacion');", true);
                return;
            }

            int id_encomienda_visualizar = 0;

            if (int.TryParse(lnk.Text, out id_encomienda_visualizar))
            {
                Response.Redirect(string.Format("~/{0}{1}/{2}",RouteConfig.ACTUALIZA_ENCOMIENDA, id_encomienda_visualizar,1));
            }

        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            //blanquear textBox
            txtNroMatricula.Text = "";
            txtApeNom.Text = "";
            txtCuit.Text = "";
            txtNroEncomiendaConsejo.Text = "";
            txtNroTramite.Text = "";

            optBusquedaTodos.Checked = false;
            optBusquedaSeleccion.Checked = true;
            //recorer items y dejar marcado solo el confirmado
            foreach (ListItem oItem in chkEstados.Items)
            {
                if (oItem.Value == "0")
                    oItem.Selected = true;
                else
                    oItem.Selected = false;
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            grdTramites.PageIndex = 0;
            CargarBandeja();
        }

        protected void btnCargarDatos_Click(object sender, EventArgs e)
        {
            try
            {
                //CargarBandeja();
                this.EjecutarScript(updgrdBandeja, "finalizarCarga();");
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = Funciones.GetErrorMessage(ex);
                this.EjecutarScript(updgrdBandeja, "finalizarCarga();showfrmError();");
            }

        }
        
        private int Traer_id_grupoconsejo()
        {

            Guid userid = (Guid)Membership.GetUser().ProviderUserKey;
            
            GrupoConsejosBL grupoConsejoBL = new GrupoConsejosBL();

            var dsGrupoConsejo = grupoConsejoBL.Get(userid);

            return dsGrupoConsejo.FirstOrDefault().Id;

        }


        protected void cmdPage(object sender, EventArgs e)
        {
            LinkButton cmdPage = (LinkButton)sender;
            grdTramites.PageIndex = int.Parse(cmdPage.Text) - 1;
        }

        protected void cmdAnterior_Click(object sender, EventArgs e)
        {
            grdTramites.PageIndex = grdTramites.PageIndex - 1;

        }
        protected void cmdSiguiente_Click(object sender, EventArgs e)
        {
            grdTramites.PageIndex = grdTramites.PageIndex + 1;
        }

        protected void grdTramites_DataBound(object sender, EventArgs e)
        {

            GridView grid = grdTramites;
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
                        cmdPage.CssClass = "btn btn-default";

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
                            btn.CssClass = "btn btn-info";
                        }
                    }
                }

            }
        }
    }
}