using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace stok_takip_sistem
{
    public partial class Panel : Form
    {
        public Panel()
        {
            InitializeComponent();
        }
        SqlConnection baglan=new  SqlConnection("Data Source=DESKTOP-CHDG3O0\\SQLEXPRESS01;Initial Catalog=MusteriYonetim;Integrated Security=True;");


        private void button1_Click(object sender, EventArgs e)
        {
            //Kullanıcı Adı ve Şifre Boş Olup olmadıgına kontrol edelim
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Kulanıcı Adı ve Parola Boş Geçilemez");
                return;
            }

            //Baglantımızı Acalım
            baglan.Open();

            //Sql kodlarını yazalım
            SqlCommand komut = new SqlCommand("Select * From Tbl_Kullanici Where KullaniciAdi=@p1 AND Sifre=@p2",baglan);
            komut.Parameters.AddWithValue("@p1",textBox1.Text);
            komut.Parameters.AddWithValue("@p2", textBox2.Text);

            SqlDataReader dr = komut.ExecuteReader();

            if (dr.Read())
            {
               
                MessageBox.Show("Giriş başarılı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Ana Formu Getir
                Form1 frm = new Form1();
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre hatalı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Bağlantıyı kapat
            baglan.Close();

        }
    }
}
