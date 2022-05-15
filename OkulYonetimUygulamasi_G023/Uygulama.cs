using System;
using System.Collections.Generic;
using System.Linq;

namespace OkulYonetimUygulamasi_G023
{
    class Uygulama
    {
        static Okul Okul = new Okul();

        public void Calistir()
        {
            SahteVeriGir();
            SahteNotGir();
            SahteAdresGir();
            SahteKitapEkle();
            Menu();

            while (true)
            {
                Console.WriteLine();
                string secim = SecimAl();

                switch (secim)
                {
                    case "1":
                        OgrenciGir();
                        break;
                    case "2":
                        NotGir();
                        break;
                    case "3":
                        OrtalamaGoster();
                        break;
                    case "4":
                        SinifinOrtalamasi();
                        break;
                    case "5":
                        AdresGoster();
                        break;
                    case "6":
                        AciklamaGir();
                        break;
                    case "7":
                        AciklamaGoster();
                        break;
                    case "8":
                        KitapGir();
                        break;
                    case "9":
                        SonKitap();
                        break;
                    case "10":
                        TümOgrencileriListele();
                        break;
                    case "11":
                        SubeyeGoreListele();
                        break;
                    case "12":
                        CinsiyeteGoreListele();
                        break;
                    case "13":
                        TarihtenSonraDoganlar();
                        break;
                    case "14":
                        IllereGoreListele();
                        break;
                    case "15":
                        NotlariGoster();
                        break;
                    case "16":
                        KitapListele();
                        break;
                    case "17":
                        OkulEnBasariliBes();
                        break;
                    case "18":
                        OkulEnBasarisizUc();
                        break;
                    case "19":
                        SubeEnBasariliBes();
                        break;
                    case "20":
                        SubeEnBasarisizUc();
                        break;                   
                    case "21":
                        OgrenciSil();
                        break;
                    case "22":
                        Guncelle();
                        break;
                    case "cikis":
                    case "çıkış":
                        Close();  
                        break;
                    case "liste":
                        Console.WriteLine();
                        Calistir();
                        Console.WriteLine();
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("Hatalı islem gerçeklestirildi. Tekrar deneyin.");
                        break;

                }              
                    Console.WriteLine();
                    Console.WriteLine("Menüyü tekrar listelemek için \"liste\", çıkıs yapmak için \"çıkıs\" yazın.");                
            }

        }

        public void OgrenciGir()    //   1 
        {
            Console.WriteLine();
            Console.WriteLine("1-Öğrenci Ekle -----------------------------------------------------");
            int alinanno = AracGerecler.SayiAl("Ögrencinin numarası: ");
            int yenino = Okul.NoOlustur(alinanno);

            string yeniAd = IlkHarfiBuyut(AracGerecler.YaziAl("Ögrencinin adı: ", true));           
            string yeniSoyad = IlkHarfiBuyut(AracGerecler.YaziAl("Ögrencinin soyadı: ", true));

            DateTime tarih = AracGerecler.TarihAl("Ögrencinin dogum tarihi: ");
            CINSIYET cins = AracGerecler.KizMiErkekMi("Ögrencinin cinsiyeti (K/E): ",true);
            SUBE sube = AracGerecler.SubeAl("Ögrencinin subesi (A/B/C): ", true);

            Okul.OgrenciEkle(yenino, yeniAd, yeniSoyad, tarih, cins, sube);
            Console.WriteLine();
            Console.WriteLine(yenino + " numaralı ögrenci sisteme basarılı bir sekilde eklenmistir.");

            if (alinanno != yenino)  
            {
                Console.WriteLine($"Sistemde {alinanno} numaralı öğrenci olduğu için verdiğiniz öğrenci no {yenino} olarak değiştirildi. ");
            }
        }


        public void NotGir()       //   2 
        {
            Console.WriteLine();
            Console.WriteLine("2-Not Gir ----------------------------------------------------------");

            int no = NoAl();

            BilgiYazdir(no);
            Console.WriteLine("---- Not Eklenilebilecek Dersler ----");
            DERSLER dersx = AracGerecler.DersGir();
            string ders = dersx.ToString();

            int adet = AracGerecler.SayiAl("Eklemek istediginiz not adedi: ");

            for (int i = 1; i <= adet; i++)
            {

                int not = AracGerecler.SayiAl(i + ". Notu girin: ");

                if (not < 0 || not > 100)
                {
                    Console.WriteLine("Girdiğiniz değer 0 ve 100 arasında olmalıdır.");
                    i--;
                    continue;
                }

                Okul.NotEkle(no, ders, not);
            }
            Console.WriteLine();
            Console.WriteLine("Bilgiler sisteme girilmistir.");
        }

        public void OrtalamaGoster()     //  3 
        {
            Console.WriteLine();
            Console.WriteLine("3-Ögrencinin Not Ortalamasını Gör ----------------------------------");

            // liste bos olunca uyari veriyor mu diye kontrol et
            if (Okul.Ogrenciler.Count == 0)
            {
                Console.WriteLine("Listede ögrenci yok.");
                return;
            }

            int no = NoAl();
            BilgiYazdir(no);

            Console.Write("Ögrencinin not ortalaması: " + Okul.OrtalamaGetir(no));
            Console.WriteLine();

        }

        public void SinifinOrtalamasi()  //  4  
        {
            Console.WriteLine();
            Console.WriteLine("4-Şubenin Not Ortalamasını Gör -------------------------------------");
                     
            SUBE sube = AracGerecler.SubeAl("Bir şube seçin (A/B/C): ", true);  
            Console.WriteLine();

            float ort = 0;
            ort = Okul.Ogrenciler.Where(a => a.Sube == sube).Average(a => a.Ortalama);

            Console.Write(sube + " subesinin not ortalaması: " + decimal.Round((decimal)ort, 2));  // virgülden sonra 2 digit göstersin diye
            Console.WriteLine();         
        }

        public void AdresGoster()        //  5
        {
            Console.WriteLine();
            Console.WriteLine("5-Ögrencinin Adresini Gir ------------------------------------------");

            if (Okul.Ogrenciler.Count == 0)
            {
                Console.WriteLine("Listede ögrenci yok.");
                return;
            }

            int no = NoAl();
            BilgiYazdir(no);
 
            string yeniIl = IlkHarfiBuyut(AracGerecler.YaziAl("Il: ", true));
            string yeniIlce = IlkHarfiBuyut(AracGerecler.YaziAl("Ilce: ", true));
            string yeniMah = IlkHarfiBuyut(AracGerecler.YaziAl("Mahalle: ", true));

            Okul.AdresEkle(no, yeniIl, yeniIlce, yeniMah);

            Console.WriteLine();
            Console.WriteLine("Bilgiler sisteme girilmistir.");

        }

        public void AciklamaGir()    //  6
        {
            Console.WriteLine();
            Console.WriteLine("6-Ögrenci için açıklama gir ----------------------------------------");

            if (Okul.Ogrenciler.Count == 0)
            {
                Console.WriteLine("Listede ögrenci yok.");
                return;
            }

            int no = NoAl();
            BilgiYazdir(no);

            Console.Write("Aciklama: ");
            string aciklama = Console.ReadLine().ToString();
            Okul.AciklamaAl(no, aciklama);

            Console.WriteLine("Bilgiler sisteme girilmistir.");
        }

        public void AciklamaGoster()     //  7
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine("7-Ögrencinin açıklamasını gör ----------------------------------");
                int no = NoAl();
                BilgiYazdir(no);
                Okul.AciklamaVarMi(no);
                Console.WriteLine("Aciklama: ");
                Console.WriteLine(Okul.AciklamaGoster(no));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void KitapGir()     //  8
        {
            Console.WriteLine();
            Console.WriteLine("8-Ögrencinin okudugu kitabı gir ------------------------------------");

            if (Okul.Ogrenciler.Count == 0)
            {
                Console.WriteLine("Listede ögrenci yok.");
                return;
            }

            int no = NoAl();
            BilgiYazdir(no);

            Console.Write("Eklenecek Kitabin Adı: ");
            string kitap = IlkHarfiBuyut(Console.ReadLine());
            Okul.KitapEkle(no, kitap);

            Console.WriteLine("Bilgiler sisteme girilmistir.");

        }

        public void SonKitap()        //  9  
        {
            Console.WriteLine();
            Console.WriteLine("9-Ögrencinin okudugu son kitabı listele ----------------------------");

            int no = NoAl();
            BilgiYazdir(no);

            Console.WriteLine("Ögrencinin Okudugu Son Kitap");
            Console.WriteLine("-----------------------------");

            List<string> Kitaplar = Okul.SonKitapGetir(no);

            foreach (string item in Kitaplar)
            {
                Console.WriteLine(item);
            }

        }

        public void TümOgrencileriListele()  //  10
        {
            Console.WriteLine();
            Console.WriteLine("10-Bütün Ögrencileri Listele --------------------------------------------------");

            if (Okul.Ogrenciler.Count == 0) 
            {
                Console.WriteLine("Listede ögrenci yok.");
                return;
            }

            Console.WriteLine();
            Listele(Okul.Ogrenciler);

        }

        public void SubeyeGoreListele()   //  11
        {
            Console.WriteLine();
            Console.WriteLine("11-Subeye Göre Ögrencileri Listele --------------------------------------------");

            if (Okul.Ogrenciler.Count == 0)
            {
                Console.WriteLine("Listede ögrenci yok.");
                return;
            }

            SUBE sb = AracGerecler.SubeAl("Listelemek istediğiniz şubeyi girin (A/B/C): ",true);
            List<Ogrenci> liste = Okul.Ogrenciler.Where(item => item.Sube == sb).ToList();

            Console.WriteLine();
            Listele(liste);
        }

        public void CinsiyeteGoreListele()  //  12
        {
            Console.WriteLine();
            Console.WriteLine("12-Cinsiyete Göre Öğrencileri Listele -----------------------------------------");

            CINSIYET cins = AracGerecler.KizMiErkekMi("Listelemek istediğiniz cinsiyeti girin (K/E): ", true);
            List<Ogrenci> liste = Okul.Ogrenciler.Where(item => item.Cinsiyet == cins).ToList();

            Console.WriteLine();
            Listele(liste);
        }

        public void TarihtenSonraDoganlar()   //  13
        {
            Console.WriteLine();
            Console.WriteLine("13-Dogum Tarihine Göre Ögrencileri Listele ------------------------------------");


            DateTime tarih = AracGerecler.TarihAl("Hangi tarihten sonraki ögrencileri listelemek istersiniz: ");
            List<Ogrenci> liste = Okul.Ogrenciler.Where(a => a.DogumTarihi > tarih).ToList();

            Console.WriteLine();
            Listele(liste);
        }

        public void IllereGoreListele()   //   14
        {
            Console.WriteLine();
            Console.WriteLine("14-Illere Göre Ögrencileri Listele --------------------------------------------");

            List<Ogrenci> liste = Okul.Ogrenciler.OrderBy(a => a.Adresi.Il).ToList();

            Console.WriteLine();
            Listele1(liste);

        }

        public void NotlariGoster()   //   15
        {
            Console.WriteLine();
            Console.WriteLine("15-Ögrencinin notlarını görüntüle ---------------------------------------------");

            if (Okul.Ogrenciler.Count == 0)
            {
                Console.WriteLine("Listede ögrenci yok.");
                return;
            }

            int no = NoAl();
            BilgiYazdir(no);

            List<DersNotu> list = Okul.OgrenciNotlariGetir(no);
            Listele2(list);
        }

        public void KitapListele()     //  16
        {
            Console.WriteLine();
            Console.WriteLine("16-Ögrencinin okudugu kitapları listele ---------------------------------------");

            int no = NoAl();
            BilgiYazdir(no);

            Listele3(Okul.KitapListele(no));
        }

        public void OkulEnBasariliBes()  //  17
        {
            Console.WriteLine();
            Console.WriteLine("17-Okuldaki en basarılı 5 ögrenciyi listele -----------------------------------");

            List<Ogrenci> liste = Okul.Ogrenciler.OrderByDescending(a => a.Ortalama).Take(5).ToList();

            Console.WriteLine();
            Listele(liste);
        }

        public void OkulEnBasarisizUc() //  18
        {
            Console.WriteLine();
            Console.WriteLine("18-Okuldaki en basarısız 3 ögrenciyi listele ----------------------------------");

            List<Ogrenci> liste = Okul.Ogrenciler.OrderBy(a => a.Ortalama).Take(3).ToList();

            Console.WriteLine();
            Listele(liste);

        }

        public void SubeEnBasariliBes() //  19
        {
            Console.WriteLine();
            Console.WriteLine("19-Subedeki en basarılı 5 ögrenciyi listele -----------------------------------");

            SUBE sb = AracGerecler.SubeAl("Listelemek istediğiniz şubeyi girin (A/B/C): ", true);
            List<Ogrenci> liste = Okul.Ogrenciler.Where(item => item.Sube == sb).OrderByDescending(a => a.Ortalama).Take(5).ToList();

            Console.WriteLine();
            Listele(liste);
        }

        public void SubeEnBasarisizUc()  //  20
        {
            Console.WriteLine();
            Console.WriteLine("20-Subedeki en basarısız 3 ögrenciyi listele ----------------------------------");

            SUBE sb = AracGerecler.SubeAl("Listelemek istediğiniz şubeyi girin (A/B/C): ", true);
            List<Ogrenci> liste = Okul.Ogrenciler.Where(item => item.Sube == sb).OrderByDescending(a => a.Ortalama).Take(5).ToList();

            Console.WriteLine();
            Listele(liste);
        }

        public void OgrenciSil()  //  21  
        {
            Console.WriteLine();
            Console.WriteLine("21-Ögrenci sil ----------------------------------------------------------------");

            if (Okul.Ogrenciler.Count == 0)
            {
                Console.WriteLine("Listede silinecek ögrenci yok.");
                return;
            }

            int no = NoAl();
            BilgiYazdir(no);

            string secim = AracGerecler.YaziAl("Ögrenciyi silmek istediginize emin misiniz (E/H): ", true);  // sayi olup olmadigi kontrol edildi

            if (secim.ToUpper() == "E")
            {
                Okul.OgrenciSil(no);
                Console.WriteLine("Ögrenci basarılı bir sekilde silindi.");

            }
            else if (secim.ToUpper() == "H")
            {
                return;
            }

        }

        public void Guncelle()  //  22 
        {
            Console.WriteLine();
            Console.WriteLine("22-Ögrenci Güncelle -----------------------------------------------------------");

            if (Okul.Ogrenciler.Count == 0)
            {
                Console.WriteLine("Listede silinecek ögrenci yok.");
                return;
            }

            int no = NoAl();
            string isim = AracGerecler.YaziAl("Ögrencinin adı: ", false);
            isim = IlkHarfiBuyut(isim);
            string soyisim = AracGerecler.YaziAl("Ögrencinin soyadı: ", false);
            soyisim = IlkHarfiBuyut(soyisim);
            DateTime tarih = AracGerecler.TarihAlGuncelle("Ögrencinin dogum tarihi: "); 
            CINSIYET cins = AracGerecler.KizMiErkekMi("Ögrencinin cinsiyeti (K/E): ",false);
            SUBE sube = AracGerecler.SubeAl("Ögrencinin subesi (A/B/C): ", false);

            Okul.Guncelle(no, isim, soyisim, tarih, cins, sube);
            Console.WriteLine(); 
            Console.WriteLine("Ogrenci güncellendi.");

        }

        public void Listele(List<Ogrenci> liste) 
        {
            if (liste.Count == 0)
            {
                Console.WriteLine("Listelenecek ögrenci yok.");
                return;
            }

            Console.WriteLine("Sube".PadRight(10, ' ') + "No".PadRight(10, ' ') +
               "Adı" + " " + "Soyadı".PadRight(21, ' ') +
               "Not Ort.".PadRight(15, ' ') + "Okuduğu Kitap Say.");

            Console.WriteLine("".PadRight(79, '-'));

            foreach (Ogrenci item in liste)
            {
                Console.WriteLine(item.Sube.ToString().PadRight(10, ' ') +
                    item.No.ToString().PadRight(10, ' ') +
                    (item.Ad + " " + item.Soyad).PadRight(25, ' ') +
                    item.Ortalama.ToString().PadRight(15, ' ')
                    + item.KitapSayisi);
            }

        }

        public void Listele1(List<Ogrenci> liste) 
        {
            if (liste.Count == 0)
            {
                Console.WriteLine("Listede ögrenci yok.");
                return;
            }
            //Sanem& Nil
            Console.WriteLine("Sube".PadRight(10, ' ') +
                              "No".PadRight(10, ' ') + ("Adı" + " " + "Soyadı").PadRight(21, ' ') +
                              "Sehir".PadRight(15, ' ') + "Semt");

            Console.WriteLine("".PadRight(79, '-'));

            foreach (Ogrenci item in liste)
            {
                Console.WriteLine(item.Sube.ToString().PadRight(10, ' ') +
                                  item.No.ToString().PadRight(10, ' ') +
                                  (item.Ad + " " + item.Soyad).PadRight(21, ' ') +
                                  item.Adresi.Il.PadRight(15, ' ') + item.Adresi.Ilce);
            }

        }

        public void Listele2(List<DersNotu> liste) 
        {
            if (liste.Count == 0) 
            {
                Console.WriteLine("Listede ögrenci yok.");
                return;
            }

            Console.WriteLine("Dersin Adi".PadRight(15, ' ') + "Notu");

            Console.WriteLine("".PadRight(20, '-'));

            foreach (DersNotu item in liste)
            {
                Console.WriteLine(item.DersAdi.ToString().PadRight(15) + item.Not);
            }
        }

        public void Listele3(List<string> liste)
        {
            Console.WriteLine("Okudugu Kitaplar");

            Console.WriteLine("-----------------");

            foreach (string item in liste)
            {
                Console.WriteLine(item);
            }
        }


        public void Close() 
        {
            Environment.Exit(0);
        }

        public void Menu() 
        {
            Console.WriteLine("------  Okul Yönetim Uygulamasi  -----");
            Console.WriteLine();
            Console.WriteLine("1-Ögrenci ekle");
            Console.WriteLine("2-Ögrencinin notunu gir");
            Console.WriteLine("3-Ögrencinin not ortalamasını gör");
            Console.WriteLine("4-Subenin not ortalamasını gör");
            Console.WriteLine("5-Ögrencinin adresini gir");
            Console.WriteLine("6-Ögrenci için açıklama gir");
            Console.WriteLine("7-Ögrencinin açıklamasını gör");
            Console.WriteLine("8-Ögrencinin okudugu kitabı gir");
            Console.WriteLine("9-Ögrencinin okudugu son kitabı gör");
            Console.WriteLine("10-Bütün ögrencileri listele");
            Console.WriteLine("11-Subeye göre ögrencileri listele");
            Console.WriteLine("12-Cinsiyete göre ögrencileri listele");
            Console.WriteLine("13-Su tarihten sonra dogan ögrencileri listele");
            Console.WriteLine("14-Illere göre sıralayarak ögrencileri listele");
            Console.WriteLine("15-Ögrencinin tüm notlarını listele");       
            Console.WriteLine("16-Ögrencinin okudugu kitapları listele");
            Console.WriteLine("17-Okuldaki en basarılı 5 ögrenciyi listele");
            Console.WriteLine("18-Okuldaki en basarısız 3 ögrenciyi listele");
            Console.WriteLine("19-Subedeki en basarılı 5 ögrenciyi listele");
            Console.WriteLine("20-Subedeki en basarısız 3 ögrenciyi listele");                                              
            Console.WriteLine("21-Ögrenci sil");
            Console.WriteLine("22-Ögrenci güncelle");
            Console.WriteLine();
            Console.WriteLine("Çıkıs yapmak için \"çıkıs\" yazıp \"enter\"a basın.");
        }

        public string SecimAl() 
        {
            Console.Write("Yapmak istediginiz islemi seçiniz: ");
            return Console.ReadLine();
        }

        static string IlkHarfiBuyut(string veri)
        {
            if (veri.Length == 0)
            {
                return veri;
            }

            string a = veri.Substring(0, 1).ToUpper() + veri.Substring(1).ToLower();
            return a;
        }

        public void SahteVeriGir() 
        {
            Okul.OgrenciEkle(1, "Elif", "Selçuk", new DateTime(2001, 5, 5), CINSIYET.Kiz, SUBE.A);
            Okul.OgrenciEkle(2, "Betül", "Yılmaz", new DateTime(2000, 10, 2), CINSIYET.Kiz, SUBE.B);
            Okul.OgrenciEkle(3, "Hakan", "Çelik", new DateTime(2001, 8, 12), CINSIYET.Erkek, SUBE.C);
            Okul.OgrenciEkle(4, "Kerem", "Akay", new DateTime(2002, 6, 10), CINSIYET.Erkek, SUBE.A);
            Okul.OgrenciEkle(5, "Hatice", "Çınar", new DateTime(2000, 6, 5), CINSIYET.Kiz, SUBE.B);
            Okul.OgrenciEkle(6, "Selim", "İleri", new DateTime(2004, 7, 20), CINSIYET.Erkek, SUBE.B);
            Okul.OgrenciEkle(7, "Selin", "Kamış", new DateTime(2002, 5, 20), CINSIYET.Kiz, SUBE.C);
            Okul.OgrenciEkle(8, "Sinan", "Avcı", new DateTime(2003, 2, 15), CINSIYET.Erkek, SUBE.A);
            Okul.OgrenciEkle(9, "Deniz", "Çoban", new DateTime(2000, 2, 2), CINSIYET.Erkek, SUBE.C);
            Okul.OgrenciEkle(10, "Selda", "Kavak", new DateTime(1999, 9, 20), CINSIYET.Kiz, SUBE.B);

        }
        public void SahteNotGir()
        {
            string[] dizi = new string[4];
            dizi[0] = "Türkçe";
            dizi[1] = "Matematik";
            dizi[2] = "Fen";
            dizi[3] = "Sosyal";
            Random rnd = new Random();

            for (int i = 1; i <= 10; i++)
            {
                for (int d = 0; d < 4; d++)
                {
                    Okul.NotEkle(i, dizi[d], rnd.Next(0, 100));
                }
            }
        }



        public void SahteNotGir1() 
        {
            Okul.NotEkle(1, "Türkçe", 80);
            Okul.NotEkle(1, "Matermatik", 85);
            Okul.NotEkle(1, "Fen", 90);
            Okul.NotEkle(1, "Sosyal", 70);
            Okul.NotEkle(2, "Türkçe", 65);
            Okul.NotEkle(2, "Matermatik", 55);
            Okul.NotEkle(2, "Fen", 40);
            Okul.NotEkle(2, "Sosyal", 70);
            Okul.NotEkle(3, "Türkçe", 45);
            Okul.NotEkle(3, "Matermatik", 70);
            Okul.NotEkle(3, "Fen", 30);
            Okul.NotEkle(3, "Sosyal", 50);
            Okul.NotEkle(4, "Türkçe", 40);
            Okul.NotEkle(4, "Matermatik", 64);
            Okul.NotEkle(4, "Fen", 82);
            Okul.NotEkle(4, "Sosyal", 50);
            Okul.NotEkle(5, "Türkçe", 75);
            Okul.NotEkle(5, "Matermatik", 70);
            Okul.NotEkle(5, "Fen", 72);
            Okul.NotEkle(5, "Sosyal", 60);
            Okul.NotEkle(6, "Türkçe", 37);
            Okul.NotEkle(6, "Matermatik", 64);
            Okul.NotEkle(6, "Fen", 35);
            Okul.NotEkle(6, "Sosyal", 50);
            Okul.NotEkle(7, "Türkçe", 65);
            Okul.NotEkle(7, "Matermatik", 74);
            Okul.NotEkle(7, "Fen", 82);
            Okul.NotEkle(7, "Sosyal", 40);
            Okul.NotEkle(8, "Türkçe", 37);
            Okul.NotEkle(8, "Matermatik", 55);
            Okul.NotEkle(8, "Fen", 48);
            Okul.NotEkle(8, "Sosyal", 20);
            Okul.NotEkle(9, "Türkçe", 55);
            Okul.NotEkle(9, "Matermatik", 90);
            Okul.NotEkle(9, "Fen", 82);
            Okul.NotEkle(9, "Sosyal", 40);
            Okul.NotEkle(10, "Türkçe", 32);
            Okul.NotEkle(10, "Matermatik", 30);
            Okul.NotEkle(10, "Fen", 55);
            Okul.NotEkle(10, "Sosyal", 55);

        }

        public void SahteAdresGir() 
        {

            Okul.AdresEkle(1, "Ankara", "Çankaya", "Bağlıca");
            Okul.AdresEkle(2, "Ankara", "Keçiören", "Osmangazi");
            Okul.AdresEkle(3, "Ankara", "Çankaya", "Çukurambar");
            Okul.AdresEkle(4, "İzmir", "Karşıyaka", "Alaybey");
            Okul.AdresEkle(5, "İzmir", "Gaziemir", "Atıfbey");
            Okul.AdresEkle(6, "İzmir", "Gaziemir", "Irmak");
            Okul.AdresEkle(7, "İzmir", "Bayraklı", "Adalet");
            Okul.AdresEkle(8, "İstanbul", "Arnavutköy", "Anadolu");
            Okul.AdresEkle(9, "İstanbul", "Beykoy", "Acarlar");
            Okul.AdresEkle(10, "İstanbul", "Ataşehir", "Atatürk");

        }

        public void SahteKitapEkle()
        {

            string[] kitaplar = new string[19];
            kitaplar[0] = ("Ölü Ozanlar Derneği");
            kitaplar[1] = ("George Orwell- 1984");
            kitaplar[2] = ("Bülbülü Öldürmek");
            kitaplar[3] = ("Hayvan Çiftliği");
            kitaplar[4] = ("Harry Potter ve Felsefe Taşı");
            kitaplar[5] = ("Çavdar Tarlasında Çocuklar");
            kitaplar[6] = ("Büyük Umutlar");
            kitaplar[7] = ("Gurur ve Ön Yargı");
            kitaplar[8] = ("Jane Eyre");
            kitaplar[9] = ("Uğultulu Tepeler");
            kitaplar[10] = ("Frankenstein");
            kitaplar[11] = ("Kuşların Şarkısı");
            kitaplar[12] = ("Noel Şarkısı");
            kitaplar[13] = ("Harry Potter Sırlar Odası");
            kitaplar[14] = ("Harry Potter Azkaban Tutsağı");
            kitaplar[15] = (" Bir Ses Böler Geceyi");
            kitaplar[16] = ("Masal Masal İçinde");
            kitaplar[17] = ("Sis ve Gece");
            kitaplar[18] = ("Agatha'nın Anahtarı ");

            Random rnd = new Random();
            for (int i = 1; i < 10; i++)
            {
                Okul.KitapEkle(i, kitaplar[rnd.Next(0, 19)]);
            }
        }

        public int NoAl()
        {
            int no;
            bool donsunMu = true;
            do
            {
                no = AracGerecler.SayiAl("Ögrencinin numarasi: ");   // Sayi kontrol
                if (!Okul.VarMi(no))  // böyle bir ögrenci var mi?
                {
                    Console.WriteLine("Bu numarada bir ögrenci yok.Tekrar deneyin");
                    continue;
                }
                return no;
            } while (true);
        }

        public void BilgiYazdir(int no)
        {
            Console.WriteLine();
            Console.Write("Ögrencinin Adı Soyadı: ");
            Console.WriteLine(Okul.AdiSoyadiGetir(no));    // Ad-Soyad getir methodu
            Console.Write("Ögrencinin Subesi: ");
            Console.WriteLine(Okul.SubeGetir(no));        // Sube Getir methodu
            Console.WriteLine();

        }


    }
}
