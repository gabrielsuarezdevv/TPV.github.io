using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tpv.Data;
using tpv.Models;

namespace tpv.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly TpvContext _context;

        public CategoriaController(TpvContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var categorias = _context.Categorias.ToList();
            return View(categorias);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var categoria = _context.Categorias.Find(id);
            return View(categoria);
        }

        [HttpPost]
        public IActionResult Edit(Categoria categoria, int id)
        {
            var categoriaExistente = _context.Categorias.Find(categoria.Id);
            categoriaExistente.Nombre = categoria.Nombre;
            _context.Categorias.Update(categoriaExistente);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var categoria = _context.Categorias.Find(id);
            _context.Categorias.Remove(categoria);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
