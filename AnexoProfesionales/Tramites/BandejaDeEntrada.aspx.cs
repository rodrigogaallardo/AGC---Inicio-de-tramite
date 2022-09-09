using AnexoProfesionales.App_Components;
using BusinesLayer.Implementation;
using DataTransferObject;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnexoProfesionales
{
    public partial class BandejaDeEntrada : BasePage
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
                CargarComboTiposDeTramite();
                CargarComboEstados();
            }
        }

        private void CargarComboTiposDeTramite()
        {
            TipoTramiteBL ttbl = new TipoTramiteBL();
            ddlTipoTramite.DataSource = ttbl.GetAll();
            ddlTipoTramite.DataTextField = "DescripcionTipoTramite";
            ddlTipoTramite.DataValueField = "IdTipoTramite";
            ddlTipoTramite.DataBind();

            ddlTipoTramite.Items.Insert(0, new ListItem("(Todos)", "-1"));
        
        }

        private void CargarComboEstados()
        {
            int id_encomienda = (hid_id_encomienda.Value.Length > 0 ? int.Parse(hid_id_encomienda.Value) : 0);
            int id_solicitud = (hid_id_solicitud.Value.Length > 0 ? int.Parse(hid_id_solicitud.Value) : 0);
            List<EncomiendaDTO> resultados = new List<EncomiendaDTO>();
            Guid userid = (Guid)Membership.GetUser().ProviderUserKey;
            EncomiendaBL ebl = new EncomiendaBL();

            var valoresEstadosValidos = ebl.GetEstadosByUserId(userid);
            EncomiendaEstadosBL eebl = new EncomiendaEstadosBL();

            ddlEstados.Items.Clear();
            ddlEstados.DataSource = eebl.GetAll().Where(x => valoresEstadosValidos.Contains(x.IdEstado)).OrderBy(x => x.IdEstado).ToList();
            ddlEstados.DataTextField = "NomEstado";
            ddlEstados.DataValueField = "IdEstado";
            ddlEstados.DataBind();

            ddlEstados.Items.Insert(0, new ListItem("(Todos)", "-1"));
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
            hid_id_encomienda.Value = txtNroAnexo.Text.Trim();
            hid_id_solicitud.Value = txtNroSolicitud.Text.Trim();
            //.Value = txtUbicacion.Text.Trim().ToUpper(); // se le pone mayuscula porque en la tabla esta en mayuscula
            hid_id_tipotramite.Value = ddlTipoTramite.SelectedValue;
            hid_id_estado.Value = ddlEstados.SelectedValue;
            grdBandeja.DataBind();

        }

        // El tipo devuelto puede ser modificado a IEnumerable, sin embargo, para ser compatible con paginación y ordenación 
        // , se deben agregar los siguientes parametros:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public List<TramitesDTO> GetTramites(int startRowIndex, int maximumRows, out int totalRowCount, string sortByExpression)
        {
            List<TramitesDTO> lstTramites = new List<TramitesDTO>();
            totalRowCount = 0;

            try
            {

                if (this.formulario_cargado)
                {
                    lstTramites = FiltrarTramites(startRowIndex, maximumRows, sortByExpression, out totalRowCount);
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
                lblCantidadRegistros.Text = string.Format("{0} trámites", totalRowCount);
            else if (totalRowCount == 1)
                lblCantidadRegistros.Text = string.Format("{0} trámite", totalRowCount);

            return lstTramites;
        }

        private List<TramitesDTO> FiltrarTramites(int startRowIndex, int maximumRows, string sortByExpression, out int totalRowCount)
        {
            List<TramitesDTO> resultados = new List<TramitesDTO>();
            List<TramitesDTO> resultadosFiltradoUbic = new List<TramitesDTO>();
            Guid userid = (Guid)Membership.GetUser().ProviderUserKey;
            EncomiendaBL ebl = new EncomiendaBL();

            totalRowCount = 0;

            int id_encomienda = (hid_id_encomienda.Value.Length > 0 ? int.Parse(hid_id_encomienda.Value) : 0);
            int id_solicitud = (hid_id_solicitud.Value.Length > 0 ? int.Parse(hid_id_solicitud.Value) : 0);
            int id_tipotramite = -1;    // Todos los tipos de trámite
            int id_estado = -1;        // Todos los estados

            int.TryParse(hid_id_tipotramite.Value, out id_tipotramite);
            int.TryParse(hid_id_estado.Value, out id_estado);

            var q = ebl.GetByTramiteEstado(userid, id_encomienda, id_tipotramite, id_estado, id_solicitud, startRowIndex, maximumRows, out totalRowCount);

            resultados = q.ToList();



            if (resultados.Count > 0)
            {
                List<int> arrEncomiendas = resultados.Select(e => e.id_encomienda).ToList();
                List<ItemDirectionDTO> lstDireccionesEncomienda = new List<ItemDirectionDTO>();

                if (arrEncomiendas.Count() > 0)
                    lstDireccionesEncomienda = ebl.GetDireccionesEncomienda(arrEncomiendas).ToList();

                //------------------------------------------------------------------------
                //Rellena la clase a devolver con los datos que faltaban (Domicilio)
                //------------------------------------------------------------------------

                foreach (var row in resultados)
                {

                    ItemDirectionDTO itemDireccion = null;

                    itemDireccion = lstDireccionesEncomienda.FirstOrDefault(x => x.id_solicitud == row.id_encomienda);

                    if (itemDireccion != null)
                        row.Domicilio = (string.IsNullOrEmpty(itemDireccion.direccion) ? "" : itemDireccion.direccion);

                }
            }
            if (!String.IsNullOrWhiteSpace(hid_ubicacion.Value))
            {
                resultadosFiltradoUbic = resultados.Where(x => x.Domicilio.Contains(hid_ubicacion.Value)).ToList();
                totalRowCount = resultadosFiltradoUbic.Count();
                return resultadosFiltradoUbic;
            }
            // ---

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
                row.Cells[5].Text = row.Cells[5].Text.Replace("\\n", "<br />");
            }

        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            ddlTipoTramite.ClearSelection();
            ddlEstados.ClearSelection();
            txtNroAnexo.Text = "";
            txtNroSolicitud.Text = "";
            Buscar();
        }

        protected void ddlTipoTramite_SelectedIndexChanged(object sender, EventArgs e)
        {

            int id_tipotramite = 0;
            int.TryParse(ddlTipoTramite.SelectedValue, out id_tipotramite);
            //CargarComboEstados(id_tipotramite);

        }


    }
}