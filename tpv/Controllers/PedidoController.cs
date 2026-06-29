using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tpv.Data;
using tpv.Models;

namespace tpv.Controllers
{
    public class PedidoController : Controller
    {
        private readonly TpvContext _context;

        public PedidoController(TpvContext context)
        {
            _context = context;
        }

        public IActionResult AgregarPedido(int Id, int cantidad = 1)
        {
            var pedidoId = HttpContext.Session.GetInt32("PedidoId");

            if (pedidoId == null)
            {
                var pedido = new Pedido();
                pedido.Fecha = DateTime.Now;
                _context.Pedidos.Add(pedido);
                _context.SaveChanges();
                HttpContext.Session.SetInt32("PedidoId", pedido.Id);
            }

            var lineaExistente = _context.LineaPedidos.FirstOrDefault(l => l.PedidoId == pedidoId && l.ProductoId == Id);

            if (lineaExistente == null)
            {
                var linea = new LineaPedido
                {
                    ProductoId = Id,
                    PedidoId = HttpContext.Session.GetInt32("PedidoId").Value,
                    Cantidad = cantidad
                };
                _context.LineaPedidos.Add(linea);
            }
            else
            {
                lineaExistente.Cantidad += cantidad;
            }

            _context.SaveChanges();

            var pedidoActual = _context.Pedidos.Find(HttpContext.Session.GetInt32("PedidoId").Value);
            pedidoActual.Total = _context.LineaPedidos
                .Where(l => l.PedidoId == HttpContext.Session.GetInt32("PedidoId").Value)
                .Include(l => l.Producto)
                .Sum(l => l.Cantidad * l.Producto.Precio);
            _context.Pedidos.Update(pedidoActual);
            _context.SaveChanges();

            return RedirectToAction("Index", "Producto");
        }

        // ─── COBRAR: muestra el ticket y cierra el pedido ─────────────────────────
        public IActionResult CerrarPedido()
        {
            var pedidoId = HttpContext.Session.GetInt32("PedidoId");

            if (pedidoId == null)
                return RedirectToAction("Index", "Producto");

            // Guardar el id antes de limpiar la sesión
            var id = pedidoId.Value;
            HttpContext.Session.Remove("PedidoId");

            // Redirigir al ticket en una nueva ventana (lo maneja el JS del Index)
            return RedirectToAction("Ticket", new { id });
        }

        // ─── TICKET ───────────────────────────────────────────────────────────────
        public IActionResult Ticket(int id)
        {
            var pedido = _context.Pedidos.Find(id);
            if (pedido == null)
                return NotFound();

            ViewBag.Lineas = _context.LineaPedidos
                .Where(l => l.PedidoId == id)
                .Include(l => l.Producto)
                .ToList();

            return View(pedido);
        }

        public IActionResult CancelarPedido()
        {
            var pedidoId = HttpContext.Session.GetInt32("PedidoId");
            if (pedidoId != null)
            {
                var lineas = _context.LineaPedidos.Where(l => l.PedidoId == pedidoId).ToList();
                _context.LineaPedidos.RemoveRange(lineas);
                var pedido = _context.Pedidos.Find(pedidoId);
                if (pedido != null) _context.Pedidos.Remove(pedido);
                _context.SaveChanges();
                HttpContext.Session.Remove("PedidoId");
            }
            return RedirectToAction("Index", "Producto");
        }

        public IActionResult EliminarLinea(int Id)
        {
            var linea = _context.LineaPedidos.Find(Id);
            _context.LineaPedidos.Remove(linea);
            _context.SaveChanges();

            var pedidoActual = _context.Pedidos.Find(HttpContext.Session.GetInt32("PedidoId").Value);
            pedidoActual.Total = _context.LineaPedidos
                .Where(l => l.PedidoId == HttpContext.Session.GetInt32("PedidoId").Value)
                .Include(l => l.Producto)
                .Sum(l => l.Cantidad * l.Producto.Precio);
            _context.Pedidos.Update(pedidoActual);
            _context.SaveChanges();

            return RedirectToAction("Index", "Producto");
        }

        public IActionResult Historial()
        {
            var pedidos = _context.Pedidos.ToList();
            return View(pedidos);
        }

        public IActionResult AplicarDescuento(int descuento)
        {
            var pedidoActual = _context.Pedidos.Find(HttpContext.Session.GetInt32("PedidoId").Value);
            pedidoActual.Descuento = descuento;
            pedidoActual.TotalSinDescuento = pedidoActual.Total;
            var valorDescuento = (pedidoActual.Descuento * pedidoActual.Total) / 100;
            pedidoActual.Total = pedidoActual.TotalSinDescuento - valorDescuento;
            _context.Pedidos.Update(pedidoActual);
            _context.SaveChanges();
            return RedirectToAction("Index", "Producto");
        }

        // ─── APARCAR ──────────────────────────────────────────────────────────────
        public IActionResult AparcarPedido()
        {
            var pedidoId = HttpContext.Session.GetInt32("PedidoId");
            if (pedidoId == null)
                return RedirectToAction("Index", "Producto");

            var tieneLineas = _context.LineaPedidos.Any(l => l.PedidoId == pedidoId);
            if (!tieneLineas)
                return RedirectToAction("Index", "Producto");

            var pedido = _context.Pedidos.Find(pedidoId.Value);
            if (pedido != null)
            {
                pedido.Aparcado = true;
                _context.Pedidos.Update(pedido);
                _context.SaveChanges();
            }

            HttpContext.Session.Remove("PedidoId");
            return RedirectToAction("Index", "Producto");
        }

        // ─── RECUPERAR ────────────────────────────────────────────────────────────
        public IActionResult RecuperarPedido(int id)
        {
            var pedidoActualId = HttpContext.Session.GetInt32("PedidoId");

            if (pedidoActualId != null && pedidoActualId.Value != id)
            {
                var tieneLineas = _context.LineaPedidos.Any(l => l.PedidoId == pedidoActualId);
                if (tieneLineas)
                {
                    var pedidoActual = _context.Pedidos.Find(pedidoActualId.Value);
                    if (pedidoActual != null)
                    {
                        pedidoActual.Aparcado = true;
                        _context.Pedidos.Update(pedidoActual);
                        _context.SaveChanges();
                    }
                }
            }

            var pedidoRecuperar = _context.Pedidos.Find(id);
            if (pedidoRecuperar != null)
            {
                pedidoRecuperar.Aparcado = false;
                _context.Pedidos.Update(pedidoRecuperar);
                _context.SaveChanges();
                HttpContext.Session.SetInt32("PedidoId", id);
            }

            return RedirectToAction("Index", "Producto");
        }
    }
}
