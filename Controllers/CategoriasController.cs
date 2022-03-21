using bluemarket.DTO;
using Microsoft.AspNetCore.Mvc;

namespace bluemarket.Controllers
{
    public class CategoriasController : Controller
    {
        [HttpPost]
        public IActionResult Salvar(CategoriaDTO categoriaTemporaria)
        {
            return View();
        }
    }
}