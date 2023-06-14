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
using static StaticClass.Constantes;

namespace SSIT.Mobile
{
    public partial class GetObleatramite : System.Web.UI.Page
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

        /// <summary>
        /// Obtiene la leyenda de productos Inflamables.
        /// </summary>
        /// <param name="productosInfamables"></param>
        /// <param name="plantas"></param>
        /// <returns></returns>
        public string GetLeyendaProductosInfamables(bool productosInfamables, ICollection<EncomiendaPlantasDTO> plantas)
        {
            if (productosInfamables)
            {
                bool declaraSubsueloSotano = false;
                foreach (var item in plantas)
                {
                    if (item.IdTipoSector == (int)TipoSector.Sótano || item.IdTipoSector == (int)TipoSector.Subsuelo)
                    {
                        declaraSubsueloSotano = true;
                    }
                }

                if (declaraSubsueloSotano)
                {
                    return "Deberá dar cumplimiento al apartado 3.8.10.3 del Código de Edificación Ley Nº 6100, no encontrándose permitido el emplazamiento de materiales inflamables en subsuelos";
                }
                else
                {
                    return "Deberá dar cumplimiento al apartado 3.8.10.3 del Código de Edificación Ley Nº 6100";
                }
            }
            else
            {
                return string.Empty;
            }
        }

        private void CargarDatosTramite(int id_solicitud)
        {
            hdfid_solicitud.Value = Convert.ToString(id_solicitud);
            int id_tramitetarea = 0;

            hdfid_tarea.Value = Convert.ToString(id_tramitetarea);

            string dispo_nro_expediente = "";

            SSITSolicitudesBL blSol = new SSITSolicitudesBL();
            var sol = blSol.Single(id_solicitud);



            string nro_expediente_sade = sol.NroExpedienteSade;
            if (nro_expediente_sade != null)
            {
                string[] datos = nro_expediente_sade.Split('-');
                if (datos.Length > 2)
                    dispo_nro_expediente = Convert.ToString(Convert.ToInt32(datos[2])) + "/" + datos[1];
            }
            lblNroSolicitud.Text = id_solicitud.ToString();
            lblNroExpediente.Text = dispo_nro_expediente;

            if (sol.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.VENCIDA)
            {
                if (sol.FechaLibrado == null &&
                    (sol.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.APRO ))
                    lblFechaLibrado.Text = "<font color='red'><b>EL PRESENTE TRAMITE NO SE ENCUENTRA LIBRADO AL USO</b></font>";
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


            //lblTipoTramite.Text = sol.TipoTramiteDescripcion + " " + sol.TipoExpedienteDescripcion;
            //Valido si es una ECI...
            if (!sol.EsECI)
            {
                lblTipoTramite.Text = sol.TipoTramiteDescripcion + " " + sol.TipoExpedienteDescripcion;
            }
            else
            {
                switch (sol.IdTipoTramite)
                {
                    case (int)TipoTramite.HabilitacionECIAdecuacion:
                        lblTipoTramite.Text = TipoTramiteDescripcion.AdecuacionECI + " - " + sol.TipoExpedienteDescripcion;
                        break;
                    case (int)TipoTramite.HabilitacionECIHabilitacion:
                        lblTipoTramite.Text = TipoTramiteDescripcion.HabilitacionECI + " - " + sol.TipoExpedienteDescripcion;
                        break;

                }
            }

            EngineBL engBL = new EngineBL();
            string descripcionCircuito = engBL.GetDescripcionCircuito(id_solicitud);

            if (descripcionCircuito != null)
                lblTipoTramite.Text += " - " + descripcionCircuito;


            EncomiendaBL blEnc = new EncomiendaBL();
            var listEnc = blEnc.GetByFKIdSolicitud(id_solicitud).Where(x => x.IdEstado == (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo).ToList();

            var encomienda = listEnc.OrderByDescending(x => x.IdEncomienda).FirstOrDefault();

            if (encomienda != null)
            {
                ws_Interface_AGC servicio = new ws_Interface_AGC();
                wsResultado ws_resultado_CAA = new wsResultado();

                ParametrosBL blParam = new ParametrosBL();
                servicio.Url = blParam.GetParametroChar("SIPSA.Url.Webservice.ws_Interface_AGC");
                string username_servicio = blParam.GetParametroChar("SIPSA.Url.Webservice.ws_Interface_AGC.User");
                string password_servicio = blParam.GetParametroChar("SIPSA.Url.Webservice.ws_Interface_AGC.Password");
                List<int> lst_encomiendas = new List<int>();
                lst_encomiendas.Add(encomienda.IdEncomienda);
                DtoCAA[] l = servicio.Get_CAAs_by_Encomiendas(username_servicio, password_servicio, lst_encomiendas.ToArray(), ref ws_resultado_CAA);

                lblNroCAA.Text = l.Length > 0 ? l[0].id_caa.ToString() : "";
                lblEstado.Text = sol.TipoEstadoSolicitudDTO.Descripcion;
                bool automatica = false;
                EngineBL blEng = new EngineBL();

                SGITramitesTareasDTO tramite = blEng.GetUltimaTareaHabilitacion(sol.IdSolicitud);
                if (tramite != null && blEng.GetIdCircuito(tramite.IdTarea) == (int)Constantes.ENG_Circuitos.SSP3)
                    automatica = true;

                SGITramitesTareasBL sgiTTBL = new SGITramitesTareasBL();
                //lblObservaciones.Text = sgiTTBL.Buscar_ObservacionPlancheta(id_solicitud);
                lblObservaciones.Text = sol.FechaDisposicion == null ? "" : sgiTTBL.Buscar_ObservacionPlanchetaDispoFirmada(id_solicitud, sol.FechaDisposicion);

                if (!automatica)
                {
                    lblFechaDisposicion.Text = sol.FechaDisposicion == null ? "" : sol.FechaDisposicion.Value.ToString();
                    lblNroDisposicionSADE.Text = sol.NroEspecialSADE == null ? "" : sol.NroEspecialSADE.ToString();

                    lblFechaDisposicion.Visible = true;
                    lblFechaDisposicionText.Visible = true;
                    lblNroDisposicionSADE.Visible = true;
                    lblNroDisposicionSADEText.Visible = true;
                    lblObservaciones.Visible = sol.FechaDisposicion == null ? false : true;
                    lblObservacionesText.Visible = true;
                    lblTextCertificado.Visible = false;
                }
                else
                {
                    lblFechaDisposicion.Visible = false;
                    lblFechaDisposicionText.Visible = false;
                    lblNroDisposicionSADE.Visible = false;
                    lblNroDisposicionSADEText.Visible = false;
                    lblObservaciones.Visible = sol.FechaDisposicion == null ? false : true;
                    lblObservacionesText.Visible = true;
                    lblTextCertificado.Visible = true;
                }


                TitularesBL titularesBL = new TitularesBL();

                var lstTitulares = titularesBL.GetTitularesSolicitud(id_solicitud);
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


                lblLeyendaProductosInflamables.Text = GetLeyendaProductosInfamables(encomienda.ProductosInflamables, plantas);


                //Se cambia por la mixtura en encomienda_ubicacion:_mixtura
                int nroSolReferencia = 0;
                int.TryParse(ConfigurationManager.AppSettings["NroSolicitudReferencia"], out nroSolReferencia);
                if (id_solicitud <= nroSolReferencia)
                    lblZonaDeclarada.Text = encomienda.ZonaDeclarada;
                else
                {
                    EncomiendaUbicacionesBL ubic = new EncomiendaUbicacionesBL();
                    IEnumerable<EncomiendaUbicacionesDTO> blDto = ubic.GetByFKIdEncomienda(encomienda.IdEncomienda);

                    lblZonaDeclarada.Text = ubic.GetMixDistritoZonaySubZonaByEncomienda(encomienda.IdEncomienda);

                    //foreach (var ue in blDto)
                    //{
                    //    foreach (var uem in ue.EncomiendaUbicacionesMixturasDTO)
                    //    {
                    //        //Busco la descripcion 
                    //        UbicacionesZonasMixturasBL uzmBL = new UbicacionesZonasMixturasBL();
                    //        UbicacionesZonasMixturasDTO uzm = uzmBL.Single(uem.IdZonaMixtura);
                    //        lblZonaDeclarada.Text += uzm.Codigo + (lblZonaDeclarada.Text.Length > 0 ? ", " : "");
                    //    }
                    //}
                }

                var dl = encomienda.EncomiendaDatosLocalDTO.FirstOrDefault();

                lblCantOperarios.Text = dl.cantidad_operarios_dl.ToString();
                lblSuperficieTotal.Text = string.Format("{0:###,###,##0.00} m2.", dl.superficie_cubierta_dl + dl.superficie_descubierta_dl);

                if (dl.ampliacion_superficie.HasValue && dl.ampliacion_superficie.Value)
                    lblSuperficieTotal.Text = string.Format("{0:###,###,##0.00} m2.", dl.superficie_cubierta_amp + dl.superficie_descubierta_amp);

                SSITSolicitudesUbicacionesBL BlUbicaciones = new SSITSolicitudesUbicacionesBL();

                var elements = BlUbicaciones.GetByFKIdSolicitud(id_solicitud);

                SSITSolicitudesBL solbL = new SSITSolicitudesBL();
                TransferenciasSolicitudesBL TransfbL = new TransferenciasSolicitudesBL();

                gridubicacion_db.DataSource = elements.ToList();
                gridubicacion_db.DataBind();
                List<int> lisSol = new List<int>();
                lisSol.Add(id_solicitud);

                lblDireccion.Text = blSol.GetDireccionSSIT(lisSol);

                RubrosBL rubBL = new RubrosBL();
                RubrosCNSubrubrosBL srub = new RubrosCNSubrubrosBL();
                var deposBL = new Encomienda_RubrosCN_DepositoBL();
                if (id_solicitud > nroSolReferencia)
                {
                    ZonaMix.InnerText = "Mixtura";
                    var rubrosCN = encomienda.EncomiendaRubrosCNDTO;
                    var srubCN = srub.GetSubRubrosByEncomienda(encomienda.IdEncomienda);
                    grdRubros.DataSource = rubrosCN;
                    grdRubros.DataBind();

                    if (srubCN.Count() > 0)
                    {
                        grdSubRubros.DataSource = srubCN;
                        grdSubRubros.DataBind();
                        grdSubRubros.Visible = true;
                    }

                    var deposCN = deposBL.GetByEncomienda(encomienda.IdEncomienda);
                    var depos = new List<RubrosDepositosCNDTO>();
                    foreach (var ed in deposCN)
                    {
                        depos.Add(ed.RubrosDepositosCNDTO);
                    }
                    if (depos.Count > 0)
                    {
                        grdDepositos.DataSource = depos;
                        grdDepositos.DataBind();
                        grdDepositos.Visible = true;
                    }
                }
                else
                {
                    ZonaMix.InnerText = "Zona";
                    var rubros = encomienda.EncomiendaRubrosDTO;
                    grdRubros.DataSource = rubros;
                    grdRubros.DataBind();


                    var lstCodRubros = rubBL.GetInfoAdicionalRubros(encomienda.EncomiendaRubrosDTO.Select(s => s.CodigoRubro).ToList());

                    if (lstCodRubros.Count() == 0)
                        pnlInformacionAdicional.Style["display"] = "none;";
                    else
                        pnlInformacionAdicional.Style["display"] = "block;";

                    blInfoAdicional.DataSource = lstCodRubros;
                    blInfoAdicional.DataBind();
                }

                lblObservacionesRubros.Text = encomienda.ObservacionesRubros;

                int idGenExp = blEng.getTareaGenerarExpediente(sol.IdSolicitud);
                if (idGenExp > 0)
                {
                    var plano = blEng.GetByFKIdTramiteTareasIdSolicitud(idGenExp, sol.IdSolicitud);
                    if (!string.IsNullOrEmpty(plano))
                        lblPlanoVisado.Text = "Plano de Uso de Actividad Económica: " + plano;
                }

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

                SSITSolicitudesUbicacionesDTO item = (SSITSolicitudesUbicacionesDTO)e.Row.DataItem;

                id_tipoubicacion = item.SubTipoUbicacionesDTO.id_tipoubicacion;

                if (item.SubTipoUbicacionesDTO.TiposDeUbicacionDTO.RequiereSMP.HasValue)
                    RequiereSMP = item.SubTipoUbicacionesDTO.TiposDeUbicacionDTO.RequiereSMP.Value;

                if (RequiereSMP)
                {
                    if (item.UbicacionesDTO.NroPartidaMatriz.HasValue)
                        lbl_NroPartidaMatriz.Text = item.UbicacionesDTO.NroPartidaMatriz.Value.ToString();

                    if (item.UbicacionesDTO.Seccion.HasValue)
                        lbl_seccion.Text = item.UbicacionesDTO.Seccion.Value.ToString();

                    lbl_manzana.Text = item.UbicacionesDTO.Manzana.Trim();
                    lbl_parcela.Text = item.UbicacionesDTO.Parcela.Trim();

                }

                pnlSMP.Visible = RequiereSMP;

                if (!id_tipoubicacion.Equals((int)Constantes.TiposDeUbicacion.ParcelaComun))
                {
                    pnlTipoUbicacion.Visible = true;
                    lblTipoUbicacion.Text = item.SubTipoUbicacionesDTO.TiposDeUbicacionDTO.DescripcionTipoUbicacion.Trim();
                    lblSubTipoUbicacion.Text = item.SubTipoUbicacionesDTO.descripcion_subtipoubicacion.Trim();
                }

                int seccion = (item.UbicacionesDTO.Seccion.HasValue ? item.UbicacionesDTO.Seccion.Value : 0);

                String dir = "";
                foreach (var p in item.SSITSolicitudesUbicacionesPuertasDTO)
                {
                    dir = p.NombreCalle + " " + p.NroPuerta;
                    break;
                }
                if (item.UbicacionesDTO.Seccion.HasValue)
                {
                    imgMapa1.ImageUrl = Functions.GetUrlMapa(seccion, item.UbicacionesDTO.Manzana.Trim(), item.UbicacionesDTO.Parcela.Trim(), dir);
                }

                List<UbicacionesPropiedadhorizontalDTO> uphs = new List<UbicacionesPropiedadhorizontalDTO>();
                foreach (var ph in item.SSITSolicitudesUbicacionesPropiedadHorizontalDTO)
                {
                    uphs.Add(ph.UbicacionesPropiedadhorizontalDTO);
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