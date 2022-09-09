using BusinesLayer.Implementation;
using DataTransferObject;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnexoProfesionales.Controls
{
    public partial class Rubros : System.Web.UI.UserControl
    {
        public void CargarDatos(EncomiendaDTO encomienda)
        {
            CargarNormativa(encomienda);
            CargarRubros(encomienda);
            CargarDatosAdicionales(encomienda);
        }

        private void CargarDatosAdicionales(EncomiendaDTO encomienda)
        {
            EncomiendaRubrosBL encomiendaRubros = new EncomiendaRubrosBL();
            ZonasPlaneamientoBL zonBL = new ZonasPlaneamientoBL();
            EncomiendaUbicacionesBL encUbicBL = new EncomiendaUbicacionesBL();
            List<int> lstZonaPlaneamiento;

            var lstRubrosSolicitud = encomiendaRubros.GetRubros(encomienda.IdEncomienda);

            if (lstRubrosSolicitud.Where(x => x.OficinaComercial == true).Any())
            {
                string rubros = string.Join(", ", lstRubrosSolicitud.Where(x => x.OficinaComercial == true).Select(s => s.CodigoRubro).ToArray());

                string plural1;
                string plural2;
                if (lstRubrosSolicitud.Where(x => x.OficinaComercial == true).Count() > 1)
                {
                    plural1 = "los rubros";
                    plural2 = "serán";
                }
                else
                {
                    plural1 = "el rubro";
                    plural2 = "será";
                }

                chkOficinaComercial.Text = string.Format("Se deja constancia que {0} <b>{1}</b> {2} utilizados solo con el fin de oficina comercial", plural1, rubros, plural2);
                pnlchkOficinaComercial.Style["display"] = "block";
                chkOficinaComercial.Checked = encomienda.DeclaraOficinaComercial;
            }
            else
            {
                chkOficinaComercial.Text = "";
                pnlchkOficinaComercial.Style["display"] = "none";
            }

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
            EncomiendaRubrosBL encomiendaRubros = new EncomiendaRubrosBL();
            var lstRubrosSolicitud = encomiendaRubros.GetRubros(encomienda.IdEncomienda);
            var lstRubrosSolicitudATAnterior = encomiendaRubros.GetRubrosATAnterior(encomienda.IdEncomienda);

            grdRubrosIngresados.DataSource = lstRubrosSolicitud.ToList();
            grdRubrosIngresados.DataBind();
            
            grdRubrosIngresadosATAnterior.DataSource = lstRubrosSolicitudATAnterior.ToList();
            grdRubrosIngresadosATAnterior.DataBind();
            pnlRubrosAnteriores.Visible = (encomienda.IdTipoTramite == (int)Constantes.TipoTramite.AMPLIACION);

        }

    }
}