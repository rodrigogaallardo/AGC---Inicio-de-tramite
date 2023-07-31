using AnexoProfesionales.Common;
using BusinesLayer.Implementation;
using DataTransferObject;
using StaticClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnexoProfesionales.Controls
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

        public int IdEncomienda
        {
            get;
            set;
        }
        public bool Visor { get; set; }

        public void CargarDatos(int IdEncomienda)
        {
            EncomiendaBL encomiendaBL = new EncomiendaBL();
            CargarDatos(encomiendaBL.Single(IdEncomienda));
        }

        public void CargarDatos(EncomiendaDTO encomienda)
        {
            this.IdEncomienda = encomienda.IdEncomienda;

            var elements = encomienda.EncomiendaUbicacionesDTO;

            chkServidumbre.Enabled = _Editable;
            if (elements.Count() > 1)
            {
                chkServidumbre.Style["display"] = "block";
                lblServidumbre.Style["display"] = "block";
            }
            else
            {
                chkServidumbre.Style["display"] = "none";
                lblServidumbre.Style["display"] = "none";
            }

            if (Visor)
            {
                chkGaleria.Visible = true;
                chkGaleria.Enabled = false;
                chkGaleria.Checked = encomienda.Contiene_galeria_paseo.HasValue && encomienda.Contiene_galeria_paseo.Value;
                chkPlantasConsecutivas.Enabled = false;
                chkPlantasConsecutivas.Visible = true;
                chkPlantasConsecutivas.Checked = encomienda.Consecutiva_Supera_10.HasValue && encomienda.Consecutiva_Supera_10.Value;
            }

            chkServidumbre.Checked = encomienda.Servidumbre_paso;

            gridubicacion_db.DataSource = elements.ToList();
            gridubicacion_db.DataBind();

            if (!_Editable)
                CargarPlantasHabilitar(encomienda);
        }

        protected void gridubicacion_db_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {

            EncomiendaUbicacionesBL encBL = new EncomiendaUbicacionesBL();
            ParametrosBL paramBL = new ParametrosBL();

            int nroSolReferencia = 0;
            int.TryParse(ConfigurationManager.AppSettings["NroSolicitudReferencia"], out nroSolReferencia);

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_NroPartidaMatriz = (Label)e.Row.FindControl("grd_NroPartidaMatriz_db");
                Label lbl_seccion = (Label)e.Row.FindControl("grd_seccion_db");
                Label lbl_manzana = (Label)e.Row.FindControl("grd_manzana_db");
                Label lbl_parcela = (Label)e.Row.FindControl("grd_parcela_db");
                Label lbl_zonificacion = (Label)e.Row.FindControl("lbl_zonificacion_db");
                Label lblZona = (Label)e.Row.FindControl("lblZona");
                Label lbl_subzona = (Label)e.Row.FindControl("lblSubZona");
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

                TextBox txtDetalle = (TextBox)e.Row.FindControl("txtDetalle");

                btnEliminar.Visible = _Editable;
                btnEditar.Visible = _Editable;

                bool RequiereSMP = true;
                int id_tipoubicacion = 0;


                var item = (EncomiendaUbicacionesDTO)e.Row.DataItem;

                id_tipoubicacion = item.SubTipoUbicacionesDTO.TiposDeUbicacionDTO.IdTipoUbicacion;

                if (item.SubTipoUbicacionesDTO.TiposDeUbicacionDTO.RequiereSMP.HasValue)
                    RequiereSMP = item.SubTipoUbicacionesDTO.TiposDeUbicacionDTO.RequiereSMP.Value;

                SSITSolicitudesUbicacionesBL ubicacionesBL = new SSITSolicitudesUbicacionesBL();
                if (RequiereSMP)
                {

                    if (item.Ubicacion.NroPartidaMatriz.HasValue)
                        lbl_NroPartidaMatriz.Text = item.Ubicacion.NroPartidaMatriz.Value.ToString();

                    if (item.Ubicacion.Seccion.HasValue)
                        lbl_seccion.Text = item.Ubicacion.Seccion.Value.ToString();


                    if (!ubicacionesBL.esUbicacionEspecialConObjetoTerritorialByIdUbicacion(item.Ubicacion.IdUbicacion))
                    {
                        lbl_manzana.Text = item.Ubicacion.Manzana.Trim();
                        lbl_parcela.Text = item.Ubicacion.Parcela.Trim();
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
                    lbl_seccion.Text = item.Ubicacion.Seccion.Value.ToString();
                    lbl_manzana.Text = item.Ubicacion.Manzana.Trim();
                    lbl_parcela.Text = item.Ubicacion.Parcela.Trim();

                    pnlDeptoLocal.Visible = false;
                    pnlSMP.Visible = true;

                }
                else
                    pnlDeptoLocal.Visible = true;// Solo aparece cuando es parcela común, para el resto lo tomna del campo local que pone al buscar


                lbl_zonificacion.Text += encBL.GetZonificacion(item.IdEncomienda.Value);

                lblTextOtros.Text = (!string.IsNullOrEmpty(item.DeptoLocalEncomiendaUbicacion) ? item.DeptoLocalEncomiendaUbicacion.Trim() : "");
                lblTextLocal.Text = (!string.IsNullOrEmpty(item.Local) ? item.Local.Trim() : "");
                lblTextDepto.Text = (!string.IsNullOrEmpty(item.Depto) ? item.Depto.Trim() : "");
                lblTextTorre.Text = (!string.IsNullOrEmpty(item.Torre) ? item.Torre.Trim() : "");

                if (lblTextOtros.Text == "" && lblTextLocal.Text == "" && lblTextDepto.Text == "" && lblTextTorre.Text == "")
                    pnlDeptoLocal.Visible = false;

                if (lblTextOtros.Text == "")
                {
                    lblOtros.Visible = false;
                    lblTextOtros.Visible = false;
                }
                if (lblTextLocal.Text == "")
                {
                    lblLocal.Visible = false;
                    lblTextLocal.Visible = false;
                }
                if (lblTextDepto.Text == "")
                {
                    lblDepto.Visible = false;
                    lblTextDepto.Visible = false;
                }
                if (lblTextTorre.Text == "")
                {
                    lblTorre.Visible = false;
                    lblTextTorre.Visible = false;
                }

                UpnDeptoLocalview.Update();
                int seccion = (item.Ubicacion.Seccion.HasValue ? item.Ubicacion.Seccion.Value : 0);
                imgFoto.ImageUrl = Functions.GetUrlFoto(seccion, item.Ubicacion.Manzana.Trim(), item.Ubicacion.Parcela.Trim(), 300, 250);


                //EncomiendaUbicacionesPuertasBL encomiendaUbicacionesPuertasBL = new EncomiendaUbicacionesPuertasBL();
                var query = item.EncomiendaUbicacionesPuertasDTO;//encomiendaUbicacionesPuertasBL.GetByFKIdEncomiendaUbicacion(item.IdEncomiendaUbicacion);
                String dir = "";
                foreach (var p in query)
                {
                    dir = p.NombreCalle + " " + p.NroPuerta;
                    break;
                }
                if (item.Ubicacion.Seccion != null)
                {
                    imgMapa1.ImageUrl = Functions.GetUrlMapa(seccion, item.Ubicacion.Manzana.Trim(), item.Ubicacion.Parcela.Trim(), dir);
                    imgMapa2.ImageUrl = Functions.GetUrlCroquis(seccion, item.Ubicacion.Manzana.Trim(), item.Ubicacion.Parcela.Trim(), dir);
                }
                else
                {
                    imgMapa1.ImageUrl = "~/Content/img/app/ImageNotFound.png";
                    imgMapa2.ImageUrl = "~/Content/img/app/ImageNotFound.png";
                }

                int id = item.IdUbicacion ?? 0;

                if (ubicacionesBL.esUbicacionEspecialConObjetoTerritorialByIdUbicacion(id))
                {
                    dtlPuertas_db.DataSource = from i in query
                                               select new
                                               {
                                                   IdSolicitudPuerta = i.IdEncomiendaPuerta,
                                                   IdSolicitudUbicacion = i.IdEncomiendaUbicacion,
                                                   CodigoCalle = i.CodigoCalle,
                                                   NombreCalle = i.NombreCalle,
                                                   NroPuerta = i.NroPuerta.ToString() + 't',
                                               };
                }
                else
                {
                    dtlPuertas_db.DataSource = query;
                }

                dtlPuertas_db.DataBind();

                if (dtlPuertas_db.Items.Count == 0)
                    pnlPuertas.Visible = false;

                UbicacionesPropiedadhorizontalBL ubicacionesPropiedadhorizontalBL = new UbicacionesPropiedadhorizontalBL();
                EncomiendaUbicacionesPropiedadHorizontalBL phBL = new EncomiendaUbicacionesPropiedadHorizontalBL();
                List<UbicacionesPropiedadhorizontalDTO> uphs = new List<UbicacionesPropiedadhorizontalDTO>();
                var phs = phBL.GetByFKIdEncomiendaUbicacion(item.IdEncomiendaUbicacion);
                foreach (var ph in phs)
                {
                    var uph = ubicacionesPropiedadhorizontalBL.Single(ph.IdPropiedadHorizontal.Value);
                    uphs.Add(uph);
                }
                dtlPartidasHorizontales.DataSource = uphs;

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
            }
        }

        private void CargarPlantasHabilitar(EncomiendaDTO encomienda)
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
            int id_caaubicacion = int.Parse(btnEditar.CommandArgument);

            ucEditarEventsArgs args = new ucEditarEventsArgs();
            args.IdUbicacion = id_caaubicacion;
            EditarClick(sender, args);
        }
    }
}