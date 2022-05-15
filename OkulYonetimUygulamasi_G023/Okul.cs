using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkulYonetimUygulamasi_G023
{
    class Okul
    {
        //Veriyi yönetecek tüm kodları buraya yazabilirsiniz.
        //Bu classta console ile ilgili bir kod olmasın
        //void metotlarda geri bildirim vermeniz
        //gerekiyorsa exceptionları kullanın.
        public List<Ogrenci> Ogrenciler = new List<Ogrenci>();


        public void NotEkle(int ogrenciNo, string ders, int not)
        {
            Ogrenci o = this.Ogrenciler.Where(a => a.No == ogrenciNo).FirstOrDefault();

            if (o != null)
            {
                DersNotu dn = new DersNotu(ders, not);
                o.Notlar.Add(dn);
            }
        }

        public void OgrenciEkle(int no, string ad, string soyad, DateTime dogumTarihi, CINSIYET cinsiyet, SUBE sube)
        {
            Ogrenci o = new Ogrenci();

            o.No = no;
            o.Ad = ad;
            o.Soyad = soyad;
            o.DogumTarihi = dogumTarihi;
            o.Cinsiyet = cinsiyet;
            o.Sube = sube;

            Ogrenciler.Add(o);

        }
      


        public void KitapEkle(int no, string kitapAdi)
        {
            Ogrenci o = this.Ogrenciler.Where(a => a.No == no).FirstOrDefault();

            if (o != null)
            {
                o.KitapEkle(kitapAdi);
            }
        }

        public void OgrenciSil(int no)    //  21
        {
            Ogrenci o = this.Ogrenciler.Where(a => a.No == no).FirstOrDefault();

            if (o != null)
            {
                Ogrenciler.Remove(o);
            }
        }

        public void Guncelle(int no, string isim, string soyisim, DateTime dogumTarihi, CINSIYET cinsiyet, SUBE sube)   //22
        {
            Ogrenci o = this.Ogrenciler.Where(a => a.No == no).FirstOrDefault();

            if (o != null)
            {
                if (!string.IsNullOrEmpty(isim))
                {
                    o.Ad = isim;
                }
                if (!string.IsNullOrEmpty(soyisim))
                {
                    o.Soyad = soyisim;
                }
                if (dogumTarihi != DateTime.MinValue) 
                {
                    o.DogumTarihi = dogumTarihi;
                }
                if (cinsiyet != CINSIYET.Empty)   // kontrol 0 da olur
                {
                    o.Cinsiyet = cinsiyet;
                }
                if (sube != SUBE.Empty)  // kontrol 0 da olur
                {
                    o.Sube = sube;
                }
            }



        }



        public float OrtalamaGetir(int no)   //  3
        {
            Ogrenci o = this.Ogrenciler.Where(a => a.No == no).FirstOrDefault();

            if (o != null)
            {
                return o.Ortalama;
            }
            return 0;
        }

        public void AdresEkle(int no, string il, string ilce, string mahalle)  //  5
        {
            Ogrenci o = this.Ogrenciler.Where(a => a.No == no).FirstOrDefault();
            if (o != null)
            {
                o.AdresEkle(il, ilce, mahalle);
            }
        }



        public int NoOlustur(int no)    // 1
        {
            while (true)
            {
                if (VarMi(no))
                {
                    no++;
                }
                else
                {
                    return no;
                }
            }
        }


        public bool VarMi(int no)
        {
            //Ogrenci o = this.Ogrenciler.Where(a => a.No == no).FirstOrDefault();
            return this.Ogrenciler.Where(a => a.No == no).FirstOrDefault() != null;
            //if (o != null)
            //{
            //    return true;
            //}
            //return false;
        }

        public string AdiSoyadiGetir(int no)
        {
            Ogrenci o = this.Ogrenciler.Where(a => a.No == no).FirstOrDefault();

            string tamIsim = "";

            if (o != null)
            {
                tamIsim = o.Ad + " " + o.Soyad;
            }

            return tamIsim;
        }

        public SUBE SubeGetir(int no)
        {
            Ogrenci o = this.Ogrenciler.Where(a => a.No == no).FirstOrDefault();

            if (o != null)
            {
                return o.Sube;
            }
            return SUBE.Empty;

        }


        public List<DersNotu> OgrenciNotlariGetir(int no)    // 15  
        {
            Ogrenci o = this.Ogrenciler.Where(a => a.No == no).FirstOrDefault();

            List<DersNotu> notlar;

            if (o != null)
            {
                notlar = o.Notlar.ToList();
                return notlar;
            }
            return null;

        }

        public List<string> KitapListele(int no)    // 16
        {
            Ogrenci o = this.Ogrenciler.Where(a => a.No == no).FirstOrDefault();

            List<string> kitaplar;

            if (o != null)
            {
                kitaplar = o.Kitaplar.ToList();
                return kitaplar;
            }
            return null;
        }

        public List<string> SonKitapGetir(int no)
        {
            Ogrenci o = this.Ogrenciler.Where(a => a.No == no).FirstOrDefault();

            List<string> kitaplar;


            if (o != null)
            {
                kitaplar = o.Kitaplar.ToList();
                kitaplar.Reverse();

                return kitaplar.Take(1).ToList();
            }
            return null;
        }



        public void AciklamaAl(int no, string aciklama)
        {
            Ogrenci o = this.Ogrenciler.Where(a => a.No == no).FirstOrDefault();

            if (o != null)
            {
                o.Aciklama += aciklama + "\n";
            }
        }


        public string AciklamaGoster(int no)  
        {
            Ogrenci o = this.Ogrenciler.Where(a => a.No == no).FirstOrDefault();

            if (o != null)
            {
                return o.Aciklama;
            }
            throw new Exception("Böyle bir ögrenci yok");
        }

        public void AciklamaVarMi(int no)
        {
            Ogrenci o = this.Ogrenciler.Where(a => a.No == no).FirstOrDefault();

            if (o.Aciklama == null)
            {
                throw new Exception("Bu ögrenciye ait açıklama bulunmamaktadır.");
            }
        }




    }
}
