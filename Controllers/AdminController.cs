using System;
using System.Net;
using System.Linq;
using bluemarket.Data;
using bluemarket.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bluemarket.Models;

namespace bluemarket.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext database;
        public AdminController(ApplicationDbContext database)
        {
            this.database = database;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Categorias()
        {
            var categorias = database.Categorias.Where(cat => cat.Status == true).ToList();
            return View(categorias);
        }

        public IActionResult NovaCategoria()
        {
            return View();
        }

        public IActionResult EditarCategoria(int id)
        {
            var categoria = database.Categorias.First(cat => cat.Id == id);
            CategoriaDTO categoriaView = new CategoriaDTO();
            categoriaView.Id = categoria.Id;
            categoriaView.Nome = categoria.Nome;
            return View(categoriaView);
        }
        public IActionResult Fornecedores()
        {
            var forncedores = database.Fornecedores.Where(forn => forn.Status == true).ToList();
            return View(forncedores);
        }

        public IActionResult NovoFornecedor()
        {
            return View();
        }

        public IActionResult EditarFornecedor(int id)
        {
            var fornecedor = database.Fornecedores.First(forn => forn.Id == id);
            FornecedorDTO fornecedorView = new FornecedorDTO();
            fornecedorView.Id = fornecedor.Id;
            fornecedorView.Nome = fornecedor.Nome;
            fornecedorView.Email = fornecedor.Email;
            fornecedorView.Telefone = fornecedor.Telefone;
            return View(fornecedorView);
        }


        public IActionResult Produtos()
        {
            var produtos = database.Produtos.Include(p => p.Categoria).Include(p => p.Fornecedor).Where(p => p.Status == true).ToList();
            return View(produtos);
        }

        public IActionResult NovoProduto()
        {
            ViewBag.Categorias = database.Categorias.ToList();
            ViewBag.Fornecedores = database.Fornecedores.ToList();
            return View();
        }

        public IActionResult EditarProduto(int id)
        {
            var produto = database.Produtos.Include(p => p.Categoria).Include(p => p.Fornecedor).First(prod => prod.Id == id);

            ProdutoDTO produtoView = new ProdutoDTO();

            produtoView.Id = produto.Id;
            produtoView.Nome = produto.Nome;
            produtoView.Categoria = produto.Categoria.Id;
            produtoView.Fornecedor = produto.Fornecedor.Id;
            produtoView.PrecoDeCustoString = produto.PrecoDeCusto.ToString();
            produtoView.PrecoDeVendaString = produto.PrecoDeVenda.ToString();
            produtoView.Medicao = produto.Medicao;
            ViewBag.Categorias = database.Categorias.ToList();
            ViewBag.Fornecedores = database.Fornecedores.ToList();

            return View(produtoView);
        }

        public IActionResult Promocoes()
        {
            var promocoes = database.Promocoes.Include(p => p.Produto).Where(p => p.Status == true).ToList();
            return View(promocoes);
        }

        public IActionResult NovaPromocao()
        {
            ViewBag.Produtos = database.Produtos.ToList();
            return View();
        }

        public IActionResult EditarPromocao(int id)
        {
            var promocao = database.Promocoes.Include(p => p.Produto).First(forn => forn.Id == id);

            PromocaoDTO promocaoView = new PromocaoDTO();
            promocaoView.Id = promocao.Id;
            promocaoView.Nome = promocao.Nome;
            promocaoView.Produto = promocao.Produto.Id;
            promocaoView.Porcentagem = promocao.Porcentagem;
            ViewBag.Produtos = database.Produtos.ToList();
            return View(promocaoView);
        }

        public IActionResult Estoques()
        {
            var estoque = database.Estoques.Include(p => p.Produto).ToList();
            return View(estoque);
        }
        public IActionResult NovoEstoque()
        {
            ViewBag.Produtos = database.Produtos.ToList();
            return View();
        }

        public IActionResult EditarEstoque(int id)
        {
            var estoque = database.Estoques.Include(p => p.Produto).First(e => e.Id == id);
            ViewBag.Produtos = database.Produtos.ToList();
            return View(estoque);
        }

        public IActionResult Vendas()
        {
            return View();
        }
    }
}