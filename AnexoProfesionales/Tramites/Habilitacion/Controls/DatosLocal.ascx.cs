using AnexoProfesionales.Common;
using BusinesLayer.Implementation;
using DataTransferObject;
using System;
using System.Linq;
using System.Web.UI;

namespace AnexoProfesionales.Controls
{
    public partial class DatosLocal : System.Web.UI.UserControl
    {

        public void CargarDatos(int IdEncomienda)
        {
            EncomiendaBL encomiendaBL = new EncomiendaBL();
            CargarDatos(encomiendaBL.Single(IdEncomienda));
        }

        public void CargarDatos(EncomiendaDTO encomienda)
        {
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
            decimal Frente = 0;
            decimal Fondo = 0;
            decimal LateralDerecho = 0;
            decimal LateralIzquierdo = 0;
            double LocalVenta = 0;
            int CantidadOperarios = 0;
            //sobrecarga
            bool SobrecargaCorresponde = false;
            bool SanitariosDentro = true;

            EncomiendaDatosLocalDTO enDl = encomienda.EncomiendaDatosLocalDTO.FirstOrDefault();
            pnlAmpliacionSuperficie.Visible = (encomienda.IdTipoTramite == (int)StaticClass.Constantes.TipoTramite.AMPLIACION);
            if (pnlAmpliacionSuperficie.Visible)
                lblTituloSuperficieHabilitacion.Text = "Superficie Habilitada";

            if (enDl != null)
            {
                SuperficieCubierta = enDl.superficie_cubierta_dl.Value;
                SuperficieDescubierta = enDl.superficie_descubierta_dl.Value;
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
                    SanitariosDentro = false;
                    SanitariosDistancia = enDl.sanitarios_distancia_dl.Value;
                    txtDistanciaSanitarios_dl.Text = SanitariosDistancia.ToString();
                    ScriptManager.RegisterStartupScript(pnlDatosLocal, pnlDatosLocal.GetType(), "script", "ocultarDistanciaSanitarios('show');", true);
                }
                CroquisUbicacion = enDl.croquis_ubicacion_dl;

                SobrecargaCorresponde = enDl.sobrecarga_corresponde_dl.Value;

                CantidadSanitarios = enDl.cantidad_sanitarios_dl.Value;
                SuperficieSanitarios = enDl.superficie_sanitarios_dl.Value;
                Frente = enDl.frente_dl.Value;
                Fondo = enDl.fondo_dl.Value;
                LateralIzquierdo = enDl.lateral_izquierdo_dl.Value;
                LateralDerecho = enDl.lateral_derecho_dl.Value;

                CantidadOperarios = enDl.cantidad_operarios_dl != null ? enDl.cantidad_operarios_dl.Value : 0;
                LocalVenta = enDl.local_venta != null ? enDl.local_venta.Value : 0;

                if (enDl.local_venta != null)
                    txtLocalVenta.Text = LocalVenta.ToString();

                if (enDl.eximido_ley_962.HasValue)
                    rbtnEximidoLey962.Checked = enDl.eximido_ley_962.Value;

                if (enDl.cumple_ley_962.HasValue)
                    rbtnCumpleLey962.Checked = enDl.cumple_ley_962.Value;

                if (enDl.salubridad_especial.HasValue)
                    chkSalubridad.Checked = enDl.salubridad_especial.Value;

                if (pnlAmpliacionSuperficie.Visible)
                {
                    optAmpliacionSuperficie_SI.Checked = (enDl.ampliacion_superficie.HasValue ? enDl.ampliacion_superficie.Value : false);
                    optAmpliacionSuperficie_NO.Checked = !optAmpliacionSuperficie_SI.Checked;

                    if (enDl.superficie_cubierta_amp.HasValue)
                        SuperficieCubiertaAmp = enDl.superficie_cubierta_amp.Value;

                    if (enDl.superficie_descubierta_amp.HasValue)
                        SuperficieDescubiertaAmp = enDl.superficie_descubierta_amp.Value;

                }
                asistentes_SI.Checked = encomienda.Asistentes350;
                asistentes_NO.Checked = !encomienda.Asistentes350;
            }

            productosInflamables_SI.Checked = encomienda.ProductosInflamables;
            productosInflamables_NO.Checked = !encomienda.ProductosInflamables;

            AcogeBeneficio_SI.Checked = encomienda.AcogeBeneficios;
            AcogeBeneficio_NO.Checked = !encomienda.AcogeBeneficios;

            txtSuperficieCubierta.Text = SuperficieCubierta.ToString();
            txtSuperficieDescubierta.Text = SuperficieDescubierta.ToString();
            txtSuperficieTotal.Text = Convert.ToString(SuperficieCubierta + SuperficieDescubierta);

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
            opt5_si.Checked = EstacionamientoBicicleta;
            opt5_no.Checked = !EstacionamientoBicicleta;
            opt3_si.Checked = RedTransitoPesado;
            opt3_no.Checked = !RedTransitoPesado;
            opt4_si.Checked = SobreAvenida;
            opt4_no.Checked = !SobreAvenida;

            txtPisos.Text = Pisos;
            txtParedes.Text = Paredes;
            txtTechos.Text = Techos;
            txtRevestimientos.Text = Revestimientos;

            opt5_dentro.Checked = SanitariosDentro;
            opt5_fuera.Checked = !SanitariosDentro;

            txtCantidadArtefactosSanitarios.Text = CantidadSanitarios.ToString();
            txtSuperficieSanitarios.Text = SuperficieSanitarios.ToString();

            #region EstacionamientoBicicleta solo se muestra en automaticas

            //EncomiendaBL encBL = new EncomiendaBL();
            //EncomiendaDTO EncDto = encBL.Single(enDl.id_encomienda);           
            #endregion

            txtCantOperarios.Text = CantidadOperarios.ToString();

            if (enDl != null)
            {
                if (enDl.dj_certificado_sobrecarga.Value)
                {
                    optsCertificadoSobrecarga_SI.Checked = true;
                    optsCertificadoSobrecarga_NO.Checked = false;
                }
                else
                {
                    optsCertificadoSobrecarga_SI.Checked = false;
                    optsCertificadoSobrecarga_NO.Checked = true;
                }
            }
            else
            {
                optsCertificadoSobrecarga_SI.Checked = false;
                optsCertificadoSobrecarga_NO.Checked = true;
            }
            //updCaracteristicas.Update();
            CargarMapas(encomienda);
        }

        private void CargarMapas(EncomiendaDTO encomienda)
        {
            var lstUbicaciones = encomienda.EncomiendaUbicacionesDTO;
            foreach (var item in lstUbicaciones)
            {
                String dir = "";
                foreach (var p in item.EncomiendaUbicacionesPuertasDTO)
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
        }
    }
}