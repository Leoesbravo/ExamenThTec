using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UsuarioGetByUserName(string userName, string contrasenia)
        {
            ML.Result result = new ML.Result();
            result = BL.Login.GetByUserName(userName);
            if (result.Correct)
            {
                ML.Usuario usuario = (ML.Usuario)result.Object;
                if (usuario.UserName == userName && usuario.Contraseña == contrasenia)
                {
                    return Json(new { redirectToUrl = Url.Action("Index", "Home") });
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
    }
}