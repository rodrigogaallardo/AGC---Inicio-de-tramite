using BusinesLayer.Implementation;
using DataTransferObject;
using SSIT.App_Components;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;


namespace SSIT.Solicitud
{
    public partial class Rubros: SecurePage
    {
        SSITSolicitudesBL solBL = new SSITSolicitudesBL();
        SSITSolicitudesRubrosBL solRubrosBL = new SSITSolicitudesRubrosBL();
        RubrosCNBL rubBL = new RubrosCNBL();
        RubrosCNSubrubrosBL rubSubrubBL = new RubrosCNSubrubrosBL();


        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager sm = ScriptManager.GetCurrent(this);

            if (sm.IsInAsyncPostBack)
            {
                ScriptManager.RegisterStartupScript(updAgregarNormativa, updAgregarNormativa.GetType(), "init_JS_updAgregarNormativa", "init_JS_updAgregarNormativa();", true);
                ScriptManager.RegisterStartupScript(updBuscarRubros, updBuscarRubros.GetType(), "init_Js_updBuscarRubros", "init_Js_updBuscarRubros();", true);

            }


            if (!IsPostBack)
            {
                hid_return_url.Value = Request.Url.AbsoluteUri;
                hid_DecimalSeparator.Value = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator.ToString();
                ComprobarSolicitud();
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
        private int id_tipo_tramite
        {
            get
            {
                int ret = 0;
                int.TryParse(hid_id_tipo_tramite.Value, out ret);
                return ret;
            }
            set
            {
                hid_id_tipo_tramite.Value = value.ToString();
            }

        }
        private void ComprobarSolicitud()
        {
            if (Page.RouteData.Values["id_solicitud"] != null)
            {
                this.id_solicitud = Convert.ToInt32(Page.RouteData.Values["id_solicitud"].ToString());
                SSITSolicitudesBL solBL = new SSITSolicitudesBL();
                var sol = solBL.Single(id_solicitud);
                if (sol != null)
                {
                    Guid userid_solicitud = (Guid)Membership.GetUser().ProviderUserKey;
                    this.id_tipo_tramite = sol.IdTipoTramite;

                    if (userid_solicitud != sol.CreateUser)
                        Server.Transfer("~/Errores/Error3002.aspx");
                    else
                    {
                        if (!(sol.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.INCOM ||
                                sol.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.COMP ||
                                sol.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.DATOSCONF ||
                                sol.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.OBSERVADO))
                        {
                            Server.Transfer("~/Errores/Error3003.aspx");
                        }
                    }
                }
                else
                    Server.Transfer("~/Errores/Error3004.aspx");

            }
            else
                Server.Transfer("~/Errores/Error3001.aspx");
        }

        protected void btnCargarDatos_Click(object sender, EventArgs e)
        {
            try
            {

                CargarCombosNormativas();
                CargarZonas(this.id_solicitud);
                CargarDatosTramite(this.id_solicitud);
                CargarNormativa(this.id_solicitud);
                CargarRubros(this.id_solicitud);

                this.EjecutarScript(updCargarDatos, "finalizarCarga();");
                
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = Funciones.GetErrorMessage(ex);
                this.EjecutarScript(updCargarDatos, "finalizarCarga();showfrmError();");
            }

        }

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

        private void CargarZonas(int IdEncomienda)
        {
            UbicacionesZonasMixturasBL zonas = new UbicacionesZonasMixturasBL();
            UbicacionesCatalogoDistritosBL distritos = new UbicacionesCatalogoDistritosBL();
            SSITSolicitudesUbicacionesBL encomiendaUbicacionesBL = new SSITSolicitudesUbicacionesBL();
            
            List<int> lstESolicitudUbicaciones = new List<int>();

            List<ZonasDistritosDTO> listzonDist = new List<ZonasDistritosDTO>();

            DataList lstZD = (DataList)updInformacionTramite.FindControl("lstZD");

            lstESolicitudUbicaciones = encomiendaUbicacionesBL.GetByFKIdSolicitud(this.id_solicitud).Select(x => (int)x.IdUbicacion).ToList();

            var lstZonasCombo = zonas.GetZonasUbicacion(lstESolicitudUbicaciones);
            var lstDistritos = distritos.GetDistritosUbicacion(lstESolicitudUbicaciones);
            listzonDist.Clear();

            foreach (var item in lstZonasCombo)
            {
                ZonasDistritosDTO zonDist = new ZonasDistritosDTO();
                zonDist.Id = item.IdZona;
                zonDist.IdTipo = 1;
                zonDist.Codigo = item.Codigo;
                zonDist.Descripcion = item.Descripcion;
                listzonDist.Add(zonDist);
            }

            foreach (var item in lstDistritos)
            {
                ZonasDistritosDTO zonDist = new ZonasDistritosDTO();
                zonDist.Id = item.IdDistrito;
                zonDist.IdTipo = 2;
                zonDist.Codigo = item.Codigo;
                zonDist.Descripcion = item.Descripcion;
                listzonDist.Add(zonDist);
            }

            lstZD.DataSource = listzonDist;
            lstZD.DataBind();
        }


        protected void ddlEntidadNormativa_SelectedIndexChanged(object sender, EventArgs e)
        {
            MostrarNroDispo();
        }

        protected void ddlTipoNormativa_SelectedIndexChanged(object sender, EventArgs e)
        {
            MostrarNroDispo();
        }
        
        #region "Paginación grilla de rubros""
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

        private void MostrarNroDispo()
        {
            int id_tipoNor = 0;
            int id_entidad = 0;

            if (ddlTipoNormativa.SelectedValue.Length > 0)
                id_tipoNor = int.Parse(ddlTipoNormativa.SelectedValue);

            if (ddlEntidadNormativa.SelectedValue.Length > 0)
                id_entidad = int.Parse(ddlEntidadNormativa.SelectedValue);

            if (id_entidad == (int)Constantes.EntidadNormativa.DGIUR && id_tipoNor == (int)Constantes.TipoNormativa.DISP)
            {
                ScriptManager.RegisterStartupScript(updAgregarNormativa, updAgregarNormativa.GetType(), "MostrarNroDispo", "MostrarNroDispo();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(updAgregarNormativa, updAgregarNormativa.GetType(), "OcultarNroDispo", "OcultarNroDispo();", true);
            }
        }

        protected void btnIngresarNormativa_Click(object sender, EventArgs e)
        {
            try
            {
                Guid userid = (Guid)Membership.GetUser().ProviderUserKey;
                int id_tiponormativa = int.Parse(ddlTipoNormativa.SelectedValue);
                int id_entidadnormativa = int.Parse(ddlEntidadNormativa.SelectedValue);
                SSITSolicitudesNormativasBL SSITSolicitudesNormativasBL = new SSITSolicitudesNormativasBL();
                SSITSolicitudesBL SSITSolicitudesBL = new SSITSolicitudesBL();

                if (id_tiponormativa == (int)Constantes.TipoNormativa.DISP &&
                    id_entidadnormativa == (int)Constantes.EntidadNormativa.DGIUR)
                {
                    if (txtnumero.Value.Trim() == "" && txtFecha.Value.Trim() == "")
                        throw new Exception("Debe ingresar el número de Disposición");
                    SSITSolicitudesBL.DescargarDispo("DI-" + txtFecha.Value.Trim() + "-" + txtnumero.Value.Trim() + "-DGIUR", this.id_solicitud, userid);
                    txtNroNormativa.Text = txtFecha.Value.Trim() + "-" + txtnumero.Value.Trim();
                }

                SSITSolicitudesNormativasDTO solicitudNormativaDTO = new SSITSolicitudesNormativasDTO();
                solicitudNormativaDTO.IdSolicitud = this.id_solicitud;
                solicitudNormativaDTO.id_tiponormativa = id_tiponormativa;
                solicitudNormativaDTO.id_entidadnormativa = id_entidadnormativa;
                solicitudNormativaDTO.nro_normativa = txtNroNormativa.Text.Trim();
                solicitudNormativaDTO.CreateUser = userid;
                solicitudNormativaDTO.CreateDate = DateTime.Now;

                SSITSolicitudesNormativasBL.Update(solicitudNormativaDTO);

                CargarNormativa(this.id_solicitud);

                this.EjecutarScript(updBotonesIngresarNormativa, "hidefrmAgregarNormativa();");

            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = Funciones.GetErrorMessage(ex);
                this.EjecutarScript(updBotonesIngresarNormativa, "showfrmError();MostrarNroDispo();");
            }
        }

        private void CargarNormativa(int IdEncomienda)
        {

            lblTipoNormativa.Text = "";
            lblEntidadNormativa.Text = "";
            lblNroNormativa.Text = "";
            hid_id_solicitud_normativa.Value = "";

            SSITSolicitudesNormativasBL solicitudNormativasBL = new SSITSolicitudesNormativasBL();
            var normativa = solicitudNormativasBL.GetNormativas(IdEncomienda).FirstOrDefault();

            if (normativa != null)
            {
                lblTipoNormativa.Text = normativa.TipoNormativaDTO.Nombre;
                lblEntidadNormativa.Text = normativa.EntidadNormativaDTO.Nombre;
                lblNroNormativa.Text = normativa.nro_normativa;
                viewNormativa2.Visible = true;
                viewNormativa1.Visible = false;
            }
            else
            {
                viewNormativa2.Visible = false;
                viewNormativa1.Visible = true;
            }
            

        }

        protected void btnEliminarNormativa_Click(object sender, EventArgs e)
        {
            try
            {
             
                SSITSolicitudesNormativasBL solicitudNormativasBL = new SSITSolicitudesNormativasBL();
                SSITSolicitudesNormativasDTO solicitudNormativaDTO = new SSITSolicitudesNormativasDTO();
                solicitudNormativaDTO.IdSolicitud = this.id_solicitud;
                solicitudNormativasBL.Delete(solicitudNormativaDTO);

                CargarNormativa(this.id_solicitud);

                this.EjecutarScript(updConfirmarEliminarNormativa, "hidefrmConfirmarEliminarNormativa();");
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                this.EjecutarScript(updConfirmarEliminarNormativa, "showfrmError();");
            }
        }

        
        private void CargarDatosTramite(int IdSolicitud)
        {

            SSITSolicitudesDTO solDTO = solBL.Single(IdSolicitud);
            SSITSolicitudesDatosLocalBL solicitudDatosLocalBL = new SSITSolicitudesDatosLocalBL();


            var dtoDatosLocal = solicitudDatosLocalBL.Single(IdSolicitud);

            if (dtoDatosLocal != null)
                lblSuperficieLocal.Text = Convert.ToString(dtoDatosLocal.superficie_cubierta_dl + dtoDatosLocal.superficie_descubierta_dl);

            lblTipoTramite.Text = solDTO.TipoTramiteDescripcion + " " + solDTO.TipoExpedienteDescripcion;
            hid_Superficie_Local.Value = lblSuperficieLocal.Text;

            
            updInformacionTramite.Update();
        }

        
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarRubros();
            pnlResultadoBusquedaRubros.Style["display"] = "block";
            pnlBotonesAgregarRubros.Style["display"] = "block";
            pnlGrupoAgregarRubros.Style["display"] = "block";
            pnlBuscarRubros.Style["display"] = "none";
        }
        private void BuscarRubros()
        {
            List<ZonasDistritosDTO> lstzonDist = new List<ZonasDistritosDTO>();
            int id = 0;
            try
            {
                decimal dSuperficieDeclarada = 0;
                decimal.TryParse(txtSuperficie.Text, out dSuperficieDeclarada);
                DataList lst = (DataList)updInformacionTramite.FindControl("lstZD");
                foreach (DataListItem item in lstZD.Items)
                {
                    ZonasDistritosDTO zd = new ZonasDistritosDTO();
                    zd.Codigo = ((Label)item.FindControl("lblZD")).Text;
                    int.TryParse(((Label)item.FindControl("lblTipo")).Text, out id);
                    zd.IdTipo = id;
                    lstzonDist.Add(zd);
                }
                var ds = solRubrosBL.GetRubros(this.id_solicitud, dSuperficieDeclarada, txtBuscar.Text.Trim(), lstzonDist);

                grdRubros.DataSource = ds.ToList();
                grdRubros.DataBind();
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                this.EjecutarScript(updBotonesAgregarRubros, "showfrmError();");
            }
        }
        protected void chkRubroElegido_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = (sender as CheckBox).Parent.Parent as GridViewRow;

                CheckBox chkRubroElegido = (CheckBox)row.FindControl("chkRubroElegido");

                if (chkRubroElegido.Checked)
                {

                    string CodRub = row.Cells[0].Text;

                    var rub = rubBL.Get(CodRub).FirstOrDefault();
                    var ds = rubSubrubBL.GetSubRubros(rub.IdRubro);

                    if (ds.Count() > 0)
                    {
                        grdSubRubros.DataSource = ds.ToList();
                        grdSubRubros.DataBind();

                        pnlAgregarSubRub.Style["display"] = "block";
                        pnlSubRubros.Style["display"] = "block";

                        ScriptManager.RegisterClientScriptBlock(updBotonesSubRub, updBotonesSubRub.GetType(), "showfrmAgregarSubRubro", "showfrmAgregarSubRubro();", true);
                    }
                }
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                this.EjecutarScript(updBotonesAgregarRubros, "showfrmError();");
            }
        }

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

        protected void grdRubros_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                // Llenar por default el campo de Superficie.
                decimal dSuperficie = 0;

                decimal.TryParse(txtSuperficie.Text, out dSuperficie);

                string Zona = DataBinder.Eval(e.Row.DataItem, "RestriccionZona").ToString();
                bool TieneNormativa = (bool)DataBinder.Eval(e.Row.DataItem, "TieneNormativa");
                

                // Si está Permitido en todo o si se especifico alguna normativa o si el tipo de trámite es transferencia
                // se permite el ingreso del rubro.
                if ((Zona.ToLower() == "tilde.png") || TieneNormativa || this.id_tipo_tramite == (int) Constantes.TipoTramite.PERMISO)
                {
                    CheckBox chkRubroElegido = (CheckBox)e.Row.Cells[5].FindControl("chkRubroElegido");
                    chkRubroElegido.Enabled = true;
                }

            }
        }

        protected void btnIngresarRubros_Click(object sender, EventArgs e)
        {
            decimal dSuperficie = 0;
            int CantRubrosElegidos = 0;

            try
            {
                //if (rblZonaDeclarada.SelectedValue.Trim().Length.Equals(0))
                //    throw new Exception("Para ingresar rubros en el Anexo es necesario haber seleccionado la Zona antes de ingresar un rubro.");

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

                var solRubrosDTO = solRubrosBL.GetByFKIdSolicitud(this.id_solicitud);
                lstcod_rubro.AddRange(solRubrosDTO.Select(s => s.CodigoRubro));
                

                foreach (GridViewRow row in grdRubros.Rows)
                {
                    CheckBox chkRubroElegido = (CheckBox)row.FindControl("chkRubroElegido");

                    if (chkRubroElegido.Checked)
                    {
                        string scod_rubro = grdRubros.DataKeys[row.RowIndex].Values["Codigo"].ToString();
                        decimal.TryParse(grdRubros.DataKeys[row.RowIndex].Values["Superficie"].ToString(), out dSuperficie);

                        SSITSolicitudesRubrosCNDTO solicitudRubroDTO = new SSITSolicitudesRubrosCNDTO();
                        solicitudRubroDTO.IdSolicitud = this.id_solicitud;
                        solicitudRubroDTO.SuperficieHabilitar = dSuperficie;
                        solicitudRubroDTO.CodigoRubro = scod_rubro;

                        var userLogued = (Guid)Membership.GetUser().ProviderUserKey;
                        solRubrosBL.Insert(solicitudRubroDTO, true, userLogued);

                        CantRubrosElegidos++;
                    }
                }

                if (CantRubrosElegidos > 0)
                {
                    //GuardarSubRubros();
                    CargarDatosTramite(this.id_solicitud);
                    CargarRubros(this.id_solicitud);

                    ScriptManager.RegisterClientScriptBlock(updBotonesAgregarRubros, updBotonesAgregarRubros.GetType(), "hidefrmAgregarRubros", "hidefrmAgregarRubros();", true);
                }
                else
                {
                    throw new Exception("Debe seleccionar los rubros/actividades que desea ingresar en la solicitud de Anexo.");
                }
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                this.EjecutarScript(updBotonesAgregarRubros, "showfrmError();");
            }
        }

        protected void btnIngresarSubRubros_Click(object sender, EventArgs e)
        {
            int CantRubrosElegidos = 0;

            try
            {
                List<int> lstid_subrubro = new List<int>();
                foreach (GridViewRow row in grdSubRubros.Rows)
                {
                    CheckBox chkRubroElegido = (CheckBox)row.FindControl("chkRubroElegido");

                    if (chkRubroElegido.Checked)
                    {
                        int id_Subrubro = 0;
                        int.TryParse(grdSubRubros.DataKeys[row.RowIndex].Values["Id_rubroCNsubrubro"].ToString(), out id_Subrubro);
                        lstid_subrubro.Add(id_Subrubro);
                        CantRubrosElegidos++;
                    }
                }

                if (this.hid_SubRubros.Value != "")
                {
                    List<int> lstSubRub = JsonConvert.DeserializeObject<List<int>>(this.hid_SubRubros.Value);
                    lstid_subrubro.AddRange(lstSubRub);
                }
                this.hid_SubRubros.Value = JsonConvert.SerializeObject(lstid_subrubro);

                if (CantRubrosElegidos > 0)
                {
                    btnIngresarRubros_Click(this, e);
                    updBotonesSubRub.Update();
                    ScriptManager.RegisterClientScriptBlock(updBotonesSubRub, updBotonesSubRub.GetType(), "hidefrmAgregarSubRubros", "hidefrmAgregarSubRubros();", true);
                }
                else
                {
                    throw new Exception("Debe seleccionar los Sub rubros/actividades que desea ingresar en la solicitud de Anexo.");
                }
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                this.EjecutarScript(updBotonesSubRub, "showfrmError();");
            }
        }

        protected void btncerrarSubRubros_Click(object sender, EventArgs e)
        {
            int CantRubrosElegidos = 0;
            foreach (GridViewRow row in grdSubRubros.Rows)
            {
                CheckBox chkRubroElegido = (CheckBox)row.FindControl("chkRubroElegido");

                if (chkRubroElegido.Checked)
                {

                    CantRubrosElegidos++;
                }
            }
            if (CantRubrosElegidos == 0)
            {
                lblError.Text = "Debe seleccionar los Sub rubros/actividades que desea ingresar en la solicitud de Anexo.";
                this.EjecutarScript(updBotonesSubRub, "showfrmError();");
            }
        }
        protected void btnEliminarRubro_Click(object sender, EventArgs e)
        {
            try
            {

                int IdSolicitudRubro = int.Parse(hid_id_caarubro_eliminar.Value);

                solRubrosBL.Delete(new SSITSolicitudesRubrosCNDTO()
                {
                    IdSolicitudRubro = IdSolicitudRubro,
                    IdSolicitud = this.id_solicitud
                });

                //if (solRubrosBL.GetRubros(this.id_solicitud).Count() > 0)
                //    solRubrosBL.ActualizarSubTipoExpediente(this.id_solicitud);

                CargarRubros(this.id_solicitud);
                CargarDatosTramite(this.id_solicitud);
                updInformacionTramite.Update();

                this.hid_SubRubros.Value = "";

                this.EjecutarScript(updRubros, "hidefrmConfirmarEliminarRubro();");
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                this.EjecutarScript(updBotonesAgregarRubros, "showfrmError();");
            }
        }

        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdRubrosIngresados.Rows.Count == 0)
                    throw new Exception(Errors.SSIT_SOLICITUD_INGRESAR_RUBROS);

                Response.Redirect(string.Format("~/" + RouteConfig.VISOR_SOLICITUD_PERMISO_MC + "{0}", this.id_solicitud));
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                this.EjecutarScript(updBotonesGuardar, "showfrmError();");
            }
        }
        private void CargarRubros(int IdSolicitud)
        {
            
            List<SSITSolicitudesRubrosCNDTO> lstRubrosCNSolicitudATAnterior = new List<SSITSolicitudesRubrosCNDTO>();
            
            var solDTO = solBL.Single(IdSolicitud);
            var lstRubrosSolicitud = solRubrosBL.GetRubros(IdSolicitud);
            var lstSubRubrosSolicitud = rubSubrubBL.GetSubRubrosByEncomienda(IdSolicitud);


            chkOficinaComercial.Text = "";
            pnlchkOficinaComercial.Style["display"] = "none";
            hid_tiene_rubros_ofc_comercial.Value = "0";


            grdRubrosIngresados.DataSource = lstRubrosSolicitud.ToList();
            grdRubrosIngresados.DataBind();

            grdSubRubrosIngresados.DataSource = lstSubRubrosSolicitud.ToList();
            grdSubRubrosIngresados.DataBind();
            grdSubRubrosIngresados.Visible = (lstSubRubrosSolicitud.Count() > 0);
            updRubros.Update();

        }

    }


}