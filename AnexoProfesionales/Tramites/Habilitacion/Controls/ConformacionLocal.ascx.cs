using BusinesLayer.Implementation;
using DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnexoProfesionales.Controls
{

    public partial class ConformacionLocal : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void CargarDatos(int IdEncomienda)
        {
            EncomiendaBL encomiendaBL = new EncomiendaBL();
            CargarDatos(encomiendaBL.Single(IdEncomienda));
        }

        public void CargarDatos(EncomiendaDTO encomienda)
        {
            grdConformacionLocal.DataSource = encomienda.EncomiendaConformacionLocalDTO;
            grdConformacionLocal.DataBind();
            decimal suma = 0;

            suma += encomienda.EncomiendaConformacionLocalDTO.Sum(p => p.superficie_conflocal ?? 0);
            
            txtSupTotal.Text = suma.ToString("N2");
        }
    }
}