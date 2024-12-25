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
    public partial class MüsteriAra : Form
    {
        public MüsteriAra()
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
    }
}
