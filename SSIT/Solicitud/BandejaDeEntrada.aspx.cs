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

namespace SSIT
{
    public partial class BandejaDeEntrada : SecurePage
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
        private int id_solicitud
        {
            get
            {
                int ret = 0;
                int.TryParse(hid_id_solicitud.Value, out ret);
                return ret;
            }
            set
            {
                hid_id_solicitud.Value = value.ToString();
            }

        }

        private string nro_expediente
        {
            get
            {
                string ret = "";
                ret = hid_nro_expediente.Value;
                return ret;
            }
            set
            {
                hid_nro_expediente.Value = value;
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {

            ScriptManager sm = ScriptManager.GetCurrent(this.Page);

            if (sm.IsInAsyncPostBack)
            {
                ScriptManager.RegisterStartupScript(updBuscar, updBuscar.GetType(), "init_JS_updBuscarUbicacion", "init_JS_updBuscarUbicacion();", true);

            }
            if (!IsPostBack)
            {
                CargarComboTiposDeTramite();
            }
        }

        private void CargarComboTiposDeTramite()
        {
            TipoTramiteBL ttBL = new TipoTramiteBL();

            List<int> lstIdTipoTramite = new List<int>();
            lstIdTipoTramite.Add((int)Constantes.TipoTramite.LIGUE);
            lstIdTipoTramite.Add((int)Constantes.TipoTramite.DESLIGUE);

            var lstTipoTramite = ttBL.getExcluirIdTipoTramite(lstIdTipoTramite).Where(x => x.Habilitado_SSIT == true).OrderBy(o => o.Orden).ToList();

            ddlTipoTramite.DataSource = lstTipoTramite;
            ddlTipoTramite.DataTextField = "DescripcionTipoTramite";
            ddlTipoTramite.DataValueField = "IdTipoTramite";
            ddlTipoTramite.DataBind();
            ddlTipoTramite.Items.Insert(0, new ListItem("(Todos)", "-1"));

            CargarComboEstados(-1);

        }

        private void CargarComboEstados(int id_tipotramite)
        {
            ddlEstados.Items.Clear();

            TipoEstadoSolicitudBL tEstBL = new TipoEstadoSolicitudBL();
            ConsultaPadronEstadosBL cpEstBL = new ConsultaPadronEstadosBL();

            TramitesBL trBL = new TramitesBL();
            Guid userid = (Guid)Membership.GetUser().ProviderUserKey;
            int totalRowCount = 0;

            var q = trBL.GetTramites(userid, id_solicitud, id_tipotramite, -1, nro_expediente, 0, 999999999, string.Empty, out totalRowCount).ToList();

            // Si se elige un tipo de tramite se cargan los estados.
            if (id_tipotramite >= 0)
            {
                if (id_tipotramite == (int)Constantes.TipoDeTramite.ConsultaPadron)
                {
                    List<ConsultaPadronEstadosDTO> lstCpadron = new List<ConsultaPadronEstadosDTO>();

                    ConsultaPadronEstadosDTO cPadronEstadosDTO = new ConsultaPadronEstadosDTO();
                    cPadronEstadosDTO.NomEstadoUsuario = "(Todos)";
                    cPadronEstadosDTO.IdEstado = -1;
                    lstCpadron.Add(cPadronEstadosDTO);

                    var lstCpadron2 = cpEstBL.GetAll().OrderBy(x => x.IdEstado).ToList();
                    lstCpadron.AddRange(lstCpadron2);


                    var valoresEstadosValidos = q.Select(x => x.IdEstado).ToList();

                    lstCpadron = lstCpadron.Where(x => valoresEstadosValidos.Contains(x.IdEstado)).ToList();

                    ddlEstados.DataSource = lstCpadron;
                    ddlEstados.DataTextField = "NomEstadoUsuario";
                    ddlEstados.DataValueField = "IdEstado";
                }
                else
                {
                    List<TipoEstadoSolicitudDTO> lstEstados = new List<TipoEstadoSolicitudDTO>();
                    var lstEstados2 = tEstBL.GetAll().OrderBy(x => x.Id).ToList();
                    TipoEstadoSolicitudDTO tesDTO = new TipoEstadoSolicitudDTO();
                    tesDTO.Descripcion = "(Todos)";
                    tesDTO.Id = -1;
                    lstEstados.Add(tesDTO);
                    lstEstados.AddRange(lstEstados2);

                    var valoresEstadosValidos = q.Select(x => x.IdEstado).ToList();

                    lstEstados = lstEstados.Where(x => valoresEstadosValidos.Contains(x.Id)).ToList();

                    ddlEstados.DataSource = lstEstados;
                    ddlEstados.DataTextField = "Descripcion";
                    ddlEstados.DataValueField = "Id";

                }
            }
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
            hid_id_solicitud.Value = txtNroSolicitud.Text.Trim();
            hid_ubicacion.Value = txtUbicacion.Text.Trim().ToUpper(); // se le pone mayuscula porque en la tabla esta en mayuscula
            hid_id_tipotramite.Value = ddlTipoTramite.SelectedValue;
            hid_id_estado.Value = ddlEstados.SelectedValue;
            hid_nro_expediente.Value = txtExpediente.Text.ToUpper();
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
            SSITSolicitudesBL solBL = new SSITSolicitudesBL();
            TransferenciasSolicitudesBL transfBL = new TransferenciasSolicitudesBL();
            ConsultaPadronSolicitudesBL cpadronBL = new ConsultaPadronSolicitudesBL();
            ParametrosBL paramBL = new ParametrosBL();
            
            List<ItemDirectionDTO> lstDireccionesSSIT = new List<ItemDirectionDTO>();
            List<ItemDirectionDTO> lstDireccionesTransf = new List<ItemDirectionDTO>();
            List<ItemDirectionDTO> lstDireccionesCPadron = new List<ItemDirectionDTO>();

            TramitesBL trBL = new TramitesBL();
            List<TramitesDTO> resultados = new List<TramitesDTO>();
            List<TramitesDTO> resultadosFiltradoUbic = new List<TramitesDTO>();
            Guid userid = (Guid)Membership.GetUser().ProviderUserKey;

            totalRowCount = 0;

            int id_solicitud = (hid_id_solicitud.Value.Length > 0 ? int.Parse(hid_id_solicitud.Value) : 0);
            int id_tipotramite = -1;    // Todos los tipos de trámite
            int id_estado = -1;        // Todos los estados

            int nroTrReferencia = 0;
            int.TryParse(paramBL.GetParametroChar("NroTransmisionReferencia"), out nroTrReferencia);

            int.TryParse(hid_id_tipotramite.Value, out id_tipotramite);
            if (!int.TryParse(hid_id_estado.Value, out id_estado))
            {
                id_estado = -1;
            }

            string nro_expediente = (hid_nro_expediente.Value.Length > 0 ? hid_nro_expediente.Value : "");
            // Consulta de Trámites 

            var q = trBL.GetTramites(userid, id_solicitud, id_tipotramite, id_estado, nro_expediente, startRowIndex, maximumRows, sortByExpression, out totalRowCount);

            resultados = q.ToList();

            if (resultados.Count > 0)
            {

                int[] arrTiposDeTramiteSSIT = new int[] {   (int)Constantes.TipoDeTramite.Transferencia, 
                                                            (int)Constantes.TipoDeTramite.ConsultaPadron };
                int[] arrTiposDeTramiteTransf = new int[] { (int)Constantes.TipoDeTramite.Transferencia };
                int[] arrTiposDeTramiteCPadron = new int[] { (int)Constantes.TipoDeTramite.ConsultaPadron };

                List<int> arrTramitesSSIT = resultados.Where(x => !arrTiposDeTramiteSSIT.Contains(x.IdTipoTramite)).Select(s => s.IdTramite).ToList();
                List<int> arrTramitesTransf = resultados.Where(x => arrTiposDeTramiteTransf.Contains(x.IdTipoTramite)).Select(s => s.IdTramite).ToList();
                List<int> arrTramitesCPadron = resultados.Where(x => arrTiposDeTramiteCPadron.Contains(x.IdTipoTramite)).Select(s => s.IdTramite).ToList();
                
                lstDireccionesSSIT = solBL.GetDireccionesSSIT(arrTramitesSSIT).ToList();
                lstDireccionesTransf = transfBL.GetDireccionesTransf(arrTramitesTransf).ToList();
                lstDireccionesCPadron = cpadronBL.GetDireccionesCpadron(arrTramitesCPadron).ToList();

                foreach (var row in resultados)
                {
                    ItemDirectionDTO itemDireccion = null;

                    if (arrTramitesSSIT.Contains(row.IdTramite) && !arrTiposDeTramiteSSIT.Contains(row.IdTipoTramite))
                    {
                        
                        itemDireccion = lstDireccionesSSIT.FirstOrDefault(x => x.id_solicitud == row.IdTramite);

                        if (itemDireccion != null)
                            row.Domicilio = (string.IsNullOrEmpty(itemDireccion.direccion) ? "" : itemDireccion.direccion);

                        if(row.IdTipoTramite == (int) Constantes.TipoDeTramite.Habilitacion)
                            row.Url = string.Format("~/" + RouteConfig.VISOR_SOLICITUD + "{0}", row.IdTramite);
                        else if (row.IdTipoTramite == (int)Constantes.TipoDeTramite.Ampliacion)
                            row.Url = string.Format("~/" + RouteConfig.VISOR_SOLICITUD_AMPLIACION + "{0}", row.IdTramite);
                        else if (row.IdTipoTramite == (int)Constantes.TipoDeTramite.RedistribucionDeUso)
                            row.Url = string.Format("~/" + RouteConfig.VISOR_SOLICITUD_REDISTRIBUCION_USO + "{0}", row.IdTramite);
                        else if (row.IdTipoTramite == (int)Constantes.TipoDeTramite.Permisos)
                            row.Url = string.Format("~/" + RouteConfig.VISOR_SOLICITUD_PERMISO_MC + "{0}", row.IdTramite);
                    }

                    if (arrTramitesTransf.Contains(row.IdTramite) && arrTiposDeTramiteTransf.Contains(row.IdTipoTramite))
                    {
                        
                        itemDireccion = lstDireccionesTransf.FirstOrDefault(x => x.id_solicitud == row.IdTramite);

                        if (itemDireccion != null)
                            row.Domicilio = (string.IsNullOrEmpty(itemDireccion.direccion) ? "" : itemDireccion.direccion);

                        if(row.IdTramite > nroTrReferencia)                        
                            row.Url = string.Format("~/" + RouteConfig.VISOR_TRANSMISIONES + "{0}", row.IdTramite);
                        else
                            row.Url = string.Format("~/" + RouteConfig.VISOR_TRANSFERENCIAS + "{0}", row.IdTramite);
                    }

                    if (arrTramitesCPadron.Contains(row.IdTramite) && arrTiposDeTramiteCPadron.Contains(row.IdTipoTramite))
                    {
                        
                        itemDireccion = lstDireccionesCPadron.FirstOrDefault(x => x.id_solicitud == row.IdTramite);

                        if (itemDireccion != null)
                            row.Domicilio = (string.IsNullOrEmpty(itemDireccion.direccion) ? "" : itemDireccion.direccion);

                        row.Url = string.Format("~/" + RouteConfig.VISOR_CPADRON + "{0}", row.IdTramite);
                    }
                }
            }
            if (!String.IsNullOrWhiteSpace(hid_ubicacion.Value))
            {
                resultadosFiltradoUbic = resultados.Where(x => x.Domicilio.Contains(hid_ubicacion.Value)).ToList();
                totalRowCount = resultadosFiltradoUbic.Count();
                return resultadosFiltradoUbic;
            }
            else
            {
                return resultados;
            }
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
            txtNroSolicitud.Text = "";
            Buscar();
        }

        protected void ddlTipoTramite_SelectedIndexChanged(object sender, EventArgs e)
        {

            int id_tipotramite = 0;
            int.TryParse(ddlTipoTramite.SelectedValue, out id_tipotramite);
            CargarComboEstados(id_tipotramite);

        }


    }
}