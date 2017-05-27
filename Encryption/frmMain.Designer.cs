namespace Encryption
{
    partial class frmMain
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
            this.txtSource = new System.Windows.Forms.TextBox();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.btnDecrypt = new System.Windows.Forms.Button();
            this.btnReadFromGoogle = new System.Windows.Forms.Button();
            this.btnWriteToGoogle = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtSource
            // 
            this.txtSource.Location = new System.Drawing.Point(12, 12);
            this.txtSource.Multiline = true;
            this.txtSource.Name = "txtSource";
            this.txtSource.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSource.Size = new System.Drawing.Size(597, 223);
            this.txtSource.TabIndex = 0;
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(12, 241);
            this.txtKey.MaxLength = 256;
            this.txtKey.Name = "txtKey";
            this.txtKey.PasswordChar = '~';
            this.txtKey.Size = new System.Drawing.Size(597, 20);
            this.txtKey.TabIndex = 1;
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.Location = new System.Drawing.Point(13, 268);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(118, 23);
            this.btnEncrypt.TabIndex = 2;
            this.btnEncrypt.Text = "Encrypt";
            this.btnEncrypt.UseVisualStyleBackColor = true;
            this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.Location = new System.Drawing.Point(491, 268);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(118, 23);
            this.btnDecrypt.TabIndex = 3;
            this.btnDecrypt.Text = "Decrypt";
            this.btnDecrypt.UseVisualStyleBackColor = true;
            this.btnDecrypt.Click += new System.EventHandler(this.btnDecrypt_Click);
            // 
            // btnReadFromGoogle
            // 
            this.btnReadFromGoogle.Location = new System.Drawing.Point(13, 297);
            this.btnReadFromGoogle.Name = "btnReadFromGoogle";
            this.btnReadFromGoogle.Size = new System.Drawing.Size(118, 23);
            this.btnReadFromGoogle.TabIndex = 4;
            this.btnReadFromGoogle.Text = "Read from Google";
            this.btnReadFromGoogle.UseVisualStyleBackColor = true;
            this.btnReadFromGoogle.Click += new System.EventHandler(this.btnReadFromGoogle_Click);
            // 
            // btnWriteToGoogle
            // 
            this.btnWriteToGoogle.Location = new System.Drawing.Point(491, 297);
            this.btnWriteToGoogle.Name = "btnWriteToGoogle";
            this.btnWriteToGoogle.Size = new System.Drawing.Size(118, 23);
            this.btnWriteToGoogle.TabIndex = 5;
            this.btnWriteToGoogle.Text = "Write to Google";
            this.btnWriteToGoogle.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 325);
            this.Controls.Add(this.btnWriteToGoogle);
            this.Controls.Add(this.btnReadFromGoogle);
            this.Controls.Add(this.btnDecrypt);
            this.Controls.Add(this.btnEncrypt);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.txtSource);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.Button btnEncrypt;
        private System.Windows.Forms.Button btnDecrypt;
        private System.Windows.Forms.Button btnReadFromGoogle;
        private System.Windows.Forms.Button btnWriteToGoogle;
    }
}

