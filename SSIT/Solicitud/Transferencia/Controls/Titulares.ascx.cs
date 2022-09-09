using BusinesLayer.Implementation;
using DataTransferObject;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSIT.Transferencia.Controls
{
    public partial class Titulares : System.Web.UI.UserControl
    {
        public void CargarDatos(TransferenciasSolicitudesDTO transferencia)
        {
            List<TitularesDTO> titulares = new List<TitularesDTO>();
            List<FirmantesDTO> firmantes = new List<FirmantesDTO>();

            foreach (var personaFisica in transferencia.TitularesPersonasFisicas)
            {
                titulares.Add(
                    new TitularesDTO()
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
                        tipo_doc = personaFisica.IdTipodocPersonal
                    });

                foreach (var personaFisicaFirmante in personaFisica.Firmantes)
                {
                    firmantes.Add(
                            new FirmantesDTO()
                            {
                                TipoPersona = Constantes.TipoPersonaFisica,
                                id_firmante = personaFisicaFirmante.id_firmante_pf,
                                Titular = personaFisicaFirmante.Apellido + ", " + personaFisicaFirmante.Nombres,
                                ApellidoNombres = personaFisica.Apellido + " " + personaFisica.Nombres,
                                DescTipoDocPersonal = personaFisicaFirmante.TipoDocumentoPersonal.Nombre,
                                Nro_Documento = personaFisicaFirmante.Nro_Documento,
                                nom_tipocaracter = personaFisicaFirmante.TipoCaracterLegal.NomTipoCaracter,
                                cargo_firmante_pj = string.Empty
                            }
                    );
                }
            }

            if (transferencia.TitularesPersonasJuridicas != null)
            {
                foreach (var personaJuridica in transferencia.TitularesPersonasJuridicas)
                {
                    titulares.Add(
                         new TitularesDTO()
                         {
                             id_persona = personaJuridica.IdPersonaJuridica,
                             TipoPersona = Constantes.TipoPersonaJuridica,
                             TipoPersonaDesc = Constantes.TipoPersonaJuridica_Desc,
                             ApellidoNomRazon = personaJuridica.RazonSocial,
                             CUIT = personaJuridica.Cuit,
                             Domicilio = personaJuridica.Calle + " " + (personaJuridica.NumeroPuerta.HasValue ? personaJuridica.NumeroPuerta.Value.ToString() : "") +
                              (!string.IsNullOrWhiteSpace(personaJuridica.Torre) ? " Torre: " + personaJuridica.Torre : "") +
                             (personaJuridica.Piso.Length > 0 ? " Piso: " + personaJuridica.Piso : "") +
                             (personaJuridica.Depto.Length > 0 ? "  Depto/UF: " + personaJuridica.Depto : "")

                         });

                    foreach (var personaJuridicaFirmante in personaJuridica.Firmantes)
                    {
                        firmantes.Add(
                                new FirmantesDTO()
                                {
                                    TipoPersona = Constantes.TipoPersonaJuridica,
                                    id_firmante = personaJuridicaFirmante.id_firmante_pj,
                                    Titular = personaJuridica.RazonSocial,
                                    ApellidoNombres = personaJuridicaFirmante.Apellido + " " + personaJuridicaFirmante.Nombres,
                                    DescTipoDocPersonal = personaJuridicaFirmante.TipoDocumentoPersonal.Nombre,
                                    Nro_Documento = personaJuridicaFirmante.Nro_Documento,
                                    nom_tipocaracter = personaJuridicaFirmante.TipoCaracterLegal.NomTipoCaracter,
                                    cargo_firmante_pj = personaJuridicaFirmante.cargo_firmante_pj
                                });
                    }
                }
            }

            grdTitularesHab.DataSource = titulares;
            grdTitularesHab.DataBind();

            grdTitularesTra.DataSource = firmantes;
            grdTitularesTra.DataBind();
        }
    }
}