namespace tpv.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public int Descuento { get; set; }
        public decimal TotalSinDescuento { get; set; }
        public bool Aparcado { get; set; } = false;
    }
}
