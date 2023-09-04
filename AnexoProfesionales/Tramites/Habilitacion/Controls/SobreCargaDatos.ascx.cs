using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinesLayer.Implementation;
using DataTransferObject;
using AnexoProfesionales.App_Components;
using System.Globalization;
using StaticClass;

namespace AnexoProfesionales
{
    public class ucAgregarSobrecargaEventsArgs : EventArgs
    {
        public int rowindex { get; set; }
        public int id_sobrecarga_detalle1 { get; set; }
        public int id_tipo_destino { get; set; }
        public string desc_tipo_destino { get; set; }
        public int id_tipo_uso { get; set; }
        public string desc_tipo_uso { get; set; }
        public decimal valor { get; set; }
        public string detalle { get; set; }
        public int id_planta { get; set; }
        public string desc_planta { get; set; }
        public string losa_sobre { get; set; }
        public int id_tipo_uso_1 { get; set; }
        public string desc_tipo_uso_1 { get; set; }
        public decimal valor_1 { get; set; }
        public int id_tipo_uso_2 { get; set; }
        public string desc_tipo_uso_2 { get; set; }
        public decimal valor_2 { get; set; }
        public string texto_carga_uso { get; set; }
        public string texto_uso_1 { get; set; }
        public string texto_uso_2 { get; set; }
        public UpdatePanel upd { get; set; }
        public bool Cancel { get; set; }    // se utilizar para saber si se cancelo o no luego del llamado.
    }

    public partial class SobreCargaDatos : System.Web.UI.UserControl
    {
        private static string _OnCerrarClick = "";

        public delegate void EventHandlerCerrar(object sender, ucAgregarSobrecargaEventsArgs e);
        public event EventHandlerCerrar CerrarClick;

        public delegate void EventHandlerAgregarSobrecarga(object sender, ucAgregarSobrecargaEventsArgs e);
        public event EventHandlerAgregarSobrecarga AgregarSobrecargaClick;

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager sm = ScriptManager.GetCurrent(this.Page);

            if (sm.IsInAsyncPostBack)
            {
                ScriptManager.RegisterStartupScript(updSobrecarga, updSobrecarga.GetType(), "init_JS_updSobrecarga", "init_JS_updSobrecarga();", true);
            }
            if (!IsPostBack)
            {
                //hid_DecimalSeparatorS.Value = CultureInfo.InvariantCulture.NumberFormat.CurrencyGroupSeparator;

                //hid_DecimalSeparatorS.Value = CultureInfo.InvariantCulture.NumberFormat.;
                hid_DecimalSeparatorS.Value = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            }
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            Inicilizar_Control();
            if (!string.IsNullOrEmpty(_OnCerrarClick))
            {
                ((BasePage)this.Page).EjecutarScript(updSobrecarga, OnCerrarClientClick);
            }
            ucAgregarSobrecargaEventsArgs args = new ucAgregarSobrecargaEventsArgs();
            args.upd = updSobrecarga;
            if (CerrarClick != null)
                CerrarClick(sender, args);
        }

        public string OnCerrarClientClick
        {
            get
            {
                return _OnCerrarClick;
            }
            set
            {
                _OnCerrarClick = value;
            }
        }

        public void cargarDatos(int id_encomienda)
        {
            EncomiendaPlantasBL blEncPlantas = new EncomiendaPlantasBL();
            EncomiendaTiposDestinosBL blTipos = new EncomiendaTiposDestinosBL();
            List<EncomiendaPlantasDTO> lstPlantasDTO = blEncPlantas.GetByFKIdEncomienda(id_encomienda).ToList();

            ddlPlantas.DataSource = lstPlantasDTO;
            ddlPlantas.DataTextField = "Detalle";
            ddlPlantas.DataValueField = "id_encomiendatiposector";
            ddlPlantas.DataBind();
            ddlPlantas.Items.Insert(0, new ListItem("", "0"));

            ddlDestino.DataSource = blTipos.GetByFKIdTipoSobrecarga(1);
            ddlDestino.DataTextField = "descripcion";
            ddlDestino.DataValueField = "id_tipo_destino";
            ddlDestino.DataBind();
            ddlDestino.Items.Insert(0, new ListItem("", "0"));
        }
        public void changedTiposSobrecargas(int id_tipoSobrecarga)
        {
            EncomiendaTiposDestinosBL blTipos = new EncomiendaTiposDestinosBL();

            ddlDestino.DataSource = blTipos.GetByFKIdTipoSobrecarga(id_tipoSobrecarga);
            ddlDestino.DataTextField = "descripcion";
            ddlDestino.DataValueField = "id_tipo_destino";
            ddlDestino.DataBind();
            ddlDestino.Items.Insert(0, new ListItem("", "0"));

            if (id_tipoSobrecarga == 1)
            {
                lblTipoDestino.Text = "Conforme a Art 8.1.3 CE:";
                lblSobrecarga.Text = "Admite sobrecarga de [kg/m2]";
                lblUso1.Text = "Pasillos de acceso general, escaleras, balcones";
                lblTxtUso1.Text = "Admite sobrecarga de [kg/m2]";
                lblUso2.Text = "Barandilla de balcones y escaleras, esfuerzo horizontal dirigido al interior y aplicado sobre el pasamanos";
                lblTxtUso2.Text = "Admite sobrecarga de [kg/m2]";
            }
            else
            {
                lblTipoDestino.Text = "Conforme a CIRSOC 4.1:";
                lblSobrecarga.Text = "Admite sobrecarga de [kN/m2]";
                lblUso1.Text = "Escaleras";
                lblTxtUso1.Text = "Admite sobrecarga de [kN/m2]";
                lblUso2.Text = "Barandas";
                lblTxtUso2.Text = "Admite sobrecarga de [kN/m2]";

            }

        }
        protected void ddlDestino_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = int.Parse(ddlDestino.SelectedValue);
            EncomiendaTiposDestinosBL blTipos = new EncomiendaTiposDestinosBL();
            EncomiendaTiposDestinosDTO tipo = blTipos.Single(id);
            hid_requiere_detalle_2.Value = tipo.requiere_detalle_2.ToString().ToLower();

            EncomiendaTiposUsosBL lbTUsos = new EncomiendaTiposUsosBL();
            ddlUsos.DataSource = lbTUsos.GetByFKIdTipoDestinoGrupo(id, 0);
            ddlUsos.DataTextField = "descripcion";
            ddlUsos.DataValueField = "id_tipo_uso";
            ddlUsos.DataBind();
            ddlUsos.Items.Insert(0, new ListItem("", "0"));

            IEnumerable<EncomiendaTiposUsosDTO> usos1 = lbTUsos.GetByFKIdTipoDestinoGrupo(id, 1);
            ddlUsos1.DataSource = usos1;
            ddlUsos1.DataTextField = "descripcion";
            ddlUsos1.DataValueField = "id_tipo_uso";
            ddlUsos1.DataBind();

            IEnumerable<EncomiendaTiposUsosDTO> usos2 = lbTUsos.GetByFKIdTipoDestinoGrupo(id, 2);
            ddlUsos2.DataSource = usos2;
            ddlUsos2.DataTextField = "descripcion";
            ddlUsos2.DataValueField = "id_tipo_uso";
            ddlUsos2.DataBind();

            //Seteo min req para los uso 1 y ya que son uno solo para cada uno
            hid_min_uso1_req.Value = "999999";
            hid_min_uso2_req.Value = "999999";
            EncomiendaRelTiposDestinosTiposUsosBL blRel = new EncomiendaRelTiposDestinosTiposUsosBL();
            IEnumerable<EncomiendaRelTiposDestinosTiposUsosDTO> lstRel;
            EncomiendaRelTiposDestinosTiposUsosDTO rel;
            if (usos1.Count() > 0)
            {
                EncomiendaTiposUsosDTO u = usos1.First();
                lstRel = blRel.GetByFKIdTipoDestinoTipoTipo(u.id_tipo_uso, id);
                if (lstRel.Count() > 0)
                {
                    rel = lstRel.First();
                    hid_min_uso1_req.Value = rel.valor_min_req.ToString();
                }
            }

            if (usos2.Count() > 0)
            {
                EncomiendaTiposUsosDTO u = usos2.First();
                lstRel = blRel.GetByFKIdTipoDestinoTipoTipo(u.id_tipo_uso, id);
                if (lstRel.Count() > 0)
                {
                    rel = lstRel.First();
                    hid_min_uso2_req.Value = rel.valor_min_req.ToString();
                }
            }

        }

        protected void ddlUsos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id_tipo_uso = int.Parse(ddlUsos.SelectedValue);
            int id_tipo_destino = int.Parse(ddlDestino.SelectedValue);
            EncomiendaRelTiposDestinosTiposUsosBL blRel = new EncomiendaRelTiposDestinosTiposUsosBL();
            IEnumerable<EncomiendaRelTiposDestinosTiposUsosDTO> lstRel;
            EncomiendaRelTiposDestinosTiposUsosDTO rel;

            lstRel = blRel.GetByFKIdTipoDestinoTipoTipo(id_tipo_uso, id_tipo_destino);

            txtDetalle.Text = "";
            txtDetalle.Enabled = true;

            if (lstRel.Count() > 0)
            {
                rel = lstRel.First();
                hid_min_req.Value = rel.valor_min_req.ToString();
                txtDetalle.Visible = rel.requiere_detalle;

                if (rel.texto_fijo_detalle != null && rel.texto_fijo_detalle.Length > 0)
                {
                    txtDetalle.Text = rel.texto_fijo_detalle;
                    txtDetalle.Enabled = false;
                }
            }
            else
            {
                hid_min_req.Value = "999999";
                txtDetalle.Visible = false;
            }

        }

        protected void btnIngresarSobrecarga_Click(object sender, EventArgs e)
        {
            ucAgregarSobrecargaEventsArgs args = new ucAgregarSobrecargaEventsArgs();
            if (hid_rowindex_fir.Value.Length == 0)
                args.rowindex = -1;
            else
                args.rowindex = int.Parse(hid_rowindex_fir.Value);
            args.id_tipo_destino = int.Parse(ddlDestino.SelectedValue);
            args.desc_tipo_destino = ddlDestino.SelectedItem.Text;
            args.id_tipo_uso = int.Parse(ddlUsos.SelectedValue);
            args.desc_tipo_uso = ddlUsos.SelectedItem.Text;
            args.valor = decimal.Parse(txtSobrecarga.Text.Trim());
            args.detalle = (txtDetalle.Visible ? txtDetalle.Text : "");
            args.id_planta = int.Parse(ddlPlantas.SelectedValue);
            args.desc_planta = ddlPlantas.SelectedItem.Text;
            args.losa_sobre = txtLosaSobre.Text.Trim();
            args.id_tipo_uso_1 = int.Parse(ddlUsos1.SelectedValue);
            args.desc_tipo_uso_1 = ddlUsos1.SelectedItem.Text;
            args.valor_1 = (txtUso1.Text.Trim().Length > 0 ? decimal.Parse(txtUso1.Text.Trim()) : decimal.Parse("0,00"));
            args.id_tipo_uso_2 = int.Parse(ddlUsos2.SelectedValue);
            args.desc_tipo_uso_2 = ddlUsos2.SelectedItem.Text;
            args.valor_2 = (txtUso2.Text.Trim().Length > 0 ? decimal.Parse(txtUso2.Text.Trim()) : decimal.Parse("0,00"));
            args.texto_carga_uso = lblSobrecarga.Text;
            args.texto_uso_1 = lblUso1.Text;
            args.texto_uso_2 = lblUso2.Text;
            args.upd = updSobrecarga;
            //Llamar al evento.
            if (AgregarSobrecargaClick != null)
            {
                AgregarSobrecargaClick(sender, args);
                if (!args.Cancel)
                    Inicilizar_Control();
            }
        }

        private void Inicilizar_Control()
        {
            txtSobrecarga.Text = "";
            txtUso1.Text = "";
            txtUso2.Text = "";
            txtLosaSobre.Text = "";
            hid_rowindex_fir.Value = "";

            ddlDestino.SelectedIndex = -1;
            ddlPlantas.SelectedIndex = -1;
            ddlUsos.SelectedIndex = -1;
            txtDetalle.Text = "";
            txtDetalle.Visible = false;
        }

        public void editar(ucAgregarSobrecargaEventsArgs args)
        {
            hid_rowindex_fir.Value = args.rowindex.ToString();
            ddlDestino.SelectedValue = args.id_tipo_destino.ToString();
            ddlDestino_SelectedIndexChanged(null, null);
            ddlUsos.SelectedValue = args.id_tipo_uso.ToString();
            ddlUsos_SelectedIndexChanged(null, null);
            txtSobrecarga.Text = args.valor.ToString();
            txtDetalle.Text = args.detalle.ToString();
            txtDetalle.Visible = txtDetalle.Text.Length > 0;
            ddlPlantas.SelectedValue = args.id_planta.ToString();
            txtLosaSobre.Text = args.losa_sobre.ToString();
            ddlUsos1.SelectedValue = args.id_tipo_uso_1.ToString();
            txtUso1.Text = !args.valor_1.Equals(decimal.Parse("0,00")) ? args.valor_1.ToString() : "";

            ddlUsos2.SelectedValue = args.id_tipo_uso_2.ToString();
            txtUso2.Text = !args.valor_2.Equals(decimal.Parse("0,00")) ? args.valor_2.ToString() : "";
            lblSobrecarga.Text = args.texto_carga_uso.ToString();
            lblUso1.Text = args.texto_uso_1.ToString();
            lblUso2.Text = args.texto_uso_2.ToString();
        }

    }
}