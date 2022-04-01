using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoGaleriUygulamasi_G022
{
    class Galeri
    {
        public List<Araba> Arabalar = new List<Araba>();
        public int ToplamAracSayisi 
        {
            get
            {
                return this.Arabalar.Count;
            }
        }
        public int KiradakiAracSayisi
        {
            get
            {
                int adet = 0;

                foreach (Araba item in this.Arabalar)
                {
                    if (item.Durum == DURUM.Kirada)
                    {
                        adet++;
                    }
                }

                return adet;

            }
        }
        public int GaleridekiAracSayisi 
        { 
            get
            {
                return this.ToplamAracSayisi - this.KiradakiAracSayisi;
            } 
        }
        public int ToplamAracKiralamaSuresi 
        {
            get
            {
                int toplam = 0;
                
                foreach(Araba arb in this.Arabalar)
                {
                    toplam += arb.KiralanmaSuresi;
                }
                
                return toplam;
            }
        }
        public int ToplamAracKiralamaAdedi
        {
            get
            {
                int toplam = 0;

                foreach (Araba item in this.Arabalar)
                {
                    toplam += item.KiralanmaSayisi;
                }
                return toplam;
            }
        }
        public float Ciro 
        { 
            get
            {
                float cr = 0;
                
                foreach(Araba arb in this.Arabalar)
                {
                    cr += (arb.KiralamaBedeli * arb.KiralanmaSuresi);
                }
                
                return cr;
            }
        }

        public void ArabaEkle(string plaka, string marka, float kiralamaBedeli, ARAC_TIPI aracTipi)
        {
            Araba a = new Araba(plaka, marka, kiralamaBedeli, aracTipi);

            this.Arabalar.Add(a);
        }

        public void ArabaKirala(string plaka, int sure)
        {
            Araba a = null;
            foreach (Araba item in this.Arabalar)
            {
                if (item.Plaka == plaka.ToUpper())
                {
                    a = item;
                    break;
                }
            }
            a.Durum = DURUM.Kirada;
            a.KiralanmaSureleri.Add(sure);
        }

        public void ArabaTeslimAl(string plaka)
        {
            Araba a = null;
            foreach (Araba item in this.Arabalar)
            {
                if (item.Plaka == plaka.ToUpper())
                {
                    a = item;
                    break;
                }
            }
            a.Durum = DURUM.Galeride;
        }
    }
}
