namespace bankaproje
{
    partial class CalisanKrediOnay
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
            this.dgvKrediTalepleri = new System.Windows.Forms.DataGridView();
            this.talepID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.musteriID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.krediMiktari = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.talepDurumu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKrediTalepleri)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvKrediTalepleri
            // 
            this.dgvKrediTalepleri.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKrediTalepleri.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.talepID,
            this.musteriID,
            this.krediMiktari,
            this.talepDurumu});
            this.dgvKrediTalepleri.Location = new System.Drawing.Point(120, 36);
            this.dgvKrediTalepleri.Name = "dgvKrediTalepleri";
            this.dgvKrediTalepleri.RowHeadersWidth = 51;
            this.dgvKrediTalepleri.RowTemplate.Height = 24;
            this.dgvKrediTalepleri.Size = new System.Drawing.Size(565, 314);
            this.dgvKrediTalepleri.TabIndex = 0;
            // 
            // talepID
            // 
            this.talepID.HeaderText = "Talep ID";
            this.talepID.MinimumWidth = 6;
            this.talepID.Name = "talepID";
            this.talepID.Width = 125;
            // 
            // musteriID
            // 
            this.musteriID.HeaderText = " Müşteri ID";
            this.musteriID.MinimumWidth = 6;
            this.musteriID.Name = "musteriID";
            this.musteriID.Width = 125;
            // 
            // krediMiktari
            // 
            this.krediMiktari.HeaderText = "Kredi Miktarı";
            this.krediMiktari.MinimumWidth = 6;
            this.krediMiktari.Name = "krediMiktari";
            this.krediMiktari.Width = 125;
            // 
            // talepDurumu
            // 
            this.talepDurumu.HeaderText = " Talep Durumu";
            this.talepDurumu.MinimumWidth = 6;
            this.talepDurumu.Name = "talepDurumu";
            this.talepDurumu.Width = 125;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(456, 385);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(123, 42);
            this.button1.TabIndex = 1;
            this.button1.Text = "Onayla";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(610, 385);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(124, 42);
            this.button2.TabIndex = 2;
            this.button2.Text = "Reddet";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // CalisanKrediOnay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dgvKrediTalepleri);
            this.Name = "CalisanKrediOnay";
            this.Text = "CalisanKrediOnay";
            this.Load += new System.EventHandler(this.CalisanKrediOnay_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKrediTalepleri)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvKrediTalepleri;
        private System.Windows.Forms.DataGridViewTextBoxColumn talepID;
        private System.Windows.Forms.DataGridViewTextBoxColumn musteriID;
        private System.Windows.Forms.DataGridViewTextBoxColumn krediMiktari;
        private System.Windows.Forms.DataGridViewTextBoxColumn talepDurumu;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}