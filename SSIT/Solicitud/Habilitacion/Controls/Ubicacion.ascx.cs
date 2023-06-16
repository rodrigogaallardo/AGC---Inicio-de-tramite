using BusinesLayer.Implementation;
using DataTransferObject;
using SSIT.Common;
using StaticClass;
using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSIT.Solicitud.Habilitacion.Controls
{
    public class ucEliminarEventsArgs : EventArgs
    {
        public int IdUbicacion { get; set; }
    }
    public class ucEditarEventsArgs : EventArgs
    {
        public int IdUbicacion { get; set; }
    }
    public partial class Ubicacion : System.Web.UI.UserControl
    {
        public delegate void EventHandlerEliminar(object sender, ucEliminarEventsArgs e);
        public event EventHandlerEliminar EliminarClick;

        public delegate void EventHandlerEditar(object sender, ucEditarEventsArgs e);
        public event EventHandlerEditar EditarClick;

        private static bool _Editable;

        [Browsable(true),
        Category("Appearance"),
        DefaultValue(true),
        Description("Devuelve/Establece si es posible eliminar las ubicaciones.")]
        public bool Editable
        {
            get
            {

                if (ViewState["Ubicaciones.ascx._Editable"] != null)
                    Editable = Convert.ToBoolean(ViewState["Ubicaciones.ascx._Editable"]);

                return _Editable;
            }
            set
            {
                // Se debe setear la propiedad editable antes de ejecutar la carga de datos
                ViewState["Ubicaciones.ascx._Editable"] = value;
                _Editable = value;
            }
        }

        public int IdSolicitud { get; set; }
        public SSITSolicitudesDTO Solicitud { get; set; }

        public void CargarDatos(int IdSolicitud)
        {
            this.IdSolicitud = IdSolicitud;
            SSITSolicitudesBL solicitudUbicaciones = new SSITSolicitudesBL();

            var elements = solicitudUbicaciones.Single(IdSolicitud);

            CargarDatos(elements);
        }

        public void CargarDatos(SSITSolicitudesDTO solicitud)
        {
            this.Solicitud = solicitud;
            this.IdSolicitud = IdSolicitud;

            chkServidumbre.Enabled = _Editable;

            if (solicitud.SSITSolicitudesUbicacionesDTO.Any())
            {
                chkServidumbre.Style["display"] = "block";
                lblServidumbre.Style["display"] = "block";
            }
            else
            {
                chkServidumbre.Style["display"] = "none";
                lblServidumbre.Style["display"] = "none";
            }
            SSITSolicitudesBL blSol = new SSITSolicitudesBL();
            var sol = blSol.Single(solicitud.IdSolicitud);

            chkServidumbre.Checked = sol.Servidumbre_paso;

            if (solicitud.SSITSolicitudesUbicacionesDTO.Count() > 1 && !sol.Servidumbre_paso)
            {
                if (((solicitud.IdTipoTramite == (int)Constantes.TipoDeTramite.Ampliacion) ||
                    (solicitud.IdTipoTramite == (int)Constantes.TipoDeTramite.RedistribucionDeUso)))
                {
                    chkServidumbre.Checked = true;
                    blSol.ActualizarServidumbreDePaso(sol, true);
                }
            }

            gridubicacion_db.DataSource = solicitud.SSITSolicitudesUbicacionesDTO;
            gridubicacion_db.DataBind();

            EncomiendaBL encBL = new EncomiendaBL();

            var enc = encBL.GetByFKIdSolicitud(this.Solicitud.IdSolicitud).OrderByDescending(x => x.IdEncomienda).FirstOrDefault();
            CargarPlantasHabilitar(enc);
        }

        private void CargarPlantasHabilitar(EncomiendaDTO encomienda)
        {
            if (encomienda != null)
            {
                var lstaPlantas = encomienda.EncomiendaPlantasDTO;
                lblPlantasHabilitar.Text = "";
                foreach (var item in lstaPlantas)
                {

                    int TamanoCampoAdicional = item.TamanoCampoAdicional;
                    bool MuestraCampoAdicional = false;
                    string separador = "";

                    if (item.TipoSectorDTO != null && item.TipoSectorDTO.MuestraCampoAdicional != null)
                        MuestraCampoAdicional = item.TipoSectorDTO.MuestraCampoAdicional.Value;

                    if (lblPlantasHabilitar.Text.Length == 0)
                        separador = "";
                    else
                        separador = ", ";

                    if (MuestraCampoAdicional)
                    {
                        if (TamanoCampoAdicional >= 10)
                            lblPlantasHabilitar.Text += separador + item.TipoSectorDTO.Descripcion.Trim();
                        else
                            lblPlantasHabilitar.Text += separador + item.TipoSectorDTO.Descripcion.Trim() + " " + item.Descripcion.Trim();
                    }
                    else
                        lblPlantasHabilitar.Text += separador + item.TipoSectorDTO.Descripcion.Trim();
                }

                if (lblPlantasHabilitar.Text.Length == 0)
                {
                    pnlPlantasHabilitar.Visible = false;
                }
            }
        }

        protected void gridubicacion_db_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_NroPartidaMatriz = (Label)e.Row.FindControl("grd_NroPartidaMatriz_db");
                Label lbl_seccion = (Label)e.Row.FindControl("grd_seccion_db");
                Label lbl_manzana = (Label)e.Row.FindControl("grd_manzana_db");
                Label lbl_parcela = (Label)e.Row.FindControl("grd_parcela_db");
                Label lbl_zonificacion = (Label)e.Row.FindControl("lbl_zonificacion_db");
                Image imgFoto = (Image)e.Row.FindControl("imgFotoParcela_db");
                Image imgMapa1 = (Image)e.Row.FindControl("imgMapa1");
                Image imgMapa2 = (Image)e.Row.FindControl("imgMapa2");

                DataList dtlPuertas_db = (DataList)e.Row.FindControl("dtlPuertas_db");
                DataList dtlPartidasHorizontales = (DataList)e.Row.FindControl("dtlPartidaHorizontales_db");
                Label lblEmptyDataPartidasHorizontales = (Label)e.Row.FindControl("lblEmptyDataPartidasHorizontales_db");
                Panel pnlSMP = (Panel)e.Row.FindControl("pnlSMPview");
                UpdatePanel UpnDeptoLocalview = (UpdatePanel)e.Row.FindControl("UpnDeptoLocalview");
                Panel pnlPuertas = (Panel)e.Row.FindControl("pnlPuertasview");
                Panel pnlTipoUbicacion = (Panel)e.Row.FindControl("pnlTipoUbicacionview");
                Label lblTipoUbicacion = (Label)e.Row.FindControl("lblTipoUbicacionview");
                Label lblSubTipoUbicacion = (Label)e.Row.FindControl("lblSubTipoUbicacionview");
                Panel pnlPartidasHorizontales = (Panel)e.Row.FindControl("pnlPartidasHorizontalesview");
                Panel pnlDeptoLocal = (Panel)e.Row.FindControl("pnlDeptoLocalview");
                Label lblOtros = (Label)e.Row.FindControl("lblOtros");
                Label lblTextOtros = (Label)e.Row.FindControl("lblTextOtros");
                Label lblTextLocal = (Label)e.Row.FindControl("lblTextLocal");
                Label lblLocal = (Label)e.Row.FindControl("lblLocal");
                Label lblTextDepto = (Label)e.Row.FindControl("lblTextDepto");
                Label lblDepto = (Label)e.Row.FindControl("lblDepto");
                Label lblTextTorre = (Label)e.Row.FindControl("lblTextTorre");
                Label lblTorre = (Label)e.Row.FindControl("lblTorre");
                LinkButton btnEliminar = (LinkButton)e.Row.FindControl("btnEliminar");
                LinkButton btnEditar = (LinkButton)e.Row.FindControl("btnEditar");

                btnEliminar.Visible = _Editable;
                btnEditar.Visible = _Editable;

                int IdSolicitudUbicacion = Convert.ToInt32(gridubicacion_db.DataKeys[e.Row.RowIndex].Value);
                bool RequiereSMP = true;
                int id_tipoubicacion = 0;


                var item = (SSITSolicitudesUbicacionesDTO)e.Row.DataItem;

                id_tipoubicacion = item.SubTipoUbicacionesDTO.id_tipoubicacion;

                if (item.SubTipoUbicacionesDTO.TiposDeUbicacionDTO.RequiereSMP.HasValue)
                    RequiereSMP = item.SubTipoUbicacionesDTO.TiposDeUbicacionDTO.RequiereSMP.Value;

                SSITSolicitudesUbicacionesBL ubicacionesBL = new SSITSolicitudesUbicacionesBL();
                if (RequiereSMP)
                {
                    if (item.UbicacionesDTO.NroPartidaMatriz.HasValue)
                        lbl_NroPartidaMatriz.Text = item.UbicacionesDTO.NroPartidaMatriz.Value.ToString();

                    if (item.UbicacionesDTO.Seccion.HasValue)
                        lbl_seccion.Text = item.UbicacionesDTO.Seccion.Value.ToString();

                    if (!ubicacionesBL.esUbicacionEspecialConObjetoTerritorialByIdUbicacion(item.UbicacionesDTO.IdUbicacion))
                    {
                        lbl_manzana.Text = item.UbicacionesDTO.Manzana.Trim();
                        lbl_parcela.Text = item.UbicacionesDTO.Parcela.Trim();
                    }

                }

                pnlSMP.Visible = RequiereSMP;


                if (id_tipoubicacion.Equals((int)Constantes.TiposDeUbicacion.ParcelaComun) || !id_tipoubicacion.Equals((int)Constantes.TiposDeUbicacion.ObjetoTerritorial)
                   || id_tipoubicacion.Equals((int)Constantes.TiposDeUbicacion.ObjetoTerritorial))
                {
                    pnlTipoUbicacion.Visible = true;
                    lblTipoUbicacion.Text = item.SubTipoUbicacionesDTO.TiposDeUbicacionDTO.DescripcionTipoUbicacion != null ?
                        item.SubTipoUbicacionesDTO.TiposDeUbicacionDTO.DescripcionTipoUbicacion.Trim() : "";
                    lblSubTipoUbicacion.Text = item.SubTipoUbicacionesDTO.descripcion_subtipoubicacion.Trim();
                    lblTextOtros.Text = item.LocalSubtipoUbicacion != null ? item.LocalSubtipoUbicacion : "";
                    lbl_seccion.Text = item.UbicacionesDTO.Seccion.Value.ToString();
                    lbl_manzana.Text = item.UbicacionesDTO.Manzana.Trim();
                    lbl_parcela.Text = item.UbicacionesDTO.Parcela.Trim();

                    pnlDeptoLocal.Visible = false;
                    pnlSMP.Visible = true;

                }
                else
                    pnlDeptoLocal.Visible = true;// Solo aparece cuando es parcela común, para el resto lo tomna del campo local que pone al buscar

                SSITSolicitudesUbicacionesBL solUbl = new SSITSolicitudesUbicacionesBL();

                lbl_zonificacion.Text += solUbl.GetZonificacion(item.IdSolicitud.Value);

                lblTextOtros.Text = (!string.IsNullOrEmpty(item.DeptoLocalUbicacion) ? item.DeptoLocalUbicacion.Trim() : "");
                lblTextLocal.Text = (!string.IsNullOrEmpty(item.Local) ? item.Local.Trim() : "");
                lblTextDepto.Text = (!string.IsNullOrEmpty(item.Depto) ? item.Depto.Trim() : "");
                lblTextTorre.Text = (!string.IsNullOrEmpty(item.Torre) ? item.Torre.Trim() : "");


                if (lblTextOtros.Text == "" && lblTextLocal.Text == "" && lblTextDepto.Text == "" && lblTextTorre.Text == "")
                    pnlDeptoLocal.Visible = false;

                if (lblTextOtros.Text == "")
                    lblTextOtros.Visible = false;
                if (lblTextLocal.Text == "")
                    lblLocal.Visible = false;
                if (lblTextDepto.Text == "")
                    lblDepto.Visible = false;
                if (lblTextTorre.Text == "")
                    lblTorre.Visible = false;

                UpnDeptoLocalview.Update();

                int seccion = (item.UbicacionesDTO.Seccion.HasValue ? item.UbicacionesDTO.Seccion.Value : 0);
                imgFoto.ImageUrl = Functions.GetUrlFoto(seccion, item.UbicacionesDTO.Manzana.Trim(), item.UbicacionesDTO.Parcela.Trim(), 300, 250);

                String dir = "";
                foreach (var p in item.SSITSolicitudesUbicacionesPuertasDTO)
                {
                    dir = p.NombreCalle + " " + p.NroPuerta;
                    break;
                }

                if (item.UbicacionesDTO.Seccion.HasValue)
                {
                    imgMapa1.ImageUrl = Functions.GetUrlMapa(seccion, item.UbicacionesDTO.Manzana.Trim(), item.UbicacionesDTO.Parcela.Trim(), dir);
                    imgMapa2.ImageUrl = Functions.GetUrlCroquis(seccion.ToString(), item.UbicacionesDTO.Manzana.Trim(), item.UbicacionesDTO.Parcela.Trim(), dir);
                }
                else
                {
                    imgMapa1.ImageUrl = "~/Content/img/app/ImageNotFound.png";
                    imgMapa2.ImageUrl = "~/Content/img/app/ImageNotFound.png";
                }

                if (ubicacionesBL.esUbicacionEspecialConObjetoTerritorialByIdUbicacion(item.UbicacionesDTO.IdUbicacion))
                {
                    dtlPuertas_db.DataSource = from i in item.SSITSolicitudesUbicacionesPuertasDTO
                                               select new
                                               {
                                                   IdSolicitudPuerta = i.IdSolicitudPuerta,
                                                   IdSolicitudUbicacion = i.IdSolicitudUbicacion,
                                                   CodigoCalle = i.CodigoCalle,
                                                   NombreCalle = i.NombreCalle,
                                                   NroPuerta = i.NroPuerta.ToString() + 't',
                                               };
                }
                else
                {
                    dtlPuertas_db.DataSource = item.SSITSolicitudesUbicacionesPuertasDTO;
                }
                dtlPuertas_db.DataBind();

                if (dtlPuertas_db.Items.Count == 0)
                    pnlPuertas.Visible = false;

                dtlPartidasHorizontales.DataSource = item.SSITSolicitudesUbicacionesPropiedadHorizontalDTO.Select(p => p.UbicacionesPropiedadhorizontalDTO);

                dtlPartidasHorizontales.DataBind();

                if (dtlPartidasHorizontales.Items.Count > 0)
                {
                    lblEmptyDataPartidasHorizontales.Visible = false;
                    pnlPartidasHorizontales.Visible = true;
                }
                else
                {
                    lblEmptyDataPartidasHorizontales.Visible = true;
                    pnlPartidasHorizontales.Visible = false;
                }

                if (HttpContext.Current.Request.CurrentExecutionFilePath.Contains("Visualizar"))
                {
                    bool Editable = true;

                    if ((Solicitud.IdEstado == (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo) || (Solicitud.IdEstado != (int)Constantes.Encomienda_Estados.Anulada))
                        Editable = false;

                    if (Editable)
                    {
                        btnEditar.Style["display"] = "none";
                        btnEliminar.Style["display"] = "none";
                    }
                }
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            LinkButton btnEliminar = (LinkButton)sender;
            int id_caaubicacion = int.Parse(btnEliminar.CommandArgument);

            ucEliminarEventsArgs args = new ucEliminarEventsArgs();
            args.IdUbicacion = id_caaubicacion;
            EliminarClick(sender, args);
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            LinkButton btnEditar = (LinkButton)sender;
            int IdSolicitudUbicacion = int.Parse(btnEditar.CommandArgument);

            ucEditarEventsArgs args = new ucEditarEventsArgs();
            args.IdUbicacion = IdSolicitudUbicacion;
            EditarClick(sender, args);
        }

    }
}