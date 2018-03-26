$(function () {
    amigo.carregarTabela();
    amigo.inicializar();
});

var amigo = {

    tabela: null,

    seletores: {
        tabela: "#tabelaAmigos",
        botaoNovo: "#btnCadastrarAmigo"
    },

    inicializar: function () {
        $(amigo.seletores.botaoNovo).on("click", amigo.eventos.onClickNovo);
    },

    eventos: {
        onClickExcluir: function (codigo) {
            bootbox.confirm({
                title: "Confirmação",
                message: "Deseja excluir o amigo selecionado? Essa ação não poderá ser desfeita.",
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
                        amigo.excluirAmigo(codigo);
                    }
                }
            });
        },
        onClickNovo: function () {
            amigo.novoAmigo();
        },
        onClickEditar: function (codigo) {
            amigo.editarAmigo(codigo);
        }
    },

    carregarTabela: function () {
        amigo.tabela = $(amigo.seletores.tabela).dataTable({
            language: {
                url: 'lib/datatables/localisation/Portuguese-Brasil.json',
                emptyTable: "Nenhum registro encontrado."
            },
            "searching": false,
            "ajax":
            {
                "url": "/Api/Amigo/GetAmigos",
                "type": "GET",
                "dataType": "JSON",
                "cache": false
            },
            "columns": [
                { "sTitle": "Nome", "data": "Nome" },
                { "sTitle": "E-mail", "data": "Email" },
                {
                    "sTitle": "Celular", "data": "Celular", "mRender": function (dado) {
                        return dado;
                    }
                },
                {
                    "sTitle": "Ações", "data": "Codigo", "bSort" : "false", "mRender": function (codigo) {
                        return "<button data-toggle='tooltip' title='Excluir' class='btn btn-default' onclick='amigo.eventos.onClickExcluir(" + codigo + ")' ><span class='glyphicon glyphicon-remove'></span></button> &nbsp;" +
                            "<button data-toggle='tooltip' title='Editar' class='btn btn-default' onclick='amigo.eventos.onClickEditar(" + codigo + ")'><span class='glyphicon glyphicon-pencil'></span></button>";
                    }
                }
            ]
        });
    },

    redesenharTabela: function () {
        amigo.tabela.api().ajax.reload();
    },

    excluirAmigo: function (codigo) {
        $.ajax({
            url: '/Api/Amigo/DeleteAmigo?id=' + codigo,
            type: 'DELETE',
            dataType: "JSON",
            success: function (data) {
                if (data.operacaoConcluidaComSucesso) {
                    bootbox.alert("Amigo excluído com sucesso!", function() { amigo.redesenharTabela(); });
                } else {
                    bootbox.alert(data.mensagem);
                }
            }
        });
    },

    novoAmigo: function () {
        modalAmigo.exibirModal();
    },

    editarAmigo: function (codigo) {
        $.ajax({
            url: '/Api/Amigo/GetAmigo?id=' + codigo,
            type: 'GET',
            success: function (data) {
                modalAmigo.exibirModal(data);
            }
        });
    }
}

var modalAmigo =
    {
        seletores: {
            modal: "#modalAmigo",
            hddCodigo: "#hddCodigo",
            txtNome: "#txtNomeAmigo",
            txtEmail: "#txtEmailAmigo",
            txtCelular: "#txtCelularAmigo",
            btnSalvar: "#btnSalvarAmigo",
            form: "#formModalAmigo",
            spanValidar: "#spanRequired"
        },

        inicializar: function () {

            modalAmigo.limparCampos();

            $(modalAmigo.seletores.txtCelular).mask("(99) 99999-9999");

            var btnSalvar = $(modalAmigo.seletores.btnSalvar);
            btnSalvar.off("click");

            btnSalvar.on("click", function () {
                if (modalAmigo.validarForm()) {
                    modalAmigo.eventos.onClickEditarSalvar();
                }
            });
        },

        eventos: {
            onClickEditarSalvar: function () {
                $.ajax({
                    url: '/Api/Amigo/PostAmigo',
                    type: 'POST',
                    dataType: "JSON",
                    data: modalAmigo.recuperarObjetoPreenchido(),
                    success: function (data) {
                        modalAmigo.fecharModal();

                        if (data.operacaoConcluidaComSucesso) {
                            bootbox.alert("Amigo salvo com sucesso!", function () { amigo.redesenharTabela(); });
                        } else {
                            bootbox.alert(data.mensagem);
                        }
                    }
                });
            }
        },

        exibirModal: function (dados) {

            modalAmigo.inicializar();

            if (dados)
                modalAmigo.preencherCampos(dados);

            $(modalAmigo.seletores.modal).modal('show');
        },

        fecharModal: function() {
            $(modalAmigo.seletores.modal).modal('hide');
        },

        limparCampos: function () {
            $(modalAmigo.seletores.modal).find("input[type=text], input[type=tel], input[type=email], input[type=hidden]").val("");
            $(modalAmigo.seletores.modal).find(".has-error").removeClass('has-error');
            $(modalAmigo.seletores.modal).find(".text-danger").remove();
        },

        validarForm: function() {
            var valido = true;
            $(modalAmigo.seletores.form).find(".required").each(function(index) {
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

        preencherCampos: function (amigo) {
            $(modalAmigo.seletores.hddCodigo).val(amigo.Codigo);
            $(modalAmigo.seletores.txtNome).val(amigo.Nome);
            $(modalAmigo.seletores.txtEmail).val(amigo.Email);

            var txtCelular = $(modalAmigo.seletores.txtCelular);
            txtCelular.val(amigo.Celular);
            txtCelular.trigger('input');
        },

        recuperarObjetoPreenchido: function () {
            return {
                Codigo: $(modalAmigo.seletores.hddCodigo).val(),
                Nome: $(modalAmigo.seletores.txtNome).val(),
                Email: $(modalAmigo.seletores.txtEmail).val(),
                Celular: $(modalAmigo.seletores.txtCelular).val()
            }
        }
    }