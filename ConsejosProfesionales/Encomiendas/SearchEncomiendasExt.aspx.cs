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
    public partial class SearchEncomiendasExt : SecurePage
    {
        #region cargar inicial

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Membership.GetUser() == null)
                    Response.Redirect("~/Default.aspx");

                optBusquedaTodos.Attributes.Add("onclick", "seleccionEstados(false);");
                optBusquedaSeleccion.Attributes.Add("onclick", "seleccionEstados(true);");
                optBusquedaSeleccion.Attributes.Add("onclick", "seleccionEstados(true);");

                CargarEstados();
                CargarBandeja();
            }
        }

        private void CargarEstados()
        {
            EncomiendaEstadosBL estadosBL = new EncomiendaEstadosBL();
            chkEstados.DataSource = estadosBL.GetAllExt();
            chkEstados.DataTextField = "NomEstado";
            chkEstados.DataValueField = "IdEstado";
            chkEstados.DataBind();

            int id_estado_pendiente = (int)Constantes.Encomienda_Estados.Confirmada;
            chkEstados.Items.FindByValue(id_estado_pendiente.ToString()).Selected = true;

            string desc = chkEstados.Items.FindByValue(id_estado_pendiente.ToString()).Text;
            btnBusquedaLimpiar.Attributes.Add("onclick", " return LimpiarCamposFiltro('" + desc + "');");

        }

        private void CargarBandeja()
        {
            grdTramites.DataBind();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <param name="totalRowCount"></param>
        /// <returns></returns>
        private IList<EncomiendaExternaDTO>  TraerEncomiendasParaBandeja(int startRowIndex, int maximumRows, out int totalRowCount)
        {
            int tipo = (Request.QueryString["tipo_certificado"] == null) ? 0 : Convert.ToInt32(Request.QueryString["tipo_certificado"]);
            if (tipo == 0 || tipo == (int)Constantes.TipoCertificado.EncomiendaLey257)
            {
                tipo = (int)Constantes.TipoCertificado.EncomiendaLey257;
                lbltituloBusq.Text = "Encomienda de Ley 257 - Búsqueda de Trámites";
                lbltituloResult.Text = "Encomienda de Ley 257 - Listado de Trámites";
            }
            else if (tipo == (int)Constantes.TipoCertificado.Formulario_inscripción_demoledores_excavadores)
            {
                lbltituloBusq.Text = "Encomienda de Demoledores y Excavadores - Búsqueda de Trámites";
                lbltituloResult.Text = "Encomienda de Demoledores y Excavadores - Listado de Trámites";
            }

            int id_grupoconsejo = Traer_id_grupoconsejo();
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
                            estados += string.Format(",'{0}','{1}'", (int)Constantes.Encomienda_Estados.Incompleta, (int)Constantes.Encomienda_Estados.Completa);
                        }
                        else
                            estados += "," + chk.Value + "";
                }
            }
            if (estados.Length > 0)
                estados = estados.Substring(1);

            string nroMatricula = txtNroMatricula.Text.Trim();
            string Apenom = txtApeNom.Text.Trim();

            int nroTramite = 0;
            int nroEncomienda = 0;

            int.TryParse(txtNroTramite.Text.Trim(), out nroTramite);
            int.TryParse(txtNroEncomienda.Text.Trim(), out nroEncomienda);
            EncomiendaBL encomiendaBL = new EncomiendaBL();

            return encomiendaBL.TraerEncomiendasExConsejos(id_grupoconsejo, nroMatricula, Apenom, txtCuit.Text.Trim(), estados, tipo, nroTramite, nroEncomienda, startRowIndex, maximumRows,out totalRowCount);

        }

        public IList<EncomiendaExternaDTO> GetTramites(int startRowIndex, int maximumRows, out int totalRowCount, string sortByExpression)
        {
            IList<EncomiendaExternaDTO> lstTramites = new List<EncomiendaExternaDTO>();
            totalRowCount = 0;

            try
            {
                totalRowCount = 0;
                lstTramites = TraerEncomiendasParaBandeja(startRowIndex, maximumRows, out totalRowCount);
                int? TotalRegistros = totalRowCount;
                totalRowCount = (TotalRegistros.HasValue ? (int)TotalRegistros : 0);

            }
            catch (Exception ex)
            {

            }


            return lstTramites;
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

        #endregion

      
        #region paginacion grilla

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            grdTramites.PageIndex = 0;
            CargarBandeja();
        }

        protected void grdTramites_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdTramites.PageIndex = e.NewPageIndex;
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
        #endregion
    }
}