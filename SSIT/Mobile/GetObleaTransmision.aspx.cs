using BusinesLayer.Implementation;
using ExternalService.ws_interface_AGC;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SSIT.Common;
using DataTransferObject;
using System.Configuration;

namespace SSIT.Mobile
{
    public partial class GetObleaTransmision : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Page.RouteData.Values["id_solicitud"] != null || Request.QueryString["id_solicitud"] != null)
                {
                    string valueBase64 = Convert.ToString(Page.RouteData.Values["id_solicitud"] != null ? Page.RouteData.Values["id_solicitud"] : Request.QueryString["id_solicitud"]);
                    byte[] encodedDataAsBytes = Convert.FromBase64String(valueBase64);
                    string returnValue = Encoding.Default.GetString(encodedDataAsBytes);
                    int id_solicitud = Convert.ToInt32(returnValue);

                    if (id_solicitud > 0)
                        CargarDatosTramite(id_solicitud);
                }
            }
        }

        private void CargarDatosTramite(int id_solicitud)
        {
            hdfid_solicitud.Value = Convert.ToString(id_solicitud);
            int id_tramitetarea = 0;

            hdfid_tarea.Value = Convert.ToString(id_tramitetarea);

            string dispo_nro_expediente = "";

            TransferenciasSolicitudesBL blSol = new TransferenciasSolicitudesBL();
            var sol = blSol.Single(id_solicitud);

            string nro_expediente_sade = sol.NumeroExpedienteSade;
            if (nro_expediente_sade != null)
            {
                string[] datos = nro_expediente_sade.Split('-');
                if (datos.Length > 2)
                    dispo_nro_expediente = Convert.ToString(Convert.ToInt32(datos[2])) + "/" + datos[1];
            }
            lblNroSolicitud.Text = id_solicitud.ToString();
            lblNroExpediente.Text = dispo_nro_expediente;

            EncomiendaBL encBl = new EncomiendaBL();
            var datoSolicitudEnc = encBl.GetByFKIdSolicitud(id_solicitud);
            var enc = datoSolicitudEnc.Where(x => x.IdEstado == (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo).OrderByDescending(x => x.IdEncomienda).FirstOrDefault();
            if (sol.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.VENCIDA)
            {
                if (sol.FechaLibrado == null &&
                    sol.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.APRO &&
                    enc.AcogeBeneficios == true)
                    lblFechaLibrado.Text = "<font color='red'><b>EL PRESENTE TRAMITE NO SE ENCUENTRA LIBRADO AL USO, YA QUE SE ACOGE A LOS BENEFICIOS DE LA DI-2023-2-GCABA-UERESGP. </b></font>";
                else if (sol.FechaLibrado == null &&
                         sol.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.APRO &&
                         enc.AcogeBeneficios == false)
                    lblFechaLibrado.Text = "<font color='red'><b>EL PRESENTE TRAMITE NO SE ENCUENTRA LIBRADO AL USO. </b></font>";
                else if (sol.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.CADUCO)
                    lblFechaLibrado.Text = "<font color='red'><b>TRAMITE CADUCO, NO LIBRADO AL USO</b></font>";
                else
                    lblFechaLibrado.Text = sol.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.SUSPEN &&
                                           sol.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.BAJA &&
                                           sol.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.RECH &&
                                           sol.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.ANU
                                           ? sol.FechaLibrado.ToString() : "";
            }
            else lblFechaLibrado.Text = "";

            lblTipoTramite.Text = sol.TipoTransmision.nom_tipotransmision;

            EngineBL engBL = new EngineBL();
            string descripcionCircuito = engBL.GetDescripcionCircuito(id_solicitud);

            if (descripcionCircuito != null)
                lblTipoTramite.Text += " - " + descripcionCircuito;


            EncomiendaBL blEnc = new EncomiendaBL();
            var listEnc = blEnc.GetByFKIdSolicitudTransf(id_solicitud).Where(x => x.IdEstado == (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo
                    && x.tipoAnexo == Constantes.TipoAnexo_A).ToList();

            var encomienda = listEnc.OrderByDescending(x => x.IdEncomienda).FirstOrDefault();

            if (encomienda != null)
            {                
                lblEstado.Text = sol.Estado.Descripcion;
                EngineBL blEng = new EngineBL();

                SGITramitesTareasBL sgiTTBL = new SGITramitesTareasBL();

                lblObservaciones.Text = sgiTTBL.Buscar_ObservacionPlanchetaTransmision(id_solicitud);

                var dispo = blEng.GetDatosDispoTransmision(id_solicitud);

                if (dispo != null)
                {
                    lblFechaDisposicion.Text = dispo.FechaDispo == null ? "" : dispo.FechaDispo.ToString();
                    lblNroDisposicionSADE.Text = dispo.NroDispo == null ? "" : dispo.NroDispo.ToString();
                }
                lblObservaciones.Visible = dispo == null ? false : true;
                lblFechaDisposicion.Visible = true;
                lblFechaDisposicionText.Visible = true;
                lblNroDisposicionSADE.Visible = true;
                lblNroDisposicionSADEText.Visible = true;                
                lblObservacionesText.Visible = true;
                lblTextCertificado.Visible = false;
               

                TitularesBL titularesBL = new TitularesBL();

                var lstTitulares = titularesBL.GetTitularesTransmision(id_solicitud);
                repeater_titulares.DataSource = lstTitulares;
                repeater_titulares.DataBind();

                //-- -------------------------
                //-- armar plantas a habilitar
                //-- -------------------------
                var plantas = encomienda.EncomiendaPlantasDTO;

                string strPlantasHabilitar = "";
                string separador = "";

                foreach (var item in plantas)
                {
                    if (string.IsNullOrEmpty(strPlantasHabilitar))
                        separador = "";
                    else
                        separador = ", ";

                    if (!item.TipoSectorDTO.MuestraCampoAdicional.HasValue)
                        strPlantasHabilitar = strPlantasHabilitar + separador + item.TipoSectorDTO.Descripcion;
                    else
                    {
                        if (item.TipoSectorDTO.MuestraCampoAdicional.Value == true)
                            strPlantasHabilitar = strPlantasHabilitar
                                                    + separador
                                                    + (item.TipoSectorDTO.TamanoCampoAdicional > 10 ? "" : item.TipoSectorDTO.Descripcion)
                                                    + " " + item.detalle_encomiendatiposector;
                    }

                }
                lblPlantashabilitar.Text = strPlantasHabilitar;

                //Se cambia por la mixtura en encomienda_ubicacion:_mixtura
                
                    EncomiendaUbicacionesBL ubic = new EncomiendaUbicacionesBL();
                    IEnumerable<EncomiendaUbicacionesDTO> blDto = ubic.GetByFKIdEncomienda(encomienda.IdEncomienda);

                    foreach (var ue in blDto)
                    {
                        foreach (var uem in ue.EncomiendaUbicacionesMixturasDTO)
                        {
                            //Busco la descripcion 
                            UbicacionesZonasMixturasBL uzmBL = new UbicacionesZonasMixturasBL();
                            UbicacionesZonasMixturasDTO uzm = uzmBL.Single(uem.IdZonaMixtura);
                            lblZonaDeclarada.Text += uzm.Codigo + (lblZonaDeclarada.Text.Length > 0 ? ", " : "");
                        }
                        foreach (var uem in ue.EncomiendaUbicacionesDistritosDTO)
                        {
                            //Busco la descripcion 
                            UbicacionesCatalogoDistritosBL udisBL = new UbicacionesCatalogoDistritosBL();
                            UbicacionesCatalogoDistritosDTO udis = udisBL.Single(uem.IdDistrito);
                            lblZonaDeclarada.Text += udis.Codigo + (lblZonaDeclarada.Text.Length > 0 ? ", " : "");
                        }
                }

                var dl = encomienda.EncomiendaDatosLocalDTO.FirstOrDefault();

                lblCantOperarios.Text = dl.cantidad_operarios_dl.ToString();
                lblSuperficieTotal.Text = string.Format("{0:###,###,##0.00} m2.", dl.superficie_cubierta_dl + dl.superficie_descubierta_dl);

                if (dl.ampliacion_superficie.HasValue && dl.ampliacion_superficie.Value)
                    lblSuperficieTotal.Text = string.Format("{0:###,###,##0.00} m2.", dl.superficie_cubierta_amp + dl.superficie_descubierta_amp);

                TransferenciaUbicacionesBL BlUbicaciones = new TransferenciaUbicacionesBL();

                var elements = BlUbicaciones.GetByFKIdSolicitud(id_solicitud);
                
                TransferenciasSolicitudesBL TransfbL = new TransferenciasSolicitudesBL();

                gridubicacion_db.DataSource = elements.ToList();
                gridubicacion_db.DataBind();
                List<int> lisSol = new List<int>();
                lisSol.Add(id_solicitud);

                lblDireccion.Text = TransfbL.GetDireccionTransf(lisSol);

                RubrosBL rubBL = new RubrosBL();
                RubrosCNSubrubrosBL srub = new RubrosCNSubrubrosBL();
                ConsultaPadronRubrosBL cprubBL = new ConsultaPadronRubrosBL();
         
                ZonaMix.InnerText = "Mixtura";
                var rubrosCN = encomienda.EncomiendaRubrosCNDTO;
                var rubros = cprubBL.GetRubros(sol.IdConsultaPadron);
                var srubCN = srub.GetSubRubrosByEncomienda(encomienda.IdEncomienda);
                foreach (var rubro in rubros)
                {
                    EncomiendaRubrosCNDTO rubroCN = new EncomiendaRubrosCNDTO();
                    rubroCN.CodigoRubro = rubro.CodidoRubro;
                    rubroCN.DescripcionRubro = rubro.DescripcionRubro;
                    rubroCN.SuperficieHabilitar = (decimal) rubro.SuperficieHabilitar;
                    rubrosCN.Add(rubroCN);
                }
                grdRubros.DataSource = rubrosCN;
                grdRubros.DataBind();

                if (srubCN.Count() > 0)
                {
                    grdSubRubros.DataSource = srubCN;
                    grdSubRubros.DataBind();
                    grdSubRubros.Visible = true;
                }

                //0145978: JADHE 57346 - SGI - Mostrar historial de observaciones
                lblObservacionesRubros.Text = sgiTTBL.GetObservacionPlanchetaTransmision(id_solicitud);

                //0148861: JADHE 57795 - AT - Actualizar lectura de oblea
                lblObservacionesRubros.Text += encomienda.ObservacionesRubros;
            }
        }

        public class StringValue
        {
            public StringValue(string s)
            {
                _value = s;
            }
            public string Value { get { return _value; } set { _value = value; } }
            string _value;
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

                bool RequiereSMP = true;
                int id_tipoubicacion = 0;

                TransferenciaUbicacionesDTO item = (TransferenciaUbicacionesDTO)e.Row.DataItem;

                id_tipoubicacion = item.SubTipoUbicacion.id_tipoubicacion;

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
                    lblTipoUbicacion.Text = item.SubTipoUbicacion.TiposDeUbicacionDTO.DescripcionTipoUbicacion.Trim();
                    lblSubTipoUbicacion.Text = item.SubTipoUbicacion.descripcion_subtipoubicacion.Trim();
                }

                int seccion = (item.Ubicacion.Seccion.HasValue ? item.Ubicacion.Seccion.Value : 0);

                String dir = "";
                foreach (var p in item.Puertas)
                {
                    dir = p.NombreCalle + " " + p.NumeroPuerta;
                    break;
                }
                if (item.Ubicacion.Seccion.HasValue)
                {
                    imgMapa1.ImageUrl = Functions.GetUrlMapa(seccion, item.Ubicacion.Manzana.Trim(), item.Ubicacion.Parcela.Trim(), dir);
                }

                List<UbicacionesPropiedadhorizontalDTO> uphs = new List<UbicacionesPropiedadhorizontalDTO>();
                foreach (var ph in item.PropiedadesHorizontales)
                {
                    uphs.Add(ph.UbicacionPropiedadaHorizontal);
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
    }
}