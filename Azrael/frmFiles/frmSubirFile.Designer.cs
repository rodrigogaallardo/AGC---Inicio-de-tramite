namespace Azrael.frmFiles
{
    partial class frmSubirFile
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
            this.btnSubir = new System.Windows.Forms.Button();
            this.lblDrawAndDrop = new System.Windows.Forms.Label();
            this.lblPath = new System.Windows.Forms.Label();
            this.btnExaminar = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // btnSubir
            // 
            this.btnSubir.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSubir.Location = new System.Drawing.Point(44, 15);
            this.btnSubir.Name = "btnSubir";
            this.btnSubir.Size = new System.Drawing.Size(75, 23);
            this.btnSubir.TabIndex = 34;
            this.btnSubir.Text = "Subir";
            this.btnSubir.UseVisualStyleBackColor = true;
            this.btnSubir.Click += new System.EventHandler(this.btnSubir_Click);
            // 
            // lblDrawAndDrop
            // 
            this.lblDrawAndDrop.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDrawAndDrop.AutoSize = true;
            this.lblDrawAndDrop.Location = new System.Drawing.Point(16, 79);
            this.lblDrawAndDrop.Name = "lblDrawAndDrop";
            this.lblDrawAndDrop.Size = new System.Drawing.Size(228, 13);
            this.lblDrawAndDrop.TabIndex = 33;
            this.lblDrawAndDrop.Text = "Arrastre y suelte el archivo o presione examinar";
            // 
            // lblPath
            // 
            this.lblPath.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblPath.AutoSize = true;
            this.lblPath.Location = new System.Drawing.Point(16, 48);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(0, 13);
            this.lblPath.TabIndex = 32;
            // 
            // btnExaminar
            // 
            this.btnExaminar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnExaminar.Location = new System.Drawing.Point(152, 15);
            this.btnExaminar.Name = "btnExaminar";
            this.btnExaminar.Size = new System.Drawing.Size(75, 23);
            this.btnExaminar.TabIndex = 31;
            this.btnExaminar.Text = "Examinar";
            this.btnExaminar.UseVisualStyleBackColor = true;
            this.btnExaminar.Click += new System.EventHandler(this.btnExaminar_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // frmSubirFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(274, 121);
            this.Controls.Add(this.btnSubir);
            this.Controls.Add(this.lblDrawAndDrop);
            this.Controls.Add(this.lblPath);
            this.Controls.Add(this.btnExaminar);
            this.Name = "frmSubirFile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Subir Archivo";
            this.Load += new System.EventHandler(this.frmSubirFile_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSubir;
        private System.Windows.Forms.Label lblDrawAndDrop;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.Button btnExaminar;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;



    }
}