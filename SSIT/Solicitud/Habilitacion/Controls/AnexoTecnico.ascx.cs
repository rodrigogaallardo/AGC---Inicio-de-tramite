using BusinesLayer.Implementation;
using DataTransferObject;
using SSIT.Common;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace SSIT.Solicitud.Habilitacion.Controls
{
    public partial class AnexoTecnico : System.Web.UI.UserControl
    {
        private int id_solicitudRef
        {
            get
            {
                int ret = 0;
                int.TryParse(Convert.ToString(Page.RouteData.Values["id_solicitudRef"]), out ret);
                return ret;
            }
            set
            {
                hid_id_solicitudRef.Value = value.ToString();
            }

        }
        private int id_solicitud { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void CargarDatos(int id_solicitud)
        {
            int nroSolReferencia = 0;
            int.TryParse(ConfigurationManager.AppSettings["NroSolicitudReferencia"], out nroSolReferencia);
            id_solicitudRef = nroSolReferencia;

            EncomiendaBL encomiendaBL = new EncomiendaBL();

            var elements = encomiendaBL.GetByFKIdSolicitud(id_solicitud).OrderBy(x => x.IdEncomienda).ToList();

            CargarDatos(elements); 
        }
        public void CargarDatos(IEnumerable<EncomiendaDTO> encomiendas)
        {
            this.id_solicitud = id_solicitud;            

            var elements = encomiendas.OrderBy(x => x.IdEncomienda);

            gridEncomienda_db.DataSource = elements;
            gridEncomienda_db.DataBind();
            updEncomienda.Update();
        }

        protected void btnConfirmarAnularAnexo_Click(object sender, EventArgs e)
        {
            int id_encomienda = 0;
            int.TryParse(hid_encomienda_anular1.Value, out id_encomienda);
            int.TryParse(hid_encomienda_anular2.Value, out id_encomienda);
            Guid userid = (Guid)Membership.GetUser().ProviderUserKey;

            EncomiendaBL encBL = new EncomiendaBL();
            EncomiendaDTO encDTO = new EncomiendaDTO();
            encDTO = encBL.Single(id_encomienda);
            encDTO.IdEstado = (int)Constantes.Encomienda_Estados.Anulada;
            encDTO.LastUpdateUser = userid;
            encBL.Update(encDTO);
            encBL.MailAnularAnexoTecnico(id_encomienda);
            CargarDatos(encDTO.IdSolicitud);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScriptOcultarAnularencomienda", "hidefrmConfirmarAnularEncomienda()", true);
        }

        protected void lnkAnular_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;

            Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "script", "showfrmConfirmarAnularAnexo(" + btn.CommandArgument.ToString() + ");", true);
        }

        protected void gridEncomienda_db_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Rubros ucRubros = (Rubros)e.Row.FindControl("Rubros");
                RubrosCN ucRubrosCN = (RubrosCN)e.Row.FindControl("RubrosCN");
                var encomiendaDTO = (EncomiendaDTO)e.Row.DataItem;
                List<int> lstEstados = new List<int>();
                lstEstados.Add((int)Constantes.Encomienda_Estados.Incompleta);
                lstEstados.Add((int)Constantes.Encomienda_Estados.Completa);

                var valor = Convert.ToInt32(ConfigurationManager.AppSettings["NroSolicitudReferencia"]);
                var id_sol = encomiendaDTO.IdSolicitud; //EncomiendaSSITSolicitudesDTO.Select(x => x.id_solicitud).FirstOrDefault()

                if (id_sol > valor || encomiendaDTO.IdTipoTramite == (int)Constantes.TipoTramite.TRANSFERENCIA)
                {
                    ucRubros.Visible = false;
                    ucRubrosCN.CargarDatos(encomiendaDTO);
                }
                else
                {
                    ucRubrosCN.Visible = false;
                    ucRubros.CargarDatos(encomiendaDTO);
                }

                if (!lstEstados.Contains(((EncomiendaDTO)(e.Row.DataItem)).IdEstado))
                {
                    LinkButton lnkAnular = (LinkButton)e.Row.FindControl("lnkAnular");
                    lnkAnular.Visible = false;
                }
                lstEstados.Add((int)Constantes.Encomienda_Estados.Anulada);
                HyperLink lnkImprimir = (HyperLink)e.Row.FindControl("lnkImprimirAnexo");
                if (lstEstados.Contains(((EncomiendaDTO)(e.Row.DataItem)).IdEstado))
                {
                    lnkImprimir.Visible = false;
                }
                else
                {

                    if (encomiendaDTO.EncomiendaDocumentosAdjuntosDTO.Any())
                    {
                        var IdFile = encomiendaDTO.EncomiendaDocumentosAdjuntosDTO.Where(x => x.id_tipodocsis == (int)Constantes.TiposDeDocumentosSistema.ENCOMIENDA_DIGITAL).Select(x => x.id_file).FirstOrDefault();

                        if (IdFile != 0)
                            lnkImprimir.NavigateUrl = string.Format("~/" + RouteConfig.DESCARGA_FILE + "{0}", Functions.ConvertToBase64String(IdFile));
                        else
                            lnkImprimir.Visible = false;

                        int IdEncomienda = ((EncomiendaDTO)(e.Row.DataItem)).IdEncomienda;
                        int IdTipoTramite = ((EncomiendaDTO)(e.Row.DataItem)).IdTipoTramite;

                        var encdocAdjDTO = encomiendaDTO.EncomiendaDocumentosAdjuntosDTO.Where(p => p.id_tipodocsis == (int)Constantes.TipoTramiteCertificados.Certif_consejo_habilitacion).FirstOrDefault();

                        HyperLink lnkImprimirCertificado = (HyperLink)e.Row.FindControl("lnkImprimirCertificado");
                        if (encdocAdjDTO != null)
                            lnkImprimirCertificado.NavigateUrl = string.Format("~/" + RouteConfig.DESCARGA_FILE + "{0}", Functions.ConvertToBase64String(encdocAdjDTO.id_file));
                        else
                            lnkImprimirCertificado.Visible = false;

                    }
                }
            }
        }

        protected void gridEncomienda_db_DataBound(object sender, EventArgs e)
        {
            GridView grid = (GridView)gridEncomienda_db;
            GridViewRow fila = (GridViewRow)grid.BottomPagerRow;
        }

    }
}