using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bluemarket.Data;
using bluemarket.DTO;
using bluemarket.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace bluemarket.Controllers
{
    [Authorize]
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
                produto.Categoria = database.Categorias.First(categoria => categoria.Id == produtoTemporario.CategoriaID);
                produto.Fornecedor = database.Fornecedores.First(fornecedor => fornecedor.Id == produtoTemporario.FornecedorID);
                produto.PrecoDeCusto = float.Parse(produtoTemporario.PrecoDeCustoString, CultureInfo.InvariantCulture.NumberFormat);
                produto.PrecoDeVenda = float.Parse(produtoTemporario.PrecoDeVendaString, CultureInfo.InvariantCulture.NumberFormat);
                produto.Medicao = produtoTemporario.Medicao;
                produto.Status = true;
                database.Produtos.Add(produto);
                database.SaveChanges();
                return RedirectToAction("Produtos", "Gestao");
            }
            else
            {
                ViewBag.Categorias = database.Categorias.ToList();
                ViewBag.Fornecedores = database.Fornecedores.ToList();
                return View("../Gestao/NovoProduto");
            }
        }

        [HttpPost]
        public IActionResult Atualizar(ProdutoDTO produtoTemporario)
        {
            if (ModelState.IsValid)
            {
                var produto = database.Produtos.First(prod => prod.Id == produtoTemporario.Id);
                produto.Nome = produtoTemporario.Nome;
                produto.Categoria = database.Categorias.First(categoria => categoria.Id == produtoTemporario.CategoriaID);
                produto.Fornecedor = database.Fornecedores.First(fornecedor => fornecedor.Id == produtoTemporario.FornecedorID);
                produto.PrecoDeCusto = float.Parse(produtoTemporario.PrecoDeCustoString, CultureInfo.InvariantCulture.NumberFormat);
                produto.PrecoDeVenda = float.Parse(produtoTemporario.PrecoDeVendaString, CultureInfo.InvariantCulture.NumberFormat);
                produto.Medicao = produtoTemporario.Medicao;
                database.SaveChanges();
                return RedirectToAction("Produtos", "Gestao");
            }
            else
            {
                return RedirectToAction("Produtos", "Gestao");
            }
        }

        public IActionResult Deletar(int id)
        {
            if (id > 0)
            {
                var produto = database.Produtos.First(prod => prod.Id == id);
                produto.Status = false;
                database.SaveChanges();
            }
            return RedirectToAction("Produtos", "Gestao");
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
            venda.Total = dados.total;
            venda.Troco = dados.troco;
            venda.ValorPago = dados.troco <= 0.01f ? dados.total : dados.total + dados.troco;
            venda.Data = DateTime.Now;
            database.Vendas.Add(venda);
            database.SaveChanges();

            List<Saida> saidas = new List<Saida>();
            foreach (var saida in dados.produtos)
            {
                Saida s = new Saida();
                s.Quantidade = saida.quantidade;
                s.ValorDaVenda = saida.subtotal;
                s.Venda = venda;
                s.Produto = database.Produtos.First(p => p.Id == saida.produto);
                s.Data = DateTime.Now;
                saidas.Add(s);
            }
            //SALVA NO BANCO
            database.AddRange(saidas);
            database.SaveChanges();
            return Ok(new { msg = "Venda processada com sucesso!!" });
        }

        public class SaidaDTO
        {
            public int produto { get; set; }
            public int quantidade { get; set; }
            public float subtotal { get; set; }
        }

        public class VendaDTO
        {
            public float total { get; set; }
            public float troco { get; set; }
            public SaidaDTO[] produtos { get; set; }
        }
    }
}