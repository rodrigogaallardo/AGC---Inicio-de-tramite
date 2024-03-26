using BusinesLayer.Implementation;
using DataTransferObject;
using SSIT.App_Components;
using SSIT.Solicitud.Transferencia.Controls;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSIT.Solicitud.Transferencia
{
    public partial class Ubicacion : BasePage
    {
        public int IdSolicitud
        {
            get
            {
                return Convert.ToInt32(Page.RouteData.Values["id_solicitud"]);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //ComprobarSolicitud();
                hid_return_url.Value = Request.Url.AbsoluteUri;
            }
            this.BuscarUbicacion.AgregarUbicacionClick += BuscarUbicacion_AgregarUbicacionClick;
            this.BuscarUbicacion.CerrarClick += BuscarUbicacion_CerrarClick;
        }

        private void BuscarUbicacion_AgregarUbicacionClick1(object sender, Controls.ucAgregarUbicacionEventsArgs e)
        {
            UpdatePanel upd = e.upd;
            UbicacionesZonasMixturasBL uMixturas = new UbicacionesZonasMixturasBL();
            UbicacionesCatalogoDistritosBL uDistritos = new UbicacionesCatalogoDistritosBL();
            try
            {
                //Alta de la ubicación   
                TransferenciaUbicacionesBL transfUbicacionesBL = new TransferenciaUbicacionesBL();
                TransferenciaUbicacionesDTO transf = new TransferenciaUbicacionesDTO();
                transf.IdSolicitud = IdSolicitud;
                transf.IdUbicacion = e.id_ubicacion;
                transf.IdSubTipoUbicacion = e.id_subtipoubicacion;
                transf.LocalSubTipoUbicacion = e.local_subtipoubicacion;
                transf.DeptoLocalTransferenciaUbicacion = e.vDeptoLocalOtros.Trim();
                transf.Depto = e.vDepto.Trim();
                transf.Local = e.vLocal.Trim();
                transf.Torre = e.vTorre.Trim();
                transf.CreateDate = DateTime.Now;
                transf.CreateUser = (Guid)Membership.GetUser().ProviderUserKey;

                List<TransferenciasUbicacionPropiedadHorizontalDTO> propiedadesHorizontales = new List<TransferenciasUbicacionPropiedadHorizontalDTO>();
                //Alta de las propiedades horizontales

                foreach (int id_propiedad_horizontal in e.ids_propiedades_horizontales)
                {
                    propiedadesHorizontales.Add(new TransferenciasUbicacionPropiedadHorizontalDTO()
                    {
                        IdPropiedadHorizontal = id_propiedad_horizontal,
                    });
                }

                List<TransferenciasUbicacionesPuertasDTO> puertas = new List<TransferenciasUbicacionesPuertasDTO>();

                //Alta de puertas
                foreach (var puerta in e.Puertas)
                {
                    puertas.Add(new TransferenciasUbicacionesPuertasDTO()
                    {
                        CodigoCalle = puerta.codigo_calle,
                        NumeroPuerta = puerta.NroPuerta
                    });
                }

                //Alta de Zonas Mixturas
                List<TransferenciaUbicacionesMixturasDTO> transfSolUbicMixturas = new List<TransferenciaUbicacionesMixturasDTO>();
                List<UbicacionesZonasMixturasDTO> zonasMixturas = uMixturas.GetZonasUbicacion(e.id_ubicacion).ToList();
                foreach (var ZonasMixturasDTO in zonasMixturas)
                {
                    transfSolUbicMixturas.Add(new TransferenciaUbicacionesMixturasDTO()
                    {
                        IdZonaMixtura = ZonasMixturasDTO.IdZona,
                        id_transfubicacion = 0
                    });
                }

                //Alta de Distritos
                List<TransferenciaUbicacionesDistritosDTO> transfSolUbicDistritos = new List<TransferenciaUbicacionesDistritosDTO>();
                List<UbicacionesCatalogoDistritosDTO> distritos = uDistritos.GetDistritosUbicacion(e.id_ubicacion).ToList();

                int IdUbicacion = transf.IdUbicacion ?? 0;

                foreach (var CatalogoDistritosDTO in distritos)
                {
                    transfSolUbicDistritos.Add(new TransferenciaUbicacionesDistritosDTO()
                    {
                        IdDistrito = CatalogoDistritosDTO.IdDistrito,
                        IdZona = uDistritos.GetIdZonaByUbicacion(IdUbicacion),
                        IdSubZona = uDistritos.GetIdSubZonaByUbicacion(IdUbicacion),
                        id_transfubicacion = 0
                    });
                }

                transf.TransferenciaUbicacionesDistritosDTO = transfSolUbicDistritos;
                transf.TransferenciaUbicacionesMixturasDTO = transfSolUbicMixturas;


                transf.PropiedadesHorizontales = propiedadesHorizontales;
                transf.Puertas = puertas;

                transfUbicacionesBL.Insert(transf);
                TransferenciasSolicitudesBL trBL = new TransferenciasSolicitudesBL();
                visUbicaciones.CargarDatos(trBL.Single(IdSolicitud));
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

        protected void BuscarUbicacion_AgregarUbicacionClick(object sender, ucAgregarUbicacionEventsArgs e)
        {
            UpdatePanel upd = e.upd;
            try
            {
                //Alta de la ubicación   
                TransferenciaUbicacionesBL transfUbicacionesBL = new TransferenciaUbicacionesBL();
                TransferenciaUbicacionesDTO transf = new TransferenciaUbicacionesDTO();
                transf.IdSolicitud = IdSolicitud;
                transf.IdUbicacion = e.id_ubicacion;
                transf.IdSubTipoUbicacion = e.id_subtipoubicacion;
                transf.LocalSubTipoUbicacion = e.local_subtipoubicacion;
                transf.DeptoLocalTransferenciaUbicacion = e.vDeptoLocalOtros.Trim();
                transf.Depto = e.vDepto.Trim();
                transf.Local = e.vLocal.Trim();
                transf.Torre = e.vTorre.Trim();
                transf.CreateDate = DateTime.Now;
                transf.CreateUser = (Guid)Membership.GetUser().ProviderUserKey;

                List<TransferenciasUbicacionPropiedadHorizontalDTO> propiedadesHorizontales = new List<TransferenciasUbicacionPropiedadHorizontalDTO>();
                //Alta de las propiedades horizontales

                foreach (int id_propiedad_horizontal in e.ids_propiedades_horizontales)
                {
                    propiedadesHorizontales.Add(new TransferenciasUbicacionPropiedadHorizontalDTO()
                    {
                        IdPropiedadHorizontal = id_propiedad_horizontal,
                    });
                }

                List<TransferenciasUbicacionesPuertasDTO> puertas = new List<TransferenciasUbicacionesPuertasDTO>();

                //Alta de puertas
                foreach (var puerta in e.Puertas)
                {
                    puertas.Add(new TransferenciasUbicacionesPuertasDTO()
                    {
                        CodigoCalle = puerta.codigo_calle,
                        NumeroPuerta = puerta.NroPuerta
                    });
                }

                transf.PropiedadesHorizontales = propiedadesHorizontales;
                transf.Puertas = puertas;

                transfUbicacionesBL.Insert(transf);
                TransferenciasSolicitudesBL trBL = new TransferenciasSolicitudesBL();
                visUbicaciones.CargarDatos(trBL.Single(IdSolicitud));
                updUbicaciones.Update();
                this.EjecutarScript(upd, "hidefrmAgregarUbicacion();");
                ScriptManager.RegisterStartupScript(updUbicaciones, updUbicaciones.GetType(), "hidefrmAgregarUbicacion", "hidefrmAgregarUbicacion();", true);
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                e.Cancel = true;
                lblError.Text = (ex.Message);
                this.EjecutarScript(updUbicaciones, "hidefrmAgregarUbicacion();");
                ScriptManager.RegisterStartupScript(updUbicaciones, updUbicaciones.GetType(), "hidefrmAgregarUbicacion", "hidefrmAgregarUbicacion();", true);
                this.EjecutarScript(updUbicaciones, "showfrmError();");
                ScriptManager.RegisterStartupScript(updUbicaciones, updUbicaciones.GetType(), "showfrmError", "showfrmError();", true);
            }
        }

        protected void BuscarUbicacion_CerrarClick(object sender, EventArgs e)
        {
            CargarDatos(IdSolicitud);
            this.EjecutarScript(updUbicaciones, "finalizarCarga();");
            updUbicaciones.Update();
            ScriptManager.RegisterStartupScript(updUbicaciones, updUbicaciones.GetType(), "hidefrmAgregarUbicacion", "hidefrmAgregarUbicacion();", true);
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
            TransferenciasSolicitudesBL trBL = new TransferenciasSolicitudesBL();
            visUbicaciones.Editable = true;
            var transf = trBL.Single(id_solicitud);
            visUbicaciones.CargarDatos(transf);
        }

        protected void visUbicaciones_EliminarClick(object sender, ucEliminarEventsArgs args)
        {
            btnEliminar_Si.CommandArgument = args.IdUbicacion.ToString();
            ScriptManager.RegisterStartupScript(updUbicaciones, updUbicaciones.GetType(), "updUbicaciones", "showfrmConfirmarEliminar();", true);
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
                int IdTransfUbicacion = int.Parse(btnEliminar_Si.CommandArgument);

                // Eliminar la ubicación.
                TransferenciaUbicacionesBL transfUbicacionesBL = new TransferenciaUbicacionesBL();
                var dto = new TransferenciaUbicacionesDTO()
                {
                    IdTransferenciaUbicacion = IdTransfUbicacion,
                    IdSolicitud = IdSolicitud
                };
                transfUbicacionesBL.Delete(dto);

                // vuelve a cargar los datos.
                TransferenciasSolicitudesBL trBL = new TransferenciasSolicitudesBL();
                visUbicaciones.CargarDatos(trBL.Single(IdSolicitud));
                ScriptManager.RegisterStartupScript(updUbicaciones, updUbicaciones.GetType(), "updUbicaciones2", "hidefrmConfirmarEliminar();", true);
                updUbicaciones.Update();

            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                this.EjecutarScript(updConfirmarEliminar, "hidefrmConfirmarEliminar();showfrmError();");
            }
        }

        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!visUbicaciones.TieneUbicaciones)
                    throw new Exception("Debe ingresar la ubicación.");

                if (hid_return_url.Value.Contains("Editar"))
                    Response.Redirect(string.Format("~/" + RouteConfig.VISOR_TRANSMISIONES + "{0}", IdSolicitud));
                else
                    Response.Redirect(string.Format("~/" + RouteConfig.AGREGAR_DATOSLOCAL_TRANSMISION + "{0}", IdSolicitud));
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                this.EjecutarScript(updBotonesGuardar, "showfrmError();");
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            try
            {
                if (hid_return_url.Value.Contains("Editar"))
                    Response.Redirect(string.Format("~/" + RouteConfig.VISOR_TRANSMISIONES + "{0}", IdSolicitud));
                else
                    Response.Redirect(string.Format("~/" + RouteConfig.AGREGAR_DATOSLOCAL_TRANSMISION + "{0}", IdSolicitud));
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                this.EjecutarScript(updBotonesGuardar, "showfrmError();");
            }
        }
    }
}