using Microsoft.EntityFrameworkCore;
using tpv.Models;

namespace tpv.Data
{
    public class TpvContext : DbContext
    {
        public TpvContext(DbContextOptions<TpvContext> options) : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<LineaPedido> LineaPedidos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
    }
}
