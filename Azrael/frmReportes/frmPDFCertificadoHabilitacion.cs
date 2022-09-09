using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinesLayer.Implementation;
using StaticClass;
using ExternalService;
using ExternalService.Class;
using DataTransferObject;
using System.IO;

namespace Azrael
{
    public partial class frmPDFCertificadoHabilitacion : Form
    {
        bool Cancelar = false;
        bool log = false;
        string path = Application.StartupPath + "\\Log.txt";
        List<SSITSolicitudesDTO> listSoli = new List<SSITSolicitudesDTO>();
        public frmPDFCertificadoHabilitacion()
        {
            InitializeComponent();
        }

        private void Habilitar(bool hab)
        {
            cmdSalir.Enabled = hab;
            cmdLimpiar.Enabled = hab;
            cmdCancelar.Enabled = !hab;
            cmdEjecutar.Enabled = hab;
            txtSolDesde.Enabled = hab;
            txtSolHasta.Enabled = hab;
            gvCertificadoHab.Enabled = hab;
            if (hab)
                Cursor.Current = Cursors.Default;
            else
                Cursor.Current = Cursors.WaitCursor;
        }

        private List<SSITSolicitudesDTO> getListaSoli()
        {
            SSITSolicitudesBL solicitudBL = new SSITSolicitudesBL();
            List<SSITSolicitudesDTO> ret = new List<SSITSolicitudesDTO>();
            if (rbLista.Checked)
            {
                List<int> Lista = new List<int>();
                foreach (DataGridViewRow item in gvCertificadoHab.Rows)
                {
                    if (item.Cells[0].Value == null)
                        continue;

                    int id_sol = 0;
                    int.TryParse(item.Cells[0].Value.ToString(), out id_sol);
                    if (id_sol > 0)
                        Lista.Add(id_sol);
                }
                ret = solicitudBL.GetListaIdSolicitud(Lista).ToList();
            }
            else
            {
                int desde = 0;
                int hasta = 0;
                int.TryParse(txtSolDesde.Text, out desde);
                int.TryParse(txtSolHasta.Text, out hasta);
                ret = solicitudBL.GetRangoIdSolicitud(desde, hasta).ToList();
            }

            if (ret.Count == 0)
                throw new Exception("No se encontraron los Informes de Consulta al Padron indicados.");

            return ret;
        }

        private void cmdEjecutar_Click(object sender, EventArgs e)
        {
            log = false;
            if (string.IsNullOrWhiteSpace(txtSolDesde.Text) && string.IsNullOrWhiteSpace(txtSolHasta.Text) && gvCertificadoHab.Rows.Count == 1)
            {
                MessageBox.Show(this, "No se encontraron las solicitudes indicadas.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                using (TextWriter tw = new StreamWriter(fs))
                {
                    try
                    {
                        Habilitar(false);

                        listSoli = getListaSoli();

                        if (listSoli.Count == 0)
                            throw new Exception("No se encontraron las solicitudes indicadas.");
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

            pbProgreso.Maximum = listSoli.Count;

            pbProgreso.Value = 0;
            pbProgreso.Step = 1;
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker1.RunWorkerAsync();
        }

        private void cmdCancelar_Click(object sender, EventArgs e)
        {
            Cancelar = true;
            backgroundWorker1.CancelAsync();
            pbProgreso.Value = 0;
            Habilitar(true);
        }

        private void cmdLimpiar_Click(object sender, EventArgs e)
        {
            gvCertificadoHab.Rows.Clear();
            txtSolHasta.Text = "";
            txtSolDesde.Text = "";
        }

        private void cmdSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmPDFCertificadoHabilitacion_Load(object sender, EventArgs e)
        {
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            gvCertificadoHab.KeyDown += new KeyEventHandler(gvCertificadoHab_KeyDown);
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

        private void gvCertificadoHab_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                DataObject d = gvCertificadoHab.GetClipboardContent();
                Clipboard.SetDataObject(d);
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.V)
            {
                try
                {
                    string s = Clipboard.GetText();
                    string[] lines = s.Split('\n');
                    int row = gvCertificadoHab.CurrentCell.RowIndex;
                    int col = gvCertificadoHab.CurrentCell.ColumnIndex;
                    if (lines.Length > 0)
                        gvCertificadoHab.Rows.Add(lines.Length - 1);
                    foreach (string line in lines)
                    {
                        if (row < gvCertificadoHab.RowCount && line.Length > 0)
                        {
                            string[] cells = line.Split('\t');
                            for (int i = 0; i < cells.GetLength(0); ++i)
                            {
                                if (col + i < this.gvCertificadoHab.ColumnCount)
                                {
                                    gvCertificadoHab[col + i, row].Value = Convert.ChangeType(cells[i], gvCertificadoHab[col + i, row].ValueType);
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

        private void rbRango_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioB = (RadioButton)sender;
            if (radioB.Checked)
            {
                txtSolDesde.Enabled = true;
                txtSolHasta.Enabled = true;
            }
            else
            {
                txtSolDesde.Enabled = false;
                txtSolHasta.Enabled = false;
            }
        }

        private void rbLista_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioB = (RadioButton)sender;
            if (radioB.Checked)
                gvCertificadoHab.Enabled = true;
            else
                gvCertificadoHab.Enabled = false;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int ContProcesados = 0;

            AzraelBL azBL = new AzraelBL();
            using (FileStream fs = new FileStream(path, FileMode.Append))
            {
                using (TextWriter tw = new StreamWriter(fs))
                {
                    foreach (var item in listSoli)
                    {
                        try
                        {
                            ExternalServiceReporting service = new ExternalServiceReporting();
                            ReportingEntity Report = service.GetPDFCertificadoHabilitacion("CertificadoHabilitacion", item.IdSolicitud, "'" + item.NroExpedienteSade + "'", false);
                            azBL.GuardarPDFCertificadoHabilitacion(item.IdSolicitud, Report.Id_file, Report.FileName, item.CreateUser);
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
