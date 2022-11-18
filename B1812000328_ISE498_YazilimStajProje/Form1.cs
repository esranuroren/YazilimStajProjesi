using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


//B181200028 Esranur ÖREN ISE498 Yazılım Stajı Projesi

//Sql Server bağlantısı yapabilmek için gerekli kütüphaneyi ekliyorum
using System.Data.SqlClient;





namespace B1812000328_ISE498_YazilimStajProje
{

    //B181200028 Esranur ÖREN ISE498 Yazılım Stajı Projesi
    public partial class Form1 : Form
    {
        //Bağlantı için kullanacağım nesnelerimi oluşturuyorum
        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter DA;


        public Form1()
        {
            InitializeComponent();
        }


        //B181200028 Esranur ÖREN ISE498 Yazılım Stajı Projesi

        //Verilerde yaptığım ekleme - silme - güncelleme işlemlerini görüntülemek ve tekrarlı komut yazmamak için 
        //metodu formload dışına yazıyorum


        void DersGetir()
        {

            baglanti = new SqlConnection("server=.; Initial Catalog= B181200028_Ise498_YazilimStajProje; Integrated Security=SSPI");

            //bağlantıyı oluşturduktan sonra açıyorum
            baglanti.Open();

            //data adaptörünü ayarlıyorum
            DA = new SqlDataAdapter("SELECT *FROM Dersler", baglanti);

            //Datatable kullanarak veriyi dataGridView'a çekiyorum
            DataTable tablo = new DataTable();
            DA.Fill(tablo);
            dataGridView1.DataSource = tablo;

            //bağlantıyı kapatıyorum
            baglanti.Close();
        }



        //B181200028 Esranur ÖREN ISE498 Yazılım Stajı Projesi

        private void Form1_Load(object sender, EventArgs e)
        {
            DersGetir();
        }
         
        //seçtiğim satırların textboxlara gelmesini sağlıyorum
        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            txtDersID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtDersKodu.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtDersAdi.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtDersYariyili.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtDersiVerenler.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        //B181200028 Esranur ÖREN ISE498 Yazılım Stajı Projesi

        //Veri ekleme butonumun kodlarını ve sql server sorgusunu yazıyorum
        private void btnEkle_Click(object sender, EventArgs e)
        {
            //tabloya veri eklerken DersID alanı otomatik artan olduğu için ona müdahale etmiyo ve insert sorgumun 
            //içine yazmıyorum

            string sorgu = "INSERT INTO Dersler(DersKodu, DersAdı, DersYarıyılı, DersiVerenler)" +
                " VALUES (@DersKodu, @DersAdı, @DersYarıyılı, @DersiVerenler)";
            komut = new SqlCommand(sorgu, baglanti);

            //parametrelerimi ayarlıyorum
            komut.Parameters.AddWithValue("@DersKodu", txtDersKodu.Text);
            komut.Parameters.AddWithValue("@DersAdı", txtDersAdi.Text);
            komut.Parameters.AddWithValue("@DersYarıyılı", txtDersYariyili.Text);
            komut.Parameters.AddWithValue("@DersiVerenler", txtDersiVerenler.Text);

            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();

            //sql serverda tablomda yapılan değişiklikleri görüntülemek için dersgetir metodumu çağırıyorum yeniden
            DersGetir();

        }


        //B181200028 Esranur ÖREN ISE498 Yazılım Stajı Projesi

        //veri silme butonumun kodlarını ve sql server sorgusunu yazıyorum
        private void btnSil_Click(object sender, EventArgs e)
        {
            //veri silme işleminde sql server sorgusunun yanlış yazılması durumunda tüm verinin silinmesi münkün olduğu
            //için tablodan sadece seçtiğim satırı silebilmek için sorguya where komutunu ekliyorum
            string sorgu = "DELETE FROM Dersler WHERE DersID=@DersID";
            komut = new SqlCommand(sorgu, baglanti);

            //parametremi ayarlıyorum
            komut.Parameters.AddWithValue("@DersID", Convert.ToInt32(txtDersID.Text));

            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            DersGetir();
        }

        //B181200028 Esranur ÖREN ISE498 Yazılım Stajı Projesi

        //veri güncelleme butonumun kodlarını ve sql server sorgusunu yazıyorum
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            //veri güncelleme işlemini DersID'ye göre uygulamak için sql server sorgusunda 
            //yeniden where komutunu kullanıyorum
            string sorgu = "UPDATE Dersler SET DersKodu=@DersKodu, DersAdı=@DersAdı, DersYarıyılı=@DersYarıyılı," +
                " DersiVerenler=@DersiVerenler WHERE DersID=@DersID";

            komut = new SqlCommand(sorgu, baglanti);

            //parametrelerimi ayarlıyorum
            komut.Parameters.AddWithValue("@DersID", Convert.ToInt32(txtDersID.Text));
            komut.Parameters.AddWithValue("@DersKodu", txtDersKodu.Text);
            komut.Parameters.AddWithValue("@DersAdı", txtDersAdi.Text);
            komut.Parameters.AddWithValue("@DersYarıyılı", txtDersYariyili.Text);
            komut.Parameters.AddWithValue("@DersiVerenler", txtDersiVerenler.Text);


            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            DersGetir();
        }

        
    }
}
