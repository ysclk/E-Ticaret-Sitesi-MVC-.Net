using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZYClkETicaret.WEBUI.Models;

namespace ZYClkETicaret.WEBUI.Controllers
{
    using System.Data.Odbc;
    using ZYClkETicaret.WEBUI.App_Classes;
    using static ZYClkETicaret.WEBUI.App_Classes.Sepet;

 //   [Authorize]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
           
            return View();
        }

        public PartialViewResult Sepet()
        {
            return PartialView();
        } 
        public PartialViewResult Slider()
        {
            var data = Context.Baglanti.Resim.Where(x => x.BuyukYol.Contains("Slider")).ToList();
            return PartialView(data);
        }
        public PartialViewResult YeniUrunler()
        {
            var data = Context.Baglanti.Urun.ToList();

            return PartialView(data);
        }
        public PartialViewResult Servisler()
        {
            return PartialView();
        }
        public PartialViewResult Modalar()
        {
            return PartialView();
        }
        public PartialViewResult Markalar()
        {
            var data = Context.Baglanti.Marka.ToList();
            return PartialView(data);
        }

        public void SepeteEkle(int id)
        {
            SepetItem si = new SepetItem();
            Urun u = Context.Baglanti.Urun.FirstOrDefault(x => x.Id == id);
            si.Urun = u;
            si.Adet = 1;
            si.Indirim = 0;
            Sepet s = new Sepet();
            s.SepeteEkle(si);
        }
        public PartialViewResult MiniSepetWidget()
        {
            if (HttpContext.Session["AktifSepet"] != null)
            {
                return PartialView((Sepet)HttpContext.Session["AktifSepet"]);
            }
            else
            {
                return PartialView();
            }
        }

        public void SepetUrunAdetDusur(int id)
        {
            if (HttpContext.Session["AktifSepet"] != null)
            {
                Sepet s = (Sepet)HttpContext.Session["AktifSepet"];
                if (s.Urunler.FirstOrDefault(x => x.Urun.Id == id).Adet > 1)
                {
                    s.Urunler.FirstOrDefault(x => x.Urun.Id == id).Adet--;
                }
                else
                {
                    SepetItem si = s.Urunler.FirstOrDefault(x => x.Urun.Id == id);
                    s.Urunler.Remove(si);
                }
            }
        }

        

        public ActionResult UrunDetay(string id)
        {
            Urun u = Context.Baglanti.Urun.FirstOrDefault(x => x.Adi == id);
            List<UrunOzellik> uos = Context.Baglanti.UrunOzellik.Where(x => x.UrunID == u.Id).ToList();
            Dictionary<string, List<OzellikDeger>> ozellik = new Dictionary<string, List<OzellikDeger>>();
            List<OzellikDeger> degers = new List<OzellikDeger>();
            foreach (UrunOzellik uo in uos)
            {
                OzellikTip ot = Context.Baglanti.OzellikTip.FirstOrDefault(x => x.Id == uo.OzellikTipID);
                OzellikDeger od = Context.Baglanti.OzellikDeger.FirstOrDefault(x => x.OzellikTipID == ot.Id && x.Id == uo.OzellikDegerID);
                bool farkliTip = false;
                foreach (var item in ozellik)
                {
                    if (item.Key != ot.Adi)
                        farkliTip = true;
                    else
                        farkliTip = false;
                }
                if (farkliTip)
                    degers = new List<OzellikDeger>();
                degers.Add(od);
                if (ozellik.Any(x => x.Key == ot.Adi))
                    ozellik[ot.Adi] = degers;
                else
                    ozellik.Add(ot.Adi, degers);
            }
            ViewBag.Ozellikler = ozellik;
            return View(u);
        }

        public PartialViewResult KategoriListesi()
        {

            var data = Context.Baglanti.Kategori.Where(x=>x.ustkategori == 0).ToList();
            return PartialView(data);
        }

        public ActionResult ElektronikUrunler(int id)
        { 
            var elist = Context.Baglanti.Kategori.FirstOrDefault(x => x.Id == id);
            return View();
        }

        public ActionResult Moda()
        {
            var elist = Context.Baglanti.Kategori.FirstOrDefault(x => x.ustkategori == 1);
            ViewBag.AltKategoriler = Context.Baglanti.Kategori.Where(x => x.ustkategori == 1).ToList();
            ViewBag.Markalar = Context.Baglanti.Marka.ToList();
            ViewBag.Renkler = Context.Baglanti.OzellikDeger.Where(x => x.OzellikTipID == 14).ToList();    
            ViewBag.Urunler = Context.Baglanti.Urun.Where(x => x.KategoriID == 1).ToList();
            var data = Context.Baglanti.Resim.ToList();  

            return View(data);
        }

        public ActionResult Elektronik()
        {
            var elist = Context.Baglanti.Kategori.FirstOrDefault(x => x.Id == 2);
            ViewBag.Markalar = Context.Baglanti.Marka.ToList();
            ViewBag.Renkler = Context.Baglanti.OzellikDeger.Where(x => x.OzellikTipID == 11).ToList();
            ViewBag.Ekranlar = Context.Baglanti.OzellikDeger.Where(x => x.OzellikTipID == 8).ToList();
            ViewBag.Ramler = Context.Baglanti.OzellikDeger.Where(x => x.OzellikTipID == 9).ToList();
            ViewBag.Hafizalar = Context.Baglanti.OzellikDeger.Where(x => x.OzellikTipID == 10).ToList();
            ViewBag.Urunler = Context.Baglanti.Urun.Where(x => x.KategoriID == 2).ToList();
             var data = Context.Baglanti.Resim.ToList();
            ViewBag.AltKategoriler = Context.Baglanti.Kategori.Where(x => x.ustkategori == 2).ToList();

            return View(data);
        }

        public ActionResult Sepetim()
        {
            Sepet s = new Sepet();
            if (HttpContext.Session["AktifSepet"] != null)
            {
               s = (Sepet)HttpContext.Session["AktifSepet"];
                ViewBag.Urunler = s.Urunler;
            }
            else
            {
                ViewBag.Mesaj = "Sepet Bos";
            }
           
         
            return View(s.Urunler);

        }
        public ActionResult OdemeyeGec()
        {

            Sepet s = new Sepet();
            if (HttpContext.Session["AktifSepet"] != null)
            {
                s = (Sepet)HttpContext.Session["AktifSepet"];
                ViewBag.Urunler = s.Urunler;
            }
            else
            {
                ViewBag.Mesaj = "Sepet Bos";
            }


            return View(s.Urunler);
        

        }


    }
}