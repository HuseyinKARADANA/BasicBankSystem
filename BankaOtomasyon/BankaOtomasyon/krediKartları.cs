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
    public partial class krediKartları : Form
    {
        public krediKartları()
        {
            InitializeComponent();
        }
        public int musteriId, kartId = -1;
        public string musteriTC;
        sqlBaglantisi bgl = new sqlBaglantisi();

        public void Listele()
        {
            DataTable dt = new DataTable();
            OleDbCommand komut = new OleDbCommand("SELECT kartId,kartNo,sonKullanmaTarihi,limitMiktar FROM TblKrediKartlari WHERE musteriId=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", musteriId);
            OleDbDataAdapter da = new OleDbDataAdapter(komut);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            bgl.baglanti().Close();
        }


        public void Temizle()
        {
            txtAcilisTarihi.Text = "";
            txtKartNo.Text = "";
            txtLimit.Text = "";
            kartId = -1;
        }

        private void krediKartları_Load(object sender, EventArgs e)
        {
            Listele();
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

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (kartId == -1)
                {
                    MessageBox.Show("Lütfen iptal edilecek olan kredi kartını seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    OleDbCommand komut = new OleDbCommand("DELETE FROM TblKrediKartlari WHERE kartId=@p1", bgl.baglanti());
                    komut.Parameters.AddWithValue("@p1", kartId);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Kredi kartı başarıyla iptal edildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void button3_Click(object sender, EventArgs e)
        {

            string kartNo = txtKartNo.Text;
            string limit = txtLimit.Text;
            string sonKullanmaTarihi = txtAcilisTarihi.Text;


            if (limit.Equals("") || kartNo.Equals("") || sonKullanmaTarihi.Equals(""))
            {
                MessageBox.Show("Bütün bilgileri doldurunuz.");
            }
            else
            {
                OleDbCommand komut = new OleDbCommand("UPDATE TblKrediKartlari SET musteriId=@p1,kartNo=@p2,sonKullanmaTarihi=@p3,limitMiktar=@p4 WHERE kartId=@p5", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", musteriId);
                komut.Parameters.AddWithValue("@p2", kartNo);
                komut.Parameters.AddWithValue("@p3", sonKullanmaTarihi);
                komut.Parameters.AddWithValue("@p4", limit);
                komut.Parameters.AddWithValue("@p5", kartId);
                komut.ExecuteNonQuery();
                Temizle();
                MessageBox.Show("Kredi kartı bilgileri başarıyla güncellendi.", "Kayıt Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                bgl.baglanti().Close();
            }
            Listele();
            Temizle();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int secilen = dataGridView1.SelectedCells[0].RowIndex;
                kartId = int.Parse(dataGridView1.Rows[secilen].Cells[0].Value.ToString());
                txtKartNo.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();       
                txtAcilisTarihi.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
                txtLimit.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
               
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string kartNo = txtKartNo.Text;
      
            string limit = txtLimit.Text;
            string sonKullanmaTarihi = txtAcilisTarihi.Text;


            if (limit.Equals("") || kartNo.Equals("") || sonKullanmaTarihi.Equals(""))
            {
                MessageBox.Show("Bütün bilgileri doldurunuz.");
            }
            else
            {
                OleDbCommand komut = new OleDbCommand("Insert Into TblKrediKartlari (musteriId,kartNo,sonKullanmaTarihi,limitMiktar) VALUES (@p1,@p2,@p3,@p4)", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", musteriId);
                komut.Parameters.AddWithValue("@p2", kartNo);
                komut.Parameters.AddWithValue("@p3", sonKullanmaTarihi);
                komut.Parameters.AddWithValue("@p4", limit);
                komut.ExecuteNonQuery();
                Temizle();
                MessageBox.Show("Kredi kartı başarıyla oluşturuldu.", "Kayıt Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                bgl.baglanti().Close();
            }
            Listele();
            Temizle();
        }
    }
}
