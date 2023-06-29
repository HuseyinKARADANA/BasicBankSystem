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
    public partial class Faturalar : Form
    {
        public Faturalar()
        {
            InitializeComponent();
        }
        public int musteriId, faturaId = -1;
        public string musteriTC;
        sqlBaglantisi bgl = new sqlBaglantisi();

        public void Listele()
        {
            DataTable dt = new DataTable();
            OleDbCommand komut = new OleDbCommand("SELECT faturaTarihi,faturaTipi,tutar,odemeDurumu FROM TblFaturalar WHERE musteriId=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", musteriId);
            OleDbDataAdapter da = new OleDbDataAdapter(komut);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            bgl.baglanti().Close();
        }


        public void Temizle()
        {
            txtFaturaTarihi.Text = "";
            txtTutar.Text = "";
            cbFaturaTipi.SelectedIndex=-1;
            cbOdeme.SelectedIndex = -1;
            faturaId = -1;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string faturaTipi = cbFaturaTipi.Text;
            string tutar = txtTutar.Text;
            string odeme = cbOdeme.Text;
            string faturaTarihi = txtFaturaTarihi.Text;


            if (cbFaturaTipi.SelectedIndex == -1 || cbOdeme.SelectedIndex==-1|| tutar.Equals("") || faturaTarihi.Equals(""))
            {
                MessageBox.Show("Bütün bilgileri doldurunuz.");
            }
            else
            {
                OleDbCommand komut = new OleDbCommand("Insert Into TblFaturalar (musteriId,faturaTarihi,faturaTipi,tutar,odemeDurumu) VALUES (@p1,@p2,@p3,@p4,@p5)", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", musteriId);
                komut.Parameters.AddWithValue("@p2", faturaTarihi);
                komut.Parameters.AddWithValue("@p3", faturaTipi);
                komut.Parameters.AddWithValue("@p4", tutar);
                komut.Parameters.AddWithValue("@p5", odeme);
                komut.ExecuteNonQuery();
                Temizle();
                MessageBox.Show("Fatura başarıyla oluşturuldu.", "Kayıt Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                bgl.baglanti().Close();
            }
            Listele();
            Temizle();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            string faturaTipi = cbFaturaTipi.Text;
            string tutar = txtTutar.Text;
            string odeme = cbOdeme.Text;
            string faturaTarihi = txtFaturaTarihi.Text;


            if (cbFaturaTipi.SelectedIndex == -1 || cbOdeme.SelectedIndex == -1 || tutar.Equals("") || faturaTarihi.Equals(""))
            {
                MessageBox.Show("Bütün bilgileri doldurunuz.");
            }
            else
            {
                OleDbCommand komut = new OleDbCommand("UPDATE TblFaturalar SET musteriId=@p1,faturaTarihi=@p2,faturaTipi=@p3,tutar=@p4,odemeDurumu=@p5 WHERE faturaId=@p6", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", musteriId);
                komut.Parameters.AddWithValue("@p2", faturaTarihi);
                komut.Parameters.AddWithValue("@p3", faturaTipi);
                komut.Parameters.AddWithValue("@p4", tutar);
                komut.Parameters.AddWithValue("@p5", odeme);
                komut.Parameters.AddWithValue("@p6", faturaId);
                komut.ExecuteNonQuery();
                Temizle();
                MessageBox.Show("Fatura başarıyla güncellendi.", "Kayıt Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                bgl.baglanti().Close();
            }
            Listele();
            Temizle();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (faturaId == -1)
                {
                    MessageBox.Show("Lütfen silinecek olan faturayı seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    OleDbCommand komut = new OleDbCommand("DELETE FROM TblFaturalar WHERE faturaId=@p1", bgl.baglanti());
                    komut.Parameters.AddWithValue("@p1", faturaId);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Fatura başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Listele();
                    Temizle();
                    bgl.baglanti().Close();
                }
            }
            catch
            {
                MessageBox.Show("Beklenmeyen bir hata oluştu.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

       

        private void Faturalar_Load(object sender, EventArgs e)
        {
            Listele();
        }
    }
}
