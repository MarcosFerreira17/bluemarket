using bluemarket.Data;
using bluemarket.DTO;
using bluemarket.Models;
using Microsoft.AspNetCore.Mvc;

namespace bluemarket.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly ApplicationDbContext database;
        public CategoriasController(ApplicationDbContext database)
        {
            this.database = database;
        }

        [HttpPost]
        public IActionResult Salvar(CategoriaDTO categoriaTemporaria)
        {
            if (ModelState.IsValid)
            {
                Categoria categoria = new Categoria();
                categoria.Nome = categoriaTemporaria.Nome;
                categoria.Status = true;
                database.Categorias.Add(categoria);
                database.SaveChanges();
                return RedirectToAction("Categorias", "Admin");
            }
            else
            {
                return View("../Admin/NovaCategoria");
            }
        }
    }
}