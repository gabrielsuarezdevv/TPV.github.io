using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tpv.Migrations
{
    /// <inheritdoc />
    public partial class AparcarPedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Aparcado",
                table: "Pedidos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aparcado",
                table: "Pedidos");
        }
    }
}
