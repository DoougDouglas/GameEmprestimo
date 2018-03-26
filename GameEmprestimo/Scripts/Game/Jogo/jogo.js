$(function () {
    jogo.carregarTabela();
    jogo.inicializar();
});

var jogo = {

    tabela: null,

    seletores: {
        tabela: "#tabelaJogos",
        botaoNovo: "#btnCadastrarJogo"
    },

    inicializar: function () {
        $(jogo.seletores.botaoNovo).on("click", jogo.eventos.onClickNovo);
    },

    eventos: {
        onClickExcluir: function (codigo) {
            bootbox.confirm({
                title: "Confirmação",
                message: "Deseja excluir o jogo selecionado? Essa ação não poderá ser desfeita.",
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
                        jogo.excluirJogo(codigo);
                    }
                }
            });
        },
        onClickNovo: function () {
            jogo.novoJogo();
        },
        onClickEditar: function (codigo) {
            jogo.editarJogo(codigo);
        }
    },

    carregarTabela: function () {
        jogo.tabela = $(jogo.seletores.tabela).dataTable({
            language: {
                url: 'lib/datatables/localisation/Portuguese-Brasil.json',
                emptyTable: "Nenhum registro encontrado."
            },
            "searching": false,
            "ajax":
                {
                    "url": "/Api/Jogo/GetJogos",
                    "type": "GET",
                    "dataType": "JSON",
                    "cache": false
                },
            "columns": [
                { "sTitle": "Nome", "data": "Nome" },
                { "sTitle": "Ano de lançamento", "data": "AnoLancamento" },
                {
                    "sTitle": "Console", "data": "Console", "mRender": function (objeto) {
                        return objeto.Descricao;
                    }
                },
                {
                    "sTitle": "Ações", "data": "Codigo", "mRender": function (codigo) {
                        return "<button data-toggle='tooltip' title='Excluir' class='btn btn-default' onclick='jogo.eventos.onClickExcluir(" + codigo + ")' ><span class='glyphicon glyphicon-remove'></span></button> &nbsp;" +
                            "<button data-toggle='tooltip' title='Editar' class='btn btn-default' onclick='jogo.eventos.onClickEditar(" + codigo + ")'><span class='glyphicon glyphicon-pencil'></span></button>";
                    }
                }
            ]
        });
    },

    redesenharTabela: function () {
        jogo.tabela.api().ajax.reload();
    },

    excluirJogo: function (codigo) {
        $.ajax({
            url: '/Api/Jogo/DeleteJogo?id='+ codigo,
            type: 'DELETE',
            dataType: "JSON",
            success: function (data) {
                if (data.operacaoConcluidaComSucesso) {
                    bootbox.alert("Jogo excluído com sucesso!", function () { jogo.redesenharTabela(); });
                } else {
                    bootbox.alert(data.mensagem);
                }
            }
        });
    },

    novoJogo: function () {
        modalJogo.exibirModal();
    },

    editarJogo: function (codigo) {
        $.ajax({
            url: '/Api/Jogo/GetJogo?id=' + codigo,
            type: 'GET',
            success: function (data) {
                modalJogo.exibirModal(data);
            }
        });
    }
}

var modalJogo =
    {
        seletores: {
            modal: "#modalJogo",
            hddCodigo: "#hddCodigo",
            txtNome: "#txtNomeJogo",
            txtAnoLancamento: "#txtAnoLancamentoJogo",
            ddlConsole: "#ddlConsoleJogo",
            btnSalvar: "#btnSalvarJogo",
            form: "#formModalJogo",
            spanValidar: "#spanRequired"
        },

        inicializar: function () {
            modalJogo.limparCampos();

            $(modalJogo.seletores.txtAnoLancamento).mask("9999");

            var btnSalvar = $(modalJogo.seletores.btnSalvar);
            btnSalvar.off("click");

            btnSalvar.on("click", function () {
                if (modalJogo.validarForm()) {
                    modalJogo.eventos.onClickEditarSalvar();
                }
            });
        },

        eventos: {
            onClickEditarSalvar: function () {
                $.ajax({
                    url: '/Api/Jogo/PostJogo',
                    type: 'POST',
                    dataType: "JSON",
                    data: modalJogo.recuperarObjetoPreenchido(),
                    success: function (data) {
                        modalJogo.fecharModal();

                        if (data.operacaoConcluidaComSucesso) {
                            bootbox.alert("Jogo salvo com sucesso!", function () { jogo.redesenharTabela(); });
                        } else {
                            bootbox.alert(data.mensagem);
                        }
                    }
                });
            }
        },

        exibirModal: function (dados) {
            modalJogo.inicializar();

            if (dados)
                modalJogo.preencherCampos(dados);

            $(modalJogo.seletores.modal).modal('show');
            
        },

        fecharModal: function () {
            $(modalJogo.seletores.modal).modal('hide');
        },

        limparCampos: function () {
            $(modalJogo.seletores.modal).find("input[type=text], input[type=number], input[type=hidden]").val("");
            $(modalJogo.seletores.modal + ' option:first').prop('selected', true);
            $(modalJogo.seletores.modal).find(".has-error").removeClass('has-error');
            $(modalJogo.seletores.modal).find(".text-danger").remove();
        },

        validarForm: function () {
            var valido = true;
            $(modalJogo.seletores.form).find(".required").each(function (index) {

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

        preencherCampos: function (jogo) {
            $(modalJogo.seletores.hddCodigo).val(jogo.Codigo);
            $(modalJogo.seletores.txtNome).val(jogo.Nome);
            $(modalJogo.seletores.ddlConsole).val(jogo.Console.Codigo);

            var txtAnoLancamento = $(modalJogo.seletores.txtAnoLancamento);
            txtAnoLancamento.val(jogo.AnoLancamento);
            txtAnoLancamento.trigger('input');
        },

        recuperarObjetoPreenchido: function () {
            return {
                Codigo: $(modalJogo.seletores.hddCodigo).val(),
                Nome: $(modalJogo.seletores.txtNome).val(),
                AnoLancamento: $(modalJogo.seletores.txtAnoLancamento).val(),
                Console: {
                    Codigo: $(modalJogo.seletores.ddlConsole).val()
                }
            }
        }
    }