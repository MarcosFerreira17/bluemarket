"use strict";

var enderecoProduto = "https://localhost:5001/Produtos/Produto/";
var enderecoGerarVenda = "https://localhost:5001/Produtos/GerarVenda/";
var produto;
var compra = [];
var __totalVenda__ = 0;

var _venda; // Inicio


$("#posvenda").hide();

function atualizarTotal() {
  $("#totalVenda").html(__totalVenda__);
}

function preencherFormulario(dadosProduto) {
  $("#campoNome").val(dadosProduto.nome);
  $("#campoCategoria").val(dadosProduto.categoria.nome);
  $("#campoFornecedor").val(dadosProduto.fornecedor.nome);
  $("#campoPreco").val(dadosProduto.precoDeVenda);
}

function zerarFormulario() {
  $("#campoNome").val("");
  $("#campoCategoria").val("");
  $("#campoFornecedor").val("");
  $("#campoPreco").val("");
  $("#campoQuantidade").val("");
}

function adicionarNaTabela(p, q) {
  var produtoTemp = {};
  Object.assign(produtoTemp, produto);
  _venda = {
    produto: produtoTemp,
    quantidade: q,
    subtotal: produtoTemp.precoDeVenda * q
  };
  __totalVenda__ += _venda.subtotal;
  atualizarTotal();
  compra.push(_venda);
  $("#compras").append("\n    <tr>\n        <td>".concat(p.id, "</td>\n        <td>").concat(p.nome, "</td>\n        <td>").concat(q, "</td>\n        <td>").concat(p.precoDeVenda, "</td>\n        <td>").concat(p.medicao, "</td>\n        <td>").concat(p.precoDeVenda * q, "</td>\n        <td><button class=\"btn btn-danger\">Remover</button></td></td>\n    </tr>"));
}

$("#produtoForm").on("submit", function (event) {
  event.preventDefault();
  var produtoParaTabela = produto;
  var qtd = $("#campoQuantidade").val();
  adicionarNaTabela(produtoParaTabela, qtd);
  zerarFormulario();
}); //Ajax

$("#pesquisar").click(function () {
  var codProduto = $("#codProduto").val();
  var enderecoTemporario = enderecoProduto + codProduto;
  $.post(enderecoTemporario, function (dados, status) {
    produto = dados;
    var med = "";

    switch (produto.medicao) {
      case 0:
        med = "L";
        break;

      case 1:
        med = "K";
        break;

      case 2:
        med = "U";
        break;

      default:
        med = "U";
        break;
    }

    produto.medicao = med;
    preencherFormulario(produto);
  }).fail(function () {
    alert("Produto Inválido.");
  });
}); //Finalização de venda

$("#finalizarVendaBTN").click(function () {
  if (__totalVenda__ <= 0) {
    alert("Compra inválida, nenhum produto adicionado.");
    return;
  }

  _valorPago = $("#valorPago").val();

  if (!isNaN(__totalVenda__)) {
    _valorPago = parseFloat(_valorPago).toFixed(2); //>>>>

    if (_valorPago >= __totalVenda__) {
      //Not a number
      var _troco = _valorPago - __totalVenda__;

      $("#posvenda").show();
      $("#prevenda").hide();
      $("#valorPago").prop("disabled", true);
      $("#troco").val(_troco); //Minimiza o aninhamento de dados dentro do meu array, para otimizar o envio de dados ao backend

      compra.forEach(function (elemento) {
        elemento.produto = elemento.produto.id;
      }); //Ao invés de retornar todos os dados aninhados, irá retornar apenas o id do produto.
      //Preparar um novo objeto.

      _venda = {
        Total: __totalVenda__,
        Troco: _troco,
        Produtos: compra
      }; //Enviar dados para o backend

      $.ajax({
        type: "POST",
        url: enderecoGerarVenda,
        dataType: "json",
        contentType: "application/json",
        data: JSON.stringify(_venda),
        success: function success(data) {
          console.log("dados enviados com sucesso");
          console.log(data);
        }
      });
      return; //Ajax pode mandar solicitações para qualquer método http
    } else {
      alert("Valor pago inferior ao valor da compra.");
      return;
    }
  } else {
    alert("Valor pago inválido, digite somente valore numéricos.");
    return;
  }
});

function restaurarModal() {
  $("#posvenda").hide();
  $("#prevenda").show();
  $("#valorPago").prop("disabled", false);
  $("#troco").val("");
  $("#valorPago").val("");
}

$("#fecharModal").click(function () {
  restaurarModal();
});