﻿using System;
using System.Collections.Generic;
using DataTransferObject;
using System.Windows.Forms;
using System.IO;
using BusinesLayer.Implementation;
using StaticClass;
using System.Linq;
using ExternalService;
using ExternalService.Class;
using System.ComponentModel;

namespace Azrael.frmReportes
{
    public partial class frmManifiestoTransmision : Form
    {
        bool Cancelar = false;
        bool log = false;
        string path = Application.StartupPath + "\\Log.txt";

        List<TransferenciasSolicitudesDTO> listSolicitudes = new List<TransferenciasSolicitudesDTO>();

        public frmManifiestoTransmision()
        {
            InitializeComponent();
        }

        private void cmdEjecutar_Click(object sender, EventArgs e)
        {
            log = false;
            if (string.IsNullOrWhiteSpace(txtSolDesde.Text) && string.IsNullOrWhiteSpace(txtSolHasta.Text) && gvTramites.Rows.Count == 1)
            {
                MessageBox.Show(this, "No se encontraron las Solicitudes indicadas.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                using (TextWriter tw = new StreamWriter(fs))
                {
                    try
                    {
                        Habilitar(false);

                        listSolicitudes = getListaSolicitudes();

                        if (listSolicitudes.Count == 0)
                            throw new Exception("No se encontraron las Solicitudes indicadas.");
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

            pbProgreso.Maximum = listSolicitudes.Count;

            pbProgreso.Value = 0;
            pbProgreso.Step = 1;
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker1.RunWorkerAsync();
        }

        private List<TransferenciasSolicitudesDTO> getListaSolicitudes()
        {
            TransferenciasSolicitudesBL solBL = new TransferenciasSolicitudesBL();
            List<TransferenciasSolicitudesDTO> ret = new List<TransferenciasSolicitudesDTO>();
            if (rbLista.Checked)
            {
                List<int> Lista = new List<int>();
                foreach (DataGridViewRow item in gvTramites.Rows)
                {
                    if (item.Cells[0].Value == null)
                        continue;

                    int id_sol = 0;
                    int.TryParse(item.Cells[0].Value.ToString(), out id_sol);
                    if (id_sol > 0)
                        Lista.Add(id_sol);
                }
                ret = solBL.GetListaIdSolicitudTransf(Lista).ToList();
            }
            else
            {
                int desde = 0;
                int hasta = 0;
                int.TryParse(txtSolDesde.Text, out desde);
                int.TryParse(txtSolHasta.Text, out hasta);
                ret = solBL.GetRangoIdSolicitud(desde, hasta).ToList();
            }

            if (ret.Count == 0)
                throw new Exception("No se encontraron las Solicitudes indicadas.");

            return ret;
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
                gvTramites.Enabled = true;
            else
                gvTramites.Enabled = false;
        }

        private void cmdSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdLimpiar_Click(object sender, EventArgs e)
        {
            gvTramites.Rows.Clear();
            txtSolHasta.Text = "";
            txtSolDesde.Text = "";
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
            txtSolDesde.Enabled = hab;
            txtSolHasta.Enabled = hab;
            gvTramites.Enabled = hab;
            if (hab)
                Cursor.Current = Cursors.Default;
            else
                Cursor.Current = Cursors.WaitCursor;
        }

        private void gvTramites_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                DataObject d = gvTramites.GetClipboardContent();
                Clipboard.SetDataObject(d);
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.V)
            {
                try
                {
                    string s = Clipboard.GetText();
                    string[] lines = s.Split('\n');
                    int row = gvTramites.CurrentCell.RowIndex;
                    int col = gvTramites.CurrentCell.ColumnIndex;
                    if (lines.Length > 0)
                        gvTramites.Rows.Add(lines.Length - 1);
                    foreach (string line in lines)
                    {
                        if (row < gvTramites.RowCount && line.Length > 0)
                        {
                            string[] cells = line.Split('\t');
                            for (int i = 0; i < cells.GetLength(0); ++i)
                            {
                                if (col + i < this.gvTramites.ColumnCount)
                                {
                                    gvTramites[col + i, row].Value = Convert.ChangeType(cells[i], gvTramites[col + i, row].ValueType);
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

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            int ContProcesados = 0;

            AzraelBL azBL = new AzraelBL();
            using (FileStream fs = new FileStream(path, FileMode.Append))
            {
                using (TextWriter tw = new StreamWriter(fs))
                {
                    foreach (var item in listSolicitudes)
                    {
                        try
                        {
                            ExternalServiceReporting service = new ExternalServiceReporting();
                            ReportingEntity Report = service.GetPDFManifiestoTransmision(item.IdSolicitud, true);
                            azBL.GuardarPDFManifiestoTransmision(item.IdSolicitud, Report.Id_file, Report.FileName, item.CreateUser);
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
                            continue;
                        }
                    }
                }
            }
        }

        private void frmManifiestoTransmision_Load(object sender, EventArgs e)
        {
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            gvTramites.KeyDown += new KeyEventHandler(gvTramites_KeyDown);
        }       
    }
}
