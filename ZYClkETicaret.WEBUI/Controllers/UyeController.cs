using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ZYClkETicaret.WEBUI.App_Classes;
using ZYClkETicaret.WEBUI.Models;

namespace ZYClkETicaret.WEBUI.Controllers
{
    [Authorize]
    public class UyeController : Controller
    {

        [AllowAnonymous]
        // GET: Uye
        public ActionResult GirisYap()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult GirisYap(Kullanici k, string Hatirla)
        {
            bool sonuc = Membership.ValidateUser(k.KullaniciAdi, k.Parola);
            if (sonuc)
            {
                if (Hatirla == "on")
                    FormsAuthentication.RedirectFromLoginPage(k.KullaniciAdi, true);
                else
                    FormsAuthentication.RedirectFromLoginPage(k.KullaniciAdi, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Mesaj = "Kullanici adi yada parola hatali!!!";
                return View();
            }
        }
        public ActionResult CikisYap()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index" , "Home");
        }

        [AllowAnonymous]
        public ActionResult KayitOl()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult KayitOl(Musteri m, Kullanici k)
        {
            MembershipCreateStatus durum;
            Membership.CreateUser(k.KullaniciAdi, k.Parola, k.Email, k.GizliSoru, k.GizliCevap, true, out durum);
            MembershipUser mu = Membership.GetUser(k.KullaniciAdi);
            string userId = mu.ProviderUserKey.ToString();
            m.Id = new Guid(userId);
           
            Roles.AddUserToRole(m.KullaniciAdi, "Musteri");
            Context.Baglanti.Musteri.Add(m);
            Context.Baglanti.SaveChanges();
            return  RedirectToAction("GirisYap","Uye");
        }

        [AllowAnonymous]
        public ActionResult ParolamiUnuttum()
        {
            return View();
        }  
        [AllowAnonymous]
        [HttpPost]
        public ActionResult ParolamiUnuttum(Kullanici k, string ParolaTekrar)
        {

            if (k.Parola.Equals(ParolaTekrar))
            {
                MembershipUser mu = Membership.GetUser(k.KullaniciAdi);
                
                if (mu.PasswordQuestion == k.GizliSoru)
                {

                    try
                    {
                        string pwd = mu.ResetPassword(k.GizliCevap);
                        bool change = mu.ChangePassword(pwd, k.Parola);
                    }
                    catch (Exception)
                    {
                        ViewBag.Mesaj = "Gizli Cevap Yanlistir!!!";
                        return  View();
                    }
                        
                      
                        return RedirectToAction("GirisYap");

                }
                else
                {
                    ViewBag.Mesaj = "Girilen Soru Yanlistir!!!";
                    return View();
                }
            }
            else
            {
                ViewBag.Mesaj = "Parola ve tekrari ayni degil!!!";
                return View();
            }

        }

    }
}