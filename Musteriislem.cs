using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bankaproje
{
    public partial class Musteriislem : Form
    {
        public Musteriislem()
        {
            InitializeComponent();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.Show();
            this.Close();


        }

        private void Musteriislem_Load(object sender, EventArgs e)
        {
            label2.Text = Form1.adSoyad;
            label3.Text = Form1.mId.ToString();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            ParaCek pc = new ParaCek();
            pc.Show();
        }

        private void btnParaYatır_Click(object sender, EventArgs e)
        {
            ParaYatır py = new ParaYatır();
            py.Show();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            Bakiye b = new Bakiye();
            b.Show();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            Havale h = new Havale();
            h.Show();
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            SifreDegistirme sd = new SifreDegistirme();
            sd.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HesapHareket hh = new HesapHareket();
            hh.Show();
        }
    }
}
