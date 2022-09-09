namespace Azrael
{
    partial class frmPDFPlanchetaCpadron
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
            this.gvInformeCP = new System.Windows.Forms.DataGridView();
            this.rbLista = new System.Windows.Forms.RadioButton();
            this.rbRango = new System.Windows.Forms.RadioButton();
            this.txtEncDesde = new System.Windows.Forms.TextBox();
            this.lblEncDesde = new System.Windows.Forms.Label();
            this.txtEncHasta = new System.Windows.Forms.TextBox();
            this.lblEncHasta = new System.Windows.Forms.Label();
            this.lblInfo1 = new System.Windows.Forms.Label();
            this.cmdSalir = new System.Windows.Forms.Button();
            this.cmdCancelar = new System.Windows.Forms.Button();
            this.cmdEjecutar = new System.Windows.Forms.Button();
            this.cmdLimpiar = new System.Windows.Forms.Button();
            this.pbProgreso = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.colCpadron = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEncomienda.SuspendLayout();
            this.gbPlanchetaCP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvInformeCP)).BeginInit();
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
            this.panelEncomienda.Location = new System.Drawing.Point(2, 1);
            this.panelEncomienda.Name = "panelEncomienda";
            this.panelEncomienda.Size = new System.Drawing.Size(662, 403);
            this.panelEncomienda.TabIndex = 27;
            // 
            // lblTipoPDF
            // 
            this.lblTipoPDF.AutoSize = true;
            this.lblTipoPDF.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipoPDF.Location = new System.Drawing.Point(202, 8);
            this.lblTipoPDF.Name = "lblTipoPDF";
            this.lblTipoPDF.Size = new System.Drawing.Size(122, 16);
            this.lblTipoPDF.TabIndex = 26;
            this.lblTipoPDF.Text = "Informe Cpadron";
            // 
            // gbPlanchetaCP
            // 
            this.gbPlanchetaCP.Controls.Add(this.gvInformeCP);
            this.gbPlanchetaCP.Controls.Add(this.rbLista);
            this.gbPlanchetaCP.Controls.Add(this.rbRango);
            this.gbPlanchetaCP.Controls.Add(this.txtEncDesde);
            this.gbPlanchetaCP.Controls.Add(this.lblEncDesde);
            this.gbPlanchetaCP.Controls.Add(this.txtEncHasta);
            this.gbPlanchetaCP.Controls.Add(this.lblEncHasta);
            this.gbPlanchetaCP.Controls.Add(this.lblInfo1);
            this.gbPlanchetaCP.Location = new System.Drawing.Point(16, 33);
            this.gbPlanchetaCP.Name = "gbPlanchetaCP";
            this.gbPlanchetaCP.Size = new System.Drawing.Size(490, 365);
            this.gbPlanchetaCP.TabIndex = 16;
            this.gbPlanchetaCP.TabStop = false;
            this.gbPlanchetaCP.Text = "Lista de Informes Cpadron";
            // 
            // gvInformeCP
            // 
            this.gvInformeCP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvInformeCP.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCpadron});
            this.gvInformeCP.Enabled = false;
            this.gvInformeCP.Location = new System.Drawing.Point(223, 62);
            this.gvInformeCP.Name = "gvInformeCP";
            this.gvInformeCP.RowHeadersWidth = 40;
            this.gvInformeCP.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.gvInformeCP.Size = new System.Drawing.Size(258, 296);
            this.gvInformeCP.TabIndex = 9;
            this.gvInformeCP.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gvInformeCP_KeyDown);
            // 
            // rbLista
            // 
            this.rbLista.AutoSize = true;
            this.rbLista.Location = new System.Drawing.Point(223, 31);
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
            // txtEncDesde
            // 
            this.txtEncDesde.Enabled = false;
            this.txtEncDesde.Location = new System.Drawing.Point(68, 62);
            this.txtEncDesde.Name = "txtEncDesde";
            this.txtEncDesde.Size = new System.Drawing.Size(123, 20);
            this.txtEncDesde.TabIndex = 0;
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
            // txtEncHasta
            // 
            this.txtEncHasta.Enabled = false;
            this.txtEncHasta.Location = new System.Drawing.Point(68, 88);
            this.txtEncHasta.Name = "txtEncHasta";
            this.txtEncHasta.Size = new System.Drawing.Size(123, 20);
            this.txtEncHasta.TabIndex = 2;
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
            this.lblInfo1.Size = new System.Drawing.Size(186, 13);
            this.lblInfo1.TabIndex = 4;
            this.lblInfo1.Text = "Rango de Informe Cpadron (Inclusive)";
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
            // pbProgreso
            // 
            this.pbProgreso.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pbProgreso.Location = new System.Drawing.Point(0, 404);
            this.pbProgreso.Name = "pbProgreso";
            this.pbProgreso.Size = new System.Drawing.Size(666, 23);
            this.pbProgreso.TabIndex = 28;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // colCpadron
            // 
            this.colCpadron.HeaderText = "id_tramite";
            this.colCpadron.Name = "colCpadron";
            // 
            // frmPDFPlanchetaCpadron
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 427);
            this.Controls.Add(this.pbProgreso);
            this.Controls.Add(this.panelEncomienda);
            this.Name = "frmPDFPlanchetaCpadron";
            this.Text = "frmPDFPlanchetaCpadron";
            this.Load += new System.EventHandler(this.frmPDFPlanchetaCpadron_Load);
            this.panelEncomienda.ResumeLayout(false);
            this.panelEncomienda.PerformLayout();
            this.gbPlanchetaCP.ResumeLayout(false);
            this.gbPlanchetaCP.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvInformeCP)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelEncomienda;
        private System.Windows.Forms.Label lblTipoPDF;
        private System.Windows.Forms.GroupBox gbPlanchetaCP;
        private System.Windows.Forms.DataGridView gvInformeCP;
        private System.Windows.Forms.RadioButton rbLista;
        private System.Windows.Forms.RadioButton rbRango;
        private System.Windows.Forms.TextBox txtEncDesde;
        private System.Windows.Forms.Label lblEncDesde;
        private System.Windows.Forms.TextBox txtEncHasta;
        private System.Windows.Forms.Label lblEncHasta;
        private System.Windows.Forms.Label lblInfo1;
        internal System.Windows.Forms.Button cmdSalir;
        internal System.Windows.Forms.Button cmdCancelar;
        internal System.Windows.Forms.Button cmdEjecutar;
        internal System.Windows.Forms.Button cmdLimpiar;
        private System.Windows.Forms.ProgressBar pbProgreso;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCpadron;
    }
}