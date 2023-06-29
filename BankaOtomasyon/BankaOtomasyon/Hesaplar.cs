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
    public partial class Hesaplar : Form
    {
        public Hesaplar()
        {
            InitializeComponent();
        }
        public int musteriId,hesapId=-1;
        public string musteriTC;
        sqlBaglantisi bgl=new sqlBaglantisi();

        public void Listele()
        {
            DataTable dt = new DataTable();
            OleDbCommand komut=new OleDbCommand("SELECT hesapId,hesapNo,bakiye,hesapTipi,acilisTarihi FROM TblHesaplar WHERE musteriId=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",musteriId);
            OleDbDataAdapter da = new OleDbDataAdapter(komut);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            bgl.baglanti().Close();
        }


        public void Temizle()
        {
            txtAcilisTarihi.Text = "";
            txtBakiye.Text = "";
            txtHesapNo.Text = "";
            cbHesapTipi.SelectedIndex = -1;
            hesapId = -1;
        }
        private void Hesaplar_Load(object sender, EventArgs e)
        {
            Listele();  
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string hesapNo = txtHesapNo.Text;
            string bakiye = txtBakiye.Text;
            string hesapTipi = cbHesapTipi.Text;
            string acilisTarihi = txtAcilisTarihi.Text;
            

            if (cbHesapTipi.SelectedIndex == -1 || hesapNo.Equals("") || bakiye.Equals("") || acilisTarihi.Equals("") )
            {
                MessageBox.Show("Bütün bilgileri doldurunuz.");
            }
            else
            {
                OleDbCommand komut = new OleDbCommand("Insert Into TblHesaplar (musteriId,hesapNo,bakiye,hesapTipi,acilisTarihi) VALUES (@p1,@p2,@p3,@p4,@p5)", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", musteriId);
                komut.Parameters.AddWithValue("@p2", hesapNo);
                komut.Parameters.AddWithValue("@p3", bakiye);
                komut.Parameters.AddWithValue("@p4", hesapTipi);
                komut.Parameters.AddWithValue("@p5", acilisTarihi);        
                komut.ExecuteNonQuery();
                Temizle();
                MessageBox.Show("Hesap başarıyla oluşturuldu.", "Kayıt Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                bgl.baglanti().Close();
            }
            Listele();
            Temizle();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            musteriAnasayfa frm=new musteriAnasayfa();
            frm.TCNo = musteriTC;
            frm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (hesapId==-1)
                {
                    MessageBox.Show("Lütfen iptal edilecek olan hesabı seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    OleDbCommand komut = new OleDbCommand("DELETE FROM TblHesaplar WHERE hesapId=@p1", bgl.baglanti());
                    komut.Parameters.AddWithValue("@p1", hesapId);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Hesap başarıyla iptal edildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            string hesapNo = txtHesapNo.Text;
            string bakiye = txtBakiye.Text;
            string hesapTipi = cbHesapTipi.Text;
            string acilisTarihi = txtAcilisTarihi.Text;


            if (cbHesapTipi.SelectedIndex == -1 || hesapNo.Equals("") || bakiye.Equals("") || acilisTarihi.Equals(""))
            {
                MessageBox.Show("Bütün bilgileri doldurunuz.");
            }
            else
            {
                if (hesapId != -1)
                {
                    OleDbCommand komut = new OleDbCommand("UPDATE TblHesaplar SET musteriId=@p1,hesapNo=@p2,bakiye=@p3,hesapTipi=@p4,acilisTarihi=@p5 WHERE hesapId=@p6", bgl.baglanti());
                    komut.Parameters.AddWithValue("@p1", musteriId);
                    komut.Parameters.AddWithValue("@p2", hesapNo);
                    komut.Parameters.AddWithValue("@p3", bakiye);
                    komut.Parameters.AddWithValue("@p4", hesapTipi);
                    komut.Parameters.AddWithValue("@p5", acilisTarihi);
                    komut.Parameters.AddWithValue("@p6", hesapId);
                    komut.ExecuteNonQuery();
                    Temizle();
                    MessageBox.Show("Hesap başarıyla güncellendi.", "Kayıt Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    bgl.baglanti().Close();
                    Listele();
                    Temizle();
                }
                else
                {
                    MessageBox.Show("Güncellenecek hesabı seçiniz.");
                }
                
            }
               
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int secilen = dataGridView1.SelectedCells[0].RowIndex;
                hesapId = int.Parse(dataGridView1.Rows[secilen].Cells[0].Value.ToString());
                txtHesapNo.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
                txtBakiye.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
                cbHesapTipi.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
                txtAcilisTarihi.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();         
            }
            catch { }
        }
    }
}
