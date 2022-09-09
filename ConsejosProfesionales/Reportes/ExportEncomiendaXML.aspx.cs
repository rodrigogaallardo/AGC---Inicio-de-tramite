using DataTransferObject;
using System;
using System.Data;
using System.Web.Security;
using BusinesLayer.Implementation;
using StaticClass;
using System.Linq;
using Reporting;

namespace ConsejosProfesionales.Reportes
{
    public partial class ExportEncomiendaXML : SecurePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            int tipo_certificado = 0;
            int id_encomienda = 0;
            int.TryParse(Request.QueryString["id"].ToString(), out id_encomienda);
            int.TryParse(Request.QueryString["tipo_certificado"].ToString(), out tipo_certificado);

            if (!IsPostBack)
            {
                Guid userid = (Guid)Membership.GetUser().ProviderUserKey;
                EncomiendaBL encomiendaBL = new EncomiendaBL();
                EncomiendaDTO dsEncomienda = null;
                
                if (tipo_certificado == 1)
                    dsEncomienda = encomiendaBL.Single(id_encomienda);
                else
                    dsEncomienda = encomiendaBL.GetEncomiendaExterna(id_encomienda);

                GrupoConsejosBL grupoConsejoBL = new GrupoConsejosBL();
                var dsGrupoConsejo = grupoConsejoBL.Get(userid);
                if (dsEncomienda.ProfesionalDTO.IdConsejo == dsGrupoConsejo.FirstOrDefault().Id)
                {
                    string nombre_tipo_tramite = Enum.GetName(typeof(Constantes.TipoCertificado), tipo_certificado);
                    int NroEncomiendaConsejo = dsEncomienda.NroEncomiendaConsejo;

                    string strfilename = string.Format("{2}_NroTramite-{0}.NroEncomiendaConsejo-{1}.xml", id_encomienda, NroEncomiendaConsejo, nombre_tipo_tramite);

                    DataSet dsRep = null;
                    if (tipo_certificado == 1)
                        dsRep = CommonReport.CargarDatosImpresionEncomienda(id_encomienda, null, false);
                    else
                        dsRep = CommonReport.CargarDatosImpresionEncomiendaExt(id_encomienda);

                    dsRep.WriteXml(Response.OutputStream, XmlWriteMode.WriteSchema);
                    Response.ContentType = "text/xml";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + strfilename);
                    Response.End();
                }
                else
                {
                    Response.Write("El usuario logueado no posee permisos para ver el Trámite Nº " + id_encomienda.ToString());
                    Response.End();
                }
            }
        }
    }
}