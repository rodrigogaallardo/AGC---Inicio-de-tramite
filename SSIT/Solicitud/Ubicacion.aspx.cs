using BusinesLayer.Implementation;
using DataTransferObject;
using SSIT.App_Components;
using SSIT.Solicitud.Habilitacion.Controls;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace SSIT.Solicitud.Habilitacion
{
    public partial class Ubicacion : SecurePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                hid_return_url.Value = Request.Url.AbsoluteUri;
                ComprobarSolicitud();
                Titulo.CargarDatos(IdSolicitud, Constantes.TIT_UBICACION);
                
            }
            this.BuscarUbicacion.AgregarUbicacionClick += BuscarUbicacion_AgregarUbicacionClick;
            this.BuscarUbicacion.CerrarClick += BuscarUbicacion_CerrarClick;
        }

        protected void BuscarUbicacion_CerrarClick(object sender, EventArgs e)
        {
            CargarDatos(IdSolicitud);
            this.EjecutarScript(updUbicaciones, "finalizarCarga();");
            updUbicaciones.Update();

            ScriptManager.RegisterStartupScript(updUbicaciones, updUbicaciones.GetType(), "hidefrmAgregarUbicacion", "hidefrmAgregarUbicacion();", true);

        }

        private int IdSolicitud
        {
            get
            {
                int ret = 0;
                int.TryParse(Page.RouteData.Values["id_solicitud"].ToString(), out ret);
                return ret;
            }
        }
        private int id_tipo_tramite
        {
            get
            {
                int ret = 0;
                
                int.TryParse(hid_id_tipo_tramite.Value, out ret);
                return ret;
            }
            set
            {
                hid_id_tipo_tramite.Value = value.ToString();
            }

        }
        private void ComprobarSolicitud()
        {
            if (Page.RouteData.Values["id_solicitud"] != null)
            {
                SSITSolicitudesBL solBL = new SSITSolicitudesBL();
                var sol = solBL.Single(IdSolicitud);
                if (sol != null)
                {
                    this.id_tipo_tramite = sol.IdTipoTramite;
                    /*Falta el userID y hacer overload de getuserid con el tipo de tramite*/
                    Guid userid_solicitud = (Guid)Membership.GetUser().ProviderUserKey;

                    if (userid_solicitud != sol.CreateUser)
                        Server.Transfer("~/Errores/Error3002.aspx");

                    if (!(sol.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.INCOM ||
                                 sol.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.COMP ||
                                 sol.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.DATOSCONF ||
                                 sol.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.OBSERVADO))
                    {
                        Server.Transfer("~/Errores/Error3003.aspx");
                    }

                    // Si proviende de una solicitud anterior se puede modificar la ubicacion para agregar puertas
                    if (sol.SSITSolicitudesOrigenDTO!= null && sol.SSITSolicitudesOrigenDTO.id_solicitud_origen.HasValue)
                    {
                        //Para los casos de herencia de datos por transmisión ampliación o RDU es necesario que en caso de existencia de baja de ubicación la misma pueda ser editada.
                        //https://mantis.grupomost.com/view.php?id=166260
                        SSITSolicitudesBL ssitBL = new SSITSolicitudesBL();
                        SSITSolicitudesUbicacionesDTO ssitUbic = ssitBL.Single(Convert.ToInt32(sol.SSITSolicitudesOrigenDTO.id_solicitud_origen)).SSITSolicitudesUbicacionesDTO.FirstOrDefault();
                        UbicacionesBL ub = new UbicacionesBL();
                        bool bajaUbicacion = ub.Single(Convert.ToInt32(ssitUbic.IdUbicacion)).BajaLogica;

                        if (!bajaUbicacion)
                            Server.Transfer("~/Errores/Error3007.aspx");
                            //SoloEditarUbicacion = true;
                    }

                }
                else
                    Server.Transfer("~/Errores/Error3004.aspx");
            }
            else
                Server.Transfer("~/Errores/Error3001.aspx");
        }

        protected void btnCargarDatos_Click(object sender, EventArgs e)
        {
            try
            {
                CargarDatos(IdSolicitud);
                this.EjecutarScript(updUbicaciones, "finalizarCarga();");
                if (hid_return_url.Value.Contains("Editar"))
                {
                    btnVolver.Style["display"] = "inline";
                    updBotonesGuardar.Update();
                }

            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                this.EjecutarScript(updUbicaciones, "finalizarCarga();showfrmError();");
            }

        }

        private void CargarDatos(int id_solicitud)
        {
            visUbicaciones.Editable = true;
            visUbicaciones.CargarDatos(id_solicitud);

            if(this.id_tipo_tramite == (int)Constantes.TipoTramite.AMPLIACION ||
                this.id_tipo_tramite == (int)Constantes.TipoTramite.REDISTRIBUCION_USO)
            {
                BuscarUbicacion.PermitirPuertasOficiales = true;
            }
        }

        protected void visUbicaciones_EliminarClick(object sender, ucEliminarEventsArgs args)
        {

            btnEliminar_Si.CommandArgument = args.IdUbicacion.ToString();
            //this.EjecutarScript(updUbicaciones, "showfrmConfirmarEliminar();");

            ScriptManager.RegisterStartupScript(updUbicaciones, updUbicaciones.GetType(),
              "updUbicaciones", "showfrmConfirmarEliminar();", true);

        }

        protected void visUbicaciones_EditarClick(object sender, ucEditarEventsArgs args)
        {
            this.BuscarUbicacion.idUbicacion = args.IdUbicacion;
            ScriptManager.RegisterStartupScript(updAgregarUbicacion, updAgregarUbicacion.GetType(), "showfrmAgregarUbicacion()", "showfrmAgregarUbicacion();", true);
            this.BuscarUbicacion.editar();

        }

        protected void btnEliminar_Si_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnEliminar_Si = (Button)sender;
                int IdSolicitudUbicacion = int.Parse(btnEliminar_Si.CommandArgument);

                // Eliminar la ubicación.
                SSITSolicitudesUbicacionesBL encomiendaUbicacionBL = new SSITSolicitudesUbicacionesBL();
                var dto = new SSITSolicitudesUbicacionesDTO()
                {
                    IdSolicitudUbicacion = IdSolicitudUbicacion,
                    IdSolicitud = IdSolicitud
                };
                encomiendaUbicacionBL.Delete(dto);

                // vuelve a cargar los datos.
                visUbicaciones.CargarDatos(this.IdSolicitud);

                ScriptManager.RegisterStartupScript(updUbicaciones, updUbicaciones.GetType(),
              "updUbicaciones2", "hidefrmConfirmarEliminar();", true);

                updUbicaciones.Update();


            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                this.EjecutarScript(updConfirmarEliminar, "hidefrmConfirmarEliminar();showfrmError();");
            }
        }
        protected void btnVolver_Click(object sender, EventArgs e)
        {
            string url = "";
            try
            {
                if (this.id_tipo_tramite == (int)Constantes.TipoTramite.AMPLIACION)
                    url = string.Format("~/" + RouteConfig.VISOR_SOLICITUD_AMPLIACION + "{0}", IdSolicitud);
                else if (this.id_tipo_tramite == (int)Constantes.TipoTramite.REDISTRIBUCION_USO)
                    url = string.Format("~/" + RouteConfig.VISOR_SOLICITUD_REDISTRIBUCION_USO + "{0}", IdSolicitud);
                else
                    url = string.Format("~/" + RouteConfig.VISOR_SOLICITUD + "{0}", IdSolicitud);

                Response.Redirect(url);
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                this.EjecutarScript(updBotonesGuardar, "showfrmError();");

            }
        }
        private void BuscarUbicacion_AgregarUbicacionClick(object sender, ucAgregarUbicacionEventsArgs e)
        {
            UpdatePanel upd = e.upd;
            UbicacionesZonasMixturasBL uMixturas = new UbicacionesZonasMixturasBL();
            UbicacionesCatalogoDistritosBL uDistritos = new UbicacionesCatalogoDistritosBL();
            //Guid userid = Functions.GetUserid();           
            try
            {
                //Alta de la ubicación   
                SSITSolicitudesUbicacionesBL ssitBL = new SSITSolicitudesUbicacionesBL();
                SSITSolicitudesUbicacionesDTO ubicacion = new SSITSolicitudesUbicacionesDTO();
                ubicacion.IdSolicitud = IdSolicitud;
                ubicacion.IdUbicacion = e.id_ubicacion;
                ubicacion.IdSubtipoUbicacion = e.id_subtipoubicacion;
                ubicacion.LocalSubtipoUbicacion = e.local_subtipoubicacion;
                ubicacion.DeptoLocalUbicacion = e.vDeptoLocalOtros.Trim();
                ubicacion.Depto = e.vDepto.Trim();
                ubicacion.Local = e.vLocal.Trim();
                ubicacion.Torre = e.vTorre.Trim();
                ubicacion.CreateDate = DateTime.Now;
                ubicacion.CreateUser = (Guid)Membership.GetUser().ProviderUserKey;

                List<UbicacionesPropiedadhorizontalDTO> propiedadesHorizontales = new List<UbicacionesPropiedadhorizontalDTO>();
                //Alta de las propiedades horizontales
                foreach (int id_propiedad_horizontal in e.ids_propiedades_horizontales)
                {
                    propiedadesHorizontales.Add(new UbicacionesPropiedadhorizontalDTO()
                    {
                        IdPropiedadHorizontal = id_propiedad_horizontal
                    });
                }
                List<UbicacionesPuertasDTO> puertas = new List<UbicacionesPuertasDTO>();
                //Alta de puertas
                foreach (var puerta in e.Puertas)
                {
                    puertas.Add(new UbicacionesPuertasDTO()
                    {
                        CodigoCalle = puerta.codigo_calle,
                        NroPuertaUbic = puerta.NroPuerta,
                        IdUbicacion = e.id_ubicacion
                    });
                }
                //visUbicaciones.CargarDatos(this.id_solicitud);

                //Alta de Zonas Mixturas
                List<SSITSolicitudesUbicacionesMixturasDTO> ssitSolUbicMixturas = new List<SSITSolicitudesUbicacionesMixturasDTO>();
                List<UbicacionesZonasMixturasDTO> zonasMixturas = uMixturas.GetZonasUbicacion(e.id_ubicacion).ToList();
                foreach (var ZonasMixturasDTO in zonasMixturas)
                {
                    ssitSolUbicMixturas.Add(new SSITSolicitudesUbicacionesMixturasDTO()
                    {
                        IdZonaMixtura = ZonasMixturasDTO.IdZona,
                        id_solicitudubicacion = 0
                    });
                }

                //Alta de Distritos

                int IdUbicacion = ubicacion.IdUbicacion ?? 0;

                List<SSITSolicitudesUbicacionesDistritoDTO> ssitSolUbicDistritos = new List<SSITSolicitudesUbicacionesDistritoDTO>();
                List<UbicacionesCatalogoDistritosDTO> distritos = uDistritos.GetDistritosUbicacion(e.id_ubicacion).ToList();
                foreach (var CatalogoDistritosDTO in distritos)
                {
                    ssitSolUbicDistritos.Add(new SSITSolicitudesUbicacionesDistritoDTO()
                    {
                        IdDistrito = CatalogoDistritosDTO.IdDistrito,
                        IdZona = uDistritos.GetIdZonaByUbicacion(IdUbicacion),
                        IdSubZona = uDistritos.GetIdSubZonaByUbicacion(IdUbicacion),
                        //id_solicitudubicacion = 0
                    });
                }

                ubicacion.SSITSolicitudesUbicacionesDistritosDTO = ssitSolUbicDistritos;
                ubicacion.SSITSolicitudesUbicacionesMixturasDTO = ssitSolUbicMixturas;

                ubicacion.PropiedadesHorizontales = propiedadesHorizontales;
                ubicacion.Puertas = puertas;

                ssitBL.Insert(ubicacion);
                CargarDatos(IdSolicitud);
                updUbicaciones.Update();
                this.EjecutarScript(updUbicaciones, "hidefrmAgregarUbicacion();");
                ScriptManager.RegisterStartupScript(updUbicaciones, updUbicaciones.GetType(), "hidefrmAgregarUbicacion", "hidefrmAgregarUbicacion();", true);

            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                e.Cancel = true;
                lblError.Text = (ex.Message);

                updUbicaciones.Update();
                this.EjecutarScript(updUbicaciones, "hidefrmAgregarUbicacion();");
                ScriptManager.RegisterStartupScript(updUbicaciones, updUbicaciones.GetType(), "hidefrmAgregarUbicacion", "hidefrmAgregarUbicacion();", true);

                this.EjecutarScript(updUbicaciones, "showfrmError();");
                ScriptManager.RegisterStartupScript(updUbicaciones, updUbicaciones.GetType(), "showfrmError", "showfrmError();", true);


            }
        }

        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            string url = "";
            try
            {
                CheckBox chkServidumbre = (CheckBox)visUbicaciones.FindControl("chkServidumbre");

                GridView gridubicacion_db = (GridView)visUbicaciones.FindControl("gridubicacion_db");

                bool Servidumbre_paso;

                if (!chkServidumbre.Checked && gridubicacion_db.Rows.Count > 1)
                    throw new Exception("Deberá tildar declarar la existencia de la servidumbre de paso para poder seleccionar más de una ubicación");

                if (gridubicacion_db.Rows.Count > 1)
                    Servidumbre_paso = true;
                else
                    Servidumbre_paso = false;

                SSITSolicitudesBL solBL = new SSITSolicitudesBL();

                var solDTO = solBL.Single(IdSolicitud);
                solDTO.Servidumbre_paso = Servidumbre_paso;
                solBL.Update(solDTO);


                if (this.id_tipo_tramite == (int)Constantes.TipoTramite.AMPLIACION)
                    url = string.Format("~/" + RouteConfig.VISOR_SOLICITUD_AMPLIACION + "{0}", IdSolicitud);
                else if (this.id_tipo_tramite == (int)Constantes.TipoTramite.REDISTRIBUCION_USO)
                    url = string.Format("~/" + RouteConfig.VISOR_SOLICITUD_REDISTRIBUCION_USO + "{0}", IdSolicitud);
                else if (this.id_tipo_tramite == (int)Constantes.TipoTramite.PERMISO)
                {
                    if (hid_return_url.Value.Contains("Editar"))
                        url = string.Format("~/" + RouteConfig.VISOR_SOLICITUD_PERMISO_MC + "{0}", IdSolicitud);
                    else
                        url = string.Format("~/" + RouteConfig.AGREGAR_DATOSLOCAL_SOLICITUD_PERMISO_MC + "{0}", IdSolicitud);
                }
                else
                    url = string.Format("~/" + RouteConfig.VISOR_SOLICITUD + "{0}", IdSolicitud);

                Response.Redirect(url);

            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                this.EjecutarScript(updBotonesGuardar, "showfrmError();");
            }
        }
        protected void cmdAnterior_Click(object sender, EventArgs e)
        {

            try
            {
                gridubicacion.PageIndex = gridubicacion.PageIndex - 1;
                //BuscarUbicaciones();
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                string error = ex.Message;
            }

        }
        protected void cmdSiguiente_Click(object sender, EventArgs e)
        {

            try
            {
                gridubicacion.PageIndex = gridubicacion.PageIndex + 1;
                //BuscarUbicaciones();
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                string error = ex.Message;
            }

        }

        protected void gridubicacion_DataBound(object sender, EventArgs e)
        {
            GridView grid = gridubicacion;
            GridViewRow fila = (GridViewRow)grid.BottomPagerRow;

            if (fila != null)
            {
                Button btnAnterior = (Button)fila.Cells[0].FindControl("cmdAnterior");
                Button btnSiguiente = (Button)fila.Cells[0].FindControl("cmdSiguiente");

                if (grid.PageIndex == 0)
                    btnAnterior.Visible = false;
                else
                    btnAnterior.Visible = true;

                if (grid.PageIndex == grid.PageCount - 1)
                    btnSiguiente.Visible = false;
                else
                    btnSiguiente.Visible = true;


                // Ocultar todos los botones con Números de Página
                for (int i = 1; i <= 19; i++)
                {
                    Button btn = (Button)fila.Cells[0].FindControl("cmdPage" + i.ToString());
                    btn.Visible = false;
                }


                if (grid.PageIndex == 0 || grid.PageCount <= 10)
                {
                    // Mostrar 10 botones o el máximo de páginas

                    for (int i = 1; i <= 10; i++)
                    {
                        if (i <= grid.PageCount)
                        {
                            Button btn = (Button)fila.Cells[0].FindControl("cmdPage" + i.ToString());
                            btn.Text = i.ToString();
                            btn.Visible = true;
                        }
                    }
                }
                else
                {
                    // Mostrar 9 botones hacia la izquierda y 9 hacia la derecha
                    // o bien los que sea posible en caso de no llegar a 9

                    int CantBucles = 0;

                    Button btnPage10 = (Button)fila.Cells[0].FindControl("cmdPage10");
                    btnPage10.Visible = true;
                    btnPage10.Text = Convert.ToString(grid.PageIndex + 1);

                    // Ubica los 9 botones hacia la izquierda
                    for (int i = grid.PageIndex - 1; i >= grid.PageIndex - 9; i--)
                    {
                        CantBucles++;
                        if (i >= 0)
                        {
                            Button btn = (Button)fila.Cells[0].FindControl("cmdPage" + Convert.ToString(10 - CantBucles));
                            btn.Visible = true;
                            btn.Text = Convert.ToString(i + 1);
                        }

                    }

                    CantBucles = 0;
                    // Ubica los 9 botones hacia la derecha
                    for (int i = grid.PageIndex + 1; i <= grid.PageIndex + 9; i++)
                    {
                        CantBucles++;
                        if (i <= grid.PageCount - 1)
                        {
                            Button btn = (Button)fila.Cells[0].FindControl("cmdPage" + Convert.ToString(10 + CantBucles));
                            btn.Visible = true;
                            btn.Text = Convert.ToString(i + 1);
                        }
                    }



                }

                //poner estilo sin seleccion a todos los botones
                Button cmdPage;
                string btnPage = "";
                for (int i = 1; i <= 19; i++)
                {
                    btnPage = "cmdPage" + i.ToString();
                    cmdPage = (Button)fila.Cells[0].FindControl(btnPage);
                    if (cmdPage != null)
                        cmdPage.CssClass = "btnPagerGrid";

                }


                // busca el boton por el texto para marcarlo como seleccionado
                string btnText = Convert.ToString(grid.PageIndex + 1);
                foreach (Control ctl in fila.Cells[0].FindControl("pnlpager").Controls)
                {
                    if (ctl is Button)
                    {
                        Button btn = (Button)ctl;
                        if (btn.Text.Equals(btnText))
                        {
                            btn.CssClass = "btnPagerGrid-selected";
                        }
                    }
                }

            }

        }
        protected void cmdPage(object sender, EventArgs e)
        {
            try
            {
                Button cmdPage = (Button)sender;
                gridubicacion.PageIndex = int.Parse(cmdPage.Text) - 1;
                //BuscarUbicaciones();
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                string error = ex.Message;
            }

        }

    }
}