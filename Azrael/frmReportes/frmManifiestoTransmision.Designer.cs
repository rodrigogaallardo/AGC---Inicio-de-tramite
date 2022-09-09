namespace Azrael.frmReportes
{
    partial class frmManifiestoTransmision
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
            this.cmdSalir = new System.Windows.Forms.Button();
            this.cmdCancelar = new System.Windows.Forms.Button();
            this.cmdEjecutar = new System.Windows.Forms.Button();
            this.cmdLimpiar = new System.Windows.Forms.Button();
            this.gvTramites = new System.Windows.Forms.DataGridView();
            this.colEncomienda = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rbLista = new System.Windows.Forms.RadioButton();
            this.lblTipoPDF = new System.Windows.Forms.Label();
            this.gbEncomiendas = new System.Windows.Forms.GroupBox();
            this.rbRango = new System.Windows.Forms.RadioButton();
            this.txtSolDesde = new System.Windows.Forms.TextBox();
            this.lblEncDesde = new System.Windows.Forms.Label();
            this.txtSolHasta = new System.Windows.Forms.TextBox();
            this.lblEncHasta = new System.Windows.Forms.Label();
            this.lblInfo1 = new System.Windows.Forms.Label();
            this.panelEncomienda = new System.Windows.Forms.Panel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.pbProgreso = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.gvTramites)).BeginInit();
            this.gbEncomiendas.SuspendLayout();
            this.panelEncomienda.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdSalir
            // 
            this.cmdSalir.Image = global::Azrael.Properties.Resources.close32;
            this.cmdSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdSalir.Location = new System.Drawing.Point(465, 235);
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
            this.cmdCancelar.Location = new System.Drawing.Point(465, 99);
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
            this.cmdEjecutar.Location = new System.Drawing.Point(465, 39);
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
            this.cmdLimpiar.Location = new System.Drawing.Point(465, 159);
            this.cmdLimpiar.Name = "cmdLimpiar";
            this.cmdLimpiar.Size = new System.Drawing.Size(119, 48);
            this.cmdLimpiar.TabIndex = 23;
            this.cmdLimpiar.Text = "Limpiar     ";
            this.cmdLimpiar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdLimpiar.UseVisualStyleBackColor = true;
            this.cmdLimpiar.Click += new System.EventHandler(this.cmdLimpiar_Click);
            // 
            // gvTramites
            // 
            this.gvTramites.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvTramites.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colEncomienda});
            this.gvTramites.Enabled = false;
            this.gvTramites.Location = new System.Drawing.Point(223, 62);
            this.gvTramites.Name = "gvTramites";
            this.gvTramites.RowHeadersWidth = 40;
            this.gvTramites.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.gvTramites.Size = new System.Drawing.Size(187, 296);
            this.gvTramites.TabIndex = 9;
            this.gvTramites.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gvTramites_KeyDown);
            // 
            // colEncomienda
            // 
            this.colEncomienda.HeaderText = "id_tramite";
            this.colEncomienda.Name = "colEncomienda";
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
            // lblTipoPDF
            // 
            this.lblTipoPDF.AutoSize = true;
            this.lblTipoPDF.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipoPDF.Location = new System.Drawing.Point(199, 11);
            this.lblTipoPDF.Name = "lblTipoPDF";
            this.lblTipoPDF.Size = new System.Drawing.Size(190, 16);
            this.lblTipoPDF.TabIndex = 25;
            this.lblTipoPDF.Text = "Manifiesto de Transmisión";
            // 
            // gbEncomiendas
            // 
            this.gbEncomiendas.Controls.Add(this.gvTramites);
            this.gbEncomiendas.Controls.Add(this.rbLista);
            this.gbEncomiendas.Controls.Add(this.rbRango);
            this.gbEncomiendas.Controls.Add(this.txtSolDesde);
            this.gbEncomiendas.Controls.Add(this.lblEncDesde);
            this.gbEncomiendas.Controls.Add(this.txtSolHasta);
            this.gbEncomiendas.Controls.Add(this.lblEncHasta);
            this.gbEncomiendas.Controls.Add(this.lblInfo1);
            this.gbEncomiendas.Location = new System.Drawing.Point(16, 30);
            this.gbEncomiendas.Name = "gbEncomiendas";
            this.gbEncomiendas.Size = new System.Drawing.Size(432, 365);
            this.gbEncomiendas.TabIndex = 16;
            this.gbEncomiendas.TabStop = false;
            this.gbEncomiendas.Text = "Lista de TR";
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
            this.lblInfo1.Size = new System.Drawing.Size(123, 13);
            this.lblInfo1.TabIndex = 4;
            this.lblInfo1.Text = "Rango de TR (Inclusive)";
            // 
            // panelEncomienda
            // 
            this.panelEncomienda.Controls.Add(this.lblTipoPDF);
            this.panelEncomienda.Controls.Add(this.gbEncomiendas);
            this.panelEncomienda.Controls.Add(this.cmdSalir);
            this.panelEncomienda.Controls.Add(this.cmdCancelar);
            this.panelEncomienda.Controls.Add(this.cmdEjecutar);
            this.panelEncomienda.Controls.Add(this.cmdLimpiar);
            this.panelEncomienda.Location = new System.Drawing.Point(12, 12);
            this.panelEncomienda.Name = "panelEncomienda";
            this.panelEncomienda.Size = new System.Drawing.Size(595, 400);
            this.panelEncomienda.TabIndex = 29;
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
            this.pbProgreso.Location = new System.Drawing.Point(0, 403);
            this.pbProgreso.Name = "pbProgreso";
            this.pbProgreso.Size = new System.Drawing.Size(616, 23);
            this.pbProgreso.TabIndex = 30;
            // 
            // frmManifiestoTransmision
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 426);
            this.Controls.Add(this.pbProgreso);
            this.Controls.Add(this.panelEncomienda);
            this.Name = "frmManifiestoTransmision";
            this.Text = "PDF Manifiesto Transmisión";
            this.Load += new System.EventHandler(this.frmManifiestoTransmision_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvTramites)).EndInit();
            this.gbEncomiendas.ResumeLayout(false);
            this.gbEncomiendas.PerformLayout();
            this.panelEncomienda.ResumeLayout(false);
            this.panelEncomienda.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button cmdSalir;
        internal System.Windows.Forms.Button cmdCancelar;
        internal System.Windows.Forms.Button cmdEjecutar;
        internal System.Windows.Forms.Button cmdLimpiar;
        private System.Windows.Forms.DataGridView gvTramites;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEncomienda;
        private System.Windows.Forms.RadioButton rbLista;
        private System.Windows.Forms.Label lblTipoPDF;
        private System.Windows.Forms.GroupBox gbEncomiendas;
        private System.Windows.Forms.RadioButton rbRango;
        private System.Windows.Forms.TextBox txtSolDesde;
        private System.Windows.Forms.Label lblEncDesde;
        private System.Windows.Forms.TextBox txtSolHasta;
        private System.Windows.Forms.Label lblEncHasta;
        private System.Windows.Forms.Label lblInfo1;
        private System.Windows.Forms.Panel panelEncomienda;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ProgressBar pbProgreso;
    }
}