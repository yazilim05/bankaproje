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
    public partial class MusteriGuncelle : Form
    {
        public MusteriGuncelle()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("server=DESKTOP-7T00QCH\\SQLEXPRESS; initial catalog = bankaotomasyonu; integrated security = sspi");

        private void button1_Click(object sender, EventArgs e)
        {

            SqlCommand komut = new SqlCommand("select * from musteriler where ID=@p1 or tcNo=@p2", con);
            komut.Parameters.AddWithValue("@p1", txtAra.Text);
            komut.Parameters.AddWithValue("@p2", txtAra.Text);
            con.Open();
            SqlDataReader dr = komut.ExecuteReader();


            if (dr.Read())
            {
                txtID.Text = dr["ID"].ToString();
                txtTcNo.Text = dr["tcNo"].ToString();
                txtAdSoyad.Text = dr["adSoyad"].ToString();
                txtAdres.Text = dr["adres"].ToString();
                maskedTextBox1.Text = dr["telefon"].ToString();
                txtBakiye.Text = dr["bakiye"].ToString();
            }
            else
            {

                MessageBox.Show(txtID.Text + "Numaralı Kayıt Bulunamadı !", "Kayıt Arama", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                txtID.Text = "";
                txtTcNo.Text = "";
                txtAdSoyad.Text = "";
                txtAdres.Text = "";
                maskedTextBox1.Text = "";
                txtBakiye.Text = "";



            }

            con.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update  musteriler set adSoyad=@p1,adres=@p2,telefon=@p3 where ID=@p4 or tcNo=@p5", con);
            komut.Parameters.AddWithValue("@p4", txtAra.Text);
            komut.Parameters.AddWithValue("@p5", txtAra.Text);
            komut.Parameters.AddWithValue("@p1", txtAdSoyad.Text);
            komut.Parameters.AddWithValue("@p2", txtAdres.Text);
            komut.Parameters.AddWithValue("@p3", maskedTextBox1.Text);

            con.Open();
            int sonuc = komut.ExecuteNonQuery();



            if (sonuc == 1)
            {
                MessageBox.Show("Güncelleme yapıldı ", "Güncelleme işlemi ", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {

                MessageBox.Show("Güncelleme yapılamadı !", "Güncelleme işlemi ", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                txtID.Text = "";
                txtTcNo.Text = "";
                txtAdSoyad.Text = "";
                txtAdres.Text = "";
                maskedTextBox1.Text = "";
                txtBakiye.Text = "";



            }

            con.Close();

        }
    }
}
