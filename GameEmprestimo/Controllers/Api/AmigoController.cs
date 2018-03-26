using Projeto.CrossCutting;
using Projeto.Domain.Entities;
using Projeto.Domain.Interfaces;
using System.Web.Http;

namespace GameEmprestimo.Controllers.Api
{
    [RoutePrefix("Api/Amigo")]
    [Authorize]
    public class AmigoController : ApiController
    {
        private readonly IAmigoBusiness _amigoBusiness;

        public AmigoController(IAmigoBusiness amigoBusiness)
        {
            _amigoBusiness = amigoBusiness;
        }

        [HttpGet]
        [Route("GetAmigos")]
        public object GetAmigos()
        {
            var amigos = _amigoBusiness.Consultar(User.Identity.Name);
            var totalRegistros = amigos != null ? amigos.Count : 0;


            return (new { recordsTotal = totalRegistros, data = amigos });
        }

        [HttpGet]
        [Route("GetAmigo")]
        public Amigo GetAmigo(int id)
        {
            return _amigoBusiness.Obter(id);
        }

        [HttpPost]
        [Route("PostAmigo")]
        public object PostAmigo(Amigo amigo)
        {
            if (amigo != null)
            {
                amigo.Usuario = User.Identity.Name;
                try
                {
                    _amigoBusiness.Salvar(amigo);
                    return (new { operacaoConcluidaComSucesso = true });
                }
                catch (ProjetoException pe)
                {
                    return new { operacaoConcluidaComSucesso = false, mensagem = pe.Message };
                }
            }
            return new { operacaoConcluidaComSucesso = false };
        }

        [HttpDelete]
        [Route("DeleteAmigo")]
        public object DeleteAmigo(int id)
        {
            try
            {
                _amigoBusiness.Excluir(id);
                return new { operacaoConcluidaComSucesso = true };
            }
            catch (ProjetoException pe)
            {
                return new { operacaoConcluidaComSucesso = false, mensagem = pe.Message };
            }
        }
    }
}
