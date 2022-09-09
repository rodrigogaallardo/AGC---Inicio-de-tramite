using BusinesLayer.Implementation;
using DataTransferObject;
using SSIT.App_Components;
using SSIT.Solicitud.Consulta_Padron.Controls;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSIT.Solicitud.Consulta_Padron
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
                ComprobarSolicitud();
                hid_return_url.Value = Request.Url.AbsoluteUri;
            }
            this.BuscarUbicacion.AgregarUbicacionClick += BuscarUbicacion_AgregarUbicacionClick;
            this.BuscarUbicacion.CerrarClick += BuscarUbicacion_CerrarClick;

        }

        protected void BuscarUbicacion_CerrarClick(object sender, EventArgs e)
        {
            CargarDatos(IdSolicitud);
            this.EjecutarScript(updUbicaciones, "hidefrmAgregarUbicacion();");
            updUbicaciones.Update();
            ScriptManager.RegisterStartupScript(updUbicaciones, updUbicaciones.GetType(), "hidefrmAgregarUbicacion", "hidefrmAgregarUbicacion();", true);
        }

        private void ComprobarSolicitud()
        {


            //Guid userid_solicitud = GetUserid(Constants.TiposDeTramite.CAA_HAB, id_solicitud);
            //Guid userid = Functions.GetUserid();
            //
            //if (userid_solicitud != userid)
            //Server.Transfer("~/Errores/Error3002.aspx");
            //EncomiendaBL 
            //var sol = db.CAA_Solicitudes.FirstOrDefault(x => x.id_caa == this.id_solicitud);
            //}
            //else
            //{
            //    Server.Transfer("~/Errores/Error3001.aspx");
            //}

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
            visUbicaciones.Editable = true;
            visUbicaciones.CargarDatos(id_solicitud);
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
                int IdConsultaPadronUbicacion = int.Parse(btnEliminar_Si.CommandArgument);

                // Eliminar la ubicación.
                ConsultaPadronUbicacionesBL consultaPadronUbicacionesBL = new ConsultaPadronUbicacionesBL();
                var dto = new ConsultaPadronUbicacionesDTO()
                {
                    IdConsultaPadronUbicacion = IdConsultaPadronUbicacion,
                    IdConsultaPadron = IdSolicitud
                };
                consultaPadronUbicacionesBL.Delete(dto);

                // vuelve a cargar los datos.
                visUbicaciones.CargarDatos(this.IdSolicitud);
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
        protected void BuscarUbicacion_AgregarUbicacionClick(object sender, ucAgregarUbicacionEventsArgs e)
        {
            UpdatePanel upd = e.upd;
            try
            {
                //Alta de la ubicación   
                ConsultaPadronUbicacionesBL consultaPadronUbicacionesBL = new ConsultaPadronUbicacionesBL();
                ConsultaPadronUbicacionesDTO consultaPadron = new ConsultaPadronUbicacionesDTO();
                consultaPadron.IdConsultaPadron = IdSolicitud;
                consultaPadron.IdUbicacion = e.id_ubicacion;
                consultaPadron.IdSubTipoUbicacion = e.id_subtipoubicacion;
                consultaPadron.LocalSubTipoUbicacion = e.local_subtipoubicacion;
                consultaPadron.DeptoLocalConsultaPadronUbicacion = e.vDeptoLocalOtros.Trim();
                consultaPadron.Depto = e.vDepto.Trim();
                consultaPadron.Local = e.vLocal.Trim();
                consultaPadron.Torre = e.vTorre.Trim();
                consultaPadron.CreateDate = DateTime.Now;
                consultaPadron.CreateUser = (Guid)Membership.GetUser().ProviderUserKey;

                List<ConsultaPadronUbicacionPropiedadHorizontalDTO> propiedadesHorizontales = new List<ConsultaPadronUbicacionPropiedadHorizontalDTO>();
                //Alta de las propiedades horizontales
          
                foreach (int id_propiedad_horizontal in e.ids_propiedades_horizontales)
                {
                    propiedadesHorizontales.Add(new ConsultaPadronUbicacionPropiedadHorizontalDTO()
                    {
                        IdPropiedadHorizontal = id_propiedad_horizontal,                        
                    });
                }

                List<ConsultaPadronUbicacionesPuertasDTO> puertas = new List<ConsultaPadronUbicacionesPuertasDTO>();

                //Alta de puertas
                foreach (var puerta in e.Puertas)
                {
                    puertas.Add(new ConsultaPadronUbicacionesPuertasDTO()
                    {
                        CodigoCalle = puerta.codigo_calle,
                        NumeroPuerta = puerta.NroPuerta
                    });
                }
             
                consultaPadron.PropiedadesHorizontales = propiedadesHorizontales;
                consultaPadron.Puertas = puertas;

                consultaPadronUbicacionesBL.Insert(consultaPadron);
                visUbicaciones.CargarDatos(this.IdSolicitud);
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

        #region "Plantas a habilitar"

        private void CargarTablaPlantasHabilitar(int IdSolicitud)
        {
            ConsultaPadronPlantasBL consultaPadronPlantasBL = new ConsultaPadronPlantasBL();
            var lstResult = consultaPadronPlantasBL.Get(IdSolicitud).ToList();

            // La siguiente lçogica lo que hace antes de lelnar la grilla con la consulta es
            // revisar si existen ya "piso" o la planta "otro", estos dos tipos de sectores están marcados como 
            // que requieren campo adicional, si requieren campo adicional quieren decir que se puede llenar mças de una vez, 
            // lo que hace la rutina es agregar uno vacío para que pueda ser llenado.

            DataTable dt = new DataTable();
            dt.Columns.Add("IdConsultaPadronTipoSector", typeof(int));
            dt.Columns.Add("id_tiposector", typeof(int));
            dt.Columns.Add("Seleccionado", typeof(bool));
            dt.Columns.Add("descripcion", typeof(string));
            dt.Columns.Add("MuestraCampoAdicional", typeof(bool));
            dt.Columns.Add("detalle", typeof(string));
            dt.Columns.Add("TamanoCampoAdicional", typeof(int));
            dt.Columns.Add("Ocultar", typeof(bool));

            int id_tiposector_ant = 0;
            bool MuestraCampoAdicional_ant = false;
            string descripcion_ant = "";
            string detalle_ant = "";
            int TamanoCampoAdicional_ant = 0;
            int? IdConsultaPadronTipoSectorAnt;

            if (lstResult.Count > 0)
            {
                IdConsultaPadronTipoSectorAnt = lstResult[0].IdConsultaPadronTipoSector;
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
                            datarw[7] = false;

                            dt.Rows.Add(datarw);
                        }

                    }

                    datarw = dt.NewRow();
                    if (item.IdConsultaPadronTipoSector == null)
                        datarw[0] = 0;
                    else
                        datarw[0] = item.IdConsultaPadronTipoSector;

                    datarw[1] = item.IdTipoSector;
                    datarw[2] = item.Seleccionado;
                    datarw[3] = item.Descripcion;
                    datarw[4] = item.MuestraCampoAdicional;
                    datarw[5] = item.Detalle;
                    datarw[6] = item.TamanoCampoAdicional;
                    datarw[7] = false;

                    dt.Rows.Add(datarw);

                    id_tiposector_ant = item.IdTipoSector;
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
                    datarw[7] = false;

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
            dt.Columns.Add("IdConsultaPadronTipoSector", typeof(int));
            dt.Columns.Add("id_tiposector", typeof(int));
            dt.Columns.Add("Seleccionado", typeof(bool));
            dt.Columns.Add("descripcion", typeof(string));
            dt.Columns.Add("MuestraCampoAdicional", typeof(bool));
            dt.Columns.Add("detalle", typeof(string));
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

                if (int.TryParse(grdPlantasHabilitar.DataKeys[row.RowIndex].Values["IdConsultaPadronTipoSector"].ToString(), out id_cpadrontiposector))
                    datarw[0] = id_cpadrontiposector;
                else
                    datarw[0] = 0;

                datarw[1] = id_tiposector;
                datarw[2] = chkSeleccionado.Checked;
                datarw[3] = hid_descripcion.Value;
                datarw[4] = txtDetalle.Visible;
                datarw[5] = txtDetalle.Text.Trim();
                datarw[6] = txtDetalle.MaxLength;
                datarw[7] = false;

                if (row.RowIndex == filaSeleccionada)   // si es la fila seleccionada trabajamos con la misma, sino la agregamos directamente
                {
                    if (chk.Checked)
                    {
                        // Si la fila seleccionada está tildada, la agregamos y además agregamos una nueva basada en esta.
                        dt.Rows.Add(datarw);

                        if (txtDetalle.Visible)// Si muestra un detalle se agrega otra igual
                        {
                            datarw = dt.NewRow();
                            datarw[0] = 0;
                            datarw[1] = id_tiposector;
                            datarw[2] = false;
                            datarw[3] = hid_descripcion.Value;
                            datarw[4] = txtDetalle.Visible;
                            datarw[5] = "";
                            datarw[6] = txtDetalle.MaxLength;
                            datarw[7] = false;

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

                List<ConsultaPadronPlantasDTO> plantas = new List<ConsultaPadronPlantasDTO>();
                ConsultaPadronPlantasBL consultaPadronPlantasBL = new ConsultaPadronPlantasBL();

                foreach (GridViewRow row in grdPlantasHabilitar.Rows)
                {
                    CheckBox chkPlanta = (CheckBox)row.FindControl("chkSeleccionado");
                    TextBox txtDetalle = (TextBox)row.FindControl("txtDetalle");
                    HiddenField hid_id_tiposector = (HiddenField)row.FindControl("hid_id_tiposector");
                    int id_tiposector = Convert.ToInt32(hid_id_tiposector.Value);
                    int id_cpadrontiposector = 0;
                    int.TryParse(grdPlantasHabilitar.DataKeys[row.RowIndex].Values["IdConsultaPadronTipoSector"].ToString(), out id_cpadrontiposector);

                    ConsultaPadronPlantasDTO planta = new ConsultaPadronPlantasDTO();
                    planta.IdConsultaPadronTipoSector = id_cpadrontiposector;
                    planta.IdConsultaPadron = IdSolicitud;
                    planta.DetalleConsultaPadronTipoSector = txtDetalle.Text.Trim();
                    planta.IdTipoSector = id_tiposector;
                    planta.Seleccionado = chkPlanta.Checked;

                    plantas.Add(planta);
                }

                consultaPadronPlantasBL.ActualizarPlantas(plantas);

                if (hid_return_url.Value.Contains("Editar"))
                    Response.Redirect(string.Format("~/" + RouteConfig.VISOR_CPADRON + "{0}", IdSolicitud));
                else
                    Response.Redirect(string.Format("~/" + RouteConfig.AGREGAR_DATOSLOCAL_CPADRON + "{0}", IdSolicitud));
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