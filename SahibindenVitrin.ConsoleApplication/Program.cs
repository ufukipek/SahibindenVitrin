using Microsoft.Graph;
using SahibindenVitrin.ConsoleApplication.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;

namespace SahibindenVitrin.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            IlanGetir("https://www.sahibinden.com");

            Console.ReadKey();
        }

        /// <summary>
        /// İlanları ekrana yazdıran fonksiyon
        /// </summary>
        /// <param name="url">İlanların alınacağım url bilgisi</param>
        private static void IlanGetir(string url)
        {
            HtmlAgilityPack.HtmlWeb htmlWeb = new HtmlAgilityPack.HtmlWeb();
            HtmlAgilityPack.HtmlDocument document = htmlWeb.Load(url);
            var linkList = document.DocumentNode.SelectNodes("//ul[@class='vitrin-list clearfix']//li")?.ToList();

            List<IlanDetay> ilanlar = new List<IlanDetay>();
            if (linkList != null)
            {
                foreach (var link in linkList)
                {
                    IlanDetay ilan = new IlanDetay();

                    int icerikBaslangic = link.InnerHtml.IndexOf("/ilan");

                    if (icerikBaslangic >= 0)
                    {
                        int icerikBitis = link.InnerHtml.Substring(icerikBaslangic).IndexOf("detay");
                        string aranacaklink = link.InnerHtml.Substring(icerikBaslangic, icerikBitis).ToString();

                        ilan.Link = url + aranacaklink + "detay";
                        document = htmlWeb.Load(ilan.Link);

                        var linkBaslik = document.DocumentNode.SelectNodes("//div[@class='classifiedDetailTitle']//h1")?.ToList();

                        if (linkBaslik != null)
                        {
                            ilan.Baslik = linkBaslik[0].InnerText;

                            var linkFiyat = document.DocumentNode.SelectNodes("//div[@class='classifiedInfo ']//h3")?.ToList();
                            if (linkFiyat != null)
                            {
                                int icerikBitisMoney = linkFiyat[0].InnerHtml.IndexOf("TL");
                                if (icerikBitisMoney >= 0)
                                {
                                    string aranacakFiyat = linkFiyat[0].InnerHtml.Substring(0, icerikBitisMoney).ToString().Trim().Replace(" ", string.Empty).Replace(".", string.Empty);
                                    ilan.Fiyat = double.Parse(aranacakFiyat);
                                    ilanlar.Add(ilan);
                                    ilan.Yazdir();
                                }
                            }
                        }
                    }
                }

                double toplamFiyat = 0;
                foreach (IlanDetay item in ilanlar)
                {
                    toplamFiyat += item.Fiyat;
                }

                Console.Write("Ortalama Fiyat = " + String.Format("{0:C2}", (toplamFiyat / ilanlar.Count)));
            }
            else
            {
                Console.WriteLine("Sunucuya Bağlanılamadı... !" + "\n" + "\n");
                Console.WriteLine("Tekrar denemek için herhangi bir tuşa basınız.");
            }
        }
    }
}
