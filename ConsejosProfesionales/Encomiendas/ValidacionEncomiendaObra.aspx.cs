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

namespace ConsejosProfesionales.Encomiendas
{
    public partial class ValidacionEncomiendaObra : BasePage
    {       
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager sm = ScriptManager.GetCurrent(this.Page);
            if (sm.IsInAsyncPostBack)
            {
                ScriptManager.RegisterStartupScript(updgrdBandeja, updgrdBandeja.GetType(), "init_JS_updBuscarTramites", "init_JS_updBuscarTramites();", true);

            }            
        }    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <param name="sortByExpression"></param>
        /// <param name="totalRowCount"></param>
        /// <returns></returns>
        public List<EncomiendaExternaDTO> GetTramites(int startRowIndex, int maximumRows, string sortByExpression, out int totalRowCount)
        {
            DateTime dt = DateTime.Now;
            
            int BusIDGrupoConsejo = Traer_id_grupoconsejo();
            
            int BusTipoTramite = 11;
            //esto lleva por default, entonces si no se tildo algun estado, seran estado "1" o sea segun la query , "todos"

            List<int> BusListEstados = new List<int>();

            if (optBusquedaSeleccion.Checked)
            {
                foreach (ListItem chk in chkEstados.Items)
                {
                    if (chk.Selected)
                    {
                        if (chk.Value == "0")
                        {
                            //Pendiente
                            BusListEstados.Add(0);
                        }
                        if (chk.Value == "4")
                        {
                            //Validado
                            BusListEstados.Add(4);
                        }
                        if (chk.Value == "5")
                        {
                            //Rechazado
                            BusListEstados.Add(5);
                        }
                        if (chk.Value == "20")
                        {
                            //Anulado
                            BusListEstados.Add(20);
                        }
                    }
                }
            }

            string BusMatricula = txtNroMatricula.Text.Trim();
            string BusApenom = txtApeNom.Text.Trim();

            string BusNroTramite = string.Empty;
            BusNroTramite = Convert.ToString(txtNroEncomienda.Text.Trim());
            //int.TryParse(txtNroEncomienda.Text.Trim(), out BusNroTramite);

            EncomiendaBL encomiendaBL = new EncomiendaBL();

            totalRowCount = 0;
            
            dt = DateTime.Now;
            var q = encomiendaBL.TraerEncomiendasDirectorObra(BusIDGrupoConsejo, BusMatricula, BusApenom, txtCuit.Text.Trim(), BusListEstados, BusNroTramite, BusTipoTramite,
                startRowIndex,
                maximumRows,
                sortByExpression,
                out totalRowCount);
            
            lblCantidadRegistros.Text = totalRowCount.ToString();

            System.Diagnostics.Debug.Write("Consulta: " + (DateTime.Now - dt).Milliseconds.ToString() + Environment.NewLine);           
            return q;            
        }

        protected void lnkNroEncomienda_Click(object sender, EventArgs e)
        {

            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.Parent.Parent;
            if (row.ForeColor == System.Drawing.Color.Red)
            {
                string script = "alert(\"Para esta dirección ya existe un director de obra asociado.\\n\\nDebe validarse previamente el trámite de desligue para poder ligar la obra a un nuevo director. \");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
            else
            {
                int id_estado = Convert.ToInt32(grdBandeja.DataKeys[row.RowIndex].Values[1]);
                int id_encomienda = Convert.ToInt32(grdBandeja.DataKeys[row.RowIndex].Values[0]);
                int id_tipocertificado = Convert.ToInt32(grdBandeja.DataKeys[row.RowIndex].Values[2]);

                Response.Redirect(string.Format("~/{0}{1}/{2}", RouteConfig.DETALLE_ENCOMIENDA_OBRA, id_encomienda, id_tipocertificado));
            }

        }
        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            //blanquear textBox
            txtNroMatricula.Text = "";
            txtApeNom.Text = "";
            txtCuit.Text = "";
            txtNroEncomienda.Text = "";

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

        protected void btnCargarDatos_Click(object sender, EventArgs e)
        {
            try
            {
                grdBandeja.PageIndex = 0;
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
            try
            {
                //lblCantidadRegistros.Text = "0";
                grdBandeja.PageIndex = 0;
                grdBandeja.DataBind();
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = Funciones.GetErrorMessage(ex);
                this.EjecutarScript(updgrdBandeja, "showfrmError();");
            }
        }

        private int Traer_id_grupoconsejo()
        {

            int ret = 0;
            Guid userid = (Guid)Membership.GetUser().ProviderUserKey;

            GrupoConsejosBL grupoConsejoBL = new GrupoConsejosBL();

            var dsGrupoConsejo = grupoConsejoBL.Get(userid);

            foreach (var dr in dsGrupoConsejo)
            {
                int.TryParse(dr.Id.ToString(), out ret);
            }
            return ret;

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
     
        #endregion

    }
}