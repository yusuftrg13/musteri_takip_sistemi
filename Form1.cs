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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglan=new SqlConnection("Data Source=DESKTOP-CHDG3O0\\SQLEXPRESS01;Initial Catalog=MusteriYonetim;Integrated Security=True");

        private void Form1_Load(object sender, EventArgs e)
        {
            

        }

        private void button4_Click(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'musteriYonetimDataSet1.Musteri_Yonet' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.musteri_YonetTableAdapter.Fill(this.musteriYonetimDataSet1.Musteri_Yonet);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //Ad Soyad Dogrulaması
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Ad alanı boş olamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; //Alan boş ise işlemi durdurur
            }
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Soyad alanı boş olamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Telefon numarası kontrol etme
            if (textBox3.Text.Length != 10 || !textBox3.Text.All(char.IsDigit))
            {
                MessageBox.Show("Telefon numarası 10 haneli ve yalnızca sayılardan olmalı");
                return;
            }

            //Adres Boş Geçilmesin

            if (string.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show("Adres alanı boş geçilemez", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Baglantıyı ac
            baglan.Open();

            //Sql komutunu yaz
            SqlCommand komut = new SqlCommand("INSERT INTO  Musteri_Yonet (Ad,Soyad,Telefon,Email,Adres)Values(@p1,@p2,@p3,@p4,@p5)",baglan);
            komut.Parameters.AddWithValue("@p1", textBox1.Text);
            komut.Parameters.AddWithValue("@p2",textBox2.Text);
            komut.Parameters.AddWithValue("@p3", textBox3.Text);
            komut.Parameters.AddWithValue("@p4", textBox4.Text);
            komut.Parameters.AddWithValue("@p5",textBox5.Text);

           

            //Komutu Çalıştır.
            komut.ExecuteNonQuery();
            //Baglantıyı Kapat
            baglan.Close();

            //Kullanıcıya Bilgi Ver
            MessageBox.Show("Başarıyla Eklendi");





      
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Baglantıyı Aç
            baglan.Open();

            //Sql Kodunu Yazalım
            SqlCommand komut = new SqlCommand("Update  Musteri_Yonet SET Ad=@p1,Soyad=@p2,Telefon=@p3,Email=@p4,Adres=@p5 Where MusteriID=@p6",baglan);
            komut.Parameters.AddWithValue("@p1",textBox1.Text);
            komut.Parameters.AddWithValue("@p2", textBox2.Text);
            komut.Parameters.AddWithValue("@p3", textBox3.Text);
            komut.Parameters.AddWithValue("@p4", textBox4.Text);
            komut.Parameters.AddWithValue("@p5", textBox5.Text);
            komut.Parameters.AddWithValue("@p6",Convert.ToInt32(TxtMusteriID.Text));

            //Komutu Çalıştır
            komut.ExecuteNonQuery();


            //Baglantıyı Kapat
            baglan.Close();


            //Kullanıcıya Bilgi Ver
            MessageBox.Show("Müşteri Bilgisi Güncellendi");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Baglantı Açalım
            baglan.Open();

            SqlCommand komut = new SqlCommand("DELETE FROM Musteri_Yonet Where MusteriId=@p1",baglan);
            komut.Parameters.AddWithValue("@p1", Convert.ToInt32(TxtMusteriID.Text));

            //Komutu Calıstır
            komut.ExecuteNonQuery();


            //Kullanıcıya Bilgi Ver
            MessageBox.Show("Müşteri Silindi");
            

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            //Secilen hücreleri TextBox'lara aktar
            TxtMusteriID.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            textBox1.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            textBox5.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
        }
    }
}
