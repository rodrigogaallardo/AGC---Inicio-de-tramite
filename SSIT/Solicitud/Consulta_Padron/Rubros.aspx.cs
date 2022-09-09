using BusinesLayer.Implementation;
using DataTransferObject;
using SSIT.App_Components;
using StaticClass;
using System;
using System.Globalization;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSIT.Solicitud.Consulta_Padron
{
    public partial class Rubros : BasePage
    {
        public int IdSolicitud
        {
            get
            {
                return Convert.ToInt32(Page.RouteData.Values["id_solicitud"]);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager sm = ScriptManager.GetCurrent(this);

            if (sm.IsInAsyncPostBack)
            {
                ScriptManager.RegisterStartupScript(updAgregarNormativa, updAgregarNormativa.GetType(), "init_JS_updAgregarNormativa", "init_JS_updAgregarNormativa();", true);
            }


            if (!IsPostBack)
            {
                hid_return_url.Value = Request.Url.AbsoluteUri;
                hid_DecimalSeparator.Value = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
                //ComprobarSolicitud();                
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void ComprobarSolicitud()
        {
            ConsultaPadronSolicitudesBL consultaPadronSolicitudesBL = new ConsultaPadronSolicitudesBL();
            var enc = consultaPadronSolicitudesBL.Single(this.IdSolicitud);
            if (enc != null)
            {
                /*Falta el userID y hacer overload de getuserid con el tipo de tramite*/
                Guid userid_solicitud = (Guid)Membership.GetUser().ProviderUserKey;

                if (userid_solicitud != enc.CreateUser)
                    Server.Transfer("~/Errores/Error3002.aspx");
                else {
                    if (!(enc.IdEstado == (int)Constantes.ConsultaPadronEstados.INCOM ||
                            enc.IdEstado == (int)Constantes.ConsultaPadronEstados.COMP ||
                               enc.IdEstado == (int)Constantes.ConsultaPadronEstados.PING))
                    {
                        Server.Transfer("~/Errores/Error3003.aspx");
                    }
                }
            }
            else
                Server.Transfer("~/Errores/Error3004.aspx");

        }

        protected void btnCargarDatos_Click(object sender, EventArgs e)
        {
            try
            {
                CargarCombosNormativas();
                CargarZonas(IdSolicitud);
                CargarDatosTramite(IdSolicitud);
                CargarNormativa(IdSolicitud);
                CargarRubros(IdSolicitud);
                CargarDocumentosRequeridos();
                CargarTipoActividad();

                updInformacionTramite.Update();
                this.EjecutarScript(updCargarDatos, "finalizarCarga();");

            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                this.EjecutarScript(updCargarDatos, "finalizarCarga();showfrmError();");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        private void CargarDatosTramite(int IdSolicitud)
        {
            ConsultaPadronDatosLocalBL consultaPadronDatosLocalBL = new ConsultaPadronDatosLocalBL();
            var dsDatosLocal = consultaPadronDatosLocalBL.GetByFKIdConsultaPadron(IdSolicitud).FirstOrDefault();
            ConsultaPadronSolicitudesBL consultaPadronSolicitudesBL = new ConsultaPadronSolicitudesBL();
            var consultaPadronDTO = consultaPadronSolicitudesBL.Single(IdSolicitud);

            txtObservaciones.Text = consultaPadronDTO.Observaciones;
            lblSuperficieLocal.Text = dsDatosLocal != null ? Convert.ToString(dsDatosLocal.SuperficieCubiertaDl + dsDatosLocal.SuperficieDescubiertaDl) : "0";
            lblTipoTramite.Text = consultaPadronDTO.TipoTramite.DescripcionTipoTramite + " " + consultaPadronDTO.TipoExpediente.DescripcionTipoExpediente;
            hid_Superficie_Local.Value = lblSuperficieLocal.Text;

            updInformacionTramite.Update();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        private void CargarNormativa(int IdSolicitud)
        {

            lblTipoNormativa.Text = "";
            lblEntidadNormativa.Text = "";
            lblNroNormativa.Text = "";
            hid_id_entidad_normativa.Value = "";

            ConsultaPadronNormativasBL consultaPadronNormativasBL = new ConsultaPadronNormativasBL();
            var normativa = consultaPadronNormativasBL.GetNormativas(IdSolicitud).FirstOrDefault();

            if (normativa != null)
            {
                lblTipoNormativa.Text = normativa.TipoNormativa.Descripcion;
                lblEntidadNormativa.Text = normativa.EntidadNormativa.Descripcion;
                lblNroNormativa.Text = normativa.NumeroNormativa;
                hid_id_entidad_normativa.Value = normativa.IdConsultaPadronTipoNormativa.ToString();
                viewNormativa2.Visible = true;
                viewNormativa1.Visible = false;
            }
            else
            {
                viewNormativa2.Visible = false;
                viewNormativa1.Visible = true;
            }
            updNormativa.Update();

        }
        /// <summary>
        /// 
        /// </summary>
        private void CargarCombosNormativas()
        {

            TipoNormativaBL tipoNormativaBL = new TipoNormativaBL();

            ddlTipoNormativa.DataSource = tipoNormativaBL.GetAll().ToList();
            ddlTipoNormativa.DataTextField = "Descripcion";
            ddlTipoNormativa.DataValueField = "Id";
            ddlTipoNormativa.DataBind();

            EntidadNormativaBL entidadNormativaBL = new EntidadNormativaBL();
            ddlEntidadNormativa.DataSource = entidadNormativaBL.GetAll().ToList();
            ddlEntidadNormativa.DataTextField = "Descripcion";
            ddlEntidadNormativa.DataValueField = "Id";
            ddlEntidadNormativa.DataBind();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        private void CargarZonas(int IdSolicitud)
        {
            ZonasPlaneamientoBL zonas = new ZonasPlaneamientoBL();
            var lstZonasCombo = zonas.GetZonasConsultaPadron(IdSolicitud);

            ddlZonaDeclarada.DataSource = lstZonasCombo;
            ddlZonaDeclarada.DataTextField = "DescripcionCompleta";
            ddlZonaDeclarada.DataValueField = "CodZonaPla";
            ddlZonaDeclarada.DataBind();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        private void CargarRubros(int IdSolicitud)
        {
            ConsultaPadronRubrosBL consultaPadronRubrosBL = new ConsultaPadronRubrosBL();
            var lstRubrosSolicitud = consultaPadronRubrosBL.GetRubros(IdSolicitud);

            grdRubrosIngresados.DataSource = lstRubrosSolicitud.ToList();
            grdRubrosIngresados.DataBind();
            updRubros.Update();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdRubrosIngresados.Rows.Count == 0)
                    throw new Exception("Debe ingresar los rubros/actividades para poder continuar con el trámite.");
                ConsultaPadronSolicitudesBL consultaPadronSolicitudesBL = new ConsultaPadronSolicitudesBL();
                var consultaPadronSolicitudesDTO = consultaPadronSolicitudesBL.Single(IdSolicitud);
                consultaPadronSolicitudesDTO.Observaciones = txtObservaciones.Text.Trim();
                consultaPadronSolicitudesBL.Update(consultaPadronSolicitudesDTO);

                if (hid_return_url.Value.Contains("Editar")){
                    Response.Redirect(string.Format("~/" + RouteConfig.VISOR_CPADRON + "{0}", IdSolicitud));
                }
                else {                
                    Response.Redirect(string.Format("~/" + RouteConfig.AGREGAR_TITULARES_CPADRON + "{0}", IdSolicitud));
                }
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                this.EjecutarScript(updBotonesGuardar, "showfrmError();");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnIngresarNormativa_Click(object sender, EventArgs e)
        {
            try
            {
                Guid userid = (Guid)Membership.GetUser().ProviderUserKey;
                int id_tiponormativa = int.Parse(ddlTipoNormativa.SelectedValue);
                int id_entidadnormativa = int.Parse(ddlEntidadNormativa.SelectedValue);
                ConsultaPadronNormativasBL consultaPadronNormativasBL = new ConsultaPadronNormativasBL();

                ConsultaPadronNormativasDTO consultaPadronNormativasDTO = new ConsultaPadronNormativasDTO();
                consultaPadronNormativasDTO.IdConsultaPadron = IdSolicitud;
                consultaPadronNormativasDTO.IdTipoNormativa = id_tiponormativa;
                consultaPadronNormativasDTO.IdEntidadNormativa = id_entidadnormativa;
                consultaPadronNormativasDTO.NumeroNormativa = txtNroNormativa.Text.Trim();
                consultaPadronNormativasDTO.CreateUser = userid;

                consultaPadronNormativasBL.Update(consultaPadronNormativasDTO);

                CargarNormativa(IdSolicitud);

                this.EjecutarScript(updBotonesIngresarNormativa, "hidefrmAgregarNormativa();");

            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                this.EjecutarScript(updBotonesIngresarNormativa, "showfrmError();");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEliminarNormativa_Click(object sender, EventArgs e)
        {
            try
            {
                int IdNormativa = int.Parse(hid_id_entidad_normativa.Value);
                ConsultaPadronNormativasBL consultaPadronNormativasBL = new ConsultaPadronNormativasBL();

                ConsultaPadronNormativasDTO consultaPadronNormativasDTO = new ConsultaPadronNormativasDTO();

                consultaPadronNormativasDTO.IdConsultaPadronTipoNormativa = IdNormativa;
                consultaPadronNormativasDTO.IdConsultaPadron = IdSolicitud;

                consultaPadronNormativasBL.Delete(consultaPadronNormativasDTO);

                CargarNormativa(IdSolicitud);

                this.EjecutarScript(updConfirmarEliminarNormativa, "hidefrmConfirmarEliminarNormativa();");
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                this.EjecutarScript(updConfirmarEliminarNormativa, "showfrmError();");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void BuscarRubros()
        {
            decimal dSuperficieDeclarada = 0;
            decimal.TryParse(txtSuperficie.Text, out dSuperficieDeclarada);

            ConsultaPadronRubrosBL rubros = new ConsultaPadronRubrosBL();
            var ds = rubros.GetRubrosCpadron(IdSolicitud, dSuperficieDeclarada, txtBuscar.Text.Trim());

            grdRubros.DataSource = ds.ToList();
            grdRubros.DataBind();              
             
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnnuevaBusqueda_Click(object sender, EventArgs e)
        {
            txtSuperficie.Text = hid_Superficie_Local.Value;
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
            updBotonesAgregarRubros.Update();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnIngresarRubros_Click(object sender, EventArgs e)
        {
            decimal dSuperficie = 0;
            int CantRubrosElegidos = 0;

            try
            {
                if (ddlZonaDeclarada.SelectedValue.Trim().Length.Equals(0))
                    throw new Exception(Errors.SSIT_CPADRON_RUBRO_ZONA);

                foreach (GridViewRow row in grdRubros.Rows)
                {
                    CheckBox chkRubroElegido = (CheckBox)row.FindControl("chkRubroElegido");

                    if (chkRubroElegido.Checked)
                    {
                        string scod_rubro = grdRubros.DataKeys[row.RowIndex].Values["Codigo"].ToString();
                        decimal.TryParse(grdRubros.DataKeys[row.RowIndex].Values["Superficie"].ToString(), out dSuperficie);

                        ConsultaPadronRubrosBL consultaPadronRubrosBL = new ConsultaPadronRubrosBL();
                        ConsultaPadronRubrosDTO consultaPadronRubrosDTO = new ConsultaPadronRubrosDTO();

                        consultaPadronRubrosDTO.IdConsultaPadron = IdSolicitud;
                        consultaPadronRubrosDTO.SuperficieHabilitar = dSuperficie;
                        consultaPadronRubrosDTO.CodidoRubro = scod_rubro;

                        consultaPadronRubrosBL.Insert(consultaPadronRubrosDTO);

                        CantRubrosElegidos++;
                    }
                }

                if (CantRubrosElegidos > 0)
                {
                    CargarDatosTramite(IdSolicitud);
                    CargarRubros(IdSolicitud);

                    ScriptManager.RegisterClientScriptBlock(updBotonesAgregarRubros, updBotonesAgregarRubros.GetType(), "hidefrmAgregarRubros", "hidefrmAgregarRubros();", true);
                }
                else
                {
                    throw new Exception("Debe seleccionar los rubros/actividades que desea ingresar en la solicitud del consulta al padrón.");
                }


            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                this.EjecutarScript(updBotonesAgregarRubros, "showfrmError();");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlZonaDeclarada_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConsultaPadronSolicitudesBL consultaPadronSolicitudesBL = new ConsultaPadronSolicitudesBL();
            consultaPadronSolicitudesBL.ActualizarZonaDeclarada(IdSolicitud, ddlZonaDeclarada.SelectedValue);
            CargarRubros(IdSolicitud);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEliminarRubro_Click(object sender, EventArgs e)
        {
            try
            {

                int id_caarubro = int.Parse(hid_id_caarubro_eliminar.Value);
                ConsultaPadronRubrosBL consultaPadronRubrosBL = new ConsultaPadronRubrosBL();

                consultaPadronRubrosBL.Delete(new ConsultaPadronRubrosDTO()
                {
                    IdConsultaPadronRubro = id_caarubro,
                    IdConsultaPadron = IdSolicitud
                });

                CargarRubros(IdSolicitud);
                CargarDatosTramite(IdSolicitud);
                updInformacionTramite.Update();

                this.EjecutarScript(updRubros, "hidefrmConfirmarEliminarRubro();");
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                this.EjecutarScript(updBotonesAgregarRubros, "showfrmError();");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarRubros();
            pnlResultadoBusquedaRubros.Style["display"] = "block";
            pnlBotonesAgregarRubros.Style["display"] = "block";
            pnlGrupoAgregarRubros.Style["display"] = "block";
            pnlBuscarRubros.Style["display"] = "none";
        }
        /// <summary>
        /// 
        /// </summary>
        private void CargarDocumentosRequeridos()
        {
            TipoDocumentacionRequeridaBL tipoDocumentacionRequeridaBL = new TipoDocumentacionRequeridaBL();
            var query = tipoDocumentacionRequeridaBL.GetAll();

            ddlTipoDocReq_runc.DataTextField = "Descripcion";
            ddlTipoDocReq_runc.DataValueField = "Id";

            ddlTipoDocReq_runc.DataSource = query.ToList();
            ddlTipoDocReq_runc.DataBind();
        }
        /// <summary>
        /// 
        /// </summary>
        private void CargarTipoActividad()
        {
            TipoActividadBL tipoDocumentacionRequeridaBL = new TipoActividadBL();
            var query = tipoDocumentacionRequeridaBL.GetAll();

            ddlTipoActividad_runc.DataTextField = "Descripcion";
            ddlTipoActividad_runc.DataValueField = "Id";

            ddlTipoActividad_runc.DataSource = query.ToList();
            ddlTipoActividad_runc.DataBind();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAgregarRubroUsoNoContemplado_Click(object sender, EventArgs e)
        {
            try
            {
                int id_tipoactividad = int.Parse(ddlTipoActividad_runc.SelectedValue);
                int id_tipodocreq = int.Parse(ddlTipoDocReq_runc.SelectedValue);
                decimal superficie = 0;
                decimal.TryParse(txtSuperficieRubro_runc.Text, out superficie);
                ConsultaPadronRubrosBL consultaPadronRubrosBL = new ConsultaPadronRubrosBL();
                ConsultaPadronRubrosDTO consultaPadronRubrosDTO = new ConsultaPadronRubrosDTO();

                consultaPadronRubrosDTO.IdConsultaPadron = IdSolicitud;
                consultaPadronRubrosDTO.IdTipoDocumentoReq = id_tipodocreq;
                consultaPadronRubrosDTO.IdTipoActividad = id_tipoactividad;
                consultaPadronRubrosDTO.SuperficieHabilitar = superficie;
                consultaPadronRubrosDTO.DescripcionRubro = txtDesc_runc.Text;

                consultaPadronRubrosBL.InsertRubroUsoNoContemplado(consultaPadronRubrosDTO);

                ScriptManager.RegisterClientScriptBlock(updBotonesAgregarRubros, updBotonesAgregarRubros.GetType(), "hidefrmAgregarRubroUsoNoContemplado", "hidefrmAgregarRubroUsoNoContemplado();", true);

                CargarRubros(IdSolicitud);

            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                this.EjecutarScript(updBotonesAgregarRubros, "showfrmError();");
            }

        }
        #region PAGINADO
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
        #endregion
    }
}