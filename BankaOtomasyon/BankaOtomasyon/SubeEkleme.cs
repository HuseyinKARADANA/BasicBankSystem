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
    public partial class SubeEkleme : Form
    {
        public SubeEkleme()
        {
            InitializeComponent();
        }
        sqlBaglantisi bgl=new sqlBaglantisi();
        public void Temizle()
        {
            txtAdres.Text = "";
            txtSubeAd.Text = "";
            txtTel.Text = "";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            personelEkleme frm=new personelEkleme();
            frm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Giris frm=new Giris();
            frm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string subeAd=txtSubeAd.Text;
            string tel=txtTel.Text;
            string adres=txtAdres.Text;
            if(subeAd.Equals("") || tel.Equals("") || adres.Equals(""))
            {
                MessageBox.Show("Bütün bilgileri doldurunuz.");
            }
            else
            {
                OleDbCommand komut = new OleDbCommand("Insert Into TblSubeler (subeAd,subeAdres,subeTel) VALUES (@p1,@p2,@p3)", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", subeAd);
                komut.Parameters.AddWithValue("@p2", adres);
                komut.Parameters.AddWithValue("@p3", tel);
                komut.ExecuteNonQuery();
                Temizle();
                MessageBox.Show("Şube başarıyla eklendi.", "Kayıt Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                bgl.baglanti().Close();
            }
            Temizle();
        }
    }
}
