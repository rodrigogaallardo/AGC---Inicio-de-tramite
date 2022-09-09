using BusinesLayer.Implementation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StaticClass;
using ExternalService;
using DataTransferObject;

namespace Azrael.frmFiles
{
    public partial class frmBuscarReplaceFile : Form
    {
        string filePath;
        public frmBuscarReplaceFile()
        {
            InitializeComponent();
        }
        private void Buscar()
        {
            Cursor.Current = Cursors.WaitCursor;
            int IdBuscador = 0;
            int.TryParse(txtID.Text, out IdBuscador);

            if (IdBuscador > 0)
            {
                AzraelBL azBL = new AzraelBL();
                var tramites = azBL.GetTramitesFiles(IdBuscador);
                gvTramites.DataSource = tramites;
            }
            Cursor.Current = Cursors.Default;

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void btnExaminar_Click(object sender, EventArgs e)
        {
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Seleccionar archivo";
            theDialog.Filter = "All files (*.*)|*.*";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = theDialog.FileName;
                lblPath.Text = Path.GetFileName(filePath);
            }
        }

        private void btnReemplazar_Click(object sender, EventArgs e)
        {
            if (gvTramites.SelectedRows.Count > 0 && filePath != null)
            {
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    int selectedrowindex = gvTramites.SelectedCells[0].RowIndex;

                    DataGridViewRow selectedRow = gvTramites.Rows[selectedrowindex];

                    int sTipoTramite = 0;
                    int sIdFile = 0;
                    int sIdDocAdjunto = 0;

                    int.TryParse(selectedRow.Cells["idTipoTramite"].Value.ToString(), out sTipoTramite);
                    int.TryParse(selectedRow.Cells["IdFile"].Value.ToString(), out sIdFile);
                    int.TryParse(selectedRow.Cells["id_docadjunto"].Value.ToString(), out sIdDocAdjunto);

                    ExternalServiceFiles esfiles = new ExternalServiceFiles();

                    byte[] fileBytes = File.ReadAllBytes(filePath);
                    string nombreArchivo = Path.GetFileName(filePath);

                    int newIdFile = esfiles.addFile(nombreArchivo, fileBytes);

                    if (sTipoTramite > 0 && sIdFile > 0 && sIdDocAdjunto > 0 && newIdFile > 0)
                    {
                        switch (sTipoTramite)
                        {
                            case (int)Constantes.AzraelTipoTramite.SSIT:
                                SSITDocumentosAdjuntosBL ssitDocBL = new SSITDocumentosAdjuntosBL();
                                SSITDocumentosAdjuntosDTO ssitDocDTO = ssitDocBL.Single(sIdDocAdjunto);
                                ssitDocDTO.id_file = newIdFile;
                                ssitDocDTO.nombre_archivo = nombreArchivo;
                                ssitDocBL.Update(ssitDocDTO);
                                break;
                            case (int)Constantes.AzraelTipoTramite.Encomienda:
                                EncomiendaDocumentosAdjuntosBL encDocAdjBL = new EncomiendaDocumentosAdjuntosBL();
                                EncomiendaDocumentosAdjuntosDTO encDocAdjDTO = encDocAdjBL.Single(sIdDocAdjunto);
                                encDocAdjDTO.id_file = newIdFile;
                                encDocAdjDTO.nombre_archivo = nombreArchivo;
                                encDocAdjBL.Update(encDocAdjDTO);
                                break;
                            case (int)Constantes.AzraelTipoTramite.CPadron:
                                ConsultaPadronDocumentosAdjuntosBL cpadronDocAdjBL = new ConsultaPadronDocumentosAdjuntosBL();
                                ConsultaPadronDocumentosAdjuntosDTO cpadronDocAdjDTO = cpadronDocAdjBL.Single(sIdDocAdjunto);
                                cpadronDocAdjDTO.IdFile = newIdFile;
                                cpadronDocAdjDTO.NombreArchivo = nombreArchivo;
                                cpadronDocAdjBL.Update(cpadronDocAdjDTO);
                                break;
                            case (int)Constantes.AzraelTipoTramite.Transferencia:
                                TransferenciasDocumentosAdjuntosBL transfDocAdjBL = new TransferenciasDocumentosAdjuntosBL();
                                TransferenciasDocumentosAdjuntosDTO transfDocAdjDTO = transfDocAdjBL.Single(sIdDocAdjunto);
                                transfDocAdjDTO.IdFile = newIdFile;
                                transfDocAdjDTO.NombreArchivo = nombreArchivo;
                                transfDocAdjBL.Update(transfDocAdjDTO);
                                break;
                            case (int)Constantes.AzraelTipoTramite.SGI_HAB:
                                SGITareaDocumentosAdjuntosBL sgiDocBL = new SGITareaDocumentosAdjuntosBL();
                                SGITareaDocumentosAdjuntosDTO sgiDocDTO = sgiDocBL.Single(sIdDocAdjunto);
                                sgiDocDTO.id_file = newIdFile;
                                sgiDocDTO.nombre_archivo = nombreArchivo;
                                sgiDocBL.Update(sgiDocDTO);
                                break;
                        }
                    }
                    Buscar();
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Archivo reemplazado");
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un archivo.");
            }
        }

        private void frmBuscarReplaceFile_Load(object sender, EventArgs e)
        {
            

        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
