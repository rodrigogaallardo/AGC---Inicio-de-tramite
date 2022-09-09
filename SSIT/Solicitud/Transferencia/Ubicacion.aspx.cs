﻿using BusinesLayer.Implementation;
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
            this.BuscarUbicacion.AgregarUbicacionClick += BuscarUbicacion_AgregarUbicacionClick1;
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
                this.EjecutarScript(upd, "hidefrmAgregarUbicacion();");
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                e.Cancel = true;
                lblError.Text = (ex.Message);
                this.EjecutarScript(upd, "hidefrmAgregarUbicacion();showfrmError();");
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
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                e.Cancel = true;
                lblError.Text = (ex.Message);
                this.EjecutarScript(upd, "hidefrmAgregarUbicacion();showfrmError();");
            }
        }

        protected void BuscarUbicacion_CerrarClick(object sender, EventArgs e)
        {
            CargarDatos(IdSolicitud);
            this.EjecutarScript(updUbicaciones, "hidefrmAgregarUbicacion();");
            updUbicaciones.Update();
            ScriptManager.RegisterStartupScript(updUbicaciones, updUbicaciones.GetType(), "hidefrmAgregarUbicacion", "hidefrmAgregarUbicacion();", true);
        }

        protected void btnCargarDatos_Click(object sender, EventArgs e)
        {
            try
            {
                CargarDatos(IdSolicitud);
                this.EjecutarScript(updUbicaciones, "finalizarCarga();");

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
            CargarTablaPlantasHabilitar(id_solicitud);
        }

        protected void visUbicaciones_EliminarClick(object sender, ucEliminarEventsArgs args)
        {
            btnEliminar_Si.CommandArgument = args.IdUbicacion.ToString();
            this.EjecutarScript(updUbicaciones, "showfrmConfirmarEliminar();");
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
                this.EjecutarScript(updUbicaciones, "hidefrmConfirmarEliminar();");
                updUbicaciones.Update();

            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                this.EjecutarScript(updConfirmarEliminar, "hidefrmConfirmarEliminar();showfrmError();");
            }
        }
        

        #region "Plantas a habilitar"

        private void CargarTablaPlantasHabilitar(int IdSolicitud)
        {
            TransferenciasPlantasBL transfPlantasBL = new TransferenciasPlantasBL();
            var lstResult = transfPlantasBL.Get(IdSolicitud).ToList();

            // La siguiente lçogica lo que hace antes de lelnar la grilla con la consulta es
            // revisar si existen ya "piso" o la planta "otro", estos dos tipos de sectores están marcados como 
            // que requieren campo adicional, si requieren campo adicional quieren decir que se puede llenar mças de una vez, 
            // lo que hace la rutina es agregar uno vacío para que pueda ser llenado.
    
        DataTable dt = new DataTable();
            dt.Columns.Add("IdTransferenciaTipoSector", typeof(int));
            dt.Columns.Add("IdTipoSector", typeof(int));
            dt.Columns.Add("Seleccionado", typeof(bool));
            dt.Columns.Add("descripcion", typeof(string));
            dt.Columns.Add("Detalle", typeof(bool));
            dt.Columns.Add("detalleDES", typeof(string));
            dt.Columns.Add("TamanoCampoAdicional", typeof(int));
            dt.Columns.Add("Ocultar", typeof(bool));

            int id_tiposector_ant = 0;
            bool seleccionado_ant = false;
            bool MuestraCampoAdicional_ant = false;
            string descripcion_ant = "";
            string detalle_ant = "";
            int TamanoCampoAdicional_ant = 0;
            int? IdTransfTipoSectorAnt;

            if (lstResult.Count > 0)
            {
                IdTransfTipoSectorAnt = lstResult[0].IdTransferenciaTipoSector;
                id_tiposector_ant = lstResult[0].IdTipoSector;
                MuestraCampoAdicional_ant = lstResult[0].MuestraCampoAdicional;
                descripcion_ant = lstResult[0].Descripcion;
                TamanoCampoAdicional_ant = lstResult[0].TamanoCampoAdicional;
                detalle_ant = lstResult[0].Detalle;

                DataRow datarw = dt.NewRow();
                foreach (var item in lstResult)
                {

                    if (item.IdTipoSector != id_tiposector_ant)
                    {

                        if (MuestraCampoAdicional_ant && detalle_ant.Length > 0)
                        {
                            datarw = dt.NewRow();
                            datarw[0] = 0;
                            datarw[1] = id_tiposector_ant;
                            datarw[2] = false;
                            datarw[3] = descripcion_ant;
                            datarw[4] = MuestraCampoAdicional_ant;
                            datarw[5] = "";
                            datarw[6] = TamanoCampoAdicional_ant;
                            datarw[7] = (item.Descripcion == "Piso" || item.Descripcion == "Otro") && item.Seleccionado == true ? true : false;

                            dt.Rows.Add(datarw);
                        }

                    }

                    datarw = dt.NewRow();
                    if (item.IdTransferenciaTipoSector == null)
                        datarw[0] = 0;
                    else
                        datarw[0] = item.IdTransferenciaTipoSector;

                    datarw[1] = item.IdTipoSector;
                    datarw[2] = item.Seleccionado;
                    datarw[3] = item.Descripcion;
                    datarw[4] = item.MuestraCampoAdicional;
                    datarw[5] = item.Detalle;
                    datarw[6] = item.TamanoCampoAdicional;
                    datarw[7] = (item.Descripcion == "Piso" || item.Descripcion == "Otro") && item.Seleccionado == true ? true : false;

                    dt.Rows.Add(datarw);

                    id_tiposector_ant = item.IdTipoSector;
                    seleccionado_ant = item.Seleccionado;
                    MuestraCampoAdicional_ant = item.MuestraCampoAdicional;
                    descripcion_ant = item.Descripcion;
                    detalle_ant = item.Detalle;
                    TamanoCampoAdicional_ant = item.TamanoCampoAdicional;

                }


                if (MuestraCampoAdicional_ant && detalle_ant.Length > 0)
                {
                    datarw = dt.NewRow();
                    datarw[0] = 0;
                    datarw[1] = id_tiposector_ant;
                    datarw[2] = false;
                    datarw[3] = descripcion_ant;
                    datarw[4] = MuestraCampoAdicional_ant;
                    datarw[5] = "";
                    datarw[6] = TamanoCampoAdicional_ant;
                    datarw[7] = (descripcion_ant == "Piso" || descripcion_ant == "Piso") && seleccionado_ant == true ? true : false;

                    dt.Rows.Add(datarw);
                }

            }

            // --Fin de la Logica 
            grdPlantasHabilitar.DataSource = dt;
            grdPlantasHabilitar.DataBind();
            updPlantas.Update();


        }
        protected void chkSeleccionado_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            int filaSeleccionada = ((GridViewRow)chk.Parent.Parent).RowIndex;
            GridViewRow rowActual = (GridViewRow)chk.Parent.Parent;
            GridView grdSeleccionPlantas = (GridView)chk.Parent.Parent.Parent.Parent;
            Req_Plantas.Style["display"] = "none";

            DataTable dt = new DataTable();
            dt.Columns.Add("IdTransferenciaTipoSector", typeof(int));
            dt.Columns.Add("IdTipoSector", typeof(int));
            dt.Columns.Add("Seleccionado", typeof(bool));
            dt.Columns.Add("descripcion", typeof(string));
            dt.Columns.Add("Detalle", typeof(bool));
            dt.Columns.Add("detalleDES", typeof(string));
            dt.Columns.Add("TamanoCampoAdicional", typeof(int));
            dt.Columns.Add("Ocultar", typeof(bool));

            foreach (GridViewRow row in grdSeleccionPlantas.Rows)
            {
                DataRow datarw;
                datarw = dt.NewRow();

                CheckBox chkSeleccionado = (CheckBox)grdSeleccionPlantas.Rows[row.RowIndex].Cells[0].FindControl("chkSeleccionado");
                HiddenField hid_id_tiposector = (HiddenField)grdSeleccionPlantas.Rows[row.RowIndex].Cells[0].FindControl("hid_id_tiposector");
                HiddenField hid_descripcion = (HiddenField)grdSeleccionPlantas.Rows[row.RowIndex].Cells[0].FindControl("hid_descripcion");
                TextBox txtDetalle = (TextBox)grdSeleccionPlantas.Rows[row.RowIndex].Cells[1].FindControl("txtDetalle");

                int id_tiposector = 0;
                int.TryParse(hid_id_tiposector.Value, out id_tiposector);

                int id_cpadrontiposector;

                if (int.TryParse(grdPlantasHabilitar.DataKeys[row.RowIndex].Values["IdTransferenciaTipoSector"].ToString(), out id_cpadrontiposector))
                    datarw[0] = id_cpadrontiposector;
                else
                    datarw[0] = 0;

                datarw[1] = id_tiposector;
                datarw[2] = chkSeleccionado.Checked;
                datarw[3] = hid_descripcion.Value;
                datarw[4] = txtDetalle.Visible;
                datarw[5] = txtDetalle.Text.Trim();
                datarw[6] = txtDetalle.MaxLength;
                datarw[7] = (hid_descripcion.Value == "Piso" || hid_descripcion.Value == "Otro") && chkSeleccionado.Checked == true ? true : false;

                if (row.RowIndex == filaSeleccionada)   // si es la fila seleccionada trabajamos con la misma, sino la agregamos directamente
                {
                    if (chk.Checked)
                    {
                        // Si la fila seleccionada está tildada, la agregamos y además agregamos una nueva basada en esta.
                        dt.Rows.Add(datarw);

                       if (txtDetalle.Visible || hid_descripcion.Value == "Piso" || hid_descripcion.Value == "Otro")// Si muestra un detalle se agrega otra igual
                        {
                            datarw = dt.NewRow();
                            datarw[0] = 0;
                            datarw[1] = id_tiposector;
                            datarw[2] = false;
                            datarw[3] = hid_descripcion.Value;
                            datarw[4] = txtDetalle.Visible;
                            datarw[5] = "";
                            datarw[6] = txtDetalle.MaxLength;
                            datarw[7] = chkSeleccionado.Checked == true ? true : false;

                            dt.Rows.Add(datarw);
                        }
                    }
                    else
                    {
                        // Si está destildada preguntamos si hay mas de una fila como esta, si la hay no la agregamos
                        //if (ContarFilasxPlanta(id_tiposector) <= 1)
                        dt.Rows.Add(datarw);
                    }
                }
                else
                {
                    dt.Rows.Add(datarw);
                }
            }
            grdSeleccionPlantas.DataSource = dt;
            grdSeleccionPlantas.DataBind();
            updPlantas.Update();

        }
        private int ContarFilasxPlanta(int id_tiposector)
        {
            int ret = 0;
            foreach (GridViewRow row in grdPlantasHabilitar.Rows)
            {
                HiddenField hid_id_tiposector = (HiddenField)row.FindControl("hid_id_tiposector");
                int id_tiposector_fila = 0;
                int.TryParse(hid_id_tiposector.Value, out id_tiposector_fila);

                if (id_tiposector.Equals(id_tiposector_fila))
                    ret += 1;
            }

            return ret;
        }
        protected void grdPlantasHabilitar_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                TextBox txtDetalle = (TextBox)e.Row.FindControl("txtDetalle");
                CheckBox chkSeleccionado = (CheckBox)e.Row.FindControl("chkSeleccionado");

                if (txtDetalle.Visible)
                {
                    if (txtDetalle.MaxLength * 10 > 250)
                        txtDetalle.Width = Unit.Parse("250px");
                    else
                        txtDetalle.Width = Unit.Parse(Convert.ToString(txtDetalle.MaxLength * 20) + "px");
                }
            }
        }

        #endregion

        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!visUbicaciones.TieneUbicaciones)
                    throw new Exception("Debe ingresar la ubicación.");

                List<TransferenciasPlantasDTO> plantas = new List<TransferenciasPlantasDTO>();
                TransferenciasPlantasBL transfPlantasBL = new TransferenciasPlantasBL();

                foreach (GridViewRow row in grdPlantasHabilitar.Rows)
                {
                    CheckBox chkPlanta = (CheckBox)row.FindControl("chkSeleccionado");
                    TextBox txtDetalle = (TextBox)row.FindControl("txtDetalle");
                    HiddenField hid_id_tiposector = (HiddenField)row.FindControl("hid_id_tiposector");
                    int id_tiposector = Convert.ToInt32(hid_id_tiposector.Value);
                    int id_cpadrontiposector = 0;
                    int.TryParse(grdPlantasHabilitar.DataKeys[row.RowIndex].Values["IdTransferenciaTipoSector"].ToString(), out id_cpadrontiposector);

                    TransferenciasPlantasDTO planta = new TransferenciasPlantasDTO();
                    planta.IdTransferenciaTipoSector = id_cpadrontiposector;
                    planta.IdSolicitud = IdSolicitud;
                    planta.DetalleTransferenciaTipoSector = txtDetalle.Text.Trim();
                    planta.IdTipoSector = id_tiposector;
                    planta.Seleccionado = chkPlanta.Checked;

                    plantas.Add(planta);
                }

                transfPlantasBL.ActualizarPlantas(plantas);

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