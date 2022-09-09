using DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSIT.Solicitud.Controls
{
    public partial class SeleccionTramitesAprobados : System.Web.UI.UserControl
    {
        BusinesLayer.Implementation.SSITSolicitudesBL blSolicitudes = new BusinesLayer.Implementation.SSITSolicitudesBL();
        BusinesLayer.Implementation.TransferenciasSolicitudesBL blTransferencias = new BusinesLayer.Implementation.TransferenciasSolicitudesBL();

        BusinesLayer.Implementation.UbicacionesBL blUbicaciones = new BusinesLayer.Implementation.UbicacionesBL();


        public int Count
        {
            get
            {
                return grdTramites.Rows.Count;
            }

        }

        public void LoadData(List<SolicitudesAprobadasDTO> lstTramites)
        {

            pnlAvisoVariosTramites.Visible = false;
            grdTramites.SelectedIndex = -1;

            grdTramites.DataSource = lstTramites;
            grdTramites.DataBind();

            if (grdTramites.Rows.Count > 0)
            {
                if (grdTramites.Rows.Count == 1)
                {
                    grdTramites.SelectRow(0);
                    ocultarBotonSeleccionar();
                }
                else
                    pnlAvisoVariosTramites.Visible = true;
            }

        }

        private void ocultarBotonSeleccionar()
        {
            foreach (GridViewRow row in grdTramites.Rows)
            {
                Button btnSeleccionar = (Button)row.FindControl("btnSeleccionar");
                if (row == grdTramites.SelectedRow)
                    btnSeleccionar.Visible = false;
                else
                    btnSeleccionar.Visible = true;
            }

        }
        protected void grdTramites_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                SolicitudesAprobadasDTO dto = (SolicitudesAprobadasDTO) e.Row.DataItem;
                Label lblSolicitud = (Label)e.Row.FindControl("lblSolicitud");
                Label lblTipoTramite = (Label)e.Row.FindControl("lblTipoTramite");
                Label lblEstado = (Label)e.Row.FindControl("lblEstado");
                Label lblUbicacion = (Label)e.Row.FindControl("lblUbicacion");
                Label lblSuperficieTotal = (Label)e.Row.FindControl("lblSuperficieTotal");
                Label lblExpediente = (Label)e.Row.FindControl("lblExpediente");
                Label lblEncomienda = (Label)e.Row.FindControl("lblEncomienda");
                Label lblTitulares = (Label)e.Row.FindControl("lblTitulares");

                List<int> lstIdsUbicaciones = dto.SSITSolicitudesUbicacionesDTO.Select(s => s.IdUbicacion.Value).ToList();

                var lstUbicaciones = blUbicaciones.GetDirecciones(lstIdsUbicaciones).ToList();

                lblSolicitud.Text = dto.IdSolicitud.ToString();
                lblTipoTramite.Text = string.Format("{0} {1} {2}", dto.TipoTramiteDescripcion, dto.TipoExpedienteDescripcion,dto.SubTipoExpedienteDescripcion);
                lblEstado.Text = dto.TipoEstadoSolicitudDTO.Descripcion;
                lblEncomienda.Text = dto.IdEncomienda.ToString();
                lblSuperficieTotal.Text = dto.SuperficieTotal.ToString() + " m2.";
                lblUbicacion.Text = string.Join(" - ", lstUbicaciones.Select(s => s.direccion).ToArray());
                lblExpediente.Text = (!string.IsNullOrEmpty(dto.NroExpediente) ? dto.NroExpediente : dto.NroExpedienteSade);

                lblTitulares.Text = dto.Titulares;  


            }
        }

        public SolicitudesAprobadasDTO GetTramiteSeleccionado()
        {
            SolicitudesAprobadasDTO result = null;

            if (grdTramites.Rows.Count > 0)
            {
                
                var row = grdTramites.SelectedRow;
                if (row != null)
                {
                    int id_solicitud = Convert.ToInt32(grdTramites.SelectedDataKey["IdSolicitud"]);
                    int id_tipotramite = Convert.ToInt32(grdTramites.SelectedDataKey["IdTipoTramite"]);

                    
                     if (id_tipotramite == (int) StaticClass.Constantes.TipoTramite.TRANSFERENCIA)
                    {
                        TransferenciasSolicitudesDTO TransfDTO = blTransferencias.Single(id_solicitud);
                        if (TransfDTO != null)
                        {
                            result = new SolicitudesAprobadasDTO
                            {
                                IdSolicitud = TransfDTO.IdSolicitud,
                                CodigoSeguridad = TransfDTO.CodigoSeguridad,
                                CreateDate = TransfDTO.CreateDate,
                                CreateUser = TransfDTO.CreateUser,
                                IdEstado = TransfDTO.IdEstado,
                                IdSubTipoExpediente = TransfDTO.IdSubTipoExpediente,
                                IdTipoExpediente = TransfDTO.IdTipoExpediente,
                                IdTipoTramite = TransfDTO.IdTipoTramite,
                                LastUpdateDate = TransfDTO.LastUpdateDate,
                                LastUpdateUser = TransfDTO.LastUpdateUser,
                                NroExpedienteSade = TransfDTO.NumeroExpedienteSade
                            };
                        }
                    }
                    else
                    {
                        SSITSolicitudesDTO SolDTO = blSolicitudes.Single(id_solicitud);

                        if (SolDTO != null)
                        {
                            result = new SolicitudesAprobadasDTO
                            {
                                IdSolicitud = SolDTO.IdSolicitud,
                                CodigoSeguridad = SolDTO.CodigoSeguridad,
                                CreateDate = SolDTO.CreateDate,
                                CreateUser = SolDTO.CreateUser,
                                NroExpediente = SolDTO.NroExpediente,
                                IdEstado = SolDTO.IdEstado,
                                IdSubTipoExpediente = SolDTO.IdSubTipoExpediente,
                                IdTipoExpediente = SolDTO.IdTipoExpediente,
                                IdTipoTramite = SolDTO.IdTipoTramite,
                                LastUpdateDate = SolDTO.LastUpdateDate,
                                LastUpdateUser = SolDTO.LastUpdateUser,
                                NroExpedienteSade = SolDTO.NroExpedienteSade,
                                Servidumbre_paso = SolDTO.Servidumbre_paso
                            };
                        }
                    }

                }

            }

            return result;
        }

        protected void btnSeleccionar_Click(object sender, EventArgs e)
        {
            Button btnSeleccionar = (Button)sender;
            GridViewRow row = (GridViewRow) btnSeleccionar.Parent.Parent;
            grdTramites.SelectRow(row.RowIndex);
        }

        protected void grdTramites_SelectedIndexChanged(object sender, EventArgs e)
        {
            ocultarBotonSeleccionar();
        }
    }
}