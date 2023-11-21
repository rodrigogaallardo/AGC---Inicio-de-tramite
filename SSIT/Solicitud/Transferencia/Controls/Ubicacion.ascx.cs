using SSIT.Common;
using BusinesLayer.Implementation;
using DataTransferObject;
using StaticClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SSIT.Solicitud.Transferencia.Controls;

namespace SSIT.Solicitud.Transferencia.Controls
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
        Description("Devuelve/Establece sies posible eliminar las ubicaciones.")]
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
                pnlPlantasHabilitar.Visible = !value;       // solo es visible al no ser editable
            }
        }

        public int IdSolicitud
        {
            get;
            set;
        }

        public int _nroTrReferencia
        {
            get;
            set;
        }
        public void CargarDatos(TransferenciasSolicitudesDTO transferencia)
        {
            ParametrosBL paramBL = new ParametrosBL();
            this.IdSolicitud = IdSolicitud;
            int nroTrReferencia = 0;
            int.TryParse(paramBL.GetParametroChar("NroTransmisionReferencia"), out nroTrReferencia);
            this._nroTrReferencia = nroTrReferencia;

            gridubicacion_db.DataSource = transferencia.Ubicaciones;
            gridubicacion_db.DataBind();

            if (!_Editable)
                CargarPlantasHabilitar(transferencia);

        }
        /// <summary>
        /// 
        /// </summary>
        public bool TieneUbicaciones
        {
            get
            {

                return (gridubicacion_db.Rows.Count > 0);
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
                Label lbl_zonificacion = (Label)e.Row.FindControl("grd_zonificacion_db");
                Label lbl_mixtura = (Label)e.Row.FindControl("zonificacion");
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

                int IdTransferenciaUbicacion = Convert.ToInt32(gridubicacion_db.DataKeys[e.Row.RowIndex].Value);
                bool RequiereSMP = true;
                int id_tipoubicacion = 0;

                TransferenciaUbicacionesDTO item = (TransferenciaUbicacionesDTO)e.Row.DataItem;

                id_tipoubicacion = item.SubTipoUbicacion.TiposDeUbicacionDTO.IdTipoUbicacion;

                if (item.SubTipoUbicacion.TiposDeUbicacionDTO.RequiereSMP.HasValue)
                    RequiereSMP = item.SubTipoUbicacion.TiposDeUbicacionDTO.RequiereSMP.Value;

                if (RequiereSMP)
                {
                    if (item.Ubicacion.NroPartidaMatriz.HasValue)
                        lbl_NroPartidaMatriz.Text = item.Ubicacion.NroPartidaMatriz.Value.ToString();

                    if (item.Ubicacion.Seccion.HasValue)
                        lbl_seccion.Text = item.Ubicacion.Seccion.Value.ToString();

                    lbl_manzana.Text = item.Ubicacion.Manzana.Trim();
                    lbl_parcela.Text = item.Ubicacion.Parcela.Trim();
                }

                pnlSMP.Visible = RequiereSMP;

                if (!id_tipoubicacion.Equals((int)Constantes.TiposDeUbicacion.ParcelaComun))
                {
                    pnlTipoUbicacion.Visible = true;
                    lblTipoUbicacion.Text = item.SubTipoUbicacion.TiposDeUbicacionDTO.DescripcionTipoUbicacion;
                    lblSubTipoUbicacion.Text = item.SubTipoUbicacion.descripcion_subtipoubicacion;
                    lblLocal.Text = item.LocalSubTipoUbicacion.Trim();
                    pnlDeptoLocal.Visible = false;
                }
                else
                    pnlDeptoLocal.Visible = true;// Solo aparece cuando es parcela común, para el resto lo tomna del campo local que pone al buscar

                TransferenciasSolicitudesBL transfbl = new TransferenciasSolicitudesBL();

                int trId = item.IdSolicitud ?? 0;

                lbl_zonificacion.Text = transfbl.GetMixDistritoZonaySubZonaByTr(trId);

                lblTextOtros.Text = (!string.IsNullOrEmpty(item.DeptoLocalTransferenciaUbicacion) ? item.DeptoLocalTransferenciaUbicacion.Trim() : "");
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

                int seccion = (item.Ubicacion.Seccion.HasValue ? item.Ubicacion.Seccion.Value : 0);
                imgFoto.ImageUrl = Functions.GetUrlFoto(seccion, item.Ubicacion.Manzana.Trim(), item.Ubicacion.Parcela.Trim(), 300, 250);

                String dir = "";
                foreach (var p in item.Puertas)
                {
                    dir = p.NombreCalle + " " + p.NumeroPuerta;
                    break;
                }
                if (item.Ubicacion.Seccion != null)
                {
                    imgMapa1.ImageUrl = Functions.GetUrlMapa(seccion, item.Ubicacion.Manzana.Trim(), item.Ubicacion.Parcela.Trim(), dir);
                    imgMapa2.ImageUrl = Functions.GetUrlCroquis(seccion.ToString(), item.Ubicacion.Manzana.Trim(), item.Ubicacion.Parcela.Trim(), dir);
                }
                else
                {
                    imgMapa1.ImageUrl = "~/Content/img/app/ImageNotFound.png";
                    imgMapa2.ImageUrl = "~/Content/img/app/ImageNotFound.png";
                }

                dtlPuertas_db.DataSource = item.Puertas;
                dtlPuertas_db.DataBind();

                if (dtlPuertas_db.Items.Count == 0)
                    pnlPuertas.Visible = false;

                dtlPartidasHorizontales.DataSource = item.PropiedadesHorizontales.Select(p => p.UbicacionPropiedadaHorizontal).ToList();
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

                if (item.IdSolicitud <= this._nroTrReferencia)
                    lbl_mixtura.Text = "Zonificación de la parcela:";
                else
                    lbl_mixtura.Text = "Area de Mixtura / Distrito de Zonificación:";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        private void CargarPlantasHabilitar(TransferenciasSolicitudesDTO transferencia)
        {
            bool tieneEncomienda= transferencia.EncomiendaTransfSolicitudesDTO.Select(e => e.id_encomienda).Any();
            
            if (tieneEncomienda)
            {
                //var listaPlantasDesdeEncomienda;
                int idEncomienda= transferencia.EncomiendaTransfSolicitudesDTO.First().id_encomienda;
                EncomiendaPlantasBL encomiendaPlantasBL = new EncomiendaPlantasBL();
                var plantasDesdeEncomienda = encomiendaPlantasBL.Get(idEncomienda).Where(x => x.Seleccionado);

                if (plantasDesdeEncomienda != null)
                {
                    foreach (var item in plantasDesdeEncomienda)
                    {
                        int TamanoCampoAdicional = item.TamanoCampoAdicional;
                        bool MuestraCampoAdicional = false;
                        string separador = "";
                     
                        MuestraCampoAdicional = item.MuestraCampoAdicional;

                        if (lblPlantasHabilitar.Text.Length == 0)
                            separador = "";
                        else
                            separador = ", ";

                        if (MuestraCampoAdicional)
                        {
                            if (TamanoCampoAdicional >= 8)
                                lblPlantasHabilitar.Text += separador + item.Descripcion.Trim() + " " + item.Detalle.Trim();
                            else
                                lblPlantasHabilitar.Text += separador + item.Descripcion.Trim();
                        }
                        else
                            lblPlantasHabilitar.Text += separador + item.Descripcion.Trim();

                      
                    }
                    return;
                }
            }

            var lstaPlantas = transferencia.Plantas;

            if (lstaPlantas != null)
            {
                foreach (var item in lstaPlantas)
                {
                    string separador = "";

                    if (lblPlantasHabilitar.Text.Length == 0)
                        separador = "";
                    else
                        separador = ", ";

                    if (string.IsNullOrWhiteSpace(item.DetalleTransferenciaTipoSector))
                    {
                        lblPlantasHabilitar.Text += separador + item.TipoSector.Descripcion.Trim();
                    }
                    else
                    {
                        lblPlantasHabilitar.Text += separador + item.DetalleTransferenciaTipoSector.Trim();
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

            if (EliminarClick != null)
                EliminarClick(sender, args);
        }
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            LinkButton btnEditar = (LinkButton)sender;
            int id_caaubicacion = int.Parse(btnEditar.CommandArgument);

            ucEditarEventsArgs args = new ucEditarEventsArgs();
            args.IdUbicacion = id_caaubicacion;
            EditarClick(sender, args);
        }
    }
}