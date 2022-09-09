using ExternalService;
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

namespace Azrael.frmFiles
{
    public partial class frmSubirFile : Form
    {
        string filePath;
        int newIdFile;
        public frmSubirFile()
        {
            InitializeComponent();
            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(frmSubirFile_DragEnter);
            this.DragDrop += new DragEventHandler(frmSubirFile_DragDrop);
        }

        void frmSubirFile_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void Habilitar(bool enable)
        {
            btnExaminar.Enabled = enable;
            btnSubir.Enabled = enable;
        }

        void frmSubirFile_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                filePath = file;
                lblPath.Text = Path.GetFileName(file);
            }
            Subir();
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

        private void Subir()
        {
            Habilitar(false);
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.RunWorkerAsync();
        }

        private void btnSubir_Click(object sender, EventArgs e)
        {
            Subir();
        }

        private void frmSubirFile_Load(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
        }


        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Habilitar(true);
            backgroundWorker1.Dispose();
            frmMostrarIdFile frmMostrarId = new frmMostrarIdFile();
            frmMostrarId.IdFileNuevo = newIdFile.ToString();
            frmMostrarId.ShowDialog();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (filePath != null)
            {
                ExternalServiceFiles esfiles = new ExternalServiceFiles();
                byte[] fileBytes = File.ReadAllBytes(filePath);
                string nombreArchivo = Path.GetFileName(filePath);
                newIdFile = esfiles.addFile(nombreArchivo, fileBytes);
            }
        }



    }
}
