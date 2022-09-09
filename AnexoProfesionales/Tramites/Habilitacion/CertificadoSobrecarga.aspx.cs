using AnexoProfesionales.App_Components;
using BusinesLayer.Implementation;
using DataTransferObject;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnexoProfesionales
{
    public partial class CertificadoSobrecarga : BasePage
    {
        private int id_encomienda
        {
            get
            {
                int ret = 0;
                int.TryParse(hid_id_encomienda.Value, out ret);
                return ret;
            }
            set
            {
                hid_id_encomienda.Value = value.ToString();
            }

        }

        private int id_encomiendadatoslocal
        {
            get
            {
                int ret = 0;
                int.TryParse(hid_id_encomiendadatoslocal.Value, out ret);
                return ret;
            }
            set
            {
                hid_id_encomiendadatoslocal.Value = value.ToString();
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {

            ScriptManager sm = ScriptManager.GetCurrent(this);

            if (sm.IsInAsyncPostBack)
            {
                //esto ScriptManager.RegisterStartupScript(updDatosLocal, updDatosLocal.GetType(), "init_JS_updDatosLocal", "init_JS_updDatosLocal();", true);
            }


            if (!IsPostBack)
            {
                hid_DecimalSeparator.Value = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator.ToString();
                hid_return_url.Value = Request.Url.AbsoluteUri;
                ComprobarEncomienda();
                Titulo.CargarDatos(id_encomienda, "Certificado de Sobrecarga");
            }
            this.ucSobreCargaDatos.AgregarSobrecargaClick += AgregarSobrecargaClick;
            this.ucSobreCargaDatos.CerrarClick += ucSobreCargaDatos_CerrarClick;

        }

        private void ucSobreCargaDatos_CerrarClick(object sender, ucAgregarSobrecargaEventsArgs e)
        {
            updCertificadoSobrecarga.Update();
            this.EjecutarScript(e.upd, "hidefrmAgregarSobrecarga();");
        }

        private void AgregarSobrecargaClick(object sender, ucAgregarSobrecargaEventsArgs e)
        {
            //lblmpeInfo.Text = "";
            try
            {
                DataTable dt = dtSobrecargasCargadas();

                if (e.rowindex == -1)
                {
                    dt.Rows.Add("0", e.id_tipo_destino, e.desc_tipo_destino, e.id_tipo_uso, e.desc_tipo_uso,
                        e.valor, e.detalle, e.id_planta, e.desc_planta, e.losa_sobre, e.id_tipo_uso_1, e.desc_tipo_uso_1,
                        e.valor_1, e.id_tipo_uso_2, e.desc_tipo_uso_2, e.valor_2, e.texto_carga_uso, e.texto_uso_1, e.texto_uso_2);
                }
                else {
                    int rowindex = e.rowindex;
                    DataRow dr = dt.Rows[rowindex];

                    dt.Rows[rowindex]["id_sobrecarga_detalle1"] = "0";
                    dt.Rows[rowindex]["id_tipo_destino"] = e.id_tipo_destino;
                    dt.Rows[rowindex]["desc_tipo_destino"] = e.desc_tipo_destino;
                    dt.Rows[rowindex]["id_tipo_uso"] = e.id_tipo_uso;
                    dt.Rows[rowindex]["desc_tipo_uso"] = e.desc_tipo_uso;
                    dt.Rows[rowindex]["valor"] = e.valor;
                    dt.Rows[rowindex]["detalle"] = e.detalle;
                    dt.Rows[rowindex]["id_planta"] = e.id_planta;
                    dt.Rows[rowindex]["desc_planta"] = e.desc_planta;
                    dt.Rows[rowindex]["losa_sobre"] = e.losa_sobre;
                    dt.Rows[rowindex]["id_tipo_uso_1"] = e.id_tipo_uso_1;
                    dt.Rows[rowindex]["desc_tipo_uso_1"] = e.desc_tipo_uso_1;
                    dt.Rows[rowindex]["valor_1"] = e.valor_1;
                    dt.Rows[rowindex]["id_tipo_uso_2"] = e.id_tipo_uso_2;
                    dt.Rows[rowindex]["desc_tipo_uso_2"] = e.desc_tipo_uso_2;
                    dt.Rows[rowindex]["valor_2"] = e.valor_2;
                    dt.Rows[rowindex]["texto_carga_uso"] = e.texto_carga_uso;
                    dt.Rows[rowindex]["texto_uso_1"] = e.texto_uso_1;
                    dt.Rows[rowindex]["texto_uso_2"] = e.texto_uso_2;
                }
                grdSobrecargas.DataSource = dt;
                grdSobrecargas.DataBind();
                updCertificadoSobrecarga.Update();
                this.EjecutarScript(e.upd, "hidefrmAgregarSobrecarga();");
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                e.Cancel = true;
                lblError.Text = (ex.Message);
                this.EjecutarScript(e.upd, "hidefrmAgregarSobrecarga();showfrmError();");
            }

        }

        private void ComprobarEncomienda()
        {
            if (Page.RouteData.Values["id_encomienda"] != null)
            {
                this.id_encomienda = Convert.ToInt32(Page.RouteData.Values["id_encomienda"].ToString());
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
                        else
                        {
                            EncomiendaDatosLocalBL blEncDl = new EncomiendaDatosLocalBL();
                            EncomiendaDatosLocalDTO enDl = blEncDl.GetByFKIdEncomienda(id_encomienda);
                            this.id_encomiendadatoslocal = enDl.id_encomiendadatoslocal;

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
                CargarDatos();
                ScriptManager.RegisterStartupScript(updCargarDatos, updCargarDatos.GetType(), "finalizarCarga", "finalizarCarga();", true);

            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                ScriptManager.RegisterStartupScript(updCargarDatos, updCargarDatos.GetType(), "finalizarCarga", "finalizarCarga();showfrmError();", true);
            }
        }

        protected string tipoString()
        {
            if (ddlTiposSobrecargas.SelectedIndex == 1)
            {
                return "Conforme a Art 8.1.3 CE:";
            }
            else
            {
                return "Conforme a CIRSOC 4.1:";
            }
        }

        private void CargarDatos()
        {
            cargarCombos(id_encomienda);
            ucSobreCargaDatos.cargarDatos(id_encomienda);

            EncomiendaCertificadoSobrecargaBL blEncCS = new EncomiendaCertificadoSobrecargaBL();

            EncomiendaCertificadoSobrecargaDTO dto = blEncCS.GetByFKIdEncomiendaDatosLocal(id_encomiendadatoslocal);
            if (dto!= null)
            {
                ddlTiposCertificado.SelectedValue = dto.id_tipo_certificado.ToString();
                ddlTiposSobrecargas.SelectedValue = dto.id_tipo_sobrecarga.ToString();
                ddlTiposSobrecargas_SelectedIndexChanged(null, null);

                CargarSobrecargas(dto.id_sobrecarga);

            }
            updCertificadoSobrecarga.Update();
        }

        private void CargarSobrecargas(int id_sobrecarga)
        {
            SobrecargasEntityBL blSob = new SobrecargasEntityBL();
            grdSobrecargas.DataSource = blSob.getSobrecargaDetallado(id_sobrecarga);
            grdSobrecargas.DataBind();
        }

        private void cargarCombos(int id_encomienda)
        {
            EncomiendaTiposCertificadosSobrecargaBL blTiposCert = new EncomiendaTiposCertificadosSobrecargaBL();
            EncomiendaTiposSobrecargasBL blTiposSob = new EncomiendaTiposSobrecargasBL();

            ddlTiposCertificado.DataSource = blTiposCert.GetAll();
            ddlTiposCertificado.DataTextField = "descripcion";
            ddlTiposCertificado.DataValueField = "id_tipo_certificado";
            ddlTiposCertificado.DataBind();

            ddlTiposSobrecargas.DataSource = blTiposSob.GetAll();
            ddlTiposSobrecargas.DataTextField = "descripcion";
            ddlTiposSobrecargas.DataValueField = "id_tipo_sobrecarga";
            ddlTiposSobrecargas.DataBind();
            ddlTiposSobrecargas.Items.Insert(0, new ListItem("", "0"));

        }

        private DataTable dtSobrecargasCargadas()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id_sobrecarga_detalle1", typeof(int));
            dt.Columns.Add("id_tipo_destino", typeof(int));
            dt.Columns.Add("desc_tipo_destino", typeof(string));
            dt.Columns.Add("id_tipo_uso", typeof(int));
            dt.Columns.Add("desc_tipo_uso", typeof(string));
            dt.Columns.Add("valor", typeof(decimal));
            dt.Columns.Add("detalle", typeof(string));
            dt.Columns.Add("id_planta", typeof(int));
            dt.Columns.Add("desc_planta", typeof(string));
            dt.Columns.Add("losa_sobre", typeof(string));
            dt.Columns.Add("id_tipo_uso_1", typeof(int));
            dt.Columns.Add("desc_tipo_uso_1", typeof(string));
            dt.Columns.Add("valor_1", typeof(decimal));
            dt.Columns.Add("id_tipo_uso_2", typeof(int));
            dt.Columns.Add("desc_tipo_uso_2", typeof(string));
            dt.Columns.Add("valor_2", typeof(decimal));
            dt.Columns.Add("texto_carga_uso", typeof(string));
            dt.Columns.Add("texto_uso_1", typeof(string));
            dt.Columns.Add("texto_uso_2", typeof(string));

            foreach (GridViewRow row in grdSobrecargas.Rows)
            {
                DataRow datarw;
                datarw = dt.NewRow();

                HiddenField hid_id_sobrecarga_detalle1 = (HiddenField)grdSobrecargas.Rows[row.RowIndex].FindControl("hid_id_sobrecarga_detalle1");
                HiddenField hid_id_tipo_destino = (HiddenField)grdSobrecargas.Rows[row.RowIndex].FindControl("hid_id_tipo_destino");
                Label lbl_tipo_destino = (Label)grdSobrecargas.Rows[row.RowIndex].FindControl("lbl_tipo_destino");
                HiddenField hid_id_tipo_uso = (HiddenField)grdSobrecargas.Rows[row.RowIndex].FindControl("hid_id_tipo_uso");
                Label lbl_tipo_uso = (Label)grdSobrecargas.Rows[row.RowIndex].FindControl("lbl_tipo_uso");
                Label lbl_valor = (Label)grdSobrecargas.Rows[row.RowIndex].FindControl("lbl_valor");
                Label lbl_detalle = (Label)grdSobrecargas.Rows[row.RowIndex].FindControl("lbl_detalle");
                HiddenField hid_id_planta = (HiddenField)grdSobrecargas.Rows[row.RowIndex].FindControl("hid_id_planta");
                Label lbl_planta = (Label)grdSobrecargas.Rows[row.RowIndex].FindControl("lbl_planta");
                Label lbl_losa_sobre = (Label)grdSobrecargas.Rows[row.RowIndex].FindControl("lbl_losa_sobre");
                HiddenField hid_id_tipo_uso_1 = (HiddenField)grdSobrecargas.Rows[row.RowIndex].FindControl("hid_id_tipo_uso_1");
                Label lbl_tipo_uso_1 = (Label)grdSobrecargas.Rows[row.RowIndex].FindControl("lbl_tipo_uso_1");
                Label lbl_valor_1 = (Label)grdSobrecargas.Rows[row.RowIndex].FindControl("lbl_valor_1");
                HiddenField hid_id_tipo_uso_2 = (HiddenField)grdSobrecargas.Rows[row.RowIndex].FindControl("hid_id_tipo_uso_2");
                Label lbl_tipo_uso_2 = (Label)grdSobrecargas.Rows[row.RowIndex].FindControl("lbl_tipo_uso_2");
                Label lbl_valor_2 = (Label)grdSobrecargas.Rows[row.RowIndex].FindControl("lbl_valor_2");
                Label lblSobrecarga = (Label)grdSobrecargas.Rows[row.RowIndex].FindControl("lblSobrecarga");
                Label lblUso1 = (Label)grdSobrecargas.Rows[row.RowIndex].FindControl("lblUso1");
                Label lblUso2 = (Label)grdSobrecargas.Rows[row.RowIndex].FindControl("lblUso2");

                datarw[0] = int.Parse(hid_id_sobrecarga_detalle1.Value);
                datarw[1] = int.Parse(hid_id_tipo_destino.Value);
                datarw[2] = HttpUtility.HtmlDecode(lbl_tipo_destino.Text);
                datarw[3] = int.Parse(hid_id_tipo_uso.Value);
                datarw[4] = HttpUtility.HtmlDecode(lbl_tipo_uso.Text);
                datarw[5] = decimal.Parse(lbl_valor.Text);
                datarw[6] = HttpUtility.HtmlDecode(lbl_detalle.Text).Trim();
                datarw[7] = int.Parse(hid_id_planta.Value);
                datarw[8] = HttpUtility.HtmlDecode(lbl_planta.Text).Trim();
                datarw[9] = HttpUtility.HtmlDecode(lbl_losa_sobre.Text);
                datarw[10] = int.Parse(hid_id_tipo_uso_1.Value);
                datarw[11] = HttpUtility.HtmlDecode(lbl_tipo_uso_1.Text);
                datarw[12] = decimal.Parse(lbl_valor_1.Text.Length > 0 ? lbl_valor_1.Text : "0,00");
                datarw[13] = int.Parse(hid_id_tipo_uso_2.Value);
                datarw[14] = HttpUtility.HtmlDecode(lbl_tipo_uso_2.Text);
                datarw[15] = decimal.Parse(lbl_valor_2.Text.Length > 0 ? lbl_valor_2.Text : "0,00");
                datarw[16] = HttpUtility.HtmlDecode(lblSobrecarga.Text);
                datarw[17] = HttpUtility.HtmlDecode(lblUso1.Text);
                datarw[18] = HttpUtility.HtmlDecode(lblUso2.Text);

                dt.Rows.Add(datarw);
            }

            return dt;
        }

        protected void ddlTiposSobrecargas_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = int.Parse(ddlTiposSobrecargas.SelectedValue);
            ucSobreCargaDatos.changedTiposSobrecargas(id);

            //Eliminos las sobrecargas cargadas
            DataTable dt = dtSobrecargasCargadas();
            dt.Rows.Clear();
            grdSobrecargas.DataSource = dt;
            grdSobrecargas.DataBind();
        }

        protected void lnkGuardarYContinuar_Click(object sender, EventArgs e)
        {
            try
            {
                EncomiendaCertificadoSobrecargaBL blEncS = new EncomiendaCertificadoSobrecargaBL();
                EncomiendaCertificadoSobrecargaDTO certDTO = blEncS.GetByFKIdEncomiendaDatosLocal(this.id_encomiendadatoslocal);

                EncomiendaSobrecargaDetalle1BL blEncS1 = new EncomiendaSobrecargaDetalle1BL();
                EncomiendaSobrecargaDetalle2BL blEncS2 = new EncomiendaSobrecargaDetalle2BL();
                IEnumerable<EncomiendaSobrecargaDetalle1DTO> list1;
                IEnumerable<EncomiendaSobrecargaDetalle2DTO> list2;

                int id_sobrecarga = 0;
                bool alta = false;
                //Elimino lo ingresado
                if (certDTO != null)
                {
                    id_sobrecarga = certDTO.id_sobrecarga;
                    list1 = blEncS1.GetByFKIdSobrecarga(certDTO.id_sobrecarga);
                    foreach (EncomiendaSobrecargaDetalle1DTO s1 in list1)
                    {
                        list2 = blEncS2.GetByFKIdSobrecargaDetalle1(s1.id_sobrecarga_detalle1);
                        foreach (EncomiendaSobrecargaDetalle2DTO s2 in list2)
                            blEncS2.Delete(s2);
                        blEncS1.Delete(s1);
                    }
                }
                else
                {
                    certDTO = new EncomiendaCertificadoSobrecargaDTO();
                    alta = true;
                }

                int id_tipo_certificado = 0;
                int id_tipo_sobrecarga = 0;

                int.TryParse(ddlTiposCertificado.SelectedValue, out id_tipo_certificado);
                int.TryParse(ddlTiposSobrecargas.SelectedValue, out id_tipo_sobrecarga);
                certDTO.id_encomienda_datoslocal = id_encomiendadatoslocal;
                certDTO.id_tipo_certificado = id_tipo_certificado;
                certDTO.id_tipo_sobrecarga = id_tipo_sobrecarga;
                certDTO.CreateDate = DateTime.Now;
                if (alta)
                    id_sobrecarga = blEncS.Insert(certDTO);
                else
                    blEncS.Update(certDTO);

                DataTable dtSobrecargas = dtSobrecargasCargadas();

                EncomiendaSobrecargaDetalle1DTO sobre1;
                EncomiendaSobrecargaDetalle2DTO sobre2;
                foreach (DataRow dr in dtSobrecargas.Rows)
                {
                    sobre1 = new EncomiendaSobrecargaDetalle1DTO();
                    sobre1.id_sobrecarga = id_sobrecarga;
                    sobre1.id_tipo_destino = int.Parse(dr["id_tipo_destino"].ToString());
                    sobre1.id_tipo_uso = int.Parse(dr["id_tipo_uso"].ToString());
                    sobre1.valor = decimal.Parse(dr["valor"].ToString(), new CultureInfo("es-AR"));
                    sobre1.id_encomiendatiposector = int.Parse(dr["id_planta"].ToString());
                    sobre1.losa_sobre = dr["losa_sobre"].ToString();
                    sobre1.detalle = dr["detalle"].ToString();
                    sobre2 = new EncomiendaSobrecargaDetalle2DTO();
                    sobre2.id_sobrecarga_detalle1 = blEncS1.Insert(sobre1);
                    sobre2.id_tipo_uso_1 = int.Parse(dr["id_tipo_uso_1"].ToString());
                    sobre2.valor_1 = decimal.Parse(dr["valor_1"].ToString(), new CultureInfo("es-AR"));
                    sobre2.id_tipo_uso_2 = int.Parse(dr["id_tipo_uso_2"].ToString());
                    sobre2.valor_2 = decimal.Parse(dr["valor_2"].ToString(), new CultureInfo("es-AR"));
                    blEncS2.Insert(sobre2);
                }

                if (hid_return_url.Value.Contains("Editar"))
                    Response.Redirect(string.Format("~/" + RouteConfig.VISOR_ENCOMIENDA + "{0}", id_encomienda));
                else
                    Response.Redirect(string.Format("~/" + RouteConfig.AGREGAR_ENCOMIENDA_RUBROS + "{0}", id_encomienda));

            }                                                                     
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                this.EjecutarScript(updBotonesGuardar, "showfrmError();");
            }

        }

        protected void btnEditarSobrecarga_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.Parent.Parent;
            DataTable dt = dtSobrecargasCargadas();

            DataRow dr = dt.Rows[row.RowIndex];
            ucAgregarSobrecargaEventsArgs args = new ucAgregarSobrecargaEventsArgs();


            args.id_tipo_destino = int.Parse(dr["id_tipo_destino"].ToString());
            args.id_tipo_uso = int.Parse(dr["id_tipo_uso"].ToString());
            args.valor = decimal.Parse(dr["valor"].ToString(), new CultureInfo("es-AR"));
            args.id_planta = int.Parse(dr["id_planta"].ToString());
            args.losa_sobre = dr["losa_sobre"].ToString();
            args.detalle = dr["detalle"].ToString();
            args.id_tipo_uso_1 = int.Parse(dr["id_tipo_uso_1"].ToString());
            args.valor_1 = decimal.Parse(dr["valor_1"].ToString(), new CultureInfo("es-AR"));
            args.id_tipo_uso_2 = int.Parse(dr["id_tipo_uso_2"].ToString());
            args.valor_2 = decimal.Parse(dr["valor_2"].ToString(), new CultureInfo("es-AR"));
            args.texto_carga_uso = dr["texto_carga_uso"].ToString();
            args.texto_uso_1 = dr["texto_uso_1"].ToString();
            args.texto_uso_2 = dr["texto_uso_2"].ToString();

            args.rowindex = row.RowIndex;
            ucSobreCargaDatos.editar(args);
            this.EjecutarScript(updGrillaSobreCargas, "showfrmAgregarSobrecarga();");
        }

        protected void btnEliminarSobrecarga_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.Parent.Parent;

            this.EjecutarScript(updGrillaSobreCargas, "showfrmConfirmarEliminar(" + row.RowIndex.ToString() + ");");
            
        }


        protected void btnConfirmarEliminarSobrecarga_Click(object sender, EventArgs e)
        {
            int Rowindex = Convert.ToInt32(hid_id_sobrecarga_eliminar.Value);

            DataTable dt = dtSobrecargasCargadas();
            dt.Rows.Remove(dt.Rows[Rowindex]);
            grdSobrecargas.DataSource = dt;
            grdSobrecargas.DataBind();
            //CargarDatos();
            //updGrillaSobreCargas.Update();

            this.EjecutarScript(updConfirmarEliminar, "hidefrmConfirmarEliminar();");
            ScriptManager.RegisterStartupScript(updCargarDatos, updCargarDatos.GetType(), "finalizarCarga", "finalizarCarga();", true);
        }
    }
}