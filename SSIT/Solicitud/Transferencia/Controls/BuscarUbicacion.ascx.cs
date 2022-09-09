using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using SSIT.Solicitud.Consulta_Padron.Controls;
using System.Web.UI.WebControls;
using BusinesLayer.Implementation;
using DataTransferObject;
using SSIT.App_Components;
using SSIT.Common;
using System.Web.Security;
using StaticClass;

namespace SSIT.Solicitud.Transferencia.Controls
{
    public class ucPuerta
    {
        public int codigo_calle { get; set; }
        public int NroPuerta { get; set; }
    }
    public class ucAgregarUbicacionEventsArgs : EventArgs
    {
        public int id_ubicacion { get; set; }
        public Nullable<int> id_subtipoubicacion { get; set; }
        public string local_subtipoubicacion { get; set; }
        public string vDeptoLocalOtros { get; set; }
        public string vDepto { get; set; }
        public string vLocal { get; set; }
        public string vTorre { get; set; }
        public List<int> ids_propiedades_horizontales = new List<int>();
        public List<ucPuerta> Puertas = new List<ucPuerta>();
        public UpdatePanel upd { get; set; }
        public bool Cancel { get; set; }    // se utilizar para saber si se cancelo o no luego del llamado.
    }
    public partial class BuscarUbicacion : System.Web.UI.UserControl
    {
        public int idUbicacion { get; set; }
        private static List<UbicacionesDTO> result = new List<UbicacionesDTO>();
        private static string _OnCerrarClick = "";

        public delegate void EventHandlerCerrar(object sender, EventArgs e);
        public event EventHandlerCerrar CerrarClick;

        public delegate void EventHandlerAgregarUbicacion(object sender, ucAgregarUbicacionEventsArgs e);
        public event EventHandlerAgregarUbicacion AgregarUbicacionClick;

        TransferenciasUbicacionesPuertasBL transfUbicacionesPuertasBL = null;
        List<TransferenciasUbicacionesPuertasDTO> lstPuertasTransfDTO = null;
        TransferenciasUbicacionesPropiedadHorizontalBL transfUbicacionPropiedadHorizontalBL = null;
        List<TransferenciasUbicacionPropiedadHorizontalDTO> lstPropHorTransfDTO = null;

        TransferenciaUbicacionesBL transfUbicBL = null;
        TransferenciaUbicacionesDTO transfUbicDTO = null;

        protected void btnCerrar_Click(object sender, EventArgs e)
        {

            if (CerrarClick != null)
                CerrarClick(sender, e);
            ScriptManager.RegisterStartupScript(updBuscarUbicacion, updBuscarUbicacion.GetType(), "hideZonaPlaneamiento", "hideZonaPlaneamiento();", true);
            Inicilizar_Control();

            if (!string.IsNullOrEmpty(_OnCerrarClick))
            {
                ((BasePage)this.Page).EjecutarScript(updBuscarUbicacion, OnCerrarClientClick);
            }
            updBuscarUbicacion.Update();
        }

        public string OnCerrarClientClick
        {
            get
            {
                return _OnCerrarClick;
            }
            set
            {
                _OnCerrarClick = value;
            }
        }
        bool _editar = false;
        public bool Edicion
        {
            get { return _editar; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager sm = ScriptManager.GetCurrent(this.Page);

            if (sm.IsInAsyncPostBack)
            {
                ScriptManager.RegisterStartupScript(updBuscarUbicacion, updBuscarUbicacion.GetType(), "init_JS_updBuscarUbicacion", "init_JS_updBuscarUbicacion();", true);

            }
            if (!IsPostBack)
            {
                CargarCalles();
                CargarComboTipoUbicacion();
            }

        }
        private int IdSolicitud
        {
            get
            {
                int ret = 0;
                int.TryParse(Page.RouteData.Values["id_Solicitud"].ToString(), out ret);
                return ret;
            }
        }
        private void Inicilizar_Control()
        {
            ScriptManager.RegisterStartupScript(updBuscarUbicacion, updBuscarUbicacion.GetType(), "hideZonaPlaneamiento", "hideZonaPlaneamiento();", true);
            pnlGridResultados.Visible = false;
            pnlContentBuscar.Visible = true;
            gridubicacion.PageIndex = 0;
            txtSeccion.Text = "";
            txtManzana.Text = "";
            txtParcela.Text = "";
            ddlSubTipoUbicacion.ClearSelection();
            ddlTipoDeUbicacion.ClearSelection();
            optTipoPartidaMatriz.Checked = true;
            txtNroPartida.Text = "";
            ddlCalles.ClearSelection();
            txtNroPuerta.Text = "";
            txtDescUbicacion.Text = "";
            pnlResultados.Update();
            hid_tabselected.Value = "";
            pnlValidacionIngresoUbicacion.Style["display"] = "none";
        }
        public void editar()
        {
            if (this.idUbicacion != 0)
            {
                int transfUbicacion = this.idUbicacion;

                _editar = true;

                TransferenciaUbicacionesBL transfUbicacionesBL = new TransferenciaUbicacionesBL();
                var dto = transfUbicacionesBL.Single(transfUbicacion);
                UbicacionesBL ubicacionesBL = new UbicacionesBL();
                if (dto != null)
                    result = new List<UbicacionesDTO>() { ubicacionesBL.Single(dto.IdUbicacion.Value) };

                gridubicacion.DataSource = result;
                gridubicacion.DataBind();
                pnlResultados.Update();
                pnlContentBuscar.Visible = false;
                pnlGridResultados.Visible = true;

                switch (result.Count)
                {
                    case 0:
                        lblCantResultados.Text = "";
                        break;
                    case 1:
                        lblCantResultados.Text = "( " + result.Count + " resultado )";
                        break;
                    default:
                        lblCantResultados.Text = "( " + result.Count + " resultados )";
                        break;
                }
                int idubicacion = result.FirstOrDefault().IdUbicacion;
                if (ubicacionesBL.validarUbicacionClausuras(idubicacion) || ubicacionesBL.validarUbicacionInhibiciones(idubicacion))
                {
                    btnEditarUbicacion.CssClass = "btn btn-default hide";
                    ScriptManager.RegisterStartupScript(pnlResultados, pnlResultados.GetType(), "deshabilitarPorInhibiciondeUbicacion", "deshabilitarPorInhibiciondeUbicacion()", true);
                    btnNuevaBusqueda.CssClass = "btn btn-primary";
                    gridubicacion.Enabled = false;
                    btnEditarUbicacion.Enabled = false;

                }
                else
                {
                    btnEditarUbicacion.CssClass = "btn btn-primary";
                    btnEditarUbicacion.Visible = (result.Count > 0 && _editar);
                    btnEditarUbicacion.Enabled = true;
                    gridubicacion.Enabled = true;
                    ScriptManager.RegisterStartupScript(pnlResultados, pnlResultados.GetType(), "habilitarPorNoInhibiciondeUbicacion", "habilitarPorNoInhibiciondeUbicacion()", true);
                    btnNuevaBusqueda.CssClass = "btn btn-default";

                }

                btnEditarSinPartida.Visible = _editar;
                btnGuardarSinPartida.Visible = false;
                btnIngresarUbicacion.Visible = false;
                udpModalSinPartida.Update();
                updBuscarUbicacion.Update();

            }

        }


        protected void btnEditarUbicacion_Click(object sender, EventArgs e)
        {
            int idubicacion = (int)gridubicacion.DataKeys[gridubicacion.Rows.Count - 1].Value;

            TransferenciaUbicacionesBL transfUbicacionBL = new TransferenciaUbicacionesBL();
            var result = transfUbicacionBL.GetByFKIdSolicitud(IdSolicitud).Where(x => x.IdUbicacion == idubicacion).FirstOrDefault();
            transfUbicacionBL.Delete(result);
            btnIngresarUbicacion_Click(sender, e);
            ScriptManager.RegisterStartupScript(udpModalSinPartida, udpModalSinPartida.GetType(), "hidefrmConfirmarNoPHFin", "hidefrmConfirmarNoPHFin()", true);
            updBuscarUbicacion.Update();
        }

        private void CargarCalles()
        {
            CallesBL calles = new CallesBL();

            var lstCalles = calles.GetCalles();

            ddlCalles.DataSource = lstCalles.ToList();
            ddlCalles.DataTextField = "NombreOficial_calle";
            ddlCalles.DataValueField = "Codigo_calle";
            ddlCalles.DataBind();

            ddlCalles.Items.Insert(0, "");
        }

        protected void btnBuscar1_Click(object sender, EventArgs e)
        {
            BuscarUbicaciones(1);
        }
        protected void btnBuscar2_Click(object sender, EventArgs e)
        {
            BuscarUbicaciones(2);
        }
        protected void btnBuscar3_Click(object sender, EventArgs e)
        {
            BuscarUbicaciones(3);
        }
        protected void btnBuscar4_Click(object sender, EventArgs e)
        {
            BuscarUbicaciones(4);
        }

        private void BuscarUbicaciones(int nrotab)
        {

            //Limpia y oculta los errores
            lstValidacionesUbicacion.Items.Clear();
            pnlValidacionIngresoUbicacion.Style["display"] = "none";
            UbicacionesBL ubicacionesBL = new UbicacionesBL();
            DateTime FechaActual = DateTime.Now;

            switch (nrotab)
            {
                case 1:
                    int vNroPartida = int.Parse(txtNroPartida.Text.Trim());
                    result = ubicacionesBL.Buscar(vNroPartida, optTipoPartidaHorizontal.Checked, FechaActual);
                    break;
                case 2:
                    int vNroPuerta = int.Parse(txtNroPuerta.Text.Trim());
                    int vcodigo_calle = int.Parse(ddlCalles.SelectedValue);

                    result = ubicacionesBL.Buscar(vNroPuerta, vcodigo_calle, FechaActual);
                    break;
                case 3:
                    int vSeccion = int.Parse(txtSeccion.Text);
                    string vManzana = txtManzana.Text.Trim();
                    string vParcela = txtParcela.Text.Trim();
                    result = ubicacionesBL.Buscar(vSeccion, vManzana, vParcela, FechaActual);
                    break;
                case 4:
                    int vid_subtipoubicacion = int.Parse(ddlSubTipoUbicacion.SelectedValue);
                    result = ubicacionesBL.Buscar(vid_subtipoubicacion, FechaActual);
                    break;
            }
            pnlContentBuscar.Visible = false;
            pnlGridResultados.Visible = true;
            if (result.Count == 0)
            {
                footBtnContinuarAviso.Style["display"] = "inline";
                pnlContentBuscar.Visible = false;
                pnlAviso.Visible = true;
                pnlContentBuscar.Visible = false;
                pnlGridResultados.Visible = false;
                updBuscarUbicacion.Update();
                updpnlmessages.Update();
            }
            else
            {
                if (result.Count > 1)
                {
                    footBtnContinuarAviso.Style["display"] = "none";
                    pnlAviso.Visible = true;
                    lblCantResultados.Visible = true;

                    grdResultados.DataSource = result.Select(p => p.Direccion).ToList();
                    grdResultados.DataBind();

                    btnIngresarUbicacion.Visible = false;
                    //btnSeleccionarUbicacion.Visible = true;

                    gridubicacion.Visible = false;
                    grdResultados.Visible = true;
                    pnlContentBuscar.Visible = false;
                    updBuscarUbicacion.Update();
                    updpnlmessages.Update();
                }
                else
                {
                    pnlAviso.Visible = false;
                    grdResultados.Visible = false;
                    gridubicacion.Visible = true;
                    gridubicacion.DataSource = result;
                    gridubicacion.DataBind();
                    pnlResultados.Update();

                    switch (result.Count)
                    {
                        case 0:
                            lblCantResultados.Text = "";
                            break;
                        case 1:
                            lblCantResultados.Text = "( " + result.Count + " resultado )";
                            break;
                        default:
                            lblCantResultados.Text = "( " + result.Count + " resultados )";
                            break;
                    }
                    int idubicacion = result.FirstOrDefault().IdUbicacion;
                    if (ubicacionesBL.validarUbicacionClausuras(idubicacion) || ubicacionesBL.validarUbicacionInhibiciones(idubicacion))
                    {
                        btnIngresarUbicacion.CssClass = "btn btn-default hide";
                        ScriptManager.RegisterStartupScript(pnlResultados, pnlResultados.GetType(), "deshabilitarPorInhibiciondeUbicacion", "deshabilitarPorInhibiciondeUbicacion()", true);
                        btnEditarUbicacion.Visible = false;
                        btnNuevaBusqueda.CssClass = "btn btn-primary";
                        gridubicacion.Enabled = false;

                    }
                    else
                    {
                        btnIngresarUbicacion.Visible = (result.Count > 0);
                        ScriptManager.RegisterStartupScript(pnlResultados, pnlResultados.GetType(), "habilitarPorNoInhibiciondeUbicacion", "habilitarPorNoInhibiciondeUbicacion()", true);
                        btnNuevaBusqueda.CssClass = "btn btn-default";
                        btnIngresarUbicacion.CssClass = "btn btn-primary";
                        gridubicacion.Enabled = true;
                    }
                    btnEditarUbicacion.Visible = false;
                    btnEditarSinPartida.Visible = false;
                    btnGuardarSinPartida.Visible = true;

                    udpModalSinPartida.Update();
                    updBuscarUbicacion.Update();
                }
            }

        }
        protected void lnkUbicacion_Click(object sender, EventArgs e)
        {
            int id_ubic = 0;

            LinkButton lnkubicacion = (LinkButton)sender;
            int.TryParse(lnkubicacion.CommandArgument, out id_ubic);


            UbicacionesBL ubicacionesBL = new UbicacionesBL();
            UbicacionesDTO ubic = ubicacionesBL.Single(id_ubic);
            List<UbicacionesDTO> result = new List<UbicacionesDTO>() { ubic };

            if (ubicacionesBL.validarUbicacionClausuras(id_ubic) || ubicacionesBL.validarUbicacionInhibiciones(id_ubic))
            {
                btnIngresarUbicacion.CssClass = "btn btn-default hide";
                ScriptManager.RegisterStartupScript(pnlResultados, pnlResultados.GetType(), "deshabilitarPorInhibiciondeUbicacion", "deshabilitarPorInhibiciondeUbicacion()", true);
                btnEditarUbicacion.Visible = false;
                btnNuevaBusqueda.CssClass = "btn btn-primary";
                gridubicacion.Enabled = false;

            }
            else
            {
                btnIngresarUbicacion.Visible = true;
                ScriptManager.RegisterStartupScript(pnlResultados, pnlResultados.GetType(), "habilitarPorNoInhibiciondeUbicacion", "habilitarPorNoInhibiciondeUbicacion()", true);
                btnNuevaBusqueda.CssClass = "btn btn-default";
                btnIngresarUbicacion.CssClass = "btn btn-primary";
                gridubicacion.Enabled = true;
            }
            pnlAviso.Visible = false;
            gridubicacion.DataSource = result;
            gridubicacion.DataBind();
            pnlResultados.Update();
            pnlContentBuscar.Visible = false;
            pnlGridResultados.Visible = true;

            lblCantResultados.Text = "( " + 1 + " resultados )";

            btnEditarUbicacion.Visible = false;
            btnEditarSinPartida.Visible = false;
            btnGuardarSinPartida.Visible = true;
            gridubicacion.Visible = true;
            grdResultados.Visible = false;
            udpModalSinPartida.Update();
            updBuscarUbicacion.Update();

        }
        protected void btnContinuarAviso_Click(object sender, EventArgs e)
        {
            pnlAviso.Visible = false;
            pnlContentBuscar.Visible = true;
            updBuscarUbicacion.Update();

        }
        protected void btnRegistro_Click(object sender, EventArgs e)
        {
            footBtnContinuarAviso.Style["display"] = "none";
            pnlGridResultados.Visible = false;
            btnRegistro.Visible = false;
            divRegistro.Style["display"] = "inline";
            divAtencion.Style["display"] = "none";
            pnlResultados.Update();
            updBuscarUbicacion.Update();
            updpnlmessages.Update();
        }

        protected void btnRegistroSalir_Click(object sender, EventArgs e)
        {

            btnRegistro.Visible = true;
            divRegistro.Style["display"] = "none";
            divAtencion.Style["display"] = "inline";
            pnlAviso.Visible = false;
            pnlContentBuscar.Visible = true;
            updpnlmessages.Update();
            updBuscarUbicacion.Update();
        }

        protected void grdResultados_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView grdresdireccion = (GridView)e.Row.FindControl("grdresdireccion");


                ItemDirectionDTO itemUbicacion = (ItemDirectionDTO)e.Row.DataItem;

                Image imgFoto = (Image)e.Row.FindControl("imgFoto");

                if (itemUbicacion.Seccion.HasValue && itemUbicacion.Seccion > 0)
                    imgFoto.ImageUrl = Functions.GetUrlFoto(itemUbicacion.Seccion.Value, itemUbicacion.Manzana, itemUbicacion.Parcela, 400, 400);
                else
                    imgFoto.ImageUrl = ResolveClientUrl("~/Content/img/app/ImageNotFound100x100.png");

            }
        }
        protected void btnNuevaBusquedar_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(updBuscarUbicacion, updBuscarUbicacion.GetType(), "hideZonaPlaneamiento", "hideZonaPlaneamiento();", true);
            txtNroPartida.Text = "";
            hid_tabselected.Value = "";
            pnlGridResultados.Visible = false;
            pnlContentBuscar.Visible = true;
            gridubicacion.PageIndex = 0;
            pnlResultados.Update();
            updBuscarUbicacion.Update();
        }
        protected void gridubicacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridubicacion.PageIndex = e.NewPageIndex;
            gridubicacion.DataSource = result;
            gridubicacion.DataBind();
            pnlResultados.Update();

        }


        protected void cmdPage(object sender, EventArgs e)
        {
            Button cmdPage = (Button)sender;
            gridubicacion.PageIndex = int.Parse(cmdPage.Text) - 1;
            gridubicacion.DataSource = result;
            gridubicacion.DataBind();
            pnlResultados.Update();

        }

        protected void cmdAnterior_Click(object sender, EventArgs e)
        {
            gridubicacion.PageIndex = gridubicacion.PageIndex - 1;

            gridubicacion.DataSource = result;
            gridubicacion.DataBind();
            pnlResultados.Update();

        }
        protected void cmdSiguiente_Click(object sender, EventArgs e)
        {
            gridubicacion.PageIndex = gridubicacion.PageIndex + 1;
            gridubicacion.DataSource = result;
            gridubicacion.DataBind();
            pnlResultados.Update();
        }

        protected void gridubicacion_DataBound(object sender, EventArgs e)
        {
            GridView grid = gridubicacion;
            int idubicacion = (int)gridubicacion.DataKeys[gridubicacion.Rows.Count - 1].Value;
            this.ZonaPlaneamiento.mostrarPlaneamiento(idubicacion);
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
                Button cmdPage;
                string btnPage = "";
                for (int i = 1; i <= 19; i++)
                {
                    btnPage = "cmdPage" + i.ToString();
                    cmdPage = (Button)fila.Cells[0].FindControl(btnPage);
                    if (cmdPage != null)
                        cmdPage.CssClass = "btn btn-default";

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
                            btn.CssClass = "btn btn-info";
                        }
                    }
                }

            }
        }

        protected void gridubicacion_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hid_id_ubicacion = (HiddenField)e.Row.FindControl("hid_id_ubicacion");
                DataList lstPuertas = (DataList)e.Row.FindControl("lstPuertas");
                CheckBoxList optPartidasHorizontales = (CheckBoxList)e.Row.FindControl("CheckBoxListPHorizontales");
                Panel pnlPartidasHorizontales = (Panel)e.Row.FindControl("pnlPartidasHorizontales");

                int id_ubicacion = int.Parse(hid_id_ubicacion.Value);

                UbicacionesPuertasBL puertasBL = new UbicacionesPuertasBL();
                var puertas = puertasBL.GetByFKIdUbicacion(id_ubicacion);

                lstPuertas.DataSource = puertas.ToList();
                lstPuertas.DataBind();

                if (hid_tabselected.Value == "3")
                {
                    Panel pnlTipoUbicacion = (Panel)e.Row.FindControl("pnlTipoUbicacion");
                    Panel pnlSMP = (Panel)e.Row.FindControl("pnlSMP");
                    Label lblTipoUbicacion = (Label)e.Row.FindControl("lblTipoUbicacion");
                    Label lblSubTipoUbicacion = (Label)e.Row.FindControl("lblSubTipoUbicacion");
                    Label lblLocal = (Label)e.Row.FindControl("lblLocal");

                    TiposDeUbicacionBL tipoUbicacion = new TiposDeUbicacionBL();
                    var dataEsp = tipoUbicacion.Get(id_ubicacion);
                    if (dataEsp != null)
                    {
                        lblTipoUbicacion.Text = dataEsp.DescripcionTipoUbicacion;
                        //lblSubTipoUbicacion.Text = dataEsp.descripcion_subtipoubicacion;
                        lblLocal.Text = txtDescUbicacion.Text;
                    }

                    pnlTipoUbicacion.Visible = true;
                    pnlSMP.Visible = false;

                }

                UbicacionesPropiedadhorizontalBL propiedadBL = new UbicacionesPropiedadhorizontalBL();
                var lstPartidasHorizontales = propiedadBL.GetByFKIdUbicacion(id_ubicacion);

                // Partidas Horizontales
                optPartidasHorizontales.DataValueField = "IdPropiedadHorizontal";
                optPartidasHorizontales.DataTextField = "DescripcionCompleta";
                optPartidasHorizontales.DataSource = lstPartidasHorizontales;
                optPartidasHorizontales.DataBind();

                pnlPartidasHorizontales.Style["display"] = (optPartidasHorizontales.Items.Count > 0 ? "block" : "none");

                if (_editar && this.idUbicacion != 0)
                {
                    TextBox txtTorre = (TextBox)e.Row.FindControl("txtTorre");
                    TextBox txtDepto = (TextBox)e.Row.FindControl("txtDepto");
                    TextBox txtLocal = (TextBox)e.Row.FindControl("txtLocal");
                    TextBox txtOtros = (TextBox)e.Row.FindControl("txtOtros");


                    if (transfUbicBL == null)
                        transfUbicBL = new TransferenciaUbicacionesBL();

                    if (transfUbicDTO == null)
                        transfUbicDTO = transfUbicBL.Single(idUbicacion);

                    string Otros = transfUbicDTO.DeptoLocalTransferenciaUbicacion ?? "";
                    string Local = transfUbicDTO.Local ?? "";
                    string Depto = transfUbicDTO.Depto ?? "";
                    string Torre = transfUbicDTO.Torre ?? "";

                    txtOtros.Text = Otros.Trim();
                    txtLocal.Text = Local.Trim();
                    txtDepto.Text = Depto.Trim();
                    txtTorre.Text = Torre.Trim();
                }

            }


        }

        public string GetUrlMapa(int? seccion, string manzana, string parcela, ItemDirectionDTO dir)
        {
            if (seccion.HasValue && dir != null)
            {
                string[] numero = dir.Numero.Split('/');
                return Functions.GetUrlMapa(seccion.Value, manzana, parcela, dir.direccion.ToString() + " " + numero.FirstOrDefault().ToString());
            }
            else
                return Functions.ImageNotFound(this.Page);

        }

        public string GetUrlCroquis(int? seccion, string manzana, string parcela, ItemDirectionDTO dir)
        {
            if (seccion.HasValue && dir != null)
            {
                string[] numero = dir.Numero.Split('/');
                return Functions.GetUrlCroquis(seccion.Value.ToString(), manzana, parcela, dir.direccion.ToString() + " " + numero.FirstOrDefault().ToString());
            }
            else
                return Functions.ImageNotFound(this.Page);

        }
        //protected void lnkAgregarOtraPuerta_Click(object sender, EventArgs e)
        //{

        //    LinkButton lnk = (LinkButton)sender;
        //    int id_ubic_puerta_origen = int.Parse(lnk.CommandArgument);
        //    DataList lstPuertas = (DataList)lnk.Parent.Parent;
        //    List<UbicacionesPuertasDTO> puertas = new List<UbicacionesPuertasDTO>();
        //    UbicacionesPuertasDTO puerta = new UbicacionesPuertasDTO();
        //    UbicacionesPuertasBL pBL = new UbicacionesPuertasBL();
        //    List<bool> puertasTildadas = new List<bool>();
        //    List<int> NroPuertasModificadas = new List<int>();

        //    foreach (DataListItem item in lstPuertas.Items)
        //    {
        //        HiddenField hid_ubic_puerta = (HiddenField)item.FindControl("hid_ubic_puerta");
        //        CheckBox chkPuerta = (CheckBox)item.FindControl("chkPuerta");
        //        TextBox txtNroPuerta = (TextBox)item.FindControl("txtNroPuerta");
        //        Label lblnombreCalle = (Label)item.FindControl("lblnombreCalle");
        //        int NroPuerta = 0;
        //        int.TryParse(txtNroPuerta.Text.Trim(), out NroPuerta);

        //        puertasTildadas.Add(chkPuerta.Checked);
        //        NroPuertasModificadas.Add(NroPuerta);


        //        HiddenField hid_Nueva_puerta = (HiddenField)item.FindControl("hid_Nueva_puerta");
        //        bool Nueva_puerta;
        //        bool.TryParse(hid_Nueva_puerta.Value, out Nueva_puerta);

        //        puerta = pBL.Single(Convert.ToInt32(hid_ubic_puerta.Value));

        //        if (Nueva_puerta)
        //            puerta.NuevaPuerta = true;
        //        puertas.Add(puerta);
        //    }

        //    puerta = pBL.Single(id_ubic_puerta_origen);
        //    puerta.NuevaPuerta = true;
        //    puertas.Add(puerta);
        //    NroPuertasModificadas.Add(puerta.NroPuertaUbic);
        //    puertasTildadas.Add(false);

        //    lstPuertas.DataSource = puertas;
        //    lstPuertas.DataBind();

        //    foreach (DataListItem item in lstPuertas.Items)
        //    {
        //        CheckBox chkPuerta = (CheckBox)item.FindControl("chkPuerta");
        //        TextBox txtNroPuerta = (TextBox)item.FindControl("txtNroPuerta");

        //        chkPuerta.Checked = puertasTildadas[item.ItemIndex];

        //        HiddenField hid_Nueva_puerta = (HiddenField)item.FindControl("hid_Nueva_puerta");
        //        bool Nueva_puerta;
        //        bool.TryParse(hid_Nueva_puerta.Value, out Nueva_puerta);

        //        txtNroPuerta.Text = NroPuertasModificadas[item.ItemIndex].ToString();
        //        if (Nueva_puerta)
        //            txtNroPuerta.Enabled = true;
        //    }
        //}

        // Devuelve true si hay puertas tildadas en la ubicacion
        private bool TildoPuertas(GridViewRow row)
        {
            bool ret = false;

            DataList lstPuertas = (DataList)row.FindControl("lstPuertas");

            foreach (DataListItem item in lstPuertas.Items)
            {
                CheckBox chkPuerta = (CheckBox)item.FindControl("chkPuerta");
                if (chkPuerta.Checked)
                {
                    ret = true;
                    break;
                }
            }

            return ret;

        }
        protected void ddlTipoDeUbicacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id_tipoubicacion = int.Parse(ddlTipoDeUbicacion.SelectedValue);
            if (id_tipoubicacion == 1) //Si es subte desabilito la validacion del local
                ReqtxtDescUbicacion.Enabled = false;
            else
                ReqtxtDescUbicacion.Enabled = true;

            CargarComboSubTipoUbicacion(id_tipoubicacion);
        }
        private void CargarComboTipoUbicacion()
        {

            TiposDeUbicacionBL tipo = new TiposDeUbicacionBL();
            var query = tipo.GetTiposDeUbicacionExcluir((int)Constantes.TiposDeUbicacion.ParcelaComun);

            ddlTipoDeUbicacion.DataTextField = "DescripcionTipoUbicacion";
            ddlTipoDeUbicacion.DataValueField = "IdTipoUbicacion";
            ddlTipoDeUbicacion.DataSource = query.ToList();
            ddlTipoDeUbicacion.DataBind();

            int id_tipoubicacion = 0;
            if (int.TryParse(ddlTipoDeUbicacion.SelectedValue, out id_tipoubicacion))
                CargarComboSubTipoUbicacion(id_tipoubicacion);

        }

        private void CargarComboSubTipoUbicacion(int id_tipoubicacion)
        {

            SubTipoUbicacionesBL subtipo = new SubTipoUbicacionesBL();
            var query = subtipo.Get(id_tipoubicacion);

            ddlSubTipoUbicacion.DataTextField = "descripcion_subtipoubicacion";
            ddlSubTipoUbicacion.DataValueField = "id_subtipoubicacion";
            ddlSubTipoUbicacion.DataSource = query.ToList();
            ddlSubTipoUbicacion.DataBind();

            updBuscarUbicacioneEspeciales.Update();
            //hid_tabselected.Value = "3";
        }

        protected void updPuertas_Load(object sender, EventArgs e)
        {
            UpdatePanel updPuertas = (UpdatePanel)sender;
            ((BasePage)this.Page).EjecutarScript(updPuertas, "init_JS_updPuertas();");
        }

        // realiza las validaciones necesarias sobre las puertas tildadas e informa los errores en la lista pasada por referencia
        private void ValidarPuertas(GridViewRow row, ref BulletedList lst)
        {
            List<int[]> puertasSeleccionadas = new List<int[]>();
            DataList lstPuertas = (DataList)row.FindControl("lstPuertas");

            foreach (DataListItem item in lstPuertas.Items)
            {
                CheckBox chkPuerta = (CheckBox)item.FindControl("chkPuerta");
                if (chkPuerta.Checked)
                {
                    int NroRegistro = item.ItemIndex + 1;
                    HiddenField hid_codigo_calle = (HiddenField)item.FindControl("hid_codigo_calle");
                    HiddenField hid_NroPuerta_ubic = (HiddenField)item.FindControl("hid_NroPuerta_ubic"); //Número de Puerta registro de base de datos (original)
                    TextBox txtNroPuerta = (TextBox)item.FindControl("txtNroPuerta");
                    Label lblnombreCalle = (Label)item.FindControl("lblnombreCalle");

                    string strPuerta = lblnombreCalle.Text;
                    int codigo_calle = 0, NroPuerta = 0, NroPuertaOriginal = 0, minvaluePuerta = 0, maxvaluePuerta = 0;

                    int.TryParse(hid_codigo_calle.Value, out codigo_calle);
                    int.TryParse(hid_NroPuerta_ubic.Value, out NroPuertaOriginal);
                    int.TryParse(txtNroPuerta.Text, out NroPuerta);


                    if (NroPuertaOriginal % 100 == 0)
                    {
                        minvaluePuerta = NroPuertaOriginal - 99;
                        maxvaluePuerta = NroPuertaOriginal;
                    }
                    else
                    {
                        minvaluePuerta = Convert.ToInt32(Math.Floor(Convert.ToDecimal(NroPuertaOriginal / 100)) * 100 + 1);
                        maxvaluePuerta = Convert.ToInt32(Math.Floor(Convert.ToDecimal(NroPuertaOriginal / 100 + 1)) * 100);
                    }

                    if (NroPuerta < minvaluePuerta || NroPuerta > maxvaluePuerta)
                    {

                        lst.Items.Add(string.Format("En la puerta Nº {0} '{1}', la numeración debe encontrarse entre {2} y {3}.",
                                NroRegistro, lblnombreCalle.Text + " " + txtNroPuerta.Text.Trim(), minvaluePuerta, maxvaluePuerta));

                    }
                    else if (NroPuerta % 2 != NroPuertaOriginal % 2)
                    {
                        string strParImpar = "";

                        if (NroPuertaOriginal % 2 == 0)
                            strParImpar = "par";
                        else
                            strParImpar = "impar";


                        lst.Items.Add(
                                string.Format("En la puerta Nº {0} '{1}', la numeración debe ser un Nº {2} según el lado de la calle elegido.",
                                NroRegistro, lblnombreCalle.Text + " " + txtNroPuerta.Text, strParImpar
                                ));

                    }
                    else
                    {
                        if (puertasSeleccionadas.Find(x => x[0] == codigo_calle && x[1] == NroPuerta) == null)
                            puertasSeleccionadas.Add(new int[2] { codigo_calle, NroPuerta });
                        else
                            lst.Items.Add(
                                string.Format("Existe más de 1 puerta con la misma numeración en la misma calle, quite el tilde a la puerta Nº {0} '{1}'.",
                                NroRegistro, lblnombreCalle.Text + " " + txtNroPuerta.Text
                                ));

                    }
                }
            }
        }

        protected void btnIngresarUbicacion_Click(object sender, EventArgs e)
        {
            lstValidacionesUbicacion.Items.Clear();
            pnlValidacionIngresoUbicacion.Style["display"] = "none";

            GridViewRow row = gridubicacion.Rows[0];        // es la fila 0 porque rows devuelve las filas de la pagina actual y como la pagina es siempre 1 hay 1 sola fila.

            if (!TildoPuertas(row) && hid_tabselected.Value != "3")     // Si no tildo puertas y no es una ubicacion especial.
                lstValidacionesUbicacion.Items.Add("Debe tildar al menos 1 puerta.");

            ValidarPuertas(row, ref lstValidacionesUbicacion);

            if (lstValidacionesUbicacion.Items.Count.Equals(0))
            {
                ucAgregarUbicacionEventsArgs args = new ucAgregarUbicacionEventsArgs();
                HiddenField hid_id_ubicacion = (HiddenField)row.FindControl("hid_id_ubicacion");
                DataList lstPuertas = (DataList)row.FindControl("lstPuertas");
                CheckBoxList chkPartidasHorizontales = (CheckBoxList)row.FindControl("CheckBoxListPHorizontales");
                TextBox txtDepto = (TextBox)row.FindControl("txtDepto");
                TextBox txtLocal = (TextBox)row.FindControl("txtLocal");
                TextBox txtOtros = (TextBox)row.FindControl("txtOtros");
                TextBox txtTorre = (TextBox)row.FindControl("txtTorre");

                args.id_ubicacion = Convert.ToInt32(hid_id_ubicacion.Value);
                args.id_subtipoubicacion = 0;
                args.vDeptoLocalOtros = txtOtros.Text.Trim();
                args.vDepto = txtDepto.Text.Trim();
                args.vLocal = txtLocal.Text.Trim();
                args.vTorre = txtTorre.Text.Trim();

                SSITSolicitudesUbicacionesBL solbl = new SSITSolicitudesUbicacionesBL();

                args.id_subtipoubicacion = solbl.getIdSubTipoUbicacionByIdUbicacion(args.id_ubicacion);
                args.local_subtipoubicacion = txtDescUbicacion.Text.Trim();

                // Agrega los datos de las ubicaciones especiales
                //if (hid_tabselected.Value.Equals("3") && ddlSubTipoUbicacion.SelectedValue.Length > 0)
                //{
                //    args.id_subtipoubicacion = int.Parse(ddlSubTipoUbicacion.SelectedValue);
                //    args.local_subtipoubicacion = txtDescUbicacion.Text.Trim();
                //}


                // agrega las calles seleccionadas
                foreach (DataListItem item in lstPuertas.Items)
                {
                    CheckBox chkPuerta = (CheckBox)item.FindControl("chkPuerta");
                    if (chkPuerta.Checked)
                    {
                        HiddenField hid_codigo_calle = (HiddenField)item.FindControl("hid_codigo_calle");
                        TextBox txtNroPuerta = (TextBox)item.FindControl("txtNroPuerta");
                        Label lblnombreCalle = (Label)item.FindControl("lblnombreCalle");
                        int codigo_calle = int.Parse(hid_codigo_calle.Value);
                        int NroPuerta = int.Parse(txtNroPuerta.Text.Trim());

                        ucPuerta puerta = new ucPuerta();
                        puerta.codigo_calle = codigo_calle;
                        puerta.NroPuerta = NroPuerta;

                        args.Puertas.Add(puerta);

                    }

                }

                //Agregar las partidas horizontales seleccionadas
                if (chkPartidasHorizontales.Items.Count > 0)
                {
                    foreach (ListItem chkPartidaHorizontal in chkPartidasHorizontales.Items)
                    {
                        if (chkPartidaHorizontal.Selected)
                        {
                            args.ids_propiedades_horizontales.Add(int.Parse(chkPartidaHorizontal.Value));
                        }
                    }


                }
                args.upd = updBuscarUbicacion;

                //Llamar al evento.
                if (AgregarUbicacionClick != null)
                {
                    AgregarUbicacionClick(sender, args);
                    if (!args.Cancel)
                        Inicilizar_Control();
                }

            }
            else
            {
                // mostrar resultados de las validaciones
                pnlValidacionIngresoUbicacion.Style["display"] = "block";
            }
            updBuscarUbicacion.Update();
        }

        protected void btnNuevaPuerta_Click(object sender, EventArgs e)
        {
            LinkButton btnNuevaPuerta = (LinkButton)sender;
            int id_ubicacion = int.Parse(btnNuevaPuerta.CommandArgument);

            NuevaPuerta.IdUbicacion = id_ubicacion;
            NuevaPuerta.CargarDatos();

            ScriptManager.RegisterStartupScript(udpfrmSolicitarNuevaPuerta, udpfrmSolicitarNuevaPuerta.GetType(), "showfrmSolicitarNuevaPuerta", "showfrmSolicitarNuevaPuerta();", true);
            udpfrmSolicitarNuevaPuerta.Update();

        }

        public string GetUrlFoto(int ancho, int alto, int? seccion, string manzana, string parcela)
        {
            if (seccion.HasValue)
                return Functions.GetUrlFoto(seccion.Value, manzana, parcela, ancho, alto);
            else
                return Functions.ImageNotFound(this.Page);

        }

        protected void lstPuertas_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (Edicion)
            {
                if (transfUbicacionesPuertasBL == null)
                    transfUbicacionesPuertasBL = new TransferenciasUbicacionesPuertasBL();

                if (lstPuertasTransfDTO == null)
                    lstPuertasTransfDTO = transfUbicacionesPuertasBL.GetByFKIdTransferenciaUbicacion(idUbicacion).ToList();

                var Codigo = Convert.ToInt32((e.Item.FindControl("hid_codigo_calle") as HiddenField).Value);
                var Numero = Convert.ToInt32((e.Item.FindControl("hid_NroPuerta_ubic") as HiddenField).Value);

                if (lstPuertasTransfDTO.Select(p => p.CodigoCalle).Contains(Codigo) && lstPuertasTransfDTO.Select(p => p.NumeroPuerta).Contains(Numero))
                    (e.Item.FindControl("chkPuerta") as CheckBox).Checked = true;
            }
            SSITSolicitudesUbicacionesBL ubicacionesBL = new SSITSolicitudesUbicacionesBL();

            UbicacionesPuertasDTO ubi = e.Item.DataItem as UbicacionesPuertasDTO;

            if (ubicacionesBL.esUbicacionEspecialConObjetoTerritorialByIdUbicacion(ubi.IdUbicacion))
            {
                txtNroPuerta.Text += "t";
            }
        }
        protected void CheckBoxListPHorizontales_DataBound(object sender, EventArgs e)
        {
            if (Edicion)
            {
                if (transfUbicacionPropiedadHorizontalBL == null)
                    transfUbicacionPropiedadHorizontalBL = new TransferenciasUbicacionesPropiedadHorizontalBL();

                if (lstPropHorTransfDTO == null)
                    lstPropHorTransfDTO = transfUbicacionPropiedadHorizontalBL.GetByFKIdSolicitudUbicacion(idUbicacion).ToList();

                CheckBoxList chk = (CheckBoxList)sender;

                foreach (ListItem item in chk.Items)
                {
                    if (lstPropHorTransfDTO.Select(p => p.IdPropiedadHorizontal).Contains(Convert.ToInt32(item.Value)))
                        item.Selected = true;
                }
            }
        }
    }
}