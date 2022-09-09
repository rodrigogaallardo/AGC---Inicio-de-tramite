namespace Azrael.frmFiles
{
    partial class frmDescargarFile
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
            this.lblIdFile = new System.Windows.Forms.Label();
            this.txtIdFile = new System.Windows.Forms.TextBox();
            this.btnDescargar = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // lblIdFile
            // 
            this.lblIdFile.AutoSize = true;
            this.lblIdFile.Location = new System.Drawing.Point(12, 26);
            this.lblIdFile.Name = "lblIdFile";
            this.lblIdFile.Size = new System.Drawing.Size(38, 13);
            this.lblIdFile.TabIndex = 0;
            this.lblIdFile.Text = "Id File:";
            // 
            // txtIdFile
            // 
            this.txtIdFile.Location = new System.Drawing.Point(56, 23);
            this.txtIdFile.MaxLength = 10;
            this.txtIdFile.Name = "txtIdFile";
            this.txtIdFile.Size = new System.Drawing.Size(100, 20);
            this.txtIdFile.TabIndex = 1;
            // 
            // btnDescargar
            // 
            this.btnDescargar.Location = new System.Drawing.Point(56, 56);
            this.btnDescargar.Name = "btnDescargar";
            this.btnDescargar.Size = new System.Drawing.Size(75, 23);
            this.btnDescargar.TabIndex = 2;
            this.btnDescargar.Text = "Descargar";
            this.btnDescargar.UseVisualStyleBackColor = true;
            this.btnDescargar.Click += new System.EventHandler(this.btnDescargar_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.RestoreDirectory = true;
            // 
            // frmDescargarFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(192, 91);
            this.Controls.Add(this.btnDescargar);
            this.Controls.Add(this.txtIdFile);
            this.Controls.Add(this.lblIdFile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDescargarFile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Descargar";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblIdFile;
        private System.Windows.Forms.TextBox txtIdFile;
        private System.Windows.Forms.Button btnDescargar;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}