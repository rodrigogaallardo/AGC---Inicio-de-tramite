using BusinesLayer.Implementation;
using DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using SIPSA.Entity;

namespace AnexoProfesionales.Controls
{
    public partial class CertificadoSobreCarga : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void CargarDatos(EncomiendaDTO encomienda)
        {
            cargarCombos(encomienda.IdEncomienda);

            EncomiendaDatosLocalDTO enDl = encomienda.EncomiendaDatosLocalDTO.FirstOrDefault();
            if (enDl != null)
            {
                EncomiendaCertificadoSobrecargaDTO dto = enDl.EncomiendaCertificadoSobrecargaDTO.FirstOrDefault();
                if (dto != null)
                {
                    ddlTiposCertificado.SelectedValue = dto.id_tipo_certificado.ToString();
                    ddlTiposSobrecargas.SelectedValue = dto.id_tipo_sobrecarga.ToString();

                    CargarSobrecargas(dto.id_sobrecarga);
                }
            }
        }

        private void cargarCombos(int id_encomienda)
        {
            EncomiendaTiposCertificadosSobrecargaBL blTiposCert = new EncomiendaTiposCertificadosSobrecargaBL();
            EncomiendaTiposSobrecargasBL blTiposSob = new EncomiendaTiposSobrecargasBL();

            ddlTiposCertificado.DataSource = blTiposCert.GetAll();
            ddlTiposCertificado.DataTextField = "descripcion";
            ddlTiposCertificado.DataValueField = "id_tipo_certificado";
            ddlTiposCertificado.DataBind();

            ddlTiposSobrecargas.DataSource = blTiposSob.GetAll();
            ddlTiposSobrecargas.DataTextField = "descripcion";
            ddlTiposSobrecargas.DataValueField = "id_tipo_sobrecarga";
            ddlTiposSobrecargas.DataBind();
            ddlTiposSobrecargas.Items.Insert(0, new ListItem("", "0"));

        }

        private void CargarSobrecargas(int id_sobrecarga)
        {
            SobrecargasEntityBL blSob = new SobrecargasEntityBL();
            grdSobrecargas.DataSource = blSob.getSobrecargaDetallado(id_sobrecarga);
            grdSobrecargas.DataBind();
        }

        protected string tipoString()
        {
            if (ddlTiposSobrecargas.SelectedIndex == 1)
            {
                return "Conforme a Art 8.1.3 CE:";
            }
            else
            {
                return "Conforme a CIRSOC 4.1:";
            }
        }

    }
}