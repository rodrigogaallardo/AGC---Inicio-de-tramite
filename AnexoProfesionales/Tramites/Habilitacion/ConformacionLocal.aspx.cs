using AnexoProfesionales.App_Components;
using BusinesLayer.Implementation;
using DataTransferObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnexoProfesionales.Controls;
using StaticClass;
using System.Web.Security;
using static StaticClass.Constantes;


namespace AnexoProfesionales
{
    public partial class ConformacionLocal : BasePage
    {



        private int id_encomienda
        {
            get
            {
                int ret = 0;
                int.TryParse(Page.RouteData.Values["id_encomienda"].ToString(), out ret);
                return ret;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hid_return_url.Value = Request.Url.AbsoluteUri;
                ComprobarEncomienda();
                Titulo.CargarDatos(id_encomienda, "Conformación de Local");
            }
            this.ucConformacionLocalDatos.AgregarConformacionLocalClick += ucConformacionLocalDatos_AgregarClick;
            this.ucConformacionLocalDatos.CerrarClick += ucConformacionLocalDatos_CerrarClick;
        }

        private void ucConformacionLocalDatos_CerrarClick(object sender, ConformacionLocalEventsArgs e)
        {
            updConformacionIngresada.Update();
            this.EjecutarScript(e.upd, "hidefrmAgregarConformacionLocal();");
        }

        private void ucConformacionLocalDatos_AgregarClick(object sender, ConformacionLocalEventsArgs e)
        {
            //lblmpeInfo.Text = "";
            try
            {
                Guid userid = (Guid)Membership.GetUser().ProviderUserKey;
                EncomiendaConformacionLocalBL blECL = new EncomiendaConformacionLocalBL();
                EncomiendaConformacionLocalDTO dto = new EncomiendaConformacionLocalDTO();
                bool alta = false;
                if (e.id_encomiendaconflocal == 0)
                {
                    alta = true;
                    dto.CreateDate = DateTime.Now;
                    dto.CreateUser = userid;
                }
                else
                {
                    dto = blECL.Single(e.id_encomiendaconflocal);
                    dto.LastUpdateDate = DateTime.Now;
                    dto.LastUpdateUser = userid;
                }

                dto.id_encomiendaconflocal = e.id_encomiendaconflocal;
                dto.id_encomienda = id_encomienda;
                dto.id_destino = e.id_destino;
                dto.largo_conflocal = e.largo_conflocal;
                dto.ancho_conflocal = e.ancho_conflocal;
                dto.alto_conflocal = e.alto_conflocal;
                dto.superficie_conflocal = e.superficie_conflocal;
                dto.Paredes_conflocal = e.Paredes_conflocal;
                dto.Techos_conflocal = e.Techos_conflocal;
                dto.Pisos_conflocal = e.Pisos_conflocal;
                dto.Frisos_conflocal = e.Frisos_conflocal;
                dto.Observaciones_conflocal = e.Observaciones_conflocal;
                dto.id_encomiendatiposector = e.id_encomiendatiposector;
                dto.id_ventilacion = e.id_ventilacion;
                dto.id_iluminacion = e.id_iluminacion;
                dto.id_tiposuperficie = e.id_tiposuperficie;
                dto.Detalle_conflocal = e.Detalle_conflocal;

                if (alta)
                    blECL.Insert(dto);
                else
                    blECL.Update(dto);

                CargarDatosTramite();
                this.EjecutarScript(e.upd, "hidefrmAgregarConformacionLocal();");
                this.EjecutarScript(e.upd, "validarSuperficie();");
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                e.Cancel = true;
                lblError.Text = (ex.Message);
                this.EjecutarScript(e.upd, "hidefrmAgregarConformacionLocal();showfrmError();");
                this.EjecutarScript(e.upd, "validarSuperficie();");
            }
        }

        private void ComprobarEncomienda()
        {
            if (Page.RouteData.Values["id_encomienda"] != null)
            {
                EncomiendaBL encomiendaBL = new EncomiendaBL();
                var enc = encomiendaBL.Single(id_encomienda);
                if (enc != null)
                {
                    /*Falta el userID y hacer overload de getuserid con el tipo de tramite*/
                    Guid userid_solicitud = (Guid)Membership.GetUser().ProviderUserKey;

                    if (userid_solicitud != enc.CreateUser)
                        Server.Transfer("~/Errores/Error3002.aspx");
                    else {
                        if (!(enc.IdEstado == (int)Constantes.Encomienda_Estados.Incompleta ||
                                enc.IdEstado == (int)Constantes.Encomienda_Estados.Completa))
                        {
                            Server.Transfer("~/Errores/Error3003.aspx");
                        }
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
                CargarCombos();
                CargarDatosTramite();
                ScriptManager.RegisterStartupScript(updCargarDatos, updCargarDatos.GetType(), "finalizarCarga", "finalizarCarga();", true);

            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                ScriptManager.RegisterStartupScript(updCargarDatos, updCargarDatos.GetType(), "finalizarCarga", "finalizarCarga();showfrmError();", true);
            }
        }

        #region carga inicial
        private void CargarDatosTramite()
        {
            EncomiendaConformacionLocalBL bl = new EncomiendaConformacionLocalBL();
            var lst = bl.GetByFKIdEncomienda(id_encomienda);
            grdConformacionLocal.DataSource = lst;
            grdConformacionLocal.DataBind();
            decimal suma = 0;
            foreach (var r in lst)
            {
                suma += r.superficie_conflocal.Value;
            }
            EncomiendaDatosLocalBL blEncDl = new EncomiendaDatosLocalBL();
            EncomiendaDatosLocalDTO enDl = blEncDl.GetByFKIdEncomienda(id_encomienda);
            decimal? aux = enDl.superficie_cubierta_dl + enDl.superficie_descubierta_dl;

            if (enDl.ampliacion_superficie.HasValue && enDl.ampliacion_superficie.Value)
            {
                aux = enDl.superficie_cubierta_amp + enDl.superficie_descubierta_amp;
            }

            txtSupTotalLocal.Text = aux.ToString();
            txtSupTotal.Text = suma.ToString("N2");
            if (txtSupTotalLocal.Text != txtSupTotal.Text)
            {
                reqSupIguales.Style["Display"] = "inline";
            }
            else
            {
                reqSupIguales.Style["Display"] = "none";
            }
            updConformacionLocal.Update();
            updConformacionIngresada.Update();
        }

        private void CargarCombos()
        {
            ucConformacionLocalDatos.CargarDatos(id_encomienda);
        }
        #endregion

        protected void grdConformacionLocal_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                EncomiendaConformacionLocalBL blECL = new EncomiendaConformacionLocalBL();
                int id_encomiendaconflocal = int.Parse(e.CommandArgument.ToString());
                EncomiendaConformacionLocalDTO dto = blECL.Single(id_encomiendaconflocal);
                switch (e.CommandName)
                {
                    case "EditarDetalle":

                        ConformacionLocalEventsArgs args = new ConformacionLocalEventsArgs();

                        args.id_encomiendaconflocal = dto.id_encomiendaconflocal;
                        args.id_destino = dto.id_destino;
                        args.largo_conflocal = dto.largo_conflocal.Value;
                        args.ancho_conflocal = dto.ancho_conflocal.Value;
                        args.alto_conflocal = dto.alto_conflocal.Value;
                        args.superficie_conflocal = dto.superficie_conflocal.Value;
                        args.Paredes_conflocal = dto.Paredes_conflocal;
                        args.Techos_conflocal = dto.Techos_conflocal;
                        args.Pisos_conflocal = dto.Pisos_conflocal;
                        args.Frisos_conflocal = dto.Frisos_conflocal;
                        args.Observaciones_conflocal = dto.Observaciones_conflocal;
                        args.id_encomiendatiposector = dto.id_encomiendatiposector;
                        args.id_ventilacion = dto.id_ventilacion;
                        args.id_iluminacion = dto.id_iluminacion;
                        args.id_tiposuperficie = dto.id_tiposuperficie;
                        args.Detalle_conflocal = dto.Detalle_conflocal;

                        ucConformacionLocalDatos.editar(args);
                        this.EjecutarScript(updConformacionIngresada, "showfrmAgregarConformacionLocal();");
                        this.EjecutarScript(updConformacionIngresada, "validarSuperficie();");
                        break;

                    case "EliminarDetalle":
                        
                        blECL.Delete(dto);
                        CargarDatosTramite();
                        this.EjecutarScript(updConformacionIngresada, "validarSuperficie();");
                        break;
                }
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                //lblmpeInfo.Text = ex.Message;
                this.EjecutarScript(updConformacionIngresada, "mostrarPopup('pnlInformacion');");
            }
        }

        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            EncomiendaBL encomiendaBL = new EncomiendaBL();
            var enc = encomiendaBL.Single(id_encomienda);
            var encDatosLocal = enc.EncomiendaDatosLocalDTO.FirstOrDefault();
            var encConfLocalDTO = enc.EncomiendaConformacionLocalDTO;
            var encRubrosCN = enc.EncomiendaRubrosCNDTO;
            decimal SuperficieTotal = 0;
            if (encDatosLocal.ampliacion_superficie.HasValue && encDatosLocal.ampliacion_superficie.Value)
                SuperficieTotal = encDatosLocal.superficie_cubierta_amp.Value + encDatosLocal.superficie_descubierta_amp.Value;
            else
                SuperficieTotal = encDatosLocal.superficie_cubierta_dl.Value + encDatosLocal.superficie_descubierta_dl.Value;

            if (enc.IdSubTipoExpediente == (int)SubtipoDeExpediente.SinPlanos)
            {
                if (!encConfLocalDTO.Any())
                {
                    lblError.Text = "Debe cargar los datos de conformación del local.";
                }

                if (((SuperficieTotal <= 60 && encRubrosCN.Where(x => x.RubrosDTO.SinBanioPCD == false).Any()) || SuperficieTotal > 60)
                    && encDatosLocal.cumple_ley_962 == true
                    && encDatosLocal.sanitarios_ubicacion_dl == 1
                    && !encConfLocalDTO.Where(x => x.id_destino == (int)TipoDestino.BañoPcD).Any())
                {
                    lblError.Text = "Debe cargar en los datos de conformación del local, el destino Baño PcD.";
                }

                if (lblError.Text != "")
                {
                    ScriptManager.RegisterStartupScript(updCargarDatos, updCargarDatos.GetType(), "finalizarCarga", "finalizarCarga();showfrmError();", true);
                }
                else
                {
                    if (hid_return_url.Value.Contains("Editar"))
                        Response.Redirect(string.Format("~/" + RouteConfig.VISOR_ENCOMIENDA + "{0}", id_encomienda));
                    else
                        Response.Redirect(string.Format("~/" + RouteConfig.VISOR_ENCOMIENDA + "{0}", id_encomienda));

                }
            }
        }

        protected void grdConformacionLocal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //Checking the RowType of the Row  
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var data = (EncomiendaConformacionLocalDTO)e.Row.DataItem;
                var destino = ((Label)e.Row.FindControl("txtGrillaDestino")); 
                if (destino.Text == "Otros")
                {
                    destino.Text += " (" + data.Detalle_conflocal + ")";
                }
            }

        }
    }
}
