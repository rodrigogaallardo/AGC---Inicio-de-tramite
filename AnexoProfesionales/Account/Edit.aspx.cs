using BusinesLayer.Implementation;
using DataTransferObject;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnexoProfesionales.Account
{
    public partial class Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            ScriptManager sm = ScriptManager.GetCurrent(this);

            if (!Page.IsPostBack)
            {
                OnPageLoaded();
            }
        }


        //Private members
        private void OnPageLoaded()
        {
            try
            {
                CargarDatos();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "mostrarError", "mostrarPopup('pnlError');", true);
            }
        }

        private void CargarDatos()
        {
            MembershipUser usuario = Membership.GetUser();

            Guid userId = (Guid)usuario.ProviderUserKey;

            ProfesionalesBL profBl = new ProfesionalesBL();

            var prof = profBl.Get(userId);

            UserName.Text = usuario.UserName;
            Email.Text = usuario.Email;
            txtDni.Text = prof.NroDocumento.ToString();
            Apellido.Text = prof.Apellido;
            Nombre.Text = prof.Nombre;
            Matricula.Text = prof.Matricula;
            Cuit.Text = prof.Cuit;
            Calle.Text = prof.Calle;
            NroPuerta.Text = prof.NroPuerta.ToString();
            Piso.Text = prof.Piso;
            Depto.Text = prof.Depto;
            Provincia.Text = prof.Provincia;
            Localidad.Text = prof.Localidad;
            Telefono.Text = prof.Telefono;

        }
    }
}