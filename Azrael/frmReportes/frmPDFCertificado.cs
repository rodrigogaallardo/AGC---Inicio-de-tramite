using BusinesLayer.Implementation;
using DataTransferObject;
using Reporting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using StaticClass;
using System.Configuration;
using ExternalService.WSConsejo;
using ExternalService;
using ExternalService.Class;

namespace Azrael
{
    public partial class frmPDFCertificado : Form
    {
        bool log = false;
        string path = Application.StartupPath + "\\Log.txt";
        bool Cancelar = false;
        List<EncomiendaDTO> listEncomiendas = new List<EncomiendaDTO>();
        public frmPDFCertificado()
        {
            InitializeComponent();
        }

        private void gvEncomienda_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }



        private void rbRango_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioB = (RadioButton)sender;
            if (radioB.Checked)
            {
                txtEncDesde.Enabled = true;
                txtEncHasta.Enabled = true;
            }
            else
            {
                txtEncDesde.Enabled = false;
                txtEncHasta.Enabled = false;
            }
        }

        private void rbLista_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioB = (RadioButton)sender;
            if (radioB.Checked)
                gvEncomienda.Enabled = true;
            else
                gvEncomienda.Enabled = false;
        }

        private void cmdSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdLimpiar_Click(object sender, EventArgs e)
        {
            gvEncomienda.Rows.Clear();
            txtEncHasta.Text = "";
            txtEncDesde.Text = "";
        }

        private void cmdCancelar_Click(object sender, EventArgs e)
        {
            Cancelar = true;
            backgroundWorker1.CancelAsync();
            pbProgreso.Value = 0;
            Habilitar(true);
        }

        private void Habilitar(bool hab)
        {
            cmdSalir.Enabled = hab;
            cmdLimpiar.Enabled = hab;
            cmdCancelar.Enabled = !hab;
            cmdEjecutar.Enabled = hab;
            txtEncDesde.Enabled = hab;
            txtEncHasta.Enabled = hab;
            gvEncomienda.Enabled = hab;
            if (hab)
                Cursor.Current = Cursors.Default;
            else
                Cursor.Current = Cursors.WaitCursor;
        }

        private void cmdEjecutar_Click(object sender, EventArgs e)
        {
            log = false;
            if (string.IsNullOrWhiteSpace(txtEncDesde.Text) && string.IsNullOrWhiteSpace(txtEncHasta.Text) && gvEncomienda.Rows.Count == 1)
            {
                MessageBox.Show(this, "No se encontraron los Anexos Técnicos indicados.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                using (TextWriter tw = new StreamWriter(fs))
                {
                    try
                    {
                        Habilitar(false);

                        listEncomiendas = getListaEncomienda();

                        if (listEncomiendas.Count == 0)
                            throw new Exception("No se encontraron los Anexos Técnicos indicados.");
                    }
                    catch (Exception ex)
                    {
                        string msg;
                        if (ex.InnerException == null)
                            msg = ex.Message;
                        else
                            msg = ex.InnerException.Message;

                        tw.WriteLine(Funciones.FechaString() + msg);
                        MessageBox.Show(this, msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Habilitar(true);
                        return;
                    }
                }
            }

            pbProgreso.Maximum = listEncomiendas.Count;

            pbProgreso.Value = 0;
            pbProgreso.Step = 1;
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker1.RunWorkerAsync();
        }

        private List<EncomiendaDTO> getListaEncomienda()
        {
            EncomiendaBL encBL = new EncomiendaBL();
            List<EncomiendaDTO> ret = new List<EncomiendaDTO>();
            if (rbLista.Checked)
            {
                List<int> Lista = new List<int>();
                foreach (DataGridViewRow item in gvEncomienda.Rows)
                {
                    if (item.Cells[0].Value == null)
                        continue;

                    int id_enc = 0;
                    int.TryParse(item.Cells[0].Value.ToString(), out id_enc);
                    if (id_enc > 0)
                        Lista.Add(id_enc);
                }
                ret = encBL.GetListaIdEncomienda(Lista).ToList();
            }
            else
            {
                int desde = 0;
                int hasta = 0;
                int.TryParse(txtEncDesde.Text, out desde);
                int.TryParse(txtEncHasta.Text, out hasta);
                ret = encBL.GetRangoIdEncomienda(desde, hasta).ToList();
            }

            ret = ret.Where(x => x.IdEstado == (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo).ToList();
            
            if (ret.Count == 0)
                throw new Exception("No se encontraron los Anexos Técnicos indicados.");

            return ret;
        }

        private void gvEncomienda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                DataObject d = gvEncomienda.GetClipboardContent();
                Clipboard.SetDataObject(d);
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.V)
            {
                try
                {
                    string s = Clipboard.GetText();
                    string[] lines = s.Split('\n');
                    int row = gvEncomienda.CurrentCell.RowIndex;
                    int col = gvEncomienda.CurrentCell.ColumnIndex;
                    if (lines.Length > 0)
                        gvEncomienda.Rows.Add(lines.Length - 1);
                    foreach (string line in lines)
                    {
                        if (row < gvEncomienda.RowCount && line.Length > 0)
                        {
                            string[] cells = line.Split('\t');
                            for (int i = 0; i < cells.GetLength(0); ++i)
                            {
                                if (col + i < this.gvEncomienda.ColumnCount)
                                {
                                    gvEncomienda[col + i, row].Value = Convert.ChangeType(cells[i], gvEncomienda[col + i, row].ValueType);
                                }
                                else
                                {
                                    break;
                                }
                            }
                            row++;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                catch
                {

                }
            }
        }

        private void PDFCertificado_Load(object sender, EventArgs e)
        {
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            gvEncomienda.KeyDown += new KeyEventHandler(gvEncomienda_KeyDown);
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbProgreso.PerformStep();
            return;
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (Cancelar == true)
                MessageBox.Show(this, "Proceso Cancelado", "Proceso Completo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (log)
                    MessageBox.Show(this, "Proceso Terminado con Errores", "Proceso Completo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show(this, "Proceso Terminado", "Proceso Completo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            Habilitar(true);
            Cancelar = false;
            backgroundWorker1.Dispose();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int ContProcesados = 0;

            AzraelBL azBL = new AzraelBL();
            using (FileStream fs = new FileStream(path, FileMode.Append))
            {
                using (TextWriter tw = new StreamWriter(fs))
                {
                    foreach (var item in listEncomiendas)
                    {
                        try
                        {
                            ExternalServiceReporting service = new ExternalServiceReporting();
                            ReportingEntity Report = service.GetPDFCertificadoConsejoEncomienda(item.IdEncomienda, true);
                            azBL.GuardarPDFCertificado(item.IdEncomienda, Report.Id_file, Report.FileName, item.CreateUser);
                            ContProcesados += 1;
                            backgroundWorker1.ReportProgress(ContProcesados);
                            if (backgroundWorker1.CancellationPending)
                                return;
                        }
                        catch (Exception ex)
                        {
                            if (ex.InnerException == null)
                                tw.WriteLine(Funciones.FechaString() + ex.Message);
                            else
                                tw.WriteLine(Funciones.FechaString() + ex.InnerException.Message);
                            log = true;
                            continue;
                        }
                    }
                }
            }
        }
    }
}
