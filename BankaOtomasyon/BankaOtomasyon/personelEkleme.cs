using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankaOtomasyon
{
    public partial class personelEkleme : Form
    {
        public personelEkleme()
        {
            InitializeComponent();
        }
        sqlBaglantisi bgl=new sqlBaglantisi();
        public void Temizle()
        {
            cbCinsiyet.SelectedIndex = -1;
            cbSubeler.SelectedIndex = 0;
            txtAd.Text = "";
            txtDTarihi.Text = "";
            txtMaas.Text = "";
            txtPozisyon.Text = "";
            txtSifre.Text = "";
            txtSoyad.Text = "";
            txtTC.Text = "";
            
        }
        private void button4_Click(object sender, EventArgs e)
        {
            SubeEkleme frm= new SubeEkleme();
            frm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Giris frm=new Giris();
            frm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int subeId =Convert.ToInt32(cbSubeler.SelectedValue);
            string ad=txtAd.Text;
            string soyad=txtSoyad.Text;
            string pozisyon=txtPozisyon.Text;
            string maas=txtMaas.Text;
            string TC=txtTC.Text;
            string sifre=txtSifre.Text;
            string dTarihi=txtDTarihi.Text;
            string cinsiyet =cbCinsiyet.Text;

            if (subeId==-1 || ad.Equals("") || soyad.Equals("") || pozisyon.Equals("") || maas.Equals("") || TC.Equals("") || sifre.Equals("") || dTarihi.Equals("") || cinsiyet.Equals(""))
            {
                MessageBox.Show("Bütün bilgileri doldurunuz.");
            }
            else
            {
                OleDbCommand komut = new OleDbCommand("Insert Into TblPersoneller (subeId,ad,soyad,pozisyon,maas,TCno,sifre,cinsiyet,dTarihi) VALUES (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1",subeId );
                komut.Parameters.AddWithValue("@p2",ad );
                komut.Parameters.AddWithValue("@p3",soyad);
                komut.Parameters.AddWithValue("@p4",pozisyon);
                komut.Parameters.AddWithValue("@p5",maas);
                komut.Parameters.AddWithValue("@p6",TC);
                komut.Parameters.AddWithValue("@p7",sifre);
                komut.Parameters.AddWithValue("@p8",cinsiyet);
                komut.Parameters.AddWithValue("@p9",dTarihi);
                komut.ExecuteNonQuery();
                Temizle();
                MessageBox.Show("Personel başarıyla eklendi.", "Kayıt Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                bgl.baglanti().Close();
            }
            Temizle();
        }

        private void personelEkleme_Load(object sender, EventArgs e)
        {
   
            OleDbDataAdapter da = new OleDbDataAdapter("Select * From TblSubeler", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            cbSubeler.ValueMember = "subeId";
            cbSubeler.DisplayMember = "subeAd";
            cbSubeler.DataSource = dt;
            bgl.baglanti().Close();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            string TCno = txtTC.Text;
            try
            {
                if (TCno.Equals(""))
                {
                    MessageBox.Show("Silmek istediğiniz personelin T.C. numarasını giriniz. ");
                }
                else
                {

                    OleDbCommand komut = new OleDbCommand("DELETE FROM TblPersoneller WHERE TCno=@p1", bgl.baglanti());
                    komut.Parameters.AddWithValue("@p1", TCno);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Personel Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    Temizle();
                    bgl.baglanti().Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Bu T.C. numarasına sahip bir personel bulunmuyor.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            }

        }
    }
}
