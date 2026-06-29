using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tpv.Migrations
{
    /// <inheritdoc />
    public partial class DescuentoPedidoAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Descuento",
                table: "Pedidos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descuento",
                table: "Pedidos");
        }
    }
}
