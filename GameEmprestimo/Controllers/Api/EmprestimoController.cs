using Projeto.CrossCutting;
using Projeto.Domain.Entities;
using Projeto.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GameEmprestimo.Controllers.Api
{
    [RoutePrefix("Api/Emprestimo")]
    [Authorize]
    public class EmprestimoController : ApiController
    {
        private readonly IEmprestimoBusiness _emprestimoBusiness;
        private readonly ITituloBusiness _tituloBusiness;


        public EmprestimoController(IEmprestimoBusiness emprestimoBusiness, ITituloBusiness tituloBusiness)
        {
            _emprestimoBusiness = emprestimoBusiness;
            _tituloBusiness = tituloBusiness;
        }

        [HttpGet]
        [Route("GetEmprestimosEmAndamento")]
        public object GetEmprestimosEmAndamento()
        {
            var emprestimos = _emprestimoBusiness.ConsultarEmAndamento(User.Identity.Name);
            var totalRegistros = emprestimos != null ? emprestimos.Count : 0;

            return Json(new { recordsTotal = totalRegistros, data = emprestimos });
        }

        //[HttpGet]
        //[Route("GetEmprestimosFinalizados")]
        //public object GetEmprestimosFinalizados()
        //{
        //    var emprestimos = _emprestimoBusiness.ConsultarFinalizados(User.Identity.Name);
        //    var totalRegistros = emprestimos != null ? emprestimos.Count : 0;

        //    return Json(new { recordsTotal = totalRegistros, data = emprestimos });
        //}

        [HttpGet]
        [Route("GetTitulosDdl")]
        public object GetTitulosDdl()
        {
            var titulosDdl = _tituloBusiness.ConsultarTituloEmprestado(User.Identity.Name);

            return Json(new { titulos = titulosDdl });
        }

        [HttpGet]
        [Route("GetEmprestimo")]
        public object GetEmprestimo(int id)
        {
            return _emprestimoBusiness.Obter(id);
        }

        [HttpPost]
        [Route("SalvarEmprestimo")]
        public object PostSalvarEmprestimo(Emprestimo emprestimo)
        {
            if (emprestimo != null)
            {
                emprestimo.Usuario = User.Identity.Name;

                try
                {
                    _emprestimoBusiness.Salvar(emprestimo);
                    return Json(new { operacaoConcluidaComSucesso = true });
                }
                catch (ProjetoException pe)
                {
                    return Json(new { operacaoConcluidaComSucesso = false, mensagem = pe.Message });
                }
            }
            return Json(new { operacaoConcluidaComSucesso = false });
        }

        [HttpDelete]
        [Route("DeleteEmprestimo")]
        public void DeleteEmprestimo(int id)
        {
            _emprestimoBusiness.Excluir(id);
        }
    }
}
