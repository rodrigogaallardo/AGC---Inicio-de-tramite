using BusinesLayer.Implementation;
using DataTransferObject;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSIT.Solicitud.Controls
{
    public partial class Titulares : System.Web.UI.UserControl
    {
        public void CargarDatos(ConsultaPadronSolicitudesDTO consultaPadron)
        {
            List<TitularesDTO> titularesDTO = new List<TitularesDTO>();
            foreach (var personaFisica in consultaPadron.TitularesPersonasFisicas)
            {
                titularesDTO.Add(new TitularesDTO()
                {
                    id_persona = personaFisica.IdPersonaFisica,
                    TipoPersona = Constantes.TipoPersonaFisica,
                    TipoPersonaDesc = Constantes.TipoPersonaFisica_Desc,
                    ApellidoNomRazon = personaFisica.Apellido + " " + personaFisica.Nombres,
                    CUIT = personaFisica.Cuit,
                    Domicilio = personaFisica.Calle + " " + personaFisica.NumeroPuerta.ToString() +
                                (!string.IsNullOrWhiteSpace(personaFisica.Torre) ? " Torre: " + personaFisica.Torre : "") +
                                (personaFisica.Piso.Length > 0 ? " Piso: " + personaFisica.Piso : "") +
                                (personaFisica.Depto.Length > 0 ? "  Depto/UF: " + personaFisica.Depto : ""),
                    tipo_doc = personaFisica.IdTipoDocumentoPersonal
                });

            }
            foreach (var personaJuridica in consultaPadron.TitularesPersonasJuridicas)
            {
                titularesDTO.Add(new TitularesDTO()
                {
                    id_persona = personaJuridica.IdPersonaJuridica,
                    TipoPersona = Constantes.TipoPersonaJuridica,
                    TipoPersonaDesc = Constantes.TipoPersonaJuridica_Desc,
                    ApellidoNomRazon = personaJuridica.RazonSocial,
                    CUIT = personaJuridica.CUIT,
                    Domicilio = personaJuridica.Calle + " " + (personaJuridica.NumeroPuerta.HasValue ? personaJuridica.NumeroPuerta.Value.ToString() : "") +
                              (!string.IsNullOrWhiteSpace(personaJuridica.Torre) ? " Torre: " + personaJuridica.Torre : "") +
                               (personaJuridica.Piso.Length > 0 ? " Piso: " + personaJuridica.Piso : "") +
                               (personaJuridica.Depto.Length > 0 ? "  Depto/UF: " + personaJuridica.Depto : "")

                });
            }


            grdTitularesHab.DataSource = titularesDTO;
            grdTitularesHab.DataBind();
        }
    }
}