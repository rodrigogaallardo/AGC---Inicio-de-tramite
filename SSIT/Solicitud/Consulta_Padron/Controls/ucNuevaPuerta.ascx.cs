using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinesLayer.Implementation;
using SSIT.Common;
using DataTransferObject;
using StaticClass;

namespace SSIT.Solicitud.Consulta_Padron.Controls
{
    public partial class ucNuevaPuerta : System.Web.UI.UserControl
    {
        public int IdUbicacion { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

            ScriptManager sm = ScriptManager.GetCurrent(this.Page);

            if (sm.IsInAsyncPostBack)
            {
                ScriptManager.RegisterStartupScript(updSolicitarNuevaPuerta, updSolicitarNuevaPuerta.GetType(), "init_Js_updSolicitarNuevaPuerta", "init_Js_updSolicitarNuevaPuerta();", true);
            }
        }
       
        public void LoadData(int id_ubicacion)
        {
            //hid_id_ubicacion.Value = IdUbicacion.ToString();
            UbicacionesBL ubicacionesBL = new UbicacionesBL();
            List<UbicacionesDTO> lstubicDTO = new List<UbicacionesDTO>();
            var ubicDTO = ubicacionesBL.Single(IdUbicacion);
            lstubicDTO.Add(ubicDTO);
            gridubicacion.DataSource = lstubicDTO;
            gridubicacion.DataBind();
            btnEnviarMail.Visible = true;
            pnlEnviadoOK.Visible = false;
        }

        protected void gridubicacion_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                UbicacionesBL ubicacionesBL = new UbicacionesBL();
                UbicacionesPuertasBL ubicPuerBL = new UbicacionesPuertasBL();
                CallesBL caBL = new CallesBL();

                HiddenField hid_id_ubicacion = (HiddenField)e.Row.FindControl("hid_id_ubicacion");
                DataList lstPuertas = (DataList)e.Row.FindControl("lstPuertas");
                DropDownList ddlCalles_NP = (DropDownList)e.Row.FindControl("ddlCalles_NP");

                int id_ubicacion = int.Parse(hid_id_ubicacion.Value);

                var ubicacionDTO = ubicacionesBL.Single(id_ubicacion);
                var ubicPueDTO = ubicPuerBL.GetByFKIdUbicacion(id_ubicacion);
                var lstCalles = caBL.GetCalles().ToList();

                lstPuertas.DataSource = ubicPueDTO.ToList();
                lstPuertas.DataBind();
                    
                ddlCalles_NP.DataSource = lstCalles;
                ddlCalles_NP.DataTextField = "NombreOficial_calle";
                ddlCalles_NP.DataValueField = "Codigo_calle";
                ddlCalles_NP.DataBind();
                ddlCalles_NP.Items.Insert(0, "");
            }

        }

        protected void btnEnviarMail_Click(object sender, EventArgs e)
        {

            if (gridubicacion.Rows.Count > 0)
            {
                Guid userid = Functions.GetUserid();
                GridViewRow row = gridubicacion.Rows[0];
                DropDownList ddlCalles_NP = (DropDownList)row.FindControl("ddlCalles_NP");
                TextBox txtNroPuerta_NP = (TextBox)row.FindControl("txtNroPuerta_NP");
                HiddenField hid_id_ubicacion = (HiddenField)row.FindControl("hid_id_ubicacion");

                int id_ubicacion = int.Parse(hid_id_ubicacion.Value);
                int NroPuerta = int.Parse(txtNroPuerta_NP.Text);

                UbicacionesBL ubicBL = new UbicacionesBL();
                ubicBL.SendMailSolicitudNuevaPuerta(userid, id_ubicacion, NroPuerta, ddlCalles_NP.SelectedItem.Text);

                btnEnviarMail.Visible = false;
                pnlEnviadoOK.Visible = true;
                //((BasePage)this.Page).EjecutarScript(updSolicitarNuevaPuerta, "hidefrmSolicitarNuevaPuerta();");
            }
        }

        public string GetUrlFoto(int ancho, int alto, int? seccion, string manzana, string parcela)
        {
            if (seccion.HasValue)
                return Functions.GetUrlFoto(seccion.Value, manzana, parcela, ancho, alto);
            else
                return Functions.ImageNotFound(this.Page);
        }     
    }
}