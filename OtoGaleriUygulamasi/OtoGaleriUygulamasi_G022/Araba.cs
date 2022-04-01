using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoGaleriUygulamasi_G022
{
    class Araba
    {
        public string Plaka { get; set; }
        public string Marka { get; set; }
        public float KiralamaBedeli { get; set; }
        public ARAC_TIPI AracTipi { get; set; }

        public DURUM Durum { get; set; }

        public List<int> KiralanmaSureleri = new List<int>();
        public int KiralanmaSayisi
        {
            get
            {
                return this.KiralanmaSureleri.Count;
            }
        }
        public int KiralanmaSuresi
        {
            get
            {
                int toplam = 0;
                foreach (int item in this.KiralanmaSureleri)
                {
                    toplam += item;
                }
                return toplam;
            }
        }



        public Araba(string plaka, string marka, float kiralamaBedeli, ARAC_TIPI aracTipi)
        {
            this.Plaka = plaka.ToUpper();
            this.Marka = marka.ToUpper();
            this.KiralamaBedeli = kiralamaBedeli;
            this.AracTipi = aracTipi;
            this.Durum = DURUM.Galeride;
        }

    }
    public enum DURUM
    {
        Galeride,
        Kirada
    }
    public enum ARAC_TIPI
    {
        SUV,
        Hatchback,
        Sedan
    }

}
