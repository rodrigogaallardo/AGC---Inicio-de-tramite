using BusinesLayer.Implementation;
using DataTransferObject;
using SSIT.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSIT.Solicitud.Consulta_Padron.Controls
{
    public partial class DatosLocal : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void CargarDatos(ConsultaPadronSolicitudesDTO consultaPadron)
        {

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
            decimal LocalVenta = 0;
            int CantidadOperarios = 0;
            bool SanitariosDentro = true;


            ConsultaPadronDatosLocalDTO enDl = consultaPadron.DatosLocal.FirstOrDefault();

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

                SanitariosUbicacion = enDl.SanitariosUbicacionDl;

                if (SanitariosUbicacion == 2)   //Fuera del local
                {
                    SanitariosDentro = false;
                    SanitariosDistancia = enDl.SanitariosDistanciaDl.Value;
                    txtDistanciaSanitarios_dl.Text = SanitariosDistancia.ToString();
                    ScriptManager.RegisterStartupScript(pnlDatosLocal, pnlDatosLocal.GetType(), "script", "ocultarDistanciaSanitarios('show');", true);
                    //objVisibility('tblDistanciaSanitarios_dl','show');"
                }

                CroquisUbicacion = enDl.CroquisUbicacionDl;

                CantidadSanitarios = enDl.CantidadSanitariosDl.Value;
                SuperficieSanitarios = enDl.SuperficieSanitariosDl.Value;
                Frente = enDl.FrenteDl.Value;
                Fondo = enDl.FondoDl.Value;
                LateralIzquierdo = enDl.LateralIzquierdoDl.Value;
                LateralDerecho = enDl.LateralDerechoDl.Value;

                CantidadOperarios = enDl.CantidadOperariosDl.HasValue ? enDl.CantidadOperariosDl.Value : 0;
                LocalVenta = enDl.Local_venta != null ? enDl.Local_venta.Value : 0;

            }
            txtSuperficieCubierta.Text = SuperficieCubierta.ToString();
            txtSuperficieDescubierta.Text = SuperficieDescubierta.ToString();
            txtSuperficieTotal.Text = Convert.ToString(SuperficieCubierta + SuperficieDescubierta);
            txtDimensionFrente.Text = DimensionFrente.ToString();
            txtFrente.Text = Frente.ToString();
            txtFondo.Text = Fondo.ToString();
            txtLatIzq.Text = LateralIzquierdo.ToString();
            txtLatDer.Text = LateralDerecho.ToString();
            txtLocalVenta.Text = LocalVenta.ToString();

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

            opt5_dentro.Checked = SanitariosDentro;
            opt5_fuera.Checked = !SanitariosDentro;

            txtCantidadArtefactosSanitarios.Text = CantidadSanitarios.ToString();
            txtSuperficieSanitarios.Text = SuperficieSanitarios.ToString();

            txtCantOperarios.Text = CantidadOperarios.ToString();

            //updCaracteristicas.Update();
            CargarMapas(consultaPadron);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idEncomienda"></param>
        private void CargarMapas(ConsultaPadronSolicitudesDTO consultaPadron)
        {
            ConsultaPadronUbicacionesBL blEncUbi = new ConsultaPadronUbicacionesBL(); 
            ConsultaPadronUbicacionesPuertasBL blEncUP = new ConsultaPadronUbicacionesPuertasBL();

            if (consultaPadron.Ubicaciones != null)
            {
                string dir = "";
                var item = consultaPadron.Ubicaciones.FirstOrDefault();
                if (item != null  && item.Ubicacion.Seccion != null)
              
                {
                    var puerta = item.Puertas.FirstOrDefault ();
                    dir = puerta.NombreCalle + " " + puerta.NumeroPuerta;
                    imgMapa1.ImageUrl = Functions.GetUrlMapa(item.Ubicacion.Seccion.Value, item.Ubicacion.Manzana, item.Ubicacion.Parcela, dir);
                    imgMapa2.ImageUrl = Functions.GetUrlCroquis(item.Ubicacion.Seccion.Value.ToString(), item.Ubicacion.Manzana, item.Ubicacion.Parcela, dir);                    
                }
                else
                {
                    imgMapa1.ImageUrl = "~/Content/img/app/ImageNotFound.png";
                    imgMapa2.ImageUrl = "~/Content/img/app/ImageNotFound.png";
                }             
            }            
        }
    }
}