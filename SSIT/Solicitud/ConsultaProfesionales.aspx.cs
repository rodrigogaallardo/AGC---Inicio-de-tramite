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
    public partial class ConsultaProfesionales : SecurePage
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
            lnkImprimirLstProf.NavigateUrl = string.Format("~/" + RouteConfig.DESCARGA_LISTADO_PROFESIONALES);
            if (sm.IsInAsyncPostBack)
            {
                ScriptManager.RegisterStartupScript(updBuscar, updBuscar.GetType(), "init_JS_updBuscarUbicacion", "init_JS_updBuscarUbicacion();", true);

            }
            if (!IsPostBack)
            {
                CargarComboGrupoConsejo();
                CargarComboLocalidad();


            }
        }
        private void CargarComboGrupoConsejo()
        {
            GrupoConsejosBL business = new GrupoConsejosBL();
            List<int> lstIdGrupoExcluir = new List<int>();
            lstIdGrupoExcluir.Add((int)Constantes.GrupoConsejos.CPIAyE);
            lstIdGrupoExcluir.Add((int)Constantes.GrupoConsejos.COPITEC);
            lstIdGrupoExcluir.Add((int)Constantes.GrupoConsejos.CPIN);
            lstIdGrupoExcluir.Add((int)Constantes.GrupoConsejos.CPIQ);

            ddlBusGrupoConsejo.DataSource = business.GetExcluye(lstIdGrupoExcluir).ToList();
            ddlBusGrupoConsejo.DataTextField = "Descripcion";
            ddlBusGrupoConsejo.DataValueField = "Id";
            ddlBusGrupoConsejo.DataBind();
            ddlBusGrupoConsejo.Items.Insert(0, "");

        }
        private void CargarComboLocalidad()
        {
            ProfesionalesBL business = new ProfesionalesBL();

            ddlBusLocalidad.DataSource = business.GetLocalidadProfesional().ToList();
            ddlBusLocalidad.DataTextField = "Depto";
            ddlBusLocalidad.DataValueField = "";
            ddlBusLocalidad.DataBind();
            ddlBusLocalidad.Items.Insert(0, "");

        }

        private void CargarComboConsejo(int id_grupoconsejo)
        {
            ConsejoProfesionalBL business = new ConsejoProfesionalBL();

            ddlBusConsejo.DataSource = business.GetConsejosxGrupo(id_grupoconsejo).ToList();
            ddlBusConsejo.DataTextField = "Nombre";
            ddlBusConsejo.DataValueField = "Id";
            ddlBusConsejo.DataBind();
            ddlBusConsejo.Items.Insert(0, "");
            ddlBusConsejo.Style["display"] = "inline-block";
        }

        protected void btnCargarDatos_Click(object sender, EventArgs e)
        {
            try
            {
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
            hid_id_solicitud.Value = txtBusMatricula.Text.Trim();
            //hid_id_tipotramite.Value = ddlTipoTramite.SelectedValue;
            //hid_id_estado.Value = ddlEstados.SelectedValue;
            grdBandeja.DataBind();

        }
        public List<ProfesionalDTO> GetProfesionales(int startRowIndex, int maximumRows, out int totalRowCount, string sortByExpression)
        {

            List<ProfesionalDTO> lstResult = new List<ProfesionalDTO>();
            totalRowCount = 0;

            try
            {
                if (this.formulario_cargado)
                {
                    lstResult = FiltrarProfesionales(startRowIndex, maximumRows, sortByExpression, out totalRowCount);
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
                lblCantidadRegistros.Text = string.Format("{0} profesionales", totalRowCount);
            else if (totalRowCount == 1)
                lblCantidadRegistros.Text = string.Format("{0} profesional", totalRowCount);

            return lstResult;
        }

        private List<ProfesionalDTO> FiltrarProfesionales(int startRowIndex, int maximumRows, string sortByExpression, out int totalRowCount)
        {

            string BusMatricula = txtBusMatricula.Text.Trim();
            string BusNomyApe = txtBusApeNom.Text.Trim();

            int BusGrupoConsejo = 99;
            if (ddlBusGrupoConsejo.SelectedValue != null && ddlBusGrupoConsejo.SelectedValue != "Todos" && ddlBusGrupoConsejo.SelectedValue != "" && ddlBusGrupoConsejo.SelectedValue != "0")
                int.TryParse(ddlBusGrupoConsejo.SelectedValue, out BusGrupoConsejo);
            int BusConsejo = 99;
            if (BusGrupoConsejo == 5 && ddlBusConsejo.SelectedValue != "Todos" && ddlBusConsejo.SelectedValue != "" && ddlBusConsejo.SelectedValue != "0")
                int.TryParse(ddlBusConsejo.SelectedValue, out BusConsejo);
            string BusLocalidad = "";
            if (ddlBusLocalidad.SelectedValue != null && ddlBusLocalidad.SelectedValue != "Todas" && ddlBusLocalidad.SelectedValue != "" && ddlBusLocalidad.SelectedValue != "0")
                BusLocalidad = ddlBusLocalidad.SelectedItem.Text.Trim();

            ProfesionalesBL business = new ProfesionalesBL();
            totalRowCount = 0;

            var q = business.GetProfesionales(BusMatricula, BusNomyApe, BusGrupoConsejo, BusConsejo, BusLocalidad,
                startRowIndex,
                maximumRows,
                sortByExpression,
                out totalRowCount);


            return q;
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
                //row.Cells[5].Text = row.Cells[5].Text.Replace("\\n", "<br />");
            }

        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBusApeNom.Text = "";
            txtBusMatricula.Text = "";
            ddlBusGrupoConsejo.ClearSelection();
            ddlBusConsejo.ClearSelection();
            ddlBusConsejo.Style["display"] = "none";
            ddlBusLocalidad.ClearSelection();
        }


        protected void ddlBusGrupoConsejo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id_grupoconsejo = 0;
            try
            {
                if (ddlBusGrupoConsejo.SelectedValue != "Todos" && ddlBusGrupoConsejo.SelectedValue != "99")
                {
                    int.TryParse(ddlBusGrupoConsejo.SelectedValue, out id_grupoconsejo);
                    if (id_grupoconsejo == 5)
                    {
                        CargarComboConsejo(id_grupoconsejo);
                    }
                    else
                    {
                        ddlBusConsejo.ClearSelection();
                        ddlBusConsejo.Style["display"] = "none";
                    }
                }
                else
                {
                    ddlBusConsejo.ClearSelection();
                    ddlBusConsejo.Style["display"] = "none";
                }
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = Funciones.GetErrorMessage(ex);
                this.EjecutarScript(updgrdBandeja, "finalizarCarga();showfrmError();");
            }
        }


    }
}