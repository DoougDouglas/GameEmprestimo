using System;
using System.Collections.Generic;
using System.Reflection;
using Projeto.CrossCutting;
using Projeto.Data.Interfaces;
using Projeto.Domain.Entities;
using Projeto.Domain.Interfaces;

namespace Projeto.Domain
{
    public class EmprestimoBusiness : IEmprestimoBusiness
    {
        private readonly IEmprestimoData _emprestimoData;
        private readonly IAmigoBusiness _amigoBusiness;
        private readonly ITituloBusiness _tituloBusiness;

        public EmprestimoBusiness(IEmprestimoData emprestimoData, IAmigoBusiness amigoBusiness, ITituloBusiness tituloBusiness)
        {
            _emprestimoData = emprestimoData;
            _amigoBusiness = amigoBusiness;
            _tituloBusiness = tituloBusiness;
        }

        public Emprestimo Obter(int codigo)
        {
            return _emprestimoData.Obter(codigo);
        }

        public List<Emprestimo> ConsultarEmAndamento(string usuario)
        {
            return _emprestimoData.ConsultarEmAndamento(usuario);
        }

        public void Excluir(int codigo)
        {
            AlterarTituloEmprestado(_emprestimoData.Obter(codigo));
            _emprestimoData.Excluir(codigo);
        }

        public void Salvar(Emprestimo emprestimo)
        {
            if (emprestimo != null && emprestimo.Validar())
            {
                emprestimo.DataEmprestimo = emprestimo.DataEmprestimo == DateTime.MinValue
                    ? DateTime.Now
                    : emprestimo.DataEmprestimo;

                emprestimo.Amigo = _amigoBusiness.Obter(emprestimo.Amigo.Codigo);
                emprestimo.Titulo = _tituloBusiness.Obter(emprestimo.Titulo.Codigo);
                if (emprestimo.Codigo > 0)
                {
                    AlterarTituloEmprestado(emprestimo);
                }
                else
                {
                    _emprestimoData.Salvar(emprestimo);
                }
                if (emprestimo.DataDevolucao == null)
                {
                    emprestimo.Titulo.IsEmprestado = "S";
                    _tituloBusiness.Salvar(emprestimo.Titulo);
                }

            }
            else
            {
                throw new ProjetoException("Dados inválidos ao salvar o empréstimo.");
            }


        }
        private void AlterarTituloEmprestado(Emprestimo emprestimo)
        {
            var emprestimoBD = _emprestimoData.Obter(emprestimo.Codigo);
            var titulo = _tituloBusiness.Obter(emprestimoBD.Titulo.Codigo);

            emprestimoBD.AmigoRefId = emprestimo.Amigo.Codigo;
            emprestimoBD.TituloRefId = emprestimo.Titulo.Codigo;
            _emprestimoData.Atualizar(emprestimoBD);

            titulo.IsEmprestado = "N";
            
            _tituloBusiness.Salvar(titulo);
        }
    }
}
