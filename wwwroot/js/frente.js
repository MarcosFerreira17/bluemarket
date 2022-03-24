
var enderecoProduto = "https://localhost:5001/Produtos/Produto/";

var produto;
var compra = [];
var __totalVenda__ = 0.0;

// Inicio
$("#valorTotal").html(__totalVenda__); 

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
    
    compra.push(produtoTemp);
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