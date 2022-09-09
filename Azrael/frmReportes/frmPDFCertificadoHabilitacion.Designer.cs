namespace Azrael
{
    partial class frmPDFCertificadoHabilitacion
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
            this.panelEncomienda = new System.Windows.Forms.Panel();
            this.lblTipoPDF = new System.Windows.Forms.Label();
            this.gbPlanchetaCP = new System.Windows.Forms.GroupBox();
            this.gvCertificadoHab = new System.Windows.Forms.DataGridView();
            this.colCpadron = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rbLista = new System.Windows.Forms.RadioButton();
            this.rbRango = new System.Windows.Forms.RadioButton();
            this.txtSolDesde = new System.Windows.Forms.TextBox();
            this.lblEncDesde = new System.Windows.Forms.Label();
            this.txtSolHasta = new System.Windows.Forms.TextBox();
            this.lblEncHasta = new System.Windows.Forms.Label();
            this.lblInfo1 = new System.Windows.Forms.Label();
            this.cmdSalir = new System.Windows.Forms.Button();
            this.cmdCancelar = new System.Windows.Forms.Button();
            this.cmdEjecutar = new System.Windows.Forms.Button();
            this.cmdLimpiar = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.pbProgreso = new System.Windows.Forms.ProgressBar();
            this.panelEncomienda.SuspendLayout();
            this.gbPlanchetaCP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvCertificadoHab)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEncomienda
            // 
            this.panelEncomienda.Controls.Add(this.lblTipoPDF);
            this.panelEncomienda.Controls.Add(this.gbPlanchetaCP);
            this.panelEncomienda.Controls.Add(this.cmdSalir);
            this.panelEncomienda.Controls.Add(this.cmdCancelar);
            this.panelEncomienda.Controls.Add(this.cmdEjecutar);
            this.panelEncomienda.Controls.Add(this.cmdLimpiar);
            this.panelEncomienda.Location = new System.Drawing.Point(1, 2);
            this.panelEncomienda.Name = "panelEncomienda";
            this.panelEncomienda.Size = new System.Drawing.Size(662, 403);
            this.panelEncomienda.TabIndex = 28;
            // 
            // lblTipoPDF
            // 
            this.lblTipoPDF.AutoSize = true;
            this.lblTipoPDF.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipoPDF.Location = new System.Drawing.Point(202, 8);
            this.lblTipoPDF.Name = "lblTipoPDF";
            this.lblTipoPDF.Size = new System.Drawing.Size(170, 16);
            this.lblTipoPDF.TabIndex = 26;
            this.lblTipoPDF.Text = "Certificado Habilitación";
            // 
            // gbPlanchetaCP
            // 
            this.gbPlanchetaCP.Controls.Add(this.gvCertificadoHab);
            this.gbPlanchetaCP.Controls.Add(this.rbLista);
            this.gbPlanchetaCP.Controls.Add(this.rbRango);
            this.gbPlanchetaCP.Controls.Add(this.txtSolDesde);
            this.gbPlanchetaCP.Controls.Add(this.lblEncDesde);
            this.gbPlanchetaCP.Controls.Add(this.txtSolHasta);
            this.gbPlanchetaCP.Controls.Add(this.lblEncHasta);
            this.gbPlanchetaCP.Controls.Add(this.lblInfo1);
            this.gbPlanchetaCP.Location = new System.Drawing.Point(16, 33);
            this.gbPlanchetaCP.Name = "gbPlanchetaCP";
            this.gbPlanchetaCP.Size = new System.Drawing.Size(511, 365);
            this.gbPlanchetaCP.TabIndex = 16;
            this.gbPlanchetaCP.TabStop = false;
            this.gbPlanchetaCP.Text = "Lista de Certificados de Habilitación";
            // 
            // gvCertificadoHab
            // 
            this.gvCertificadoHab.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvCertificadoHab.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCpadron});
            this.gvCertificadoHab.Enabled = false;
            this.gvCertificadoHab.Location = new System.Drawing.Point(246, 62);
            this.gvCertificadoHab.Name = "gvCertificadoHab";
            this.gvCertificadoHab.RowHeadersWidth = 40;
            this.gvCertificadoHab.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.gvCertificadoHab.Size = new System.Drawing.Size(258, 296);
            this.gvCertificadoHab.TabIndex = 9;
            this.gvCertificadoHab.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gvCertificadoHab_KeyDown);
            // 
            // colCpadron
            // 
            this.colCpadron.HeaderText = "id_tramite";
            this.colCpadron.Name = "colCpadron";
            // 
            // rbLista
            // 
            this.rbLista.AutoSize = true;
            this.rbLista.Location = new System.Drawing.Point(246, 31);
            this.rbLista.Name = "rbLista";
            this.rbLista.Size = new System.Drawing.Size(66, 17);
            this.rbLista.TabIndex = 6;
            this.rbLista.TabStop = true;
            this.rbLista.Text = "Por Lista";
            this.rbLista.UseVisualStyleBackColor = true;
            this.rbLista.CheckedChanged += new System.EventHandler(this.rbLista_CheckedChanged);
            // 
            // rbRango
            // 
            this.rbRango.AutoSize = true;
            this.rbRango.Location = new System.Drawing.Point(24, 31);
            this.rbRango.Name = "rbRango";
            this.rbRango.Size = new System.Drawing.Size(76, 17);
            this.rbRango.TabIndex = 5;
            this.rbRango.TabStop = true;
            this.rbRango.Text = "Por Rango";
            this.rbRango.UseVisualStyleBackColor = true;
            this.rbRango.CheckedChanged += new System.EventHandler(this.rbRango_CheckedChanged);
            // 
            // txtSolDesde
            // 
            this.txtSolDesde.Enabled = false;
            this.txtSolDesde.Location = new System.Drawing.Point(68, 62);
            this.txtSolDesde.Name = "txtSolDesde";
            this.txtSolDesde.Size = new System.Drawing.Size(123, 20);
            this.txtSolDesde.TabIndex = 0;
            // 
            // lblEncDesde
            // 
            this.lblEncDesde.AutoSize = true;
            this.lblEncDesde.Location = new System.Drawing.Point(21, 66);
            this.lblEncDesde.Name = "lblEncDesde";
            this.lblEncDesde.Size = new System.Drawing.Size(41, 13);
            this.lblEncDesde.TabIndex = 1;
            this.lblEncDesde.Text = "Desde:";
            // 
            // txtSolHasta
            // 
            this.txtSolHasta.Enabled = false;
            this.txtSolHasta.Location = new System.Drawing.Point(68, 88);
            this.txtSolHasta.Name = "txtSolHasta";
            this.txtSolHasta.Size = new System.Drawing.Size(123, 20);
            this.txtSolHasta.TabIndex = 2;
            // 
            // lblEncHasta
            // 
            this.lblEncHasta.AutoSize = true;
            this.lblEncHasta.Location = new System.Drawing.Point(24, 92);
            this.lblEncHasta.Name = "lblEncHasta";
            this.lblEncHasta.Size = new System.Drawing.Size(38, 13);
            this.lblEncHasta.TabIndex = 3;
            this.lblEncHasta.Text = "Hasta:";
            // 
            // lblInfo1
            // 
            this.lblInfo1.AutoSize = true;
            this.lblInfo1.Location = new System.Drawing.Point(24, 125);
            this.lblInfo1.Name = "lblInfo1";
            this.lblInfo1.Size = new System.Drawing.Size(216, 13);
            this.lblInfo1.TabIndex = 4;
            this.lblInfo1.Text = "Rango de Certificado Habilitación (Inclusive)";
            // 
            // cmdSalir
            // 
            this.cmdSalir.Image = global::Azrael.Properties.Resources.close32;
            this.cmdSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdSalir.Location = new System.Drawing.Point(533, 238);
            this.cmdSalir.Name = "cmdSalir";
            this.cmdSalir.Size = new System.Drawing.Size(119, 48);
            this.cmdSalir.TabIndex = 21;
            this.cmdSalir.Text = "Salir     ";
            this.cmdSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdSalir.UseVisualStyleBackColor = true;
            this.cmdSalir.Click += new System.EventHandler(this.cmdSalir_Click);
            // 
            // cmdCancelar
            // 
            this.cmdCancelar.Enabled = false;
            this.cmdCancelar.Image = global::Azrael.Properties.Resources.cancel32;
            this.cmdCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdCancelar.Location = new System.Drawing.Point(533, 102);
            this.cmdCancelar.Name = "cmdCancelar";
            this.cmdCancelar.Size = new System.Drawing.Size(119, 48);
            this.cmdCancelar.TabIndex = 24;
            this.cmdCancelar.Text = "Cancelar     ";
            this.cmdCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdCancelar.UseVisualStyleBackColor = true;
            this.cmdCancelar.Click += new System.EventHandler(this.cmdCancelar_Click);
            // 
            // cmdEjecutar
            // 
            this.cmdEjecutar.Image = global::Azrael.Properties.Resources.Play32;
            this.cmdEjecutar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdEjecutar.Location = new System.Drawing.Point(533, 42);
            this.cmdEjecutar.Name = "cmdEjecutar";
            this.cmdEjecutar.Size = new System.Drawing.Size(119, 48);
            this.cmdEjecutar.TabIndex = 22;
            this.cmdEjecutar.Text = "Ejecutar     ";
            this.cmdEjecutar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdEjecutar.UseVisualStyleBackColor = true;
            this.cmdEjecutar.Click += new System.EventHandler(this.cmdEjecutar_Click);
            // 
            // cmdLimpiar
            // 
            this.cmdLimpiar.Image = global::Azrael.Properties.Resources.clear32;
            this.cmdLimpiar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdLimpiar.Location = new System.Drawing.Point(533, 162);
            this.cmdLimpiar.Name = "cmdLimpiar";
            this.cmdLimpiar.Size = new System.Drawing.Size(119, 48);
            this.cmdLimpiar.TabIndex = 23;
            this.cmdLimpiar.Text = "Limpiar     ";
            this.cmdLimpiar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdLimpiar.UseVisualStyleBackColor = true;
            this.cmdLimpiar.Click += new System.EventHandler(this.cmdLimpiar_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // pbProgreso
            // 
            this.pbProgreso.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pbProgreso.Location = new System.Drawing.Point(0, 407);
            this.pbProgreso.Name = "pbProgreso";
            this.pbProgreso.Size = new System.Drawing.Size(665, 23);
            this.pbProgreso.TabIndex = 29;
            // 
            // frmPDFCertificadoHabilitacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 430);
            this.Controls.Add(this.pbProgreso);
            this.Controls.Add(this.panelEncomienda);
            this.Name = "frmPDFCertificadoHabilitacion";
            this.Text = "frmPDFCertificadoHabilitacion";
            this.Load += new System.EventHandler(this.frmPDFCertificadoHabilitacion_Load);
            this.panelEncomienda.ResumeLayout(false);
            this.panelEncomienda.PerformLayout();
            this.gbPlanchetaCP.ResumeLayout(false);
            this.gbPlanchetaCP.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvCertificadoHab)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelEncomienda;
        private System.Windows.Forms.Label lblTipoPDF;
        private System.Windows.Forms.GroupBox gbPlanchetaCP;
        private System.Windows.Forms.DataGridView gvCertificadoHab;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCpadron;
        private System.Windows.Forms.RadioButton rbLista;
        private System.Windows.Forms.RadioButton rbRango;
        private System.Windows.Forms.TextBox txtSolDesde;
        private System.Windows.Forms.Label lblEncDesde;
        private System.Windows.Forms.TextBox txtSolHasta;
        private System.Windows.Forms.Label lblEncHasta;
        private System.Windows.Forms.Label lblInfo1;
        internal System.Windows.Forms.Button cmdSalir;
        internal System.Windows.Forms.Button cmdCancelar;
        internal System.Windows.Forms.Button cmdEjecutar;
        internal System.Windows.Forms.Button cmdLimpiar;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ProgressBar pbProgreso;
    }
}