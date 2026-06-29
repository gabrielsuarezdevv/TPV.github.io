using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tpv.Data;
using tpv.Models;

namespace tpv.Controllers
{
    public class ProductoController : Controller
    {
        private readonly TpvContext _context;

        public ProductoController(TpvContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? categoriaId)
        {
            var pedidoId = HttpContext.Session.GetInt32("PedidoId");
            if (pedidoId != null)
            {
                ViewBag.Lineas = _context.LineaPedidos.Where(l => l.PedidoId == pedidoId).Include(l => l.Producto).ToList();
                var pedidoActual = _context.Pedidos.Find(pedidoId);
                ViewBag.Total = pedidoActual.Total;
            }
            var productos = categoriaId.HasValue
                ? _context.Productos.Where(p => p.CategoriaId == categoriaId).ToList()
                : _context.Productos.ToList();
            ViewBag.Categorias = _context.Categorias.ToList();
            ViewBag.CategoriaActual = categoriaId;

            // Pedidos aparcados para mostrarlos en el panel
            ViewBag.PedidosAparcados = _context.Pedidos
                .Where(p => p.Aparcado)
                .OrderByDescending(p => p.Fecha)
                .ToList();

            return View(productos);
        }

        public IActionResult Create()
        {
            ViewBag.Categorias = _context.Categorias.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Producto producto, IFormFile imagen)
        {
            var ruta = Path.Combine("wwwroot/images", imagen.FileName);
            using (var stream = new FileStream(ruta, FileMode.Create))
            {
                imagen.CopyTo(stream);
            }
            producto.Imagen = "/images/" + imagen.FileName;
            _context.Productos.Add(producto);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var producto = _context.Productos.Find(id);
            return View(producto);
        }

        [HttpPost]
        public IActionResult Edit(Producto producto, IFormFile imagen)
        {
            var productoExistente = _context.Productos.Find(producto.Id);

            if (imagen != null)
            {
                var ruta = Path.Combine("wwwroot/images", imagen.FileName);
                using (var stream = new FileStream(ruta, FileMode.Create))
                {
                    imagen.CopyTo(stream);
                }
                productoExistente.Imagen = "/images/" + imagen.FileName;
            }

            productoExistente.Nombre = producto.Nombre;
            productoExistente.Precio = producto.Precio;
            _context.Productos.Update(productoExistente);
            _context.SaveChanges();
            return RedirectToAction("Gestion");
        }
        public IActionResult Delete(int id) 
        {
            var producto = _context.Productos.Find(id);
            _context.Productos.Remove(producto);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Gestion()
        {
            var productos = _context.Productos.ToList();
            return View(productos);
        }
    }
}
