using BusinesLayer.Implementation;
using DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StaticClass;

namespace SSIT.Solicitud.Habilitacion.Controls
{
    public partial class Rubros2 : System.Web.UI.UserControl
    {
        public bool _enabled
        {
            get
            {
                bool ret;
                bool.TryParse(hid_enabled.Value, out ret);
                return ret;
            }
            set
            {
                hid_enabled.Value = value.ToString();
            }
        }

        private int id_solicitud
        {
            get
            {
                int ret = 0;
                int.TryParse(Convert.ToString(Page.RouteData.Values["id_solicitud"]), out ret);
                return ret;
            }
            set
            {
                hid_id_solicitud.Value = value.ToString();
            }

        }

        public void CargarDatos(int idSol)
        {
            _enabled = true;
            id_solicitud = idSol;
            SSITSolicitudesNuevasBL blSol = new SSITSolicitudesNuevasBL();
            SSITSolicitudesNuevasDTO sol = blSol.Single(idSol);
            if (sol.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.ETRA || sol.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.ANU)
            {
                AgregarRubro.Visible = false;
                _enabled = false;
            }
            CargarRubros(sol);
        }


        private void CargarRubros(SSITSolicitudesNuevasDTO sol)
        {

            if (sol.RelRubrosSolicitudesNuevasDTO != null)
            {
                grdRubrosIngresados.DataSource = sol.RelRubrosSolicitudesNuevasDTO;
                grdRubrosIngresados.DataBind();
            }
            else
            {
                RubrosCNBL rcnBL = new RubrosCNBL();

                var lstRubros = rcnBL.GetAll();

                grdRubrosIngresados.DataSource = lstRubros;
                grdRubrosIngresados.DataBind();
            }
        }

        public void ValidarRubros()
        {
            if (grdRubrosIngresados.Rows.Count == 0)
                throw new Exception(Errors.ENCOMIENDA_INGRESAR_RUBROS);
        }
        protected void btnnuevaBusqueda_Click(object sender, EventArgs e)
        {

            /*txtSuperficie.Text = hid_Superficie_Local.Value;
            if (this.IdTipoTramite == (int)Constantes.TipoTramite.AMPLIACION)
                txtSuperficie.Text = hid_Superficie_Total_Ampliar.Value;

            pnlResultadoBusquedaRubros.Style["display"] = "none";
            pnlBotonesAgregarRubros.Style["display"] = "none";
            pnlGrupoAgregarRubros.Style["display"] = "none";
            pnlBuscarRubros.Style["display"] = "block";
            pnlBotonesBuscarRubros.Style["display"] = "block";
            BotonesBuscarRubros.Style["display"] = "block";
            txtBuscar.Text = "";
            ValidadorAgregarRubros.Style["display"] = "none";
            txtBuscar.Focus();

            updBotonesBuscarRubros.Update();
            updBotonesAgregarRubros.Update();*/
        }
        private void BuscarRubros()
        {
            decimal dSuperficieDeclarada = 0;
            decimal.TryParse(txtSuperficie.Text, out dSuperficieDeclarada);

            RubrosCNBL rcnBL = new RubrosCNBL();
            var ds = rcnBL.GetRubros(txtBuscar.Text.Trim(), dSuperficieDeclarada);

            grdRubros.DataSource = ds.ToList();
            grdRubros.DataBind();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarRubros();
            pnlResultadoBusquedaRubros.Style["display"] = "block";
            pnlBotonesAgregarRubros.Style["display"] = "block";
            pnlGrupoAgregarRubros.Style["display"] = "block";
            pnlBuscarRubros.Style["display"] = "none";
        }
        protected void btnIngresarRubros_Click(object sender, EventArgs e)
        {
            decimal dSuperficie = 0;
            int CantRubrosElegidos = 0;
            decimal super = 0;

            try
            {
                SSITSolicitudesNuevasBL solbl = new SSITSolicitudesNuevasBL();
                RelRubrosCNSolicitudesNuevasBL relbl = new RelRubrosCNSolicitudesNuevasBL();

                var sol = solbl.Single(id_solicitud);

                List<string> lstcod_rubro = new List<string>();
                foreach (GridViewRow row in grdRubros.Rows)
                {
                    CheckBox chkRubroElegido = (CheckBox)row.FindControl("chkRubroElegido");

                    if (chkRubroElegido.Checked)
                    {
                        string scod_rubro = grdRubros.DataKeys[row.RowIndex].Values["Codigo"].ToString();
                        lstcod_rubro.Add(scod_rubro);
                    }
                }

                var solRubrosDTO = relbl.GetRByIdSolicitud(id_solicitud);
                lstcod_rubro.AddRange(solRubrosDTO.Select(s => s.codigo));


                foreach (GridViewRow row in grdRubros.Rows)
                {
                    CheckBox chkRubroElegido = (CheckBox)row.FindControl("chkRubroElegido");

                    if (chkRubroElegido.Checked)
                    {
                        string scod_rubro = grdRubros.DataKeys[row.RowIndex].Values["Codigo"].ToString();
                        string sdescripcion = grdRubros.DataKeys[row.RowIndex].Values["Nombre"].ToString();
                        decimal.TryParse(Convert.ToString(txtSuperficie.Text), out super);

                        RelRubrosSolicitudesNuevasDTO RelRubrosSolicitudesNuevasDTO = new RelRubrosSolicitudesNuevasDTO();
                        RelRubrosSolicitudesNuevasDTO.IdSolicitud = id_solicitud;
                        RelRubrosSolicitudesNuevasDTO.codigo = scod_rubro;
                        RelRubrosSolicitudesNuevasDTO.Descripcion = sdescripcion;
                        RelRubrosSolicitudesNuevasDTO.Superficie = super;
                        relbl.Insert(RelRubrosSolicitudesNuevasDTO);

                        CantRubrosElegidos++;
                    }
                }

                if (CantRubrosElegidos > 0)
                {
                    CargarDatos(id_solicitud);
                    updRubros.Update();
                    ScriptManager.RegisterClientScriptBlock(updBotonesAgregarRubros, updBotonesAgregarRubros.GetType(), "hidefrmAgregarRubros", "hidefrmAgregarRubros();", true);
                }
                else
                {
                    throw new Exception("Debe seleccionar los rubros/actividades que desea ingresar en la solicitud");
                }
            }
            catch (Exception ex)
            {
                /* LogError.Write(ex, ex.Message);
                 lblError.Text = ex.Message;
                 this.EjecutarScript(updBotonesAgregarRubros, "showfrmError();");*/
            }
        }
        protected void grdRubros_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }



        protected void grdRubros_DataBound(object sender, EventArgs e)
        {

            GridView grid = grdRubros;
            GridViewRow fila = (GridViewRow)grid.BottomPagerRow;
            if (fila != null)
            {
                Button btnAnterior = (Button)fila.Cells[0].FindControl("cmdAnterior");
                Button btnSiguiente = (Button)fila.Cells[0].FindControl("cmdSiguiente");

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
                    Button btn = (Button)fila.Cells[0].FindControl("cmdPage" + i.ToString());
                    btn.Visible = false;
                }


                if (grid.PageIndex == 0 || grid.PageCount <= 10)
                {
                    // Mostrar 10 botones o el máximo de páginas

                    for (int i = 1; i <= 10; i++)
                    {
                        if (i <= grid.PageCount)
                        {
                            Button btn = (Button)fila.Cells[0].FindControl("cmdPage" + i.ToString());
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

                    Button btnPage10 = (Button)fila.Cells[0].FindControl("cmdPage10");
                    btnPage10.Visible = true;
                    btnPage10.Text = Convert.ToString(grid.PageIndex + 1);

                    // Ubica los 9 botones hacia la izquierda
                    for (int i = grid.PageIndex - 1; i >= grid.PageIndex - 9; i--)
                    {
                        CantBucles++;
                        if (i >= 0)
                        {
                            Button btn = (Button)fila.Cells[0].FindControl("cmdPage" + Convert.ToString(10 - CantBucles));
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
                            Button btn = (Button)fila.Cells[0].FindControl("cmdPage" + Convert.ToString(10 + CantBucles));
                            btn.Visible = true;
                            btn.Text = Convert.ToString(i + 1);
                        }
                    }



                }

                //poner estilo sin seleccion a todos los botones
                Button cmdPage;
                string btnPage = "";
                for (int i = 1; i <= 19; i++)
                {
                    btnPage = "cmdPage" + i.ToString();
                    cmdPage = (Button)fila.Cells[0].FindControl(btnPage);
                    if (cmdPage != null)
                        cmdPage.CssClass = "btnPagerGrid";

                }


                // busca el boton por el texto para marcarlo como seleccionado
                string btnText = Convert.ToString(grid.PageIndex + 1);
                foreach (Control ctl in fila.Cells[0].FindControl("pnlpager").Controls)
                {
                    if (ctl is Button)
                    {
                        Button btn = (Button)ctl;
                        if (btn.Text.Equals(btnText))
                        {
                            btn.CssClass = "btnPagerGrid-selected";
                        }
                    }
                }

            }
        }
        protected void btnEliminarRubro_Click(object sender, EventArgs e)
        {
            try
            {
                SSITSolicitudesNuevasBL solbl = new SSITSolicitudesNuevasBL();
                var sol = solbl.Single(id_solicitud);

                if (sol.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.ETRA)
                {
                    RelRubrosCNSolicitudesNuevasBL rubl = new RelRubrosCNSolicitudesNuevasBL();
                    int id_relrub = int.Parse(hid_id_rubro_eliminar.Value);

                    rubl.Delete(new RelRubrosSolicitudesNuevasDTO()
                    {
                        idRelRubSol = id_relrub,
                        IdSolicitud = id_solicitud
                    });

                }

                CargarDatos(id_solicitud);
                ScriptManager.RegisterClientScriptBlock(updRubros, updRubros.GetType(), "hidefrmConfirmarEliminarRubro", "hidefrmConfirmarEliminarRubro();", true);
                updRubros.Update();
            }
            catch (Exception ex)
            {
                /* LogError.Write(ex, ex.Message);
                 lblError.Text = ex.Message;
                 this.EjecutarScript(updBotonesAgregarRubros, "showfrmError();");*/
            }
        }

        protected void cmdAnterior_Click(object sender, EventArgs e)
        {
            grdRubros.PageIndex = grdRubros.PageIndex - 1;
            BuscarRubros();

        }
        protected void cmdSiguiente_Click(object sender, EventArgs e)
        {
            grdRubros.PageIndex = grdRubros.PageIndex + 1;
            BuscarRubros();
        }
        protected void grdRubros_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdRubros.PageIndex = e.NewPageIndex;
            BuscarRubros();
        }

        protected void cmdPage(object sender, EventArgs e)
        {
            Button cmdPage = (Button)sender;

            grdRubros.PageIndex = int.Parse(cmdPage.Text) - 1;
            BuscarRubros();


        }

        protected void grdRubrosIngresados_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnEliminarRubro = (LinkButton)e.Row.FindControl("btnEliminarRubro");
                btnEliminarRubro.Visible = _enabled;

                e.Row.Cells[3].Visible = _enabled;
            }

            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[3].Visible = _enabled;
            }
        }
    }
}