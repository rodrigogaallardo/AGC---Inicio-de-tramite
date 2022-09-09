using BusinesLayer.Implementation;
using SSIT.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSIT.Mailer
{
    public partial class MailSolicitudNuevaPuerta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public StaticClass.MailSolicitudNuevaPuerta GetData()
        {

            object puserid = Request.QueryString["userid"];
            object pid_ubicacion = Request.QueryString["id_ubicacion"];

            if (puserid != null && pid_ubicacion != null)
            {
                Guid userid = Guid.Parse(puserid.ToString());
                int id_ubicacion = Convert.ToInt32(pid_ubicacion);

                UsuarioBL usuBL = new UsuarioBL();
                UbicacionesBL ubicBL = new UbicacionesBL();
                UbicacionesPuertasBL ubicPuBL = new UbicacionesPuertasBL();

                var datos_usuario = usuBL.Single(userid);
                var datos_ubicacion = ubicBL.Single(id_ubicacion);

                StaticClass.MailSolicitudNuevaPuerta result = new StaticClass.MailSolicitudNuevaPuerta();

                if (datos_usuario != null)
                {
                    result.Username = datos_usuario.UserName;
                    result.Email = datos_usuario.Email;
                    result.Apellido =  datos_usuario.Apellido;
                    result.Nombre = datos_usuario.Nombre;
                }
                if (datos_ubicacion != null)
                {
                    result.NroPartidaMatriz = datos_ubicacion.NroPartidaMatriz;
                    result.Seccion = datos_ubicacion.Seccion;
                    result.Manzana = datos_ubicacion.Manzana;
                    result.Parcela = datos_ubicacion.Parcela;
               

                    var puerta = ubicPuBL.GetByFKIdUbicacion(id_ubicacion).FirstOrDefault();
                    result.NroPuerta = puerta.NroPuertaUbic;
                    result.Calle = puerta.Nombre_calle;
                    if (puerta != null)
                    {
                        string direccion = string.Format("{0} {1}", puerta.Nombre_calle, puerta.NroPuertaUbic);
                        result.UrlMapa = Functions.GetUrlMapa(result.Seccion.Value, result.Manzana, result.Parcela, direccion);
                    }

                    result.urlFoto = Functions.GetUrlFoto(result.Seccion.Value, result.Manzana, result.Parcela, 350, 250);
                }

                return result;
            }
            else
            {
                return null;
            }
        }
    }
}