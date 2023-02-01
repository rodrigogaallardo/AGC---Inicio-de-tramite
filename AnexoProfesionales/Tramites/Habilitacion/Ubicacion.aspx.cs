using AnexoProfesionales.App_Components;
using AnexoProfesionales.Controls;
using AnexoProfesionales.Tramites.Habilitacion.Controls;
using BusinesLayer.Implementation;
using DataTransferObject;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnexoProfesionales
{
    public partial class Ubicacion : BasePage
    {
        EncomiendaBL encBL = new EncomiendaBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                hid_return_url.Value = Request.Url.AbsoluteUri;
                ComprobarSolicitud();
                Titulo.CargarDatos(IdEncomienda, "Ubicación");

            }
            this.BuscarUbicacion.AgregarUbicacionClick += BuscarUbicacion_AgregarUbicacionClick;
            this.BuscarUbicacion.CerrarClick += BuscarUbicacion_CerrarClick;             
        }
   

        protected void BuscarUbicacion_CerrarClick(object sender, EventArgs e)
        {
            CargarDatos(IdEncomienda);
            this.EjecutarScript(updUbicaciones, "finalizarCarga();");
            updUbicaciones.Update();
            ScriptManager.RegisterStartupScript(updUbicaciones, updUbicaciones.GetType(), "hidefrmAgregarUbicacion", "hidefrmAgregarUbicacion();", true);

        }
        protected void visUbicaciones_EditarClick(object sender, ucEditarEventsArgs args)
        {
            this.BuscarUbicacion.idUbicacion = args.IdUbicacion;
            ScriptManager.RegisterStartupScript(updAgregarUbicacion, updAgregarUbicacion.GetType(), "showfrmAgregarUbicacion()", "showfrmAgregarUbicacion();", true);
            this.BuscarUbicacion.editar();

        }
        private int IdEncomienda
        {
            get
            {
                int ret = 0;
                int.TryParse(Page.RouteData.Values["id_encomienda"].ToString(), out ret);
                return ret;
            }
        }

        private int id_tipo_tramite
        {
            get
            {
                int ret = 0;

                int.TryParse(ViewState["_id_tipo_tramite"].ToString(), out ret);
                return ret;
            }
            set
            {
                ViewState["_id_tipo_tramite"] = value.ToString();
            }

        }

        private void ComprobarSolicitud()
        {
            
            if (Page.RouteData.Values["id_encomienda"] != null)
            {

                var id_encomienda = Convert.ToInt32(Page.RouteData.Values["id_encomienda"].ToString());
                EncomiendaDTO enc = encBL.Single(id_encomienda);


                if (enc != null)
                {
                    /*Falta el userID y hacer overload de getuserid con el tipo de tramite*/
                    Guid userid_solicitud = (Guid)Membership.GetUser().ProviderUserKey;
                    this.id_tipo_tramite = enc.IdTipoTramite;

                    if (userid_solicitud != enc.CreateUser)
                        Server.Transfer("~/Errores/error3002.aspx");
                    else {
                        if (!(enc.IdEstado == (int)Constantes.Encomienda_Estados.Incompleta ||
                                enc.IdEstado == (int)Constantes.Encomienda_Estados.Completa))
                        {
                            Server.Transfer("~/Errores/error3003.aspx");
                        }
                        // Saco esta verificacion porque así lo pidieron #312
                        // ahora permite modificar la ubicacion aunque sea una ampliacion
                        // Si proviende de una solicitud anterior se puede modificar la ubicacion par agregar puertas
                        //if (enc.IdTipoTramite != (int)Constantes.TipoDeTramite.Ampliacion &&
                        //    encBL.PoseeHabilitacionConAnexoTecnicoAnterior(enc.IdEncomienda))
                        //{
                        //    Server.Transfer("~/Errores/Error3006.aspx");
                        //    //SoloEditarUbicacion = true;
                        //}
                    }
                }
                else
                    Server.Transfer("~/Errores/error3004.aspx");
            }
            else
                Server.Transfer("~/Errores/error3001.aspx");
        }

        protected void btnCargarDatos_Click(object sender, EventArgs e)
        {
            try
            {
                
                var enc = encBL.Single(IdEncomienda);
                CargarDatos(enc);
                
                if (hid_return_url.Value.Contains("Editar"))
                {
                    btnVolver.Style["display"] = "inline";
                    updBotonesGuardar.Update();
                }
                this.EjecutarScript(updUbicaciones, "finalizarCarga();");

            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                this.EjecutarScript(updUbicaciones, "finalizarCarga();showfrmError();");
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEncomienda"></param>
        private void CargarDatos(int IdEncomienda)
        {
            
            var enc = encBL.Single(IdEncomienda);
            CargarDatos(enc);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="enc"></param>
        private void CargarDatos(EncomiendaDTO enc)
        {

            visUbicaciones.Editable = true;
            visUbicaciones.CargarDatos(enc);

            CargarTablaPlantasHabilitar(enc);

            if (this.id_tipo_tramite == (int)Constantes.TipoTramite.AMPLIACION ||
              this.id_tipo_tramite == (int)Constantes.TipoTramite.REDISTRIBUCION_USO)
            {
                BuscarUbicacion.PermitirPuertasOficiales = true;
            }


            //if (SoloEditarUbicacion)
            //{
            //    btnAgregarUbicacion.Visible = false;
            //    GridView gridubicacion_db = (GridView)visUbicaciones.FindControl("gridubicacion_db");

            //    foreach(GridViewRow row in gridubicacion_db.Rows)
            //    {
            //        LinkButton btnEliminar = (LinkButton)row.FindControl("btnEliminar");
            //        btnEliminar.Visible = false;
            //    }

            //    // Inhabilitar el resto de los controles (Plantas)
            //    chkSI.Enabled = false;
            //    chkNO.Enabled = false;
            //    grdPlantasHabilitar.Enabled = false;


            //}

        }

        protected void visUbicaciones_EliminarClick(object sender, ucEliminarEventsArgs args)
        {

            btnEliminar_Si.CommandArgument = args.IdUbicacion.ToString();
            this.EjecutarScript(updUbicaciones, "showfrmConfirmarEliminar();");            
        }

        protected void btnEliminar_Si_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnEliminar_Si = (Button)sender;
                int IdEncomiendaUbicacion = int.Parse(btnEliminar_Si.CommandArgument);

                // Eliminar la ubicación.
                EncomiendaUbicacionesBL encomiendaUbicacionBL = new EncomiendaUbicacionesBL();
                var dto = new EncomiendaUbicacionesDTO() { 
                    IdEncomiendaUbicacion = IdEncomiendaUbicacion,
                    IdEncomienda = IdEncomienda
                };                 
                encomiendaUbicacionBL.Delete(dto);

                // vuelve a cargar los datos.
                visUbicaciones.CargarDatos(this.IdEncomienda);
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
        protected void btnVolver_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(string.Format("~/" + RouteConfig.VISOR_ENCOMIENDA + "{0}", IdEncomienda));
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                this.EjecutarScript(updBotonesGuardar, "showfrmError();");

            }
        }
        protected void BuscarUbicacion_AgregarUbicacionClick(object sender, ucAgregarUbicacionEventsArgs e)
        {
            UpdatePanel upd = e.upd;
            //Guid userid = Functions.GetUserid();           
            try
            {
                UbicacionesZonasMixturasBL uMixturas = new UbicacionesZonasMixturasBL();
                UbicacionesCatalogoDistritosBL uDistritos = new UbicacionesCatalogoDistritosBL();
                //Alta de la ubicación   
                EncomiendaUbicacionesBL encUbicBL = new EncomiendaUbicacionesBL();
                EncomiendaUbicacionesDTO encomienda = new EncomiendaUbicacionesDTO();                
                encomienda.IdEncomienda = IdEncomienda;
                encomienda.IdUbicacion = e.id_ubicacion;
                encomienda.IdSubtipoUbicacion = e.id_subtipoubicacion;
                encomienda.LocalSubtipoUbicacion = e.local_subtipoubicacion;
                encomienda.DeptoLocalEncomiendaUbicacion = e.vDeptoLocalOtros.Trim();
                encomienda.Depto = e.vDepto.Trim();
                encomienda.Local = e.vLocal.Trim();
                encomienda.Torre = e.vTorre.Trim();
                encomienda.AnchoCalle = e.vAnchoCalle;
                encomienda.InmuebleCatalogado = e.vInmuebleCatalogado_SI;
                encomienda.CreateDate = DateTime.Now;
                encomienda.CreateUser = (Guid)Membership.GetUser().ProviderUserKey;

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
                    puertas.Add(new UbicacionesPuertasDTO() { 
                        CodigoCalle = puerta.codigo_calle,
                        NroPuertaUbic = puerta.NroPuerta,
                        IdUbicacion = e.id_ubicacion
                    });                 
                }               
               
                //Alta de Zonas Mixturas
                List<Encomienda_Ubicaciones_MixturasDTO> encomiendaMixturas = new List<Encomienda_Ubicaciones_MixturasDTO>();
                List<UbicacionesZonasMixturasDTO> zonasMixturas = uMixturas.GetZonasUbicacion(e.id_ubicacion).ToList();
                foreach (var ZonasMixturasDTO in zonasMixturas)
                {
                    encomiendaMixturas.Add(new Encomienda_Ubicaciones_MixturasDTO()
                    {
                        IdZonaMixtura = ZonasMixturasDTO.IdZona
                    });
                }

                //Alta de Distritos
                List<Encomienda_Ubicaciones_DistritosDTO> encomiendaDistritos = new List<Encomienda_Ubicaciones_DistritosDTO>();
                List<UbicacionesCatalogoDistritosDTO> distritos = uDistritos.GetDistritosUbicacion(e.id_ubicacion).ToList();

                int IdUbicacion = encomienda.IdUbicacion ?? 0;

                foreach (var CatalogoDistritosDTO in distritos)
                {
                    encomiendaDistritos.Add(new Encomienda_Ubicaciones_DistritosDTO()
                    {
                        IdDistrito = CatalogoDistritosDTO.IdDistrito,
                        IdZona = uDistritos.GetIdZonaByUbicacion(IdUbicacion),
                        IdSubZona = uDistritos.GetIdSubZonaByUbicacion(IdUbicacion),
                    });
                }

                encomienda.EncomiendaUbicacionesDistritosDTO = encomiendaDistritos;
                encomienda.EncomiendaUbicacionesMixturasDTO = encomiendaMixturas;

                //visUbicaciones.CargarDatos(this.id_solicitud);
                encomienda.PropiedadesHorizontales = propiedadesHorizontales;
                encomienda.Puertas = puertas;

                encUbicBL.Insert(encomienda);

                CargarDatos(IdEncomienda);
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
                this.EjecutarScript(updUbicaciones, "hidefrmAgregarUbicacion();showfrmError();");
                ScriptManager.RegisterStartupScript(updUbicaciones, updUbicaciones.GetType(), "hidefrmAgregarUbicacion", "hidefrmAgregarUbicacion();", true);
          
            }
        }

        #region "Plantas a habilitar"

        private void CargarTablaPlantasHabilitar(EncomiendaDTO enc)
        {     
            EncomiendaPlantasBL encomiendaPlantasBL = new EncomiendaPlantasBL();
            var lstResult = encomiendaPlantasBL.Get(enc.IdEncomienda).ToList(); 
            
            // La siguiente lçogica lo que hace antes de lelnar la grilla con la consulta es
            // revisar si existen ya "piso" o la planta "otro", estos dos tipos de sectores están marcados como 
            // que requieren campo adicional, si requieren campo adicional quieren decir que se puede llenar mças de una vez, 
            // lo que hace la rutina es agregar uno vacío para que pueda ser llenado.

            DataTable dt = new DataTable();
            dt.Columns.Add("id_tiposector", typeof(int));
            dt.Columns.Add("Seleccionado", typeof(bool));
            dt.Columns.Add("descripcion", typeof(string));
            dt.Columns.Add("MuestraCampoAdicional", typeof(bool));
            dt.Columns.Add("detalle", typeof(string));
            dt.Columns.Add("TamanoCampoAdicional", typeof(int));
            dt.Columns.Add("Ocultar", typeof(bool));

            if (enc.Contiene_galeria_paseo.HasValue)
            {
                chkSI.Checked = enc.Contiene_galeria_paseo.Value;
                chkNO.Checked = !chkSI.Checked;
            }
            else
                chkNO.Checked = true;

            if (enc.Consecutiva_Supera_10.HasValue)
                chkConsecutivas.Checked = enc.Consecutiva_Supera_10.Value;

            int id_tiposector_ant = 0;
            bool MuestraCampoAdicional_ant = false;
            string descripcion_ant = "";
            string detalle_ant = "";
            int TamanoCampoAdicional_ant = 0;

            if (lstResult.Count > 0)
            {
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
                            datarw[0] = id_tiposector_ant;
                            datarw[1] = false;
                            datarw[2] = descripcion_ant;
                            datarw[3] = MuestraCampoAdicional_ant;
                            datarw[4] = "";
                            datarw[5] = TamanoCampoAdicional_ant;
                            datarw[6] = false;

                            dt.Rows.Add(datarw);
                        }

                    }

                    datarw = dt.NewRow();

                    datarw[0] = item.IdTipoSector;
                    datarw[1] = item.Seleccionado;
                    datarw[2] = item.Descripcion;
                    datarw[3] = item.MuestraCampoAdicional;
                    datarw[4] = item.Detalle;
                    datarw[5] = item.TamanoCampoAdicional;
                    datarw[6] = false;

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
                    datarw[0] = id_tiposector_ant;
                    datarw[1] = false;
                    datarw[2] = descripcion_ant;
                    datarw[3] = MuestraCampoAdicional_ant;
                    datarw[4] = "";
                    datarw[5] = TamanoCampoAdicional_ant;
                    datarw[6] = false;

                    dt.Rows.Add(datarw);
                }

            }

            // --Fin de la Logica 
            grdPlantasHabilitar.DataSource = dt;
            grdPlantasHabilitar.DataBind();
            updPlantas.Update();
            updGaleria.Update();

        }
        protected void chkSeleccionado_CheckedChanged(object sender, EventArgs e)
        {

            CheckBox chk = (CheckBox)sender;
            int filaSeleccionada = ((GridViewRow)chk.Parent.Parent).RowIndex;
            GridViewRow rowActual = (GridViewRow)chk.Parent.Parent;
            GridView grdSeleccionPlantas = (GridView)chk.Parent.Parent.Parent.Parent;
            Req_Plantas.Style["display"] = "none";


            DataTable dt = new DataTable();
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


                datarw[0] = id_tiposector;
                datarw[1] = chkSeleccionado.Checked;
                datarw[2] = hid_descripcion.Value;
                datarw[3] = txtDetalle.Visible;
                datarw[4] = txtDetalle.Text.Trim();
                datarw[5] = txtDetalle.MaxLength;
                datarw[6] = false;


                if (row.RowIndex == filaSeleccionada)   // si es la fila seleccionada trabajamos con la misma, sino la agregamos directamente
                {

                    if (chk.Checked)
                    {
                        // Si la fila seleccionada está tildada, la agregamos y además agregamos una nueva basada en esta.
                        dt.Rows.Add(datarw);

                        if (txtDetalle.Visible)     // Si muestra un detalle se agrega otra igual
                        {
                            datarw = dt.NewRow();
                            datarw[0] = id_tiposector;
                            datarw[1] = false;
                            datarw[2] = hid_descripcion.Value;
                            datarw[3] = txtDetalle.Visible;
                            datarw[4] = "";
                            datarw[5] = txtDetalle.MaxLength;
                            datarw[6] = false;

                            dt.Rows.Add(datarw);
                        }
                    }
                    else
                    {
                        // Si está destildada preguntamos si hay mas de una fila como esta, si la hay no la agregamos
                        if (ContarFilasxPlanta(id_tiposector) <= 1)
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
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "comprobarnumericosScript", "comprobarnumericos();", true);
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
                HiddenField hid_id_tiposector = (HiddenField)e.Row.FindControl("hid_id_tiposector");
                RangeValidator rvPiso = (RangeValidator)e.Row.FindControl("rvPiso");

                if (txtDetalle.Visible)
                {
                    if (txtDetalle.MaxLength * 10 > 250)
                        txtDetalle.Width = Unit.Parse("250px");
                    else
                        txtDetalle.Width = Unit.Parse(Convert.ToString(txtDetalle.MaxLength * 20) + "px");
                }

                if (Convert.ToInt32(hid_id_tiposector.Value) == (int)Constantes.TipoSector.Piso)
                    txtDetalle.CssClass = txtDetalle.CssClass + " classAutonumeric";

            }
        }

        #endregion

        protected void btnContinuar_Click(object sender, EventArgs e)
        {
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

                var encDTO = encBL.Single(IdEncomienda);
                encDTO.Servidumbre_paso = Servidumbre_paso;
                encDTO.Consecutiva_Supera_10 = chkConsecutivas.Checked;
                encDTO.Contiene_galeria_paseo = chkSI.Checked;

                encBL.Update(encDTO);

                var solDTO = solBL.Single(encDTO.IdSolicitud); //EncomiendaSSITSolicitudesDTO.Select(x => x.id_solicitud).FirstOrDefault()
                if (solDTO != null)
                {
                    solDTO.Servidumbre_paso = Servidumbre_paso;
                    solBL.Update(solDTO);
                }
                List<string> lstPisos = new List<string>();

                // Se arma una lista con las plantas seleccionadas en la pantalla
                List<EncomiendaPlantasDTO> lstPlantasPantalla = new List<EncomiendaPlantasDTO>();
                foreach (GridViewRow row in grdPlantasHabilitar.Rows)
                {
                    CheckBox chkPlanta = (CheckBox)row.FindControl("chkSeleccionado");
                    TextBox txtDetalle = (TextBox)row.FindControl("txtDetalle");
                    HiddenField hid_id_tiposector = (HiddenField)row.FindControl("hid_id_tiposector");

                    if (chkPlanta.Checked)
                    {
                        int id_tiposector = Convert.ToInt32(hid_id_tiposector.Value);
                        EncomiendaPlantasDTO planta = new EncomiendaPlantasDTO();
                        planta.id_encomienda = IdEncomienda;
                        planta.Descripcion = txtDetalle.Text.Trim();
                        planta.IdTipoSector = id_tiposector;

                        if (id_tiposector == (int)Constantes.TipoSector.Piso && lstPisos.Contains(txtDetalle.Text.Trim()))
                            throw new Exception("No se puede agregar números de pisos repetidos.");
                        else if(id_tiposector == (int)Constantes.TipoSector.Piso)
                            lstPisos.Add(txtDetalle.Text.Trim());

             
                        lstPlantasPantalla.Add(planta);
                    }
                   
                }

                // Se arma una lista con las plantas de la base de datos.
                EncomiendaPlantasBL encomiendaPlantasBL = new EncomiendaPlantasBL();
                List<EncomiendaPlantasDTO> lstPlantasBD =  encomiendaPlantasBL.GetByFKIdEncomienda(this.IdEncomienda).ToList();


                IEnumerable <EncomiendaPlantasDTO> qPlantasEliminar = lstPlantasBD.Where(plan_db => !lstPlantasPantalla.Any(plan_pant => 
                                                                                            plan_pant.Descripcion == plan_db.Descripcion
                                                                                            && plan_pant.IdTipoSector == plan_db.IdTipoSector));

                IEnumerable<EncomiendaPlantasDTO> qPlantasAgregar = lstPlantasPantalla.Where(plan_pant => !lstPlantasBD.Any(plan_db =>
                                                                                            plan_db.Descripcion == plan_pant.Descripcion
                                                                                            && plan_db.IdTipoSector == plan_pant.IdTipoSector));
                foreach (var planta in qPlantasAgregar)
                {
                    encomiendaPlantasBL.Insert(planta);
                }

                foreach (var planta in qPlantasEliminar)
                {
                    try
                    {
                        encomiendaPlantasBL.Delete(planta);
                    }
                    catch
                    {
                        throw new Exception("Hay locales ingresados sobre las plantas eliminadas, por favor, primero elimine/modifique el local en cuestion dentro de la conformación del local.");
                    }
                }
                    
                if (hid_return_url.Value.Contains("Editar"))
                    Response.Redirect(string.Format("~/" + RouteConfig.VISOR_ENCOMIENDA + "{0}", IdEncomienda));
                else
                    Response.Redirect(string.Format("~/" + RouteConfig.AGREGAR_ENCOMIENDA_DATOSLOCAL + "{0}", IdEncomienda));
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = Funciones.GetErrorMessage(ex);
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