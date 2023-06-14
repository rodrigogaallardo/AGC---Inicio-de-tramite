using AnexoProfesionales.App_Components;
using AnexoProfesionales.Common;
using BusinesLayer.Implementation;
using DataTransferObject;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Security;
using System.Web.UI;


namespace AnexoProfesionales
{
    public partial class DatosLocal : BasePage
    {

        EncomiendaBL encBL = new EncomiendaBL();

        private int id_encomienda
        {
            get
            {
                int ret = 0;
                int.TryParse(hid_id_encomienda.Value, out ret);
                return ret;
            }
            set
            {
                hid_id_encomienda.Value = value.ToString();
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {

            ScriptManager sm = ScriptManager.GetCurrent(this);

            if (sm.IsInAsyncPostBack)
            {
                ScriptManager.RegisterStartupScript(updDatosLocal, updDatosLocal.GetType(), "init_JS_updDatosLocal", "init_JS_updDatosLocal();", true);
                ScriptManager.RegisterStartupScript(updAmpliacionSuperficie, updAmpliacionSuperficie.GetType(), "init_Js_updAmpliacionSuperficie", "init_Js_updAmpliacionSuperficie();", true);
            }


            if (!IsPostBack)
            {
                hid_DecimalSeparator.Value = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
                hid_return_url.Value = Request.Url.AbsoluteUri;
                ComprobarEncomienda();
                Titulo.CargarDatos(id_encomienda, "Datos del Local");
            }


        }

        private void ComprobarEncomienda()
        {
            if (Page.RouteData.Values["id_encomienda"] != null)
            {
                this.id_encomienda = Convert.ToInt32(Page.RouteData.Values["id_encomienda"].ToString());

                var enc = encBL.Single(id_encomienda);
                if (enc != null)
                {
                    /*Falta el userID y hacer overload de getuserid con el tipo de tramite*/
                    Guid userid_solicitud = (Guid)Membership.GetUser().ProviderUserKey;

                    if (userid_solicitud != enc.CreateUser)
                        Server.Transfer("~/Errores/Error3002.aspx");
                    else
                    {
                        if (!(enc.IdEstado == (int)Constantes.Encomienda_Estados.Incompleta ||
                                enc.IdEstado == (int)Constantes.Encomienda_Estados.Completa))
                        {
                            Server.Transfer("~/Errores/Error3003.aspx");
                        }

                        //140723: JADHE 54604 - AT - REQ AMP RDU Carga Heredada - Poder Modificar Datos en AT
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
                CargarDatos(id_encomienda);
                CargarMapas(id_encomienda);
                ScriptManager.RegisterStartupScript(updCargarDatos, updCargarDatos.GetType(), "finalizarCarga", "finalizarCarga();", true);

            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                ScriptManager.RegisterStartupScript(updCargarDatos, updCargarDatos.GetType(), "finalizarCarga", "finalizarCarga();showfrmError();", true);
            }

        }

        private void CargarDatos(int idEncomienda)
        {

            EncomiendaDatosLocalBL blEncDl = new EncomiendaDatosLocalBL();

            decimal SuperficieCubierta = 0;
            decimal SuperficieDescubierta = 0;
            decimal SuperficieCubiertaAmp = 0.00m;
            decimal SuperficieDescubiertaAmp = 0.00m;

            decimal DimensionFrente = 0;
            bool LugarCargaDescarga = false;
            bool Estacionamiento = false;
            bool EstacionamientoBicicleta = false;
            bool RedTransitoPesado = false;
            bool SobreAvenida = false;
            string Pisos = "";
            string Paredes = "";
            string Techos = "";
            string Revestimientos = "";
            int SanitariosUbicacion = 0;
            decimal SanitariosDistancia = 0;
            string CroquisUbicacion = "";
            int CantidadSanitarios = 0;
            decimal SuperficieSanitarios = 0;
            //decimal SuperficieSemicubierta = 0;
            decimal Frente = 0;
            decimal Fondo = 0;
            decimal LateralDerecho = 0;
            decimal LateralIzquierdo = 0;
            double LocalVenta = 0;
            int CantidadOperarios = 0;

            txtSuperficieCubiertaAmp.Text = SuperficieCubiertaAmp.ToString();
            txtSuperficieDescubiertaAmp.Text = SuperficieDescubiertaAmp.ToString();

            //sobrecarga
            bool SobrecargaCorresponde = false;

            EncomiendaDatosLocalDTO enDl = blEncDl.GetByFKIdEncomienda(idEncomienda);

            EncomiendaDTO EncDto = encBL.Single(idEncomienda);


            pnlAmpliacionSuperficie.Visible = (EncDto.IdTipoTramite == (int)Constantes.TipoTramite.AMPLIACION);
            if (pnlAmpliacionSuperficie.Visible)
                lblTituloSuperficieHabilitacion.Text = "Superficie Habilitada";

            if (enDl != null)
            {
                SuperficieCubierta = enDl.superficie_cubierta_dl.Value;
                SuperficieDescubierta = enDl.superficie_descubierta_dl.Value;
                //SuperficieSemicubierta = enDl.superficie_semicubierta_dl.Value;
                DimensionFrente = enDl.dimesion_frente_dl.Value;

                LugarCargaDescarga = enDl.lugar_carga_descarga_dl;
                Estacionamiento = enDl.estacionamiento_dl;
                EstacionamientoBicicleta = enDl.estacionamientoBicicleta_dl;
                RedTransitoPesado = enDl.red_transito_pesado_dl;
                SobreAvenida = enDl.sobre_avenida_dl;

                Pisos = enDl.materiales_pisos_dl;
                Paredes = enDl.materiales_paredes_dl;
                Techos = enDl.materiales_techos_dl;
                Revestimientos = enDl.materiales_revestimientos_dl;


                SanitariosUbicacion = enDl.sanitarios_ubicacion_dl.Value;

                if (SanitariosUbicacion == 2)   //Fuera del local
                {
                    this.EjecutarScript(updCaracteristicas, "objVisibility('tblDistanciaSanitarios_dl','show');");
                    SanitariosDistancia = enDl.sanitarios_distancia_dl.Value;
                }

                CroquisUbicacion = enDl.croquis_ubicacion_dl;


                SobrecargaCorresponde = enDl.dj_certificado_sobrecarga == null ? false : enDl.dj_certificado_sobrecarga.Value;
                if (SobrecargaCorresponde)
                {
                    divcheckDeclaracion.Visible = true;
                    divcheckDeclaracion.Attributes.Add("Style", "color: Gray;");
                }

                CantidadSanitarios = enDl.cantidad_sanitarios_dl.Value;
                SuperficieSanitarios = enDl.superficie_sanitarios_dl.Value;
                Frente = enDl.frente_dl.Value;
                Fondo = enDl.fondo_dl.Value;
                LateralIzquierdo = enDl.lateral_izquierdo_dl.Value;
                LateralDerecho = enDl.lateral_derecho_dl.Value;

                CantidadOperarios = enDl.cantidad_operarios_dl != null ? enDl.cantidad_operarios_dl.Value : 0;
                LocalVenta = enDl.local_venta != null ? enDl.local_venta.Value : 0;

                if (enDl.eximido_ley_962.HasValue)
                    rbtnEximidoLey962.Checked = enDl.eximido_ley_962.Value;

                if (enDl.cumple_ley_962.HasValue)
                    rbtnCumpleLey962.Checked = enDl.cumple_ley_962.Value;

                if (enDl.salubridad_especial.HasValue)
                    chk.Checked = enDl.salubridad_especial.Value;


                if (pnlAmpliacionSuperficie.Visible)
                {
                    optAmpliacionSuperficie_SI.Checked = (enDl.ampliacion_superficie.HasValue ? enDl.ampliacion_superficie.Value : false);
                    optAmpliacionSuperficie_NO.Checked = !optAmpliacionSuperficie_SI.Checked;

                    if (enDl.superficie_cubierta_amp.HasValue)
                        SuperficieCubiertaAmp = enDl.superficie_cubierta_amp.Value;

                    if (enDl.superficie_descubierta_amp.HasValue)
                        SuperficieDescubiertaAmp = enDl.superficie_descubierta_amp.Value;

                    txtSuperficieCubiertaAmp.Enabled = optAmpliacionSuperficie_SI.Checked;
                    txtSuperficieDescubiertaAmp.Enabled = optAmpliacionSuperficie_SI.Checked;

                }
                asistentes_SI.Checked = EncDto.Asistentes350;
                asistentes_NO.Checked = !EncDto.Asistentes350;
            }

            productosInflamables_SI.Checked = EncDto.ProductosInflamables;
            productosInflamables_NO.Checked = !EncDto.ProductosInflamables;

            AcogeBeneficio_SI.Checked = (bool)EncDto.AcogeBeneficios;
            AcogeBeneficio_NO.Checked = (bool)!EncDto.AcogeBeneficios;

            if (!rbtnCumpleLey962.Checked && !rbtnEximidoLey962.Checked)
            {
                rbtnCumpleLey962.Checked = true;
            }
            txtSuperficieCubierta.Text = SuperficieCubierta.ToString();
            txtSuperficieDescubierta.Text = SuperficieDescubierta.ToString();
            txtSuperficieTotal.Text = Convert.ToString(SuperficieCubierta + SuperficieDescubierta);
            // txtSupSemiCubierta

            txtSuperficieCubiertaAmp.Text = SuperficieCubiertaAmp.ToString();
            txtSuperficieDescubiertaAmp.Text = SuperficieDescubiertaAmp.ToString();
            txtSuperficieTotalAmp.Text = Convert.ToString(SuperficieCubiertaAmp + SuperficieDescubiertaAmp);

            txtDimensionFrente.Text = DimensionFrente.ToString();
            txtFrente.Text = Frente.ToString();
            txtFondo.Text = Fondo.ToString();
            txtLatIzq.Text = LateralIzquierdo.ToString();
            txtLatDer.Text = LateralDerecho.ToString();

            opt1_si.Checked = LugarCargaDescarga;
            opt1_no.Checked = !LugarCargaDescarga;
            opt2_si.Checked = Estacionamiento;
            opt2_no.Checked = !Estacionamiento;
            opt3_si.Checked = RedTransitoPesado;
            opt3_no.Checked = !RedTransitoPesado;
            opt4_si.Checked = SobreAvenida;
            opt4_no.Checked = !SobreAvenida;
            opt5_si.Checked = EstacionamientoBicicleta;
            opt5_no.Checked = !EstacionamientoBicicleta;

            txtPisos.Text = Pisos;
            txtParedes.Text = Paredes;
            txtTechos.Text = Techos;
            txtRevestimientos.Text = Revestimientos;

            switch (SanitariosUbicacion)
            {
                case 1:
                    opt5_dentro.Checked = true;
                    break;
                case 2:
                    opt5_fuera.Checked = true;
                    txtDistanciaSanitarios_dl.Text = SanitariosDistancia.ToString();
                    break;
            }


            txtCantidadArtefactosSanitarios.Text = CantidadSanitarios.ToString();
            txtSuperficieSanitarios.Text = SuperficieSanitarios.ToString();

            if (CantidadSanitarios > 0)
                txtSuperficieSanitarios.Enabled = true;

            txtLocalVenta.Text = LocalVenta.ToString();

            txtCantOperarios.Text = CantidadOperarios.ToString();

            optsCertificadoSobrecarga_SI.Checked = SobrecargaCorresponde;
            optsCertificadoSobrecarga_NO.Checked = !SobrecargaCorresponde;

            checkBDJCertificado.Checked = optsCertificadoSobrecarga_SI.Checked;
            divcheckDeclaracion.Visible = optsCertificadoSobrecarga_SI.Checked;

            if (rbtnCumpleLey962.Checked)
                txtSuperficieSanitarios.Enabled = true;
            updCaracteristicas.Update();
        }

        private void CargarMapas(int idEncomienda)
        {
            EncomiendaUbicacionesBL blEncUbi = new EncomiendaUbicacionesBL();
            var lstUbicaciones = blEncUbi.GetByFKIdEncomienda(id_encomienda);
            foreach (var item in lstUbicaciones)
            {
                var query = item.EncomiendaUbicacionesPuertasDTO;
                String dir = "";
                foreach (var p in query)
                {
                    dir = p.NombreCalle + " " + p.NroPuerta;
                    break;
                }
                if (item.Ubicacion.Seccion != null)
                {
                    imgMapa1.ImageUrl = Functions.GetUrlMapa(item.Ubicacion.Seccion.Value, item.Ubicacion.Manzana, item.Ubicacion.Parcela, dir);
                    imgMapa2.ImageUrl = Functions.GetUrlCroquis(item.Ubicacion.Seccion.Value, item.Ubicacion.Manzana, item.Ubicacion.Parcela, dir);
                }
                break;
            }
            updDatosLocal.Update();

        }

        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            Guid Userid = (Guid)Membership.GetUser().ProviderUserKey;

            EncomiendaDatosLocalBL blEncDl = new EncomiendaDatosLocalBL();

            EncomiendaDatosLocalDTO enDl = blEncDl.GetByFKIdEncomienda(id_encomienda);
            bool alta = false;
            if (enDl == null)
            {
                alta = true;
                enDl = new EncomiendaDatosLocalDTO();
                enDl.id_encomienda = id_encomienda;
                enDl.CreateUser = Userid;
                enDl.CreateDate = DateTime.Now;
            }
            else
            {
                enDl.LastUpdateUser = Userid;
                enDl.LastUpdateDate = DateTime.Now;
            }

            decimal SuperficieCubierta;
            decimal SuperficieDescubierta;
            decimal DimensionFrente;
            bool LugarCargaDescarga = false;
            bool Estacionamiento = false;
            bool EstacionamientoBicicleta = false;
            bool RedTransitoPesado = false;
            bool SobreAvenida = false;
            string Pisos;
            string Paredes;
            string Techos;
            string Revestimientos;
            int SanitariosUbicacion = 0;
            decimal SanitariosDistancia = 0;
            int CantidadOperarios = 0;
            decimal local_venta = 0;

            decimal.TryParse(txtSuperficieCubierta.Text, out SuperficieCubierta);
            decimal.TryParse(txtSuperficieDescubierta.Text, out SuperficieDescubierta);
            decimal.TryParse(txtDimensionFrente.Text, out DimensionFrente);

            LugarCargaDescarga = opt1_si.Checked;
            Estacionamiento = opt2_si.Checked;
            EstacionamientoBicicleta = opt5_si.Checked;
            RedTransitoPesado = opt3_si.Checked;
            SobreAvenida = opt4_si.Checked;
            Pisos = txtPisos.Text.Trim();
            Paredes = txtParedes.Text.Trim();
            Techos = txtTechos.Text.Trim();
            Revestimientos = txtRevestimientos.Text.Trim();

            if (opt5_dentro.Checked)
                SanitariosUbicacion = 1;

            if (opt5_fuera.Checked)
            {
                SanitariosUbicacion = 2;
                decimal.TryParse(txtDistanciaSanitarios_dl.Text, out SanitariosDistancia);
            }

            int CantidadSanitarios = 0;
            decimal SuperficieSanitarios = 0;
            decimal frente = 0;
            decimal fondo = 0;
            decimal LateralIzquierdo = 0;
            decimal LateralDerecho = 0;

            int.TryParse(txtCantidadArtefactosSanitarios.Text, out CantidadSanitarios);
            decimal.TryParse(txtSuperficieSanitarios.Text, out SuperficieSanitarios);

            decimal.TryParse(txtFrente.Text, out frente);
            decimal.TryParse(txtFondo.Text, out fondo);
            decimal.TryParse(txtLatIzq.Text, out LateralIzquierdo);
            decimal.TryParse(txtLatDer.Text, out LateralDerecho);

            int.TryParse(txtCantOperarios.Text, out CantidadOperarios);
            if (txtLocalVenta.Visible)
                decimal.TryParse(txtLocalVenta.Text, out local_venta);
            bool SobrecargaCorresponde = optsCertificadoSobrecarga_SI.Checked;

            enDl.superficie_cubierta_dl = SuperficieCubierta;
            enDl.superficie_descubierta_dl = SuperficieDescubierta;
            enDl.dimesion_frente_dl = DimensionFrente;
            enDl.lugar_carga_descarga_dl = LugarCargaDescarga;
            enDl.estacionamiento_dl = Estacionamiento;
            enDl.estacionamientoBicicleta_dl = EstacionamientoBicicleta;
            enDl.red_transito_pesado_dl = RedTransitoPesado;
            enDl.sobre_avenida_dl = SobreAvenida;
            enDl.materiales_pisos_dl = Pisos;
            enDl.materiales_paredes_dl = Paredes;
            enDl.materiales_techos_dl = Techos;
            enDl.materiales_revestimientos_dl = Revestimientos;
            enDl.sanitarios_ubicacion_dl = SanitariosUbicacion;
            enDl.sanitarios_distancia_dl = SanitariosDistancia;
            enDl.cantidad_sanitarios_dl = CantidadSanitarios;
            enDl.superficie_sanitarios_dl = SuperficieSanitarios;
            enDl.frente_dl = frente;
            enDl.fondo_dl = fondo;
            enDl.lateral_izquierdo_dl = LateralIzquierdo;
            enDl.lateral_derecho_dl = LateralDerecho;
            enDl.cantidad_operarios_dl = CantidadOperarios;
            enDl.local_venta = Convert.ToDouble(local_venta);
            enDl.sobrecarga_corresponde_dl = false;
            enDl.cumple_ley_962 = rbtnCumpleLey962.Checked;
            enDl.eximido_ley_962 = rbtnEximidoLey962.Checked;
            enDl.salubridad_especial = chk.Checked;
            enDl.dj_certificado_sobrecarga = checkBDJCertificado.Checked;

            if (pnlAmpliacionSuperficie.Visible)
            {
                decimal SuperficieCubiertaAmp = 0;
                decimal SuperficieDescubiertaAmp = 0;
                decimal.TryParse(txtSuperficieCubiertaAmp.Text, out SuperficieCubiertaAmp);
                decimal.TryParse(txtSuperficieDescubiertaAmp.Text, out SuperficieDescubiertaAmp);

                enDl.ampliacion_superficie = optAmpliacionSuperficie_SI.Checked;
                enDl.superficie_cubierta_amp = SuperficieCubiertaAmp;
                enDl.superficie_descubierta_amp = SuperficieDescubiertaAmp;
            }


            if (alta)
                blEncDl.Insert(enDl);
            else
            {
                blEncDl.Update(enDl);
                if (!SobrecargaCorresponde)
                {
                    EncomiendaCertificadoSobrecargaBL blEncS = new EncomiendaCertificadoSobrecargaBL();
                    EncomiendaCertificadoSobrecargaDTO certDTO = blEncS.GetByFKIdEncomiendaDatosLocal(enDl.id_encomiendadatoslocal);

                    EncomiendaSobrecargaDetalle1BL blEncS1 = new EncomiendaSobrecargaDetalle1BL();
                    EncomiendaSobrecargaDetalle2BL blEncS2 = new EncomiendaSobrecargaDetalle2BL();
                    IEnumerable<EncomiendaSobrecargaDetalle1DTO> list1;
                    IEnumerable<EncomiendaSobrecargaDetalle2DTO> list2;

                    int id_sobrecarga = 0;

                    //Elimino lo ingresado
                    if (certDTO != null)
                    {
                        id_sobrecarga = certDTO.id_sobrecarga;
                        list1 = blEncS1.GetByFKIdSobrecarga(certDTO.id_sobrecarga);
                        foreach (EncomiendaSobrecargaDetalle1DTO s1 in list1)
                        {
                            list2 = blEncS2.GetByFKIdSobrecargaDetalle1(s1.id_sobrecarga_detalle1);
                            foreach (EncomiendaSobrecargaDetalle2DTO s2 in list2)
                                blEncS2.Delete(s2);
                            blEncS1.Delete(s1);
                        }
                    }
                }
            }

            EncomiendaBL encBL = new EncomiendaBL();
            EncomiendaDTO encDTO = new EncomiendaDTO();
            encDTO = encBL.Single(id_encomienda);
            encDTO.Asistentes350 = asistentes_SI.Checked;
            encDTO.ProductosInflamables = productosInflamables_SI.Checked;
            encDTO.AcogeBeneficios = AcogeBeneficio_SI.Checked;
            encBL.Update(encDTO);

            //llamo al actualizart porq si cambio el local de venta e impacta en el tipo
            var repoRubro = new EncomiendaRubrosCNBL();
            if (encDTO.EncomiendaRubrosCNDTO.Count > 0)
                repoRubro.ActualizarSubTipoExpediente(id_encomienda);




            Response.Redirect(string.Format("~/" + RouteConfig.VISOR_ENCOMIENDA + "{0}", id_encomienda));

            //if (hid_return_url.Value.Contains("Editar"))
            //{
            //    if (SobrecargaCorresponde)
            //        Response.Redirect(string.Format("~/" + RouteConfig.EDITAR_ENCOMIENDA_CERTIFICADOSOBRECARGA + "{0}", id_encomienda));
            //    else
            //        Response.Redirect(string.Format("~/" + RouteConfig.VISOR_ENCOMIENDA + "{0}", id_encomienda));
            //}
            //else
            //{
            //    if (SobrecargaCorresponde)
            //        Response.Redirect(string.Format("~/" + RouteConfig.AGREGAR_ENCOMIENDA_CERTIFICADOSOBRECARGA + "{0}", id_encomienda));
            //    else
            //        Response.Redirect(string.Format("~/" + RouteConfig.AGREGAR_ENCOMIENDA_RUBROS + "{0}", id_encomienda));
            //}

        }

        protected void optsCertificadoSobrecarga_SI_CheckedChanged(object sender, EventArgs e)
        {
            divcheckDeclaracion.Visible = true;
            this.checkBDJCertificado.Checked = true;
        }

        protected void optsCertificadoSobrecarga_NO_CheckedChanged(object sender, EventArgs e)
        {
            divcheckDeclaracion.Visible = false;
            checkBDJCertificado.Checked = false;
        }

        protected void checkBDJCertificado_CheckedChanged(object sender, EventArgs e)
        {
            //this.checkBDJCertificado.Checked = true;
        }
    }
}
