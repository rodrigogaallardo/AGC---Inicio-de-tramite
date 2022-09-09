using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinesLayer.Implementation;
using AnexoProfesionales.Common;
using ExternalService;
//using SSIT.Mailer;
using StaticClass;

namespace SSIT.Controls
{
    public partial class ucNuevaPuerta : System.Web.UI.UserControl
    {
        public int IdUbicacion { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCalles();
            }
        }
        private void CargarCalles()
        {
            CallesBL calles = new CallesBL();

            var lstCalles = calles.GetCalles();

            ddlCallesUbic.DataSource = lstCalles.ToList();
            ddlCallesUbic.DataTextField = "NombreOficial_calle";
            ddlCallesUbic.DataValueField = "Codigo_calle";
            ddlCallesUbic.DataBind();
            ddlCallesUbic.Items.Insert(0, "");
        }
        public void CargarDatos()
        {           
            UbicacionesBL ubicacionesBL = new UbicacionesBL();
            var ubicacionDTO = ubicacionesBL.Single(IdUbicacion);
            UbicacionesPuertasBL ubicacionesPuertasBL = new UbicacionesPuertasBL();
            var query = ubicacionesPuertasBL.GetByFKIdUbicacion(IdUbicacion);

            grdSeleccionPuertas_pa.DataSource = query;
            grdSeleccionPuertas_pa.DataBind();
            hid_id_ubicacionPuerta.Value = IdUbicacion.ToString();
            grd_NroPartidaMatriz_pa.Text = ubicacionDTO.NroPartidaMatriz.HasValue ? ubicacionDTO.NroPartidaMatriz.Value.ToString() : "";
            grd_seccion_pa.Text = ubicacionDTO.Seccion.HasValue ? ubicacionDTO.Seccion.Value.ToString() : "";
            grd_manzana_pa.Text = ubicacionDTO.Manzana;
            grd_parcela_pa.Text = ubicacionDTO.Parcela;

            lblMailRtaSolicitudAlta.Text = "*Ud recibirá a su casilla " +  " un mail de confirmación de la solicitud y a la brevedad una respuesta.";
        }

        protected void btnEnviarPedidoAltaUbic_Click(object sender, EventArgs e)
        {
            IdUbicacion = Convert.ToInt32(hid_id_ubicacionPuerta.Value);
            UbicacionesBL ubicacionesBL = new UbicacionesBL();
            var ubicacionDTO = ubicacionesBL.Single(IdUbicacion);
            MembershipUser usuario = Membership.GetUser();
            Guid userid = (Guid)usuario.ProviderUserKey;
            string nroPuerta = txtNroPuerta_pa.Text.Trim();
           
            UsuarioBL usuBL = new UsuarioBL();
            var usuDTO = usuBL.Single(userid);

            string foto = Funciones.GetUrlFoto(ubicacionDTO.Seccion.Value, ubicacionDTO.Manzana, ubicacionDTO.Parcela, 200, 200);

            string direccion = ddlCallesUbic.SelectedItem.ToString() + " " + nroPuerta;
            string mapa = Funciones.GetUrlMapa(ubicacionDTO.Seccion.Value, ubicacionDTO.Manzana, ubicacionDTO.Parcela, direccion);

            MailMessages mailer = new MailMessages();
            string htmlBody = mailer.MailSolicitudNuevaPuerta(usuDTO.UserName, usuDTO.Apellido, usuDTO.Nombre, usuDTO.Email, ubicacionDTO.NroPartidaMatriz.Value.ToString(),
                ubicacionDTO.Seccion.Value.ToString(), ubicacionDTO.Manzana.ToString(), ubicacionDTO.Parcela.ToString(), ddlCallesUbic.SelectedItem.ToString(), nroPuerta, foto, mapa);

            EmailServiceBL mailService = new EmailServiceBL();
            EmailEntity emailEntity = new EmailEntity();
            emailEntity.Email = usuario.Email;
            emailEntity.Html = htmlBody;
            emailEntity.Asunto = "Solicitud de nueva calle en parcela";
            emailEntity.IdEstado = (int)ExternalService.TiposDeEstadosEmail.PendienteDeEnvio;
            emailEntity.IdTipoEmail = (int)ExternalService.TiposDeMail.Generico;
            emailEntity.IdOrigen = (int)ExternalService.MailOrigenes.SSIT;
            emailEntity.CantIntentos = 3;
            emailEntity.CantMaxIntentos = 3;
            emailEntity.FechaAlta = DateTime.Now;
            emailEntity.Prioridad = 1;

            mailService.SendMail(emailEntity);

            emailEntity.Email = "ubicacionesagc@buenosaires.gob.ar";
            emailEntity.Html = htmlBody;
            emailEntity.Asunto = "Solicitud de nueva calle en parcela";
            emailEntity.IdEstado = (int)ExternalService.TiposDeEstadosEmail.PendienteDeEnvio;
            emailEntity.IdTipoEmail = (int)ExternalService.TiposDeMail.Generico;
            emailEntity.IdOrigen = (int)ExternalService.MailOrigenes.SSIT;
            emailEntity.CantIntentos = 3;
            emailEntity.CantMaxIntentos = 3;
            emailEntity.FechaAlta = DateTime.Now;
            emailEntity.Prioridad = 1;

            mailService.SendMail(emailEntity);
        }           
    }
}     