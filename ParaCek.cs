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
    public partial class ParaCek : Form
    {
        public ParaCek()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("server=DESKTOP-7T00QCH\\SQLEXPRESS; initial catalog = bankaotomasyonu; integrated security = sspi");


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Para çekilecek miktarı al
                float sayi = float.Parse(maskedTextBox1.Text);

                // Bakiye kontrolü için veritabanından mevcut bakiyeyi çek
                con.Open();
                SqlCommand bakiyeKomut = new SqlCommand("SELECT bakiye FROM musteriler WHERE ID = @p1", con);
                bakiyeKomut.Parameters.AddWithValue("@p1", Form1.mId);
                object result = bakiyeKomut.ExecuteScalar();
                con.Close();

                if (result == null)
                {
                    MessageBox.Show("Kullanıcı bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                float mevcutBakiye = Convert.ToSingle(result);

                // Yetersiz bakiye kontrolü
                if (sayi > mevcutBakiye)
                {
                    MessageBox.Show("Yetersiz Bakiye", "Para Çekme İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Bakiyeyi güncelle
                con.Open();
                SqlCommand guncelleKomut = new SqlCommand("UPDATE musteriler SET bakiye = bakiye - @p1 WHERE ID = @p2", con);
                guncelleKomut.Parameters.AddWithValue("@p1", sayi);
                guncelleKomut.Parameters.AddWithValue("@p2", Form1.mId);
                int sonuc = guncelleKomut.ExecuteNonQuery();
                con.Close();

                if (sonuc == 1)
                {
                    MessageBox.Show("Para çekme işlemi başarıyla gerçekleştirildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    maskedTextBox1.Text = "";
                    HareketKaydet.kaydet(Form1.mId,sayi +("TL Para Çekildi"));
                }
                else
                {
                    MessageBox.Show("Para çekme işlemi başarısız!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Geçerli bir miktar giriniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
    }
}
