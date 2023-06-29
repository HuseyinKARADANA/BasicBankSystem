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
    public partial class Islemler : Form
    {
        public Islemler()
        {
            InitializeComponent();
        }
        sqlBaglantisi bgl=new sqlBaglantisi();
        public int musteriId, islemId = -1;
        public string musteriTC;

        public void Temizle()
        {
            txtAliciHesap.Text = "";
            cbHesaplar.SelectedIndex = -1;
            txtTutar.Text = "";
            lblBakiye.Text = "";
            lblHesapTipi.Text = "";
            islemId = -1;
            cbIslemTuru.SelectedIndex= -1;
            txtAciklama.Text = "";
        }
        public void Listele()
        {
            DataTable dt = new DataTable();
            OleDbCommand komut = new OleDbCommand("SELECT * FROM TblIslemler WHERE musteriId=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", musteriId);
            OleDbDataAdapter da = new OleDbDataAdapter(komut);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            bgl.baglanti().Close();
        }
        private void cbHesaplar_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                OleDbCommand komut = new OleDbCommand("SELECT  hesapTipi,bakiye FROM TblHesaplar WHERE hesapId=@p1", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", cbHesaplar.SelectedValue);
                OleDbDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    lblHesapTipi.Text = dr[0].ToString();
                    lblBakiye.Text = dr[1].ToString();
                }
                bgl.baglanti().Close();
            }
            catch (Exception ex)
            {

            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            musteriAnasayfa frm = new musteriAnasayfa();
            frm.TCNo = musteriTC;
            frm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int cekilen =int.Parse(txtTutar.Text);
            string aliciHesap=txtAliciHesap.Text;
            string gonderen = cbHesaplar.Text;
            string islemTuru = cbIslemTuru.Text;
            string  aciklama=txtAciklama.Text;
            if (cekilen < int.Parse(lblBakiye.Text) && !aliciHesap.Equals(""))
            {

                OleDbCommand komut = new OleDbCommand("UPDATE TblHesaplar SET bakiye=bakiye-@p1 WHERE hesapId=@p2", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1",cekilen);
                komut.Parameters.AddWithValue("@p2", cbHesaplar.SelectedValue);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                OleDbCommand komut2 = new OleDbCommand("UPDATE TblHesaplar SET bakiye = bakiye+@q1 WHERE hesapNo = @q2", bgl.baglanti());
                komut2.Parameters.AddWithValue("@q1",  cekilen);
                komut2.Parameters.AddWithValue("@q2", aliciHesap);
                komut2.ExecuteNonQuery();
                bgl.baglanti().Close();
                OleDbCommand komut3 = new OleDbCommand("INSERT INTO TblIslemler (musteriId,aliciHesapNo,tarih,islemTuru,tutar,aciklama) VALUES (@s1,@s2,@s3,@s4,@s5,@s6)", bgl.baglanti());
                komut3.Parameters.AddWithValue("@s1", musteriId);
                komut3.Parameters.AddWithValue("@s2", aliciHesap);
                komut3.Parameters.AddWithValue("@s3", DateTime.Now);
                komut3.Parameters.AddWithValue("@s4",islemTuru);
                komut3.Parameters.AddWithValue("@s5",cekilen);
                komut3.Parameters.AddWithValue("@s6",aciklama);
                komut3.ExecuteNonQuery();
                Temizle();
                MessageBox.Show("Para başarıyla gönderildi.", "İşlem Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Listele();
                bgl.baglanti().Close();
            }
        }

        private void Islemler_Load(object sender, EventArgs e)
        {
            OleDbCommand komut=new OleDbCommand("Select * From TblHesaplar WHERE musteriId=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", musteriId);
            OleDbDataAdapter da = new OleDbDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cbHesaplar.ValueMember = "hesapId";
            cbHesaplar.DisplayMember = "hesapNo";
            cbHesaplar.DataSource = dt;
            bgl.baglanti().Close();

            OleDbCommand komut2 = new OleDbCommand("SELECT * FROM TblIslemTuru",bgl.baglanti());
            OleDbDataReader dr=komut2.ExecuteReader();
            while (dr.Read())
            {
                cbIslemTuru.Items.Add(dr[1].ToString());
            }
            Listele();
        }
    }
}
