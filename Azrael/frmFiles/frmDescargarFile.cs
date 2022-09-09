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
    public partial class frmDescargarFile : Form
    {
        public frmDescargarFile()
        {
            InitializeComponent();
        }

        private void btnDescargar_Click(object sender, EventArgs e)
        {
            int id_file = 0;

            int.TryParse(txtIdFile.Text, out id_file);

            if (id_file <= 0)
            {
                MessageBox.Show("Debe ingresar un Id File valido.");
                return;
            }

            Cursor.Current = Cursors.WaitCursor;


            ExternalServiceFiles esFiles = new ExternalServiceFiles();
            string fileName;
            var fileByte = esFiles.downloadFile(id_file, out fileName);


            SaveFileDialog savefile = new SaveFileDialog();
            savefile.FileName = fileName;
            savefile.Filter = "All files (*.*)|*.*";

            if (savefile.ShowDialog() == DialogResult.OK)
            {
                using (Stream fs = (Stream)savefile.OpenFile())
                {
                    fs.Write(fileByte, 0, fileByte.Length);
                    fs.Close();
                }
                DialogResult dialogResult = MessageBox.Show("Archivo guardado" + Environment.NewLine + "¿Desea abrir el archivo?", "Descarga de archivo", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                    System.Diagnostics.Process.Start(savefile.FileName);
            }
            Cursor.Current = Cursors.Default;
        }
    }
}
