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
    public partial class CalisanKrediOnay : Form
    {
        public CalisanKrediOnay()
        {
            InitializeComponent();
        }

        private void CalisanKrediOnay_Load(object sender, EventArgs e)
        {
            KrediTalepleriniListele();
        }
        private void KrediTalepleriniListele()
        {
            // Veritabanından bekleyen kredi taleplerini al
            SqlConnection con = new SqlConnection("server=DESKTOP-7T00QCH\\SQLEXPRESS; initial catalog = bankaotomasyonu; integrated security = sspi");
            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT talepID, musteriID, krediMiktari, talepDurumu FROM kredi_talepleri WHERE talepDurumu = 'Beklemede'", con);
            SqlDataReader dr = cmd.ExecuteReader();

            // DataGridView'e veri yükle
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvKrediTalepleri.DataSource = dt;

            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dgvKrediTalepleri.SelectedRows.Count > 0)
            {
                int talepID = Convert.ToInt32(dgvKrediTalepleri.SelectedRows[0].Cells[0].Value);
                Onayla(talepID);
            }
            else
            {
                MessageBox.Show("Lütfen bir kredi talebi seçin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dgvKrediTalepleri.SelectedRows.Count > 0)
            {
                int talepID = Convert.ToInt32(dgvKrediTalepleri.SelectedRows[0].Cells[0].Value);
                Reddet(talepID);
            }
            else
            {
                MessageBox.Show("Lütfen bir kredi talebi seçin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }
        private void Onayla(int talepID)
        {
            // Onay işlemi
            SqlConnection con = new SqlConnection("server=DESKTOP-7T00QCH\\SQLEXPRESS; initial catalog = bankaotomasyonu; integrated security = sspi");
            con.Open();

            // Kredi talebini onayla
            SqlCommand cmd = new SqlCommand("UPDATE kredi_talepleri SET talepDurumu = 'Onaylandı' WHERE talepID = @talepID", con);
            cmd.Parameters.AddWithValue("@talepID", talepID);
            cmd.ExecuteNonQuery();

            // Kredi miktarını müşterinin hesabına ekle
            SqlCommand guncelleBakiyeCmd = new SqlCommand("UPDATE musteriler SET bakiye = bakiye + (SELECT krediMiktari FROM kredi_talepleri WHERE talepID = @talepID) WHERE ID = (SELECT musteriID FROM kredi_talepleri WHERE talepID = @talepID)", con);
            guncelleBakiyeCmd.Parameters.AddWithValue("@talepID", talepID);
            guncelleBakiyeCmd.ExecuteNonQuery();

            con.Close();

            // Başarı mesajı
            MessageBox.Show("Kredi başvurusu onaylandı ve müşterinin hesabına eklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Kredi taleplerini yeniden listele
            KrediTalepleriniListele();
        }

        private void Reddet(int talepID)
        {
            // Reddet işlemi
            SqlConnection con = new SqlConnection("server=DESKTOP-7T00QCH\\SQLEXPRESS; initial catalog = bankaotomasyonu; integrated security = sspi");
            con.Open();

            // Kredi talebini reddet
            SqlCommand cmd = new SqlCommand("UPDATE kredi_talepleri SET talepDurumu = 'Reddedildi' WHERE talepID = @talepID", con);
            cmd.Parameters.AddWithValue("@talepID", talepID);
            cmd.ExecuteNonQuery();

            con.Close();

            // Başarı mesajı
            MessageBox.Show("Kredi başvurusu reddedildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Kredi taleplerini yeniden listele
            KrediTalepleriniListele();
        }
    }
}
