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
    public partial class Giris : Form
    {
        public Giris()
        {
            InitializeComponent();
        }

        sqlBaglantisi bgl=new sqlBaglantisi();
        private void button1_Click(object sender, EventArgs e)
        {
            string tc=txtTc.Text;   
            string sifre=txtSifre.Text;

            if(tc.Equals("") || sifre.Equals(""))
            {
                MessageBox.Show("Bütün giriş bilgilerini doldurunuz.");
            }
            else
            {
                OleDbCommand komut=new OleDbCommand("SELECT * FROM TblMusteriler WHERE TCno=@p1 and sifre=@p2",bgl.baglanti());
                komut.Parameters.AddWithValue("@p1",tc);
                komut.Parameters.AddWithValue("@p2", sifre);
                OleDbDataReader dr=komut.ExecuteReader();
                if (dr.Read())
                {
                    musteriAnasayfa frm1 = new musteriAnasayfa();
                    frm1.TCNo = tc;
                    frm1.Show();
                    this.Hide();
                }
                else
                {
                    OleDbCommand komut2 = new OleDbCommand("SELECT * FROM TblPersoneller WHERE TCno=@p1 and sifre=@p2", bgl.baglanti());
                    komut2.Parameters.AddWithValue("@p1", tc);
                    komut2.Parameters.AddWithValue("@p2", sifre);
                    OleDbDataReader dr2 = komut2.ExecuteReader();
                    if (dr2.Read())
                    {
                        personelEkleme frm = new personelEkleme();
                        frm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Hatalı Kullanıcı Adı & Şifre");
                    }
                }
                bgl.baglanti().Close();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MusteriKayit frm=new MusteriKayit();
            frm.Show();
            this.Hide();
        }

        private void Giris_Load(object sender, EventArgs e)
        {

        }
    }
}
