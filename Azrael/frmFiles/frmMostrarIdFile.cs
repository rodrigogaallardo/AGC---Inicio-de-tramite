using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Azrael.frmFiles
{
    public partial class frmMostrarIdFile : Form
    {
        public string IdFileNuevo;

        public frmMostrarIdFile()
        {
            InitializeComponent();
        }

        private void btnCopiar_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtIdFile.Text);
        }

        private void frmMostrarIdFile_Load(object sender, EventArgs e)
        {
            txtIdFile.Text = IdFileNuevo;
        }
    }
}
