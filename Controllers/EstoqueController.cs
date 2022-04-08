using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using bluemarket.Data;
using bluemarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public IActionResult Salvar(Estoque estoqueTemp)
        {
            database.Estoques.Add(estoqueTemp);
            database.SaveChanges();
            return RedirectToAction("Estoque", "Gestao");
        }
    }
}
