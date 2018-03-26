using System.Collections.Generic;
using Projeto.CrossCutting;
using Projeto.Data.Interfaces;
using Projeto.Domain.Entities;
using Projeto.Domain.Interfaces;

namespace Projeto.Domain
{
    public class AmigoBusiness : IAmigoBusiness
    {
        private readonly IAmigoData _amigoData;
        private readonly IEmprestimoAmigoTituloBusiness _emprestimoBusiness;

        public AmigoBusiness(IAmigoData amigoData, IEmprestimoAmigoTituloBusiness emprestimoBusiness)
        {
            _amigoData = amigoData;
            _emprestimoBusiness = emprestimoBusiness;
        }

        public Amigo Obter(int codigo)
        {
            return _amigoData.Obter(codigo);
        }

        public List<Amigo> Consultar(string usuario)
        {
            return _amigoData.Consultar(usuario);
        }

        public void Excluir(int codigo)
        {
            if (!_emprestimoBusiness.VerificarAmigoPossuiEmprestimo(codigo))
            {
                _amigoData.Excluir(codigo);
            }
            else
            {
                throw new ProjetoException("Não é possível excluir o amigo, pois ele possui um empréstimo associado.");
            }
        }

        public void Salvar(Amigo amigo)
        {
            if (amigo != null && amigo.Validar())
            {
                if (amigo.Codigo > 0)
                {
                    _amigoData.Atualizar(amigo);
                }
                else
                {
                    _amigoData.Salvar(amigo);
                }
            }
            else
            {
                throw new ProjetoException("Dados inválidos ao salvar o amigo.");
            }
        }
    }
}
