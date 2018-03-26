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
    public class EmprestimoController : Controller
    {
        private readonly IAmigoBusiness _amigoBusiness;
        private readonly ITituloBusiness _tituloBusiness;

        public EmprestimoController(IAmigoBusiness amigoBusiness, ITituloBusiness tituloBusiness)
        {
            _amigoBusiness = amigoBusiness;
            _tituloBusiness = tituloBusiness;
        }

        public ActionResult Index()
        {
            return View(new EmprestimoViewModel
            {
                Amigos = _amigoBusiness.Consultar(User.Identity.Name),
                Titulos = _tituloBusiness.ConsultarTituloEmprestado(User.Identity.Name)
            });
        }
    }
}