namespace Azrael.frmFiles
{
    partial class frmBuscarReplaceFile
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtID = new System.Windows.Forms.TextBox();
            this.lblID = new System.Windows.Forms.Label();
            this.lblAyuda = new System.Windows.Forms.Label();
            this.gvTramites = new System.Windows.Forms.DataGridView();
            this.idTipoTramite = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_docadjunto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdTramite = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoTramite1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoTramite2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoDocReq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoDocSis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblPath = new System.Windows.Forms.Label();
            this.btnReemplazar = new System.Windows.Forms.Button();
            this.btnExaminar = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.gvTramites)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(97, 12);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(100, 20);
            this.txtID.TabIndex = 0;
            this.txtID.TextChanged += new System.EventHandler(this.txtID_TextChanged);
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(72, 16);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(19, 13);
            this.lblID.TabIndex = 1;
            this.lblID.Text = "Id:";
            // 
            // lblAyuda
            // 
            this.lblAyuda.AutoSize = true;
            this.lblAyuda.Location = new System.Drawing.Point(43, 51);
            this.lblAyuda.Name = "lblAyuda";
            this.lblAyuda.Size = new System.Drawing.Size(230, 13);
            this.lblAyuda.TabIndex = 2;
            this.lblAyuda.Text = "(Solicitud, AT, CPadron, Transferencia o IDFile)";
            // 
            // gvTramites
            // 
            this.gvTramites.AllowUserToAddRows = false;
            this.gvTramites.AllowUserToDeleteRows = false;
            this.gvTramites.AllowUserToOrderColumns = true;
            this.gvTramites.AllowUserToResizeRows = false;
            this.gvTramites.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvTramites.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvTramites.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idTipoTramite,
            this.id_docadjunto,
            this.IdTramite,
            this.IdFile,
            this.TipoTramite1,
            this.TipoTramite2,
            this.Estado,
            this.TipoDocReq,
            this.TipoDocSis,
            this.FileName});
            this.gvTramites.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvTramites.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gvTramites.Location = new System.Drawing.Point(0, 0);
            this.gvTramites.Name = "gvTramites";
            this.gvTramites.RowHeadersVisible = false;
            this.gvTramites.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvTramites.Size = new System.Drawing.Size(949, 271);
            this.gvTramites.TabIndex = 4;
            // 
            // idTipoTramite
            // 
            this.idTipoTramite.DataPropertyName = "idTipoTramite";
            this.idTipoTramite.HeaderText = "ID";
            this.idTipoTramite.Name = "idTipoTramite";
            this.idTipoTramite.Visible = false;
            // 
            // id_docadjunto
            // 
            this.id_docadjunto.DataPropertyName = "id_docadjunto";
            this.id_docadjunto.HeaderText = "id_docadjunto";
            this.id_docadjunto.Name = "id_docadjunto";
            this.id_docadjunto.Visible = false;
            // 
            // IdTramite
            // 
            this.IdTramite.DataPropertyName = "IdTramite";
            this.IdTramite.HeaderText = "IdTramite";
            this.IdTramite.Name = "IdTramite";
            // 
            // IdFile
            // 
            this.IdFile.DataPropertyName = "IdFile";
            this.IdFile.HeaderText = "IdFile";
            this.IdFile.Name = "IdFile";
            // 
            // TipoTramite1
            // 
            this.TipoTramite1.DataPropertyName = "TipoTramite1";
            this.TipoTramite1.HeaderText = "TipoTramite1";
            this.TipoTramite1.Name = "TipoTramite1";
            // 
            // TipoTramite2
            // 
            this.TipoTramite2.DataPropertyName = "TipoTramite2";
            this.TipoTramite2.HeaderText = "TipoTramite2";
            this.TipoTramite2.Name = "TipoTramite2";
            // 
            // Estado
            // 
            this.Estado.DataPropertyName = "Estado";
            this.Estado.HeaderText = "Estado";
            this.Estado.Name = "Estado";
            // 
            // TipoDocReq
            // 
            this.TipoDocReq.DataPropertyName = "TipoDocReq";
            this.TipoDocReq.HeaderText = "TipoDocReq";
            this.TipoDocReq.Name = "TipoDocReq";
            // 
            // TipoDocSis
            // 
            this.TipoDocSis.DataPropertyName = "TipoDocSis";
            this.TipoDocSis.HeaderText = "TipoDocSis";
            this.TipoDocSis.Name = "TipoDocSis";
            // 
            // FileName
            // 
            this.FileName.DataPropertyName = "FileName";
            this.FileName.HeaderText = "FileName";
            this.FileName.Name = "FileName";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblPath);
            this.groupBox1.Controls.Add(this.btnReemplazar);
            this.groupBox1.Controls.Add(this.btnExaminar);
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Location = new System.Drawing.Point(445, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(496, 80);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.Location = new System.Drawing.Point(22, 55);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(0, 13);
            this.lblPath.TabIndex = 11;
            // 
            // btnReemplazar
            // 
            this.btnReemplazar.Location = new System.Drawing.Point(187, 19);
            this.btnReemplazar.Name = "btnReemplazar";
            this.btnReemplazar.Size = new System.Drawing.Size(75, 23);
            this.btnReemplazar.TabIndex = 10;
            this.btnReemplazar.Text = "Reemplazar";
            this.btnReemplazar.UseVisualStyleBackColor = true;
            this.btnReemplazar.Click += new System.EventHandler(this.btnReemplazar_Click);
            // 
            // btnExaminar
            // 
            this.btnExaminar.Location = new System.Drawing.Point(106, 19);
            this.btnExaminar.Name = "btnExaminar";
            this.btnExaminar.Size = new System.Drawing.Size(75, 23);
            this.btnExaminar.TabIndex = 9;
            this.btnExaminar.Text = "Examinar";
            this.btnExaminar.UseVisualStyleBackColor = true;
            this.btnExaminar.Click += new System.EventHandler(this.btnExaminar_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(25, 19);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 23);
            this.btnBuscar.TabIndex = 8;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.RestoreDirectory = true;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(949, 100);
            this.panel1.TabIndex = 9;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.gvTramites);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 100);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(949, 271);
            this.panel2.TabIndex = 10;
            // 
            // frmBuscarReplaceFile
            // 
            this.AcceptButton = this.btnBuscar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 371);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblAyuda);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBuscarReplaceFile";
            this.Text = "Reemplazar File";
            this.Load += new System.EventHandler(this.frmBuscarReplaceFile_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvTramites)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Label lblAyuda;
        private System.Windows.Forms.DataGridView gvTramites;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.Button btnReemplazar;
        private System.Windows.Forms.Button btnExaminar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.DataGridViewTextBoxColumn idTipoTramite;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_docadjunto;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdTramite;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoTramite1;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoTramite2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Estado;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoDocReq;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoDocSis;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}