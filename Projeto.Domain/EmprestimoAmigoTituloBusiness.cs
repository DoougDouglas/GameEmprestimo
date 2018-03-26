using Projeto.Data.Interfaces;
using Projeto.Domain.Interfaces;

namespace Projeto.Domain
{
    public class EmprestimoAmigoTituloBusiness : IEmprestimoAmigoTituloBusiness
    {
        private readonly IEmprestimoData _emprestimoData;

        public EmprestimoAmigoTituloBusiness(IEmprestimoData emprestimoData)
        {
            _emprestimoData = emprestimoData;
        }

        public bool VerificarAmigoPossuiEmprestimo(int codigo)
        {
            return _emprestimoData.VerificarAmigoPossuiEmprestimo(codigo);
        }

        public bool VerificarTituloPossuiEmprestimo(int codigo)
        {
            return _emprestimoData.VerificarTituloPossuiEmprestimo(codigo);
        }
    }
}
