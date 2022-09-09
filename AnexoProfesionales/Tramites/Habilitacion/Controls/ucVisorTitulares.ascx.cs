using BusinesLayer.Implementation;
using DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnexoProfesionales.Tramites.Habilitacion.Controls
{
    public partial class VisorTitulares : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void CargarDatos(EncomiendaDTO encDTO)
        {
            List<EncomiendaFirmantesPersonasFisicasDTO> lstFirPF = new List<EncomiendaFirmantesPersonasFisicasDTO>();
            List<EncomiendaFirmantesPersonasJuridicasDTO> lstFirPJ = new List<EncomiendaFirmantesPersonasJuridicasDTO>();
            
            var encTitPFDTO = encDTO.EncomiendaTitularesPersonasFisicasDTO;
            var encTitPJDTO = encDTO.EncomiendaTitularesPersonasJuridicasDTO;

            if (encTitPFDTO.Count() == 0)
                pnlTitularesPF.Style["display"] = "none";
            else
            {
                foreach (var firPF in encTitPFDTO)
                    lstFirPF.AddRange(firPF.EncomiendaFirmantesPersonasFisicasDTO);

                grdTitularesPF.DataSource = encTitPFDTO;
                grdTitularesPF.DataBind();

                grdFirmantesPF.DataSource = lstFirPF;
                grdFirmantesPF.DataBind();
            }

            if (encTitPJDTO.Count() == 0)
                pnlTitularesPJ.Style["display"] = "none";
            else
            {
                grdTitularesPJ.DataSource = encTitPJDTO;
                grdTitularesPJ.DataBind();

                FirmantesBL firmatesBL = new FirmantesBL();
                
                grdFirmantesPJ.DataSource = firmatesBL.GetFirmantesPJEncomienda(encDTO.IdEncomienda);
                grdFirmantesPJ.DataBind();
            }
        }
    }
}