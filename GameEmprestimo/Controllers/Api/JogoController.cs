using Projeto.CrossCutting;
using Projeto.Domain.Entities;
using Projeto.Domain.Interfaces;
using System.Web.Http;

namespace GameEmprestimo.Controllers.Api
{
    [RoutePrefix("Api/Jogo")]
    [Authorize]
    public class JogoController : ApiController
    {
        private readonly ITituloBusiness _tituloBusiness;

        public JogoController(ITituloBusiness tituloBusiness)
        {
            _tituloBusiness = tituloBusiness;
        }

        [HttpGet]
        [Route("GetJogos")]
        public object GetJogos()
        {
            var jogos = _tituloBusiness.Consultar(User.Identity.Name);
            var totalRegistros = jogos != null ? jogos.Count : 0;

            return (new { recordsTotal = totalRegistros, data = jogos });
        }

        [HttpGet]
        [Route("GetJogo")]
        public Titulo GetJogo(int id)
        {
            return _tituloBusiness.Obter(id);
        }

        [HttpPost]
        [Route("PostJogo")]
        public object PostJogo(Titulo jogo)
        {
            if (jogo != null)
            {
                jogo.Usuario = User.Identity.Name;
                try
                {
                    _tituloBusiness.Salvar(jogo);
                    return (new { operacaoConcluidaComSucesso = true });
                }
                catch (ProjetoException pe)
                {
                    return (new { operacaoConcluidaComSucesso = false, mensagem = pe.Message });
                }
            }
            return (new { operacaoConcluidaComSucesso = false });
        }

        [HttpDelete]
        [Route("DeleteJogo")]
        public object DeleteJogo(int id)
        {
            try
            {
                _tituloBusiness.Excluir(id);
                return (new { operacaoConcluidaComSucesso = true });
            }
            catch (ProjetoException pe)
            {
                return (new { operacaoConcluidaComSucesso = false, mensagem = pe.Message });
            }
        }
    }
}
