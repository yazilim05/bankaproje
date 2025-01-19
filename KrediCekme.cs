using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bankaproje
{
    public partial class KrediCekme : Form
    {
        public KrediCekme()
        {
            InitializeComponent();
        }
        string conString = "server=DESKTOP-7T00QCH\\SQLEXPRESS; initial catalog = bankaotomasyonu; integrated security = sspi";

        private void btnKrediCek_Click(object sender, EventArgs e)
        {
            decimal krediMiktari = 0;
            if (!decimal.TryParse(txtKrediMiktari.Text, out krediMiktari) || krediMiktari <= 0)
            {
                MessageBox.Show("Lütfen geçerli bir kredi miktarı giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Belirli kredi limit kontrolü
            decimal minKredi = 1000;  // Minimum çekilebilecek kredi
            decimal maxKredi = 50000; // Maksimum çekilebilecek kredi

            if (krediMiktari < minKredi || krediMiktari > maxKredi)
            {
                MessageBox.Show($"Kredi talebi yalnızca {minKredi:C} ile {maxKredi:C} arasında olabilir.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Müşteri ID'sini al (örneğin, kullanıcı giriş yaptıktan sonra bu değer alınır)
            int musteriID = Form1.mId;

            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();

                    // Müşterinin kredi limiti veritabanından alınır
                    SqlCommand limitKomut = new SqlCommand("SELECT krediLimiti FROM musteriler WHERE ID = @musteriID", con);
                    limitKomut.Parameters.AddWithValue("@musteriID", musteriID);
                    object result = limitKomut.ExecuteScalar();

                    if (result == null)
                    {
                        MessageBox.Show("Kredi limiti bilgisine ulaşılamadı. Lütfen sistem yöneticisine başvurun.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    decimal krediLimiti = Convert.ToDecimal(result);

                    // Talep edilen kredi miktarı müşteri limitini aşıyor mu?
                    if (krediMiktari > krediLimiti)
                    {
                        MessageBox.Show($"Kredi talebiniz başarısız oldu. Maksimum talep edebileceğiniz kredi miktarı: {krediLimiti:C}.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Kredi talebini veritabanına ekle
                    SqlCommand cmd = new SqlCommand("INSERT INTO kredi_talepleri (musteriID, krediMiktari, talepDurumu) VALUES (@musteriID, @krediMiktari, 'Beklemede')", con);
                    cmd.Parameters.AddWithValue("@musteriID", musteriID);
                    cmd.Parameters.AddWithValue("@krediMiktari", krediMiktari);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Kredi talebiniz başarıyla gönderildi. Onay süreci başlatılacaktır.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtKrediMiktari.Clear(); // TextBox'ı temizle
                    }
                    else
                    {
                        MessageBox.Show("Kredi talebiniz gönderilemedi. Lütfen tekrar deneyin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        private void maskedTextBoxKrediMiktari_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}
