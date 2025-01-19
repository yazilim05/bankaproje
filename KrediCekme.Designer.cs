namespace bankaproje
{
    partial class KrediCekme
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtKrediMiktari = new System.Windows.Forms.MaskedTextBox();
            this.btnKrediCek = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(158, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Kredi miktarını girin";
            // 
            // txtKrediMiktari
            // 
            this.txtKrediMiktari.Location = new System.Drawing.Point(369, 112);
            this.txtKrediMiktari.Mask = "0000000";
            this.txtKrediMiktari.Name = "txtKrediMiktari";
            this.txtKrediMiktari.Size = new System.Drawing.Size(148, 22);
            this.txtKrediMiktari.TabIndex = 1;
            this.txtKrediMiktari.ValidatingType = typeof(int);
            this.txtKrediMiktari.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.maskedTextBoxKrediMiktari_MaskInputRejected);
            // 
            // btnKrediCek
            // 
            this.btnKrediCek.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnKrediCek.Location = new System.Drawing.Point(369, 169);
            this.btnKrediCek.Name = "btnKrediCek";
            this.btnKrediCek.Size = new System.Drawing.Size(148, 39);
            this.btnKrediCek.TabIndex = 2;
            this.btnKrediCek.Text = "Talep oluştur";
            this.btnKrediCek.UseVisualStyleBackColor = true;
            this.btnKrediCek.Click += new System.EventHandler(this.btnKrediCek_Click);
            // 
            // KrediCekme
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnKrediCek);
            this.Controls.Add(this.txtKrediMiktari);
            this.Controls.Add(this.label1);
            this.Name = "KrediCekme";
            this.Text = "KrediCekme";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox txtKrediMiktari;
        private System.Windows.Forms.Button btnKrediCek;
    }
}