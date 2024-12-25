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
    public partial class Bakiye : Form
    {
        public Bakiye()
        {
            InitializeComponent();
        }

        private void Bakiye_Load(object sender, EventArgs e)
        {
            try
            {
                // Veritabanından güncel bakiyeyi çek
                SqlConnection con = new SqlConnection("server=DESKTOP-7T00QCH\\SQLEXPRESS; initial catalog=bankaotomasyonu; integrated security=sspi");
                SqlCommand bakiyeKomut = new SqlCommand("SELECT bakiye FROM musteriler WHERE ID = @p1", con);
                bakiyeKomut.Parameters.AddWithValue("@p1", Form1.mId);

                con.Open();
                object result = bakiyeKomut.ExecuteScalar();
                con.Close();

                if (result != null)
                {
                    // Form1.mBakiye'yi güncel bakiye ile güncelle
                    Form1.mBakiye = Convert.ToSingle(result);
                    lblBakiye.Text = Form1.mBakiye.ToString("0.00") + " TL";
                }
                else
                {
                    MessageBox.Show("Kullanıcı bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            HareketKaydet.kaydet(Form1.mId, "Bakiye Sorgulandı");

        }
    }
}
