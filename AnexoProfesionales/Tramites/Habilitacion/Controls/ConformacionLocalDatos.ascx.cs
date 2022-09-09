using AnexoProfesionales.App_Components;
using BusinesLayer.Implementation;
using DataTransferObject;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnexoProfesionales.Controls
{

    public class ConformacionLocalEventsArgs : EventArgs
    {
        public int id_encomiendaconflocal { get; set; }
        public int id_destino { get; set; }
        public decimal largo_conflocal { get; set; }
        public decimal ancho_conflocal { get; set; }
        public decimal alto_conflocal { get; set; }
        public string Paredes_conflocal { get; set; }
        public string Techos_conflocal { get; set; }
        public string Pisos_conflocal { get; set; }
        public string Frisos_conflocal { get; set; }
        public string Observaciones_conflocal { get; set; }
        public string Detalle_conflocal { get; set; }
        public int? id_encomiendatiposector { get; set; }
        public int? id_ventilacion { get; set; }
        public int? id_iluminacion { get; set; }
        public decimal superficie_conflocal { get; set; }
        public int id_tiposuperficie { get; set; }
        public UpdatePanel upd { get; set; }
        public bool Cancel { get; set; }    // se utilizar para saber si se cancelo o no luego del llamado.
    }


    public partial class ConformacionLocalDatos : System.Web.UI.UserControl
    {
        private static string _OnCerrarClick = "";

        public delegate void EventHandlerCerrar(object sender, ConformacionLocalEventsArgs e);
        public event EventHandlerCerrar CerrarClick;

        public delegate void EventHandlerConformacionLocal(object sender, ConformacionLocalEventsArgs e);
        public event EventHandlerConformacionLocal AgregarConformacionLocalClick;

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager sm = ScriptManager.GetCurrent(this.Page);

            if (sm.IsInAsyncPostBack)
            {
                ScriptManager.RegisterStartupScript(updConformacionLocalDatos, updConformacionLocalDatos.GetType(), "init_JS_updConformacionLocal", "init_JS_updConformacionLocal();", true);
            }
            if (!IsPostBack)
            {
                hid_DecimalSeparatorCL.Value = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            }

        }
        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            Inicilizar_Control();
            if (!string.IsNullOrEmpty(_OnCerrarClick))
            {
                ((BasePage)this.Page).EjecutarScript(updConformacionLocalDatos, OnCerrarClientClick);
            }
            ConformacionLocalEventsArgs args = new ConformacionLocalEventsArgs();
            args.upd = updConformacionLocalDatos;
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

        public void CargarDatos(int IdEncomienda)
        {
            TipoDestinoBL blTD = new TipoDestinoBL();
            IEnumerable<TipoDestinoDTO> lstTD= blTD.GetAll();
            ddlDestino.DataSource = lstTD;
            ddlDestino.DataTextField = "nombre";
            ddlDestino.DataValueField = "id";
            ddlDestino.DataBind();
            ddlDestino_SelectedIndexChanged(null, null);

            EncomiendaPlantasBL blEP = new EncomiendaPlantasBL();
            ddlPlanta.DataTextField = "Detalle";
            ddlPlanta.DataValueField = "id_encomiendatiposector";
            ddlPlanta.DataSource = blEP.GetByFKIdEncomienda(IdEncomienda);
            ddlPlanta.DataBind();
            ddlPlanta.Items.Insert(0, new ListItem("", "0"));

            TipoVentilacionBL blTV = new TipoVentilacionBL();
            ddlVentilacion.DataTextField = "nom_ventilacion";
            ddlVentilacion.DataValueField = "id_ventilacion";
            ddlVentilacion.DataSource = blTV.GetAll();
            ddlVentilacion.DataBind();
            ddlVentilacion.Items.Insert(0, new ListItem("", "-1"));
            ddlVentilacion.SelectedIndex = 1;

            TipoIluminacionBL blTI = new TipoIluminacionBL();
            ddlIluminacion.DataTextField = "nom_iluminacion";
            ddlIluminacion.DataValueField = "id_iluminacion";
            ddlIluminacion.DataSource = blTI.GetAll();
            ddlIluminacion.DataBind();
            ddlIluminacion.Items.Insert(0, new ListItem("", "0"));

            TipoSuperficieBL blTS = new TipoSuperficieBL();
            ddlTipoSuperficie.DataTextField = "Nombre";
            ddlTipoSuperficie.DataValueField = "Id";
            ddlTipoSuperficie.DataSource = blTS.GetAll();
            ddlTipoSuperficie.DataBind();
            ddlTipoSuperficie.Items.Insert(0, new ListItem("", "0"));
            updpnlDatos.Update();
        }

        private void Inicilizar_Control()
        {
            hid_conflocal.Value = "0";
            ddlDestino.SelectedIndex = 0;
            ddlPlanta.SelectedIndex = 0;
            txtAlto.Text = "";
            txtAncho.Text = "";
            txtLargo.Text = "";
            txtSuperficie.Text = "";
            txtParedes.Text = "";
            txtPisos.Text = "";
            txtTechos.Text = "";
            txtFrisos.Text = "";
            txtObservaciones.Text = "";
            txtDetalle.Text = "";
            ddlVentilacion.SelectedIndex = 0;
            ddlIluminacion.SelectedIndex = 0;
            ddlTipoSuperficie.SelectedIndex = 0;
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            ConformacionLocalEventsArgs args = new ConformacionLocalEventsArgs();
            if (hid_conflocal.Value.Length == 0)
                args.id_encomiendaconflocal = 0;
            else
                args.id_encomiendaconflocal = int.Parse(hid_conflocal.Value);
            args.id_destino = int.Parse(ddlDestino.SelectedValue);
            args.largo_conflocal = decimal.Parse(txtLargo.Text);
            args.ancho_conflocal = decimal.Parse(txtAncho.Text);
            args.alto_conflocal = decimal.Parse(txtAlto.Text); 
            args.superficie_conflocal = decimal.Parse(txtSuperficie.Text); 
            args.Paredes_conflocal = txtParedes.Text.Trim(); 
            args.Techos_conflocal = txtTechos.Text.Trim();
            args.Pisos_conflocal = txtPisos.Text.Trim();
            args.Frisos_conflocal = txtFrisos.Text.Trim();
            args.Observaciones_conflocal = txtObservaciones.Text.Trim();
            args.id_encomiendatiposector = Convert.ToInt32(ddlPlanta.SelectedValue);
            args.id_ventilacion = Convert.ToInt32(ddlVentilacion.SelectedValue);
            args.id_iluminacion = Convert.ToInt32(ddlIluminacion.SelectedValue);
            args.id_tiposuperficie = Convert.ToInt32(ddlTipoSuperficie.SelectedValue);
            args.Detalle_conflocal = txtDetalle.Text.Trim();
            args.upd = updConformacionLocalDatos;
            //Llamar al evento.
            if (args.id_destino == (int)Constantes.TipoDestino.PlayaCargaDescarga && args.superficie_conflocal < 30)
            {
                ScriptManager.RegisterStartupScript(updpnlDatos, updpnlDatos.GetType(), "validar", "validar();", true);
                return;
            }
            if (AgregarConformacionLocalClick != null)
            {
                AgregarConformacionLocalClick(sender, args);
                if (!args.Cancel)
                    Inicilizar_Control();
            }
        }

        public void editar(ConformacionLocalEventsArgs args)
        {
            hid_conflocal.Value = args.id_encomiendaconflocal.ToString();

            ddlDestino.SelectedValue = args.id_destino.ToString();
            ddlDestino_SelectedIndexChanged(null, null);
            txtLargo.Text = args.largo_conflocal.ToString();
            txtAncho.Text = args.ancho_conflocal.ToString();
            txtAlto.Text = args.alto_conflocal.ToString();
            txtSuperficie.Text = args.superficie_conflocal.ToString();
            txtParedes.Text = args.Paredes_conflocal;
            txtTechos.Text = args.Techos_conflocal;
            txtPisos.Text = args.Pisos_conflocal;
            txtFrisos.Text = args.Frisos_conflocal;
            txtObservaciones.Text = args.Observaciones_conflocal;
            if (args.id_encomiendatiposector.HasValue)
                ddlPlanta.SelectedValue =  args.id_encomiendatiposector.Value.ToString();
            if (args.id_ventilacion.HasValue)
                ddlVentilacion.SelectedValue =  args.id_ventilacion.Value.ToString();
            if (args.id_iluminacion.HasValue)
                ddlIluminacion.SelectedValue = args.id_iluminacion.ToString();

            ddlTipoSuperficie.SelectedValue = args.id_tiposuperficie.ToString();
            txtDetalle.Text = args.Detalle_conflocal;
        }

        protected void ddlDestino_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id_tipo_destino = int.Parse(ddlDestino.SelectedValue);
            TipoDestinoBL blTD = new TipoDestinoBL();

            TipoDestinoDTO t = blTD.Single(id_tipo_destino);
            txtDetalle.Text = "";
            pnlTxtDetalle.Visible = false;
            pnlTxtDetalle.Visible = t.RequiereDetalle;
            updpnlDatos.Update();
        }
    }
}