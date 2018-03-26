using GameEmprestimo.Models;
using Projeto.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameEmprestimo.Controllers
{
    [Authorize]
    public class JogoController : Controller
    {

        private readonly IConsoleBusiness _consoleBusiness;

        public JogoController(IConsoleBusiness consoleBusiness)
        {
            _consoleBusiness = consoleBusiness;
        }

        public ActionResult Index()
        {
            var retorno = _consoleBusiness.Listar();
            return View(new JogoViewModel() { Consoles = retorno });
        }
    }
}