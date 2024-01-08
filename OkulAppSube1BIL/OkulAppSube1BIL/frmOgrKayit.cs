using OkulApp.MODEL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OkulApp.BLL;

namespace OkulAppSube1BIL
{
    public partial class frmOgrKayit : Form
    {
        public int ButtonKontrol; // ise Bul ve Kaydet Aktif, Güncelle ve sil Pasif

        public int Ogrenciid { get; set; }
        public frmOgrKayit()
        {
            InitializeComponent();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (txtAd.Text.Length <1 && txtSoyad.Text.Length <1 && txtNumara.Text.Length <1)
            {
                MessageBox.Show("Alan boş bırakılamaz.");
                return;
            }
            try
            {
                var obl = new OgrenciBL();
                bool sonuc = obl.OgrenciEkle(new Ogrenci { Ad = txtAd.Text.Trim(), Soyad = txtSoyad.Text.Trim(), Numara = txtNumara.Text.Trim() });
                MessageBox.Show(sonuc ? "Ekleme Başarılı!" : "Ekleme Başarısız!!");
                ButtonKontrol = 1;
                ButtonDurum();
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 2627:
                        MessageBox.Show("Bu numaralı öğrenci daha önce kayıtlı");
                        break;
                    default:
                        MessageBox.Show("Veritabanı hatası");
                        break;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Bilinmeyen Hata!!");
            }
        }

        private void btnBul_Click(object sender, EventArgs e)
        {
            ButtonKontrol = 0;
            frmOgrBul frm = new frmOgrBul(this);
            frm.ShowDialog();
            ButtonDurum();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                var obl = new OgrenciBL();
                bool guncellemeBasarili = obl.OgrenciGuncelle(new Ogrenci { Ad = txtAd.Text.Trim(), Soyad = txtSoyad.Text.Trim(), Numara = txtNumara.Text.Trim(), Ogrenciid = Ogrenciid });
                if (guncellemeBasarili)
                {
                    MessageBox.Show("Öğrenci bilgisi başarıyla güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Öğrenci bilgisi güncelleme sırasında bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu :" + ex.ToString());
            }

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                var obl = new OgrenciBL();
            bool silmeBasarili = obl.OgrenciSil(Ogrenciid);
              if (silmeBasarili)
            {
                MessageBox.Show("Öğrenci bilgisi başarıyla silindi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Silme sırasında bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu :" + ex.ToString());
            }
        }

        private void grpOgrenci_Enter(object sender, EventArgs e)
        {

        }

        private void frmOgrKayit_Load(object sender, EventArgs e)
        {
            ButtonKontrol = 0;
            ButtonDurum();

        }

        public void ButtonDurum()
        {
            if (ButtonKontrol == 0)
            {
                btnGuncelle.Enabled = false;
                btnSil.Enabled = false;
            }
            else if (ButtonKontrol == 1)
            {
                btnGuncelle.Enabled = true;
                btnSil.Enabled = true;
            }

        }
    }

    //Güncelleme Başarılı mesajı
    //Güncelleme butonu aktifliği?
    //Silme butonu aktifliği
    //Silme işlemi mesajı
    //Tüm işlemlerde try-catch
    //Helperda bulunan connection ve commandlerin dispose edilmesi (IDisposable Pattern)
    //Singleton Pattern (Sürkeli nesne oluşmadan tek nesne üstünden işlemlerin yapılması)
    //Öğretmen entity'si için kalan CRUD işlemleri



    interface ITransfer
    {
        int Eft(string gondericiiban, string aliciiban, double tutar);
        int Havale(string gondericiiban, string aliciiban, double tutar);

    }

    class Transfer : ITransfer
    {
        public int Eft(string gondericiiban, string aliciiban, double tutar)
        {
            throw new NotImplementedException();
        }

        public int Havale(string gondericiiban, string aliciiban, double tutar)
        {
            throw new NotImplementedException();
        }

        //
    }
}

//Garbage Collector
