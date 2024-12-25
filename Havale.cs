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
    public partial class Havale : Form
    {
        public Havale()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("server=DESKTOP-7T00QCH\\SQLEXPRESS; initial catalog = bankaotomasyonu; integrated security = sspi");


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Gönderilecek miktarı al
                float miktar = float.Parse(txtMiktar.Text);
                string aliciHesapNo = txtNo.Text; // Alıcı hesap numarası TextBox'tan alınır
                float havaleUcreti = 5.0f; // Havale ücreti

                // Girilen miktarın geçerli olup olmadığını kontrol et
                if (miktar <= 0)
                {
                    MessageBox.Show("Lütfen geçerli bir miktar giriniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Gönderici bakiyesini kontrol etmek için veritabanından mevcut bakiyeyi çek
                con.Open();
                SqlCommand bakiyeKomut = new SqlCommand("SELECT bakiye FROM musteriler WHERE ID = @p1", con);
                bakiyeKomut.Parameters.AddWithValue("@p1", Form1.mId);
                object result = bakiyeKomut.ExecuteScalar();
                con.Close();

                if (result == null)
                {
                    MessageBox.Show("Gönderici kullanıcı bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                float mevcutBakiye = Convert.ToSingle(result);

                // Yetersiz bakiye kontrolü (gönderilecek miktar + havale ücreti)
                if (miktar + havaleUcreti > mevcutBakiye)
                {
                    MessageBox.Show("Yetersiz bakiye! Havale ücreti dahil toplam işlem miktarı bakiyenizi aşıyor.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Alıcı hesabını kontrol et
                con.Open();
                SqlCommand aliciKomut = new SqlCommand("SELECT ID FROM musteriler WHERE ID = @p1", con);
                aliciKomut.Parameters.AddWithValue("@p1", aliciHesapNo);
                object aliciIdResult = aliciKomut.ExecuteScalar();
                con.Close();

                if (aliciIdResult == null)
                {
                    MessageBox.Show("Alıcı hesap numarası bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int aliciId = Convert.ToInt32(aliciIdResult);

                // Transaction başlat
                SqlTransaction transaction = null;
                try
                {
                    con.Open();
                    transaction = con.BeginTransaction();

                    // Göndericinin bakiyesini azalt (miktar + havale ücreti)
                    SqlCommand gondericiKomut = new SqlCommand("UPDATE musteriler SET bakiye = bakiye - @p1 WHERE ID = @p2", con, transaction);
                    gondericiKomut.Parameters.AddWithValue("@p1", miktar + havaleUcreti);
                    gondericiKomut.Parameters.AddWithValue("@p2", Form1.mId);
                    gondericiKomut.ExecuteNonQuery();

                    // Alıcının bakiyesini artır (sadece gönderilecek miktar)
                    SqlCommand aliciGuncelleKomut = new SqlCommand("UPDATE musteriler SET bakiye = bakiye + @p1 WHERE ID = @p2", con, transaction);
                    aliciGuncelleKomut.Parameters.AddWithValue("@p1", miktar);
                    aliciGuncelleKomut.Parameters.AddWithValue("@p2", aliciId);
                    aliciGuncelleKomut.ExecuteNonQuery();

                    // Transaction'ı başarılı olarak işaretle
                    transaction.Commit();

                    MessageBox.Show("EFT/Havale işlemi başarıyla gerçekleştirildi. 5 TL havale ücreti kesilmiştir.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    HareketKaydet.kaydet(Form1.mId,(miktar + "TL Havale gönderildi"));
                    HareketKaydet.kaydet(int.Parse(txtNo.Text),(miktar + "TL Havale alındı"));


                    txtMiktar.Text = "";
                    txtNo.Text = "";
                }
                catch (Exception ex)
                {
                    // Hata durumunda transaction'ı geri al
                    transaction?.Rollback();
                    MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    con.Close();
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
