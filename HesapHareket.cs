using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bankaproje
{
    public partial class HesapHareket : Form
    {
        public HesapHareket()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("server=DESKTOP-7T00QCH\\SQLEXPRESS; initial catalog = bankaotomasyonu; integrated security = sspi");


        private void HesapHareket_Load(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select * from hareketler where musteriID=@p1", con);
            komut.Parameters.AddWithValue("@p1", Form1.mId);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;



        }
    }
}
