namespace Azrael.frmFiles
{
    partial class frmMostrarIdFile
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
            this.txtIdFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCopiar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtIdFile
            // 
            this.txtIdFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIdFile.Location = new System.Drawing.Point(88, 14);
            this.txtIdFile.Name = "txtIdFile";
            this.txtIdFile.ReadOnly = true;
            this.txtIdFile.Size = new System.Drawing.Size(100, 31);
            this.txtIdFile.TabIndex = 0;
            this.txtIdFile.Text = "954744";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Id File:";
            // 
            // btnCopiar
            // 
            this.btnCopiar.Location = new System.Drawing.Point(60, 51);
            this.btnCopiar.Name = "btnCopiar";
            this.btnCopiar.Size = new System.Drawing.Size(75, 23);
            this.btnCopiar.TabIndex = 2;
            this.btnCopiar.Text = "Copiar";
            this.btnCopiar.UseVisualStyleBackColor = true;
            this.btnCopiar.Click += new System.EventHandler(this.btnCopiar_Click);
            // 
            // frmMostrarIdFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(214, 91);
            this.Controls.Add(this.btnCopiar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtIdFile);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMostrarIdFile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nuevo ID File";
            this.Load += new System.EventHandler(this.frmMostrarIdFile_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtIdFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCopiar;
    }
}