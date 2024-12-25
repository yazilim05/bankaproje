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
    public partial class MusteriEkle : Form
    {
        public MusteriEkle()
        {
            InitializeComponent();

          
        }
        private MaskedTextBox maskedTextBoxTelefon;
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        SqlConnection con = new SqlConnection("server=DESKTOP-7T00QCH\\SQLEXPRESS; initial catalog = bankaotomasyonu; integrated security = sspi");
        private void button1_Click(object sender, EventArgs e)
        {

            string connectionString = "Server=DESKTOP-7T00QCH\\SQLEXPRESS;;Database=bankaotomasyonu;Trusted_Connection=True;";

            string query = "INSERT INTO Musteriler (tcNo, AdSoyad, Adres, Bakiye, Telefon) VALUES (@tc, @adSoyad, @adres, @bakiye, @telefon)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Veri türlerine dikkat ederek parametreleri ekleyin
                        command.Parameters.AddWithValue("@tc", txtTcNo.Text);
                        command.Parameters.AddWithValue("@adSoyad", txtAdSoyad.Text);
                        command.Parameters.AddWithValue("@adres", txtAdres.Text);

                        // Bakiye decimal olduğu için dönüşüm yapılmalı
                        decimal bakiye = decimal.Parse(txtBakiye.Text);
                        command.Parameters.AddWithValue("@bakiye", bakiye);

                        // Telefon numarası metin türünde olabilir
                        command.Parameters.AddWithValue("@telefon", maskedTextBox1.Text);

                        int rowsAffected = command.ExecuteNonQuery();
                        MessageBox.Show($"{rowsAffected} kayıt başarıyla eklendi.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Bir hata oluştu: {ex.Message}");
                }
            }
            SqlCommand komut = new SqlCommand("insert into musteriler (tcNo,adSoyad,adres,telefon,sifre,bakiye,durum) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7)", con);
            komut.Parameters.AddWithValue("@p1", txtTcNo);
            komut.Parameters.AddWithValue("@p2", txtAdSoyad);
            komut.Parameters.AddWithValue("@p3", txtAdres); 
            komut.Parameters.AddWithValue("@p4", maskedTextBox1); 
            komut.Parameters.AddWithValue("@p5", txtTcNo);
            komut.Parameters.AddWithValue("@p6", txtBakiye); 
            komut.Parameters.AddWithValue("@p7", 1);

            

            if (txtTcNo.Text == "" || txtAdSoyad.Text == "" || txtAdres.Text == "" || maskedTextBox1.Text == "") //|| txtBakiye.Text == ""//
            {
                MessageBox.Show("Tüm Alanları Eksiksiz Giriniz!", "Musteri Kayıt Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                con.Open();
                int sonuc = komut.ExecuteNonQuery();
                con.Close();
                if (sonuc == 1)
                {
                    MessageBox.Show("Yeni Musteri Kaydi Tamam", "Musteri Kaydı");
                }

                else
                {
                    MessageBox.Show("Yeni Kayit Yapılamadı !", "Musteri Kayıt Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            txtTcNo.Text = "";
            txtAdSoyad.Text = "";
            txtAdres.Text = "";
            maskedTextBox1.Text = "";
            txtBakiye.Text = "";




        }
          

              

        

        private void MusteriEkle_Load(object sender, EventArgs e)
        {
            
        }
    }
}
