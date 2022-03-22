using System.Linq;
using bluemarket.Data;
using bluemarket.DTO;
using bluemarket.Models;
using Microsoft.AspNetCore.Mvc;

namespace bluemarket.Controllers
{
    public class FornecedorController : Controller
    {
        private readonly ApplicationDbContext database;
        public FornecedorController(ApplicationDbContext database)
        {
            this.database = database;
        }

        [HttpPost]
        public IActionResult Salvar(FornecedorDTO fornecedorTemporario)
        {
            if (ModelState.IsValid)
            {
                Fornecedor fornecedor = new Fornecedor();
                fornecedor.Nome = fornecedorTemporario.Nome;
                fornecedor.Email = fornecedorTemporario.Email;
                fornecedor.Telefone = fornecedorTemporario.Telefone;
                fornecedor.Status = true;

                database.Fornecedores.Add(fornecedor);
                database.SaveChanges();

                return RedirectToAction("Fornecedores", "Admin");
            }
            else
            {
                return View("../Admin/NovoFornecedor");
            }
        }

        [HttpPost]
        public IActionResult Atualizar(FornecedorDTO fornecedorTemporario)
        {
            if (ModelState.IsValid)
            {
                var fornecedor = database.Fornecedores.First(forn => forn.Id == fornecedorTemporario.Id);
                fornecedor.Nome = fornecedorTemporario.Nome;
                fornecedor.Email = fornecedorTemporario.Email;
                fornecedor.Telefone = fornecedorTemporario.Telefone;
                database.SaveChanges();
                return RedirectToAction("Fornecedores", "Admin");
            }
            else
            {
                return View("../Admin/EditarFornecedor");
            }
        }
        [HttpPost]
        public IActionResult Deletar(int id)
        {
            if (id > 0)
            {
                var fornecedor = database.Fornecedores.First(forn => forn.Id == id);
                fornecedor.Status = false;
                database.SaveChanges();
            }
            return RedirectToAction("Fornecedores", "Admin");
        }

    }
}