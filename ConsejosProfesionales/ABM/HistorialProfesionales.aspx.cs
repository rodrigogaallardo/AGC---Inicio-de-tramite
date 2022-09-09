using BusinesLayer.Implementation;
using DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConsejosProfesionales.ABM
{
    public partial class HistorialProfesionales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager sm = ScriptManager.GetCurrent(this.Page);
            if (!IsPostBack)
            {
                hiddenIdProfesional.Value = Request.QueryString["id"].ToString();
            }
        }
        protected void grdProfesionales_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Profesional_historialDTO oRow = (Profesional_historialDTO)e.Row.DataItem;
            }
        }

        protected void grdProfesionales_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            grdProfesionales.PageIndex = e.NewPageIndex;
            Buscar();

        }
        private void Buscar()
        {
            grdProfesionales.DataBind();
        }
        public IEnumerable<Profesional_historialDTO> grdProfesionales_GetData(int maximumRows, int startRowIndex, out int totalRowCount, string sortByExpression)
        {
            ProfesionalesBL profesionalesBL = new ProfesionalesBL();
            totalRowCount = 0;
            int idProfesional = 0;
            int.TryParse(hiddenIdProfesional.Value, out idProfesional);
            var ds = profesionalesBL.GetHistorial(idProfesional, startRowIndex, maximumRows, out totalRowCount).ToList();

            int cantResultados = totalRowCount;
            lblCantResultados.Text = cantResultados.ToString();
            lblCantResultados.Visible = (cantResultados > 0);
            return ds;
        }
        protected void cmdAnterior_Click(object sender, EventArgs e)
        {
            grdProfesionales.PageIndex = grdProfesionales.PageIndex - 1;
            Buscar();

        }
        protected void cmdSiguiente_Click(object sender, EventArgs e)
        {
            grdProfesionales.PageIndex = grdProfesionales.PageIndex + 1;
            Buscar();
        }
        protected void cmdPage(object sender, EventArgs e)
        {
            LinkButton cmdPage = (LinkButton)sender;
            grdProfesionales.PageIndex = int.Parse(cmdPage.Text) - 1;
            Buscar();


        }
        protected void grdProfesionales_DataBound(object sender, EventArgs e)
        {
            GridView grid = grdProfesionales;
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

        protected void cmdGuardar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ABM/ABMProfesionales.aspx?id=" + hiddenIdProfesional.Value.ToString());
        }
    }
}