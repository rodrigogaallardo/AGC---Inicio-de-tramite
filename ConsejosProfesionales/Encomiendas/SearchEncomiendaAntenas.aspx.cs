using BusinesLayer.Implementation;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using DataTransferObject;
using System.Web.UI.WebControls;

namespace ConsejosProfesionales.Encomiendas
{
    public partial class SearchEncomiendaAntenas : SecurePage
    {
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
            EncomiendaBL encomiendaBL = new EncomiendaBL();
            var estados =  encomiendaBL.GetAntEstados();

            chkEstados.DataSource = estados;
            chkEstados.DataTextField = "nom_estado";
            chkEstados.DataValueField = "id_estado";
            chkEstados.DataBind();

            ListItem itm = new ListItem();
            itm.Text = "Trámites sin confirmar";
            itm.Value = "-1";

            chkEstados.Items.Insert(0, itm);

            int id_estado_pendiente = (int)Constantes.Estado_Encomienda_Antenas.PendienteIngreso;
            chkEstados.Items.FindByValue(id_estado_pendiente.ToString()).Selected = true;

        }

        private void CargarBandeja()
        {
            grdTramites.DataSource = TraerEncomiendasParaBandeja();
            grdTramites.DataBind();
        }

        private IEnumerable<EncomiendaAntenasGrillaDTO> TraerEncomiendasParaBandeja()
        {
            Guid userid = (Guid)Membership.GetUser().ProviderUserKey;

            string estados = "";

            if (optBusquedaSeleccion.Checked)
            {
                foreach (ListItem chk in chkEstados.Items)
                {
                    if (chk.Selected)
                        if (chk.Value == "-1")
                        {
                            //Trámites sin confirmar
                            estados += string.Format(",'{0}','{1}'", (int)Constantes.Estado_Encomienda_Antenas.Incompleto, (int)Constantes.Estado_Encomienda_Antenas.Completo);
                        }
                        else
                            estados += "," + chk.Value;

                }
            }
            if (estados.Length > 0)
                estados = estados.Substring(1);

            string nroMatricula = txtNroMatricula.Text.Trim();
            string Apenom = txtApeNom.Text.Trim();

       
            int nroTramite = 0;

            int.TryParse(txtNroTramite.Text.Trim(), out nroTramite);

            return TraerEncomiendasConsejos(nroMatricula, Apenom, txtCuit.Text.Trim(), estados, nroTramite);
        }

        protected void btnCargarDatos_Click(object sender, EventArgs e)
        {
            try
            {
                CargarBandeja();
                this.EjecutarScript(updgrdBandeja, "finalizarCarga();");

            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = Funciones.GetErrorMessage(ex);
                this.EjecutarScript(updgrdBandeja, "finalizarCarga();showfrmError();");
            }

        }
        
        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            //blanquear textBox
            txtNroMatricula.Text = "";
            txtApeNom.Text = "";
            txtCuit.Text = "";
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
        protected void lnkNroEncomienda_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.Parent.Parent;
            int id_estado = Convert.ToInt32(grdTramites.DataKeys[row.RowIndex].Values[1]);

            if (id_estado == (int)Constantes.Estado_Encomienda_Antenas.Incompleto ||
                id_estado == (int)Constantes.Estado_Encomienda_Antenas.Completo ||
                id_estado == (int)Constantes.Estado_Encomienda_Antenas.Anulado)
            {
                //los pdf se guardan en base cuando el profesional confirma el mismo, en cualquier otro caso no se permite visualizar el pdf
                //lblmpeInfo.Text = "Sólo podrá visualizar las Encomiendas que hayan sido Confirmadas por el Profesional.";
                //ScriptManager.RegisterClientScriptBlock(pnlGrillaTramites, pnlGrillaTramites.GetType(), "mostrarError", string.Format("mostrarPopup('{0}');", pnlInformacion.ClientID), true);
                //return;
            }

            int id_encomienda_visualizar = 0;

            if (int.TryParse(lnk.Text, out id_encomienda_visualizar))
            {
                Response.Redirect(string.Format("~/{0}{1}/{2}",RouteConfig.ACTUALIZA_ENCOMIENDA_ANTENA, id_encomienda_visualizar, 1)); 
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            grdTramites.PageIndex = 0;
            CargarBandeja();
        }

        protected void cmdPage(object sender, EventArgs e)
        {
            LinkButton cmdPage = (LinkButton)sender;
            grdTramites.PageIndex = int.Parse(cmdPage.Text) - 1;
            CargarBandeja();
        }

        protected void cmdAnterior_Click(object sender, EventArgs e)
        {
            grdTramites.PageIndex = grdTramites.PageIndex - 1;
            CargarBandeja();

        }
        protected void cmdSiguiente_Click(object sender, EventArgs e)
        {
            grdTramites.PageIndex = grdTramites.PageIndex + 1;
            CargarBandeja();
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="matricula"></param>
        /// <param name="Apenom"></param>
        /// <param name="cuit"></param>
        /// <param name="estados"></param>
        /// <param name="pIdEncomienda"></param>
        /// <returns></returns>
        private IEnumerable<EncomiendaAntenasGrillaDTO> TraerEncomiendasConsejos(string matricula, string Apenom, string cuit, string estados, int pIdEncomienda)
        {
            EncomiendaBL encomiendaBL = new EncomiendaBL();
            return encomiendaBL.Encomienda_TraerEncomiendasConsejo_ANT(matricula, Apenom, cuit, estados, pIdEncomienda);            
        }
    }
}