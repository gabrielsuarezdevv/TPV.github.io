using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tpv.Migrations
{
    /// <inheritdoc />
    public partial class FamiliaProductosAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Familia",
                table: "Productos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Familia",
                table: "Productos");
        }
    }
}
