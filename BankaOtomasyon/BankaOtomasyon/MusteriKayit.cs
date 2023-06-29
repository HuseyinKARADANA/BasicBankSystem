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
    public partial class MusteriKayit : Form
    {
        public MusteriKayit()
        {
            InitializeComponent();
        }
        sqlBaglantisi bgl=new sqlBaglantisi();
        public void Temizle()
        {
            txtAd.Text = "";
            cbCinsiyet.SelectedIndex = -1;
            txtAdres.Text = "";
            txtDTarihi.Text = "";
            txtEposta.Text = "";
            txtSifre.Text = "";
            txtSoyad.Text = "";
            txtTC.Text = "";
            txtTel.Text = ""; 
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ad = txtAd.Text;
            string soyad = txtSoyad.Text;
            string tel = txtTel.Text;
            string ePosta = txtEposta.Text;
            string adres = txtAdres.Text;
            string TC = txtTC.Text;
            string sifre = txtSifre.Text;
            string dTarihi = txtDTarihi.Text;
            string cinsiyet = cbCinsiyet.Text;

            if (ad.Equals("") || soyad.Equals("") || tel.Equals("")||adres.Equals("") || ePosta.Equals("") || TC.Equals("") || sifre.Equals("") || dTarihi.Equals("") || cinsiyet.Equals(""))
            {
                MessageBox.Show("Bütün bilgileri doldurunuz.");
            }
            else
            {
                OleDbCommand komut = new OleDbCommand("Insert Into TblMusteriler (ad,soyad,dTarihi,cinsiyet,telefon,eposta,adres,TCno,sifre) VALUES (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1",ad);
                komut.Parameters.AddWithValue("@p2",soyad);
                komut.Parameters.AddWithValue("@p3",dTarihi);
                komut.Parameters.AddWithValue("@p4",cinsiyet);
                komut.Parameters.AddWithValue("@p5",tel);
                komut.Parameters.AddWithValue("@p6",ePosta);
                komut.Parameters.AddWithValue("@p7",adres);
                komut.Parameters.AddWithValue("@p8",TC);
                komut.Parameters.AddWithValue("@p9",sifre);
      
                
                komut.ExecuteNonQuery();
                Temizle();
                MessageBox.Show("Müşteri başarıyla kaydedildi.", "Kayıt Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                bgl.baglanti().Close();
            }
            Temizle();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Giris frm=new Giris();
            frm.Show();
            this.Hide();
        }
    }
}
