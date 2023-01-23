using BusinesLayer.Implementation;
using DataTransferObject;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace AnexoProfesionales.Controls
{
    public partial class RubrosCN : System.Web.UI.UserControl
    {
        public void CargarDatos(EncomiendaDTO encomienda)
        {
            CargarNormativa(encomienda);
            CargarRubros(encomienda);
            CargarSubRubros(encomienda);
            CargarDepositos(encomienda);
            CargarDatosAdicionales(encomienda);
        }

        private void CargarDatosAdicionales(EncomiendaDTO encomienda)
        {
            EncomiendaRubrosCNBL encomiendaRubros = new EncomiendaRubrosCNBL();
            ZonasPlaneamientoBL zonBL = new ZonasPlaneamientoBL();
            EncomiendaUbicacionesBL encUbicBL = new EncomiendaUbicacionesBL();
            List<int> lstZonaPlaneamiento;

            var lstRubrosSolicitud = encomiendaRubros.GetRubros(encomienda.IdEncomienda);

            //if (lstRubrosSolicitud.Where(x => x.OficinaComercial == true).Any())
            //{
            //    string rubros = string.Join(", ", lstRubrosSolicitud.Where(x => x.OficinaComercial == true).Select(s => s.CodigoRubro).ToArray());

            //    string plural1;
            //    string plural2;
            //    if (lstRubrosSolicitud.Where(x => x.OficinaComercial == true).Count() > 1)
            //    {
            //        plural1 = "los rubros";
            //        plural2 = "serán";
            //    }
            //    else
            //    {
            //        plural1 = "el rubro";
            //        plural2 = "será";
            //    }

            //    chkOficinaComercial.Text = string.Format("Se deja constancia que {0} <b>{1}</b> {2} utilizados solo con el fin de oficina comercial", plural1, rubros, plural2);
            //    pnlchkOficinaComercial.Style["display"] = "block";
            //    chkOficinaComercial.Checked = encomienda.DeclaraOficinaComercial;
            //}
            //else
            //{
                chkOficinaComercial.Text = "";
                pnlchkOficinaComercial.Style["display"] = "none";
            //}

            var ecnUbicDTO = encomienda.EncomiendaUbicacionesDTO;
            var zonPla = zonBL.GetZonaPlaneamientoByIdEncomienda(encomienda.IdEncomienda);

            lstZonaPlaneamiento = ecnUbicDTO.Select(s => s.IdZonaPlaneamiento).ToList();
            lstZonaPlaneamiento.AddRange(zonPla.Select(s => s.IdZonaPlaneamiento).ToList());

            if (encUbicBL.esZonaResidencial(lstZonaPlaneamiento))
            {
                pnlchkCumpleArticulo521.Style["display"] = "block";
                chkCumpleArticulo521.Checked = encomienda.CumpleArticulo521;
            }
            else
                pnlchkCumpleArticulo521.Style["display"] = "none";

            
            lblObservacionesRubros.Text = (!string.IsNullOrEmpty(encomienda.ObservacionesRubros) ? encomienda.ObservacionesRubros : String.Empty);
            pnlObservacionesRubros.Visible = lblObservacionesRubros.Text.Length > 0;
            
            lblObservacionesRubrosATAnterior.Text = (!string.IsNullOrEmpty(encomienda.ObservacionesRubrosATAnterior) ? encomienda.ObservacionesRubrosATAnterior : String.Empty);
            pnlObservacionesRubrosATAnterior.Visible = lblObservacionesRubrosATAnterior.Text.Length > 0;

        }

        private void CargarNormativa(EncomiendaDTO encomienda)
        {

            lblTipoNormativa.Text = "";
            lblEntidadNormativa.Text = "";
            lblNroNormativa.Text = "";

            var normativa = encomienda.EncomiendaNormativasDTO.FirstOrDefault(); 

            if (normativa != null)
            {
                lblTipoNormativa.Text = normativa.TipoNormativaDTO.Nombre;
                lblEntidadNormativa.Text = normativa.EntidadNormativaDTO.Nombre;
                lblNroNormativa.Text = normativa.NroNormativa;
                pnlNormativa.Visible = true;
            }
            else
            {
                pnlNormativa.Visible = false;
            }

        }

        private void CargarRubros(EncomiendaDTO encomienda)
        {
            EncomiendaRubrosCNBL encomiendaRubros = new EncomiendaRubrosCNBL();
            EncomiendaRubrosBL encRubros = new EncomiendaRubrosBL();
            SSITSolicitudesBL solBL = new SSITSolicitudesBL();
            TransferenciasSolicitudesBL trBL = new TransferenciasSolicitudesBL();
            List<EncomiendaRubrosDTO> lstRubrosSolicitudATAnterior = new List<EncomiendaRubrosDTO>();
            List<EncomiendaRubrosCNDTO> lstRubrosCNSolicitudATAnterior = new List<EncomiendaRubrosCNDTO>();

            int nroSolReferencia = 0;
            int.TryParse(ConfigurationManager.AppSettings["NroSolicitudReferencia"], out nroSolReferencia);


            lstRubrosSolicitudATAnterior = encRubros.GetRubrosATAnterior(encomienda.IdEncomienda).ToList();
            lstRubrosCNSolicitudATAnterior = encomiendaRubros.GetRubrosCNATAnterior(encomienda.IdEncomienda).ToList();


            if (lstRubrosSolicitudATAnterior.Count > 0)
                TituloRubrosAnterioresCN.Visible = true;

            if (lstRubrosCNSolicitudATAnterior.Count > 0)
                TituloRubrosAnterioresAT.Visible = true;

            var lstRubrosSolicitud = encomiendaRubros.GetRubros(encomienda.IdEncomienda);          

            grdRubrosIngresados.DataSource = lstRubrosSolicitud.ToList();
            grdRubrosIngresados.DataBind();

            // Ahora muestra siempre los cuadros vacios
            grdRubrosIngresadosATAnterior.DataSource = lstRubrosSolicitudATAnterior.ToList();
            grdRubrosIngresadosATAnterior.DataBind();
            grdRubrosCNIngresadosATAnterior.DataSource = lstRubrosCNSolicitudATAnterior.ToList();
            grdRubrosCNIngresadosATAnterior.DataBind();

            pnlRubrosAnteriores.Visible = true;
            pnlRubrosCNAnteriores.Visible = true;
        }

        private void CargarSubRubros(EncomiendaDTO encomienda)
        {
            RubrosCNSubrubrosBL subrubros = new RubrosCNSubrubrosBL();
            var lstSubRubrosSolicitud = subrubros.GetSubRubrosByEncomienda(encomienda.IdEncomienda);

            grdSubRubros.DataSource = lstSubRubrosSolicitud.ToList();
            grdSubRubros.DataBind();

            pnlSubRubros.Visible = (lstSubRubrosSolicitud.Count() > 0);
        }

        private void CargarDepositos(EncomiendaDTO encomienda)
        {
            var lstDepositos = encomienda.Encomienda_RubrosCN_DepositoDTO.ToList();
            grdDepositos.DataSource = lstDepositos.ToList();
            grdDepositos.DataBind();

            pnlDepositos.Visible = (lstDepositos.Count() > 0);
        }
    }
}