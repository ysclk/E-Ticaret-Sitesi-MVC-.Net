using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZYClkETicaret.WEBUI.Controllers
{
    using System.Configuration;
    using System.Drawing;
    using System.IO;
    using System.Text;
    using System.Web.Security;
    using ZYClkETicaret.WEBUI.App_Classes;
    using ZYClkETicaret.WEBUI.Models;
    [Authorize]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Urunler()
        {
            return View(Context.Baglanti.Urun.ToList());
        } 
        
        public ActionResult UrunEkle()
        {
            ViewBag.Kategoriler = Context.Baglanti.Kategori.ToList();
            ViewBag.Markalar = Context.Baglanti.Marka.ToList();
            return View();
        }
        [HttpPost]

        public ActionResult UrunEkle(Urun urun)
        {
           //urun.SonKullanmaTarihi = DateTime.Now;
            urun.EklenmeTarihi = DateTime.Now;
            Context.Baglanti.Urun.Add(urun);
            Context.Baglanti.SaveChanges();
            return RedirectToAction("Urunler");
        }

        public ActionResult Markalar()
        {
            
            return View(Context.Baglanti.Marka.ToList());
        }
         public ActionResult MarkaEkle()
        {
            
            return View();
        }


        [HttpPost]
        public ActionResult MarkaEkle(Marka marka, HttpPostedFileBase fileUpload)
        {
         
            int resimID = 0;
            if(fileUpload != null)
            {
                Image img = Image.FromStream(fileUpload.InputStream);
                int width = Convert.ToInt32(ConfigurationManager.AppSettings["MarkaWidth"].ToString());
                int height = Convert.ToInt32(ConfigurationManager.AppSettings["MarkaHeight"].ToString());
                Bitmap bm = new Bitmap(img, width, height);
                string name = "/Content/MarkaResim/" + Guid.NewGuid() + Path.GetExtension(fileUpload.FileName);
                bm.Save(Server.MapPath(name));
                Resim rsm = new Resim();
                rsm.OrtaYol = name;
                Context.Baglanti.Resim.Add(rsm);
                Context.Baglanti.SaveChanges();
                if(rsm.Id != null)
                {
                    resimID = rsm.Id;
                }
                if(rsm.Id != -1)
                {
                    marka.ResimID = resimID;
                }
            
              
            }

            Context.Baglanti.Marka.Add(marka);
            Context.Baglanti.SaveChanges();
            return RedirectToAction("Markalar");
        }

        public ActionResult Kategoriler()
        {

            return View(Context.Baglanti.Kategori.ToList());
        }

        public ActionResult KategoriEkle()
        {
            var data = Context.Baglanti.Kategori.Where(x => x.ustkategori == 0).ToList();
            ViewBag.Kategoriler = data;
            return View();
        }

        [HttpPost]
        public ActionResult KategoriEkle(Kategori ktg)
        {
            Context.Baglanti.Kategori.Add(ktg);
            Context.Baglanti.SaveChanges();
          
            return RedirectToAction("Kategoriler");
        }

        public ActionResult OzellikTipleri()
        {
            return View(Context.Baglanti.OzellikTip.ToList());
        }

        public ActionResult OzellikTipEkle()
        {
            return View(Context.Baglanti.Kategori.ToList());
        }

        [HttpPost]
        public ActionResult OzellikTipEkle(OzellikTip otip)
        {
            Context.Baglanti.OzellikTip.Add(otip);
            Context.Baglanti.SaveChanges();
            return RedirectToAction("OzellikTipleri");
        }

        public ActionResult OzellikDegerleri()
        {
            return View(Context.Baglanti.OzellikDeger.ToList());
        }

        public ActionResult OzellikDegerEkle()
        {
            return View(Context.Baglanti.OzellikTip.ToList());
        }
        [HttpPost]
        public ActionResult OzellikDegerEkle(OzellikDeger odeger)
            
        {
            ViewBag.OzellikTipleri = Context.Baglanti.OzellikTip.ToList();
            Context.Baglanti.OzellikDeger.Add(odeger);
            Context.Baglanti.SaveChanges();
            return RedirectToAction("OzellikDegerleri");
        }

        public ActionResult UrunOzellikleri()
        {
            return View(Context.Baglanti.UrunOzellik.ToList());
        }

        public ActionResult UrunOzellikEkle(int? Id)
        {
            if (Id != null)
            {
                var data = Context.Baglanti.Urun.Where(x => x.Id == Id).ToList();
                return View(data);
            }
            else
            {
                var data = Context.Baglanti.Urun.ToList();
                return View(data);
            }
            
        }


        public ActionResult UrunOzellikSil(int urunId, int tipId, int degerId)

        {
            UrunOzellik uo = Context.Baglanti.UrunOzellik.FirstOrDefault(x => x.UrunID == urunId && x.OzellikTipID == tipId && x.OzellikDegerID == degerId);
            Context.Baglanti.UrunOzellik.ToList();
            Context.Baglanti.SaveChanges();
            return RedirectToAction("UrunOzellikleri");
        }
        public PartialViewResult UrunOzellikTipWidget(int? katid)//null olabilir
        {
            if(katid != null)
            {
                var data = Context.Baglanti.OzellikTip.Where(x => x.KategoriID == katid).ToList();
                return PartialView(data);
            }
            else
            {
                var data = Context.Baglanti.OzellikTip.ToList();
                return PartialView(data);
            }
        }

        public PartialViewResult UrunOzellikDegerWidget(int? tipid)//null olabilir
        {
            if (tipid != null)
            {
                var data = Context.Baglanti.OzellikDeger.Where(x => x.OzellikTipID == tipid).ToList();
                return PartialView(data);
            }
            else
            {
                var data = Context.Baglanti.OzellikDeger.ToList();
                return PartialView(data);
            }
        }

        public string UrunOzellikTipListele(object list)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append( " < div class='form-group'>   <label class='col-lg-2 control-label'>Özellik</label>"
                       + " <div class='col-lg-10'>   <select class='form-control m-bot15' name='Id' id='urun'> ");
            List<OzellikTip> ot = (List<OzellikTip>)list;
            foreach (OzellikTip tip in ot)
            {
                sb.Append("<option value=" +tip.Id + ">" +   @tip.Adi + "</option>");
                sb.Append("</select></div> </div>");
            }
            return sb.ToString();
        }
        [HttpPost]
        public ActionResult UrunOzellikEkle(UrunOzellik uo)
        {
            Context.Baglanti.UrunOzellik.Add(uo);
            Context.Baglanti.SaveChanges();
            return RedirectToAction("UrunOzellikleri");
        }

        public ActionResult UrunResimEkle(int id)
        {
          //  var data = Context.Baglanti.Urun.FirstOrDefault(x => x.Id == id);
            return View(id);
        }

        [HttpPost]
        public ActionResult UrunResimEkle(int urunId, HttpPostedFileBase fileUpload )
        {
            if (fileUpload != null)
            {
                Image img = Image.FromStream(fileUpload.InputStream);
                Bitmap ortaResim = new Bitmap(img, Settings.UrunOrtaBoyut);
                Bitmap buyukResim = new Bitmap(img, Settings.UrunBuyukBoyut);
                string ortaYol = "/Content/UrunResim/Orta/" + Guid.NewGuid() + Path.GetExtension(fileUpload.FileName);
                string buyukYol = "/Content/UrunResim/Buyuk/" + Guid.NewGuid() + Path.GetExtension(fileUpload.FileName);
                ortaResim.Save(Server.MapPath(ortaYol));
                buyukResim.Save(Server.MapPath(buyukYol));
                Resim rsm = new Resim();
                rsm.OrtaYol = ortaYol;
                rsm.BuyukYol = buyukYol;
                rsm.UrunID = urunId;
                if (Context.Baglanti.Resim.FirstOrDefault(x => x.UrunID == urunId && x.Varsayilan == true) != null)
                    rsm.Varsayilan = true;
                else
                    rsm.Varsayilan = false;
                Context.Baglanti.Resim.Add(rsm);
                Context.Baglanti.SaveChanges();
                return View(urunId);
                
            }
            return View(urunId);
        }

        public ActionResult SliderResimleri()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SliderResimleri(HttpPostedFileBase fileUpload)
        {
            if (fileUpload != null)
            {
                Image img = Image.FromStream(fileUpload.InputStream);
                Bitmap bmp = new Bitmap(img, Settings.SliderResimBoyut);
                string yol = "/Content/SliderResim/" + Guid.NewGuid() + Path.GetExtension(fileUpload.FileName);
                bmp.Save(Server.MapPath(yol));
                Resim rsm = new Resim();
                rsm.BuyukYol = yol;
                Context.Baglanti.Resim.Add(rsm);
                Context.Baglanti.SaveChanges();
            }
            return RedirectToAction("SliderResimleri");
        }

        public ActionResult Roller()
        {
            List<string> roller =  Roles.GetAllRoles().ToList();
            return View(roller);
        }
        
    

        public ActionResult RolEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RolEkle(string RolAdi)
        {
            Roles.CreateRole(RolAdi);
            return RedirectToAction("Roller");
        }

        public ActionResult Kullanicilar()
        {
            MembershipUserCollection users = Membership.GetAllUsers();
            return View(users);
        }

        public ActionResult KullaniciEkle()
        {
            return View();
        }
        //wTNpG57qEdDGX7E
        [HttpPost]
        public ActionResult KullaniciEkle(Kullanici k)
        {
            MembershipCreateStatus durum;
            Membership.CreateUser(k.KullaniciAdi, k.Parola, k.Email, k.GizliSoru, k.GizliCevap, true, out durum);
            string mesaj = "";
            switch (durum)
            {
                case MembershipCreateStatus.Success:
                    break;
                case MembershipCreateStatus.InvalidUserName:
                    mesaj += "Gecersiz kullanici adi";
                    break;
                case MembershipCreateStatus.InvalidPassword:
                    mesaj += "Gecersiz parola";
                    break;
                case MembershipCreateStatus.InvalidQuestion:
                    mesaj += "Gecersiz Gizli Soru";
                    break;
                case MembershipCreateStatus.InvalidAnswer:
                    mesaj += "Gecersiz gizli cevap";
                    break;
                case MembershipCreateStatus.InvalidEmail:
                    mesaj += "Gecersiz email";
                    break;
                case MembershipCreateStatus.DuplicateUserName:
                    mesaj += "Tekrarlanmis kullanici adi";
                    break;
                case MembershipCreateStatus.DuplicateEmail:
                    mesaj += "Kullanilmis mail adresi girildi!";
                    break;
                case MembershipCreateStatus.UserRejected:
                    break;
                case MembershipCreateStatus.InvalidProviderUserKey:
                    break;
                case MembershipCreateStatus.DuplicateProviderUserKey:
                    mesaj += "Kullanilmis key hatasi";
                    break;
                case MembershipCreateStatus.ProviderError:
                    break;
                default:
                    break;
            }
            ViewBag.Mesaj = mesaj;
            if (durum == MembershipCreateStatus.Success)
                return RedirectToAction("Kullanicilar");
            else
                return View();
        }

        [Authorize(Roles="Admin, Moderatör")]
       // [Authorize(Users = "zumray")]
        public ActionResult RolAta()
        {
            ViewBag.Roller = Roles.GetAllRoles().ToList();
            ViewBag.Kullanicilar = Membership.GetAllUsers();
            return View();
        }

        [Authorize(Roles = "Admin, Moderatör")]
        [HttpPost]
        public ActionResult RolAta(string KullaniciAdi, string RolAdi)
        {
       
            Roles.AddUserToRole(KullaniciAdi, RolAdi);
            return RedirectToAction("Kullanicilar");
        }

        [HttpPost]
        public string KullaniciRolleri(string kullaniciadi)
        {
            List<string> roller = Roles.GetRolesForUser(kullaniciadi).ToList();
            string rol = "";
            foreach (string item in roller)
            {
                rol += item + "-";
            }
            rol = rol.Remove(rol.Length - 1, 1);
            return rol;
        }

    }
}