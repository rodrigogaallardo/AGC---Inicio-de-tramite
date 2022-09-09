using BusinesLayer.Implementation;
using DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSIT.Solicitud.Habilitacion.Controls
{
    public partial class Rubros : System.Web.UI.UserControl
    {
        public void CargarDatos(EncomiendaDTO encomienda)
        {
            CargarNormativa(encomienda);
            CargarRubros(encomienda.EncomiendaRubrosDTO);

        }
        public void CargarDatos(SSITSolicitudesDTO solicitud)
        {
            CargarNormativa(solicitud);
            CargarRubros(solicitud.SSITSolicitudesRubrosCNDTO);

        }


        private void CargarNormativa(EncomiendaDTO encomienda)
        {

            lblTipoNormativa.Text = "";
            lblEntidadNormativa.Text = "";
            lblNroNormativa.Text = "";
            lblObservacionesRubros.Text = "";

            if (!string.IsNullOrEmpty(encomienda.ObservacionesRubros))
            {
                lblObservacionesRubros.Text = encomienda.ObservacionesRubros;
                pnlObservacionesRubros.Visible = true;
            }
            else
            {
                pnlObservacionesRubros.Visible = false;
            }


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

        private void CargarNormativa(SSITSolicitudesDTO solicitud)
        {

            SSITSolicitudesNormativasBL solNormBL = new SSITSolicitudesNormativasBL();
            lblTipoNormativa.Text = "";
            lblEntidadNormativa.Text = "";
            lblNroNormativa.Text = "";
            lblObservacionesRubros.Text = "";

            /*
            if (!string.IsNullOrEmpty(solicitud.ObservacionesRubros))
            {
                lblObservacionesRubros.Text = solicitud.ObservacionesRubros;
                pnlObservacionesRubros.Visible = true;
            }
            else
            {
                pnlObservacionesRubros.Visible = false;
            }
            */

            var normativa = solNormBL.Single(solicitud.IdSolicitud);

            if (normativa != null)
            {
                lblTipoNormativa.Text = normativa.TipoNormativaDTO.Nombre;
                lblEntidadNormativa.Text = normativa.EntidadNormativaDTO.Nombre;
                lblNroNormativa.Text = normativa.nro_normativa;
                pnlNormativa.Visible = true;
            }
            else
            {
                pnlNormativa.Visible = false;
            }
        }


        private void CargarRubros(IEnumerable<EncomiendaRubrosDTO> encomiendaRubrosDTO)
        {
            grdRubrosIngresados.DataSource = encomiendaRubrosDTO;
            grdRubrosIngresados.DataBind();
        }

        private void CargarRubros(IEnumerable<SSITSolicitudesRubrosCNDTO> solicitudRubrosDTO)
        {
            grdRubrosIngresados.DataSource = solicitudRubrosDTO;
            grdRubrosIngresados.DataBind();
        }
    }
}