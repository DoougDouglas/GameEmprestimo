namespace Projeto.Domain.Interfaces
{
    public interface IEmprestimoAmigoTituloBusiness
    {
        bool VerificarAmigoPossuiEmprestimo(int codigo);
        bool VerificarTituloPossuiEmprestimo(int codigo);
    }
}
