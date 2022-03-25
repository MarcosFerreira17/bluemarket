var enderecoProduto = "https://localhost:5001/Produtos/Produto/";

var produto;
var compra = [];
var __totalVenda__ = 0;

// Inicio

$("#posvenda").hide();

function atualizarTotal()
{
    $("#totalVenda").html(__totalVenda__); 
}

function preencherFormulario(dadosProduto){
    $("#campoNome").val(dadosProduto.nome);
    $("#campoCategoria").val(dadosProduto.categoria.nome);    
    $("#campoFornecedor").val(dadosProduto.fornecedor.nome);
    $("#campoPreco").val(dadosProduto.precoDeVenda);
}

function zerarFormulario(){
    $("#campoNome").val("");
    $("#campoCategoria").val(""); 
    $("#campoFornecedor").val("");
    $("#campoPreco").val("");
    $("#campoQuantidade").val("");
}

function adicionarNaTabela(p, q){
    
    var produtoTemp = {};
    
    Object.assign(produtoTemp,produto);
    
    var venda = {produto: produtoTemp, quantidade: q, subtotal: produtoTemp.precoDeVenda * q};

    __totalVenda__ += venda.subtotal;

    atualizarTotal();

    compra.push(venda);
    $("#compras").append(`
    <tr>
        <td>${p.id}</td>
        <td>${p.nome}</td>
        <td>${q}</td>
        <td>${p.precoDeVenda}</td>
        <td>${p.medicao}</td>
        <td>${p.precoDeVenda * q}</td>
        <td><button class="btn btn-danger">Remover</button></td></td>
    </tr>`);
}

$("#produtoForm").on("submit", function(event){
    event.preventDefault();
    var produtoParaTabela = produto;
    var qtd = $("#campoQuantidade").val();
    
    adicionarNaTabela(produtoParaTabela, qtd);
    zerarFormulario();
});

//Ajax

$("#pesquisar").click(function() {
    var codProduto = $("#codProduto").val();
    var enderecoTemporario = enderecoProduto + codProduto;
    $.post(enderecoTemporario, function(dados, status){

        produto = dados;
        var med = "";
        switch(produto.medicao){
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

    }).fail(function(){
        alert("Produto Inválido.");
    });
});

//Finalização de venda
$("#finalizarVendaBTN").click(function(){
    if(__totalVenda__ <= 0){
        alert("Compra inválida, nenhum produto adicionado.");
        return;
    }

    _valorPago = $("#valorPago").val();
    if(!isNaN(__totalVenda__)){
        _valorPago = parseFloat(_valorPago);
        if(_valorPago >= __totalVenda__){ //Not a number
            var troco = _valorPago - __totalVenda__;
            $("#posvenda").show();
            $("#prevenda").hide();
            $("#valorPago").prop("disabled", true);
            $("#troco").val(troco);


            compra.forEach(elemento => {
                elemento.produto = elemento.produto.id
            });            

            return;
        } else {
            alert("Valor pago inferior ao valor da compra.");
        }
    } else {
        alert("Valor pago inválido, digite somente valore numéricos.");
        return;
    }
});

function restaurarModal(){
    $("#posvenda").hide();
    $("#prevenda").show();
    $("#valorPago").prop("disabled", false);
    $("#troco").val("");
    $("#valorPago").val("");
}

$("#fecharModal").click(function(){
    restaurarModal();
});