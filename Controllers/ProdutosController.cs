using System.Globalization;
using System.Net;
using System.Linq;
using bluemarket.Data;
using bluemarket.DTO;
using bluemarket.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace bluemarket.Controllers
{
    public class ProdutosController : Controller
    {

        private readonly ApplicationDbContext database;
        public ProdutosController(ApplicationDbContext database)
        {
            this.database = database;
        }

        [HttpPost]
        public IActionResult Salvar(ProdutoDTO produtoTemporario)
        {
            if (ModelState.IsValid)
            {
                Produto produto = new Produto();
                produto.Nome = produtoTemporario.Nome;
                produto.Categoria = database.Categorias.First(categoria => categoria.Id == produtoTemporario.Categoria);
                produto.Fornecedor = database.Fornecedores.First(fornecedor => fornecedor.Id == produtoTemporario.Fornecedor);
                produto.PrecoDeCusto = float.Parse(produtoTemporario.PrecoDeCustoString, CultureInfo.InvariantCulture.NumberFormat);
                produto.PrecoDeVenda = float.Parse(produtoTemporario.PrecoDeVendaString, CultureInfo.InvariantCulture.NumberFormat);
                produto.Medicao = produtoTemporario.Medicao;
                produto.Status = true;
                database.Produtos.Add(produto);
                database.SaveChanges();
                return RedirectToAction("Produtos", "Admin");
            }
            else
            {
                ViewBag.Categorias = database.Categorias.ToList();
                ViewBag.Fornecedores = database.Fornecedores.ToList();
                return View("../Admin/NovoProduto");
            }

        }
        [HttpPost]
        public IActionResult Atualizar(ProdutoDTO produtoTemporario)
        {
            if (ModelState.IsValid)
            {
                var produto = database.Produtos.First(prod => prod.Id == produtoTemporario.Id);
                produto.Nome = produtoTemporario.Nome;
                produto.Categoria = database.Categorias.First(categoria => categoria.Id == produtoTemporario.Categoria);
                produto.Fornecedor = database.Fornecedores.First(fornecedor => fornecedor.Id == produtoTemporario.Fornecedor);
                produto.PrecoDeCusto = float.Parse(produtoTemporario.PrecoDeCustoString, CultureInfo.InvariantCulture.NumberFormat);
                produto.PrecoDeVenda = float.Parse(produtoTemporario.PrecoDeVendaString, CultureInfo.InvariantCulture.NumberFormat);
                produto.Medicao = produtoTemporario.Medicao;
                database.SaveChanges();
                return RedirectToAction("Produtos", "Admin");
            }
            else
            {
                return View("../Admin/EditarProduto");
            }
        }
        [HttpPost]
        public IActionResult Deletar(int id)
        {
            if (id > 0)
            {
                var produto = database.Produtos.First(prod => prod.Id == id);
                produto.Status = false;
                database.SaveChanges();
            }
            return RedirectToAction("Produtos", "Admin");
        }
        [HttpPost]
        public IActionResult Produto(int id)
        {
            if (id > 0)
            {
                var produto = database.Produtos.Where(p => p.Status == true).Include(p => p.Categoria).Include(p => p.Fornecedor).First(p => p.Id == id);

                if (produto != null)
                {
                    var estoque = database.Estoques.First(e => e.Produto.Id == produto.Id);
                    if (estoque == null)
                    {
                        produto = null;
                    }
                }

                if (produto != null)
                {
                    Promocao promocao;
                    try
                    {
                        promocao = database.Promocoes.First(p => p.Produto.Id == id && p.Status == true);
                    }
                    catch (Exception)
                    {
                        promocao = null;
                    }

                    if (promocao != null)
                    {
                        produto.PrecoDeVenda -= (produto.PrecoDeVenda * (promocao.Porcentagem / 100));
                    }

                    Response.StatusCode = 200;
                    return Json(produto);
                }
                else
                {
                    Response.StatusCode = 404;
                    return Json(null);
                }
            }
            Response.StatusCode = 404;
            return Json(null);
        }
        [HttpPost]
        public IActionResult GerarVenda([FromBody] VendaDTO dados)
        {
            //Gerando venda
            Venda venda = new Venda();
            venda.Total = dados.Total;
            venda.Troco = dados.Troco;
            venda.ValorPago = dados.Troco <= 0.01f ? dados.Total : dados.Total + dados.Troco;
            venda.Data = DateTime.Now;
            database.Vendas.Add(venda);
            database.SaveChanges();

            // List<Saida> saidas = new List<Saida>();
            // foreach (var saida in dados.Produtos)
            // {
            //     Saida s = new Saida();
            //     s.Quantidade = saida.Quantidade;
            //     s.ValorDaVenda = saida.Subtotal;
            //     s.Venda = venda;
            //     s.Produto = database.Produtos.First(p => p.Id == saida.Produto);
            //     s.Data = DateTime.Now;
            //     saidas.Add(s);
            // }
            // //SALVA NO BANCO
            // database.AddRange(saidas);
            // database.SaveChanges();
            return Ok(new { msg = "Venda processada com sucesso!!" });
        }

    }


}