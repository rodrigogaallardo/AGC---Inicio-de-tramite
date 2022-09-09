using BusinesLayer.Implementation;
using DataTransferObject;
using SSIT.App_Components;
using SSIT.Common;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Security;
using System.Web.UI;
using System.Linq;

namespace SSIT.Solicitud.Transferencia
{
    public partial class DatosLocal : BasePage
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
                ScriptManager.RegisterStartupScript(updDatosLocal, updDatosLocal.GetType(), "init_JS_updDatosLocal", "init_JS_updDatosLocal();", true);
            }


            if (!IsPostBack)
            {
                hid_DecimalSeparator.Value = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
                hid_return_url.Value = Request.Url.AbsoluteUri;
                // ComprobarEncomienda();
            }
        }
            protected void btnCargarDatos_Click(object sender, EventArgs e)
        {
            try
            {
                CargarDatos(IdSolicitud);
                CargarMapas(IdSolicitud);
                ScriptManager.RegisterStartupScript(updCargarDatos, updCargarDatos.GetType(), "finalizarCarga", "finalizarCarga();", true);

            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                ScriptManager.RegisterStartupScript(updCargarDatos, updCargarDatos.GetType(), "finalizarCarga", "finalizarCarga();showfrmError();", true);
            }

        }

        private void CargarDatos(int IdSolicitud)
        {

            TransferenciasDatosLocalBL blEncDl = new TransferenciasDatosLocalBL();

            decimal SuperficieCubierta = 0;
            decimal SuperficieDescubierta = 0;
            decimal DimensionFrente = 0;
            bool LugarCargaDescarga = false;
            bool Estacionamiento = false;
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
            decimal Frente = 0;
            decimal Fondo = 0;
            decimal LateralDerecho = 0;
            decimal LateralIzquierdo = 0;
            int CantidadOperarios = 0;
            decimal LocalVenta = 0;

            TransferenciasDatosLocalDTO enDl = blEncDl.GetByFKIdSolicitud(IdSolicitud).FirstOrDefault();

            if (enDl != null)
            {
                SuperficieCubierta = enDl.SuperficieCubiertaDl.Value;
                SuperficieDescubierta = enDl.SuperficieDescubiertaDl.Value;
                DimensionFrente = enDl.DimesionFrenteDl.Value;

                LugarCargaDescarga = enDl.LugarCargaDescargaDl;
                Estacionamiento = enDl.EstacionamientoDl;
                RedTransitoPesado = enDl.RedTransitoPesadoDl;
                SobreAvenida = enDl.SobreAvenidaDl;

                Pisos = enDl.MaterialesPisosDl;
                Paredes = enDl.MaterialesParedesDl;
                Techos = enDl.MaterialesTechosDl;
                Revestimientos = enDl.MaterialesRevestimientosDl;
                LocalVenta = enDl.Local_venta != null ? enDl.Local_venta.Value : 0;
                optsCertificadoSobrecarga_SI.Checked = enDl.Dj_certificado_sobrecarga;
                optsCertificadoSobrecarga_NO.Checked = !enDl.Dj_certificado_sobrecarga;

                SanitariosUbicacion = enDl.SanitariosUbicacionDl;

                if (SanitariosUbicacion == 2)   //Fuera del local
                {
                    this.EjecutarScript(updCaracteristicas, "objVisibility('tblDistanciaSanitarios_dl','show');");
                    SanitariosDistancia = enDl.SanitariosDistanciaDl.Value;
                }

                CroquisUbicacion = enDl.CroquisUbicacionDl;

                CantidadSanitarios = enDl.CantidadSanitariosDl.Value;
                SuperficieSanitarios = enDl.SuperficieSanitariosDl.Value;
                Frente = enDl.FrenteDl.Value;
                Fondo = enDl.FondoDl.Value;
                LateralIzquierdo = enDl.LateralIzquierdoDl.Value;
                LateralDerecho = enDl.LateralDerechoDl.Value;

                CantidadOperarios = enDl.CantidadOperariosDl != null ? enDl.CantidadOperariosDl.Value : 0;

            }
            txtSuperficieCubierta.Text = SuperficieCubierta.ToString();
            txtSuperficieDescubierta.Text = SuperficieDescubierta.ToString();
            txtSuperficieTotal.Text = Convert.ToString(SuperficieCubierta + SuperficieDescubierta);
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
            if (LocalVenta != 0)
                txtLocalVenta.Text = LocalVenta.ToString();
            txtCantOperarios.Text = CantidadOperarios.ToString();

            updCaracteristicas.Update();
        }

        private void CargarMapas(int idsol)
        {
            TransferenciaUbicacionesBL blEncUbi = new TransferenciaUbicacionesBL();

            var lstUbicaciones = blEncUbi.GetByFKIdSolicitud(IdSolicitud);
            foreach (var item in lstUbicaciones)
            {
                String dir = "";
                foreach (var p in item.Puertas)
                {
                    dir = p.NombreCalle + " " + p.NumeroPuerta;
                    break;
                }
                if (item.Ubicacion.Seccion.HasValue)
                {
                    imgMapa1.ImageUrl = Functions.GetUrlMapa(item.Ubicacion.Seccion.Value, item.Ubicacion.Manzana, item.Ubicacion.Parcela, dir);
                    imgMapa2.ImageUrl = Functions.GetUrlCroquis(item.Ubicacion.Seccion.Value.ToString(), item.Ubicacion.Manzana, item.Ubicacion.Parcela, dir);
                }
                break;
            }
            updDatosLocal.Update();
        }

        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            Guid Userid = (Guid)Membership.GetUser().ProviderUserKey;

            TransferenciasDatosLocalBL blEncDl = new TransferenciasDatosLocalBL();

            var enDl = blEncDl.GetByFKIdSolicitud(IdSolicitud).FirstOrDefault();
            bool alta = false;
            if (enDl == null)
            {
                alta = true;
                enDl = new TransferenciasDatosLocalDTO();
                enDl.IdSolicitud = IdSolicitud;
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
            enDl.SuperficieCubiertaDl = SuperficieCubierta;
            enDl.SuperficieDescubiertaDl = SuperficieDescubierta;
            enDl.DimesionFrenteDl = DimensionFrente;
            enDl.LugarCargaDescargaDl = LugarCargaDescarga;
            enDl.EstacionamientoDl = Estacionamiento;
            enDl.RedTransitoPesadoDl = RedTransitoPesado;
            enDl.SobreAvenidaDl = SobreAvenida;
            enDl.MaterialesPisosDl = Pisos;
            enDl.MaterialesParedesDl = Paredes;
            enDl.MaterialesTechosDl = Techos;
            enDl.MaterialesRevestimientosDl = Revestimientos;
            enDl.SanitariosUbicacionDl = SanitariosUbicacion;
            enDl.SanitariosDistanciaDl = SanitariosDistancia;
            enDl.CantidadSanitariosDl = CantidadSanitarios;
            enDl.SuperficieSanitariosDl = SuperficieSanitarios;
            enDl.FrenteDl = frente;
            enDl.FondoDl = fondo;
            enDl.LateralIzquierdoDl = LateralIzquierdo;
            enDl.LateralDerechoDl = LateralDerecho;
            enDl.CantidadOperariosDl = CantidadOperarios;
            enDl.Local_venta = local_venta;
            enDl.Dj_certificado_sobrecarga = optsCertificadoSobrecarga_SI.Checked ? true : false;

            if (alta)
                blEncDl.Insert(enDl);
            else
            {
                blEncDl.Update(enDl);
            }

            if (hid_return_url.Value.Contains("Editar"))
                Response.Redirect(string.Format("~/" + RouteConfig.VISOR_CPADRON + "{0}", IdSolicitud));
            else
                Response.Redirect(string.Format("~/" + RouteConfig.AGREGAR_RUBROS_CPADRON + "{0}", IdSolicitud));

        }
    }
 }
