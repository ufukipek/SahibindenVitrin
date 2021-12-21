using System;
using System.Collections.Generic;
using System.Text;

namespace SahibindenVitrin.ConsoleApplication.Model
{
    /// <summary>
    /// İlan detay modeli
    /// </summary>
    class IlanDetay
    {
        /// <summary>
        /// İlan başlık bilgisi
        /// </summary>
        public string Baslik { get; set; }

        /// <summary>
        /// İlan fiyat bilgisi
        /// </summary>
        public double Fiyat { get; set; }

        /// <summary>
        /// İlana ait link bilgisi
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// İlana ait bilgileri console da çıktı olarak iletir.
        /// </summary>
        public void Yazdir()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Baslik);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Fiyat = " + String.Format("{0:N}", Fiyat)+" TL");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(Link);
            Console.ResetColor();
            Console.WriteLine("***************************************************************************");
        }
    }
}
