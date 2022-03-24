using System.Linq;
using System.Net.Mime;
using bluemarket.Data;
using bluemarket.DTO;
using bluemarket.Models;
using Microsoft.AspNetCore.Mvc;

namespace bluemarket.Controllers
{
    public class EstoqueController : Controller
    {
        private readonly ApplicationDbContext database;

        public EstoqueController(ApplicationDbContext database)
        {
            this.database = database;
        }

        [HttpPost]
        public IActionResult Salvar(Estoque estoqueTemporario)
        {
            database.Estoques.Add(estoqueTemporario);
            database.SaveChanges();
            return RedirectToAction("Estoques", "Admin");
        }

        [HttpPost]
        public IActionResult Atualizar(Estoque estoqueTemporario)
        {
            database.Update(estoqueTemporario);
            database.SaveChanges();
            return RedirectToAction("Estoques", "Admin");
        }

        // [HttpPost]
        // public IActionResult Deletar(int id)
        // {
        //     if (id > 0)
        //     {
        //         var estoque = database.Estoques.First(est => est.Id == id);
        //         estoque.Status = false;
        //         database.SaveChanges();
        //     }
        //     else
        //     {
        //         return RedirectToAction("Estoques", "Admin");
        //     }

        // }


    }
}