using Azrael.frmFiles;
using Azrael.frmReportes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Azrael
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        private bool ValidarFormsAbiertos(string frmName)
        {
            FormCollection fc = Application.OpenForms;

            foreach (Form frm in fc)
                if (frm.Name == frmName)
                    return true;

            return false;
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void encomiendaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (ValidarFormsAbiertos("frmPDFEncomienda"))
                return;

            frmPDFEncomienda pdfEncFRM = new frmPDFEncomienda();
            pdfEncFRM.MdiParent = this;
            pdfEncFRM.Show();
            pdfEncFRM.Focus();
        }

        private void certificadoATToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (ValidarFormsAbiertos("frmPDFCertificado"))
                return;

            frmPDFCertificado pdfCertFRM = new frmPDFCertificado();
            pdfCertFRM.MdiParent = this;
            pdfCertFRM.Show();
            pdfCertFRM.Focus();
        }

        private void obleaSolicitudToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (ValidarFormsAbiertos("frmPDFOblea"))
                return;

            frmPDFOblea pdfObleaFRM = new frmPDFOblea();
            pdfObleaFRM.MdiParent = this;
            pdfObleaFRM.Show();
            pdfObleaFRM.Focus();
        }

        private void solicitudHabilitacionToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (ValidarFormsAbiertos("frmPDFSolicitudHabilitacion"))
                return;

            frmPDFSolicitudHabilitacion pdfSolicitudHabilitacionFRM = new frmPDFSolicitudHabilitacion();
            pdfSolicitudHabilitacionFRM.MdiParent = this;
            pdfSolicitudHabilitacionFRM.Show();
            pdfSolicitudHabilitacionFRM.Focus();
        }
        
        private void descargarFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ValidarFormsAbiertos("frmDescargarFile"))
                return;

            frmDescargarFile DescargarFileFRM = new frmDescargarFile();
            DescargarFileFRM.MdiParent = this;
            DescargarFileFRM.Show();
            DescargarFileFRM.Focus();

        }

        private void buscarYRemplazarEnTramiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ValidarFormsAbiertos("frmBuscarReplaceFile"))
                return;

            frmBuscarReplaceFile BuscarReplaceFileFRM = new frmBuscarReplaceFile();
            BuscarReplaceFileFRM.MdiParent = this;
            BuscarReplaceFileFRM.Show();
            BuscarReplaceFileFRM.Focus();
        }

        private void uploadFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ValidarFormsAbiertos("frmSubirFile"))
                return;

            frmSubirFile frmSubirFileFRM = new frmSubirFile();
            frmSubirFileFRM.MdiParent = this;
            frmSubirFileFRM.Show();
            frmSubirFileFRM.Focus();

        }

        private void Main_Load(object sender, EventArgs e)
        {
            string Ambiente = ConfigurationManager.AppSettings["Ambiente"];
            this.Text += " - " + Ambiente;
        }

        private void eventualesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        
        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void informeCPadronToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ValidarFormsAbiertos("frmPDFPlanchetaCpadron"))
                return;

            frmPDFPlanchetaCpadron pdfcpFRM = new frmPDFPlanchetaCpadron();
            pdfcpFRM.MdiParent = this;
            pdfcpFRM.Show();
            pdfcpFRM.Focus();
        }

        private void certificadoHabilitaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ValidarFormsAbiertos("frmPDFCertificadoHabilitacion"))
                return;

            frmPDFCertificadoHabilitacion pdfchFRM = new frmPDFCertificadoHabilitacion();
            pdfchFRM.MdiParent = this;
            pdfchFRM.Show();
            pdfchFRM.Focus();
        }

        private void dDRRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ValidarFormsAbiertos("frmDDRR"))
                return;

            var frmDDRR = new frmDDRR();
            frmDDRR.MdiParent = this;
            frmDDRR.Show();
            frmDDRR.Focus();
        }

        private void manifiestoDeTransmisionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ValidarFormsAbiertos("frmManifiestoTransmision"))
                return;

            var frm = new frmManifiestoTransmision();
            frm.MdiParent = this;
            frm.Show();
            frm.Focus();
        }

        private void obleaTransmisionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ValidarFormsAbiertos("frmPDFObleaTransmision"))
                return;

            var frm = new frmPDFObleaTransmision();
            frm.MdiParent = this;
            frm.Show();
            frm.Focus();
        }
    }
}
