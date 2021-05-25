using FilmsStorage.Models.DAL;
using FilmsStorage.Models.DB;
using FilmsStorage.Models.ViewModels;
using FilmsStorage.SL;
using System.Web.Mvc;

using System.Web.Security;
namespace FilmsStorage.Controllers
{ 
    [Authorize]
    public class ProfileController : BaseController
    {
        // GET: Profile
       
        public ActionResult Index()
        {
            if (base.User == null)
            {
                return RedirectToAction("Login");
            }
            User currentUser = _DAL.Users.ByID(base.User.UserID);
            return View(currentUser);
        }
        [AllowAnonymous]
        public ViewResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                User userByLogin = _DAL.Users.ByLogin(loginModel.LoginName);
                if (userByLogin != null)
                {
                    if (userByLogin.Password == loginModel.Password)
                    {
                        _SL.Users.SetLoginCookie(userByLogin);

                        return RedirectToAction("Index");

                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Password incorrect";
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "No such user";
                }
            }

            return View(loginModel);
        }
        [AllowAnonymous]
        public ViewResult Register()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                User newUser = _DAL.Users.Register(registerModel);
                _SL.Users.SetLoginCookie(newUser);            
                return RedirectToAction("Index");
            }
            else
            {
                return View(registerModel);
            }

        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}