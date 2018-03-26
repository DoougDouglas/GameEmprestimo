$(function () {
    emprestimo.carregarTabelas();
    emprestimo.inicializar();
});

var emprestimo = {

    tabelaEmAndamento: null,

    tabelaFinalizados: null,

    seletores: {
        tabelaEmAndamento: "#tabelaEmprestimosEmAndamento",
        botaoNovo: "#btnCadastrarEmprestimo",
        ddlTitulo: "#ddlTitulosEmprestimo"
    },

    inicializar: function () {
        $(emprestimo.seletores.botaoNovo).on("click", emprestimo.eventos.onClickNovo);
    },

    eventos: {
        onClickExcluir: function (codigo) {
            bootbox.confirm({
                title: "Confirmação",
                message: "Deseja excluir o empréstimo selecionado? Essa ação não poderá ser desfeita.",
                buttons: {
                    cancel: {
                        label: '<i class="fa fa-times"></i> Cancelar'
                    },
                    confirm: {
                        label: '<i class="fa fa-check"></i> Confirmar'
                    }
                },
                callback: function (result) {
                    if (result) {
                        emprestimo.excluirEmprestimo(codigo);
                    }
                }
            });
        },
        onClickNovo: function () {
            emprestimo.novoEmprestimo();
        },
        onClickEditar: function (codigo) {
            emprestimo.editarEmprestimo(codigo);
        }
    },

    carregarTabelas: function () {
        emprestimo.tabelaEmAndamento = $(emprestimo.seletores.tabelaEmAndamento).dataTable({
            language: {
                url: 'lib/datatables/localisation/Portuguese-Brasil.json',
                emptyTable: "Nenhum registro encontrado."
            },
            "searching": false,
            "ajax":
                {
                    "url": "/Api/Emprestimo/GetEmprestimosEmAndamento",
                    "type": "GET",
                    "dataType": "JSON",
                    "cache": false
                },
            "columns": [
                {
                    "sTitle": "Jogo", "data": "Titulo", "mRender": function (objeto) {
                        return objeto.Nome;
                    }
                },
                {
                    "sTitle": "Amigo", "data": "Amigo", "mRender": function (objeto) {
                        return objeto.Nome;
                    }
                },
                {
                    "sTitle": "Data do empréstimo", "data": "DataEmprestimo", "mRender": function (objeto) {
                        return emprestimo.formatarData(objeto);
                    }
                },
                {
                    "sTitle": "Ações", "data": "Codigo", "mRender": function (codigo) {
                        return "<button data-toggle='tooltip' title='Excluir' class='btn btn-default' onclick='emprestimo.eventos.onClickExcluir(" + codigo + ")' ><span class='glyphicon glyphicon-remove'></span></button> &nbsp;" +
                            "<button data-toggle='tooltip' title='Editar' class='btn btn-default' onclick='emprestimo.eventos.onClickEditar(" + codigo + ")'><span class='glyphicon glyphicon-pencil'></span></button>";
                    }
                }
            ]
        });
    },

    formatarData: function (valor) {
        var data = new Date(valor);
        return data.getDate() + "/" + (data.getMonth() + 1) + "/" + data.getFullYear();
    },

    redesenharTabela: function () {
        emprestimo.tabelaEmAndamento.api().ajax.reload();
        emprestimo.renderizarDropTitulo();
    },

    renderizarDropTitulo: function () {
        $.ajax({
            url: '/Api/Emprestimo/GetTitulosDdl',
            type: 'GET',
            success: function (data) {
                var html = "<select class='form-control required' data-val='true' id='ddlTitulosEmprestimo' name='CodigoTitulo'>";
                html += "<option value>Selecione um Jogo</option>";
                $.map(data.titulos, function (val, i) {
                    html += "<option value=" + data.titulos[i].Codigo + ">" + data.titulos[i].Nome + "</option>";
                });
                html += "</select>";
                $(emprestimo.seletores.ddlTitulo).html(html);
            }
        });
    },

    excluirEmprestimo: function (codigo) {
        $.ajax({
            url: '/Api/Emprestimo/DeleteEmprestimo?id=' + codigo,
            type: 'DELETE',
            success: function () {
                bootbox.alert("Empréstimo excluído com sucesso!", function () { emprestimo.redesenharTabela(); });
            }
        });
    },

    novoEmprestimo: function () {
        modalEmprestimo.exibirModal();
    },

    editarEmprestimo: function (codigo) {
        $.ajax({
            url: '/Api/Emprestimo/GetEmprestimo?id=' + codigo,
            type: 'GET',
            success: function (data) {
                modalEmprestimo.exibirModal(data);
            }
        });
    }
}

var modalEmprestimo =
    {
        seletores: {
            modal: "#modalEmprestimo",
            hddCodigo: "#hddCodigo",
            ddlAmigosEmprestimo: "#ddlAmigosEmprestimo",
            ddlTitulosEmprestimo: "#ddlTitulosEmprestimo",
            btnSalvar: "#btnSalvarEmprestimo",
            form: "#formModalEmprestimo"
        },

        inicializar: function () {
            modalEmprestimo.limparCampos();

            var btnSalvar = $(modalEmprestimo.seletores.btnSalvar);
            btnSalvar.off("click");

            btnSalvar.on("click", function () {
                if (modalEmprestimo.validarForm()) {
                    modalEmprestimo.eventos.onClickEditarSalvar();
                }
            });
        },

        eventos: {
            onClickEditarSalvar: function () {
                $.ajax({
                    url: '/Api/Emprestimo/SalvarEmprestimo',
                    type: 'POST',
                    dataType: "JSON",
                    data: modalEmprestimo.recuperarObjetoPreenchido(),
                    success: function (data) {
                        modalEmprestimo.fecharModal();

                        if (data.operacaoConcluidaComSucesso) {
                            bootbox.alert("Empréstimo salvo com sucesso!", function () { emprestimo.redesenharTabela(); });
                        } else {
                            bootbox.alert(data.mensagem);
                        }
                    }
                });
            }
        },

        exibirModal: function (dados) {
            modalEmprestimo.inicializar();

            if (dados)
                modalEmprestimo.preencherCampos(dados);

            $(modalEmprestimo.seletores.modal).modal('show');
        },

        fecharModal: function () {
            $(modalEmprestimo.seletores.modal).modal('hide');
        },

        limparCampos: function () {
            $(modalEmprestimo.seletores.ddlAmigosEmprestimo + ' option:first').prop('selected', true);
            $(modalEmprestimo.seletores.ddlTitulosEmprestimo + ' option:first').prop('selected', true);
            $(modalEmprestimo.seletores.modal).find(".has-error").removeClass('has-error');
            $(modalEmprestimo.seletores.modal).find(".text-danger").remove();
        },

        validarForm: function () {
            var valido = true;
            $(modalEmprestimo.seletores.form).find(".required").each(function (index) {
                if ($(this).val().trim() === "") {
                    $(this).closest('div').addClass('has-error');
                    if ($($(this).next()).attr('id') != 'spanRequired') {
                        $(this).parent().append('<span id="spanRequired" class="text-danger field-validation-error">O campo ' + $(this).attr('name') + ' é obrigatório.</span>')
                    }
                    valido = false;
                } else {
                    $(this).closest('div').removeClass('has-error');
                    $($(this).next()).hide();
                }
            });
            return valido;
        },

        preencherCampos: function (emprestimo) {
            $(modalEmprestimo.seletores.hddCodigo).val(emprestimo.Codigo);
            $(modalEmprestimo.seletores.ddlAmigosEmprestimo).val(emprestimo.Amigo.Codigo);
            $(modalEmprestimo.seletores.ddlAmigosEmprestimo).attr("disabled",true);
            $(modalEmprestimo.seletores.ddlTitulosEmprestimo).val("");
        },

        recuperarObjetoPreenchido: function () {
            return {
                Codigo: $(modalEmprestimo.seletores.hddCodigo).val(),
                Amigo: {
                    Codigo: $(modalEmprestimo.seletores.ddlAmigosEmprestimo).val()
                },
                Titulo: {
                    Codigo: $(modalEmprestimo.seletores.ddlTitulosEmprestimo).val()
                }
            }
        }
    }