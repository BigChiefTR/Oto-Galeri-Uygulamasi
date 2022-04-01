using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace OtoGaleriUygulamasi_G022
{
    class Program
    {

        static Galeri OtoGaleri = new Galeri();

        //1) Kullanıcı ile iletişim kurulan,
        //bilgi alış verişi yapılan tüm kodlar bu sayfada yazılacak

        //2) Bu sayfa Araba classını bilmeyecek, Galeri classı ara katman olacak.
        //Araba eklemek istediğimizde kullanıcıdan veriler buradan alacağız, galeri classındaki bir metoda göndereceğiz, araba oluşturma ve listeye ekleme işini galeri classındaki metot yapacak.


        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Uygulama();
        }

        static void Uygulama()
        {
            SahteArabaEkle();
            Menu();
        }

        static void Menu()
        {
            Console.WriteLine("Galeri Otomasyon");
            Console.WriteLine("1- Araba Kirala(K)");
            Console.WriteLine("2- Araba Teslim Al(T)");
            Console.WriteLine("3- Kiradaki arabaları listele(R)");
            Console.WriteLine("4- Müsait arabaları listele(M)");
            Console.WriteLine("5- Tüm arabaları listele(A)");
            Console.WriteLine("6- Kiralama İptali(I)");
            Console.WriteLine("7- Yeni araba Ekle(Y)");
            Console.WriteLine("8- Araba sil(S)");
            Console.WriteLine("9- Bilgileri göster(G)");

            while (true)
            {
                Console.WriteLine();
                IslemYap(SecimAl("Seçiminiz: "));
                // Console.WriteLine();


            }
        }

        static void ArabaKirala()
        {
            Console.WriteLine("-Araç Kirala-");


            string plaka;
            while (true)
            {
                bool kiradaMi = false;
                Console.Write("Kiralanacak aracın plakası: ");
                plaka = Console.ReadLine();
                if (PlakaAl(plaka) == false)
                {
                    Console.WriteLine("Giriş tanımlanamadı. Tekrar deneyin.");
                }
                else
                {
                    foreach (Araba item in OtoGaleri.Arabalar)
                    {
                        if (item.Plaka == plaka.ToUpper())
                        {
                            if (item.Durum == DURUM.Galeride)
                            {
                                goto SUREAL;
                            }
                            else
                            {
                                Console.WriteLine("Araç zaten kirada.");
                                kiradaMi = true;
                                break;
                            }
                        }
                    }
                    if (kiradaMi == false)
                    {
                        Console.WriteLine("Galeriye ait böyle bir araç yok.");
                    }
                }
            }

        SUREAL:

            int sure;
            string sureStr;
            do
            {
                Console.Write("Kiralama süresi: ");
                sureStr = Console.ReadLine();
                if (int.TryParse(sureStr, out sure) == true)
                {
                    break;
                }
                Console.WriteLine("Hatalı giriş yapıldı, tekrar deneyin.");
            }
            while (true);

            OtoGaleri.ArabaKirala(plaka, sure);
            Console.WriteLine();
            Console.WriteLine(plaka + " plakalı araç " + sure + " saatliğine kiralandı.");

        }

        static void ArabaTeslimAl()
        {
            Console.WriteLine("-Araç Teslim-");
            if (OtoGaleri.KiradakiAracSayisi == 0)
            {
                Console.WriteLine("Kirada araç yok.");
                return;
            }

            string plaka;
            do
            {
                bool galerideMi = false;
                Console.Write("Teslim edilecek aracın plakası: ");
                plaka = Console.ReadLine().ToUpper();
                if (PlakaAl(plaka) == true)
                {
                    foreach (Araba item in OtoGaleri.Arabalar)
                    {
                        if (item.Plaka == plaka && item.Durum == DURUM.Kirada)
                        {
                            item.Durum = DURUM.Galeride;
                            Console.WriteLine("Araç galeride beklemeye alındı.");
                            return;
                        }
                        if (item.Durum == DURUM.Galeride && item.Plaka == plaka)
                        {
                            Console.WriteLine("Hatalı giriş yapıldı. Araç zaten galeride.");
                            galerideMi = true;
                            break;

                        }
                        if (item.Plaka != plaka)
                        {
                            continue;


                        }
                    }
                    if (galerideMi == false)
                    {
                        Console.WriteLine("Galeriye ait bu plakada bir araç yok.");
                    }
                }
                else
                {
                    Console.WriteLine("Giriş tanımlanamadı.Tekrar deneyin.");
                }
            } while (true);

        }


        static void KiradakiArabalariListele()
        {
            if (OtoGaleri.KiradakiAracSayisi == 0)
            {
                Console.WriteLine("Listelenecek araç yok.");
                return;
            }
            Console.WriteLine("-Kiradaki Araçlar-");

            Console.WriteLine("Plaka".PadRight(15) + "Marka".PadRight(14) + "Kiralama Bedeli".PadRight(25)
                + "Araç Tipi".PadRight(16) + "Kiralanma Sayısı".PadRight(26) + "Durum".PadRight(10));
            Console.WriteLine("----------------------------------------------------------------------------------------------------");
            foreach (Araba item in OtoGaleri.Arabalar)
            {
                if (item.Durum == DURUM.Kirada)
                {
                    string bedel = item.KiralamaBedeli.ToString() + "₺";
                    Console.WriteLine(item.Plaka.PadRight(15) + item.Marka.PadRight(15) + bedel.PadRight(24) + item.AracTipi.ToString().PadRight(16)
                        + item.KiralanmaSayisi.ToString().PadRight(26) + item.Durum.ToString().PadRight(10));
                }
            }
        }

        static void MusaitArabalariListele()
        {
            if (OtoGaleri.GaleridekiAracSayisi == 0)
            {
                Console.WriteLine("Listelenecek araç yok.");
                return;
            }
            Console.WriteLine("-Müsait Araçlar-");
            Console.WriteLine("Plaka".PadRight(15) + "Marka".PadRight(14) + "Kiralama Bedeli".PadRight(25)
                + "Araç Tipi".PadRight(16) + "Kiralanma Sayısı".PadRight(26) + "Durum".PadRight(10));
            Console.WriteLine("----------------------------------------------------------------------------------------------------");
            foreach (Araba item in OtoGaleri.Arabalar)
            {
                if (item.Durum == DURUM.Galeride)
                {
                    string bedel = item.KiralamaBedeli.ToString() + "₺";
                    Console.WriteLine(item.Plaka.PadRight(15) + item.Marka.PadRight(15) + bedel.PadRight(24) + item.AracTipi.ToString().PadRight(16)
                       + item.KiralanmaSayisi.ToString().PadRight(26) + item.Durum.ToString().PadRight(10));
                }
            }
        }

        static void TumArabalariListele()
        {
            if (OtoGaleri.ToplamAracSayisi == 0)
            {
                Console.WriteLine("Listelenecek araç yok.");
                return;
            }
            Console.WriteLine("-Tüm Araçlar-");
            Console.WriteLine("Plaka".PadRight(15) + "Marka".PadRight(14) + "Kiralama Bedeli".PadRight(25)
                + "Araç Tipi".PadRight(16) + "Kiralanma Sayısı".PadRight(26) + "Durum".PadRight(10));
            Console.WriteLine("----------------------------------------------------------------------------------------------------");
            foreach (Araba item in OtoGaleri.Arabalar)
            {
                string bedel = item.KiralamaBedeli.ToString() + "₺";
                Console.WriteLine(item.Plaka.PadRight(15) + item.Marka.PadRight(15) + bedel.PadRight(24) + item.AracTipi.ToString().PadRight(16)
                        + item.KiralanmaSayisi.ToString().PadRight(26) + item.Durum.ToString().PadRight(10));
            }
        }

        static void KiralamaIptali()
        {

            Console.WriteLine("-Kiralama İptali-");
            if (OtoGaleri.KiradakiAracSayisi == 0)
            {
                Console.WriteLine("Kirada araç yok.");
                return;
            }


            do
            {
                bool galerideMi = false;
                Console.Write("Kiralaması iptal edilecek aracın plakası: ");
                string plaka = Console.ReadLine().ToUpper();
                if (PlakaAl(plaka) == false)
                {
                    Console.WriteLine("Giriş tanımlanamadı. Tekrar deneyin.");
                }
                else
                {
                    foreach (Araba item in OtoGaleri.Arabalar)
                    {
                        if (item.Plaka == plaka && item.Durum == DURUM.Kirada)
                        {
                            item.Durum = DURUM.Galeride;
                            item.KiralanmaSureleri.RemoveAt(item.KiralanmaSureleri.Count - 1);
                            Console.WriteLine();
                            Console.WriteLine("İptal gerçekleştirildi.");
                            return;

                        }
                        else if (item.Durum == DURUM.Galeride && item.Plaka == plaka)
                        {
                            galerideMi = true;
                            Console.WriteLine("Hatalı giriş yapıldı. Araç zaten galeride.");
                            break;
                        }
                    }
                    if (galerideMi == false)
                    {
                        Console.WriteLine("Galeriye ait böyle bir araç yok.");
                    }
                }

            } while (true);
        }

        static void ArabaEkle()
        {
            Console.WriteLine("-Yeni Araç Ekle-");
            string plaka;
            while (true)
            {
                bool galerideMi = false;
                Console.Write("Plaka: ");
                plaka = Console.ReadLine();
                if (PlakaAl(plaka) == false)
                {
                    Console.WriteLine("Bu şekilde plaka girişi yapamazsınız. Tekrar deneyin.");

                }
                else
                {
                    foreach (Araba item in OtoGaleri.Arabalar)
                    {
                        if (item.Plaka == plaka.ToUpper())
                        {
                            galerideMi = true;
                            Console.WriteLine("Aynı plakada araç mevcut. Girdiğiniz plakayı kontrol edin.");
                            break;
                        }

                    }
                    if (galerideMi == false)
                    {
                        break;
                    }
                }
            }

            Console.Write("Marka: ");
            string marka = Console.ReadLine();


            float kiralamaBedeli;
            string kBedeli;
            while (true)
            {
                Console.Write("Kiralama bedeli: ");
                kBedeli = Console.ReadLine();
                if (float.TryParse(kBedeli, out kiralamaBedeli) == true)
                {
                    break;
                }
                Console.WriteLine("Hatalı giriş yapıldı, tekrar deneyin.");
            }
            //
            Console.WriteLine(" Araç Tipleri");
            Console.WriteLine("- SUV için 1");
            Console.WriteLine("- Hatchback için 2");
            Console.WriteLine("- Sedan için 3");

            ARAC_TIPI aracTipi = ARAC_TIPI.SUV;
            string arabaTipi;
            int arTipi;
            while (true)
            {
                Console.Write("Araç Tipi: ");
                arabaTipi = Console.ReadLine();
                if (int.TryParse(arabaTipi, out arTipi) == false)
                {
                    Console.WriteLine("Hatalı giriş yapıldı, tekrar deneyin.");
                }
                else if (arTipi >= 1 && arTipi <= 3)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Hatalı giriş yapıldı, tekrar deneyin.");
                }
            }

            switch (arTipi)
            {
                case 1:
                    aracTipi = ARAC_TIPI.SUV;
                    break;
                case 2:
                    aracTipi = ARAC_TIPI.Hatchback;
                    break;
                case 3:
                    aracTipi = ARAC_TIPI.Sedan;
                    break;
            }




            OtoGaleri.ArabaEkle(plaka, marka, kiralamaBedeli, aracTipi);
            Console.WriteLine();
            Console.WriteLine("Araç başarılı bir şekilde eklendi.");

        }


        static void ArabaSil()
        {
            Console.WriteLine("-Araba Sil-");
            string plaka;
            do
            {
                bool kiradaMi = false;
                Console.Write("Silinmek istenen araç plakasını girin: ");
                plaka = Console.ReadLine().ToUpper();
                if (PlakaAl(plaka) == true)
                {
                    foreach (Araba item in OtoGaleri.Arabalar)
                    {
                        if (item.Plaka == plaka)
                        {
                            if (item.Durum == DURUM.Galeride)
                            {
                                OtoGaleri.Arabalar.Remove(item);
                                Console.WriteLine();
                                Console.WriteLine("Araç silindi.");
                                return;
                            }
                            else
                            {
                                kiradaMi = true;
                                break;
                            }

                        }

                    }

                }
                if (PlakaAl(plaka) == false)
                {
                    Console.WriteLine("Giriş tanımlanamadı. Tekrar deneyin. ");
                }
                if (kiradaMi == true)
                {
                    Console.WriteLine("Araç kirada olduğu için silme işlemi gerçekleştirilemedi.");
                }
                if (PlakaAl(plaka) == true && kiradaMi == false)
                {
                    Console.WriteLine("Galeriye ait bu plakada bir araç yok.");
                }
            } while (true);

        }

        static void BilgileriGoster()
        {

            Console.WriteLine("-Galeri Bilgileri-");
            Console.WriteLine("Toplam Araç Sayısı: " + OtoGaleri.ToplamAracSayisi);
            Console.WriteLine("Kiradaki Araç Sayısı: " + OtoGaleri.KiradakiAracSayisi);
            Console.WriteLine("Bekleyen Araç Sayısı: " + OtoGaleri.GaleridekiAracSayisi);
            Console.WriteLine("Toplam araç kiralama süresi: " + OtoGaleri.ToplamAracKiralamaSuresi);
            Console.WriteLine("Toplam araç kiralama adedi: " + OtoGaleri.ToplamAracKiralamaAdedi);
            Console.WriteLine("Ciro: " + OtoGaleri.Ciro);
            Console.WriteLine();
        }






        static void SahteArabaEkle()
        {
            OtoGaleri.ArabaEkle("34asd3434", "Ford", 120, ARAC_TIPI.Hatchback);
            OtoGaleri.ArabaEkle("35aa3535", "Opel", 260, ARAC_TIPI.Sedan);
            OtoGaleri.ArabaEkle("40bb5454", "Fiat", 220, ARAC_TIPI.Hatchback);
            OtoGaleri.ArabaEkle("10gg5454", "Bmw", 400, ARAC_TIPI.SUV);
            OtoGaleri.ArabaEkle("41mm2022", "Mercedes", 600, ARAC_TIPI.SUV);
        }

        static string SecimAl(string text)
        {

            string[] menuSecenekleri = {"1", "2", "3", "4", "5", "6", "7", "8", "9",
                                        "K", "T", "R", "M", "A", "I", "Y", "S", "G"};
            while (true)
            {
                Console.Write(text);
                string giris = Console.ReadLine().ToUpper();

                foreach (string mS in menuSecenekleri)
                {
                    if (giris == mS)
                    {
                        Console.WriteLine();
                        return giris;
                    }
                }

                Console.WriteLine();

            }
        }

        static void IslemYap(string secim)
        {
            switch (secim)
            {
                case "1":
                case "K":
                    ArabaKirala();
                    break;
                case "2":
                case "T":
                    ArabaTeslimAl();
                    break;
                case "3":
                case "R":
                    KiradakiArabalariListele();
                    break;
                case "4":
                case "M":
                    MusaitArabalariListele();
                    break;
                case "5":
                case "A":
                    TumArabalariListele();
                    break;
                case "6":
                case "I":
                    KiralamaIptali();
                    break;
                case "7":
                case "Y":
                    ArabaEkle();
                    break;
                case "8":
                case "S":
                    ArabaSil();
                    break;
                case "9":
                case "G":
                    BilgileriGoster();
                    break;

            }
        }


        static bool PlakaAl(string text)
        {




            string plaka = text;

            bool boslukVarMi = false;
            for (int i = 0; i < plaka.Length; i++)
            {
                if (char.IsWhiteSpace(plaka[i]))
                {
                    boslukVarMi = true;
                    break;
                }
            }

            int plakaKarakterSayisi = plaka.Length;
            int plSayi;

            if (boslukVarMi == false && (plakaKarakterSayisi >= 7 && plakaKarakterSayisi <= 9))
            {
                if (int.TryParse(plaka.Substring(0, 2), out plSayi) == true)
                {


                    if (char.IsLetter(plaka[2]) && int.TryParse(plaka.Substring(3, 4), out plSayi))
                    {
                        return true;
                    }
                    else if (char.IsLetter(plaka[2]) && char.IsLetter(plaka[3]))
                    {
                        int uzunluk = plaka.Substring(4).Length;

                        if ((uzunluk >= 3 && uzunluk <= 4) && int.TryParse(plaka.Substring(4), out plSayi))
                        {
                            return true;
                        }
                    }
                    if (char.IsLetter(plaka[2]) && char.IsLetter(plaka[3]) && char.IsLetter(plaka[4]))
                    {
                        int uzunluk = plaka.Substring(5).Length;
                        if ((uzunluk >= 2 && uzunluk <= 4) && int.TryParse(plaka.Substring(5), out plSayi))
                        {
                            return true;
                        }
                    }
                }


            }

            return false;

        }
    }
}
