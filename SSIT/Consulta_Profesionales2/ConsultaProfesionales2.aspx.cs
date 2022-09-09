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
using System.Data;

namespace SSIT.Consulta_Profesionales2
{
    public partial class ConsultaProfesionales2 : BasePage
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

        private string _profesional;
        public string profesional
        {
            get
            {
                if (_profesional == null || _profesional.Trim() == "")
                {
                    _profesional = hid_profesional.Value;
                }
                return _profesional;
            }
            set
            {
                hid_profesional.Value = value.ToString();
                _profesional = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager sm = ScriptManager.GetCurrent(this.Page);

            if (sm.IsInAsyncPostBack)
            {
                ScriptManager.RegisterStartupScript(updBuscar, updBuscar.GetType(), "init_JS_updpnlCircuito", "init_JS_updpnlCircuito();", true);
                ScriptManager.RegisterStartupScript(updgrdBandeja, updgrdBandeja.GetType(), "init_js_updgrdBandeja", "init_js_updgrdBandeja();", true);
            }
            if (!IsPostBack)
            {
                CargarComboCircuitos();
            }
        }
        private void CargarComboCircuitos()
        {
            EngineBL business = new EngineBL();

            List<int> lstIdCircuito = new List<int>();
            lstIdCircuito.Add((int)Constantes.ENG_Circuitos.SSP3);
            lstIdCircuito.Add((int)Constantes.ENG_Circuitos.SSP2);
            lstIdCircuito.Add((int)Constantes.ENG_Circuitos.SCP4);
            lstIdCircuito.Add((int)Constantes.ENG_Circuitos.SCP3);
            lstIdCircuito.Add((int)Constantes.ENG_Circuitos.SCP2);
            lstIdCircuito.Add((int)Constantes.ENG_Circuitos.ESPAR2);

            // lstIdCircuitoExcluir.Add((int)Constantes.ENG_Circuitos.ESCU_HP);
            // lstIdCircuitoExcluir.Add((int)Constantes.ENG_Circuitos.ESCU_IP);

           // ddlCircuitos.DataSource = business.GetDescripcionCircuitos(lstIdCircuitoExcluir).ToList();
            ddlCircuitos.DataSource = lstIdCircuito;
           // ddlCircuitos.DataTextField = "nombre_circuito";
            //ddlCircuitos.DataValueField = "id_circuito";
            ddlCircuitos.DataBind();
            ddlCircuitos.Items.Insert(0, "");

            
            foreach (ListItem item in ddlCircuitos.Items)
            {
                if(item.Value.Trim() != "")
                    switch (Convert.ToInt16(item.Value))
                    {
                        case 11:
                            item.Text = "Simples";
                            break;
                        case 12:
                            item.Text = "Comercios, Industrias y Servicios";
                            break;
                        case 14:
                            item.Text = "Con Habilitación Previa";
                            break;
                        case 15:
                            item.Text = "Automáticas";
                            break;
                        case 18:
                            item.Text = "Recreación";
                            break;
                        case 19:
                            item.Text = "Salud y Educación";
                            break;
                    }                
            }
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
            hid_profesional.Value = txtprofesional.Text;
            this.formulario_cargado = true;
            lblCantidadRegistros.Text = "0";
            grdBandeja.PageIndex = 0;
            grdBandeja.DataBind();

        }


        public List<SSIT_Listado_ProfesionalesDTO> GetProfesionales(int startRowIndex, int maximumRows, out int totalRowCount, string sortByExpression)
        {

            List<SSIT_Listado_ProfesionalesDTO> lstResult = new List<SSIT_Listado_ProfesionalesDTO>();
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

        private List<SSIT_Listado_ProfesionalesDTO> FiltrarProfesionales(int startRowIndex, int maximumRows, string sortByExpression, out int totalRowCount)
        {

            int BusCircuito = 0;
            if (ddlCircuitos.SelectedValue != null && ddlCircuitos.SelectedValue != "Todos" && ddlCircuitos.SelectedValue != "" && ddlCircuitos.SelectedValue != "0")
                int.TryParse(ddlCircuitos.SelectedValue, out BusCircuito);

            SSIT_Listado_ProfesionalesBL business = new SSIT_Listado_ProfesionalesBL();
            totalRowCount = 0;

            var q = business.GetProfesionalesSolicitud(BusCircuito, startRowIndex,
                maximumRows,
                sortByExpression,
                profesional,
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
            txtprofesional.Text = "";
            ddlCircuitos.ClearSelection();
        }


    }
}