using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using IHffA7.Models;
using IHffA7.Models.repositories;
using System.Web.Security;

namespace IHffA7.Controllers
{
    public class AccountController : Controller
    {
        private IAccountRepository accountRepository = new AccountRepository();
        private WhatsUp1617S_martinstinsGenerated ctx = new WhatsUp1617S_martinstinsGenerated();

        //
        // GET: /Account/Login

        public ActionResult Login()
        {
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        public ActionResult Login(LoginModel login_account)
        {
            if (ModelState.IsValid)
            {
                Accounts account = accountRepository.GetAccount(
                    login_account.Emailadres, login_account.Wachtwoord);

                if (account != null)
                {
                    FormsAuthentication.SetAuthCookie(account.Emailadres, false);
                    Session["loggedin_account"] = account;
                    return RedirectToAction("Index", "Contact");
                }
                else
                {
                    ModelState.AddModelError("login-error",
                        "De ingevulde gebruikersnaam of het wachtwoord is incorrect.");
                }
            }
            return View(login_account);
        }

        //
        // POST: /Account/LogOff

        [HttpPost]
        [Authorize]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        ////
        //// GET: /Account/Register

        //public ActionResult Register()
        //{
        //    return View();
        //}

        ////
        //// POST: /Account/Register

        //[HttpPost]
        //public ActionResult Register(Account account)
        //{
        //    // Controleer of emailadres al bestaat:

        //    var checkEmail =
        //        (from entry in ctx.Accounts where (entry.Emailadres == account.Emailadres) select entry).SingleOrDefault();

        //    if (ModelState.IsValid)
        //    {
        //        if (checkEmail == null)
        //        {
        //            //Account aanmaken
        //            accountRepository.AddAccount(account);
        //            //Account aangemaakt...? Ga terug naar de index-pagina
        //            return RedirectToAction("Index", "Contact");
        //        }
        //        else
        //        {
        //            ViewBag.existMessage = "Dit emailadres bestaat al in de database";
        //            return View(account);
        //        }
        //    }
        //    // niet (correct) account-gegevens ingevoerd...? (modelstate = fout) Ga terug naar Registreren-pagina
        //    return View(account);
        //}
    }
}