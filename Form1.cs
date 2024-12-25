using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace bankaproje
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        SqlConnection con = new SqlConnection("server=DESKTOP-7T00QCH\\SQLEXPRESS; initial catalog = bankaotomasyonu; integrated security = sspi");
        public static string adSoyad = "";
        public static int mId=0;
        public static float mBakiye=0.0f;
        private void Form1_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            string kAdi = textBox1.Text;
            string parola = textBox2.Text;
            bool sonuc = false;

            if (radioButton2.Checked)
            {
                if (kAdi == "admin" && parola == "1")
                {
                    Yetkilislem yi = new Yetkilislem();
                    yi.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Hatalı Kullanıcı Adı veya Parola!", "Hatalı Giriş Denemesi");
                }
            }
            else
            {
                con.Open();
                SqlCommand komut = new SqlCommand("SELECT * FROM musteriler WHERE tcNo=@p1 AND sifre=@p2 AND durum=1", con);
                komut.Parameters.AddWithValue("@p1", kAdi);
                komut.Parameters.AddWithValue("@p2", parola);

                SqlDataReader dr = komut.ExecuteReader();
                if (dr.Read()) // Kullanıcı bulunduysa
                {
                    adSoyad = dr["adSoyad"].ToString();
                    mId = int.Parse(dr["ID"].ToString());
                    if (dr["bakiye"] != DBNull.Value && !string.IsNullOrEmpty(dr["bakiye"].ToString()))
                    {
                        mBakiye = float.Parse(dr["bakiye"].ToString());
                    }
                    sonuc = true; // Giriş başarılı
                }
                else
                {
                    MessageBox.Show("Hatalı Kullanıcı Adı veya Parola!", "Hatalı Giriş Denemesi");
                }

                con.Close();
            }

            if (sonuc) // Sadece başarılı girişlerde müşteri işlem ekranı açılır
            {
                Musteriislem mi = new Musteriislem();
                mi.Show();
                this.Hide();
            }

            textBox1.Text = "";
            textBox2.Text = "";


        }
        
            
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}
