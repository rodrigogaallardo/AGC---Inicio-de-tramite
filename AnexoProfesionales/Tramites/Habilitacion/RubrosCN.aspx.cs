using AnexoProfesionales.App_Components;
using BusinesLayer.Implementation;
using DataTransferObject;
using Newtonsoft.Json;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using static StaticClass.Constantes;

namespace AnexoProfesionales
{
    public partial class RubrosCN : BasePage
    {
        EncomiendaRubrosCNBL encRubrosCNBL = new EncomiendaRubrosCNBL();
        EncomiendaRubrosBL encRubros = new EncomiendaRubrosBL();
        RubrosCNBL rubBL = new RubrosCNBL();
        EncomiendaBL encBL = new EncomiendaBL();
        RubrosCNSubrubrosBL rubSubrubBL = new RubrosCNSubrubrosBL();
        EncomiendaRubrosCNATAnteriorBL encRubrosCNATA = new EncomiendaRubrosCNATAnteriorBL();
        EncomiendaRubrosATAnteriorBL encRubrosATA = new EncomiendaRubrosATAnteriorBL();
        Encomienda_RubrosCN_DepositoBL encRubDepBL = new Encomienda_RubrosCN_DepositoBL();


        private List<Tuple<string, int>> EncomiendaRubrosCNDeposito
        {
            get
            {
                return JsonConvert.DeserializeObject<List<Tuple<string, int>>>(hid_RubrosDep.Value);
            }
            set
            {
                hid_RubrosDep.Value = JsonConvert.SerializeObject(value);
            }
        }
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
                Titulo.CargarDatos(IdEncomienda, "Rubros o Actividades");
                EncomiendaRubrosCNDeposito = new List<Tuple<string, int>>();
                if (Cache["RubrosDepositos"] != null)
                    Cache.Remove("RubrosDepositos");
            }
        }

        private void ComprobarSolicitud()
        {
            if (Page.RouteData.Values["id_encomienda"] != null)
            {
                int nroSolReferencia = 0;
                int.TryParse(ConfigurationManager.AppSettings["NroSolicitudReferencia"], out nroSolReferencia);
                var enc = encBL.Single(this.IdEncomienda);

                if (enc != null)
                {
                    //valido la numeracion de la solicitud
                    if (enc.IdSolicitud <= nroSolReferencia && enc.EncomiendaTransfSolicitudesDTO.Count <= 0) //EncomiendaSSITSolicitudesDTO.Select(x => x.id_solicitud).FirstOrDefault()
                    {
                        Server.Transfer("~/Errores/Error3003.aspx");
                        return;
                    }
                    /*Falta el userID y hacer overload de getuserid con el tipo de tramite*/
                    Guid userid_solicitud = (Guid)Membership.GetUser().ProviderUserKey;
                    this.IdTipoTramite = enc.EncomiendaSSITSolicitudesDTO.Select(x => x.SSITSolicitudesDTO.IdTipoTramite).FirstOrDefault();

                    if (enc.EncomiendaTransfSolicitudesDTO.Count > 0)
                        this.IdTipoTramite = enc.EncomiendaTransfSolicitudesDTO.FirstOrDefault().TransferenciasSolicitudesDTO.IdTipoTramite;

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
                        //if (enc.IdTipoTramite == (int)Constantes.TipoTramite.REDISTRIBUCION_USO && encBL.PoseeHabilitacionConAnexoTecnicoAnterior(enc.IdEncomienda))
                        //    Server.Transfer("~/Errores/Error3006.aspx");
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
        private List<clsItemGrillaSeleccionarDepositos> CargarDepositos()
        {
            List<clsItemGrillaSeleccionarDepositos> depositos = new List<clsItemGrillaSeleccionarDepositos>();
            string CodRub = hid_idRubro.Value;
            RubrosCNDTO rub = rubBL.Get(CodRub).FirstOrDefault();


            if (rub.TieneRubroDeposito)
            {
                RubrosDepositosCNBL rdBL = new RubrosDepositosCNBL();
                var encomiendaDTO = encBL.Single(IdEncomienda);
                DataList lst = (DataList)updInformacionTramite.FindControl("lstZD");
                int id;

                List<ZonasDistritosDTO> lstzonDist = new List<ZonasDistritosDTO>();
                foreach (DataListItem item in lstZD.Items)
                {
                    ZonasDistritosDTO zd = new ZonasDistritosDTO();
                    zd.Codigo = ((Label)item.FindControl("lblZD")).Text;
                    int.TryParse(((Label)item.FindControl("lblTipo")).Text, out id);
                    zd.IdTipo = id;
                    lstzonDist.Add(zd);
                }
                lstzonDist = (from mci in lstzonDist select mci).Distinct().ToList();

                decimal superficie = (decimal)encomiendaDTO.EncomiendaDatosLocalDTO.FirstOrDefault().superficie_cubierta_dl + (decimal)encomiendaDTO.EncomiendaDatosLocalDTO.FirstOrDefault().superficie_descubierta_dl;
                if (Cache["RubrosDepositos"] == null)
                {
                    depositos = rdBL.CargarCuadroDepositos(encomiendaDTO, superficie);
                    Cache["RubrosDepositos"] = depositos;
                }
                else
                {
                    depositos = (List<clsItemGrillaSeleccionarDepositos>)Cache.Get("RubrosDepositos");
                }

                grdDepositos.DataSource = depositos;
                grdDepositos.DataBind();

            }
            return depositos;
        }

        private void CargarDatosTramite(int id_encomienda)
        {
            var encomiendaDTO = encBL.Single(IdEncomienda);
            CargarDatosTramite(encomiendaDTO);
        }

        private void GuardarRubros()
        {
            var result = false;
            var rubros = new List<string>();
            var superaLimiteSuperficieRubro = true;

            try
            {
                foreach (GridViewRow row in grdRubros.Rows)
                {
                    CheckBox chkRubroElegido = (CheckBox)row.FindControl("chkRubroElegido");

                    if (chkRubroElegido.Checked)
                    {
                        string scod_rubro = grdRubros.DataKeys[row.RowIndex].Values["Codigo"].ToString();
                        decimal.TryParse(grdRubros.DataKeys[row.RowIndex].Values["Superficie"].ToString(), out decimal dSuperficie);

                        var encomiendaRubroDTO = new EncomiendaRubrosCNDTO
                        {
                            IdEncomienda = IdEncomienda,
                            SuperficieHabilitar = dSuperficie,
                            CodigoRubro = scod_rubro
                        };

                        if (!rubBL.CategorizaciónDeImpacto(scod_rubro))
                        {
                            encomiendaRubroDTO.idImpactoAmbiental = 3;
                        }

                        //var limitesuperficieRubro = rubBL.GetLimiteMixtura(encomiendaRubroDTO.CodigoRubro, encomiendaRubroDTO.IdEncomienda);

                        //if (decimal.Parse(limitesuperficieRubro) >= encomiendaRubroDTO.SuperficieHabilitar) 
                        //{
                        var userLogued = (Guid)Membership.GetUser().ProviderUserKey;
                        encRubrosCNBL.Insert(encomiendaRubroDTO, userLogued);

                        rubros.Add(scod_rubro);
                        chkRubroElegido.Checked = false;
                        result = true;
                        superaLimiteSuperficieRubro = false;
                        //}                        
                    }
                }
            }
            catch (ValidationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                DeshacerGuardarRubros();
            }

            if (superaLimiteSuperficieRubro)
            {
                throw new ValidationException("La superficie ingresada supera el limite establecido para el rubro.");
            }

            if (!result)
            {
                throw new ValidationException("Debe seleccionar los rubros/actividades que desea ingresar en la solicitud de Anexo.");
            }
        }

        private void DeshacerGuardarRubros()
        {
            var rubros = new List<string>();

            foreach (GridViewRow row in grdRubros.Rows)
            {
                CheckBox chkRubroElegido = (CheckBox)row.FindControl("chkRubroElegido");

                if (chkRubroElegido.Checked)
                {
                    string scod_rubro = grdRubros.DataKeys[row.RowIndex].Values["Codigo"].ToString();

                    var encomiendaRubroDTO = encRubrosCNBL.GetByFKIdEncomienda(IdEncomienda).Where(x => x.CodigoRubro == scod_rubro).FirstOrDefault();

                    if (encomiendaRubroDTO != null)
                    {
                        EliminarRubro(encomiendaRubroDTO.IdEncomiendaRubro);
                    }
                }
            }
        }

        private void GuardarSubRubros()
        {
            var agregados = new List<EncomiendaRubrosCNDTO>();

            try
            {
                IEnumerable<EncomiendaRubrosCNDTO> rub = encRubrosCNBL.GetByFKIdEncomienda(IdEncomienda);
                List<int> lstsubr = rubSubrubBL.GetSubRubrosByEncomienda(IdEncomienda).Select(x => x.Id_rubroCNsubrubro).ToList();

                foreach (GridViewRow row in grdSubRubros.Rows)
                {
                    CheckBox chkRubroElegido = (CheckBox)row.FindControl("chkRubroElegido");

                    if (chkRubroElegido.Checked)
                    {
                        int.TryParse(grdSubRubros.DataKeys[row.RowIndex].Values["Id_rubroCNsubrubro"].ToString(), out int id_Subrubro);

                        RubrosCNSubRubrosDTO subRub = rubSubrubBL.Single(id_Subrubro);
                        EncomiendaRubrosCNDTO encRub = rub.Where(x => x.IdRubro == subRub.Id_rubroCN).FirstOrDefault();

                        if (!lstsubr.Contains(id_Subrubro))
                        {
                            EncomiendaRubrosCNSubrubrosDTO sRub = new EncomiendaRubrosCNSubrubrosDTO
                            {
                                Id_EncRubro = encRub.IdEncomiendaRubro,
                                Id_rubrosubrubro = id_Subrubro
                            };

                            encRubrosCNBL.InsertSubRub(sRub);
                            agregados.Add(encRub);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                int? idEncRub = encRubrosCNBL.GetByFKIdEncomienda(IdEncomienda).FirstOrDefault()?.IdEncomiendaRubro;
                EliminarRubro(idEncRub ?? 0);
            }

            if (agregados.Any())
            {
                grdSubRubros.Rows.Cast<GridViewRow>().Select(x => ((CheckBox)x.FindControl("chkRubroElegido")).Checked = false);
            }
            else
            {
                throw new ValidationException("Debe seleccionar los Sub rubros/actividades que desea ingresar en la solicitud de Anexo.");
            }
        }

        private void GuardarDepositos()
        {
            var agregados = new List<Encomienda_RubrosCN_DepositoDTO>();

            try
            {
                RubrosCNDTO rub = rubBL.Get(hid_idRubro.Value).FirstOrDefault();
                IEnumerable<Encomienda_RubrosCN_DepositoDTO> encRubDeps = encRubDepBL.GetByEncomienda(IdEncomienda);

                foreach (var i in EncomiendaRubrosCNDeposito)
                {
                    if (encRubDeps.Any(x => x.IdRubro == rub.IdRubro && x.IdDeposito == i.Item2))
                    {
                        throw new ValidationException($"El rubro depósito {i.Item2} ya se encuentra en la encomienda. Si necesita modificar la superficie, eliminelo e ingreselo nuevamente con la nueva superficie");
                    }

                    var encRubDep = new Encomienda_RubrosCN_DepositoDTO
                    {
                        id_encomienda = IdEncomienda,
                        IdRubro = rub.IdRubro,
                        IdDeposito = i.Item2
                    };

                    encRubDepBL.InsertRubDeposito(encRubDep);
                    agregados.Add(encRubDep);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                int? idEncRub = encRubrosCNBL.GetByFKIdEncomienda(IdEncomienda).FirstOrDefault()?.IdEncomiendaRubro;
                EliminarRubro(idEncRub ?? 0);
            }

            if (agregados.Any())
            {
                grdDepositos.Rows.Cast<GridViewRow>().Select(x => ((CheckBox)x.FindControl("chkRubroElegido")).Checked = false);
            }
            else
            {
                throw new ValidationException("Debe seleccionar los rubros depósitos que desea ingresar en la solicitud de Anexo.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEncomienda"></param>
        private void CargarDatosTramite(EncomiendaDTO encomiendaDTO)
        {
            EncomiendaDatosLocalBL encomiendaDatosLocalBL = new EncomiendaDatosLocalBL();
            SSITSolicitudesBL solBL = new SSITSolicitudesBL();
            var Solicitud = solBL.Single(encomiendaDTO.IdSolicitud);

            var dtoDatosLocal = encomiendaDTO.EncomiendaDatosLocalDTO.FirstOrDefault();

            txtObservaciones.Text = encomiendaDTO.ObservacionesRubros;
            txtObservacionesATAnterior.Text = encomiendaDTO.ObservacionesRubrosATAnterior;
            infmodificaciones_SI.Checked = encomiendaDTO.InformaModificacion;
            infmodificaciones_NO.Checked = !encomiendaDTO.InformaModificacion;
            txtDetalleModificaciones.Text = encomiendaDTO.DetalleModificacion;

            if (dtoDatosLocal != null)
                lblSuperficieLocal.Text = Convert.ToString(dtoDatosLocal.superficie_cubierta_dl + dtoDatosLocal.superficie_descubierta_dl);

            lblTipoTramite.Text = encomiendaDTO.TipoTramiteDescripcion + " " + encomiendaDTO.TipoExpedienteDescripcion;
            hid_Superficie_Local.Value = lblSuperficieLocal.Text;

            if (encomiendaDTO.EncomiendaSSITSolicitudesDTO.Select(x => x.SSITSolicitudesDTO.IdTipoTramite).FirstOrDefault() == (int)Constantes.TipoTramite.AMPLIACION)
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
            int tipoTramite = encomiendaDTO.EncomiendaSSITSolicitudesDTO.Select(x => x.SSITSolicitudesDTO.IdTipoTramite).FirstOrDefault();
            if (tipoTramite == (int)Constantes.TipoTramite.AMPLIACION &&
                tipoTramite == (int)Constantes.TipoTramite.REDISTRIBUCION_USO)
            {
                alerta.Visible = false;
                mensaje.Visible = false;
            }
            InfModificacion.Visible = false;
            if (encomiendaDTO.IdTipoTramite == (int)Constantes.TipoTramite.TRANSFERENCIA)
                InfModificacion.Visible = true;

            //Valido si es un ECI
            bool esEci = (encomiendaDTO != null && encomiendaDTO.EsECI) || (Solicitud != null && Solicitud.EsECI);
            if (esEci)
            {
                hidEsECI.Value = "true";
                pnlInfoAdicional.Visible = true;
                if (encomiendaDTO.EsActBaile != null)
                {
                    rbActBaileSI.Checked = (bool)encomiendaDTO.EsActBaile;
                    rbActBaileNo.Checked = !(bool)encomiendaDTO.EsActBaile;
                }

                if (encomiendaDTO.EsLuminaria != null)
                {
                    rbLuminariaSi.Checked = (bool)encomiendaDTO.EsLuminaria;
                    rbLuminariaNo.Checked = !(bool)encomiendaDTO.EsLuminaria;
                }

            }
            else
            {
                hidEsECI.Value = "false";
                pnlInfoAdicional.Visible = false;
            }
            updInfModificacion.Update();
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

            updAgregarNormativa.Update();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEncomienda"></param>
        private void CargarZonas(int IdEncomienda)
        {
            UbicacionesZonasMixturasBL zonas = new UbicacionesZonasMixturasBL();
            UbicacionesCatalogoDistritosBL distritos = new UbicacionesCatalogoDistritosBL();
            EncomiendaUbicacionesBL encomiendaUbicacionesBL = new EncomiendaUbicacionesBL();

            List<int> lstEncomiendaUbicaciones = new List<int>();

            List<ZonasDistritosDTO> listzonDist = new List<ZonasDistritosDTO>();

            DataList lstZD = (DataList)updInformacionTramite.FindControl("lstZD");

            lstEncomiendaUbicaciones = encomiendaUbicacionesBL.GetByFKIdEncomienda(IdEncomienda).Select(x => (int)x.IdUbicacion).ToList();

            var lstZonasCombo = zonas.GetZonasUbicacion(lstEncomiendaUbicaciones);
            var lstDistritos = distritos.GetDistritosUbicacion(lstEncomiendaUbicaciones);
            listzonDist.Clear();

            foreach (var item in lstZonasCombo)
            {
                //rblZonaDeclarada.Items.Add(new ListItem { Text = item.Descripcion, Value = item.Codigo });
                ZonasDistritosDTO zonDist = new ZonasDistritosDTO();
                zonDist.Id = item.IdZona;
                zonDist.IdTipo = 1;
                zonDist.Codigo = item.Codigo;
                zonDist.Descripcion = item.Descripcion;
                listzonDist.Add(zonDist);
            }

            foreach (var item in lstDistritos)
            {
                //rblZonaDeclarada.Items.Add(new ListItem { Text = item.Descripcion, Value = item.Codigo });
                ZonasDistritosDTO zonDist = new ZonasDistritosDTO();
                zonDist.Id = item.IdDistrito;
                zonDist.IdTipo = 2;
                zonDist.Codigo = item.Codigo;
                zonDist.Descripcion = item.Descripcion;
                listzonDist.Add(zonDist);
            }

            lstZD.DataSource = listzonDist;
            lstZD.DataBind();
            //string ZonaDeclarada = encBL.Single(IdEncomienda).ZonaMixtura;

            //rblZonaDeclarada.SelectedValue = ZonaDeclarada;
        }

        protected void rblZonaDeclarada_SelectedIndexChanged(object sender, EventArgs e)
        {
            //encBL.ActualizarZonaDeclarada(IdEncomienda, rblZonaDeclarada.SelectedValue);
            CargarRubros(IdEncomienda);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEncomienda"></param>        
        private void CargarRubros(int IdEncomienda)
        {
            Encomienda_RubrosCN_DepositoBL rubDepBL = new Encomienda_RubrosCN_DepositoBL();
            var encomiendaDTO = encBL.Single(IdEncomienda);
            var lstRubrosSolicitud = encRubrosCNBL.GetRubros(IdEncomienda);
            var lstSubRubrosSolicitud = rubSubrubBL.GetSubRubrosByEncomienda(IdEncomienda);

            EncomiendaRubrosCNDeposito = rubDepBL.GetByEncToList(IdEncomienda);

            chkOficinaComercial.Text = "";
            pnlchkOficinaComercial.Style["display"] = "none";
            hid_tiene_rubros_ofc_comercial.Value = "0";

            grdRubrosIngresados.DataSource = lstRubrosSolicitud.ToList();
            grdRubrosIngresados.DataBind();

            grdSubRubrosIngresados.DataSource = lstSubRubrosSolicitud.ToList();
            grdSubRubrosIngresados.DataBind();
            grdSubRubrosIngresados.Visible = true;
            lblSubRubrosIngresados.Visible = true;

            List<Encomienda_RubrosCN_DepositoDTO> listRubDep = rubDepBL.GetByEncomienda(IdEncomienda);
            grdRubrosCN_DepositoIngresado.DataSource = listRubDep;
            grdRubrosCN_DepositoIngresado.DataBind();
            grdRubrosCN_DepositoIngresado.Visible = (listRubDep.Count() > 0);
            lblDepositoIngresado.Visible = (listRubDep.Count() > 0);

            updRubros.Update();

            //Verificamos relaciones
            var encSol = encomiendaDTO.EncomiendaSSITSolicitudesDTO?.FirstOrDefault();
            var encTransf = encomiendaDTO.EncomiendaTransfSolicitudesDTO?.FirstOrDefault();

            //Corroboramos el tipo
            if (encSol?.SSITSolicitudesDTO?.IdTipoTramite == (int)TipoTramite.AMPLIACION
                || encSol?.SSITSolicitudesDTO?.IdTipoTramite == (int)TipoTramite.REDISTRIBUCION_USO
                || encTransf?.TransferenciasSolicitudesDTO?.idSolicitudRef != null)
            {
                bool digital = false;
                int id_sol_ref = 0;

                ParametrosDTO parametrosDTO = new ParametrosBL().GetParametros(ConfigurationManager.AppSettings["CodParam"]);

                if (encTransf != null || encSol?.SSITSolicitudesDTO?.IdTipoTramite == (int)TipoTramite.REDISTRIBUCION_USO)
                {
                    lblTituloBoxRubros.Text = "Asimilación de Rubros";
                }
                else
                {
                    lblTituloBoxRubros.Text = "Rubros a Ampliar";
                }

                if (encSol != null)
                {
                    var solicitud = new SSITSolicitudesBL().Single(encomiendaDTO.IdSolicitud);
                    if (solicitud?.SSITSolicitudesOrigenDTO != null && (solicitud.SSITSolicitudesOrigenDTO.id_solicitud_origen.HasValue || solicitud.SSITSolicitudesOrigenDTO.id_transf_origen.HasValue))
                    {
                        id_sol_ref = solicitud.SSITSolicitudesOrigenDTO.id_solicitud_origen != null ? solicitud.SSITSolicitudesOrigenDTO.id_solicitud_origen.Value :
                          solicitud.SSITSolicitudesOrigenDTO.id_transf_origen.Value;
                        digital = true;
                    }
                }

                if (encTransf != null)
                {
                    var transferencia = new TransferenciasSolicitudesBL().Single(encomiendaDTO.IdSolicitud);
                    if (transferencia != null && transferencia.idSolicitudRef.HasValue)
                    {
                        id_sol_ref = encTransf.TransferenciasSolicitudesDTO.idSolicitudRef.Value;

                        //Si nuestro tramite actual heredó de una transferencia anterior y tiene rubros con el formato viejo se debe permitir asimilar los rubros.
                        //Caso contrario si tiene los rubros del formato nuevos no es necesario asimilar los rubros, oculto el botón.
                        var Encomienda = encBL.GetUltimaEncomiendaAprobada(id_sol_ref);
                        //var lstRubrosSolicitudAnterior = encRubrosCNBL.GetRubros(Encomienda.IdEncomienda);
                        //Si tiene rubros con codigo viejo, deja asimilar rubros
                        //var lstRubrosSolicitudAnterior = encRubros.GetRubros(Encomienda.IdEncomienda);
                        //if (lstRubrosSolicitudAnterior.Count() > 0)
                        btnAgregarRubros.Visible = true;    // permite asimilar rubros siempre en una transferencia con solicitud origen segun indico mariela

                        digital = true;
                    }
                }

                var lstRubrosCNSolicitudATAnterior = encRubrosCNBL.GetRubrosCNATAnterior(IdEncomienda).ToList();
                grdRubrosCNIngresadosATAnterior.DataSource = lstRubrosCNSolicitudATAnterior;
                grdRubrosCNIngresadosATAnterior.DataBind();
                pnlRubrosATAnterior.Visible = true;
                pnlRubrosCNATAnterior.Visible = true;
                // lo saque afuera para mostrar el mensaje si esta vacio
                var lstRubrosSolicitudATAnterior = encRubros.GetRubrosATAnterior(IdEncomienda).ToList();
                grdRubrosIngresadosATAnterior.DataSource = lstRubrosSolicitudATAnterior;
                grdRubrosIngresadosATAnterior.DataBind();
                //Si es una ampliacion digital
                if (digital)
                {
                    //Verificar tipo de rubro de la herencia
                    if (id_sol_ref < parametrosDTO.ValornumParam)
                    {

                        pnlRubrosATAnterior.Visible = true;
                        pnlRubrosCNATAnterior.Visible = true;
                    }

                    //Quitar la opción de editar
                    foreach (GridViewRow row in grdRubrosIngresadosATAnterior.Rows)
                    {
                        LinkButton btnEliminarRubroATAnterior = (LinkButton)row.FindControl("btnEliminarRubroATAnterior");
                        btnEliminarRubroATAnterior.Visible = false;
                    }
                    btnAgregarRubrosATAnterior.Visible = false;

                    foreach (GridViewRow row in grdRubrosCNIngresadosATAnterior.Rows)
                    {
                        LinkButton btnEliminarRubroCNATAnterior = (LinkButton)row.FindControl("btnEliminarRubroCNATAnterior");
                        btnEliminarRubroCNATAnterior.Visible = false;
                    }
                    btnAgregarRubrosCNATAnterior.Visible = false;
                }

                updRubrosATAnterior.Update();
                updRubrosCNATAnterior.Update();
            }
        }

        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdRubrosIngresados.Rows.Count == 0)
                    throw new Exception(Errors.ENCOMIENDA_INGRESAR_RUBROS);

                EncomiendaUbicacionesBL encUbicBL = new EncomiendaUbicacionesBL();
                ZonasPlaneamientoBL zonPlanBL = new ZonasPlaneamientoBL();

                var ecnUbicDTO = encUbicBL.GetByFKIdEncomienda(IdEncomienda);

                bool declaraOficinaComercial = false;

                var lstRubrosSolicitud = encRubrosCNBL.GetRubros(IdEncomienda);

                //if (lstRubrosSolicitud.Where(x => x.OficinaComercial == true).Any())
                //    declaraOficinaComercial = chkOficinaComercial.Checked;
                var encomienda = encBL.Single(IdEncomienda);

                encomienda.CumpleArticulo521 = true;
                encomienda.DeclaraOficinaComercial = declaraOficinaComercial;
                encomienda.ObservacionesRubros = txtObservaciones.Text.Trim();
                encomienda.InformaModificacion = infmodificaciones_SI.Checked;
                encomienda.DetalleModificacion = txtDetalleModificaciones.Text;

                //Si es una ECI
                SSITSolicitudesBL sol = new SSITSolicitudesBL();
                var Solicitud = sol.Single(encomienda.IdSolicitud);
                bool esEci = (encomienda != null && encomienda.EsECI) || (Solicitud != null && Solicitud.EsECI);
                if (esEci)
                {
                    encomienda.EsActBaile = rbActBaileSI.Checked;
                    encomienda.EsLuminaria = rbLuminariaSi.Checked;

                    //Valido que posea el rubro 2.1.1 de ECI
                    if (!lstRubrosSolicitud.Any(x => x.CodigoRubro == EncomiendaDTO.CodRubroECI))
                    {
                        throw new ValidationException(string.Format(Errors.SSIT_ENCOMIENDA_ECI_SIN_RUBRO_ANEXO, encomienda.IdEncomienda, EncomiendaDTO.CodRubroECI, TipoTramiteDescripcion.HabilitacionECI));
                    }
                }
                else
                {
                    encomienda.EsActBaile = null;
                    encomienda.EsLuminaria = null;
                }
                // Valida que NO sea una SSP si es una redistribución de Uso
                if (encomienda.IdTipoTramite == (int)Constantes.TipoDeTramite.RedistribucionDeUso
                    && encomienda.IdTipoExpediente == (int)Constantes.TipoDeExpediente.Simple
                    && encomienda.IdSubTipoExpediente == (int)Constantes.SubtipoDeExpediente.SinPlanos)
                    throw new Exception(Errors.ENCOMIENDA_REDISTRIBUION_USO_NO_SSP);


                if (pnlRubrosATAnterior.Visible)
                    encomienda.ObservacionesRubrosATAnterior = txtObservacionesATAnterior.Text;

                encBL.Update(encomienda);

                if (Solicitud != null)
                {
                    //Actualizo la solicitud
                    Solicitud.EsECI = encomienda.EsECI;
                    sol.Update(Solicitud);
                }

                if (hid_return_url.Value.Contains("Editar"))
                {
                    Response.Redirect(string.Format("~/" + RouteConfig.VISOR_ENCOMIENDA + "{0}", IdEncomienda), true);
                }
                else
                {
                    if (encomienda.IdSubTipoExpediente == (int)Constantes.SubtipoDeExpediente.SinPlanos)
                        Response.Redirect(string.Format("~/" + RouteConfig.AGREGAR_ENCOMIENDA_CONFORMACIONLOCAL + "{0}", IdEncomienda), true);
                    else
                        Response.Redirect(string.Format("~/" + RouteConfig.AGREGAR_ENCOMIENDA_CARGAPLANO + "{0}", IdEncomienda), true);
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
                string nroNorm = "";
                Guid userid = (Guid)Membership.GetUser().ProviderUserKey;
                int id_tiponormativa = int.Parse(ddlTipoNormativa.SelectedValue);
                int id_entidadnormativa = int.Parse(ddlEntidadNormativa.SelectedValue);
                EncomiendaNormativasBL encomiendaNormativasBL = new EncomiendaNormativasBL();
                EncomiendaBL encomiendaBL = new EncomiendaBL();

                if (id_tiponormativa == (int)Constantes.TipoNormativa.DISP &&
                    id_entidadnormativa == (int)Constantes.EntidadNormativa.DGIUR)
                {
                    if (txtnumero.Value.Trim() == "" || txtFecha.Value.Trim() == "")
                        throw new Exception("Debe ingresar el número de Disposición");

                    nroNorm = $"DI-{txtFecha.Value.Trim()}-{txtnumero.Value.Trim()}-GCABA-DGIUR";


                    encomiendaBL.DescargarDispo(nroNorm, IdEncomienda, userid);
                }
                else
                {
                    if (txtNroNormativa.Text.Trim() == "")
                        throw new Exception("Debe ingresar el número de Normativa");

                    nroNorm = txtNroNormativa.Text.Trim();
                }

                txtNroNormativa.Text = nroNorm;

                EncomiendaNormativasDTO encomiendaNormativaDTO = new EncomiendaNormativasDTO();
                encomiendaNormativaDTO.IdEncomienda = IdEncomienda;
                encomiendaNormativaDTO.IdTipoNormativa = id_tiponormativa;
                encomiendaNormativaDTO.IdEntidadNormativa = id_entidadnormativa;
                encomiendaNormativaDTO.NroNormativa = nroNorm;
                encomiendaNormativaDTO.CreateUser = userid;

                encomiendaNormativasBL.Update(encomiendaNormativaDTO);

                CargarNormativa(IdEncomienda);

                this.EjecutarScript(updBotonesIngresarNormativa, "hidefrmAgregarNormativa();");

            }
            catch (Exception ex)
            {
                LogError.Write(ex);
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

        protected void ddlEntidadNormativa_SelectedIndexChanged(object sender, EventArgs e)
        {
            MostrarNroDispo();
        }

        protected void ddlTipoNormativa_SelectedIndexChanged(object sender, EventArgs e)
        {
            MostrarNroDispo();
        }


        protected void chkRubroElegido_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                hid_idRubro.Value = "";
                GridViewRow row = (sender as CheckBox).Parent.Parent as GridViewRow;
                CheckBox chkRubroElegido = (CheckBox)row.FindControl("chkRubroElegido");
                List<clsItemGrillaSeleccionarDepositos> depositos = new List<clsItemGrillaSeleccionarDepositos>();
                lblDepositos.Visible = false;
                grdDepositos.Visible = false;
                if (chkRubroElegido.Checked)
                {
                    string CodRub = row.Cells[0].Text;
                    hid_idRubro.Value = CodRub;
                    grdSubRubros.DataSource = null;
                    grdSubRubros.DataBind();
                    RubrosCNDTO rub = rubBL.Get(CodRub).FirstOrDefault();
                    var ds = rubSubrubBL.GetSubRubrosVigentes(rub.IdRubro, IdEncomienda);

                    if (ds.Count() > 0)
                    {
                        grdSubRubros.Visible = true;
                        grdSubRubros.DataSource = ds.ToList();
                        grdSubRubros.DataBind();
                        pnlAgregarSubRub.Style["display"] = "block";
                        pnlSubRubros.Style["display"] = "block";
                    }

                    if (Cache["RubrosDepositos"] != null)
                        Cache.Remove("RubrosDepositos");

                    depositos = CargarDepositos();
                    if (rub.TieneRubroDeposito && depositos.Count > 0)
                    {
                        pnlAgregarSubRub.Style["display"] = "block";
                        pnlSubRubros.Style["display"] = "block";
                        lblDepositos.Visible = true;
                        grdDepositos.Visible = true;
                        if (ds.Count() > 0)
                        {
                            lblpanelSubRubros.Text = "Agregar SubRubros y depositos";
                        }
                        else
                        {
                            grdSubRubros.Visible = false;
                            lblpanelSubRubros.Text = "Agregar depositos";
                        }
                    }

                    if (ds.Count() > 0 || depositos.Count > 0)
                    {
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
                updAgregarNormativa.Update();
                ScriptManager.RegisterStartupScript(updAgregarNormativa, updAgregarNormativa.GetType(), "MostrarNroDispo", "MostrarNroDispo();", true);
            }
            else
            {
                updAgregarNormativa.Update();
                ScriptManager.RegisterStartupScript(updAgregarNormativa, updAgregarNormativa.GetType(), "OcultarNroDispo", "OcultarNroDispo();", true);
            }
        }
        private void BuscarRubros()
        {
            List<ZonasDistritosDTO> lstzonDist = new List<ZonasDistritosDTO>();
            try
            {
                //if (rblZonaDeclarada.SelectedValue.Trim().Length.Equals(0))
                //throw new Exception("Para ingresar rubros en el Anexo es necesario haber seleccionado la Zona antes de ingresar un rubro.");
                decimal dSuperficieDeclarada = 0;
                decimal.TryParse(txtSuperficie.Text, out dSuperficieDeclarada);
                DataList lst = (DataList)updInformacionTramite.FindControl("lstZD");
                foreach (DataListItem item in lstZD.Items)
                {
                    ZonasDistritosDTO zd = new ZonasDistritosDTO();
                    zd.Codigo = ((Label)item.FindControl("lblZD")).Text;
                    int.TryParse(((Label)item.FindControl("lblTipo")).Text, out int id);
                    zd.IdTipo = id;
                    lstzonDist.Add(zd);
                }
                IEnumerable<RubrosCNDTO> ds = encRubrosCNBL.GetRubros(IdEncomienda, dSuperficieDeclarada, txtBuscar.Text.Trim(), lstzonDist);

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

        protected void lstZD_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            // (e.Item.FindControl("lblTipo") as Label).Visible = false;
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
                decimal.TryParse(txtSuperficie.Text, out decimal dSuperficie);

                string Zona = DataBinder.Eval(e.Row.DataItem, "RestriccionZona").ToString();
                bool EsAnterior = (bool)DataBinder.Eval(e.Row.DataItem, "EsAnterior");
                bool TieneNormativa = (bool)DataBinder.Eval(e.Row.DataItem, "TieneNormativa");
                var Codigo = DataBinder.Eval(e.Row.DataItem, "Codigo").ToString();
                var exist = new EncomiendaRubrosCNBL().GetRubros(IdEncomienda).Where(x => x.CodigoRubro.Equals(Codigo)).Any();

                // Si está Permitido en todo o si se especifico alguna normativa o si el tipo de trámite es transferencia
                // se permite el ingreso del rubro.
                if (!exist && (Zona.ToLower() == "tilde.png"
                    || TieneNormativa
                    || IdTipoTramite == (int)Constantes.TipoDeTramite.Transferencia
                    || IdTipoTramite == (int)Constantes.TipoDeTramite.RedistribucionDeUso
                    ))
                {
                    CheckBox chkRubroElegido = (CheckBox)e.Row.Cells[5].FindControl("chkRubroElegido");
                    chkRubroElegido.Enabled = true;
                }

                if (EsAnterior)
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#fff5f3");

            }
        }

        protected void btnIngresarRubros_Click(object sender, EventArgs e)
        {
            try
            {
                GuardarRubros();
                encRubrosCNBL.ActualizarSubTipoExpediente(IdEncomienda);

                CargarDatosTramite(IdEncomienda);
                CargarRubros(IdEncomienda);

                //Actualizo si es ECI
                var sol = new SSITSolicitudesBL();
                var encomienda = encBL.Single(IdEncomienda);
                var Solicitud = sol.Single(encomienda.IdSolicitud);
                if (Solicitud != null)
                {
                    //Actualizo la solicitud
                    Solicitud.EsECI = encomienda.EsECI;
                    sol.Update(Solicitud);
                }

                ScriptManager.RegisterClientScriptBlock(updBotonesAgregarRubros, updBotonesAgregarRubros.GetType(), "hidefrmAgregarRubros", "hidefrmAgregarRubros();", true);
            }
            catch (ValidationException ex)
            {
                LogError.Write(ex);
                lblAlert.Text = ex.Message;
                this.EjecutarScript(updBotonesAgregarRubros, "showfrmAlert();");
            }
            catch (Exception ex)
            {
                LogError.Write(ex);
                lblError.Text = ex.Message;
                this.EjecutarScript(updBotonesAgregarRubros, "showfrmError();");
            }
        }

        protected void btnCerrarSubRubros_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(updBotonesSubRub, updBotonesSubRub.GetType(), "hidefrmAgregarSubRubros", "hidefrmAgregarSubRubros();", true);
            ScriptManager.RegisterClientScriptBlock(updBotonesAgregarRubros, updBotonesAgregarRubros.GetType(), "hidefrmAgregarRubros", "hidefrmAgregarRubros();", true);

            EncomiendaRubrosCNDeposito = new List<Tuple<string, int>>();
        }

        protected void btnIngresarSubRubros_Click(object sender, EventArgs e)
        {
            try
            {
                GuardarRubros();

                if (grdSubRubros.Visible)
                {
                    GuardarSubRubros();
                }
                else if (grdDepositos.Visible)
                {
                    GuardarDepositos();
                }

                encRubrosCNBL.ActualizarSubTipoExpediente(IdEncomienda);

                CargarDatosTramite(IdEncomienda);
                CargarRubros(IdEncomienda);

                //Actualizo si es ECI
                var sol = new SSITSolicitudesBL();
                var encomienda = encBL.Single(IdEncomienda);
                var Solicitud = sol.Single(encomienda.IdSolicitud);
                if (Solicitud != null)
                {
                    //Actualizo la solicitud
                    Solicitud.EsECI = encomienda.EsECI;
                    sol.Update(Solicitud);
                }

                updBotonesSubRub.Update();

                ScriptManager.RegisterClientScriptBlock(updBotonesAgregarRubros, updBotonesAgregarRubros.GetType(), "hidefrmAgregarRubros", "hidefrmAgregarRubros();", true);
                ScriptManager.RegisterClientScriptBlock(updBotonesSubRub, updBotonesSubRub.GetType(), "hidefrmAgregarSubRubros", "hidefrmAgregarSubRubros();", true);
                EncomiendaRubrosCNDeposito = new List<Tuple<string, int>>();
            }
            catch (ValidationException ex)
            {
                LogError.Write(ex);
                lblAlert.Text = ex.Message;
                this.EjecutarScript(updBotonesSubRub, "showfrmAlert();");
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                this.EjecutarScript(updBotonesSubRub, "showfrmError();");
            }
        }

        protected void EliminarRubro(int idEncomiendaRubro)
        {
            var Rubro = encRubrosCNBL.Single(idEncomiendaRubro);
            //elimino los deposito del rubro
            if (Rubro != null)
            {
                Encomienda_RubrosCN_DepositoBL enRubDep = new Encomienda_RubrosCN_DepositoBL();
                enRubDep.DeleteRubDeposito(IdEncomienda, Rubro.IdRubro);

                encRubrosCNBL.Delete(new EncomiendaRubrosCNDTO()
                {
                    IdEncomiendaRubro = idEncomiendaRubro,
                    IdEncomienda = IdEncomienda
                });
            }
        }

        protected void btnEliminarRubro_Click(object sender, EventArgs e)
        {
            try
            {
                int id_encomiendarubro = int.Parse(hid_id_caarubro_eliminar.Value);

                EliminarRubro(id_encomiendarubro);

                if (encRubrosCNBL.GetRubros(IdEncomienda).Count() > 0)
                    encRubrosCNBL.ActualizarSubTipoExpediente(IdEncomienda);

                //Actualizo si es ECI
                SSITSolicitudesBL sol = new SSITSolicitudesBL();
                var encomienda = encBL.Single(IdEncomienda);
                var Solicitud = sol.Single(encomienda.IdSolicitud);
                if (Solicitud != null)
                {
                    //Actualizo la solicitud
                    Solicitud.EsECI = encomienda.EsECI;
                    sol.Update(Solicitud);
                }
                //
                CargarRubros(IdEncomienda);
                CargarDatosTramite(IdEncomienda);
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

        protected void btnEliminarRubroATAnterior_Click(object sender, EventArgs e)
        {
            try
            {
                int id_encomiendarubro = int.Parse(hid_id_caarubro_eliminar_ATAnterior.Value);
                var RubroCPU = encRubros.GetRubrosATAnterior(IdEncomienda);

                if (RubroCPU != null && RubroCPU.Count() > 0)
                    encRubrosATA.Delete(id_encomiendarubro);
                else
                    encRubrosCNATA.Delete(id_encomiendarubro);

                CargarRubros(IdEncomienda);

                this.EjecutarScript(updConfirmarEliminarRubro, "hidefrmConfirmarEliminarRubroATAnterior();");
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                this.EjecutarScript(updBotonesAgregarRubros, "showfrmError();");
            }
        }

        //MODIFICO
        protected void btnEliminarRubroCNATAnterior_Click(object sender, EventArgs e)
        {
            try
            {
                int id_encomiendarubro = int.Parse(hid_id_caarubro_eliminar_ATAnterior.Value);
                //var RubroCPU = encRubros.GetRubrosATAnterior(IdEncomienda);
                var RubroCUR = encRubrosCNBL.GetRubrosCNATAnterior(IdEncomienda);

                if (RubroCUR != null && RubroCUR.Count() > 0)
                    encRubrosCNATA.Delete(id_encomiendarubro);
                else
                    encRubrosATA.Delete(id_encomiendarubro);

                CargarRubros(IdEncomienda);

                this.EjecutarScript(updConfirmarEliminarRubro, "hidefrmConfirmarEliminarRubroCNATAnterior();");
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
                //encRubrosCNBL.InsertRubroUsoNoContemplado(encomiendaRubroDTO);

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
            LinkButton cmdPage = (LinkButton)sender;

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

                //poner estilo sin seleccion a todos los botones
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

        #region "Paginacion grilla AT Anterior"
        protected void grdRubrosATAnterior_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdRubrosATAnterior.PageIndex = e.NewPageIndex;
            BuscarRubrosATAnterior();
        }

        protected void cmdPageATAnterior(object sender, EventArgs e)
        {
            LinkButton cmdPage = (LinkButton)sender;

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
                LinkButton btnAnterior = (LinkButton)fila.Cells[0].FindControl("cmdAnteriorATAnterior");
                LinkButton btnSiguiente = (LinkButton)fila.Cells[0].FindControl("cmdSiguienteATAnterior");

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
                    LinkButton btn = (LinkButton)fila.Cells[0].FindControl("cmdPageATAnterior" + i.ToString());
                    btn.Visible = false;
                }


                if (grid.PageIndex == 0 || grid.PageCount <= 10)
                {
                    // Mostrar 10 botones o el máximo de páginas

                    for (int i = 1; i <= 10; i++)
                    {
                        if (i <= grid.PageCount)
                        {
                            LinkButton btn = (LinkButton)fila.Cells[0].FindControl("cmdPageATAnterior" + i.ToString());
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

                    LinkButton btnPage10 = (LinkButton)fila.Cells[0].FindControl("cmdPageATAnterior10");
                    btnPage10.Visible = true;
                    btnPage10.Text = Convert.ToString(grid.PageIndex + 1);

                    // Ubica los 9 botones hacia la izquierda
                    for (int i = grid.PageIndex - 1; i >= grid.PageIndex - 9; i--)
                    {
                        CantBucles++;
                        if (i >= 0)
                        {
                            LinkButton btn = (LinkButton)fila.Cells[0].FindControl("cmdPageATAnterior" + Convert.ToString(10 - CantBucles));
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
                            LinkButton btn = (LinkButton)fila.Cells[0].FindControl("cmdPageATAnterior" + Convert.ToString(10 + CantBucles));
                            btn.Visible = true;
                            btn.Text = Convert.ToString(i + 1);
                        }
                    }



                }

                //poner estilo sin seleccion a todos los botones
                LinkButton cmdPage;
                string btnPage = "";
                for (int i = 1; i <= 19; i++)
                {
                    btnPage = "cmdPageATAnterior" + i.ToString();
                    cmdPage = (LinkButton)fila.Cells[0].FindControl(btnPage);
                    if (cmdPage != null)
                        cmdPage.CssClass = "btn  btn-sm btn-default";

                }


                // busca el boton por el texto para marcarlo como seleccionado
                string btnText = Convert.ToString(grid.PageIndex + 1);
                foreach (Control ctl in fila.Cells[0].FindControl("pnlpagerATAnterior").Controls)
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

        #region "Paginacion grilla CNAT Anterior"

        protected void grdRubrosCNATAnterior_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdRubrosCNATAnterior.PageIndex = e.NewPageIndex;
            BuscarRubrosCNATAnterior();
        }

        protected void cmdPageCNATAnterior(object sender, EventArgs e)
        {
            LinkButton cmdPage = (LinkButton)sender;

            grdRubrosCNATAnterior.PageIndex = int.Parse(cmdPage.Text) - 1;
            BuscarRubrosCNATAnterior();


        }
        protected void cmdAnteriorCNATAnterior_Click(object sender, EventArgs e)
        {
            grdRubrosCNATAnterior.PageIndex = grdRubrosCNATAnterior.PageIndex - 1;
            BuscarRubrosCNATAnterior();

        }
        protected void cmdSiguienteCNATAnterior_Click(object sender, EventArgs e)
        {
            grdRubrosCNATAnterior.PageIndex = grdRubrosCNATAnterior.PageIndex + 1;
            BuscarRubrosCNATAnterior();
        }

        protected void grdRubrosCNATAnterior_DataBound(object sender, EventArgs e)
        {

            GridView grid = grdRubrosCNATAnterior;
            GridViewRow fila = (GridViewRow)grid.BottomPagerRow;
            if (fila != null)
            {
                LinkButton btnAnterior = (LinkButton)fila.Cells[0].FindControl("cmdAnteriorCNATAnterior");
                LinkButton btnSiguiente = (LinkButton)fila.Cells[0].FindControl("cmdSiguienteCNATAnterior");

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
                    LinkButton btn = (LinkButton)fila.Cells[0].FindControl("cmdPageCNATAnterior" + i.ToString());
                    btn.Visible = false;
                }


                if (grid.PageIndex == 0 || grid.PageCount <= 10)
                {
                    // Mostrar 10 botones o el máximo de páginas

                    for (int i = 1; i <= 10; i++)
                    {
                        if (i <= grid.PageCount)
                        {
                            LinkButton btn = (LinkButton)fila.Cells[0].FindControl("cmdPageCNATAnterior" + i.ToString());
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

                    LinkButton btnPage10 = (LinkButton)fila.Cells[0].FindControl("cmdPageCNATAnterior10");
                    btnPage10.Visible = true;
                    btnPage10.Text = Convert.ToString(grid.PageIndex + 1);

                    // Ubica los 9 botones hacia la izquierda
                    for (int i = grid.PageIndex - 1; i >= grid.PageIndex - 9; i--)
                    {
                        CantBucles++;
                        if (i >= 0)
                        {
                            LinkButton btn = (LinkButton)fila.Cells[0].FindControl("cmdPageCNATAnterior" + Convert.ToString(10 - CantBucles));
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
                            LinkButton btn = (LinkButton)fila.Cells[0].FindControl("cmdPageCNATAnterior" + Convert.ToString(10 + CantBucles));
                            btn.Visible = true;
                            btn.Text = Convert.ToString(i + 1);
                        }
                    }



                }

                //poner estilo sin seleccion a todos los botones
                LinkButton cmdPage;
                string btnPage = "";
                for (int i = 1; i <= 19; i++)
                {
                    btnPage = "cmdPageCNATAnterior" + i.ToString();
                    cmdPage = (LinkButton)fila.Cells[0].FindControl(btnPage);
                    if (cmdPage != null)
                        cmdPage.CssClass = "btn  btn-sm btn-default";

                }


                // busca el boton por el texto para marcarlo como seleccionado
                string btnText = Convert.ToString(grid.PageIndex + 1);
                foreach (Control ctl in fila.Cells[0].FindControl("pnlpagerCNATAnterior").Controls)
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

            var ds = encRubros.GetRubros(IdEncomienda, dSuperficieDeclarada, txtBuscarATAnterior.Text.Trim(), string.Empty); // rblZonaDeclarada.SelectedValue

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
                CheckBox chkRubroElegido = (CheckBox)e.Row.Cells[4].FindControl("chkRubroElegidoATAnterior");
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

                var encRubrosDTO = encRubros.GetByFKIdEncomienda(IdEncomienda);
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

                        encRubros.InsertATAnterior(encomiendaRubroDTO);


                        //Cuando es Ampliacion inserta en las dos grillas.
                        //var userLogued = (Guid)Membership.GetUser().ProviderUserKey;
                        //if (this.IdTipoTramite == (int)Constantes.TipoTramite.AMPLIACION)
                        //{
                        //    encRubrosCNBL.Insert(encomiendaRubroDTO, userLogued);
                        //    encRubrosCNBL.ActualizarSubTipoExpediente(IdEncomienda);
                        //}
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

        //BUSQUEDAS RUBROS CN ANTERIOR
        protected void btnBuscarCNATAnterior_Click(object sender, EventArgs e)
        {
            BuscarRubrosCNATAnterior();
            pnlResultadoBusquedaRubrosCNATAnterior.Style["display"] = "block";
            pnlBotonesAgregarRubrosCNATAnterior.Style["display"] = "block";
            pnlGrupoAgregarRubrosCNATAnterior.Style["display"] = "block";
            pnlBuscarRubrosCNATAnterior.Style["display"] = "none";
        }

        private void BuscarRubrosCNATAnterior()
        {
            List<ZonasDistritosDTO> lstzonDist = new List<ZonasDistritosDTO>();
            decimal dSuperficieDeclarada = 0;
            decimal.TryParse(txtSuperficie.Text, out dSuperficieDeclarada);
            DataList lst = (DataList)updInformacionTramite.FindControl("lstZD");

            foreach (DataListItem item in lstZD.Items)
            {
                ZonasDistritosDTO zd = new ZonasDistritosDTO();
                zd.Codigo = ((Label)item.FindControl("lblZD")).Text;
                int.TryParse(((Label)item.FindControl("lblTipo")).Text, out int id);
                zd.IdTipo = id;
                lstzonDist.Add(zd);
            }


            decimal.TryParse(txtSuperficieCNATAnterior.Text, out dSuperficieDeclarada);

            IEnumerable<RubrosCNDTO> ds = encRubrosCNBL.GetRubros(IdEncomienda, dSuperficieDeclarada, txtBuscarCNATAnterior.Text.Trim(), lstzonDist); // rblZonaDeclarada.SelectedValue           
            grdRubrosCNATAnterior.DataSource = ds.ToList();
            grdRubrosCNATAnterior.DataBind();
        }

        protected void grdRubrosCNATAnterior_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Llenar por default el campo de Superficie.
                decimal dSuperficie = 0;
                decimal.TryParse(txtSuperficieCNATAnterior.Text, out dSuperficie);
                string Zona = DataBinder.Eval(e.Row.DataItem, "RestriccionZona").ToString();
                bool EsAnterior = (bool)DataBinder.Eval(e.Row.DataItem, "EsAnterior");
                bool TieneNormativa = (bool)DataBinder.Eval(e.Row.DataItem, "TieneNormativa");
                CheckBox chkRubroElegido = (CheckBox)e.Row.Cells[4].FindControl("chkRubroElegidoCNATAnterior");
                chkRubroElegido.Enabled = true;

                if (EsAnterior)
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#fff5f3");
            }
        }

        protected void btnnuevaBusquedaCNATAnterior_Click(object sender, EventArgs e)
        {
            txtSuperficieCNATAnterior.Text = hid_Superficie_Local.Value;
            pnlResultadoBusquedaRubrosCNATAnterior.Style["display"] = "none";
            pnlBotonesAgregarRubrosCNATAnterior.Style["display"] = "none";
            pnlGrupoAgregarRubrosCNATAnterior.Style["display"] = "none";
            pnlBuscarRubrosCNATAnterior.Style["display"] = "block";
            pnlBotonesBuscarRubrosCNATAnterior.Style["display"] = "block";
            BotonesBuscarRubrosCNATAnterior.Style["display"] = "block";
            txtBuscarCNATAnterior.Text = "";
            ValidadorAgregarRubrosCNATAnterior.Style["display"] = "none";
            txtBuscarCNATAnterior.Focus();

            updBotonesBuscarRubrosCNATAnterior.Update();
            updBotonesAgregarRubrosCNATAnterior.Update();
        }

        protected void btnIngresarRubrosCNATAnterior_Click(object sender, EventArgs e)
        {
            decimal dSuperficie = 0;
            int CantRubrosElegidos = 0;

            try
            {
                List<string> lstcod_rubro = new List<string>();
                foreach (GridViewRow row in grdRubrosCNATAnterior.Rows)
                {
                    CheckBox chkRubroElegidoATAnterior = (CheckBox)row.FindControl("chkRubroElegidoCNATAnterior");

                    if (chkRubroElegidoATAnterior.Checked)
                    {
                        string scod_rubro = grdRubrosCNATAnterior.DataKeys[row.RowIndex].Values["Codigo"].ToString();
                        lstcod_rubro.Add(scod_rubro);
                    }
                }

                var encRubrosDTO = encRubros.GetByFKIdEncomienda(IdEncomienda);
                lstcod_rubro.AddRange(encRubrosDTO.Select(s => s.CodigoRubro));

                foreach (GridViewRow row in grdRubrosCNATAnterior.Rows)
                {
                    CheckBox chkRubroElegidoCNATAnterior = (CheckBox)row.FindControl("chkRubroElegidoCNATAnterior");

                    if (chkRubroElegidoCNATAnterior.Checked)
                    {
                        string scod_rubro = grdRubrosCNATAnterior.DataKeys[row.RowIndex].Values["Codigo"].ToString();
                        decimal.TryParse(grdRubrosCNATAnterior.DataKeys[row.RowIndex].Values["Superficie"].ToString(), out dSuperficie);

                        EncomiendaRubrosCNDTO encomiendaRubroDTO = new EncomiendaRubrosCNDTO();
                        encomiendaRubroDTO.IdEncomienda = IdEncomienda;
                        encomiendaRubroDTO.SuperficieHabilitar = dSuperficie;
                        encomiendaRubroDTO.CodigoRubro = scod_rubro;

                        //encRubros.InsertATAnterior(encomiendaRubroDTO);
                        encRubrosCNBL.InsertATAnterior(encomiendaRubroDTO);
                        CantRubrosElegidos++;
                    }
                }

                if (CantRubrosElegidos > 0)
                {
                    CargarDatosTramite(IdEncomienda);
                    CargarRubros(IdEncomienda);

                    ScriptManager.RegisterClientScriptBlock(updBotonesAgregarRubrosCNATAnterior, updBotonesAgregarRubros.GetType(), "hidefrmAgregarRubrosCNATAnterior", "hidefrmAgregarRubrosCNATAnterior();", true);
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
                this.EjecutarScript(updBotonesAgregarRubrosCNATAnterior, "showfrmError();");
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
                decimal superficieMaxHabilitar = decimal.Parse(txtSuperficieMaximaRubroActual.Text.Trim());
                EncomiendaRubrosCNDTO encrubDTO = encRubrosCNBL.Single(id_encomiendarubro);
                EncomiendaNormativasBL encomiendaNormativasBL = new EncomiendaNormativasBL();

                var normativa = encomiendaNormativasBL.GetNormativas(IdEncomienda).FirstOrDefault();
                bool TieneNormativa = (normativa != null);
                // bool EsRubroAnteriormenteHabilitado = (encRubrosBL.GetRubrosATAnterior(this.IdEncomienda).Count(x => x.CodigoRubro == encrubDTO.CodigoRubro) > 0);

                // si no esta en la grilla de rubros anteriores (en el caso de ampliación) y valida la superficie segun la zona.
                //if (EsRubroAnteriormenteHabilitado ||
                //    encRubrosBL.ValidarSuperficie(encrubDTO.CodigoRubro, rblZonaDeclarada.SelectedValue, superficieHabilitar, TieneNormativa))
                if (superficieHabilitar > 0 && superficieHabilitar <= superficieMaxHabilitar)
                {
                    encrubDTO.SuperficieHabilitar = superficieHabilitar;
                    encRubrosCNBL.Update(encrubDTO);
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

        protected void grdDepositos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdDepositos.PageIndex = e.NewPageIndex;
            CargarDepositos();
        }

        protected void cmdPageDeposito(object sender, EventArgs e)
        {
            LinkButton cmdPage = (LinkButton)sender;
            grdDepositos.PageIndex = int.Parse(cmdPage.Text) - 1;
            CargarDepositos();

        }

        protected void cmdAnteriorDeposito_Click(object sender, EventArgs e)
        {
            if (grdDepositos.PageIndex > 0)
                grdDepositos.PageIndex = grdDepositos.PageIndex - 1;
            CargarDepositos();
        }

        protected void cmdSiguienteDeposito_Click(object sender, EventArgs e)
        {
            grdDepositos.PageIndex += 1;
            CargarDepositos();
        }

        protected void grdDepositos_DataBound(object sender, EventArgs e)
        {
            GridView grid = grdDepositos;
            GridViewRow fila = grid.BottomPagerRow;
            if (fila != null)
            {
                LinkButton btnAnterior = (LinkButton)fila.Cells[0].FindControl("cmdAnteriorDeposito");
                LinkButton btnSiguiente = (LinkButton)fila.Cells[0].FindControl("cmdSiguienteDeposito");

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
                    LinkButton btn = (LinkButton)fila.Cells[0].FindControl("cmdPageDeposito" + i.ToString());
                    btn.Visible = false;
                }


                if (grid.PageIndex == 0 || grid.PageCount <= 10)
                {
                    // Mostrar 10 botones o el máximo de páginas

                    for (int i = 1; i <= 10; i++)
                    {
                        if (i <= grid.PageCount)
                        {
                            LinkButton btn = (LinkButton)fila.Cells[0].FindControl("cmdPageDeposito" + i.ToString());
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

                    LinkButton btnPage10 = (LinkButton)fila.Cells[0].FindControl("cmdPageDeposito10");
                    btnPage10.Visible = true;
                    btnPage10.Text = Convert.ToString(grid.PageIndex + 1);

                    // Ubica los 9 botones hacia la izquierda
                    for (int i = grid.PageIndex - 1; i >= grid.PageIndex - 9; i--)
                    {
                        CantBucles++;
                        if (i >= 0)
                        {
                            LinkButton btn = (LinkButton)fila.Cells[0].FindControl("cmdPageDeposito" + Convert.ToString(10 - CantBucles));
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
                            LinkButton btn = (LinkButton)fila.Cells[0].FindControl("cmdPageDeposito" + Convert.ToString(10 + CantBucles));
                            btn.Visible = true;
                            btn.Text = Convert.ToString(i + 1);
                        }
                    }



                }

                //poner estilo sin seleccion a todos los botones
                LinkButton cmdPage;
                string btnPage = "";
                for (int i = 1; i <= 19; i++)
                {
                    btnPage = "cmdPageDeposito" + i.ToString();
                    cmdPage = (LinkButton)fila.Cells[0].FindControl(btnPage);
                    if (cmdPage != null)
                        cmdPage.CssClass = "btn  btn-sm btn-default";

                }

                // busca el boton por el texto para marcarlo como seleccionado
                string btnText = Convert.ToString(grid.PageIndex + 1);
                foreach (Control ctl in fila.Cells[0].FindControl("pnlpagerDeposito").Controls)
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

        protected void grdDepositos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridViewRow grw = (GridViewRow)e.Row;
                int deposito = Convert.ToInt32(grdDepositos.DataKeys[grw.RowIndex].Values["IdDeposito"]);
                string idRubro = Convert.ToString(hid_idRubro.Value);
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRubroElegido = (CheckBox)e.Row.Cells[3].FindControl("chkDepositoElegido");
                    if (EncomiendaRubrosCNDeposito.Any(x => x.Item1 == idRubro && x.Item2 == deposito))
                    {
                        chkRubroElegido.Checked = true;
                    }
                    else
                    {
                        chkRubroElegido.Checked = false;
                    }

                    chkRubroElegido.Enabled = ((clsItemGrillaSeleccionarDepositos)e.Row.DataItem).Resultado;
                }
            }
        }

        protected void chkDepositoElegido_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkBox = (sender as CheckBox);
            GridViewRow grw = (GridViewRow)chkBox.Parent.Parent;
            string idRubrodeposito = grdDepositos.DataKeys[grw.RowIndex].Values["IdDeposito"].ToString();
            string idRubro = Convert.ToString(hid_idRubro.Value);

            List<Tuple<string, int>> idDepositos = EncomiendaRubrosCNDeposito;
            Tuple<string, int> dep = new Tuple<string, int>(idRubro, Convert.ToInt32(idRubrodeposito));

            if (chkBox.Checked)
            {
                idDepositos.Add(dep);
            }
            else
            {
                idDepositos.Remove(dep);
            }

            EncomiendaRubrosCNDeposito = idDepositos;
        }

    }
}