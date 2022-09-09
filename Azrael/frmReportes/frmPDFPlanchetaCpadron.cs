using DataTransferObject;
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
using BusinesLayer.Implementation;
using StaticClass;
using ExternalService;
using ExternalService.Class;

namespace Azrael
{
    public partial class frmPDFPlanchetaCpadron : Form
    {
        bool Cancelar = false;
        bool log = false;
        string path = Application.StartupPath + "\\Log.txt";
        List<ConsultaPadronSolicitudesDTO> listCpadron = new List<ConsultaPadronSolicitudesDTO>();

        public frmPDFPlanchetaCpadron()
        {
            InitializeComponent();
        }

        private void cmdLimpiar_Click(object sender, EventArgs e)
        {
            gvInformeCP.Rows.Clear();
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

        private void cmdSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmPDFPlanchetaCpadron_Load(object sender, EventArgs e)
        {
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            gvInformeCP.KeyDown += new KeyEventHandler(gvInformeCP_KeyDown);
        }

        private void Habilitar(bool hab)
        {
            cmdSalir.Enabled = hab;
            cmdLimpiar.Enabled = hab;
            cmdCancelar.Enabled = !hab;
            cmdEjecutar.Enabled = hab;
            txtEncDesde.Enabled = hab;
            txtEncHasta.Enabled = hab;
            gvInformeCP.Enabled = hab;
            if (hab)
                Cursor.Current = Cursors.Default;
            else
                Cursor.Current = Cursors.WaitCursor;
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

        private void gvInformeCP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                DataObject d = gvInformeCP.GetClipboardContent();
                Clipboard.SetDataObject(d);
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.V)
            {
                try
                {
                    string s = Clipboard.GetText();
                    string[] lines = s.Split('\n');
                    int row = gvInformeCP.CurrentCell.RowIndex;
                    int col = gvInformeCP.CurrentCell.ColumnIndex;
                    if (lines.Length > 0)
                        gvInformeCP.Rows.Add(lines.Length - 1);
                    foreach (string line in lines)
                    {
                        if (row < gvInformeCP.RowCount && line.Length > 0)
                        {
                            string[] cells = line.Split('\t');
                            for (int i = 0; i < cells.GetLength(0); ++i)
                            {
                                if (col + i < this.gvInformeCP.ColumnCount)
                                {
                                    gvInformeCP[col + i, row].Value = Convert.ChangeType(cells[i], gvInformeCP[col + i, row].ValueType);
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

        private void cmdEjecutar_Click(object sender, EventArgs e)
        {
            log = false;
            if (string.IsNullOrWhiteSpace(txtEncDesde.Text) && string.IsNullOrWhiteSpace(txtEncHasta.Text) && gvInformeCP.Rows.Count == 1)
            {
                MessageBox.Show(this, "No se encontraron los Informes de Consulta al Padron indicados.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                using (TextWriter tw = new StreamWriter(fs))
                {
                    try
                    {
                        Habilitar(false);

                        listCpadron = getListaInformeCpadron();

                        if (listCpadron.Count == 0)
                            throw new Exception("No se encontraron los Informes de Consulta al Padron indicados.");
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

            pbProgreso.Maximum = listCpadron.Count;

            pbProgreso.Value = 0;
            pbProgreso.Step = 1;
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker1.RunWorkerAsync();
        }

        private List<ConsultaPadronSolicitudesDTO> getListaInformeCpadron()
        {
            ConsultaPadronSolicitudesBL informecpBL = new ConsultaPadronSolicitudesBL();
            List<ConsultaPadronSolicitudesDTO> ret = new List<ConsultaPadronSolicitudesDTO>();
            if (rbLista.Checked)
            {
                List<int> Lista = new List<int>();
                foreach (DataGridViewRow item in gvInformeCP.Rows)
                {
                    if (item.Cells[0].Value == null)
                        continue;

                    int id_cp = 0;
                    int.TryParse(item.Cells[0].Value.ToString(), out id_cp);
                    if (id_cp > 0)
                        Lista.Add(id_cp);
                }
                ret = informecpBL.GetListaInformeCpadron(Lista).ToList();
            }
            else
            {
                int desde = 0;
                int hasta = 0;
                int.TryParse(txtEncDesde.Text, out desde);
                int.TryParse(txtEncHasta.Text, out hasta);
                ret = informecpBL.GetRangoIdCpadron(desde, hasta).ToList();
            }

            if (ret.Count == 0)
                throw new Exception("No se encontraron los Informes de Consulta al Padron indicados.");

            return ret;
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
                gvInformeCP.Enabled = true;
            else
                gvInformeCP.Enabled = false;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int ContProcesados = 0;

            AzraelBL azBL = new AzraelBL();
            using (FileStream fs = new FileStream(path, FileMode.Append))
            {
                using (TextWriter tw = new StreamWriter(fs))
                {
                    foreach (var item in listCpadron)
                    {
                        try
                        {
                            ExternalServiceReporting service = new ExternalServiceReporting();
                            ReportingEntity Report = service.GetPDFReportePlancheta("InformeCPadron", item.IdConsultaPadron, 1, true); 
                            azBL.GuardarPDFInformeCpadron(item.IdConsultaPadron, Report.Id_file, Report.FileName, item.CreateUser);
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
