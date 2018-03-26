using GameEmprestimo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameEmprestimo.Controllers
{
    [Authorize]
    public class AmigoController : Controller
    {
        // GET: Amigo
        public ActionResult Index()
        {
            return View(new AmigoViewModel());
        }
    }
}