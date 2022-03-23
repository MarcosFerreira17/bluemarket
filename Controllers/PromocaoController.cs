using System.Linq;
using bluemarket.Data;
using bluemarket.DTO;
using bluemarket.Models;
using Microsoft.AspNetCore.Mvc;

namespace bluemarket.Controllers
{
    public class PromocaoController : Controller
    {

        private readonly ApplicationDbContext database;
        public PromocaoController(ApplicationDbContext database)
        {
            this.database = database;
        }

        [HttpPost]
        public IActionResult Salvar(PromocaoDTO promocaoTemporaria)
        {
            if (ModelState.IsValid)
            {
                Promocao promocao = new Promocao();
                promocao.Nome = promocaoTemporaria.Nome;
                promocao.Produto = database.Produtos.First(prod => prod.Id == promocaoTemporaria.Produto);
                promocao.Porcentagem = promocaoTemporaria.Porcentagem;
                promocao.Status = true;
                database.Promocoes.Add(promocao);
                database.SaveChanges();
                return RedirectToAction("Promocoes", "Admin");
            }
            else
            {
                ViewBag.Pordutos = database.Produtos.ToList();
                return View("../Admin/NovaPromocao");
            }
        }

        [HttpPost]
        public IActionResult Atualizar(PromocaoDTO promocaoTemporaria)
        {
            if (ModelState.IsValid)
            {
                var promocao = database.Promocoes.First(pro => pro.Id == promocaoTemporaria.Id);
                promocao.Nome = promocaoTemporaria.Nome;
                promocao.Produto = database.Produtos.First(prod => prod.Id == promocaoTemporaria.Produto);
                promocao.Porcentagem = promocaoTemporaria.Porcentagem;
                database.SaveChanges();
                return RedirectToAction("Promocoes", "Admin");
            }
            else
            {
                return View("../Admin/EditarPromocao");
            }
        }

        [HttpPost]
        public IActionResult Deletar(int id)
        {
            if (id > 0)
            {
                var promocao = database.Promocoes.First(prom => prom.Id == id);
                promocao.Status = false;
                database.SaveChanges();
            }
            return RedirectToAction("Promocoes", "Admin");
        }
    }
}