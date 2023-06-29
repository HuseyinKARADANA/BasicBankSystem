using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankaOtomasyon
{
    public partial class musteriAnasayfa : Form
    {
        public musteriAnasayfa()
        {
            InitializeComponent();
        }
        public string TCNo;
        int id;
        sqlBaglantisi bgl=new sqlBaglantisi();
        public void BilgileriListele()
        {
            OleDbCommand komut = new OleDbCommand("SELECT * FROM TblMusteriler WHERE TCno=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TCNo);
            OleDbDataReader dr = komut.ExecuteReader();

            while (dr.Read())
            {
                lblAd.Text = dr[1].ToString();
                lblSoyad.Text = dr[2].ToString();
                id =Convert.ToInt32(dr[0].ToString());
            }
            bgl.baglanti().Close();
        }

        public void Listele()
        {
            DataTable dt = new DataTable();
            OleDbCommand komut = new OleDbCommand("SELECT * FROM TblIslemler WHERE musteriId=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", id);
            OleDbDataAdapter da = new OleDbDataAdapter(komut);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            bgl.baglanti().Close();
        }
        private void musteriAnasayfa_Load(object sender, EventArgs e)
        {
            lblTC.Text = TCNo;
            BilgileriListele();
            Listele();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hesaplar frm=new Hesaplar();
            frm.musteriId = id;
            frm.musteriTC = TCNo;
            frm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            krediKartları frm=new krediKartları();
            frm.musteriId = id;
            frm.musteriTC = TCNo;
            frm.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Giris frm=new Giris();
            frm.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Faturalar frm=new Faturalar();
            frm.musteriId = id;
            frm.musteriTC = TCNo;
            frm.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Islemler frm=new Islemler();
            frm.musteriId = id;
            frm.musteriTC = TCNo;
            frm.Show();
            this.Hide();
        }
    }
}
