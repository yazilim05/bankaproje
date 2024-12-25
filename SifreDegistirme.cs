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
    public partial class SifreDegistirme : Form
    {
        public SifreDegistirme()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("server=DESKTOP-7T00QCH\\SQLEXPRESS; initial catalog = bankaotomasyonu; integrated security = sspi");


        private void button1_Click(object sender, EventArgs e)
        {
            if (txtEski.Text == "" || txtYeni.Text == "")
            {
                MessageBox.Show("Lütfen alanları giriniz", "Şifre Değiştirme İşlemi");
            }
            else if (txtYeni.Text.Length < 5)
            {
                MessageBox.Show("En az 5 karakter uzunluğunda bir şifre belirleyiniz", "Şifre Değiştirme İşlemi");
            }
            else
            {
                SqlCommand komut = new SqlCommand("UPDATE musteriler SET sifre = @p1 WHERE sifre = @p2 AND ID = @p3", con);

                komut.Parameters.AddWithValue("@p1", txtYeni.Text); // Yeni şifre
                komut.Parameters.AddWithValue("@p2", txtEski.Text); // Eski şifre
                komut.Parameters.AddWithValue("@p3", Form1.mId);  // Kullanıcının ID'si (oturumdan alınmalı)

                try
                {
                    con.Open();
                    int sonuc = komut.ExecuteNonQuery();

                    if (sonuc == 1)
                    {
                        MessageBox.Show("Şifre değiştirme işlemi başarılı", "Şifre Değiştirme İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        HareketKaydet.kaydet(Form1.mId, "Şifre değiştirildi");

                    }
                    else
                    {
                        MessageBox.Show("Eski şifre hatalı veya kullanıcı bulunamadı!", "Şifre Değiştirme İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}", "Şifre Değiştirme İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    con.Close();
                }

                txtEski.Text = "";
                txtYeni.Text = "";
            }

        }


    }

}