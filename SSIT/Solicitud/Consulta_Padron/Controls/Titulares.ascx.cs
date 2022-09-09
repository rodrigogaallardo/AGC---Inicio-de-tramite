using BusinesLayer.Implementation;
using DataTransferObject;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using SIPSA.Entity;

namespace SSIT.Consulta_Padron.Controls
{
    public partial class Titulares : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void CargarDatos(ConsultaPadronSolicitudesDTO consultaPadron)
        {
            List<TitularesDTO> titularesDTO = new List<TitularesDTO>();
            List<TitularesSHDTO> titularesSHDTO = new List<TitularesSHDTO>();
            foreach (var personaFisica in consultaPadron.TitularesSolicitudPersonasFisicas)
            {
                titularesDTO.Add(new TitularesDTO()
                {
                    id_persona = personaFisica.IdPersonaFisica,
                                    TipoPersona = Constantes.TipoPersonaFisica,
                                    TipoPersonaDesc = Constantes.TipoPersonaFisica_Desc,
                                    ApellidoNomRazon = personaFisica.Apellido + " " + personaFisica.Nombres,
                                    CUIT = personaFisica.CUIT,
                                    Domicilio = personaFisica.Calle + " " + personaFisica.NumeroPuerta.ToString() 
                                                + (!string.IsNullOrWhiteSpace(personaFisica.Torre) ? " Torre: " + personaFisica.Torre : "") 
                                                + (!string.IsNullOrWhiteSpace(personaFisica.Piso) ? " Piso: " + personaFisica.Piso : "") 
                                                + (!string.IsNullOrWhiteSpace(personaFisica.Depto) ? "  Depto/UF: " + personaFisica.Depto : ""),
                                    tipo_doc = personaFisica.IdTipoDocumentoPersonal
                });
            }
            foreach (var personaJuridica in consultaPadron.TitularesPersonasSolicitudJuridicas)
            {
                titularesDTO.Add(new TitularesDTO()
                {

                    id_persona = personaJuridica.IdPersonaJuridica,
                    TipoPersona = Constantes.TipoPersonaJuridica,
                    TipoPersonaDesc = Constantes.TipoPersonaJuridica_Desc,
                    ApellidoNomRazon = personaJuridica.RazonSocial,
                    CUIT = personaJuridica.CUIT,
                    Domicilio = personaJuridica.Calle + " "
                                + (personaJuridica.NumeroPuerta.HasValue ? personaJuridica.NumeroPuerta.Value.ToString() : "")
                                + (!string.IsNullOrWhiteSpace(personaJuridica.Torre) ? " Torre: " + personaJuridica.Torre : "")
                                + (!string.IsNullOrWhiteSpace(personaJuridica.Piso) ? " Piso: " + personaJuridica.Piso : "")
                                + (!string.IsNullOrWhiteSpace(personaJuridica.Depto) ? "  Depto/UF: " + personaJuridica.Depto : "")
                });

      
            }
            grdTitularesHab.DataSource = titularesDTO;
            grdTitularesHab.DataBind();
            if (consultaPadron.TitularesPersonasSolicitudJuridicasTitulares != null)
            {
                foreach (var titularesPJ in consultaPadron.TitularesPersonasSolicitudJuridicasTitulares)
                {
                    titularesSHDTO.Add(new TitularesSHDTO()
                    {

                        IdPersonaJuridica = titularesPJ.IdPersonaJuridica,
                        Apellidos = titularesPJ.Apellido,
                        Nombres = titularesPJ.Nombres,
                        NroDoc = titularesPJ.NumeroDocumento,
                        IdTipoDocPersonal = titularesPJ.IdTipoDocumentoPersonal,
                        Email = titularesPJ.Email
                    });
                    grdTitularesPJtitulares.DataSource = titularesSHDTO;
                    grdTitularesPJtitulares.DataBind();
                }
            }
        }
                        
    }
}