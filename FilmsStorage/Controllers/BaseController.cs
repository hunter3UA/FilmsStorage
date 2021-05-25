using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FilmsStorage.Controllers
{
    // створюємо новий контроллер для перевизначення 
    // об єкта User для встановлення нового додаткового поля UserID
    public class BaseController : Controller
    {
        protected virtual new Login.CustomPrincipal User
        {
            get { return HttpContext.User as Login.CustomPrincipal; }
        }
    }
}