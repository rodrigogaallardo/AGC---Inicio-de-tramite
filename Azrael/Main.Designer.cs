namespace Azrael
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.regenerarPDFToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.encomiendaToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.certificadoATToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.obleaSolicitudToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.solicitudHabilitacionToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.sGIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.informeCPadronToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.certificadoHabilitaciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dDRRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manifiestoDeTransmisionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.descargarFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buscarYRemplazarEnTramiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.obleaTransmisionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem,
            this.reportesToolStripMenuItem,
            this.filesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1008, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.salirToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "Menu";
            this.menuToolStripMenuItem.Click += new System.EventHandler(this.menuToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(93, 6);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(96, 22);
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            // 
            // reportesToolStripMenuItem
            // 
            this.reportesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.regenerarPDFToolStripMenuItem1});
            this.reportesToolStripMenuItem.Name = "reportesToolStripMenuItem";
            this.reportesToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.reportesToolStripMenuItem.Text = "Reportes";
            // 
            // regenerarPDFToolStripMenuItem1
            // 
            this.regenerarPDFToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.encomiendaToolStripMenuItem1,
            this.certificadoATToolStripMenuItem1,
            this.obleaSolicitudToolStripMenuItem1,
            this.solicitudHabilitacionToolStripMenuItem1,
            this.sGIToolStripMenuItem,
            this.dDRRToolStripMenuItem,
            this.manifiestoDeTransmisionToolStripMenuItem,
            this.obleaTransmisionesToolStripMenuItem});
            this.regenerarPDFToolStripMenuItem1.Name = "regenerarPDFToolStripMenuItem1";
            this.regenerarPDFToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.regenerarPDFToolStripMenuItem1.Text = "Regenerar PDF";
            // 
            // encomiendaToolStripMenuItem1
            // 
            this.encomiendaToolStripMenuItem1.Name = "encomiendaToolStripMenuItem1";
            this.encomiendaToolStripMenuItem1.Size = new System.Drawing.Size(212, 22);
            this.encomiendaToolStripMenuItem1.Text = "Encomienda";
            this.encomiendaToolStripMenuItem1.Click += new System.EventHandler(this.encomiendaToolStripMenuItem1_Click);
            // 
            // certificadoATToolStripMenuItem1
            // 
            this.certificadoATToolStripMenuItem1.Name = "certificadoATToolStripMenuItem1";
            this.certificadoATToolStripMenuItem1.Size = new System.Drawing.Size(212, 22);
            this.certificadoATToolStripMenuItem1.Text = "Certificado AT";
            this.certificadoATToolStripMenuItem1.Click += new System.EventHandler(this.certificadoATToolStripMenuItem1_Click);
            // 
            // obleaSolicitudToolStripMenuItem1
            // 
            this.obleaSolicitudToolStripMenuItem1.Name = "obleaSolicitudToolStripMenuItem1";
            this.obleaSolicitudToolStripMenuItem1.Size = new System.Drawing.Size(212, 22);
            this.obleaSolicitudToolStripMenuItem1.Text = "Oblea Solicitud";
            this.obleaSolicitudToolStripMenuItem1.Click += new System.EventHandler(this.obleaSolicitudToolStripMenuItem1_Click);
            // 
            // solicitudHabilitacionToolStripMenuItem1
            // 
            this.solicitudHabilitacionToolStripMenuItem1.Name = "solicitudHabilitacionToolStripMenuItem1";
            this.solicitudHabilitacionToolStripMenuItem1.Size = new System.Drawing.Size(212, 22);
            this.solicitudHabilitacionToolStripMenuItem1.Text = "Solicitud Habilitacion";
            this.solicitudHabilitacionToolStripMenuItem1.Click += new System.EventHandler(this.solicitudHabilitacionToolStripMenuItem1_Click);
            // 
            // sGIToolStripMenuItem
            // 
            this.sGIToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.informeCPadronToolStripMenuItem,
            this.certificadoHabilitaciónToolStripMenuItem});
            this.sGIToolStripMenuItem.Name = "sGIToolStripMenuItem";
            this.sGIToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.sGIToolStripMenuItem.Text = "SGI";
            // 
            // informeCPadronToolStripMenuItem
            // 
            this.informeCPadronToolStripMenuItem.Name = "informeCPadronToolStripMenuItem";
            this.informeCPadronToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.informeCPadronToolStripMenuItem.Text = "Informe CPadron";
            this.informeCPadronToolStripMenuItem.Click += new System.EventHandler(this.informeCPadronToolStripMenuItem_Click);
            // 
            // certificadoHabilitaciónToolStripMenuItem
            // 
            this.certificadoHabilitaciónToolStripMenuItem.Name = "certificadoHabilitaciónToolStripMenuItem";
            this.certificadoHabilitaciónToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.certificadoHabilitaciónToolStripMenuItem.Text = "Certificado Habilitación";
            this.certificadoHabilitaciónToolStripMenuItem.Click += new System.EventHandler(this.certificadoHabilitaciónToolStripMenuItem_Click);
            // 
            // dDRRToolStripMenuItem
            // 
            this.dDRRToolStripMenuItem.Name = "dDRRToolStripMenuItem";
            this.dDRRToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.dDRRToolStripMenuItem.Text = "DDRR";
            this.dDRRToolStripMenuItem.Click += new System.EventHandler(this.dDRRToolStripMenuItem_Click);
            // 
            // manifiestoDeTransmisionToolStripMenuItem
            // 
            this.manifiestoDeTransmisionToolStripMenuItem.Name = "manifiestoDeTransmisionToolStripMenuItem";
            this.manifiestoDeTransmisionToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.manifiestoDeTransmisionToolStripMenuItem.Text = "Manifiesto de Transmisión";
            this.manifiestoDeTransmisionToolStripMenuItem.Click += new System.EventHandler(this.manifiestoDeTransmisionToolStripMenuItem_Click);
            // 
            // filesToolStripMenuItem
            // 
            this.filesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.descargarFileToolStripMenuItem,
            this.uploadFileToolStripMenuItem,
            this.buscarYRemplazarEnTramiteToolStripMenuItem});
            this.filesToolStripMenuItem.Name = "filesToolStripMenuItem";
            this.filesToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.filesToolStripMenuItem.Text = "Files";
            // 
            // descargarFileToolStripMenuItem
            // 
            this.descargarFileToolStripMenuItem.Name = "descargarFileToolStripMenuItem";
            this.descargarFileToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.descargarFileToolStripMenuItem.Text = "Descargar";
            this.descargarFileToolStripMenuItem.Click += new System.EventHandler(this.descargarFileToolStripMenuItem_Click);
            // 
            // uploadFileToolStripMenuItem
            // 
            this.uploadFileToolStripMenuItem.Name = "uploadFileToolStripMenuItem";
            this.uploadFileToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.uploadFileToolStripMenuItem.Text = "Subir";
            this.uploadFileToolStripMenuItem.Click += new System.EventHandler(this.uploadFileToolStripMenuItem_Click);
            // 
            // buscarYRemplazarEnTramiteToolStripMenuItem
            // 
            this.buscarYRemplazarEnTramiteToolStripMenuItem.Name = "buscarYRemplazarEnTramiteToolStripMenuItem";
            this.buscarYRemplazarEnTramiteToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.buscarYRemplazarEnTramiteToolStripMenuItem.Text = "Buscar y remplazar";
            this.buscarYRemplazarEnTramiteToolStripMenuItem.Click += new System.EventHandler(this.buscarYRemplazarEnTramiteToolStripMenuItem_Click);
            // 
            // obleaTransmisionesToolStripMenuItem
            // 
            this.obleaTransmisionesToolStripMenuItem.Name = "obleaTransmisionesToolStripMenuItem";
            this.obleaTransmisionesToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.obleaTransmisionesToolStripMenuItem.Text = "Oblea Transmisión";
            this.obleaTransmisionesToolStripMenuItem.Click += new System.EventHandler(this.obleaTransmisionesToolStripMenuItem_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 605);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Azrael";
            this.Load += new System.EventHandler(this.Main_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem reportesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem regenerarPDFToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem encomiendaToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem certificadoATToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem obleaSolicitudToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem solicitudHabilitacionToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem filesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem descargarFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uploadFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buscarYRemplazarEnTramiteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sGIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem informeCPadronToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem certificadoHabilitaciónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dDRRToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manifiestoDeTransmisionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem obleaTransmisionesToolStripMenuItem;
    }
}

