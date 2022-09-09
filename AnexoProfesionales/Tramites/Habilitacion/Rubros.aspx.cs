using AnexoProfesionales.App_Components;
using BusinesLayer.Implementation;
using DataTransferObject;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using static StaticClass.Constantes;

namespace AnexoProfesionales
{
    public partial class Rubros : BasePage
    {

        EncomiendaRubrosBL encRubrosBL = new EncomiendaRubrosBL();
        RubrosBL rubBL = new RubrosBL();
        EncomiendaBL encBL = new EncomiendaBL();

        private int IdEncomienda
        {
            get
            {
                int ret = 0;
                int.TryParse(Page.RouteData.Values["id_encomienda"].ToString(), out ret);
                return ret;
            }
        }

        private int IdTipoTramite
        {
            set
            {
                hid_id_tipo_tramite.Value = value.ToString();
            }
            get
            {
                int ret = 0;
                int.TryParse(hid_id_tipo_tramite.Value, out ret);
                return ret;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager sm = ScriptManager.GetCurrent(this);

            if (sm.IsInAsyncPostBack)
            {
                ScriptManager.RegisterStartupScript(updAgregarNormativa, updAgregarNormativa.GetType(), "init_JS_updAgregarNormativa", "init_JS_updAgregarNormativa();", true);
                ScriptManager.RegisterStartupScript(updfrmAgregarActividades, updfrmAgregarActividades.GetType(), "init_Js_updfrmAgregarActividades", "init_Js_updfrmAgregarActividades();", true);
                ScriptManager.RegisterStartupScript(updBuscarRubros, updBuscarRubros.GetType(), "init_Js_updBuscarRubros", "init_Js_updBuscarRubros();", true);
                ScriptManager.RegisterStartupScript(updEditarSuperficieRubroActual, updEditarSuperficieRubroActual.GetType(), "init_Js_updEditarSuperficieRubroActual", "init_Js_updEditarSuperficieRubroActual();", true);

            }


            if (!IsPostBack)
            {
                hid_return_url.Value = Request.Url.AbsoluteUri;
                hid_DecimalSeparator.Value = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator.ToString();
                ComprobarSolicitud();
                MostrarAvisoLey6099();
                Titulo.CargarDatos(IdEncomienda, "Rubros o Actividades");
            }
        }

        private void MostrarAvisoLey6099()
        {
            mensaje.Visible = !(this.IdTipoTramite == (int)Constantes.TipoDeTramite.Habilitacion);
        }

        private void ComprobarSolicitud()
        {
            if (Page.RouteData.Values["id_encomienda"] != null)
            {

                var enc = encBL.Single(this.IdEncomienda);
                if (enc != null)
                {
                    /*Falta el userID y hacer overload de getuserid con el tipo de tramite*/
                    Guid userid_solicitud = (Guid)Membership.GetUser().ProviderUserKey;
                    //TransferenciasSolicitudesBL trBL = new TransferenciasSolicitudesBL();
                    //var sol = trBL.Single(enc.IdSolicitud);
                    //if (sol != null)
                    //    this.IdTipoTramite = (int)Constantes.TipoDeTramite.Transferencia;
                    //else
                    //    this.IdTipoTramite = enc SSITSolicitudesDTO.IdTipoTramite;
                    if (enc.EncomiendaSSITSolicitudesDTO != null)
                        this.IdTipoTramite = enc.EncomiendaSSITSolicitudesDTO.
                                //Where(x => x.id_solicitud == enc.EncomiendaSSITSolicitudesDTO.Select(y => y.id_solicitud).FirstOrDefault()).
                                Select(y => y.SSITSolicitudesDTO.IdTipoTramite).FirstOrDefault();
                    else this.IdTipoTramite = (int)Constantes.TipoDeTramite.Transferencia;

                    if (userid_solicitud != enc.CreateUser)
                        Server.Transfer("~/Errores/Error3002.aspx");
                    else
                    {
                        if (!(enc.IdEstado == (int)Constantes.Encomienda_Estados.Incompleta ||
                                enc.IdEstado == (int)Constantes.Encomienda_Estados.Completa))
                        {
                            Server.Transfer("~/Errores/Error3003.aspx");
                        }

                        // Si es una redistribución de uso y proviende de una solicitud anterior
                        if (enc.IdTipoTramite == (int)Constantes.TipoTramite.REDISTRIBUCION_USO && encBL.PoseeHabilitacionConAnexoTecnicoAnterior(enc.IdEncomienda))
                            Server.Transfer("~/Errores/Error3006.aspx");
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
                CargarZonas(IdEncomienda);
                CargarDatosTramite(IdEncomienda);
                CargarNormativa(IdEncomienda);
                CargarRubros(IdEncomienda);
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

        private void CargarDatosTramite(int id_encomienda)
        {
            var encomiendaDTO = encBL.Single(IdEncomienda);
            CargarDatosTramite(encomiendaDTO);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEncomienda"></param>
        private void CargarDatosTramite(EncomiendaDTO encomiendaDTO)
        {
            EncomiendaDatosLocalBL encomiendaDatosLocalBL = new EncomiendaDatosLocalBL();

            var dtoDatosLocal = encomiendaDTO.EncomiendaDatosLocalDTO.FirstOrDefault();

            txtObservaciones.Text = encomiendaDTO.ObservacionesRubros;
            txtObservacionesATAnterior.Text = encomiendaDTO.ObservacionesRubrosATAnterior;

            if (dtoDatosLocal != null)
                lblSuperficieLocal.Text = Convert.ToString(dtoDatosLocal.superficie_cubierta_dl + dtoDatosLocal.superficie_descubierta_dl);

            lblTipoTramite.Text = encomiendaDTO.TipoTramiteDescripcion + " " + encomiendaDTO.TipoExpedienteDescripcion;
            hid_Superficie_Local.Value = lblSuperficieLocal.Text;

            if (this.IdTipoTramite == (int)Constantes.TipoTramite.AMPLIACION || this.IdTipoTramite == (int)Constantes.TipoTramite.REDISTRIBUCION_USO ||
                this.IdTipoTramite == (int)Constantes.TipoTramite.TRANSFERENCIA)
            {
                pnlSuperficieAmpliacion.Visible = true;
                //pnlRubrosATAnterior.Visible = true;
                lblSuperficieHabilitar.Text = "Superficie habilitada:";

                if (dtoDatosLocal != null && dtoDatosLocal.ampliacion_superficie.HasValue && dtoDatosLocal.ampliacion_superficie.Value)
                    lblSuperficieTotalAmpliar.Text = Convert.ToString(dtoDatosLocal.superficie_cubierta_amp + dtoDatosLocal.superficie_descubierta_amp);
                else
                    lblSuperficieTotalAmpliar.Text = lblSuperficieLocal.Text;

                hid_Superficie_Total_Ampliar.Value = lblSuperficieTotalAmpliar.Text;
            }

            updInformacionTramite.Update();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEncomienda"></param>
        private void CargarNormativa(int IdEncomienda)
        {

            lblTipoNormativa.Text = "";
            lblEntidadNormativa.Text = "";
            lblNroNormativa.Text = "";
            hid_id_entidad_normativa.Value = "";

            EncomiendaNormativasBL encomiendaNormativasBL = new EncomiendaNormativasBL();
            var normativa = encomiendaNormativasBL.GetNormativas(IdEncomienda).FirstOrDefault();

            if (normativa != null)
            {
                lblTipoNormativa.Text = normativa.TipoNormativaDTO.Nombre;
                lblEntidadNormativa.Text = normativa.EntidadNormativaDTO.Nombre;
                lblNroNormativa.Text = normativa.NroNormativa;
                hid_id_entidad_normativa.Value = normativa.IdEncomiendaNormativa.ToString();
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
        /// <param name="IdEncomienda"></param>
        private void CargarZonas(int IdEncomienda)
        {
            ZonasPlaneamientoBL zonas = new ZonasPlaneamientoBL();
            var lstZonasCombo = zonas.GetZonasEncomienda(IdEncomienda);

            //ddlZonaDeclarada.DataSource = lstZonasCombo;
            //ddlZonaDeclarada.DataTextField = "DescripcionCompleta";
            //ddlZonaDeclarada.DataValueField = "CodZonaPla";
            //ddlZonaDeclarada.DataBind();

            foreach (var item in lstZonasCombo)
            {
                rblZonaDeclarada.Items.Add(new ListItem { Text = item.DescripcionCompleta, Value = item.CodZonaPla });
            }

            string ZonaDeclarada = encBL.Single(IdEncomienda).ZonaDeclarada;

            rblZonaDeclarada.SelectedValue = ZonaDeclarada;

            //ddlZonaDeclarada.SelectedValue = ZonaDeclarada;
        }

        protected void rblZonaDeclarada_SelectedIndexChanged(object sender, EventArgs e)
        {
            encBL.ActualizarZonaDeclarada(IdEncomienda, rblZonaDeclarada.SelectedValue);
            CargarRubros(IdEncomienda);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEncomienda"></param>        
        private void CargarRubros(int IdEncomienda)
        {

            var encomiendaDTO = encBL.Single(IdEncomienda);
            var lstRubrosSolicitud = encRubrosBL.GetRubros(IdEncomienda);

            #region OficinaComercial
            if (lstRubrosSolicitud.Where(x => x.OficinaComercial == true).Any())
            {
                string rubros = string.Join(", ", lstRubrosSolicitud.Where(x => x.OficinaComercial == true).Select(s => s.CodigoRubro).ToArray());

                string plural1;
                string plural2;
                string plural3;
                if (lstRubrosSolicitud.Where(x => x.OficinaComercial == true).Count() > 1)
                {
                    plural1 = "los rubros";
                    plural2 = "serán";
                    plural3 = "utilizados";
                }
                else
                {
                    plural1 = "el rubro";
                    plural2 = "será";
                    plural3 = "utilizado";
                }

                chkOficinaComercial.Text = string.Format("Se deja constancia que {0} <b>{1}</b> {2} {3} solo con el fin de oficina comercial", plural1, rubros, plural2, plural3);
                pnlchkOficinaComercial.Style["display"] = "block";
                chkOficinaComercial.Checked = encomiendaDTO.DeclaraOficinaComercial;
                hid_tiene_rubros_ofc_comercial.Value = "1";

                lblMensajeRubrosOfcComercial.Text = string.Format("¿Está seguro que no desea declarar el uso del/los rubro/s <b>{0}</b> solo con fin de oficina comercial?", rubros);

            }
            else
            {
                chkOficinaComercial.Text = "";
                pnlchkOficinaComercial.Style["display"] = "none";
                hid_tiene_rubros_ofc_comercial.Value = "0";
            }
            #endregion

            EncomiendaUbicacionesBL encUbicBL = new EncomiendaUbicacionesBL();
            var ecnUbicDTO = encUbicBL.GetByFKIdEncomienda(IdEncomienda);
            var zonPla = new ZonasPlaneamientoBL().GetZonaPlaneamientoByIdEncomienda(IdEncomienda);

            List<int> lstZonaPlaneamiento = ecnUbicDTO.Select(s => s.IdZonaPlaneamiento).ToList();
            lstZonaPlaneamiento.AddRange(zonPla.Select(s => s.IdZonaPlaneamiento).ToList());

            if (encUbicBL.esZonaResidencial(lstZonaPlaneamiento))
            {
                pnlchkCumpleArticulo521.Style["display"] = "block";
                chkCumpleArticulo521.Checked = encomiendaDTO.CumpleArticulo521;
            }
            else
                pnlchkCumpleArticulo521.Style["display"] = "none";


            grdRubrosIngresados.DataSource = lstRubrosSolicitud.ToList();
            grdRubrosIngresados.DataBind();
            updRubros.Update();

            #region Ampliacion o Transferencia
            //Verificamos relaciones
            var encSol = encomiendaDTO.EncomiendaSSITSolicitudesDTO?.FirstOrDefault();
            var encTransf = encomiendaDTO.EncomiendaTransfSolicitudesDTO?.FirstOrDefault();

            //Corroboramos el tipo
            if (encSol?.SSITSolicitudesDTO?.IdTipoTramite == (int)TipoTramite.AMPLIACION
                || encSol?.SSITSolicitudesDTO?.IdTipoTramite == (int)TipoTramite.REDISTRIBUCION_USO
                || encTransf?.TransferenciasSolicitudesDTO?.idSolicitudRef != null)
            {
                bool digital = false;

                if (encTransf != null || encSol?.SSITSolicitudesDTO?.IdTipoTramite == (int)TipoTramite.REDISTRIBUCION_USO)
                    lblTituloBoxRubros.Text = "Asimilación de Rubros";
                else
                    lblTituloBoxRubros.Text = "Rubros a Ampliar";

                var solicitud = new SSITSolicitudesBL().Single(encomiendaDTO.IdSolicitud);
                if (encSol != null)
                {
                    digital = solicitud?.SSITSolicitudesOrigenDTO != null && solicitud.SSITSolicitudesOrigenDTO.id_solicitud_origen.HasValue;
                }

                if (encTransf != null)
                {
                    var transferencia = new TransferenciasSolicitudesBL().Single(encomiendaDTO.IdSolicitud);
                    digital = transferencia != null && transferencia.idSolicitudRef.HasValue;
                }

                var lstRubrosSolicitudATAnterior = encRubrosBL.GetRubrosATAnterior(IdEncomienda).ToList();
                grdRubrosIngresadosATAnterior.DataSource = lstRubrosSolicitudATAnterior.ToList();
                grdRubrosIngresadosATAnterior.DataBind();
                pnlRubrosATAnterior.Visible = lstRubrosSolicitudATAnterior.Count > 0 || solicitud.SSITSolicitudesOrigenDTO == null;

                //Si es una ampliacion digital
                if (digital)
                {
                    //Quitar la opción de editar
                    foreach (GridViewRow row in grdRubrosIngresadosATAnterior.Rows)
                    {
                        LinkButton btnEliminarRubroATAnterior = (LinkButton)row.FindControl("btnEliminarRubroATAnterior");
                        btnEliminarRubroATAnterior.Visible = false;
                    }
                    btnAgregarRubrosATAnterior.Visible = false;

                }

                updRubrosATAnterior.Update();
            }
            #endregion
        }

        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdRubrosIngresados.Rows.Count == 0)
                    throw new Exception(Errors.ENCOMIENDA_INGRESAR_RUBROS);

                EncomiendaUbicacionesBL encUbicBL = new EncomiendaUbicacionesBL();
                ZonasPlaneamientoBL zonPlanBL = new ZonasPlaneamientoBL();
                List<int> lstZonaPlaneamiento;

                var ecnUbicDTO = encUbicBL.GetByFKIdEncomienda(IdEncomienda);

                lstZonaPlaneamiento = ecnUbicDTO.Select(s => s.IdZonaPlaneamiento).ToList();

                var SelectedZonaPla = zonPlanBL.GetByCodZonaPlaneamiento(rblZonaDeclarada.SelectedValue);

                if (SelectedZonaPla.Any())
                    lstZonaPlaneamiento.Add(Convert.ToInt32(SelectedZonaPla.FirstOrDefault().IdZonaPlaneamiento));

                bool cumpleArticulo521 = false;
                bool declaraOficinaComercial = false;

                if (encUbicBL.esZonaResidencial(lstZonaPlaneamiento))
                    if (!chkCumpleArticulo521.Checked)
                        throw new Exception(Errors.ENCOMIENDA_DECLARAR_ARTICULO_521);
                    else
                        cumpleArticulo521 = true;

                var lstRubrosSolicitud = encRubrosBL.GetRubros(IdEncomienda);

                if (lstRubrosSolicitud.Where(x => x.OficinaComercial == true).Any())
                    declaraOficinaComercial = chkOficinaComercial.Checked;

                var encomienda = encBL.Single(IdEncomienda);
                encomienda.CumpleArticulo521 = cumpleArticulo521;
                encomienda.DeclaraOficinaComercial = declaraOficinaComercial;
                encomienda.ObservacionesRubros = txtObservaciones.Text.Trim();

                // Valida que NO sea una SSP si es una redistribución de Uso
                if (encomienda.IdTipoTramite == (int)Constantes.TipoDeTramite.RedistribucionDeUso
                    && encomienda.IdTipoExpediente == (int)Constantes.TipoDeExpediente.Simple
                    && encomienda.IdSubTipoExpediente == (int)Constantes.SubtipoDeExpediente.SinPlanos)
                    throw new Exception(Errors.ENCOMIENDA_REDISTRIBUION_USO_NO_SSP);


                if (pnlRubrosATAnterior.Visible)
                    encomienda.ObservacionesRubrosATAnterior = txtObservacionesATAnterior.Text;

                encBL.Update(encomienda);

                if (hid_return_url.Value.Contains("Editar"))
                {
                    Response.Redirect(string.Format("~/" + RouteConfig.VISOR_ENCOMIENDA + "{0}", IdEncomienda));
                }
                else
                {
                    if (encomienda.IdSubTipoExpediente == (int)Constantes.SubtipoDeExpediente.SinPlanos)
                        Response.Redirect(string.Format("~/" + RouteConfig.AGREGAR_ENCOMIENDA_CONFORMACIONLOCAL + "{0}", IdEncomienda));
                    else
                        Response.Redirect(string.Format("~/" + RouteConfig.AGREGAR_ENCOMIENDA_CARGAPLANO + "{0}", IdEncomienda));
                }

            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                this.EjecutarScript(updBotonesGuardar, "showfrmError();");
            }
        }

        protected void btnIngresarNormativa_Click(object sender, EventArgs e)
        {
            try
            {
                Guid userid = (Guid)Membership.GetUser().ProviderUserKey;
                int id_tiponormativa = int.Parse(ddlTipoNormativa.SelectedValue);
                int id_entidadnormativa = int.Parse(ddlEntidadNormativa.SelectedValue);
                EncomiendaNormativasBL encomiendaNormativasBL = new EncomiendaNormativasBL();

                EncomiendaNormativasDTO encomiendaNormativaDTO = new EncomiendaNormativasDTO();
                encomiendaNormativaDTO.IdEncomienda = IdEncomienda;
                encomiendaNormativaDTO.IdTipoNormativa = id_tiponormativa;
                encomiendaNormativaDTO.IdEntidadNormativa = id_entidadnormativa;
                encomiendaNormativaDTO.NroNormativa = txtNroNormativa.Text.Trim();
                encomiendaNormativaDTO.CreateUser = userid;

                encomiendaNormativasBL.Update(encomiendaNormativaDTO);

                CargarNormativa(IdEncomienda);

                this.EjecutarScript(updBotonesIngresarNormativa, "hidefrmAgregarNormativa();");

            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                this.EjecutarScript(updBotonesIngresarNormativa, "showfrmError();");
            }
        }

        protected void btnEliminarNormativa_Click(object sender, EventArgs e)
        {
            try
            {
                int IdEncomiendaNormativa = int.Parse(hid_id_entidad_normativa.Value);
                EncomiendaNormativasBL encomiendaNormativasBL = new EncomiendaNormativasBL();
                EncomiendaNormativasDTO encomiendaNormativaDTO = new EncomiendaNormativasDTO();
                encomiendaNormativaDTO.IdEncomiendaNormativa = IdEncomiendaNormativa;
                encomiendaNormativaDTO.IdEncomienda = IdEncomienda;
                encomiendaNormativasBL.Delete(encomiendaNormativaDTO);

                CargarNormativa(IdEncomienda);

                this.EjecutarScript(updConfirmarEliminarNormativa, "hidefrmConfirmarEliminarNormativa();");
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                this.EjecutarScript(updConfirmarEliminarNormativa, "showfrmError();");
            }
        }

        private void BuscarRubros()
        {
            decimal dSuperficieDeclarada = 0;
            decimal.TryParse(txtSuperficie.Text, out dSuperficieDeclarada);

            var ds = encRubrosBL.GetRubros(IdEncomienda, dSuperficieDeclarada, txtBuscar.Text.Trim(), rblZonaDeclarada.SelectedValue);

            grdRubros.DataSource = ds.ToList();
            grdRubros.DataBind();
        }


        protected void btnnuevaBusqueda_Click(object sender, EventArgs e)
        {

            txtSuperficie.Text = hid_Superficie_Local.Value;
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
                string Superficie = DataBinder.Eval(e.Row.DataItem, "RestriccionSup").ToString();
                bool EsAnterior = (bool)DataBinder.Eval(e.Row.DataItem, "EsAnterior");
                bool TieneNormativa = (bool)DataBinder.Eval(e.Row.DataItem, "TieneNormativa");
                int IdTipoTramite = (int)DataBinder.Eval(e.Row.DataItem, "IdTipoTramite");
                // Si está Permitido en todo o si se especifico alguna normativa o si el tipo de trámite es transferencia o habilitacion
                // se permite el ingreso del rubro.

                if ((!EsAnterior && (Zona.ToLower() == "tilde.png" && Superficie.ToLower() == "tilde.png") || TieneNormativa)
                    && (IdTipoTramite == (int)Constantes.TipoDeTramite.Habilitacion
                    || IdTipoTramite == (int)Constantes.TipoDeTramite.Transferencia
                    || IdTipoTramite == (int)Constantes.TipoDeTramite.Ampliacion
                    || IdTipoTramite == (int)Constantes.TipoDeTramite.RedistribucionDeUso))
                {
                    CheckBox chkRubroElegido = (CheckBox)e.Row.Cells[6].FindControl("chkRubroElegido");
                    chkRubroElegido.Enabled = true;
                }
                if (EsAnterior)
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#fff5f3");
            }
        }



        protected void btnIngresarRubros_Click(object sender, EventArgs e)
        {
            decimal dSuperficie = 0;
            int CantRubrosElegidos = 0;

            try
            {
                if (rblZonaDeclarada.SelectedValue.Trim().Length.Equals(0))
                    throw new Exception("Para ingresar rubros en el Anexo es necesario haber seleccionado la Zona antes de ingresar un rubro.");

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

                var encRubrosDTO = encRubrosBL.GetByFKIdEncomienda(IdEncomienda);
                lstcod_rubro.AddRange(encRubrosDTO.Select(s => s.CodigoRubro));

                ZonasPlaneamientoBL zonBL = new ZonasPlaneamientoBL();
                var zonPla = zonBL.GetZonaPlaneamientoByIdEncomienda(IdEncomienda);

                rubBL.ValidarRubrosIndividuales(lstcod_rubro);


                foreach (GridViewRow row in grdRubros.Rows)
                {
                    CheckBox chkRubroElegido = (CheckBox)row.FindControl("chkRubroElegido");

                    if (chkRubroElegido.Checked)
                    {
                        string scod_rubro = grdRubros.DataKeys[row.RowIndex].Values["Codigo"].ToString();
                        decimal.TryParse(grdRubros.DataKeys[row.RowIndex].Values["Superficie"].ToString(), out dSuperficie);

                        EncomiendaRubrosDTO encomiendaRubroDTO = new EncomiendaRubrosDTO();
                        encomiendaRubroDTO.IdEncomienda = IdEncomienda;
                        encomiendaRubroDTO.SuperficieHabilitar = dSuperficie;
                        encomiendaRubroDTO.CodigoRubro = scod_rubro;

                        if (!rubBL.CategorizaciónDeImpacto(Convert.ToInt32(scod_rubro)))
                        {
                            encomiendaRubroDTO.IdImpactoAmbiental = 3;
                        }

                        encRubrosBL.Insert(encomiendaRubroDTO);

                        CantRubrosElegidos++;
                    }
                }

                if (CantRubrosElegidos > 0)
                {
                    CargarDatosTramite(IdEncomienda);
                    CargarRubros(IdEncomienda);

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




        protected void btnEliminarRubro_Click(object sender, EventArgs e)
        {
            try
            {

                int id_encomiendarubro = int.Parse(hid_id_caarubro_eliminar.Value);

                encRubrosBL.Delete(new EncomiendaRubrosDTO()
                {
                    IdEncomiendaRubro = id_encomiendarubro,
                    IdEncomienda = IdEncomienda
                });



                CargarRubros(IdEncomienda);
                CargarDatosTramite(IdEncomienda);
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
        protected void btnAgregarRubroUsoNoContemplado_Click(object sender, EventArgs e)
        {
            try
            {
                int id_tipoactividad = int.Parse(ddlTipoActividad_runc.SelectedValue);
                int id_tipodocreq = int.Parse(ddlTipoDocReq_runc.SelectedValue);
                decimal superficie = 0;
                decimal.TryParse(txtSuperficieRubro_runc.Text, out superficie);

                EncomiendaRubrosDTO encomiendaRubroDTO = new EncomiendaRubrosDTO();
                encomiendaRubroDTO.IdEncomienda = IdEncomienda;
                encomiendaRubroDTO.IdTipoDocumentoRequerido = id_tipodocreq;
                encomiendaRubroDTO.IdTipoActividad = id_tipoactividad;
                encomiendaRubroDTO.SuperficieHabilitar = superficie;
                encomiendaRubroDTO.DescripcionRubro = txtDesc_runc.Text;
                encRubrosBL.InsertRubroUsoNoContemplado(encomiendaRubroDTO);

                ScriptManager.RegisterClientScriptBlock(updBotonesAgregarRubros, updBotonesAgregarRubros.GetType(), "hidefrmAgregarRubroUsoNoContemplado", "hidefrmAgregarRubroUsoNoContemplado();", true);

                CargarRubros(IdEncomienda);

            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                this.EjecutarScript(updBotonesAgregarRubros, "showfrmError();");
            }

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

        #region "Paginacion grilla AT Anterior"
        protected void grdRubrosATAnterior_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdRubrosATAnterior.PageIndex = e.NewPageIndex;
            BuscarRubrosATAnterior();
        }

        protected void cmdPageATAnterior(object sender, EventArgs e)
        {
            Button cmdPage = (Button)sender;

            grdRubrosATAnterior.PageIndex = int.Parse(cmdPage.Text) - 1;
            BuscarRubrosATAnterior();


        }
        protected void cmdAnteriorATAnterior_Click(object sender, EventArgs e)
        {
            grdRubrosATAnterior.PageIndex = grdRubrosATAnterior.PageIndex - 1;
            BuscarRubrosATAnterior();

        }
        protected void cmdSiguienteATAnterior_Click(object sender, EventArgs e)
        {
            grdRubrosATAnterior.PageIndex = grdRubrosATAnterior.PageIndex + 1;
            BuscarRubrosATAnterior();
        }

        protected void grdRubrosATAnterior_DataBound(object sender, EventArgs e)
        {

            GridView grid = grdRubrosATAnterior;
            GridViewRow fila = (GridViewRow)grid.BottomPagerRow;
            if (fila != null)
            {
                Button btnAnterior = (Button)fila.Cells[0].FindControl("cmdAnteriorATAnterior");
                Button btnSiguiente = (Button)fila.Cells[0].FindControl("cmdSiguienteATAnterior");

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
                    Button btn = (Button)fila.Cells[0].FindControl("cmdPageATAnterior" + i.ToString());
                    btn.Visible = false;
                }


                if (grid.PageIndex == 0 || grid.PageCount <= 10)
                {
                    // Mostrar 10 botones o el máximo de páginas

                    for (int i = 1; i <= 10; i++)
                    {
                        if (i <= grid.PageCount)
                        {
                            Button btn = (Button)fila.Cells[0].FindControl("cmdPageATAnterior" + i.ToString());
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

                    Button btnPage10 = (Button)fila.Cells[0].FindControl("cmdPageATAnterior10");
                    btnPage10.Visible = true;
                    btnPage10.Text = Convert.ToString(grid.PageIndex + 1);

                    // Ubica los 9 botones hacia la izquierda
                    for (int i = grid.PageIndex - 1; i >= grid.PageIndex - 9; i--)
                    {
                        CantBucles++;
                        if (i >= 0)
                        {
                            Button btn = (Button)fila.Cells[0].FindControl("cmdPageATAnterior" + Convert.ToString(10 - CantBucles));
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
                            Button btn = (Button)fila.Cells[0].FindControl("cmdPageATAnterior" + Convert.ToString(10 + CantBucles));
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
                    btnPage = "cmdPageATAnterior" + i.ToString();
                    cmdPage = (Button)fila.Cells[0].FindControl(btnPage);
                    if (cmdPage != null)
                        cmdPage.CssClass = "btnPagerGrid";

                }


                // busca el boton por el texto para marcarlo como seleccionado
                string btnText = Convert.ToString(grid.PageIndex + 1);
                foreach (Control ctl in fila.Cells[0].FindControl("pnlpagerATAnterior").Controls)
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


        protected void btnBuscarATAnterior_Click(object sender, EventArgs e)
        {
            BuscarRubrosATAnterior();
            pnlResultadoBusquedaRubrosATAnterior.Style["display"] = "block";
            pnlBotonesAgregarRubrosATAnterior.Style["display"] = "block";
            pnlGrupoAgregarRubrosATAnterior.Style["display"] = "block";
            pnlBuscarRubrosATAnterior.Style["display"] = "none";
        }

        private void BuscarRubrosATAnterior()
        {
            decimal dSuperficieDeclarada = 0;
            decimal.TryParse(txtSuperficieATAnterior.Text, out dSuperficieDeclarada);


            var ds = encRubrosBL.GetRubrosHistoricos(IdEncomienda, dSuperficieDeclarada, txtBuscarATAnterior.Text.Trim(), rblZonaDeclarada.SelectedValue);

            grdRubrosATAnterior.DataSource = ds.ToList();
            grdRubrosATAnterior.DataBind();
        }

        protected void grdRubrosATAnterior_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                // Llenar por default el campo de Superficie.
                decimal dSuperficie = 0;
                decimal.TryParse(txtSuperficieATAnterior.Text, out dSuperficie);

                string Zona = DataBinder.Eval(e.Row.DataItem, "RestriccionZona").ToString();
                string Superficie = DataBinder.Eval(e.Row.DataItem, "RestriccionSup").ToString();
                bool EsAnterior = (bool)DataBinder.Eval(e.Row.DataItem, "EsAnterior");
                bool TieneNormativa = (bool)DataBinder.Eval(e.Row.DataItem, "TieneNormativa");
                int IdTipoTramite = (int)DataBinder.Eval(e.Row.DataItem, "IdTipoTramite");
                CheckBox chkRubroElegido = (CheckBox)e.Row.Cells[6].FindControl("chkRubroElegidoATAnterior");
                chkRubroElegido.Enabled = true;

                if (EsAnterior)
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#fff5f3");

            }
        }

        protected void btnnuevaBusquedaATAnterior_Click(object sender, EventArgs e)
        {


            txtSuperficieATAnterior.Text = hid_Superficie_Local.Value;
            pnlResultadoBusquedaRubrosATAnterior.Style["display"] = "none";
            pnlBotonesAgregarRubrosATAnterior.Style["display"] = "none";
            pnlGrupoAgregarRubrosATAnterior.Style["display"] = "none";
            pnlBuscarRubrosATAnterior.Style["display"] = "block";
            pnlBotonesBuscarRubrosATAnterior.Style["display"] = "block";
            BotonesBuscarRubrosATAnterior.Style["display"] = "block";
            txtBuscarATAnterior.Text = "";
            ValidadorAgregarRubrosATAnterior.Style["display"] = "none";
            txtBuscarATAnterior.Focus();

            updBotonesBuscarRubrosATAnterior.Update();
            updBotonesAgregarRubrosATAnterior.Update();
        }

        protected void btnIngresarRubrosATAnterior_Click(object sender, EventArgs e)
        {
            decimal dSuperficie = 0;
            int CantRubrosElegidos = 0;

            try
            {

                List<string> lstcod_rubro = new List<string>();
                foreach (GridViewRow row in grdRubrosATAnterior.Rows)
                {
                    CheckBox chkRubroElegidoATAnterior = (CheckBox)row.FindControl("chkRubroElegidoATAnterior");

                    if (chkRubroElegidoATAnterior.Checked)
                    {
                        string scod_rubro = grdRubrosATAnterior.DataKeys[row.RowIndex].Values["Codigo"].ToString();
                        lstcod_rubro.Add(scod_rubro);
                    }
                }



                var encRubrosDTO = encRubrosBL.GetByFKIdEncomienda(IdEncomienda);
                lstcod_rubro.AddRange(encRubrosDTO.Select(s => s.CodigoRubro));

                foreach (GridViewRow row in grdRubrosATAnterior.Rows)
                {
                    CheckBox chkRubroElegidoATAnterior = (CheckBox)row.FindControl("chkRubroElegidoATAnterior");

                    if (chkRubroElegidoATAnterior.Checked)
                    {
                        string scod_rubro = grdRubrosATAnterior.DataKeys[row.RowIndex].Values["Codigo"].ToString();
                        decimal.TryParse(grdRubrosATAnterior.DataKeys[row.RowIndex].Values["Superficie"].ToString(), out dSuperficie);

                        EncomiendaRubrosDTO encomiendaRubroDTO = new EncomiendaRubrosDTO();
                        encomiendaRubroDTO.IdEncomienda = IdEncomienda;
                        encomiendaRubroDTO.SuperficieHabilitar = dSuperficie;
                        encomiendaRubroDTO.CodigoRubro = scod_rubro;

                        if (!rubBL.CategorizaciónDeImpacto(Convert.ToInt32(scod_rubro)))
                        {
                            encomiendaRubroDTO.IdImpactoAmbiental = 3;
                        }

                        encRubrosBL.InsertATAnterior(encomiendaRubroDTO);


                        // Cuando es Ampliacion inserta en las dos grillas.
                        if (this.IdTipoTramite == (int)Constantes.TipoTramite.AMPLIACION)
                            encRubrosBL.Insert(encomiendaRubroDTO);

                        CantRubrosElegidos++;
                    }
                }

                if (CantRubrosElegidos > 0)
                {
                    CargarDatosTramite(IdEncomienda);
                    CargarRubros(IdEncomienda);

                    ScriptManager.RegisterClientScriptBlock(updBotonesAgregarRubrosATAnterior, updBotonesAgregarRubros.GetType(), "hidefrmAgregarRubrosATAnterior", "hidefrmAgregarRubrosATAnterior();", true);
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
                this.EjecutarScript(updBotonesAgregarRubrosATAnterior, "showfrmError();");
            }
        }

        protected void btnEliminarRubroATAnterior_Click(object sender, EventArgs e)
        {
            EncomiendaRubrosDTO encRubroAnterior = null;
            EncomiendaRubrosDTO encRubro = null;
            try
            {

                int id_encomiendarubro = int.Parse(hid_id_caarubro_eliminar_ATAnterior.Value);


                encRubroAnterior = encRubrosBL.GetRubrosATAnterior(IdEncomienda).FirstOrDefault(x => x.IdEncomiendaRubro == id_encomiendarubro);
                if (encRubroAnterior != null)
                    encRubro = encRubrosBL.GetRubros(IdEncomienda).FirstOrDefault(x => x.CodigoRubro == encRubroAnterior.CodigoRubro);

                encRubrosBL.DeleteATAnterior(new EncomiendaRubrosDTO()
                {
                    IdEncomiendaRubro = id_encomiendarubro,
                    IdEncomienda = IdEncomienda
                });

                // Si existe en la tabla de rubros a ampliar lo borra.
                if (encRubro != null)
                {
                    encRubrosBL.Delete(new EncomiendaRubrosDTO()
                    {
                        IdEncomiendaRubro = encRubro.IdEncomiendaRubro,
                        IdEncomienda = IdEncomienda
                    });
                }

                CargarRubros(IdEncomienda);
                CargarDatosTramite(IdEncomienda);
                updInformacionTramite.Update();

                this.EjecutarScript(updConfirmarEliminarRubroATAnterior, "hidefrmConfirmarEliminarRubroATAnterior();");
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                this.EjecutarScript(updConfirmarEliminarRubroATAnterior, "showfrmError();");
            }
        }


        protected void btnEditarRubro_Click(object sender, EventArgs e)
        {

            try
            {
                LinkButton btnEditarRubro = (LinkButton)sender;
                HiddenField hid_id_encomiendarubro = (HiddenField)btnEditarRubro.Parent.FindControl("hid_id_encomiendarubro");
                hid_id_encomiendarubro_editar_superficie.Value = hid_id_encomiendarubro.Value;
                txtSuperficieRubroActual.Text = "";

                if (this.IdTipoTramite == (int)Constantes.TipoDeTramite.Ampliacion)
                    txtSuperficieMaximaRubroActual.Text = hid_Superficie_Total_Ampliar.Value.ToString();
                else
                    txtSuperficieMaximaRubroActual.Text = hid_Superficie_Local.Value.ToString();


                this.EjecutarScript(updEditarSuperficieRubroActual, "showfrmEditarSuperficieRubroActual();");
            }
            catch (Exception ex)
            {

                lblError.Text = ex.Message;
                this.EjecutarScript(updEditarSuperficieRubroActual, "showfrmError();");
            }

        }

        protected void btnAceptarSuperficieRubroActual_Click(object sender, EventArgs e)
        {

            try
            {
                int id_encomiendarubro = Convert.ToInt32(hid_id_encomiendarubro_editar_superficie.Value);
                decimal superficieHabilitar = decimal.Parse(txtSuperficieRubroActual.Text.Trim());
                EncomiendaRubrosDTO encrubDTO = encRubrosBL.Single(id_encomiendarubro);
                EncomiendaNormativasBL encomiendaNormativasBL = new EncomiendaNormativasBL();

                var normativa = encomiendaNormativasBL.GetNormativas(IdEncomienda).FirstOrDefault();
                bool TieneNormativa = (normativa != null);
                bool EsRubroAnteriormenteHabilitado = (encRubrosBL.GetRubrosATAnterior(this.IdEncomienda).Count(x => x.CodigoRubro == encrubDTO.CodigoRubro) > 0);

                // si no esta en la grilla de rubros anteriores (en el caso de ampliación) y valida la superficie segun la zona.
                if (EsRubroAnteriormenteHabilitado ||
                    encRubrosBL.ValidarSuperficie(encrubDTO.CodigoRubro, rblZonaDeclarada.SelectedValue, superficieHabilitar, TieneNormativa))
                {
                    encrubDTO.SuperficieHabilitar = superficieHabilitar;
                    encRubrosBL.Update(encrubDTO);
                    CargarRubros(this.IdEncomienda);
                }
                else
                {
                    throw new Exception("Según la zona y superficie, no es posible modificar el rubro.");
                }


                this.EjecutarScript(updBotonesAceptarEditarSuperficieRubroActual, "hidefrmEditarSuperficieRubroActual();");
            }
            catch (Exception ex)
            {

                lblError.Text = ex.Message;
                this.EjecutarScript(updBotonesAceptarEditarSuperficieRubroActual, "hidefrmEditarSuperficieRubroActual();showfrmError();");
            }

        }


    }
}