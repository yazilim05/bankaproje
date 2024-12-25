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
    public partial class ParaYatır : Form
    {
        public ParaYatır()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("server=DESKTOP-7T00QCH\\SQLEXPRESS; initial catalog = bankaotomasyonu; integrated security = sspi");


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Para yatırılacak miktarı al
                float sayi = float.Parse(maskedTextBox1.Text);

                // Girilen miktarın geçerliliğini kontrol et
                if (sayi < 10)
                {
                    MessageBox.Show("En az 10 TL yatırabilirsiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Mevcut bakiye için veritabanından kullanıcıyı kontrol et
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

                // Bakiyeyi güncelle
                con.Open();
                SqlCommand guncelleKomut = new SqlCommand("UPDATE musteriler SET bakiye = bakiye + @p1 WHERE ID = @p2", con);
                guncelleKomut.Parameters.AddWithValue("@p1", sayi);
                guncelleKomut.Parameters.AddWithValue("@p2", Form1.mId);
                int sonuc = guncelleKomut.ExecuteNonQuery();
                con.Close();

                if (sonuc == 1)
                {
                    MessageBox.Show("Para yatırma işlemi başarıyla gerçekleştirildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    maskedTextBox1.Text = "";
                    HareketKaydet.kaydet(Form1.mId,sayi +( "TL Para Yatırıldı"));

                }
                else
                {
                    MessageBox.Show("Para yatırma işlemi başarısız!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
