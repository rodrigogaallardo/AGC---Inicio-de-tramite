using BusinesLayer.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataTransferObject;

namespace SSIT.Solicitud.Consulta_Padron.Controls
{
    public partial class Rubros : System.Web.UI.UserControl
    {
        public void CargarDatos(ConsultaPadronSolicitudesDTO consultaPadron)
        {
            CargarNormativa(consultaPadron);
            CargarRubros(consultaPadron);
        }

        private void CargarNormativa(ConsultaPadronSolicitudesDTO consultaPadron)
        {

            lblTipoNormativa.Text = "";
            lblEntidadNormativa.Text = "";
            lblNroNormativa.Text = "";

            var normativa = consultaPadron.Normativa.FirstOrDefault();

            if (normativa != null)
            {
                lblTipoNormativa.Text = normativa.TipoNormativa.Descripcion;
                lblEntidadNormativa.Text = normativa.EntidadNormativa.Descripcion;
                lblNroNormativa.Text = normativa.NumeroNormativa;
                pnlNormativa.Visible = true;
            }
            else
            {
                pnlNormativa.Visible = false;
            }

        }

        private void CargarRubros(ConsultaPadronSolicitudesDTO consultaPadron)
        {

            grdRubrosIngresados.DataSource = consultaPadron.Rubros;
            grdRubrosIngresados.DataBind();
        }

        public bool OcultarZonaSup
        {
            get;
            set;
        }

        protected void grdRubrosIngresados_DataBound(object sender, EventArgs e)
        {
            if (OcultarZonaSup)
            {
                grdRubrosIngresados.Columns[3].Visible = false;
                grdRubrosIngresados.Columns[4].Visible = false;
            } 
        }
    }
}