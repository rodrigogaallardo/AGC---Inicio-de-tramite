using BusinesLayer.Implementation;
using DataTransferObject;
using StaticClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text.exceptions;
using iTextSharp.text.pdf;

namespace SSIT.Solicitud.Controls
{
    public class ucCargaDocumentosEventsArgs : EventArgs
    {
        public int id_tdocreq { get; set; }
        public string detalle_tdocreq { get; set; }
        public string nombre_archivo { get; set; }
        public byte[] Documento { get; set; }
    }

    public class CargaDocumentoErrorEventArgs : EventArgs
    {
        public int Code { get; set; }
        public string Description { get; set; }
    }

    public partial class ucCargaDocumentos : System.Web.UI.UserControl
    {

        public delegate void EventHandlerErrorCargaDocumento(object sender, CargaDocumentoErrorEventArgs e);
        public event EventHandlerErrorCargaDocumento ErrorCargaDocumento;

        public delegate void EventHandlerSubirDocumento(object sender, ucCargaDocumentosEventsArgs e);
        public event EventHandlerSubirDocumento SubirDocumentoClick;


        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager sm = ScriptManager.GetCurrent(this.Page);

            if (sm.IsInAsyncPostBack)
            {
                
            }

            ScriptManager.RegisterStartupScript(pnlucCargaDocumento, pnlucCargaDocumento.GetType(), this.ClientID + "_NameSpace.init_Js_ucCargaDocumento", this.ClientID + "_NameSpace.init_Js_ucCargaDocumento();", true);
        }

        public void CargarCombo(List<TiposDeDocumentosRequeridosDTO> lstTiposDocumentos)
        {
            ddlTiposDeDocumentosRequeridos.DataSource = lstTiposDocumentos;
            ddlTiposDeDocumentosRequeridos.DataTextField = "Descripcion_compuesta";
            ddlTiposDeDocumentosRequeridos.DataValueField = "id_tdocreq";
            ddlTiposDeDocumentosRequeridos.DataBind();
            ddlTiposDeDocumentosRequeridos.Items.Insert(0, "");

            if (ddlTiposDeDocumentosRequeridos.Items.Count == 2)    // Se cuenta el vacío y un elemento. Si esto ocurre se selecciona el elemento
            {
                ddlTiposDeDocumentosRequeridos.SelectedIndex = 1;
                ddlTiposDeDocumentosRequeridos_SelectedIndexChanged(ddlTiposDeDocumentosRequeridos, new EventArgs());
            }

        }

        protected virtual void OnSubirDocumentoClick(EventArgs e)
        {
            if (SubirDocumentoClick != null)
            {
                //Guid userid = (Guid)Membership.GetUser().ProviderUserKey;

                pnlErrorUcCD.Style["display"] = "none";
                string savedFileName = "C:\\Temporal\\" + hid_filename_documentoUcCD.Value;

                //Elimina las fotos de firmas con mas de 1 dÃ­a para mantener el directorio limpio.
                string[] lstArchs = Directory.GetFiles("C:\\Temporal");
                foreach (string arch in lstArchs)
                {
                    DateTime fechaCreacion = File.GetCreationTime(arch);
                    if (fechaCreacion < DateTime.Now.AddDays(-3))
                        File.Delete(arch);
                }
                lblErrorUcCD.Text = "";
                byte[] Documento = new byte[0];

                TiposDeDocumentosRequeridosBL tdrBL = new TiposDeDocumentosRequeridosBL();
                int id_tdocreq = int.Parse(ddlTiposDeDocumentosRequeridos.SelectedValue);
                var tdrDTO = tdrBL.Single(id_tdocreq);

                if (hid_filename_documentoUcCD.Value.Length > 0)
                {
                    Documento = File.ReadAllBytes(savedFileName);
                    if (Documento.Length > Convert.ToInt32(hid_size_max.Value))
                        throw new Exception(string.Format(Errors.FILE_TAMANIO_INCORRECTO, hid_size_max_MB));
                    if (tdrDTO.formato_archivo.ToLower() == Constantes.EXTENSION_PDF && !IsPdfFullPermissions(savedFileName))
                            throw new Exception(Errors.FILE_DOCUMENTO_PROTEGIDO);                    
                }                

                if (!Path.GetExtension(hid_filename_documentoUcCD.Value.ToLower()).Contains(tdrDTO.formato_archivo))
                    throw new Exception(Errors.FILE_FORMATO_INCORRECTO);

                if (tdrDTO.formato_archivo.ToLower() == Constantes.EXTENSION_PDF)
                {
                    bool isFirmando = Funciones.isFirmadoPdf(Documento);
                    if (tdrDTO.verificar_firma_digital && !isFirmando)
                        throw new Exception(Errors.FILE_TIPO_DOCUMENTO_SIN_FIRMA);
                    else if (!tdrDTO.verificar_firma_digital && isFirmando)
                        throw new Exception(Errors.FILE_TIPO_DOCUMENTO_CON_FIRMA);
                }

                //Chequeo los archivos JPEG 
                if (tdrDTO.formato_archivo.ToLower() == Constantes.EXTENSION_JPG)
                {
                    bool isJpeg;
                    using (System.Drawing.Image test = System.Drawing.Image.FromFile(savedFileName))
                    {
                        isJpeg = (test.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg));
                    }
                    if (!isJpeg)
                        throw new Exception(Errors.FILE_FORMATO_INCORRECTO);
                    File.Delete(savedFileName);
                }

                if (lblErrorUcCD.Text == "")
                {
                    ucCargaDocumentosEventsArgs args = new ucCargaDocumentosEventsArgs();
                    args.id_tdocreq = id_tdocreq;
                    args.detalle_tdocreq = txtDetalle.Text;
                    args.nombre_archivo = hid_filename_documentoUcCD.Value;
                    args.Documento = Documento;

                    SubirDocumentoClick(this, args);
                }
            }
        }
        protected void btnSubirDocumentoUcCD_Click(object sender, EventArgs e)
        {
            try
            {
                OnSubirDocumentoClick(e);
            }
            catch (Exception ex)
            {
                if (ErrorCargaDocumento != null)
                {
                    CargaDocumentoErrorEventArgs args = new CargaDocumentoErrorEventArgs();
                    args.Description = ex.Message;
                    ErrorCargaDocumento(this, args);
                }
            }
        }

        protected void ddlTiposDeDocumentosRequeridos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id_tdocreq = 0;
            if (int.TryParse(ddlTiposDeDocumentosRequeridos.SelectedValue, out id_tdocreq))
            {
                TiposDeDocumentosRequeridosBL blTipos = new TiposDeDocumentosRequeridosBL();
                var TipoDoc = blTipos.Single(id_tdocreq);
                if (TipoDoc.RequiereDetalle)
                    ScriptManager.RegisterStartupScript(updTiposDeDocumentosRequeridosCD, updTiposDeDocumentosRequeridosCD.GetType(), this.ClientID + "_NameSpace.showDetalleDocumentoCD", this.ClientID + "_NameSpace.showDetalleDocumentoCD();", true);
                else
                    ScriptManager.RegisterStartupScript(updTiposDeDocumentosRequeridosCD, updTiposDeDocumentosRequeridosCD.GetType(), this.ClientID + "_NameSpace.hideDetalleDocumentoCD", this.ClientID + "_NameSpace.hideDetalleDocumentoCD();", true);

                hid_requiere_detalle.Value = TipoDoc.RequiereDetalle.ToString();
                hid_formato_archivo.Value = TipoDoc.formato_archivo;
                hid_size_max.Value = Convert.ToString(TipoDoc.tamanio_maximo_mb * 1024 * 1024);
                hid_size_max_MB.Value = Convert.ToString(TipoDoc.tamanio_maximo_mb);
            }
            else
            {
                ScriptManager.RegisterStartupScript(updTiposDeDocumentosRequeridosCD, updTiposDeDocumentosRequeridosCD.GetType(), this.ClientID + "_NameSpace.hideDetalleDocumentoCD", this.ClientID + "_NameSpace.hideDetalleDocumentoCD();", true);
                hid_requiere_detalle.Value = "False";
                hid_formato_archivo.Value = "pdf";
                hid_size_max.Value = "2097152";
                hid_size_max_MB.Value = "2";
            }
        }

        private bool IsPdfFullPermissions(string documento)
        {
            bool ret = true;
            try
            {
                PdfReader pdfReader = new PdfReader(documento);
                pdfReader.Dispose();

                if (!pdfReader.IsOpenedWithFullPermissions)
                    ret = false;


            }
            catch (BadPasswordException)
            {
                ret = false;
            }

            return ret;

        }

    }
}